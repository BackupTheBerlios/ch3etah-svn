What is it? 
-----------
CH3ETAH is an extensible IDE and framework for managing small and large-scale code generation projects. Through an abstracted code generation layer, CH3ETAH can support almost any template language, workflows, and even tasks not directly related to code generation.

For more information, check out the project website (http://ch3etah.sourceforge.net).

Compilation and Installation
----------------------------

    CH3ETAH currently requires .NET Framework version 1.1 to run.
    
    To compile for the first time, run the _BUILD_ALL.BAT file in the .\src directory. This will make sure the library asseblies referenced by CH3ETAH are built, then build CH3ETAH itself. Subsequent builds can be done by simply running _BUILD.BAT.
    
    This version includes support for CodeSmith templates. However, due to licence restrictions, we cannot redistribute the CodeSmith engine with this application. Because of this, the code necessary for CodeSmith support has not been included in the build files mentioned above. In order to compile CH3ETAH with CodeSmith support, you will need to do the following:
    
    1. Run the previously mentioned _BUILD_ALL.BAT file.
    2. Download CodeSmith (http://www.codesmithtools.com/)
    3. Copy CodeSmith.Engine.dll to the .\src\Libraries\bin directory.
    4. Compile/run using the Ch3Etah.CodeSmithSupport.sln solution in the .\src directory.
