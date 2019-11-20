# Spleeter_Gui
Windows desktop front end for Spleeter - AI source separation

This project is a simple C# desktop front end for Spleeter.

If you just want to run the program and don't care about building it, you can just grab this ZIP file  
https://drive.google.com/open?id=1a4hGYrezNzjjc_CGfRMWOoPBnf7vuBdL  
extract it, and run SpleeterGui.exe

Here's a quick video running it https://www.youtube.com/watch?v=bdNzVPLzOLE

You WILL need to grab the python files from the google drive and pop it in the bin/debug/python folder. 
or do your own python/spleeter install and tweak the paths in the c# app.

First run....The first run will take a little longer and it will scroll a whole bunch of text in the command window when you hit "process". this is normal. To keep this project small i have not included the working files for spleeter, python will automatically fetch them as needed from the spleeter github project.

Here are some examples of what spleeter can do  
https://www.youtube.com/watch?v=nxJfIsus0Ig  
https://www.youtube.com/watch?v=PHGAmZhuI-c  
https://www.youtube.com/watch?v=3X5nfc2d1Rw  
https://www.youtube.com/watch?v=9KkWJHC2bz0  
https://www.youtube.com/watch?v=Mygm1sFlQXc  

The project contains the C# source code for the Graphical User Interface. It also contains Python3.7 and the Spleeter project.
The aim for this project was to make it easy for Windows users to download and run Spleeter without needing to use the command line tools to do so. 


Feel free to inspect the source code and build for yourself. you can also install your own python/tensorflow/ffmpeg/spleeter, just delete the python directory in the bin/debug folder and follow the guide on the spleeter link below.

For more information on the spleeter project please visit https://github.com/deezer/spleeter

