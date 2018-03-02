; Script generated by the HM NIS Edit Script Wizard.

; HM NIS Edit Wizard helper defines
!define PRODUCT_NAME "������� �������"
!define PRODUCT_VERSION "1.0"
!define PRODUCT_PUBLISHER "Tess Partners"
!define PRODUCT_WEB_SITE "http://www.tesspartners.com"
!define PRODUCT_DIR_REGKEY "Software\Microsoft\Windows\CurrentVersion\App Paths\LichenSystaw2005.exe"
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

; MUI 1.67 compatible ------
!include "MUI.nsh"

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\modern-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"

; Welcome page
!insertmacro MUI_PAGE_WELCOME
; License page
;!insertmacro MUI_PAGE_LICENSE "c:\path\to\licence\YourSoftwareLicence.txt"
; Directory page
!insertmacro MUI_PAGE_DIRECTORY
; Instfiles page
!insertmacro MUI_PAGE_INSTFILES
; Finish page
!define MUI_FINISHPAGE_RUN "$INSTDIR\LichenSystaw2005.exe"
!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
!insertmacro MUI_UNPAGE_INSTFILES

; Language files
!insertmacro MUI_LANGUAGE "Bulgarian"

; MUI end ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "Setup.exe"
InstallDir "$PROGRAMFILES\Tess Partners\Human Resources"
InstallDirRegKey HKLM "${PRODUCT_DIR_REGKEY}" ""
ShowInstDetails show
ShowUnInstDetails show

Section "MainSection" SEC01
  SetOutPath "$INSTDIR"
  SetOverwrite try
  File "D:\HR2006SVN\bin\Debug\AddressInputControl.dll"
  File "D:\HR2006SVN\bin\Debug\BugBox.dll"
  File "D:\HR2006SVN\bin\Debug\CheckedComboBox.dll"
  File "D:\HR2006SVN\bin\Debug\CheckedNumBox.dll"
  File "D:\HR2006SVN\bin\Debug\CheckedTextBox.dll"
  File "D:\HR2006SVN\bin\Debug\ComboBoxIntelisense.dll"
  File "D:\HR2006SVN\bin\Debug\Config.xml"
  File "D:\HR2006SVN\bin\Debug\DataLayer.dll"
  File "D:\HR2006SVN\bin\Debug\DataLayer.pdb"
  File "D:\HR2006SVN\bin\Debug\Interop.Excel.dll"
  File "D:\HR2006SVN\bin\Debug\Interop.Office.dll"
  File "D:\HR2006SVN\bin\Debug\LichenSystaw2005.exe"
  CreateDirectory "$SMPROGRAMS\������� �������"
  CreateShortCut "$SMPROGRAMS\������� �������\������� �������.lnk" "$INSTDIR\LichenSystaw2005.exe"
  CreateShortCut "$DESKTOP\������� �������.lnk" "$INSTDIR\LichenSystaw2005.exe"
  File "D:\HR2006SVN\bin\Debug\LichenSystaw2005.pdb"
  File "D:\HR2006SVN\bin\Debug\NewTabControl.dll"
  File "D:\HR2006SVN\bin\Debug\NumberBox1.dll"
  File "D:\HR2006SVN\bin\Debug\PassportDataControll.dll"
  File "D:\HR2006SVN\bin\Debug\System.Xml.dll"
SectionEnd

