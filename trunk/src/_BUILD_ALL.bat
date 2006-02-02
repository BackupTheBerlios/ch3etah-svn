del "libraries.build.log"
cls
CALL "%VS71COMNTOOLS%"\vsvars32.bat
DevEnv.com "Libraries\Ch3Etah.Libraries.sln" /build Debug /nologo /out "libraries.build.log"
REM DevEnv.com "Ch3Etah.sln" /rebuild Debug /nologo
CALL _BUILD.bat
