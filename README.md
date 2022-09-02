## SpleeterGUI - Music source separation desktop app  
Windows Desktop Front end for Spleeter - AI source separation  
   
Latest installer can be downloaded from here https://makenweb.com/#spleetergui  
No need to install python or spleeter, this app contains a portable version of python pre-loaded with spleeter.  

The aim for this project is to make it easy for Windows users to download and run Spleeter without needing to use the command line tools to do so.  

![SpleeterGUI_app](/Spleeter_GUI.png)  

This project is a simple C# desktop front end for Spleeter.  
Please consider donating and help pay for hosting and development paypal.me/makenitso  

## Version history  
27/07/2020 - MS Defender is falsely identifying V2.7 as a trojan. The exe has been submitted to Microsoft and given the all clear.  
             Defender definition version 1.319.2309.0 finds no threat
	     More details can be found here https://github.com/boy1dr/SpleeterGui/issues/36

| Date | Version | Notes |
| ----: |:-------:| ----- |
| 5/04/2022 | 2.9.2 | Upgraded spleeter to 2.3.0.   Updated python files, the spleeter core update feature is working again. |
| 30/01/2021 | 2.9.1 | Upgraded spleeter to 2.1.2.   Updated command syntax for latest Spleeter version. |
| 9/11/2020 | 2.9 | Upgraded spleeter to 2.0.1 and python.  |
| 31/07/2020 | 2.8 | Upgraded the project to 64bit. |
| 19/07/2020 | 2.7 | Updated help, set paths for python/ffmpeg (use your own python) |
| 4/07/2020 | 2.6 | Recombine audio and multi-lingual update. |
| 10/05/2020 | 2.5 | UI update, Additional help menu items for version check and spleeter core upgrade, Display installed spleeter version on startup |
| 4/05/2020 | 2.4 | Bug fix: "full bandwidth" mode checked but not enabled by default. |
| 27/12/2019 | 2.3 | Accessibility update (process button, tab order, access labels and descriptions, ding on complete etc.) |
| 24/12/2019 | 2.2 | New windows MSI installer. Drag and drop processing. |
| 21/12/2019 | 2.0 | Interface update, added Batch processing. |
| 17/12/2019 | 1.1 | Added High quality/expert mode. |
  

## Older versions  
Version 2.8 https://makenweb.com/downloads/SpleeterGUI_V2.8.msi  
Version 2.7 https://makenweb.com/downloads/SpleeterGUI_V2.7.msi  

## Examples
https://www.youtube.com/watch?v=bdNzVPLzOLE  
https://www.youtube.com/watch?v=nxJfIsus0Ig  
https://www.youtube.com/watch?v=PHGAmZhuI-c  
https://www.youtube.com/watch?v=3X5nfc2d1Rw  
https://www.youtube.com/watch?v=9KkWJHC2bz0  
https://www.youtube.com/watch?v=Mygm1sFlQXc  

The project contains the C# source code for the Graphical User Interface. It also contains Python3.7 and the Spleeter project.  

Feel free to inspect the source code and build for yourself. you can also install your own python/tensorflow/ffmpeg/spleeter.
