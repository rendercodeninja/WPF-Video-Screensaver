using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace CineScreenSaver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Reference to the core app instance
        private App mAppInstance = ((App)Application.Current);

        private List<string> mVideoFiles;

        //Whether mouse movement to close screen saver is acceptable or not
        private bool mIsMouseMoveAcceptable = false;


        //Class constcutor
        public MainWindow()
        {
            //Initialize the window component
            InitializeComponent();

            //The timeout timer to accept mouse movements
            DispatcherTimer mMouseAcceptTimer = new DispatcherTimer();
            mMouseAcceptTimer.Interval = new TimeSpan(0, 0, 1);
            mMouseAcceptTimer.Tick += OnMouseAcceptTimerTriggered;
            mMouseAcceptTimer.Start();

            //Get the cached folder path
            string folderPath = mAppInstance.GetCachedFoldePath();
            //Get the supported video files from the path
            mVideoFiles = mAppInstance.GetSelectedPathFiles(folderPath, false);

            //If no 
            if (mVideoFiles == null || mVideoFiles.Count == 0)
            {
                //Show the message label
                uMessageLabel.Visibility = Visibility.Visible;
                return;
            }

            //Get a random video url from the list
            uMediaElement.Source = new Uri(mVideoFiles [new Random().Next(mVideoFiles.Count)]);
            //Start playing the video
            uMediaElement.Play();
    }

        //Event will be triggered when
        private void OnMouseAcceptTimerTriggered(object sender, EventArgs e)
        {
            //State mouse movement is acceptable
            mIsMouseMoveAcceptable = true;
        }

        //Event will be triggered when any keyboard input is recieved
        private void OnKeyboardPressed(object sender, KeyEventArgs e)
        {
            //Shutdown the screensaver
            CloseScreenSaver();
        }

        //Event will be triggered when any mouse input is recieved
        private void OnMousePressed(object sender, MouseButtonEventArgs e)
        {
            //Shutdown the screensaver
            CloseScreenSaver();
        }
        
        //Event will be triggered when a mouse move occured
        private void OnMouseMoved(object sender, MouseEventArgs e)
        {
           
            //Return if mouse movement is not acceptable
            if (!mIsMouseMoveAcceptable)
                return;

            //Shutdown the screensaver
            CloseScreenSaver();
        }

        //Function to close the screensaver
        private void CloseScreenSaver()
        {
            //Shutdown the application
            Application.Current.Shutdown();
        }

       
    }
}
