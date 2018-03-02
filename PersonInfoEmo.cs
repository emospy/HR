using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.IO;
namespace LichenSystaw2004
{
	public class formPersonalData : System.Windows.Forms.Form
	{		
		private ArrayList arrDirection;
		private DataView vueDirection, vueDepartment, vueSector, vuePosition, vueAdministration, vueAssignment;
		private DataTable dtTree;
		private DataTable dtLanguage = new DataTable();
		private DataTable dtPosition;
		private DataViewRowState dvrs;
		private int parent, positionID, oldPositionID, nodeID;		
		private mainForm mainform;
		private string User;
		private	DataLayer.AssignmentAction assignmentAction;
		private DataLayer.PictureAction pictureAction;
		private DataLayer.LanguageAction languageAction;
		bool IsAssignmentLoadForm = false;
		bool IsAssignmentEdit = false;
		bool IsAssignment = true;
		bool IsFiredEdit = false;
		bool IsLoading = false;
		DataTable dtAssignment = new DataTable();

		DataLayer.AbsenceAction absenceAction;
		bool IsAbsenceLoadForm = false;
		bool IsAbsenceEdit = false;
		DataTable dtAbsence = new DataTable();

		//DataLayer.HolidayPackage holidayPackage;
		DataLayer.HolidayAction holidayAction;
		//DataTable dtHoliday = new DataTable();
		DataTable dtYearHoliday = new DataTable();

		bool IsPenaltyLoadForm = false;
		bool IsPenaltyEdit = false;
		DataTable dtPenalty = new DataTable();

		DataTable dtNotes = new DataTable();
		bool IsActive = false;
		DataLayer.NoteAction note;

		DataLayer.FiredAction firedAction;
		DataTable dtFired = new DataTable();
		
		enum Operations //Описание на операциите които могат да се извършват в досието
		{
			AddNewPerson = 1,
			ViewPersonData,
			EditPenalty,
			AddPenalty,
			EditAssignment,
			AddAssignment,
			EditAbsence,
			AddAbsence,
			EditNotes,
			FirePerson,
			AddFired,
			EditFired
		}
		Operations Op;  //Пази информация за текущата операция

		enum LockButtons  // Описание на прозорците на които може да се отключват и заключват бутони. Въвел съм го за по-удобно, за да не се пишат сртингове при извикването на функциите
		{
			Penalty=1,
			Absence,
			Assignment,
			Notes,
			Fired
		}

		bool PersonalDataChangedValue = false;  //Ако не сме правили промени по личните данни на лицето тази променлива ще остане фалсе и няма да се прави обръщение към базата данни при натискане на бутон запис

		DataLayer.PersonalAction personAction;
		DataLayer.PenaltyAction penaltyAction;
		
		#region Control_List
		private System.Windows.Forms.Button buttonОК;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Label labelSex;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.TabPage TabPersonalInfo;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox comboBoxSex;
		private BugBox.NumBox numBoxPcCard;
		private System.Windows.Forms.Label labelJKkwartal;
		private System.Windows.Forms.DateTimePicker dateTimePickerPCCardPublished;
		private System.Windows.Forms.Label labelPublishedByy;
		private System.Windows.Forms.Label labelPublishedBy;
		private System.Windows.Forms.TextBox textBoxPublishedFrom;
		private System.Windows.Forms.Label labelStreet;
		private System.Windows.Forms.TextBox textBoxStreet;
		private System.Windows.Forms.Label labelNumBlock;
		private System.Windows.Forms.TextBox textBoxNumBlock;
		private BugBox.NumBox numBoxTelephone;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label labelKwartal;
		private System.Windows.Forms.TextBox textBoxKwartal;
		private ComboBoxIntelisense.InteliCombo comboBoxNaselenoMqsto;
		private System.Windows.Forms.Label labelNaselenoMqsto;
		private System.Windows.Forms.Label labelRegion;
		private System.Windows.Forms.ComboBox comboBoxRegion;
		private BugBox.BugBox numBoxEgn;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxNames;
		private System.Windows.Forms.Label labelNames;
		private System.Windows.Forms.ComboBox comboBoxCountry;
		private System.Windows.Forms.Label labelCountry;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TabPage tabPageAssignment;
		private System.Windows.Forms.Button buttonAssignmentPrint;
		private System.Windows.Forms.DateTimePicker dateTimePickerContractExpiry;
		private System.Windows.Forms.Button buttonAssignmentDelete;
		private System.Windows.Forms.Button buttonAssignmentSave;
		private System.Windows.Forms.Button buttonAssignmentEdit;
		private System.Windows.Forms.RadioButton radioButtonAdditional;
		private System.Windows.Forms.RadioButton radioButtonAssignment;
		private System.Windows.Forms.Button buttonAssignment;
		private System.Windows.Forms.DateTimePicker dateTimePickerAssignedAt;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBoxContractNumber;
		private System.Windows.Forms.ComboBox comboBoxAssignReason;
		private System.Windows.Forms.ComboBox comboBoxContract;
		private System.Windows.Forms.ComboBox comboBoxPosition;
		private System.Windows.Forms.TabPage tabPageAbsence;
		private System.Windows.Forms.Button buttonAbsenceDelete;
		private System.Windows.Forms.Button buttonAbsenceSave;
		private System.Windows.Forms.Button buttonAbsenceEdit;
		private System.Windows.Forms.Button buttonAbsenceAdd;
		private System.Windows.Forms.DataGrid dataGridAbsence;
		private System.Windows.Forms.GroupBox groupBoxAbsece;
		private System.Windows.Forms.DateTimePicker dateTimePickerAbsenceOrderFormData;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.TextBox textBoxAbsenceNumberOrder;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.TextBox textBoxAbsenceReason;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.ComboBox comboBoxAbsenceTypeAbsence;
		private System.Windows.Forms.Label label24;
		private BugBox.NumBox numBoxAbsenceDays;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.DateTimePicker dateTimePickerAbsenceToData;
		private System.Windows.Forms.DateTimePicker dateTimePickerAbsenceFromData;
		private System.Windows.Forms.TabPage tabPagePenalty;
		private System.Windows.Forms.Button buttonPenaltyDelete;
		private System.Windows.Forms.Button buttonPenaltySave;
		private System.Windows.Forms.Button buttonPebaltyEdit;
		private System.Windows.Forms.Button buttonPenaltyAdd;
		private System.Windows.Forms.GroupBox groupBoxPenalty;
		private System.Windows.Forms.TabPage tabPageNotes;
		private System.Windows.Forms.Button buttonNotes;
		private System.Windows.Forms.TextBox textBoxNotes;
		private System.Windows.Forms.TabPage tabPageAtestacii;
		private System.Windows.Forms.Label label2;
		private NewTabControl.NTabControl tabControl1;
		private System.Windows.Forms.Button buttonAssignmentCancel;
		private System.Windows.Forms.Button buttonAbsencePrint;
		private System.Windows.Forms.Button buttonAbsenceCancel;
		private System.Windows.Forms.Button buttonPenaltyCancel;
		private System.Windows.Forms.DateTimePicker dateTimePenaltyFormDate;
		private BugBox.NumBox numBoxPenaltyOrder;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label labelPenaltyReason;
		private System.Windows.Forms.Label labelPenalty;
		private System.Windows.Forms.DateTimePicker dateTimePickerPenaltyDate;
		private System.Windows.Forms.DataGrid dataGridPenalty;
		private System.Windows.Forms.GroupBox groupBoxAbsenceGrid;
		private System.Windows.Forms.GroupBox groupBoxPenaltyGrid;
		private System.Windows.Forms.GroupBox groupBoxAssignmentGrid;
		private System.Windows.Forms.DataGrid dataGridAssignment;
		private System.Windows.Forms.Label label39;
		private BugBox.NumBox numBoxAssignmentExpY;
		private BugBox.NumBox numBoxAssignmentExtM;
		private BugBox.NumBox numBoxAssignmentExpD;
		private System.Windows.Forms.Label label41;
		private System.Windows.Forms.TextBox textBoxBornTown;
		private System.Windows.Forms.ComboBox comboBoxLaw;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label38;
		private System.Windows.Forms.Label label37;
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.TextBox textBoxDiplom;
		private System.Windows.Forms.ComboBox comboBoxFamilyStatus;
		private System.Windows.Forms.ComboBox comboBoxEducation;
		private System.Windows.Forms.Label labelCategory;
		private System.Windows.Forms.ComboBox comboBoxCategory;
		private System.Windows.Forms.Label labelMilitaryStatus;
		private System.Windows.Forms.ComboBox comboBoxMilitaryStatus;
		private System.Windows.Forms.Label labelScience;
		private System.Windows.Forms.ComboBox comboBoxScience;
		private System.Windows.Forms.Label labelScienceLevel;
		private System.Windows.Forms.Label labelMilitaryRang;
		private System.Windows.Forms.ComboBox comboBoxMilitaryRang;
		private System.Windows.Forms.ComboBox comboBoxScienceLevel;
		private System.Windows.Forms.ComboBox comboBoxProfesion;
		private System.Windows.Forms.Label labelProfesion;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label42;
		private System.Windows.Forms.Label label43;
		private BugBox.NumBox numBoxStartDay;
		private BugBox.NumBox numBoxStartMonth;
		private BugBox.NumBox numBoxStartYear;
		private BugBox.NumBox numBoxOrgDay;
		private BugBox.NumBox numBoxOrgMonth;
		private BugBox.NumBox numBoxOrgYear;
		private BugBox.NumBox numBoxTotalDay;
		private BugBox.NumBox numBoxTotalYear;
		private BugBox.NumBox numBoxTotalMonth;
		private System.Windows.Forms.DateTimePicker dateTimePickerPostypilNa;
		private System.Windows.Forms.Label labelHiredAt;
		private System.Windows.Forms.CheckedListBox checkedListBoxLanguage;
		private System.Windows.Forms.Label labelLanguage;
		private System.Windows.Forms.Label labellanguageLevel;
		private System.Windows.Forms.TextBox textBoxNKPCode;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label40;
		private System.Windows.Forms.TextBox textBoxNKPLevel;
		private System.Windows.Forms.Label labelLevel1;
		private System.Windows.Forms.Label labelLevel4;
		private System.Windows.Forms.Label labelLevel2;
		private System.Windows.Forms.Label labelLevel3;
		private System.Windows.Forms.ComboBox comboBoxLevel1;
		private System.Windows.Forms.ComboBox comboBoxLevel4;
		private System.Windows.Forms.ComboBox comboBoxLevel3;
		private System.Windows.Forms.ComboBox comboBoxLevel2;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button buttonPicture;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Button buttonHistory;
		private System.Windows.Forms.Button buttonDeletePicture;
		private BugBox.NumBox numBoxMonthlyAddon;
		private System.Windows.Forms.Label label46;
		private System.Windows.Forms.Label label45;
		private System.Windows.Forms.ComboBox comboBoxYearlyAddon;
		private BugBox.NumBox numBoxBaseSalary;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBoxSalaryAddon;
		private System.Windows.Forms.TextBox textBoxClassPercent;
		private System.Windows.Forms.ComboBox comboBoxWorkTime;
		private System.Windows.Forms.ComboBox comboBoxForYear;
		private System.Windows.Forms.DataGrid dataGridYears;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.ComboBox comboBoxLanguageLevel;
		private System.Windows.Forms.ComboBox comboBoxPenaltyReason;
		private System.Windows.Forms.DateTimePicker dateTimePickerPenaltyTo;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.ComboBox comboBoxTypePenalty;
		private System.Windows.Forms.TabPage tabPageFired;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.ComboBox comboBoxFiredReason;
		private System.Windows.Forms.TextBox textBoxFiredCompensation;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.DateTimePicker dateTimePickerFiredFromDate;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.ComboBox comboBoxFiredComponsationWork;
		private System.Windows.Forms.Label label44;
		private System.Windows.Forms.Label label47;
		private System.Windows.Forms.ComboBox comboBoxFiredCompensationMistimed;
		private System.Windows.Forms.ComboBox comboBoxFiredNumberSalary;
		private System.Windows.Forms.DataGrid dataGridFired;
		private System.Windows.Forms.Button buttonFiredPrint;
		private System.Windows.Forms.Button buttonFiredCancel;
		private System.Windows.Forms.Button buttonFiredDelete;
		private System.Windows.Forms.Button buttonFiredSave;
		private System.Windows.Forms.Button buttonFiredEdit;
		private System.Windows.Forms.Button buttonFiredNew;
		private BugBox.NumBox numBoxFiredUnusedHoliday;
		private System.Windows.Forms.Label label48;
		private System.Windows.Forms.Button buttonFire;
		private System.Windows.Forms.GroupBox groupBoxFired;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.ComponentModel.IContainer components;
		private BugBox.NumBox numBoxNumHoliday;
		private System.Windows.Forms.Label label49;
		private System.Windows.Forms.DateTimePicker dateTimePickerTestPeriod;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.DateTimePicker dateTimePickerContractDate;
		private System.Windows.Forms.ComboBox comboBoxPrefix;
		private System.Windows.Forms.Button buttonPenaltyPrint;
		#endregion

		public formPersonalData( string Identifier, mainForm main)
		{			
			Op = Operations.ViewPersonData;
			this.mainform = main;
			this.parent = Int32.Parse(Identifier);
			
			this.personAction = new DataLayer.PersonalAction("FirmPersonal", this.mainform.connString );
			this.note = new DataLayer.NoteAction( this.mainform.connString );
			this.penaltyAction = new DataLayer.PenaltyAction( "Penalty", this.mainform.connString );
			this.absenceAction  = new DataLayer.AbsenceAction( "Absence", this.mainform.connString );
			this.holidayAction = new DataLayer.HolidayAction( "year_holiday", this.mainform.connString );   
			this.pictureAction = new DataLayer.PictureAction( "Pictures", this.mainform.connString );
			this.languageAction = new DataLayer.LanguageAction( "languagelevel", this.mainform.connString );
			this.dtLanguage = this.languageAction.SelectWhere( "languagelevel", this.parent );
			this.firedAction = new DataLayer.FiredAction( "fired", this.mainform.connString );
			this.dtTree = main.nomenclaatureData.TreeTable;
			this.User = main.User;

			InitializeComponent();
			
			comboBoxAbsenceTypeAbsence.Items.Add( "Платен отпуск" );
			comboBoxAbsenceTypeAbsence.Items.Add( "Неплатен отпуск" );
		}

