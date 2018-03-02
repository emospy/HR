; Script generated with the Venis Install Wizard

; Define your application name
!define APPNAME "������� �������"
!define APPNAMEANDVERSION "������� ������� 2.48"

; Main Install settings
Name "${APPNAMEANDVERSION}"
InstallDir "$PROGRAMFILES\Tess Partners\Human Resources\"
InstallDirRegKey HKLM "Software\${APPNAME}" ""
OutFile "D:\WorkEmo\GitWork\HR\Installer\HRSetup.exe"

; Modern interface settings
!include "MUI.nsh"

!define MUI_ABORTWARNING
!define MUI_FINISHPAGE_RUN "$INSTDIR\Human Resources.exe"

!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH

!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

; Set languages (first is default language)
!insertmacro MUI_LANGUAGE "English"
!insertmacro MUI_RESERVEFILE_LANGDLL

Section "������� �������" Section1

	; Set Section properties
	SetOverwrite on

	; Set Section Files and Shortcuts
	SetOutPath "$INSTDIR\"
	File "D:\WorkEmo\GitWork\HR\bin\Release\AddressInputControl.dll"
	File "D:\WorkEmo\GitWork\HR\bin\Release\BugBox.dll"
	File "D:\WorkEmo\GitWork\HR\bin\Release\CheckedComboBox.dll"
	File "D:\WorkEmo\GitWork\HR\bin\Release\CheckedCombo.dll"
	File "D:\WorkEmo\GitWork\HR\bin\Release\CheckedNumBox.dll"
	File "D:\WorkEmo\GitWork\HR\bin\Release\CheckedTextBox.dll"
	File "D:\WorkEmo\GitWork\HR\bin\Release\ComboBoxIntelisense.dll"	
	File "D:\WorkEmo\GitWork\HR\bin\Release\ExcelExport.dll"	
	File "D:\WorkEmo\GitWork\HR\bin\Release\HolidayPlan.dll"
	File "D:\WorkEmo\GitWork\HR\bin\Release\HRDataLayer.dll"
	File "D:\WorkEmo\GitWork\HR\bin\Release\Human Resources.exe"
	File "D:\WorkEmo\GitWork\HR\bin\Release\NewTabControl.dll"
	File "D:\WorkEmo\GitWork\HR\bin\Release\NumberBox1.dll"
	File "D:\WorkEmo\GitWork\HR\bin\Release\office.dll"
	File "D:\WorkEmo\GitWork\HR\bin\Release\PassportDataControll.dll"
	File "D:\WorkEmo\GitWork\HR\bin\Release\SicknessFrame.exe"	
	File "D:\WorkEmo\GitWork\HR\bin\Release\System.Data.dll"	
	File "D:\WorkEmo\GitWork\HR\bin\Release\System.Data.Entity.dll"		
	File "D:\WorkEmo\GitWork\HR\bin\Release\System.dll"
	File "D:\WorkEmo\GitWork\HR\bin\Release\System.Management.dll"
	File "D:\WorkEmo\GitWork\HR\bin\Release\System.Xml.dll"	
	File "D:\WorkEmo\GitWork\HR\bin\Release\Telerik.Windows.Controls.dll"
	File "D:\WorkEmo\GitWork\HR\bin\Release\Telerik.Windows.Controls.GridView.dll"	
	File "D:\WorkEmo\GitWork\HR\bin\Release\Telerik.Windows.Controls.Input.dll"	
	File "D:\WorkEmo\GitWork\HR\bin\Release\Telerik.Windows.Data.dll"		
	
	File "D:\WorkEmo\GitWork\HR\hrdll\Microsoft.SqlServer.ConnectionInfo.dll"
	File "D:\WorkEmo\GitWork\HR\hrdll\Microsoft.SqlServer.Management.Sdk.Sfc.dll"
	File "D:\WorkEmo\GitWork\HR\hrdll\Microsoft.SqlServer.Smo.dll"
	File "D:\WorkEmo\GitWork\HR\hrdll\Microsoft.SqlServer.SmoExtended.dll"
	File "D:\WorkEmo\GitWork\HR\hrdll\Microsoft.SqlServer.SqlEnum.dll"		
	File "D:\WorkEmo\GitWork\HR\hrdll\Microsoft.SqlServer.SqlClrProvider.dll"		
	
	File "D:\WorkEmo\GitWork\HR\DataBaseUpdater\bin\Debug\DataBaseUpdater.exe"
	
	File "D:\WorkEmo\GitWork\HR\hrdll\Interop.Excel.dll"
	File "D:\WorkEmo\GitWork\HR\hrdll\Interop.Excel.dll"
	File "D:\WorkEmo\GitWork\HR\hrdll\Interop.Excel.dll"
	File "D:\WorkEmo\GitWork\HR\hrdll\Interop.Excel.dll"
	File "D:\WorkEmo\GitWork\HR\hrdll\Interop.Excel.dll"
	
	CreateShortCut "$DESKTOP\������� �������.lnk" "$INSTDIR\Human Resources.exe" "$INSTDIR\My Program.exe"
	CreateDirectory "$SMPROGRAMS\������� �������"
	CreateShortCut "$SMPROGRAMS\������� �������\������� �������.lnk" "$INSTDIR\Human Resources.exe" "$INSTDIR\My Program.exe"
	CreateShortCut "$SMPROGRAMS\������� �������\Uninstall.lnk" "$INSTDIR\uninstall.exe"

