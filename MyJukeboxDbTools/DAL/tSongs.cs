//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyJukeboxDbTools.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class tSongs
    {
        public int ID { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public string Titel { get; set; }
        public string Pfad { get; set; }
        public string FileName { get; set; }
        public Nullable<int> ID_Genre { get; set; }
        public Nullable<int> ID_Catalog { get; set; }
        public Nullable<int> ID_Media { get; set; }
    
        public virtual tCatalogs tCatalogs { get; set; }
        public virtual tGenres tGenres { get; set; }
        public virtual tMedia tMedia { get; set; }
    }
}
