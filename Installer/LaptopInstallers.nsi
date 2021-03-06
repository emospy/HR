; Script generated with the Venis Install Wizard

; Define your application name
!define APPNAME "������� �������"
!define APPNAMEANDVERSION "������� ������� 1.19"

; Main Install settings
Name "${APPNAMEANDVERSION}"
InstallDir "$PROGRAMFILES\Tess Partners\Human Resources\"
InstallDirRegKey HKLM "Software\${APPNAME}" ""
OutFile "C:\WorkEmo\HR\hr2006\trunk\Installer\HRSetup.exe"

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
	File "C:\WorkEmo\HR\hr2006\trunk\bin\Debug\AddressInputControl.dll"
	File "C:\WorkEmo\HR\hr2006\trunk\bin\Debug\BugBox.dll"
	File "C:\WorkEmo\HR\hr2006\trunk\bin\Debug\CheckedComboBox.dll"
	File "C:\WorkEmo\HR\hr2006\trunk\bin\Debug\CheckedCombo.dll"
	File "C:\WorkEmo\HR\hr2006\trunk\bin\Debug\CheckedNumBox.dll"
	File "C:\WorkEmo\HR\hr2006\trunk\bin\Debug\CheckedTextBox.dll"
	File "C:\WorkEmo\HR\hr2006\trunk\bin\Debug\ComboBoxIntelisense.dll"
	File "C:\WorkEmo\HR\hr2006\trunk\bin\Debug\DataLayer.dll"
	File "C:\WorkEmo\HR\hr2006\trunk\bin\Debug\Human Resources.exe"
	File "C:\WorkEmo\HR\hr2006\trunk\hrdll\Interop.Excel.dll"
	File "C:\WorkEmo\HR\hr2006\trunk\hrdll\Interop.Word.dll"
	File "C:\WorkEmo\HR\hr2006\trunk\hrdll\Interop.VBIDE.dll"
	File "C:\WorkEmo\HR\hr2006\trunk\hrdll\Interop.Office.dll"
	File "C:\WorkEmo\HR\hr2006\trunk\hrdll\MySql.Data.dll"
	File "C:\WorkEmo\HR\hr2006\trunk\bin\Debug\NewTabControl.dll"
	File "C:\WorkEmo\HR\hr2006\trunk\bin\Debug\NumberBox1.dll"
	File "C:\WorkEmo\HR\hr2006\trunk\bin\Debug\PassportDataControll.dll"
	File "C:\WorkEmo\HR\hr2006\trunk\bin\Debug\System.Xml.dll"
	File "C:\WorkEmo\HR\hr2006\trunk\testdatabase\bin\Debug\CopyDatabase.exe"
	File "C:\WorkEmo\HR\hr2006\trunk\DataBaseConverter\bin\Debug\DataBaseConverter.exe"
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
	File "C:\WorkEmo\HR\hr2006\trunk\Templates\Helpmac.rtf"
	File "C:\WorkEmo\HR\hr2006\trunk\Templates\dogovor sro4en bez izpit.rtf"
	File "C:\WorkEmo\HR\hr2006\trunk\Templates\dogovor.rtf"
	File "C:\WorkEmo\HR\hr2006\trunk\Templates\zapoved_za_naznachavane bez izpitanie.rtf"
	File "C:\WorkEmo\HR\hr2006\trunk\Templates\zapoved_za_naznachavane s izpitanie.rtf"
	File "C:\WorkEmo\HR\hr2006\trunk\Templates\zapoved_za_naznachavane_trudov_dogovor.rtf"
	File "C:\WorkEmo\HR\hr2006\trunk\Templates\zapoved_za_prekratjavane.rtf"
	File "C:\WorkEmo\HR\hr2006\trunk\Templates\TemplateCharacteristic.rtf"
	File "C:\WorkEmo\HR\hr2006\trunk\Templates\cover_letter_u62.rtf"

SectionEnd

