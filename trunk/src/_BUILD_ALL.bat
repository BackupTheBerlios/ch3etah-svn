CALL "%VS71COMNTOOLS%"\vsvars32.bat
DevEnv.com "Libraries\Ch3Etah.Libraries.sln" /build Debug /nologo
DevEnv.com "Ch3Etah.sln" /build Debug /nologo

pause