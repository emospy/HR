using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;

namespace LichenSystaw2004
{
	/// <summary>
	/// Summary description for AddNewPerson.
	/// </summary>
	public class formPersonalData : System.Windows.Forms.Form
	{
		#region Control_List

		DataLayer.AssignmentAction assignmentAction;
		bool IsAssignmentLoadForm = false;
		bool IsAssignmentEdit = false;
		bool IsAssignment = true;
		DataTable dtAssignment = new DataTable();

		DataLayer.AbsenceAction absenceAction;
		bool IsAbsenceLoadForm = false;
		bool IsAbsenceEdit = false;
		DataTable dtAbsence = new DataTable();

		bool IsPenaltyLoadForm = false;
		bool IsPenaltyEdit = false;
		DataTable dtPenalty = new DataTable();

		DataTable dtNotes = new DataTable();
		bool IsActive = false;
		DataLayer.NoteAction note;

		DataLayer.PersonalAction personAction;
		DataLayer.AssignmentAction assAction;
		DataLayer.PenaltyAction penaltyAction;

		System.Random rand1= new Random( System.DateTime.Now.Day + 
			System.DateTime.Now.DayOfYear + System.DateTime.Now.Year + System.DateTime.Now.Second/10 );
       System.Random rand;

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxNames;
		private System.Windows.Forms.Label labelNames;
		private System.Windows.Forms.ComboBox comboBoxLive;
		private System.Windows.Forms.Label labelLive;
		private System.Windows.Forms.Button buttonОК;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.ComboBox comboBoxCountry;
		private System.Windows.Forms.Label labelCountry;
		private System.Windows.Forms.Label labelRegion;
		private System.Windows.Forms.ComboBox comboBoxRegion;
		private System.Windows.Forms.Label labelNaselenoMqsto;
		private System.Windows.Forms.Label labelSex;
		private System.Windows.Forms.Label labelFamilyStatus;
		private System.Windows.Forms.ComboBox comboBoxFamilyStatus;
		private System.Windows.Forms.Label labelEducation;
		private System.Windows.Forms.ComboBox comboBoxEducation;
		private System.Windows.Forms.TextBox textBoxDiplom;
		private System.Windows.Forms.Label labelDiplom;
		private System.Windows.Forms.Label labelProfesion;
		private System.Windows.Forms.ComboBox comboBoxProfesion;
		private System.Windows.Forms.CheckedListBox checkedListBoxLanguage;
		private System.Windows.Forms.Label labelLanguage;
		private System.Windows.Forms.Label labellanguageLevel;
		private System.Windows.Forms.CheckedListBox checkedListBoxLangLevel;
		private System.Windows.Forms.Label labelScience;
		private System.Windows.Forms.ComboBox comboBoxScience;
		private System.Windows.Forms.Label labelScienceLevel;
		private System.Windows.Forms.ComboBox comboBoxScienceLevel;
		private System.Windows.Forms.Label labelMilitaryStatus;
		private System.Windows.Forms.ComboBox comboBoxMilitaryStatus;
		private System.Windows.Forms.Label labelMilitaryRang;
		private System.Windows.Forms.ComboBox comboBoxMilitaryRang;
		private System.Windows.Forms.Label labelEmployeStatus;
		private System.Windows.Forms.ComboBox comboBoxEmployeStatus;
		private System.Windows.Forms.Label labelCategory;
		private System.Windows.Forms.ComboBox comboBoxCategory;
		private System.Windows.Forms.Label labelHiredAt;
		private System.Windows.Forms.Label labelWorkExperiance;
		private System.Windows.Forms.TextBox textBoxWorkExpiriance;
		private ComboBoxIntelisense.InteliCombo comboBoxNaselenoMqsto;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage TabPersonalInfo;
		private System.Windows.Forms.TextBox textBoxKwartal;
		private System.Windows.Forms.Label labelKwartal;
		private System.Windows.Forms.Label labelStreet;
		private System.Windows.Forms.TextBox textBoxStreet;
		private System.Windows.Forms.Label labelNumBlock;
		private System.Windows.Forms.TextBox textBoxNumBlock;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label labelJKkwartal;
		private System.Windows.Forms.Label labelPublishedBy;
		private System.Windows.Forms.TextBox textBoxPublishedFrom;
		private BugBox.NumBox numBoxPcCard;
		private System.Windows.Forms.Label labelPublishedByy;
		private mainForm mainform;
		private string personName;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.TabPage tabPageAssignment;
		private System.Windows.Forms.DateTimePicker dateTimePickerAssignedAt;
		private System.Windows.Forms.TextBox textBoxNKIDCode;
		private System.Windows.Forms.TextBox textBoxSalaryAddon;
		private System.Windows.Forms.TextBox textBoxContractNumber;
		private System.Windows.Forms.TextBox textBoxNKIDName;
		private System.Windows.Forms.TextBox textBoxClassPercent;
		private System.Windows.Forms.ComboBox comboBoxStaff;
		private System.Windows.Forms.ComboBox comboBoxAssignReason;
		private System.Windows.Forms.ComboBox comboBoxWorkTime;
		private System.Windows.Forms.ComboBox comboBoxContract;
		private System.Windows.Forms.ComboBox comboBoxPosition;
		private System.Windows.Forms.ComboBox comboBoxLevel3;
		private System.Windows.Forms.ComboBox comboBoxLevel2;
		private System.Windows.Forms.ComboBox comboBoxLevel1;
		private System.Windows.Forms.Button buttonAssignment;
		private System.Windows.Forms.RadioButton radioButtonAssignment;
		private System.Windows.Forms.RadioButton radioButtonAdditional;
		private int egn;
		private System.Windows.Forms.DataGrid dataGridAssignment;
		private System.Windows.Forms.TabPage tabPageAbsence;
		private System.Windows.Forms.TabPage tabPagePenalty;
		private System.Windows.Forms.TabPage tabPageNotes;
		private System.Windows.Forms.TextBox textBoxNotes;
		private System.Windows.Forms.Button buttonNotes;
		private System.Windows.Forms.GroupBox groupBoxPenalty;
		private System.Windows.Forms.DateTimePicker dateTimePickerPenaltyDate;
		private System.Windows.Forms.Label labelPenalty;
		private System.Windows.Forms.TextBox textBoxPenaltyReason;
		private System.Windows.Forms.Label labelPenaltyReason;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label21;
		private BugBox.NumBox numBoxPenaltyOrder;
		private System.Windows.Forms.DateTimePicker dateTimePenaltyFormDate;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBoxAbsece;
		private System.Windows.Forms.GroupBox groupBoxHoliday;
		private System.Windows.Forms.GroupBox groupBoxAbsence;
		private System.Windows.Forms.DateTimePicker dateTimePickerAbsenceFromData;
		private System.Windows.Forms.DateTimePicker dateTimePickerAbsenceToData;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label23;
		private BugBox.NumBox numBoxAbsenceDays;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.TextBox textBoxAbsenceReason;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.TextBox textBoxAbsenceNumberOrder;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.DateTimePicker dateTimePickerAbsenceOrderFormData;
		private BugBox.NumBox numBoxAbsenceCurrentYearPlan;
		private BugBox.NumBox numBoxAbsenceCurrentYearUsed;
		private BugBox.NumBox numBoxAbsenceCurrentYearRest;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.Label label32;
		private BugBox.NumBox numBoxAbsenceUnpayedHoliday;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.TabPage tabPageAditionalInfo;
		private BugBox.BugBox numBoxEgn;
		private BugBox.NumBox numBoxTelephone;
		private System.Windows.Forms.Button buttonPenaltyAdd;
		private System.Windows.Forms.Button buttonPebaltyEdit;
		private System.Windows.Forms.Button buttonPenaltySave;
		private System.Windows.Forms.Button buttonPenaltyDelete;
		private System.Windows.Forms.Button buttonAbsenceDelete;
		private System.Windows.Forms.Button buttonAbsenceSave;
		private System.Windows.Forms.Button buttonAbsenceEdit;
		private System.Windows.Forms.Button buttonAbsenceAdd;
		private System.Windows.Forms.DataGrid dataGridPenalty;
		private BugBox.NumBox numBoxAbsenceLastYearRest;
		private BugBox.NumBox numBoxAbsenceLastYearUsed;
		private BugBox.NumBox numBoxAbsenceLastYearPlan;
		private System.Windows.Forms.DataGrid dataGridAbsence;
		private System.Windows.Forms.ComboBox comboBoxAbsenceTypeAbsence;
		private System.Windows.Forms.Button buttonAssignmentDelete;
		private System.Windows.Forms.Button buttonAssignmentSave;
		private System.Windows.Forms.Button buttonAssignmentEdit;
		private System.Windows.Forms.Label labelPart;
		private System.Windows.Forms.Label labelControl;
		private System.Windows.Forms.Label labelDirection;
		private BugBox.NumBox numBoxNumberKids;
		private BugBox.NumBox numBoxBaseSalary;
		private System.Windows.Forms.DateTimePicker dateTimePickerContractExpiry;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.DateTimePicker dateTimePickerPostypilNa;
		private System.Windows.Forms.DateTimePicker dateTimePickerPCCardPublished;
		private System.Windows.Forms.Button buttonPrintD;
		private int ID;
		#endregion

