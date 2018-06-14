[![Build status](https://ci.appveyor.com/api/projects/status/github/GREEDY2/GREEDY?svg=true)](https://ci.appveyor.com/project/valentk777/greedy)

# GREEDY
Update 2018-02-06
Valentinas: Sometimes some npm install is missing (if you start from the new project). Need to add it manually like {npm i react-modal-dialog, npm install --save react-alert}

============
Update 2017-12-04
Marius: Facebook login will only work on a specific port and for authorized users only (all of you have been invited for authorization on facebook). You need to change the port of the web on your machine:
1. In solution explorer right click on GREEDY
2. Press Properties
3. Go to Debug
4. Change App url to: http://localhost:6969/
5. Save

============
Update 2017-11-12
Valentinas: TestData moved to GOOGLE DRIVE:
https://drive.google.com/drive/folders/1vJ01rK7XpMIyDPhhu0pDCqWoxjJ8tZk5?usp=sharing

============
Update 2017-11-12
Aidas: How to drop database:
1. Delete GREEDY database in SQL Server Object Explorer > Databases > GREEDY
2. Strat NUGER consol PM>
3. Write this command: PM> entityframework\update-database

============
Update: 2017-10-11
Marius: most of you will need some files and extensions in order for the project to work on you machine. Here is the guide on how to get them:
1. Open Visual Studio
2. Press Tools in the top menu
3. Press Get tools and features...
4. These extensions need to be selected and installed:
    In Web & Cloud - ASP.NET web development, Node.js development
    In Other Toolsets - .NET Core cross-platform development
5. In the summary menu make sure the fields are checked:
    In ASP.NET web devolpment - everything excluding F#
6. Press install and wait for instalation process to finish.
7. Download https://download.microsoft.com/download/0/F/D/0FD852A4-7EA1-4E2A-983A-0484AC19B92C/dotnet-sdk-2.0.0-win-x64.exe
(For windows 10 64bits system*)
8. In https://nodejs.org/en/ download the rocomended for most users version.
9. Install both files.
10. Hope that the project will work on your machine.