		public formPersonalData(mainForm main)
		{
			Op = Operations.AddNewPerson;
			this.mainform = main;

			this.parent = 0;
			this.personAction = new DataLayer.PersonalAction("FirmPersonal", this.mainform.connString );
			this.note = new DataLayer.NoteAction( this.mainform.connString );
			this.penaltyAction = new DataLayer.PenaltyAction( "Penalty", this.mainform.connString );
			this.absenceAction  = new DataLayer.AbsenceAction( "Absence", this.mainform.connString );	
			this.holidayAction = new DataLayer.HolidayAction( "year_holiday", this.mainform.connString );   
			this.pictureAction = new DataLayer.PictureAction( "Pictures", this.mainform.connString );
			this.languageAction = new DataLayer.LanguageAction( "languagelevel", this.mainform.connString );
			this.dtLanguage = this.languageAction.SelectWhere( "languagelevel", this.parent );
			this.dtTree = main.nomenclaatureData.TreeTable;
			this.firedAction = new DataLayer.FiredAction( "fired", this.mainform.connString );
			this.User = main.User;

			InitializeComponent();

			comboBoxAbsenceTypeAbsence.Items.Add( "Платен отпуск" );
			comboBoxAbsenceTypeAbsence.Items.Add( "Неплатен отпуск" );
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			//			if( disposing )
			//			{
			//				if(components != null)
			//				{
			//			:-*		components.Dispose();
			//				}
			//			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(formPersonalData));
			this.buttonОК = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.labelSex = new System.Windows.Forms.Label();
			this.buttonSave = new System.Windows.Forms.Button();
			this.TabPersonalInfo = new System.Windows.Forms.TabPage();
			this.buttonDeletePicture = new System.Windows.Forms.Button();
			this.buttonPicture = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label19 = new System.Windows.Forms.Label();
			this.label42 = new System.Windows.Forms.Label();
			this.label43 = new System.Windows.Forms.Label();
			this.numBoxStartDay = new BugBox.NumBox();
			this.numBoxStartMonth = new BugBox.NumBox();
			this.numBoxStartYear = new BugBox.NumBox();
			this.numBoxOrgDay = new BugBox.NumBox();
			this.numBoxOrgMonth = new BugBox.NumBox();
			this.numBoxOrgYear = new BugBox.NumBox();
			this.numBoxTotalDay = new BugBox.NumBox();
			this.numBoxTotalYear = new BugBox.NumBox();
			this.numBoxTotalMonth = new BugBox.NumBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.comboBoxLanguageLevel = new System.Windows.Forms.ComboBox();
			this.dateTimePickerPostypilNa = new System.Windows.Forms.DateTimePicker();
			this.labelHiredAt = new System.Windows.Forms.Label();
			this.checkedListBoxLanguage = new System.Windows.Forms.CheckedListBox();
			this.labelLanguage = new System.Windows.Forms.Label();
			this.labellanguageLevel = new System.Windows.Forms.Label();
			this.labelMilitaryRang = new System.Windows.Forms.Label();
			this.labelScience = new System.Windows.Forms.Label();
			this.comboBoxMilitaryRang = new System.Windows.Forms.ComboBox();
			this.comboBoxScienceLevel = new System.Windows.Forms.ComboBox();
			this.comboBoxScience = new System.Windows.Forms.ComboBox();
			this.labelScienceLevel = new System.Windows.Forms.Label();
			this.comboBoxProfesion = new System.Windows.Forms.ComboBox();
			this.label37 = new System.Windows.Forms.Label();
			this.label36 = new System.Windows.Forms.Label();
			this.label38 = new System.Windows.Forms.Label();
			this.textBoxDiplom = new System.Windows.Forms.TextBox();
			this.comboBoxFamilyStatus = new System.Windows.Forms.ComboBox();
			this.comboBoxEducation = new System.Windows.Forms.ComboBox();
			this.labelCategory = new System.Windows.Forms.Label();
			this.comboBoxCategory = new System.Windows.Forms.ComboBox();
			this.labelMilitaryStatus = new System.Windows.Forms.Label();
			this.comboBoxMilitaryStatus = new System.Windows.Forms.ComboBox();
			this.labelProfesion = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.comboBoxPrefix = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.comboBoxSex = new System.Windows.Forms.ComboBox();
			this.numBoxPcCard = new BugBox.NumBox();
			this.labelJKkwartal = new System.Windows.Forms.Label();
			this.dateTimePickerPCCardPublished = new System.Windows.Forms.DateTimePicker();
			this.labelPublishedByy = new System.Windows.Forms.Label();
			this.labelPublishedBy = new System.Windows.Forms.Label();
			this.textBoxPublishedFrom = new System.Windows.Forms.TextBox();
			this.labelStreet = new System.Windows.Forms.Label();
			this.textBoxStreet = new System.Windows.Forms.TextBox();
			this.labelNumBlock = new System.Windows.Forms.Label();
			this.textBoxNumBlock = new System.Windows.Forms.TextBox();
			this.numBoxTelephone = new BugBox.NumBox();
			this.label3 = new System.Windows.Forms.Label();
			this.labelKwartal = new System.Windows.Forms.Label();
			this.textBoxKwartal = new System.Windows.Forms.TextBox();
			this.comboBoxNaselenoMqsto = new ComboBoxIntelisense.InteliCombo();
			this.labelNaselenoMqsto = new System.Windows.Forms.Label();
			this.labelRegion = new System.Windows.Forms.Label();
			this.comboBoxRegion = new System.Windows.Forms.ComboBox();
			this.numBoxEgn = new BugBox.BugBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxNames = new System.Windows.Forms.TextBox();
			this.labelNames = new System.Windows.Forms.Label();
			this.comboBoxCountry = new System.Windows.Forms.ComboBox();
			this.labelCountry = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxBornTown = new System.Windows.Forms.TextBox();
			this.tabPageAssignment = new System.Windows.Forms.TabPage();
			this.dateTimePickerContractDate = new System.Windows.Forms.DateTimePicker();
			this.label14 = new System.Windows.Forms.Label();
			this.numBoxNumHoliday = new BugBox.NumBox();
			this.numBoxMonthlyAddon = new BugBox.NumBox();
			this.label46 = new System.Windows.Forms.Label();
			this.label45 = new System.Windows.Forms.Label();
			this.comboBoxYearlyAddon = new System.Windows.Forms.ComboBox();
			this.numBoxBaseSalary = new BugBox.NumBox();
			this.label17 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.textBoxSalaryAddon = new System.Windows.Forms.TextBox();
			this.textBoxClassPercent = new System.Windows.Forms.TextBox();
			this.comboBoxWorkTime = new System.Windows.Forms.ComboBox();
			this.label40 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.textBoxNKPCode = new System.Windows.Forms.TextBox();
			this.textBoxNKPLevel = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.comboBoxLaw = new System.Windows.Forms.ComboBox();
			this.labelLevel1 = new System.Windows.Forms.Label();
			this.comboBoxLevel1 = new System.Windows.Forms.ComboBox();
			this.label39 = new System.Windows.Forms.Label();
			this.numBoxAssignmentExpD = new BugBox.NumBox();
			this.numBoxAssignmentExtM = new BugBox.NumBox();
			this.numBoxAssignmentExpY = new BugBox.NumBox();
			this.buttonAssignmentCancel = new System.Windows.Forms.Button();
			this.buttonAssignmentPrint = new System.Windows.Forms.Button();
			this.dateTimePickerContractExpiry = new System.Windows.Forms.DateTimePicker();
			this.buttonAssignmentDelete = new System.Windows.Forms.Button();
			this.buttonAssignmentSave = new System.Windows.Forms.Button();
			this.buttonAssignmentEdit = new System.Windows.Forms.Button();
			this.radioButtonAdditional = new System.Windows.Forms.RadioButton();
			this.buttonAssignment = new System.Windows.Forms.Button();
			this.dateTimePickerAssignedAt = new System.Windows.Forms.DateTimePicker();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.labelLevel4 = new System.Windows.Forms.Label();
			this.labelLevel2 = new System.Windows.Forms.Label();
			this.textBoxContractNumber = new System.Windows.Forms.TextBox();
			this.comboBoxAssignReason = new System.Windows.Forms.ComboBox();
			this.comboBoxContract = new System.Windows.Forms.ComboBox();
			this.comboBoxPosition = new System.Windows.Forms.ComboBox();
			this.comboBoxLevel4 = new System.Windows.Forms.ComboBox();
			this.comboBoxLevel3 = new System.Windows.Forms.ComboBox();
			this.comboBoxLevel2 = new System.Windows.Forms.ComboBox();
			this.groupBoxAssignmentGrid = new System.Windows.Forms.GroupBox();
			this.dataGridAssignment = new System.Windows.Forms.DataGrid();
			this.radioButtonAssignment = new System.Windows.Forms.RadioButton();
			this.labelLevel3 = new System.Windows.Forms.Label();
			this.label41 = new System.Windows.Forms.Label();
			this.dateTimePickerTestPeriod = new System.Windows.Forms.DateTimePicker();
			this.label49 = new System.Windows.Forms.Label();
			this.tabPageAbsence = new System.Windows.Forms.TabPage();
			this.buttonAbsenceCancel = new System.Windows.Forms.Button();
			this.buttonAbsencePrint = new System.Windows.Forms.Button();
			this.buttonAbsenceDelete = new System.Windows.Forms.Button();
			this.buttonAbsenceSave = new System.Windows.Forms.Button();
			this.buttonAbsenceEdit = new System.Windows.Forms.Button();
			this.buttonAbsenceAdd = new System.Windows.Forms.Button();
			this.groupBoxAbsenceGrid = new System.Windows.Forms.GroupBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.dataGridYears = new System.Windows.Forms.DataGrid();
			this.buttonHistory = new System.Windows.Forms.Button();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.dataGridAbsence = new System.Windows.Forms.DataGrid();
			this.groupBoxAbsece = new System.Windows.Forms.GroupBox();
			this.label29 = new System.Windows.Forms.Label();
			this.comboBoxForYear = new System.Windows.Forms.ComboBox();
			this.dateTimePickerAbsenceOrderFormData = new System.Windows.Forms.DateTimePicker();
			this.label28 = new System.Windows.Forms.Label();
			this.label27 = new System.Windows.Forms.Label();
			this.textBoxAbsenceNumberOrder = new System.Windows.Forms.TextBox();
			this.label26 = new System.Windows.Forms.Label();
			this.textBoxAbsenceReason = new System.Windows.Forms.TextBox();
			this.label25 = new System.Windows.Forms.Label();
			this.comboBoxAbsenceTypeAbsence = new System.Windows.Forms.ComboBox();
			this.label24 = new System.Windows.Forms.Label();
			this.numBoxAbsenceDays = new BugBox.NumBox();
			this.label23 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.dateTimePickerAbsenceToData = new System.Windows.Forms.DateTimePicker();
			this.dateTimePickerAbsenceFromData = new System.Windows.Forms.DateTimePicker();
			this.tabPagePenalty = new System.Windows.Forms.TabPage();
			this.buttonPenaltyPrint = new System.Windows.Forms.Button();
			this.buttonPenaltyCancel = new System.Windows.Forms.Button();
			this.buttonPenaltyDelete = new System.Windows.Forms.Button();
			this.buttonPenaltySave = new System.Windows.Forms.Button();
			this.buttonPebaltyEdit = new System.Windows.Forms.Button();
			this.buttonPenaltyAdd = new System.Windows.Forms.Button();
			this.groupBoxPenaltyGrid = new System.Windows.Forms.GroupBox();
			this.dataGridPenalty = new System.Windows.Forms.DataGrid();
			this.groupBoxPenalty = new System.Windows.Forms.GroupBox();
			this.label31 = new System.Windows.Forms.Label();
			this.comboBoxTypePenalty = new System.Windows.Forms.ComboBox();
			this.label30 = new System.Windows.Forms.Label();
			this.dateTimePickerPenaltyTo = new System.Windows.Forms.DateTimePicker();
			this.comboBoxPenaltyReason = new System.Windows.Forms.ComboBox();
			this.dateTimePenaltyFormDate = new System.Windows.Forms.DateTimePicker();
			this.numBoxPenaltyOrder = new BugBox.NumBox();
			this.label21 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.labelPenaltyReason = new System.Windows.Forms.Label();
			this.labelPenalty = new System.Windows.Forms.Label();
			this.dateTimePickerPenaltyDate = new System.Windows.Forms.DateTimePicker();
			this.tabPageNotes = new System.Windows.Forms.TabPage();
			this.buttonNotes = new System.Windows.Forms.Button();
			this.textBoxNotes = new System.Windows.Forms.TextBox();
			this.tabPageAtestacii = new System.Windows.Forms.TabPage();
			this.label2 = new System.Windows.Forms.Label();
			this.tabControl1 = new NewTabControl.NTabControl();
			this.tabPageFired = new System.Windows.Forms.TabPage();
			this.buttonFire = new System.Windows.Forms.Button();
			this.label48 = new System.Windows.Forms.Label();
			this.numBoxFiredUnusedHoliday = new BugBox.NumBox();
			this.buttonFiredPrint = new System.Windows.Forms.Button();
			this.buttonFiredCancel = new System.Windows.Forms.Button();
			this.buttonFiredDelete = new System.Windows.Forms.Button();
			this.buttonFiredSave = new System.Windows.Forms.Button();
			this.buttonFiredEdit = new System.Windows.Forms.Button();
			this.buttonFiredNew = new System.Windows.Forms.Button();
			this.groupBoxFired = new System.Windows.Forms.GroupBox();
			this.dataGridFired = new System.Windows.Forms.DataGrid();
			this.label47 = new System.Windows.Forms.Label();
			this.comboBoxFiredNumberSalary = new System.Windows.Forms.ComboBox();
			this.label44 = new System.Windows.Forms.Label();
			this.comboBoxFiredCompensationMistimed = new System.Windows.Forms.ComboBox();
			this.label35 = new System.Windows.Forms.Label();
			this.comboBoxFiredComponsationWork = new System.Windows.Forms.ComboBox();
			this.dateTimePickerFiredFromDate = new System.Windows.Forms.DateTimePicker();
			this.label34 = new System.Windows.Forms.Label();
			this.label33 = new System.Windows.Forms.Label();
			this.textBoxFiredCompensation = new System.Windows.Forms.TextBox();
			this.label32 = new System.Windows.Forms.Label();
			this.comboBoxFiredReason = new System.Windows.Forms.ComboBox();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.TabPersonalInfo.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.tabPageAssignment.SuspendLayout();
			this.groupBoxAssignmentGrid.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridAssignment)).BeginInit();
			this.tabPageAbsence.SuspendLayout();
			this.groupBoxAbsenceGrid.SuspendLayout();
			this.groupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridYears)).BeginInit();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridAbsence)).BeginInit();
			this.groupBoxAbsece.SuspendLayout();
			this.tabPagePenalty.SuspendLayout();
			this.groupBoxPenaltyGrid.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridPenalty)).BeginInit();
			this.groupBoxPenalty.SuspendLayout();
			this.tabPageNotes.SuspendLayout();
			this.tabPageAtestacii.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPageFired.SuspendLayout();
			this.groupBoxFired.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridFired)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonОК
			// 
			this.buttonОК.Image = ((System.Drawing.Image)(resources.GetObject("buttonОК.Image")));
			this.buttonОК.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonОК.Location = new System.Drawing.Point(240, 496);
			this.buttonОК.Name = "buttonОК";
			this.buttonОК.Size = new System.Drawing.Size(104, 23);
			this.buttonОК.TabIndex = 26;
			this.buttonОК.Text = "    Запис и изход";
			this.buttonОК.Click += new System.EventHandler(this.buttonОК_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Image")));
			this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonCancel.Location = new System.Drawing.Point(472, 496);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(80, 23);
			this.buttonCancel.TabIndex = 28;
			this.buttonCancel.Text = " Изход";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// labelSex
			// 
			this.labelSex.Location = new System.Drawing.Point(408, 184);
			this.labelSex.Name = "labelSex";
			this.labelSex.Size = new System.Drawing.Size(100, 24);
			this.labelSex.TabIndex = 16;
			this.labelSex.Text = "Пол:";
			// 
			// buttonSave
			// 
			this.buttonSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.Image")));
			this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonSave.Location = new System.Drawing.Point(368, 496);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.TabIndex = 27;
			this.buttonSave.Text = " Запис";
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// TabPersonalInfo
			// 
			this.TabPersonalInfo.Controls.Add(this.buttonDeletePicture);
			this.TabPersonalInfo.Controls.Add(this.buttonPicture);
			this.TabPersonalInfo.Controls.Add(this.pictureBox1);
			this.TabPersonalInfo.Controls.Add(this.groupBox1);
			this.TabPersonalInfo.Controls.Add(this.groupBox3);
			this.TabPersonalInfo.Controls.Add(this.groupBox2);
			this.TabPersonalInfo.Location = new System.Drawing.Point(4, 22);
			this.TabPersonalInfo.Name = "TabPersonalInfo";
			this.TabPersonalInfo.Size = new System.Drawing.Size(760, 462);
			this.TabPersonalInfo.TabIndex = 0;
			this.TabPersonalInfo.Text = "Лични данни";
			// 
			// buttonDeletePicture
			// 
			this.buttonDeletePicture.Location = new System.Drawing.Point(128, 424);
			this.buttonDeletePicture.Name = "buttonDeletePicture";
			this.buttonDeletePicture.Size = new System.Drawing.Size(104, 23);
			this.buttonDeletePicture.TabIndex = 109;
			this.buttonDeletePicture.Text = "Изтрий снимка";
			this.buttonDeletePicture.Click += new System.EventHandler(this.button1_Click);
			// 
			// buttonPicture
			// 
			this.buttonPicture.Location = new System.Drawing.Point(128, 392);
			this.buttonPicture.Name = "buttonPicture";
			this.buttonPicture.Size = new System.Drawing.Size(104, 23);
			this.buttonPicture.TabIndex = 108;
			this.buttonPicture.Text = "Отвори снимка";
			this.buttonPicture.Click += new System.EventHandler(this.buttonPicture_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(24, 344);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(96, 112);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 107;
			this.pictureBox1.TabStop = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label19);
			this.groupBox1.Controls.Add(this.label42);
			this.groupBox1.Controls.Add(this.label43);
			this.groupBox1.Controls.Add(this.numBoxStartDay);
			this.groupBox1.Controls.Add(this.numBoxStartMonth);
			this.groupBox1.Controls.Add(this.numBoxStartYear);
			this.groupBox1.Controls.Add(this.numBoxOrgDay);
			this.groupBox1.Controls.Add(this.numBoxOrgMonth);
			this.groupBox1.Controls.Add(this.numBoxOrgYear);
			this.groupBox1.Controls.Add(this.numBoxTotalDay);
			this.groupBox1.Controls.Add(this.numBoxTotalYear);
			this.groupBox1.Controls.Add(this.numBoxTotalMonth);
			this.groupBox1.Location = new System.Drawing.Point(256, 368);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(480, 80);
			this.groupBox1.TabIndex = 106;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Трудов стаж  [ГГГ, ММ, ДД] ";
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(48, 24);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(120, 16);
			this.label19.TabIndex = 95;
			this.label19.Text = "При постъпване :";
			// 
			// label42
			// 
			this.label42.Location = new System.Drawing.Point(200, 24);
			this.label42.Name = "label42";
			this.label42.Size = new System.Drawing.Size(120, 16);
			this.label42.TabIndex = 96;
			this.label42.Text = "В администрацията:";
			// 
			// label43
			// 
			this.label43.Location = new System.Drawing.Point(336, 24);
			this.label43.Name = "label43";
			this.label43.Size = new System.Drawing.Size(120, 16);
			this.label43.TabIndex = 96;
			this.label43.Text = "Общо :";
			// 
			// numBoxStartDay
			// 
			this.numBoxStartDay.Location = new System.Drawing.Point(120, 48);
			this.numBoxStartDay.Name = "numBoxStartDay";
			this.numBoxStartDay.ReadOnly = true;
			this.numBoxStartDay.Size = new System.Drawing.Size(32, 20);
			this.numBoxStartDay.TabIndex = 87;
			this.numBoxStartDay.TabStop = false;
			this.numBoxStartDay.Text = "";
			// 
			// numBoxStartMonth
			// 
			this.numBoxStartMonth.Location = new System.Drawing.Point(80, 48);
			this.numBoxStartMonth.Name = "numBoxStartMonth";
			this.numBoxStartMonth.ReadOnly = true;
			this.numBoxStartMonth.Size = new System.Drawing.Size(32, 20);
			this.numBoxStartMonth.TabIndex = 86;
			this.numBoxStartMonth.TabStop = false;
			this.numBoxStartMonth.Text = "";
			// 
			// numBoxStartYear
			// 
			this.numBoxStartYear.Location = new System.Drawing.Point(40, 48);
			this.numBoxStartYear.Name = "numBoxStartYear";
			this.numBoxStartYear.ReadOnly = true;
			this.numBoxStartYear.Size = new System.Drawing.Size(28, 20);
			this.numBoxStartYear.TabIndex = 85;
			this.numBoxStartYear.TabStop = false;
			this.numBoxStartYear.Text = "";
			// 
			// numBoxOrgDay
			// 
			this.numBoxOrgDay.Location = new System.Drawing.Point(288, 48);
			this.numBoxOrgDay.Name = "numBoxOrgDay";
			this.numBoxOrgDay.ReadOnly = true;
			this.numBoxOrgDay.Size = new System.Drawing.Size(32, 20);
			this.numBoxOrgDay.TabIndex = 90;
			this.numBoxOrgDay.TabStop = false;
			this.numBoxOrgDay.Text = "";
			// 
			// numBoxOrgMonth
			// 
			this.numBoxOrgMonth.Location = new System.Drawing.Point(240, 48);
			this.numBoxOrgMonth.Name = "numBoxOrgMonth";
			this.numBoxOrgMonth.ReadOnly = true;
			this.numBoxOrgMonth.Size = new System.Drawing.Size(32, 20);
			this.numBoxOrgMonth.TabIndex = 89;
			this.numBoxOrgMonth.TabStop = false;
			this.numBoxOrgMonth.Text = "";
			// 
			// numBoxOrgYear
			// 
			this.numBoxOrgYear.Location = new System.Drawing.Point(200, 48);
			this.numBoxOrgYear.Name = "numBoxOrgYear";
			this.numBoxOrgYear.ReadOnly = true;
			this.numBoxOrgYear.Size = new System.Drawing.Size(32, 20);
			this.numBoxOrgYear.TabIndex = 88;
			this.numBoxOrgYear.TabStop = false;
			this.numBoxOrgYear.Text = "";
			// 
			// numBoxTotalDay
			// 
			this.numBoxTotalDay.Location = new System.Drawing.Point(424, 48);
			this.numBoxTotalDay.Name = "numBoxTotalDay";
			this.numBoxTotalDay.ReadOnly = true;
			this.numBoxTotalDay.Size = new System.Drawing.Size(32, 20);
			this.numBoxTotalDay.TabIndex = 93;
			this.numBoxTotalDay.TabStop = false;
			this.numBoxTotalDay.Text = "";
			// 
			// numBoxTotalYear
			// 
			this.numBoxTotalYear.Location = new System.Drawing.Point(344, 48);
			this.numBoxTotalYear.Name = "numBoxTotalYear";
			this.numBoxTotalYear.ReadOnly = true;
			this.numBoxTotalYear.Size = new System.Drawing.Size(32, 20);
			this.numBoxTotalYear.TabIndex = 92;
			this.numBoxTotalYear.TabStop = false;
			this.numBoxTotalYear.Text = "";
			// 
			// numBoxTotalMonth
			// 
			this.numBoxTotalMonth.Location = new System.Drawing.Point(384, 48);
			this.numBoxTotalMonth.Name = "numBoxTotalMonth";
			this.numBoxTotalMonth.ReadOnly = true;
			this.numBoxTotalMonth.Size = new System.Drawing.Size(32, 20);
			this.numBoxTotalMonth.TabIndex = 91;
			this.numBoxTotalMonth.TabStop = false;
			this.numBoxTotalMonth.Text = "";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.comboBoxLanguageLevel);
			this.groupBox3.Controls.Add(this.dateTimePickerPostypilNa);
			this.groupBox3.Controls.Add(this.labelHiredAt);
			this.groupBox3.Controls.Add(this.checkedListBoxLanguage);
			this.groupBox3.Controls.Add(this.labelLanguage);
			this.groupBox3.Controls.Add(this.labellanguageLevel);
			this.groupBox3.Controls.Add(this.labelMilitaryRang);
			this.groupBox3.Controls.Add(this.labelScience);
			this.groupBox3.Controls.Add(this.comboBoxMilitaryRang);
			this.groupBox3.Controls.Add(this.comboBoxScienceLevel);
			this.groupBox3.Controls.Add(this.comboBoxScience);
			this.groupBox3.Controls.Add(this.labelScienceLevel);
			this.groupBox3.Controls.Add(this.comboBoxProfesion);
			this.groupBox3.Controls.Add(this.label37);
			this.groupBox3.Controls.Add(this.label36);
			this.groupBox3.Controls.Add(this.label38);
			this.groupBox3.Controls.Add(this.textBoxDiplom);
			this.groupBox3.Controls.Add(this.comboBoxFamilyStatus);
			this.groupBox3.Controls.Add(this.comboBoxEducation);
			this.groupBox3.Controls.Add(this.labelCategory);
			this.groupBox3.Controls.Add(this.comboBoxCategory);
			this.groupBox3.Controls.Add(this.labelMilitaryStatus);
			this.groupBox3.Controls.Add(this.comboBoxMilitaryStatus);
			this.groupBox3.Controls.Add(this.labelProfesion);
			this.groupBox3.Location = new System.Drawing.Point(8, 192);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(736, 136);
			this.groupBox3.TabIndex = 105;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Лични данни";
			// 
			// comboBoxLanguageLevel
			// 
			this.comboBoxLanguageLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxLanguageLevel.Items.AddRange(new object[] {
																	   "Перфектно",
																	   "Писмено и говоримо",
																	   "Говоримо",
																	   "Средно",
																	   "Слабо"});
			this.comboBoxLanguageLevel.Location = new System.Drawing.Point(296, 112);
			this.comboBoxLanguageLevel.Name = "comboBoxLanguageLevel";
			this.comboBoxLanguageLevel.Size = new System.Drawing.Size(121, 21);
			this.comboBoxLanguageLevel.TabIndex = 111;
			this.comboBoxLanguageLevel.SelectedIndexChanged += new System.EventHandler(this.comboBoxLanguageLevel_SelectedIndexChanged);
			// 
			// dateTimePickerPostypilNa
			// 
			this.dateTimePickerPostypilNa.Location = new System.Drawing.Point(424, 112);
			this.dateTimePickerPostypilNa.Name = "dateTimePickerPostypilNa";
			this.dateTimePickerPostypilNa.Size = new System.Drawing.Size(136, 20);
			this.dateTimePickerPostypilNa.TabIndex = 25;
			this.dateTimePickerPostypilNa.Value = new System.DateTime(2005, 1, 12, 9, 43, 36, 687);
			this.dateTimePickerPostypilNa.ValueChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// labelHiredAt
			// 
			this.labelHiredAt.Location = new System.Drawing.Point(424, 96);
			this.labelHiredAt.Name = "labelHiredAt";
			this.labelHiredAt.Size = new System.Drawing.Size(120, 16);
			this.labelHiredAt.TabIndex = 110;
			this.labelHiredAt.Text = "Постъпил на:";
			// 
			// checkedListBoxLanguage
			// 
			this.checkedListBoxLanguage.Location = new System.Drawing.Point(160, 112);
			this.checkedListBoxLanguage.Name = "checkedListBoxLanguage";
			this.checkedListBoxLanguage.Size = new System.Drawing.Size(120, 19);
			this.checkedListBoxLanguage.TabIndex = 23;
			this.checkedListBoxLanguage.SelectedIndexChanged += new System.EventHandler(this.LanguageChanged);
			this.checkedListBoxLanguage.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxLanguage_ItemCheck);
			// 
			// labelLanguage
			// 
			this.labelLanguage.Location = new System.Drawing.Point(160, 96);
			this.labelLanguage.Name = "labelLanguage";
			this.labelLanguage.Size = new System.Drawing.Size(112, 16);
			this.labelLanguage.TabIndex = 106;
			this.labelLanguage.Text = "Чужди езици :";
			// 
			// labellanguageLevel
			// 
			this.labellanguageLevel.Location = new System.Drawing.Point(296, 96);
			this.labellanguageLevel.Name = "labellanguageLevel";
			this.labellanguageLevel.Size = new System.Drawing.Size(128, 16);
			this.labellanguageLevel.TabIndex = 108;
			this.labellanguageLevel.Text = "Степен на владеене";
			// 
			// labelMilitaryRang
			// 
			this.labelMilitaryRang.Location = new System.Drawing.Point(152, 56);
			this.labelMilitaryRang.Name = "labelMilitaryRang";
			this.labelMilitaryRang.Size = new System.Drawing.Size(120, 16);
			this.labelMilitaryRang.TabIndex = 92;
			this.labelMilitaryRang.Text = "Военен ранг";
			// 
			// labelScience
			// 
			this.labelScience.Location = new System.Drawing.Point(424, 56);
			this.labelScience.Name = "labelScience";
			this.labelScience.Size = new System.Drawing.Size(120, 16);
			this.labelScience.TabIndex = 88;
			this.labelScience.Text = "Научно звание";
			// 
			// comboBoxMilitaryRang
			// 
			this.comboBoxMilitaryRang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMilitaryRang.ItemHeight = 13;
			this.comboBoxMilitaryRang.Location = new System.Drawing.Point(152, 72);
			this.comboBoxMilitaryRang.Name = "comboBoxMilitaryRang";
			this.comboBoxMilitaryRang.Size = new System.Drawing.Size(121, 21);
			this.comboBoxMilitaryRang.TabIndex = 20;
			this.comboBoxMilitaryRang.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// comboBoxScienceLevel
			// 
			this.comboBoxScienceLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxScienceLevel.ItemHeight = 13;
			this.comboBoxScienceLevel.Location = new System.Drawing.Point(16, 72);
			this.comboBoxScienceLevel.Name = "comboBoxScienceLevel";
			this.comboBoxScienceLevel.Size = new System.Drawing.Size(121, 21);
			this.comboBoxScienceLevel.TabIndex = 19;
			this.comboBoxScienceLevel.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// comboBoxScience
			// 
			this.comboBoxScience.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxScience.ItemHeight = 13;
			this.comboBoxScience.Location = new System.Drawing.Point(424, 72);
			this.comboBoxScience.Name = "comboBoxScience";
			this.comboBoxScience.Size = new System.Drawing.Size(121, 21);
			this.comboBoxScience.TabIndex = 18;
			this.comboBoxScience.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// labelScienceLevel
			// 
			this.labelScienceLevel.Location = new System.Drawing.Point(16, 56);
			this.labelScienceLevel.Name = "labelScienceLevel";
			this.labelScienceLevel.Size = new System.Drawing.Size(120, 16);
			this.labelScienceLevel.TabIndex = 90;
			this.labelScienceLevel.Text = "Научна степен";
			// 
			// comboBoxProfesion
			// 
			this.comboBoxProfesion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxProfesion.ItemHeight = 13;
			this.comboBoxProfesion.Location = new System.Drawing.Point(552, 72);
			this.comboBoxProfesion.Name = "comboBoxProfesion";
			this.comboBoxProfesion.Size = new System.Drawing.Size(121, 21);
			this.comboBoxProfesion.TabIndex = 17;
			this.comboBoxProfesion.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// label37
			// 
			this.label37.Location = new System.Drawing.Point(16, 96);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(118, 16);
			this.label37.TabIndex = 103;
			this.label37.Text = "Семейно положение";
			// 
			// label36
			// 
			this.label36.Location = new System.Drawing.Point(16, 16);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(86, 16);
			this.label36.TabIndex = 102;
			this.label36.Text = "Образование :";
			// 
			// label38
			// 
			this.label38.Location = new System.Drawing.Point(144, 16);
			this.label38.Name = "label38";
			this.label38.Size = new System.Drawing.Size(110, 16);
			this.label38.TabIndex = 104;
			this.label38.Text = "Диплома данни :";
			// 
			// textBoxDiplom
			// 
			this.textBoxDiplom.Location = new System.Drawing.Point(144, 32);
			this.textBoxDiplom.Name = "textBoxDiplom";
			this.textBoxDiplom.Size = new System.Drawing.Size(584, 20);
			this.textBoxDiplom.TabIndex = 16;
			this.textBoxDiplom.Text = "";
			this.textBoxDiplom.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// comboBoxFamilyStatus
			// 
			this.comboBoxFamilyStatus.Location = new System.Drawing.Point(16, 112);
			this.comboBoxFamilyStatus.Name = "comboBoxFamilyStatus";
			this.comboBoxFamilyStatus.Size = new System.Drawing.Size(121, 21);
			this.comboBoxFamilyStatus.TabIndex = 15;
			this.comboBoxFamilyStatus.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// comboBoxEducation
			// 
			this.comboBoxEducation.Location = new System.Drawing.Point(16, 32);
			this.comboBoxEducation.Name = "comboBoxEducation";
			this.comboBoxEducation.Size = new System.Drawing.Size(121, 21);
			this.comboBoxEducation.TabIndex = 14;
			this.comboBoxEducation.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// labelCategory
			// 
			this.labelCategory.Location = new System.Drawing.Point(576, 96);
			this.labelCategory.Name = "labelCategory";
			this.labelCategory.Size = new System.Drawing.Size(120, 16);
			this.labelCategory.TabIndex = 98;
			this.labelCategory.Text = "Категория";
			// 
			// comboBoxCategory
			// 
			this.comboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxCategory.ItemHeight = 13;
			this.comboBoxCategory.Location = new System.Drawing.Point(576, 112);
			this.comboBoxCategory.Name = "comboBoxCategory";
			this.comboBoxCategory.Size = new System.Drawing.Size(121, 21);
			this.comboBoxCategory.TabIndex = 22;
			this.comboBoxCategory.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// labelMilitaryStatus
			// 
			this.labelMilitaryStatus.Location = new System.Drawing.Point(288, 56);
			this.labelMilitaryStatus.Name = "labelMilitaryStatus";
			this.labelMilitaryStatus.Size = new System.Drawing.Size(120, 16);
			this.labelMilitaryStatus.TabIndex = 94;
			this.labelMilitaryStatus.Text = "Военна отчетност";
			// 
			// comboBoxMilitaryStatus
			// 
			this.comboBoxMilitaryStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMilitaryStatus.ItemHeight = 13;
			this.comboBoxMilitaryStatus.Items.AddRange(new object[] {
																		"Отслужил",
																		"Неотслужил"});
			this.comboBoxMilitaryStatus.Location = new System.Drawing.Point(288, 72);
			this.comboBoxMilitaryStatus.Name = "comboBoxMilitaryStatus";
			this.comboBoxMilitaryStatus.Size = new System.Drawing.Size(121, 21);
			this.comboBoxMilitaryStatus.TabIndex = 21;
			this.comboBoxMilitaryStatus.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// labelProfesion
			// 
			this.labelProfesion.Location = new System.Drawing.Point(552, 56);
			this.labelProfesion.Name = "labelProfesion";
			this.labelProfesion.Size = new System.Drawing.Size(120, 16);
			this.labelProfesion.TabIndex = 86;
			this.labelProfesion.Text = "Професия";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.comboBoxPrefix);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.comboBoxSex);
			this.groupBox2.Controls.Add(this.numBoxPcCard);
			this.groupBox2.Controls.Add(this.labelJKkwartal);
			this.groupBox2.Controls.Add(this.dateTimePickerPCCardPublished);
			this.groupBox2.Controls.Add(this.labelPublishedByy);
			this.groupBox2.Controls.Add(this.labelPublishedBy);
			this.groupBox2.Controls.Add(this.textBoxPublishedFrom);
			this.groupBox2.Controls.Add(this.labelStreet);
			this.groupBox2.Controls.Add(this.textBoxStreet);
			this.groupBox2.Controls.Add(this.labelNumBlock);
			this.groupBox2.Controls.Add(this.textBoxNumBlock);
			this.groupBox2.Controls.Add(this.numBoxTelephone);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.labelKwartal);
			this.groupBox2.Controls.Add(this.textBoxKwartal);
			this.groupBox2.Controls.Add(this.comboBoxNaselenoMqsto);
			this.groupBox2.Controls.Add(this.labelNaselenoMqsto);
			this.groupBox2.Controls.Add(this.labelRegion);
			this.groupBox2.Controls.Add(this.comboBoxRegion);
			this.groupBox2.Controls.Add(this.numBoxEgn);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.textBoxNames);
			this.groupBox2.Controls.Add(this.labelNames);
			this.groupBox2.Controls.Add(this.comboBoxCountry);
			this.groupBox2.Controls.Add(this.labelCountry);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.textBoxBornTown);
			this.groupBox2.Location = new System.Drawing.Point(8, 8);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(736, 184);
			this.groupBox2.TabIndex = 65;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Паспортни данни";
			// 
			// comboBoxPrefix
			// 
			this.comboBoxPrefix.Location = new System.Drawing.Point(360, 72);
			this.comboBoxPrefix.Name = "comboBoxPrefix";
			this.comboBoxPrefix.Size = new System.Drawing.Size(40, 21);
			this.comboBoxPrefix.TabIndex = 87;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(536, 136);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 16);
			this.label5.TabIndex = 86;
			this.label5.Text = "Пол :";
			// 
			// comboBoxSex
			// 
			this.comboBoxSex.ItemHeight = 13;
			this.comboBoxSex.Location = new System.Drawing.Point(536, 152);
			this.comboBoxSex.Name = "comboBoxSex";
			this.comboBoxSex.Size = new System.Drawing.Size(80, 21);
			this.comboBoxSex.TabIndex = 13;
			this.comboBoxSex.SelectedIndexChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// numBoxPcCard
			// 
			this.numBoxPcCard.Location = new System.Drawing.Point(16, 152);
			this.numBoxPcCard.MaxLength = 255;
			this.numBoxPcCard.Name = "numBoxPcCard";
			this.numBoxPcCard.Size = new System.Drawing.Size(120, 20);
			this.numBoxPcCard.TabIndex = 10;
			this.numBoxPcCard.Text = "";
			this.numBoxPcCard.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// labelJKkwartal
			// 
			this.labelJKkwartal.Location = new System.Drawing.Point(16, 136);
			this.labelJKkwartal.Name = "labelJKkwartal";
			this.labelJKkwartal.Size = new System.Drawing.Size(112, 16);
			this.labelJKkwartal.TabIndex = 79;
			this.labelJKkwartal.Text = "Л.К. \\ Л.П. Номер :";
			// 
			// dateTimePickerPCCardPublished
			// 
			this.dateTimePickerPCCardPublished.Location = new System.Drawing.Point(336, 152);
			this.dateTimePickerPCCardPublished.Name = "dateTimePickerPCCardPublished";
			this.dateTimePickerPCCardPublished.Size = new System.Drawing.Size(176, 20);
			this.dateTimePickerPCCardPublished.TabIndex = 12;
			this.dateTimePickerPCCardPublished.Value = new System.DateTime(2005, 1, 12, 9, 33, 28, 578);
			this.dateTimePickerPCCardPublished.ValueChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// labelPublishedByy
			// 
			this.labelPublishedByy.Location = new System.Drawing.Point(336, 136);
			this.labelPublishedByy.Name = "labelPublishedByy";
			this.labelPublishedByy.Size = new System.Drawing.Size(136, 16);
			this.labelPublishedByy.TabIndex = 83;
			this.labelPublishedByy.Text = "Дата на издаване :";
			// 
			// labelPublishedBy
			// 
			this.labelPublishedBy.Location = new System.Drawing.Point(152, 136);
			this.labelPublishedBy.Name = "labelPublishedBy";
			this.labelPublishedBy.Size = new System.Drawing.Size(96, 16);
			this.labelPublishedBy.TabIndex = 81;
			this.labelPublishedBy.Text = "Издаден от :";
			// 
			// textBoxPublishedFrom
			// 
			this.textBoxPublishedFrom.Location = new System.Drawing.Point(152, 152);
			this.textBoxPublishedFrom.MaxLength = 255;
			this.textBoxPublishedFrom.Name = "textBoxPublishedFrom";
			this.textBoxPublishedFrom.Size = new System.Drawing.Size(168, 20);
			this.textBoxPublishedFrom.TabIndex = 11;
			this.textBoxPublishedFrom.Text = "";
			this.textBoxPublishedFrom.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// labelStreet
			// 
			this.labelStreet.Location = new System.Drawing.Point(16, 96);
			this.labelStreet.Name = "labelStreet";
			this.labelStreet.Size = new System.Drawing.Size(128, 16);
			this.labelStreet.TabIndex = 74;
			this.labelStreet.Text = "Улица/Булевард :";
			// 
			// textBoxStreet
			// 
			this.textBoxStreet.Location = new System.Drawing.Point(16, 112);
			this.textBoxStreet.Name = "textBoxStreet";
			this.textBoxStreet.Size = new System.Drawing.Size(296, 20);
			this.textBoxStreet.TabIndex = 7;
			this.textBoxStreet.Text = "";
			this.textBoxStreet.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// labelNumBlock
			// 
			this.labelNumBlock.Location = new System.Drawing.Point(328, 96);
			this.labelNumBlock.Name = "labelNumBlock";
			this.labelNumBlock.Size = new System.Drawing.Size(168, 16);
			this.labelNumBlock.TabIndex = 76;
			this.labelNumBlock.Text = "N:, Бл., вх., ет., ап. :";
			// 
			// textBoxNumBlock
			// 
			this.textBoxNumBlock.Location = new System.Drawing.Point(328, 112);
			this.textBoxNumBlock.Name = "textBoxNumBlock";
			this.textBoxNumBlock.Size = new System.Drawing.Size(192, 20);
			this.textBoxNumBlock.TabIndex = 8;
			this.textBoxNumBlock.Text = "";
			this.textBoxNumBlock.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// numBoxTelephone
			// 
			this.numBoxTelephone.Location = new System.Drawing.Point(544, 112);
			this.numBoxTelephone.Name = "numBoxTelephone";
			this.numBoxTelephone.Size = new System.Drawing.Size(168, 20);
			this.numBoxTelephone.TabIndex = 9;
			this.numBoxTelephone.Text = "";
			this.numBoxTelephone.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(544, 96);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 16);
			this.label3.TabIndex = 77;
			this.label3.Text = "Телефон :";
			// 
			// labelKwartal
			// 
			this.labelKwartal.Location = new System.Drawing.Point(568, 56);
			this.labelKwartal.Name = "labelKwartal";
			this.labelKwartal.Size = new System.Drawing.Size(120, 16);
			this.labelKwartal.TabIndex = 72;
			this.labelKwartal.Text = "Ж.К./Квартал :";
			// 
			// textBoxKwartal
			// 
			this.textBoxKwartal.Location = new System.Drawing.Point(568, 72);
			this.textBoxKwartal.MaxLength = 255;
			this.textBoxKwartal.Name = "textBoxKwartal";
			this.textBoxKwartal.Size = new System.Drawing.Size(144, 20);
			this.textBoxKwartal.TabIndex = 6;
			this.textBoxKwartal.Text = "";
			this.textBoxKwartal.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// comboBoxNaselenoMqsto
			// 
			this.comboBoxNaselenoMqsto.ItemHeight = 13;
			this.comboBoxNaselenoMqsto.Location = new System.Drawing.Point(400, 72);
			this.comboBoxNaselenoMqsto.Name = "comboBoxNaselenoMqsto";
			this.comboBoxNaselenoMqsto.Size = new System.Drawing.Size(160, 21);
			this.comboBoxNaselenoMqsto.TabIndex = 5;
			this.comboBoxNaselenoMqsto.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			this.comboBoxNaselenoMqsto.SelectedIndexChanged += new System.EventHandler(this.comboBoxNaselenoMqsto_SelectedIndexChanged);
			// 
			// labelNaselenoMqsto
			// 
			this.labelNaselenoMqsto.Location = new System.Drawing.Point(360, 56);
			this.labelNaselenoMqsto.Name = "labelNaselenoMqsto";
			this.labelNaselenoMqsto.Size = new System.Drawing.Size(104, 16);
			this.labelNaselenoMqsto.TabIndex = 69;
			this.labelNaselenoMqsto.Text = "Населено място :";
			// 
			// labelRegion
			// 
			this.labelRegion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.labelRegion.Location = new System.Drawing.Point(192, 56);
			this.labelRegion.Name = "labelRegion";
			this.labelRegion.Size = new System.Drawing.Size(56, 16);
			this.labelRegion.TabIndex = 68;
			this.labelRegion.Text = "Област :";
			// 
			// comboBoxRegion
			// 
			this.comboBoxRegion.ItemHeight = 13;
			this.comboBoxRegion.Location = new System.Drawing.Point(192, 72);
			this.comboBoxRegion.Name = "comboBoxRegion";
			this.comboBoxRegion.Size = new System.Drawing.Size(160, 21);
			this.comboBoxRegion.TabIndex = 4;
			this.toolTip1.SetToolTip(this.comboBoxRegion, "Област на местожижеене");
			this.comboBoxRegion.SelectedIndexChanged += new System.EventHandler(this.comboBoxRegion_SelectedIndexChanged);
			// 
			// numBoxEgn
			// 
			this.numBoxEgn.Location = new System.Drawing.Point(16, 32);
			this.numBoxEgn.MaxLength = 10;
			this.numBoxEgn.Name = "numBoxEgn";
			this.numBoxEgn.OnlyInteger = false;
			this.numBoxEgn.OnlyPositive = false;
			this.numBoxEgn.Size = new System.Drawing.Size(88, 20);
			this.numBoxEgn.TabIndex = 0;
			this.numBoxEgn.Text = "";
			this.toolTip1.SetToolTip(this.numBoxEgn, "Единен Граждански номер на лицето");
			this.numBoxEgn.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "ЕГН :";
			// 
			// textBoxNames
			// 
			this.textBoxNames.Location = new System.Drawing.Point(120, 32);
			this.textBoxNames.MaxLength = 255;
			this.textBoxNames.Name = "textBoxNames";
			this.textBoxNames.Size = new System.Drawing.Size(328, 20);
			this.textBoxNames.TabIndex = 1;
			this.textBoxNames.Text = "";
			this.textBoxNames.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// labelNames
			// 
			this.labelNames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.labelNames.Location = new System.Drawing.Point(120, 16);
			this.labelNames.Name = "labelNames";
			this.labelNames.Size = new System.Drawing.Size(136, 16);
			this.labelNames.TabIndex = 3;
			this.labelNames.Text = "Трите имена на лицето :";
			// 
			// comboBoxCountry
			// 
			this.comboBoxCountry.ItemHeight = 13;
			this.comboBoxCountry.Location = new System.Drawing.Point(464, 32);
			this.comboBoxCountry.Name = "comboBoxCountry";
			this.comboBoxCountry.Size = new System.Drawing.Size(248, 21);
			this.comboBoxCountry.TabIndex = 2;
			this.comboBoxCountry.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// labelCountry
			// 
			this.labelCountry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.labelCountry.Location = new System.Drawing.Point(464, 16);
			this.labelCountry.Name = "labelCountry";
			this.labelCountry.Size = new System.Drawing.Size(96, 16);
			this.labelCountry.TabIndex = 9;
			this.labelCountry.Text = "Гражданство :";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 56);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(128, 16);
			this.label4.TabIndex = 64;
			this.label4.Text = "Месторождение град :";
			// 
			// textBoxBornTown
			// 
			this.textBoxBornTown.Location = new System.Drawing.Point(16, 72);
			this.textBoxBornTown.Name = "textBoxBornTown";
			this.textBoxBornTown.Size = new System.Drawing.Size(136, 20);
			this.textBoxBornTown.TabIndex = 3;
			this.textBoxBornTown.Text = "";
			this.textBoxBornTown.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// tabPageAssignment
			// 
			this.tabPageAssignment.Controls.Add(this.dateTimePickerContractDate);
			this.tabPageAssignment.Controls.Add(this.label14);
			this.tabPageAssignment.Controls.Add(this.numBoxNumHoliday);
			this.tabPageAssignment.Controls.Add(this.numBoxMonthlyAddon);
			this.tabPageAssignment.Controls.Add(this.label46);
			this.tabPageAssignment.Controls.Add(this.label45);
			this.tabPageAssignment.Controls.Add(this.comboBoxYearlyAddon);
			this.tabPageAssignment.Controls.Add(this.numBoxBaseSalary);
			this.tabPageAssignment.Controls.Add(this.label17);
			this.tabPageAssignment.Controls.Add(this.label16);
			this.tabPageAssignment.Controls.Add(this.label15);
			this.tabPageAssignment.Controls.Add(this.label8);
			this.tabPageAssignment.Controls.Add(this.textBoxSalaryAddon);
			this.tabPageAssignment.Controls.Add(this.textBoxClassPercent);
			this.tabPageAssignment.Controls.Add(this.comboBoxWorkTime);
			this.tabPageAssignment.Controls.Add(this.label40);
			this.tabPageAssignment.Controls.Add(this.label11);
			this.tabPageAssignment.Controls.Add(this.textBoxNKPCode);
			this.tabPageAssignment.Controls.Add(this.textBoxNKPLevel);
			this.tabPageAssignment.Controls.Add(this.label18);
			this.tabPageAssignment.Controls.Add(this.comboBoxLaw);
			this.tabPageAssignment.Controls.Add(this.labelLevel1);
			this.tabPageAssignment.Controls.Add(this.comboBoxLevel1);
			this.tabPageAssignment.Controls.Add(this.label39);
			this.tabPageAssignment.Controls.Add(this.numBoxAssignmentExpD);
			this.tabPageAssignment.Controls.Add(this.numBoxAssignmentExtM);
			this.tabPageAssignment.Controls.Add(this.numBoxAssignmentExpY);
			this.tabPageAssignment.Controls.Add(this.buttonAssignmentCancel);
			this.tabPageAssignment.Controls.Add(this.buttonAssignmentPrint);
			this.tabPageAssignment.Controls.Add(this.dateTimePickerContractExpiry);
			this.tabPageAssignment.Controls.Add(this.buttonAssignmentDelete);
			this.tabPageAssignment.Controls.Add(this.buttonAssignmentSave);
			this.tabPageAssignment.Controls.Add(this.buttonAssignmentEdit);
			this.tabPageAssignment.Controls.Add(this.radioButtonAdditional);
			this.tabPageAssignment.Controls.Add(this.buttonAssignment);
			this.tabPageAssignment.Controls.Add(this.dateTimePickerAssignedAt);
			this.tabPageAssignment.Controls.Add(this.label13);
			this.tabPageAssignment.Controls.Add(this.label12);
			this.tabPageAssignment.Controls.Add(this.label10);
			this.tabPageAssignment.Controls.Add(this.label9);
			this.tabPageAssignment.Controls.Add(this.label7);
			this.tabPageAssignment.Controls.Add(this.label6);
			this.tabPageAssignment.Controls.Add(this.labelLevel4);
			this.tabPageAssignment.Controls.Add(this.labelLevel2);
			this.tabPageAssignment.Controls.Add(this.textBoxContractNumber);
			this.tabPageAssignment.Controls.Add(this.comboBoxAssignReason);
			this.tabPageAssignment.Controls.Add(this.comboBoxContract);
			this.tabPageAssignment.Controls.Add(this.comboBoxPosition);
			this.tabPageAssignment.Controls.Add(this.comboBoxLevel4);
			this.tabPageAssignment.Controls.Add(this.comboBoxLevel3);
			this.tabPageAssignment.Controls.Add(this.comboBoxLevel2);
			this.tabPageAssignment.Controls.Add(this.groupBoxAssignmentGrid);
			this.tabPageAssignment.Controls.Add(this.radioButtonAssignment);
			this.tabPageAssignment.Controls.Add(this.labelLevel3);
			this.tabPageAssignment.Controls.Add(this.label41);
			this.tabPageAssignment.Controls.Add(this.dateTimePickerTestPeriod);
			this.tabPageAssignment.Controls.Add(this.label49);
			this.tabPageAssignment.Location = new System.Drawing.Point(4, 22);
			this.tabPageAssignment.Name = "tabPageAssignment";
			this.tabPageAssignment.Size = new System.Drawing.Size(760, 462);
			this.tabPageAssignment.TabIndex = 2;
			this.tabPageAssignment.Text = "Назначаване";
			// 
			// dateTimePickerContractDate
			// 
			this.dateTimePickerContractDate.Location = new System.Drawing.Point(584, 200);
			this.dateTimePickerContractDate.Name = "dateTimePickerContractDate";
			this.dateTimePickerContractDate.Size = new System.Drawing.Size(168, 20);
			this.dateTimePickerContractDate.TabIndex = 87;
			this.dateTimePickerContractDate.Value = new System.DateTime(2005, 9, 12, 9, 43, 0, 0);
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(584, 184);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(104, 16);
			this.label14.TabIndex = 88;
			this.label14.Text = "Договор от дата:";
			// 
			// numBoxNumHoliday
			// 
			this.numBoxNumHoliday.Location = new System.Drawing.Point(624, 240);
			this.numBoxNumHoliday.Name = "numBoxNumHoliday";
			this.numBoxNumHoliday.TabIndex = 86;
			this.numBoxNumHoliday.Text = "";
			// 
			// numBoxMonthlyAddon
			// 
			this.numBoxMonthlyAddon.Location = new System.Drawing.Point(384, 240);
			this.numBoxMonthlyAddon.Name = "numBoxMonthlyAddon";
			this.numBoxMonthlyAddon.TabIndex = 77;
			this.numBoxMonthlyAddon.Text = "";
			// 
			// label46
			// 
			this.label46.Location = new System.Drawing.Point(384, 224);
			this.label46.Name = "label46";
			this.label46.Size = new System.Drawing.Size(112, 16);
			this.label46.TabIndex = 85;
			this.label46.Text = "Месечни надбавки :";
			// 
			// label45
			// 
			this.label45.Location = new System.Drawing.Point(496, 224);
			this.label45.Name = "label45";
			this.label45.Size = new System.Drawing.Size(112, 16);
			this.label45.TabIndex = 84;
			this.label45.Text = "Годишни надбавки :";
			// 
			// comboBoxYearlyAddon
			// 
			this.comboBoxYearlyAddon.Location = new System.Drawing.Point(496, 240);
			this.comboBoxYearlyAddon.Name = "comboBoxYearlyAddon";
			this.comboBoxYearlyAddon.Size = new System.Drawing.Size(104, 21);
			this.comboBoxYearlyAddon.TabIndex = 78;
			// 
			// numBoxBaseSalary
			// 
			this.numBoxBaseSalary.Location = new System.Drawing.Point(100, 240);
			this.numBoxBaseSalary.Name = "numBoxBaseSalary";
			this.numBoxBaseSalary.Size = new System.Drawing.Size(90, 20);
			this.numBoxBaseSalary.TabIndex = 73;
			this.numBoxBaseSalary.Text = "";
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(192, 224);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(88, 16);
			this.label17.TabIndex = 83;
			this.label17.Text = "Добавки О.З. %";
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(288, 224);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(88, 16);
			this.label16.TabIndex = 82;
			this.label16.Text = "% пр. време:";
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(96, 224);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(80, 16);
			this.label15.TabIndex = 81;
			this.label15.Text = "Осн. заплата:";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(8, 224);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100, 16);
			this.label8.TabIndex = 79;
			this.label8.Text = "Работно време:";
			// 
			// textBoxSalaryAddon
			// 
			this.textBoxSalaryAddon.Location = new System.Drawing.Point(195, 240);
			this.textBoxSalaryAddon.Name = "textBoxSalaryAddon";
			this.textBoxSalaryAddon.Size = new System.Drawing.Size(90, 20);
			this.textBoxSalaryAddon.TabIndex = 74;
			this.textBoxSalaryAddon.Text = "22";
			// 
			// textBoxClassPercent
			// 
			this.textBoxClassPercent.Location = new System.Drawing.Point(288, 240);
			this.textBoxClassPercent.Name = "textBoxClassPercent";
			this.textBoxClassPercent.Size = new System.Drawing.Size(90, 20);
			this.textBoxClassPercent.TabIndex = 75;
			this.textBoxClassPercent.Text = "";
			// 
			// comboBoxWorkTime
			// 
			this.comboBoxWorkTime.Location = new System.Drawing.Point(8, 240);
			this.comboBoxWorkTime.Name = "comboBoxWorkTime";
			this.comboBoxWorkTime.Size = new System.Drawing.Size(90, 21);
			this.comboBoxWorkTime.TabIndex = 72;
			// 
			// label40
			// 
			this.label40.Location = new System.Drawing.Point(624, 104);
			this.label40.Name = "label40";
			this.label40.Size = new System.Drawing.Size(100, 16);
			this.label40.TabIndex = 71;
			this.label40.Text = "Код по НКП";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(360, 104);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(100, 16);
			this.label11.TabIndex = 70;
			this.label11.Text = "Длъжност по НКП";
			// 
			// textBoxNKPCode
			// 
			this.textBoxNKPCode.Location = new System.Drawing.Point(624, 120);
			this.textBoxNKPCode.Name = "textBoxNKPCode";
			this.textBoxNKPCode.ReadOnly = true;
			this.textBoxNKPCode.Size = new System.Drawing.Size(128, 20);
			this.textBoxNKPCode.TabIndex = 69;
			this.textBoxNKPCode.Text = "";
			// 
			// textBoxNKPLevel
			// 
			this.textBoxNKPLevel.Location = new System.Drawing.Point(360, 120);
			this.textBoxNKPLevel.Name = "textBoxNKPLevel";
			this.textBoxNKPLevel.ReadOnly = true;
			this.textBoxNKPLevel.Size = new System.Drawing.Size(256, 20);
			this.textBoxNKPLevel.TabIndex = 68;
			this.textBoxNKPLevel.Text = "";
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(264, 104);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(104, 16);
			this.label18.TabIndex = 59;
			this.label18.Text = "Правоотношение :";
			// 
			// comboBoxLaw
			// 
			this.comboBoxLaw.Location = new System.Drawing.Point(264, 120);
			this.comboBoxLaw.Name = "comboBoxLaw";
			this.comboBoxLaw.Size = new System.Drawing.Size(88, 21);
			this.comboBoxLaw.TabIndex = 5;
			// 
			// labelLevel1
			// 
			this.labelLevel1.Location = new System.Drawing.Point(8, 24);
			this.labelLevel1.Name = "labelLevel1";
			this.labelLevel1.Size = new System.Drawing.Size(112, 16);
			this.labelLevel1.TabIndex = 57;
			this.labelLevel1.Text = "Администрация:";
			// 
			// comboBoxLevel1
			// 
			this.comboBoxLevel1.DropDownWidth = 370;
			this.comboBoxLevel1.Location = new System.Drawing.Point(8, 40);
			this.comboBoxLevel1.Name = "comboBoxLevel1";
			this.comboBoxLevel1.Size = new System.Drawing.Size(250, 21);
			this.comboBoxLevel1.TabIndex = 0;
			this.comboBoxLevel1.SelectedIndexChanged += new System.EventHandler(this.comboBoxLevel1_SelectedIndexChanged);
			// 
			// label39
			// 
			this.label39.Location = new System.Drawing.Point(432, 184);
			this.label39.Name = "label39";
			this.label39.Size = new System.Drawing.Size(160, 16);
			this.label39.TabIndex = 52;
			this.label39.Text = "Трудов стаж (ГГ, ММ, ДД] :";
			// 
			// numBoxAssignmentExpD
			// 
			this.numBoxAssignmentExpD.Location = new System.Drawing.Point(544, 200);
			this.numBoxAssignmentExpD.MaxLength = 2;
			this.numBoxAssignmentExpD.Name = "numBoxAssignmentExpD";
			this.numBoxAssignmentExpD.Size = new System.Drawing.Size(32, 20);
			this.numBoxAssignmentExpD.TabIndex = 21;
			this.numBoxAssignmentExpD.Text = "0";
			// 
			// numBoxAssignmentExtM
			// 
			this.numBoxAssignmentExtM.Location = new System.Drawing.Point(496, 200);
			this.numBoxAssignmentExtM.MaxLength = 2;
			this.numBoxAssignmentExtM.Name = "numBoxAssignmentExtM";
			this.numBoxAssignmentExtM.Size = new System.Drawing.Size(32, 20);
			this.numBoxAssignmentExtM.TabIndex = 20;
			this.numBoxAssignmentExtM.Text = "0";
			// 
			// numBoxAssignmentExpY
			// 
			this.numBoxAssignmentExpY.Location = new System.Drawing.Point(440, 200);
			this.numBoxAssignmentExpY.MaxLength = 3;
			this.numBoxAssignmentExpY.Name = "numBoxAssignmentExpY";
			this.numBoxAssignmentExpY.Size = new System.Drawing.Size(48, 20);
			this.numBoxAssignmentExpY.TabIndex = 19;
			this.numBoxAssignmentExpY.Text = "0";
			this.numBoxAssignmentExpY.TextChanged += new System.EventHandler(this.numBoxAssignmentExpY_TextChanged);
			// 
			// buttonAssignmentCancel
			// 
			this.buttonAssignmentCancel.Location = new System.Drawing.Point(128, 424);
			this.buttonAssignmentCancel.Name = "buttonAssignmentCancel";
			this.buttonAssignmentCancel.Size = new System.Drawing.Size(120, 23);
			this.buttonAssignmentCancel.TabIndex = 24;
			this.buttonAssignmentCancel.Text = "Отказ";
			this.buttonAssignmentCancel.Click += new System.EventHandler(this.buttonAssignmentCancel_Click);
			// 
			// buttonAssignmentPrint
			// 
			this.buttonAssignmentPrint.Location = new System.Drawing.Point(8, 424);
			this.buttonAssignmentPrint.Name = "buttonAssignmentPrint";
			this.buttonAssignmentPrint.Size = new System.Drawing.Size(96, 23);
			this.buttonAssignmentPrint.TabIndex = 22;
			this.buttonAssignmentPrint.Text = "Печат";
			this.buttonAssignmentPrint.Click += new System.EventHandler(this.buttonPrintD_Click);
			// 
			// dateTimePickerContractExpiry
			// 
			this.dateTimePickerContractExpiry.Location = new System.Drawing.Point(296, 200);
			this.dateTimePickerContractExpiry.Name = "dateTimePickerContractExpiry";
			this.dateTimePickerContractExpiry.Size = new System.Drawing.Size(136, 20);
			this.dateTimePickerContractExpiry.TabIndex = 17;
			this.dateTimePickerContractExpiry.Value = new System.DateTime(2005, 9, 12, 9, 43, 0, 0);
			// 
			// buttonAssignmentDelete
			// 
			this.buttonAssignmentDelete.Location = new System.Drawing.Point(392, 424);
			this.buttonAssignmentDelete.Name = "buttonAssignmentDelete";
			this.buttonAssignmentDelete.Size = new System.Drawing.Size(104, 23);
			this.buttonAssignmentDelete.TabIndex = 26;
			this.buttonAssignmentDelete.Text = "Премахва";
			this.buttonAssignmentDelete.Click += new System.EventHandler(this.buttonAssignmentDelete_Click);
			// 
			// buttonAssignmentSave
			// 
			this.buttonAssignmentSave.Location = new System.Drawing.Point(264, 424);
			this.buttonAssignmentSave.Name = "buttonAssignmentSave";
			this.buttonAssignmentSave.Size = new System.Drawing.Size(104, 23);
			this.buttonAssignmentSave.TabIndex = 25;
			this.buttonAssignmentSave.Text = "Запис";
			this.buttonAssignmentSave.Click += new System.EventHandler(this.buttonAssignmentSave_Click);
			// 
			// buttonAssignmentEdit
			// 
			this.buttonAssignmentEdit.Location = new System.Drawing.Point(664, 424);
			this.buttonAssignmentEdit.Name = "buttonAssignmentEdit";
			this.buttonAssignmentEdit.Size = new System.Drawing.Size(88, 23);
			this.buttonAssignmentEdit.TabIndex = 28;
			this.buttonAssignmentEdit.Text = "Корекция";
			this.buttonAssignmentEdit.Click += new System.EventHandler(this.buttonAssignmentEdit_Click);
			// 
			// radioButtonAdditional
			// 
			this.radioButtonAdditional.Location = new System.Drawing.Point(360, 8);
			this.radioButtonAdditional.Name = "radioButtonAdditional";
			this.radioButtonAdditional.Size = new System.Drawing.Size(184, 16);
			this.radioButtonAdditional.TabIndex = 30;
			this.radioButtonAdditional.Text = "Допълнителни споразумeния";
			this.radioButtonAdditional.CheckedChanged += new System.EventHandler(this.radioButtonAdditional_CheckedChanged);
			// 
			// buttonAssignment
			// 
			this.buttonAssignment.Location = new System.Drawing.Point(528, 424);
			this.buttonAssignment.Name = "buttonAssignment";
			this.buttonAssignment.Size = new System.Drawing.Size(112, 23);
			this.buttonAssignment.TabIndex = 27;
			this.buttonAssignment.Text = "Назначаване";
			this.buttonAssignment.Click += new System.EventHandler(this.buttonAssignment_Click);
			// 
			// dateTimePickerAssignedAt
			// 
			this.dateTimePickerAssignedAt.Location = new System.Drawing.Point(8, 200);
			this.dateTimePickerAssignedAt.Name = "dateTimePickerAssignedAt";
			this.dateTimePickerAssignedAt.Size = new System.Drawing.Size(136, 20);
			this.dateTimePickerAssignedAt.TabIndex = 16;
			this.dateTimePickerAssignedAt.Value = new System.DateTime(2005, 1, 12, 9, 43, 37, 546);
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(208, 144);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(152, 16);
			this.label13.TabIndex = 28;
			this.label13.Text = "Основание за назначаване:";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(584, 144);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(72, 16);
			this.label12.TabIndex = 27;
			this.label12.Text = "Договор N:";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(8, 184);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(100, 16);
			this.label10.TabIndex = 25;
			this.label10.Text = "Назначен на:";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(296, 184);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(80, 16);
			this.label9.TabIndex = 24;
			this.label9.Text = "Договор до :";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 144);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(80, 16);
			this.label7.TabIndex = 22;
			this.label7.Text = "Тип договор:";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 104);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 16);
			this.label6.TabIndex = 21;
			this.label6.Text = "Длъжност :";
			// 
			// labelLevel4
			// 
			this.labelLevel4.Location = new System.Drawing.Point(360, 64);
			this.labelLevel4.Name = "labelLevel4";
			this.labelLevel4.Size = new System.Drawing.Size(72, 16);
			this.labelLevel4.TabIndex = 20;
			this.labelLevel4.Text = "Сектор :";
			// 
			// labelLevel2
			// 
			this.labelLevel2.Location = new System.Drawing.Point(264, 24);
			this.labelLevel2.Name = "labelLevel2";
			this.labelLevel2.Size = new System.Drawing.Size(72, 16);
			this.labelLevel2.TabIndex = 18;
			this.labelLevel2.Text = "Дирекция :";
			// 
			// textBoxContractNumber
			// 
			this.textBoxContractNumber.Location = new System.Drawing.Point(584, 160);
			this.textBoxContractNumber.Name = "textBoxContractNumber";
			this.textBoxContractNumber.Size = new System.Drawing.Size(168, 20);
			this.textBoxContractNumber.TabIndex = 7;
			this.textBoxContractNumber.Text = "";
			// 
			// comboBoxAssignReason
			// 
			this.comboBoxAssignReason.Location = new System.Drawing.Point(208, 160);
			this.comboBoxAssignReason.Name = "comboBoxAssignReason";
			this.comboBoxAssignReason.Size = new System.Drawing.Size(370, 21);
			this.comboBoxAssignReason.TabIndex = 8;
			// 
			// comboBoxContract
			// 
			this.comboBoxContract.Items.AddRange(new object[] {
																  "Безсрочен",
																  "Безсрочен със срок на изпитване",
																  "Срочен",
																  "Срочен със срок на изпитване"});
			this.comboBoxContract.Location = new System.Drawing.Point(8, 160);
			this.comboBoxContract.Name = "comboBoxContract";
			this.comboBoxContract.Size = new System.Drawing.Size(200, 21);
			this.comboBoxContract.TabIndex = 6;
			this.comboBoxContract.SelectedIndexChanged += new System.EventHandler(this.comboBoxContract_SelectedIndexChanged);
			// 
			// comboBoxPosition
			// 
			this.comboBoxPosition.Location = new System.Drawing.Point(8, 120);
			this.comboBoxPosition.Name = "comboBoxPosition";
			this.comboBoxPosition.Size = new System.Drawing.Size(256, 21);
			this.comboBoxPosition.TabIndex = 4;
			this.comboBoxPosition.SelectedIndexChanged += new System.EventHandler(this.comboBoxPosition_SelectedIndexChanged);
			// 
			// comboBoxLevel4
			// 
			this.comboBoxLevel4.DropDownWidth = 370;
			this.comboBoxLevel4.Location = new System.Drawing.Point(360, 80);
			this.comboBoxLevel4.Name = "comboBoxLevel4";
			this.comboBoxLevel4.Size = new System.Drawing.Size(392, 21);
			this.comboBoxLevel4.TabIndex = 3;
			this.comboBoxLevel4.SelectedIndexChanged += new System.EventHandler(this.comboBoxLevel4_SelectedIndexChanged);
			// 
			// comboBoxLevel3
			// 
			this.comboBoxLevel3.DropDownWidth = 370;
			this.comboBoxLevel3.Location = new System.Drawing.Point(8, 80);
			this.comboBoxLevel3.Name = "comboBoxLevel3";
			this.comboBoxLevel3.Size = new System.Drawing.Size(344, 21);
			this.comboBoxLevel3.TabIndex = 2;
			this.comboBoxLevel3.SelectedIndexChanged += new System.EventHandler(this.comboBoxLevel3_SelectedIndexChanged);
			// 
			// comboBoxLevel2
			// 
			this.comboBoxLevel2.DropDownWidth = 370;
			this.comboBoxLevel2.Location = new System.Drawing.Point(264, 40);
			this.comboBoxLevel2.Name = "comboBoxLevel2";
			this.comboBoxLevel2.Size = new System.Drawing.Size(488, 21);
			this.comboBoxLevel2.TabIndex = 1;
			this.comboBoxLevel2.SelectedIndexChanged += new System.EventHandler(this.comboBoxLevel2_SelectedIndexChanged);
			// 
			// groupBoxAssignmentGrid
			// 
			this.groupBoxAssignmentGrid.Controls.Add(this.dataGridAssignment);
			this.groupBoxAssignmentGrid.Location = new System.Drawing.Point(8, 264);
			this.groupBoxAssignmentGrid.Name = "groupBoxAssignmentGrid";
			this.groupBoxAssignmentGrid.Size = new System.Drawing.Size(744, 152);
			this.groupBoxAssignmentGrid.TabIndex = 48;
			this.groupBoxAssignmentGrid.TabStop = false;
			this.groupBoxAssignmentGrid.Text = "Регистър на назначенията";
			// 
			// dataGridAssignment
			// 
			this.dataGridAssignment.AllowSorting = false;
			this.dataGridAssignment.DataMember = "";
			this.dataGridAssignment.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridAssignment.Location = new System.Drawing.Point(8, 16);
			this.dataGridAssignment.Name = "dataGridAssignment";
			this.dataGridAssignment.ReadOnly = true;
			this.dataGridAssignment.Size = new System.Drawing.Size(728, 128);
			this.dataGridAssignment.TabIndex = 47;
			this.dataGridAssignment.TabStop = false;
			this.dataGridAssignment.Click += new System.EventHandler(this.dataGridAssignment_Click);
			// 
			// radioButtonAssignment
			// 
			this.radioButtonAssignment.Checked = true;
			this.radioButtonAssignment.Location = new System.Drawing.Point(16, 8);
			this.radioButtonAssignment.Name = "radioButtonAssignment";
			this.radioButtonAssignment.Size = new System.Drawing.Size(88, 16);
			this.radioButtonAssignment.TabIndex = 29;
			this.radioButtonAssignment.TabStop = true;
			this.radioButtonAssignment.Text = "Назначение";
			// 
			// labelLevel3
			// 
			this.labelLevel3.Location = new System.Drawing.Point(8, 64);
			this.labelLevel3.Name = "labelLevel3";
			this.labelLevel3.Size = new System.Drawing.Size(80, 16);
			this.labelLevel3.TabIndex = 19;
			this.labelLevel3.Text = "Отдел :";
			// 
			// label41
			// 
			this.label41.ForeColor = System.Drawing.Color.Black;
			this.label41.Location = new System.Drawing.Point(624, 224);
			this.label41.Name = "label41";
			this.label41.Size = new System.Drawing.Size(112, 16);
			this.label41.TabIndex = 54;
			this.label41.Text = "Полагаем отпуск :";
			// 
			// dateTimePickerTestPeriod
			// 
			this.dateTimePickerTestPeriod.Location = new System.Drawing.Point(152, 200);
			this.dateTimePickerTestPeriod.Name = "dateTimePickerTestPeriod";
			this.dateTimePickerTestPeriod.Size = new System.Drawing.Size(136, 20);
			this.dateTimePickerTestPeriod.TabIndex = 16;
			this.dateTimePickerTestPeriod.Value = new System.DateTime(2005, 1, 12, 9, 43, 37, 546);
			// 
			// label49
			// 
			this.label49.Location = new System.Drawing.Point(152, 184);
			this.label49.Name = "label49";
			this.label49.Size = new System.Drawing.Size(120, 16);
			this.label49.TabIndex = 25;
			this.label49.Text = "Изпитателен срок до:";
			// 
			// tabPageAbsence
			// 
			this.tabPageAbsence.Controls.Add(this.buttonAbsenceCancel);
			this.tabPageAbsence.Controls.Add(this.buttonAbsencePrint);
			this.tabPageAbsence.Controls.Add(this.buttonAbsenceDelete);
			this.tabPageAbsence.Controls.Add(this.buttonAbsenceSave);
			this.tabPageAbsence.Controls.Add(this.buttonAbsenceEdit);
			this.tabPageAbsence.Controls.Add(this.buttonAbsenceAdd);
			this.tabPageAbsence.Controls.Add(this.groupBoxAbsenceGrid);
			this.tabPageAbsence.Controls.Add(this.groupBoxAbsece);
			this.tabPageAbsence.Location = new System.Drawing.Point(4, 22);
			this.tabPageAbsence.Name = "tabPageAbsence";
			this.tabPageAbsence.Size = new System.Drawing.Size(760, 462);
			this.tabPageAbsence.TabIndex = 3;
			this.tabPageAbsence.Text = "Отсъствия";
			// 
			// buttonAbsenceCancel
			// 
			this.buttonAbsenceCancel.Location = new System.Drawing.Point(272, 432);
			this.buttonAbsenceCancel.Name = "buttonAbsenceCancel";
			this.buttonAbsenceCancel.TabIndex = 11;
			this.buttonAbsenceCancel.Text = "Отказ";
			this.buttonAbsenceCancel.Click += new System.EventHandler(this.buttonAbsenceCancel_Click);
			// 
			// buttonAbsencePrint
			// 
			this.buttonAbsencePrint.Location = new System.Drawing.Point(64, 432);
			this.buttonAbsencePrint.Name = "buttonAbsencePrint";
			this.buttonAbsencePrint.TabIndex = 10;
			this.buttonAbsencePrint.Text = "Печат";
			// 
			// buttonAbsenceDelete
			// 
			this.buttonAbsenceDelete.Location = new System.Drawing.Point(368, 432);
			this.buttonAbsenceDelete.Name = "buttonAbsenceDelete";
			this.buttonAbsenceDelete.TabIndex = 9;
			this.buttonAbsenceDelete.Text = "Премахва";
			this.buttonAbsenceDelete.Click += new System.EventHandler(this.buttonAbsenceDelete_Click);
			// 
			// buttonAbsenceSave
			// 
			this.buttonAbsenceSave.Location = new System.Drawing.Point(168, 432);
			this.buttonAbsenceSave.Name = "buttonAbsenceSave";
			this.buttonAbsenceSave.TabIndex = 8;
			this.buttonAbsenceSave.Text = "Запис";
			this.buttonAbsenceSave.Click += new System.EventHandler(this.buttonAbsenceSave_Click);
			// 
			// buttonAbsenceEdit
			// 
			this.buttonAbsenceEdit.Location = new System.Drawing.Point(576, 432);
			this.buttonAbsenceEdit.Name = "buttonAbsenceEdit";
			this.buttonAbsenceEdit.TabIndex = 7;
			this.buttonAbsenceEdit.Text = "Корекция";
			this.buttonAbsenceEdit.Click += new System.EventHandler(this.buttonAbsenceEdit_Click);
			// 
			// buttonAbsenceAdd
			// 
			this.buttonAbsenceAdd.Location = new System.Drawing.Point(488, 432);
			this.buttonAbsenceAdd.Name = "buttonAbsenceAdd";
			this.buttonAbsenceAdd.TabIndex = 6;
			this.buttonAbsenceAdd.Text = "Отсъствие";
			this.buttonAbsenceAdd.Click += new System.EventHandler(this.buttonAbsenceAdd_Click);
			// 
			// groupBoxAbsenceGrid
			// 
			this.groupBoxAbsenceGrid.Controls.Add(this.groupBox5);
			this.groupBoxAbsenceGrid.Controls.Add(this.groupBox4);
			this.groupBoxAbsenceGrid.Location = new System.Drawing.Point(8, 128);
			this.groupBoxAbsenceGrid.Name = "groupBoxAbsenceGrid";
			this.groupBoxAbsenceGrid.Size = new System.Drawing.Size(744, 280);
			this.groupBoxAbsenceGrid.TabIndex = 2;
			this.groupBoxAbsenceGrid.TabStop = false;
			this.groupBoxAbsenceGrid.Text = "Регистър на отсъствията на служителя";
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.dataGridYears);
			this.groupBox5.Controls.Add(this.buttonHistory);
			this.groupBox5.Location = new System.Drawing.Point(520, 16);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(216, 256);
			this.groupBox5.TabIndex = 15;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Отпуски";
			// 
			// dataGridYears
			// 
			this.dataGridYears.DataMember = "";
			this.dataGridYears.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridYears.Location = new System.Drawing.Point(8, 16);
			this.dataGridYears.Name = "dataGridYears";
			this.dataGridYears.ReadOnly = true;
			this.dataGridYears.Size = new System.Drawing.Size(200, 200);
			this.dataGridYears.TabIndex = 14;
			// 
			// buttonHistory
			// 
			this.buttonHistory.Location = new System.Drawing.Point(8, 224);
			this.buttonHistory.Name = "buttonHistory";
			this.buttonHistory.Size = new System.Drawing.Size(200, 24);
			this.buttonHistory.TabIndex = 13;
			this.buttonHistory.Text = "Корекция на история отпуски";
			this.buttonHistory.Click += new System.EventHandler(this.buttonHistory_Click);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.dataGridAbsence);
			this.groupBox4.Location = new System.Drawing.Point(8, 16);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(504, 256);
			this.groupBox4.TabIndex = 14;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Всички отсъствия";
			// 
			// dataGridAbsence
			// 
			this.dataGridAbsence.DataMember = "";
			this.dataGridAbsence.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridAbsence.Location = new System.Drawing.Point(8, 16);
			this.dataGridAbsence.Name = "dataGridAbsence";
			this.dataGridAbsence.ReadOnly = true;
			this.dataGridAbsence.Size = new System.Drawing.Size(488, 232);
			this.dataGridAbsence.TabIndex = 0;
			this.dataGridAbsence.Click += new System.EventHandler(this.dataGridAbsence_Click);
			// 
			// groupBoxAbsece
			// 
			this.groupBoxAbsece.Controls.Add(this.label29);
			this.groupBoxAbsece.Controls.Add(this.comboBoxForYear);
			this.groupBoxAbsece.Controls.Add(this.dateTimePickerAbsenceOrderFormData);
			this.groupBoxAbsece.Controls.Add(this.label28);
			this.groupBoxAbsece.Controls.Add(this.label27);
			this.groupBoxAbsece.Controls.Add(this.textBoxAbsenceNumberOrder);
			this.groupBoxAbsece.Controls.Add(this.label26);
			this.groupBoxAbsece.Controls.Add(this.textBoxAbsenceReason);
			this.groupBoxAbsece.Controls.Add(this.label25);
			this.groupBoxAbsece.Controls.Add(this.comboBoxAbsenceTypeAbsence);
			this.groupBoxAbsece.Controls.Add(this.label24);
			this.groupBoxAbsece.Controls.Add(this.numBoxAbsenceDays);
			this.groupBoxAbsece.Controls.Add(this.label23);
			this.groupBoxAbsece.Controls.Add(this.label22);
			this.groupBoxAbsece.Controls.Add(this.dateTimePickerAbsenceToData);
			this.groupBoxAbsece.Controls.Add(this.dateTimePickerAbsenceFromData);
			this.groupBoxAbsece.Location = new System.Drawing.Point(8, 8);
			this.groupBoxAbsece.Name = "groupBoxAbsece";
			this.groupBoxAbsece.Size = new System.Drawing.Size(736, 104);
			this.groupBoxAbsece.TabIndex = 0;
			this.groupBoxAbsece.TabStop = false;
			this.groupBoxAbsece.Text = "Данни за отсъствие";
			// 
			// label29
			// 
			this.label29.Location = new System.Drawing.Point(576, 16);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(100, 16);
			this.label29.TabIndex = 16;
			this.label29.Text = "За година :";
			// 
			// comboBoxForYear
			// 
			this.comboBoxForYear.Location = new System.Drawing.Point(576, 32);
			this.comboBoxForYear.Name = "comboBoxForYear";
			this.comboBoxForYear.Size = new System.Drawing.Size(112, 21);
			this.comboBoxForYear.TabIndex = 15;
			// 
			// dateTimePickerAbsenceOrderFormData
			// 
			this.dateTimePickerAbsenceOrderFormData.Location = new System.Drawing.Point(424, 72);
			this.dateTimePickerAbsenceOrderFormData.Name = "dateTimePickerAbsenceOrderFormData";
			this.dateTimePickerAbsenceOrderFormData.Size = new System.Drawing.Size(136, 20);
			this.dateTimePickerAbsenceOrderFormData.TabIndex = 14;
			this.dateTimePickerAbsenceOrderFormData.Value = new System.DateTime(2005, 1, 12, 9, 43, 38, 312);
			// 
			// label28
			// 
			this.label28.Location = new System.Drawing.Point(408, 56);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(136, 16);
			this.label28.TabIndex = 13;
			this.label28.Text = "Заповед от дата :";
			// 
			// label27
			// 
			this.label27.Location = new System.Drawing.Point(288, 56);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(120, 16);
			this.label27.TabIndex = 11;
			this.label27.Text = "Номер заповед :";
			// 
			// textBoxAbsenceNumberOrder
			// 
			this.textBoxAbsenceNumberOrder.Location = new System.Drawing.Point(288, 72);
			this.textBoxAbsenceNumberOrder.Name = "textBoxAbsenceNumberOrder";
			this.textBoxAbsenceNumberOrder.Size = new System.Drawing.Size(128, 20);
			this.textBoxAbsenceNumberOrder.TabIndex = 10;
			this.textBoxAbsenceNumberOrder.Text = "";
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(16, 56);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(136, 16);
			this.label26.TabIndex = 9;
			this.label26.Text = "Основание/Бележки :";
			// 
			// textBoxAbsenceReason
			// 
			this.textBoxAbsenceReason.Location = new System.Drawing.Point(16, 72);
			this.textBoxAbsenceReason.Name = "textBoxAbsenceReason";
			this.textBoxAbsenceReason.Size = new System.Drawing.Size(264, 20);
			this.textBoxAbsenceReason.TabIndex = 8;
			this.textBoxAbsenceReason.Text = "";
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(376, 16);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(136, 16);
			this.label25.TabIndex = 7;
			this.label25.Text = "Вид отсъствие :";
			// 
			// comboBoxAbsenceTypeAbsence
			// 
			this.comboBoxAbsenceTypeAbsence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxAbsenceTypeAbsence.Items.AddRange(new object[] {
																			"Полагаем годишен отпуск",
																			"Болнични",
																			"Неплатен отпуск",
																			"Отглеждане на дете",
																			"Командировка"});
			this.comboBoxAbsenceTypeAbsence.Location = new System.Drawing.Point(376, 32);
			this.comboBoxAbsenceTypeAbsence.Name = "comboBoxAbsenceTypeAbsence";
			this.comboBoxAbsenceTypeAbsence.Size = new System.Drawing.Size(184, 21);
			this.comboBoxAbsenceTypeAbsence.TabIndex = 6;
			this.comboBoxAbsenceTypeAbsence.SelectedIndexChanged += new System.EventHandler(this.comboBoxAbsenceTypeAbsence_SelectedIndexChanged);
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(312, 16);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(64, 16);
			this.label24.TabIndex = 5;
			this.label24.Text = "Брой дни :";
			// 
			// numBoxAbsenceDays
			// 
			this.numBoxAbsenceDays.Location = new System.Drawing.Point(312, 32);
			this.numBoxAbsenceDays.Name = "numBoxAbsenceDays";
			this.numBoxAbsenceDays.Size = new System.Drawing.Size(56, 20);
			this.numBoxAbsenceDays.TabIndex = 4;
			this.numBoxAbsenceDays.Text = "";
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(168, 16);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(72, 16);
			this.label23.TabIndex = 3;
			this.label23.Text = "До дата :";
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(16, 16);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(136, 16);
			this.label22.TabIndex = 2;
			this.label22.Text = "От дата :";
			// 
			// dateTimePickerAbsenceToData
			// 
			this.dateTimePickerAbsenceToData.Location = new System.Drawing.Point(168, 32);
			this.dateTimePickerAbsenceToData.Name = "dateTimePickerAbsenceToData";
			this.dateTimePickerAbsenceToData.Size = new System.Drawing.Size(136, 20);
			this.dateTimePickerAbsenceToData.TabIndex = 1;
			this.dateTimePickerAbsenceToData.Value = new System.DateTime(2005, 1, 12, 9, 43, 38, 484);
			// 
			// dateTimePickerAbsenceFromData
			// 
			this.dateTimePickerAbsenceFromData.Location = new System.Drawing.Point(16, 32);
			this.dateTimePickerAbsenceFromData.Name = "dateTimePickerAbsenceFromData";
			this.dateTimePickerAbsenceFromData.Size = new System.Drawing.Size(136, 20);
			this.dateTimePickerAbsenceFromData.TabIndex = 0;
			this.dateTimePickerAbsenceFromData.Value = new System.DateTime(2005, 1, 12, 9, 43, 38, 500);
			// 
			// tabPagePenalty
			// 
			this.tabPagePenalty.Controls.Add(this.buttonPenaltyPrint);
			this.tabPagePenalty.Controls.Add(this.buttonPenaltyCancel);
			this.tabPagePenalty.Controls.Add(this.buttonPenaltyDelete);
			this.tabPagePenalty.Controls.Add(this.buttonPenaltySave);
			this.tabPagePenalty.Controls.Add(this.buttonPebaltyEdit);
			this.tabPagePenalty.Controls.Add(this.buttonPenaltyAdd);
			this.tabPagePenalty.Controls.Add(this.groupBoxPenaltyGrid);
			this.tabPagePenalty.Controls.Add(this.groupBoxPenalty);
			this.tabPagePenalty.Location = new System.Drawing.Point(4, 22);
			this.tabPagePenalty.Name = "tabPagePenalty";
			this.tabPagePenalty.Size = new System.Drawing.Size(760, 462);
			this.tabPagePenalty.TabIndex = 4;
			this.tabPagePenalty.Text = "Наказания";
			// 
			// buttonPenaltyPrint
			// 
			this.buttonPenaltyPrint.Location = new System.Drawing.Point(40, 424);
			this.buttonPenaltyPrint.Name = "buttonPenaltyPrint";
			this.buttonPenaltyPrint.TabIndex = 7;
			this.buttonPenaltyPrint.Text = "Печат";
			// 
			// buttonPenaltyCancel
			// 
			this.buttonPenaltyCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonPenaltyCancel.Image")));
			this.buttonPenaltyCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonPenaltyCancel.Location = new System.Drawing.Point(224, 424);
			this.buttonPenaltyCancel.Name = "buttonPenaltyCancel";
			this.buttonPenaltyCancel.TabIndex = 6;
			this.buttonPenaltyCancel.Text = " Отказ";
			this.buttonPenaltyCancel.Click += new System.EventHandler(this.buttonPenaltyCancel_Click);
			// 
			// buttonPenaltyDelete
			// 
			this.buttonPenaltyDelete.Image = ((System.Drawing.Image)(resources.GetObject("buttonPenaltyDelete.Image")));
			this.buttonPenaltyDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonPenaltyDelete.Location = new System.Drawing.Point(320, 424);
			this.buttonPenaltyDelete.Name = "buttonPenaltyDelete";
			this.buttonPenaltyDelete.Size = new System.Drawing.Size(88, 23);
			this.buttonPenaltyDelete.TabIndex = 5;
			this.buttonPenaltyDelete.Text = "   Премахва";
			this.buttonPenaltyDelete.Click += new System.EventHandler(this.buttonPenaltyDelete_Click);
			// 
			// buttonPenaltySave
			// 
			this.buttonPenaltySave.Image = ((System.Drawing.Image)(resources.GetObject("buttonPenaltySave.Image")));
			this.buttonPenaltySave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonPenaltySave.Location = new System.Drawing.Point(128, 424);
			this.buttonPenaltySave.Name = "buttonPenaltySave";
			this.buttonPenaltySave.TabIndex = 4;
			this.buttonPenaltySave.Text = " Запис";
			this.buttonPenaltySave.Click += new System.EventHandler(this.buttonPenaltySave_Click);
			// 
			// buttonPebaltyEdit
			// 
			this.buttonPebaltyEdit.Image = ((System.Drawing.Image)(resources.GetObject("buttonPebaltyEdit.Image")));
			this.buttonPebaltyEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonPebaltyEdit.Location = new System.Drawing.Point(544, 424);
			this.buttonPebaltyEdit.Name = "buttonPebaltyEdit";
			this.buttonPebaltyEdit.Size = new System.Drawing.Size(88, 23);
			this.buttonPebaltyEdit.TabIndex = 3;
			this.buttonPebaltyEdit.Text = "     Корекция";
			this.buttonPebaltyEdit.Click += new System.EventHandler(this.buttonPebaltyEdit_Click);
			// 
			// buttonPenaltyAdd
			// 
			this.buttonPenaltyAdd.Location = new System.Drawing.Point(424, 424);
			this.buttonPenaltyAdd.Name = "buttonPenaltyAdd";
			this.buttonPenaltyAdd.Size = new System.Drawing.Size(104, 23);
			this.buttonPenaltyAdd.TabIndex = 2;
			this.buttonPenaltyAdd.Text = "Ново наказание";
			this.buttonPenaltyAdd.Click += new System.EventHandler(this.buttonPenaltyAdd_Click);
			// 
			// groupBoxPenaltyGrid
			// 
			this.groupBoxPenaltyGrid.Controls.Add(this.dataGridPenalty);
			this.groupBoxPenaltyGrid.Location = new System.Drawing.Point(8, 160);
			this.groupBoxPenaltyGrid.Name = "groupBoxPenaltyGrid";
			this.groupBoxPenaltyGrid.Size = new System.Drawing.Size(744, 256);
			this.groupBoxPenaltyGrid.TabIndex = 1;
			this.groupBoxPenaltyGrid.TabStop = false;
			this.groupBoxPenaltyGrid.Text = "Данни за  наложени наказания за служителя";
			// 
			// dataGridPenalty
			// 
			this.dataGridPenalty.DataMember = "";
			this.dataGridPenalty.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridPenalty.Location = new System.Drawing.Point(8, 16);
			this.dataGridPenalty.Name = "dataGridPenalty";
			this.dataGridPenalty.ReadOnly = true;
			this.dataGridPenalty.Size = new System.Drawing.Size(728, 232);
			this.dataGridPenalty.TabIndex = 17;
			this.dataGridPenalty.Click += new System.EventHandler(this.dataGridPenalty_Click);
			// 
			// groupBoxPenalty
			// 
			this.groupBoxPenalty.Controls.Add(this.label31);
			this.groupBoxPenalty.Controls.Add(this.comboBoxTypePenalty);
			this.groupBoxPenalty.Controls.Add(this.label30);
			this.groupBoxPenalty.Controls.Add(this.dateTimePickerPenaltyTo);
			this.groupBoxPenalty.Controls.Add(this.comboBoxPenaltyReason);
			this.groupBoxPenalty.Controls.Add(this.dateTimePenaltyFormDate);
			this.groupBoxPenalty.Controls.Add(this.numBoxPenaltyOrder);
			this.groupBoxPenalty.Controls.Add(this.label21);
			this.groupBoxPenalty.Controls.Add(this.label20);
			this.groupBoxPenalty.Controls.Add(this.labelPenaltyReason);
			this.groupBoxPenalty.Controls.Add(this.labelPenalty);
			this.groupBoxPenalty.Controls.Add(this.dateTimePickerPenaltyDate);
			this.groupBoxPenalty.Location = new System.Drawing.Point(8, 8);
			this.groupBoxPenalty.Name = "groupBoxPenalty";
			this.groupBoxPenalty.Size = new System.Drawing.Size(744, 136);
			this.groupBoxPenalty.TabIndex = 0;
			this.groupBoxPenalty.TabStop = false;
			this.groupBoxPenalty.Text = "Данни за наказание";
			// 
			// label31
			// 
			this.label31.Location = new System.Drawing.Point(8, 80);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(100, 16);
			this.label31.TabIndex = 28;
			this.label31.Text = "Вид наказание:";
			// 
			// comboBoxTypePenalty
			// 
			this.comboBoxTypePenalty.Location = new System.Drawing.Point(8, 96);
			this.comboBoxTypePenalty.Name = "comboBoxTypePenalty";
			this.comboBoxTypePenalty.Size = new System.Drawing.Size(424, 21);
			this.comboBoxTypePenalty.TabIndex = 27;
			// 
			// label30
			// 
			this.label30.Location = new System.Drawing.Point(584, 16);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(100, 16);
			this.label30.TabIndex = 26;
			this.label30.Text = "Валидно до:";
			// 
			// dateTimePickerPenaltyTo
			// 
			this.dateTimePickerPenaltyTo.Location = new System.Drawing.Point(584, 32);
			this.dateTimePickerPenaltyTo.Name = "dateTimePickerPenaltyTo";
			this.dateTimePickerPenaltyTo.Size = new System.Drawing.Size(136, 20);
			this.dateTimePickerPenaltyTo.TabIndex = 25;
			// 
			// comboBoxPenaltyReason
			// 
			this.comboBoxPenaltyReason.Location = new System.Drawing.Point(8, 32);
			this.comboBoxPenaltyReason.Name = "comboBoxPenaltyReason";
			this.comboBoxPenaltyReason.Size = new System.Drawing.Size(424, 21);
			this.comboBoxPenaltyReason.TabIndex = 24;
			// 
			// dateTimePenaltyFormDate
			// 
			this.dateTimePenaltyFormDate.Location = new System.Drawing.Point(584, 96);
			this.dateTimePenaltyFormDate.Name = "dateTimePenaltyFormDate";
			this.dateTimePenaltyFormDate.Size = new System.Drawing.Size(136, 20);
			this.dateTimePenaltyFormDate.TabIndex = 23;
			this.dateTimePenaltyFormDate.Value = new System.DateTime(2005, 1, 12, 9, 43, 38, 640);
			// 
			// numBoxPenaltyOrder
			// 
			this.numBoxPenaltyOrder.Location = new System.Drawing.Point(440, 96);
			this.numBoxPenaltyOrder.Name = "numBoxPenaltyOrder";
			this.numBoxPenaltyOrder.Size = new System.Drawing.Size(128, 20);
			this.numBoxPenaltyOrder.TabIndex = 22;
			this.numBoxPenaltyOrder.Text = "";
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(584, 80);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(128, 16);
			this.label21.TabIndex = 21;
			this.label21.Text = "От дата :";
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(440, 80);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(128, 16);
			this.label20.TabIndex = 20;
			this.label20.Text = "Номер заповед :";
			// 
			// labelPenaltyReason
			// 
			this.labelPenaltyReason.Location = new System.Drawing.Point(8, 16);
			this.labelPenaltyReason.Name = "labelPenaltyReason";
			this.labelPenaltyReason.Size = new System.Drawing.Size(128, 16);
			this.labelPenaltyReason.TabIndex = 19;
			this.labelPenaltyReason.Text = "Основание :";
			// 
			// labelPenalty
			// 
			this.labelPenalty.Location = new System.Drawing.Point(440, 16);
			this.labelPenalty.Name = "labelPenalty";
			this.labelPenalty.Size = new System.Drawing.Size(96, 16);
			this.labelPenalty.TabIndex = 17;
			this.labelPenalty.Text = "В сила от :";
			// 
			// dateTimePickerPenaltyDate
			// 
			this.dateTimePickerPenaltyDate.Location = new System.Drawing.Point(440, 32);
			this.dateTimePickerPenaltyDate.Name = "dateTimePickerPenaltyDate";
			this.dateTimePickerPenaltyDate.Size = new System.Drawing.Size(136, 20);
			this.dateTimePickerPenaltyDate.TabIndex = 16;
			this.dateTimePickerPenaltyDate.Value = new System.DateTime(2005, 1, 12, 9, 43, 38, 734);
			// 
			// tabPageNotes
			// 
			this.tabPageNotes.Controls.Add(this.buttonNotes);
			this.tabPageNotes.Controls.Add(this.textBoxNotes);
			this.tabPageNotes.Location = new System.Drawing.Point(4, 22);
			this.tabPageNotes.Name = "tabPageNotes";
			this.tabPageNotes.Size = new System.Drawing.Size(760, 462);
			this.tabPageNotes.TabIndex = 5;
			this.tabPageNotes.Text = "Бележки";
			// 
			// buttonNotes
			// 
			this.buttonNotes.Image = ((System.Drawing.Image)(resources.GetObject("buttonNotes.Image")));
			this.buttonNotes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonNotes.Location = new System.Drawing.Point(328, 432);
			this.buttonNotes.Name = "buttonNotes";
			this.buttonNotes.Size = new System.Drawing.Size(96, 23);
			this.buttonNotes.TabIndex = 1;
			this.buttonNotes.Text = "    Активирай";
			this.buttonNotes.Click += new System.EventHandler(this.buttonNotes_Click);
			// 
			// textBoxNotes
			// 
			this.textBoxNotes.Location = new System.Drawing.Point(8, 8);
			this.textBoxNotes.Multiline = true;
			this.textBoxNotes.Name = "textBoxNotes";
			this.textBoxNotes.ReadOnly = true;
			this.textBoxNotes.Size = new System.Drawing.Size(744, 416);
			this.textBoxNotes.TabIndex = 0;
			this.textBoxNotes.Text = "";
			// 
			// tabPageAtestacii
			// 
			this.tabPageAtestacii.Controls.Add(this.label2);
			this.tabPageAtestacii.Location = new System.Drawing.Point(4, 22);
			this.tabPageAtestacii.Name = "tabPageAtestacii";
			this.tabPageAtestacii.Size = new System.Drawing.Size(760, 462);
			this.tabPageAtestacii.TabIndex = 6;
			this.tabPageAtestacii.Text = "Aтестации";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label2.Location = new System.Drawing.Point(112, 208);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(560, 23);
			this.label2.TabIndex = 0;
			this.label2.Text = "Модул Атестации ще е наличен в следващата версия на продукта.";
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.TabPersonalInfo);
			this.tabControl1.Controls.Add(this.tabPageAssignment);
			this.tabControl1.Controls.Add(this.tabPageAbsence);
			this.tabControl1.Controls.Add(this.tabPagePenalty);
			this.tabControl1.Controls.Add(this.tabPageFired);
			this.tabControl1.Controls.Add(this.tabPageNotes);
			this.tabControl1.Controls.Add(this.tabPageAtestacii);
			this.tabControl1.ItemSize = new System.Drawing.Size(100, 18);
			this.tabControl1.Location = new System.Drawing.Point(8, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(768, 488);
			this.tabControl1.TabIndex = 47;
			this.tabControl1.SelectedIndexChanging += new NewTabControl.NTabControl.SelectedTabPageChangeEventHandler(this.tabControl1_SelectedIndexChanging);
			// 
			// tabPageFired
			// 
			this.tabPageFired.Controls.Add(this.buttonFire);
			this.tabPageFired.Controls.Add(this.label48);
			this.tabPageFired.Controls.Add(this.numBoxFiredUnusedHoliday);
			this.tabPageFired.Controls.Add(this.buttonFiredPrint);
			this.tabPageFired.Controls.Add(this.buttonFiredCancel);
			this.tabPageFired.Controls.Add(this.buttonFiredDelete);
			this.tabPageFired.Controls.Add(this.buttonFiredSave);
			this.tabPageFired.Controls.Add(this.buttonFiredEdit);
			this.tabPageFired.Controls.Add(this.buttonFiredNew);
			this.tabPageFired.Controls.Add(this.groupBoxFired);
			this.tabPageFired.Controls.Add(this.label47);
			this.tabPageFired.Controls.Add(this.comboBoxFiredNumberSalary);
			this.tabPageFired.Controls.Add(this.label44);
			this.tabPageFired.Controls.Add(this.comboBoxFiredCompensationMistimed);
			this.tabPageFired.Controls.Add(this.label35);
			this.tabPageFired.Controls.Add(this.comboBoxFiredComponsationWork);
			this.tabPageFired.Controls.Add(this.dateTimePickerFiredFromDate);
			this.tabPageFired.Controls.Add(this.label34);
			this.tabPageFired.Controls.Add(this.label33);
			this.tabPageFired.Controls.Add(this.textBoxFiredCompensation);
			this.tabPageFired.Controls.Add(this.label32);
			this.tabPageFired.Controls.Add(this.comboBoxFiredReason);
			this.tabPageFired.Location = new System.Drawing.Point(4, 22);
			this.tabPageFired.Name = "tabPageFired";
			this.tabPageFired.Size = new System.Drawing.Size(760, 462);
			this.tabPageFired.TabIndex = 7;
			this.tabPageFired.Text = "Прекратени договори";
			// 
			// buttonFire
			// 
			this.buttonFire.Location = new System.Drawing.Point(40, 408);
			this.buttonFire.Name = "buttonFire";
			this.buttonFire.TabIndex = 80;
			this.buttonFire.Text = "Прекрати";
			this.buttonFire.Click += new System.EventHandler(this.buttonFire_Click);
			// 
			// label48
			// 
			this.label48.Location = new System.Drawing.Point(216, 96);
			this.label48.Name = "label48";
			this.label48.Size = new System.Drawing.Size(128, 16);
			this.label48.TabIndex = 79;
			this.label48.Text = "Не използван отпуск";
			// 
			// numBoxFiredUnusedHoliday
			// 
			this.numBoxFiredUnusedHoliday.Location = new System.Drawing.Point(216, 112);
			this.numBoxFiredUnusedHoliday.Name = "numBoxFiredUnusedHoliday";
			this.numBoxFiredUnusedHoliday.Size = new System.Drawing.Size(152, 20);
			this.numBoxFiredUnusedHoliday.TabIndex = 78;
			this.numBoxFiredUnusedHoliday.Text = "";
			// 
			// buttonFiredPrint
			// 
			this.buttonFiredPrint.Location = new System.Drawing.Point(136, 408);
			this.buttonFiredPrint.Name = "buttonFiredPrint";
			this.buttonFiredPrint.TabIndex = 77;
			this.buttonFiredPrint.Text = "Печат";
			// 
			// buttonFiredCancel
			// 
			this.buttonFiredCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonFiredCancel.Image")));
			this.buttonFiredCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonFiredCancel.Location = new System.Drawing.Point(320, 408);
			this.buttonFiredCancel.Name = "buttonFiredCancel";
			this.buttonFiredCancel.TabIndex = 76;
			this.buttonFiredCancel.Text = " Отказ";
			this.buttonFiredCancel.Click += new System.EventHandler(this.buttonFiredCancel_Click);
			// 
			// buttonFiredDelete
			// 
			this.buttonFiredDelete.Image = ((System.Drawing.Image)(resources.GetObject("buttonFiredDelete.Image")));
			this.buttonFiredDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonFiredDelete.Location = new System.Drawing.Point(416, 408);
			this.buttonFiredDelete.Name = "buttonFiredDelete";
			this.buttonFiredDelete.Size = new System.Drawing.Size(88, 23);
			this.buttonFiredDelete.TabIndex = 75;
			this.buttonFiredDelete.Text = "   Премахва";
			this.buttonFiredDelete.Click += new System.EventHandler(this.buttonFiredDelete_Click);
			// 
			// buttonFiredSave
			// 
			this.buttonFiredSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonFiredSave.Image")));
			this.buttonFiredSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonFiredSave.Location = new System.Drawing.Point(224, 408);
			this.buttonFiredSave.Name = "buttonFiredSave";
			this.buttonFiredSave.TabIndex = 74;
			this.buttonFiredSave.Text = " Запис";
			this.buttonFiredSave.Click += new System.EventHandler(this.buttonFiredSave_Click);
			// 
			// buttonFiredEdit
			// 
			this.buttonFiredEdit.Image = ((System.Drawing.Image)(resources.GetObject("buttonFiredEdit.Image")));
			this.buttonFiredEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonFiredEdit.Location = new System.Drawing.Point(640, 408);
			this.buttonFiredEdit.Name = "buttonFiredEdit";
			this.buttonFiredEdit.Size = new System.Drawing.Size(88, 23);
			this.buttonFiredEdit.TabIndex = 73;
			this.buttonFiredEdit.Text = "     Корекция";
			this.buttonFiredEdit.Click += new System.EventHandler(this.buttonFiredEdit_Click);
			// 
			// buttonFiredNew
			// 
			this.buttonFiredNew.Location = new System.Drawing.Point(520, 408);
			this.buttonFiredNew.Name = "buttonFiredNew";
			this.buttonFiredNew.Size = new System.Drawing.Size(104, 23);
			this.buttonFiredNew.TabIndex = 72;
			this.buttonFiredNew.Text = "Ново наказание";
			this.buttonFiredNew.Click += new System.EventHandler(this.buttonFiredNew_Click);
			// 
			// groupBoxFired
			// 
			this.groupBoxFired.Controls.Add(this.dataGridFired);
			this.groupBoxFired.Location = new System.Drawing.Point(16, 144);
			this.groupBoxFired.Name = "groupBoxFired";
			this.groupBoxFired.Size = new System.Drawing.Size(736, 248);
			this.groupBoxFired.TabIndex = 71;
			this.groupBoxFired.TabStop = false;
			this.groupBoxFired.Text = "Прекратени договори";
			// 
			// dataGridFired
			// 
			this.dataGridFired.DataMember = "";
			this.dataGridFired.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridFired.Location = new System.Drawing.Point(16, 24);
			this.dataGridFired.Name = "dataGridFired";
			this.dataGridFired.ReadOnly = true;
			this.dataGridFired.Size = new System.Drawing.Size(712, 208);
			this.dataGridFired.TabIndex = 70;
			this.dataGridFired.Click += new System.EventHandler(this.dataGridFired_Click);
			// 
			// label47
			// 
			this.label47.Location = new System.Drawing.Point(8, 96);
			this.label47.Name = "label47";
			this.label47.Size = new System.Drawing.Size(128, 16);
			this.label47.TabIndex = 69;
			this.label47.Text = "Брой взети заплати";
			// 
			// comboBoxFiredNumberSalary
			// 
			this.comboBoxFiredNumberSalary.DropDownWidth = 370;
			this.comboBoxFiredNumberSalary.Location = new System.Drawing.Point(8, 112);
			this.comboBoxFiredNumberSalary.Name = "comboBoxFiredNumberSalary";
			this.comboBoxFiredNumberSalary.Size = new System.Drawing.Size(192, 21);
			this.comboBoxFiredNumberSalary.TabIndex = 68;
			// 
			// label44
			// 
			this.label44.Location = new System.Drawing.Point(384, 56);
			this.label44.Name = "label44";
			this.label44.Size = new System.Drawing.Size(232, 16);
			this.label44.TabIndex = 67;
			this.label44.Text = "Обезщетения за не навременно известие";
			// 
			// comboBoxFiredCompensationMistimed
			// 
			this.comboBoxFiredCompensationMistimed.DropDownWidth = 370;
			this.comboBoxFiredCompensationMistimed.Location = new System.Drawing.Point(384, 72);
			this.comboBoxFiredCompensationMistimed.Name = "comboBoxFiredCompensationMistimed";
			this.comboBoxFiredCompensationMistimed.Size = new System.Drawing.Size(360, 21);
			this.comboBoxFiredCompensationMistimed.TabIndex = 66;
			// 
			// label35
			// 
			this.label35.Location = new System.Drawing.Point(8, 56);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(240, 16);
			this.label35.TabIndex = 65;
			this.label35.Text = "Обезщетения за оставане без работа";
			// 
			// comboBoxFiredComponsationWork
			// 
			this.comboBoxFiredComponsationWork.DropDownWidth = 370;
			this.comboBoxFiredComponsationWork.Location = new System.Drawing.Point(8, 72);
			this.comboBoxFiredComponsationWork.Name = "comboBoxFiredComponsationWork";
			this.comboBoxFiredComponsationWork.Size = new System.Drawing.Size(368, 21);
			this.comboBoxFiredComponsationWork.TabIndex = 64;
			// 
			// dateTimePickerFiredFromDate
			// 
			this.dateTimePickerFiredFromDate.Location = new System.Drawing.Point(600, 32);
			this.dateTimePickerFiredFromDate.Name = "dateTimePickerFiredFromDate";
			this.dateTimePickerFiredFromDate.Size = new System.Drawing.Size(144, 20);
			this.dateTimePickerFiredFromDate.TabIndex = 63;
			// 
			// label34
			// 
			this.label34.Location = new System.Drawing.Point(600, 16);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(88, 16);
			this.label34.TabIndex = 62;
			this.label34.Text = "Считано от:";
			// 
			// label33
			// 
			this.label33.Location = new System.Drawing.Point(384, 96);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(272, 16);
			this.label33.TabIndex = 61;
			this.label33.Text = "Обезщетение при прекратяване";
			// 
			// textBoxFiredCompensation
			// 
			this.textBoxFiredCompensation.Location = new System.Drawing.Point(384, 112);
			this.textBoxFiredCompensation.Name = "textBoxFiredCompensation";
			this.textBoxFiredCompensation.Size = new System.Drawing.Size(360, 20);
			this.textBoxFiredCompensation.TabIndex = 60;
			this.textBoxFiredCompensation.Text = "";
			// 
			// label32
			// 
			this.label32.Location = new System.Drawing.Point(8, 16);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(208, 16);
			this.label32.TabIndex = 59;
			this.label32.Text = "Основания за прекратяване";
			// 
			// comboBoxFiredReason
			// 
			this.comboBoxFiredReason.DropDownWidth = 370;
			this.comboBoxFiredReason.Location = new System.Drawing.Point(8, 32);
			this.comboBoxFiredReason.Name = "comboBoxFiredReason";
			this.comboBoxFiredReason.Size = new System.Drawing.Size(584, 21);
			this.comboBoxFiredReason.TabIndex = 58;
			// 
			// formPersonalData
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(792, 533);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.labelSex);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonОК);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "formPersonalData";
			this.Text = "Лично досие на служител";
			this.Load += new System.EventHandler(this.PersonalDataForm_Load);
			this.TabPersonalInfo.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.tabPageAssignment.ResumeLayout(false);
			this.groupBoxAssignmentGrid.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridAssignment)).EndInit();
			this.tabPageAbsence.ResumeLayout(false);
			this.groupBoxAbsenceGrid.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridYears)).EndInit();
			this.groupBox4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridAbsence)).EndInit();
			this.groupBoxAbsece.ResumeLayout(false);
			this.tabPagePenalty.ResumeLayout(false);
			this.groupBoxPenaltyGrid.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridPenalty)).EndInit();
			this.groupBoxPenalty.ResumeLayout(false);
			this.tabPageNotes.ResumeLayout(false);
			this.tabPageAtestacii.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPageFired.ResumeLayout(false);
			this.groupBoxFired.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridFired)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region Assignment functions

		private void buttonAssignmentEdit_Click(object sender, System.EventArgs e)		
		{
			this.ClearAssignmentBindings();
			Op = Operations.EditAssignment;
			if( this.dataGridAssignment.VisibleRowCount > 0 )
			{
				IsAssignmentEdit = true;
				if( this.dtAssignment.Rows[this.dataGridAssignment.CurrentRowIndex]["positionID"].ToString() != "" )
				{				
					this.oldPositionID = int.Parse( this.dtAssignment.Rows[this.dataGridAssignment.CurrentRowIndex]["positionID"].ToString());
				}				
				this.EnableButtons( false, false, true, false, false, true, LockButtons.Assignment);
				this.ControlEnabled( true, LockButtons.Assignment );
				this.comboBoxContract_SelectedIndexChanged(sender,e);
			}			
		}
		private void buttonAssignmentDelete_Click(object sender, System.EventArgs e)
		{
			if( this.dataGridAssignment.VisibleRowCount >= 1 )
			{
				if(this.radioButtonAssignment.Checked && this.dtAssignment.Rows.Count > 1)
				{
					MessageBox.Show("Не можете да изтриете назначението. По този договор вече има сключено допълнително споразумение.");
					return;
				}
				if( MessageBox.Show( this, "Сигурни ли сте че искате да премахнете назначението " + this.dataGridAssignment[ this.dataGridAssignment.CurrentRowIndex, 2 ].ToString(), "Изтриване", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{					
					DataRow rowPosition = this.dtPosition.Rows.Find(this.dtAssignment.Rows[this.dataGridAssignment.CurrentRowIndex]["PositionID"]);
					if(rowPosition != null)
					{
						if(int.Parse(this.dtAssignment.Rows[this.dataGridAssignment.CurrentRowIndex]["IsActive"].ToString()) == 1 && rowPosition["TypePosition"].ToString() == "Постоянна")
						{
							this.assignmentAction.UpdateStaff( int.Parse( rowPosition["ID"].ToString() ), false, 1);
							this.personAction.UpdatePersonPosition(this.parent, 0);						
						}
						else if(int.Parse( this.dtAssignment.Rows[this.dataGridAssignment.CurrentRowIndex]["IsActive"].ToString()) == 1 && rowPosition["TypePosition"].ToString() == "Сезонна")
						{
							this.personAction.UpdatePersonPosition(this.parent, 0);
						}
					}					
					
					//Ако трием основно нaзначение трябва да се зачисти и таблицата за отпуските за да не остава боклук
					if(this.radioButtonAssignment.Checked)
					{
						this.holidayAction.DeleteRow(this.parent.ToString());
					}
					this.assignmentAction.DeleteRow( this.dtAssignment.Rows[this.dataGridAssignment.CurrentRowIndex]["ID"].ToString() );
					this.dtAssignment.Rows.Remove(this.dtAssignment.Rows[this.dataGridAssignment.CurrentRowIndex]);
					this.dataGridAssignment.Refresh();
				}
			}
		}

		private bool checkAssignment()
		{
			DataRow rowPosition;
			if(this.positionID == this.oldPositionID)
				return true;
			else
			{
				rowPosition = this.dtPosition.Rows.Find(this.positionID);
				if(this.positionID == 0)
				{
					this.assignmentAction.UpdateStaff(this.oldPositionID, false, 1);
					return true;
				}
				if(rowPosition["TypePosition"].ToString() == "Сезонна")
				{
					int months;
					months = this.dateTimePickerContractExpiry.Value.Month - this.dateTimePickerAssignedAt.Value.Month;
					if(int.Parse( rowPosition["free"].ToString()) - months < 0 )
					{
						if(MessageBox.Show("За съответната длъжност няма достатъчно свободни месеци. Сигурни ли сте че изкате да сключите назначението?","Няма свободни щатни бройки",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
						{
							return false;
						}
						this.assignmentAction.UpdateStaff(this.positionID, true, 1);
						this.assignmentAction.UpdateStaff(this.oldPositionID, false, 1);
						return true;
					}
					else 
					{
						return true;
					}
				}
				else
				{
					if(int.Parse( rowPosition["free"].ToString()) - 1 < 0 )
					{
						if(MessageBox.Show("За съответната длъжност няма свободна щатна бройка. Сигурни ли сте че изкате да сключите назначението?","Няма свободни щатни бройки",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
						{						
							return false;
						}						
					}
					this.assignmentAction.UpdateStaff(this.positionID, true, 1);
					this.assignmentAction.UpdateStaff(this.oldPositionID, false, 1);
					return true;
				}
			}
		}

		private void buttonAssignmentSave_Click(object sender, System.EventArgs e)
		{
			DataLayer.AssignmentPackage package = new DataLayer.AssignmentPackage();
			
			this.ValidateAssignment( package );
			bool IsValid = this.checkAssignment();
			if(IsValid == false)
			{
				if(Op == Operations.AddAssignment && IsAssignmentEdit)
				{
					SetAssignmentBindings();
				}
				Op = Operations.ViewPersonData;
				this.ControlEnabled( false, LockButtons.Assignment );
				this.EnableButtons( true, true, false, true, true, false, LockButtons.Assignment );
				IsAssignmentEdit = false;
				return;
			}
			bool IsUnvalid = false;			

			#region AddNew
			if( !IsAssignmentEdit )
			{
				// Towa e pri dobawqne na now red
				if( this.IsAssignment )
				{
					if( this.dataGridAssignment.VisibleRowCount > 0 )
					{
						MessageBox.Show( "Не може да има повече от едно назначение" );
						IsUnvalid = true;
					}
				}				
				if( !IsUnvalid )
				{
					int OldPreviousID = 0; //проверка за старо назначение и деактивирането му
					foreach(DataRow row in this.dtAssignment.Rows)
					{
						if(row["IsActive"].ToString() == "1")
						{
							OldPreviousID = int.Parse(row["id"].ToString());
						
							package.ParentContractID = row["ParentContractID"].ToString();
							package.ParentContractDate = (DateTime) row["ParentContractDate"];					
							
							package.PrevAssignemntID = OldPreviousID;
							this.assignmentAction.UpdateActivation(OldPreviousID, 0);
							DataRow row2 = this.dtAssignment.Rows.Find(OldPreviousID);
							row2["isactive"] = 0;
							break;
						}
					}

					if( OldPreviousID == 0 )
					{
						package.ParentContractID =  this.textBoxContractNumber.Text;
						package.ParentContractDate = System.DateTime.Today;
						package.PrevAssignemntID = 0;
						if(!IsAssignmentEdit)
							this.assignmentAction.UpdateActivation(OldPreviousID, 0);
					}

					package.IsActive = 1;									
					
					package.ID = this.assignmentAction.InsertAssignment( package );

					this.AddAssignmentPackageToTable( package );

					if(this.IsAssignment)
					{
						this.textBoxNotes.Text = this.textBoxNotes.Text + "\r\nНазначен на " + this.dateTimePickerAssignedAt.Text;
						CalculatePersonalExperience();
					}
					else
					{						
						this.assignmentAction.UpdateActivation(OldPreviousID, 0);
						this.textBoxNotes.Text = this.textBoxNotes.Text + "\r\nСключил допълнително споразумение на " + this.dateTimePickerAssignedAt.Text;
					}
					//Проверява дали таблицата за отпуските съществува
					int left = 0, new_total = 0, total = 0, change = 0, day = 0, rest = 0;
					new_total = int.Parse(this.numBoxNumHoliday.Text);
					if(this.dtYearHoliday.Rows.Count > 0)
					{						
						DataRow rowH = this.dtYearHoliday.Rows.Find( DateTime.Now.Year );
						int numholidays;
						try
						{
							numholidays = int.Parse(this.numBoxNumHoliday.Text );
						}
						catch(System.FormatException)
						{
							numholidays = 0;
							MessageBox.Show("Некоректно зададен брой дни отпуск","Грешка при въвеждане");
						}
						if( (int) rowH["Total"] != numholidays ) //Ако съществува проверява дали ще има някаква корекция върху отпуските
						{							
							left = (int)rowH["leftover"];
							total = (int)rowH["total"];
							if( this.radioButtonAdditional.Checked ) //При направа на доп. спораз се добава пропорциаонлано отпуска
							{
								change = new_total - total;
								day = 365 - this.dateTimePickerAssignedAt.Value.DayOfYear;
								rest = (change * day) / 365 ;
								if( 365 % change > change/2 )
								{
									rest++;
								}
								left += rest;
								if(left < 0) 
									left = 0;
								this.holidayAction.AddHoliday( this.parent, true, this.dateTimePickerAssignedAt.Value,left, new_total );	
								rowH["Total"] = new_total;
								rowH["leftover"] = left;
							}
							else // До тук може да се стигне ако имаме назначение което е било изтрито. Тогава понастоящем реда за отпуски си остава неизтрит.
							{
								left = new_total - total; //При добавяне (първо назначение) total = 0
								// Ако служителя е назанчен текущата година се добавя само частичен отпуск. 
			//Да се доразгледа в случай че назначението е било от предишна година, а назначаваме служителя сега.
								if(this.dateTimePickerAssignedAt.Value.Year == DateTime.Now.Year)
								{
									day = 365 - this.dateTimePickerAssignedAt.Value.DayOfYear;
									rest = (left * day) / 365 ;
									if( 365 % left > left/2 )
									{
										rest++;
									}
									left = rest;
									if(left < 0) 
										left = 0;
								}
								this.holidayAction.AddHoliday( this.parent, false, this.dateTimePickerAssignedAt.Value, left, new_total );	
								rowH["Total"] = new_total;
								rowH["leftover"] = left;
							}
						}
					}
					else //При назначаване се добавя пропорциаонално отпуска ако сме назначени текущата година или се назначава цялата отпуска при старо назначение
					{
						left = new_total - total; //При добавяне (първо назначение) total = 0
						if(this.dateTimePickerAssignedAt.Value.Year == DateTime.Now.Year) // Ако служителя е назанчен текущата година се добавя само частичен отпуск
						{							
							day = 365 - this.dateTimePickerAssignedAt.Value.DayOfYear;
							rest = (left * day) / 365 ;
							if( 365 % left > left/2 )
							{
								rest++;
							}
							left = rest;
							if(left < 0) 
								left = 0;
						}
						this.holidayAction.AddHoliday( this.parent, false, this.dateTimePickerAssignedAt.Value, left, new_total );	
						DataRow rowH = this.dtYearHoliday.NewRow();// Добавя се нов ред в таблицата за отпуските
						rowH["parent"] = this.parent;
						rowH["leftover"] = left;
						rowH["Total"] = new_total;
						rowH["year"] = DateTime.Now.Year;
						this.dtYearHoliday.Rows.Add(rowH);
					}					
					this.personAction.UpdatePersonPosition(this.parent, this.nodeID);
					note.UpdateNotes( "Notes", this.parent, this.textBoxNotes.Text );
				}
			}
			#endregion
			#region edit
			else	// Towa e pri update
			{
				DataRow row = this.dtAssignment.Rows.Find(this.vueAssignment[this.dataGridAssignment.CurrentRowIndex]["id"]) ;
				if( row != null )
				{
					package.ID = (int)row["id"];
					package.ParentContractDate = (DateTime) row["ParentContractDate"];
					package.ParentContractID = row["ParentContractID"].ToString();
					//package.IsActive =  this.dtAssignment.Rows[this.dataGridAssignment.CurrentRowIndex]["isActive"];
					if(row["isActive"].ToString() == "1")
					{
						package.IsActive = 1;
					}

					row["AssignedAt"] = package.AssignedAt;
					row["AssignReason"] = package.AssignReason;
					row["BaseSalary"] = package.BaseSalary;
					row["ClassPercent"] = package.ClassPercent;
					row["Contract"] = package.Contract;
					row["ContractExpiry"] = package.ContractExpiry;
					row["ContractNumber"] = package.ContractNumber;
					row["IsAdditionalAssignment"] = package.IsAditionalAssignment;
					row["Level1"] = package.Level1;
					row["Level2"] = package.Level2;
					row["Level3"] = package.Level3;
					row["Level4"] = package.Level4;
					row["NKPCode"] = package.NKPCode;
					row["EKDALevel"] = package.NKPLevel;
					row["EKDACode"] = package.EKDACode;
					row["NKPLevel"] = package.EKDALevel;
					row["NumberKids"] = package.NumberKids;
					row["Position"] = package.Position;
					row["SalaryAddon"] = package.SalaryAddon;
                    row["IsActive"] = package.IsActive;
					row["ParentContractID"] = package.ParentContractID;
					row["ParentContractDate"] = package.ParentContractDate;
					row["PrevAssignmentID"] = package.PrevAssignemntID;
					row["WorkTime"] = package.WorkTime;
					row["modifiedbyuser"] = package.User;
					row["Years"] = package.Years;
					row["Months"] = package.Months;
					row["Days"] = package.Days;
					row["positionID"] = package.PositionID;
					row["Rang"] = package.Rang;
					row["NumHoliday"] = package.NumHoliday;
					row["TestContractDate"] = package.TestContractDate;
                    
					this.assignmentAction.UpdateAssignment( package );
					
					DataRow rowH;
					rowH = this.dtYearHoliday.Rows.Find( DateTime.Now.Year );
					int numholidays;
					if(this.numBoxNumHoliday.Text == "")
					{
						numholidays = 0;
					}
					else
					{
						try
						{
							numholidays = int.Parse( this.numBoxNumHoliday.Text);
						}
						catch(System.FormatException)
						{
							numholidays = 0;
							MessageBox.Show("Некоректно зададен брой дни отпуск","Грешка при въвеждане");
						}
					}
					if( (int) rowH["Total"] != numholidays && row["IsActive"].ToString() == "1" )
					{
						int left, total, new_total;					
						left = (int)rowH["leftover"];
						total = (int)rowH["total"];
						new_total = numholidays;
						#region comment
//						if(this.radioButtonAssignment.Checked) //Ako коригираме основното назначение и то е от текущата година се смята пропорционален отпуск
//						{
//							if(this.dateTimePickerAssignedAt.Value.Year == DateTime.Now.Year) 
//							{
//								day = 365 - this.dateTimePickerAssignedAt.Value.DayOfYear;
//								rest = (left * day) / 365 ;
//								if( 365 % left > left/2 )
//								{
//									rest++;
//								}
//								left = rest;
//								if(left < 0) 
//									left = 0;
//							}
//							else // Ako e от минала гадина се смята само с колко дни трябва да се промени и се оразява за цялата година
//							{								
//								if (left > new_total)
//									left = new_total;
//								else
//								{
//									left = new_total - total + left;
//								}
//								if( left < 0 ) 
//									left = 0;
//							}
//						}
//						else  //Ako коригираме допълнително споразумение
//						{
//							DateTime FirstAssigned = (DateTime) this.dtAssignment.Rows[0]["AssignedAt"];
//							if(FirstAssigned.Year == DateTime.Now.Year) //Проверяваме дали първото назначение на служителя не е от текущата госина. Ако е така добавяме само пропорционално увеличения/намален отпуск 
//							{
//								day = 365 - FirstAssigned.Day;
//								rest = (left * day) / 365 ;
//								if( 365 % left > left/2 )
//								{
//									rest++;
//								}
//								left = rest;
//								if(left < 0) 
//									left = 0;
//							}
//							else // Ako e от минала гадина се смята само с колко дни трябва да се промени и се отразява за цялата година
//							{								
//								if (left > new_total)
//									left = new_total;
//								else
//								{
//									left = new_total - total + left;
//								}
//								if( left < 0 ) 
//									left = 0;
//							}
//						}
						#endregion
						this.holidayAction.AddHoliday( this.parent, true, this.dateTimePickerAssignedAt.Value, left, new_total );
						rowH["Total"] = new_total;
						//rowH["leftover"] = left;
					}					
					this.personAction.UpdatePersonPosition(this.parent, this.nodeID);
				}
				if(package.IsAditionalAssignment == false)
				{
					CalculatePersonalExperience();
				}
			}
			#endregion
			
			this.Refresh();
			if(Op == Operations.AddAssignment || Op == Operations.EditAssignment)
			{
				SetAssignmentBindings();
			}
			Op = Operations.ViewPersonData;
			this.ControlEnabled( false, LockButtons.Assignment );
			this.EnableButtons( true, true, false, true, true, false, LockButtons.Assignment );
			IsAssignmentEdit = false;
		}
		
		private void buttonAssignmentCancel_Click(object sender, System.EventArgs e)
		{
			if( Op == Operations.AddAssignment)  // Ако поерацията е била по добавяне зачиства боклука
			{
				this.textBoxContractNumber.Text = "";				
				this.numBoxBaseSalary.Text = "";
				this.textBoxSalaryAddon.Text = "";
				this.textBoxClassPercent.Text = "";				
				this.dateTimePickerAssignedAt.Text = "";
				this.dateTimePickerContractExpiry.Text = "";
				SetAssignmentBindings();
			}
			else if (Op == Operations.EditAssignment)
			{
				SetAssignmentBindings();
			}
			Op = Operations.ViewPersonData;
			this.ControlEnabled( false, LockButtons.Assignment );
			this.EnableButtons( true, true, false, true, true, false, LockButtons.Assignment );		
		}

		private void radioButtonAdditional_CheckedChanged(object sender, System.EventArgs e)
		{
			string cond;
			this.ClearAssignmentBindings();
			if( this.radioButtonAdditional.Checked )
			{
				this.buttonAssignment.Text = "Доп. споразумение";
				this.IsAssignment = false;
				this.tabPageAssignment.Text = "Допълнителни Споразумения";
//				this.RefreshAssignmentDataSource( false );
				cond = "isadditionalassignment = " + (1).ToString(); //За допълнително споразумение

				vueAssignment = new DataView(dtAssignment, cond, "id", dvrs);				
			}
			else
			{
				this.buttonAssignment.Text = "Назначаване";
				this.IsAssignment = true;
				this.tabPageAssignment.Text = "Назначаване";
				//this.RefreshAssignmentDataSource( false );
				cond = "isadditionalassignment = " + (0).ToString(); //За назначение
				vueAssignment = new DataView(dtAssignment, cond, "id", dvrs);		
			}
			this.dataGridAssignment.DataSource = this.vueAssignment;
			this.SetAssignmentBindings();
		}

		private void buttonAssignment_Click(object sender, System.EventArgs e)
		{
			if(this.radioButtonAssignment.Checked && this.dtAssignment.Rows.Count >= 1)
			{
				MessageBox.Show("Не може да има второ назначение", "Грешка при назначаване");
				return;
			}
			else if(this.radioButtonAdditional.Checked && this.dtAssignment.Rows.Count < 1)
			{
				MessageBox.Show("Не може да се сключи допълнително споразумение без да има сключено назначение", "Грешка при назначаване");
				return;
			}
			if(this.radioButtonAdditional.Checked)
			{
				this.numBoxAssignmentExpD.Enabled = false;
				this.numBoxAssignmentExpY.Enabled = false;
				this.numBoxAssignmentExtM.Enabled = false;
				this.CalcExperience();
			}
			Op = Operations.AddAssignment;
			ClearAssignmentBindings();
			this.IsAssignmentEdit = false;
			this.EnableButtons( false, false, true, false, false, true, LockButtons.Assignment );
			this.ControlEnabled( true, LockButtons.Assignment);
			this.comboBoxContract_SelectedIndexChanged(sender, e);

			//		    DataLayer.AssignmentPackage assPackage = new DataLayer.AssignmentPackage();
			//			DataLayer.AssignmentAction assAction = new DataLayer.AssignmentAction( "personAssignment", this.mainform.connString );
			//			this.ValidateAssignment( assPackage );
			//			assAction.MakeAssignment( assPackage );
		}

		private void SetAssignmentBindings()
		{
			this.comboBoxLevel1.DataBindings.Add( "Tag", this.vueAssignment, "level1" );			
			this.comboBoxLevel2.DataBindings.Add( "Tag", this.vueAssignment, "level2" );
			this.comboBoxLevel3.DataBindings.Add( "Tag", this.vueAssignment, "level3" );
			this.comboBoxLevel4.DataBindings.Add( "Tag", this.vueAssignment, "level4" );
			this.comboBoxPosition.DataBindings.Add( "Tag", this.vueAssignment, "position" );
			this.comboBoxContract.DataBindings.Add( "Tag", this.vueAssignment, "contract" );
			this.comboBoxWorkTime.DataBindings.Add( "Tag", this.vueAssignment, "WorkTime" );
			this.comboBoxAssignReason.DataBindings.Add( "Tag", this.vueAssignment, "AssignReason" );
			this.comboBoxLaw.DataBindings.Add( "Tag", this.vueAssignment, "law");
			this.comboBoxYearlyAddon.DataBindings.Add( "Tag", this.vueAssignment, "YearlyAddon");
			//			this.comboBoxStaff.DataBindings.Add( "Tag", this.dtAssignment, "staff" );

			this.dateTimePickerAssignedAt.DataBindings.Add( "Text", this.vueAssignment, "assignedat" );
			this.dateTimePickerContractExpiry.DataBindings.Add( "Value", this.vueAssignment, "contractexpiry" );
			this.dateTimePickerTestPeriod.DataBindings.Add( "Value", this.vueAssignment, "TestContractDate");
			this.dateTimePickerContractDate.DataBindings.Add("Value", this.vueAssignment, "ParentContractDate");

			this.textBoxContractNumber.DataBindings.Add( "Text", this.vueAssignment, "contractNumber" );
			this.textBoxSalaryAddon.DataBindings.Add( "Text", this.vueAssignment, "SalaryAddon" );
			this.textBoxClassPercent.DataBindings.Add( "Text", this.vueAssignment, "ClassPercent" );

			this.numBoxMonthlyAddon.DataBindings.Add("Text", this.vueAssignment, "MonthlyAddon");
			this.numBoxBaseSalary.DataBindings.Add( "Text", this.vueAssignment, "BaseSalary" );
			this.numBoxNumHoliday.DataBindings.Add("Text", this.vueAssignment, "NumHoliday");
			this.numBoxAssignmentExtM.DataBindings.Add("Text", this.vueAssignment, "months");
			this.numBoxAssignmentExpD.DataBindings.Add("Text", this.vueAssignment, "days");
			this.numBoxAssignmentExpY.DataBindings.Add("Text", this.vueAssignment, "years");
		}
        
		private void ClearAssignmentBindings()
		{
			this.comboBoxLevel1.DataBindings.Clear();
			this.comboBoxLevel1.Tag = "";
			this.comboBoxLevel2.DataBindings.Clear();
            this.comboBoxLevel2.Tag = "";
			this.comboBoxLevel3.DataBindings.Clear();
			this.comboBoxLevel3.Tag = "";
			this.comboBoxLevel4.DataBindings.Clear();
			this.comboBoxLevel4.Tag = "";
			this.comboBoxPosition.DataBindings.Clear();
			this.comboBoxContract.DataBindings.Clear();
			this.comboBoxWorkTime.DataBindings.Clear();
			this.comboBoxAssignReason.DataBindings.Clear();
			this.comboBoxLaw.DataBindings.Clear();
			this.comboBoxYearlyAddon.DataBindings.Clear();
//			this.comboBoxStaff.DataBindings.Clear();
			this.textBoxContractNumber.DataBindings.Clear();
			this.textBoxSalaryAddon.DataBindings.Clear();
			this.textBoxClassPercent.DataBindings.Clear();

			this.dateTimePickerContractExpiry.DataBindings.Clear();
			this.dateTimePickerAssignedAt.DataBindings.Clear();
			this.dateTimePickerTestPeriod.DataBindings.Clear();
			this.dateTimePickerContractDate.DataBindings.Clear();

			this.numBoxBaseSalary.DataBindings.Clear();
			this.numBoxMonthlyAddon.DataBindings.Clear();
			this.numBoxNumHoliday.DataBindings.Clear();
			this.numBoxAssignmentExpD.DataBindings.Clear();
			this.numBoxAssignmentExtM.DataBindings.Clear();
			this.numBoxAssignmentExpY.DataBindings.Clear();		
		}

		private void ValidateAssignment(DataLayer.AssignmentPackage package)
		{
			package.Parent = this.parent;

			package.PositionID = this.positionID;			

			package.User = this.User;

			if( this.radioButtonAdditional.Checked )
			{
				package.IsAditionalAssignment = true;
			}
			else
			{
				package.IsAditionalAssignment = false;
			}

			if( this.comboBoxLevel1.SelectedIndex == -1 )
			{
				package.Level1 = " ";
			}
			else
			{
				package.Level1 = this.comboBoxLevel1.SelectedItem.ToString();
			}

			if( this.comboBoxLevel2.SelectedIndex == -1 )
			{
				package.Level2 = " ";
			}
			else
			{
				package.Level2 = this.comboBoxLevel2.SelectedItem.ToString();
			}

			if( this.comboBoxLevel3.SelectedIndex == -1 )
			{
				package.Level3 = " ";
			}
			else
			{
				package.Level3 = this.comboBoxLevel3.SelectedItem.ToString();
			}
	
			if( this.comboBoxLevel4.SelectedIndex == -1 )
			{
				package.Level4 = " ";
			}
			else
			{
				package.Level4 = this.comboBoxLevel4.SelectedItem.ToString();
			}

			if( this.comboBoxPosition.SelectedIndex == -1 )
			{
				package.Position = " ";
			}
			else
			{
				package.Position = this.comboBoxPosition.SelectedItem.ToString();
			}

			if( this.comboBoxContract.SelectedIndex == -1 )
			{
				package.Contract = " ";
			}

			else
			{
				package.Contract = this.comboBoxContract.SelectedItem.ToString();
			}

			if( this.comboBoxWorkTime.SelectedIndex == -1 )
			{
				package.WorkTime = " ";
			}
			else
			{
				package.WorkTime = this.comboBoxWorkTime.SelectedItem.ToString();
			}

			package.AssignedAt = this.dateTimePickerAssignedAt.Value;

			if( this.comboBoxAssignReason.Text == "" )
			{
				package.AssignReason = "";
			}
			else
			{
				package.AssignReason = this.comboBoxAssignReason.Text;
			}

			if(this.comboBoxPosition.SelectedIndex - 1 >= 0)
			{			
				package.EKDACode = this.vuePosition[this.comboBoxPosition.SelectedIndex - 1]["EKDACode"].ToString();
				package.EKDALevel = this.vuePosition[this.comboBoxPosition.SelectedIndex - 1]["EKDALevel"].ToString();
				package.NKPCode = this.vuePosition[this.comboBoxPosition.SelectedIndex - 1]["NKPCode"].ToString();
				package.NKPLevel = this.vuePosition[this.comboBoxPosition.SelectedIndex - 1]["NKPLevel"].ToString();
				package.Rang = this.vuePosition[this.comboBoxPosition.SelectedIndex - 1]["Rang"].ToString();
			}

//			if( this.comboBoxStaff.SelectedIndex == -1 )
//			{
//				package.Staff = " ";
//			}
//			else
//			{
//				package.Staff = this.comboBoxStaff.SelectedItem.ToString();
//			}

			package.ContractNumber = this.textBoxContractNumber.Text;
			package.ContractExpiry = dateTimePickerContractExpiry.Value;
			package.TestContractDate = dateTimePickerTestPeriod.Value;
			package.BaseSalary = this.numBoxBaseSalary.Text;
			package.SalaryAddon = this.textBoxSalaryAddon.Text;
			package.ClassPercent = this.textBoxClassPercent.Text;
			package.Portion = this.numBoxMonthlyAddon.Text;
			package.NumHoliday = this.numBoxNumHoliday.Text;

			if( this.numBoxAssignmentExpY.Text == "" )
			{
                package.Years = 0;
			}
			else
			{
				package.Years = int.Parse( this.numBoxAssignmentExpY.Text );
			}
			if( this.numBoxAssignmentExtM.Text == "" )
			{
				package.Months = 0;
			}
			else
			{
				package.Months = int.Parse( this.numBoxAssignmentExtM.Text );
			}

			if( this.numBoxAssignmentExpD.Text == "" )
			{
				package.Days = 0;
			}
			else
			{
				package.Days = int.Parse( this.numBoxAssignmentExpD.Text );
			}
		
			if(this.comboBoxLaw.SelectedIndex == -1)
			{
				package.Law = "";
			}
			else
			{
				package.Law = this.comboBoxLaw.Text;
			}
			if(this.comboBoxYearlyAddon.SelectedIndex == -1)
			{
				package.YearlyAddon = "";
			}
			else
			{
				package.YearlyAddon = this.comboBoxYearlyAddon.Text;
			}
		}

		private void RefreshAssignmentDataSource( bool IsFormLoad )
		{
			try
			{				
				this.dtAssignment = this.assignmentAction.SelectBasicDataFromFirmPersonal(this.parent);
			}
			catch
			{
				MessageBox.Show("Липсваща или некоректна таблица за назначенията");
			}

			this.dtAssignment.PrimaryKey = new DataColumn[]{this.dtAssignment.Columns["ID"]};
			string cond;
			if( this.radioButtonAdditional.Checked )
			{
				this.IsAssignment = false;
				this.tabPageAssignment.Text = "Допълнителни Споразумения";
				//				this.RefreshAssignmentDataSource( false );
				cond = "isadditionalassignment = " + (1).ToString(); //За допълнително споразумение

				vueAssignment = new DataView(dtAssignment, cond, "id", dvrs);				
			}
			else
			{
				this.IsAssignment = true;
				this.tabPageAssignment.Text = "Назначаване";
				//this.RefreshAssignmentDataSource( false );
				cond = "isadditionalassignment = " + (0).ToString(); //За назначение
				vueAssignment = new DataView(dtAssignment, cond, "id", dvrs);		
			}
			this.dataGridAssignment.DataSource = this.vueAssignment;
			this.dtAssignment.TableName = "personassignment";
			
			JustifyGrid(dataGridAssignment);

			if( ! this.IsAssignmentLoadForm ) 
			{
				ClearAssignmentBindings();
			}
			try
			{
				//Tuka gyrmeshe predi, prawq prowerka pyrwo dali ima neshtow dtAssignment tablicata
				if( this.dtAssignment.Rows.Count > 0 )
				{
					SetAssignmentBindings();
				}
			}
			catch
			{
				MessageBox.Show("Липсващи данни за назначение");
			}
			this.IsAssignmentLoadForm = false;
			//this.PrevAssignmentIndex = 0;
			if(this.IsAssignment && this.dtAssignment.Rows.Count > 0)
			{
				this.positionID = int.Parse( this.dtAssignment.Rows[0]["PositionID"].ToString() );
				this.oldPositionID = this.positionID;
			}
			else if(this.IsAssignment == false)
			{
				if(this.dtAssignment.Rows.Count > 0)
				{
					for(int i = 0; i < this.dtAssignment.Rows.Count; i++ )
					{
						if(this.dtAssignment.Rows[i]["IsActive"].ToString() == "1")
						{
							this.oldPositionID = this.positionID = int.Parse( this.dtAssignment.Rows[i]["PositionID"].ToString() );
							break;
						}
					}
				}
			}					
		}

		private void AddAssignmentPackageToTable( DataLayer.AssignmentPackage package )
		{
			DataRow row = this.dtAssignment.NewRow();
			row["ID"] = package.ID;
			row["parent"] = package.Parent;			
			row["AssignedAt"] = package.AssignedAt;
			row["AssignReason"] = package.AssignReason;
			row["BaseSalary"] = package.BaseSalary;
			row["ClassPercent"] = package.ClassPercent;
			row["Contract"] = package.Contract;
			row["ContractExpiry"] = package.ContractExpiry;
			row["ContractNumber"] = package.ContractNumber;
			row["IsAdditionalAssignment"] = package.IsAditionalAssignment;
			row["Level1"] = package.Level1;
			row["Level2"] = package.Level2;
			row["Level3"] = package.Level3;
			row["Level4"] = package.Level4;
			row["NKPCode"] = package.NKPCode;
			row["NKPlevel"] = package.NKPLevel;
			row["EKDACode"] = package.EKDACode;
			row["EKDAlevel"] = package.EKDALevel;
			row["NumberKids"] = package.NumberKids;
			row["Position"] = package.Position;
			row["SalaryAddon"] = package.SalaryAddon;
			row["PositionID"] = package.PositionID;
			row["IsActive"] = package.IsActive;
			row["ParentContractID"] = package.ParentContractID;
			row["ParentContractDate"] = package.ParentContractDate;
			row["PrevAssignmentID"] = package.PrevAssignemntID;
			row["WorkTime"] = package.WorkTime;
			row["modifiedbyuser"] = package.User;
            row["Years"] = package.Years;
			row["Months"] = package.Months;
			row["Days"] = package.Days;
			row["Law"] = package.Law;
			row["YearlyAddon"] = package.YearlyAddon;
			row["MonthlyAddon"] = package.Portion;
			row["Rang"] = package.Rang;
			row["NumHoliday"] = package.NumHoliday;
			row["TestContractDate"] = package.TestContractDate;

			this.dtAssignment.Rows.Add( row );
		}

		private void dataGridAssignment_Click(object sender, System.EventArgs e)
		{
			if(dataGridAssignment.CurrentRowIndex == -1)
				return;
			dataGridAssignment.Select(dataGridAssignment.CurrentRowIndex);  //селектираме реда на който е кликнато
//			if(PrevAssignmentIndex != dataGridAssignment.CurrentRowIndex)
//			{
			int index = comboBoxLevel1.FindString( comboBoxLevel1.Tag.ToString() );
			if( index > -1 )
			{
				comboBoxLevel1.SelectedIndex = index;
			}
			else
			{
				comboBoxLevel1.SelectedIndex = 0;
			}

			index = comboBoxLevel2.FindString( comboBoxLevel2.Tag.ToString() );
			if( index > -1 )
			{
				comboBoxLevel2.SelectedIndex = index;
			}
			else
			{
				comboBoxLevel2.SelectedIndex = 0;
			}			

			index = comboBoxLevel3.FindString( comboBoxLevel3.Tag.ToString() );
			if( index > -1 )
			{
				comboBoxLevel3.SelectedIndex = index;
			}
			else
			{
				comboBoxLevel3.SelectedIndex = 0;
			}

			index = comboBoxLevel4.FindString( comboBoxLevel4.Tag.ToString() );
			if( index > -1 )
			{
				comboBoxLevel4.SelectedIndex = index;
			}
			else
			{
				comboBoxLevel4.SelectedIndex = 0;
			}
			
			index = comboBoxPosition.FindString( comboBoxPosition.Tag.ToString() );
			if( index > -1 )
			{
				comboBoxPosition.SelectedIndex = index;
			}
			else
			{
				comboBoxPosition.SelectedIndex = 0;
			}
			
			index = comboBoxContract.FindString( comboBoxContract.Tag.ToString() );
			if( index > -1 )
			{
				comboBoxContract.SelectedIndex = index;
			}
			else
			{
				comboBoxContract.SelectedIndex = 0;
			}

			index = comboBoxWorkTime.FindString( comboBoxWorkTime.Tag.ToString() );
			if( index > -1 )
			{
				comboBoxWorkTime.SelectedIndex = index;
			}
			else
			{
				comboBoxWorkTime.SelectedIndex = 0;
			}

			index = comboBoxAssignReason.FindString( comboBoxAssignReason.Tag.ToString() );
			if( index > -1 )
			{
				comboBoxAssignReason.SelectedIndex = index;
			}
			else
			{
				comboBoxAssignReason.SelectedIndex = 0;
			}				

			index = comboBoxYearlyAddon.FindString( comboBoxYearlyAddon.Tag.ToString() );
			if( index > -1 )
			{
				comboBoxYearlyAddon.SelectedIndex = index;
			}
			else
			{
				comboBoxYearlyAddon.SelectedIndex = 0;
			}		
	
			index = comboBoxLaw.FindString( comboBoxLaw.Tag.ToString() );
			if( index > -1 )
			{
				comboBoxLaw.SelectedIndex = index;
			}
			else
			{
				comboBoxLaw.SelectedIndex = 0;
			}

			//CalcExperience();
			
//				index = comboBoxStaff.FindString( comboBoxStaff.Tag.ToString() );
//				if( index > -1 )
//				{
//					comboBoxStaff.SelectedIndex = index;
//				}
//				else
//				{
//					comboBoxStaff.SelectedIndex = 0;
//				}
//				PrevAssignmentIndex = dataGridAssignment.CurrentRowIndex;
//			}
		}

		private void CalcExperience()
		{
			if( this.dtAssignment.Rows.Count > 0 )
			{	//Трудов стаж					
				DateTime AssignDate = Convert.ToDateTime( this.dtAssignment.Rows[0]["AssignedAt"] );
				//int years = (int)this.dtAssignment.Rows[0]["Years"];
				if( DateTime.Compare( DateTime.Now, AssignDate ) == 1)
				{
					int AssY, AssM, AssD, CYear, CDay, CMonth, TY, TM, TD;
						
					AssY = AssignDate.Year; 
					AssM = AssignDate.Month; 
					AssD = AssignDate.Day;
					CYear = DateTime.Now.Year - AssY;
					if( (CMonth = DateTime.Now.Month - AssM) < 0)
					{							
						CYear--;
					}
					if( (CDay = DateTime.Now.Day - AssD) <= 0)
					{
						CDay += 30;
						CMonth--;
						if(CMonth < 0)
						{
							CMonth += 12;
							CYear--;
						}
					}
					TY = TM = TD = 0;
					TY = CYear + (int)this.dtAssignment.Rows[0]["Years"];
					TM = CMonth + (int)this.dtAssignment.Rows[0]["Months"];
					TD = CDay + (int)this.dtAssignment.Rows[0]["Days"];
					if(TD >= 30)
					{
						TM++;
						TD -= 30;
					}
					if(TM >= 12)
					{
						TM -=12;
						TY++;
					}
					this.numBoxAssignmentExpY.Text = TY.ToString();
					this.numBoxAssignmentExtM.Text = TM.ToString();
					this.numBoxAssignmentExpD.Text = TD.ToString();
				}
				else
				{
					this.numBoxAssignmentExpY.Text = this.dtAssignment.Rows[0]["Years"].ToString();
					this.numBoxAssignmentExtM.Text = this.dtAssignment.Rows[0]["Months"].ToString();
					this.numBoxAssignmentExpD.Text = this.dtAssignment.Rows[0]["Days"].ToString();
					MessageBox.Show( "Моля проверете дали датата на компютъра е вярна" );
				}				
			}
		}

		private void TreeLoad()
		{
			dvrs = DataViewRowState.CurrentRows;
			vueAdministration = new DataView(dtTree, "par = 0", "level", dvrs);

			this.arrDirection = new ArrayList();
			this.arrDirection.Add("");			

			for(int i = 0; i < vueAdministration.Count; i++)
			{
				arrDirection.Add(vueAdministration[i]["level"]);
			}
			this.comboBoxLevel1.DataSource = arrDirection;
//			object sender = new object();
//			System.EventArgs e = new System.EventArgs();
//			comboBoxLevel1_SelectedIndexChanged(sender, e );
		}

		private void comboBoxLevel1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string cond;
			this.comboBoxLevel2.Items.Clear();
			this.comboBoxLevel2.Text = "";
			this.comboBoxLevel2.Items.Add("");
			this.comboBoxLevel3.Items.Clear();
			this.comboBoxLevel3.Text = "";
			this.comboBoxLevel3.Items.Add("");
			this.comboBoxLevel4.Items.Clear();
			this.comboBoxLevel4.Text = "";
			this.comboBoxLevel4.Items.Add("");
			this.comboBoxPosition.Items.Clear();
			this.comboBoxPosition.Text = "";
			this.comboBoxPosition.Items.Add("");

			if(this.comboBoxLevel1.SelectedIndex > 0)
			{
				cond = "par = " + this.vueAdministration[this.comboBoxLevel1.SelectedIndex - 1]["id"].ToString();

				vueDirection = new DataView(dtTree, cond, "level", dvrs);

				for(int i = 0; i < this.vueDirection.Count; i++)
				{
					this.comboBoxLevel2.Items.Add(vueDirection[i]["level"]);
				}

				vuePosition = new DataView(dtPosition, cond, "id", dvrs);
				for(int i = 0; i < this.vuePosition.Count; i++)
				{
					this.comboBoxPosition.Items.Add(vuePosition[i]["nameOfPosition"]);
				}
				this.nodeID = (int) this.vueAdministration[this.comboBoxLevel1.SelectedIndex - 1]["id"];
			}
			else
			{
				this.nodeID = 0;
			}			
		}

		private void comboBoxLevel2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string cond;
			this.comboBoxLevel3.Items.Clear();
			this.comboBoxLevel3.Text = "";
			this.comboBoxLevel3.Items.Add("");
			this.comboBoxLevel4.Items.Clear();
			this.comboBoxLevel4.Text = "";
			this.comboBoxLevel4.Items.Add("");
			this.comboBoxPosition.Items.Clear();
			this.comboBoxPosition.Text = "";
			this.comboBoxPosition.Items.Add("");

			if(this.comboBoxLevel2.SelectedIndex > 0)
			{
				cond = "par = " + this.vueDirection[this.comboBoxLevel2.SelectedIndex - 1]["id"].ToString();

				vueDepartment = new DataView(dtTree, cond, "level", dvrs);

				for(int i = 0; i < this.vueDepartment.Count; i++)
				{
					this.comboBoxLevel3.Items.Add(vueDepartment[i]["level"]);
				}				
				this.nodeID = (int) this.vueDirection[this.comboBoxLevel2.SelectedIndex - 1]["id"];
			}
			else if (this.comboBoxLevel1.SelectedIndex > 0)
			{
				this.nodeID = (int) this.vueAdministration[this.comboBoxLevel1.SelectedIndex - 1]["id"];
			}
			else
			{
				this.nodeID = 0;
			}
			cond = "par = " + this.nodeID;
			vuePosition = new DataView(dtPosition, cond, "id", dvrs);
			for(int i = 0; i < this.vuePosition.Count; i++)
			{
				this.comboBoxPosition.Items.Add(vuePosition[i]["nameOfPosition"]);
			}
		}

		private void comboBoxLevel3_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string cond;
			this.comboBoxLevel4.Items.Clear();
			this.comboBoxLevel4.Text = "";
			this.comboBoxLevel4.Items.Add("");
			this.comboBoxPosition.Items.Clear();
			this.comboBoxPosition.Text = "";
			this.comboBoxPosition.Items.Add("");


			if(this.comboBoxLevel3.SelectedIndex > 0)
			{
				cond = "par = " + this.vueDepartment[this.comboBoxLevel3.SelectedIndex - 1]["id"].ToString();

				vueSector = new DataView(dtTree, cond, "level", dvrs);

				for(int i = 0; i < this.vueSector.Count; i++)
				{
					this.comboBoxLevel4.Items.Add(vueSector[i]["level"]);
				}				
				this.nodeID = (int) this.vueDepartment[this.comboBoxLevel3.SelectedIndex - 1]["id"];
			}	
			else if(this.comboBoxLevel2.SelectedIndex > 0)
			{
				this.nodeID = (int) this.vueDirection[this.comboBoxLevel2.SelectedIndex - 1]["id"];
			}
			else if(this.comboBoxLevel1.SelectedIndex > 0)
			{
				this.nodeID = (int) this.vueAdministration[this.comboBoxLevel1.SelectedIndex - 1]["id"];
			}
			else
			{
				this.nodeID = 0;
			}	
			cond = "par = " + this.nodeID;
			vuePosition = new DataView(dtPosition, cond, "id", dvrs);
			for(int i = 0; i < this.vuePosition.Count; i++)
			{
				this.comboBoxPosition.Items.Add(vuePosition[i]["nameOfPosition"]);
			}
		}

		private void comboBoxPosition_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.comboBoxPosition.SelectedIndex > 0)
			{
				this.positionID = int.Parse( vuePosition[this.comboBoxPosition.SelectedIndex -1]["id"].ToString());
				this.textBoxNKPLevel.Text = vuePosition[this.comboBoxPosition.SelectedIndex - 1]["nkplevel"].ToString();
				this.textBoxNKPCode.Text = vuePosition[this.comboBoxPosition.SelectedIndex - 1]["nkpcode"].ToString();
			}
			else
			{
				this.textBoxNKPCode.Text = this.textBoxNKPLevel.Text = "";
				this.positionID = 0;
			}
		}

		private void comboBoxLevel4_SelectedIndexChanged(object sender, System.EventArgs e)
		{	
			string cond;
			this.comboBoxPosition.Items.Clear();
			this.comboBoxPosition.Text = "";
			this.comboBoxPosition.Items.Add("");

			if(this.comboBoxLevel4.SelectedIndex > 0)
			{				
				this.nodeID = (int)this.vueSector[this.comboBoxLevel4.SelectedIndex - 1]["id"];
			}		
			else if(this.comboBoxLevel3.SelectedIndex > 0)
			{
				this.nodeID = (int) this.vueDepartment[this.comboBoxLevel3.SelectedIndex - 1]["id"];
			}
			else if(this.comboBoxLevel2.SelectedIndex > 0)
			{
				this.nodeID = (int) this.vueDirection[this.comboBoxLevel2.SelectedIndex - 1]["id"];
			}
			else if(this.comboBoxLevel1.SelectedIndex > 0)
			{
				this.nodeID = (int) this.vueAdministration[this.comboBoxLevel1.SelectedIndex - 1]["id"];
			}
			else
			{
				this.nodeID = 0;
			}
			cond = "par = " + this.nodeID;
			vuePosition = new DataView(dtPosition, cond, "id", dvrs);
			for(int i = 0; i < this.vuePosition.Count; i++)
			{
				this.comboBoxPosition.Items.Add(vuePosition[i]["nameOfPosition"]);
			}
		}
		private void numBoxAssignmentExpY_TextChanged(object sender, System.EventArgs e)
		{
			if(Op == Operations.AddAssignment)
			{
				if( int.Parse(this.numBoxAssignmentExpY.Text) >= 3)
				{
					this.textBoxClassPercent.Text = this.numBoxAssignmentExpY.Text;
				}
			}
		}
		private void comboBoxContract_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(Op == Operations.AddAssignment || Op == Operations.EditAssignment)
			{
				switch(this.comboBoxContract.SelectedIndex)
				{
					case 1:
					{
						this.dateTimePickerContractExpiry.Enabled = false;
						this.dateTimePickerTestPeriod.Enabled = false;						
						break;
					}
					case 2:
					{
						this.dateTimePickerContractExpiry.Enabled = false;
						this.dateTimePickerTestPeriod.Enabled = true;
						break;
					}
					case 3:
					{
						this.dateTimePickerContractExpiry.Enabled = true;
						this.dateTimePickerTestPeriod.Enabled = false;
						break;
					}
					case 4:
					{
						this.dateTimePickerContractExpiry.Enabled = true;
						this.dateTimePickerTestPeriod.Enabled = true;
						break;
					}
					default :
					{
						this.dateTimePickerContractExpiry.Enabled = false;
						this.dateTimePickerTestPeriod.Enabled = false;
						break;
					}
				}
			}
		}
		#endregion		

		#region Absence functions

		private void buttonAbsenceAdd_Click(object sender, System.EventArgs e)
		{
			Op = Operations.AddAbsence;
			this.IsAbsenceEdit = false;
			ClearAbsenceBindings();
			this.EnableButtons( false, false, true, false, false, true, LockButtons.Absence );
			//this.ClearControls( false );
			this.ControlEnabled( true, LockButtons.Absence);
		}

		private void buttonAbsenceEdit_Click(object sender, System.EventArgs e)
		{
			Op = Operations.EditAbsence;
			if( this.dataGridAbsence.VisibleRowCount > 0 )
			{
				IsAbsenceEdit = true;
				this.EnableButtons( false, false, true, false, false, true, LockButtons.Absence);
				this.ControlEnabled( true, LockButtons.Absence );
			}
		}

		private void buttonAbsenceSave_Click(object sender, System.EventArgs e)
		{
			DataLayer.AbsencePackage package = new DataLayer.AbsencePackage();
			this.ValidateAbsenceData( package );
			bool IsUnpayed = false;
			if( this.comboBoxAbsenceTypeAbsence.SelectedIndex != -1 )
			{
//				if( this.dtHoliday.Rows.Count > 0 )
//				{
					if( this.comboBoxAbsenceTypeAbsence.SelectedIndex == 0 ) 
					{
						// При платени отпуски
						DataRow rowZ;
						int otpusk = 0;
						int left = 0;
						
						int temp = int.Parse( this.dtYearHoliday.Rows[this.comboBoxForYear.SelectedIndex]["year"].ToString());
						rowZ = this.dtYearHoliday.Rows.Find( temp );
						if( rowZ != null )
						{
							left = int.Parse( rowZ[ "leftover" ].ToString());
							otpusk = left - int.Parse( this.numBoxAbsenceDays.Text );

						}
						if( otpusk < 0 )
						{
							MessageBox.Show( "Няма достатъчно отпуск за съответната година" );
							return;
						}
						else
						{
							rowZ[ "leftover" ] = otpusk;
						    this.dtYearHoliday.Rows[ this.dtYearHoliday.Rows.Count - 1]["leftover"] = otpusk;
							this.holidayAction.UpdateYear(this.parent, "year_holiday", rowZ["year"].ToString(), otpusk.ToString() ); 
							//this.dtYearHoliday.Rows[ this.dtYearHoliday.Rows.Count - 1]["year"].ToString(), this.dtYearHoliday.Rows[ this.dtYearHoliday.Rows.Count - 1]["left"].ToString());
						}
					}
					else //При неплатени отпуски
					{
						if(  this.comboBoxAbsenceTypeAbsence.SelectedIndex >= 0 )
						{
							//this.dtHoliday.Rows[ 0 ]["unpayed" ] =(int)this.dtHoliday.Rows[ 0 ][ "unpayed" ] + package.CountDays;
						}
					}
				}
//				else
//				{
//					MessageBox.Show( "Таблицата с отсъствията е повредена или нечетима!" );
//
//					Op = Operations.ViewPersonData;
//					this.ControlEnabled( false, LockButtons.Absence );
//					this.EnableButtons( true, true, false, true, false, false, LockButtons.Absence);
//					IsAbsenceEdit = false;
//					return; // Ако няма таблица с отпуските не може да се редактира
//				}
				if( !IsAbsenceEdit )
				{
					// Towa e pri dobawqne na now red
					//Ако сме искали платен отпуск но не ни е стигнало времето и го записваме като нелатен
					if( IsUnpayed )
					{
						package.TypeAbsence = "Неплатен отпуск";
					}
					this.AddAbsencePackageToTable( package );
					this.absenceAction.UpdateDataAdapter( this.dtAbsence );
					this.textBoxNotes.Text = this.textBoxNotes.Text + "\r\nОтсъствал от " + this.dateTimePickerAbsenceFromData.Text + " до " + this.dateTimePickerAbsenceToData.Text;
					note.UpdateNotes( "Notes", this.parent, this.textBoxNotes.Text );
				}
				else
				{
					// Towa e pri update					
					DataRow row = this.dtAbsence.Rows[this.dataGridAbsence.CurrentRowIndex];
					if( row != null )
					{
						package.ID = (int)dtAbsence.Rows[dataGridAbsence.CurrentRowIndex]["id"];
						row["CountDays"] = package.CountDays;
						row["FromDate"] = package.FromDate;
						row["NumberOrder"] = package.NumberOrder;
						row["OrderFromDate"] = package.OrderFromDate;
						row["Reason"] = package.Reason;
						row["ToDate"] = package.ToDate;
						row["TypeAbsence"] = package.TypeAbsence;
						row["modifiedByuser"] = package.User;

						this.absenceAction.UpdateAbsence( package );
					}				
				this.Refresh();
			}

			if(Op == Operations.AddAbsence)
			{
				SetAbsenceBindings();
			}
			Op = Operations.ViewPersonData;
			this.ControlEnabled( false, LockButtons.Absence );
			this.EnableButtons( true, true, false, true, false, false, LockButtons.Absence);
			IsAbsenceEdit = false;
		}
	
		private void buttonAbsenceDelete_Click(object sender, System.EventArgs e)
		{
			if( this.dataGridAbsence.VisibleRowCount >= 1 )
			{
				if( MessageBox.Show( this, "Сигурни ли сте че искате да изтриете отсъствието " + this.dataGridAbsence[ this.dataGridAbsence.CurrentRowIndex, 2 ].ToString(), "Изтриване", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					this.absenceAction.DeleteRow( this.dataGridAbsence[ this.dataGridAbsence.CurrentRowIndex, 0 ].ToString(), this.dataGridAbsence[ this.dataGridAbsence.CurrentRowIndex, 1 ].ToString());
					this.dtAbsence.Rows.Remove(this.dtAbsence.Rows[this.dataGridAbsence.CurrentRowIndex]);
					this.dataGridAbsence.Refresh();
					this.EnableButtons( true, true, false, true, false, false, LockButtons.Absence );
				}
			}
		}

		private void buttonAbsenceCancel_Click(object sender, System.EventArgs e)
		{
			if(Op == Operations.AddAbsence)  //Ако операцията е бил по добавяне зачиства боклука
			{
				this.textBoxAbsenceReason.Text = "";
				this.dateTimePickerAbsenceFromData.Text = "";
				this.dateTimePickerAbsenceToData.Text = "";
				this.dateTimePickerAbsenceOrderFormData.Text = "";
				SetAbsenceBindings();
			}

			Op = Operations.ViewPersonData;
			this.ControlEnabled( false, LockButtons.Absence );
			this.EnableButtons( true, true, false, true, false, false, LockButtons.Absence );
			
		}	

		private void ValidateAbsenceData( DataLayer.AbsencePackage package )
		{
			package.Parent = this.parent;
			package.User = this.User;
			if( this.IsAbsenceEdit )
			{
				package.ID = (int)dtAbsence.Rows[this.dataGridAbsence.CurrentRowIndex]["id"];
			}			

			package.FromDate = this.dateTimePickerAbsenceFromData.Value;
			package.ToDate = this.dateTimePickerAbsenceToData.Value;
			package.OrderFromDate = this.dateTimePickerAbsenceOrderFormData.Value;

			if( this.numBoxAbsenceDays.Text =="" )
			{
				package.CountDays = 0;
			}
			else
			{
				package.CountDays = int.Parse( this.numBoxAbsenceDays.Text );
			}
//////////////////////////////////////////////
//			if( this.numBoxAbsenceLastYearPlan.Text =="" )
//			{
//				package.LastYearPlan = 0;
//			}
//			else
//			{
//				package.LastYearPlan = int.Parse( this.numBoxAbsenceLastYearPlan.Text );
//			}
//
//			if( this.numBoxAbsenceLastYearUsed.Text =="" )
//			{
//				package.LastYearUsed = 0;
//			}
//			else
//			{
//				package.LastYearUsed = int.Parse( this.numBoxAbsenceLastYearUsed.Text );
//			}
//
//			
//			if( this.numBoxAbsenceLastYearRest.Text =="" )
//			{
//				package.LastYearRest = 0;
//			}
//			else
//			{
//				package.LastYearRest = int.Parse( this.numBoxAbsenceLastYearRest.Text );
//			}
//
//			if( this.numBoxAbsenceUnpayedHoliday.Text =="" )
//			{
//				package.UnpayedHoliday = 0;
//			}
//			else
//			{
//				package.UnpayedHoliday = int.Parse( this.numBoxAbsenceUnpayedHoliday.Text );
//			}
//
//			
//			if( this.numBoxAbsenceCurrentYearPlan.Text =="" )
//			{
//				package.CurrentYearPlan = 0;
//			}
//			else
//			{
//				package.CurrentYearPlan = int.Parse( this.numBoxAbsenceCurrentYearPlan.Text );
//			}
//
//			if( this.numBoxAbsenceCurrentYearUsed.Text =="" )
//			{
//				package.CurrentYearUsed = 0;
//			}
//			else
//			{
//				package.CurrentYearUsed = int.Parse( this.numBoxAbsenceCurrentYearUsed.Text );
//			}
//
//			if( this.numBoxAbsenceCurrentYearRest.Text =="" )
//			{
//				package.CurrentYearRest = 0;
//			}
//			else
//			{
//				package.CurrentYearRest = int.Parse( this.numBoxAbsenceCurrentYearRest.Text );
//			}
//////////////////////////////////////
			if( textBoxAbsenceReason.Text == "" )
			{
				package.Reason = "Непоказано";
			}
			else
			{
				package.Reason = textBoxAbsenceReason.Text;
			}

			if( textBoxAbsenceNumberOrder.Text == "" )
			{
				package.NumberOrder = "Непоказано";
			}
			else
			{
				package.NumberOrder = textBoxAbsenceNumberOrder.Text;
			}

			if( this.comboBoxAbsenceTypeAbsence.SelectedIndex == -1 )
			{
				package.TypeAbsence = " ";
			}
			else
			{
				package.TypeAbsence = this.comboBoxAbsenceTypeAbsence.SelectedItem.ToString();
			}
		}

		private void ClearAbsenceBindings()
		{
			this.dateTimePickerAbsenceFromData.DataBindings.Clear();
			this.dateTimePickerAbsenceToData.DataBindings.Clear();
			this.numBoxAbsenceDays.DataBindings.Clear();
			this.numBoxAbsenceDays.DataBindings.Clear();
			this.textBoxAbsenceReason.DataBindings.Clear();
						
			this.comboBoxAbsenceTypeAbsence.DataBindings.Clear();

			this.textBoxAbsenceReason.DataBindings.Clear();
			this.textBoxAbsenceNumberOrder.DataBindings.Clear();
			this.dateTimePickerAbsenceOrderFormData.DataBindings.Clear();
		}

		private void SetAbsenceBindings()
		{
			this.comboBoxAbsenceTypeAbsence.DataBindings.Add( "Tag", this.dtAbsence, "TypeAbsence" );

			this.numBoxAbsenceDays.DataBindings.Add("Text", this.dtAbsence, "CountDays");

			this.textBoxAbsenceReason.DataBindings.Add("Text", this.dtAbsence, "Reason");
			
			this.dateTimePickerAbsenceFromData.DataBindings.Add("Value", this.dtAbsence, "FromDate");
			this.dateTimePickerAbsenceToData.DataBindings.Add("Value", this.dtAbsence, "ToDate");
			this.dateTimePickerAbsenceOrderFormData.DataBindings.Add("Value", this.dtAbsence, "OrderFromDate");
 
			this.textBoxAbsenceNumberOrder.DataBindings.Add("Text", this.dtAbsence, "NumberOrder");
		}

		private void RefreshAbsenceDataSource( bool IsFormLoad )
		{
			this.dtAbsence = this.absenceAction.SelectBasicDataFromFirmPersonal( this.parent );
			this.dtAbsence.PrimaryKey = new DataColumn[]{this.dtAbsence.Columns["ID"]};
			this.dataGridAbsence.DataSource = this.dtAbsence;
			this.dtAbsence.TableName = "absence";

			
			this.dtYearHoliday = this.holidayAction.SelectYearHoliday( this.parent );
			this.dtYearHoliday.PrimaryKey = new DataColumn[]{this.dtYearHoliday.Columns[ "year" ]};		
			this.dataGridYears.DataSource = this.dtYearHoliday;
			this.comboBoxForYear.DataSource = this.dtYearHoliday;
			this.comboBoxForYear.DisplayMember = "year";
			this.dtYearHoliday.TableName = "year_holiday";

			this.JustifyGrid(this.dataGridAbsence);
			this.JustifyGrid(this.dataGridYears);

			if( ! this.IsAbsenceLoadForm )
			{
				ClearAbsenceBindings();
			}

			SetAbsenceBindings();

			this.textBoxAbsenceReason.Text = "";
			this.dateTimePickerAbsenceFromData.Text = "";
			this.dateTimePickerAbsenceToData.Text = "";
			this.dateTimePickerAbsenceOrderFormData.Text = "";

			this.IsAbsenceLoadForm = false;
		}

		private void AddAbsencePackageToTable( DataLayer.AbsencePackage package)
		{
			DataRow row = this.dtAbsence.NewRow();
			row["ID"] = package.ID;
			row["Parent"] = package.Parent;
			row["CountDays"] = package.CountDays;
			row["FromDate"] = package.FromDate;
			row["NumberOrder"] = package.NumberOrder;
			row["OrderFromDate"] = package.OrderFromDate;
			row["Reason"] = package.Reason;
			row["ToDate"] = package.ToDate;
			row["TypeAbsence"] = package.TypeAbsence;
			row["modifiedbyuser"] = package.User;
		
			this.dtAbsence.Rows.Add( row );
		}

		private void dataGridAbsence_Click(object sender, System.EventArgs e)
		{
//			if(dataGridAbsence.CurrentRowIndex == -1)
//				return;
//			dataGridAbsence.Select(dataGridAbsence.CurrentRowIndex); 
//
//			int index = comboBoxAbsenceTypeAbsence.FindString( comboBoxAbsenceTypeAbsence.Tag.ToString() );
//			if( index > -1 )
//			{
//				comboBoxAbsenceTypeAbsence.SelectedIndex = index;
//			}
//			//			else
//			//			{
//			//				comboBoxAbsenceTypeAbsence.SelectedIndex = 0;
//			//			}		
		}
		private void buttonHistory_Click(object sender, System.EventArgs e)
		{
//			formYearAdd form = new formYearAdd( this.parent, "year_holiday", "parent",
//				"year", "leftover", "История на отсъствия", this.mainform );
//			form.ShowDialog();
//			this.dtYearHoliday = form.dt;
//			this.dataGridYears.DataSource = this.dtYearHoliday;
//			//this.dtYearHoliday.
//			this.JustifyGrid(this.dataGridYears);
		}

		private void comboBoxAbsenceTypeAbsence_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.comboBoxAbsenceTypeAbsence.SelectedIndex != 0)
			{
				this.comboBoxForYear.Enabled = false;
			}
			else
			{
				this.comboBoxForYear.Enabled = true;
			}
		}

		#endregion

		#region Penalty functions

		private void buttonPenaltyAdd_Click(object sender, System.EventArgs e)
		{
			foreach(TabPage tp in this.tabControl1.TabPages)
			{
				if(this.tabPagePenalty != tp)
				{
					tp.Enabled = false;
				}
			}
				
			Op = Operations.AddPenalty;
			ClearPenaltyBindings();
			if(dataGridPenalty.CurrentRowIndex != -1)
				this.dataGridPenalty.UnSelect(dataGridPenalty.CurrentRowIndex);
			this.IsPenaltyEdit = false;
			this.EnableButtons( false, false, true, false, false, true, LockButtons.Penalty );
			//this.ClearControls( false );
			this.ControlEnabled( true, LockButtons.Penalty);			
		}

		private void buttonPebaltyEdit_Click(object sender, System.EventArgs e)
		{
			Op = Operations.EditPenalty;
			if( this.dataGridPenalty.VisibleRowCount > 0 )
			{
				IsPenaltyEdit = true;
				this.EnableButtons( false, false, true, false, false, true, LockButtons.Penalty);
				this.ControlEnabled( true, LockButtons.Penalty );
			}
		}

		private void buttonPenaltyCancel_Click(object sender, System.EventArgs e)
		{
			foreach(TabPage tp in this.tabControl1.TabPages)
			{
				tp.Enabled = false;
			}
			if(Op == Operations.AddPenalty)  // Трбва да се провери преди смяната на операцията
			{				
				this.numBoxPenaltyOrder.Text = "";
				this.dateTimePenaltyFormDate.Text = "";
				SetPenaltyBindings();
			}
			Op = Operations.ViewPersonData;
			this.ControlEnabled( false, LockButtons.Penalty );
			this.EnableButtons( true, true, false, true, false, false, LockButtons.Penalty );			
		}	

		private void buttonPenaltySave_Click(object sender, System.EventArgs e)
		{
			PenaltySave();
		}
		private bool PenaltySave()
		{
			bool result;
			DataLayer.PenaltyPackage package = new DataLayer.PenaltyPackage();
			result = this.ValidatePenaltyData( package );
			if(result == true)
			{
				foreach(TabPage tp in this.tabControl1.TabPages)
				{
					tp.Enabled = true;
				}	

				if( Op == Operations.AddPenalty )
				{				
					package.ID = this.penaltyAction.InsertPenalty( package );				
					this.AddPenaltyPackageToTable( package );
					this.textBoxNotes.Text = this.textBoxNotes.Text + "\r\nНаказан на " + this.dateTimePickerPenaltyDate.Text;
					note.UpdateNotes( "Notes", this.parent, this.textBoxNotes.Text );
					SetPenaltyBindings();
				}
				else
				{
					DataRow row = this.dtPenalty.Rows[this.dataGridPenalty.CurrentRowIndex ];
					if( row != null )
					{
						row["NumberOrder"] = package.NumberOrder;
						row["Reason"] = package.Reason;
						row["FromDate"] = package.FromDate;
						row["orderdate"] = package.OrderDate;
						row["modifiedbyuser"] = package.User;
						row["typepenalty"] = package.Type;
						row["todate"] = package.ToDate;
						this.penaltyAction.UpdatePenalty( package );
					}
				}
			
				Op = Operations.ViewPersonData;
				this.ControlEnabled( false, LockButtons.Penalty );
				this.EnableButtons( true, true, false, true, false, false, LockButtons.Penalty );
				this.Refresh();
			}
			return result;
		}
		// Da se pogledne funkciqta za iztriwaneto
		private void buttonPenaltyDelete_Click(object sender, System.EventArgs e)
		{
			if( this.dataGridPenalty.VisibleRowCount >= 1 )
			{
				if( MessageBox.Show( this, "Сигурни ли сте че искате да изтриете наказанието " + this.dataGridPenalty[ this.dataGridPenalty.CurrentRowIndex, 2 ].ToString(), "Изтриване", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					this.penaltyAction.DeleteRow( this.dtPenalty.Rows[this.dataGridPenalty.CurrentRowIndex]["id"].ToString());
//					this.RefreshPenaltyDataSource( false );
					//dtPenalty.Rows.Remove(dataGridPenalty.C);
					dtPenalty.Rows.Remove(dtPenalty.Rows[this.dataGridPenalty.CurrentRowIndex]);
					//this.Refresh{};
					this.dataGridPenalty.Refresh();

					this.EnableButtons( true, true, false, true, false, false, LockButtons.Penalty );
				}
			}
		}

		private bool ValidatePenaltyData(DataLayer.PenaltyPackage package)
		{			
			package.Parent = this.parent;
			package.User = this.User;
			if( this.IsPenaltyEdit )
			{
				package.ID = (int)this.dtPenalty.Rows[this.dataGridPenalty.CurrentRowIndex]["id"];
			}		
			if( this.numBoxPenaltyOrder.Text =="" )
			{
				package.NumberOrder = 0;
			}
			else
			{
				package.NumberOrder = int.Parse( this.numBoxPenaltyOrder.Text );
			}
			if( this.comboBoxPenaltyReason.SelectedIndex <= 0 )
			{
				package.Reason = " ";
			}
			else
			{
				package.Reason = this.comboBoxPenaltyReason.Text;
			}

			if( this.comboBoxTypePenalty.SelectedIndex <= 0 )
			{
				package.Type = " ";
			}
			else
			{
				package.Type = this.comboBoxTypePenalty.Text;
			}

			package.OrderDate = this.dateTimePickerPenaltyDate.Value;
			package.FromDate = this.dateTimePenaltyFormDate.Value;
			package.ToDate = this.dateTimePickerPenaltyTo.Value;
			return true;
		}


		private void ClearPenaltyBindings()
		{
			this.dateTimePenaltyFormDate.DataBindings.Clear();
			this.dateTimePickerPenaltyTo.DataBindings.Clear();
			
			this.comboBoxTypePenalty.DataBindings.Clear();
			
			this.comboBoxPenaltyReason.DataBindings.Clear();
			this.numBoxPenaltyOrder.DataBindings.Clear();
			this.numBoxPenaltyOrder.DataBindings.Clear();
			this.dateTimePickerPenaltyDate.DataBindings.Clear();
		}

		private void SetPenaltyBindings()
		{			
			this.comboBoxPenaltyReason.DataBindings.Add("Tag", this.dtPenalty, "Reason");
			this.comboBoxTypePenalty.DataBindings.Add("Tag", this.dtPenalty, "typepenalty");			
			this.numBoxPenaltyOrder.DataBindings.Add("Text", this.dtPenalty, "NumberOrder");
			this.dateTimePenaltyFormDate.DataBindings.Add("Value", this.dtPenalty, "FromDate");
			this.dateTimePickerPenaltyDate.DataBindings.Add("Value", this.dtPenalty, "orderdate");
			this.dateTimePickerPenaltyTo.DataBindings.Add("Value", this.dtPenalty, "todate");
		}

		private void RefreshPenaltyDataSource( bool IsFormLoad)
		{
			//this.dataGridPenalty.Controls.Clear();
			this.dtPenalty = this.penaltyAction.SelectBasicDataFromFirmPersonal( this.parent );
			this.dtPenalty.PrimaryKey = new DataColumn[]{this.dtPenalty.Columns["ID"]};	
			this.dataGridPenalty.DataSource = this.dtPenalty;
			this.dtPenalty.TableName = "penalty";
			JustifyGrid(dataGridPenalty);

			if( ! this.IsPenaltyLoadForm )
			{
				ClearPenaltyBindings();
			}

			SetPenaltyBindings();					
			
			this.numBoxPenaltyOrder.Text = "";
			this.dateTimePenaltyFormDate.Text = "";

			this.IsPenaltyLoadForm = false;		
		}

		private void AddPenaltyPackageToTable( DataLayer.PenaltyPackage package )
		{
			DataRow row = this.dtPenalty.NewRow();
			row["ID"] = package.ID;
			row["parent"] = package.Parent;
			row["Reason"] = package.Reason;
			row["NumberOrder"] = package.NumberOrder;
			row["FromDate"] = package.FromDate;
			row["orderdate"] = package.OrderDate;
			row["modifiedbyuser"] = package.User;
			row["typepenalty"] = package.Type;
			row["todate"] = package.ToDate;
			this.dtPenalty.Rows.Add( row );
		}

		private void dataGridPenalty_Click(object sender, System.EventArgs e)
		{
			if(dataGridPenalty.CurrentRowIndex == -1)
				return;			
			dataGridPenalty.Select(dataGridPenalty.CurrentRowIndex); 
			int index = comboBoxTypePenalty.FindString( comboBoxTypePenalty.Tag.ToString() );
			if( index > -1 )
			{
				comboBoxTypePenalty.SelectedIndex = index;
			}
			else
			{
				comboBoxTypePenalty.SelectedIndex = 0;
			}

			if(dataGridAssignment.CurrentRowIndex == -1)
				return;
			dataGridAssignment.Select(dataGridAssignment.CurrentRowIndex);  //селектираме реда на който е кликнато
			
			index = comboBoxPenaltyReason.FindString( comboBoxPenaltyReason.Tag.ToString() );
			if( index > -1 )
			{
				comboBoxPenaltyReason.SelectedIndex = index;
			}
			else
			{
				comboBoxPenaltyReason.SelectedIndex = 0;
			}
		}	
		#endregion

		#region Personal functions

		private void comboBoxRegion_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.comboBoxNaselenoMqsto.Items.Clear();
			foreach( City city in this.mainform.nomenclaatureData.arrCity)
			{
				if(city.code == this.comboBoxRegion.SelectedIndex)
				{
					this.comboBoxNaselenoMqsto.Items.Add(city.Name);
					this.comboBoxPrefix.Items.Add(city.Prefix);
				}
			}
		}

		private void PersonalDataChanged(object sender, System.EventArgs e)
		{
			this.PersonalDataChangedValue = true;
		}	

		public bool ValidateAddPersonResult(DataLayer.DataPackage package)
		{
			if(package.ID == 0)
			{				
			}
			else
			{
				package.ID = this.parent;
			}

			package.User = this.User;			

			if( this.numBoxEgn.Text == "" )
			{
				MessageBox.Show("Необходимо е да въведете ЕГН на лицето. \n Личните данни няма да бъдат записани.", "Грешка при въвеждане");
				return false;
			}
			else
			{	
				try
				{
					package.Egn = this.numBoxEgn.Text;
					package.bornDate  = new DateTime( int.Parse( package.Egn.Substring( 0, 2 )) + 1900,	int.Parse( package.Egn.Substring( 2, 2 )),	int.Parse( package.Egn.Substring( 4, 2 )) );
				}
				catch(System.ArgumentOutOfRangeException)
				{
					MessageBox.Show("Въведеното ЕГН е некоректно. \n Личните данни няма да бъдат записани.", "Грешка при въвеждане");
					return false;
				}
						
			}

			if(this.textBoxNames.Text == "")
			{
				MessageBox.Show("Необходимо е да въведете име на лицето. \n Личните данни няма да бъдат записани.", "Грешка при въвеждане");
				return false;
			}
			package.FName = this.textBoxNames.Text;

			if( this.textBoxBornTown.Text == "" )
			{
				package.BornTown = " ";
			}
			else
			{
				package.BornTown = this.textBoxBornTown.Text;
			}			
			if( this.comboBoxCategory.SelectedIndex == -1 )
			{
				package.Category = "";
			}
			else
			{
				package.Category = this.comboBoxCategory.SelectedItem.ToString();
			}	

			if( this.comboBoxCountry.SelectedIndex == -1 )
			{
				package.Country = "";
			}
			else
			{
				package.Country = this.comboBoxCountry.SelectedItem.ToString();
			}

			package.DiplomDate = this.textBoxDiplom.Text;  
			if( this.comboBoxEducation.SelectedIndex == -1 )
			{
				package.Education = "";
			}
			else
			{
				package.Education = this.comboBoxEducation.SelectedItem.ToString();
			}

			if( this.comboBoxFamilyStatus.SelectedIndex == -1 )
			{
				package.FamilyStatus = "";
			}
			else
			{
				package.FamilyStatus = this.comboBoxFamilyStatus.SelectedItem.ToString();
			}

			package.HiredAt = this.dateTimePickerPostypilNa.Value;

			package.Kwartal = this.textBoxKwartal.Text;
			if( this.checkedListBoxLanguage.SelectedIndex == -1 )
			{
				package.Languages = " ";
			}
			else
			{
				package.Languages = this.checkedListBoxLanguage.SelectedItem.ToString();
			}

			if( this.comboBoxMilitaryRang.SelectedIndex == -1 )
			{
				package.MilitaryRang = "";
			}
			else
			{
				package.MilitaryRang = this.comboBoxMilitaryRang.SelectedItem.ToString();
			}

			if( this.comboBoxMilitaryStatus.SelectedIndex == 0 )
			{
				package.MilitaryStatus = false;
			}
			else
			{
				package.MilitaryStatus = true;
			}

			package.NumBlockHouse = this.textBoxNumBlock.Text;

			package.PCard = this.numBoxPcCard.Text;
			
			package.PCardPublish = this.dateTimePickerPCCardPublished.Value;

			package.PublishedBy = this.textBoxPublishedFrom.Text;
			if( this.comboBoxRegion.SelectedIndex == -1 )
			{
				package.Region = "";
			}
			else
			{
				package.Region = this.comboBoxRegion.SelectedItem.ToString();
			}

			if( this.comboBoxScienceLevel.SelectedIndex == -1 )
			{
				package.ScienceLevel = "";
			}
			else
			{
				package.ScienceLevel = this.comboBoxScienceLevel.SelectedItem.ToString();
			}

			if( this.comboBoxScience.SelectedIndex == -1 )
			{
				package.ScienceTitle = "";
			}
			else
			{
				package.ScienceTitle = this.comboBoxScience.SelectedItem.ToString();
			}

			package.Street = this.textBoxStreet.Text;
			
			package.Phone = this.numBoxTelephone.Text;
			
			if( this.comboBoxNaselenoMqsto.SelectedIndex == -1 )
			{
				package.Town = "";
			}
			else
			{
				package.Town = this.comboBoxNaselenoMqsto.SelectedItem.ToString();
			}

//			if( this.comboBoxEmployeStatus.SelectedIndex == -1 )
//			{
//				package.WorkStatus = "";
//			}
//			else
//			{
//				package.WorkStatus = this.comboBoxEmployeStatus.SelectedItem.ToString();
//			}

			if( this.comboBoxProfesion.SelectedIndex == -1 )
			{
				package.Profession = "";
			}
			else
			{
				package.Profession = this.comboBoxProfesion.SelectedItem.ToString();
			}

			if( this.comboBoxSex.SelectedIndex == -1 )
			{
				package.Sex = "";
			}
			else
			{
				package.Sex = this.comboBoxSex.SelectedItem.ToString();
			}		
			package.fired = 0;		
			return true;
		}		
		
		private void buttonPicture_Click(object sender, System.EventArgs e)
		{
			string fileName;
			long m_lImageFileLength;
			byte[] m_barrImg;
			FileStream fs;
			try
			{

				if( this.openFileDialog1.ShowDialog( this ) == DialogResult.OK )
				{
					fileName = this.openFileDialog1.FileName;
					FileInfo fiImage=new FileInfo( fileName );
					m_lImageFileLength=fiImage.Length;
					if( m_lImageFileLength > 1000000 )
					{
						MessageBox.Show( "Файлът е по-голям от допустимия размер! " );				
						fiImage = null;
						return;
					}
					
					this.pictureBox1.Image=Image.FromFile( fileName );
					
					fs=new FileStream( fileName, FileMode.Open, 
						FileAccess.Read,FileShare.Read);
					m_barrImg=new byte[Convert.ToInt32(m_lImageFileLength)];
					int iBytesRead = fs.Read(m_barrImg,0, 
						Convert.ToInt32(m_lImageFileLength));
					fs.Close();
					if( this.pictureAction.CheckForPicture( this.parent ))
					{
						this.pictureAction.UpdatePicture( "pictures", this.parent, m_barrImg );
					}
					else
					{
						this.pictureAction.InsertValueIntoTable( "pictures", this.parent, m_barrImg );
					}
					m_barrImg = null;
					fs = null;
				}
			}
			catch(Exception ex)
			{
				m_barrImg = null;
				fs = null;
				//fs.Close();
				MessageBox.Show(ex.Message);
			}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if( this.pictureAction.CheckForPicture( this.parent ))
			{
				this.pictureAction.DeletePicture( this.parent );
				this.pictureBox1.Image = null;
			}
			else
			{
				MessageBox.Show( "Няма избрана снимка!" );
			}
		}
		
		private void LanguageChanged(object sender, System.EventArgs e)
		{
			this.PersonalDataChangedValue = true;

			if( this.checkedListBoxLanguage.GetSelected( this.checkedListBoxLanguage.SelectedIndex ) )
			{
				string level = "";
				int i = 0;
				foreach( DataRow row in this.dtLanguage.Rows )
				{
					if( (string)row[ "language" ] == checkedListBoxLanguage.SelectedItem.ToString() )
					{
						level = (string)row[ "level" ];
						break;
					}												
				}
				i = this.comboBoxLanguageLevel.FindString( level );
				if( i > -1 )
					this.comboBoxLanguageLevel.SelectedIndex = i;
			}
		}

		private void comboBoxLanguageLevel_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.PersonalDataChangedValue = true;
			foreach( DataRow row in this.dtLanguage.Rows )
			{
				if(checkedListBoxLanguage.SelectedItem == null)
					break;
				if( (string)row["language"] == checkedListBoxLanguage.SelectedItem.ToString() )
				{
					row["level"] =  this.comboBoxLanguageLevel.SelectedItem.ToString();
					break;
				}
			}
		}

		private void checkedListBoxLanguage_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
		{
			if( !IsLoading )
			{
				this.PersonalDataChangedValue = true;
				if( !checkedListBoxLanguage.GetItemChecked( this.checkedListBoxLanguage.SelectedIndex ) )
				{
					//Insert
					this.dtLanguage.Rows.Add( new object[]{ checkedListBoxLanguage.SelectedItem.ToString(), this.comboBoxLanguageLevel.SelectedItem.ToString() });
				}
				else
				{
					//Delete
					int i = 0;
					bool HaveLanguage = false;
					foreach( DataRow row in this.dtLanguage.Rows )
					{
						i++;
						if( (string)row["language"] == checkedListBoxLanguage.SelectedItem.ToString() )
						{
							HaveLanguage = true;
							break;
						}
					}
					if( HaveLanguage )
					{
						this.dtLanguage.Rows.RemoveAt( i -1 );
					}
				}
			}
		}

		private void comboBoxNaselenoMqsto_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.comboBoxPrefix.SelectedIndex = this.comboBoxNaselenoMqsto.SelectedIndex;
		}

		#endregion
		 
		#region Fired functions
		private void SetFiredBindings()
		{			
			this.comboBoxFiredCompensationMistimed.DataBindings.Add( "Text", this.dtFired, "CompensationTime" );
			this.comboBoxFiredComponsationWork.DataBindings.Add( "Text", this.dtFired, "CompensationWork" );
			this.comboBoxFiredNumberSalary.DataBindings.Add( "Text", this.dtFired, "NumberSalary" );
			this.comboBoxFiredReason.DataBindings.Add( "Tag", this.dtFired, "reason" );
			this.textBoxFiredCompensation.DataBindings.Add( "Text", this.dtFired, "Compensation" );
			this.dateTimePickerFiredFromDate.DataBindings.Add( "Value", this.dtFired, "FromDate" );
			this.numBoxFiredUnusedHoliday.DataBindings.Add( "Text", this.dtFired, "CountDays" );
		}
        
		private void ClearFiredBindings()
		{
			this.comboBoxFiredCompensationMistimed.DataBindings.Clear();
			this.comboBoxFiredComponsationWork.DataBindings.Clear();
			this.comboBoxFiredNumberSalary.DataBindings.Clear();
			this.comboBoxFiredReason.DataBindings.Clear();
			this.textBoxFiredCompensation.DataBindings.Clear();
			this.dateTimePickerFiredFromDate.DataBindings.Clear();
			this.numBoxFiredUnusedHoliday.DataBindings.Clear();
		}
		private void ValidateFiredData(DataLayer.FiredPackage package)
		{			
			package.Parent = this.parent;
			package.User = this.User;
			if( this.IsFiredEdit )
			{
				package.ID = int.Parse( this.dtFired.Rows[this.dataGridFired.CurrentRowIndex]["id"].ToString());
			}		
			
			if( this.comboBoxFiredReason.SelectedIndex <= 0 )
			{
				package.Reason = " ";
			}
			else
			{
				package.Reason = this.comboBoxFiredReason.Text;
			}
			
//			if( this.comboBoxFiredComponsationWork.SelectedIndex <= 0 )
//			{
//				package.CompensationWork = " ";
//			}
//			else
//			{
				package.CompensationWork= this.comboBoxFiredComponsationWork.Text;
//			}
			
//			if( this.comboBoxFiredCompensationMistimed.SelectedIndex <= 0 )
//			{
//				package.CompensationTime = " ";
//			}
//			else
//			{
				package.CompensationTime = this.comboBoxFiredCompensationMistimed.Text;
//			}
//			if( this.comboBoxFiredNumberSalary.SelectedIndex <= 0 )
//			{
//				package.NumbertSalary = " ";
//			}
//			else
//			{
				package.NumbertSalary = this.comboBoxFiredNumberSalary.Text;
//			}
			package.CountDays = int.Parse( numBoxFiredUnusedHoliday.Text );
			package.Compensation = textBoxFiredCompensation.Text;
			package.FromDate= this.dateTimePickerFiredFromDate.Value;
			
		}

		private void AddFiredPackageToTable( DataLayer.FiredPackage package )
		{
			DataRow row = this.dtFired.NewRow();
			row["ID"] = package.ID;
			row["BaseSalary"] = package.BaseSalary;
			row["Compensation"] = package.Compensation;
			row["CompensationTime"] = package.CompensationTime;
			row["CompensationWork"] = package.CompensationWork;
			row["CountDays"] = package.CountDays;
			row["FromDate"] = package.FromDate;
			row["Level1"] = package.Level1;
			row["Level2"] = package.Level2;
			row["Level3"] = package.Level3;
			row["Level4"] = package.Level4;
			row["NumberSalary"] = package.NumbertSalary;
			row["Position"] = package.Position;
			row["Reason"] = package.Reason;
			row["ModifiedByUser"] = package.User;
			this.dtFired.Rows.Add( row );
		}
		private void buttonFiredNew_Click(object sender, System.EventArgs e)
		{
			foreach(TabPage tp in this.tabControl1.TabPages)
			{
				if(this.tabPageFired != tp)
				{
					tp.Enabled = false;
				}
			}
				
			Op = Operations.AddFired;
			ClearFiredBindings();
			if(dataGridFired.CurrentRowIndex != -1)
				this.dataGridFired.UnSelect(dataGridFired.CurrentRowIndex);
			this.IsFiredEdit = false;
			this.EnableButtons( false, false, true, false, false, true, LockButtons.Fired );
			//this.ClearControls( false );
			this.ControlEnabled( true, LockButtons.Fired);	
		}

		private void buttonFiredEdit_Click(object sender, System.EventArgs e)
		{
			Op = Operations.EditFired;
			if( this.dataGridFired.VisibleRowCount > 0 )
			{
				IsFiredEdit = true;
				this.EnableButtons( false, false, true, false, false, true, LockButtons.Fired);
				this.ControlEnabled( true, LockButtons.Fired );
			}
		}

		private void buttonFiredCancel_Click(object sender, System.EventArgs e)
		{
			foreach(TabPage tp in this.tabControl1.TabPages)
			{
				tp.Enabled = false;
			}
			if(Op == Operations.AddFired)  // Трбва да се провери преди смяната на операцията
			{				
				//				this.numBoxPenaltyOrder.Text = "";
				//				this.dateTimePenaltyFormDate.Text = "";
				SetFiredBindings();
			}
			Op = Operations.ViewPersonData;
			this.ControlEnabled( false, LockButtons.Fired );
			this.EnableButtons( true, true, false, true, false, false, LockButtons.Fired );			
		}

		private void buttonFiredSave_Click(object sender, System.EventArgs e)
		{
			foreach(TabPage tp in this.tabControl1.TabPages)
			{
				tp.Enabled = true;
			}
			DataLayer.FiredPackage package = new DataLayer.FiredPackage();
			this.ValidateFiredData( package );

			if( Op == Operations.AddFired )
			{				
				package.ID = this.firedAction.InsertFired( package );				
				this.AddFiredPackageToTable( package );
				this.textBoxNotes.Text = this.textBoxNotes.Text + "\r\nОсвободен на " + this.dateTimePickerFiredFromDate.Text;
				note.UpdateNotes( "Notes", this.parent, this.textBoxNotes.Text );
				SetFiredBindings();
			}
			else
			{
				DataRow row = this.dtFired.Rows[this.dataGridFired.CurrentRowIndex ];
				if( row != null )
				{
					row["BaseSalary"] = package.BaseSalary;
					row["Compensation"] = package.Compensation;
					row["CompensationTime"] = package.CompensationTime;
					row["CompensationWork"] = package.CompensationWork;
					row["CountDays"] = package.CountDays;
					row["FromDate"] = package.FromDate;
					row["Level1"] = package.Level1;
					row["Level2"] = package.Level2;
					row["Level3"] = package.Level3;
					row["Level4"] = package.Level4;
					row["NumberSalary"] = package.NumbertSalary;
					row["Position"] = package.Position;
					row["Reason"] = package.Reason;
					row["ModifiedByUser"] = package.User;
					this.firedAction.UpdateFired( package );
				}
			}
			
			Op = Operations.ViewPersonData;
			this.ControlEnabled( false, LockButtons.Fired );
			this.EnableButtons( true, true, false, true, false, false, LockButtons.Fired );
			this.Refresh();
		}

		private void buttonFiredDelete_Click(object sender, System.EventArgs e)
		{
			if( this.dataGridFired.VisibleRowCount >= 1 )
			{
				if( MessageBox.Show( this, "Сигурни ли сте че искате да изтриете прекратяването " + this.dataGridFired[ this.dataGridFired.CurrentRowIndex, 2 ].ToString(), "Изтриване", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					this.firedAction.DeleteRow( this.dtFired.Rows[this.dataGridFired.CurrentRowIndex]["id"].ToString());
					//					this.RefreshPenaltyDataSource( false );
					//dtPenalty.Rows.Remove(dataGridPenalty.C);
					dtFired.Rows.Remove(dtFired.Rows[this.dataGridFired.CurrentRowIndex]);
					//this.Refresh{};
					this.dataGridFired.Refresh();

					this.EnableButtons( true, true, false, true, false, false, LockButtons.Fired );
				}
			}
		}
	
		private void buttonFire_Click(object sender, System.EventArgs e)
		{
			if( this.dataGridFired.VisibleRowCount >= 1 && this.dtAssignment.Rows.Count > 0)
			{
				if( MessageBox.Show( this, "Сигурни ли сте че искате да прекратите трудовият договор?", "Прекратяване", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{					
					DataRow rowFired = this.dtFired.Rows.Find(this.dtFired.Rows[this.dataGridFired.CurrentRowIndex]["ID"]);
					DataRow rowPosition = this.dtPosition.Rows.Find(this.dtAssignment.Rows[this.dataGridAssignment.CurrentRowIndex]["PositionID"]);
					if(rowPosition != null && rowFired != null)
					{
						DataLayer.FiredPackage package = new DataLayer.FiredPackage();
						this.ValidateFiredData(package);
						package.Level1 = rowPosition["level1"].ToString();
						package.Level2 = rowPosition["level2"].ToString();
						package.Level3 = rowPosition["level3"].ToString();
						package.Level4 = rowPosition["level4"].ToString();
						package.Position = rowPosition["position"].ToString();
						package.BaseSalary = rowPosition["basesalary"].ToString();
						package.ID = (int)rowFired["id"];
						this.firedAction.UpdateFired(package);

						if(int.Parse(this.dtAssignment.Rows[this.dataGridAssignment.CurrentRowIndex]["IsActive"].ToString()) == 1 && rowPosition["TypePosition"].ToString() == "Постоянна")
						{

							this.assignmentAction.UpdateStaff( int.Parse( rowPosition["ID"].ToString() ), false, 1);
							this.personAction.UpdatePersonPosition(this.parent, 0);
							Op = Operations.FirePerson;
							this.Save_Person(sender, e);
						}
						else if(int.Parse( this.dtAssignment.Rows[this.dataGridAssignment.CurrentRowIndex]["IsActive"].ToString()) == 1 && rowPosition["TypePosition"].ToString() == "Сезонна")
						{
							this.personAction.UpdatePersonPosition(this.parent, 0);
						}
					}
					//					this.assignmentAction.DeleteRow( this.dtAssignment.Rows[this.dataGridAssignment.CurrentRowIndex]["ID"].ToString() );
					//					this.dtAssignment.Rows.Remove(this.dtAssignment.Rows[this.dataGridAssignment.CurrentRowIndex]);
					//					this.dataGridAssignment.Refresh();					
				}
			}
			else
			{
				if( this.dtAssignment.Rows.Count <= 0 )
				{
					MessageBox.Show("Служителят не е назначен");
				}
				else
				{
					MessageBox.Show(" Не са въведени данни за прекратяване на договорът");
				}
			}
		}
		private void RefreshFiredDataSource( bool IsFormLoad)
		{			
			this.dtFired = this.firedAction.SelectWhere( "fired", new string[] {"*"}, 1, "" );
			this.dtFired.PrimaryKey = new DataColumn[]{this.dtFired.Columns["ID"]};	
			this.dataGridFired.DataSource = this.dtFired;
			this.dtFired.TableName = "Fired";
			JustifyGrid(dataGridFired);			
			SetFiredBindings();
		}

		private void dataGridFired_Click(object sender, System.EventArgs e)
		{
			if(dataGridFired.CurrentRowIndex == -1)
				return;			
			dataGridFired.Select(dataGridFired.CurrentRowIndex); 
			int index = comboBoxFiredReason.FindString( comboBoxFiredReason.Tag.ToString() );
			if( index > -1 )
			{
				comboBoxFiredReason.SelectedIndex = index;
			}
			else
			{
				comboBoxFiredReason.SelectedIndex = 0;
			}			
		}

		#endregion

		#region Other Functions

		private void buttonОК_Click(object sender, EventArgs e)
		{
			Save_Person(sender, e);
			this.Close();
		}

		//Da se dowyr[i funkciqta za zapomnqneto
		private void buttonSave_Click(object sender, System.EventArgs e)
		{
			bool result = Save_Person(sender, e);
			if(Op == Operations.AddNewPerson && result)
			{
				Op = Operations.ViewPersonData;
				//this.EnableButtons( true, true, false, true, false, false, LockButtons.Penalty );
				//this.EnableButtons( true, true, false, true, false, false, LockButtons.Absence );
				this.EnableButtons( true, true, false, true, false, false, LockButtons.Assignment );	
			}
		}

		private bool Save_Person(object sender, System.EventArgs e)
		{
			bool result;
			DataLayer.DataPackage package = new DataLayer.DataPackage();
			DataLayer.DataAction action = new DataLayer.DataAction( "person", this.mainform.connString );
			package.ID = this.parent;
			result = this.ValidateAddPersonResult( package );
			if(result == true)
			{
				switch (Op)
				{
					case Operations.AddNewPerson :
					{					
						action.InsertPerson( package);
						package.ID = action.GetLastInsertID();					
						this.parent = package.ID;
						DataRow row = this.mainform.dtKartoteka.NewRow();

						row["id"] = package.ID;
						row["bornTown"] = package.BornTown;
						row["category"] = package.Category;
						row["country"] = package.Country;
						row["diplomdate"] = package.DiplomDate;
						row["education"] = package.Education;
						row["egn"] = package.Egn;
						row["familystatus"] = package.FamilyStatus;
						row["name"] = package.FName;
						row["hiredat"] = package.HiredAt;
						row["id"] = package.ID;
						row["kwartal"] = package.Kwartal;
						row["languages"] = package.Languages;
						row["militaryrang"] = package.MilitaryRang;
						row["militarystatus"] = package.MilitaryStatus;
						row["numblockhouse"] = package.NumBlockHouse;
						row["pcard"] = package.PCard;
						row["pcardpublish"] = package.PCardPublish;
						row["profession"] = package.Profession;
						row["publishedby"] = package.PublishedBy;
						row["region"] = package.Region;
						row["sciencelevel"] = package.ScienceLevel;
						row["sciencetitle"] = package.ScienceTitle;
						row["sex"] = package.Sex;
						row["street"] = package.Street;
						row["Phone"] = package.Phone;
						row["town"] = package.Town;
						row["workexperience"] = package.WorkExperience;
						row["bornDate"] = package.bornDate;
					
						this.mainform.dtKartoteka.Rows.Add( row );
						PersonalDataChangedValue = false;
						foreach(DataRow rowD in this.dtLanguage.Rows )
						{						
							this.languageAction.InsertValueIntoTable( "languagelevel", this.parent, (string)rowD["language"], (string)rowD["level"]);
						}
						MessageBox.Show("ДАнните за лицето са записани");
						break;
					}
					case Operations.ViewPersonData :
					{
						if(PersonalDataChangedValue)
						{
							action.UpdatePerson( package, this.parent );
							//////////////////////////// Update predstawlqwa triene na starite i insert na nowite
							this.languageAction.DeletePicture( this.parent );
							foreach(DataRow rowD in this.dtLanguage.Rows )
							{						
								this.languageAction.InsertValueIntoTable( "languagelevel", this.parent, (string)rowD["language"], (string)rowD["level"]);
							}
							////////////////////////////
							DataRow row = this.mainform.dtKartoteka.Rows.Find(this.parent);
							row["borntown"] = package.BornTown;
							row["category"] = package.Category;
							row["country"] = package.Country;
							row["diplomdate"] = package.DiplomDate;
							row["education"] = package.Education;
							row["egn"] = package.Egn;
							row["familystatus"] = package.FamilyStatus;
							row["name"] = package.FName;
							row["hiredat"] = package.HiredAt;
							row["id"] = package.ID;
							row["kwartal"] = package.Kwartal;
							row["languages"] = package.Languages;
							row["militaryrang"] = package.MilitaryRang;
							row["militarystatus"] = package.MilitaryStatus;
							row["numblockhouse"] = package.NumBlockHouse;
							row["pcard"] = package.PCard;
							row["pcardpublish"] = package.PCardPublish;
							row["profession"] = package.Profession;
							row["publishedby"] = package.PublishedBy;
							row["region"] = package.Region;
							row["sciencelevel"] = package.ScienceLevel;
							row["sciencetitle"] = package.ScienceTitle;
							row["sex"] = package.Sex;
							row["street"] = package.Street;
							row["Phone"] = package.Phone;
							row["town"] = package.Town;
							row["workexperience"] = package.WorkExperience;
							row["bornDate"] = package.bornDate;

							PersonalDataChangedValue = false;	
							MessageBox.Show("Данните за лицето са записани");
						}
						break;
					}
					case Operations.FirePerson :
					{
						package.fired = 1;
						package.exported = 1;
						action.UpdatePerson( package, this.parent );
					         									
						mainform.dtKartoteka.Rows.Remove(mainform.dtKartoteka.Rows.Find(this.parent) );
						break;
					}
					case Operations.AddPenalty :
					{
						this.buttonPenaltyAdd_Click(sender, e);
						break;
					}
					case Operations.EditPenalty :
					{
						this.buttonPenaltyAdd_Click(sender, e);
						break;
					}
					case Operations.AddAbsence :
					{					
						this.buttonAbsenceSave_Click(sender, e);
						break;
					}
					case Operations.EditAbsence :
					{					
						this.buttonAbsenceSave_Click(sender, e);
						break;
					}
					case Operations.AddAssignment :
					{
						this.buttonAssignmentSave_Click(sender, e);
						break;
					}
					case Operations.EditAssignment :
					{
						this.buttonAssignmentSave_Click(sender, e);
						break;
					}
				}
			}	
			return result;
		}		
		private void EnableButtons(bool add, bool edit, bool save, bool delete, bool print, bool cancel, LockButtons Enum)
		{
			switch( Enum )
			{
				case LockButtons.Penalty : 
				{
					this.buttonPenaltyAdd.Enabled = add;
					this.buttonPebaltyEdit.Enabled = edit;
					this.buttonPenaltySave.Enabled = save;
					this.buttonPenaltyDelete.Enabled = delete;
					this.buttonPenaltyCancel.Enabled = cancel;
					this.buttonPenaltyPrint.Enabled = print;
					break;
				}
				case LockButtons.Absence : 
				{
					this.buttonAbsenceAdd.Enabled = add;
					this.buttonAbsenceEdit.Enabled = edit;
					this.buttonAbsenceSave.Enabled = save;
					this.buttonAbsenceDelete.Enabled = delete;
					this.buttonAbsenceCancel.Enabled = cancel;
					this.buttonAbsencePrint.Enabled = print;
					break;
				}
				case LockButtons.Assignment : 
				{
					this.buttonAssignment.Enabled = add;
					this.buttonAssignmentEdit.Enabled = edit;
					this.buttonAssignmentSave.Enabled = save;
					this.buttonAssignmentDelete.Enabled = delete;
					this.buttonAssignmentPrint.Enabled = print;
					this.buttonAssignmentCancel.Enabled = cancel;
					break;
				}
				case LockButtons.Fired : 
				{
					this.buttonFiredNew.Enabled = add;
					this.buttonFiredEdit.Enabled = edit;
					this.buttonFiredSave.Enabled = save;
					this.buttonFiredDelete.Enabled = delete;
					this.buttonFiredPrint.Enabled = print;
					this.buttonFiredCancel.Enabled = cancel;
					break;
				}

			}
		}

		private void ControlEnabled( bool IsEnabled, LockButtons Enum)
		{
			switch( Enum )
			{
				case LockButtons.Penalty :
				{
					foreach(Control ctrl in this.tabPagePenalty.Controls)
					{
						if( ctrl.GetType().Name != "Button")
						{
							ctrl.Enabled = IsEnabled;
						}
					}
					
					this.groupBoxPenaltyGrid.Enabled = ! IsEnabled;
					
					
//					this.dateTimePenaltyFormDate.Enabled = !ToMakeReadOnly;
//					this.dateTimePickerPenaltyDate.Enabled = !ToMakeReadOnly;
//					this.textBoxPenaltyReason.ReadOnly  = ToMakeReadOnly;   ///>????????????
//					this.numBoxPenaltyOrder.ReadOnly = ToMakeReadOnly; ///???????????????
					break;
				}
				case LockButtons.Absence:
				{
					foreach(Control ctrl in this.tabPageAbsence.Controls)
					{
						if( ctrl.GetType().Name != "Button")
						{
							ctrl.Enabled = IsEnabled;
						}
					}					
					this.groupBoxAbsenceGrid.Enabled = ! IsEnabled;
					break;
				}
				case LockButtons.Fired:
				{
					foreach(Control ctrl in this.tabPageFired.Controls)
					{
						if( ctrl.GetType().Name != "Button")
						{
							ctrl.Enabled = IsEnabled;
						}
					}					
					this.groupBoxFired.Enabled = ! IsEnabled;
					break;
				}
				case LockButtons.Assignment:
				{
					foreach(Control ctrl in this.tabPageAssignment.Controls)
					{
						if( ctrl.GetType().Name != "Button")
						{
							ctrl.Enabled = IsEnabled;
						}
					}
					if(Op == Operations.ViewPersonData)
					{
						this.groupBoxAssignmentGrid.Enabled = true;
						this.radioButtonAdditional.Enabled = true;
						this.radioButtonAssignment.Enabled = true;
					}					
					break;
				}
				case LockButtons.Notes:
				{
					this.buttonNotes.Enabled = IsEnabled;					
					break;
				}
			}
			EnableTabs(!IsEnabled);
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void PersonalDataForm_Load(object sender, System.EventArgs e)
		{
			LoadNomenklatures();			

			if( Op == Operations.ViewPersonData)
			{
				this.IsAssignmentLoadForm = true;
				this.assignmentAction = new DataLayer.AssignmentAction( "PersonAssignment", this.mainform.connString);
				this.RefreshAssignmentDataSource( true );
				this.ControlEnabled( false, LockButtons.Assignment );
				this.EnableButtons( true, true, false, true, true, false, LockButtons.Assignment );
				LoadPersonalData();	
						
//				if(this.dtAssignment.Rows.Count > 0)
//				{
					this.IsPenaltyLoadForm = true;
					this.RefreshPenaltyDataSource( true );
					this.ControlEnabled( false, LockButtons.Penalty );
					this.EnableButtons( true, true, false, true, false, false, LockButtons.Penalty );

					this.IsAbsenceLoadForm = true;
					this.RefreshAbsenceDataSource( true );
					this.ControlEnabled( false, LockButtons.Absence );
					this.EnableButtons( true, true, false, true, false, false, LockButtons.Absence );

					this.RefreshFiredDataSource(true);
					this.ControlEnabled(false, LockButtons.Fired);
					this.EnableButtons( true, true, false, true, false, false, LockButtons.Fired);
//				}
			}
			else
			{
				this.RefreshPenaltyDataSource( true );
				this.RefreshAbsenceDataSource( true );

				this.assignmentAction = new DataLayer.AssignmentAction( "PersonAssignment", this.mainform.connString);
				this.IsAssignmentLoadForm = true;
				this.RefreshAssignmentDataSource( true );

				this.ControlEnabled(false, LockButtons.Penalty);
				this.ControlEnabled(false, LockButtons.Assignment);
				this.ControlEnabled(false, LockButtons.Absence);
				this.ControlEnabled(false, LockButtons.Fired);
				this.ControlEnabled(false, LockButtons.Notes);
				this.EnableButtons( false, false, false, false, false, false, LockButtons.Penalty );
				this.EnableButtons( false, false, false, false, false, false, LockButtons.Absence );
				this.EnableButtons( false, false, false, false, false, false, LockButtons.Assignment );
				this.EnableButtons( false, false, false, false, false, false, LockButtons.Fired );

			}
		}

		private void LoadPersonalData()
		{
			DataSet dsPerson;
			string arg = "0";
			
			DataLayer.DataAction daa = new DataLayer.DataAction("", this.mainform.connString );

			dsPerson = daa.SelectAllInfoForPerson( this.parent );
			#region Loading Personal Info	
			//DataSet ds;		
			

			try
			{
				this.numBoxEgn.Text = dsPerson.Tables[0].Rows[0]["egn"].ToString();
				this.textBoxNames.Text = dsPerson.Tables[0].Rows[0]["name"].ToString();
				this.textBoxDiplom.Text = dsPerson.Tables[0].Rows[0]["diplomdate"].ToString();
				this.textBoxKwartal.Text = dsPerson.Tables[0].Rows[0]["kwartal"].ToString();
				this.textBoxNumBlock.Text = dsPerson.Tables[0].Rows[0]["numblockhouse"].ToString();
				this.textBoxPublishedFrom.Text = dsPerson.Tables[0].Rows[0]["publishedby"].ToString();
				this.textBoxStreet.Text = dsPerson.Tables[0].Rows[0]["street"].ToString();
				this.numBoxTelephone.Text = dsPerson.Tables[0].Rows[0]["phone"].ToString();
				this.numBoxPcCard.Text = dsPerson.Tables[0].Rows[0]["pcard"].ToString();
				this.dateTimePickerPCCardPublished.Value = (DateTime)dsPerson.Tables[0].Rows[0]["pcardpublish"];
				this.dateTimePickerPostypilNa.Value = (DateTime)dsPerson.Tables[0].Rows[0]["hiredat"];
	            this.textBoxBornTown.Text = dsPerson.Tables[0].Rows[0]["borntown"].ToString();
				if( int.Parse(dsPerson.Tables[0].Rows[0]["militarystatus"].ToString()) == 0 )
				{
					this.comboBoxMilitaryStatus.SelectedIndex = 0;
				}
				else
				{
					this.comboBoxMilitaryStatus.SelectedIndex = 1;
				}
				
				arg = dsPerson.Tables[0].Rows[0]["familystatus"].ToString();
				if( arg  == "" )
				{
					arg = "0";
				}
				int index = this.comboBoxFamilyStatus.FindStringExact( arg );
				if( index > -1 )
				{
					this.comboBoxFamilyStatus.SelectedIndex = index;
				}
				index = 0;
				
				arg = dsPerson.Tables[0].Rows[0]["profession"].ToString();
				if( arg  == "" )
				{
					arg = "0";
				}
				index = this.comboBoxProfesion.FindStringExact( arg );
				if( index > -1 )
				{
					this.comboBoxProfesion.SelectedIndex = index;
				}
				index = 0;

				arg = dsPerson.Tables[0].Rows[0]["sciencelevel"].ToString();
				if( arg  == "" )
				{
					arg = "0";
				}
				index = this.comboBoxScienceLevel.FindStringExact( arg );
				if( index > -1 )
				{
					this.comboBoxScienceLevel.SelectedIndex = index;
				}
				index = 0;

				arg = dsPerson.Tables[0].Rows[0]["sciencetitle"].ToString();
				if( arg  == "" )
				{
					arg = "0";
				}
				index = this.comboBoxScience.FindStringExact( arg );
				if( index > -1 )
				{
					this.comboBoxScience.SelectedIndex = index;
				}
				index = 0;

				arg = dsPerson.Tables[0].Rows[0]["militaryrang"].ToString();
				if( arg  == "" )
				{
					arg = "0";
				}
				index = this.comboBoxMilitaryRang.FindStringExact(arg );
				if( index > -1 )
				{
					this.comboBoxMilitaryRang.SelectedIndex = index;
				}
				index = 0;

				///////////////////languages				
				index = this.checkedListBoxLanguage.FindStringExact( (string)dsPerson.Tables[0].Rows[0]["languages"]);
				if( index > -1 )
				{
					this.checkedListBoxLanguage.SelectedIndex = index;
					this.checkedListBoxLanguage.SetItemChecked(index, true );
				}
				index = 0;
				//ds.Tables.Remove( ds.Tables[0] );
				///////////////////////////////////////////////////////
				
				arg = dsPerson.Tables[0].Rows[0]["education"].ToString();
				if( arg  == "" )
				{
					arg = "0";
				}	
				index = this.comboBoxEducation.FindStringExact(arg );
				if( index > -1 )
				{
					this.comboBoxEducation.SelectedIndex = index;
				}
				index = 0;
				
				arg = dsPerson.Tables[0].Rows[0]["country"].ToString();
				if( arg  == "" )
				{
					arg = "0";
				}		
				index = this.comboBoxCountry.FindStringExact( (string)dsPerson.Tables[0].Rows[0]["country"]);
				if( index > -1 )
				{
					this.comboBoxCountry.SelectedIndex = index;
				}
				index = 0;

				arg = dsPerson.Tables[0].Rows[0]["category"].ToString();
				if( arg  == "" )
				{
					arg = "0";
				}
				index = this.comboBoxCategory.FindStringExact( arg );
				if( index > -1 )
				{
					this.comboBoxCategory.SelectedIndex = index;
				}
				index = 0;

				index = this.comboBoxRegion.FindStringExact( (string)dsPerson.Tables[0].Rows[0]["region"]);
				if( index > -1 )
				{
					this.comboBoxRegion.SelectedIndex = index;
				}
				index = 0;

				index = this.comboBoxNaselenoMqsto.FindStringExact( (string)dsPerson.Tables[0].Rows[0]["town"]);
				if( index > -1)
				{
					this.comboBoxNaselenoMqsto.SelectedIndex = index;
				}
				index = 0;

				index = this.comboBoxSex.FindStringExact( (string)dsPerson.Tables[0].Rows[0]["sex"].ToString());
				if( index > -1 )
				{
					this.comboBoxSex.SelectedIndex = index;
				}
				index = 0;


				if( this.dtAssignment.Rows.Count > 0)
				{
					//Трудов стаж	
					CalculatePersonalExperience();
				}	
				this.Text += " " + this.textBoxNames.Text;
			}
				//catch(SystemOut
			catch(System.ArgumentException e)
			{
				MessageBox.Show(e.Message,"Липсващи данни за лицето",MessageBoxButtons.OK,MessageBoxIcon.Error);
				this.Close();
			}

			#endregion

			#region Loading Notes Info

			this.dtNotes = this.note.SelectAllFormNotes( "Notes", this.parent );
			if( this.dtNotes.Rows.Count > 0 )
			{
				this.textBoxNotes.Text = this.dtNotes.Rows[ 0 ][ "level" ].ToString();
			}

			#endregion

			#region Loading Picture

			
			byte[] img = null;
			img = this.pictureAction.SelectWhere( "Pictures", this.parent );
			if( img != null )
			{
				MemoryStream stream = new MemoryStream( img );
				pictureBox1.Image=Image.FromStream( stream );
			}
			#endregion
			
			#region Loading Languages
//			this.dtLanguage.Columns.Add( "Language" );
//			this.dtLanguage.Columns.Add( "Level" );
//			this.dtLanguage = this.languageAction.SelectWhere( "languagelevel", this.parent );
			
			DispalayTableLanguage();
			#endregion
			PersonalDataChangedValue = false;
		}

		private void CalculatePersonalExperience()
		{
			this.numBoxStartYear.Text = dtAssignment.Rows[0][ "years"].ToString();
			this.numBoxStartMonth.Text = dtAssignment.Rows[0][ "months"].ToString();
			this.numBoxStartDay.Text = dtAssignment.Rows[0][ "days"].ToString();				
					
			DateTime AssignDate = Convert.ToDateTime( this.dtAssignment.Rows[0]["AssignedAt"] );
			//int years = (int)this.dtAssignment.Rows[0]["Years"];
			if( DateTime.Compare( DateTime.Now, AssignDate ) == 1)
			{
				int AssY, AssM, AssD, CYear, CDay, CMonth, TY, TM, TD;
						
				AssY = AssignDate.Year; 
				AssM = AssignDate.Month; 
				AssD = AssignDate.Day;
				CYear = DateTime.Now.Year - AssY;
				if( (CMonth = DateTime.Now.Month - AssM) < 0)
				{							
					CYear--;
				}
				if( (CDay = DateTime.Now.Day - AssD) <= 0)
				{
					CDay += 30;
					CMonth--;
					if(CMonth < 0)
					{
						CMonth += 12;
						CYear--;
					}
				}
				TY = TM = TD = 0;
				TY = CYear + (int)this.dtAssignment.Rows[0]["Years"];
				TM = CMonth + (int)this.dtAssignment.Rows[0]["Months"];
				TD = CDay + (int)this.dtAssignment.Rows[0]["Days"];
				if(TD >= 30)
				{
					TM++;
					TD -= 30;
				}
				if(TM >= 12)
				{
					TM -=12;
					TY++;
				}
				this.numBoxTotalYear.Text = TY.ToString();
				this.numBoxOrgYear.Text = CYear.ToString();
				this.numBoxTotalMonth.Text = TM.ToString();
				this.numBoxOrgMonth.Text = CMonth.ToString();
				this.numBoxTotalDay.Text = TD.ToString();
				this.numBoxOrgDay.Text = CDay.ToString();
			}
			else
			{
				MessageBox.Show( "Проверете датата на компютъра дали е вярна" );
			}						
		}

		private void DispalayTableLanguage()
		{
			IsLoading = true;
			int i = 0;
			foreach( DataRow row in this.dtLanguage.Rows )
			{
				i = this.checkedListBoxLanguage.FindString( (string) row["language"] );
				if(i > 0)
					this.checkedListBoxLanguage.SetItemChecked( i, true );
			}
			IsLoading = false;
		}
		private void LoadNomenklatures()
		{
			int index;
			DataSet ds;
			
			DataLayer.DataAction daa = new DataLayer.DataAction("", this.mainform.connString );			
		
			#region Loading Personal Info nomenklature

			this.comboBoxFamilyStatus.DataSource = this.mainform.nomenclaatureData.arrFamilyStatus;
			this.comboBoxProfesion.DataSource = this.mainform.nomenclaatureData.arrProfession;			
			this.comboBoxScienceLevel.DataSource = this.mainform.nomenclaatureData.arrScienceLevel;
			this.comboBoxScience.DataSource = this.mainform.nomenclaatureData.arrScienceTitle;
			this.comboBoxMilitaryRang.DataSource = this.mainform.nomenclaatureData.arrMilitaryRang;
			this.comboBoxEducation.DataSource = this.mainform.nomenclaatureData.arrEducation;
			this.comboBoxCountry.DataSource = this.mainform.nomenclaatureData.arrCountrys;
			this.comboBoxCategory.DataSource = this.mainform.nomenclaatureData.arrCategory;
			this.comboBoxRegion.DataSource = this.mainform.nomenclaatureData.arrRegion;
			this.comboBoxSex.DataSource = this.mainform.nomenclaatureData.arrSex;
			//this.comboBoxBornTown.DataSource = this.mainform.nomenclaatureData.arrBornRegion;
			ds = daa.SelectFromTable( "language", "level" );
			foreach( DataRow dr in ds.Tables[0].Rows)
			{
				this.checkedListBoxLanguage.Items.Add(dr[0].ToString());
			}
			
			this.dateTimePickerPostypilNa.Value = DateTime.Now;
			index = this.comboBoxCountry.FindString( " БЪЛГАРИЯ",0);
			if( index > -1 )
			 {
				 this.comboBoxCountry.SelectedIndex = index;
			 }
			#endregion

			#region Loading Assignment Info

			//this.comboBoxStaff.DataSource = this.mainform.nomenclaatureData.arrStaff;
			this.comboBoxWorkTime.DataSource = this.mainform.nomenclaatureData.arrWorkTime;
			this.comboBoxContract.DataSource = this.mainform.nomenclaatureData.arrContract;
			this.comboBoxAssignReason.DataSource = this.mainform.nomenclaatureData.arrAssignReason;
			this.comboBoxLaw.DataSource = this.mainform.nomenclaatureData.arrLaw;
			this.comboBoxYearlyAddon.DataSource = this.mainform.nomenclaatureData.arrYearlyAddon;
	
			this.labelLevel1.Text = this.mainform.nomenclaatureData.FirmStructure[0];
			this.labelLevel2.Text = this.mainform.nomenclaatureData.FirmStructure[1];
			this.labelLevel3.Text = this.mainform.nomenclaatureData.FirmStructure[2];
			this.labelLevel4.Text = this.mainform.nomenclaatureData.FirmStructure[3];			

			this.TreeLoad();
			
			this.dtPosition = this.personAction.SelectAll("firmpersonal3");	
			this.dtPosition.PrimaryKey =  new DataColumn[]{this.dtPosition.Columns["ID"]};
			
			this.dateTimePickerAssignedAt.Value = DateTime.Now;
			this.dateTimePickerContractExpiry.Value = DateTime.Now;
			this.dateTimePickerTestPeriod.Value = DateTime.Now;
			this.dateTimePickerContractDate.Value = DateTime.Now;
			#endregion

			#region Loading Absence Info

			//holidayPackage = new DataLayer.HolidayPackage();
			this.dateTimePickerAbsenceFromData.Value = DateTime.Now;
			this.dateTimePickerAbsenceOrderFormData.Value = DateTime.Now;
			this.dateTimePickerAbsenceToData.Value = DateTime.Now;
			#endregion

			#region Loading Penalty Info
			this.comboBoxPenaltyReason.DataSource = this.mainform.nomenclaatureData.arrPenaltyReason;	
			this.comboBoxTypePenalty.DataSource = this.mainform.nomenclaatureData.arrTypePenalty;
            this.dateTimePenaltyFormDate.Value = DateTime.Now;
			this.dateTimePickerPenaltyDate.Value = DateTime.Now;
			this.dateTimePickerPenaltyTo.Value = DateTime.Now;
			
			#endregion

			#region Loading Notes Info
			#endregion
			#region Loading Fired Info
            this.dateTimePickerFiredFromDate.Value = DateTime.Now;
			this.comboBoxFiredReason.DataSource = this.mainform.nomenclaatureData.arrReasonFired;


			#endregion

		}
	
		private void buttonNotes_Click(object sender, System.EventArgs e)
		{
			if( !IsActive )
			{
				
				this.textBoxNotes.ReadOnly = false;
				this.buttonNotes.Text = "Запиши";
				IsActive = true;
			}
			else
			{
				note.UpdateNotes( "Notes", this.parent, this.textBoxNotes.Text );
				this.textBoxNotes.ReadOnly = true;
				this.buttonNotes.Text = "Активирай";
				IsActive = false;
			}
		}

		private void buttonPrintD_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();

			openFileDialog1.InitialDirectory = "" ;
			openFileDialog1.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*" ;
			openFileDialog1.FilterIndex = 1 ;
			openFileDialog1.RestoreDirectory = true ;
			openFileDialog1.Multiselect = false;
			openFileDialog1.Title = "Изберете шаблон за печат";

			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{			
				PrintDoc(openFileDialog1.FileName);				
			}			
		}

		public void PrintDoc(String DocName)
		{
			RichTextBox Rt = new RichTextBox();			
			try
			{
				Rt.LoadFile(DocName);
			}
			catch(System.IO.IOException e)
			{
				MessageBox.Show("Моля затворете шаблонния файл за да можете да разпечатвате");
			}
			Rt.Rtf = Rt.Rtf.Replace("<1>",this.numBoxEgn.Text);
			Rt.Rtf = Rt.Rtf.Replace("<2>",this.textBoxNames.Text);
			Rt.Rtf = Rt.Rtf.Replace("<3>",this.comboBoxCountry.Text);
			Rt.Rtf = Rt.Rtf.Replace("<4>",this.comboBoxCountry.Text);
			Rt.Rtf = Rt.Rtf.Replace("<5>",this.comboBoxRegion.Text);
			Rt.Rtf = Rt.Rtf.Replace("<6>",this.comboBoxNaselenoMqsto.Text);
			Rt.Rtf = Rt.Rtf.Replace("<7>",this.textBoxKwartal.Text);
			Rt.Rtf = Rt.Rtf.Replace("<8>",this.textBoxStreet.Text);
			Rt.Rtf = Rt.Rtf.Replace("<9>",this.textBoxNumBlock.Text);
			Rt.Rtf = Rt.Rtf.Replace("<10>",this.numBoxTelephone.Text);
			Rt.Rtf = Rt.Rtf.Replace("<11>",this.numBoxPcCard.Text);
			Rt.Rtf = Rt.Rtf.Replace("<12>",this.dateTimePickerPCCardPublished.Text);
			Rt.Rtf = Rt.Rtf.Replace("<13>",this.textBoxPublishedFrom.Text);
			Rt.Rtf = Rt.Rtf.Replace("<14>",this.comboBoxFamilyStatus.Text);
			Rt.Rtf = Rt.Rtf.Replace("<15>",this.comboBoxEducation.Text);
			Rt.Rtf = Rt.Rtf.Replace("<16>",this.textBoxDiplom.Text);
			Rt.Rtf = Rt.Rtf.Replace("<17>",this.comboBoxProfesion.Text);
			Rt.Rtf = Rt.Rtf.Replace("<18>","");
			Rt.Rtf = Rt.Rtf.Replace("<19>",this.comboBoxScience.Text);
			Rt.Rtf = Rt.Rtf.Replace("<20>",this.comboBoxScienceLevel.Text);
			Rt.Rtf = Rt.Rtf.Replace("<21>",this.comboBoxMilitaryRang.Text);
			Rt.Rtf = Rt.Rtf.Replace("<22>",this.comboBoxMilitaryStatus.Text);
			//Rt.Rtf = Rt.Rtf.Replace("<23>",this.comboBoxEmployeStatus.Text);
			Rt.Rtf = Rt.Rtf.Replace("<24>",this.comboBoxCategory.Text);
			Rt.Rtf = Rt.Rtf.Replace("<25>",this.dateTimePickerPostypilNa.Text);
			Rt.Rtf = Rt.Rtf.Replace("<26>",this.numBoxAssignmentExpY.Text);
			Rt.Rtf = Rt.Rtf.Replace("<91>",this.numBoxAssignmentExtM.Text);
			Rt.Rtf = Rt.Rtf.Replace("<92>",this.numBoxAssignmentExpD.Text);		

			Rt.Rtf = Rt.Rtf.Replace("<27>",this.comboBoxLevel2.Text);
			Rt.Rtf = Rt.Rtf.Replace("<28>",this.comboBoxLevel3.Text);
			Rt.Rtf = Rt.Rtf.Replace("<29>",this.comboBoxLevel4.Text);
			Rt.Rtf = Rt.Rtf.Replace("<30>",this.comboBoxPosition.Text);
			Rt.Rtf = Rt.Rtf.Replace("<31>",this.comboBoxContract.Text);
			Rt.Rtf = Rt.Rtf.Replace("<32>",this.comboBoxWorkTime.Text);
			Rt.Rtf = Rt.Rtf.Replace("<33>",this.dateTimePickerAssignedAt.Text);
			Rt.Rtf = Rt.Rtf.Replace("<34>",this.comboBoxAssignReason.Text);
			//Rt.Rtf = Rt.Rtf.Replace("<35>",this.comboBoxStaff.Text);
			Rt.Rtf = Rt.Rtf.Replace("<36>",this.textBoxContractNumber.Text);
			Rt.Rtf = Rt.Rtf.Replace("<37>",this.dateTimePickerContractExpiry.Text);
			Rt.Rtf = Rt.Rtf.Replace("<39>",this.numBoxBaseSalary.Text);
			Rt.Rtf = Rt.Rtf.Replace("<40>",this.textBoxSalaryAddon.Text);
			Rt.Rtf = Rt.Rtf.Replace("<41>",this.textBoxClassPercent.Text);
			Rt.Rtf = Rt.Rtf.Replace("<93>",this.dtAssignment.Rows[this.dataGridAssignment.CurrentRowIndex]["ParentContractID"].ToString() );
			Rt.Rtf = Rt.Rtf.Replace("<94>", this.dtAssignment.Rows[this.dataGridAssignment.CurrentRowIndex]["ParentContractDate"].ToString());
			Rt.Rtf = Rt.Rtf.Replace("<95>", this.dtAssignment.Rows[this.dataGridAssignment.CurrentRowIndex]["EKDALevel"].ToString());
			Rt.Rtf = Rt.Rtf.Replace("<96>", this.dtAssignment.Rows[this.dataGridAssignment.CurrentRowIndex]["EKDACode"].ToString());
			Rt.Rtf = Rt.Rtf.Replace("<101>",this.dtAssignment.Rows[this.dataGridAssignment.CurrentRowIndex]["Rang"].ToString() );
			Rt.Rtf = Rt.Rtf.Replace("<97>",this.textBoxNKPLevel.Text);
			Rt.Rtf = Rt.Rtf.Replace("<98>",this.textBoxNKPCode.Text);	
			Rt.Rtf = Rt.Rtf.Replace("<99>", this.numBoxMonthlyAddon.Text);
			Rt.Rtf = Rt.Rtf.Replace("<100>", this.numBoxNumHoliday.Text);
			Rt.Rtf = Rt.Rtf.Replace("<101>",this.dtAssignment.Rows[this.dataGridAssignment.CurrentRowIndex]["Rang"].ToString() );
			Rt.Rtf = Rt.Rtf.Replace("<102>", this.numBoxAssignmentExpY.Text);
			Rt.Rtf = Rt.Rtf.Replace("<103>", this.numBoxAssignmentExtM.Text);
			Rt.Rtf = Rt.Rtf.Replace("<104>", this.numBoxAssignmentExpD.Text);
			Rt.Rtf = Rt.Rtf.Replace("<105>", this.dtAssignment.Rows[this.dataGridAssignment.CurrentRowIndex]["ParentContractId"].ToString() );
			Rt.Rtf = Rt.Rtf.Replace("<106>", this.dateTimePickerContractDate.Text);
			Rt.Rtf = Rt.Rtf.Replace("<107>", this.dateTimePickerTestPeriod.Text);
			
			if(this.comboBoxLevel2.Text != "")
			{
				Rt.Rtf = Rt.Rtf.Replace("<108>", "\\par\r\n в " + this.comboBoxLevel2.Text);
			}
			else if(this.comboBoxLevel1.Text != "")
			{
				Rt.Rtf = Rt.Rtf.Replace("<108>", "\\par\r\n в " + this.comboBoxLevel1.Text);
			}
			else
			{
				Rt.Rtf = Rt.Rtf.Replace("<108>", "");
			}
			if(this.comboBoxLevel3.Text != "")
			{
				Rt.Rtf = Rt.Rtf.Replace("<109>", "\\par\r\n в " + this.comboBoxLevel3.Text);
			}
			else
			{
				Rt.Rtf = Rt.Rtf.Replace("<109>", "");
			}
			if(this.comboBoxLevel4.Text != "")
			{
				Rt.Rtf = Rt.Rtf.Replace("<110>", "\\par\r\n в " + this.comboBoxLevel4.Text);
			}
			else
			{
				Rt.Rtf = Rt.Rtf.Replace("<110>", "");
			}

			Rt.Rtf = Rt.Rtf.Replace("<44>",this.dateTimePickerAbsenceFromData.Text);
			Rt.Rtf = Rt.Rtf.Replace("<45>",this.dateTimePickerAbsenceToData.Text);
			Rt.Rtf = Rt.Rtf.Replace("<46>",this.numBoxAbsenceDays.Text);
			Rt.Rtf = Rt.Rtf.Replace("<47>",this.comboBoxAbsenceTypeAbsence.Text);
			Rt.Rtf = Rt.Rtf.Replace("<48>",this.textBoxAbsenceReason.Text);
			Rt.Rtf = Rt.Rtf.Replace("<49>",this.textBoxAbsenceNumberOrder.Text);
			Rt.Rtf = Rt.Rtf.Replace("<50>",this.dateTimePickerAbsenceOrderFormData.Text);
			Rt.Rtf = Rt.Rtf.Replace("<59>",this.dateTimePickerPenaltyDate.Text);
			Rt.Rtf = Rt.Rtf.Replace("<60>",this.comboBoxPenaltyReason.Text);
			Rt.Rtf = Rt.Rtf.Replace("<61>",this.numBoxPenaltyOrder.Text);
			Rt.Rtf = Rt.Rtf.Replace("<62>",this.dateTimePenaltyFormDate.Text);

			Rt.Rtf = Rt.Rtf.Replace("<63>",this.textBoxNotes.Text);

			DataRow Row = this.mainform.nomenclaatureData.AdminTable.Rows[0];
		
			Rt.Rtf = Rt.Rtf.Replace("<64>", (string) Row["firmname"]  );
			Rt.Rtf = Rt.Rtf.Replace("<65>", (string) Row["type"]);
			Rt.Rtf = Rt.Rtf.Replace("<66>", Row["kind"].ToString());
			Rt.Rtf = Rt.Rtf.Replace("<67>", (string) Row["region"].ToString());
			Rt.Rtf = Rt.Rtf.Replace("<68>", (string) Row["town"]);
			Rt.Rtf = Rt.Rtf.Replace("<69>", (string) Row["postalcode"]);
			Rt.Rtf = Rt.Rtf.Replace("<70>", (string) Row["addressdata"]);
			Rt.Rtf = Rt.Rtf.Replace("<71>", (string) Row["email"]);
			Rt.Rtf = Rt.Rtf.Replace("<72>", (string) Row["phone"]); 
			Rt.Rtf = Rt.Rtf.Replace("<73>", Row["nominalemployees"].ToString());
			Rt.Rtf = Rt.Rtf.Replace("<74>", (string) Row["securenumber"]);
			Rt.Rtf = Rt.Rtf.Replace("<75>", (string) Row["directorname"]);
			Rt.Rtf = Rt.Rtf.Replace("<76>", (string) Row["egndirector"]);
			Rt.Rtf = Rt.Rtf.Replace("<77>", (string) Row["directorlsys"]);
			Rt.Rtf = Rt.Rtf.Replace("<78>", (string) Row["egndirectorlsys"]);
			Rt.Rtf = Rt.Rtf.Replace("<79>", (string) Row["mainaccountantname"]);
			Rt.Rtf = Rt.Rtf.Replace("<80>", (string) Row["egnmainaccountant"]);
			Rt.Rtf = Rt.Rtf.Replace("<81>", (string) Row["mainconsult"]);
			Rt.Rtf = Rt.Rtf.Replace("<82>", (string) Row["egnmainconsult"]);
			Rt.Rtf = Rt.Rtf.Replace("<83>", (string) Row["trz"]);
			Rt.Rtf = Rt.Rtf.Replace("<84>", (string) Row["egntrz"]);
			Rt.Rtf = Rt.Rtf.Replace("<85>", (string) Row["bankname"]);
			Rt.Rtf = Rt.Rtf.Replace("<86>", (string) Row["bankaccount"]);
			Rt.Rtf = Rt.Rtf.Replace("<87>", (string) Row["bankcode"]);
			Rt.Rtf = Rt.Rtf.Replace("<88>", System.DateTime.Now.Date.ToString() );
			Rt.Rtf = Rt.Rtf.Replace("<89>", (string) Row["bulstat"]);
			Rt.Rtf = Rt.Rtf.Replace("<90>", (string) Row["taxNum"]);

			int i = 1;
			while(true)
			{				
				string Filename;
				try
				{
					Filename = "Document" + i.ToString() + ".doc";
					Rt.SaveFile(Filename);
					System.Diagnostics.Process.Start("winword.exe", Filename);
					break;
				}
				catch(System.IO.IOException)
				{
					i++;
					if(i > 255)
					{
						MessageBox.Show("Моля затворете шаблонния файл за да можете да разпечатвате");
					}
				}
			}
		}
		
		private void EnableTabs(bool IsEnabled)
		{
			for(int i = 0; i < this.tabControl1.TabPages.Count; i++ )
			{
				if(i != this.tabControl1.SelectedIndex)
				{
					this.tabControl1.TabPages[i].Enabled = IsEnabled;
				}
			}				
		}
	
		private void JustifyGrid(DataGrid grid)
		{
			Graphics Graphics = grid.CreateGraphics();
			DataGridTableStyle ts = new DataGridTableStyle();
			try
			{	
				DataTable dataTable;
				try
				{
					dataTable = (DataTable)grid.DataSource;
				}
				catch(System.InvalidCastException)
				{
					DataView data = (DataView) grid.DataSource;
					dataTable = data.Table;
				}
                				
				int	nRowsToScan = dataTable.Rows.Count;
				
				// Clear any existing table styles.
				grid.TableStyles.Clear();

				// Use mapping name that is defined in the data source.
				ts.MappingName = dataTable.TableName;

				// Now create the column styles within the table style.
				DataGridTextBoxColumn columnStyle;
				int iWidth;

				for (int iCurrCol = 0; iCurrCol < dataTable.Columns.Count; iCurrCol++)
				{
					DataColumn dataColumn = dataTable.Columns[iCurrCol];

					columnStyle = new DataGridTextBoxColumn();

					columnStyle.TextBox.Enabled = false;
	
					switch( dataTable.TableName)
					{
						case "personassignment":
						{
							switch( dataColumn.ColumnName)
							{										
								case "level1":
								{
									columnStyle.HeaderText = this.mainform.nomenclaatureData.FirmStructure[0];
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}
								case "level2":
								{
									columnStyle.HeaderText = this.mainform.nomenclaatureData.FirmStructure[1]; 
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}
								case "level3":
								{
									columnStyle.HeaderText = this.mainform.nomenclaatureData.FirmStructure[2]; 
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}
								case "level4":
								{
									columnStyle.HeaderText = this.mainform.nomenclaatureData.FirmStructure[3]; 
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}
								case "position":
								{
									columnStyle.HeaderText = "Длъжност"; 
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}
								case "contract":
								{
									columnStyle.HeaderText = "Договор"; 
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}
								case "worktime":
								{
									columnStyle.HeaderText = "Работно време"; 
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}
								case "assignedAt":
								{
									columnStyle.HeaderText = "Назначен на"; 
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}
								case "staff":
								{
									columnStyle.HeaderText = "Щат"; 
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}								
								default :
								{
									columnStyle.HeaderText = dataColumn.ColumnName; 
									columnStyle.MappingName = dataColumn.ColumnName;
									columnStyle.Width = 0; //скрива колоната

									// Add the new column style to the table style.
									ts.GridColumnStyles.Add(columnStyle);
									continue;
								}
							}
							break;
						}
						case "penalty":
						{
							switch( dataColumn.ColumnName)
							{
								case "typePenalty":
								{
									columnStyle.HeaderText = "Валидно от";
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}
								case "penaltyDatefrom":
								{
									columnStyle.HeaderText = "Валидно от";
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}
								case "dateTo":
								{
									columnStyle.HeaderText = "Валидно до";
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}
								case "reason":
								{
									columnStyle.HeaderText = "Причина"; 
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}
								case "numberOrder":
								{
									columnStyle.HeaderText = "Номер на заповед"; 
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}
								case "fromDate":
								{
									columnStyle.HeaderText = "Дата на постановлението"; 
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}
								default :
								{
									columnStyle.HeaderText = dataColumn.ColumnName; 
									columnStyle.MappingName = dataColumn.ColumnName;
									columnStyle.Width = 0; // скрива колоната

									// Add the new column style to the table style.
									ts.GridColumnStyles.Add(columnStyle);
									continue;
								}
							}							
							break;
						}

						case "year_holiday":
						{
							switch( dataColumn.ColumnName)
							{
								case "year":
								{
									columnStyle.HeaderText = "Година";
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}
								case "leftover":
								{
									columnStyle.HeaderText = "Остатък";
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}
								case "total":
								{
									columnStyle.HeaderText = "Полагаем";
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}
								default :
								{
									columnStyle.HeaderText = dataColumn.ColumnName; 
									columnStyle.MappingName = dataColumn.ColumnName;
									columnStyle.Width = 0; // скрива колоната

									// Add the new column style to the table style.
									ts.GridColumnStyles.Add(columnStyle);
									continue;
								}
							}							
							break;
						}

						case "Fired":
						{
							switch( dataColumn.ColumnName)
							{
								case "FromDate":
								{
									columnStyle.HeaderText = "От дата";
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}
								case "reason":
								{
									columnStyle.HeaderText = "Основание";
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}
								case "Compensation":
								{
									columnStyle.HeaderText = "Обезщетение";
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}
								case "CompensationWork":
								{
									columnStyle.HeaderText = "Обезщетение за оставане без работа";
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}
								case "CompensationTime":
								{
									columnStyle.HeaderText = "Обезщетение за ненавременно предупредение";
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}
								case "NumberSalary":
								{
									columnStyle.HeaderText = "Брой изплатени заплати";
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}								
								default :
								{
									columnStyle.HeaderText = dataColumn.ColumnName; 
									columnStyle.MappingName = dataColumn.ColumnName;
									columnStyle.Width = 0; // скрива колоната

									// Add the new column style to the table style.
									ts.GridColumnStyles.Add(columnStyle);
									continue;
								}
							}
							break;
						}

						case "absence":
						{
							switch( dataColumn.ColumnName)
							{
								case "fromDate":
								{
									columnStyle.HeaderText = "От дата";
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}
								case "toDate":
								{
									columnStyle.HeaderText = "До дата"; 
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}
								case "countDays":
								{
									columnStyle.HeaderText = "Брой дни"; 
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}
								case "typeAbsence":
								{
									columnStyle.HeaderText = "Вид отсъствие"; 
									columnStyle.MappingName = dataColumn.ColumnName;
									break;
								}
								default :
								{
									columnStyle.HeaderText = dataColumn.ColumnName; 
									columnStyle.MappingName = dataColumn.ColumnName;
									columnStyle.Width = 0; // скрива колоната

									// Add the new column style to the table style.
									ts.GridColumnStyles.Add(columnStyle);
									continue;
								}
							}							
							break;
						}
					}

					// Set width to header text width.
					iWidth = (int)(Graphics.MeasureString
						(columnStyle.HeaderText,
						grid.Font).Width);

					// Change width, if data width is
					// wider than header text width.
					// Check the width of the data in the first X rows.
					DataRow dataRow;
					for (int iRow = 0; iRow < nRowsToScan; iRow++)
					{
						dataRow = dataTable.Rows[iRow];

						if (null != dataRow[dataColumn.ColumnName])
						{
							int iColWidth = (int)(Graphics.MeasureString
								(dataRow.ItemArray[iCurrCol].ToString(),
								grid.Font).Width);
							iWidth = (int)System.Math.Max(iWidth, iColWidth);
						}
					}
					columnStyle.Width = iWidth + 4;

					// Add the new column style to the table style.
					ts.GridColumnStyles.Add(columnStyle);
				}
				// Add the new table style to the data grid.
				grid.TableStyles.Add(ts);
			}
			catch(System.Exception e)
			{
				MessageBox.Show(e.Message, "Some Error");
			}

			finally
			{
				Graphics.Dispose();
			}
		}		
		private void tabControl1_SelectedIndexChanging(object sender, NewTabControl.TabPageChangeEventArgs e)
		{
			if(e.NextTab.Enabled == false)
				MessageBox.Show("Не може да сменяте страницата на досието по време на редакция.");
		}		
		#endregion				
	}
}