		public formPersonalData(  string personName, int egn, mainForm main)
		{
			this.mainform = main;
			this.personName = personName;
			this.egn = egn;

			this.personAction = new DataLayer.PersonalAction("FirmPersonal", this.mainform.connString );
			this.note = new DataLayer.NoteAction( this.mainform.connString );
			this.penaltyAction = new DataLayer.PenaltyAction( "Penalty", this.mainform.connString );
			this.absenceAction  = new DataLayer.AbsenceAction( "Absence", this.mainform.connString );

			InitializeComponent();
			this.comboBoxLive.DropDownStyle = ComboBoxStyle.DropDownList; 
			this.buttonОК.Click += new EventHandler(buttonОК_Click);
			this.buttonCancel.Click += new EventHandler(buttonCancel_Click);

			for( int i = 0; i < rand1.Next( 150 ); i++)
			{
				rand1.Next();
			}
			this.rand = new Random( rand1.Next() );


		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.textBoxNames = new System.Windows.Forms.TextBox();
			this.labelNames = new System.Windows.Forms.Label();
			this.comboBoxLive = new System.Windows.Forms.ComboBox();
			this.labelLive = new System.Windows.Forms.Label();
			this.buttonОК = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.comboBoxFamilyStatus = new System.Windows.Forms.ComboBox();
			this.comboBoxEducation = new System.Windows.Forms.ComboBox();
			this.textBoxDiplom = new System.Windows.Forms.TextBox();
			this.comboBoxProfesion = new System.Windows.Forms.ComboBox();
			this.comboBoxScienceLevel = new System.Windows.Forms.ComboBox();
			this.comboBoxMilitaryStatus = new System.Windows.Forms.ComboBox();
			this.comboBoxEmployeStatus = new System.Windows.Forms.ComboBox();
			this.comboBoxCategory = new System.Windows.Forms.ComboBox();
			this.numBoxEgn = new BugBox.BugBox();
			this.comboBoxCountry = new System.Windows.Forms.ComboBox();
			this.labelCountry = new System.Windows.Forms.Label();
			this.labelRegion = new System.Windows.Forms.Label();
			this.comboBoxRegion = new System.Windows.Forms.ComboBox();
			this.labelNaselenoMqsto = new System.Windows.Forms.Label();
			this.labelSex = new System.Windows.Forms.Label();
			this.labelFamilyStatus = new System.Windows.Forms.Label();
			this.labelEducation = new System.Windows.Forms.Label();
			this.labelDiplom = new System.Windows.Forms.Label();
			this.labelProfesion = new System.Windows.Forms.Label();
			this.checkedListBoxLanguage = new System.Windows.Forms.CheckedListBox();
			this.labelLanguage = new System.Windows.Forms.Label();
			this.labellanguageLevel = new System.Windows.Forms.Label();
			this.checkedListBoxLangLevel = new System.Windows.Forms.CheckedListBox();
			this.labelScience = new System.Windows.Forms.Label();
			this.comboBoxScience = new System.Windows.Forms.ComboBox();
			this.labelScienceLevel = new System.Windows.Forms.Label();
			this.labelMilitaryStatus = new System.Windows.Forms.Label();
			this.labelMilitaryRang = new System.Windows.Forms.Label();
			this.comboBoxMilitaryRang = new System.Windows.Forms.ComboBox();
			this.labelEmployeStatus = new System.Windows.Forms.Label();
			this.labelCategory = new System.Windows.Forms.Label();
			this.dateTimePickerPostypilNa = new System.Windows.Forms.DateTimePicker();
			this.labelHiredAt = new System.Windows.Forms.Label();
			this.labelWorkExperiance = new System.Windows.Forms.Label();
			this.textBoxWorkExpiriance = new System.Windows.Forms.TextBox();
			this.comboBoxNaselenoMqsto = new ComboBoxIntelisense.InteliCombo();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.TabPersonalInfo = new System.Windows.Forms.TabPage();
			this.dateTimePickerPCCardPublished = new System.Windows.Forms.DateTimePicker();
			this.labelPublishedByy = new System.Windows.Forms.Label();
			this.numBoxPcCard = new BugBox.NumBox();
			this.labelPublishedBy = new System.Windows.Forms.Label();
			this.textBoxPublishedFrom = new System.Windows.Forms.TextBox();
			this.labelJKkwartal = new System.Windows.Forms.Label();
			this.numBoxTelephone = new BugBox.NumBox();
			this.label3 = new System.Windows.Forms.Label();
			this.labelNumBlock = new System.Windows.Forms.Label();
			this.textBoxNumBlock = new System.Windows.Forms.TextBox();
			this.labelStreet = new System.Windows.Forms.Label();
			this.textBoxStreet = new System.Windows.Forms.TextBox();
			this.labelKwartal = new System.Windows.Forms.Label();
			this.textBoxKwartal = new System.Windows.Forms.TextBox();
			this.tabPageAditionalInfo = new System.Windows.Forms.TabPage();
			this.tabPageAssignment = new System.Windows.Forms.TabPage();
			this.dateTimePickerContractExpiry = new System.Windows.Forms.DateTimePicker();
			this.numBoxBaseSalary = new BugBox.NumBox();
			this.numBoxNumberKids = new BugBox.NumBox();
			this.buttonAssignmentDelete = new System.Windows.Forms.Button();
			this.buttonAssignmentSave = new System.Windows.Forms.Button();
			this.buttonAssignmentEdit = new System.Windows.Forms.Button();
			this.radioButtonAdditional = new System.Windows.Forms.RadioButton();
			this.radioButtonAssignment = new System.Windows.Forms.RadioButton();
			this.buttonAssignment = new System.Windows.Forms.Button();
			this.dateTimePickerAssignedAt = new System.Windows.Forms.DateTimePicker();
			this.label19 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.labelPart = new System.Windows.Forms.Label();
			this.labelControl = new System.Windows.Forms.Label();
			this.labelDirection = new System.Windows.Forms.Label();
			this.dataGridAssignment = new System.Windows.Forms.DataGrid();
			this.textBoxNKIDCode = new System.Windows.Forms.TextBox();
			this.textBoxSalaryAddon = new System.Windows.Forms.TextBox();
			this.textBoxContractNumber = new System.Windows.Forms.TextBox();
			this.textBoxNKIDName = new System.Windows.Forms.TextBox();
			this.textBoxClassPercent = new System.Windows.Forms.TextBox();
			this.comboBoxStaff = new System.Windows.Forms.ComboBox();
			this.comboBoxAssignReason = new System.Windows.Forms.ComboBox();
			this.comboBoxWorkTime = new System.Windows.Forms.ComboBox();
			this.comboBoxContract = new System.Windows.Forms.ComboBox();
			this.comboBoxPosition = new System.Windows.Forms.ComboBox();
			this.comboBoxLevel3 = new System.Windows.Forms.ComboBox();
			this.comboBoxLevel2 = new System.Windows.Forms.ComboBox();
			this.comboBoxLevel1 = new System.Windows.Forms.ComboBox();
			this.tabPageAbsence = new System.Windows.Forms.TabPage();
			this.buttonAbsenceDelete = new System.Windows.Forms.Button();
			this.buttonAbsenceSave = new System.Windows.Forms.Button();
			this.buttonAbsenceEdit = new System.Windows.Forms.Button();
			this.buttonAbsenceAdd = new System.Windows.Forms.Button();
			this.groupBoxAbsence = new System.Windows.Forms.GroupBox();
			this.dataGridAbsence = new System.Windows.Forms.DataGrid();
			this.groupBoxHoliday = new System.Windows.Forms.GroupBox();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label35 = new System.Windows.Forms.Label();
			this.label34 = new System.Windows.Forms.Label();
			this.label33 = new System.Windows.Forms.Label();
			this.label32 = new System.Windows.Forms.Label();
			this.numBoxAbsenceUnpayedHoliday = new BugBox.NumBox();
			this.label31 = new System.Windows.Forms.Label();
			this.label30 = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.numBoxAbsenceCurrentYearRest = new BugBox.NumBox();
			this.numBoxAbsenceCurrentYearUsed = new BugBox.NumBox();
			this.numBoxAbsenceCurrentYearPlan = new BugBox.NumBox();
			this.numBoxAbsenceLastYearRest = new BugBox.NumBox();
			this.numBoxAbsenceLastYearUsed = new BugBox.NumBox();
			this.numBoxAbsenceLastYearPlan = new BugBox.NumBox();
			this.groupBoxAbsece = new System.Windows.Forms.GroupBox();
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
			this.buttonPenaltyDelete = new System.Windows.Forms.Button();
			this.buttonPenaltySave = new System.Windows.Forms.Button();
			this.buttonPebaltyEdit = new System.Windows.Forms.Button();
			this.buttonPenaltyAdd = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.dataGridPenalty = new System.Windows.Forms.DataGrid();
			this.groupBoxPenalty = new System.Windows.Forms.GroupBox();
			this.dateTimePenaltyFormDate = new System.Windows.Forms.DateTimePicker();
			this.numBoxPenaltyOrder = new BugBox.NumBox();
			this.label21 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.labelPenaltyReason = new System.Windows.Forms.Label();
			this.textBoxPenaltyReason = new System.Windows.Forms.TextBox();
			this.labelPenalty = new System.Windows.Forms.Label();
			this.dateTimePickerPenaltyDate = new System.Windows.Forms.DateTimePicker();
			this.tabPageNotes = new System.Windows.Forms.TabPage();
			this.buttonNotes = new System.Windows.Forms.Button();
			this.textBoxNotes = new System.Windows.Forms.TextBox();
			this.buttonPrintD = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.TabPersonalInfo.SuspendLayout();
			this.tabPageAditionalInfo.SuspendLayout();
			this.tabPageAssignment.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridAssignment)).BeginInit();
			this.tabPageAbsence.SuspendLayout();
			this.groupBoxAbsence.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridAbsence)).BeginInit();
			this.groupBoxHoliday.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.groupBoxAbsece.SuspendLayout();
			this.tabPagePenalty.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridPenalty)).BeginInit();
			this.groupBoxPenalty.SuspendLayout();
			this.tabPageNotes.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "ЕГН";
			// 
			// textBoxNames
			// 
			this.textBoxNames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxNames.Location = new System.Drawing.Point(128, 24);
			this.textBoxNames.Name = "textBoxNames";
			this.textBoxNames.Size = new System.Drawing.Size(368, 20);
			this.textBoxNames.TabIndex = 2;
			this.textBoxNames.Text = "";
			// 
			// labelNames
			// 
			this.labelNames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.labelNames.Location = new System.Drawing.Point(128, 8);
			this.labelNames.Name = "labelNames";
			this.labelNames.Size = new System.Drawing.Size(232, 23);
			this.labelNames.TabIndex = 3;
			this.labelNames.Text = "Трите имена на лицето";
			// 
			// comboBoxLive
			// 
			this.comboBoxLive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxLive.Items.AddRange(new object[] {
															  "България"});
			this.comboBoxLive.Location = new System.Drawing.Point(8, 64);
			this.comboBoxLive.Name = "comboBoxLive";
			this.comboBoxLive.Size = new System.Drawing.Size(384, 21);
			this.comboBoxLive.TabIndex = 4;
			this.comboBoxLive.Text = "Изберете";
			// 
			// labelLive
			// 
			this.labelLive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.labelLive.Location = new System.Drawing.Point(8, 48);
			this.labelLive.Name = "labelLive";
			this.labelLive.Size = new System.Drawing.Size(384, 23);
			this.labelLive.TabIndex = 5;
			this.labelLive.Text = "Месторождение Държава";
			// 
			// buttonОК
			// 
			this.buttonОК.Location = new System.Drawing.Point(240, 368);
			this.buttonОК.Name = "buttonОК";
			this.buttonОК.TabIndex = 6;
			this.buttonОК.Text = "ОК";
			this.buttonОК.Click += new System.EventHandler(this.buttonОК_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(328, 368);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 7;
			this.buttonCancel.Text = "Откажи";
			// 
			// comboBoxFamilyStatus
			// 
			this.comboBoxFamilyStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxFamilyStatus.Items.AddRange(new object[] {
																	  "Семеен",
																	  "Несеен",
																	  "Разведен",
																	  "Вдовец",
																	  "Непоказано"});
			this.comboBoxFamilyStatus.Location = new System.Drawing.Point(8, 32);
			this.comboBoxFamilyStatus.Name = "comboBoxFamilyStatus";
			this.comboBoxFamilyStatus.Size = new System.Drawing.Size(121, 21);
			this.comboBoxFamilyStatus.TabIndex = 17;
			// 
			// comboBoxEducation
			// 
			this.comboBoxEducation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxEducation.Items.AddRange(new object[] {
																   "Висше",
																   "Начално",
																   "Непоказано",
																   "Основно",
																   "Полувисше",
																   "Средно",
																   "Средноспециално",
																   "Среднотехническо"});
			this.comboBoxEducation.Location = new System.Drawing.Point(128, 32);
			this.comboBoxEducation.Name = "comboBoxEducation";
			this.comboBoxEducation.Size = new System.Drawing.Size(121, 21);
			this.comboBoxEducation.Sorted = true;
			this.comboBoxEducation.TabIndex = 19;
			// 
			// textBoxDiplom
			// 
			this.textBoxDiplom.Location = new System.Drawing.Point(248, 32);
			this.textBoxDiplom.Name = "textBoxDiplom";
			this.textBoxDiplom.TabIndex = 21;
			this.textBoxDiplom.Text = "";
			// 
			// comboBoxProfesion
			// 
			this.comboBoxProfesion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxProfesion.Location = new System.Drawing.Point(8, 72);
			this.comboBoxProfesion.Name = "comboBoxProfesion";
			this.comboBoxProfesion.Size = new System.Drawing.Size(121, 21);
			this.comboBoxProfesion.TabIndex = 23;
			// 
			// comboBoxScienceLevel
			// 
			this.comboBoxScienceLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxScienceLevel.Location = new System.Drawing.Point(136, 112);
			this.comboBoxScienceLevel.Name = "comboBoxScienceLevel";
			this.comboBoxScienceLevel.Size = new System.Drawing.Size(121, 21);
			this.comboBoxScienceLevel.TabIndex = 31;
			// 
			// comboBoxMilitaryStatus
			// 
			this.comboBoxMilitaryStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMilitaryStatus.Items.AddRange(new object[] {
																		"Отслужил",
																		"Неотслужил"});
			this.comboBoxMilitaryStatus.Location = new System.Drawing.Point(8, 160);
			this.comboBoxMilitaryStatus.Name = "comboBoxMilitaryStatus";
			this.comboBoxMilitaryStatus.Size = new System.Drawing.Size(121, 21);
			this.comboBoxMilitaryStatus.TabIndex = 35;
			// 
			// comboBoxEmployeStatus
			// 
			this.comboBoxEmployeStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxEmployeStatus.Items.AddRange(new object[] {
																	   "Работи",
																	   "Не работи"});
			this.comboBoxEmployeStatus.Location = new System.Drawing.Point(136, 160);
			this.comboBoxEmployeStatus.Name = "comboBoxEmployeStatus";
			this.comboBoxEmployeStatus.Size = new System.Drawing.Size(121, 21);
			this.comboBoxEmployeStatus.TabIndex = 37;
			// 
			// comboBoxCategory
			// 
			this.comboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxCategory.Location = new System.Drawing.Point(264, 160);
			this.comboBoxCategory.Name = "comboBoxCategory";
			this.comboBoxCategory.Size = new System.Drawing.Size(121, 21);
			this.comboBoxCategory.TabIndex = 39;
			// 
			// numBoxEgn
			// 
			this.numBoxEgn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.numBoxEgn.Location = new System.Drawing.Point(8, 24);
			this.numBoxEgn.Name = "numBoxEgn";
			this.numBoxEgn.OnlyInteger = false;
			this.numBoxEgn.OnlyPositive = false;
			this.numBoxEgn.Size = new System.Drawing.Size(104, 20);
			this.numBoxEgn.TabIndex = 46;
			this.numBoxEgn.Text = "";
			// 
			// comboBoxCountry
			// 
			this.comboBoxCountry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxCountry.Location = new System.Drawing.Point(160, 64);
			this.comboBoxCountry.Name = "comboBoxCountry";
			this.comboBoxCountry.Size = new System.Drawing.Size(392, 21);
			this.comboBoxCountry.TabIndex = 8;
			// 
			// labelCountry
			// 
			this.labelCountry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.labelCountry.Location = new System.Drawing.Point(152, 48);
			this.labelCountry.Name = "labelCountry";
			this.labelCountry.Size = new System.Drawing.Size(332, 16);
			this.labelCountry.TabIndex = 9;
			this.labelCountry.Text = "Националност";
			// 
			// labelRegion
			// 
			this.labelRegion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.labelRegion.Location = new System.Drawing.Point(8, 88);
			this.labelRegion.Name = "labelRegion";
			this.labelRegion.Size = new System.Drawing.Size(332, 16);
			this.labelRegion.TabIndex = 11;
			this.labelRegion.Text = "Област";
			// 
			// comboBoxRegion
			// 
			this.comboBoxRegion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxRegion.Location = new System.Drawing.Point(8, 104);
			this.comboBoxRegion.Name = "comboBoxRegion";
			this.comboBoxRegion.Size = new System.Drawing.Size(384, 21);
			this.comboBoxRegion.TabIndex = 10;
			this.comboBoxRegion.SelectedIndexChanged += new System.EventHandler(this.comboBoxRegion_SelectedIndexChanged);
			// 
			// labelNaselenoMqsto
			// 
			this.labelNaselenoMqsto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.labelNaselenoMqsto.Location = new System.Drawing.Point(160, 88);
			this.labelNaselenoMqsto.Name = "labelNaselenoMqsto";
			this.labelNaselenoMqsto.Size = new System.Drawing.Size(332, 16);
			this.labelNaselenoMqsto.TabIndex = 13;
			this.labelNaselenoMqsto.Text = "Населено място";
			// 
			// labelSex
			// 
			this.labelSex.Location = new System.Drawing.Point(408, 184);
			this.labelSex.Name = "labelSex";
			this.labelSex.Size = new System.Drawing.Size(100, 24);
			this.labelSex.TabIndex = 16;
			this.labelSex.Text = "Пол:";
			// 
			// labelFamilyStatus
			// 
			this.labelFamilyStatus.Location = new System.Drawing.Point(8, 16);
			this.labelFamilyStatus.Name = "labelFamilyStatus";
			this.labelFamilyStatus.Size = new System.Drawing.Size(120, 16);
			this.labelFamilyStatus.TabIndex = 18;
			this.labelFamilyStatus.Text = "Семейно Положение";
			// 
			// labelEducation
			// 
			this.labelEducation.Location = new System.Drawing.Point(128, 16);
			this.labelEducation.Name = "labelEducation";
			this.labelEducation.Size = new System.Drawing.Size(120, 16);
			this.labelEducation.TabIndex = 20;
			this.labelEducation.Text = "Образувание";
			// 
			// labelDiplom
			// 
			this.labelDiplom.Location = new System.Drawing.Point(248, 16);
			this.labelDiplom.Name = "labelDiplom";
			this.labelDiplom.Size = new System.Drawing.Size(100, 16);
			this.labelDiplom.TabIndex = 22;
			this.labelDiplom.Text = "Диплома данни";
			// 
			// labelProfesion
			// 
			this.labelProfesion.Location = new System.Drawing.Point(8, 56);
			this.labelProfesion.Name = "labelProfesion";
			this.labelProfesion.Size = new System.Drawing.Size(120, 16);
			this.labelProfesion.TabIndex = 24;
			this.labelProfesion.Text = "Професия";
			// 
			// checkedListBoxLanguage
			// 
			this.checkedListBoxLanguage.Items.AddRange(new object[] {
																		"Английски",
																		"Немски ",
																		"Френски",
																		"Руски",
																		"Испански",
																		"Португалски",
																		"Италиански",
																		"Гръцки",
																		"Сръбски",
																		"Румънски",
																		"Турски",
																		"(Цигански)",
																		""});
			this.checkedListBoxLanguage.Location = new System.Drawing.Point(128, 72);
			this.checkedListBoxLanguage.Name = "checkedListBoxLanguage";
			this.checkedListBoxLanguage.Size = new System.Drawing.Size(120, 19);
			this.checkedListBoxLanguage.TabIndex = 25;
			// 
			// labelLanguage
			// 
			this.labelLanguage.Location = new System.Drawing.Point(136, 56);
			this.labelLanguage.Name = "labelLanguage";
			this.labelLanguage.Size = new System.Drawing.Size(100, 16);
			this.labelLanguage.TabIndex = 26;
			this.labelLanguage.Text = "Езици";
			// 
			// labellanguageLevel
			// 
			this.labellanguageLevel.Location = new System.Drawing.Point(248, 56);
			this.labellanguageLevel.Name = "labellanguageLevel";
			this.labellanguageLevel.Size = new System.Drawing.Size(128, 16);
			this.labellanguageLevel.TabIndex = 28;
			this.labellanguageLevel.Text = "Степен на владеене";
			// 
			// checkedListBoxLangLevel
			// 
			this.checkedListBoxLangLevel.Items.AddRange(new object[] {
																		 "Перфектно",
																		 "Писмено и говоримо",
																		 "Говоримо",
																		 "Средно",
																		 "Слабо"});
			this.checkedListBoxLangLevel.Location = new System.Drawing.Point(248, 72);
			this.checkedListBoxLangLevel.Name = "checkedListBoxLangLevel";
			this.checkedListBoxLangLevel.Size = new System.Drawing.Size(120, 19);
			this.checkedListBoxLangLevel.TabIndex = 27;
			// 
			// labelScience
			// 
			this.labelScience.Location = new System.Drawing.Point(8, 96);
			this.labelScience.Name = "labelScience";
			this.labelScience.Size = new System.Drawing.Size(120, 16);
			this.labelScience.TabIndex = 30;
			this.labelScience.Text = "Научно звание";
			// 
			// comboBoxScience
			// 
			this.comboBoxScience.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxScience.Location = new System.Drawing.Point(8, 112);
			this.comboBoxScience.Name = "comboBoxScience";
			this.comboBoxScience.Size = new System.Drawing.Size(121, 21);
			this.comboBoxScience.TabIndex = 29;
			// 
			// labelScienceLevel
			// 
			this.labelScienceLevel.Location = new System.Drawing.Point(136, 96);
			this.labelScienceLevel.Name = "labelScienceLevel";
			this.labelScienceLevel.Size = new System.Drawing.Size(120, 16);
			this.labelScienceLevel.TabIndex = 32;
			this.labelScienceLevel.Text = "Научна степен";
			// 
			// labelMilitaryStatus
			// 
			this.labelMilitaryStatus.Location = new System.Drawing.Point(8, 144);
			this.labelMilitaryStatus.Name = "labelMilitaryStatus";
			this.labelMilitaryStatus.Size = new System.Drawing.Size(120, 16);
			this.labelMilitaryStatus.TabIndex = 36;
			this.labelMilitaryStatus.Text = "Воененна отчетност";
			// 
			// labelMilitaryRang
			// 
			this.labelMilitaryRang.Location = new System.Drawing.Point(264, 96);
			this.labelMilitaryRang.Name = "labelMilitaryRang";
			this.labelMilitaryRang.Size = new System.Drawing.Size(120, 16);
			this.labelMilitaryRang.TabIndex = 34;
			this.labelMilitaryRang.Text = "Военен ранг";
			// 
			// comboBoxMilitaryRang
			// 
			this.comboBoxMilitaryRang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMilitaryRang.Location = new System.Drawing.Point(264, 112);
			this.comboBoxMilitaryRang.Name = "comboBoxMilitaryRang";
			this.comboBoxMilitaryRang.Size = new System.Drawing.Size(121, 21);
			this.comboBoxMilitaryRang.TabIndex = 33;
			// 
			// labelEmployeStatus
			// 
			this.labelEmployeStatus.Location = new System.Drawing.Point(136, 144);
			this.labelEmployeStatus.Name = "labelEmployeStatus";
			this.labelEmployeStatus.Size = new System.Drawing.Size(120, 16);
			this.labelEmployeStatus.TabIndex = 38;
			this.labelEmployeStatus.Text = "Работен статус";
			// 
			// labelCategory
			// 
			this.labelCategory.Location = new System.Drawing.Point(264, 144);
			this.labelCategory.Name = "labelCategory";
			this.labelCategory.Size = new System.Drawing.Size(120, 16);
			this.labelCategory.TabIndex = 40;
			this.labelCategory.Text = "Категория";
			// 
			// dateTimePickerPostypilNa
			// 
			this.dateTimePickerPostypilNa.Location = new System.Drawing.Point(8, 200);
			this.dateTimePickerPostypilNa.Name = "dateTimePickerPostypilNa";
			this.dateTimePickerPostypilNa.Size = new System.Drawing.Size(136, 20);
			this.dateTimePickerPostypilNa.TabIndex = 41;
			// 
			// labelHiredAt
			// 
			this.labelHiredAt.Location = new System.Drawing.Point(8, 184);
			this.labelHiredAt.Name = "labelHiredAt";
			this.labelHiredAt.Size = new System.Drawing.Size(120, 16);
			this.labelHiredAt.TabIndex = 42;
			this.labelHiredAt.Text = "Постъпил на:";
			// 
			// labelWorkExperiance
			// 
			this.labelWorkExperiance.Location = new System.Drawing.Point(144, 184);
			this.labelWorkExperiance.Name = "labelWorkExperiance";
			this.labelWorkExperiance.Size = new System.Drawing.Size(88, 16);
			this.labelWorkExperiance.TabIndex = 43;
			this.labelWorkExperiance.Text = "Трудов Стаж";
			// 
			// textBoxWorkExpiriance
			// 
			this.textBoxWorkExpiriance.Location = new System.Drawing.Point(152, 200);
			this.textBoxWorkExpiriance.Name = "textBoxWorkExpiriance";
			this.textBoxWorkExpiriance.TabIndex = 44;
			this.textBoxWorkExpiriance.Text = "";
			// 
			// comboBoxNaselenoMqsto
			// 
			this.comboBoxNaselenoMqsto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxNaselenoMqsto.Items.AddRange(new object[] {
																	   "sofia",
																	   "sofia kw",
																	   "po",
																	   "plovdiv",
																	   "plov",
																	   "gyzy na geog"});
			this.comboBoxNaselenoMqsto.Location = new System.Drawing.Point(160, 104);
			this.comboBoxNaselenoMqsto.Name = "comboBoxNaselenoMqsto";
			this.comboBoxNaselenoMqsto.Size = new System.Drawing.Size(392, 21);
			this.comboBoxNaselenoMqsto.TabIndex = 45;
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.TabPersonalInfo);
			this.tabControl1.Controls.Add(this.tabPageAditionalInfo);
			this.tabControl1.Controls.Add(this.tabPageAssignment);
			this.tabControl1.Controls.Add(this.tabPageAbsence);
			this.tabControl1.Controls.Add(this.tabPagePenalty);
			this.tabControl1.Controls.Add(this.tabPageNotes);
			this.tabControl1.Location = new System.Drawing.Point(8, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(656, 368);
			this.tabControl1.TabIndex = 47;
			// 
			// TabPersonalInfo
			// 
			this.TabPersonalInfo.Controls.Add(this.dateTimePickerPCCardPublished);
			this.TabPersonalInfo.Controls.Add(this.labelPublishedByy);
			this.TabPersonalInfo.Controls.Add(this.numBoxPcCard);
			this.TabPersonalInfo.Controls.Add(this.labelPublishedBy);
			this.TabPersonalInfo.Controls.Add(this.textBoxPublishedFrom);
			this.TabPersonalInfo.Controls.Add(this.labelJKkwartal);
			this.TabPersonalInfo.Controls.Add(this.numBoxTelephone);
			this.TabPersonalInfo.Controls.Add(this.label3);
			this.TabPersonalInfo.Controls.Add(this.labelNumBlock);
			this.TabPersonalInfo.Controls.Add(this.textBoxNumBlock);
			this.TabPersonalInfo.Controls.Add(this.labelStreet);
			this.TabPersonalInfo.Controls.Add(this.textBoxStreet);
			this.TabPersonalInfo.Controls.Add(this.labelKwartal);
			this.TabPersonalInfo.Controls.Add(this.textBoxKwartal);
			this.TabPersonalInfo.Controls.Add(this.numBoxEgn);
			this.TabPersonalInfo.Controls.Add(this.label1);
			this.TabPersonalInfo.Controls.Add(this.textBoxNames);
			this.TabPersonalInfo.Controls.Add(this.labelNames);
			this.TabPersonalInfo.Controls.Add(this.comboBoxCountry);
			this.TabPersonalInfo.Controls.Add(this.labelCountry);
			this.TabPersonalInfo.Controls.Add(this.comboBoxLive);
			this.TabPersonalInfo.Controls.Add(this.labelLive);
			this.TabPersonalInfo.Controls.Add(this.comboBoxNaselenoMqsto);
			this.TabPersonalInfo.Controls.Add(this.labelRegion);
			this.TabPersonalInfo.Controls.Add(this.comboBoxRegion);
			this.TabPersonalInfo.Controls.Add(this.labelNaselenoMqsto);
			this.TabPersonalInfo.Location = new System.Drawing.Point(4, 22);
			this.TabPersonalInfo.Name = "TabPersonalInfo";
			this.TabPersonalInfo.Size = new System.Drawing.Size(648, 342);
			this.TabPersonalInfo.TabIndex = 0;
			this.TabPersonalInfo.Text = "Задължителни";
			// 
			// dateTimePickerPCCardPublished
			// 
			this.dateTimePickerPCCardPublished.Location = new System.Drawing.Point(96, 184);
			this.dateTimePickerPCCardPublished.Name = "dateTimePickerPCCardPublished";
			this.dateTimePickerPCCardPublished.Size = new System.Drawing.Size(176, 20);
			this.dateTimePickerPCCardPublished.TabIndex = 62;
			// 
			// labelPublishedByy
			// 
			this.labelPublishedByy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.labelPublishedByy.Location = new System.Drawing.Point(96, 168);
			this.labelPublishedByy.Name = "labelPublishedByy";
			this.labelPublishedByy.Size = new System.Drawing.Size(332, 16);
			this.labelPublishedByy.TabIndex = 61;
			this.labelPublishedByy.Text = "Издаден на";
			// 
			// numBoxPcCard
			// 
			this.numBoxPcCard.Location = new System.Drawing.Point(8, 184);
			this.numBoxPcCard.Name = "numBoxPcCard";
			this.numBoxPcCard.Size = new System.Drawing.Size(88, 20);
			this.numBoxPcCard.TabIndex = 60;
			this.numBoxPcCard.Text = "";
			// 
			// labelPublishedBy
			// 
			this.labelPublishedBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.labelPublishedBy.Location = new System.Drawing.Point(272, 168);
			this.labelPublishedBy.Name = "labelPublishedBy";
			this.labelPublishedBy.Size = new System.Drawing.Size(332, 16);
			this.labelPublishedBy.TabIndex = 59;
			this.labelPublishedBy.Text = "Издаден от";
			// 
			// textBoxPublishedFrom
			// 
			this.textBoxPublishedFrom.Location = new System.Drawing.Point(272, 184);
			this.textBoxPublishedFrom.Name = "textBoxPublishedFrom";
			this.textBoxPublishedFrom.Size = new System.Drawing.Size(128, 20);
			this.textBoxPublishedFrom.TabIndex = 58;
			this.textBoxPublishedFrom.Text = "";
			// 
			// labelJKkwartal
			// 
			this.labelJKkwartal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.labelJKkwartal.Location = new System.Drawing.Point(8, 168);
			this.labelJKkwartal.Name = "labelJKkwartal";
			this.labelJKkwartal.Size = new System.Drawing.Size(332, 16);
			this.labelJKkwartal.TabIndex = 57;
			this.labelJKkwartal.Text = "Л.К. Номер";
			// 
			// numBoxTelephone
			// 
			this.numBoxTelephone.Location = new System.Drawing.Point(320, 144);
			this.numBoxTelephone.Name = "numBoxTelephone";
			this.numBoxTelephone.Size = new System.Drawing.Size(88, 20);
			this.numBoxTelephone.TabIndex = 55;
			this.numBoxTelephone.Text = "";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Location = new System.Drawing.Point(320, 128);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(312, 16);
			this.label3.TabIndex = 54;
			this.label3.Text = "Телефон";
			// 
			// labelNumBlock
			// 
			this.labelNumBlock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.labelNumBlock.Location = new System.Drawing.Point(216, 128);
			this.labelNumBlock.Name = "labelNumBlock";
			this.labelNumBlock.Size = new System.Drawing.Size(332, 16);
			this.labelNumBlock.TabIndex = 52;
			this.labelNumBlock.Text = "N:, Бл., вх., ет., ап.";
			// 
			// textBoxNumBlock
			// 
			this.textBoxNumBlock.Location = new System.Drawing.Point(216, 144);
			this.textBoxNumBlock.Name = "textBoxNumBlock";
			this.textBoxNumBlock.Size = new System.Drawing.Size(104, 20);
			this.textBoxNumBlock.TabIndex = 51;
			this.textBoxNumBlock.Text = "";
			// 
			// labelStreet
			// 
			this.labelStreet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.labelStreet.Location = new System.Drawing.Point(112, 128);
			this.labelStreet.Name = "labelStreet";
			this.labelStreet.Size = new System.Drawing.Size(332, 16);
			this.labelStreet.TabIndex = 50;
			this.labelStreet.Text = "Улица/Булевард";
			// 
			// textBoxStreet
			// 
			this.textBoxStreet.Location = new System.Drawing.Point(112, 144);
			this.textBoxStreet.Name = "textBoxStreet";
			this.textBoxStreet.Size = new System.Drawing.Size(104, 20);
			this.textBoxStreet.TabIndex = 49;
			this.textBoxStreet.Text = "";
			// 
			// labelKwartal
			// 
			this.labelKwartal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.labelKwartal.Location = new System.Drawing.Point(8, 128);
			this.labelKwartal.Name = "labelKwartal";
			this.labelKwartal.Size = new System.Drawing.Size(332, 16);
			this.labelKwartal.TabIndex = 48;
			this.labelKwartal.Text = "Ж.К./Квартал";
			// 
			// textBoxKwartal
			// 
			this.textBoxKwartal.Location = new System.Drawing.Point(8, 144);
			this.textBoxKwartal.Name = "textBoxKwartal";
			this.textBoxKwartal.Size = new System.Drawing.Size(104, 20);
			this.textBoxKwartal.TabIndex = 47;
			this.textBoxKwartal.Text = "";
			// 
			// tabPageAditionalInfo
			// 
			this.tabPageAditionalInfo.Controls.Add(this.comboBoxFamilyStatus);
			this.tabPageAditionalInfo.Controls.Add(this.comboBoxEducation);
			this.tabPageAditionalInfo.Controls.Add(this.textBoxDiplom);
			this.tabPageAditionalInfo.Controls.Add(this.labelFamilyStatus);
			this.tabPageAditionalInfo.Controls.Add(this.labelEducation);
			this.tabPageAditionalInfo.Controls.Add(this.labelDiplom);
			this.tabPageAditionalInfo.Controls.Add(this.comboBoxProfesion);
			this.tabPageAditionalInfo.Controls.Add(this.labelProfesion);
			this.tabPageAditionalInfo.Controls.Add(this.checkedListBoxLangLevel);
			this.tabPageAditionalInfo.Controls.Add(this.checkedListBoxLanguage);
			this.tabPageAditionalInfo.Controls.Add(this.labelLanguage);
			this.tabPageAditionalInfo.Controls.Add(this.labellanguageLevel);
			this.tabPageAditionalInfo.Controls.Add(this.labelScience);
			this.tabPageAditionalInfo.Controls.Add(this.comboBoxScience);
			this.tabPageAditionalInfo.Controls.Add(this.labelScienceLevel);
			this.tabPageAditionalInfo.Controls.Add(this.labelMilitaryRang);
			this.tabPageAditionalInfo.Controls.Add(this.comboBoxMilitaryRang);
			this.tabPageAditionalInfo.Controls.Add(this.comboBoxScienceLevel);
			this.tabPageAditionalInfo.Controls.Add(this.labelMilitaryStatus);
			this.tabPageAditionalInfo.Controls.Add(this.labelEmployeStatus);
			this.tabPageAditionalInfo.Controls.Add(this.labelCategory);
			this.tabPageAditionalInfo.Controls.Add(this.comboBoxMilitaryStatus);
			this.tabPageAditionalInfo.Controls.Add(this.comboBoxEmployeStatus);
			this.tabPageAditionalInfo.Controls.Add(this.comboBoxCategory);
			this.tabPageAditionalInfo.Controls.Add(this.dateTimePickerPostypilNa);
			this.tabPageAditionalInfo.Controls.Add(this.labelHiredAt);
			this.tabPageAditionalInfo.Controls.Add(this.labelWorkExperiance);
			this.tabPageAditionalInfo.Controls.Add(this.textBoxWorkExpiriance);
			this.tabPageAditionalInfo.Location = new System.Drawing.Point(4, 22);
			this.tabPageAditionalInfo.Name = "tabPageAditionalInfo";
			this.tabPageAditionalInfo.Size = new System.Drawing.Size(648, 342);
			this.tabPageAditionalInfo.TabIndex = 1;
			this.tabPageAditionalInfo.Text = "Допълнителни";
			// 
			// tabPageAssignment
			// 
			this.tabPageAssignment.Controls.Add(this.buttonPrintD);
			this.tabPageAssignment.Controls.Add(this.dateTimePickerContractExpiry);
			this.tabPageAssignment.Controls.Add(this.numBoxBaseSalary);
			this.tabPageAssignment.Controls.Add(this.numBoxNumberKids);
			this.tabPageAssignment.Controls.Add(this.buttonAssignmentDelete);
			this.tabPageAssignment.Controls.Add(this.buttonAssignmentSave);
			this.tabPageAssignment.Controls.Add(this.buttonAssignmentEdit);
			this.tabPageAssignment.Controls.Add(this.radioButtonAdditional);
			this.tabPageAssignment.Controls.Add(this.radioButtonAssignment);
			this.tabPageAssignment.Controls.Add(this.buttonAssignment);
			this.tabPageAssignment.Controls.Add(this.dateTimePickerAssignedAt);
			this.tabPageAssignment.Controls.Add(this.label19);
			this.tabPageAssignment.Controls.Add(this.label18);
			this.tabPageAssignment.Controls.Add(this.label17);
			this.tabPageAssignment.Controls.Add(this.label16);
			this.tabPageAssignment.Controls.Add(this.label15);
			this.tabPageAssignment.Controls.Add(this.label14);
			this.tabPageAssignment.Controls.Add(this.label13);
			this.tabPageAssignment.Controls.Add(this.label12);
			this.tabPageAssignment.Controls.Add(this.label11);
			this.tabPageAssignment.Controls.Add(this.label10);
			this.tabPageAssignment.Controls.Add(this.label9);
			this.tabPageAssignment.Controls.Add(this.label8);
			this.tabPageAssignment.Controls.Add(this.label7);
			this.tabPageAssignment.Controls.Add(this.label6);
			this.tabPageAssignment.Controls.Add(this.labelPart);
			this.tabPageAssignment.Controls.Add(this.labelControl);
			this.tabPageAssignment.Controls.Add(this.labelDirection);
			this.tabPageAssignment.Controls.Add(this.dataGridAssignment);
			this.tabPageAssignment.Controls.Add(this.textBoxNKIDCode);
			this.tabPageAssignment.Controls.Add(this.textBoxSalaryAddon);
			this.tabPageAssignment.Controls.Add(this.textBoxContractNumber);
			this.tabPageAssignment.Controls.Add(this.textBoxNKIDName);
			this.tabPageAssignment.Controls.Add(this.textBoxClassPercent);
			this.tabPageAssignment.Controls.Add(this.comboBoxStaff);
			this.tabPageAssignment.Controls.Add(this.comboBoxAssignReason);
			this.tabPageAssignment.Controls.Add(this.comboBoxWorkTime);
			this.tabPageAssignment.Controls.Add(this.comboBoxContract);
			this.tabPageAssignment.Controls.Add(this.comboBoxPosition);
			this.tabPageAssignment.Controls.Add(this.comboBoxLevel3);
			this.tabPageAssignment.Controls.Add(this.comboBoxLevel2);
			this.tabPageAssignment.Controls.Add(this.comboBoxLevel1);
			this.tabPageAssignment.Location = new System.Drawing.Point(4, 22);
			this.tabPageAssignment.Name = "tabPageAssignment";
			this.tabPageAssignment.Size = new System.Drawing.Size(648, 342);
			this.tabPageAssignment.TabIndex = 2;
			this.tabPageAssignment.Text = "Назначаване";
			// 
			// dateTimePickerContractExpiry
			// 
			this.dateTimePickerContractExpiry.Location = new System.Drawing.Point(280, 120);
			this.dateTimePickerContractExpiry.Name = "dateTimePickerContractExpiry";
			this.dateTimePickerContractExpiry.Size = new System.Drawing.Size(128, 20);
			this.dateTimePickerContractExpiry.TabIndex = 44;
			// 
			// numBoxBaseSalary
			// 
			this.numBoxBaseSalary.Location = new System.Drawing.Point(536, 120);
			this.numBoxBaseSalary.Name = "numBoxBaseSalary";
			this.numBoxBaseSalary.TabIndex = 43;
			this.numBoxBaseSalary.Text = "";
			// 
			// numBoxNumberKids
			// 
			this.numBoxNumberKids.Location = new System.Drawing.Point(424, 120);
			this.numBoxNumberKids.Name = "numBoxNumberKids";
			this.numBoxNumberKids.TabIndex = 42;
			this.numBoxNumberKids.Text = "";
			// 
			// buttonAssignmentDelete
			// 
			this.buttonAssignmentDelete.Location = new System.Drawing.Point(544, 304);
			this.buttonAssignmentDelete.Name = "buttonAssignmentDelete";
			this.buttonAssignmentDelete.TabIndex = 41;
			this.buttonAssignmentDelete.Text = "Изтрий";
			this.buttonAssignmentDelete.Click += new System.EventHandler(this.buttonAssignmentDelete_Click);
			// 
			// buttonAssignmentSave
			// 
			this.buttonAssignmentSave.Location = new System.Drawing.Point(544, 272);
			this.buttonAssignmentSave.Name = "buttonAssignmentSave";
			this.buttonAssignmentSave.TabIndex = 40;
			this.buttonAssignmentSave.Text = "Запис";
			this.buttonAssignmentSave.Click += new System.EventHandler(this.buttonAssignmentSave_Click);
			// 
			// buttonAssignmentEdit
			// 
			this.buttonAssignmentEdit.Location = new System.Drawing.Point(544, 240);
			this.buttonAssignmentEdit.Name = "buttonAssignmentEdit";
			this.buttonAssignmentEdit.TabIndex = 39;
			this.buttonAssignmentEdit.Text = "Корекция";
			this.buttonAssignmentEdit.Click += new System.EventHandler(this.buttonAssignmentEdit_Click);
			// 
			// radioButtonAdditional
			// 
			this.radioButtonAdditional.Location = new System.Drawing.Point(296, 160);
			this.radioButtonAdditional.Name = "radioButtonAdditional";
			this.radioButtonAdditional.Size = new System.Drawing.Size(184, 16);
			this.radioButtonAdditional.TabIndex = 38;
			this.radioButtonAdditional.Text = "Допълнително споразумние";
			this.radioButtonAdditional.CheckedChanged += new System.EventHandler(this.radioButtonAdditional_CheckedChanged);
			// 
			// radioButtonAssignment
			// 
			this.radioButtonAssignment.Checked = true;
			this.radioButtonAssignment.Location = new System.Drawing.Point(304, 144);
			this.radioButtonAssignment.Name = "radioButtonAssignment";
			this.radioButtonAssignment.TabIndex = 37;
			this.radioButtonAssignment.TabStop = true;
			this.radioButtonAssignment.Text = "Назначение";
			// 
			// buttonAssignment
			// 
			this.buttonAssignment.Location = new System.Drawing.Point(544, 208);
			this.buttonAssignment.Name = "buttonAssignment";
			this.buttonAssignment.Size = new System.Drawing.Size(72, 23);
			this.buttonAssignment.TabIndex = 36;
			this.buttonAssignment.Text = "Назначаване";
			this.buttonAssignment.Click += new System.EventHandler(this.buttonAssignment_Click);
			// 
			// dateTimePickerAssignedAt
			// 
			this.dateTimePickerAssignedAt.Location = new System.Drawing.Point(296, 72);
			this.dateTimePickerAssignedAt.Name = "dateTimePickerAssignedAt";
			this.dateTimePickerAssignedAt.TabIndex = 35;
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(360, 184);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(100, 16);
			this.label19.TabIndex = 34;
			this.label19.Text = "Код по НКИД:";
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(16, 184);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(280, 16);
			this.label18.TabIndex = 33;
			this.label18.Text = "Наименование на дейност в която е заето лицето:";
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(16, 144);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(100, 16);
			this.label17.TabIndex = 32;
			this.label17.Text = "Добавкикъм О.З.";
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(168, 144);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(100, 16);
			this.label16.TabIndex = 31;
			this.label16.Text = "Клас %:";
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(536, 104);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(100, 16);
			this.label15.TabIndex = 30;
			this.label15.Text = "Осн. заплата:";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(424, 104);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(100, 16);
			this.label14.TabIndex = 29;
			this.label14.Text = "Бр. деца:";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(504, 56);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(152, 16);
			this.label13.TabIndex = 28;
			this.label13.Text = "Основание за назначаване:";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(168, 104);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(100, 16);
			this.label12.TabIndex = 27;
			this.label12.Text = "Договор N:";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(16, 104);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(100, 16);
			this.label11.TabIndex = 26;
			this.label11.Text = "Щат:";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(320, 56);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(100, 16);
			this.label10.TabIndex = 25;
			this.label10.Text = "Назначен на:";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(280, 104);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(100, 16);
			this.label9.TabIndex = 24;
			this.label9.Text = "Договор до :";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(168, 56);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100, 16);
			this.label8.TabIndex = 23;
			this.label8.Text = "Работно време:";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(16, 56);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 16);
			this.label7.TabIndex = 22;
			this.label7.Text = "Договор:";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(456, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 16);
			this.label6.TabIndex = 21;
			this.label6.Text = "Длъжност :";
			// 
			// labelPart
			// 
			this.labelPart.Location = new System.Drawing.Point(320, 8);
			this.labelPart.Name = "labelPart";
			this.labelPart.Size = new System.Drawing.Size(100, 16);
			this.labelPart.TabIndex = 20;
			this.labelPart.Text = "Звено :";
			// 
			// labelControl
			// 
			this.labelControl.Location = new System.Drawing.Point(168, 8);
			this.labelControl.Name = "labelControl";
			this.labelControl.Size = new System.Drawing.Size(100, 16);
			this.labelControl.TabIndex = 19;
			this.labelControl.Text = "Управление :";
			// 
			// labelDirection
			// 
			this.labelDirection.Location = new System.Drawing.Point(16, 8);
			this.labelDirection.Name = "labelDirection";
			this.labelDirection.Size = new System.Drawing.Size(100, 16);
			this.labelDirection.TabIndex = 18;
			this.labelDirection.Text = "Дирекция :";
			// 
			// dataGridAssignment
			// 
			this.dataGridAssignment.DataMember = "";
			this.dataGridAssignment.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridAssignment.Location = new System.Drawing.Point(16, 224);
			this.dataGridAssignment.Name = "dataGridAssignment";
			this.dataGridAssignment.ReadOnly = true;
			this.dataGridAssignment.Size = new System.Drawing.Size(488, 112);
			this.dataGridAssignment.TabIndex = 17;
			// 
			// textBoxNKIDCode
			// 
			this.textBoxNKIDCode.Location = new System.Drawing.Point(360, 200);
			this.textBoxNKIDCode.Name = "textBoxNKIDCode";
			this.textBoxNKIDCode.TabIndex = 15;
			this.textBoxNKIDCode.Text = "";
			// 
			// textBoxSalaryAddon
			// 
			this.textBoxSalaryAddon.Location = new System.Drawing.Point(16, 160);
			this.textBoxSalaryAddon.Name = "textBoxSalaryAddon";
			this.textBoxSalaryAddon.TabIndex = 14;
			this.textBoxSalaryAddon.Text = "";
			// 
			// textBoxContractNumber
			// 
			this.textBoxContractNumber.Location = new System.Drawing.Point(168, 120);
			this.textBoxContractNumber.Name = "textBoxContractNumber";
			this.textBoxContractNumber.TabIndex = 13;
			this.textBoxContractNumber.Text = "";
			// 
			// textBoxNKIDName
			// 
			this.textBoxNKIDName.Location = new System.Drawing.Point(16, 200);
			this.textBoxNKIDName.Name = "textBoxNKIDName";
			this.textBoxNKIDName.Size = new System.Drawing.Size(328, 20);
			this.textBoxNKIDName.TabIndex = 12;
			this.textBoxNKIDName.Text = "";
			// 
			// textBoxClassPercent
			// 
			this.textBoxClassPercent.Location = new System.Drawing.Point(168, 160);
			this.textBoxClassPercent.Name = "textBoxClassPercent";
			this.textBoxClassPercent.TabIndex = 10;
			this.textBoxClassPercent.Text = "";
			// 
			// comboBoxStaff
			// 
			this.comboBoxStaff.Items.AddRange(new object[] {
															   "Щат",
															   "1/2 Щат",
															   "1/4 Щат",
															   "Извънщатен"});
			this.comboBoxStaff.Location = new System.Drawing.Point(16, 120);
			this.comboBoxStaff.Name = "comboBoxStaff";
			this.comboBoxStaff.Size = new System.Drawing.Size(121, 21);
			this.comboBoxStaff.TabIndex = 7;
			// 
			// comboBoxAssignReason
			// 
			this.comboBoxAssignReason.Location = new System.Drawing.Point(504, 72);
			this.comboBoxAssignReason.Name = "comboBoxAssignReason";
			this.comboBoxAssignReason.Size = new System.Drawing.Size(121, 21);
			this.comboBoxAssignReason.TabIndex = 6;
			// 
			// comboBoxWorkTime
			// 
			this.comboBoxWorkTime.Items.AddRange(new object[] {
																  "Пълен работен ден",
																  "Половин работен ден",
																  "Неопределено работно време",
																  "Непоказано"});
			this.comboBoxWorkTime.Location = new System.Drawing.Point(168, 72);
			this.comboBoxWorkTime.Name = "comboBoxWorkTime";
			this.comboBoxWorkTime.Size = new System.Drawing.Size(121, 21);
			this.comboBoxWorkTime.TabIndex = 5;
			// 
			// comboBoxContract
			// 
			this.comboBoxContract.Items.AddRange(new object[] {
																  "Безсрочен",
																  "Срочен",
																  "Втори трудов договор",
																  "Изпитателен срок",
																  "Граждански договор общ",
																  "Граждански договор и кон.....",
																  "Допълнително споразумение"});
			this.comboBoxContract.Location = new System.Drawing.Point(16, 72);
			this.comboBoxContract.Name = "comboBoxContract";
			this.comboBoxContract.Size = new System.Drawing.Size(121, 21);
			this.comboBoxContract.TabIndex = 4;
			// 
			// comboBoxPosition
			// 
			this.comboBoxPosition.Location = new System.Drawing.Point(456, 24);
			this.comboBoxPosition.Name = "comboBoxPosition";
			this.comboBoxPosition.Size = new System.Drawing.Size(121, 21);
			this.comboBoxPosition.TabIndex = 3;
			// 
			// comboBoxLevel3
			// 
			this.comboBoxLevel3.Location = new System.Drawing.Point(320, 24);
			this.comboBoxLevel3.Name = "comboBoxLevel3";
			this.comboBoxLevel3.Size = new System.Drawing.Size(121, 21);
			this.comboBoxLevel3.TabIndex = 2;
			// 
			// comboBoxLevel2
			// 
			this.comboBoxLevel2.Location = new System.Drawing.Point(168, 24);
			this.comboBoxLevel2.Name = "comboBoxLevel2";
			this.comboBoxLevel2.Size = new System.Drawing.Size(121, 21);
			this.comboBoxLevel2.TabIndex = 1;
			// 
			// comboBoxLevel1
			// 
			this.comboBoxLevel1.Items.AddRange(new object[] {
																"Непоказана"});
			this.comboBoxLevel1.Location = new System.Drawing.Point(16, 24);
			this.comboBoxLevel1.Name = "comboBoxLevel1";
			this.comboBoxLevel1.Size = new System.Drawing.Size(121, 21);
			this.comboBoxLevel1.TabIndex = 0;
			// 
			// tabPageAbsence
			// 
			this.tabPageAbsence.Controls.Add(this.buttonAbsenceDelete);
			this.tabPageAbsence.Controls.Add(this.buttonAbsenceSave);
			this.tabPageAbsence.Controls.Add(this.buttonAbsenceEdit);
			this.tabPageAbsence.Controls.Add(this.buttonAbsenceAdd);
			this.tabPageAbsence.Controls.Add(this.groupBoxAbsence);
			this.tabPageAbsence.Controls.Add(this.groupBoxHoliday);
			this.tabPageAbsence.Controls.Add(this.groupBoxAbsece);
			this.tabPageAbsence.Location = new System.Drawing.Point(4, 22);
			this.tabPageAbsence.Name = "tabPageAbsence";
			this.tabPageAbsence.Size = new System.Drawing.Size(648, 342);
			this.tabPageAbsence.TabIndex = 3;
			this.tabPageAbsence.Text = "Отсъствия";
			// 
			// buttonAbsenceDelete
			// 
			this.buttonAbsenceDelete.Location = new System.Drawing.Point(544, 304);
			this.buttonAbsenceDelete.Name = "buttonAbsenceDelete";
			this.buttonAbsenceDelete.TabIndex = 9;
			this.buttonAbsenceDelete.Text = "Изтрий";
			this.buttonAbsenceDelete.Click += new System.EventHandler(this.buttonAbsenceDelete_Click);
			// 
			// buttonAbsenceSave
			// 
			this.buttonAbsenceSave.Location = new System.Drawing.Point(544, 272);
			this.buttonAbsenceSave.Name = "buttonAbsenceSave";
			this.buttonAbsenceSave.TabIndex = 8;
			this.buttonAbsenceSave.Text = "Запис";
			this.buttonAbsenceSave.Click += new System.EventHandler(this.buttonAbsenceSave_Click);
			// 
			// buttonAbsenceEdit
			// 
			this.buttonAbsenceEdit.Location = new System.Drawing.Point(544, 240);
			this.buttonAbsenceEdit.Name = "buttonAbsenceEdit";
			this.buttonAbsenceEdit.TabIndex = 7;
			this.buttonAbsenceEdit.Text = "Корекция";
			this.buttonAbsenceEdit.Click += new System.EventHandler(this.buttonAbsenceEdit_Click);
			// 
			// buttonAbsenceAdd
			// 
			this.buttonAbsenceAdd.Location = new System.Drawing.Point(544, 208);
			this.buttonAbsenceAdd.Name = "buttonAbsenceAdd";
			this.buttonAbsenceAdd.TabIndex = 6;
			this.buttonAbsenceAdd.Text = "Добавяне";
			this.buttonAbsenceAdd.Click += new System.EventHandler(this.buttonAbsenceAdd_Click);
			// 
			// groupBoxAbsence
			// 
			this.groupBoxAbsence.Controls.Add(this.dataGridAbsence);
			this.groupBoxAbsence.Location = new System.Drawing.Point(8, 200);
			this.groupBoxAbsence.Name = "groupBoxAbsence";
			this.groupBoxAbsence.Size = new System.Drawing.Size(520, 136);
			this.groupBoxAbsence.TabIndex = 2;
			this.groupBoxAbsence.TabStop = false;
			this.groupBoxAbsence.Text = "Регистър на отсъствията на служителя";
			// 
			// dataGridAbsence
			// 
			this.dataGridAbsence.DataMember = "";
			this.dataGridAbsence.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridAbsence.Location = new System.Drawing.Point(8, 16);
			this.dataGridAbsence.Name = "dataGridAbsence";
			this.dataGridAbsence.ReadOnly = true;
			this.dataGridAbsence.Size = new System.Drawing.Size(504, 112);
			this.dataGridAbsence.TabIndex = 0;
			// 
			// groupBoxHoliday
			// 
			this.groupBoxHoliday.Controls.Add(this.numericUpDown1);
			this.groupBoxHoliday.Controls.Add(this.label35);
			this.groupBoxHoliday.Controls.Add(this.label34);
			this.groupBoxHoliday.Controls.Add(this.label33);
			this.groupBoxHoliday.Controls.Add(this.label32);
			this.groupBoxHoliday.Controls.Add(this.numBoxAbsenceUnpayedHoliday);
			this.groupBoxHoliday.Controls.Add(this.label31);
			this.groupBoxHoliday.Controls.Add(this.label30);
			this.groupBoxHoliday.Controls.Add(this.label29);
			this.groupBoxHoliday.Controls.Add(this.numBoxAbsenceCurrentYearRest);
			this.groupBoxHoliday.Controls.Add(this.numBoxAbsenceCurrentYearUsed);
			this.groupBoxHoliday.Controls.Add(this.numBoxAbsenceCurrentYearPlan);
			this.groupBoxHoliday.Controls.Add(this.numBoxAbsenceLastYearRest);
			this.groupBoxHoliday.Controls.Add(this.numBoxAbsenceLastYearUsed);
			this.groupBoxHoliday.Controls.Add(this.numBoxAbsenceLastYearPlan);
			this.groupBoxHoliday.Location = new System.Drawing.Point(8, 120);
			this.groupBoxHoliday.Name = "groupBoxHoliday";
			this.groupBoxHoliday.Size = new System.Drawing.Size(568, 80);
			this.groupBoxHoliday.TabIndex = 1;
			this.groupBoxHoliday.TabStop = false;
			this.groupBoxHoliday.Text = "Отпуски";
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(24, 48);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(80, 20);
			this.numericUpDown1.TabIndex = 14;
			this.numericUpDown1.Value = new System.Decimal(new int[] {
																		 20,
																		 0,
																		 0,
																		 0});
			// 
			// label35
			// 
			this.label35.ForeColor = System.Drawing.Color.Blue;
			this.label35.Location = new System.Drawing.Point(16, 24);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(136, 16);
			this.label35.TabIndex = 13;
			this.label35.Text = "Полагаем отпуск (дни)";
			// 
			// label34
			// 
			this.label34.ForeColor = System.Drawing.Color.Blue;
			this.label34.Location = new System.Drawing.Point(168, 56);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(72, 16);
			this.label34.TabIndex = 12;
			this.label34.Text = "За годината";
			// 
			// label33
			// 
			this.label33.ForeColor = System.Drawing.Color.Blue;
			this.label33.Location = new System.Drawing.Point(168, 24);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(72, 16);
			this.label33.TabIndex = 11;
			this.label33.Text = "Мин. година";
			// 
			// label32
			// 
			this.label32.ForeColor = System.Drawing.Color.Red;
			this.label32.Location = new System.Drawing.Point(472, 8);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(88, 16);
			this.label32.TabIndex = 10;
			this.label32.Text = "Неплатен отпуск";
			// 
			// numBoxAbsenceUnpayedHoliday
			// 
			this.numBoxAbsenceUnpayedHoliday.Location = new System.Drawing.Point(472, 24);
			this.numBoxAbsenceUnpayedHoliday.Name = "numBoxAbsenceUnpayedHoliday";
			this.numBoxAbsenceUnpayedHoliday.Size = new System.Drawing.Size(88, 20);
			this.numBoxAbsenceUnpayedHoliday.TabIndex = 9;
			this.numBoxAbsenceUnpayedHoliday.Text = "";
			// 
			// label31
			// 
			this.label31.Location = new System.Drawing.Point(392, 8);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(72, 16);
			this.label31.TabIndex = 8;
			this.label31.Text = "Остатък";
			// 
			// label30
			// 
			this.label30.Location = new System.Drawing.Point(320, 8);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(72, 16);
			this.label30.TabIndex = 7;
			this.label30.Text = "Ползвани";
			// 
			// label29
			// 
			this.label29.Location = new System.Drawing.Point(240, 8);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(72, 16);
			this.label29.TabIndex = 6;
			this.label29.Text = "По план";
			// 
			// numBoxAbsenceCurrentYearRest
			// 
			this.numBoxAbsenceCurrentYearRest.Location = new System.Drawing.Point(392, 56);
			this.numBoxAbsenceCurrentYearRest.Name = "numBoxAbsenceCurrentYearRest";
			this.numBoxAbsenceCurrentYearRest.Size = new System.Drawing.Size(72, 20);
			this.numBoxAbsenceCurrentYearRest.TabIndex = 5;
			this.numBoxAbsenceCurrentYearRest.Text = "";
			// 
			// numBoxAbsenceCurrentYearUsed
			// 
			this.numBoxAbsenceCurrentYearUsed.Location = new System.Drawing.Point(320, 56);
			this.numBoxAbsenceCurrentYearUsed.Name = "numBoxAbsenceCurrentYearUsed";
			this.numBoxAbsenceCurrentYearUsed.Size = new System.Drawing.Size(64, 20);
			this.numBoxAbsenceCurrentYearUsed.TabIndex = 4;
			this.numBoxAbsenceCurrentYearUsed.Text = "";
			// 
			// numBoxAbsenceCurrentYearPlan
			// 
			this.numBoxAbsenceCurrentYearPlan.Location = new System.Drawing.Point(240, 56);
			this.numBoxAbsenceCurrentYearPlan.Name = "numBoxAbsenceCurrentYearPlan";
			this.numBoxAbsenceCurrentYearPlan.Size = new System.Drawing.Size(72, 20);
			this.numBoxAbsenceCurrentYearPlan.TabIndex = 3;
			this.numBoxAbsenceCurrentYearPlan.Text = "";
			// 
			// numBoxAbsenceLastYearRest
			// 
			this.numBoxAbsenceLastYearRest.Location = new System.Drawing.Point(392, 24);
			this.numBoxAbsenceLastYearRest.Name = "numBoxAbsenceLastYearRest";
			this.numBoxAbsenceLastYearRest.Size = new System.Drawing.Size(72, 20);
			this.numBoxAbsenceLastYearRest.TabIndex = 2;
			this.numBoxAbsenceLastYearRest.Text = "";
			// 
			// numBoxAbsenceLastYearUsed
			// 
			this.numBoxAbsenceLastYearUsed.Location = new System.Drawing.Point(320, 24);
			this.numBoxAbsenceLastYearUsed.Name = "numBoxAbsenceLastYearUsed";
			this.numBoxAbsenceLastYearUsed.Size = new System.Drawing.Size(64, 20);
			this.numBoxAbsenceLastYearUsed.TabIndex = 1;
			this.numBoxAbsenceLastYearUsed.Text = "";
			// 
			// numBoxAbsenceLastYearPlan
			// 
			this.numBoxAbsenceLastYearPlan.Location = new System.Drawing.Point(240, 24);
			this.numBoxAbsenceLastYearPlan.Name = "numBoxAbsenceLastYearPlan";
			this.numBoxAbsenceLastYearPlan.Size = new System.Drawing.Size(72, 20);
			this.numBoxAbsenceLastYearPlan.TabIndex = 0;
			this.numBoxAbsenceLastYearPlan.Text = "";
			// 
			// groupBoxAbsece
			// 
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
			this.groupBoxAbsece.Size = new System.Drawing.Size(568, 104);
			this.groupBoxAbsece.TabIndex = 0;
			this.groupBoxAbsece.TabStop = false;
			this.groupBoxAbsece.Text = "Данни за отсъствие";
			// 
			// dateTimePickerAbsenceOrderFormData
			// 
			this.dateTimePickerAbsenceOrderFormData.Location = new System.Drawing.Point(424, 72);
			this.dateTimePickerAbsenceOrderFormData.Name = "dateTimePickerAbsenceOrderFormData";
			this.dateTimePickerAbsenceOrderFormData.Size = new System.Drawing.Size(136, 20);
			this.dateTimePickerAbsenceOrderFormData.TabIndex = 14;
			// 
			// label28
			// 
			this.label28.Location = new System.Drawing.Point(408, 56);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(136, 16);
			this.label28.TabIndex = 13;
			this.label28.Text = "Заповед от дата";
			// 
			// label27
			// 
			this.label27.Location = new System.Drawing.Point(288, 56);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(120, 16);
			this.label27.TabIndex = 11;
			this.label27.Text = "Номер заповед";
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
			this.label26.Text = "Основание/Бележки";
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
			this.label25.Text = "Вид отсъствие";
			// 
			// comboBoxAbsenceTypeAbsence
			// 
			this.comboBoxAbsenceTypeAbsence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxAbsenceTypeAbsence.Items.AddRange(new object[] {
																			"Болнични",
																			"Полагаем годишен отпуск",
																			"Неплатен отпуск",
																			"Отглеждане на дете",
																			"Командировка",
																			"Полагаем отпуск минали години"});
			this.comboBoxAbsenceTypeAbsence.Location = new System.Drawing.Point(376, 32);
			this.comboBoxAbsenceTypeAbsence.Name = "comboBoxAbsenceTypeAbsence";
			this.comboBoxAbsenceTypeAbsence.Size = new System.Drawing.Size(184, 21);
			this.comboBoxAbsenceTypeAbsence.TabIndex = 6;
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(312, 16);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(56, 16);
			this.label24.TabIndex = 5;
			this.label24.Text = "Брой дни";
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
			this.label23.Size = new System.Drawing.Size(136, 16);
			this.label23.TabIndex = 3;
			this.label23.Text = "До дата";
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(16, 16);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(136, 16);
			this.label22.TabIndex = 2;
			this.label22.Text = "От дата";
			// 
			// dateTimePickerAbsenceToData
			// 
			this.dateTimePickerAbsenceToData.Location = new System.Drawing.Point(168, 32);
			this.dateTimePickerAbsenceToData.Name = "dateTimePickerAbsenceToData";
			this.dateTimePickerAbsenceToData.Size = new System.Drawing.Size(136, 20);
			this.dateTimePickerAbsenceToData.TabIndex = 1;
			// 
			// dateTimePickerAbsenceFromData
			// 
			this.dateTimePickerAbsenceFromData.Location = new System.Drawing.Point(16, 32);
			this.dateTimePickerAbsenceFromData.Name = "dateTimePickerAbsenceFromData";
			this.dateTimePickerAbsenceFromData.Size = new System.Drawing.Size(136, 20);
			this.dateTimePickerAbsenceFromData.TabIndex = 0;
			// 
			// tabPagePenalty
			// 
			this.tabPagePenalty.Controls.Add(this.buttonPenaltyDelete);
			this.tabPagePenalty.Controls.Add(this.buttonPenaltySave);
			this.tabPagePenalty.Controls.Add(this.buttonPebaltyEdit);
			this.tabPagePenalty.Controls.Add(this.buttonPenaltyAdd);
			this.tabPagePenalty.Controls.Add(this.groupBox1);
			this.tabPagePenalty.Controls.Add(this.groupBoxPenalty);
			this.tabPagePenalty.Location = new System.Drawing.Point(4, 22);
			this.tabPagePenalty.Name = "tabPagePenalty";
			this.tabPagePenalty.Size = new System.Drawing.Size(648, 342);
			this.tabPagePenalty.TabIndex = 4;
			this.tabPagePenalty.Text = "Наказания";
			// 
			// buttonPenaltyDelete
			// 
			this.buttonPenaltyDelete.Location = new System.Drawing.Point(544, 304);
			this.buttonPenaltyDelete.Name = "buttonPenaltyDelete";
			this.buttonPenaltyDelete.TabIndex = 5;
			this.buttonPenaltyDelete.Text = "Изтрий";
			this.buttonPenaltyDelete.Click += new System.EventHandler(this.buttonPenaltyDelete_Click);
			// 
			// buttonPenaltySave
			// 
			this.buttonPenaltySave.Location = new System.Drawing.Point(544, 272);
			this.buttonPenaltySave.Name = "buttonPenaltySave";
			this.buttonPenaltySave.TabIndex = 4;
			this.buttonPenaltySave.Text = "Запис";
			this.buttonPenaltySave.Click += new System.EventHandler(this.buttonPenaltySave_Click);
			// 
			// buttonPebaltyEdit
			// 
			this.buttonPebaltyEdit.Location = new System.Drawing.Point(544, 240);
			this.buttonPebaltyEdit.Name = "buttonPebaltyEdit";
			this.buttonPebaltyEdit.TabIndex = 3;
			this.buttonPebaltyEdit.Text = "Корекция";
			this.buttonPebaltyEdit.Click += new System.EventHandler(this.buttonPebaltyEdit_Click);
			// 
			// buttonPenaltyAdd
			// 
			this.buttonPenaltyAdd.Location = new System.Drawing.Point(544, 208);
			this.buttonPenaltyAdd.Name = "buttonPenaltyAdd";
			this.buttonPenaltyAdd.TabIndex = 2;
			this.buttonPenaltyAdd.Text = "Добавяне";
			this.buttonPenaltyAdd.Click += new System.EventHandler(this.buttonPenaltyAdd_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.dataGridPenalty);
			this.groupBox1.Location = new System.Drawing.Point(24, 160);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(488, 160);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Данни за  наложени наказания за служителя";
			// 
			// dataGridPenalty
			// 
			this.dataGridPenalty.DataMember = "";
			this.dataGridPenalty.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridPenalty.Location = new System.Drawing.Point(8, 24);
			this.dataGridPenalty.Name = "dataGridPenalty";
			this.dataGridPenalty.ReadOnly = true;
			this.dataGridPenalty.Size = new System.Drawing.Size(472, 128);
			this.dataGridPenalty.TabIndex = 0;
			// 
			// groupBoxPenalty
			// 
			this.groupBoxPenalty.Controls.Add(this.dateTimePenaltyFormDate);
			this.groupBoxPenalty.Controls.Add(this.numBoxPenaltyOrder);
			this.groupBoxPenalty.Controls.Add(this.label21);
			this.groupBoxPenalty.Controls.Add(this.label20);
			this.groupBoxPenalty.Controls.Add(this.labelPenaltyReason);
			this.groupBoxPenalty.Controls.Add(this.textBoxPenaltyReason);
			this.groupBoxPenalty.Controls.Add(this.labelPenalty);
			this.groupBoxPenalty.Controls.Add(this.dateTimePickerPenaltyDate);
			this.groupBoxPenalty.Location = new System.Drawing.Point(24, 8);
			this.groupBoxPenalty.Name = "groupBoxPenalty";
			this.groupBoxPenalty.Size = new System.Drawing.Size(376, 144);
			this.groupBoxPenalty.TabIndex = 0;
			this.groupBoxPenalty.TabStop = false;
			this.groupBoxPenalty.Text = "Данни за наказание";
			// 
			// dateTimePenaltyFormDate
			// 
			this.dateTimePenaltyFormDate.Location = new System.Drawing.Point(160, 96);
			this.dateTimePenaltyFormDate.Name = "dateTimePenaltyFormDate";
			this.dateTimePenaltyFormDate.Size = new System.Drawing.Size(136, 20);
			this.dateTimePenaltyFormDate.TabIndex = 7;
			// 
			// numBoxPenaltyOrder
			// 
			this.numBoxPenaltyOrder.Location = new System.Drawing.Point(16, 96);
			this.numBoxPenaltyOrder.Name = "numBoxPenaltyOrder";
			this.numBoxPenaltyOrder.Size = new System.Drawing.Size(128, 20);
			this.numBoxPenaltyOrder.TabIndex = 6;
			this.numBoxPenaltyOrder.Text = "";
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(160, 72);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(128, 16);
			this.label21.TabIndex = 5;
			this.label21.Text = "От дата";
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(16, 72);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(128, 16);
			this.label20.TabIndex = 4;
			this.label20.Text = "Номер заповед";
			// 
			// labelPenaltyReason
			// 
			this.labelPenaltyReason.Location = new System.Drawing.Point(160, 24);
			this.labelPenaltyReason.Name = "labelPenaltyReason";
			this.labelPenaltyReason.Size = new System.Drawing.Size(128, 16);
			this.labelPenaltyReason.TabIndex = 3;
			this.labelPenaltyReason.Text = "Основание";
			// 
			// textBoxPenaltyReason
			// 
			this.textBoxPenaltyReason.Location = new System.Drawing.Point(160, 40);
			this.textBoxPenaltyReason.Name = "textBoxPenaltyReason";
			this.textBoxPenaltyReason.Size = new System.Drawing.Size(184, 20);
			this.textBoxPenaltyReason.TabIndex = 2;
			this.textBoxPenaltyReason.Text = "";
			// 
			// labelPenalty
			// 
			this.labelPenalty.Location = new System.Drawing.Point(16, 24);
			this.labelPenalty.Name = "labelPenalty";
			this.labelPenalty.Size = new System.Drawing.Size(128, 16);
			this.labelPenalty.TabIndex = 1;
			this.labelPenalty.Text = "Дата на наказанието";
			// 
			// dateTimePickerPenaltyDate
			// 
			this.dateTimePickerPenaltyDate.Location = new System.Drawing.Point(16, 40);
			this.dateTimePickerPenaltyDate.Name = "dateTimePickerPenaltyDate";
			this.dateTimePickerPenaltyDate.Size = new System.Drawing.Size(136, 20);
			this.dateTimePickerPenaltyDate.TabIndex = 0;
			// 
			// tabPageNotes
			// 
			this.tabPageNotes.Controls.Add(this.buttonNotes);
			this.tabPageNotes.Controls.Add(this.textBoxNotes);
			this.tabPageNotes.Location = new System.Drawing.Point(4, 22);
			this.tabPageNotes.Name = "tabPageNotes";
			this.tabPageNotes.Size = new System.Drawing.Size(648, 342);
			this.tabPageNotes.TabIndex = 5;
			this.tabPageNotes.Text = "Бележки";
			// 
			// buttonNotes
			// 
			this.buttonNotes.Location = new System.Drawing.Point(392, 296);
			this.buttonNotes.Name = "buttonNotes";
			this.buttonNotes.TabIndex = 1;
			this.buttonNotes.Text = "Активирай";
			this.buttonNotes.Click += new System.EventHandler(this.buttonNotes_Click);
			// 
			// textBoxNotes
			// 
			this.textBoxNotes.Location = new System.Drawing.Point(24, 24);
			this.textBoxNotes.Multiline = true;
			this.textBoxNotes.Name = "textBoxNotes";
			this.textBoxNotes.ReadOnly = true;
			this.textBoxNotes.Size = new System.Drawing.Size(344, 296);
			this.textBoxNotes.TabIndex = 0;
			this.textBoxNotes.Text = "";
			// 
			// buttonPrintD
			// 
			this.buttonPrintD.Location = new System.Drawing.Point(544, 176);
			this.buttonPrintD.Name = "buttonPrintD";
			this.buttonPrintD.TabIndex = 45;
			this.buttonPrintD.Text = "Печат";
			this.buttonPrintD.Click += new System.EventHandler(this.buttonPrintD_Click);
			// 
			// formPersonalData
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(688, 398);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.labelSex);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonОК);
			this.Name = "formPersonalData";
			this.Text = "Лично досие на служител";
			this.Load += new System.EventHandler(this.AddNewPersonForm_Load);
			this.tabControl1.ResumeLayout(false);
			this.TabPersonalInfo.ResumeLayout(false);
			this.tabPageAditionalInfo.ResumeLayout(false);
			this.tabPageAssignment.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridAssignment)).EndInit();
			this.tabPageAbsence.ResumeLayout(false);
			this.groupBoxAbsence.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridAbsence)).EndInit();
			this.groupBoxHoliday.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.groupBoxAbsece.ResumeLayout(false);
			this.tabPagePenalty.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridPenalty)).EndInit();
			this.groupBoxPenalty.ResumeLayout(false);
			this.tabPageNotes.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		private void buttonОК_Click(object sender, EventArgs e)
		{
			DataLayer.DataPackage package = new DataLayer.DataPackage();
			DataLayer.DataAction action = new DataLayer.DataAction( "person3", this.mainform.connString );
			this.ValidateAddPersonResult( package );
			action.UpdatePerson( package, this.personName, this.egn );
			this.Close();
		}
		private void EnableButtons( bool add, bool edit, bool save, bool delete, string kindButton )
		{
			switch( kindButton )
			{
				case "Penalty" : 
					this.buttonPenaltyAdd.Enabled = add;
					this.buttonPebaltyEdit.Enabled = edit;
					this.buttonPenaltySave.Enabled = save;
					this.buttonPenaltyDelete.Enabled = delete;
					break;
				case "Absence" : 
					this.buttonAbsenceAdd.Enabled = add;
					this.buttonAbsenceEdit.Enabled = edit;
					this.buttonAbsenceSave.Enabled = save;
					this.buttonAbsenceDelete.Enabled = delete;
					break;

				case "Assignment" : 
					this.buttonAssignment.Enabled = add;
					this.buttonAssignmentEdit.Enabled = edit;
					this.buttonAssignmentSave.Enabled = save;
					this.buttonAssignmentDelete.Enabled = delete;
					break;
			}
		}
		private void ControlReadOnly( bool ToMakeReadOnly, string kindButtons)
		{
			switch( kindButtons )
			{
				case "Penalty" :
					this.dateTimePenaltyFormDate.Enabled = !ToMakeReadOnly;
					this.dateTimePickerPenaltyDate.Enabled = !ToMakeReadOnly;
					this.textBoxPenaltyReason.ReadOnly  = ToMakeReadOnly;
					this.numBoxPenaltyOrder.ReadOnly = ToMakeReadOnly;
					break;
				case "Absence":
					foreach(Control ctrl in this.tabPageAbsence.Controls)
					{
						if( ctrl.GetType().Name != "Button")
						{
							ctrl.Enabled = !ToMakeReadOnly;
						}
					}
					this.groupBoxAbsence.Enabled = true;
					break;
				case "Assignment":

					foreach(Control ctrl in this.tabPageAssignment.Controls)
					{
						if( ctrl.GetType().Name != "Button")
						{
							ctrl.Enabled = !ToMakeReadOnly;
						}
					}
					this.dataGridAssignment.Enabled = true;
					this.radioButtonAdditional.Enabled = true;
					this.radioButtonAssignment.Enabled = true;
					break;
			}
		}
		private void AddPenaltyPackageToTable( DataLayer.PenaltyPackage package )
		{
			DataRow row = this.dtPenalty.NewRow();
			row["ID"] = package.ID;
			row["ID2"] = package.ID2;
			row["Reason"] = package.Reason;
			row["NumberOrder"] = package.NumberOrder;
			row["FromDate"] = package.FromDate;
			row["PenaltyDate"] = package.PenaltyDate;
			this.dtPenalty.Rows.Add( row );

		}

		private void AddAssignmentPackageToTable( DataLayer.AssignmentPackage package )
		{
			DataRow row = this.dtAssignment.NewRow();
			row["ID"] = package.ID;
			row["ID2"] = package.ID2;
			row["ID3"] = package.ID3;
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
			row["NKIDCode"] = package.NKIDCode;
			row["NKIDName"] = package.NKIDName;
			row["NumberKids"] = package.NumberKids;
			row["Position"] = package.Position;
			row["SalaryAddon"] = package.SalaryAddon;
			row["Staff"] = package.Staff;
			row["WorkTime"] = package.WorkTime;

			this.dtAssignment.Rows.Add( row );
		}
		private void AddAbsencePackageToTable( DataLayer.AbsencePackage package)
		{
			DataRow row = this.dtAbsence.NewRow();
			row["ID"] = package.ID;
			row["ID2"] = package.ID2;
			row["CountDays"] = package.CountDays;
			row["CurrentYearPlan"] = package.CurrentYearPlan;
			row["CurrentYearRest"] = package.CurrentYearRest;
			row["CurrentYearUsed"] = package.CurrentYearUsed;
			row["FromDate"] = package.FromDate;
			row["LastYearPlan"] = package.LastYearPlan;
			row["LastYearRest"] = package.LastYearRest;
			row["LastYearUsed"] = package.LastYearUsed;
			row["NumberOrder"] = package.NumberOrder;
			row["OrderFromDate"] = package.OrderFromDate;
			row["Reason"] = package.Reason;
			row["ToDate"] = package.ToDate;
			row["TypeAbsence"] = package.TypeAbsence;
			row["UnpayedHoliday"] = package.UnpayedHoliday;
		

			this.dtAbsence.Rows.Add( row );
		}
		private void RefreshPenaltyDataSource( bool IsFormLoad)
		{
			this.dataGridPenalty.Controls.Clear();
			this.dtPenalty = this.penaltyAction.SelectBasicDataFromFirmPersonal( this.ID );
			this.dtPenalty.PrimaryKey = new DataColumn[]{this.dtPenalty.Columns["ID2"]};	
			this.dataGridPenalty.DataSource = this.dtPenalty;
			if( ! this.IsPenaltyLoadForm )
			{
				this.dateTimePenaltyFormDate.DataBindings.RemoveAt(0);
				this.textBoxPenaltyReason.DataBindings.RemoveAt(0);
				this.textBoxPenaltyReason.DataBindings.RemoveAt(0);
				this.numBoxPenaltyOrder.DataBindings.RemoveAt(0);
				this.numBoxPenaltyOrder.DataBindings.RemoveAt(0);
				this.dateTimePickerPenaltyDate.DataBindings.RemoveAt(0);
			}

			this.textBoxPenaltyReason.DataBindings.Add("Tag", this.dtPenalty, "ID");
			this.textBoxPenaltyReason.DataBindings.Add("Text", this.dtPenalty, "Reason");
			this.numBoxPenaltyOrder.DataBindings.Add("Tag", this.dtPenalty, "ID2");
			this.numBoxPenaltyOrder.DataBindings.Add("Text", this.dtPenalty, "NumberOrder");
			this.dateTimePenaltyFormDate.DataBindings.Add("Value", this.dtPenalty, "FromDate");
			this.dateTimePickerPenaltyDate.DataBindings.Add("Value", this.dtPenalty, "PenaltyDate");

			this.IsPenaltyLoadForm = false;
		
		}
		private void RefreshAssignmentDataSource( bool IsFormLoad )
		{
			this.dataGridAssignment.Controls.Clear();
			this.dtAssignment = this.assignmentAction.SelectBasicDataForPersonAssignment( this.ID, !this.IsAssignment );
			this.dtAssignment.PrimaryKey = new DataColumn[]{this.dtAssignment.Columns["ID2"]};
			this.dataGridAssignment.DataSource = this.dtAssignment;
			this.BindingContext[ this.dtAssignment ].PositionChanged += new EventHandler(formPersonalData_PositionChangedAssignment);
			if( ! this.IsAssignmentLoadForm )
			{
				this.comboBoxLevel1.DataBindings.Clear();
				this.comboBoxLevel2.DataBindings.Clear();
				this.comboBoxLevel3.DataBindings.Clear();
				this.comboBoxPosition.DataBindings.Clear();
				this.comboBoxContract.DataBindings.Clear();
				this.comboBoxWorkTime.DataBindings.Clear();
				this.comboBoxAssignReason.DataBindings.Clear();
				this.comboBoxStaff.DataBindings.Clear();
				this.textBoxContractNumber.DataBindings.Clear();

				this.dateTimePickerContractExpiry.DataBindings.Clear();
				this.dateTimePickerAssignedAt.DataBindings.Clear();

				this.numBoxNumberKids.DataBindings.Clear();
				this.numBoxBaseSalary.DataBindings.Clear();
				this.textBoxSalaryAddon.DataBindings.Clear();
				this.textBoxClassPercent.DataBindings.Clear();
				this.textBoxNKIDName.DataBindings.Clear();
				this.textBoxNKIDCode.DataBindings.Clear();	

			}

			this.comboBoxLevel1.DataBindings.Add( "Tag", this.dtAssignment, "level1" );
			this.comboBoxLevel2.DataBindings.Add( "Tag", this.dtAssignment, "level2" );
			this.comboBoxLevel3.DataBindings.Add( "Tag", this.dtAssignment, "level3" );
			this.comboBoxPosition.DataBindings.Add( "Tag", this.dtAssignment, "position" );
			this.comboBoxContract.DataBindings.Add( "Tag", this.dtAssignment, "contract" );
			this.comboBoxWorkTime.DataBindings.Add( "Tag", this.dtAssignment, "WorkTime" );
			this.comboBoxAssignReason.DataBindings.Add( "Tag", this.dtAssignment, "AssignReason" );
			this.comboBoxStaff.DataBindings.Add( "Tag", this.dtAssignment, "staff" );

			this.dateTimePickerAssignedAt.DataBindings.Add( "Text", this.dtAssignment, "assignedat" );
			this.dateTimePickerContractExpiry.DataBindings.Add( "Value", this.dtAssignment, "contractexpiry" );

			this.textBoxContractNumber.DataBindings.Add( "Text", this.dtAssignment, "contractNumber" );
			this.textBoxContractNumber.DataBindings.Add( "Tag", this.dtAssignment, "ID" );
			this.numBoxNumberKids.DataBindings.Add( "Tag", this.dtAssignment, "ID2" );
			this.numBoxNumberKids.DataBindings.Add( "Text", this.dtAssignment, "NumberKids" );
			this.numBoxBaseSalary.DataBindings.Add( "Text", this.dtAssignment, "BaseSalary" );
			this.textBoxSalaryAddon.DataBindings.Add( "Text", this.dtAssignment, "SalaryAddon" );
			this.textBoxClassPercent.DataBindings.Add( "Text", this.dtAssignment, "ClassPercent" );
			this.textBoxNKIDName.DataBindings.Add( "Text", this.dtAssignment, "NKIDName" );
			this.textBoxNKIDCode.DataBindings.Add( "Text", this.dtAssignment, "NKIDCode" );




			this.IsAssignmentLoadForm = false;

		}
		private void RefreshAbsenceDataSource( bool IsFormLoad )
		{
			this.dataGridAbsence.Controls.Clear();
			this.dtAbsence = this.absenceAction.SelectBasicDataFromFirmPersonal( this.ID );
			this.dtAbsence.PrimaryKey = new DataColumn[]{this.dtAbsence.Columns["ID2"]};
			this.dataGridAbsence.DataSource = this.dtAbsence;
			this.BindingContext[ this.dtAbsence ].PositionChanged +=new EventHandler(formPersonalData_PositionChanged);

			if( ! this.IsAbsenceLoadForm )
			{
				this.dateTimePickerAbsenceFromData.DataBindings.RemoveAt(0);
				this.dateTimePickerAbsenceToData.DataBindings.RemoveAt(0);
				this.numBoxAbsenceDays.DataBindings.RemoveAt(0);
				this.numBoxAbsenceDays.DataBindings.RemoveAt(0);
				this.textBoxAbsenceReason.DataBindings.RemoveAt(0);
				
				this.comboBoxAbsenceTypeAbsence.DataBindings.Clear();

				this.textBoxAbsenceReason.DataBindings.RemoveAt(0);
				this.textBoxAbsenceNumberOrder.DataBindings.RemoveAt(0);
				this.dateTimePickerAbsenceOrderFormData.DataBindings.RemoveAt(0);

				this.numBoxAbsenceLastYearPlan.DataBindings.RemoveAt(0);
				this.numBoxAbsenceLastYearUsed.DataBindings.RemoveAt(0);
				this.numBoxAbsenceLastYearRest.DataBindings.RemoveAt(0);
				this.numBoxAbsenceUnpayedHoliday.DataBindings.RemoveAt(0);
				this.numBoxAbsenceCurrentYearPlan.DataBindings.RemoveAt(0);
				this.numBoxAbsenceCurrentYearUsed.DataBindings.RemoveAt(0);
				this.numBoxAbsenceCurrentYearRest.DataBindings.RemoveAt(0);

			}

			this.comboBoxAbsenceTypeAbsence.DataBindings.Add( "Tag", this.dtAbsence, "TypeAbsence" );

			this.numBoxAbsenceDays.DataBindings.Add("Tag", this.dtAbsence, "ID");
			this.numBoxAbsenceDays.DataBindings.Add("Text", this.dtAbsence, "CountDays");

			this.textBoxAbsenceReason.DataBindings.Add("Tag", this.dtAbsence, "ID2");
			this.textBoxAbsenceReason.DataBindings.Add("Text", this.dtAbsence, "Reason");
			
			this.dateTimePickerAbsenceFromData.DataBindings.Add("Value", this.dtAbsence, "FromDate");
			this.dateTimePickerAbsenceToData.DataBindings.Add("Value", this.dtAbsence, "ToDate");
			this.dateTimePickerAbsenceOrderFormData.DataBindings.Add("Value", this.dtAbsence, "OrderFromDate");
 
			this.textBoxAbsenceNumberOrder.DataBindings.Add("Text", this.dtAbsence, "NumberOrder");

			this.numBoxAbsenceLastYearPlan.DataBindings.Add("Text", this.dtAbsence, "LastYearPlan");
			this.numBoxAbsenceLastYearUsed.DataBindings.Add("Text", this.dtAbsence, "LastYearUsed");
			this.numBoxAbsenceLastYearRest.DataBindings.Add("Text", this.dtAbsence, "LastYearRest");
			this.numBoxAbsenceUnpayedHoliday.DataBindings.Add("Text", this.dtAbsence, "UnpayedHoliday");
			this.numBoxAbsenceCurrentYearPlan.DataBindings.Add("Text", this.dtAbsence, "CurrentYearPlan");
			this.numBoxAbsenceCurrentYearUsed.DataBindings.Add("Text", this.dtAbsence, "CurrentYearUsed");
			this.numBoxAbsenceCurrentYearRest.DataBindings.Add("Text", this.dtAbsence, "CurrentYearRest");

			this.IsAbsenceLoadForm = false;
		}
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void AddNewPersonForm_Load(object sender, System.EventArgs e)
		{
			this.LoadNomenklatures();

			this.IsPenaltyLoadForm = true;
			this.RefreshPenaltyDataSource( true );
			this.ControlReadOnly( true, "Penalty" );
			this.EnableButtons( true, true, false, true, "Penalty" );

			this.IsAbsenceLoadForm = true;
			this.RefreshAbsenceDataSource( true );
			this.ControlReadOnly( true, "Absence" );
			this.EnableButtons( true, true, false, true, "Absence" );

			assignmentAction = new DataLayer.AssignmentAction( "PersonAssignment", this.mainform.connString);
			this.IsAssignmentLoadForm = true;
			this.RefreshAssignmentDataSource( true );
			this.ControlReadOnly( true, "Assignment" );
			this.EnableButtons( true, true, false, true, "Assignment" );
			
		}

		private void LoadNomenklatures()
		{
			#region Loading Personal Info nomenklature
			
			DataSet ds;
			DataSet dsPerson;
			string arg = "0";
			DataLayer.DataAction daa = new DataLayer.DataAction("", this.mainform.connString);
			dsPerson = daa.SelectAllInfoForPerson( this.personName, this.egn );
			this.ID =  int.Parse( dsPerson.Tables[0].Rows[0]["ID"].ToString());
			this.numBoxEgn.Text = this.egn.ToString();
			this.textBoxNames.Text = (string)dsPerson.Tables[0].Rows[0]["name"];
			this.textBoxDiplom.Text = (string)dsPerson.Tables[0].Rows[0]["diplomadata"];
			this.textBoxKwartal.Text = (string)dsPerson.Tables[0].Rows[0]["kwartal"];
			//this.textBoxNumBlock.Text = (string)dsPerson.Tables[0].Rows[0]["numblockhouse"];
			this.textBoxPublishedFrom.Text = (string)dsPerson.Tables[0].Rows[0]["publishedby"];
			this.textBoxStreet.Text = (string)dsPerson.Tables[0].Rows[0]["street"];
			this.textBoxWorkExpiriance.Text = dsPerson.Tables[0].Rows[0]["workexpiriance"].ToString();
			this.ID = int.Parse( dsPerson.Tables[ 0 ].Rows[ 0 ][ "ID" ].ToString() );
			this.numBoxTelephone.Text = dsPerson.Tables[0].Rows[0]["telephone"].ToString();
			this.numBoxPcCard.Text = dsPerson.Tables[0].Rows[0]["pcard"].ToString();
            this.dateTimePickerPCCardPublished.Value = (DateTime)dsPerson.Tables[0].Rows[0]["pcardpublish"];
			this.dateTimePickerPostypilNa.Value = (DateTime)dsPerson.Tables[0].Rows[0]["hiredat"];
 
			if( int.Parse(dsPerson.Tables[0].Rows[0]["militarystatus"].ToString()) == 0 )
			{
				this.comboBoxMilitaryStatus.SelectedIndex = 0;
			}
			else
			{
				this.comboBoxMilitaryStatus.SelectedIndex = 1;
			}
			
			// Combobox popylwane
			//			ds = daa.SelectFromTable( "profession", "level" );
			//			foreach( DataRow dr in ds.Tables[0].Rows)
			//			{
			//				this.comboBoxProfesion.Items.Add(dr[0].ToString());
			//			}
			this.comboBoxFamilyStatus.DataSource = this.mainform.nomenclaatureData.arrFamilyStatus;
			arg = dsPerson.Tables[0].Rows[0]["familiStatus"].ToString();
			if( arg  == "" )
			{
				arg = "0";
			}
			int index = this.comboBoxProfesion.FindStringExact( arg );
			if( index > -1 )
			{
				this.comboBoxFamilyStatus.SelectedIndex = index;
			}
			index = 0;
			//
			this.comboBoxProfesion.DataSource = this.mainform.nomenclaatureData.arrProfession;
			arg = dsPerson.Tables[0].Rows[0]["proffesion"].ToString();
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

			this.comboBoxScienceLevel.DataSource = this.mainform.nomenclaatureData.arrScienceLevel;
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

			this.comboBoxScience.DataSource = this.mainform.nomenclaatureData.arrScienceTitle;
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


			this.comboBoxMilitaryRang.DataSource = this.mainform.nomenclaatureData.arrMilitaryRang;
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
			ds = daa.SelectFromTable( "languages", "level" );
			foreach( DataRow dr in ds.Tables[0].Rows)
			{
				this.checkedListBoxLanguage.Items.Add(dr[0].ToString());
			}
			index = this.checkedListBoxLanguage.FindStringExact( (string)dsPerson.Tables[0].Rows[0]["languages"]);
			if( index > -1 )
			{
				this.checkedListBoxLanguage.SelectedIndex = index;
				this.checkedListBoxLanguage.SetItemChecked(index, true );
			}
			index = 0;
			ds.Tables.Remove( ds.Tables[0] );
			///////////////////////////////////////////////////////
			this.comboBoxEducation.DataSource = this.mainform.nomenclaatureData.arrEducation;
			
			arg = dsPerson.Tables[0].Rows[0]["education"].ToString();
			if( arg  == "" )
			{
				arg = "0";
			}	
			if( index > -1 )
			{
				this.comboBoxEducation.SelectedIndex = index;
			}
			index = 0;
			///jh

			
			foreach( Countrys country in this.mainform.nomenclaatureData.arrCountrys )
			{
				this.comboBoxCountry.Items.Add( country.Code+ " "+ country.CountryName);
			}
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

			this.comboBoxCategory.DataSource = this.mainform.nomenclaatureData.arrCategory;
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


			this.comboBoxRegion.DataSource = this.mainform.nomenclaatureData.arrRegion;
			arg = dsPerson.Tables[0].Rows[0]["region"].ToString();
			if( arg  == "" )
			{
				arg = "0";
			}
			index = this.comboBoxRegion.FindStringExact( (string)dsPerson.Tables[0].Rows[0]["region"]);
			if( index > -1 )
			{
				this.comboBoxRegion.SelectedIndex = index;
			}
			index = 0;

			#endregion

			#region Loading Assignment Info

			//this.assAction = new DataLayer.AssignmentAction( "personAssignment", this.mainform.connString );
			//this.dataGridAssignment.DataSource = this.assAction.SelectBasicDataForPersonAssignment( this.ID, false );
	
			this.labelDirection.Text = this.mainform.nomenclaatureData.FirmStructure[0];
			this.labelControl.Text = this.mainform.nomenclaatureData.FirmStructure[1];
			this.labelPart.Text = this.mainform.nomenclaatureData.FirmStructure[2];

			foreach(Nodes node in this.mainform.nomenclaatureData.arrDirection)
			{
				this.comboBoxLevel1.Items.Add( node.NodeName );
			}
			foreach(Nodes node in this.mainform.nomenclaatureData.arrControl)
			{
				this.comboBoxLevel2.Items.Add( node.NodeName );
			}
			foreach(Nodes node in this.mainform.nomenclaatureData.arrTeam)
			{
				this.comboBoxLevel3.Items.Add( node.NodeName );
			}
			//this.comboBoxPosition.DataSource = 
			this.comboBoxPosition.DataSource =  this.personAction.SelectBasicDataFromFirmPersonal();
			this.comboBoxPosition.DisplayMember = "NameOfPostition";
			#endregion

			#region Loading Absence Info

			#endregion

			#region Loading Penalty Info

			#endregion

			#region Loading Notes Info

			this.dtNotes = this.note.SelectAllFormNotes( "Notes", this.ID );
			if( this.dtNotes.Rows.Count > 0 )
			{
				this.textBoxNotes.Text = this.dtNotes.Rows[ 0 ][ "Notes" ].ToString();
			}

			#endregion

		}
		public void ValidateAddPersonResult(DataLayer.DataPackage package)
		{
			package.FName = this.textBoxNames.Text;
			if( this.numBoxEgn.Text == "" )
			{
				package.Egn = 0;
			}
			else
			{
				package.Egn = Int32.Parse( this.numBoxEgn.Text);
			}
			if( this.comboBoxCountry.SelectedIndex == -1 )
			{
				package.BornCountry = "Непоказана";
			}
			else
			{
				package.BornCountry = this.comboBoxCountry.SelectedItem.ToString();
			}
			if( this.comboBoxCategory.SelectedIndex == -1 )
			{
				package.Category = "Непоказана";
			}
			else
			{
				package.Category = this.comboBoxCategory.SelectedItem.ToString();
			}	
			if( this.comboBoxCountry.SelectedIndex == -1 )
			{
				package.Country = "Непоказана";
			}
			else
			{
				package.Country = this.comboBoxCountry.SelectedItem.ToString();
			}
			package.DiplomaData = this.textBoxDiplom.Text;  
			if( this.comboBoxEducation.SelectedIndex == -1 )
			{
				package.Education = "Непоказана";
			}
			else
			{
				package.Education = this.comboBoxEducation.SelectedItem.ToString();
			}
			if( this.comboBoxFamilyStatus.SelectedIndex == -1 )
			{
				package.FamilyStatus = "Непоказана";
			}
			else
			{
				package.FamilyStatus = this.comboBoxFamilyStatus.SelectedItem.ToString();
			}

			package.HiredAt = this.dateTimePickerPostypilNa.Value;

			package.Kwartal = this.textBoxKwartal.Text;
			if( this.checkedListBoxLanguage.SelectedIndex == -1 )
			{
				package.Languages = "Непоказана";
			}
			else
			{
				package.Languages = this.checkedListBoxLanguage.SelectedItem.ToString();
			}
			if( this.comboBoxMilitaryRang.SelectedIndex == -1 )
			{
				package.MilitaryRang = "Непоказана";
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

			package.NumbBlockHouse = this.textBoxNumBlock.Text;
			if( this.numBoxPcCard.Text  == "" )
			{
				package.PCard = 0;
			}
			else
			{
				package.PCard = Int32.Parse(this.numBoxPcCard.Text);
			}
			package.PCardPublish = this.dateTimePickerPCCardPublished.Value;

			package.PublishedBy = this.textBoxPublishedFrom.Text;
			if( this.comboBoxRegion.SelectedIndex == -1 )
			{
				package.Region1 = "Непоказано";
			}
			else
			{
				package.Region1 = this.comboBoxRegion.SelectedItem.ToString();
			}
			if( this.comboBoxScienceLevel.SelectedIndex == -1 )
			{
				package.ScienceLevel = "Непоказано";
			}
			else
			{
				package.ScienceLevel = this.comboBoxScienceLevel.SelectedItem.ToString();
			}
			if( this.comboBoxScience.SelectedIndex == -1 )
			{
				package.ScienceTitle = "Непоказано";
			}
			else
			{
				package.ScienceTitle = this.comboBoxScience.SelectedItem.ToString();
			}
			package.Street = this.textBoxStreet.Text;
			if( this.numBoxTelephone.Text  == "" )
			{
				package.Telephone = 0;
			}
			else
			{
				package.Telephone = Int32.Parse(this.numBoxTelephone.Text);
			}
			if( this.comboBoxNaselenoMqsto.SelectedIndex == -1 )
			{
				package.Town = "Непоказана";
			}
			else
			{
				package.Town = this.comboBoxNaselenoMqsto.SelectedItem.ToString();
			}
			if( this.textBoxWorkExpiriance.Text  == "" )
			{
				package.WorkExpiriance = 0;
			}
			else
			{
				package.WorkExpiriance = Int32.Parse( this.textBoxWorkExpiriance.Text);
			}
			if( this.comboBoxEmployeStatus.SelectedIndex == -1 )
			{
				package.WorkStatus = "Непоказана";
			}
			else
			{
				package.WorkStatus = this.comboBoxEmployeStatus.SelectedItem.ToString();
			}
			if( this.comboBoxProfesion.SelectedIndex == -1 )
			{
				package.Proffesion = "Непоказана";
			}
			else
			{
				package.Proffesion = this.comboBoxProfesion.SelectedItem.ToString();
			}
		}

		public void ValidateAssignment(DataLayer.AssignmentPackage package)
		{
			package.ID = this.ID;

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
				package.Level1 = "Непоказана";
			}
			else
			{
				package.Level1 = this.comboBoxLevel1.SelectedItem.ToString();
			}

			if( this.comboBoxLevel2.SelectedIndex == -1 )
			{
				package.Level2 = "Непоказана";
			}
			else
			{
				package.Level2 = this.comboBoxLevel2.SelectedItem.ToString();
			}

			if( this.comboBoxLevel3.SelectedIndex == -1 )
			{
				package.Level3 = "Непоказана";
			}
			else
			{
				package.Level3 = this.comboBoxLevel3.SelectedItem.ToString();
			}	

			if( this.comboBoxPosition.SelectedIndex == -1 )
			{
				package.Position = "Непоказана";
			}
			else
			{
				package.Position = this.comboBoxPosition.SelectedItem.ToString();
			}

			if( this.comboBoxContract.SelectedIndex == -1 )
			{
				package.Contract = "Непоказана";
			}
			else
			{
				package.Contract = this.comboBoxContract.SelectedItem.ToString();
			}

			if( this.comboBoxWorkTime.SelectedIndex == -1 )
			{
				package.WorkTime = "Непоказана";
			}
			else
			{
				package.WorkTime = this.comboBoxWorkTime.SelectedItem.ToString();
			}

			package.AssignedAt = this.dateTimePickerAssignedAt.Value;

			if( this.comboBoxAssignReason.SelectedIndex == -1 )
			{
				package.AssignReason = "Непоказана";
			}
			else
			{
				package.AssignReason = this.comboBoxAssignReason.SelectedItem.ToString();
			}

			if( this.comboBoxStaff.SelectedIndex == -1 )
			{
				package.Staff = "Непоказана";
			}
			else
			{
				package.Staff = this.comboBoxStaff.SelectedItem.ToString();
			}

			package.ContractNumber = this.textBoxContractNumber.Text;

			package.ContractExpiry = dateTimePickerContractExpiry.Value;

			package.NumberKids = this.numBoxNumberKids.Text;

			package.BaseSalary = this.numBoxBaseSalary.Text;

			package.SalaryAddon = this.textBoxSalaryAddon.Text;

			package.ClassPercent = this.textBoxClassPercent.Text;
		}

		private void ValidatePenaltyData(DataLayer.PenaltyPackage package)
		{
			
			package.ID = this.ID;
			if( this.IsPenaltyEdit )
			{
				package.ID2 = ((int)this.numBoxPenaltyOrder.Tag);
			}
			else
			{
				package.ID2 = this.GenerateUniqueID();
			}
			if( this.numBoxPenaltyOrder.Text =="" )
			{
				package.NumberOrder = 0;
			}
			else
			{
				package.NumberOrder = int.Parse( this.numBoxPenaltyOrder.Text );
			}
			if( this.textBoxPenaltyReason.Text =="" )
			{
				package.Reason = "Непоказана";
			}
			else
			{
				package.Reason = this.textBoxPenaltyReason.Text;
			}
			package.PenaltyDate = this.dateTimePickerPenaltyDate.Value;
			package.FromDate = this.dateTimePenaltyFormDate.Value;
		}

		private void ValidateAbsenceData( DataLayer.AbsencePackage package )
		{
			package.ID = this.ID;
			if( this.IsAbsenceEdit )
			{
				package.ID2 = ((int)this.numBoxAbsenceDays.Tag);
			}
			else
			{
				package.ID2 = this.GenerateUniqueID();
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

			if( this.numBoxAbsenceLastYearPlan.Text =="" )
			{
				package.LastYearPlan = 0;
			}
			else
			{
				package.LastYearPlan = int.Parse( this.numBoxAbsenceLastYearPlan.Text );
			}

			if( this.numBoxAbsenceLastYearUsed.Text =="" )
			{
				package.LastYearUsed = 0;
			}
			else
			{
				package.LastYearUsed = int.Parse( this.numBoxAbsenceLastYearUsed.Text );
			}

			
			if( this.numBoxAbsenceLastYearRest.Text =="" )
			{
				package.LastYearRest = 0;
			}
			else
			{
				package.LastYearRest = int.Parse( this.numBoxAbsenceLastYearRest.Text );
			}

			if( this.numBoxAbsenceUnpayedHoliday.Text =="" )
			{
				package.UnpayedHoliday = 0;
			}
			else
			{
				package.UnpayedHoliday = int.Parse( this.numBoxAbsenceUnpayedHoliday.Text );
			}

			
			if( this.numBoxAbsenceCurrentYearPlan.Text =="" )
			{
				package.CurrentYearPlan = 0;
			}
			else
			{
				package.CurrentYearPlan = int.Parse( this.numBoxAbsenceCurrentYearPlan.Text );
			}

			if( this.numBoxAbsenceCurrentYearUsed.Text =="" )
			{
				package.CurrentYearUsed = 0;
			}
			else
			{
				package.CurrentYearUsed = int.Parse( this.numBoxAbsenceCurrentYearUsed.Text );
			}

			if( this.numBoxAbsenceCurrentYearRest.Text =="" )
			{
				package.CurrentYearRest = 0;
			}
			else
			{
				package.CurrentYearRest = int.Parse( this.numBoxAbsenceCurrentYearRest.Text );
			}

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
				package.TypeAbsence = "Непоказана";
			}
			else
			{
				package.TypeAbsence = this.comboBoxAbsenceTypeAbsence.SelectedItem.ToString();
			}
		}
		internal int GenerateUniqueID()
		{
			 // Не е добър алгоритъм, понякога може да се повторят ИД, Trqbwa da se promeni
			 for( int i = 0; i < rand.Next( 150 ); i++ )
			 {
				 rand.Next();
			 }
			return rand.Next();
		}
		private void comboBoxRegion_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.comboBoxNaselenoMqsto.Items.Clear();
			DataSet ds = new DataSet();
			DataLayer.DataAction daa = new DataLayer.DataAction("", this.mainform.connString);
			ds = daa.SelectColumnFromTable( "towns", new string[]{"Name","Prefix"} );
			foreach( DataRow dr in ds.Tables[0].Rows)
			{
				this.comboBoxNaselenoMqsto.Items.Add( dr[0].ToString()+ " "+ dr[1].ToString());
			}
			ds.Dispose();
		}

		private void buttonAssignment_Click(object sender, System.EventArgs e)
		{
			this.IsAssignmentEdit = false;
			this.EnableButtons( false, true, true, true, "Assignment" );
			this.ControlReadOnly( false, "Assignment");

			//		    DataLayer.AssignmentPackage assPackage = new DataLayer.AssignmentPackage();
			//			DataLayer.AssignmentAction assAction = new DataLayer.AssignmentAction( "personAssignment", this.mainform.connString );
			//			this.ValidateAssignment( assPackage );
			//			assAction.MakeAssignment( assPackage );
			
		}

		private void radioButtonAdditional_CheckedChanged(object sender, System.EventArgs e)
		{
			if( this.radioButtonAdditional.Checked )
			{
				this.IsAssignment = false;
				this.tabPageAssignment.Text = "Допълнителни Споразумения";
				this.RefreshAssignmentDataSource( false );
			}
			else
			{
				this.IsAssignment = true;
				this.tabPageAssignment.Text = "Назначаване";
				this.RefreshAssignmentDataSource( false );
			}
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
				note.UpdateNotes( "Notes", this.ID, this.textBoxNotes.Text );
				this.textBoxNotes.ReadOnly = true;
				this.buttonNotes.Text = "Активирай";
				IsActive = false;
			}
		}

		private void buttonPenaltyAdd_Click(object sender, System.EventArgs e)
		{
			this.IsPenaltyEdit = false;
			this.EnableButtons( false, true, true, true, "Penalty" );
			//this.ClearControls( false );
			this.ControlReadOnly( false, "Penalty");
		}

		private void buttonPebaltyEdit_Click(object sender, System.EventArgs e)
		{
			if( this.dataGridPenalty.VisibleRowCount > 0 )
			{
				IsPenaltyEdit = true;
				this.EnableButtons( false, true, true, true, "Penalty");
				this.ControlReadOnly( false, "Penalty" );
			}
		}

		private void buttonPenaltySave_Click(object sender, System.EventArgs e)
		{
			DataLayer.PenaltyPackage package = new DataLayer.PenaltyPackage();
			this.ValidatePenaltyData( package );

			if( !IsPenaltyEdit )
			{
				this.AddPenaltyPackageToTable( package );
				this.penaltyAction.UpdateDataAdapter( this.dtPenalty );
			}
			else
			{
				DataRow row = this.dtPenalty.Rows.Find( this.numBoxPenaltyOrder.Tag );
				if( row != null )
				{
					row["NumberOrder"] = package.NumberOrder;
					row["Reason"] = package.Reason;
					row["FromDate"] = package.FromDate;
					row["PenaltyDate"] = package.PenaltyDate;
					//this.penaltyAction.UpdateDataAdapter( this.dtPenalty );
					//this.penaltyAction.UpdatePenalty( this.dtPenalty );
					this.penaltyAction.UpdatePenalty( package );
				}
			}
			
			//this.RefreshDataSource( false )
			this.Refresh();
			//this.ClearControls( false );
			this.ControlReadOnly( true, "Penalty" );
			this.EnableButtons( true, true, false, true, "Penalty" );
			IsPenaltyEdit = false;
		}

		private void buttonPenaltyDelete_Click(object sender, System.EventArgs e)
		{
			if( this.dataGridPenalty.VisibleRowCount >= 1 )
			{
				if( MessageBox.Show( this, "Сигурни ли сте че искате да изтриете наказанието " + this.dataGridPenalty[ this.dataGridPenalty.CurrentRowIndex, 2 ].ToString(), "Изтриване", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					this.penaltyAction.DeleteRow( int.Parse(this.dataGridPenalty[ this.dataGridPenalty.CurrentRowIndex, 0 ].ToString()), int.Parse(this.dataGridPenalty[ this.dataGridPenalty.CurrentRowIndex, 1 ].ToString()));
					this.RefreshPenaltyDataSource( false );
					this.EnableButtons( true, true, false, true, "Penalty" );
				}
			}
		}

		private void buttonAbsenceAdd_Click(object sender, System.EventArgs e)
		{
			this.IsAbsenceEdit = false;
			this.EnableButtons( false, true, true, true, "Absence" );
			//this.ClearControls( false );
			this.ControlReadOnly( false, "Absence");
		}

		private void buttonAbsenceEdit_Click(object sender, System.EventArgs e)
		{
			if( this.dataGridAbsence.VisibleRowCount > 0 )
			{
				IsAbsenceEdit = true;
				this.EnableButtons( false, true, true, true, "Absence");
				this.ControlReadOnly( false, "Absence" );
			}
		}

		private void buttonAbsenceSave_Click(object sender, System.EventArgs e)
		{
			DataLayer.AbsencePackage package = new DataLayer.AbsencePackage();
			this.ValidateAbsenceData( package );

			if( !IsAbsenceEdit )
			{
				// Towa e pri dobawqne na now red

				this.AddAbsencePackageToTable( package );
				this.absenceAction.UpdateDataAdapter( this.dtAbsence );
			}
			else
			{
				/*
				 * Towa e pri update
				*/	
				
				DataRow row = this.dtAbsence.Rows.Find( this.textBoxAbsenceReason.Tag );
				if( row != null )
				{
					package.ID2 = int.Parse( this.textBoxAbsenceReason.Tag.ToString());
					row["CountDays"] = package.CountDays;
					row["CurrentYearPlan"] = package.CurrentYearPlan;

					row["CurrentYearRest"] = package.CurrentYearRest;
					row["CurrentYearUsed"] = package.CurrentYearUsed;
					row["FromDate"] = package.FromDate;

					row["LastYearPlan"] = package.LastYearPlan;
					row["LastYearRest"] = package.LastYearRest;

					row["LastYearUsed"] = package.LastYearUsed;
					row["NumberOrder"] = package.NumberOrder;
					row["OrderFromDate"] = package.OrderFromDate;
					row["Reason"] = package.Reason;
					row["ToDate"] = package.ToDate;
					row["TypeAbsence"] = package.TypeAbsence;
					row["UnpayedHoliday"] = package.UnpayedHoliday;

					this.absenceAction.UpdatePenalty( package );
				}
			}
			
			this.Refresh();
			this.ControlReadOnly( true, "Absence" );
			this.EnableButtons( true, true, false, true, "Absence" );
			IsAbsenceEdit = false;
		}

		private void buttonAbsenceDelete_Click(object sender, System.EventArgs e)
		{
			if( this.dataGridAbsence.VisibleRowCount >= 1 )
			{
				if( MessageBox.Show( this, "Сигурни ли сте че искате да изтриете наказанието " + this.dataGridAbsence[ this.dataGridAbsence.CurrentRowIndex, 2 ].ToString(), "Изтриване", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					this.absenceAction.DeleteRow( int.Parse(this.dataGridAbsence[ this.dataGridAbsence.CurrentRowIndex, 0 ].ToString()), int.Parse(this.dataGridAbsence[ this.dataGridAbsence.CurrentRowIndex, 1 ].ToString()));
					this.RefreshAbsenceDataSource( false );
					this.EnableButtons( true, true, false, true, "Absence" );
				}
			}
		}

		private void formPersonalData_PositionChanged(object sender, EventArgs e)
		{
			int index = comboBoxAbsenceTypeAbsence.FindString( comboBoxAbsenceTypeAbsence.Tag.ToString() );
			if( index > -1 )
			{
				comboBoxAbsenceTypeAbsence.SelectedIndex = index;
			}
		}
		private void formPersonalData_PositionChangedAssignment(object sender, EventArgs e)
		{
			int index = comboBoxLevel1.FindString( comboBoxLevel1.Tag.ToString() );
			if( index > -1 )
			{
				comboBoxLevel1.SelectedIndex = index;
			}
			index = 0;

			index = comboBoxLevel2.FindString( comboBoxLevel2.Tag.ToString() );
			if( index > -1 )
			{
				comboBoxLevel2.SelectedIndex = index;
			}
			index = 0;

			index = comboBoxLevel3.FindString( comboBoxLevel3.Tag.ToString() );
			if( index > -1 )
			{
				comboBoxLevel3.SelectedIndex = index;
			}
			index = 0;

			index = comboBoxPosition.FindString( comboBoxPosition.Tag.ToString() );
			if( index > -1 )
			{
				comboBoxPosition.SelectedIndex = index;
			}
			index = 0;

			index = comboBoxContract.FindString( comboBoxContract.Tag.ToString() );
			if( index > -1 )
			{
				comboBoxContract.SelectedIndex = index;
			}
			index = 0;

			index = comboBoxWorkTime.FindString( comboBoxWorkTime.Tag.ToString() );
			if( index > -1 )
			{
				comboBoxWorkTime.SelectedIndex = index;
			}
			index = 0;

			index = comboBoxAssignReason.FindString( comboBoxAssignReason.Tag.ToString() );
			if( index > -1 )
			{
				comboBoxAssignReason.SelectedIndex = index;
			}
			index = 0;

			index = comboBoxStaff.FindString( comboBoxStaff.Tag.ToString() );
			if( index > -1 )
			{
				comboBoxStaff.SelectedIndex = index;
			}
			index = 0;



		}
		

		private void buttonAssignmentEdit_Click(object sender, System.EventArgs e)
		
		{
			if( this.dataGridAssignment.VisibleRowCount > 0 )
			{
				IsAssignmentEdit = true;
				this.EnableButtons( false, true, true, true, "Assignment");
				this.ControlReadOnly( false, "Assignment" );
			}
		}

		private void buttonAssignmentDelete_Click(object sender, System.EventArgs e)
		{
			if( this.dataGridAssignment.VisibleRowCount >= 1 )
			{
				if( MessageBox.Show( this, "Сигурни ли сте че искате да премахнете назначението " + this.dataGridAssignment[ this.dataGridAssignment.CurrentRowIndex, 2 ].ToString(), "Изтриване", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					this.assignmentAction.DeleteRow( int.Parse(this.dataGridAssignment[ this.dataGridAssignment.CurrentRowIndex, 0 ].ToString()), int.Parse(this.dataGridAssignment[ this.dataGridAssignment.CurrentRowIndex, 1 ].ToString()));
					this.RefreshAssignmentDataSource( false );
					this.EnableButtons( true, true, false, true, "Assignment" );
				}
			}
		}

		private void buttonAssignmentSave_Click(object sender, System.EventArgs e)
		{
			DataLayer.AssignmentPackage package = new DataLayer.AssignmentPackage();
			this.ValidateAssignment( package );
			bool IsUnvalid = false;
			if( !IsAssignmentEdit )
			{
				// Towa e pri dobawqne na now red
				if( this.IsAssignment )
				{
					
					package.ID2 = 0;
					package.ID3 = this.GenerateUniqueID();

					if( this.dataGridAssignment.VisibleRowCount > 0 )
					{
						MessageBox.Show( "Не може да има повече от едно назначение" );
						IsUnvalid = true;

					}

				}
				else
				{
					package.ID2 = this.GenerateUniqueID();
					package.ID3 = this.GenerateUniqueID();
				}
				if( !IsUnvalid )
				{
					this.AddAssignmentPackageToTable( package );
					this.assignmentAction.UpdateDataAdapter( this.dtAssignment );
				}
			}
			else
			{
				/*
				 * Towa e pri update
				*/	
				
				DataRow row = this.dtAssignment.Rows.Find( this.numBoxNumberKids.Tag );
				if( row != null )
				{
					package.ID2 = int.Parse( this.numBoxNumberKids.Tag.ToString());

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
					row["NKIDCode"] = package.NKIDCode;
					row["NKIDName"] = package.NKIDName;
					row["NumberKids"] = package.NumberKids;
					row["Position"] = package.Position;
					row["SalaryAddon"] = package.SalaryAddon;
					row["Staff"] = package.Staff;
					row["WorkTime"] = package.WorkTime;

					this.assignmentAction.UpdateAssignment( package );
				}
			}
			
			this.Refresh();
			this.ControlReadOnly( true, "Assignment" );
			this.EnableButtons( true, true, false, true, "Assignment" );
			IsAssignmentEdit = false;
		}

		private void buttonPrintD_Click(object sender, System.EventArgs e)
		{
			PrintDoc("Test.rtf");
		}

		public void PrintDoc(String DocName)
		{
			RichTextBox Rt = new RichTextBox();
			RichTextBox Rt2 = new RichTextBox();

			Rt.LoadFile(DocName);
			Rt.Rtf = Rt.Rtf.Replace("<1>",this.egn.ToString());
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
			Rt.Rtf = Rt.Rtf.Replace("<23>",this.comboBoxEmployeStatus.Text);
			Rt.Rtf = Rt.Rtf.Replace("<24>",this.comboBoxCategory.Text);
			Rt.Rtf = Rt.Rtf.Replace("<25>",this.dateTimePickerPostypilNa.Text);
			Rt.Rtf = Rt.Rtf.Replace("<26>",this.textBoxWorkExpiriance.Text);

			Rt.Rtf = Rt.Rtf.Replace("<27>",this.comboBoxLevel1.Text);
			Rt.Rtf = Rt.Rtf.Replace("<28>",this.comboBoxLevel2.Text);
			Rt.Rtf = Rt.Rtf.Replace("<29>",this.comboBoxLevel3.Text);
			Rt.Rtf = Rt.Rtf.Replace("<30>",this.comboBoxPosition.Text);
			Rt.Rtf = Rt.Rtf.Replace("<31>",this.comboBoxContract.Text);
			Rt.Rtf = Rt.Rtf.Replace("<32>",this.comboBoxWorkTime.Text);
			Rt.Rtf = Rt.Rtf.Replace("<33>",this.dateTimePickerAssignedAt.Text);
			Rt.Rtf = Rt.Rtf.Replace("<34>",this.comboBoxAssignReason.Text);
			Rt.Rtf = Rt.Rtf.Replace("<35>",this.comboBoxStaff.Text);
			Rt.Rtf = Rt.Rtf.Replace("<36>",this.textBoxContractNumber.Text);
			Rt.Rtf = Rt.Rtf.Replace("<37>",this.dateTimePickerContractExpiry.Text);
			Rt.Rtf = Rt.Rtf.Replace("<38>",this.numBoxNumberKids.Text);
			Rt.Rtf = Rt.Rtf.Replace("<39>",this.numBoxBaseSalary.Text);
			Rt.Rtf = Rt.Rtf.Replace("<40>",this.textBoxSalaryAddon.Text);
			Rt.Rtf = Rt.Rtf.Replace("<41>",this.textBoxClassPercent.Text);
			Rt.Rtf = Rt.Rtf.Replace("<42>",this.textBoxNKIDName.Text);
			Rt.Rtf = Rt.Rtf.Replace("<43>",this.textBoxNKIDCode.Text);

			Rt.Rtf = Rt.Rtf.Replace("<44>",this.dateTimePickerAbsenceFromData.Text);
			Rt.Rtf = Rt.Rtf.Replace("<45>",this.dateTimePickerAbsenceToData.Text);
			Rt.Rtf = Rt.Rtf.Replace("<46>",this.numBoxAbsenceDays.Text);
			Rt.Rtf = Rt.Rtf.Replace("<47>",this.comboBoxAbsenceTypeAbsence.Text);
			Rt.Rtf = Rt.Rtf.Replace("<48>",this.textBoxAbsenceReason.Text);
			Rt.Rtf = Rt.Rtf.Replace("<49>",this.textBoxAbsenceNumberOrder.Text);
			Rt.Rtf = Rt.Rtf.Replace("<50>",this.dateTimePickerAbsenceOrderFormData.Text);
			Rt.Rtf = Rt.Rtf.Replace("<51>",this.numericUpDown1.Value.ToString());
			Rt.Rtf = Rt.Rtf.Replace("<52>",this.numBoxAbsenceLastYearPlan.Text);
			Rt.Rtf = Rt.Rtf.Replace("<53>",this.numBoxAbsenceLastYearUsed.Text);
			Rt.Rtf = Rt.Rtf.Replace("<54>",this.numBoxAbsenceLastYearRest.Text);
			Rt.Rtf = Rt.Rtf.Replace("<55>",this.numBoxAbsenceUnpayedHoliday.Text);
			Rt.Rtf = Rt.Rtf.Replace("<56>",this.numBoxAbsenceCurrentYearPlan.Text);
			Rt.Rtf = Rt.Rtf.Replace("<57>",this.numBoxAbsenceCurrentYearUsed.Text);
			Rt.Rtf = Rt.Rtf.Replace("<58>",this.numBoxAbsenceCurrentYearRest.Text);

			Rt.Rtf = Rt.Rtf.Replace("<59>",this.dateTimePickerPenaltyDate.Text);
			Rt.Rtf = Rt.Rtf.Replace("<60>",this.textBoxPenaltyReason.Text);
			Rt.Rtf = Rt.Rtf.Replace("<61>",this.numBoxPenaltyOrder.Text);
			Rt.Rtf = Rt.Rtf.Replace("<62>",this.dateTimePenaltyFormDate.Text);

			Rt.Rtf = Rt.Rtf.Replace("<63>",this.textBoxNotes.Text);

			DataRow Row = this.mainform.nomenclaatureData.AdminTable.Rows[0];
			string Str;
			Str = (string) Row["firmname"];

            Rt.Rtf = Rt.Rtf.Replace("<64>", (string) Row["firmname"]  );
			//Rt.Rtf = Rt.Rtf.Replace("<64>", Str.c );
			//Convert(System.Text.Encoding.ASCII, System.Text.Encoding.Unicode, Row["firmname"], 0, Row["firmname"] )
//			Rt.Rtf = Rt.Rtf.Replace("<64>", (string) Encoding.Convert(System.Text.Encoding.ASCII, System.Text.Encoding.Unicode, (byte[]) Row["firmname"]) );
			//Rt.Rtf = Rt.Rtf.Replace("<64>", Str.
			Rt.Rtf = Rt.Rtf.Replace("<65>", (string) Row["type"]);
			Rt.Rtf = Rt.Rtf.Replace("<66>", (string) Row["kind"]);
			Rt.Rtf = Rt.Rtf.Replace("<67>", (string) Row["region"]);
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
			Rt.Rtf = Rt.Rtf.Replace("<86>", (string) Row["bancaccount"]);
			Rt.Rtf = Rt.Rtf.Replace("<87>", (string) Row["bankcode"]);

			Rt.SaveFile("Temp.rtf");
			System.Diagnostics.Process.Start( "wordpad.exe", "Temp.Rtf" );
		}

	}
}