Section "Database" Section3

	; Set Section properties
	SetOverwrite on

	; Set Section Files and Shortcuts
	SetOutPath "$INSTDIR\Database\HRDB\"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\absence.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\absence.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\absence.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\admininfo.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\admininfo.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\admininfo.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\attestations.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\attestations.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\attestations.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\category.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\category.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\category.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\contract.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\contract.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\contract.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\country.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\country.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\country.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\db.opt"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\dod.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\dod.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\dod.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\dshtr.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\dshtr.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\dshtr.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\dshtrtree.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\dshtrtree.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\dshtrtree.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\education.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\education.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\education.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\ekda.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\ekda.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\ekda.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\experience.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\experience.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\experience.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\familystatus.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\familystatus.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\familystatus.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\fired.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\fired.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\fired.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\firmpersonal3.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\firmpersonal3.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\firmpersonal3.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\firmstructure.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\firmstructure.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\firmstructure.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\globalpositions.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\globalpositions.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\globalpositions.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\holiday.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\holiday.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\holiday.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\kind.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\kind.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\kind.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\language.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\language.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\language.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\languagelevel.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\languagelevel.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\languagelevel.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\languageknowledge.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\languageknowledge.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\languageknowledge.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\law.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\law.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\law.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\militaryrang.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\militaryrang.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\militaryrang.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\militarystatus.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\militarystatus.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\militarystatus.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\newtree2.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\newtree2.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\newtree2.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\nkid.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\nkid.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\nkid.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\nkp.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\nkp.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\nkp.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\notes.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\notes.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\notes.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\options.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\options.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\options.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\penalty.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\penalty.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\penalty.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\penaltyreason.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\penaltyreason.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\penaltyreason.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\person.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\person.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\person.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\personassignment.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\personassignment.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\personassignment.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\pictures.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\pictures.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\pictures.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\profession.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\profession.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\profession.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\rang.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\rang.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\rang.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\reasonassignment.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\reasonassignment.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\reasonassignment.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\reasonfired.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\reasonfired.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\reasonfired.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\region.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\region.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\region.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\sciencelevel.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\sciencelevel.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\sciencelevel.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\sciencetitle.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\sciencetitle.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\sciencetitle.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\sex.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\sex.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\sex.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\staff.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\staff.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\staff.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\towns.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\towns.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\towns.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\type.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\type.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\type.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\typepenalty.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\typepenalty.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\typepenalty.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\worktime.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\worktime.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\worktime.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\year.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\year.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\year.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\yearlyaddon.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\yearlyaddon.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\yearlyaddon.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\year_holiday.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\year_holiday.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\year_holiday.MYI"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\nkpclass.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\nkpclass.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\HRDB2Clean\nkpclass.MYI"

SectionEnd

	Section "User" Section4

	; Set Section properties
	SetOverwrite off
	
	; Set Section Files and Shortcuts
	SetOutPath "$INSTDIR\DataBase\user\"
	File "C:\WorkEmo\HR\hr2006\trunk\user\db.opt"
	File "C:\WorkEmo\HR\hr2006\trunk\user\experience.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\user\law.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\user\rang.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\user\user.frm"
	File "C:\WorkEmo\HR\hr2006\trunk\user\user.MYD"
	File "C:\WorkEmo\HR\hr2006\trunk\user\user.MYI"
	ExecWait '"$instdir\copydatabase.exe"'	
	SectionEnd
	
	Section "XLS" Section5
	
	; Set Section properties
	SetOverwrite off
	; Set Section Files and Shortcuts
	SetOutPath "$INSTDIR\"
	File "C:\WorkEmo\HR\hr2006\trunk\Templates\TemplateCustom.xls"
	File "C:\WorkEmo\HR\hr2006\trunk\Templates\TemplateFree.xls"
	File "C:\WorkEmo\HR\hr2006\trunk\Templates\TemplateHoliday.xls"
	File "C:\WorkEmo\HR\hr2006\trunk\Templates\TemplateOSR.xls"
	File "C:\WorkEmo\HR\hr2006\trunk\Templates\TemplatePSR.xls"
	File "C:\WorkEmo\HR\hr2006\trunk\Templates\TemplateZZBUT.xls"
	File "C:\WorkEmo\HR\hr2006\trunk\Templates\TemplateAttestations.xls"
	SectionEnd
	
	Section "XML" Section6
		SetOverwrite off
		SetOutPath "$INSTDIR\XMLLabels\"
		File "C:\WorkEmo\HR\hr2006\trunk\XMLLabels\HRLabels\DTDDoc.dtd"
		File "C:\WorkEmo\HR\hr2006\trunk\XMLLabels\HRLabels\PersonInfo.xml"
		File "C:\WorkEmo\HR\hr2006\trunk\XMLLabels\HRLabels\ExcelDTD.dtd"
		File "C:\WorkEmo\HR\hr2006\trunk\XMLLabels\HRLabels\OSR.xml"
		SectionEnd
	