SectionEnd

Section "Templates" Section2

	; Set Section properties
	SetOverwrite off

	; Set Section Files and Shortcuts
	SetOutPath "$INSTDIR\Templates\"

	File "D:\WorkEmo\GitWork\HR\Templates\dogovor sro4en bez izpit.rtf"
	File "D:\WorkEmo\GitWork\HR\Templates\dogovor.rtf"
	File "D:\WorkEmo\GitWork\HR\Templates\zapoved_za_naznachavane bez izpitanie.rtf"
	File "D:\WorkEmo\GitWork\HR\Templates\zapoved_za_naznachavane s izpitanie.rtf"
	File "D:\WorkEmo\GitWork\HR\Templates\zapoved_za_naznachavane_trudov_dogovor.rtf"
	File "D:\WorkEmo\GitWork\HR\Templates\zapoved_za_prekratjavane.rtf"
	File "D:\WorkEmo\GitWork\HR\Templates\TemplateCharacteristic.rtf"
	File "D:\WorkEmo\GitWork\HR\Templates\cover_letter_u62.rtf"
SectionEnd
	
Section "XLS" Section5
	
	; Set Section properties
	SetOverwrite off
	; Set Section Files and Shortcuts
	SetOutPath "$INSTDIR\"
	File "D:\WorkEmo\GitWork\HR\Templates\TemplateCustom.xls"
	File "D:\WorkEmo\GitWork\HR\Templates\TemplateFree.xls"
	File "D:\WorkEmo\GitWork\HR\Templates\TemplateImportantHoliday.xls"
	File "D:\WorkEmo\GitWork\HR\Templates\TemplateOSR.xls"
	File "D:\WorkEmo\GitWork\HR\Templates\TemplatePSR.xls"
	File "D:\WorkEmo\GitWork\HR\Templates\TemplateZZBUT.xls"
	File "D:\WorkEmo\GitWork\HR\Templates\TemplateAttestations.xls"
	File "D:\WorkEmo\GitWork\HR\Templates\TemplateMilitaryRangs.xls"
	; Set Section properties
	SetOverwrite on
	File "D:\WorkEmo\GitWork\HR\Templates\TemplateHoliday.xls"
SectionEnd
	
	Section "XML" Section6
		SetOverwrite off
		SetOutPath "$INSTDIR\XMLLabels\"
		File "D:\WorkEmo\GitWork\HR\XMLLabels\HRLabels\DTDDoc.dtd"
		File "D:\WorkEmo\GitWork\HR\XMLLabels\HRLabels\PersonInfo.xml"
		File "D:\WorkEmo\GitWork\HR\XMLLabels\HRLabels\ExcelDTD.dtd"
		File "D:\WorkEmo\GitWork\HR\XMLLabels\HRLabels\OSR.xml"
		File "D:\WorkEmo\GitWork\HR\XMLLabels\HRLabels\PSR.xml"
		File "D:\WorkEmo\GitWork\HR\XMLLabels\HRLabels\KartotekaQuery.xml"		
		File "D:\WorkEmo\GitWork\HR\XMLLabels\HRLabels\PersonTabs.xml"
		File "D:\WorkEmo\GitWork\HR\bin\Release\config.xml"
		SectionEnd
	
	Section "OverWritten" Section7

	; Set Section properties
	SetOverwrite on

	; Set Section Files and Shortcuts
	SetOutPath "$INSTDIR\Templates\"
	File "D:\WorkEmo\GitWork\HR\Templates\Helpmac.rtf"
	SetOutPath "$INSTDIR\"

SectionEnd

