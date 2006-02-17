del "libraries.build.log"
cls
CALL "%VS71COMNTOOLS%"\vsvars32.bat
DevEnv.com "src\Libraries\Ch3Etah.Libraries.sln" /build Debug /nologo /out "src\libraries.build.log"

pause