Section -FinishSection

	WriteRegStr HKLM "Software\${APPNAME}" "" "$INSTDIR"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}" "DisplayName" "${APPNAME}"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}" "UninstallString" "$INSTDIR\uninstall.exe"
	WriteUninstaller "$INSTDIR\uninstall.exe"
	ExecWait '"$instdir\DataBaseConverter.exe"'

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

	; Clean up Templates
	Delete "$INSTDIR\Templates\Helpmac.rtf"
	Delete "$INSTDIR\Templates\dogovor sro4en bez izpit.rtf"
	Delete "$INSTDIR\Templates\dogovor.rtf"
	Delete "$INSTDIR\Templates\zapoved_za_naznachavane bez izpitanie.rtf"
	Delete "$INSTDIR\Templates\zapoved_za_naznachavane s izpitanie.rtf"
	Delete "$INSTDIR\Templates\zapoved_za_naznachavane_trudov_dogovor.rtf"
	Delete "$INSTDIR\Templates\zapoved_za_prekratjavane.rtf"
	Delete "$INSTDIR\Templates\TemplateCharacteristic.rtf"
	
	; Clean up Database	
	Delete "$INSTDIR\Database\HRDB\absence.frm"
	Delete "$INSTDIR\Database\HRDB\absence.MYD"
	Delete "$INSTDIR\Database\HRDB\absence.MYI"
	Delete "$INSTDIR\Database\HRDB\admininfo.frm"
	Delete "$INSTDIR\Database\HRDB\admininfo.MYD"
	Delete "$INSTDIR\Database\HRDB\admininfo.MYI"
	Delete "$INSTDIR\Database\HRDB\attestations.frm"
	Delete "$INSTDIR\Database\HRDB\attestations.MYD"
	Delete "$INSTDIR\Database\HRDB\attestations.MYI"
	Delete "$INSTDIR\Database\HRDB\category.frm"
	Delete "$INSTDIR\Database\HRDB\category.MYD"
	Delete "$INSTDIR\Database\HRDB\category.MYI"
	Delete "$INSTDIR\Database\HRDB\contract.frm"
	Delete "$INSTDIR\Database\HRDB\contract.MYD"
	Delete "$INSTDIR\Database\HRDB\contract.MYI"
	Delete "$INSTDIR\Database\HRDB\country.frm"
	Delete "$INSTDIR\Database\HRDB\country.MYD"
	Delete "$INSTDIR\Database\HRDB\country.MYI"
	Delete "$INSTDIR\Database\HRDB\db.opt"
	Delete "$INSTDIR\Database\HRDB\dod.frm"
	Delete "$INSTDIR\Database\HRDB\dod.MYD"
	Delete "$INSTDIR\Database\HRDB\dod.MYI"
	Delete "$INSTDIR\Database\HRDB\dshtr.frm"
	Delete "$INSTDIR\Database\HRDB\dshtr.MYD"
	Delete "$INSTDIR\Database\HRDB\dshtr.MYI"
	Delete "$INSTDIR\Database\HRDB\dshtrtree.frm"
	Delete "$INSTDIR\Database\HRDB\dshtrtree.MYD"
	Delete "$INSTDIR\Database\HRDB\dshtrtree.MYI"
	Delete "$INSTDIR\Database\HRDB\education.frm"
	Delete "$INSTDIR\Database\HRDB\education.MYD"
	Delete "$INSTDIR\Database\HRDB\education.MYI"
	Delete "$INSTDIR\Database\HRDB\ekda.frm"
	Delete "$INSTDIR\Database\HRDB\ekda.MYD"
	Delete "$INSTDIR\Database\HRDB\ekda.MYI"
	Delete "$INSTDIR\Database\HRDB\experience.frm"
	Delete "$INSTDIR\Database\HRDB\experience.MYD"
	Delete "$INSTDIR\Database\HRDB\experience.MYI"
	Delete "$INSTDIR\Database\HRDB\familystatus.frm"
	Delete "$INSTDIR\Database\HRDB\familystatus.MYD"
	Delete "$INSTDIR\Database\HRDB\familystatus.MYI"
	Delete "$INSTDIR\Database\HRDB\fired.frm"
	Delete "$INSTDIR\Database\HRDB\fired.MYD"
	Delete "$INSTDIR\Database\HRDB\fired.MYI"
	Delete "$INSTDIR\Database\HRDB\firmpersonal3.frm"
	Delete "$INSTDIR\Database\HRDB\firmpersonal3.MYD"
	Delete "$INSTDIR\Database\HRDB\firmpersonal3.MYI"
	Delete "$INSTDIR\Database\HRDB\firmstructure.frm"
	Delete "$INSTDIR\Database\HRDB\firmstructure.MYD"
	Delete "$INSTDIR\Database\HRDB\firmstructure.MYI"
	Delete "$INSTDIR\Database\HRDB\globalpositions.frm"
	Delete "$INSTDIR\Database\HRDB\globalpositions.MYD"
	Delete "$INSTDIR\Database\HRDB\globalpositions.MYI"
	Delete "$INSTDIR\Database\HRDB\holiday.frm"
	Delete "$INSTDIR\Database\HRDB\holiday.MYD"
	Delete "$INSTDIR\Database\HRDB\holiday.MYI"
	Delete "$INSTDIR\Database\HRDB\kind.frm"
	Delete "$INSTDIR\Database\HRDB\kind.MYD"
	Delete "$INSTDIR\Database\HRDB\kind.MYI"
	Delete "$INSTDIR\Database\HRDB\language.frm"
	Delete "$INSTDIR\Database\HRDB\language.MYD"
	Delete "$INSTDIR\Database\HRDB\language.MYI"
	Delete "$INSTDIR\Database\HRDB\languagelevel.frm"
	Delete "$INSTDIR\Database\HRDB\languagelevel.MYD"
	Delete "$INSTDIR\Database\HRDB\languagelevel.MYI"
	Delete "$INSTDIR\Database\HRDB\languageknowledge.frm"
	Delete "$INSTDIR\Database\HRDB\languageknowledge.MYD"
	Delete "$INSTDIR\Database\HRDB\languageknowledge.MYI"
	Delete "$INSTDIR\Database\HRDB\law.frm"
	Delete "$INSTDIR\Database\HRDB\law.MYD"
	Delete "$INSTDIR\Database\HRDB\law.MYI"
	Delete "$INSTDIR\Database\HRDB\militaryrang.frm"
	Delete "$INSTDIR\Database\HRDB\militaryrang.MYD"
	Delete "$INSTDIR\Database\HRDB\militaryrang.MYI"
	Delete "$INSTDIR\Database\HRDB\militarystatus.frm"
	Delete "$INSTDIR\Database\HRDB\militarystatus.MYD"
	Delete "$INSTDIR\Database\HRDB\militarystatus.MYI"
	Delete "$INSTDIR\Database\HRDB\newtree2.frm"
	Delete "$INSTDIR\Database\HRDB\newtree2.MYD"
	Delete "$INSTDIR\Database\HRDB\newtree2.MYI"
	Delete "$INSTDIR\Database\HRDB\nkid.frm"
	Delete "$INSTDIR\Database\HRDB\nkid.MYD"
	Delete "$INSTDIR\Database\HRDB\nkid.MYI"
	Delete "$INSTDIR\Database\HRDB\nkp.frm"
	Delete "$INSTDIR\Database\HRDB\nkp.MYD"
	Delete "$INSTDIR\Database\HRDB\nkp.MYI"
	Delete "$INSTDIR\Database\HRDB\notes.frm"
	Delete "$INSTDIR\Database\HRDB\notes.MYD"
	Delete "$INSTDIR\Database\HRDB\notes.MYI"
	Delete "$INSTDIR\Database\HRDB\options.frm"
	Delete "$INSTDIR\Database\HRDB\options.MYD"
	Delete "$INSTDIR\Database\HRDB\options.MYI"
	Delete "$INSTDIR\Database\HRDB\penalty.frm"
	Delete "$INSTDIR\Database\HRDB\penalty.MYD"
	Delete "$INSTDIR\Database\HRDB\penalty.MYI"
	Delete "$INSTDIR\Database\HRDB\penaltyreason.frm"
	Delete "$INSTDIR\Database\HRDB\penaltyreason.MYD"
	Delete "$INSTDIR\Database\HRDB\penaltyreason.MYI"
	Delete "$INSTDIR\Database\HRDB\person.frm"
	Delete "$INSTDIR\Database\HRDB\person.MYD"
	Delete "$INSTDIR\Database\HRDB\person.MYI"
	Delete "$INSTDIR\Database\HRDB\personassignment.frm"
	Delete "$INSTDIR\Database\HRDB\personassignment.MYD"
	Delete "$INSTDIR\Database\HRDB\personassignment.MYI"
	Delete "$INSTDIR\Database\HRDB\pictures.frm"
	Delete "$INSTDIR\Database\HRDB\pictures.MYD"
	Delete "$INSTDIR\Database\HRDB\pictures.MYI"
	Delete "$INSTDIR\Database\HRDB\profession.frm"
	Delete "$INSTDIR\Database\HRDB\profession.MYD"
	Delete "$INSTDIR\Database\HRDB\profession.MYI"
	Delete "$INSTDIR\Database\HRDB\rang.frm"
	Delete "$INSTDIR\Database\HRDB\rang.MYD"
	Delete "$INSTDIR\Database\HRDB\rang.MYI"
	Delete "$INSTDIR\Database\HRDB\reasonassignment.frm"
	Delete "$INSTDIR\Database\HRDB\reasonassignment.MYD"
	Delete "$INSTDIR\Database\HRDB\reasonassignment.MYI"
	Delete "$INSTDIR\Database\HRDB\reasonfired.frm"
	Delete "$INSTDIR\Database\HRDB\reasonfired.MYD"
	Delete "$INSTDIR\Database\HRDB\reasonfired.MYI"
	Delete "$INSTDIR\Database\HRDB\region.frm"
	Delete "$INSTDIR\Database\HRDB\region.MYD"
	Delete "$INSTDIR\Database\HRDB\region.MYI"
	Delete "$INSTDIR\Database\HRDB\sciencelevel.frm"
	Delete "$INSTDIR\Database\HRDB\sciencelevel.MYD"
	Delete "$INSTDIR\Database\HRDB\sciencelevel.MYI"
	Delete "$INSTDIR\Database\HRDB\sciencetitle.frm"
	Delete "$INSTDIR\Database\HRDB\sciencetitle.MYD"
	Delete "$INSTDIR\Database\HRDB\sciencetitle.MYI"
	Delete "$INSTDIR\Database\HRDB\sex.frm"
	Delete "$INSTDIR\Database\HRDB\sex.MYD"
	Delete "$INSTDIR\Database\HRDB\sex.MYI"
	Delete "$INSTDIR\Database\HRDB\staff.frm"
	Delete "$INSTDIR\Database\HRDB\staff.MYD"
	Delete "$INSTDIR\Database\HRDB\staff.MYI"
	Delete "$INSTDIR\Database\HRDB\towns.frm"
	Delete "$INSTDIR\Database\HRDB\towns.MYD"
	Delete "$INSTDIR\Database\HRDB\towns.MYI"
	Delete "$INSTDIR\Database\HRDB\type.frm"
	Delete "$INSTDIR\Database\HRDB\type.MYD"
	Delete "$INSTDIR\Database\HRDB\type.MYI"
	Delete "$INSTDIR\Database\HRDB\typepenalty.frm"
	Delete "$INSTDIR\Database\HRDB\typepenalty.MYD"
	Delete "$INSTDIR\Database\HRDB\typepenalty.MYI"
	Delete "$INSTDIR\Database\HRDB\worktime.frm"
	Delete "$INSTDIR\Database\HRDB\worktime.MYD"
	Delete "$INSTDIR\Database\HRDB\worktime.MYI"
	Delete "$INSTDIR\Database\HRDB\year.frm"
	Delete "$INSTDIR\Database\HRDB\year.MYD"
	Delete "$INSTDIR\Database\HRDB\year.MYI"
	Delete "$INSTDIR\Database\HRDB\yearlyaddon.frm"
	Delete "$INSTDIR\Database\HRDB\yearlyaddon.MYD"
	Delete "$INSTDIR\Database\HRDB\yearlyaddon.MYI"
	Delete "$INSTDIR\Database\HRDB\year_holiday.frm"
	Delete "$INSTDIR\Database\HRDB\year_holiday.MYD"
	Delete "$INSTDIR\Database\HRDB\year_holiday.MYI"
	Delete "$INSTDIR\Database\HRDB\nkpclass.frm"
	Delete "$INSTDIR\Database\HRDB\nkpclass.MYD"
	Delete "$INSTDIR\Database\HRDB\nkpclass.MYI"
	;Delete User
	Delete "$INSTDIR\Database\user\db.opt"
	Delete "$INSTDIR\Database\user\experience.frm"
	Delete "$INSTDIR\Database\user\law.frm"
	Delete "$INSTDIR\Database\user\rang.frm"
	Delete "$INSTDIR\Database\user\user.frm"
	Delete "$INSTDIR\Database\user\user.MYD"
	Delete "$INSTDIR\Database\user\user.MYI"
	;Delete XMLLabels 
	Delete "$INSTDIR\XMLLabels\OSR.XML"
	Delete "$INSTDIR\XMLLabels\PersonInfo.xml"
	Delete "$INSTDIR\XMLLabels\DTDoc.dtd"
	Delete "$INSTDIR\XMLLabels\ExcelDTD.dtd"
	
		; Remove remaining directories
	RMDir "$SMPROGRAMS\������� �������"
	RMDir "$INSTDIR\Database\user\"
	RMDir "$INSTDIR\Database\HRDB\"
	RMDir "$INSTDIR\Database\"
	RMDir "$INSTDIR\Templates\"
	RMDir "$INSTDIR\XMLLabels\"
	RMDir "$INSTDIR\"

SectionEnd

; eof