Section "DataBase" SEC02
  SetOutPath "$PROGRAMFILES\MySQL\MySQL Server 4.1\data\hrdb"
  SetOverwrite off
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\absence.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\absence.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\absence.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\admininfo.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\admininfo.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\admininfo.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\category.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\category.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\category.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\contract.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\contract.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\contract.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\country.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\country.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\country.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\db.opt"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\dod.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\dod.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\dod.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\education.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\education.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\education.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\ekda.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\ekda.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\ekda.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\experience.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\experience.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\experience.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\familystatus.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\familystatus.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\familystatus.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\fired.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\fired.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\fired.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\firmpersonal3.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\firmpersonal3.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\firmpersonal3.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\firmstructure.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\firmstructure.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\firmstructure.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\globalpositions.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\globalpositions.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\globalpositions.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\holiday.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\holiday.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\holiday.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\kind.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\kind.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\kind.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\language.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\language.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\language.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\languagelevel.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\languagelevel.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\languagelevel.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\law.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\law.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\law.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\militaryrang.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\militaryrang.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\militaryrang.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\newtree2.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\newtree2.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\newtree2.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\nkid.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\nkid.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\nkid.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\nkp.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\nkp.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\nkp.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\notes.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\notes.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\notes.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\penalty.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\penalty.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\penalty.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\penaltyreason.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\penaltyreason.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\penaltyreason.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\person.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\person.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\person.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\personassignment.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\personassignment.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\personassignment.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\pictures.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\pictures.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\pictures.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\profession.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\profession.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\profession.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\rang.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\rang.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\rang.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\reasonassignment.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\reasonassignment.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\reasonassignment.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\reasonfired.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\reasonfired.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\reasonfired.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\region.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\region.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\region.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\sciencelevel.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\sciencelevel.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\sciencelevel.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\sciencetitle.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\sciencetitle.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\sciencetitle.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\sex.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\sex.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\sex.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\staff.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\staff.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\staff.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\towns.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\towns.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\towns.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\type.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\type.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\type.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\typepenalty.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\typepenalty.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\typepenalty.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\worktime.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\worktime.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\worktime.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\year.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\year.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\year.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\yearlyaddon.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\yearlyaddon.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\yearlyaddon.MYI"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\year_holiday.frm"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\year_holiday.MYD"
  File "G:\Program Files\MySQL\MySQL Server 4.1\data\HRDB2\year_holiday.MYI"
SectionEnd

Section -AdditionalIcons
  SetOutPath $INSTDIR
  WriteIniStr "$INSTDIR\${PRODUCT_NAME}.url" "InternetShortcut" "URL" "${PRODUCT_WEB_SITE}"
  CreateShortCut "$SMPROGRAMS\������� �������\Website.lnk" "$INSTDIR\${PRODUCT_NAME}.url"
  CreateShortCut "$SMPROGRAMS\������� �������\Uninstall.lnk" "$INSTDIR\uninst.exe"
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr HKLM "${PRODUCT_DIR_REGKEY}" "" "$INSTDIR\LichenSystaw2005.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayIcon" "$INSTDIR\LichenSystaw2005.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
SectionEnd


Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "$(^Name) was successfully removed from your computer."
FunctionEnd

Function un.onInit
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 '������� �� ��� �� ������ �� ���������� �������� "������� �������"?' IDYES +2
  Abort
FunctionEnd

Section Uninstall
  Delete "$INSTDIR\${PRODUCT_NAME}.url"
  Delete "$INSTDIR\uninst.exe"
  Delete "$INSTDIR\System.Xml.dll"
  Delete "$INSTDIR\PassportDataControll.dll"
  Delete "$INSTDIR\NumberBox1.dll"
  Delete "$INSTDIR\NewTabControl.dll"
  Delete "$INSTDIR\LichenSystaw2005.pdb"
  Delete "$INSTDIR\LichenSystaw2005.exe"
  Delete "$INSTDIR\Interop.Office.dll"
  Delete "$INSTDIR\Interop.Excel.dll"
  Delete "$INSTDIR\DataLayer.pdb"
  Delete "$INSTDIR\DataLayer.dll"
  Delete "$INSTDIR\Config.xml"
  Delete "$INSTDIR\ComboBoxIntelisense.dll"
  Delete "$INSTDIR\CheckedTextBox.dll"
  Delete "$INSTDIR\CheckedNumBox.dll"
  Delete "$INSTDIR\CheckedComboBox.dll"
  Delete "$INSTDIR\BugBox.dll"
  Delete "$INSTDIR\AddressInputControl.dll"

  Delete "$SMPROGRAMS\������� �������\Uninstall.lnk"
  Delete "$SMPROGRAMS\������� �������\Website.lnk"
  Delete "$DESKTOP\������� �������.lnk"
  Delete "$SMPROGRAMS\������� �������\������� �������.lnk"

  RMDir "$SMPROGRAMS\������� �������"
  RMDir "$INSTDIR"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  DeleteRegKey HKLM "${PRODUCT_DIR_REGKEY}"
  SetAutoClose true
SectionEnd