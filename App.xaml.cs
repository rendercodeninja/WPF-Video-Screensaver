using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Forms;
using WaveSim;
using System.Reflection;
using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Application = System.Windows.Application;

namespace CineScreenSaver
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //The cached folder path
        private string mCachedFolderpath = string.Empty;
        //The supported video file formats
        List<string> mSupportedFormats = new List<string>() { ".mp4" };

        //WPF content content for win32form
        private HwndSource winWPFContent;


        //Callback will be triggered on app startup
        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            //Argument to start the screensaver
            if (e.Args.Length == 0 || e.Args[0].ToLower().StartsWith("/s"))
            {
                foreach (Screen s in Screen.AllScreens)
                {
                    if (s != Screen.PrimaryScreen)
                    {
                        Blackout window = new Blackout();
                        window.Left = s.WorkingArea.Left;
                        window.Top = s.WorkingArea.Top;
                        window.Width = s.WorkingArea.Width;
                        window.Height = s.WorkingArea.Height;
                        window.Show();
                    }
                    else
                    {
                        MainWindow window = new MainWindow();
                        window.Left = s.WorkingArea.Left;
                        window.Top = s.WorkingArea.Top;
                        window.Width = s.WorkingArea.Width;
                        window.Height = s.WorkingArea.Height;
                        window.Show();
                    }
                }
            }
            //Argument to preview the screen saver
            else if (e.Args[0].ToLower().StartsWith("/p"))
            {
                MainWindow window = new MainWindow();
                Int32 previewHandle = Convert.ToInt32(e.Args[1]);
                IntPtr pPreviewHnd = new IntPtr(previewHandle);
                RECT lpRect = new RECT();
                bool bGetRect = Win32API.GetClientRect(pPreviewHnd, ref lpRect);

                HwndSourceParameters sourceParams = new HwndSourceParameters("sourceParams");

                sourceParams.PositionX = 0;
                sourceParams.PositionY = 0;
                sourceParams.Height = lpRect.Bottom - lpRect.Top;
                sourceParams.Width = lpRect.Right - lpRect.Left;
                sourceParams.ParentWindow = pPreviewHnd;
                sourceParams.WindowStyle = (int)(WindowStyles.WS_VISIBLE | WindowStyles.WS_CHILD | WindowStyles.WS_CLIPCHILDREN);

                winWPFContent = new HwndSource(sourceParams);
                winWPFContent.Disposed += (o, args) => window.Close();
                winWPFContent.RootVisual = window.RootGrid;
            }

            //Argument to configure the screensaver
            else if (e.Args[0].ToLower().StartsWith("/c"))
            {
                SettingsWindow window = new SettingsWindow();
                window.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                window.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                window.Show();

            }
        }

                
        //Function to update the cached folder path
        public void SetCachedFolderPath(string path)
        {
            //Return if path is empty
            if (string.IsNullOrEmpty(path))
                return;

            //Open HKEY_CURRENT_USER\Software with write access
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);

            //Get the app name and current version
            string appName = Assembly.GetEntryAssembly().GetName().Name;
            string appVersion = Assembly.GetEntryAssembly().GetName().Version.ToString();

            //Create/open a subkey based on app name
            key.CreateSubKey(appName);
            key = key.OpenSubKey(appName, true);

            //Create or open subkey within appname based on app version
            key.CreateSubKey(appVersion);
            key = key.OpenSubKey(appVersion, true);

            //Update folder path value
            key.SetValue("cachePath", path);

        }

        //Function to get teh cached folder path
        public string GetCachedFoldePath()
        {
            //The resultant path string
            string resultPath = string.Empty;

            //Open HKEY_CURRENT_USER\Software
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software");

            //Get the app name and current version
            string appName = Assembly.GetEntryAssembly().GetName().Name;
            string appVersion = Assembly.GetEntryAssembly().GetName().Version.ToString();

            //Create/open a subkey based on app name            
            key = key.OpenSubKey(appName);

            //Create or open subkey within appname based on app version  
            if(key!=null)            
                key = key.OpenSubKey(appVersion);

            //Get the registery value
            if(key!=null)
                resultPath =  key.GetValue("cachePath").ToString();

            //Return the resultant path
            return resultPath;

        }

        //Function to get the selected folder data
        public List<string> GetSelectedPathFiles(string path, bool fileNameOnly)
        {
            //If path empty or directory not found, return null
            if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
                return null;

            //Get the files in the selected directory with supported formats
            List<string> files = Directory.GetFiles(path, "*.*",
              SearchOption.AllDirectories).Where(s => mSupportedFormats.Contains(Path.GetExtension(s))).ToList<string>();
           

            //Get file names only if requested
            if (fileNameOnly)
            {
                for (int i = 0; i < files.Count; i++)
                    files[i] = Path.GetFileName(files[i]);
            }

            //Return the files
            return files;
        }

    }

    
}
