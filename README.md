# Spleeter_Gui
Windows desktop front end for Spleeter - AI source separation  
  
This project is a simple C# desktop front end for Spleeter.  

27/07/2020 - MS Defender is falsely identifying V2.7 as a trojan. The exe has been submitted to microsoft and given the all clear.  
             Defender definition version 1.319.2309.0 finds no threat
	     More details can be found here https://github.com/boy1dr/SpleeterGui/issues/36

Please consider donating and help pay for hosting and development paypal.me/makenitso  
  
19/07/2020 Version 2.7 Updated help, set paths for python/ffmpeg (use your own python)  
 4/07/2020 Version 2.6 Recombine audio and multi-lingual update.  
10/05/2020 Version 2.5 UI update, Additional help menu items for version check and spleeter core ugrade, Display installed spleeter version on startup  
 4/05/2020 Version 2.4 Bug fix: "full bandwidth" mode checked but not enabled by default.  
27/12/2019 Version 2.3 Accessibility update (process button, tab order, access labels and descriptions, ding on complete etc)   
24/12/2019 Version 2.2 New windows MSI installer. Drag and drop processing.  
21/12/2019 Version 2.0 update. Interface update, added Batch processing.  
17/12/2019 Version 1.1 update. Added High quality/expert mode.  
  
Latest installer can be donwloaded from here https://makenweb.com/#spleetergui  

Older versions...  
Version 2.0 https://drive.google.com/open?id=1O6q68IPwPtAgdpK5ZSKiY5-Ew-jmUv9Y  
Version 1.1 https://drive.google.com/open?id=1PlZKGzs0K7da5Rq036qChdz3-_Fc9bpq  
extract it, and run SpleeterGui.exe  


Here's a quick video running it https://www.youtube.com/watch?v=bdNzVPLzOLE  

You WILL need to grab the python files from the google drive and pop it in the bin/debug/python folder.  
or do your own python/spleeter install and update the python location in the expert mode of the GUI.  

First run....The first run will take a little longer and it will scroll a whole bunch of text in the command window when you hit "process". this is normal. To keep this project small i have not included the working files for spleeter, python will automatically fetch them as needed from the spleeter github project.

Here are some examples of what spleeter can do  
https://www.youtube.com/watch?v=nxJfIsus0Ig  
https://www.youtube.com/watch?v=PHGAmZhuI-c  
https://www.youtube.com/watch?v=3X5nfc2d1Rw  
https://www.youtube.com/watch?v=9KkWJHC2bz0  
https://www.youtube.com/watch?v=Mygm1sFlQXc  

The project contains the C# source code for the Graphical User Interface. It also contains Python3.7 and the Spleeter project.  
The aim for this project was to make it easy for Windows users to download and run Spleeter without needing to use the command line tools to do so.  


Feel free to inspect the source code and build for yourself. you can also install your own python/tensorflow/ffmpeg/spleeter.
