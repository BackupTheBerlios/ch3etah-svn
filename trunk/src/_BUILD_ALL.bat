CALL "%VS71COMNTOOLS%"\vsvars32.bat
DevEnv.com "Libraries\Ch3Etah.Libraries.sln" /build Debug /nologo
REM DevEnv.com "Ch3Etah.sln" /rebuild Debug /nologo
CALL _BUILD.bat
