; THIS IS AN NSIS INSTALLER SCRIPT. IN ORDER TO COMPILE THIS SCRIPT YOU
; MUST HAVE NSIS INSTALLED. IT IS OPEN SOURCE AND CAN BE DOWNLOADED FROM
; http://nsis.sourceforge.net/
;
; ALSO, TO CREATE THE INSTALLER AS PART OF A NANT SCRIPT, THE NSIS
; INSTALATION DIRECTORY WILL NEED TO BE IN THE 'PATH' SYSTEM VARIABLE. TO 
; DO THIS IN WINDOWS XP, RIGHT-CLICK ON 'MY COMPUTER', SELECT 'PROPERTIES',
; CLICK ON THE 'ADVANCED' TAB, AND THEN ON 'ENVIRONMENT VARIABLES'.


!define PRODUCT_NAME "CH3ETAH"
!ifndef PRODUCT_VERSION
  !define PRODUCT_VERSION "0.0.0"
!endif
!ifndef RELEASE_MODE
  !define RELEASE_MODE "Debug"
!endif
!define PRODUCT_PUBLISHER "Jacob Eggleston"
!define PRODUCT_WEB_SITE "http://ch3etah.sourceforge.net"
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\Ch3EtahGui.exe"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"
!define PRODUCT_STARTMENU_REGVAL "NSIS:StartMenuDir"
!define PRODUCT_SOURCE_PATH "..\Ch3EtahGui\bin\Debug\"
!ifndef INSTALLER_PATH
  !define INSTALLER_PATH "${RELEASE_MODE}\${PRODUCT_NAME}-${PRODUCT_VERSION}.exe"
!endif

SetCompressor lzma

; File-type association helper macros
!include "FileAssoc.nsh"
; MUI 1.67 compatible ------
!include "MUI.nsh"

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "orange-install.ico"
!define MUI_UNICON "orange-uninstall.ico"
!define MUI_HEADERIMAGE
!define MUI_HEADERIMAGE_BITMAP "Install_Header.bmp"
!define MUI_HEADERIMAGE_UNBITMAP "Uninstall_Header.bmp"
!define MUI_WELCOMEFINISHPAGE_BITMAP "Install_Banner.bmp"
!define MUI_UNWELCOMEFINISHPAGE_BITMAP "Uninstall_Banner.bmp"
!define MUI_UNFINISHPAGE_NOAUTOCLOSE

; Welcome page
!insertmacro MUI_PAGE_WELCOME
; License page
!insertmacro MUI_PAGE_LICENSE "..\_LICENSE.rtf"
; Directory page
!insertmacro MUI_PAGE_DIRECTORY
; Start menu page
var ICONS_GROUP
!define MUI_STARTMENUPAGE_NODISABLE
!define MUI_STARTMENUPAGE_DEFAULTFOLDER "CH3ETAH"
!define MUI_STARTMENUPAGE_REGISTRY_ROOT "${PRODUCT_UNINST_ROOT_KEY}"
!define MUI_STARTMENUPAGE_REGISTRY_KEY "${PRODUCT_UNINST_KEY}"
!define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "${PRODUCT_STARTMENU_REGVAL}"

!define MUI_CUSTOMFUNCTION_GUIINIT onGuiInit
!insertmacro MUI_PAGE_STARTMENU Application $ICONS_GROUP
; Instfiles page
!define MUI_PAGE_CUSTOMFUNCTION_LEAVE "onLeaveFinishPage"
!insertmacro MUI_PAGE_INSTFILES
; Finish page
;!define MUI_FINISHPAGE_RUN "$INSTDIR\Ch3EtahGui.exe"
!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
!insertmacro MUI_UNPAGE_CONFIRM
!define MUI_PAGE_CUSTOMFUNCTION_LEAVE "un.onLeaveFinishPage"
!insertmacro MUI_UNPAGE_INSTFILES
; !insertmacro MUI_UNPAGE_FINISH

; Language files
!insertmacro MUI_LANGUAGE "English"

; Reserve files
!insertmacro MUI_RESERVEFILE_INSTALLOPTIONS

; MUI end ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "${INSTALLER_PATH}"
InstallDir "$PROGRAMFILES\CH3ETAH"
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" ""
ShowInstDetails show
ShowUnInstDetails show

!define DOT_MAJOR "1"
!define DOT_MINOR "1"
Function onGUIInit
  ; make sure the installer isn't already running.
  System::Call 'kernel32::CreateMutexA(i 0, i 0, t "CH3ETAH INSTALLER") i .r1 ?e'
  Pop $R0
  StrCmp $R0 0 +3
    MessageBox MB_OK|MB_ICONEXCLAMATION "The installer is already running."
    Abort

  ; check to make sure the .NET Framework is installed.
  Call IsDotNetInstalled
  
  ; check to see if previous versions were installed using an MSI installer
  Push "{2F9CFD10-78F1-4253-A2CD-05EC7EB771D7}" ; CH3ETAH PRODUCT CODE USED IN PREVIOUS MSI INSTALLERS
  Call UninstallPreviousMSI