Section -FinishSection

	WriteRegStr HKLM "Software\${APPNAME}" "" "$INSTDIR"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}" "DisplayName" "${APPNAME}"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}" "UninstallString" "$INSTDIR\uninstall.exe"
	WriteUninstaller "$INSTDIR\uninstall.exe"
	;ExecWait '"$instdir\DataBaseUpdater.exe"'
	

SectionEnd

; Modern install component descriptions
;!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
;	!insertmacro MUI_DESCRIPTION_TEXT ${Section1} "������� ��������"
;	!insertmacro MUI_DESCRIPTION_TEXT ${Section2} "������� �� �����"
;	!insertmacro MUI_DESCRIPTION_TEXT ${Section3} "���� �����"
;	!insertmacro MUI_DESCRIPTION_TEXT ${Section4} "���� ����� � �����������"
;!insertmacro MUI_FUNCTION_DESCRIPTION_END

;Uninstall section
Section Uninstall

	;Remove from registry...
	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}"
	DeleteRegKey HKLM "SOFTWARE\${APPNAME}"

	; Delete self
	Delete "$INSTDIR\uninstall.exe"

	; Delete Shortcuts
	Delete "$DESKTOP\������� �������.lnk"
	Delete "$SMPROGRAMS\������� �������\������� �������.lnk"
	Delete "$SMPROGRAMS\������� �������\Uninstall.lnk"

	; Clean up ������� �������
	Delete "$INSTDIR\AddressInputControl.dll"
	Delete "$INSTDIR\BugBox.dll"
	Delete "$INSTDIR\CheckedComboBox.dll"
	Delete "$INSTDIR\CheckedNumBox.dll"
	Delete "$INSTDIR\CheckedTextBox.dll"
	Delete "$INSTDIR\ComboBoxIntelisense.dll"
	Delete "$INSTDIR\DataLayer.dll"
	Delete "$INSTDIR\Human Resources.exe"
	Delete "$INSTDIR\Interop.Excel.dll"
	Delete "$INSTDIR\Interop.Word.dll"
	Delete "$INSTDIR\Interop.VBIDE.dll"
	Delete "$INSTDIR\Interop.Office.dll"
	Delete "$INSTDIR\MySql.Data.dll"
	Delete "$INSTDIR\NewTabControl.dll"
	Delete "$INSTDIR\NumberBox1.dll"
	Delete "$INSTDIR\PassportDataControll.dll"
	Delete "$INSTDIR\System.Xml.dll"
	Delete "$INSTDIR\TemplateCustom.xls"
	Delete "$INSTDIR\TemplateFree.xls"
	Delete "$INSTDIR\TemplateHoliday.xls"
	Delete "$INSTDIR\TemplateOSR.xls"
	Delete "$INSTDIR\TemplatePSR.xls"
	Delete "$INSTDIR\TemplateZZBUT.xls"
	Delete "$INSTDIR\TemplateAttestations.xls"
	Delete "$INSTDIR\config.xml"
	Delete "$INSTDIR\copydatabase.exe"
	Delete "$INSTDIR\databaseconverter.exe"
	Delete "$INSTDIR\checkedcombo.dll"

	; Clean up Templates
	Delete "$INSTDIR\Templates\Helpmac.rtf"
	Delete "$INSTDIR\Templates\dogovor sro4en bez izpit.rtf"
	Delete "$INSTDIR\Templates\dogovor.rtf"
	Delete "$INSTDIR\Templates\zapoved_za_naznachavane bez izpitanie.rtf"
	Delete "$INSTDIR\Templates\zapoved_za_naznachavane s izpitanie.rtf"
	Delete "$INSTDIR\Templates\zapoved_za_naznachavane_trudov_dogovor.rtf"
	Delete "$INSTDIR\Templates\zapoved_za_prekratjavane.rtf"
	Delete "$INSTDIR\Templates\TemplateCharacteristic.rtf"
	Delete "$INSTDIR\Templates\cover_letter_u62.rtf"
	
	
	;Delete XMLLabels 
	Delete "$INSTDIR\XMLLabels\OSR.XML"
	Delete "$INSTDIR\XMLLabels\PersonInfo.xml"
	Delete "$INSTDIR\XMLLabels\DTDDoc.dtd"
	Delete "$INSTDIR\XMLLabels\ExcelDTD.dtd"
	
		; Remove remaining directories
	RMDir "$SMPROGRAMS\������� �������"
	RMDir "$INSTDIR\Database\"
	RMDir "$INSTDIR\Templates\"
	RMDir "$INSTDIR\XMLLabels\"
	RMDir "$INSTDIR\"

SectionEnd

; eof