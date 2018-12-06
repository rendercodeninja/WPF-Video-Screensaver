# WPF Video Screensaver
---
A simple screen saver app created in WPF uisng MediaPlayer element. Plays a random video from predefined folder on each launch. Limited to support mp4 format videos (Tested with 1080p 60fps videos). This is a modified version of Wm. Barrett Simms's WPF screensaver ([link](https://wbsimms.com/create-screensaver-net-wpf/))

### Prerequisites
1. Visual Studio 2017 or above
2. Targeted framework - .NET Framework 4.6.1

### Building the Screensaver
* Open the csproject file with visual studio, set the solution configurations to 'Release' and build. 
* Rename the output executable 'CineScreenSaver.exe' in bin/Release folder to 'CineScreenSaver.scr'
* Copy the executable to your System32 folder.

### Cofiguring the Screensaver
Once the screensaver executable is successfully copied to the System32 folder, Select 'Change screen saver' option under 'Power Options' from 'Control Panel'.

Choose 'CineScreenSaver' from the drop down list.

<!-- <p align="center"> -->
![alt text](https://raw.githubusercontent.com/EverCG/WPF-Video-Screensaver/master/git-images/img-changescreensaver.jpg)
<!-- </p> -->

Click 'Settings' and choose a folder with some MP4 video files.

<!-- <p align="center"> -->
![alt text](https://raw.githubusercontent.com/EverCG/WPF-Video-Screensaver/master/git-images/img-screensaversettings.jpg)
<!-- </p> -->

Done! 

Click Preview to view the screensaver.


### Known Issues
If the build executable doesn't run in 'System32' folder but everywhere else, make sure you uncheck 'Prefer 32-bit' in under Project settings and build.