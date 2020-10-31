using MyJukeboxDbTools.Common;
using MyJukeboxDbTools.DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyJukeboxDbTools.BLL
{
    public class DataGetSet
    {
        public async Task RefillMD5Table()
        {
            List<tMD5> rec = new List<tMD5>();
            using (var context = new MyJukeboxEntities())
            {
                await Task.Run(() =>
                {
                    var result = context.tSongs
                                    .OrderBy(s => s.ID)
                                    .Select(s => new { s.ID, s.Pfad, s.FileName });

                    foreach (var s in result)
                    {
                        string hash = Helpers.MD5($"{s.Pfad}\\{s.FileName}");
                        Console.WriteLine($"ID_Song={s.ID}, md5={hash}");

                        rec.Add(new tMD5
                        {
                            ID_Song = s.ID,
                            MD5 = hash
                        }
                            );
                    }

                    context.tMD5.AddRange(rec);
                    context.SaveChanges();

                    Console.WriteLine($"{rec} records added.");
                });
            }
        }

        public bool TruncateTableMD5()
        {
            try
            {
                var context = new MyJukeboxEntities();
                var result = context.Database.ExecuteSqlCommand("truncate table [tMD5]");

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print($"TruncateTableMD5_Error: {ex.Message}");
                return false;
            }
        }

        public async Task CreareNewDbFullathListAsync()
        {
            var context = new MyJukeboxEntities();

            await Task.Run(() =>
            {
                var result = context.tSongs
                                .Select(s => new { s.Pfad, s.FileName })
                                .OrderBy(s => new { s.Pfad, s.FileName })
                                .ToList();

                if (result == null)
                    return;

                string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\MyJukebox_tSongs.rpt";

                if (File.Exists(documents))
                    File.Delete(documents);

                using (StreamWriter sr = new StreamWriter(documents))
                {
                    foreach (var s in result)
                    {
                        var fullpath = $"{s.Pfad}\\{s.FileName}";
                        sr.WriteLine(fullpath);
                    }
                }
            });
        }

        public async Task CreateNewFileListAsync()
        {
            string startFolder = @"\\win2k16dc01\FS012";
            string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\win2k16dc01_FS012.txt";

            await Task.Run(() =>
            {
                string[] allfiles = Directory.GetFileSystemEntries(startFolder, "*.mp3", SearchOption.AllDirectories);

                File.WriteAllLines(documents, allfiles, System.Text.Encoding.UTF8);

            });
        }

        //public void CreateNewFileList()
        //{
        //    string startFolder = @"\\win2k16dc01\FS012";
        //    string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\win2k16dc01_FS012.txt";

        //    string[] allfiles = Directory.GetFileSystemEntries(startFolder, "*.mp3", SearchOption.AllDirectories);
        //    File.WriteAllLines(documents, allfiles, System.Text.Encoding.UTF8);
        //}
    }
}
