del "ch3etah.build.log"
cls
CALL "%VS71COMNTOOLS%"\vsvars32.bat
DevEnv.com "src\Ch3Etah.CodeSmithSupport.sln" /rebuild Debug /nologo /out "src\ch3etah.build.log"
makensis /DPRODUCT_VERSION=0.5.2 src\Installer\Ch3Etah.Installer.nsi

pause