FunctionEnd

Function onLeaveFinishPage
  !insertmacro UPDATEFILEASSOC
FunctionEnd

Function un.onLeaveFinishPage
  !insertmacro UPDATEFILEASSOC
FunctionEnd


Section "Program Files" SEC01
  Call IsDotNetInstalled
  SetOutPath "$INSTDIR"
  SetOverwrite try
  File "..\_LICENSE.rtf"
  File "..\_LICENSE.txt"
  File "..\Ch3EtahGui\*.ico"
  File "${PRODUCT_SOURCE_PATH}*.exe"
  File "${PRODUCT_SOURCE_PATH}*.pdb"
  File "${PRODUCT_SOURCE_PATH}*.dll"
  File "${PRODUCT_SOURCE_PATH}*.exe.config"
  File "${PRODUCT_SOURCE_PATH}*.xshd"

  ; Shortcuts
  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
  CreateDirectory "$SMPROGRAMS\$ICONS_GROUP"
  CreateShortCut "$SMPROGRAMS\$ICONS_GROUP\CH3ETAH.lnk" "$INSTDIR\Ch3EtahGui.exe"
  CreateShortCut "$DESKTOP\CH3ETAH.lnk" "$INSTDIR\Ch3EtahGui.exe"
  !insertmacro MUI_STARTMENU_WRITE_END
SectionEnd

;Section "CodeSmith Template Engine" SEC02
;  SetOverwrite ifnewer
;  File "${PRODUCT_SOURCE_PATH}CodeSmith.Engine.dll"
;
;; Shortcuts
;  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
;  !insertmacro MUI_STARTMENU_WRITE_END
;SectionEnd

Section -AdditionalIcons
  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
  WriteIniStr "$INSTDIR\${PRODUCT_NAME} Website.url" "InternetShortcut" "URL" "${PRODUCT_WEB_SITE}"
  CreateShortCut "$SMPROGRAMS\$ICONS_GROUP\${PRODUCT_NAME} Website.lnk" "$INSTDIR\${PRODUCT_NAME} Website.url"
  CreateShortCut "$SMPROGRAMS\$ICONS_GROUP\Uninstall.lnk" "$INSTDIR\uninst.exe"
  !insertmacro MUI_STARTMENU_WRITE_END
SectionEnd

Section -Post
  DetailPrint "Creating uninstaller..."
  WriteUninstaller "$INSTDIR\uninst.exe"
  DetailPrint "Adding application to Add/Remove Programs list..."
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\Ch3EtahGui.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\Ch3EtahGui.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"

  DetailPrint "Creating file type associations..."
  !insertmacro APP_ASSOCIATE "ch3" "Ch3etah.ProjectFile" "CH3ETAH Project File" "$INSTDIR\Project.ico" "Open" "$\"$INSTDIR\Ch3EtahGui.exe$\" $\"%1$\""
  ;!insertmacro APP_ASSOCIATE_ADDVERB "Ch3etah.ProjectFile" "generate" "Generate Code" "$\"$INSTDIR\Ch3EtahGui.exe$\" $\"%1$\" /run"
  DetailPrint ""
  DetailPrint "    Updating explorer icons..."
  DetailPrint "    (THIS MAY TAKE A FEW MOMENTS, PLEASE BE PATIENT)"
  DetailPrint ""
  ;!insertmacro UPDATEFILEASSOC
SectionEnd


Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name) was successfully removed from your computer."
FunctionEnd

Function un.onInit
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "Are you sure you want to completely remove $(^Name) and all of its components?" IDYES +2
  Abort
FunctionEnd

Section Uninstall
  !insertmacro MUI_STARTMENU_GETFOLDER "Application" $ICONS_GROUP
  Delete "$INSTDIR\${PRODUCT_NAME} Website.url"
  Delete "$INSTDIR\uninst.exe"
  Delete "$INSTDIR\_LICENSE.rtf"
  Delete "$INSTDIR\_LICENSE.txt"
  Delete "$INSTDIR\*.ico"
  Delete "$INSTDIR\*.exe"
  Delete "$INSTDIR\*.pdb"
  Delete "$INSTDIR\*.dll"
  Delete "$INSTDIR\*.exe.config"
  Delete "$INSTDIR\*.xshd"

  Delete "$SMPROGRAMS\$ICONS_GROUP\Uninstall.lnk"
  Delete "$SMPROGRAMS\$ICONS_GROUP\${PRODUCT_NAME} Website.lnk"
  Delete "$DESKTOP\CH3ETAH.lnk"
  Delete "$SMPROGRAMS\$ICONS_GROUP\CH3ETAH.lnk"

  RMDir "$SMPROGRAMS\$ICONS_GROUP"
  ;RMDir "$INSTDIR"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  !insertmacro APP_UNASSOCIATE "ch3" "Ch3etah.ProjectFile"
  DetailPrint ""
  DetailPrint "    Updating explorer icons..."
  DetailPrint "    THIS MAY TAKE A FEW MOMENTS, PLEASE BE PATIENT."
  DetailPrint "    THE UNINSTALLER WILL CLOSE AUTOMATICALLY WHEN FINISHED."
  DetailPrint ""
  ;!insertmacro UPDATEFILEASSOC
  SetAutoClose true
