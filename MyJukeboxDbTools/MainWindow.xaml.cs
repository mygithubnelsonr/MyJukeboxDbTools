using MyJukeboxDbTools.BLL;
using System;
using System.Windows;

namespace MyJukeboxDbTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Move_Window(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private async void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            statusText.Content = "";
            buttonStart.IsEnabled = false;

            try
            {
                if (checkboxFullpathfromdb.IsChecked == true)
                {
                    statusText.Content = "write Database fullpath list...";
                    DataGetSet dataGetSet = new DataGetSet();
                    await dataGetSet.CreareNewDbFullathListAsync();
                    dataGetSet = null;
                }

                if (checkboxFullpathfromfile.IsChecked == true)
                {
                    statusText.Content = "write File fullpath list...";
                    DataGetSet dataGetSet = new DataGetSet();
                    await dataGetSet.CreateNewFileListAsync();
                    dataGetSet = null;
                }
                statusText.Content = "finish";

                buttonStart.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                buttonStart.IsEnabled = true;
            }
        }

    }
}
