del "ch3etah.build.log"
cls
CALL "%VS71COMNTOOLS%"\vsvars32.bat
DevEnv.com "Ch3Etah.CodeSmithSupport.sln" /rebuild Debug /nologo /out "ch3etah.build.log"
makensis /DPRODUCT_VERSION=0.5.2 Installer\Ch3Etah.Installer.nsi

pause