SectionEnd


;==================================================================
; check to see if an older version was installed using an MSI
;==================================================================
!define OLDVERSIONWARNING \
  "A previous version of this software was found. The previous version must be uninstalled before proceding.$\n$\nWould you like it to be uninstalled now?"
!define OLDVERSIONREMOVEERROR \
  "There was a problem removing the software. Please try to uninstall it manually."
!define INSTALLSTATE_DEFAULT "5"
!define INSTALLLEVEL_MAXIMUM "0xFFFF"
!define INSTALLSTATE_ABSENT "2"
!define ERROR_SUCCESS "0"
Function UninstallPreviousMSI
  Pop $R0

  System::Call "msi::MsiQueryProductStateA(t '$R0') i.r0"
  StrCmp $0 "${INSTALLSTATE_DEFAULT}" 0 Done

  MessageBox MB_YESNO|MB_ICONQUESTION "${OLDVERSIONWARNING}" \
  IDNO AbortInstall

  System::Call "msi::MsiConfigureProductA(t '$R0', \
    i ${INSTALLLEVEL_MAXIMUM}, i ${INSTALLSTATE_ABSENT}) i.r0"
  StrCmp $0 ${ERROR_SUCCESS} Done

    MessageBox MB_OK|MB_ICONEXCLAMATION \
    "${OLDVERSIONREMOVEERROR}"

  AbortInstall:
    Abort

  Done:
FunctionEnd

;==================================================================
; check to see if the .NET framework is installed
;==================================================================
; Usage
; Define in your script two constants:
;   DOT_MAJOR "(Major framework version)"
;   DOT_MINOR "{Minor frameword version)"
;
; Call IsDotNetInstalled
; This function will abort the installation if the required version
; or higher version of the .NET Framework is not installed.  Place it in
; either your .onInit function or your first install section before
; other code.
Function IsDotNetInstalled

  StrCpy $0 "0"
  StrCpy $1 "SOFTWARE\Microsoft\.NETFramework" ;registry entry to look in.
  StrCpy $2 0

  StartEnum:
    ;Enumerate the versions installed.
    EnumRegKey $3 HKLM "$1\policy" $2

    ;If we don't find any versions installed, it's not here.
    StrCmp $3 "" noDotNet notEmpty

    ;We found something.
    notEmpty:
      ;Find out if the RegKey starts with 'v'.
      ;If it doesn't, goto the next key.
      StrCpy $4 $3 1 0
      StrCmp $4 "v" +1 goNext
      StrCpy $4 $3 1 1

      ;It starts with 'v'.  Now check to see how the installed major version
      ;relates to our required major version.
      ;If it's equal check the minor version, if it's greater,
      ;we found a good RegKey.
      IntCmp $4 ${DOT_MAJOR} +1 goNext yesDotNetReg
      ;Check the minor version.  If it's equal or greater to our requested
      ;version then we're good.
      StrCpy $4 $3 1 3
      IntCmp $4 ${DOT_MINOR} yesDotNetReg goNext yesDotNetReg

    goNext:
      ;Go to the next RegKey.
      IntOp $2 $2 + 1
      goto StartEnum

  yesDotNetReg:
    ;Now that we've found a good RegKey, let's make sure it's actually
    ;installed by getting the install path and checking to see if the
    ;mscorlib.dll exists.
    EnumRegValue $2 HKLM "$1\policy\$3" 0
    ;$2 should equal whatever comes after the major and minor versions
    ;(ie, v1.1.4322)
    StrCmp $2 "" noDotNet
    ReadRegStr $4 HKLM $1 "InstallRoot"
    ;Hopefully the install root isn't empty.
    StrCmp $4 "" noDotNet
    ;build the actuall directory path to mscorlib.dll.
    StrCpy $4 "$4$3.$2\mscorlib.dll"
    IfFileExists $4 yesDotNet noDotNet

  noDotNet:
    ;Nope, something went wrong along the way.  Looks like the
    ;proper .NETFramework isn't installed.
    MessageBox MB_OK "You must have v${DOT_MAJOR}.${DOT_MINOR} or greater of the .NETFramework installed.  Aborting!"
    Abort

  yesDotNet:
    ;Everything checks out.  Go on with the rest of the installation.

FunctionEnd




