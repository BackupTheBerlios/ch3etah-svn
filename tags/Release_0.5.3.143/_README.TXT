What is it? 
-----------
CH3ETAH is an extensible IDE and framework for managing small and large-scale code generation projects. Through an abstracted code generation layer, CH3ETAH can support almost any template language, workflows, and even tasks not directly related to code generation.

For more information, check out the project website (http://ch3etah.sourceforge.net).

Compiling and Installation
--------------------------

    CH3ETAH currently requires .NET Framework version 1.1 to run.
    
    To compile for the first time, run the _BUILD_LIBRARIES.BAT file in the root directory. This will make sure the library asseblies referenced by CH3ETAH are built. then you can build CH3ETAH itself. Subsequent builds can be done by simply running _BUILD.BAT or _BUILD_CODESMITH_SUPPORT.BAT (see the note on CodeSmith support below).
    
    * In order to run any of the BAT files that call ch3etah.build, you will need to have NAnt (http://nant.sf.net) installed on your machine and the NAnt install directory in your system Path.
    
    * In order to build the installer, or run any of the build targets that depend on or call it, you will need to install NSIS (http://nsis.sf.net) and place a reference to the NSIS install directory in your system Path.
    
    * In order to run any of the build targets which upload/download content to/from the SourceForge website, you will need to install PuTTY (http://www.chiark.greenend.org.uk/~sgtatham/putty/) and place a reference to the PuTTY install directory in your system Path. You may also need to create a SourceForge login to be able to download, and obviously uploading content requires that you have Admin privileges on the CH3ETAH project space ;).

Note on CodeSmith Support
-------------------------
    This version includes support for CodeSmith templates. However, due to licence restrictions, we cannot redistribute the CodeSmith engine with this application. Because of this, the code necessary for CodeSmith support has not been included in the normal build files. In order to compile CH3ETAH with CodeSmith support, you will need to do the following:
    
    1. Run the previously mentioned _BUILD_LIBRARIES.BAT file.
    2. Download CodeSmith (http://www.codesmithtools.com/)
    3. Place CodeSmith.Engine.dll in the .\src\Libraries\bin directory.
    4. Run _BUILD_CODESMITH_SUPPORT.BAT to compile and build an installer for CH3ETAH with support for CodeSmith.
       4a. Alternately you can build/run using the Ch3Etah.CodeSmithSupport.sln solution in the .\src directory.
