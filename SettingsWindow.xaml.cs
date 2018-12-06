using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Application = System.Windows.Application;

namespace CineScreenSaver
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {

        //The cached folder path
        private string mCachedFolderPath = string.Empty;

        //Reference to the core app instance
        private App mAppInstance = ((App)Application.Current);

        

        //Class constructor
        public SettingsWindow()
        {
            //Initialize the window component
            InitializeComponent();

            this.Closed += OnSettingsWindowClosed;

            //Get the cached folder path
            mCachedFolderPath = mAppInstance.GetCachedFoldePath();            

            //If path available, update settings view
            if (!string.IsNullOrEmpty(mCachedFolderPath))
                UpdateSettingsView(mCachedFolderPath);
        }

        //Function will be called when the select folder button is clicked
        private void OnSelectFolderClicked(object sender, RoutedEventArgs e)
        {
            //Create a new folder browser dialog
            using (var fbd = new FolderBrowserDialog())
            {
                //Show the newly created folder browser dialog
                DialogResult result = fbd.ShowDialog();

                //If the dialog box OK is pressed and result path is not empty
                if(result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrEmpty(fbd.SelectedPath))
                {
                    //Get the selected path
                    mCachedFolderPath = fbd.SelectedPath;

                    //Update the settings view on dialog close
                    UpdateSettingsView(mCachedFolderPath);


                    

                    


                    
                }
            }
        }

        //Function to update the settings view
        private void UpdateSettingsView(string folderPath)
        {
            //Get the files in at the folder path
            List<string> fileList = mAppInstance.GetSelectedPathFiles(folderPath, true);

            //Update the folder path
            uFolderPath.Text = folderPath;

            //Update the file count
            uFileCount.Content = (fileList.Count > 0 ? "Total video files found : " + fileList.Count : "No supported videos found in folder.") ;

            //Update the file list
            uFileList.ItemsSource = fileList;

        }

        


        //Event will be triggered when this window is closed
        private void OnSettingsWindowClosed(object sender, EventArgs e)
        {
            //Return if folder path is empty
            if (string.IsNullOrEmpty(mCachedFolderPath))
                return;
            
            //Update the new cached path to registery
            mAppInstance.SetCachedFolderPath(mCachedFolderPath);
        }
    }


}
