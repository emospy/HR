using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.IO;
using HRDataLayer;
using Word;
using System.Collections.Generic;
using DataLayer;
using System.Windows.Forms.Integration;


namespace HR
{
	/// <summary>
	/// Required designer variable.
	/// </summary>word
	public class formPersonalData : System.Windows.Forms.Form
	{
		private DataView vueDirection, vueDepartment, vueSector, vuePosition, vueAdministration, vueAssignment, vueNotes, vuePenalty;
		private DataTable dtTree;
		private DataTable dtPosition, dtAttestations, dtEducations, dtLevel1, dtLevel2, dtLevel3, dtLevel4, dtControlLabels, dtLanguages;
		private DataViewRowState dvrs;
		private int parent, positionID, oldPositionID, nodeID;
		private mainForm mainform;
		private string User;
		private string Year;
		private DataAction dataAdapter;
		private static object missing = System.Reflection.Missing.Value;
		private static object vk_false = false;
		private static object vk_true = true;
		private bool GridSelect;
		private int idAssignment;

		bool IsAssignmentEdit = false;
		bool IsAssignment = true;
		bool IsFiredEdit = false;

		DataTable dtAssignment = new DataTable();

		bool IsAbsenceEdit = false;
		DataTable dtAbsence = new DataTable();

		DataTable dtComboPosiiton = new DataTable(); //This table is made according a requirement 

		DataTable dtYearHoliday = new DataTable();

		DataTable dtPenalty = new DataTable();

		DataTable dtNotes = new DataTable();

		DataTable dtFired = new DataTable();
		DataTable dtRang = new DataTable();
		DataTable dtCards = new DataTable();

		bool PersonalDataChangedValue = false;  //Ако не сме правили промени по личните данни на лицето тази променлива ще остане фалсе и няма да се прави обръщение към базата данни при натискане на бутон запис

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
			FirePerson,
			AddFired,
			EditFired,
			AddAttestation,
			EditAttestation,
			AddEducation,
			EditEducation,
			AddNote,
			EditNotes,
			AddRang,
			EditRang,
			AddCard,
			EditCard,
		}
		Operations Op;  //Пази информация за текущата операция

		enum LockButtons  // Описание на прозорците на които може да се отключват и заключват бутони. Въвел съм го за по-удобно, за да не се пишат сртингове при извикването на функциите
		{
			Penalty = 1,
			Absence,
			Assignment,
			Notes,
			Fired,
			Attestation,
			Education,
			Rang,
			Card
		}

		#region Control_List
		private System.Windows.Forms.Button buttonОК;
		private System.Windows.Forms.Button buttonCancel;
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
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label labelKwartal;
		private System.Windows.Forms.TextBox textBoxKwartal;
		private System.Windows.Forms.Label labelNaselenoMqsto;
		private System.Windows.Forms.Label labelRegion;
		private BugBox.BugBox numBoxEgn;
		private System.Windows.Forms.Label labelEGN;
		private System.Windows.Forms.TextBox textBoxNames;
		private System.Windows.Forms.Label labelNames;
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
		private System.Windows.Forms.GroupBox groupBoxAbsece;
		private System.Windows.Forms.DateTimePicker dateTimePickerAbsenceOrderFormData;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.TextBox textBoxAbsenceNumberOrder;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.TextBox textBoxAbsenceAttachment7;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.ComboBox comboBoxAbsenceTypeAbsence;
		private System.Windows.Forms.Label label24;
		private BugBox.NumBox numBoxAbsenceCalendarDays;
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
		private System.Windows.Forms.TabPage tabPageAtestacii;
		private NewTabControl.NTabControl tabControlCardNew;
		private System.Windows.Forms.Button buttonAssignmentCancel;
		private System.Windows.Forms.Button buttonAbsencePrint;
		private System.Windows.Forms.Button buttonAbsenceCancel;
		private System.Windows.Forms.Button buttonPenaltyCancel;
		private System.Windows.Forms.DateTimePicker dateTimePickerPenaltyOrderDate;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label labelPenaltyReason;
		private System.Windows.Forms.Label labelPenalty;
		private System.Windows.Forms.DateTimePicker dateTimePickerPenaltyFromDate;
		private System.Windows.Forms.GroupBox groupBoxAbsenceGrid;
		private System.Windows.Forms.GroupBox groupBoxPenaltyGrid;
		private System.Windows.Forms.GroupBox groupBoxAssignmentGrid;
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
		private System.Windows.Forms.Label labelMilitaryStatus;
		private System.Windows.Forms.ComboBox comboBoxMilitaryStatus;
		private System.Windows.Forms.Label labelScience;
		private System.Windows.Forms.ComboBox comboBoxScience;
		private System.Windows.Forms.Label labelScienceLevel;
		private System.Windows.Forms.Label labelMilitaryRang;
		private System.Windows.Forms.ComboBox comboBoxMilitaryRang;
		private System.Windows.Forms.ComboBox comboBoxScienceLevel;
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
		private System.Windows.Forms.Label labelLanguage;
		private System.Windows.Forms.Label labelSpecialSkills;
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
		private System.Windows.Forms.ComboBox comboBoxAbsenceForYear;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.ComboBox comboBoxSpecialSkills;
		private System.Windows.Forms.ComboBox comboBoxPenaltyReason;
		private System.Windows.Forms.DateTimePicker dateTimePickerPenaltyToDate;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.ComboBox comboBoxTypePenalty;
		private System.Windows.Forms.TabPage tabPageFired;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.ComboBox comboBoxFiredReason;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.DateTimePicker dateTimePickerFiredFromDate;
		private System.Windows.Forms.Button buttonFiredPrint;
		private System.Windows.Forms.Button buttonFiredCancel;
		private System.Windows.Forms.Button buttonFiredDelete;
		private System.Windows.Forms.Button buttonFiredSave;
		private System.Windows.Forms.Button buttonFiredEdit;
		private System.Windows.Forms.Button buttonFiredNew;
		private System.Windows.Forms.Button buttonFire;
		private System.Windows.Forms.GroupBox groupBoxFired;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.ComponentModel.IContainer components;
		private BugBox.NumBox numBoxNumHoliday;
		private System.Windows.Forms.Label label49;
		private System.Windows.Forms.DateTimePicker dateTimePickerTestPeriod;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.DateTimePicker dateTimePickerContractDate;
		private System.Windows.Forms.Button buttonNomenkEducation;
		private System.Windows.Forms.Button buttonNomenkScienceLevel;
		private System.Windows.Forms.Button buttonNomenkFamilyStatus;
		private System.Windows.Forms.Button buttonNomenkScienceTitle;
		private System.Windows.Forms.Button buttonLanguageEdit;
		private System.Windows.Forms.Button buttonNomenkMilitaryRang;
		private System.Windows.Forms.Label label50;
		private BugBox.NumBox numBoxAddNumHoliday;
		private System.Windows.Forms.TextBox textBoxSpeciality;
		private System.Windows.Forms.ComboBox comboBoxReceivedAddon;
		private System.Windows.Forms.Label label51;
		private System.Windows.Forms.Label label52;
		private System.Windows.Forms.Button buttonPenaltyPrint;
		private System.Windows.Forms.ComboBox comboBoxRang;
		private System.Windows.Forms.Label label53;
		private System.Windows.Forms.Button buttonSelectPosition;
		private System.Windows.Forms.TabPage tabPageCharacteristics;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.TextBox textBoxNKPClass;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.Label label44;
		private System.Windows.Forms.TextBox textBoxRequirements;
		private System.Windows.Forms.Label label47;
		private System.Windows.Forms.TextBox textBoxCompetence;
		private System.Windows.Forms.Label label48;
		private System.Windows.Forms.TextBox textBoxBasicResponsibilities;
		private System.Windows.Forms.Label label54;
		private System.Windows.Forms.Label label55;
		private System.Windows.Forms.TextBox textBoxBasicDuties;
		private System.Windows.Forms.TextBox textBoxConnections;
		private System.Windows.Forms.TextBox textBoxNKPCode2;
		private System.Windows.Forms.Label label56;
		private System.Windows.Forms.ComboBox comboBoxClothesMoney;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.Button buttonNomenklatureSpecialSkills;
		private System.Windows.Forms.TextBox textBoxOther;
		private System.Windows.Forms.Label label57;
		private System.Windows.Forms.Button buttonPenaltyReason;
		private System.Windows.Forms.Button buttonTypePenalty;
		private System.Windows.Forms.GroupBox groupBox7;
		private System.Windows.Forms.TextBox textBoxTown;
		private System.Windows.Forms.TextBox textBoxCountry;
		private System.Windows.Forms.TextBox textBoxRegion;
		private System.Windows.Forms.Button buttonatestationsCancel;
		private System.Windows.Forms.Button buttonAtestationsPrint;
		private System.Windows.Forms.Button buttonatestationsDelete;
		private System.Windows.Forms.Button buttonAtestationsSave;
		private System.Windows.Forms.Button buttonAtestationsEdit;
		private System.Windows.Forms.Button buttonAtestationsAdd;
		private System.Windows.Forms.Button buttonExpCalculator;
		private System.Windows.Forms.GroupBox groupBoxAttestationRegister;
		private System.Windows.Forms.DateTimePicker dateTimePickerWorkPlan;
		private System.Windows.Forms.ComboBox comboBoxTotalMark;
		private System.Windows.Forms.Label label58;
		private BugBox.NumBox numBoxYear;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label64;
		private System.Windows.Forms.Label label69;
		private System.Windows.Forms.CheckBox checkBoxhasWorkPlan;
		private System.Windows.Forms.CheckBox checkBoxMiddleMeetingDate;
		private System.Windows.Forms.DateTimePicker dateTimePickerFinalMeeting;
		private System.Windows.Forms.TextBox textBoxControllingBoss;
		private System.Windows.Forms.CheckBox checkBoxRang;
		private System.Windows.Forms.DateTimePicker dateTimePickerRangDate;
		private System.Windows.Forms.CheckBox checkBoxPosition;
		private System.Windows.Forms.DateTimePicker dateTimePickerPositionDate;
		private System.Windows.Forms.GroupBox groupBox8;
		private System.Windows.Forms.GroupBox groupBox9;
		private System.Windows.Forms.Button buttonRangUpdateFile;
		private System.Windows.Forms.Button buttonPositionFile;
		private System.Windows.Forms.TextBox textBoxAttestationFile;
		private System.Windows.Forms.TextBox textBoxRetortFile;
		private System.Windows.Forms.TextBox textBoxPositionFile;
		private System.Windows.Forms.TextBox textBoxRangUpdateFile;
		private System.Windows.Forms.Button buttonAttestationFileView;
		private System.Windows.Forms.Button buttonRangUpdateFileView;
		private System.Windows.Forms.Button buttonRetortFileView;
		private System.Windows.Forms.Button buttonPositionFileView;
		private System.Windows.Forms.Button buttonAttestationFile;
		private System.Windows.Forms.Label label65;
		private System.Windows.Forms.Label label70;
		private System.Windows.Forms.Button buttonRetortFile;
		private System.Windows.Forms.Label label71;
		private System.Windows.Forms.Label label72;
		private System.Windows.Forms.TextBox textBoxAttestationsOther;
		private System.Windows.Forms.Label label59;
		private System.Windows.Forms.Label label61;
		private System.Windows.Forms.CheckBox checkBoxFinalMeeting;
		private System.Windows.Forms.TextBox textBoxBoss;
		private System.Windows.Forms.CheckBox checkBoxObjection;
		private System.Windows.Forms.CheckBox checkBoxHasTraining;
		private System.Windows.Forms.DateTimePicker dateTimePickerMiddleMeetingDate;
		private System.Windows.Forms.ComboBox comboBoxNewRang;
		private System.Windows.Forms.DateTimePicker dateTimePickerObjectionDate;
		private System.Windows.Forms.TextBox textBoxTrainingData;
		private System.Windows.Forms.Button buttonNomenklatureRang;
		private System.Windows.Forms.Button buttonReasonFired;
		private System.Windows.Forms.Label label60;
		private System.Windows.Forms.Label labelVacationLeft;
		private System.Windows.Forms.TextBox textBoxFireOrder;
		private System.Windows.Forms.Label label62;
		private System.Windows.Forms.TabPage tabPageEducation;
		private System.Windows.Forms.GroupBox groupBoxEducationData;
		private System.Windows.Forms.GroupBox groupBoxEducationHistory;
		private System.Windows.Forms.Label label63;
		private System.Windows.Forms.Button buttonReasonAssignment;
		private System.Windows.Forms.Label label66;
		private System.Windows.Forms.Label label67;
		private System.Windows.Forms.Label label68;
		private System.Windows.Forms.Label label73;
		private System.Windows.Forms.Label label74;
		private System.Windows.Forms.Label label75;
		private System.Windows.Forms.Button buttonEducationCatalog;
		private System.Windows.Forms.TextBox textBoxEducationCode;
		private BugBox.NumBox numBoxEducationDays;
		private BugBox.NumBox numBoxEducationHours;
		private BugBox.NumBox numBoxEducationPrice;
		private System.Windows.Forms.TextBox textBoxEducationCertificate;
		private System.Windows.Forms.Label label76;
		private System.Windows.Forms.Label label77;
		private System.Windows.Forms.Label label78;
		private System.Windows.Forms.Label label79;
		private System.Windows.Forms.Button buttonEducationCancel;
		private System.Windows.Forms.Button buttonEducationDelete;
		private System.Windows.Forms.Button buttonEducationSave;
		private System.Windows.Forms.Button buttonEducationAdd;
		private System.Windows.Forms.Button buttonEducationPrint;
		private System.Windows.Forms.Button buttonEducationEdit;
		private System.Windows.Forms.TextBox textBoxEducationTheme;
		private System.Windows.Forms.TextBox textBoxEducationArea;
		private System.Windows.Forms.DateTimePicker dateTimePickerEducationToDate;
		private System.Windows.Forms.DateTimePicker dateTimePickerEducationFromDate;
		private System.Windows.Forms.TextBox textBoxEducationOrganisation;
		private System.Windows.Forms.TextBox textBoxEducationPlace;
		private System.Windows.Forms.Label label80;
		private System.Windows.Forms.ComboBox comboBoxEGN;
		private System.Windows.Forms.DateTimePicker dateTimePickerBirthDate;
		private System.Windows.Forms.TextBox textBoxEngName;
		private System.Windows.Forms.Label label81;
		private Label labelCurrentAddress;
		private TextBox textBoxCurrentAddress;
		private Label label83;
		private BugBox.NumBox numBoxBruto;
		private ComboBox comboBoxNotesFilter;
		private GroupBox groupBoxNotesGrid;
		private GroupBox groupBoxNotesFilter;
		private GroupBox groupBoxNotes;
		private Button buttonNotesPrint;
		private Button buttonNotesCancel;
		private Button buttonNotesDelete;
		private Button buttonNotesSave;
		private Button buttonNotesEdit;
		private Button buttonNotesAdd;
		private TextBox textBoxNoteText;
		private DateTimePicker dateTimePickerNotes;
		private ComboBox comboBoxNoteType;
		private Label label84;
		private Label label82;
		private Label label1;
		private Label label85;
		private TextBox textBoxNoteTypeDocument;
		private Label label86;
		private Label label87;
		private ComboBox comboBoxTutorName;
		private ComboBox comboBoxTutorAbsenceReason;
		private Label labelWorkBook;
		private Label labelOther4;
		private Label labelOther3;
		private Label labelOther2;
		private Label labelOther1;
		private TextBox textBoxWorkBook;
		private TextBox textBoxOther4;
		private TextBox textBoxOther3;
		private TextBox textBoxOther2;
		private TextBox textBoxOther1;
		private Button buttonAssignmentExcel;
		private TextBox textBoxPenaltyNumberOrder;
		private DataGridView dataGridViewAssignment;
		private DataGridView dataGridViewAbsence;
		private DataGridView dataGridViewYears;
		private DataGridView dataGridViewPenalties;
		private DataGridView dataGridViewFired;
		private DataGridView dataGridViewNotes;
		private DataGridView dataGridViewAttestations;
		private DataGridView dataGridViewEducations;
		private RadioButton radioButtonPenalties;
		private RadioButton radioButtonBonuses;
		private Button buttonAbsenceExcel;
		private Button buttonPenaltiesExcel;
		private Button buttonFiredExcel;
		private Button buttonHistoryExcel;
		private Button buttonAttestationsExcel;
		private Button buttonEducationsExcel;
		private DataGridView dataGridViewLanguages;
		private Label labelWorkBookDate;
		private DateTimePicker dateTimePickerWorkBook;
		private Button buttonLanguageLevel;
		private Button buttonForeignLanguages;
		private Button buttonLanguageDelete;
		private Button buttonLanguageAdd;
		private TextBox textBoxTelephone;
		private Button buttonAttached;
		private TabPage tabPageMilitaryRang;
		private Button buttonRangExcel;
		private Button buttonRangNew;
		private Button buttonRangPrint;
		private Button buttonRangCancel;
		private Button buttonRangDelete;
		private Button buttonRangSave;
		private Button buttonRangEdit;
		private GroupBox groupBoxRangHistory;
		private DataGridView dataGridViewRang;
		private GroupBox groupBox15;
		private RadioButton radioButton3;
		private RadioButton radioButton4;
		private GroupBox groupBox12;
		private DataGridView dataGridView2;
		private GroupBox groupBox13;
		private Label label94;
		private Label label95;
		private Label label96;
		private Label label97;
		private Label label98;
		private Label label99;
		private Label label88;
		private TextBox textBoxRangOrderNumber;
		private Label label89;
		private Label label90;
		private Label label91;
		private DateTimePicker dateTimePickerRangValidFrom;
		private DateTimePicker dateTimePickerRangOrderDate;
		private ComboBox comboBoxNSORang;
		private Button buttonMilitaryAssignemntLink;
		private Label label106;
		private TextBox textBoxAbsenceNAPDocs;
		private Label label105;
		private TextBox textBoxAbsenceReasons;
		private Label label104;
		private Label label103;
		private TextBox textBoxAbsenceNotes;
		private Label label102;
		private ComboBox comboBoxAbsenceSicknessDuration;
		private DateTimePicker dateTimePickerAbsenceSicknessIssuedAtDate;
		private Label label101;
		private Label label100;
		private TextBox textBoxAbsenceAdditionalDocs;
		private Label label93;
		private BugBox.NumBox numBoxAbsenceWorkDays;
		private Label label92;
		private TextBox textBoxAbsenceDec39;
		private TextBox textBoxAbsenceMKB;
		private Label label107;
		private TextBox textBoxAbsenceSicknessNumber;
		private Label label108;
		private ComboBox comboBoxNSODegree;
		private Button buttonRangdegreeNomenklature;
		private Button buttonRangNomenklature;
		private DateTimePicker dateTimePickerFireOdredDate;
		private Label label109;
		private Label label110;
		private DateTimePicker dateTimePickerPCardExpiry;
		private Button button1;
		private ComboBox comboBoxEkdaDegree;
		private Label label111;
		private Button buttonFiredRestore;
		private TextBox textBoxResponsibleFor;
		private Label label112;
		private TabPage tabPageCards;
		private Button buttonCardExcel;
		private Button buttonCardNew;
		private Button buttonCardPrint;
		private Button buttonCardCancel;
		private Button buttonCardDelete;
		private Button buttonCardSave;
		private Button buttonCardEdit;
		private GroupBox groupBoxCardHistory;
		private GroupBox groupBox10;
		private Label label115;
		private Label label117;
		private DateTimePicker dateTimePickerCardIssue;
		private ComboBox comboBoxCardMilitaryRang;
		private DataGridView dataGridViewCards;
		#endregion
		private Label label118;
		private ComboBox comboBoxCardMilitaryRangEng;
		private TextBox textBoxCardSign;
		private Label label120;
		private TextBox textBoxCardSeries;
		private Label label119;
		private TextBox textBoxCardNumber;
		private Label label113;
		private Label labelTatalStaff;
		private BugBox.NumBox numBoxExpTotalD;
		private BugBox.NumBox numBoxExpTotalM;
		private BugBox.NumBox numBoxExpTotalY;
		bool IsFiredd = false;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public formPersonalData(string Identifier, mainForm main, bool IsFiredd)
		{
			try
			{
				Op = Operations.ViewPersonData;
				this.IsFiredd = IsFiredd;
				this.mainform = main;
				this.parent = Int32.Parse(Identifier);

				this.dataAdapter = new DataLayer.DataAction(this.mainform.connString);
				this.dtTree = main.nomenclaatureData.dtTreeTable;
				this.User = main.User;

				InitializeComponent();

				this.numBoxBaseSalary.IsFloat = true;
				this.numBoxMonthlyAddon.IsFloat = true;

				//comboBoxAbsenceTypeAbsence.Items.Add("Платен отпуск");
				//comboBoxAbsenceTypeAbsence.Items.Add("Неплатен отпуск");
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// Required designer variable.
		/// </summary>
		/// 
		public formPersonalData(mainForm main, bool IsFired)
		{
			try
			{
				Op = Operations.AddNewPerson;
				this.IsFiredd = IsFired;
				this.mainform = main;

				this.parent = 0;
				this.dataAdapter = new DataLayer.DataAction(this.mainform.connString);
				this.dtTree = main.nomenclaatureData.dtTreeTable;
				this.User = main.User;

				InitializeComponent();

				if (this.IsFiredd) // Ako slujitelq e uwolnen - disable na wsichki butoni
				{
					foreach (Control ctrl in this.Controls)
					{
						if (ctrl is Button)
						{
							ctrl.Enabled = false;
						}
					}
				}
				this.buttonFiredRestore.Enabled = true;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			//			if( disposing )
			//			{
			//				if(components != null)
			//				{
			//			:-*		components.Dispose();
			//				}
			//			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formPersonalData));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle73 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle74 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle75 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle76 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle77 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle78 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle79 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle80 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle81 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle82 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle83 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle84 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle85 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle86 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle87 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle88 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle89 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle90 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle91 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle92 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle93 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle94 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle95 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle96 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle97 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle98 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle99 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle100 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle101 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle102 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle103 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle104 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle105 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle106 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle107 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle108 = new System.Windows.Forms.DataGridViewCellStyle();
			this.buttonОК = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.TabPersonalInfo = new System.Windows.Forms.TabPage();
			this.textBoxResponsibleFor = new System.Windows.Forms.TextBox();
			this.label112 = new System.Windows.Forms.Label();
			this.buttonAttached = new System.Windows.Forms.Button();
			this.buttonLanguageDelete = new System.Windows.Forms.Button();
			this.buttonLanguageAdd = new System.Windows.Forms.Button();
			this.textBoxOther4 = new System.Windows.Forms.TextBox();
			this.buttonLanguageLevel = new System.Windows.Forms.Button();
			this.buttonForeignLanguages = new System.Windows.Forms.Button();
			this.dataGridViewLanguages = new System.Windows.Forms.DataGridView();
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
			this.dateTimePickerPostypilNa = new System.Windows.Forms.DateTimePicker();
			this.labelHiredAt = new System.Windows.Forms.Label();
			this.labelOther4 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.textBoxTelephone = new System.Windows.Forms.TextBox();
			this.label51 = new System.Windows.Forms.Label();
			this.comboBoxClothesMoney = new System.Windows.Forms.ComboBox();
			this.comboBoxReceivedAddon = new System.Windows.Forms.ComboBox();
			this.label56 = new System.Windows.Forms.Label();
			this.labelMilitaryStatus = new System.Windows.Forms.Label();
			this.comboBoxMilitaryStatus = new System.Windows.Forms.ComboBox();
			this.buttonNomenkMilitaryRang = new System.Windows.Forms.Button();
			this.buttonNomenkScienceTitle = new System.Windows.Forms.Button();
			this.buttonNomenkScienceLevel = new System.Windows.Forms.Button();
			this.buttonNomenkEducation = new System.Windows.Forms.Button();
			this.comboBoxSpecialSkills = new System.Windows.Forms.ComboBox();
			this.labelSpecialSkills = new System.Windows.Forms.Label();
			this.labelScience = new System.Windows.Forms.Label();
			this.comboBoxMilitaryRang = new System.Windows.Forms.ComboBox();
			this.comboBoxScienceLevel = new System.Windows.Forms.ComboBox();
			this.comboBoxScience = new System.Windows.Forms.ComboBox();
			this.labelScienceLevel = new System.Windows.Forms.Label();
			this.label36 = new System.Windows.Forms.Label();
			this.label38 = new System.Windows.Forms.Label();
			this.textBoxDiplom = new System.Windows.Forms.TextBox();
			this.comboBoxEducation = new System.Windows.Forms.ComboBox();
			this.textBoxSpeciality = new System.Windows.Forms.TextBox();
			this.label52 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.buttonNomenklatureSpecialSkills = new System.Windows.Forms.Button();
			this.buttonNomenklatureRang = new System.Windows.Forms.Button();
			this.label53 = new System.Windows.Forms.Label();
			this.comboBoxRang = new System.Windows.Forms.ComboBox();
			this.labelMilitaryRang = new System.Windows.Forms.Label();
			this.comboBoxFamilyStatus = new System.Windows.Forms.ComboBox();
			this.buttonNomenkFamilyStatus = new System.Windows.Forms.Button();
			this.label37 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label110 = new System.Windows.Forms.Label();
			this.dateTimePickerPCardExpiry = new System.Windows.Forms.DateTimePicker();
			this.labelWorkBookDate = new System.Windows.Forms.Label();
			this.dateTimePickerWorkBook = new System.Windows.Forms.DateTimePicker();
			this.textBoxWorkBook = new System.Windows.Forms.TextBox();
			this.textBoxOther3 = new System.Windows.Forms.TextBox();
			this.textBoxOther2 = new System.Windows.Forms.TextBox();
			this.textBoxOther1 = new System.Windows.Forms.TextBox();
			this.labelWorkBook = new System.Windows.Forms.Label();
			this.labelOther3 = new System.Windows.Forms.Label();
			this.labelOther2 = new System.Windows.Forms.Label();
			this.labelOther1 = new System.Windows.Forms.Label();
			this.labelCurrentAddress = new System.Windows.Forms.Label();
			this.textBoxCurrentAddress = new System.Windows.Forms.TextBox();
			this.textBoxEngName = new System.Windows.Forms.TextBox();
			this.label81 = new System.Windows.Forms.Label();
			this.dateTimePickerBirthDate = new System.Windows.Forms.DateTimePicker();
			this.comboBoxEGN = new System.Windows.Forms.ComboBox();
			this.label80 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.comboBoxSex = new System.Windows.Forms.ComboBox();
			this.numBoxPcCard = new BugBox.NumBox();
			this.labelJKkwartal = new System.Windows.Forms.Label();
			this.labelPublishedByy = new System.Windows.Forms.Label();
			this.labelPublishedBy = new System.Windows.Forms.Label();
			this.textBoxPublishedFrom = new System.Windows.Forms.TextBox();
			this.labelKwartal = new System.Windows.Forms.Label();
			this.textBoxKwartal = new System.Windows.Forms.TextBox();
			this.labelNaselenoMqsto = new System.Windows.Forms.Label();
			this.labelRegion = new System.Windows.Forms.Label();
			this.numBoxEgn = new BugBox.BugBox();
			this.labelEGN = new System.Windows.Forms.Label();
			this.textBoxNames = new System.Windows.Forms.TextBox();
			this.labelNames = new System.Windows.Forms.Label();
			this.labelCountry = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxBornTown = new System.Windows.Forms.TextBox();
			this.dateTimePickerPCCardPublished = new System.Windows.Forms.DateTimePicker();
			this.textBoxTown = new System.Windows.Forms.TextBox();
			this.textBoxCountry = new System.Windows.Forms.TextBox();
			this.textBoxRegion = new System.Windows.Forms.TextBox();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.buttonDeletePicture = new System.Windows.Forms.Button();
			this.buttonPicture = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.textBoxOther = new System.Windows.Forms.TextBox();
			this.labelLanguage = new System.Windows.Forms.Label();
			this.buttonLanguageEdit = new System.Windows.Forms.Button();
			this.label57 = new System.Windows.Forms.Label();
			this.tabPageAssignment = new System.Windows.Forms.TabPage();
			this.comboBoxEkdaDegree = new System.Windows.Forms.ComboBox();
			this.label111 = new System.Windows.Forms.Label();
			this.buttonAssignmentExcel = new System.Windows.Forms.Button();
			this.label86 = new System.Windows.Forms.Label();
			this.label87 = new System.Windows.Forms.Label();
			this.comboBoxTutorName = new System.Windows.Forms.ComboBox();
			this.comboBoxTutorAbsenceReason = new System.Windows.Forms.ComboBox();
			this.numBoxBruto = new BugBox.NumBox();
			this.buttonReasonAssignment = new System.Windows.Forms.Button();
			this.labelLevel2 = new System.Windows.Forms.Label();
			this.buttonSelectPosition = new System.Windows.Forms.Button();
			this.radioButtonAssignment = new System.Windows.Forms.RadioButton();
			this.dateTimePickerContractDate = new System.Windows.Forms.DateTimePicker();
			this.label14 = new System.Windows.Forms.Label();
			this.numBoxNumHoliday = new BugBox.NumBox();
			this.comboBoxYearlyAddon = new System.Windows.Forms.ComboBox();
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
			this.label12 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.labelLevel4 = new System.Windows.Forms.Label();
			this.textBoxContractNumber = new System.Windows.Forms.TextBox();
			this.comboBoxAssignReason = new System.Windows.Forms.ComboBox();
			this.comboBoxContract = new System.Windows.Forms.ComboBox();
			this.comboBoxPosition = new System.Windows.Forms.ComboBox();
			this.comboBoxLevel4 = new System.Windows.Forms.ComboBox();
			this.comboBoxLevel3 = new System.Windows.Forms.ComboBox();
			this.comboBoxLevel2 = new System.Windows.Forms.ComboBox();
			this.groupBoxAssignmentGrid = new System.Windows.Forms.GroupBox();
			this.dataGridViewAssignment = new System.Windows.Forms.DataGridView();
			this.labelLevel3 = new System.Windows.Forms.Label();
			this.dateTimePickerTestPeriod = new System.Windows.Forms.DateTimePicker();
			this.label49 = new System.Windows.Forms.Label();
			this.numBoxAddNumHoliday = new BugBox.NumBox();
			this.buttonExpCalculator = new System.Windows.Forms.Button();
			this.numBoxBaseSalary = new BugBox.NumBox();
			this.numBoxMonthlyAddon = new BugBox.NumBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label83 = new System.Windows.Forms.Label();
			this.label46 = new System.Windows.Forms.Label();
			this.label45 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label41 = new System.Windows.Forms.Label();
			this.label50 = new System.Windows.Forms.Label();
			this.tabPageAbsence = new System.Windows.Forms.TabPage();
			this.buttonAbsenceExcel = new System.Windows.Forms.Button();
			this.buttonAbsenceCancel = new System.Windows.Forms.Button();
			this.buttonAbsencePrint = new System.Windows.Forms.Button();
			this.buttonAbsenceDelete = new System.Windows.Forms.Button();
			this.buttonAbsenceSave = new System.Windows.Forms.Button();
			this.buttonAbsenceEdit = new System.Windows.Forms.Button();
			this.buttonAbsenceAdd = new System.Windows.Forms.Button();
			this.groupBoxAbsenceGrid = new System.Windows.Forms.GroupBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.dataGridViewYears = new System.Windows.Forms.DataGridView();
			this.labelVacationLeft = new System.Windows.Forms.Label();
			this.label60 = new System.Windows.Forms.Label();
			this.buttonHistory = new System.Windows.Forms.Button();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.dataGridViewAbsence = new System.Windows.Forms.DataGridView();
			this.groupBoxAbsece = new System.Windows.Forms.GroupBox();
			this.label107 = new System.Windows.Forms.Label();
			this.textBoxAbsenceSicknessNumber = new System.Windows.Forms.TextBox();
			this.textBoxAbsenceMKB = new System.Windows.Forms.TextBox();
			this.label106 = new System.Windows.Forms.Label();
			this.textBoxAbsenceNAPDocs = new System.Windows.Forms.TextBox();
			this.label105 = new System.Windows.Forms.Label();
			this.textBoxAbsenceReasons = new System.Windows.Forms.TextBox();
			this.label104 = new System.Windows.Forms.Label();
			this.label103 = new System.Windows.Forms.Label();
			this.textBoxAbsenceNotes = new System.Windows.Forms.TextBox();
			this.label102 = new System.Windows.Forms.Label();
			this.comboBoxAbsenceSicknessDuration = new System.Windows.Forms.ComboBox();
			this.dateTimePickerAbsenceSicknessIssuedAtDate = new System.Windows.Forms.DateTimePicker();
			this.label101 = new System.Windows.Forms.Label();
			this.label100 = new System.Windows.Forms.Label();
			this.textBoxAbsenceAdditionalDocs = new System.Windows.Forms.TextBox();
			this.label93 = new System.Windows.Forms.Label();
			this.numBoxAbsenceWorkDays = new BugBox.NumBox();
			this.label92 = new System.Windows.Forms.Label();
			this.textBoxAbsenceDec39 = new System.Windows.Forms.TextBox();
			this.label29 = new System.Windows.Forms.Label();
			this.comboBoxAbsenceForYear = new System.Windows.Forms.ComboBox();
			this.dateTimePickerAbsenceOrderFormData = new System.Windows.Forms.DateTimePicker();
			this.label28 = new System.Windows.Forms.Label();
			this.label27 = new System.Windows.Forms.Label();
			this.textBoxAbsenceNumberOrder = new System.Windows.Forms.TextBox();
			this.label26 = new System.Windows.Forms.Label();
			this.textBoxAbsenceAttachment7 = new System.Windows.Forms.TextBox();
			this.label25 = new System.Windows.Forms.Label();
			this.comboBoxAbsenceTypeAbsence = new System.Windows.Forms.ComboBox();
			this.label24 = new System.Windows.Forms.Label();
			this.numBoxAbsenceCalendarDays = new BugBox.NumBox();
			this.label23 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.dateTimePickerAbsenceToData = new System.Windows.Forms.DateTimePicker();
			this.dateTimePickerAbsenceFromData = new System.Windows.Forms.DateTimePicker();
			this.tabPagePenalty = new System.Windows.Forms.TabPage();
			this.buttonPenaltiesExcel = new System.Windows.Forms.Button();
			this.buttonPenaltyAdd = new System.Windows.Forms.Button();
			this.radioButtonPenalties = new System.Windows.Forms.RadioButton();
			this.radioButtonBonuses = new System.Windows.Forms.RadioButton();
			this.buttonPenaltyPrint = new System.Windows.Forms.Button();
			this.buttonPenaltyCancel = new System.Windows.Forms.Button();
			this.buttonPenaltyDelete = new System.Windows.Forms.Button();
			this.buttonPenaltySave = new System.Windows.Forms.Button();
			this.buttonPebaltyEdit = new System.Windows.Forms.Button();
			this.groupBoxPenaltyGrid = new System.Windows.Forms.GroupBox();
			this.dataGridViewPenalties = new System.Windows.Forms.DataGridView();
			this.groupBoxPenalty = new System.Windows.Forms.GroupBox();
			this.textBoxPenaltyNumberOrder = new System.Windows.Forms.TextBox();
			this.buttonTypePenalty = new System.Windows.Forms.Button();
			this.buttonPenaltyReason = new System.Windows.Forms.Button();
			this.label31 = new System.Windows.Forms.Label();
			this.comboBoxTypePenalty = new System.Windows.Forms.ComboBox();
			this.label30 = new System.Windows.Forms.Label();
			this.dateTimePickerPenaltyToDate = new System.Windows.Forms.DateTimePicker();
			this.comboBoxPenaltyReason = new System.Windows.Forms.ComboBox();
			this.dateTimePickerPenaltyOrderDate = new System.Windows.Forms.DateTimePicker();
			this.label21 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.labelPenaltyReason = new System.Windows.Forms.Label();
			this.labelPenalty = new System.Windows.Forms.Label();
			this.dateTimePickerPenaltyFromDate = new System.Windows.Forms.DateTimePicker();
			this.tabPageNotes = new System.Windows.Forms.TabPage();
			this.buttonHistoryExcel = new System.Windows.Forms.Button();
			this.buttonNotesPrint = new System.Windows.Forms.Button();
			this.groupBoxNotes = new System.Windows.Forms.GroupBox();
			this.label85 = new System.Windows.Forms.Label();
			this.textBoxNoteTypeDocument = new System.Windows.Forms.TextBox();
			this.label84 = new System.Windows.Forms.Label();
			this.label82 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBoxNoteType = new System.Windows.Forms.ComboBox();
			this.textBoxNoteText = new System.Windows.Forms.TextBox();
			this.dateTimePickerNotes = new System.Windows.Forms.DateTimePicker();
			this.buttonNotesCancel = new System.Windows.Forms.Button();
			this.groupBoxNotesGrid = new System.Windows.Forms.GroupBox();
			this.dataGridViewNotes = new System.Windows.Forms.DataGridView();
			this.buttonNotesDelete = new System.Windows.Forms.Button();
			this.buttonNotesSave = new System.Windows.Forms.Button();
			this.groupBoxNotesFilter = new System.Windows.Forms.GroupBox();
			this.comboBoxNotesFilter = new System.Windows.Forms.ComboBox();
			this.buttonNotesEdit = new System.Windows.Forms.Button();
			this.buttonNotesAdd = new System.Windows.Forms.Button();
			this.tabPageAtestacii = new System.Windows.Forms.TabPage();
			this.buttonAttestationsExcel = new System.Windows.Forms.Button();
			this.groupBox9 = new System.Windows.Forms.GroupBox();
			this.buttonRangUpdateFile = new System.Windows.Forms.Button();
			this.buttonPositionFile = new System.Windows.Forms.Button();
			this.textBoxAttestationFile = new System.Windows.Forms.TextBox();
			this.textBoxRetortFile = new System.Windows.Forms.TextBox();
			this.textBoxPositionFile = new System.Windows.Forms.TextBox();
			this.textBoxRangUpdateFile = new System.Windows.Forms.TextBox();
			this.buttonAttestationFileView = new System.Windows.Forms.Button();
			this.buttonRangUpdateFileView = new System.Windows.Forms.Button();
			this.buttonRetortFileView = new System.Windows.Forms.Button();
			this.buttonPositionFileView = new System.Windows.Forms.Button();
			this.buttonAttestationFile = new System.Windows.Forms.Button();
			this.label65 = new System.Windows.Forms.Label();
			this.label70 = new System.Windows.Forms.Label();
			this.buttonRetortFile = new System.Windows.Forms.Button();
			this.label71 = new System.Windows.Forms.Label();
			this.label72 = new System.Windows.Forms.Label();
			this.buttonatestationsCancel = new System.Windows.Forms.Button();
			this.buttonAtestationsPrint = new System.Windows.Forms.Button();
			this.buttonatestationsDelete = new System.Windows.Forms.Button();
			this.buttonAtestationsSave = new System.Windows.Forms.Button();
			this.buttonAtestationsEdit = new System.Windows.Forms.Button();
			this.buttonAtestationsAdd = new System.Windows.Forms.Button();
			this.groupBoxAttestationRegister = new System.Windows.Forms.GroupBox();
			this.dataGridViewAttestations = new System.Windows.Forms.DataGridView();
			this.groupBox8 = new System.Windows.Forms.GroupBox();
			this.checkBoxFinalMeeting = new System.Windows.Forms.CheckBox();
			this.comboBoxNewRang = new System.Windows.Forms.ComboBox();
			this.label59 = new System.Windows.Forms.Label();
			this.textBoxAttestationsOther = new System.Windows.Forms.TextBox();
			this.textBoxTrainingData = new System.Windows.Forms.TextBox();
			this.checkBoxHasTraining = new System.Windows.Forms.CheckBox();
			this.dateTimePickerFinalMeeting = new System.Windows.Forms.DateTimePicker();
			this.checkBoxObjection = new System.Windows.Forms.CheckBox();
			this.dateTimePickerObjectionDate = new System.Windows.Forms.DateTimePicker();
			this.dateTimePickerRangDate = new System.Windows.Forms.DateTimePicker();
			this.dateTimePickerWorkPlan = new System.Windows.Forms.DateTimePicker();
			this.checkBoxhasWorkPlan = new System.Windows.Forms.CheckBox();
			this.dateTimePickerMiddleMeetingDate = new System.Windows.Forms.DateTimePicker();
			this.checkBoxMiddleMeetingDate = new System.Windows.Forms.CheckBox();
			this.comboBoxTotalMark = new System.Windows.Forms.ComboBox();
			this.label58 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.numBoxYear = new BugBox.NumBox();
			this.textBoxControllingBoss = new System.Windows.Forms.TextBox();
			this.label64 = new System.Windows.Forms.Label();
			this.label69 = new System.Windows.Forms.Label();
			this.textBoxBoss = new System.Windows.Forms.TextBox();
			this.checkBoxPosition = new System.Windows.Forms.CheckBox();
			this.dateTimePickerPositionDate = new System.Windows.Forms.DateTimePicker();
			this.label61 = new System.Windows.Forms.Label();
			this.checkBoxRang = new System.Windows.Forms.CheckBox();
			this.tabControlCardNew = new NewTabControl.NTabControl();
			this.tabPageFired = new System.Windows.Forms.TabPage();
			this.buttonFiredRestore = new System.Windows.Forms.Button();
			this.buttonFiredExcel = new System.Windows.Forms.Button();
			this.buttonFire = new System.Windows.Forms.Button();
			this.buttonFiredPrint = new System.Windows.Forms.Button();
			this.buttonFiredCancel = new System.Windows.Forms.Button();
			this.buttonFiredDelete = new System.Windows.Forms.Button();
			this.buttonFiredSave = new System.Windows.Forms.Button();
			this.buttonFiredEdit = new System.Windows.Forms.Button();
			this.buttonFiredNew = new System.Windows.Forms.Button();
			this.groupBoxFired = new System.Windows.Forms.GroupBox();
			this.dataGridViewFired = new System.Windows.Forms.DataGridView();
			this.label32 = new System.Windows.Forms.Label();
			this.groupBox7 = new System.Windows.Forms.GroupBox();
			this.dateTimePickerFireOdredDate = new System.Windows.Forms.DateTimePicker();
			this.label109 = new System.Windows.Forms.Label();
			this.textBoxFireOrder = new System.Windows.Forms.TextBox();
			this.comboBoxFiredReason = new System.Windows.Forms.ComboBox();
			this.buttonReasonFired = new System.Windows.Forms.Button();
			this.label62 = new System.Windows.Forms.Label();
			this.dateTimePickerFiredFromDate = new System.Windows.Forms.DateTimePicker();
			this.label34 = new System.Windows.Forms.Label();
			this.tabPageCharacteristics = new System.Windows.Forms.TabPage();
			this.label33 = new System.Windows.Forms.Label();
			this.textBoxNKPCode2 = new System.Windows.Forms.TextBox();
			this.textBoxNKPClass = new System.Windows.Forms.TextBox();
			this.label35 = new System.Windows.Forms.Label();
			this.label44 = new System.Windows.Forms.Label();
			this.textBoxRequirements = new System.Windows.Forms.TextBox();
			this.label47 = new System.Windows.Forms.Label();
			this.textBoxCompetence = new System.Windows.Forms.TextBox();
			this.label48 = new System.Windows.Forms.Label();
			this.textBoxBasicResponsibilities = new System.Windows.Forms.TextBox();
			this.label54 = new System.Windows.Forms.Label();
			this.label55 = new System.Windows.Forms.Label();
			this.textBoxBasicDuties = new System.Windows.Forms.TextBox();
			this.textBoxConnections = new System.Windows.Forms.TextBox();
			this.tabPageEducation = new System.Windows.Forms.TabPage();
			this.buttonEducationsExcel = new System.Windows.Forms.Button();
			this.buttonEducationPrint = new System.Windows.Forms.Button();
			this.buttonEducationCancel = new System.Windows.Forms.Button();
			this.buttonEducationDelete = new System.Windows.Forms.Button();
			this.buttonEducationSave = new System.Windows.Forms.Button();
			this.buttonEducationEdit = new System.Windows.Forms.Button();
			this.buttonEducationAdd = new System.Windows.Forms.Button();
			this.groupBoxEducationHistory = new System.Windows.Forms.GroupBox();
			this.dataGridViewEducations = new System.Windows.Forms.DataGridView();
			this.groupBoxEducationData = new System.Windows.Forms.GroupBox();
			this.textBoxEducationArea = new System.Windows.Forms.TextBox();
			this.textBoxEducationTheme = new System.Windows.Forms.TextBox();
			this.label76 = new System.Windows.Forms.Label();
			this.textBoxEducationOrganisation = new System.Windows.Forms.TextBox();
			this.textBoxEducationPlace = new System.Windows.Forms.TextBox();
			this.dateTimePickerEducationToDate = new System.Windows.Forms.DateTimePicker();
			this.dateTimePickerEducationFromDate = new System.Windows.Forms.DateTimePicker();
			this.buttonEducationCatalog = new System.Windows.Forms.Button();
			this.label75 = new System.Windows.Forms.Label();
			this.textBoxEducationCertificate = new System.Windows.Forms.TextBox();
			this.numBoxEducationPrice = new BugBox.NumBox();
			this.label74 = new System.Windows.Forms.Label();
			this.numBoxEducationHours = new BugBox.NumBox();
			this.numBoxEducationDays = new BugBox.NumBox();
			this.label73 = new System.Windows.Forms.Label();
			this.label68 = new System.Windows.Forms.Label();
			this.label67 = new System.Windows.Forms.Label();
			this.textBoxEducationCode = new System.Windows.Forms.TextBox();
			this.label66 = new System.Windows.Forms.Label();
			this.label63 = new System.Windows.Forms.Label();
			this.label77 = new System.Windows.Forms.Label();
			this.label78 = new System.Windows.Forms.Label();
			this.label79 = new System.Windows.Forms.Label();
			this.tabPageMilitaryRang = new System.Windows.Forms.TabPage();
			this.buttonRangExcel = new System.Windows.Forms.Button();
			this.buttonRangNew = new System.Windows.Forms.Button();
			this.buttonRangPrint = new System.Windows.Forms.Button();
			this.buttonRangCancel = new System.Windows.Forms.Button();
			this.buttonRangDelete = new System.Windows.Forms.Button();
			this.buttonRangSave = new System.Windows.Forms.Button();
			this.buttonRangEdit = new System.Windows.Forms.Button();
			this.groupBoxRangHistory = new System.Windows.Forms.GroupBox();
			this.dataGridViewRang = new System.Windows.Forms.DataGridView();
			this.groupBox15 = new System.Windows.Forms.GroupBox();
			this.buttonRangdegreeNomenklature = new System.Windows.Forms.Button();
			this.label108 = new System.Windows.Forms.Label();
			this.comboBoxNSODegree = new System.Windows.Forms.ComboBox();
			this.buttonMilitaryAssignemntLink = new System.Windows.Forms.Button();
			this.buttonRangNomenklature = new System.Windows.Forms.Button();
			this.label88 = new System.Windows.Forms.Label();
			this.textBoxRangOrderNumber = new System.Windows.Forms.TextBox();
			this.label89 = new System.Windows.Forms.Label();
			this.label90 = new System.Windows.Forms.Label();
			this.label91 = new System.Windows.Forms.Label();
			this.dateTimePickerRangValidFrom = new System.Windows.Forms.DateTimePicker();
			this.dateTimePickerRangOrderDate = new System.Windows.Forms.DateTimePicker();
			this.comboBoxNSORang = new System.Windows.Forms.ComboBox();
			this.tabPageCards = new System.Windows.Forms.TabPage();
			this.buttonCardExcel = new System.Windows.Forms.Button();
			this.buttonCardNew = new System.Windows.Forms.Button();
			this.buttonCardPrint = new System.Windows.Forms.Button();
			this.buttonCardCancel = new System.Windows.Forms.Button();
			this.buttonCardDelete = new System.Windows.Forms.Button();
			this.buttonCardSave = new System.Windows.Forms.Button();
			this.buttonCardEdit = new System.Windows.Forms.Button();
			this.groupBoxCardHistory = new System.Windows.Forms.GroupBox();
			this.dataGridViewCards = new System.Windows.Forms.DataGridView();
			this.groupBox10 = new System.Windows.Forms.GroupBox();
			this.textBoxCardSign = new System.Windows.Forms.TextBox();
			this.label120 = new System.Windows.Forms.Label();
			this.textBoxCardSeries = new System.Windows.Forms.TextBox();
			this.label119 = new System.Windows.Forms.Label();
			this.textBoxCardNumber = new System.Windows.Forms.TextBox();
			this.label113 = new System.Windows.Forms.Label();
			this.label118 = new System.Windows.Forms.Label();
			this.comboBoxCardMilitaryRangEng = new System.Windows.Forms.ComboBox();
			this.label115 = new System.Windows.Forms.Label();
			this.label117 = new System.Windows.Forms.Label();
			this.dateTimePickerCardIssue = new System.Windows.Forms.DateTimePicker();
			this.comboBoxCardMilitaryRang = new System.Windows.Forms.ComboBox();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.label96 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.radioButton3 = new System.Windows.Forms.RadioButton();
			this.radioButton4 = new System.Windows.Forms.RadioButton();
			this.groupBox12 = new System.Windows.Forms.GroupBox();
			this.dataGridView2 = new System.Windows.Forms.DataGridView();
			this.groupBox13 = new System.Windows.Forms.GroupBox();
			this.label94 = new System.Windows.Forms.Label();
			this.label95 = new System.Windows.Forms.Label();
			this.label97 = new System.Windows.Forms.Label();
			this.label98 = new System.Windows.Forms.Label();
			this.label99 = new System.Windows.Forms.Label();
			this.labelTatalStaff = new System.Windows.Forms.Label();
			this.numBoxExpTotalD = new BugBox.NumBox();
			this.numBoxExpTotalM = new BugBox.NumBox();
			this.numBoxExpTotalY = new BugBox.NumBox();
			this.TabPersonalInfo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewLanguages)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.tabPageAssignment.SuspendLayout();
			this.groupBoxAssignmentGrid.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewAssignment)).BeginInit();
			this.tabPageAbsence.SuspendLayout();
			this.groupBoxAbsenceGrid.SuspendLayout();
			this.groupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewYears)).BeginInit();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewAbsence)).BeginInit();
			this.groupBoxAbsece.SuspendLayout();
			this.tabPagePenalty.SuspendLayout();
			this.groupBoxPenaltyGrid.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewPenalties)).BeginInit();
			this.groupBoxPenalty.SuspendLayout();
			this.tabPageNotes.SuspendLayout();
			this.groupBoxNotes.SuspendLayout();
			this.groupBoxNotesGrid.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewNotes)).BeginInit();
			this.groupBoxNotesFilter.SuspendLayout();
			this.tabPageAtestacii.SuspendLayout();
			this.groupBox9.SuspendLayout();
			this.groupBoxAttestationRegister.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewAttestations)).BeginInit();
			this.groupBox8.SuspendLayout();
			this.tabControlCardNew.SuspendLayout();
			this.tabPageFired.SuspendLayout();
			this.groupBoxFired.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewFired)).BeginInit();
			this.groupBox7.SuspendLayout();
			this.tabPageCharacteristics.SuspendLayout();
			this.tabPageEducation.SuspendLayout();
			this.groupBoxEducationHistory.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewEducations)).BeginInit();
			this.groupBoxEducationData.SuspendLayout();
			this.tabPageMilitaryRang.SuspendLayout();
			this.groupBoxRangHistory.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewRang)).BeginInit();
			this.groupBox15.SuspendLayout();
			this.tabPageCards.SuspendLayout();
			this.groupBoxCardHistory.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewCards)).BeginInit();
			this.groupBox10.SuspendLayout();
			this.groupBox12.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
			this.groupBox13.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonОК
			// 
			this.buttonОК.Image = ((System.Drawing.Image)(resources.GetObject("buttonОК.Image")));
			this.buttonОК.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonОК.Location = new System.Drawing.Point(281, 647);
			this.buttonОК.Name = "buttonОК";
			this.buttonОК.Size = new System.Drawing.Size(130, 23);
			this.buttonОК.TabIndex = 0;
			this.buttonОК.Text = "Запис и изход";
			this.toolTip1.SetToolTip(this.buttonОК, "Запис на данните и затваряне на досието");
			this.buttonОК.Click += new System.EventHandler(this.buttonОК_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Image")));
			this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonCancel.Location = new System.Drawing.Point(581, 647);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(130, 23);
			this.buttonCancel.TabIndex = 2;
			this.buttonCancel.Text = "Изход";
			this.toolTip1.SetToolTip(this.buttonCancel, "Затваряне на досието");
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.Image")));
			this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonSave.Location = new System.Drawing.Point(431, 647);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(130, 23);
			this.buttonSave.TabIndex = 1;
			this.buttonSave.Text = "Запис";
			this.toolTip1.SetToolTip(this.buttonSave, "Запис на данните");
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// TabPersonalInfo
			// 
			this.TabPersonalInfo.Controls.Add(this.labelTatalStaff);
			this.TabPersonalInfo.Controls.Add(this.numBoxExpTotalD);
			this.TabPersonalInfo.Controls.Add(this.numBoxExpTotalM);
			this.TabPersonalInfo.Controls.Add(this.numBoxExpTotalY);
			this.TabPersonalInfo.Controls.Add(this.textBoxResponsibleFor);
			this.TabPersonalInfo.Controls.Add(this.label112);
			this.TabPersonalInfo.Controls.Add(this.buttonAttached);
			this.TabPersonalInfo.Controls.Add(this.buttonLanguageDelete);
			this.TabPersonalInfo.Controls.Add(this.buttonLanguageAdd);
			this.TabPersonalInfo.Controls.Add(this.textBoxOther4);
			this.TabPersonalInfo.Controls.Add(this.buttonLanguageLevel);
			this.TabPersonalInfo.Controls.Add(this.buttonForeignLanguages);
			this.TabPersonalInfo.Controls.Add(this.dataGridViewLanguages);
			this.TabPersonalInfo.Controls.Add(this.groupBox1);
			this.TabPersonalInfo.Controls.Add(this.labelOther4);
			this.TabPersonalInfo.Controls.Add(this.groupBox3);
			this.TabPersonalInfo.Controls.Add(this.groupBox2);
			this.TabPersonalInfo.Controls.Add(this.groupBox6);
			this.TabPersonalInfo.Controls.Add(this.textBoxOther);
			this.TabPersonalInfo.Controls.Add(this.labelLanguage);
			this.TabPersonalInfo.Controls.Add(this.buttonLanguageEdit);
			this.TabPersonalInfo.Controls.Add(this.label57);
			this.TabPersonalInfo.Location = new System.Drawing.Point(4, 22);
			this.TabPersonalInfo.Name = "TabPersonalInfo";
			this.TabPersonalInfo.Size = new System.Drawing.Size(984, 615);
			this.TabPersonalInfo.TabIndex = 0;
			this.TabPersonalInfo.Text = "Лични данни";
			this.TabPersonalInfo.UseVisualStyleBackColor = true;
			// 
			// textBoxResponsibleFor
			// 
			this.textBoxResponsibleFor.Location = new System.Drawing.Point(187, 458);
			this.textBoxResponsibleFor.Multiline = true;
			this.textBoxResponsibleFor.Name = "textBoxResponsibleFor";
			this.textBoxResponsibleFor.Size = new System.Drawing.Size(381, 51);
			this.textBoxResponsibleFor.TabIndex = 127;
			this.textBoxResponsibleFor.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// label112
			// 
			this.label112.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label112.Location = new System.Drawing.Point(187, 439);
			this.label112.Name = "label112";
			this.label112.Size = new System.Drawing.Size(377, 16);
			this.label112.TabIndex = 128;
			this.label112.Text = "Заччислена техника:";
			// 
			// buttonAttached
			// 
			this.buttonAttached.Location = new System.Drawing.Point(19, 512);
			this.buttonAttached.Name = "buttonAttached";
			this.buttonAttached.Size = new System.Drawing.Size(147, 23);
			this.buttonAttached.TabIndex = 108;
			this.buttonAttached.Text = "Прикачени документи";
			this.buttonAttached.Click += new System.EventHandler(this.buttonAttached_Click);
			// 
			// buttonLanguageDelete
			// 
			this.buttonLanguageDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonLanguageDelete.Image = ((System.Drawing.Image)(resources.GetObject("buttonLanguageDelete.Image")));
			this.buttonLanguageDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonLanguageDelete.Location = new System.Drawing.Point(582, 555);
			this.buttonLanguageDelete.Name = "buttonLanguageDelete";
			this.buttonLanguageDelete.Size = new System.Drawing.Size(130, 23);
			this.buttonLanguageDelete.TabIndex = 126;
			this.buttonLanguageDelete.TabStop = false;
			this.buttonLanguageDelete.Tag = "Изтриване на чужд език";
			this.buttonLanguageDelete.Text = "Изтриване";
			this.toolTip1.SetToolTip(this.buttonLanguageDelete, "Номенклатура чужди езици");
			this.buttonLanguageDelete.Click += new System.EventHandler(this.buttonLanguageDelete_Click);
			// 
			// buttonLanguageAdd
			// 
			this.buttonLanguageAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonLanguageAdd.Image = ((System.Drawing.Image)(resources.GetObject("buttonLanguageAdd.Image")));
			this.buttonLanguageAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonLanguageAdd.Location = new System.Drawing.Point(852, 555);
			this.buttonLanguageAdd.Name = "buttonLanguageAdd";
			this.buttonLanguageAdd.Size = new System.Drawing.Size(120, 23);
			this.buttonLanguageAdd.TabIndex = 125;
			this.buttonLanguageAdd.TabStop = false;
			this.buttonLanguageAdd.Tag = "Редакция на чужд език";
			this.buttonLanguageAdd.Text = "Добавяне";
			this.toolTip1.SetToolTip(this.buttonLanguageAdd, "Номенклатура чужди езици");
			this.buttonLanguageAdd.Click += new System.EventHandler(this.buttonLanguageAdd_Click);
			// 
			// textBoxOther4
			// 
			this.textBoxOther4.Location = new System.Drawing.Point(187, 527);
			this.textBoxOther4.MaxLength = 255;
			this.textBoxOther4.Name = "textBoxOther4";
			this.textBoxOther4.Size = new System.Drawing.Size(180, 20);
			this.textBoxOther4.TabIndex = 19;
			this.textBoxOther4.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// buttonLanguageLevel
			// 
			this.buttonLanguageLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonLanguageLevel.Image = ((System.Drawing.Image)(resources.GetObject("buttonLanguageLevel.Image")));
			this.buttonLanguageLevel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonLanguageLevel.Location = new System.Drawing.Point(783, 585);
			this.buttonLanguageLevel.Name = "buttonLanguageLevel";
			this.buttonLanguageLevel.Size = new System.Drawing.Size(190, 23);
			this.buttonLanguageLevel.TabIndex = 124;
			this.buttonLanguageLevel.TabStop = false;
			this.buttonLanguageLevel.Tag = "Добавяне на данни към номенклатурата за чужди езици";
			this.buttonLanguageLevel.Text = "Степен на владеене";
			this.toolTip1.SetToolTip(this.buttonLanguageLevel, "Номенклатура чужди езици");
			this.buttonLanguageLevel.Click += new System.EventHandler(this.buttonNomenklatureLanguageLevel_Click);
			// 
			// buttonForeignLanguages
			// 
			this.buttonForeignLanguages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonForeignLanguages.Image = ((System.Drawing.Image)(resources.GetObject("buttonForeignLanguages.Image")));
			this.buttonForeignLanguages.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonForeignLanguages.Location = new System.Drawing.Point(582, 585);
			this.buttonForeignLanguages.Name = "buttonForeignLanguages";
			this.buttonForeignLanguages.Size = new System.Drawing.Size(190, 23);
			this.buttonForeignLanguages.TabIndex = 123;
			this.buttonForeignLanguages.TabStop = false;
			this.buttonForeignLanguages.Tag = "Добавяне на данни към номенклатурата за чужди езици";
			this.buttonForeignLanguages.Text = "Номенклатура чужди езици";
			this.toolTip1.SetToolTip(this.buttonForeignLanguages, "Номенклатура чужди езици");
			this.buttonForeignLanguages.Click += new System.EventHandler(this.buttonForeignLanguages_Click);
			// 
			// dataGridViewLanguages
			// 
			this.dataGridViewLanguages.AllowUserToAddRows = false;
			this.dataGridViewLanguages.AllowUserToDeleteRows = false;
			this.dataGridViewLanguages.AllowUserToResizeRows = false;
			this.dataGridViewLanguages.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			dataGridViewCellStyle73.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle73.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle73.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle73.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle73.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle73.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle73.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewLanguages.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle73;
			this.dataGridViewLanguages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle74.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle74.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle74.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle74.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle74.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle74.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle74.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewLanguages.DefaultCellStyle = dataGridViewCellStyle74;
			this.dataGridViewLanguages.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridViewLanguages.ImeMode = System.Windows.Forms.ImeMode.On;
			this.dataGridViewLanguages.Location = new System.Drawing.Point(582, 397);
			this.dataGridViewLanguages.MultiSelect = false;
			this.dataGridViewLanguages.Name = "dataGridViewLanguages";
			this.dataGridViewLanguages.ReadOnly = true;
			dataGridViewCellStyle75.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle75.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle75.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle75.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle75.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle75.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle75.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewLanguages.RowHeadersDefaultCellStyle = dataGridViewCellStyle75;
			this.dataGridViewLanguages.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewLanguages.Size = new System.Drawing.Size(391, 147);
			this.dataGridViewLanguages.TabIndex = 122;
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
			this.groupBox1.Controls.Add(this.dateTimePickerPostypilNa);
			this.groupBox1.Controls.Add(this.labelHiredAt);
			this.groupBox1.Location = new System.Drawing.Point(8, 548);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(569, 60);
			this.groupBox1.TabIndex = 106;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Трудов стаж по специалността  [ГГГ, ММ, ДД] ";
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(10, 14);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(122, 16);
			this.label19.TabIndex = 95;
			this.label19.Text = "При постъпване :";
			// 
			// label42
			// 
			this.label42.Location = new System.Drawing.Point(138, 14);
			this.label42.Name = "label42";
			this.label42.Size = new System.Drawing.Size(120, 16);
			this.label42.TabIndex = 96;
			this.label42.Text = "В администрацията:";
			// 
			// label43
			// 
			this.label43.Location = new System.Drawing.Point(263, 14);
			this.label43.Name = "label43";
			this.label43.Size = new System.Drawing.Size(114, 16);
			this.label43.TabIndex = 96;
			this.label43.Text = "Общо :";
			// 
			// numBoxStartDay
			// 
			this.numBoxStartDay.Location = new System.Drawing.Point(86, 32);
			this.numBoxStartDay.Name = "numBoxStartDay";
			this.numBoxStartDay.ReadOnly = true;
			this.numBoxStartDay.Size = new System.Drawing.Size(30, 20);
			this.numBoxStartDay.TabIndex = 87;
			this.numBoxStartDay.TabStop = false;
			// 
			// numBoxStartMonth
			// 
			this.numBoxStartMonth.Location = new System.Drawing.Point(48, 32);
			this.numBoxStartMonth.Name = "numBoxStartMonth";
			this.numBoxStartMonth.ReadOnly = true;
			this.numBoxStartMonth.Size = new System.Drawing.Size(30, 20);
			this.numBoxStartMonth.TabIndex = 86;
			this.numBoxStartMonth.TabStop = false;
			// 
			// numBoxStartYear
			// 
			this.numBoxStartYear.Location = new System.Drawing.Point(10, 32);
			this.numBoxStartYear.Name = "numBoxStartYear";
			this.numBoxStartYear.ReadOnly = true;
			this.numBoxStartYear.Size = new System.Drawing.Size(30, 20);
			this.numBoxStartYear.TabIndex = 85;
			this.numBoxStartYear.TabStop = false;
			// 
			// numBoxOrgDay
			// 
			this.numBoxOrgDay.Location = new System.Drawing.Point(214, 32);
			this.numBoxOrgDay.Name = "numBoxOrgDay";
			this.numBoxOrgDay.ReadOnly = true;
			this.numBoxOrgDay.Size = new System.Drawing.Size(30, 20);
			this.numBoxOrgDay.TabIndex = 90;
			this.numBoxOrgDay.TabStop = false;
			// 
			// numBoxOrgMonth
			// 
			this.numBoxOrgMonth.Location = new System.Drawing.Point(176, 32);
			this.numBoxOrgMonth.Name = "numBoxOrgMonth";
			this.numBoxOrgMonth.ReadOnly = true;
			this.numBoxOrgMonth.Size = new System.Drawing.Size(30, 20);
			this.numBoxOrgMonth.TabIndex = 89;
			this.numBoxOrgMonth.TabStop = false;
			// 
			// numBoxOrgYear
			// 
			this.numBoxOrgYear.Location = new System.Drawing.Point(138, 32);
			this.numBoxOrgYear.Name = "numBoxOrgYear";
			this.numBoxOrgYear.ReadOnly = true;
			this.numBoxOrgYear.Size = new System.Drawing.Size(30, 20);
			this.numBoxOrgYear.TabIndex = 88;
			this.numBoxOrgYear.TabStop = false;
			// 
			// numBoxTotalDay
			// 
			this.numBoxTotalDay.Location = new System.Drawing.Point(339, 32);
			this.numBoxTotalDay.Name = "numBoxTotalDay";
			this.numBoxTotalDay.ReadOnly = true;
			this.numBoxTotalDay.Size = new System.Drawing.Size(30, 20);
			this.numBoxTotalDay.TabIndex = 93;
			this.numBoxTotalDay.TabStop = false;
			// 
			// numBoxTotalYear
			// 
			this.numBoxTotalYear.Location = new System.Drawing.Point(263, 32);
			this.numBoxTotalYear.Name = "numBoxTotalYear";
			this.numBoxTotalYear.ReadOnly = true;
			this.numBoxTotalYear.Size = new System.Drawing.Size(30, 20);
			this.numBoxTotalYear.TabIndex = 92;
			this.numBoxTotalYear.TabStop = false;
			// 
			// numBoxTotalMonth
			// 
			this.numBoxTotalMonth.Location = new System.Drawing.Point(301, 32);
			this.numBoxTotalMonth.Name = "numBoxTotalMonth";
			this.numBoxTotalMonth.ReadOnly = true;
			this.numBoxTotalMonth.Size = new System.Drawing.Size(30, 20);
			this.numBoxTotalMonth.TabIndex = 91;
			this.numBoxTotalMonth.TabStop = false;
			// 
			// dateTimePickerPostypilNa
			// 
			this.dateTimePickerPostypilNa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.dateTimePickerPostypilNa.Enabled = false;
			this.dateTimePickerPostypilNa.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerPostypilNa.Location = new System.Drawing.Point(384, 32);
			this.dateTimePickerPostypilNa.Name = "dateTimePickerPostypilNa";
			this.dateTimePickerPostypilNa.Size = new System.Drawing.Size(170, 20);
			this.dateTimePickerPostypilNa.TabIndex = 11;
			this.dateTimePickerPostypilNa.Value = new System.DateTime(2005, 1, 12, 9, 43, 36, 687);
			this.dateTimePickerPostypilNa.ValueChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// labelHiredAt
			// 
			this.labelHiredAt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelHiredAt.Location = new System.Drawing.Point(385, 14);
			this.labelHiredAt.Name = "labelHiredAt";
			this.labelHiredAt.Size = new System.Drawing.Size(169, 16);
			this.labelHiredAt.TabIndex = 110;
			this.labelHiredAt.Text = "Постъпил на:";
			// 
			// labelOther4
			// 
			this.labelOther4.Location = new System.Drawing.Point(187, 511);
			this.labelOther4.Name = "labelOther4";
			this.labelOther4.Size = new System.Drawing.Size(183, 16);
			this.labelOther4.TabIndex = 120;
			this.labelOther4.Text = "Други 4 :";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.textBoxTelephone);
			this.groupBox3.Controls.Add(this.label51);
			this.groupBox3.Controls.Add(this.comboBoxClothesMoney);
			this.groupBox3.Controls.Add(this.comboBoxReceivedAddon);
			this.groupBox3.Controls.Add(this.label56);
			this.groupBox3.Controls.Add(this.labelMilitaryStatus);
			this.groupBox3.Controls.Add(this.comboBoxMilitaryStatus);
			this.groupBox3.Controls.Add(this.buttonNomenkMilitaryRang);
			this.groupBox3.Controls.Add(this.buttonNomenkScienceTitle);
			this.groupBox3.Controls.Add(this.buttonNomenkScienceLevel);
			this.groupBox3.Controls.Add(this.buttonNomenkEducation);
			this.groupBox3.Controls.Add(this.comboBoxSpecialSkills);
			this.groupBox3.Controls.Add(this.labelSpecialSkills);
			this.groupBox3.Controls.Add(this.labelScience);
			this.groupBox3.Controls.Add(this.comboBoxMilitaryRang);
			this.groupBox3.Controls.Add(this.comboBoxScienceLevel);
			this.groupBox3.Controls.Add(this.comboBoxScience);
			this.groupBox3.Controls.Add(this.labelScienceLevel);
			this.groupBox3.Controls.Add(this.label36);
			this.groupBox3.Controls.Add(this.label38);
			this.groupBox3.Controls.Add(this.textBoxDiplom);
			this.groupBox3.Controls.Add(this.comboBoxEducation);
			this.groupBox3.Controls.Add(this.textBoxSpeciality);
			this.groupBox3.Controls.Add(this.label52);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.buttonNomenklatureSpecialSkills);
			this.groupBox3.Controls.Add(this.buttonNomenklatureRang);
			this.groupBox3.Controls.Add(this.label53);
			this.groupBox3.Controls.Add(this.comboBoxRang);
			this.groupBox3.Controls.Add(this.labelMilitaryRang);
			this.groupBox3.Controls.Add(this.comboBoxFamilyStatus);
			this.groupBox3.Controls.Add(this.buttonNomenkFamilyStatus);
			this.groupBox3.Controls.Add(this.label37);
			this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.groupBox3.Location = new System.Drawing.Point(187, 232);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(789, 143);
			this.groupBox3.TabIndex = 1;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Лични данни";
			// 
			// textBoxTelephone
			// 
			this.textBoxTelephone.Location = new System.Drawing.Point(590, 113);
			this.textBoxTelephone.MaxLength = 255;
			this.textBoxTelephone.Name = "textBoxTelephone";
			this.textBoxTelephone.Size = new System.Drawing.Size(191, 20);
			this.textBoxTelephone.TabIndex = 9;
			this.textBoxTelephone.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// label51
			// 
			this.label51.Enabled = false;
			this.label51.Location = new System.Drawing.Point(261, 224);
			this.label51.Name = "label51";
			this.label51.Size = new System.Drawing.Size(40, 16);
			this.label51.TabIndex = 111;
			this.label51.Text = "Пари за дрехи :";
			this.label51.Visible = false;
			// 
			// comboBoxClothesMoney
			// 
			this.comboBoxClothesMoney.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxClothesMoney.Enabled = false;
			this.comboBoxClothesMoney.Items.AddRange(new object[] {
            "Неполучени",
            "Получени"});
			this.comboBoxClothesMoney.Location = new System.Drawing.Point(136, 240);
			this.comboBoxClothesMoney.Name = "comboBoxClothesMoney";
			this.comboBoxClothesMoney.Size = new System.Drawing.Size(121, 21);
			this.comboBoxClothesMoney.TabIndex = 3;
			this.comboBoxClothesMoney.Visible = false;
			// 
			// comboBoxReceivedAddon
			// 
			this.comboBoxReceivedAddon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxReceivedAddon.Enabled = false;
			this.comboBoxReceivedAddon.Items.AddRange(new object[] {
            "Неполучени",
            "Получени"});
			this.comboBoxReceivedAddon.Location = new System.Drawing.Point(261, 240);
			this.comboBoxReceivedAddon.Name = "comboBoxReceivedAddon";
			this.comboBoxReceivedAddon.Size = new System.Drawing.Size(48, 21);
			this.comboBoxReceivedAddon.TabIndex = 3;
			this.comboBoxReceivedAddon.Visible = false;
			this.comboBoxReceivedAddon.SelectedIndexChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// label56
			// 
			this.label56.Enabled = false;
			this.label56.Location = new System.Drawing.Point(136, 224);
			this.label56.Name = "label56";
			this.label56.Size = new System.Drawing.Size(100, 16);
			this.label56.TabIndex = 111;
			this.label56.Text = "Пари за дрехи :";
			this.label56.Visible = false;
			// 
			// labelMilitaryStatus
			// 
			this.labelMilitaryStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelMilitaryStatus.Enabled = false;
			this.labelMilitaryStatus.Location = new System.Drawing.Point(10, 224);
			this.labelMilitaryStatus.Name = "labelMilitaryStatus";
			this.labelMilitaryStatus.Size = new System.Drawing.Size(120, 16);
			this.labelMilitaryStatus.TabIndex = 94;
			this.labelMilitaryStatus.Text = "Военна отчетност :";
			this.labelMilitaryStatus.Visible = false;
			// 
			// comboBoxMilitaryStatus
			// 
			this.comboBoxMilitaryStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxMilitaryStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMilitaryStatus.Enabled = false;
			this.comboBoxMilitaryStatus.ItemHeight = 13;
			this.comboBoxMilitaryStatus.Location = new System.Drawing.Point(10, 240);
			this.comboBoxMilitaryStatus.Name = "comboBoxMilitaryStatus";
			this.comboBoxMilitaryStatus.Size = new System.Drawing.Size(120, 21);
			this.comboBoxMilitaryStatus.TabIndex = 14;
			this.comboBoxMilitaryStatus.Visible = false;
			this.comboBoxMilitaryStatus.SelectedIndexChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// buttonNomenkMilitaryRang
			// 
			this.buttonNomenkMilitaryRang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonNomenkMilitaryRang.Font = new System.Drawing.Font("Symbol", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonNomenkMilitaryRang.Image = ((System.Drawing.Image)(resources.GetObject("buttonNomenkMilitaryRang.Image")));
			this.buttonNomenkMilitaryRang.Location = new System.Drawing.Point(177, 112);
			this.buttonNomenkMilitaryRang.Name = "buttonNomenkMilitaryRang";
			this.buttonNomenkMilitaryRang.Size = new System.Drawing.Size(21, 21);
			this.buttonNomenkMilitaryRang.TabIndex = 9;
			this.buttonNomenkMilitaryRang.TabStop = false;
			this.buttonNomenkMilitaryRang.Tag = "Добавяне на данни към номенклатурата за научна степен";
			this.toolTip1.SetToolTip(this.buttonNomenkMilitaryRang, "Номенклатура военен ранг");
			this.buttonNomenkMilitaryRang.Click += new System.EventHandler(this.buttonNomenkMilitaryRang_Click);
			// 
			// buttonNomenkScienceTitle
			// 
			this.buttonNomenkScienceTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonNomenkScienceTitle.Image = ((System.Drawing.Image)(resources.GetObject("buttonNomenkScienceTitle.Image")));
			this.buttonNomenkScienceTitle.Location = new System.Drawing.Point(373, 72);
			this.buttonNomenkScienceTitle.Name = "buttonNomenkScienceTitle";
			this.buttonNomenkScienceTitle.Size = new System.Drawing.Size(21, 21);
			this.buttonNomenkScienceTitle.TabIndex = 5;
			this.buttonNomenkScienceTitle.TabStop = false;
			this.buttonNomenkScienceTitle.Tag = "Добавяне на данни към номенклатурата за научно звание";
			this.toolTip1.SetToolTip(this.buttonNomenkScienceTitle, "Номенклатура научно звание");
			this.buttonNomenkScienceTitle.Click += new System.EventHandler(this.buttonNomenkScienceTitle_Click);
			// 
			// buttonNomenkScienceLevel
			// 
			this.buttonNomenkScienceLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonNomenkScienceLevel.Image = ((System.Drawing.Image)(resources.GetObject("buttonNomenkScienceLevel.Image")));
			this.buttonNomenkScienceLevel.Location = new System.Drawing.Point(567, 72);
			this.buttonNomenkScienceLevel.Name = "buttonNomenkScienceLevel";
			this.buttonNomenkScienceLevel.Size = new System.Drawing.Size(21, 21);
			this.buttonNomenkScienceLevel.TabIndex = 7;
			this.buttonNomenkScienceLevel.TabStop = false;
			this.buttonNomenkScienceLevel.Tag = "Добавяне на данни към номенклатурата за научна степен";
			this.toolTip1.SetToolTip(this.buttonNomenkScienceLevel, "Номенклатура Научна степен");
			this.buttonNomenkScienceLevel.Click += new System.EventHandler(this.buttonNomenkScienceLevel_Click);
			// 
			// buttonNomenkEducation
			// 
			this.buttonNomenkEducation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonNomenkEducation.Image = ((System.Drawing.Image)(resources.GetObject("buttonNomenkEducation.Image")));
			this.buttonNomenkEducation.Location = new System.Drawing.Point(177, 72);
			this.buttonNomenkEducation.Name = "buttonNomenkEducation";
			this.buttonNomenkEducation.Size = new System.Drawing.Size(21, 21);
			this.buttonNomenkEducation.TabIndex = 3;
			this.buttonNomenkEducation.TabStop = false;
			this.buttonNomenkEducation.Tag = "Добавяне на данни към номенклатурата за образование";
			this.toolTip1.SetToolTip(this.buttonNomenkEducation, "Номенклатура образование");
			this.buttonNomenkEducation.Click += new System.EventHandler(this.buttonNomenkEducation_Click);
			// 
			// comboBoxSpecialSkills
			// 
			this.comboBoxSpecialSkills.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxSpecialSkills.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSpecialSkills.Location = new System.Drawing.Point(201, 112);
			this.comboBoxSpecialSkills.Name = "comboBoxSpecialSkills";
			this.comboBoxSpecialSkills.Size = new System.Drawing.Size(170, 21);
			this.comboBoxSpecialSkills.TabIndex = 7;
			this.comboBoxSpecialSkills.SelectedIndexChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// labelSpecialSkills
			// 
			this.labelSpecialSkills.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelSpecialSkills.Location = new System.Drawing.Point(201, 96);
			this.labelSpecialSkills.Name = "labelSpecialSkills";
			this.labelSpecialSkills.Size = new System.Drawing.Size(192, 16);
			this.labelSpecialSkills.TabIndex = 108;
			this.labelSpecialSkills.Text = "Специални умения :";
			// 
			// labelScience
			// 
			this.labelScience.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelScience.Location = new System.Drawing.Point(201, 55);
			this.labelScience.Name = "labelScience";
			this.labelScience.Size = new System.Drawing.Size(177, 16);
			this.labelScience.TabIndex = 88;
			this.labelScience.Text = "Научно звание :";
			// 
			// comboBoxMilitaryRang
			// 
			this.comboBoxMilitaryRang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxMilitaryRang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMilitaryRang.ItemHeight = 13;
			this.comboBoxMilitaryRang.Location = new System.Drawing.Point(5, 112);
			this.comboBoxMilitaryRang.Name = "comboBoxMilitaryRang";
			this.comboBoxMilitaryRang.Size = new System.Drawing.Size(170, 21);
			this.comboBoxMilitaryRang.TabIndex = 6;
			this.comboBoxMilitaryRang.SelectedIndexChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// comboBoxScienceLevel
			// 
			this.comboBoxScienceLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxScienceLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxScienceLevel.ItemHeight = 13;
			this.comboBoxScienceLevel.Location = new System.Drawing.Point(396, 72);
			this.comboBoxScienceLevel.Name = "comboBoxScienceLevel";
			this.comboBoxScienceLevel.Size = new System.Drawing.Size(170, 21);
			this.comboBoxScienceLevel.TabIndex = 4;
			this.comboBoxScienceLevel.SelectedIndexChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// comboBoxScience
			// 
			this.comboBoxScience.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxScience.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxScience.ItemHeight = 13;
			this.comboBoxScience.Location = new System.Drawing.Point(201, 72);
			this.comboBoxScience.Name = "comboBoxScience";
			this.comboBoxScience.Size = new System.Drawing.Size(170, 21);
			this.comboBoxScience.TabIndex = 3;
			this.comboBoxScience.SelectedIndexChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// labelScienceLevel
			// 
			this.labelScienceLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelScienceLevel.Location = new System.Drawing.Point(396, 55);
			this.labelScienceLevel.Name = "labelScienceLevel";
			this.labelScienceLevel.Size = new System.Drawing.Size(187, 16);
			this.labelScienceLevel.TabIndex = 90;
			this.labelScienceLevel.Text = "Научна степен :";
			// 
			// label36
			// 
			this.label36.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label36.Location = new System.Drawing.Point(5, 55);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(169, 16);
			this.label36.TabIndex = 102;
			this.label36.Text = "Образование :";
			// 
			// label38
			// 
			this.label38.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label38.Location = new System.Drawing.Point(395, 15);
			this.label38.Name = "label38";
			this.label38.Size = new System.Drawing.Size(385, 16);
			this.label38.TabIndex = 104;
			this.label38.Text = "Диплома данни :";
			// 
			// textBoxDiplom
			// 
			this.textBoxDiplom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxDiplom.Location = new System.Drawing.Point(395, 31);
			this.textBoxDiplom.Name = "textBoxDiplom";
			this.textBoxDiplom.Size = new System.Drawing.Size(385, 20);
			this.textBoxDiplom.TabIndex = 1;
			this.textBoxDiplom.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// comboBoxEducation
			// 
			this.comboBoxEducation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxEducation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxEducation.Location = new System.Drawing.Point(5, 72);
			this.comboBoxEducation.Name = "comboBoxEducation";
			this.comboBoxEducation.Size = new System.Drawing.Size(170, 21);
			this.comboBoxEducation.TabIndex = 2;
			this.comboBoxEducation.SelectedIndexChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// textBoxSpeciality
			// 
			this.textBoxSpeciality.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxSpeciality.Location = new System.Drawing.Point(5, 31);
			this.textBoxSpeciality.Name = "textBoxSpeciality";
			this.textBoxSpeciality.Size = new System.Drawing.Size(385, 20);
			this.textBoxSpeciality.TabIndex = 0;
			this.textBoxSpeciality.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// label52
			// 
			this.label52.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label52.Location = new System.Drawing.Point(5, 15);
			this.label52.Name = "label52";
			this.label52.Size = new System.Drawing.Size(385, 16);
			this.label52.TabIndex = 121;
			this.label52.Text = "Специалност :";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(590, 96);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(195, 16);
			this.label3.TabIndex = 77;
			this.label3.Text = "Телефон :";
			// 
			// buttonNomenklatureSpecialSkills
			// 
			this.buttonNomenklatureSpecialSkills.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonNomenklatureSpecialSkills.Image = ((System.Drawing.Image)(resources.GetObject("buttonNomenklatureSpecialSkills.Image")));
			this.buttonNomenklatureSpecialSkills.Location = new System.Drawing.Point(373, 112);
			this.buttonNomenklatureSpecialSkills.Name = "buttonNomenklatureSpecialSkills";
			this.buttonNomenklatureSpecialSkills.Size = new System.Drawing.Size(21, 21);
			this.buttonNomenklatureSpecialSkills.TabIndex = 13;
			this.buttonNomenklatureSpecialSkills.TabStop = false;
			this.buttonNomenklatureSpecialSkills.Tag = "Добавяне на данни към номенклатурата за степен на владеене на чужд език";
			this.toolTip1.SetToolTip(this.buttonNomenklatureSpecialSkills, "Номенклатура степен на владеене");
			this.buttonNomenklatureSpecialSkills.Click += new System.EventHandler(this.buttonNomenklatureSpecialSkills_Click);
			// 
			// buttonNomenklatureRang
			// 
			this.buttonNomenklatureRang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonNomenklatureRang.Image = ((System.Drawing.Image)(resources.GetObject("buttonNomenklatureRang.Image")));
			this.buttonNomenklatureRang.Location = new System.Drawing.Point(567, 112);
			this.buttonNomenklatureRang.Name = "buttonNomenklatureRang";
			this.buttonNomenklatureRang.Size = new System.Drawing.Size(21, 21);
			this.buttonNomenklatureRang.TabIndex = 15;
			this.buttonNomenklatureRang.TabStop = false;
			this.buttonNomenklatureRang.Tag = "Добавяне на данни към номенклатурата за научна степен";
			this.toolTip1.SetToolTip(this.buttonNomenklatureRang, "Номенклатура  ранг");
			this.buttonNomenklatureRang.Click += new System.EventHandler(this.buttonNomenklatureRang_Click);
			// 
			// label53
			// 
			this.label53.Location = new System.Drawing.Point(396, 96);
			this.label53.Name = "label53";
			this.label53.Size = new System.Drawing.Size(192, 16);
			this.label53.TabIndex = 113;
			this.label53.Text = "Ранг :";
			this.toolTip1.SetToolTip(this.label53, "Ранг :");
			// 
			// comboBoxRang
			// 
			this.comboBoxRang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxRang.Location = new System.Drawing.Point(396, 112);
			this.comboBoxRang.Name = "comboBoxRang";
			this.comboBoxRang.Size = new System.Drawing.Size(170, 21);
			this.comboBoxRang.TabIndex = 8;
			this.comboBoxRang.SelectedIndexChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// labelMilitaryRang
			// 
			this.labelMilitaryRang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelMilitaryRang.Location = new System.Drawing.Point(5, 96);
			this.labelMilitaryRang.Name = "labelMilitaryRang";
			this.labelMilitaryRang.Size = new System.Drawing.Size(193, 16);
			this.labelMilitaryRang.TabIndex = 92;
			this.labelMilitaryRang.Text = "Военен ранг :";
			// 
			// comboBoxFamilyStatus
			// 
			this.comboBoxFamilyStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxFamilyStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxFamilyStatus.Location = new System.Drawing.Point(590, 72);
			this.comboBoxFamilyStatus.Name = "comboBoxFamilyStatus";
			this.comboBoxFamilyStatus.Size = new System.Drawing.Size(170, 21);
			this.comboBoxFamilyStatus.TabIndex = 5;
			this.comboBoxFamilyStatus.SelectedIndexChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// buttonNomenkFamilyStatus
			// 
			this.buttonNomenkFamilyStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonNomenkFamilyStatus.Image = ((System.Drawing.Image)(resources.GetObject("buttonNomenkFamilyStatus.Image")));
			this.buttonNomenkFamilyStatus.Location = new System.Drawing.Point(762, 72);
			this.buttonNomenkFamilyStatus.Name = "buttonNomenkFamilyStatus";
			this.buttonNomenkFamilyStatus.Size = new System.Drawing.Size(21, 21);
			this.buttonNomenkFamilyStatus.TabIndex = 12;
			this.buttonNomenkFamilyStatus.TabStop = false;
			this.buttonNomenkFamilyStatus.Tag = "Добавяне на данни към номенклатурата за семейно положение";
			this.toolTip1.SetToolTip(this.buttonNomenkFamilyStatus, "Номенклатура семейно положение");
			this.buttonNomenkFamilyStatus.Click += new System.EventHandler(this.buttonNomenkFamilyStatus_Click);
			// 
			// label37
			// 
			this.label37.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label37.Location = new System.Drawing.Point(590, 55);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(188, 16);
			this.label37.TabIndex = 103;
			this.label37.Text = "Семейно положение :";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label110);
			this.groupBox2.Controls.Add(this.dateTimePickerPCardExpiry);
			this.groupBox2.Controls.Add(this.labelWorkBookDate);
			this.groupBox2.Controls.Add(this.dateTimePickerWorkBook);
			this.groupBox2.Controls.Add(this.textBoxWorkBook);
			this.groupBox2.Controls.Add(this.textBoxOther3);
			this.groupBox2.Controls.Add(this.textBoxOther2);
			this.groupBox2.Controls.Add(this.textBoxOther1);
			this.groupBox2.Controls.Add(this.labelWorkBook);
			this.groupBox2.Controls.Add(this.labelOther3);
			this.groupBox2.Controls.Add(this.labelOther2);
			this.groupBox2.Controls.Add(this.labelOther1);
			this.groupBox2.Controls.Add(this.labelCurrentAddress);
			this.groupBox2.Controls.Add(this.textBoxCurrentAddress);
			this.groupBox2.Controls.Add(this.textBoxEngName);
			this.groupBox2.Controls.Add(this.label81);
			this.groupBox2.Controls.Add(this.dateTimePickerBirthDate);
			this.groupBox2.Controls.Add(this.comboBoxEGN);
			this.groupBox2.Controls.Add(this.label80);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.comboBoxSex);
			this.groupBox2.Controls.Add(this.numBoxPcCard);
			this.groupBox2.Controls.Add(this.labelJKkwartal);
			this.groupBox2.Controls.Add(this.labelPublishedByy);
			this.groupBox2.Controls.Add(this.labelPublishedBy);
			this.groupBox2.Controls.Add(this.textBoxPublishedFrom);
			this.groupBox2.Controls.Add(this.labelKwartal);
			this.groupBox2.Controls.Add(this.textBoxKwartal);
			this.groupBox2.Controls.Add(this.labelNaselenoMqsto);
			this.groupBox2.Controls.Add(this.labelRegion);
			this.groupBox2.Controls.Add(this.numBoxEgn);
			this.groupBox2.Controls.Add(this.labelEGN);
			this.groupBox2.Controls.Add(this.textBoxNames);
			this.groupBox2.Controls.Add(this.labelNames);
			this.groupBox2.Controls.Add(this.labelCountry);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.textBoxBornTown);
			this.groupBox2.Controls.Add(this.dateTimePickerPCCardPublished);
			this.groupBox2.Controls.Add(this.textBoxTown);
			this.groupBox2.Controls.Add(this.textBoxCountry);
			this.groupBox2.Controls.Add(this.textBoxRegion);
			this.groupBox2.Location = new System.Drawing.Point(8, 8);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(968, 218);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Паспортни данни";
			// 
			// label110
			// 
			this.label110.Location = new System.Drawing.Point(582, 134);
			this.label110.Name = "label110";
			this.label110.Size = new System.Drawing.Size(180, 16);
			this.label110.TabIndex = 131;
			this.label110.Text = "Валидна до:";
			// 
			// dateTimePickerPCardExpiry
			// 
			this.dateTimePickerPCardExpiry.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerPCardExpiry.Location = new System.Drawing.Point(582, 151);
			this.dateTimePickerPCardExpiry.Name = "dateTimePickerPCardExpiry";
			this.dateTimePickerPCardExpiry.Size = new System.Drawing.Size(180, 20);
			this.dateTimePickerPCardExpiry.TabIndex = 130;
			this.dateTimePickerPCardExpiry.Value = new System.DateTime(2005, 1, 12, 9, 33, 28, 578);
			this.dateTimePickerPCardExpiry.ValueChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// labelWorkBookDate
			// 
			this.labelWorkBookDate.Location = new System.Drawing.Point(775, 176);
			this.labelWorkBookDate.Name = "labelWorkBookDate";
			this.labelWorkBookDate.Size = new System.Drawing.Size(180, 16);
			this.labelWorkBookDate.TabIndex = 129;
			this.labelWorkBookDate.Text = "Дата на издаване :";
			// 
			// dateTimePickerWorkBook
			// 
			this.dateTimePickerWorkBook.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerWorkBook.Location = new System.Drawing.Point(775, 192);
			this.dateTimePickerWorkBook.Name = "dateTimePickerWorkBook";
			this.dateTimePickerWorkBook.Size = new System.Drawing.Size(180, 20);
			this.dateTimePickerWorkBook.TabIndex = 20;
			this.dateTimePickerWorkBook.Value = new System.DateTime(2005, 1, 12, 9, 33, 28, 578);
			this.dateTimePickerWorkBook.ValueChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// textBoxWorkBook
			// 
			this.textBoxWorkBook.Location = new System.Drawing.Point(582, 192);
			this.textBoxWorkBook.MaxLength = 255;
			this.textBoxWorkBook.Name = "textBoxWorkBook";
			this.textBoxWorkBook.Size = new System.Drawing.Size(180, 20);
			this.textBoxWorkBook.TabIndex = 15;
			this.textBoxWorkBook.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// textBoxOther3
			// 
			this.textBoxOther3.Location = new System.Drawing.Point(392, 192);
			this.textBoxOther3.MaxLength = 255;
			this.textBoxOther3.Name = "textBoxOther3";
			this.textBoxOther3.Size = new System.Drawing.Size(180, 20);
			this.textBoxOther3.TabIndex = 18;
			this.textBoxOther3.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// textBoxOther2
			// 
			this.textBoxOther2.Location = new System.Drawing.Point(200, 192);
			this.textBoxOther2.MaxLength = 255;
			this.textBoxOther2.Name = "textBoxOther2";
			this.textBoxOther2.Size = new System.Drawing.Size(180, 20);
			this.textBoxOther2.TabIndex = 17;
			this.textBoxOther2.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// textBoxOther1
			// 
			this.textBoxOther1.Location = new System.Drawing.Point(9, 192);
			this.textBoxOther1.MaxLength = 255;
			this.textBoxOther1.Name = "textBoxOther1";
			this.textBoxOther1.Size = new System.Drawing.Size(180, 20);
			this.textBoxOther1.TabIndex = 16;
			this.textBoxOther1.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// labelWorkBook
			// 
			this.labelWorkBook.Location = new System.Drawing.Point(582, 176);
			this.labelWorkBook.Name = "labelWorkBook";
			this.labelWorkBook.Size = new System.Drawing.Size(180, 16);
			this.labelWorkBook.TabIndex = 122;
			this.labelWorkBook.Text = "Трудова книжка номер :";
			// 
			// labelOther3
			// 
			this.labelOther3.Location = new System.Drawing.Point(392, 176);
			this.labelOther3.Name = "labelOther3";
			this.labelOther3.Size = new System.Drawing.Size(184, 16);
			this.labelOther3.TabIndex = 118;
			this.labelOther3.Text = "Други 3 :";
			// 
			// labelOther2
			// 
			this.labelOther2.Location = new System.Drawing.Point(200, 176);
			this.labelOther2.Name = "labelOther2";
			this.labelOther2.Size = new System.Drawing.Size(182, 16);
			this.labelOther2.TabIndex = 116;
			this.labelOther2.Text = "Други 2 :";
			// 
			// labelOther1
			// 
			this.labelOther1.Location = new System.Drawing.Point(9, 176);
			this.labelOther1.Name = "labelOther1";
			this.labelOther1.Size = new System.Drawing.Size(181, 16);
			this.labelOther1.TabIndex = 114;
			this.labelOther1.Text = "Други 1 :";
			// 
			// labelCurrentAddress
			// 
			this.labelCurrentAddress.Location = new System.Drawing.Point(486, 95);
			this.labelCurrentAddress.Name = "labelCurrentAddress";
			this.labelCurrentAddress.Size = new System.Drawing.Size(472, 16);
			this.labelCurrentAddress.TabIndex = 112;
			this.labelCurrentAddress.Text = "Настоящ адрес :";
			// 
			// textBoxCurrentAddress
			// 
			this.textBoxCurrentAddress.Location = new System.Drawing.Point(486, 111);
			this.textBoxCurrentAddress.MaxLength = 255;
			this.textBoxCurrentAddress.Name = "textBoxCurrentAddress";
			this.textBoxCurrentAddress.Size = new System.Drawing.Size(472, 20);
			this.textBoxCurrentAddress.TabIndex = 10;
			// 
			// textBoxEngName
			// 
			this.textBoxEngName.Location = new System.Drawing.Point(176, 72);
			this.textBoxEngName.MaxLength = 255;
			this.textBoxEngName.Name = "textBoxEngName";
			this.textBoxEngName.Size = new System.Drawing.Size(427, 20);
			this.textBoxEngName.TabIndex = 6;
			this.textBoxEngName.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// label81
			// 
			this.label81.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label81.Location = new System.Drawing.Point(176, 56);
			this.label81.Name = "label81";
			this.label81.Size = new System.Drawing.Size(427, 16);
			this.label81.TabIndex = 110;
			this.label81.Text = "Name:";
			// 
			// dateTimePickerBirthDate
			// 
			this.dateTimePickerBirthDate.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.dateTimePickerBirthDate.Enabled = false;
			this.dateTimePickerBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerBirthDate.Location = new System.Drawing.Point(9, 72);
			this.dateTimePickerBirthDate.Name = "dateTimePickerBirthDate";
			this.dateTimePickerBirthDate.Size = new System.Drawing.Size(160, 20);
			this.dateTimePickerBirthDate.TabIndex = 5;
			this.dateTimePickerBirthDate.Value = new System.DateTime(2005, 1, 12, 9, 43, 36, 687);
			this.dateTimePickerBirthDate.ValueChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// comboBoxEGN
			// 
			this.comboBoxEGN.Items.AddRange(new object[] {
            "ЕГН",
            "ЛНЧ"});
			this.comboBoxEGN.Location = new System.Drawing.Point(9, 31);
			this.comboBoxEGN.Name = "comboBoxEGN";
			this.comboBoxEGN.Size = new System.Drawing.Size(56, 21);
			this.comboBoxEGN.TabIndex = 0;
			this.comboBoxEGN.SelectedIndexChanged += new System.EventHandler(this.comboBoxEGN_SelectedIndexChanged);
			// 
			// label80
			// 
			this.label80.Location = new System.Drawing.Point(8, 56);
			this.label80.Name = "label80";
			this.label80.Size = new System.Drawing.Size(160, 16);
			this.label80.TabIndex = 106;
			this.label80.Text = "Дата на раждане:";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(775, 134);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(180, 16);
			this.label5.TabIndex = 86;
			this.label5.Text = "Пол :";
			// 
			// comboBoxSex
			// 
			this.comboBoxSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSex.ItemHeight = 13;
			this.comboBoxSex.Location = new System.Drawing.Point(775, 150);
			this.comboBoxSex.Name = "comboBoxSex";
			this.comboBoxSex.Size = new System.Drawing.Size(180, 21);
			this.comboBoxSex.TabIndex = 14;
			this.comboBoxSex.SelectedIndexChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// numBoxPcCard
			// 
			this.numBoxPcCard.Location = new System.Drawing.Point(9, 151);
			this.numBoxPcCard.MaxLength = 255;
			this.numBoxPcCard.Name = "numBoxPcCard";
			this.numBoxPcCard.Size = new System.Drawing.Size(180, 20);
			this.numBoxPcCard.TabIndex = 11;
			this.numBoxPcCard.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// labelJKkwartal
			// 
			this.labelJKkwartal.Location = new System.Drawing.Point(9, 134);
			this.labelJKkwartal.Name = "labelJKkwartal";
			this.labelJKkwartal.Size = new System.Drawing.Size(180, 16);
			this.labelJKkwartal.TabIndex = 79;
			this.labelJKkwartal.Text = "Л.К. Номер :";
			// 
			// labelPublishedByy
			// 
			this.labelPublishedByy.Location = new System.Drawing.Point(392, 134);
			this.labelPublishedByy.Name = "labelPublishedByy";
			this.labelPublishedByy.Size = new System.Drawing.Size(180, 16);
			this.labelPublishedByy.TabIndex = 83;
			this.labelPublishedByy.Text = "Дата на издаване :";
			// 
			// labelPublishedBy
			// 
			this.labelPublishedBy.Location = new System.Drawing.Point(200, 134);
			this.labelPublishedBy.Name = "labelPublishedBy";
			this.labelPublishedBy.Size = new System.Drawing.Size(180, 16);
			this.labelPublishedBy.TabIndex = 81;
			this.labelPublishedBy.Text = "Издадена от :";
			// 
			// textBoxPublishedFrom
			// 
			this.textBoxPublishedFrom.Location = new System.Drawing.Point(200, 151);
			this.textBoxPublishedFrom.MaxLength = 255;
			this.textBoxPublishedFrom.Name = "textBoxPublishedFrom";
			this.textBoxPublishedFrom.Size = new System.Drawing.Size(180, 20);
			this.textBoxPublishedFrom.TabIndex = 12;
			this.textBoxPublishedFrom.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// labelKwartal
			// 
			this.labelKwartal.Location = new System.Drawing.Point(9, 95);
			this.labelKwartal.Name = "labelKwartal";
			this.labelKwartal.Size = new System.Drawing.Size(472, 16);
			this.labelKwartal.TabIndex = 72;
			this.labelKwartal.Text = "Адрес :";
			// 
			// textBoxKwartal
			// 
			this.textBoxKwartal.Location = new System.Drawing.Point(9, 111);
			this.textBoxKwartal.MaxLength = 255;
			this.textBoxKwartal.Name = "textBoxKwartal";
			this.textBoxKwartal.Size = new System.Drawing.Size(472, 20);
			this.textBoxKwartal.TabIndex = 9;
			this.textBoxKwartal.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// labelNaselenoMqsto
			// 
			this.labelNaselenoMqsto.Location = new System.Drawing.Point(788, 56);
			this.labelNaselenoMqsto.Name = "labelNaselenoMqsto";
			this.labelNaselenoMqsto.Size = new System.Drawing.Size(171, 16);
			this.labelNaselenoMqsto.TabIndex = 69;
			this.labelNaselenoMqsto.Text = "Населено място :";
			// 
			// labelRegion
			// 
			this.labelRegion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelRegion.Location = new System.Drawing.Point(609, 56);
			this.labelRegion.Name = "labelRegion";
			this.labelRegion.Size = new System.Drawing.Size(171, 16);
			this.labelRegion.TabIndex = 68;
			this.labelRegion.Text = "Област :";
			// 
			// numBoxEgn
			// 
			this.numBoxEgn.Location = new System.Drawing.Point(72, 32);
			this.numBoxEgn.MaxLength = 10;
			this.numBoxEgn.Name = "numBoxEgn";
			this.numBoxEgn.OnlyInteger = false;
			this.numBoxEgn.OnlyPositive = false;
			this.numBoxEgn.Size = new System.Drawing.Size(96, 20);
			this.numBoxEgn.TabIndex = 1;
			this.toolTip1.SetToolTip(this.numBoxEgn, "Единен Граждански номер на лицето");
			this.numBoxEgn.TextChanged += new System.EventHandler(this.numBoxEgn_TextChanged);
			// 
			// labelEGN
			// 
			this.labelEGN.Location = new System.Drawing.Point(72, 16);
			this.labelEGN.Name = "labelEGN";
			this.labelEGN.Size = new System.Drawing.Size(96, 16);
			this.labelEGN.TabIndex = 1;
			this.labelEGN.Text = "ЕГН :";
			// 
			// textBoxNames
			// 
			this.textBoxNames.Location = new System.Drawing.Point(176, 32);
			this.textBoxNames.MaxLength = 255;
			this.textBoxNames.Name = "textBoxNames";
			this.textBoxNames.Size = new System.Drawing.Size(427, 20);
			this.textBoxNames.TabIndex = 2;
			this.textBoxNames.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// labelNames
			// 
			this.labelNames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelNames.Location = new System.Drawing.Point(176, 16);
			this.labelNames.Name = "labelNames";
			this.labelNames.Size = new System.Drawing.Size(430, 16);
			this.labelNames.TabIndex = 3;
			this.labelNames.Text = "Трите имена на лицето :";
			// 
			// labelCountry
			// 
			this.labelCountry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.labelCountry.Location = new System.Drawing.Point(609, 16);
			this.labelCountry.Name = "labelCountry";
			this.labelCountry.Size = new System.Drawing.Size(170, 16);
			this.labelCountry.TabIndex = 9;
			this.labelCountry.Text = "Гражданство :";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(788, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(170, 16);
			this.label4.TabIndex = 64;
			this.label4.Text = "Месторождение град :";
			// 
			// textBoxBornTown
			// 
			this.textBoxBornTown.Location = new System.Drawing.Point(788, 32);
			this.textBoxBornTown.Name = "textBoxBornTown";
			this.textBoxBornTown.Size = new System.Drawing.Size(170, 20);
			this.textBoxBornTown.TabIndex = 4;
			this.textBoxBornTown.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// dateTimePickerPCCardPublished
			// 
			this.dateTimePickerPCCardPublished.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerPCCardPublished.Location = new System.Drawing.Point(392, 151);
			this.dateTimePickerPCCardPublished.Name = "dateTimePickerPCCardPublished";
			this.dateTimePickerPCCardPublished.Size = new System.Drawing.Size(180, 20);
			this.dateTimePickerPCCardPublished.TabIndex = 13;
			this.dateTimePickerPCCardPublished.Value = new System.DateTime(2005, 1, 12, 9, 33, 28, 578);
			this.dateTimePickerPCCardPublished.ValueChanged += new System.EventHandler(this.dateTimePickerPCCardPublished_ValueChanged);
			// 
			// textBoxTown
			// 
			this.textBoxTown.Location = new System.Drawing.Point(788, 72);
			this.textBoxTown.MaxLength = 255;
			this.textBoxTown.Name = "textBoxTown";
			this.textBoxTown.Size = new System.Drawing.Size(170, 20);
			this.textBoxTown.TabIndex = 8;
			this.textBoxTown.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// textBoxCountry
			// 
			this.textBoxCountry.Location = new System.Drawing.Point(609, 32);
			this.textBoxCountry.MaxLength = 255;
			this.textBoxCountry.Name = "textBoxCountry";
			this.textBoxCountry.Size = new System.Drawing.Size(170, 20);
			this.textBoxCountry.TabIndex = 3;
			this.textBoxCountry.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// textBoxRegion
			// 
			this.textBoxRegion.Location = new System.Drawing.Point(609, 72);
			this.textBoxRegion.MaxLength = 255;
			this.textBoxRegion.Name = "textBoxRegion";
			this.textBoxRegion.Size = new System.Drawing.Size(170, 20);
			this.textBoxRegion.TabIndex = 7;
			this.textBoxRegion.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.buttonDeletePicture);
			this.groupBox6.Controls.Add(this.buttonPicture);
			this.groupBox6.Controls.Add(this.pictureBox1);
			this.groupBox6.Location = new System.Drawing.Point(8, 232);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(168, 274);
			this.groupBox6.TabIndex = 114;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Снимка";
			// 
			// buttonDeletePicture
			// 
			this.buttonDeletePicture.Location = new System.Drawing.Point(10, 245);
			this.buttonDeletePicture.Name = "buttonDeletePicture";
			this.buttonDeletePicture.Size = new System.Drawing.Size(147, 23);
			this.buttonDeletePicture.TabIndex = 5;
			this.buttonDeletePicture.Text = "Изтрий снимка";
			this.buttonDeletePicture.Click += new System.EventHandler(this.buttonDeletePicture_Click);
			// 
			// buttonPicture
			// 
			this.buttonPicture.Location = new System.Drawing.Point(10, 218);
			this.buttonPicture.Name = "buttonPicture";
			this.buttonPicture.Size = new System.Drawing.Size(147, 23);
			this.buttonPicture.TabIndex = 4;
			this.buttonPicture.Text = "Отвори снимка";
			this.buttonPicture.Click += new System.EventHandler(this.buttonPicture_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(9, 16);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(150, 200);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 107;
			this.pictureBox1.TabStop = false;
			// 
			// textBoxOther
			// 
			this.textBoxOther.Location = new System.Drawing.Point(189, 397);
			this.textBoxOther.Multiline = true;
			this.textBoxOther.Name = "textBoxOther";
			this.textBoxOther.Size = new System.Drawing.Size(381, 39);
			this.textBoxOther.TabIndex = 0;
			this.textBoxOther.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// labelLanguage
			// 
			this.labelLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelLanguage.Location = new System.Drawing.Point(582, 378);
			this.labelLanguage.Name = "labelLanguage";
			this.labelLanguage.Size = new System.Drawing.Size(391, 16);
			this.labelLanguage.TabIndex = 106;
			this.labelLanguage.Text = "Чужди езици :";
			// 
			// buttonLanguageEdit
			// 
			this.buttonLanguageEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonLanguageEdit.Image = ((System.Drawing.Image)(resources.GetObject("buttonLanguageEdit.Image")));
			this.buttonLanguageEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonLanguageEdit.Location = new System.Drawing.Point(722, 555);
			this.buttonLanguageEdit.Name = "buttonLanguageEdit";
			this.buttonLanguageEdit.Size = new System.Drawing.Size(120, 23);
			this.buttonLanguageEdit.TabIndex = 11;
			this.buttonLanguageEdit.TabStop = false;
			this.buttonLanguageEdit.Tag = "Редакция на чужд език";
			this.buttonLanguageEdit.Text = "Редакция";
			this.toolTip1.SetToolTip(this.buttonLanguageEdit, "Номенклатура чужди езици");
			this.buttonLanguageEdit.Click += new System.EventHandler(this.buttonLanguageEdit_Click);
			// 
			// label57
			// 
			this.label57.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label57.Location = new System.Drawing.Point(189, 378);
			this.label57.Name = "label57";
			this.label57.Size = new System.Drawing.Size(377, 16);
			this.label57.TabIndex = 121;
			this.label57.Text = "Други :";
			// 
			// tabPageAssignment
			// 
			this.tabPageAssignment.Controls.Add(this.comboBoxEkdaDegree);
			this.tabPageAssignment.Controls.Add(this.label111);
			this.tabPageAssignment.Controls.Add(this.buttonAssignmentExcel);
			this.tabPageAssignment.Controls.Add(this.label86);
			this.tabPageAssignment.Controls.Add(this.label87);
			this.tabPageAssignment.Controls.Add(this.comboBoxTutorName);
			this.tabPageAssignment.Controls.Add(this.comboBoxTutorAbsenceReason);
			this.tabPageAssignment.Controls.Add(this.numBoxBruto);
			this.tabPageAssignment.Controls.Add(this.buttonReasonAssignment);
			this.tabPageAssignment.Controls.Add(this.labelLevel2);
			this.tabPageAssignment.Controls.Add(this.buttonSelectPosition);
			this.tabPageAssignment.Controls.Add(this.radioButtonAssignment);
			this.tabPageAssignment.Controls.Add(this.dateTimePickerContractDate);
			this.tabPageAssignment.Controls.Add(this.label14);
			this.tabPageAssignment.Controls.Add(this.numBoxNumHoliday);
			this.tabPageAssignment.Controls.Add(this.comboBoxYearlyAddon);
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
			this.tabPageAssignment.Controls.Add(this.label12);
			this.tabPageAssignment.Controls.Add(this.label10);
			this.tabPageAssignment.Controls.Add(this.label9);
			this.tabPageAssignment.Controls.Add(this.label6);
			this.tabPageAssignment.Controls.Add(this.labelLevel4);
			this.tabPageAssignment.Controls.Add(this.textBoxContractNumber);
			this.tabPageAssignment.Controls.Add(this.comboBoxAssignReason);
			this.tabPageAssignment.Controls.Add(this.comboBoxContract);
			this.tabPageAssignment.Controls.Add(this.comboBoxPosition);
			this.tabPageAssignment.Controls.Add(this.comboBoxLevel4);
			this.tabPageAssignment.Controls.Add(this.comboBoxLevel3);
			this.tabPageAssignment.Controls.Add(this.comboBoxLevel2);
			this.tabPageAssignment.Controls.Add(this.groupBoxAssignmentGrid);
			this.tabPageAssignment.Controls.Add(this.labelLevel3);
			this.tabPageAssignment.Controls.Add(this.dateTimePickerTestPeriod);
			this.tabPageAssignment.Controls.Add(this.label49);
			this.tabPageAssignment.Controls.Add(this.numBoxAddNumHoliday);
			this.tabPageAssignment.Controls.Add(this.buttonExpCalculator);
			this.tabPageAssignment.Controls.Add(this.numBoxBaseSalary);
			this.tabPageAssignment.Controls.Add(this.numBoxMonthlyAddon);
			this.tabPageAssignment.Controls.Add(this.label7);
			this.tabPageAssignment.Controls.Add(this.label13);
			this.tabPageAssignment.Controls.Add(this.label8);
			this.tabPageAssignment.Controls.Add(this.label83);
			this.tabPageAssignment.Controls.Add(this.label46);
			this.tabPageAssignment.Controls.Add(this.label45);
			this.tabPageAssignment.Controls.Add(this.label17);
			this.tabPageAssignment.Controls.Add(this.label16);
			this.tabPageAssignment.Controls.Add(this.label15);
			this.tabPageAssignment.Controls.Add(this.label41);
			this.tabPageAssignment.Controls.Add(this.label50);
			this.tabPageAssignment.Location = new System.Drawing.Point(4, 22);
			this.tabPageAssignment.Name = "tabPageAssignment";
			this.tabPageAssignment.Size = new System.Drawing.Size(984, 615);
			this.tabPageAssignment.TabIndex = 2;
			this.tabPageAssignment.Text = "Назначаване";
			this.tabPageAssignment.UseVisualStyleBackColor = true;
			// 
			// comboBoxEkdaDegree
			// 
			this.comboBoxEkdaDegree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxEkdaDegree.Items.AddRange(new object[] {
            "",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
			this.comboBoxEkdaDegree.Location = new System.Drawing.Point(826, 289);
			this.comboBoxEkdaDegree.Name = "comboBoxEkdaDegree";
			this.comboBoxEkdaDegree.Size = new System.Drawing.Size(148, 21);
			this.comboBoxEkdaDegree.TabIndex = 125;
			// 
			// label111
			// 
			this.label111.Location = new System.Drawing.Point(826, 274);
			this.label111.Name = "label111";
			this.label111.Size = new System.Drawing.Size(148, 16);
			this.label111.TabIndex = 126;
			this.label111.Text = "Степен на длъжност:";
			// 
			// buttonAssignmentExcel
			// 
			this.buttonAssignmentExcel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonAssignmentExcel.Image = ((System.Drawing.Image)(resources.GetObject("buttonAssignmentExcel.Image")));
			this.buttonAssignmentExcel.Location = new System.Drawing.Point(65, 589);
			this.buttonAssignmentExcel.Name = "buttonAssignmentExcel";
			this.buttonAssignmentExcel.Size = new System.Drawing.Size(27, 23);
			this.buttonAssignmentExcel.TabIndex = 29;
			this.buttonAssignmentExcel.UseVisualStyleBackColor = true;
			this.buttonAssignmentExcel.Click += new System.EventHandler(this.buttonAssignmentExcel_Click);
			// 
			// label86
			// 
			this.label86.Location = new System.Drawing.Point(417, 273);
			this.label86.Name = "label86";
			this.label86.Size = new System.Drawing.Size(95, 16);
			this.label86.TabIndex = 12;
			this.label86.Text = "Вид отсъствие:";
			// 
			// label87
			// 
			this.label87.Location = new System.Drawing.Point(8, 273);
			this.label87.Name = "label87";
			this.label87.Size = new System.Drawing.Size(200, 16);
			this.label87.TabIndex = 124;
			this.label87.Text = "Заместван служител:";
			// 
			// comboBoxTutorName
			// 
			this.comboBoxTutorName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxTutorName.DropDownWidth = 370;
			this.comboBoxTutorName.Location = new System.Drawing.Point(8, 289);
			this.comboBoxTutorName.Name = "comboBoxTutorName";
			this.comboBoxTutorName.Size = new System.Drawing.Size(404, 21);
			this.comboBoxTutorName.TabIndex = 27;
			// 
			// comboBoxTutorAbsenceReason
			// 
			this.comboBoxTutorAbsenceReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxTutorAbsenceReason.DropDownWidth = 370;
			this.comboBoxTutorAbsenceReason.Items.AddRange(new object[] {
            "",
            "Полагаем годишен отпуск",
            "Болнични",
            "Неплатен отпуск",
            "Платен отпуск",
            "Отглеждане на дете",
            "Командировка",
            "Полагаем отпуск минали години",
            "Обучение"});
			this.comboBoxTutorAbsenceReason.Location = new System.Drawing.Point(418, 289);
			this.comboBoxTutorAbsenceReason.Name = "comboBoxTutorAbsenceReason";
			this.comboBoxTutorAbsenceReason.Size = new System.Drawing.Size(402, 21);
			this.comboBoxTutorAbsenceReason.TabIndex = 28;
			// 
			// numBoxBruto
			// 
			this.numBoxBruto.Location = new System.Drawing.Point(554, 249);
			this.numBoxBruto.Name = "numBoxBruto";
			this.numBoxBruto.Size = new System.Drawing.Size(130, 20);
			this.numBoxBruto.TabIndex = 23;
			// 
			// buttonReasonAssignment
			// 
			this.buttonReasonAssignment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonReasonAssignment.Image = ((System.Drawing.Image)(resources.GetObject("buttonReasonAssignment.Image")));
			this.buttonReasonAssignment.Location = new System.Drawing.Point(594, 168);
			this.buttonReasonAssignment.Name = "buttonReasonAssignment";
			this.buttonReasonAssignment.Size = new System.Drawing.Size(21, 21);
			this.buttonReasonAssignment.TabIndex = 118;
			this.buttonReasonAssignment.TabStop = false;
			this.buttonReasonAssignment.Tag = "Добавяне на данни към номенклатурата за научно звание";
			this.toolTip1.SetToolTip(this.buttonReasonAssignment, "Номенклатура научно звание");
			this.buttonReasonAssignment.Click += new System.EventHandler(this.buttonReasonAssignment_Click);
			// 
			// labelLevel2
			// 
			this.labelLevel2.Location = new System.Drawing.Point(495, 33);
			this.labelLevel2.Name = "labelLevel2";
			this.labelLevel2.Size = new System.Drawing.Size(480, 16);
			this.labelLevel2.TabIndex = 18;
			this.labelLevel2.Text = "Дирекция :";
			// 
			// buttonSelectPosition
			// 
			this.buttonSelectPosition.Image = ((System.Drawing.Image)(resources.GetObject("buttonSelectPosition.Image")));
			this.buttonSelectPosition.Location = new System.Drawing.Point(349, 128);
			this.buttonSelectPosition.Name = "buttonSelectPosition";
			this.buttonSelectPosition.Size = new System.Drawing.Size(21, 21);
			this.buttonSelectPosition.TabIndex = 89;
			this.buttonSelectPosition.Click += new System.EventHandler(this.buttonSelectPosition_Click);
			// 
			// radioButtonAssignment
			// 
			this.radioButtonAssignment.Checked = true;
			this.radioButtonAssignment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.radioButtonAssignment.Location = new System.Drawing.Point(10, 4);
			this.radioButtonAssignment.Name = "radioButtonAssignment";
			this.radioButtonAssignment.Size = new System.Drawing.Size(136, 24);
			this.radioButtonAssignment.TabIndex = 29;
			this.radioButtonAssignment.TabStop = true;
			this.radioButtonAssignment.Text = "Назначение";
			// 
			// dateTimePickerContractDate
			// 
			this.dateTimePickerContractDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerContractDate.Location = new System.Drawing.Point(796, 209);
			this.dateTimePickerContractDate.Name = "dateTimePickerContractDate";
			this.dateTimePickerContractDate.Size = new System.Drawing.Size(180, 20);
			this.dateTimePickerContractDate.TabIndex = 18;
			this.toolTip1.SetToolTip(this.dateTimePickerContractDate, "Дата на сключване на трудовия договор");
			this.dateTimePickerContractDate.Value = new System.DateTime(2005, 9, 12, 9, 43, 0, 0);
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(796, 193);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(176, 16);
			this.label14.TabIndex = 88;
			this.label14.Text = "Договор от дата:";
			this.toolTip1.SetToolTip(this.label14, "Дата на сключване на трудовия договор");
			// 
			// numBoxNumHoliday
			// 
			this.numBoxNumHoliday.Location = new System.Drawing.Point(826, 249);
			this.numBoxNumHoliday.Name = "numBoxNumHoliday";
			this.numBoxNumHoliday.Size = new System.Drawing.Size(65, 20);
			this.numBoxNumHoliday.TabIndex = 25;
			this.numBoxNumHoliday.Text = "0";
			// 
			// comboBoxYearlyAddon
			// 
			this.comboBoxYearlyAddon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxYearlyAddon.Location = new System.Drawing.Point(690, 248);
			this.comboBoxYearlyAddon.Name = "comboBoxYearlyAddon";
			this.comboBoxYearlyAddon.Size = new System.Drawing.Size(130, 21);
			this.comboBoxYearlyAddon.TabIndex = 24;
			// 
			// textBoxSalaryAddon
			// 
			this.textBoxSalaryAddon.Location = new System.Drawing.Point(146, 249);
			this.textBoxSalaryAddon.Name = "textBoxSalaryAddon";
			this.textBoxSalaryAddon.Size = new System.Drawing.Size(130, 20);
			this.textBoxSalaryAddon.TabIndex = 20;
			this.toolTip1.SetToolTip(this.textBoxSalaryAddon, "Добавки към основната заплата");
			this.textBoxSalaryAddon.TextChanged += new System.EventHandler(this.Salary_Changed);
			// 
			// textBoxClassPercent
			// 
			this.textBoxClassPercent.Location = new System.Drawing.Point(282, 249);
			this.textBoxClassPercent.Name = "textBoxClassPercent";
			this.textBoxClassPercent.Size = new System.Drawing.Size(130, 20);
			this.textBoxClassPercent.TabIndex = 21;
			this.toolTip1.SetToolTip(this.textBoxClassPercent, "Процент прослужено време");
			this.textBoxClassPercent.TextChanged += new System.EventHandler(this.Salary_Changed);
			// 
			// comboBoxWorkTime
			// 
			this.comboBoxWorkTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxWorkTime.Location = new System.Drawing.Point(617, 168);
			this.comboBoxWorkTime.Name = "comboBoxWorkTime";
			this.comboBoxWorkTime.Size = new System.Drawing.Size(168, 21);
			this.comboBoxWorkTime.TabIndex = 10;
			// 
			// label40
			// 
			this.label40.Location = new System.Drawing.Point(848, 113);
			this.label40.Name = "label40";
			this.label40.Size = new System.Drawing.Size(127, 16);
			this.label40.TabIndex = 16;
			this.label40.Text = "Код по НКПД :";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(496, 113);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(346, 16);
			this.label11.TabIndex = 15;
			this.label11.Text = "Длъжност по НКПД :";
			// 
			// textBoxNKPCode
			// 
			this.textBoxNKPCode.Location = new System.Drawing.Point(848, 130);
			this.textBoxNKPCode.Name = "textBoxNKPCode";
			this.textBoxNKPCode.ReadOnly = true;
			this.textBoxNKPCode.Size = new System.Drawing.Size(128, 20);
			this.textBoxNKPCode.TabIndex = 7;
			this.textBoxNKPCode.TabStop = false;
			// 
			// textBoxNKPLevel
			// 
			this.textBoxNKPLevel.Location = new System.Drawing.Point(496, 130);
			this.textBoxNKPLevel.Name = "textBoxNKPLevel";
			this.textBoxNKPLevel.ReadOnly = true;
			this.textBoxNKPLevel.Size = new System.Drawing.Size(346, 20);
			this.textBoxNKPLevel.TabIndex = 6;
			this.textBoxNKPLevel.TabStop = false;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(376, 113);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(112, 16);
			this.label18.TabIndex = 59;
			this.label18.Text = "Правоотношение :";
			// 
			// comboBoxLaw
			// 
			this.comboBoxLaw.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxLaw.Location = new System.Drawing.Point(376, 129);
			this.comboBoxLaw.Name = "comboBoxLaw";
			this.comboBoxLaw.Size = new System.Drawing.Size(112, 21);
			this.comboBoxLaw.TabIndex = 5;
			// 
			// labelLevel1
			// 
			this.labelLevel1.Location = new System.Drawing.Point(8, 33);
			this.labelLevel1.Name = "labelLevel1";
			this.labelLevel1.Size = new System.Drawing.Size(480, 16);
			this.labelLevel1.TabIndex = 27;
			this.labelLevel1.Text = "Администрация:";
			// 
			// comboBoxLevel1
			// 
			this.comboBoxLevel1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxLevel1.DropDownWidth = 370;
			this.comboBoxLevel1.Location = new System.Drawing.Point(8, 49);
			this.comboBoxLevel1.Name = "comboBoxLevel1";
			this.comboBoxLevel1.Size = new System.Drawing.Size(480, 21);
			this.comboBoxLevel1.TabIndex = 0;
			this.comboBoxLevel1.SelectedIndexChanged += new System.EventHandler(this.comboBoxLevel1_SelectedIndexChanged);
			// 
			// label39
			// 
			this.label39.Location = new System.Drawing.Point(566, 193);
			this.label39.Name = "label39";
			this.label39.Size = new System.Drawing.Size(224, 16);
			this.label39.TabIndex = 15;
			this.label39.Text = "Трудов стаж (ГГ, ММ, ДД) :";
			// 
			// numBoxAssignmentExpD
			// 
			this.numBoxAssignmentExpD.Location = new System.Drawing.Point(698, 209);
			this.numBoxAssignmentExpD.MaxLength = 2;
			this.numBoxAssignmentExpD.Name = "numBoxAssignmentExpD";
			this.numBoxAssignmentExpD.Size = new System.Drawing.Size(60, 20);
			this.numBoxAssignmentExpD.TabIndex = 17;
			this.numBoxAssignmentExpD.Text = "0";
			// 
			// numBoxAssignmentExtM
			// 
			this.numBoxAssignmentExtM.Location = new System.Drawing.Point(632, 209);
			this.numBoxAssignmentExtM.MaxLength = 2;
			this.numBoxAssignmentExtM.Name = "numBoxAssignmentExtM";
			this.numBoxAssignmentExtM.Size = new System.Drawing.Size(60, 20);
			this.numBoxAssignmentExtM.TabIndex = 16;
			this.numBoxAssignmentExtM.Text = "0";
			// 
			// numBoxAssignmentExpY
			// 
			this.numBoxAssignmentExpY.Location = new System.Drawing.Point(566, 209);
			this.numBoxAssignmentExpY.MaxLength = 3;
			this.numBoxAssignmentExpY.Name = "numBoxAssignmentExpY";
			this.numBoxAssignmentExpY.Size = new System.Drawing.Size(60, 20);
			this.numBoxAssignmentExpY.TabIndex = 15;
			this.numBoxAssignmentExpY.Text = "0";
			this.numBoxAssignmentExpY.TextChanged += new System.EventHandler(this.numBoxAssignmentExpY_TextChanged);
			// 
			// buttonAssignmentCancel
			// 
			this.buttonAssignmentCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonAssignmentCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonAssignmentCancel.Image")));
			this.buttonAssignmentCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonAssignmentCancel.Location = new System.Drawing.Point(238, 589);
			this.buttonAssignmentCancel.Name = "buttonAssignmentCancel";
			this.buttonAssignmentCancel.Size = new System.Drawing.Size(130, 23);
			this.buttonAssignmentCancel.TabIndex = 31;
			this.buttonAssignmentCancel.Text = "Отказ";
			this.toolTip1.SetToolTip(this.buttonAssignmentCancel, "Отказ от записването на данни");
			this.buttonAssignmentCancel.Click += new System.EventHandler(this.buttonAssignmentCancel_Click);
			// 
			// buttonAssignmentPrint
			// 
			this.buttonAssignmentPrint.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonAssignmentPrint.Image = ((System.Drawing.Image)(resources.GetObject("buttonAssignmentPrint.Image")));
			this.buttonAssignmentPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonAssignmentPrint.Location = new System.Drawing.Point(100, 589);
			this.buttonAssignmentPrint.Name = "buttonAssignmentPrint";
			this.buttonAssignmentPrint.Size = new System.Drawing.Size(130, 23);
			this.buttonAssignmentPrint.TabIndex = 30;
			this.buttonAssignmentPrint.Text = "Печат";
			this.toolTip1.SetToolTip(this.buttonAssignmentPrint, "Печат на трудов договор или допълнително споразумение");
			this.buttonAssignmentPrint.Click += new System.EventHandler(this.buttonPrintD_Click);
			// 
			// dateTimePickerContractExpiry
			// 
			this.dateTimePickerContractExpiry.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerContractExpiry.Location = new System.Drawing.Point(380, 209);
			this.dateTimePickerContractExpiry.Name = "dateTimePickerContractExpiry";
			this.dateTimePickerContractExpiry.Size = new System.Drawing.Size(180, 20);
			this.dateTimePickerContractExpiry.TabIndex = 14;
			this.toolTip1.SetToolTip(this.dateTimePickerContractExpiry, "Дата на изтичане на договора");
			this.dateTimePickerContractExpiry.Value = new System.DateTime(2005, 9, 12, 9, 43, 0, 0);
			// 
			// buttonAssignmentDelete
			// 
			this.buttonAssignmentDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonAssignmentDelete.Image = ((System.Drawing.Image)(resources.GetObject("buttonAssignmentDelete.Image")));
			this.buttonAssignmentDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonAssignmentDelete.Location = new System.Drawing.Point(514, 589);
			this.buttonAssignmentDelete.Name = "buttonAssignmentDelete";
			this.buttonAssignmentDelete.Size = new System.Drawing.Size(130, 23);
			this.buttonAssignmentDelete.TabIndex = 33;
			this.buttonAssignmentDelete.Text = "Премахва";
			this.toolTip1.SetToolTip(this.buttonAssignmentDelete, "Премахване на избраното назначение");
			this.buttonAssignmentDelete.Click += new System.EventHandler(this.buttonAssignmentDelete_Click);
			// 
			// buttonAssignmentSave
			// 
			this.buttonAssignmentSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonAssignmentSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonAssignmentSave.Image")));
			this.buttonAssignmentSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonAssignmentSave.Location = new System.Drawing.Point(376, 589);
			this.buttonAssignmentSave.Name = "buttonAssignmentSave";
			this.buttonAssignmentSave.Size = new System.Drawing.Size(130, 23);
			this.buttonAssignmentSave.TabIndex = 32;
			this.buttonAssignmentSave.Text = "Запис";
			this.toolTip1.SetToolTip(this.buttonAssignmentSave, "Запис на данните");
			this.buttonAssignmentSave.Click += new System.EventHandler(this.buttonAssignmentSave_Click);
			// 
			// buttonAssignmentEdit
			// 
			this.buttonAssignmentEdit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonAssignmentEdit.Image = ((System.Drawing.Image)(resources.GetObject("buttonAssignmentEdit.Image")));
			this.buttonAssignmentEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonAssignmentEdit.Location = new System.Drawing.Point(652, 589);
			this.buttonAssignmentEdit.Name = "buttonAssignmentEdit";
			this.buttonAssignmentEdit.Size = new System.Drawing.Size(130, 23);
			this.buttonAssignmentEdit.TabIndex = 34;
			this.buttonAssignmentEdit.Text = "Корекция";
			this.toolTip1.SetToolTip(this.buttonAssignmentEdit, "Корекция на данните за избраното назначение");
			this.buttonAssignmentEdit.Click += new System.EventHandler(this.buttonAssignmentEdit_Click);
			// 
			// radioButtonAdditional
			// 
			this.radioButtonAdditional.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.radioButtonAdditional.Location = new System.Drawing.Point(499, 4);
			this.radioButtonAdditional.Name = "radioButtonAdditional";
			this.radioButtonAdditional.Size = new System.Drawing.Size(272, 24);
			this.radioButtonAdditional.TabIndex = 30;
			this.radioButtonAdditional.Text = "Допълнителни споразумeния";
			this.radioButtonAdditional.CheckedChanged += new System.EventHandler(this.radioButtonAdditional_CheckedChanged);
			// 
			// buttonAssignment
			// 
			this.buttonAssignment.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonAssignment.Image = ((System.Drawing.Image)(resources.GetObject("buttonAssignment.Image")));
			this.buttonAssignment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonAssignment.Location = new System.Drawing.Point(790, 589);
			this.buttonAssignment.Name = "buttonAssignment";
			this.buttonAssignment.Size = new System.Drawing.Size(130, 23);
			this.buttonAssignment.TabIndex = 35;
			this.buttonAssignment.Text = "Назначаване";
			this.toolTip1.SetToolTip(this.buttonAssignment, "Назначаване на длъжност");
			this.buttonAssignment.Click += new System.EventHandler(this.buttonAssignment_Click);
			// 
			// dateTimePickerAssignedAt
			// 
			this.dateTimePickerAssignedAt.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerAssignedAt.Location = new System.Drawing.Point(8, 209);
			this.dateTimePickerAssignedAt.Name = "dateTimePickerAssignedAt";
			this.dateTimePickerAssignedAt.Size = new System.Drawing.Size(180, 20);
			this.dateTimePickerAssignedAt.TabIndex = 12;
			this.toolTip1.SetToolTip(this.dateTimePickerAssignedAt, "Дата на, която договора влиза в сила");
			this.dateTimePickerAssignedAt.Value = new System.DateTime(2005, 1, 12, 9, 43, 37, 546);
			this.dateTimePickerAssignedAt.ValueChanged += new System.EventHandler(this.dateTimePickerAssignedAt_ValueChanged);
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(788, 153);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(183, 16);
			this.label12.TabIndex = 51;
			this.label12.Text = "Договор N:";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(8, 193);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(180, 16);
			this.label10.TabIndex = 25;
			this.label10.Text = "Назначен на:";
			this.toolTip1.SetToolTip(this.label10, "Дата на, която договора влиза в сила");
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(380, 193);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(180, 16);
			this.label9.TabIndex = 24;
			this.label9.Text = "Договор до :";
			this.toolTip1.SetToolTip(this.label9, "Дата на изтичане на договора");
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 113);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(335, 16);
			this.label6.TabIndex = 21;
			this.label6.Text = "Длъжност :";
			// 
			// labelLevel4
			// 
			this.labelLevel4.Location = new System.Drawing.Point(496, 73);
			this.labelLevel4.Name = "labelLevel4";
			this.labelLevel4.Size = new System.Drawing.Size(480, 16);
			this.labelLevel4.TabIndex = 20;
			this.labelLevel4.Text = "Сектор :";
			// 
			// textBoxContractNumber
			// 
			this.textBoxContractNumber.Location = new System.Drawing.Point(789, 169);
			this.textBoxContractNumber.Name = "textBoxContractNumber";
			this.textBoxContractNumber.Size = new System.Drawing.Size(185, 20);
			this.textBoxContractNumber.TabIndex = 11;
			// 
			// comboBoxAssignReason
			// 
			this.comboBoxAssignReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxAssignReason.Location = new System.Drawing.Point(212, 168);
			this.comboBoxAssignReason.Name = "comboBoxAssignReason";
			this.comboBoxAssignReason.Size = new System.Drawing.Size(378, 21);
			this.comboBoxAssignReason.TabIndex = 9;
			this.comboBoxAssignReason.SelectedIndexChanged += new System.EventHandler(this.comboBoxAssignReason_SelectedIndexChanged);
			// 
			// comboBoxContract
			// 
			this.comboBoxContract.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxContract.Location = new System.Drawing.Point(8, 168);
			this.comboBoxContract.Name = "comboBoxContract";
			this.comboBoxContract.Size = new System.Drawing.Size(200, 21);
			this.comboBoxContract.TabIndex = 8;
			this.comboBoxContract.SelectedIndexChanged += new System.EventHandler(this.comboBoxContract_SelectedIndexChanged);
			// 
			// comboBoxPosition
			// 
			this.comboBoxPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxPosition.Location = new System.Drawing.Point(8, 129);
			this.comboBoxPosition.Name = "comboBoxPosition";
			this.comboBoxPosition.Size = new System.Drawing.Size(335, 21);
			this.comboBoxPosition.TabIndex = 4;
			this.comboBoxPosition.SelectedIndexChanged += new System.EventHandler(this.comboBoxPosition_SelectedIndexChanged);
			// 
			// comboBoxLevel4
			// 
			this.comboBoxLevel4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxLevel4.DropDownWidth = 370;
			this.comboBoxLevel4.Location = new System.Drawing.Point(496, 89);
			this.comboBoxLevel4.Name = "comboBoxLevel4";
			this.comboBoxLevel4.Size = new System.Drawing.Size(478, 21);
			this.comboBoxLevel4.TabIndex = 3;
			this.comboBoxLevel4.SelectedIndexChanged += new System.EventHandler(this.comboBoxLevel4_SelectedIndexChanged);
			// 
			// comboBoxLevel3
			// 
			this.comboBoxLevel3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxLevel3.DropDownWidth = 370;
			this.comboBoxLevel3.Location = new System.Drawing.Point(8, 89);
			this.comboBoxLevel3.Name = "comboBoxLevel3";
			this.comboBoxLevel3.Size = new System.Drawing.Size(480, 21);
			this.comboBoxLevel3.TabIndex = 2;
			this.comboBoxLevel3.SelectedIndexChanged += new System.EventHandler(this.comboBoxLevel3_SelectedIndexChanged);
			// 
			// comboBoxLevel2
			// 
			this.comboBoxLevel2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxLevel2.DropDownWidth = 370;
			this.comboBoxLevel2.Location = new System.Drawing.Point(496, 49);
			this.comboBoxLevel2.Name = "comboBoxLevel2";
			this.comboBoxLevel2.Size = new System.Drawing.Size(480, 21);
			this.comboBoxLevel2.TabIndex = 1;
			this.comboBoxLevel2.SelectedIndexChanged += new System.EventHandler(this.comboBoxLevel2_SelectedIndexChanged);
			// 
			// groupBoxAssignmentGrid
			// 
			this.groupBoxAssignmentGrid.Controls.Add(this.dataGridViewAssignment);
			this.groupBoxAssignmentGrid.Location = new System.Drawing.Point(8, 320);
			this.groupBoxAssignmentGrid.Name = "groupBoxAssignmentGrid";
			this.groupBoxAssignmentGrid.Size = new System.Drawing.Size(968, 263);
			this.groupBoxAssignmentGrid.TabIndex = 48;
			this.groupBoxAssignmentGrid.TabStop = false;
			this.groupBoxAssignmentGrid.Text = "Регистър на назначенията";
			// 
			// dataGridViewAssignment
			// 
			this.dataGridViewAssignment.AllowUserToAddRows = false;
			this.dataGridViewAssignment.AllowUserToDeleteRows = false;
			this.dataGridViewAssignment.AllowUserToResizeRows = false;
			this.dataGridViewAssignment.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			dataGridViewCellStyle76.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle76.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle76.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle76.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle76.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle76.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle76.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewAssignment.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle76;
			this.dataGridViewAssignment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle77.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle77.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle77.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle77.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle77.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle77.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle77.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewAssignment.DefaultCellStyle = dataGridViewCellStyle77;
			this.dataGridViewAssignment.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewAssignment.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridViewAssignment.Location = new System.Drawing.Point(3, 16);
			this.dataGridViewAssignment.MultiSelect = false;
			this.dataGridViewAssignment.Name = "dataGridViewAssignment";
			this.dataGridViewAssignment.ReadOnly = true;
			dataGridViewCellStyle78.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle78.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle78.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle78.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle78.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle78.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle78.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewAssignment.RowHeadersDefaultCellStyle = dataGridViewCellStyle78;
			this.dataGridViewAssignment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewAssignment.Size = new System.Drawing.Size(962, 244);
			this.dataGridViewAssignment.TabIndex = 0;
			this.dataGridViewAssignment.Click += new System.EventHandler(this.dataGridViewAssignment_Click);
			// 
			// labelLevel3
			// 
			this.labelLevel3.Location = new System.Drawing.Point(8, 73);
			this.labelLevel3.Name = "labelLevel3";
			this.labelLevel3.Size = new System.Drawing.Size(480, 16);
			this.labelLevel3.TabIndex = 19;
			this.labelLevel3.Text = "Отдел :";
			// 
			// dateTimePickerTestPeriod
			// 
			this.dateTimePickerTestPeriod.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerTestPeriod.Location = new System.Drawing.Point(194, 209);
			this.dateTimePickerTestPeriod.Name = "dateTimePickerTestPeriod";
			this.dateTimePickerTestPeriod.Size = new System.Drawing.Size(180, 20);
			this.dateTimePickerTestPeriod.TabIndex = 13;
			this.toolTip1.SetToolTip(this.dateTimePickerTestPeriod, "Дата на изтичане на изпитателния срок");
			this.dateTimePickerTestPeriod.Value = new System.DateTime(2005, 1, 12, 9, 43, 37, 546);
			// 
			// label49
			// 
			this.label49.Location = new System.Drawing.Point(194, 193);
			this.label49.Name = "label49";
			this.label49.Size = new System.Drawing.Size(180, 16);
			this.label49.TabIndex = 25;
			this.label49.Text = "Изпитателен срок до:";
			this.toolTip1.SetToolTip(this.label49, "Дата на изтичане на изпитателния срок");
			// 
			// numBoxAddNumHoliday
			// 
			this.numBoxAddNumHoliday.Location = new System.Drawing.Point(907, 249);
			this.numBoxAddNumHoliday.Name = "numBoxAddNumHoliday";
			this.numBoxAddNumHoliday.Size = new System.Drawing.Size(65, 20);
			this.numBoxAddNumHoliday.TabIndex = 26;
			this.numBoxAddNumHoliday.Text = "0";
			// 
			// buttonExpCalculator
			// 
			this.buttonExpCalculator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonExpCalculator.Image = ((System.Drawing.Image)(resources.GetObject("buttonExpCalculator.Image")));
			this.buttonExpCalculator.Location = new System.Drawing.Point(764, 209);
			this.buttonExpCalculator.Name = "buttonExpCalculator";
			this.buttonExpCalculator.Size = new System.Drawing.Size(26, 20);
			this.buttonExpCalculator.TabIndex = 118;
			this.buttonExpCalculator.TabStop = false;
			this.buttonExpCalculator.Tag = "Добавяне на данни към номенклатурата за научно звание";
			this.toolTip1.SetToolTip(this.buttonExpCalculator, "Калкулатор трудов стаж");
			this.buttonExpCalculator.Click += new System.EventHandler(this.buttonExpCalculator_Click);
			// 
			// numBoxBaseSalary
			// 
			this.numBoxBaseSalary.Location = new System.Drawing.Point(10, 249);
			this.numBoxBaseSalary.Name = "numBoxBaseSalary";
			this.numBoxBaseSalary.Size = new System.Drawing.Size(130, 20);
			this.numBoxBaseSalary.TabIndex = 19;
			this.toolTip1.SetToolTip(this.numBoxBaseSalary, "Основна залата");
			this.numBoxBaseSalary.TextChanged += new System.EventHandler(this.Salary_Changed);
			// 
			// numBoxMonthlyAddon
			// 
			this.numBoxMonthlyAddon.Location = new System.Drawing.Point(418, 249);
			this.numBoxMonthlyAddon.Name = "numBoxMonthlyAddon";
			this.numBoxMonthlyAddon.Size = new System.Drawing.Size(130, 20);
			this.numBoxMonthlyAddon.TabIndex = 22;
			this.numBoxMonthlyAddon.TextChanged += new System.EventHandler(this.Salary_Changed);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 153);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(200, 16);
			this.label7.TabIndex = 22;
			this.label7.Text = "Тип договор:";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(212, 153);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(382, 16);
			this.label13.TabIndex = 28;
			this.label13.Text = "Основание за назначаване:";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(617, 153);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(168, 16);
			this.label8.TabIndex = 79;
			this.label8.Text = "Работно време:";
			// 
			// label83
			// 
			this.label83.Location = new System.Drawing.Point(554, 233);
			this.label83.Name = "label83";
			this.label83.Size = new System.Drawing.Size(138, 16);
			this.label83.TabIndex = 20;
			this.label83.Text = "Брутно възнаграждение :";
			// 
			// label46
			// 
			this.label46.Location = new System.Drawing.Point(416, 233);
			this.label46.Name = "label46";
			this.label46.Size = new System.Drawing.Size(130, 16);
			this.label46.TabIndex = 21;
			this.label46.Text = "Месечни надбавки :";
			// 
			// label45
			// 
			this.label45.Location = new System.Drawing.Point(690, 233);
			this.label45.Name = "label45";
			this.label45.Size = new System.Drawing.Size(130, 16);
			this.label45.TabIndex = 84;
			this.label45.Text = "Годишни надбавки :";
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(146, 233);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(130, 16);
			this.label17.TabIndex = 83;
			this.label17.Text = "% Надбавки :";
			this.toolTip1.SetToolTip(this.label17, "Добавки към основната заплата");
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(282, 233);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(130, 16);
			this.label16.TabIndex = 82;
			this.label16.Text = "% пр. време:";
			this.toolTip1.SetToolTip(this.label16, "Процент прослужено време");
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(10, 233);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(130, 16);
			this.label15.TabIndex = 81;
			this.label15.Text = "Осн. заплата:";
			this.toolTip1.SetToolTip(this.label15, "Основна заплата");
			// 
			// label41
			// 
			this.label41.ForeColor = System.Drawing.Color.Black;
			this.label41.Location = new System.Drawing.Point(826, 233);
			this.label41.Name = "label41";
			this.label41.Size = new System.Drawing.Size(145, 16);
			this.label41.TabIndex = 54;
			this.label41.Text = "Полагаем отпуск :";
			// 
			// label50
			// 
			this.label50.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label50.Location = new System.Drawing.Point(890, 249);
			this.label50.Name = "label50";
			this.label50.Size = new System.Drawing.Size(16, 16);
			this.label50.TabIndex = 18;
			this.label50.Text = "+";
			// 
			// tabPageAbsence
			// 
			this.tabPageAbsence.Controls.Add(this.buttonAbsenceExcel);
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
			this.tabPageAbsence.Size = new System.Drawing.Size(984, 615);
			this.tabPageAbsence.TabIndex = 3;
			this.tabPageAbsence.Text = "Отсъствия";
			this.tabPageAbsence.UseVisualStyleBackColor = true;
			// 
			// buttonAbsenceExcel
			// 
			this.buttonAbsenceExcel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonAbsenceExcel.Image = ((System.Drawing.Image)(resources.GetObject("buttonAbsenceExcel.Image")));
			this.buttonAbsenceExcel.Location = new System.Drawing.Point(65, 589);
			this.buttonAbsenceExcel.Name = "buttonAbsenceExcel";
			this.buttonAbsenceExcel.Size = new System.Drawing.Size(27, 23);
			this.buttonAbsenceExcel.TabIndex = 0;
			this.buttonAbsenceExcel.UseVisualStyleBackColor = true;
			this.buttonAbsenceExcel.Click += new System.EventHandler(this.buttonAbsenceExcel_Click);
			// 
			// buttonAbsenceCancel
			// 
			this.buttonAbsenceCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonAbsenceCancel.Image")));
			this.buttonAbsenceCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonAbsenceCancel.Location = new System.Drawing.Point(238, 589);
			this.buttonAbsenceCancel.Name = "buttonAbsenceCancel";
			this.buttonAbsenceCancel.Size = new System.Drawing.Size(130, 23);
			this.buttonAbsenceCancel.TabIndex = 2;
			this.buttonAbsenceCancel.Tag = "Отказ от запис на данните";
			this.buttonAbsenceCancel.Text = "Отказ";
			this.buttonAbsenceCancel.Click += new System.EventHandler(this.buttonAbsenceCancel_Click);
			// 
			// buttonAbsencePrint
			// 
			this.buttonAbsencePrint.Image = ((System.Drawing.Image)(resources.GetObject("buttonAbsencePrint.Image")));
			this.buttonAbsencePrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonAbsencePrint.Location = new System.Drawing.Point(100, 589);
			this.buttonAbsencePrint.Name = "buttonAbsencePrint";
			this.buttonAbsencePrint.Size = new System.Drawing.Size(130, 23);
			this.buttonAbsencePrint.TabIndex = 1;
			this.buttonAbsencePrint.Tag = "Печат на бланка за отсъствие";
			this.buttonAbsencePrint.Text = "Печат";
			this.buttonAbsencePrint.Click += new System.EventHandler(this.buttonPrintD_Click);
			// 
			// buttonAbsenceDelete
			// 
			this.buttonAbsenceDelete.Image = ((System.Drawing.Image)(resources.GetObject("buttonAbsenceDelete.Image")));
			this.buttonAbsenceDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonAbsenceDelete.Location = new System.Drawing.Point(514, 589);
			this.buttonAbsenceDelete.Name = "buttonAbsenceDelete";
			this.buttonAbsenceDelete.Size = new System.Drawing.Size(130, 23);
			this.buttonAbsenceDelete.TabIndex = 4;
			this.buttonAbsenceDelete.Tag = "Премахване на отсъствие";
			this.buttonAbsenceDelete.Text = "Премахва";
			this.buttonAbsenceDelete.Click += new System.EventHandler(this.buttonAbsenceDelete_Click);
			// 
			// buttonAbsenceSave
			// 
			this.buttonAbsenceSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonAbsenceSave.Image")));
			this.buttonAbsenceSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonAbsenceSave.Location = new System.Drawing.Point(376, 589);
			this.buttonAbsenceSave.Name = "buttonAbsenceSave";
			this.buttonAbsenceSave.Size = new System.Drawing.Size(130, 23);
			this.buttonAbsenceSave.TabIndex = 3;
			this.buttonAbsenceSave.Tag = "Запис на данните";
			this.buttonAbsenceSave.Text = "Запис";
			this.buttonAbsenceSave.Click += new System.EventHandler(this.buttonAbsenceSave_Click);
			// 
			// buttonAbsenceEdit
			// 
			this.buttonAbsenceEdit.Image = ((System.Drawing.Image)(resources.GetObject("buttonAbsenceEdit.Image")));
			this.buttonAbsenceEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonAbsenceEdit.Location = new System.Drawing.Point(652, 589);
			this.buttonAbsenceEdit.Name = "buttonAbsenceEdit";
			this.buttonAbsenceEdit.Size = new System.Drawing.Size(130, 23);
			this.buttonAbsenceEdit.TabIndex = 5;
			this.buttonAbsenceEdit.Tag = "Корекция на данните за избраното отсъствие";
			this.buttonAbsenceEdit.Text = "Корекция";
			this.buttonAbsenceEdit.Click += new System.EventHandler(this.buttonAbsenceEdit_Click);
			// 
			// buttonAbsenceAdd
			// 
			this.buttonAbsenceAdd.Image = ((System.Drawing.Image)(resources.GetObject("buttonAbsenceAdd.Image")));
			this.buttonAbsenceAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonAbsenceAdd.Location = new System.Drawing.Point(790, 589);
			this.buttonAbsenceAdd.Name = "buttonAbsenceAdd";
			this.buttonAbsenceAdd.Size = new System.Drawing.Size(130, 23);
			this.buttonAbsenceAdd.TabIndex = 6;
			this.buttonAbsenceAdd.Tag = "Въвеждане на ново отсъствие";
			this.buttonAbsenceAdd.Text = "Отсъствие";
			this.buttonAbsenceAdd.Click += new System.EventHandler(this.buttonAbsenceAdd_Click);
			// 
			// groupBoxAbsenceGrid
			// 
			this.groupBoxAbsenceGrid.Controls.Add(this.groupBox5);
			this.groupBoxAbsenceGrid.Controls.Add(this.groupBox4);
			this.groupBoxAbsenceGrid.Location = new System.Drawing.Point(8, 162);
			this.groupBoxAbsenceGrid.Name = "groupBoxAbsenceGrid";
			this.groupBoxAbsenceGrid.Size = new System.Drawing.Size(968, 420);
			this.groupBoxAbsenceGrid.TabIndex = 0;
			this.groupBoxAbsenceGrid.TabStop = false;
			this.groupBoxAbsenceGrid.Text = "Регистър на отсъствията на служителя";
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.dataGridViewYears);
			this.groupBox5.Controls.Add(this.labelVacationLeft);
			this.groupBox5.Controls.Add(this.label60);
			this.groupBox5.Controls.Add(this.buttonHistory);
			this.groupBox5.Location = new System.Drawing.Point(681, 18);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(281, 393);
			this.groupBox5.TabIndex = 15;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "История отпуски";
			// 
			// dataGridViewYears
			// 
			this.dataGridViewYears.AllowUserToAddRows = false;
			this.dataGridViewYears.AllowUserToDeleteRows = false;
			this.dataGridViewYears.AllowUserToResizeColumns = false;
			this.dataGridViewYears.AllowUserToResizeRows = false;
			this.dataGridViewYears.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			dataGridViewCellStyle79.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle79.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle79.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle79.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle79.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle79.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle79.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewYears.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle79;
			this.dataGridViewYears.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle80.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle80.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle80.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle80.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle80.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle80.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle80.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewYears.DefaultCellStyle = dataGridViewCellStyle80;
			this.dataGridViewYears.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridViewYears.Location = new System.Drawing.Point(3, 16);
			this.dataGridViewYears.MultiSelect = false;
			this.dataGridViewYears.Name = "dataGridViewYears";
			this.dataGridViewYears.ReadOnly = true;
			dataGridViewCellStyle81.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle81.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle81.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle81.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle81.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle81.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle81.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewYears.RowHeadersDefaultCellStyle = dataGridViewCellStyle81;
			this.dataGridViewYears.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
			this.dataGridViewYears.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewYears.Size = new System.Drawing.Size(275, 324);
			this.dataGridViewYears.TabIndex = 17;
			// 
			// labelVacationLeft
			// 
			this.labelVacationLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.labelVacationLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelVacationLeft.Location = new System.Drawing.Point(100, 343);
			this.labelVacationLeft.Name = "labelVacationLeft";
			this.labelVacationLeft.Size = new System.Drawing.Size(175, 18);
			this.labelVacationLeft.TabIndex = 16;
			// 
			// label60
			// 
			this.label60.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label60.Location = new System.Drawing.Point(6, 344);
			this.label60.Name = "label60";
			this.label60.Size = new System.Drawing.Size(88, 14);
			this.label60.TabIndex = 15;
			this.label60.Text = "Общ остатък : ";
			// 
			// buttonHistory
			// 
			this.buttonHistory.Location = new System.Drawing.Point(6, 364);
			this.buttonHistory.Name = "buttonHistory";
			this.buttonHistory.Size = new System.Drawing.Size(269, 24);
			this.buttonHistory.TabIndex = 13;
			this.buttonHistory.Text = "Корекция на история отпуски";
			this.buttonHistory.Click += new System.EventHandler(this.buttonHistory_Click);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.dataGridViewAbsence);
			this.groupBox4.Location = new System.Drawing.Point(8, 18);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(667, 393);
			this.groupBox4.TabIndex = 14;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Всички отсъствия";
			// 
			// dataGridViewAbsence
			// 
			this.dataGridViewAbsence.AllowUserToAddRows = false;
			this.dataGridViewAbsence.AllowUserToDeleteRows = false;
			this.dataGridViewAbsence.AllowUserToResizeColumns = false;
			this.dataGridViewAbsence.AllowUserToResizeRows = false;
			this.dataGridViewAbsence.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			dataGridViewCellStyle82.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle82.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle82.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle82.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle82.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle82.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle82.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewAbsence.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle82;
			this.dataGridViewAbsence.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle83.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle83.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle83.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle83.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle83.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle83.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle83.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewAbsence.DefaultCellStyle = dataGridViewCellStyle83;
			this.dataGridViewAbsence.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewAbsence.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridViewAbsence.Location = new System.Drawing.Point(3, 16);
			this.dataGridViewAbsence.MultiSelect = false;
			this.dataGridViewAbsence.Name = "dataGridViewAbsence";
			this.dataGridViewAbsence.ReadOnly = true;
			dataGridViewCellStyle84.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle84.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle84.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle84.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle84.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle84.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle84.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewAbsence.RowHeadersDefaultCellStyle = dataGridViewCellStyle84;
			this.dataGridViewAbsence.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewAbsence.Size = new System.Drawing.Size(661, 374);
			this.dataGridViewAbsence.TabIndex = 0;
			this.dataGridViewAbsence.SelectionChanged += new System.EventHandler(this.dataGridViewAbsence_SelectionChanged);
			this.dataGridViewAbsence.Click += new System.EventHandler(this.dataGridViewAbsence_Click);
			// 
			// groupBoxAbsece
			// 
			this.groupBoxAbsece.Controls.Add(this.label107);
			this.groupBoxAbsece.Controls.Add(this.textBoxAbsenceSicknessNumber);
			this.groupBoxAbsece.Controls.Add(this.textBoxAbsenceMKB);
			this.groupBoxAbsece.Controls.Add(this.label106);
			this.groupBoxAbsece.Controls.Add(this.textBoxAbsenceNAPDocs);
			this.groupBoxAbsece.Controls.Add(this.label105);
			this.groupBoxAbsece.Controls.Add(this.textBoxAbsenceReasons);
			this.groupBoxAbsece.Controls.Add(this.label104);
			this.groupBoxAbsece.Controls.Add(this.label103);
			this.groupBoxAbsece.Controls.Add(this.textBoxAbsenceNotes);
			this.groupBoxAbsece.Controls.Add(this.label102);
			this.groupBoxAbsece.Controls.Add(this.comboBoxAbsenceSicknessDuration);
			this.groupBoxAbsece.Controls.Add(this.dateTimePickerAbsenceSicknessIssuedAtDate);
			this.groupBoxAbsece.Controls.Add(this.label101);
			this.groupBoxAbsece.Controls.Add(this.label100);
			this.groupBoxAbsece.Controls.Add(this.textBoxAbsenceAdditionalDocs);
			this.groupBoxAbsece.Controls.Add(this.label93);
			this.groupBoxAbsece.Controls.Add(this.numBoxAbsenceWorkDays);
			this.groupBoxAbsece.Controls.Add(this.label92);
			this.groupBoxAbsece.Controls.Add(this.textBoxAbsenceDec39);
			this.groupBoxAbsece.Controls.Add(this.label29);
			this.groupBoxAbsece.Controls.Add(this.comboBoxAbsenceForYear);
			this.groupBoxAbsece.Controls.Add(this.dateTimePickerAbsenceOrderFormData);
			this.groupBoxAbsece.Controls.Add(this.label28);
			this.groupBoxAbsece.Controls.Add(this.label27);
			this.groupBoxAbsece.Controls.Add(this.textBoxAbsenceNumberOrder);
			this.groupBoxAbsece.Controls.Add(this.label26);
			this.groupBoxAbsece.Controls.Add(this.textBoxAbsenceAttachment7);
			this.groupBoxAbsece.Controls.Add(this.label25);
			this.groupBoxAbsece.Controls.Add(this.comboBoxAbsenceTypeAbsence);
			this.groupBoxAbsece.Controls.Add(this.label24);
			this.groupBoxAbsece.Controls.Add(this.numBoxAbsenceCalendarDays);
			this.groupBoxAbsece.Controls.Add(this.label23);
			this.groupBoxAbsece.Controls.Add(this.label22);
			this.groupBoxAbsece.Controls.Add(this.dateTimePickerAbsenceToData);
			this.groupBoxAbsece.Controls.Add(this.dateTimePickerAbsenceFromData);
			this.groupBoxAbsece.Location = new System.Drawing.Point(8, 8);
			this.groupBoxAbsece.Name = "groupBoxAbsece";
			this.groupBoxAbsece.Size = new System.Drawing.Size(968, 148);
			this.groupBoxAbsece.TabIndex = 0;
			this.groupBoxAbsece.TabStop = false;
			this.groupBoxAbsece.Text = "Данни за отсъствие";
			// 
			// label107
			// 
			this.label107.Location = new System.Drawing.Point(8, 57);
			this.label107.Name = "label107";
			this.label107.Size = new System.Drawing.Size(170, 16);
			this.label107.TabIndex = 37;
			this.label107.Text = "Болничен лист :";
			// 
			// textBoxAbsenceSicknessNumber
			// 
			this.textBoxAbsenceSicknessNumber.Location = new System.Drawing.Point(8, 76);
			this.textBoxAbsenceSicknessNumber.Name = "textBoxAbsenceSicknessNumber";
			this.textBoxAbsenceSicknessNumber.Size = new System.Drawing.Size(170, 20);
			this.textBoxAbsenceSicknessNumber.TabIndex = 6;
			// 
			// textBoxAbsenceMKB
			// 
			this.textBoxAbsenceMKB.Location = new System.Drawing.Point(537, 117);
			this.textBoxAbsenceMKB.Name = "textBoxAbsenceMKB";
			this.textBoxAbsenceMKB.Size = new System.Drawing.Size(72, 20);
			this.textBoxAbsenceMKB.TabIndex = 15;
			// 
			// label106
			// 
			this.label106.Location = new System.Drawing.Point(791, 100);
			this.label106.Name = "label106";
			this.label106.Size = new System.Drawing.Size(170, 16);
			this.label106.TabIndex = 34;
			this.label106.Text = "Документ в НАП :";
			// 
			// textBoxAbsenceNAPDocs
			// 
			this.textBoxAbsenceNAPDocs.Location = new System.Drawing.Point(791, 117);
			this.textBoxAbsenceNAPDocs.Name = "textBoxAbsenceNAPDocs";
			this.textBoxAbsenceNAPDocs.Size = new System.Drawing.Size(170, 20);
			this.textBoxAbsenceNAPDocs.TabIndex = 17;
			// 
			// label105
			// 
			this.label105.Location = new System.Drawing.Point(615, 100);
			this.label105.Name = "label105";
			this.label105.Size = new System.Drawing.Size(170, 16);
			this.label105.TabIndex = 32;
			this.label105.Text = "Причини :";
			// 
			// textBoxAbsenceReasons
			// 
			this.textBoxAbsenceReasons.Location = new System.Drawing.Point(615, 117);
			this.textBoxAbsenceReasons.Name = "textBoxAbsenceReasons";
			this.textBoxAbsenceReasons.Size = new System.Drawing.Size(170, 20);
			this.textBoxAbsenceReasons.TabIndex = 16;
			// 
			// label104
			// 
			this.label104.Location = new System.Drawing.Point(537, 100);
			this.label104.Name = "label104";
			this.label104.Size = new System.Drawing.Size(72, 16);
			this.label104.TabIndex = 30;
			this.label104.Text = "МКБ :";
			this.toolTip1.SetToolTip(this.label104, "Брой работни дни за отсъствието");
			// 
			// label103
			// 
			this.label103.Location = new System.Drawing.Point(540, 57);
			this.label103.Name = "label103";
			this.label103.Size = new System.Drawing.Size(138, 16);
			this.label103.TabIndex = 28;
			this.label103.Text = "Забележки :";
			// 
			// textBoxAbsenceNotes
			// 
			this.textBoxAbsenceNotes.Location = new System.Drawing.Point(540, 76);
			this.textBoxAbsenceNotes.Name = "textBoxAbsenceNotes";
			this.textBoxAbsenceNotes.Size = new System.Drawing.Size(138, 20);
			this.textBoxAbsenceNotes.TabIndex = 9;
			// 
			// label102
			// 
			this.label102.Location = new System.Drawing.Point(361, 57);
			this.label102.Name = "label102";
			this.label102.Size = new System.Drawing.Size(167, 16);
			this.label102.TabIndex = 26;
			this.label102.Text = "Първичен / продължение :";
			this.toolTip1.SetToolTip(this.label102, "Година за която ще се пуска полагаем отпуск");
			// 
			// comboBoxAbsenceSicknessDuration
			// 
			this.comboBoxAbsenceSicknessDuration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxAbsenceSicknessDuration.Items.AddRange(new object[] {
            "Първичен",
            "Продължение"});
			this.comboBoxAbsenceSicknessDuration.Location = new System.Drawing.Point(361, 75);
			this.comboBoxAbsenceSicknessDuration.Name = "comboBoxAbsenceSicknessDuration";
			this.comboBoxAbsenceSicknessDuration.Size = new System.Drawing.Size(167, 21);
			this.comboBoxAbsenceSicknessDuration.TabIndex = 8;
			this.toolTip1.SetToolTip(this.comboBoxAbsenceSicknessDuration, "Година за която ще се пуска полагаем отпуск");
			// 
			// dateTimePickerAbsenceSicknessIssuedAtDate
			// 
			this.dateTimePickerAbsenceSicknessIssuedAtDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerAbsenceSicknessIssuedAtDate.Location = new System.Drawing.Point(185, 76);
			this.dateTimePickerAbsenceSicknessIssuedAtDate.Name = "dateTimePickerAbsenceSicknessIssuedAtDate";
			this.dateTimePickerAbsenceSicknessIssuedAtDate.Size = new System.Drawing.Size(170, 20);
			this.dateTimePickerAbsenceSicknessIssuedAtDate.TabIndex = 7;
			this.toolTip1.SetToolTip(this.dateTimePickerAbsenceSicknessIssuedAtDate, "Дата на която заповедта влиза в сила");
			this.dateTimePickerAbsenceSicknessIssuedAtDate.Value = new System.DateTime(2005, 1, 12, 9, 43, 38, 312);
			// 
			// label101
			// 
			this.label101.Location = new System.Drawing.Point(185, 57);
			this.label101.Name = "label101";
			this.label101.Size = new System.Drawing.Size(170, 16);
			this.label101.TabIndex = 24;
			this.label101.Text = "Издаден на :";
			this.toolTip1.SetToolTip(this.label101, "Дата на която заповедта влиза в сила");
			// 
			// label100
			// 
			this.label100.Location = new System.Drawing.Point(361, 100);
			this.label100.Name = "label100";
			this.label100.Size = new System.Drawing.Size(170, 16);
			this.label100.TabIndex = 22;
			this.label100.Text = "Съпровождащи документи :";
			// 
			// textBoxAbsenceAdditionalDocs
			// 
			this.textBoxAbsenceAdditionalDocs.Location = new System.Drawing.Point(361, 117);
			this.textBoxAbsenceAdditionalDocs.Name = "textBoxAbsenceAdditionalDocs";
			this.textBoxAbsenceAdditionalDocs.Size = new System.Drawing.Size(170, 20);
			this.textBoxAbsenceAdditionalDocs.TabIndex = 14;
			// 
			// label93
			// 
			this.label93.Location = new System.Drawing.Point(360, 14);
			this.label93.Name = "label93";
			this.label93.Size = new System.Drawing.Size(97, 16);
			this.label93.TabIndex = 20;
			this.label93.Text = "Работни дни :";
			this.toolTip1.SetToolTip(this.label93, "Брой работни дни за отсъствието");
			// 
			// numBoxAbsenceWorkDays
			// 
			this.numBoxAbsenceWorkDays.Location = new System.Drawing.Point(360, 32);
			this.numBoxAbsenceWorkDays.Name = "numBoxAbsenceWorkDays";
			this.numBoxAbsenceWorkDays.Size = new System.Drawing.Size(97, 20);
			this.numBoxAbsenceWorkDays.TabIndex = 2;
			this.toolTip1.SetToolTip(this.numBoxAbsenceWorkDays, "Брой работни дни за отсъствието");
			// 
			// label92
			// 
			this.label92.Location = new System.Drawing.Point(185, 100);
			this.label92.Name = "label92";
			this.label92.Size = new System.Drawing.Size(170, 16);
			this.label92.TabIndex = 18;
			this.label92.Text = "Декларация по чл.39 :";
			// 
			// textBoxAbsenceDec39
			// 
			this.textBoxAbsenceDec39.Location = new System.Drawing.Point(185, 117);
			this.textBoxAbsenceDec39.Name = "textBoxAbsenceDec39";
			this.textBoxAbsenceDec39.Size = new System.Drawing.Size(170, 20);
			this.textBoxAbsenceDec39.TabIndex = 13;
			// 
			// label29
			// 
			this.label29.Location = new System.Drawing.Point(844, 14);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(118, 16);
			this.label29.TabIndex = 16;
			this.label29.Text = "За година :";
			this.toolTip1.SetToolTip(this.label29, "Година за която ще се пуска полагаем отпуск");
			// 
			// comboBoxAbsenceForYear
			// 
			this.comboBoxAbsenceForYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxAbsenceForYear.Location = new System.Drawing.Point(844, 31);
			this.comboBoxAbsenceForYear.Name = "comboBoxAbsenceForYear";
			this.comboBoxAbsenceForYear.Size = new System.Drawing.Size(118, 21);
			this.comboBoxAbsenceForYear.TabIndex = 5;
			this.toolTip1.SetToolTip(this.comboBoxAbsenceForYear, "Година за която ще се пуска полагаем отпуск");
			// 
			// dateTimePickerAbsenceOrderFormData
			// 
			this.dateTimePickerAbsenceOrderFormData.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerAbsenceOrderFormData.Location = new System.Drawing.Point(792, 76);
			this.dateTimePickerAbsenceOrderFormData.Name = "dateTimePickerAbsenceOrderFormData";
			this.dateTimePickerAbsenceOrderFormData.Size = new System.Drawing.Size(170, 20);
			this.dateTimePickerAbsenceOrderFormData.TabIndex = 11;
			this.toolTip1.SetToolTip(this.dateTimePickerAbsenceOrderFormData, "Дата на която заповедта влиза в сила");
			this.dateTimePickerAbsenceOrderFormData.Value = new System.DateTime(2005, 1, 12, 9, 43, 38, 312);
			// 
			// label28
			// 
			this.label28.Location = new System.Drawing.Point(792, 57);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(170, 16);
			this.label28.TabIndex = 13;
			this.label28.Text = "Заповед от дата :";
			this.toolTip1.SetToolTip(this.label28, "Дата на която заповедта влиза в сила");
			// 
			// label27
			// 
			this.label27.Location = new System.Drawing.Point(684, 57);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(102, 16);
			this.label27.TabIndex = 11;
			this.label27.Text = "Номер заповед :";
			// 
			// textBoxAbsenceNumberOrder
			// 
			this.textBoxAbsenceNumberOrder.Location = new System.Drawing.Point(684, 76);
			this.textBoxAbsenceNumberOrder.Name = "textBoxAbsenceNumberOrder";
			this.textBoxAbsenceNumberOrder.Size = new System.Drawing.Size(102, 20);
			this.textBoxAbsenceNumberOrder.TabIndex = 10;
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(8, 98);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(170, 16);
			this.label26.TabIndex = 9;
			this.label26.Text = "Приложение №7 :";
			// 
			// textBoxAbsenceAttachment7
			// 
			this.textBoxAbsenceAttachment7.Location = new System.Drawing.Point(8, 117);
			this.textBoxAbsenceAttachment7.Name = "textBoxAbsenceAttachment7";
			this.textBoxAbsenceAttachment7.Size = new System.Drawing.Size(170, 20);
			this.textBoxAbsenceAttachment7.TabIndex = 12;
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(569, 14);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(249, 16);
			this.label25.TabIndex = 7;
			this.label25.Text = "Вид отсъствие :";
			// 
			// comboBoxAbsenceTypeAbsence
			// 
			this.comboBoxAbsenceTypeAbsence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxAbsenceTypeAbsence.Items.AddRange(new object[] {
            "",
            "Полагаем годишен отпуск",
            "Болнични",
            "Неплатен отпуск",
            "Платен отпуск",
            "Отглеждане на дете",
            "Болнични след раждане",
            "Командировка",
            "Полагаем отпуск минали години",
            "Обучение",
            "Прекратяване на отпуск",
            "Полагаем отпуск ТЕЛК",
            "Полагаем отпуск обучение",
            "Полагаем отпуск друг"});
			this.comboBoxAbsenceTypeAbsence.Location = new System.Drawing.Point(569, 31);
			this.comboBoxAbsenceTypeAbsence.MaxDropDownItems = 12;
			this.comboBoxAbsenceTypeAbsence.Name = "comboBoxAbsenceTypeAbsence";
			this.comboBoxAbsenceTypeAbsence.Size = new System.Drawing.Size(269, 21);
			this.comboBoxAbsenceTypeAbsence.TabIndex = 4;
			this.comboBoxAbsenceTypeAbsence.SelectedIndexChanged += new System.EventHandler(this.comboBoxAbsenceTypeAbsence_SelectedIndexChanged);
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(466, 14);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(97, 16);
			this.label24.TabIndex = 5;
			this.label24.Text = "Календарни дни :";
			this.toolTip1.SetToolTip(this.label24, "Брой работни дни за отсъствието");
			// 
			// numBoxAbsenceCalendarDays
			// 
			this.numBoxAbsenceCalendarDays.Location = new System.Drawing.Point(466, 32);
			this.numBoxAbsenceCalendarDays.Name = "numBoxAbsenceCalendarDays";
			this.numBoxAbsenceCalendarDays.Size = new System.Drawing.Size(97, 20);
			this.numBoxAbsenceCalendarDays.TabIndex = 3;
			this.toolTip1.SetToolTip(this.numBoxAbsenceCalendarDays, "Брой работни дни за отсъствието");
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(185, 14);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(170, 16);
			this.label23.TabIndex = 3;
			this.label23.Text = "До дата :";
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(8, 14);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(170, 16);
			this.label22.TabIndex = 2;
			this.label22.Text = "От дата :";
			// 
			// dateTimePickerAbsenceToData
			// 
			this.dateTimePickerAbsenceToData.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerAbsenceToData.Location = new System.Drawing.Point(185, 32);
			this.dateTimePickerAbsenceToData.Name = "dateTimePickerAbsenceToData";
			this.dateTimePickerAbsenceToData.Size = new System.Drawing.Size(170, 20);
			this.dateTimePickerAbsenceToData.TabIndex = 1;
			this.dateTimePickerAbsenceToData.Value = new System.DateTime(2005, 1, 12, 9, 43, 38, 484);
			this.dateTimePickerAbsenceToData.ValueChanged += new System.EventHandler(this.dateTimePickerAbsenceFromData_ValueChanged);
			// 
			// dateTimePickerAbsenceFromData
			// 
			this.dateTimePickerAbsenceFromData.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerAbsenceFromData.Location = new System.Drawing.Point(8, 33);
			this.dateTimePickerAbsenceFromData.Name = "dateTimePickerAbsenceFromData";
			this.dateTimePickerAbsenceFromData.Size = new System.Drawing.Size(170, 20);
			this.dateTimePickerAbsenceFromData.TabIndex = 0;
			this.dateTimePickerAbsenceFromData.Value = new System.DateTime(2005, 1, 12, 9, 43, 38, 500);
			this.dateTimePickerAbsenceFromData.ValueChanged += new System.EventHandler(this.dateTimePickerAbsenceFromData_ValueChanged);
			// 
			// tabPagePenalty
			// 
			this.tabPagePenalty.Controls.Add(this.buttonPenaltiesExcel);
			this.tabPagePenalty.Controls.Add(this.buttonPenaltyAdd);
			this.tabPagePenalty.Controls.Add(this.radioButtonPenalties);
			this.tabPagePenalty.Controls.Add(this.radioButtonBonuses);
			this.tabPagePenalty.Controls.Add(this.buttonPenaltyPrint);
			this.tabPagePenalty.Controls.Add(this.buttonPenaltyCancel);
			this.tabPagePenalty.Controls.Add(this.buttonPenaltyDelete);
			this.tabPagePenalty.Controls.Add(this.buttonPenaltySave);
			this.tabPagePenalty.Controls.Add(this.buttonPebaltyEdit);
			this.tabPagePenalty.Controls.Add(this.groupBoxPenaltyGrid);
			this.tabPagePenalty.Controls.Add(this.groupBoxPenalty);
			this.tabPagePenalty.Location = new System.Drawing.Point(4, 22);
			this.tabPagePenalty.Name = "tabPagePenalty";
			this.tabPagePenalty.Size = new System.Drawing.Size(984, 615);
			this.tabPagePenalty.TabIndex = 4;
			this.tabPagePenalty.Text = "Наказания";
			this.tabPagePenalty.UseVisualStyleBackColor = true;
			// 
			// buttonPenaltiesExcel
			// 
			this.buttonPenaltiesExcel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonPenaltiesExcel.Image = ((System.Drawing.Image)(resources.GetObject("buttonPenaltiesExcel.Image")));
			this.buttonPenaltiesExcel.Location = new System.Drawing.Point(65, 589);
			this.buttonPenaltiesExcel.Name = "buttonPenaltiesExcel";
			this.buttonPenaltiesExcel.Size = new System.Drawing.Size(27, 23);
			this.buttonPenaltiesExcel.TabIndex = 0;
			this.buttonPenaltiesExcel.UseVisualStyleBackColor = true;
			this.buttonPenaltiesExcel.Click += new System.EventHandler(this.buttonPenaltiesExcel_Click);
			// 
			// buttonPenaltyAdd
			// 
			this.buttonPenaltyAdd.Image = ((System.Drawing.Image)(resources.GetObject("buttonPenaltyAdd.Image")));
			this.buttonPenaltyAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonPenaltyAdd.Location = new System.Drawing.Point(790, 589);
			this.buttonPenaltyAdd.Name = "buttonPenaltyAdd";
			this.buttonPenaltyAdd.Size = new System.Drawing.Size(130, 23);
			this.buttonPenaltyAdd.TabIndex = 6;
			this.buttonPenaltyAdd.Tag = "Въвеждане на ново наказание";
			this.buttonPenaltyAdd.Text = "Наказание";
			this.buttonPenaltyAdd.Click += new System.EventHandler(this.buttonPenaltyAdd_Click);
			// 
			// radioButtonPenalties
			// 
			this.radioButtonPenalties.Checked = true;
			this.radioButtonPenalties.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.radioButtonPenalties.Location = new System.Drawing.Point(336, 6);
			this.radioButtonPenalties.Name = "radioButtonPenalties";
			this.radioButtonPenalties.Size = new System.Drawing.Size(140, 24);
			this.radioButtonPenalties.TabIndex = 124;
			this.radioButtonPenalties.TabStop = true;
			this.radioButtonPenalties.Text = "Наказания";
			// 
			// radioButtonBonuses
			// 
			this.radioButtonBonuses.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.radioButtonBonuses.Location = new System.Drawing.Point(508, 6);
			this.radioButtonBonuses.Name = "radioButtonBonuses";
			this.radioButtonBonuses.Size = new System.Drawing.Size(140, 24);
			this.radioButtonBonuses.TabIndex = 125;
			this.radioButtonBonuses.Text = "Награди";
			this.radioButtonBonuses.CheckedChanged += new System.EventHandler(this.radioButtonBonuses_CheckedChanged);
			// 
			// buttonPenaltyPrint
			// 
			this.buttonPenaltyPrint.Image = ((System.Drawing.Image)(resources.GetObject("buttonPenaltyPrint.Image")));
			this.buttonPenaltyPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonPenaltyPrint.Location = new System.Drawing.Point(100, 589);
			this.buttonPenaltyPrint.Name = "buttonPenaltyPrint";
			this.buttonPenaltyPrint.Size = new System.Drawing.Size(130, 23);
			this.buttonPenaltyPrint.TabIndex = 1;
			this.buttonPenaltyPrint.Tag = "Печат на бланка за наказание";
			this.buttonPenaltyPrint.Text = "Печат";
			this.buttonPenaltyPrint.Click += new System.EventHandler(this.buttonPrintD_Click);
			// 
			// buttonPenaltyCancel
			// 
			this.buttonPenaltyCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonPenaltyCancel.Image")));
			this.buttonPenaltyCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonPenaltyCancel.Location = new System.Drawing.Point(238, 589);
			this.buttonPenaltyCancel.Name = "buttonPenaltyCancel";
			this.buttonPenaltyCancel.Size = new System.Drawing.Size(130, 23);
			this.buttonPenaltyCancel.TabIndex = 2;
			this.buttonPenaltyCancel.Tag = "Отказ от запис на данните";
			this.buttonPenaltyCancel.Text = "Отказ";
			this.buttonPenaltyCancel.Click += new System.EventHandler(this.buttonPenaltyCancel_Click);
			// 
			// buttonPenaltyDelete
			// 
			this.buttonPenaltyDelete.Image = ((System.Drawing.Image)(resources.GetObject("buttonPenaltyDelete.Image")));
			this.buttonPenaltyDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonPenaltyDelete.Location = new System.Drawing.Point(514, 589);
			this.buttonPenaltyDelete.Name = "buttonPenaltyDelete";
			this.buttonPenaltyDelete.Size = new System.Drawing.Size(130, 23);
			this.buttonPenaltyDelete.TabIndex = 4;
			this.buttonPenaltyDelete.Tag = "Премахване на наказание";
			this.buttonPenaltyDelete.Text = "Премахва";
			this.buttonPenaltyDelete.Click += new System.EventHandler(this.buttonPenaltyDelete_Click);
			// 
			// buttonPenaltySave
			// 
			this.buttonPenaltySave.Image = ((System.Drawing.Image)(resources.GetObject("buttonPenaltySave.Image")));
			this.buttonPenaltySave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonPenaltySave.Location = new System.Drawing.Point(376, 589);
			this.buttonPenaltySave.Name = "buttonPenaltySave";
			this.buttonPenaltySave.Size = new System.Drawing.Size(130, 23);
			this.buttonPenaltySave.TabIndex = 3;
			this.buttonPenaltySave.Tag = "Запис на данните";
			this.buttonPenaltySave.Text = "Запис";
			this.buttonPenaltySave.Click += new System.EventHandler(this.buttonPenaltySave_Click);
			// 
			// buttonPebaltyEdit
			// 
			this.buttonPebaltyEdit.Image = ((System.Drawing.Image)(resources.GetObject("buttonPebaltyEdit.Image")));
			this.buttonPebaltyEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonPebaltyEdit.Location = new System.Drawing.Point(652, 589);
			this.buttonPebaltyEdit.Name = "buttonPebaltyEdit";
			this.buttonPebaltyEdit.Size = new System.Drawing.Size(130, 23);
			this.buttonPebaltyEdit.TabIndex = 5;
			this.buttonPebaltyEdit.Tag = "Корекция на данните за избраното наказание";
			this.buttonPebaltyEdit.Text = "Корекция";
			this.buttonPebaltyEdit.Click += new System.EventHandler(this.buttonPebaltyEdit_Click);
			// 
			// groupBoxPenaltyGrid
			// 
			this.groupBoxPenaltyGrid.Controls.Add(this.dataGridViewPenalties);
			this.groupBoxPenaltyGrid.Location = new System.Drawing.Point(8, 139);
			this.groupBoxPenaltyGrid.Name = "groupBoxPenaltyGrid";
			this.groupBoxPenaltyGrid.Size = new System.Drawing.Size(968, 444);
			this.groupBoxPenaltyGrid.TabIndex = 1;
			this.groupBoxPenaltyGrid.TabStop = false;
			this.groupBoxPenaltyGrid.Text = "Данни за  наложени наказания за служителя";
			// 
			// dataGridViewPenalties
			// 
			this.dataGridViewPenalties.AllowUserToAddRows = false;
			this.dataGridViewPenalties.AllowUserToDeleteRows = false;
			this.dataGridViewPenalties.AllowUserToResizeRows = false;
			this.dataGridViewPenalties.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			dataGridViewCellStyle85.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle85.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle85.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle85.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle85.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle85.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle85.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewPenalties.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle85;
			this.dataGridViewPenalties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle86.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle86.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle86.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle86.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle86.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle86.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle86.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewPenalties.DefaultCellStyle = dataGridViewCellStyle86;
			this.dataGridViewPenalties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewPenalties.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridViewPenalties.Location = new System.Drawing.Point(3, 16);
			this.dataGridViewPenalties.MultiSelect = false;
			this.dataGridViewPenalties.Name = "dataGridViewPenalties";
			this.dataGridViewPenalties.ReadOnly = true;
			dataGridViewCellStyle87.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle87.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle87.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle87.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle87.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle87.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle87.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewPenalties.RowHeadersDefaultCellStyle = dataGridViewCellStyle87;
			this.dataGridViewPenalties.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewPenalties.Size = new System.Drawing.Size(962, 425);
			this.dataGridViewPenalties.TabIndex = 0;
			this.dataGridViewPenalties.Click += new System.EventHandler(this.dataGridPenalty_Click);
			// 
			// groupBoxPenalty
			// 
			this.groupBoxPenalty.Controls.Add(this.textBoxPenaltyNumberOrder);
			this.groupBoxPenalty.Controls.Add(this.buttonTypePenalty);
			this.groupBoxPenalty.Controls.Add(this.buttonPenaltyReason);
			this.groupBoxPenalty.Controls.Add(this.label31);
			this.groupBoxPenalty.Controls.Add(this.comboBoxTypePenalty);
			this.groupBoxPenalty.Controls.Add(this.label30);
			this.groupBoxPenalty.Controls.Add(this.dateTimePickerPenaltyToDate);
			this.groupBoxPenalty.Controls.Add(this.comboBoxPenaltyReason);
			this.groupBoxPenalty.Controls.Add(this.dateTimePickerPenaltyOrderDate);
			this.groupBoxPenalty.Controls.Add(this.label21);
			this.groupBoxPenalty.Controls.Add(this.label20);
			this.groupBoxPenalty.Controls.Add(this.labelPenaltyReason);
			this.groupBoxPenalty.Controls.Add(this.labelPenalty);
			this.groupBoxPenalty.Controls.Add(this.dateTimePickerPenaltyFromDate);
			this.groupBoxPenalty.Location = new System.Drawing.Point(8, 26);
			this.groupBoxPenalty.Name = "groupBoxPenalty";
			this.groupBoxPenalty.Size = new System.Drawing.Size(968, 107);
			this.groupBoxPenalty.TabIndex = 0;
			this.groupBoxPenalty.TabStop = false;
			this.groupBoxPenalty.Text = "Данни за наказание";
			// 
			// textBoxPenaltyNumberOrder
			// 
			this.textBoxPenaltyNumberOrder.Location = new System.Drawing.Point(606, 78);
			this.textBoxPenaltyNumberOrder.Name = "textBoxPenaltyNumberOrder";
			this.textBoxPenaltyNumberOrder.Size = new System.Drawing.Size(170, 20);
			this.textBoxPenaltyNumberOrder.TabIndex = 4;
			// 
			// buttonTypePenalty
			// 
			this.buttonTypePenalty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonTypePenalty.Image = ((System.Drawing.Image)(resources.GetObject("buttonTypePenalty.Image")));
			this.buttonTypePenalty.Location = new System.Drawing.Point(581, 78);
			this.buttonTypePenalty.Name = "buttonTypePenalty";
			this.buttonTypePenalty.Size = new System.Drawing.Size(21, 21);
			this.buttonTypePenalty.TabIndex = 120;
			this.buttonTypePenalty.TabStop = false;
			this.buttonTypePenalty.Tag = "Добавяне на данни към номенклатурата за научно звание";
			this.toolTip1.SetToolTip(this.buttonTypePenalty, "Номенклатура вид наказание");
			this.buttonTypePenalty.Click += new System.EventHandler(this.buttonTypePenalty_Click);
			// 
			// buttonPenaltyReason
			// 
			this.buttonPenaltyReason.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonPenaltyReason.Image = ((System.Drawing.Image)(resources.GetObject("buttonPenaltyReason.Image")));
			this.buttonPenaltyReason.Location = new System.Drawing.Point(581, 38);
			this.buttonPenaltyReason.Name = "buttonPenaltyReason";
			this.buttonPenaltyReason.Size = new System.Drawing.Size(21, 21);
			this.buttonPenaltyReason.TabIndex = 119;
			this.buttonPenaltyReason.TabStop = false;
			this.buttonPenaltyReason.Tag = "Добавяне на данни към номенклатурата за научно звание";
			this.toolTip1.SetToolTip(this.buttonPenaltyReason, "Номенклатура научно звание");
			this.buttonPenaltyReason.Click += new System.EventHandler(this.buttonPenaltyReason_Click);
			// 
			// label31
			// 
			this.label31.Location = new System.Drawing.Point(6, 62);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(568, 16);
			this.label31.TabIndex = 28;
			this.label31.Text = "Вид :";
			// 
			// comboBoxTypePenalty
			// 
			this.comboBoxTypePenalty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxTypePenalty.Location = new System.Drawing.Point(6, 78);
			this.comboBoxTypePenalty.Name = "comboBoxTypePenalty";
			this.comboBoxTypePenalty.Size = new System.Drawing.Size(568, 21);
			this.comboBoxTypePenalty.TabIndex = 3;
			// 
			// label30
			// 
			this.label30.Location = new System.Drawing.Point(790, 22);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(170, 16);
			this.label30.TabIndex = 26;
			this.label30.Text = "Валидно до:";
			// 
			// dateTimePickerPenaltyToDate
			// 
			this.dateTimePickerPenaltyToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerPenaltyToDate.Location = new System.Drawing.Point(790, 38);
			this.dateTimePickerPenaltyToDate.Name = "dateTimePickerPenaltyToDate";
			this.dateTimePickerPenaltyToDate.Size = new System.Drawing.Size(170, 20);
			this.dateTimePickerPenaltyToDate.TabIndex = 2;
			// 
			// comboBoxPenaltyReason
			// 
			this.comboBoxPenaltyReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxPenaltyReason.Location = new System.Drawing.Point(6, 38);
			this.comboBoxPenaltyReason.Name = "comboBoxPenaltyReason";
			this.comboBoxPenaltyReason.Size = new System.Drawing.Size(568, 21);
			this.comboBoxPenaltyReason.TabIndex = 0;
			// 
			// dateTimePickerPenaltyOrderDate
			// 
			this.dateTimePickerPenaltyOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerPenaltyOrderDate.Location = new System.Drawing.Point(790, 78);
			this.dateTimePickerPenaltyOrderDate.Name = "dateTimePickerPenaltyOrderDate";
			this.dateTimePickerPenaltyOrderDate.Size = new System.Drawing.Size(170, 20);
			this.dateTimePickerPenaltyOrderDate.TabIndex = 5;
			this.toolTip1.SetToolTip(this.dateTimePickerPenaltyOrderDate, "Дата на която влиза в сила заповедта");
			this.dateTimePickerPenaltyOrderDate.Value = new System.DateTime(2005, 1, 12, 9, 43, 38, 640);
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(790, 62);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(170, 16);
			this.label21.TabIndex = 21;
			this.label21.Text = "От дата :";
			this.toolTip1.SetToolTip(this.label21, "Дата на която влиза в сила заповедта");
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(606, 62);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(170, 16);
			this.label20.TabIndex = 20;
			this.label20.Text = "Номер заповед :";
			// 
			// labelPenaltyReason
			// 
			this.labelPenaltyReason.Location = new System.Drawing.Point(6, 22);
			this.labelPenaltyReason.Name = "labelPenaltyReason";
			this.labelPenaltyReason.Size = new System.Drawing.Size(568, 16);
			this.labelPenaltyReason.TabIndex = 19;
			this.labelPenaltyReason.Text = "Основание :";
			// 
			// labelPenalty
			// 
			this.labelPenalty.Location = new System.Drawing.Point(606, 22);
			this.labelPenalty.Name = "labelPenalty";
			this.labelPenalty.Size = new System.Drawing.Size(170, 16);
			this.labelPenalty.TabIndex = 17;
			this.labelPenalty.Text = "В сила от :";
			// 
			// dateTimePickerPenaltyFromDate
			// 
			this.dateTimePickerPenaltyFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerPenaltyFromDate.Location = new System.Drawing.Point(606, 38);
			this.dateTimePickerPenaltyFromDate.Name = "dateTimePickerPenaltyFromDate";
			this.dateTimePickerPenaltyFromDate.Size = new System.Drawing.Size(170, 20);
			this.dateTimePickerPenaltyFromDate.TabIndex = 1;
			this.dateTimePickerPenaltyFromDate.Value = new System.DateTime(2005, 1, 12, 9, 43, 38, 734);
			// 
			// tabPageNotes
			// 
			this.tabPageNotes.Controls.Add(this.buttonHistoryExcel);
			this.tabPageNotes.Controls.Add(this.buttonNotesPrint);
			this.tabPageNotes.Controls.Add(this.groupBoxNotes);
			this.tabPageNotes.Controls.Add(this.buttonNotesCancel);
			this.tabPageNotes.Controls.Add(this.groupBoxNotesGrid);
			this.tabPageNotes.Controls.Add(this.buttonNotesDelete);
			this.tabPageNotes.Controls.Add(this.buttonNotesSave);
			this.tabPageNotes.Controls.Add(this.groupBoxNotesFilter);
			this.tabPageNotes.Controls.Add(this.buttonNotesEdit);
			this.tabPageNotes.Controls.Add(this.buttonNotesAdd);
			this.tabPageNotes.Location = new System.Drawing.Point(4, 22);
			this.tabPageNotes.Name = "tabPageNotes";
			this.tabPageNotes.Size = new System.Drawing.Size(984, 615);
			this.tabPageNotes.TabIndex = 5;
			this.tabPageNotes.Text = "История";
			this.tabPageNotes.UseVisualStyleBackColor = true;
			// 
			// buttonHistoryExcel
			// 
			this.buttonHistoryExcel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonHistoryExcel.Location = new System.Drawing.Point(65, 589);
			this.buttonHistoryExcel.Name = "buttonHistoryExcel";
			this.buttonHistoryExcel.Size = new System.Drawing.Size(27, 23);
			this.buttonHistoryExcel.TabIndex = 0;
			this.buttonHistoryExcel.UseVisualStyleBackColor = true;
			this.buttonHistoryExcel.Click += new System.EventHandler(this.buttonHistoryExcel_Click);
			// 
			// buttonNotesPrint
			// 
			this.buttonNotesPrint.Image = ((System.Drawing.Image)(resources.GetObject("buttonNotesPrint.Image")));
			this.buttonNotesPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonNotesPrint.Location = new System.Drawing.Point(100, 589);
			this.buttonNotesPrint.Name = "buttonNotesPrint";
			this.buttonNotesPrint.Size = new System.Drawing.Size(130, 23);
			this.buttonNotesPrint.TabIndex = 1;
			this.buttonNotesPrint.Tag = "Печат на бележки";
			this.buttonNotesPrint.Text = "Печат";
			this.buttonNotesPrint.Click += new System.EventHandler(this.buttonPrintD_Click);
			// 
			// groupBoxNotes
			// 
			this.groupBoxNotes.Controls.Add(this.label85);
			this.groupBoxNotes.Controls.Add(this.textBoxNoteTypeDocument);
			this.groupBoxNotes.Controls.Add(this.label84);
			this.groupBoxNotes.Controls.Add(this.label82);
			this.groupBoxNotes.Controls.Add(this.label1);
			this.groupBoxNotes.Controls.Add(this.comboBoxNoteType);
			this.groupBoxNotes.Controls.Add(this.textBoxNoteText);
			this.groupBoxNotes.Controls.Add(this.dateTimePickerNotes);
			this.groupBoxNotes.Location = new System.Drawing.Point(8, 49);
			this.groupBoxNotes.Name = "groupBoxNotes";
			this.groupBoxNotes.Size = new System.Drawing.Size(973, 145);
			this.groupBoxNotes.TabIndex = 75;
			this.groupBoxNotes.TabStop = false;
			this.groupBoxNotes.Text = "Данни за бележка";
			// 
			// label85
			// 
			this.label85.AutoSize = true;
			this.label85.Location = new System.Drawing.Point(475, 17);
			this.label85.Name = "label85";
			this.label85.Size = new System.Drawing.Size(80, 13);
			this.label85.TabIndex = 7;
			this.label85.Text = "Вид документ:";
			// 
			// textBoxNoteTypeDocument
			// 
			this.textBoxNoteTypeDocument.Location = new System.Drawing.Point(475, 34);
			this.textBoxNoteTypeDocument.Multiline = true;
			this.textBoxNoteTypeDocument.Name = "textBoxNoteTypeDocument";
			this.textBoxNoteTypeDocument.Size = new System.Drawing.Size(493, 21);
			this.textBoxNoteTypeDocument.TabIndex = 2;
			// 
			// label84
			// 
			this.label84.AutoSize = true;
			this.label84.Location = new System.Drawing.Point(6, 58);
			this.label84.Name = "label84";
			this.label84.Size = new System.Drawing.Size(43, 13);
			this.label84.TabIndex = 5;
			this.label84.Text = "Текст :";
			// 
			// label82
			// 
			this.label82.AutoSize = true;
			this.label82.Location = new System.Drawing.Point(214, 17);
			this.label82.Name = "label82";
			this.label82.Size = new System.Drawing.Size(105, 13);
			this.label82.TabIndex = 4;
			this.label82.Text = "Тип на бележката :";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Дата :";
			// 
			// comboBoxNoteType
			// 
			this.comboBoxNoteType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxNoteType.FormattingEnabled = true;
			this.comboBoxNoteType.Items.AddRange(new object[] {
            "",
            "Назначение",
            "Споразумение",
            "Отсъствие",
            "Наказание",
            "Награда",
            "Прекратяване",
            "Атестация",
            "Обучение",
            "Други"});
			this.comboBoxNoteType.Location = new System.Drawing.Point(214, 34);
			this.comboBoxNoteType.Name = "comboBoxNoteType";
			this.comboBoxNoteType.Size = new System.Drawing.Size(255, 21);
			this.comboBoxNoteType.TabIndex = 1;
			// 
			// textBoxNoteText
			// 
			this.textBoxNoteText.Location = new System.Drawing.Point(6, 74);
			this.textBoxNoteText.Multiline = true;
			this.textBoxNoteText.Name = "textBoxNoteText";
			this.textBoxNoteText.Size = new System.Drawing.Size(961, 65);
			this.textBoxNoteText.TabIndex = 3;
			// 
			// dateTimePickerNotes
			// 
			this.dateTimePickerNotes.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerNotes.Location = new System.Drawing.Point(6, 35);
			this.dateTimePickerNotes.Name = "dateTimePickerNotes";
			this.dateTimePickerNotes.Size = new System.Drawing.Size(202, 20);
			this.dateTimePickerNotes.TabIndex = 0;
			// 
			// buttonNotesCancel
			// 
			this.buttonNotesCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonNotesCancel.Image")));
			this.buttonNotesCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonNotesCancel.Location = new System.Drawing.Point(238, 589);
			this.buttonNotesCancel.Name = "buttonNotesCancel";
			this.buttonNotesCancel.Size = new System.Drawing.Size(130, 23);
			this.buttonNotesCancel.TabIndex = 2;
			this.buttonNotesCancel.Tag = "Отказ от запис на данни";
			this.buttonNotesCancel.Text = "Отказ";
			this.buttonNotesCancel.Click += new System.EventHandler(this.buttonNotesCancel_Click);
			// 
			// groupBoxNotesGrid
			// 
			this.groupBoxNotesGrid.Controls.Add(this.dataGridViewNotes);
			this.groupBoxNotesGrid.Location = new System.Drawing.Point(10, 200);
			this.groupBoxNotesGrid.Name = "groupBoxNotesGrid";
			this.groupBoxNotesGrid.Size = new System.Drawing.Size(971, 383);
			this.groupBoxNotesGrid.TabIndex = 74;
			this.groupBoxNotesGrid.TabStop = false;
			this.groupBoxNotesGrid.Text = "Бележки";
			// 
			// dataGridViewNotes
			// 
			this.dataGridViewNotes.AllowUserToAddRows = false;
			this.dataGridViewNotes.AllowUserToDeleteRows = false;
			this.dataGridViewNotes.AllowUserToResizeRows = false;
			this.dataGridViewNotes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			dataGridViewCellStyle88.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle88.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle88.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle88.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle88.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle88.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle88.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewNotes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle88;
			this.dataGridViewNotes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle89.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle89.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle89.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle89.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle89.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle89.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle89.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewNotes.DefaultCellStyle = dataGridViewCellStyle89;
			this.dataGridViewNotes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewNotes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridViewNotes.Location = new System.Drawing.Point(3, 16);
			this.dataGridViewNotes.MultiSelect = false;
			this.dataGridViewNotes.Name = "dataGridViewNotes";
			this.dataGridViewNotes.ReadOnly = true;
			dataGridViewCellStyle90.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle90.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle90.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle90.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle90.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle90.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle90.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewNotes.RowHeadersDefaultCellStyle = dataGridViewCellStyle90;
			this.dataGridViewNotes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewNotes.Size = new System.Drawing.Size(965, 364);
			this.dataGridViewNotes.TabIndex = 0;
			this.dataGridViewNotes.Click += new System.EventHandler(this.dataGridNotes_Click);
			// 
			// buttonNotesDelete
			// 
			this.buttonNotesDelete.Image = ((System.Drawing.Image)(resources.GetObject("buttonNotesDelete.Image")));
			this.buttonNotesDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonNotesDelete.Location = new System.Drawing.Point(514, 589);
			this.buttonNotesDelete.Name = "buttonNotesDelete";
			this.buttonNotesDelete.Size = new System.Drawing.Size(130, 23);
			this.buttonNotesDelete.TabIndex = 4;
			this.buttonNotesDelete.Tag = "Премахва бележка";
			this.buttonNotesDelete.Text = "Премахва";
			this.buttonNotesDelete.Click += new System.EventHandler(this.buttonNotesDelete_Click);
			// 
			// buttonNotesSave
			// 
			this.buttonNotesSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonNotesSave.Image")));
			this.buttonNotesSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonNotesSave.Location = new System.Drawing.Point(376, 589);
			this.buttonNotesSave.Name = "buttonNotesSave";
			this.buttonNotesSave.Size = new System.Drawing.Size(130, 23);
			this.buttonNotesSave.TabIndex = 3;
			this.buttonNotesSave.Tag = "Запис на данни";
			this.buttonNotesSave.Text = "Запис";
			this.buttonNotesSave.Click += new System.EventHandler(this.buttonNotesSave_Click);
			// 
			// groupBoxNotesFilter
			// 
			this.groupBoxNotesFilter.Controls.Add(this.comboBoxNotesFilter);
			this.groupBoxNotesFilter.Location = new System.Drawing.Point(8, 3);
			this.groupBoxNotesFilter.Name = "groupBoxNotesFilter";
			this.groupBoxNotesFilter.Size = new System.Drawing.Size(973, 47);
			this.groupBoxNotesFilter.TabIndex = 73;
			this.groupBoxNotesFilter.TabStop = false;
			this.groupBoxNotesFilter.Text = "Филтър";
			// 
			// comboBoxNotesFilter
			// 
			this.comboBoxNotesFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxNotesFilter.FormattingEnabled = true;
			this.comboBoxNotesFilter.Items.AddRange(new object[] {
            "",
            "Назначение",
            "Споразумение",
            "Отсъствие",
            "Наказание",
            "Награда",
            "Прекратяване",
            "Атестация",
            "Обучение",
            "Други"});
			this.comboBoxNotesFilter.Location = new System.Drawing.Point(6, 19);
			this.comboBoxNotesFilter.Name = "comboBoxNotesFilter";
			this.comboBoxNotesFilter.Size = new System.Drawing.Size(961, 21);
			this.comboBoxNotesFilter.TabIndex = 72;
			this.comboBoxNotesFilter.SelectedIndexChanged += new System.EventHandler(this.comboBoxNotesFilter_SelectedIndexChanged);
			// 
			// buttonNotesEdit
			// 
			this.buttonNotesEdit.Image = ((System.Drawing.Image)(resources.GetObject("buttonNotesEdit.Image")));
			this.buttonNotesEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonNotesEdit.Location = new System.Drawing.Point(652, 589);
			this.buttonNotesEdit.Name = "buttonNotesEdit";
			this.buttonNotesEdit.Size = new System.Drawing.Size(130, 23);
			this.buttonNotesEdit.TabIndex = 5;
			this.buttonNotesEdit.Tag = "Корекция на бележка";
			this.buttonNotesEdit.Text = "Корекция";
			this.buttonNotesEdit.Click += new System.EventHandler(this.buttonNotesEdit_Click);
			// 
			// buttonNotesAdd
			// 
			this.buttonNotesAdd.Image = ((System.Drawing.Image)(resources.GetObject("buttonNotesAdd.Image")));
			this.buttonNotesAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonNotesAdd.Location = new System.Drawing.Point(790, 589);
			this.buttonNotesAdd.Name = "buttonNotesAdd";
			this.buttonNotesAdd.Size = new System.Drawing.Size(130, 23);
			this.buttonNotesAdd.TabIndex = 6;
			this.buttonNotesAdd.Tag = "Добавяне на нова бележка";
			this.buttonNotesAdd.Text = "Бележка";
			this.buttonNotesAdd.Click += new System.EventHandler(this.buttonNotesAdd_Click);
			// 
			// tabPageAtestacii
			// 
			this.tabPageAtestacii.Controls.Add(this.buttonAttestationsExcel);
			this.tabPageAtestacii.Controls.Add(this.groupBox9);
			this.tabPageAtestacii.Controls.Add(this.buttonatestationsCancel);
			this.tabPageAtestacii.Controls.Add(this.buttonAtestationsPrint);
			this.tabPageAtestacii.Controls.Add(this.buttonatestationsDelete);
			this.tabPageAtestacii.Controls.Add(this.buttonAtestationsSave);
			this.tabPageAtestacii.Controls.Add(this.buttonAtestationsEdit);
			this.tabPageAtestacii.Controls.Add(this.buttonAtestationsAdd);
			this.tabPageAtestacii.Controls.Add(this.groupBoxAttestationRegister);
			this.tabPageAtestacii.Controls.Add(this.groupBox8);
			this.tabPageAtestacii.Location = new System.Drawing.Point(4, 22);
			this.tabPageAtestacii.Name = "tabPageAtestacii";
			this.tabPageAtestacii.Size = new System.Drawing.Size(984, 615);
			this.tabPageAtestacii.TabIndex = 6;
			this.tabPageAtestacii.Text = "Aтестации";
			this.tabPageAtestacii.UseVisualStyleBackColor = true;
			// 
			// buttonAttestationsExcel
			// 
			this.buttonAttestationsExcel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonAttestationsExcel.Image = ((System.Drawing.Image)(resources.GetObject("buttonAttestationsExcel.Image")));
			this.buttonAttestationsExcel.Location = new System.Drawing.Point(65, 589);
			this.buttonAttestationsExcel.Name = "buttonAttestationsExcel";
			this.buttonAttestationsExcel.Size = new System.Drawing.Size(27, 23);
			this.buttonAttestationsExcel.TabIndex = 0;
			this.buttonAttestationsExcel.UseVisualStyleBackColor = true;
			this.buttonAttestationsExcel.Click += new System.EventHandler(this.buttonAttestationsExcel_Click);
			// 
			// groupBox9
			// 
			this.groupBox9.Controls.Add(this.buttonRangUpdateFile);
			this.groupBox9.Controls.Add(this.buttonPositionFile);
			this.groupBox9.Controls.Add(this.textBoxAttestationFile);
			this.groupBox9.Controls.Add(this.textBoxRetortFile);
			this.groupBox9.Controls.Add(this.textBoxPositionFile);
			this.groupBox9.Controls.Add(this.textBoxRangUpdateFile);
			this.groupBox9.Controls.Add(this.buttonAttestationFileView);
			this.groupBox9.Controls.Add(this.buttonRangUpdateFileView);
			this.groupBox9.Controls.Add(this.buttonRetortFileView);
			this.groupBox9.Controls.Add(this.buttonPositionFileView);
			this.groupBox9.Controls.Add(this.buttonAttestationFile);
			this.groupBox9.Controls.Add(this.label65);
			this.groupBox9.Controls.Add(this.label70);
			this.groupBox9.Controls.Add(this.buttonRetortFile);
			this.groupBox9.Controls.Add(this.label71);
			this.groupBox9.Controls.Add(this.label72);
			this.groupBox9.Enabled = false;
			this.groupBox9.Location = new System.Drawing.Point(8, 160);
			this.groupBox9.Name = "groupBox9";
			this.groupBox9.Size = new System.Drawing.Size(968, 104);
			this.groupBox9.TabIndex = 95;
			this.groupBox9.TabStop = false;
			this.groupBox9.Text = "Пълни атестационни формуляри";
			// 
			// buttonRangUpdateFile
			// 
			this.buttonRangUpdateFile.Enabled = false;
			this.buttonRangUpdateFile.Image = ((System.Drawing.Image)(resources.GetObject("buttonRangUpdateFile.Image")));
			this.buttonRangUpdateFile.Location = new System.Drawing.Point(440, 72);
			this.buttonRangUpdateFile.Name = "buttonRangUpdateFile";
			this.buttonRangUpdateFile.Size = new System.Drawing.Size(20, 20);
			this.buttonRangUpdateFile.TabIndex = 106;
			this.toolTip1.SetToolTip(this.buttonRangUpdateFile, "Избор на файл с формуляр за повишаване в ранг");
			// 
			// buttonPositionFile
			// 
			this.buttonPositionFile.Enabled = false;
			this.buttonPositionFile.Image = ((System.Drawing.Image)(resources.GetObject("buttonPositionFile.Image")));
			this.buttonPositionFile.Location = new System.Drawing.Point(921, 72);
			this.buttonPositionFile.Name = "buttonPositionFile";
			this.buttonPositionFile.Size = new System.Drawing.Size(20, 20);
			this.buttonPositionFile.TabIndex = 105;
			this.toolTip1.SetToolTip(this.buttonPositionFile, "Избор на файл сформуляр за повишаване в длъжност");
			// 
			// textBoxAttestationFile
			// 
			this.textBoxAttestationFile.Enabled = false;
			this.textBoxAttestationFile.Location = new System.Drawing.Point(9, 31);
			this.textBoxAttestationFile.Name = "textBoxAttestationFile";
			this.textBoxAttestationFile.Size = new System.Drawing.Size(420, 20);
			this.textBoxAttestationFile.TabIndex = 0;
			// 
			// textBoxRetortFile
			// 
			this.textBoxRetortFile.Enabled = false;
			this.textBoxRetortFile.Location = new System.Drawing.Point(492, 31);
			this.textBoxRetortFile.Name = "textBoxRetortFile";
			this.textBoxRetortFile.Size = new System.Drawing.Size(420, 20);
			this.textBoxRetortFile.TabIndex = 1;
			// 
			// textBoxPositionFile
			// 
			this.textBoxPositionFile.Enabled = false;
			this.textBoxPositionFile.Location = new System.Drawing.Point(492, 72);
			this.textBoxPositionFile.Name = "textBoxPositionFile";
			this.textBoxPositionFile.Size = new System.Drawing.Size(420, 20);
			this.textBoxPositionFile.TabIndex = 3;
			// 
			// textBoxRangUpdateFile
			// 
			this.textBoxRangUpdateFile.Enabled = false;
			this.textBoxRangUpdateFile.Location = new System.Drawing.Point(9, 72);
			this.textBoxRangUpdateFile.Name = "textBoxRangUpdateFile";
			this.textBoxRangUpdateFile.Size = new System.Drawing.Size(420, 20);
			this.textBoxRangUpdateFile.TabIndex = 2;
			// 
			// buttonAttestationFileView
			// 
			this.buttonAttestationFileView.Enabled = false;
			this.buttonAttestationFileView.Image = ((System.Drawing.Image)(resources.GetObject("buttonAttestationFileView.Image")));
			this.buttonAttestationFileView.Location = new System.Drawing.Point(466, 31);
			this.buttonAttestationFileView.Name = "buttonAttestationFileView";
			this.buttonAttestationFileView.Size = new System.Drawing.Size(20, 20);
			this.buttonAttestationFileView.TabIndex = 104;
			// 
			// buttonRangUpdateFileView
			// 
			this.buttonRangUpdateFileView.Enabled = false;
			this.buttonRangUpdateFileView.Image = ((System.Drawing.Image)(resources.GetObject("buttonRangUpdateFileView.Image")));
			this.buttonRangUpdateFileView.Location = new System.Drawing.Point(464, 72);
			this.buttonRangUpdateFileView.Name = "buttonRangUpdateFileView";
			this.buttonRangUpdateFileView.Size = new System.Drawing.Size(20, 20);
			this.buttonRangUpdateFileView.TabIndex = 109;
			// 
			// buttonRetortFileView
			// 
			this.buttonRetortFileView.Enabled = false;
			this.buttonRetortFileView.Image = ((System.Drawing.Image)(resources.GetObject("buttonRetortFileView.Image")));
			this.buttonRetortFileView.Location = new System.Drawing.Point(942, 31);
			this.buttonRetortFileView.Name = "buttonRetortFileView";
			this.buttonRetortFileView.Size = new System.Drawing.Size(20, 20);
			this.buttonRetortFileView.TabIndex = 108;
			// 
			// buttonPositionFileView
			// 
			this.buttonPositionFileView.Enabled = false;
			this.buttonPositionFileView.Image = ((System.Drawing.Image)(resources.GetObject("buttonPositionFileView.Image")));
			this.buttonPositionFileView.Location = new System.Drawing.Point(945, 72);
			this.buttonPositionFileView.Name = "buttonPositionFileView";
			this.buttonPositionFileView.Size = new System.Drawing.Size(20, 20);
			this.buttonPositionFileView.TabIndex = 107;
			// 
			// buttonAttestationFile
			// 
			this.buttonAttestationFile.Enabled = false;
			this.buttonAttestationFile.Image = ((System.Drawing.Image)(resources.GetObject("buttonAttestationFile.Image")));
			this.buttonAttestationFile.Location = new System.Drawing.Point(439, 31);
			this.buttonAttestationFile.Name = "buttonAttestationFile";
			this.buttonAttestationFile.Size = new System.Drawing.Size(20, 20);
			this.buttonAttestationFile.TabIndex = 102;
			this.toolTip1.SetToolTip(this.buttonAttestationFile, "Избор на файл с атестационен формуляр");
			// 
			// label65
			// 
			this.label65.Enabled = false;
			this.label65.Location = new System.Drawing.Point(9, 16);
			this.label65.Name = "label65";
			this.label65.Size = new System.Drawing.Size(420, 16);
			this.label65.TabIndex = 94;
			this.label65.Text = "Атестационна оценка :";
			// 
			// label70
			// 
			this.label70.Enabled = false;
			this.label70.Location = new System.Drawing.Point(9, 56);
			this.label70.Name = "label70";
			this.label70.Size = new System.Drawing.Size(420, 16);
			this.label70.TabIndex = 95;
			this.label70.Text = "Повишаване в ранг :";
			// 
			// buttonRetortFile
			// 
			this.buttonRetortFile.Enabled = false;
			this.buttonRetortFile.Image = ((System.Drawing.Image)(resources.GetObject("buttonRetortFile.Image")));
			this.buttonRetortFile.Location = new System.Drawing.Point(918, 31);
			this.buttonRetortFile.Name = "buttonRetortFile";
			this.buttonRetortFile.Size = new System.Drawing.Size(20, 20);
			this.buttonRetortFile.TabIndex = 103;
			this.toolTip1.SetToolTip(this.buttonRetortFile, "Избор на файл с формуляр за възражение");
			// 
			// label71
			// 
			this.label71.Enabled = false;
			this.label71.Location = new System.Drawing.Point(492, 16);
			this.label71.Name = "label71";
			this.label71.Size = new System.Drawing.Size(420, 16);
			this.label71.TabIndex = 97;
			this.label71.Text = "Възражение :";
			// 
			// label72
			// 
			this.label72.Enabled = false;
			this.label72.Location = new System.Drawing.Point(492, 56);
			this.label72.Name = "label72";
			this.label72.Size = new System.Drawing.Size(420, 16);
			this.label72.TabIndex = 96;
			this.label72.Text = "Повишаване в длъжност :";
			// 
			// buttonatestationsCancel
			// 
			this.buttonatestationsCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonatestationsCancel.Image")));
			this.buttonatestationsCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonatestationsCancel.Location = new System.Drawing.Point(238, 589);
			this.buttonatestationsCancel.Name = "buttonatestationsCancel";
			this.buttonatestationsCancel.Size = new System.Drawing.Size(130, 23);
			this.buttonatestationsCancel.TabIndex = 2;
			this.buttonatestationsCancel.Text = "Отказ";
			this.toolTip1.SetToolTip(this.buttonatestationsCancel, "Отказ от записването на данни");
			this.buttonatestationsCancel.Click += new System.EventHandler(this.buttonatestationsCancel_Click);
			// 
			// buttonAtestationsPrint
			// 
			this.buttonAtestationsPrint.Image = ((System.Drawing.Image)(resources.GetObject("buttonAtestationsPrint.Image")));
			this.buttonAtestationsPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonAtestationsPrint.Location = new System.Drawing.Point(100, 589);
			this.buttonAtestationsPrint.Name = "buttonAtestationsPrint";
			this.buttonAtestationsPrint.Size = new System.Drawing.Size(130, 23);
			this.buttonAtestationsPrint.TabIndex = 1;
			this.buttonAtestationsPrint.Text = "Печат";
			this.toolTip1.SetToolTip(this.buttonAtestationsPrint, "Печат на трудов договор или допълнително споразумение");
			this.buttonAtestationsPrint.Click += new System.EventHandler(this.buttonPrintD_Click);
			// 
			// buttonatestationsDelete
			// 
			this.buttonatestationsDelete.Image = ((System.Drawing.Image)(resources.GetObject("buttonatestationsDelete.Image")));
			this.buttonatestationsDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonatestationsDelete.Location = new System.Drawing.Point(514, 589);
			this.buttonatestationsDelete.Name = "buttonatestationsDelete";
			this.buttonatestationsDelete.Size = new System.Drawing.Size(130, 23);
			this.buttonatestationsDelete.TabIndex = 4;
			this.buttonatestationsDelete.Text = "Премахва";
			this.toolTip1.SetToolTip(this.buttonatestationsDelete, "Премахване на избраното назначение");
			this.buttonatestationsDelete.Click += new System.EventHandler(this.buttonatestationsDelete_Click);
			// 
			// buttonAtestationsSave
			// 
			this.buttonAtestationsSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonAtestationsSave.Image")));
			this.buttonAtestationsSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonAtestationsSave.Location = new System.Drawing.Point(376, 589);
			this.buttonAtestationsSave.Name = "buttonAtestationsSave";
			this.buttonAtestationsSave.Size = new System.Drawing.Size(130, 23);
			this.buttonAtestationsSave.TabIndex = 3;
			this.buttonAtestationsSave.Text = "Запис";
			this.toolTip1.SetToolTip(this.buttonAtestationsSave, "Запис на данните");
			this.buttonAtestationsSave.Click += new System.EventHandler(this.buttonAtestationsSave_Click);
			// 
			// buttonAtestationsEdit
			// 
			this.buttonAtestationsEdit.Image = ((System.Drawing.Image)(resources.GetObject("buttonAtestationsEdit.Image")));
			this.buttonAtestationsEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonAtestationsEdit.Location = new System.Drawing.Point(652, 589);
			this.buttonAtestationsEdit.Name = "buttonAtestationsEdit";
			this.buttonAtestationsEdit.Size = new System.Drawing.Size(130, 23);
			this.buttonAtestationsEdit.TabIndex = 5;
			this.buttonAtestationsEdit.Text = "Корекция";
			this.toolTip1.SetToolTip(this.buttonAtestationsEdit, "Корекция на данните за избраното назначение");
			this.buttonAtestationsEdit.Click += new System.EventHandler(this.buttonAtestationsEdit_Click);
			// 
			// buttonAtestationsAdd
			// 
			this.buttonAtestationsAdd.Image = ((System.Drawing.Image)(resources.GetObject("buttonAtestationsAdd.Image")));
			this.buttonAtestationsAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonAtestationsAdd.Location = new System.Drawing.Point(790, 589);
			this.buttonAtestationsAdd.Name = "buttonAtestationsAdd";
			this.buttonAtestationsAdd.Size = new System.Drawing.Size(130, 23);
			this.buttonAtestationsAdd.TabIndex = 6;
			this.buttonAtestationsAdd.Text = "   Атестация";
			this.toolTip1.SetToolTip(this.buttonAtestationsAdd, "Нова атестация");
			this.buttonAtestationsAdd.Click += new System.EventHandler(this.buttonAtestationsAdd_Click);
			// 
			// groupBoxAttestationRegister
			// 
			this.groupBoxAttestationRegister.Controls.Add(this.dataGridViewAttestations);
			this.groupBoxAttestationRegister.Enabled = false;
			this.groupBoxAttestationRegister.Location = new System.Drawing.Point(8, 264);
			this.groupBoxAttestationRegister.Name = "groupBoxAttestationRegister";
			this.groupBoxAttestationRegister.Size = new System.Drawing.Size(968, 319);
			this.groupBoxAttestationRegister.TabIndex = 55;
			this.groupBoxAttestationRegister.TabStop = false;
			this.groupBoxAttestationRegister.Text = "Регистър на атестациите";
			// 
			// dataGridViewAttestations
			// 
			this.dataGridViewAttestations.AllowUserToAddRows = false;
			this.dataGridViewAttestations.AllowUserToDeleteRows = false;
			this.dataGridViewAttestations.AllowUserToResizeRows = false;
			this.dataGridViewAttestations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			dataGridViewCellStyle91.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle91.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle91.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle91.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle91.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle91.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle91.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewAttestations.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle91;
			this.dataGridViewAttestations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle92.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle92.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle92.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle92.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle92.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle92.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle92.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewAttestations.DefaultCellStyle = dataGridViewCellStyle92;
			this.dataGridViewAttestations.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewAttestations.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridViewAttestations.Location = new System.Drawing.Point(3, 16);
			this.dataGridViewAttestations.Name = "dataGridViewAttestations";
			this.dataGridViewAttestations.ReadOnly = true;
			dataGridViewCellStyle93.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle93.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle93.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle93.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle93.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle93.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle93.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewAttestations.RowHeadersDefaultCellStyle = dataGridViewCellStyle93;
			this.dataGridViewAttestations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewAttestations.Size = new System.Drawing.Size(962, 300);
			this.dataGridViewAttestations.TabIndex = 0;
			// 
			// groupBox8
			// 
			this.groupBox8.Controls.Add(this.checkBoxFinalMeeting);
			this.groupBox8.Controls.Add(this.comboBoxNewRang);
			this.groupBox8.Controls.Add(this.label59);
			this.groupBox8.Controls.Add(this.textBoxAttestationsOther);
			this.groupBox8.Controls.Add(this.textBoxTrainingData);
			this.groupBox8.Controls.Add(this.checkBoxHasTraining);
			this.groupBox8.Controls.Add(this.dateTimePickerFinalMeeting);
			this.groupBox8.Controls.Add(this.checkBoxObjection);
			this.groupBox8.Controls.Add(this.dateTimePickerObjectionDate);
			this.groupBox8.Controls.Add(this.dateTimePickerRangDate);
			this.groupBox8.Controls.Add(this.dateTimePickerWorkPlan);
			this.groupBox8.Controls.Add(this.checkBoxhasWorkPlan);
			this.groupBox8.Controls.Add(this.dateTimePickerMiddleMeetingDate);
			this.groupBox8.Controls.Add(this.checkBoxMiddleMeetingDate);
			this.groupBox8.Controls.Add(this.comboBoxTotalMark);
			this.groupBox8.Controls.Add(this.label58);
			this.groupBox8.Controls.Add(this.label2);
			this.groupBox8.Controls.Add(this.numBoxYear);
			this.groupBox8.Controls.Add(this.textBoxControllingBoss);
			this.groupBox8.Controls.Add(this.label64);
			this.groupBox8.Controls.Add(this.label69);
			this.groupBox8.Controls.Add(this.textBoxBoss);
			this.groupBox8.Controls.Add(this.checkBoxPosition);
			this.groupBox8.Controls.Add(this.dateTimePickerPositionDate);
			this.groupBox8.Controls.Add(this.label61);
			this.groupBox8.Controls.Add(this.checkBoxRang);
			this.groupBox8.Location = new System.Drawing.Point(8, 8);
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.Size = new System.Drawing.Size(968, 152);
			this.groupBox8.TabIndex = 94;
			this.groupBox8.TabStop = false;
			this.groupBox8.Text = "Съкратена атестация";
			// 
			// checkBoxFinalMeeting
			// 
			this.checkBoxFinalMeeting.Location = new System.Drawing.Point(616, 16);
			this.checkBoxFinalMeeting.Name = "checkBoxFinalMeeting";
			this.checkBoxFinalMeeting.Size = new System.Drawing.Size(170, 16);
			this.checkBoxFinalMeeting.TabIndex = 96;
			this.checkBoxFinalMeeting.Text = "Закл. среща :";
			// 
			// comboBoxNewRang
			// 
			this.comboBoxNewRang.Location = new System.Drawing.Point(184, 71);
			this.comboBoxNewRang.Name = "comboBoxNewRang";
			this.comboBoxNewRang.Size = new System.Drawing.Size(170, 21);
			this.comboBoxNewRang.TabIndex = 7;
			// 
			// label59
			// 
			this.label59.Location = new System.Drawing.Point(643, 56);
			this.label59.Name = "label59";
			this.label59.Size = new System.Drawing.Size(317, 16);
			this.label59.TabIndex = 94;
			this.label59.Text = "Други :";
			// 
			// textBoxAttestationsOther
			// 
			this.textBoxAttestationsOther.Location = new System.Drawing.Point(642, 72);
			this.textBoxAttestationsOther.Name = "textBoxAttestationsOther";
			this.textBoxAttestationsOther.Size = new System.Drawing.Size(317, 20);
			this.textBoxAttestationsOther.TabIndex = 9;
			// 
			// textBoxTrainingData
			// 
			this.textBoxTrainingData.Location = new System.Drawing.Point(360, 72);
			this.textBoxTrainingData.Name = "textBoxTrainingData";
			this.textBoxTrainingData.Size = new System.Drawing.Size(276, 20);
			this.textBoxTrainingData.TabIndex = 8;
			// 
			// checkBoxHasTraining
			// 
			this.checkBoxHasTraining.Location = new System.Drawing.Point(359, 56);
			this.checkBoxHasTraining.Name = "checkBoxHasTraining";
			this.checkBoxHasTraining.Size = new System.Drawing.Size(276, 16);
			this.checkBoxHasTraining.TabIndex = 90;
			this.checkBoxHasTraining.Text = "Обучение";
			// 
			// dateTimePickerFinalMeeting
			// 
			this.dateTimePickerFinalMeeting.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerFinalMeeting.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dateTimePickerFinalMeeting.Location = new System.Drawing.Point(616, 32);
			this.dateTimePickerFinalMeeting.Name = "dateTimePickerFinalMeeting";
			this.dateTimePickerFinalMeeting.Size = new System.Drawing.Size(170, 20);
			this.dateTimePickerFinalMeeting.TabIndex = 4;
			// 
			// checkBoxObjection
			// 
			this.checkBoxObjection.Location = new System.Drawing.Point(792, 16);
			this.checkBoxObjection.Name = "checkBoxObjection";
			this.checkBoxObjection.Size = new System.Drawing.Size(170, 16);
			this.checkBoxObjection.TabIndex = 78;
			this.checkBoxObjection.Text = "Възражение";
			// 
			// dateTimePickerObjectionDate
			// 
			this.dateTimePickerObjectionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerObjectionDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dateTimePickerObjectionDate.Location = new System.Drawing.Point(792, 32);
			this.dateTimePickerObjectionDate.Name = "dateTimePickerObjectionDate";
			this.dateTimePickerObjectionDate.Size = new System.Drawing.Size(170, 20);
			this.dateTimePickerObjectionDate.TabIndex = 5;
			// 
			// dateTimePickerRangDate
			// 
			this.dateTimePickerRangDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerRangDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dateTimePickerRangDate.Location = new System.Drawing.Point(8, 72);
			this.dateTimePickerRangDate.Name = "dateTimePickerRangDate";
			this.dateTimePickerRangDate.Size = new System.Drawing.Size(170, 20);
			this.dateTimePickerRangDate.TabIndex = 6;
			// 
			// dateTimePickerWorkPlan
			// 
			this.dateTimePickerWorkPlan.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerWorkPlan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dateTimePickerWorkPlan.Location = new System.Drawing.Point(259, 32);
			this.dateTimePickerWorkPlan.Name = "dateTimePickerWorkPlan";
			this.dateTimePickerWorkPlan.Size = new System.Drawing.Size(170, 20);
			this.dateTimePickerWorkPlan.TabIndex = 2;
			// 
			// checkBoxhasWorkPlan
			// 
			this.checkBoxhasWorkPlan.Location = new System.Drawing.Point(259, 16);
			this.checkBoxhasWorkPlan.Name = "checkBoxhasWorkPlan";
			this.checkBoxhasWorkPlan.Size = new System.Drawing.Size(170, 16);
			this.checkBoxhasWorkPlan.TabIndex = 77;
			this.checkBoxhasWorkPlan.Text = "Работен план";
			// 
			// dateTimePickerMiddleMeetingDate
			// 
			this.dateTimePickerMiddleMeetingDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerMiddleMeetingDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dateTimePickerMiddleMeetingDate.Location = new System.Drawing.Point(438, 32);
			this.dateTimePickerMiddleMeetingDate.Name = "dateTimePickerMiddleMeetingDate";
			this.dateTimePickerMiddleMeetingDate.Size = new System.Drawing.Size(170, 20);
			this.dateTimePickerMiddleMeetingDate.TabIndex = 3;
			// 
			// checkBoxMiddleMeetingDate
			// 
			this.checkBoxMiddleMeetingDate.Location = new System.Drawing.Point(438, 16);
			this.checkBoxMiddleMeetingDate.Name = "checkBoxMiddleMeetingDate";
			this.checkBoxMiddleMeetingDate.Size = new System.Drawing.Size(170, 16);
			this.checkBoxMiddleMeetingDate.TabIndex = 74;
			this.checkBoxMiddleMeetingDate.Text = "Междинна среща";
			// 
			// comboBoxTotalMark
			// 
			this.comboBoxTotalMark.Items.AddRange(new object[] {
            "",
            "1",
            "2",
            "3",
            "4",
            "5"});
			this.comboBoxTotalMark.Location = new System.Drawing.Point(136, 31);
			this.comboBoxTotalMark.Name = "comboBoxTotalMark";
			this.comboBoxTotalMark.Size = new System.Drawing.Size(117, 21);
			this.comboBoxTotalMark.TabIndex = 1;
			// 
			// label58
			// 
			this.label58.Location = new System.Drawing.Point(136, 16);
			this.label58.Name = "label58";
			this.label58.Size = new System.Drawing.Size(117, 16);
			this.label58.TabIndex = 72;
			this.label58.Text = "Обща оценка :";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 16);
			this.label2.TabIndex = 57;
			this.label2.Text = "Година :";
			// 
			// numBoxYear
			// 
			this.numBoxYear.Location = new System.Drawing.Point(8, 32);
			this.numBoxYear.Name = "numBoxYear";
			this.numBoxYear.Size = new System.Drawing.Size(120, 20);
			this.numBoxYear.TabIndex = 0;
			// 
			// textBoxControllingBoss
			// 
			this.textBoxControllingBoss.Location = new System.Drawing.Point(578, 112);
			this.textBoxControllingBoss.Name = "textBoxControllingBoss";
			this.textBoxControllingBoss.Size = new System.Drawing.Size(385, 20);
			this.textBoxControllingBoss.TabIndex = 12;
			// 
			// label64
			// 
			this.label64.Location = new System.Drawing.Point(575, 96);
			this.label64.Name = "label64";
			this.label64.Size = new System.Drawing.Size(380, 16);
			this.label64.TabIndex = 65;
			this.label64.Text = "Контролиращ ръководител :";
			// 
			// label69
			// 
			this.label69.Location = new System.Drawing.Point(184, 96);
			this.label69.Name = "label69";
			this.label69.Size = new System.Drawing.Size(380, 16);
			this.label69.TabIndex = 69;
			this.label69.Text = "Оценяващ ръководител :";
			// 
			// textBoxBoss
			// 
			this.textBoxBoss.Location = new System.Drawing.Point(184, 112);
			this.textBoxBoss.Name = "textBoxBoss";
			this.textBoxBoss.Size = new System.Drawing.Size(385, 20);
			this.textBoxBoss.TabIndex = 11;
			// 
			// checkBoxPosition
			// 
			this.checkBoxPosition.Location = new System.Drawing.Point(8, 96);
			this.checkBoxPosition.Name = "checkBoxPosition";
			this.checkBoxPosition.Size = new System.Drawing.Size(170, 16);
			this.checkBoxPosition.TabIndex = 75;
			this.checkBoxPosition.Text = "Повишаване в длъжност";
			// 
			// dateTimePickerPositionDate
			// 
			this.dateTimePickerPositionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerPositionDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.dateTimePickerPositionDate.Location = new System.Drawing.Point(8, 112);
			this.dateTimePickerPositionDate.Name = "dateTimePickerPositionDate";
			this.dateTimePickerPositionDate.Size = new System.Drawing.Size(170, 20);
			this.dateTimePickerPositionDate.TabIndex = 10;
			// 
			// label61
			// 
			this.label61.Location = new System.Drawing.Point(184, 56);
			this.label61.Name = "label61";
			this.label61.Size = new System.Drawing.Size(170, 16);
			this.label61.TabIndex = 66;
			this.label61.Text = "Ранг :";
			// 
			// checkBoxRang
			// 
			this.checkBoxRang.Location = new System.Drawing.Point(8, 56);
			this.checkBoxRang.Name = "checkBoxRang";
			this.checkBoxRang.Size = new System.Drawing.Size(170, 16);
			this.checkBoxRang.TabIndex = 76;
			this.checkBoxRang.Text = "Повишаване в ранг :";
			// 
			// tabControlCardNew
			// 
			this.tabControlCardNew.Controls.Add(this.TabPersonalInfo);
			this.tabControlCardNew.Controls.Add(this.tabPageAssignment);
			this.tabControlCardNew.Controls.Add(this.tabPageAbsence);
			this.tabControlCardNew.Controls.Add(this.tabPagePenalty);
			this.tabControlCardNew.Controls.Add(this.tabPageFired);
			this.tabControlCardNew.Controls.Add(this.tabPageNotes);
			this.tabControlCardNew.Controls.Add(this.tabPageCharacteristics);
			this.tabControlCardNew.Controls.Add(this.tabPageAtestacii);
			this.tabControlCardNew.Controls.Add(this.tabPageEducation);
			this.tabControlCardNew.Controls.Add(this.tabPageMilitaryRang);
			this.tabControlCardNew.Controls.Add(this.tabPageCards);
			this.tabControlCardNew.ItemSize = new System.Drawing.Size(100, 18);
			this.tabControlCardNew.Location = new System.Drawing.Point(0, 0);
			this.tabControlCardNew.Name = "tabControlCardNew";
			this.tabControlCardNew.SelectedIndex = 0;
			this.tabControlCardNew.Size = new System.Drawing.Size(992, 641);
			this.tabControlCardNew.TabIndex = 0;
			this.tabControlCardNew.SelectedIndexChanging += new NewTabControl.NTabControl.SelectedTabPageChangeEventHandler(this.tabControl1_SelectedIndexChanging);
			// 
			// tabPageFired
			// 
			this.tabPageFired.Controls.Add(this.buttonFiredRestore);
			this.tabPageFired.Controls.Add(this.buttonFiredExcel);
			this.tabPageFired.Controls.Add(this.buttonFire);
			this.tabPageFired.Controls.Add(this.buttonFiredPrint);
			this.tabPageFired.Controls.Add(this.buttonFiredCancel);
			this.tabPageFired.Controls.Add(this.buttonFiredDelete);
			this.tabPageFired.Controls.Add(this.buttonFiredSave);
			this.tabPageFired.Controls.Add(this.buttonFiredEdit);
			this.tabPageFired.Controls.Add(this.buttonFiredNew);
			this.tabPageFired.Controls.Add(this.groupBoxFired);
			this.tabPageFired.Controls.Add(this.label32);
			this.tabPageFired.Controls.Add(this.groupBox7);
			this.tabPageFired.Location = new System.Drawing.Point(4, 22);
			this.tabPageFired.Name = "tabPageFired";
			this.tabPageFired.Size = new System.Drawing.Size(984, 615);
			this.tabPageFired.TabIndex = 7;
			this.tabPageFired.Text = "Прекратени договори";
			this.tabPageFired.UseVisualStyleBackColor = true;
			// 
			// buttonFiredRestore
			// 
			this.buttonFiredRestore.Image = ((System.Drawing.Image)(resources.GetObject("buttonFiredRestore.Image")));
			this.buttonFiredRestore.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonFiredRestore.Location = new System.Drawing.Point(567, 560);
			this.buttonFiredRestore.Name = "buttonFiredRestore";
			this.buttonFiredRestore.Size = new System.Drawing.Size(130, 23);
			this.buttonFiredRestore.TabIndex = 73;
			this.buttonFiredRestore.Tag = "Реално прекратяване на договора";
			this.buttonFiredRestore.Text = "Възстанови";
			this.buttonFiredRestore.Click += new System.EventHandler(this.buttonResotre_Click);
			// 
			// buttonFiredExcel
			// 
			this.buttonFiredExcel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonFiredExcel.Image = ((System.Drawing.Image)(resources.GetObject("buttonFiredExcel.Image")));
			this.buttonFiredExcel.Location = new System.Drawing.Point(65, 589);
			this.buttonFiredExcel.Name = "buttonFiredExcel";
			this.buttonFiredExcel.Size = new System.Drawing.Size(27, 23);
			this.buttonFiredExcel.TabIndex = 0;
			this.buttonFiredExcel.UseVisualStyleBackColor = true;
			this.buttonFiredExcel.Click += new System.EventHandler(this.buttonFiredExcel_Click);
			// 
			// buttonFire
			// 
			this.buttonFire.Image = ((System.Drawing.Image)(resources.GetObject("buttonFire.Image")));
			this.buttonFire.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonFire.Location = new System.Drawing.Point(298, 560);
			this.buttonFire.Name = "buttonFire";
			this.buttonFire.Size = new System.Drawing.Size(130, 23);
			this.buttonFire.TabIndex = 7;
			this.buttonFire.Tag = "Реално прекратяване на договора";
			this.buttonFire.Text = "Прекрати";
			this.buttonFire.Click += new System.EventHandler(this.buttonFire_Click);
			// 
			// buttonFiredPrint
			// 
			this.buttonFiredPrint.Image = ((System.Drawing.Image)(resources.GetObject("buttonFiredPrint.Image")));
			this.buttonFiredPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonFiredPrint.Location = new System.Drawing.Point(100, 589);
			this.buttonFiredPrint.Name = "buttonFiredPrint";
			this.buttonFiredPrint.Size = new System.Drawing.Size(130, 23);
			this.buttonFiredPrint.TabIndex = 1;
			this.buttonFiredPrint.Tag = "Печат на заповед за прекратяване на договор";
			this.buttonFiredPrint.Text = "Печат";
			this.buttonFiredPrint.Click += new System.EventHandler(this.buttonPrintD_Click);
			// 
			// buttonFiredCancel
			// 
			this.buttonFiredCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonFiredCancel.Image")));
			this.buttonFiredCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonFiredCancel.Location = new System.Drawing.Point(238, 589);
			this.buttonFiredCancel.Name = "buttonFiredCancel";
			this.buttonFiredCancel.Size = new System.Drawing.Size(130, 23);
			this.buttonFiredCancel.TabIndex = 2;
			this.buttonFiredCancel.Tag = "Отказ от запис на данни";
			this.buttonFiredCancel.Text = "Отказ";
			this.buttonFiredCancel.Click += new System.EventHandler(this.buttonFiredCancel_Click);
			// 
			// buttonFiredDelete
			// 
			this.buttonFiredDelete.Image = ((System.Drawing.Image)(resources.GetObject("buttonFiredDelete.Image")));
			this.buttonFiredDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonFiredDelete.Location = new System.Drawing.Point(514, 589);
			this.buttonFiredDelete.Name = "buttonFiredDelete";
			this.buttonFiredDelete.Size = new System.Drawing.Size(130, 23);
			this.buttonFiredDelete.TabIndex = 4;
			this.buttonFiredDelete.Tag = "Премахва данни за прекратяване на договор";
			this.buttonFiredDelete.Text = "Премахва";
			this.buttonFiredDelete.Click += new System.EventHandler(this.buttonFiredDelete_Click);
			// 
			// buttonFiredSave
			// 
			this.buttonFiredSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonFiredSave.Image")));
			this.buttonFiredSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonFiredSave.Location = new System.Drawing.Point(376, 589);
			this.buttonFiredSave.Name = "buttonFiredSave";
			this.buttonFiredSave.Size = new System.Drawing.Size(130, 23);
			this.buttonFiredSave.TabIndex = 3;
			this.buttonFiredSave.Tag = "Запис на данни";
			this.buttonFiredSave.Text = "Запис";
			this.buttonFiredSave.Click += new System.EventHandler(this.buttonFiredSave_Click);
			// 
			// buttonFiredEdit
			// 
			this.buttonFiredEdit.Image = ((System.Drawing.Image)(resources.GetObject("buttonFiredEdit.Image")));
			this.buttonFiredEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonFiredEdit.Location = new System.Drawing.Point(652, 589);
			this.buttonFiredEdit.Name = "buttonFiredEdit";
			this.buttonFiredEdit.Size = new System.Drawing.Size(130, 23);
			this.buttonFiredEdit.TabIndex = 5;
			this.buttonFiredEdit.Tag = "Корекция на данните за прекратяване на договор";
			this.buttonFiredEdit.Text = "Корекция";
			this.buttonFiredEdit.Click += new System.EventHandler(this.buttonFiredEdit_Click);
			// 
			// buttonFiredNew
			// 
			this.buttonFiredNew.Image = ((System.Drawing.Image)(resources.GetObject("buttonFiredNew.Image")));
			this.buttonFiredNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonFiredNew.Location = new System.Drawing.Point(790, 589);
			this.buttonFiredNew.Name = "buttonFiredNew";
			this.buttonFiredNew.Size = new System.Drawing.Size(130, 23);
			this.buttonFiredNew.TabIndex = 6;
			this.buttonFiredNew.Tag = "Въвеждане на пробни данни за прекратяване на договор";
			this.buttonFiredNew.Text = "Прекратяване";
			this.buttonFiredNew.Click += new System.EventHandler(this.buttonFiredNew_Click);
			// 
			// groupBoxFired
			// 
			this.groupBoxFired.Controls.Add(this.dataGridViewFired);
			this.groupBoxFired.Location = new System.Drawing.Point(8, 144);
			this.groupBoxFired.Name = "groupBoxFired";
			this.groupBoxFired.Size = new System.Drawing.Size(968, 410);
			this.groupBoxFired.TabIndex = 71;
			this.groupBoxFired.TabStop = false;
			this.groupBoxFired.Text = "Прекратени договори";
			// 
			// dataGridViewFired
			// 
			this.dataGridViewFired.AllowUserToAddRows = false;
			this.dataGridViewFired.AllowUserToDeleteRows = false;
			this.dataGridViewFired.AllowUserToResizeRows = false;
			this.dataGridViewFired.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			dataGridViewCellStyle94.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle94.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle94.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle94.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle94.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle94.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle94.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewFired.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle94;
			this.dataGridViewFired.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle95.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle95.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle95.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle95.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle95.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle95.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle95.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewFired.DefaultCellStyle = dataGridViewCellStyle95;
			this.dataGridViewFired.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewFired.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridViewFired.Location = new System.Drawing.Point(3, 16);
			this.dataGridViewFired.MultiSelect = false;
			this.dataGridViewFired.Name = "dataGridViewFired";
			this.dataGridViewFired.ReadOnly = true;
			dataGridViewCellStyle96.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle96.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle96.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle96.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle96.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle96.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle96.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewFired.RowHeadersDefaultCellStyle = dataGridViewCellStyle96;
			this.dataGridViewFired.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewFired.Size = new System.Drawing.Size(962, 391);
			this.dataGridViewFired.TabIndex = 0;
			this.dataGridViewFired.Click += new System.EventHandler(this.dataGridFired_Click);
			// 
			// label32
			// 
			this.label32.Location = new System.Drawing.Point(16, 24);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(208, 16);
			this.label32.TabIndex = 59;
			this.label32.Text = "Основаниe за прекратяване:";
			// 
			// groupBox7
			// 
			this.groupBox7.Controls.Add(this.dateTimePickerFireOdredDate);
			this.groupBox7.Controls.Add(this.label109);
			this.groupBox7.Controls.Add(this.textBoxFireOrder);
			this.groupBox7.Controls.Add(this.comboBoxFiredReason);
			this.groupBox7.Controls.Add(this.buttonReasonFired);
			this.groupBox7.Controls.Add(this.label62);
			this.groupBox7.Controls.Add(this.dateTimePickerFiredFromDate);
			this.groupBox7.Controls.Add(this.label34);
			this.groupBox7.Location = new System.Drawing.Point(8, 8);
			this.groupBox7.Name = "groupBox7";
			this.groupBox7.Size = new System.Drawing.Size(968, 128);
			this.groupBox7.TabIndex = 72;
			this.groupBox7.TabStop = false;
			this.groupBox7.Text = "Данни за прекратяване";
			// 
			// dateTimePickerFireOdredDate
			// 
			this.dateTimePickerFireOdredDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerFireOdredDate.Location = new System.Drawing.Point(792, 33);
			this.dateTimePickerFireOdredDate.Name = "dateTimePickerFireOdredDate";
			this.dateTimePickerFireOdredDate.Size = new System.Drawing.Size(170, 20);
			this.dateTimePickerFireOdredDate.TabIndex = 121;
			// 
			// label109
			// 
			this.label109.Location = new System.Drawing.Point(792, 16);
			this.label109.Name = "label109";
			this.label109.Size = new System.Drawing.Size(170, 16);
			this.label109.TabIndex = 122;
			this.label109.Text = "Заповед от дата:";
			// 
			// textBoxFireOrder
			// 
			this.textBoxFireOrder.Location = new System.Drawing.Point(616, 33);
			this.textBoxFireOrder.Name = "textBoxFireOrder";
			this.textBoxFireOrder.Size = new System.Drawing.Size(170, 20);
			this.textBoxFireOrder.TabIndex = 1;
			// 
			// comboBoxFiredReason
			// 
			this.comboBoxFiredReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxFiredReason.DropDownWidth = 370;
			this.comboBoxFiredReason.Location = new System.Drawing.Point(3, 32);
			this.comboBoxFiredReason.Name = "comboBoxFiredReason";
			this.comboBoxFiredReason.Size = new System.Drawing.Size(577, 21);
			this.comboBoxFiredReason.TabIndex = 0;
			// 
			// buttonReasonFired
			// 
			this.buttonReasonFired.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonReasonFired.Image = ((System.Drawing.Image)(resources.GetObject("buttonReasonFired.Image")));
			this.buttonReasonFired.Location = new System.Drawing.Point(586, 32);
			this.buttonReasonFired.Name = "buttonReasonFired";
			this.buttonReasonFired.Size = new System.Drawing.Size(21, 21);
			this.buttonReasonFired.TabIndex = 120;
			this.buttonReasonFired.TabStop = false;
			this.buttonReasonFired.Tag = "Добавяне на данни към номенклатурата за научно звание";
			this.toolTip1.SetToolTip(this.buttonReasonFired, "Номенклатура научно звание");
			this.buttonReasonFired.Click += new System.EventHandler(this.buttonReasonFired_Click);
			// 
			// label62
			// 
			this.label62.Location = new System.Drawing.Point(616, 16);
			this.label62.Name = "label62";
			this.label62.Size = new System.Drawing.Size(170, 16);
			this.label62.TabIndex = 62;
			this.label62.Text = "Заповед №:";
			// 
			// dateTimePickerFiredFromDate
			// 
			this.dateTimePickerFiredFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerFiredFromDate.Location = new System.Drawing.Point(3, 76);
			this.dateTimePickerFiredFromDate.Name = "dateTimePickerFiredFromDate";
			this.dateTimePickerFiredFromDate.Size = new System.Drawing.Size(170, 20);
			this.dateTimePickerFiredFromDate.TabIndex = 2;
			// 
			// label34
			// 
			this.label34.Location = new System.Drawing.Point(3, 59);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(170, 16);
			this.label34.TabIndex = 62;
			this.label34.Text = "Считано от:";
			// 
			// tabPageCharacteristics
			// 
			this.tabPageCharacteristics.Controls.Add(this.label33);
			this.tabPageCharacteristics.Controls.Add(this.textBoxNKPCode2);
			this.tabPageCharacteristics.Controls.Add(this.textBoxNKPClass);
			this.tabPageCharacteristics.Controls.Add(this.label35);
			this.tabPageCharacteristics.Controls.Add(this.label44);
			this.tabPageCharacteristics.Controls.Add(this.textBoxRequirements);
			this.tabPageCharacteristics.Controls.Add(this.label47);
			this.tabPageCharacteristics.Controls.Add(this.textBoxCompetence);
			this.tabPageCharacteristics.Controls.Add(this.label48);
			this.tabPageCharacteristics.Controls.Add(this.textBoxBasicResponsibilities);
			this.tabPageCharacteristics.Controls.Add(this.label54);
			this.tabPageCharacteristics.Controls.Add(this.label55);
			this.tabPageCharacteristics.Controls.Add(this.textBoxBasicDuties);
			this.tabPageCharacteristics.Controls.Add(this.textBoxConnections);
			this.tabPageCharacteristics.Location = new System.Drawing.Point(4, 22);
			this.tabPageCharacteristics.Name = "tabPageCharacteristics";
			this.tabPageCharacteristics.Size = new System.Drawing.Size(984, 615);
			this.tabPageCharacteristics.TabIndex = 8;
			this.tabPageCharacteristics.Text = "Длъжностна характеристика";
			this.tabPageCharacteristics.UseVisualStyleBackColor = true;
			// 
			// label33
			// 
			this.label33.Location = new System.Drawing.Point(798, 4);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(178, 16);
			this.label33.TabIndex = 29;
			this.label33.Text = "Код по НКПД";
			// 
			// textBoxNKPCode2
			// 
			this.textBoxNKPCode2.Location = new System.Drawing.Point(798, 20);
			this.textBoxNKPCode2.Name = "textBoxNKPCode2";
			this.textBoxNKPCode2.ReadOnly = true;
			this.textBoxNKPCode2.Size = new System.Drawing.Size(178, 20);
			this.textBoxNKPCode2.TabIndex = 28;
			// 
			// textBoxNKPClass
			// 
			this.textBoxNKPClass.Location = new System.Drawing.Point(6, 20);
			this.textBoxNKPClass.Name = "textBoxNKPClass";
			this.textBoxNKPClass.ReadOnly = true;
			this.textBoxNKPClass.Size = new System.Drawing.Size(786, 20);
			this.textBoxNKPClass.TabIndex = 27;
			// 
			// label35
			// 
			this.label35.Location = new System.Drawing.Point(6, 4);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(786, 16);
			this.label35.TabIndex = 26;
			this.label35.Text = "Клас по НКПД:";
			// 
			// label44
			// 
			this.label44.Location = new System.Drawing.Point(6, 504);
			this.label44.Name = "label44";
			this.label44.Size = new System.Drawing.Size(970, 16);
			this.label44.TabIndex = 25;
			this.label44.Text = "V. ИЗИСКВАНИЯ ЗА ЗАЕМАНЕ НА ДЛЪЖНОСТТА:";
			// 
			// textBoxRequirements
			// 
			this.textBoxRequirements.Location = new System.Drawing.Point(6, 520);
			this.textBoxRequirements.Multiline = true;
			this.textBoxRequirements.Name = "textBoxRequirements";
			this.textBoxRequirements.ReadOnly = true;
			this.textBoxRequirements.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxRequirements.Size = new System.Drawing.Size(970, 89);
			this.textBoxRequirements.TabIndex = 24;
			// 
			// label47
			// 
			this.label47.Location = new System.Drawing.Point(6, 392);
			this.label47.Name = "label47";
			this.label47.Size = new System.Drawing.Size(970, 16);
			this.label47.TabIndex = 23;
			this.label47.Text = "IV. НЕОБХОДИМА КОМПЕТЕНТНОСТ ЗА ИЗПЪЛНЕНИЕ НА ДЛЪЖНОСТТА:";
			// 
			// textBoxCompetence
			// 
			this.textBoxCompetence.Location = new System.Drawing.Point(6, 408);
			this.textBoxCompetence.Multiline = true;
			this.textBoxCompetence.Name = "textBoxCompetence";
			this.textBoxCompetence.ReadOnly = true;
			this.textBoxCompetence.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxCompetence.Size = new System.Drawing.Size(970, 89);
			this.textBoxCompetence.TabIndex = 22;
			// 
			// label48
			// 
			this.label48.Location = new System.Drawing.Point(6, 278);
			this.label48.Name = "label48";
			this.label48.Size = new System.Drawing.Size(970, 16);
			this.label48.TabIndex = 21;
			this.label48.Text = "III. ОРГАНИЗАЦИОННИ ВРЪЗКИ И ВЗАИМООТНОШЕНИЯ:";
			// 
			// textBoxBasicResponsibilities
			// 
			this.textBoxBasicResponsibilities.Location = new System.Drawing.Point(6, 177);
			this.textBoxBasicResponsibilities.Multiline = true;
			this.textBoxBasicResponsibilities.Name = "textBoxBasicResponsibilities";
			this.textBoxBasicResponsibilities.ReadOnly = true;
			this.textBoxBasicResponsibilities.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxBasicResponsibilities.Size = new System.Drawing.Size(970, 89);
			this.textBoxBasicResponsibilities.TabIndex = 19;
			// 
			// label54
			// 
			this.label54.Location = new System.Drawing.Point(6, 161);
			this.label54.Name = "label54";
			this.label54.Size = new System.Drawing.Size(970, 16);
			this.label54.TabIndex = 18;
			this.label54.Text = "II. ОСНОВНИ ОТГОВОРНОСТИ, ПРИСЪЩИ ЗА ДЛЪЖНОСТТА:";
			// 
			// label55
			// 
			this.label55.Location = new System.Drawing.Point(6, 46);
			this.label55.Name = "label55";
			this.label55.Size = new System.Drawing.Size(970, 16);
			this.label55.TabIndex = 17;
			this.label55.Text = "I. ОСНОВНИ ДЛЪЖНОСТНИ ЗАДЪЛЖЕНИЯ:";
			// 
			// textBoxBasicDuties
			// 
			this.textBoxBasicDuties.Location = new System.Drawing.Point(6, 62);
			this.textBoxBasicDuties.MaxLength = 64000;
			this.textBoxBasicDuties.Multiline = true;
			this.textBoxBasicDuties.Name = "textBoxBasicDuties";
			this.textBoxBasicDuties.ReadOnly = true;
			this.textBoxBasicDuties.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxBasicDuties.Size = new System.Drawing.Size(970, 89);
			this.textBoxBasicDuties.TabIndex = 16;
			// 
			// textBoxConnections
			// 
			this.textBoxConnections.Location = new System.Drawing.Point(6, 293);
			this.textBoxConnections.Multiline = true;
			this.textBoxConnections.Name = "textBoxConnections";
			this.textBoxConnections.ReadOnly = true;
			this.textBoxConnections.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxConnections.Size = new System.Drawing.Size(970, 89);
			this.textBoxConnections.TabIndex = 20;
			// 
			// tabPageEducation
			// 
			this.tabPageEducation.Controls.Add(this.buttonEducationsExcel);
			this.tabPageEducation.Controls.Add(this.buttonEducationPrint);
			this.tabPageEducation.Controls.Add(this.buttonEducationCancel);
			this.tabPageEducation.Controls.Add(this.buttonEducationDelete);
			this.tabPageEducation.Controls.Add(this.buttonEducationSave);
			this.tabPageEducation.Controls.Add(this.buttonEducationEdit);
			this.tabPageEducation.Controls.Add(this.buttonEducationAdd);
			this.tabPageEducation.Controls.Add(this.groupBoxEducationHistory);
			this.tabPageEducation.Controls.Add(this.groupBoxEducationData);
			this.tabPageEducation.Location = new System.Drawing.Point(4, 22);
			this.tabPageEducation.Name = "tabPageEducation";
			this.tabPageEducation.Size = new System.Drawing.Size(984, 615);
			this.tabPageEducation.TabIndex = 9;
			this.tabPageEducation.Text = "Обучения";
			this.tabPageEducation.UseVisualStyleBackColor = true;
			// 
			// buttonEducationsExcel
			// 
			this.buttonEducationsExcel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonEducationsExcel.Image = ((System.Drawing.Image)(resources.GetObject("buttonEducationsExcel.Image")));
			this.buttonEducationsExcel.Location = new System.Drawing.Point(65, 589);
			this.buttonEducationsExcel.Name = "buttonEducationsExcel";
			this.buttonEducationsExcel.Size = new System.Drawing.Size(27, 23);
			this.buttonEducationsExcel.TabIndex = 0;
			this.buttonEducationsExcel.UseVisualStyleBackColor = true;
			this.buttonEducationsExcel.Click += new System.EventHandler(this.buttonEducationsExcel_Click);
			// 
			// buttonEducationPrint
			// 
			this.buttonEducationPrint.Image = ((System.Drawing.Image)(resources.GetObject("buttonEducationPrint.Image")));
			this.buttonEducationPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonEducationPrint.Location = new System.Drawing.Point(100, 589);
			this.buttonEducationPrint.Name = "buttonEducationPrint";
			this.buttonEducationPrint.Size = new System.Drawing.Size(130, 23);
			this.buttonEducationPrint.TabIndex = 1;
			this.buttonEducationPrint.Text = "Печат";
			this.toolTip1.SetToolTip(this.buttonEducationPrint, "Печат на трудов договор или допълнително споразумение");
			// 
			// buttonEducationCancel
			// 
			this.buttonEducationCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonEducationCancel.Image")));
			this.buttonEducationCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonEducationCancel.Location = new System.Drawing.Point(238, 589);
			this.buttonEducationCancel.Name = "buttonEducationCancel";
			this.buttonEducationCancel.Size = new System.Drawing.Size(130, 23);
			this.buttonEducationCancel.TabIndex = 2;
			this.buttonEducationCancel.Text = "Отказ";
			this.toolTip1.SetToolTip(this.buttonEducationCancel, "Отказ от записването на данни");
			this.buttonEducationCancel.Click += new System.EventHandler(this.buttonEducationCancel_Click);
			// 
			// buttonEducationDelete
			// 
			this.buttonEducationDelete.Image = ((System.Drawing.Image)(resources.GetObject("buttonEducationDelete.Image")));
			this.buttonEducationDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonEducationDelete.Location = new System.Drawing.Point(514, 589);
			this.buttonEducationDelete.Name = "buttonEducationDelete";
			this.buttonEducationDelete.Size = new System.Drawing.Size(130, 23);
			this.buttonEducationDelete.TabIndex = 4;
			this.buttonEducationDelete.Text = "Премахва";
			this.toolTip1.SetToolTip(this.buttonEducationDelete, "Премахване на избраното назначение");
			this.buttonEducationDelete.Click += new System.EventHandler(this.buttonEducationDelete_Click);
			// 
			// buttonEducationSave
			// 
			this.buttonEducationSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonEducationSave.Image")));
			this.buttonEducationSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonEducationSave.Location = new System.Drawing.Point(376, 589);
			this.buttonEducationSave.Name = "buttonEducationSave";
			this.buttonEducationSave.Size = new System.Drawing.Size(130, 23);
			this.buttonEducationSave.TabIndex = 3;
			this.buttonEducationSave.Text = "Запис";
			this.toolTip1.SetToolTip(this.buttonEducationSave, "Запис на данните");
			this.buttonEducationSave.Click += new System.EventHandler(this.buttonEducationSave_Click);
			// 
			// buttonEducationEdit
			// 
			this.buttonEducationEdit.Image = ((System.Drawing.Image)(resources.GetObject("buttonEducationEdit.Image")));
			this.buttonEducationEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonEducationEdit.Location = new System.Drawing.Point(652, 589);
			this.buttonEducationEdit.Name = "buttonEducationEdit";
			this.buttonEducationEdit.Size = new System.Drawing.Size(130, 23);
			this.buttonEducationEdit.TabIndex = 5;
			this.buttonEducationEdit.Text = "Корекция";
			this.toolTip1.SetToolTip(this.buttonEducationEdit, "Корекция на данните за избраното назначение");
			this.buttonEducationEdit.Click += new System.EventHandler(this.buttonEducationEdit_Click);
			// 
			// buttonEducationAdd
			// 
			this.buttonEducationAdd.Image = ((System.Drawing.Image)(resources.GetObject("buttonEducationAdd.Image")));
			this.buttonEducationAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonEducationAdd.Location = new System.Drawing.Point(790, 589);
			this.buttonEducationAdd.Name = "buttonEducationAdd";
			this.buttonEducationAdd.Size = new System.Drawing.Size(130, 23);
			this.buttonEducationAdd.TabIndex = 6;
			this.buttonEducationAdd.Text = "Обучение";
			this.toolTip1.SetToolTip(this.buttonEducationAdd, "Въвеждане на обучение");
			this.buttonEducationAdd.Click += new System.EventHandler(this.buttonEducationAdd_Click);
			// 
			// groupBoxEducationHistory
			// 
			this.groupBoxEducationHistory.Controls.Add(this.dataGridViewEducations);
			this.groupBoxEducationHistory.Enabled = false;
			this.groupBoxEducationHistory.Location = new System.Drawing.Point(8, 216);
			this.groupBoxEducationHistory.Name = "groupBoxEducationHistory";
			this.groupBoxEducationHistory.Size = new System.Drawing.Size(968, 367);
			this.groupBoxEducationHistory.TabIndex = 56;
			this.groupBoxEducationHistory.TabStop = false;
			this.groupBoxEducationHistory.Text = "Регистър на обученията";
			// 
			// dataGridViewEducations
			// 
			this.dataGridViewEducations.AllowUserToAddRows = false;
			this.dataGridViewEducations.AllowUserToDeleteRows = false;
			this.dataGridViewEducations.AllowUserToResizeRows = false;
			this.dataGridViewEducations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			dataGridViewCellStyle97.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle97.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle97.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle97.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle97.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle97.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle97.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewEducations.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle97;
			this.dataGridViewEducations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle98.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle98.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle98.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle98.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle98.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle98.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle98.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewEducations.DefaultCellStyle = dataGridViewCellStyle98;
			this.dataGridViewEducations.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewEducations.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridViewEducations.Location = new System.Drawing.Point(3, 16);
			this.dataGridViewEducations.MultiSelect = false;
			this.dataGridViewEducations.Name = "dataGridViewEducations";
			this.dataGridViewEducations.ReadOnly = true;
			dataGridViewCellStyle99.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle99.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle99.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle99.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle99.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle99.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle99.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewEducations.RowHeadersDefaultCellStyle = dataGridViewCellStyle99;
			this.dataGridViewEducations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewEducations.Size = new System.Drawing.Size(962, 348);
			this.dataGridViewEducations.TabIndex = 0;
			this.dataGridViewEducations.Click += new System.EventHandler(this.dataGridViewEducations_Click);
			// 
			// groupBoxEducationData
			// 
			this.groupBoxEducationData.Controls.Add(this.textBoxEducationArea);
			this.groupBoxEducationData.Controls.Add(this.textBoxEducationTheme);
			this.groupBoxEducationData.Controls.Add(this.label76);
			this.groupBoxEducationData.Controls.Add(this.textBoxEducationOrganisation);
			this.groupBoxEducationData.Controls.Add(this.textBoxEducationPlace);
			this.groupBoxEducationData.Controls.Add(this.dateTimePickerEducationToDate);
			this.groupBoxEducationData.Controls.Add(this.dateTimePickerEducationFromDate);
			this.groupBoxEducationData.Controls.Add(this.buttonEducationCatalog);
			this.groupBoxEducationData.Controls.Add(this.label75);
			this.groupBoxEducationData.Controls.Add(this.textBoxEducationCertificate);
			this.groupBoxEducationData.Controls.Add(this.numBoxEducationPrice);
			this.groupBoxEducationData.Controls.Add(this.label74);
			this.groupBoxEducationData.Controls.Add(this.numBoxEducationHours);
			this.groupBoxEducationData.Controls.Add(this.numBoxEducationDays);
			this.groupBoxEducationData.Controls.Add(this.label73);
			this.groupBoxEducationData.Controls.Add(this.label68);
			this.groupBoxEducationData.Controls.Add(this.label67);
			this.groupBoxEducationData.Controls.Add(this.textBoxEducationCode);
			this.groupBoxEducationData.Controls.Add(this.label66);
			this.groupBoxEducationData.Controls.Add(this.label63);
			this.groupBoxEducationData.Controls.Add(this.label77);
			this.groupBoxEducationData.Controls.Add(this.label78);
			this.groupBoxEducationData.Controls.Add(this.label79);
			this.groupBoxEducationData.Location = new System.Drawing.Point(8, 8);
			this.groupBoxEducationData.Name = "groupBoxEducationData";
			this.groupBoxEducationData.Size = new System.Drawing.Size(968, 208);
			this.groupBoxEducationData.TabIndex = 1;
			this.groupBoxEducationData.TabStop = false;
			this.groupBoxEducationData.Text = "Данни за обучението";
			// 
			// textBoxEducationArea
			// 
			this.textBoxEducationArea.Location = new System.Drawing.Point(8, 32);
			this.textBoxEducationArea.Name = "textBoxEducationArea";
			this.textBoxEducationArea.Size = new System.Drawing.Size(954, 20);
			this.textBoxEducationArea.TabIndex = 0;
			// 
			// textBoxEducationTheme
			// 
			this.textBoxEducationTheme.Location = new System.Drawing.Point(8, 72);
			this.textBoxEducationTheme.Name = "textBoxEducationTheme";
			this.textBoxEducationTheme.Size = new System.Drawing.Size(778, 20);
			this.textBoxEducationTheme.TabIndex = 1;
			// 
			// label76
			// 
			this.label76.Location = new System.Drawing.Point(8, 136);
			this.label76.Name = "label76";
			this.label76.Size = new System.Drawing.Size(170, 16);
			this.label76.TabIndex = 139;
			this.label76.Text = "От дата:";
			// 
			// textBoxEducationOrganisation
			// 
			this.textBoxEducationOrganisation.Location = new System.Drawing.Point(638, 152);
			this.textBoxEducationOrganisation.Name = "textBoxEducationOrganisation";
			this.textBoxEducationOrganisation.Size = new System.Drawing.Size(324, 20);
			this.textBoxEducationOrganisation.TabIndex = 10;
			// 
			// textBoxEducationPlace
			// 
			this.textBoxEducationPlace.Location = new System.Drawing.Point(360, 152);
			this.textBoxEducationPlace.Name = "textBoxEducationPlace";
			this.textBoxEducationPlace.Size = new System.Drawing.Size(272, 20);
			this.textBoxEducationPlace.TabIndex = 9;
			// 
			// dateTimePickerEducationToDate
			// 
			this.dateTimePickerEducationToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerEducationToDate.Location = new System.Drawing.Point(184, 152);
			this.dateTimePickerEducationToDate.Name = "dateTimePickerEducationToDate";
			this.dateTimePickerEducationToDate.Size = new System.Drawing.Size(170, 20);
			this.dateTimePickerEducationToDate.TabIndex = 8;
			// 
			// dateTimePickerEducationFromDate
			// 
			this.dateTimePickerEducationFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerEducationFromDate.Location = new System.Drawing.Point(8, 152);
			this.dateTimePickerEducationFromDate.Name = "dateTimePickerEducationFromDate";
			this.dateTimePickerEducationFromDate.Size = new System.Drawing.Size(170, 20);
			this.dateTimePickerEducationFromDate.TabIndex = 7;
			// 
			// buttonEducationCatalog
			// 
			this.buttonEducationCatalog.Image = ((System.Drawing.Image)(resources.GetObject("buttonEducationCatalog.Image")));
			this.buttonEducationCatalog.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonEducationCatalog.Location = new System.Drawing.Point(372, 178);
			this.buttonEducationCatalog.Name = "buttonEducationCatalog";
			this.buttonEducationCatalog.Size = new System.Drawing.Size(224, 21);
			this.buttonEducationCatalog.TabIndex = 11;
			this.buttonEducationCatalog.Text = "Избери обучение от номенклатура";
			this.buttonEducationCatalog.Click += new System.EventHandler(this.buttonEducationCatalog_Click);
			// 
			// label75
			// 
			this.label75.Location = new System.Drawing.Point(272, 96);
			this.label75.Name = "label75";
			this.label75.Size = new System.Drawing.Size(690, 16);
			this.label75.TabIndex = 131;
			this.label75.Text = "Данни за сертификата:";
			// 
			// textBoxEducationCertificate
			// 
			this.textBoxEducationCertificate.Location = new System.Drawing.Point(272, 112);
			this.textBoxEducationCertificate.Name = "textBoxEducationCertificate";
			this.textBoxEducationCertificate.Size = new System.Drawing.Size(690, 20);
			this.textBoxEducationCertificate.TabIndex = 6;
			// 
			// numBoxEducationPrice
			// 
			this.numBoxEducationPrice.Location = new System.Drawing.Point(184, 112);
			this.numBoxEducationPrice.Name = "numBoxEducationPrice";
			this.numBoxEducationPrice.Size = new System.Drawing.Size(80, 20);
			this.numBoxEducationPrice.TabIndex = 5;
			this.toolTip1.SetToolTip(this.numBoxEducationPrice, "Основна залата");
			// 
			// label74
			// 
			this.label74.Location = new System.Drawing.Point(184, 96);
			this.label74.Name = "label74";
			this.label74.Size = new System.Drawing.Size(80, 16);
			this.label74.TabIndex = 128;
			this.label74.Text = "Цена:";
			// 
			// numBoxEducationHours
			// 
			this.numBoxEducationHours.Location = new System.Drawing.Point(96, 112);
			this.numBoxEducationHours.Name = "numBoxEducationHours";
			this.numBoxEducationHours.Size = new System.Drawing.Size(80, 20);
			this.numBoxEducationHours.TabIndex = 4;
			this.toolTip1.SetToolTip(this.numBoxEducationHours, "Основна залата");
			// 
			// numBoxEducationDays
			// 
			this.numBoxEducationDays.Location = new System.Drawing.Point(8, 112);
			this.numBoxEducationDays.Name = "numBoxEducationDays";
			this.numBoxEducationDays.Size = new System.Drawing.Size(80, 20);
			this.numBoxEducationDays.TabIndex = 3;
			this.toolTip1.SetToolTip(this.numBoxEducationDays, "Основна залата");
			// 
			// label73
			// 
			this.label73.Location = new System.Drawing.Point(96, 96);
			this.label73.Name = "label73";
			this.label73.Size = new System.Drawing.Size(80, 16);
			this.label73.TabIndex = 125;
			this.label73.Text = "Брой часове:";
			// 
			// label68
			// 
			this.label68.Location = new System.Drawing.Point(8, 96);
			this.label68.Name = "label68";
			this.label68.Size = new System.Drawing.Size(80, 16);
			this.label68.TabIndex = 124;
			this.label68.Text = "Брой дни:";
			// 
			// label67
			// 
			this.label67.Location = new System.Drawing.Point(792, 56);
			this.label67.Name = "label67";
			this.label67.Size = new System.Drawing.Size(170, 16);
			this.label67.TabIndex = 123;
			this.label67.Text = "Код:";
			// 
			// textBoxEducationCode
			// 
			this.textBoxEducationCode.Location = new System.Drawing.Point(792, 72);
			this.textBoxEducationCode.Name = "textBoxEducationCode";
			this.textBoxEducationCode.Size = new System.Drawing.Size(170, 20);
			this.textBoxEducationCode.TabIndex = 2;
			// 
			// label66
			// 
			this.label66.Location = new System.Drawing.Point(8, 56);
			this.label66.Name = "label66";
			this.label66.Size = new System.Drawing.Size(778, 16);
			this.label66.TabIndex = 121;
			this.label66.Text = "Тема:";
			// 
			// label63
			// 
			this.label63.Location = new System.Drawing.Point(8, 16);
			this.label63.Name = "label63";
			this.label63.Size = new System.Drawing.Size(954, 16);
			this.label63.TabIndex = 1;
			this.label63.Text = "Област на обучението:";
			// 
			// label77
			// 
			this.label77.Location = new System.Drawing.Point(184, 136);
			this.label77.Name = "label77";
			this.label77.Size = new System.Drawing.Size(170, 16);
			this.label77.TabIndex = 139;
			this.label77.Text = "До дата:";
			// 
			// label78
			// 
			this.label78.Location = new System.Drawing.Point(360, 136);
			this.label78.Name = "label78";
			this.label78.Size = new System.Drawing.Size(272, 16);
			this.label78.TabIndex = 139;
			this.label78.Text = "Място на провеждане:";
			// 
			// label79
			// 
			this.label79.Location = new System.Drawing.Point(638, 136);
			this.label79.Name = "label79";
			this.label79.Size = new System.Drawing.Size(324, 16);
			this.label79.TabIndex = 139;
			this.label79.Text = "Обучаваща организация:";
			// 
			// tabPageMilitaryRang
			// 
			this.tabPageMilitaryRang.Controls.Add(this.buttonRangExcel);
			this.tabPageMilitaryRang.Controls.Add(this.buttonRangNew);
			this.tabPageMilitaryRang.Controls.Add(this.buttonRangPrint);
			this.tabPageMilitaryRang.Controls.Add(this.buttonRangCancel);
			this.tabPageMilitaryRang.Controls.Add(this.buttonRangDelete);
			this.tabPageMilitaryRang.Controls.Add(this.buttonRangSave);
			this.tabPageMilitaryRang.Controls.Add(this.buttonRangEdit);
			this.tabPageMilitaryRang.Controls.Add(this.groupBoxRangHistory);
			this.tabPageMilitaryRang.Controls.Add(this.groupBox15);
			this.tabPageMilitaryRang.Location = new System.Drawing.Point(4, 22);
			this.tabPageMilitaryRang.Name = "tabPageMilitaryRang";
			this.tabPageMilitaryRang.Size = new System.Drawing.Size(984, 615);
			this.tabPageMilitaryRang.TabIndex = 10;
			this.tabPageMilitaryRang.Text = "Военни звания";
			this.tabPageMilitaryRang.UseVisualStyleBackColor = true;
			// 
			// buttonRangExcel
			// 
			this.buttonRangExcel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonRangExcel.Image = ((System.Drawing.Image)(resources.GetObject("buttonRangExcel.Image")));
			this.buttonRangExcel.Location = new System.Drawing.Point(65, 577);
			this.buttonRangExcel.Name = "buttonRangExcel";
			this.buttonRangExcel.Size = new System.Drawing.Size(27, 23);
			this.buttonRangExcel.TabIndex = 8;
			this.buttonRangExcel.UseVisualStyleBackColor = true;
			this.buttonRangExcel.Click += new System.EventHandler(this.buttonRangExcel_Click);
			// 
			// buttonRangNew
			// 
			this.buttonRangNew.Image = ((System.Drawing.Image)(resources.GetObject("buttonRangNew.Image")));
			this.buttonRangNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonRangNew.Location = new System.Drawing.Point(790, 577);
			this.buttonRangNew.Name = "buttonRangNew";
			this.buttonRangNew.Size = new System.Drawing.Size(130, 23);
			this.buttonRangNew.TabIndex = 15;
			this.buttonRangNew.Tag = "Въвеждане на ново наказание";
			this.buttonRangNew.Text = "Звание";
			this.buttonRangNew.Click += new System.EventHandler(this.buttonRangNew_Click);
			// 
			// buttonRangPrint
			// 
			this.buttonRangPrint.Image = ((System.Drawing.Image)(resources.GetObject("buttonRangPrint.Image")));
			this.buttonRangPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonRangPrint.Location = new System.Drawing.Point(100, 577);
			this.buttonRangPrint.Name = "buttonRangPrint";
			this.buttonRangPrint.Size = new System.Drawing.Size(130, 23);
			this.buttonRangPrint.TabIndex = 10;
			this.buttonRangPrint.Tag = "Печат на бланка за наказание";
			this.buttonRangPrint.Text = "Печат";
			this.buttonRangPrint.Click += new System.EventHandler(this.buttonPrintD_Click);
			// 
			// buttonRangCancel
			// 
			this.buttonRangCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonRangCancel.Image")));
			this.buttonRangCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonRangCancel.Location = new System.Drawing.Point(238, 577);
			this.buttonRangCancel.Name = "buttonRangCancel";
			this.buttonRangCancel.Size = new System.Drawing.Size(130, 23);
			this.buttonRangCancel.TabIndex = 11;
			this.buttonRangCancel.Tag = "Отказ от запис на данните";
			this.buttonRangCancel.Text = "Отказ";
			this.buttonRangCancel.Click += new System.EventHandler(this.buttonRangCancel_Click);
			// 
			// buttonRangDelete
			// 
			this.buttonRangDelete.Image = ((System.Drawing.Image)(resources.GetObject("buttonRangDelete.Image")));
			this.buttonRangDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonRangDelete.Location = new System.Drawing.Point(514, 577);
			this.buttonRangDelete.Name = "buttonRangDelete";
			this.buttonRangDelete.Size = new System.Drawing.Size(130, 23);
			this.buttonRangDelete.TabIndex = 13;
			this.buttonRangDelete.Tag = "Премахване на наказание";
			this.buttonRangDelete.Text = "Премахва";
			this.buttonRangDelete.Click += new System.EventHandler(this.buttonRangDelete_Click);
			// 
			// buttonRangSave
			// 
			this.buttonRangSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonRangSave.Image")));
			this.buttonRangSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonRangSave.Location = new System.Drawing.Point(376, 577);
			this.buttonRangSave.Name = "buttonRangSave";
			this.buttonRangSave.Size = new System.Drawing.Size(130, 23);
			this.buttonRangSave.TabIndex = 12;
			this.buttonRangSave.Tag = "Запис на данните";
			this.buttonRangSave.Text = "Запис";
			this.buttonRangSave.Click += new System.EventHandler(this.buttonRangSave_Click);
			// 
			// buttonRangEdit
			// 
			this.buttonRangEdit.Image = ((System.Drawing.Image)(resources.GetObject("buttonRangEdit.Image")));
			this.buttonRangEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonRangEdit.Location = new System.Drawing.Point(652, 577);
			this.buttonRangEdit.Name = "buttonRangEdit";
			this.buttonRangEdit.Size = new System.Drawing.Size(130, 23);
			this.buttonRangEdit.TabIndex = 14;
			this.buttonRangEdit.Tag = "Корекция на данните за избраното наказание";
			this.buttonRangEdit.Text = "Корекция";
			this.buttonRangEdit.Click += new System.EventHandler(this.buttonRangEdit_Click);
			// 
			// groupBoxRangHistory
			// 
			this.groupBoxRangHistory.Controls.Add(this.dataGridViewRang);
			this.groupBoxRangHistory.Location = new System.Drawing.Point(8, 127);
			this.groupBoxRangHistory.Name = "groupBoxRangHistory";
			this.groupBoxRangHistory.Size = new System.Drawing.Size(968, 444);
			this.groupBoxRangHistory.TabIndex = 9;
			this.groupBoxRangHistory.TabStop = false;
			this.groupBoxRangHistory.Text = "Военни звания";
			// 
			// dataGridViewRang
			// 
			this.dataGridViewRang.AllowUserToAddRows = false;
			this.dataGridViewRang.AllowUserToDeleteRows = false;
			this.dataGridViewRang.AllowUserToResizeRows = false;
			this.dataGridViewRang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			dataGridViewCellStyle100.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle100.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle100.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle100.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle100.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle100.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle100.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewRang.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle100;
			this.dataGridViewRang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle101.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle101.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle101.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle101.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle101.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle101.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle101.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewRang.DefaultCellStyle = dataGridViewCellStyle101;
			this.dataGridViewRang.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewRang.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridViewRang.Location = new System.Drawing.Point(3, 16);
			this.dataGridViewRang.MultiSelect = false;
			this.dataGridViewRang.Name = "dataGridViewRang";
			this.dataGridViewRang.ReadOnly = true;
			dataGridViewCellStyle102.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle102.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle102.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle102.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle102.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle102.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle102.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewRang.RowHeadersDefaultCellStyle = dataGridViewCellStyle102;
			this.dataGridViewRang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewRang.Size = new System.Drawing.Size(962, 425);
			this.dataGridViewRang.TabIndex = 0;
			this.dataGridViewRang.Click += new System.EventHandler(this.dataGridRang_Click);
			// 
			// groupBox15
			// 
			this.groupBox15.Controls.Add(this.buttonRangdegreeNomenklature);
			this.groupBox15.Controls.Add(this.label108);
			this.groupBox15.Controls.Add(this.comboBoxNSODegree);
			this.groupBox15.Controls.Add(this.buttonMilitaryAssignemntLink);
			this.groupBox15.Controls.Add(this.buttonRangNomenklature);
			this.groupBox15.Controls.Add(this.label88);
			this.groupBox15.Controls.Add(this.textBoxRangOrderNumber);
			this.groupBox15.Controls.Add(this.label89);
			this.groupBox15.Controls.Add(this.label90);
			this.groupBox15.Controls.Add(this.label91);
			this.groupBox15.Controls.Add(this.dateTimePickerRangValidFrom);
			this.groupBox15.Controls.Add(this.dateTimePickerRangOrderDate);
			this.groupBox15.Controls.Add(this.comboBoxNSORang);
			this.groupBox15.Location = new System.Drawing.Point(8, 14);
			this.groupBox15.Name = "groupBox15";
			this.groupBox15.Size = new System.Drawing.Size(968, 107);
			this.groupBox15.TabIndex = 7;
			this.groupBox15.TabStop = false;
			this.groupBox15.Text = "Данни за военно звание";
			// 
			// buttonRangdegreeNomenklature
			// 
			this.buttonRangdegreeNomenklature.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRangdegreeNomenklature.Image = ((System.Drawing.Image)(resources.GetObject("buttonRangdegreeNomenklature.Image")));
			this.buttonRangdegreeNomenklature.Location = new System.Drawing.Point(284, 78);
			this.buttonRangdegreeNomenklature.Name = "buttonRangdegreeNomenklature";
			this.buttonRangdegreeNomenklature.Size = new System.Drawing.Size(21, 21);
			this.buttonRangdegreeNomenklature.TabIndex = 26;
			this.buttonRangdegreeNomenklature.TabStop = false;
			this.buttonRangdegreeNomenklature.Tag = "Добавяне на данни към номенклатурата за образование";
			this.toolTip1.SetToolTip(this.buttonRangdegreeNomenklature, "Номенклатура образование");
			this.buttonRangdegreeNomenklature.Click += new System.EventHandler(this.buttonRangdegreeNomenklature_Click);
			// 
			// label108
			// 
			this.label108.AutoSize = true;
			this.label108.Location = new System.Drawing.Point(17, 61);
			this.label108.Name = "label108";
			this.label108.Size = new System.Drawing.Size(142, 13);
			this.label108.TabIndex = 25;
			this.label108.Text = "Степен на военно звание :";
			// 
			// comboBoxNSODegree
			// 
			this.comboBoxNSODegree.FormattingEnabled = true;
			this.comboBoxNSODegree.Location = new System.Drawing.Point(17, 78);
			this.comboBoxNSODegree.Name = "comboBoxNSODegree";
			this.comboBoxNSODegree.Size = new System.Drawing.Size(250, 21);
			this.comboBoxNSODegree.TabIndex = 24;
			// 
			// buttonMilitaryAssignemntLink
			// 
			this.buttonMilitaryAssignemntLink.Image = ((System.Drawing.Image)(resources.GetObject("buttonMilitaryAssignemntLink.Image")));
			this.buttonMilitaryAssignemntLink.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonMilitaryAssignemntLink.Location = new System.Drawing.Point(389, 78);
			this.buttonMilitaryAssignemntLink.Name = "buttonMilitaryAssignemntLink";
			this.buttonMilitaryAssignemntLink.Size = new System.Drawing.Size(191, 23);
			this.buttonMilitaryAssignemntLink.TabIndex = 23;
			this.buttonMilitaryAssignemntLink.Tag = "";
			this.buttonMilitaryAssignemntLink.Text = "Връзка с назначение";
			this.buttonMilitaryAssignemntLink.Click += new System.EventHandler(this.buttonMilitaryAssignmentLink_Click);
			// 
			// buttonRangNomenklature
			// 
			this.buttonRangNomenklature.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonRangNomenklature.Image = ((System.Drawing.Image)(resources.GetObject("buttonRangNomenklature.Image")));
			this.buttonRangNomenklature.Location = new System.Drawing.Point(284, 37);
			this.buttonRangNomenklature.Name = "buttonRangNomenklature";
			this.buttonRangNomenklature.Size = new System.Drawing.Size(21, 21);
			this.buttonRangNomenklature.TabIndex = 22;
			this.buttonRangNomenklature.TabStop = false;
			this.buttonRangNomenklature.Tag = "Добавяне на данни към номенклатурата за образование";
			this.toolTip1.SetToolTip(this.buttonRangNomenklature, "Номенклатура образование");
			this.buttonRangNomenklature.Click += new System.EventHandler(this.buttonNomenkMilitaryRang_Click);
			// 
			// label88
			// 
			this.label88.AutoSize = true;
			this.label88.Location = new System.Drawing.Point(754, 19);
			this.label88.Name = "label88";
			this.label88.Size = new System.Drawing.Size(61, 13);
			this.label88.TabIndex = 21;
			this.label88.Text = "В сила от :";
			// 
			// textBoxRangOrderNumber
			// 
			this.textBoxRangOrderNumber.Location = new System.Drawing.Point(321, 37);
			this.textBoxRangOrderNumber.Name = "textBoxRangOrderNumber";
			this.textBoxRangOrderNumber.Size = new System.Drawing.Size(200, 20);
			this.textBoxRangOrderNumber.TabIndex = 20;
			// 
			// label89
			// 
			this.label89.AutoSize = true;
			this.label89.Location = new System.Drawing.Point(538, 19);
			this.label89.Name = "label89";
			this.label89.Size = new System.Drawing.Size(188, 13);
			this.label89.TabIndex = 19;
			this.label89.Text = "Дата на подписване на заповедта :";
			// 
			// label90
			// 
			this.label90.AutoSize = true;
			this.label90.Location = new System.Drawing.Point(321, 19);
			this.label90.Name = "label90";
			this.label90.Size = new System.Drawing.Size(101, 13);
			this.label90.TabIndex = 18;
			this.label90.Text = "Номер на заповед";
			// 
			// label91
			// 
			this.label91.AutoSize = true;
			this.label91.Location = new System.Drawing.Point(17, 20);
			this.label91.Name = "label91";
			this.label91.Size = new System.Drawing.Size(89, 13);
			this.label91.TabIndex = 17;
			this.label91.Text = "Военно звание :";
			// 
			// dateTimePickerRangValidFrom
			// 
			this.dateTimePickerRangValidFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerRangValidFrom.Location = new System.Drawing.Point(754, 37);
			this.dateTimePickerRangValidFrom.Name = "dateTimePickerRangValidFrom";
			this.dateTimePickerRangValidFrom.Size = new System.Drawing.Size(200, 20);
			this.dateTimePickerRangValidFrom.TabIndex = 16;
			// 
			// dateTimePickerRangOrderDate
			// 
			this.dateTimePickerRangOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerRangOrderDate.Location = new System.Drawing.Point(538, 37);
			this.dateTimePickerRangOrderDate.Name = "dateTimePickerRangOrderDate";
			this.dateTimePickerRangOrderDate.Size = new System.Drawing.Size(200, 20);
			this.dateTimePickerRangOrderDate.TabIndex = 15;
			// 
			// comboBoxNSORang
			// 
			this.comboBoxNSORang.FormattingEnabled = true;
			this.comboBoxNSORang.Location = new System.Drawing.Point(17, 37);
			this.comboBoxNSORang.Name = "comboBoxNSORang";
			this.comboBoxNSORang.Size = new System.Drawing.Size(250, 21);
			this.comboBoxNSORang.TabIndex = 14;
			// 
			// tabPageCards
			// 
			this.tabPageCards.Controls.Add(this.buttonCardExcel);
			this.tabPageCards.Controls.Add(this.buttonCardNew);
			this.tabPageCards.Controls.Add(this.buttonCardPrint);
			this.tabPageCards.Controls.Add(this.buttonCardCancel);
			this.tabPageCards.Controls.Add(this.buttonCardDelete);
			this.tabPageCards.Controls.Add(this.buttonCardSave);
			this.tabPageCards.Controls.Add(this.buttonCardEdit);
			this.tabPageCards.Controls.Add(this.groupBoxCardHistory);
			this.tabPageCards.Controls.Add(this.groupBox10);
			this.tabPageCards.Location = new System.Drawing.Point(4, 22);
			this.tabPageCards.Name = "tabPageCards";
			this.tabPageCards.Size = new System.Drawing.Size(984, 615);
			this.tabPageCards.TabIndex = 11;
			this.tabPageCards.Text = "Служебни карти";
			this.tabPageCards.UseVisualStyleBackColor = true;
			// 
			// buttonCardExcel
			// 
			this.buttonCardExcel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonCardExcel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCardExcel.Image")));
			this.buttonCardExcel.Location = new System.Drawing.Point(55, 585);
			this.buttonCardExcel.Name = "buttonCardExcel";
			this.buttonCardExcel.Size = new System.Drawing.Size(27, 23);
			this.buttonCardExcel.TabIndex = 16;
			this.buttonCardExcel.UseVisualStyleBackColor = true;
			this.buttonCardExcel.Click += new System.EventHandler(this.buttonCardExcel_Click);
			// 
			// buttonCardNew
			// 
			this.buttonCardNew.Image = ((System.Drawing.Image)(resources.GetObject("buttonCardNew.Image")));
			this.buttonCardNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonCardNew.Location = new System.Drawing.Point(780, 585);
			this.buttonCardNew.Name = "buttonCardNew";
			this.buttonCardNew.Size = new System.Drawing.Size(130, 23);
			this.buttonCardNew.TabIndex = 22;
			this.buttonCardNew.Tag = "Въвеждане на ново наказание";
			this.buttonCardNew.Text = "Карта";
			this.buttonCardNew.Click += new System.EventHandler(this.buttonCardNew_Click);
			// 
			// buttonCardPrint
			// 
			this.buttonCardPrint.Image = ((System.Drawing.Image)(resources.GetObject("buttonCardPrint.Image")));
			this.buttonCardPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonCardPrint.Location = new System.Drawing.Point(90, 585);
			this.buttonCardPrint.Name = "buttonCardPrint";
			this.buttonCardPrint.Size = new System.Drawing.Size(130, 23);
			this.buttonCardPrint.TabIndex = 17;
			this.buttonCardPrint.Tag = "Печат на бланка за наказание";
			this.buttonCardPrint.Text = "Печат";
			this.buttonCardPrint.Click += new System.EventHandler(this.buttonPrintD_Click);
			// 
			// buttonCardCancel
			// 
			this.buttonCardCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCardCancel.Image")));
			this.buttonCardCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonCardCancel.Location = new System.Drawing.Point(228, 585);
			this.buttonCardCancel.Name = "buttonCardCancel";
			this.buttonCardCancel.Size = new System.Drawing.Size(130, 23);
			this.buttonCardCancel.TabIndex = 18;
			this.buttonCardCancel.Tag = "Отказ от запис на данните";
			this.buttonCardCancel.Text = "Отказ";
			this.buttonCardCancel.Click += new System.EventHandler(this.buttonCardCancel_Click);
			// 
			// buttonCardDelete
			// 
			this.buttonCardDelete.Image = ((System.Drawing.Image)(resources.GetObject("buttonCardDelete.Image")));
			this.buttonCardDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonCardDelete.Location = new System.Drawing.Point(504, 585);
			this.buttonCardDelete.Name = "buttonCardDelete";
			this.buttonCardDelete.Size = new System.Drawing.Size(130, 23);
			this.buttonCardDelete.TabIndex = 20;
			this.buttonCardDelete.Tag = "Премахване на наказание";
			this.buttonCardDelete.Text = "Премахва";
			this.buttonCardDelete.Click += new System.EventHandler(this.buttonCardDelete_Click);
			// 
			// buttonCardSave
			// 
			this.buttonCardSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonCardSave.Image")));
			this.buttonCardSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonCardSave.Location = new System.Drawing.Point(366, 585);
			this.buttonCardSave.Name = "buttonCardSave";
			this.buttonCardSave.Size = new System.Drawing.Size(130, 23);
			this.buttonCardSave.TabIndex = 19;
			this.buttonCardSave.Tag = "Запис на данните";
			this.buttonCardSave.Text = "Запис";
			this.buttonCardSave.Click += new System.EventHandler(this.buttonCardSave_Click);
			// 
			// buttonCardEdit
			// 
			this.buttonCardEdit.Image = ((System.Drawing.Image)(resources.GetObject("buttonCardEdit.Image")));
			this.buttonCardEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonCardEdit.Location = new System.Drawing.Point(642, 585);
			this.buttonCardEdit.Name = "buttonCardEdit";
			this.buttonCardEdit.Size = new System.Drawing.Size(130, 23);
			this.buttonCardEdit.TabIndex = 21;
			this.buttonCardEdit.Tag = "Корекция на данните за избраното наказание";
			this.buttonCardEdit.Text = "Корекция";
			this.buttonCardEdit.Click += new System.EventHandler(this.buttonCardEdit_Click);
			// 
			// groupBoxCardHistory
			// 
			this.groupBoxCardHistory.Controls.Add(this.dataGridViewCards);
			this.groupBoxCardHistory.Location = new System.Drawing.Point(8, 119);
			this.groupBoxCardHistory.Name = "groupBoxCardHistory";
			this.groupBoxCardHistory.Size = new System.Drawing.Size(968, 444);
			this.groupBoxCardHistory.TabIndex = 10;
			this.groupBoxCardHistory.TabStop = false;
			this.groupBoxCardHistory.Text = "Служебни карти";
			// 
			// dataGridViewCards
			// 
			this.dataGridViewCards.AllowUserToAddRows = false;
			this.dataGridViewCards.AllowUserToDeleteRows = false;
			this.dataGridViewCards.AllowUserToResizeRows = false;
			this.dataGridViewCards.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			dataGridViewCellStyle103.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle103.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle103.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle103.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle103.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle103.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle103.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCards.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle103;
			this.dataGridViewCards.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle104.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle104.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle104.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle104.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle104.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle104.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle104.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewCards.DefaultCellStyle = dataGridViewCellStyle104;
			this.dataGridViewCards.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewCards.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridViewCards.Location = new System.Drawing.Point(3, 16);
			this.dataGridViewCards.MultiSelect = false;
			this.dataGridViewCards.Name = "dataGridViewCards";
			this.dataGridViewCards.ReadOnly = true;
			dataGridViewCellStyle105.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle105.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle105.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle105.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle105.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle105.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle105.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridViewCards.RowHeadersDefaultCellStyle = dataGridViewCellStyle105;
			this.dataGridViewCards.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewCards.Size = new System.Drawing.Size(962, 425);
			this.dataGridViewCards.TabIndex = 1;
			this.dataGridViewCards.Click += new System.EventHandler(this.dataGridCard_Click);
			// 
			// groupBox10
			// 
			this.groupBox10.Controls.Add(this.textBoxCardSign);
			this.groupBox10.Controls.Add(this.label120);
			this.groupBox10.Controls.Add(this.textBoxCardSeries);
			this.groupBox10.Controls.Add(this.label119);
			this.groupBox10.Controls.Add(this.textBoxCardNumber);
			this.groupBox10.Controls.Add(this.label113);
			this.groupBox10.Controls.Add(this.label118);
			this.groupBox10.Controls.Add(this.comboBoxCardMilitaryRangEng);
			this.groupBox10.Controls.Add(this.label115);
			this.groupBox10.Controls.Add(this.label117);
			this.groupBox10.Controls.Add(this.dateTimePickerCardIssue);
			this.groupBox10.Controls.Add(this.comboBoxCardMilitaryRang);
			this.groupBox10.Location = new System.Drawing.Point(8, 12);
			this.groupBox10.Name = "groupBox10";
			this.groupBox10.Size = new System.Drawing.Size(968, 107);
			this.groupBox10.TabIndex = 8;
			this.groupBox10.TabStop = false;
			this.groupBox10.Text = "Данни за военно звание";
			// 
			// textBoxCardSign
			// 
			this.textBoxCardSign.Location = new System.Drawing.Point(17, 78);
			this.textBoxCardSign.Name = "textBoxCardSign";
			this.textBoxCardSign.Size = new System.Drawing.Size(207, 20);
			this.textBoxCardSign.TabIndex = 33;
			// 
			// label120
			// 
			this.label120.AutoSize = true;
			this.label120.Location = new System.Drawing.Point(14, 61);
			this.label120.Name = "label120";
			this.label120.Size = new System.Drawing.Size(83, 13);
			this.label120.TabIndex = 32;
			this.label120.Text = "Номер на знак";
			// 
			// textBoxCardSeries
			// 
			this.textBoxCardSeries.Location = new System.Drawing.Point(731, 37);
			this.textBoxCardSeries.Name = "textBoxCardSeries";
			this.textBoxCardSeries.Size = new System.Drawing.Size(207, 20);
			this.textBoxCardSeries.TabIndex = 31;
			// 
			// label119
			// 
			this.label119.AutoSize = true;
			this.label119.Location = new System.Drawing.Point(728, 20);
			this.label119.Name = "label119";
			this.label119.Size = new System.Drawing.Size(89, 13);
			this.label119.TabIndex = 30;
			this.label119.Text = "Номер на серия";
			// 
			// textBoxCardNumber
			// 
			this.textBoxCardNumber.Location = new System.Drawing.Point(496, 38);
			this.textBoxCardNumber.Name = "textBoxCardNumber";
			this.textBoxCardNumber.Size = new System.Drawing.Size(207, 20);
			this.textBoxCardNumber.TabIndex = 29;
			// 
			// label113
			// 
			this.label113.AutoSize = true;
			this.label113.Location = new System.Drawing.Point(493, 21);
			this.label113.Name = "label113";
			this.label113.Size = new System.Drawing.Size(88, 13);
			this.label113.TabIndex = 28;
			this.label113.Text = "Номер на карта";
			// 
			// label118
			// 
			this.label118.AutoSize = true;
			this.label118.Location = new System.Drawing.Point(257, 20);
			this.label118.Name = "label118";
			this.label118.Size = new System.Drawing.Size(89, 13);
			this.label118.TabIndex = 27;
			this.label118.Text = "Военно звание :";
			// 
			// comboBoxCardMilitaryRangEng
			// 
			this.comboBoxCardMilitaryRangEng.FormattingEnabled = true;
			this.comboBoxCardMilitaryRangEng.Location = new System.Drawing.Point(260, 37);
			this.comboBoxCardMilitaryRangEng.Name = "comboBoxCardMilitaryRangEng";
			this.comboBoxCardMilitaryRangEng.Size = new System.Drawing.Size(207, 21);
			this.comboBoxCardMilitaryRangEng.TabIndex = 26;
			// 
			// label115
			// 
			this.label115.AutoSize = true;
			this.label115.Location = new System.Drawing.Point(257, 61);
			this.label115.Name = "label115";
			this.label115.Size = new System.Drawing.Size(99, 13);
			this.label115.TabIndex = 19;
			this.label115.Text = "Дата на издаване";
			// 
			// label117
			// 
			this.label117.AutoSize = true;
			this.label117.Location = new System.Drawing.Point(17, 20);
			this.label117.Name = "label117";
			this.label117.Size = new System.Drawing.Size(89, 13);
			this.label117.TabIndex = 17;
			this.label117.Text = "Военно звание :";
			// 
			// dateTimePickerCardIssue
			// 
			this.dateTimePickerCardIssue.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerCardIssue.Location = new System.Drawing.Point(260, 78);
			this.dateTimePickerCardIssue.Name = "dateTimePickerCardIssue";
			this.dateTimePickerCardIssue.Size = new System.Drawing.Size(207, 20);
			this.dateTimePickerCardIssue.TabIndex = 15;
			// 
			// comboBoxCardMilitaryRang
			// 
			this.comboBoxCardMilitaryRang.FormattingEnabled = true;
			this.comboBoxCardMilitaryRang.Location = new System.Drawing.Point(17, 37);
			this.comboBoxCardMilitaryRang.Name = "comboBoxCardMilitaryRang";
			this.comboBoxCardMilitaryRang.Size = new System.Drawing.Size(207, 21);
			this.comboBoxCardMilitaryRang.TabIndex = 14;
			// 
			// label96
			// 
			this.label96.Location = new System.Drawing.Point(790, 62);
			this.label96.Name = "label96";
			this.label96.Size = new System.Drawing.Size(170, 16);
			this.label96.TabIndex = 21;
			this.label96.Text = "От дата :";
			this.toolTip1.SetToolTip(this.label96, "Дата на която влиза в сила заповедта");
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
			this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button1.Location = new System.Drawing.Point(755, 647);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(130, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "Изход";
			this.toolTip1.SetToolTip(this.button1, "Затваряне на досието");
			this.button1.Visible = false;
			this.button1.Click += new System.EventHandler(this.FitPersonsToTheirPositions);
			// 
			// radioButton3
			// 
			this.radioButton3.Checked = true;
			this.radioButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.radioButton3.Location = new System.Drawing.Point(336, 6);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new System.Drawing.Size(140, 24);
			this.radioButton3.TabIndex = 124;
			this.radioButton3.TabStop = true;
			this.radioButton3.Text = "Наказания";
			// 
			// radioButton4
			// 
			this.radioButton4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.radioButton4.Location = new System.Drawing.Point(508, 6);
			this.radioButton4.Name = "radioButton4";
			this.radioButton4.Size = new System.Drawing.Size(140, 24);
			this.radioButton4.TabIndex = 125;
			this.radioButton4.Text = "Награди";
			// 
			// groupBox12
			// 
			this.groupBox12.Controls.Add(this.dataGridView2);
			this.groupBox12.Location = new System.Drawing.Point(8, 139);
			this.groupBox12.Name = "groupBox12";
			this.groupBox12.Size = new System.Drawing.Size(968, 444);
			this.groupBox12.TabIndex = 1;
			this.groupBox12.TabStop = false;
			this.groupBox12.Text = "Данни за  наложени наказания за служителя";
			// 
			// dataGridView2
			// 
			this.dataGridView2.AllowUserToAddRows = false;
			this.dataGridView2.AllowUserToDeleteRows = false;
			this.dataGridView2.AllowUserToResizeRows = false;
			this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			dataGridViewCellStyle106.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle106.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle106.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle106.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle106.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle106.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle106.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle106;
			this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle107.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle107.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle107.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle107.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle107.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle107.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle107.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle107;
			this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dataGridView2.Location = new System.Drawing.Point(3, 16);
			this.dataGridView2.MultiSelect = false;
			this.dataGridView2.Name = "dataGridView2";
			this.dataGridView2.ReadOnly = true;
			dataGridViewCellStyle108.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle108.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle108.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle108.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle108.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle108.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle108.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle108;
			this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView2.Size = new System.Drawing.Size(962, 425);
			this.dataGridView2.TabIndex = 0;
			// 
			// groupBox13
			// 
			this.groupBox13.Controls.Add(this.label94);
			this.groupBox13.Controls.Add(this.label95);
			this.groupBox13.Controls.Add(this.label96);
			this.groupBox13.Controls.Add(this.label97);
			this.groupBox13.Controls.Add(this.label98);
			this.groupBox13.Controls.Add(this.label99);
			this.groupBox13.Location = new System.Drawing.Point(8, 26);
			this.groupBox13.Name = "groupBox13";
			this.groupBox13.Size = new System.Drawing.Size(968, 107);
			this.groupBox13.TabIndex = 0;
			this.groupBox13.TabStop = false;
			this.groupBox13.Text = "Данни за наказание";
			// 
			// label94
			// 
			this.label94.Location = new System.Drawing.Point(6, 62);
			this.label94.Name = "label94";
			this.label94.Size = new System.Drawing.Size(568, 16);
			this.label94.TabIndex = 28;
			this.label94.Text = "Вид :";
			// 
			// label95
			// 
			this.label95.Location = new System.Drawing.Point(790, 22);
			this.label95.Name = "label95";
			this.label95.Size = new System.Drawing.Size(170, 16);
			this.label95.TabIndex = 26;
			this.label95.Text = "Валидно до:";
			// 
			// label97
			// 
			this.label97.Location = new System.Drawing.Point(606, 62);
			this.label97.Name = "label97";
			this.label97.Size = new System.Drawing.Size(170, 16);
			this.label97.TabIndex = 20;
			this.label97.Text = "Номер заповед :";
			// 
			// label98
			// 
			this.label98.Location = new System.Drawing.Point(6, 22);
			this.label98.Name = "label98";
			this.label98.Size = new System.Drawing.Size(568, 16);
			this.label98.TabIndex = 19;
			this.label98.Text = "Основание :";
			// 
			// label99
			// 
			this.label99.Location = new System.Drawing.Point(606, 22);
			this.label99.Name = "label99";
			this.label99.Size = new System.Drawing.Size(170, 16);
			this.label99.TabIndex = 17;
			this.label99.Text = "В сила от :";
			// 
			// labelTatalStaff
			// 
			this.labelTatalStaff.Location = new System.Drawing.Point(377, 509);
			this.labelTatalStaff.Name = "labelTatalStaff";
			this.labelTatalStaff.Size = new System.Drawing.Size(181, 16);
			this.labelTatalStaff.TabIndex = 132;
			this.labelTatalStaff.Text = "Общ стаж при постъпване :";
			// 
			// numBoxExpTotalD
			// 
			this.numBoxExpTotalD.Location = new System.Drawing.Point(453, 527);
			this.numBoxExpTotalD.Name = "numBoxExpTotalD";
			this.numBoxExpTotalD.Size = new System.Drawing.Size(30, 20);
			this.numBoxExpTotalD.TabIndex = 131;
			this.numBoxExpTotalD.TabStop = false;
			this.numBoxExpTotalD.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// numBoxExpTotalM
			// 
			this.numBoxExpTotalM.Location = new System.Drawing.Point(415, 527);
			this.numBoxExpTotalM.Name = "numBoxExpTotalM";
			this.numBoxExpTotalM.Size = new System.Drawing.Size(30, 20);
			this.numBoxExpTotalM.TabIndex = 130;
			this.numBoxExpTotalM.TabStop = false;
			this.numBoxExpTotalM.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// numBoxExpTotalY
			// 
			this.numBoxExpTotalY.Location = new System.Drawing.Point(377, 527);
			this.numBoxExpTotalY.Name = "numBoxExpTotalY";
			this.numBoxExpTotalY.Size = new System.Drawing.Size(30, 20);
			this.numBoxExpTotalY.TabIndex = 129;
			this.numBoxExpTotalY.TabStop = false;
			this.numBoxExpTotalY.TextChanged += new System.EventHandler(this.PersonalDataChanged);
			// 
			// formPersonalData
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(992, 673);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonОК);
			this.Controls.Add(this.tabControlCardNew);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "formPersonalData";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Лично досие на служител";
			this.Load += new System.EventHandler(this.PersonalDataForm_Load);
			this.TabPersonalInfo.ResumeLayout(false);
			this.TabPersonalInfo.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewLanguages)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox6.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.tabPageAssignment.ResumeLayout(false);
			this.tabPageAssignment.PerformLayout();
			this.groupBoxAssignmentGrid.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewAssignment)).EndInit();
			this.tabPageAbsence.ResumeLayout(false);
			this.groupBoxAbsenceGrid.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewYears)).EndInit();
			this.groupBox4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewAbsence)).EndInit();
			this.groupBoxAbsece.ResumeLayout(false);
			this.groupBoxAbsece.PerformLayout();
			this.tabPagePenalty.ResumeLayout(false);
			this.groupBoxPenaltyGrid.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewPenalties)).EndInit();
			this.groupBoxPenalty.ResumeLayout(false);
			this.groupBoxPenalty.PerformLayout();
			this.tabPageNotes.ResumeLayout(false);
			this.groupBoxNotes.ResumeLayout(false);
			this.groupBoxNotes.PerformLayout();
			this.groupBoxNotesGrid.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewNotes)).EndInit();
			this.groupBoxNotesFilter.ResumeLayout(false);
			this.tabPageAtestacii.ResumeLayout(false);
			this.groupBox9.ResumeLayout(false);
			this.groupBox9.PerformLayout();
			this.groupBoxAttestationRegister.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewAttestations)).EndInit();
			this.groupBox8.ResumeLayout(false);
			this.groupBox8.PerformLayout();
			this.tabControlCardNew.ResumeLayout(false);
			this.tabPageFired.ResumeLayout(false);
			this.groupBoxFired.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewFired)).EndInit();
			this.groupBox7.ResumeLayout(false);
			this.groupBox7.PerformLayout();
			this.tabPageCharacteristics.ResumeLayout(false);
			this.tabPageCharacteristics.PerformLayout();
			this.tabPageEducation.ResumeLayout(false);
			this.groupBoxEducationHistory.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewEducations)).EndInit();
			this.groupBoxEducationData.ResumeLayout(false);
			this.groupBoxEducationData.PerformLayout();
			this.tabPageMilitaryRang.ResumeLayout(false);
			this.groupBoxRangHistory.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewRang)).EndInit();
			this.groupBox15.ResumeLayout(false);
			this.groupBox15.PerformLayout();
			this.tabPageCards.ResumeLayout(false);
			this.groupBoxCardHistory.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewCards)).EndInit();
			this.groupBox10.ResumeLayout(false);
			this.groupBox10.PerformLayout();
			this.groupBox12.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
			this.groupBox13.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Assignment functions

		private void buttonAssignment_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.radioButtonAssignment.Checked && this.dtAssignment.Rows.Count >= 1)
				{
					MessageBox.Show("Не може да има второ назначение", "Грешка при назначаване");
					return;
				}
				else if (this.radioButtonAdditional.Checked && this.dtAssignment.Rows.Count < 1)
				{
					MessageBox.Show("Не може да се сключи допълнително споразумение без да има сключено назначение", "Грешка при назначаване");
					return;
				}
				Op = Operations.AddAssignment;

				this.IsAssignmentEdit = false;
				this.EnableButtons(false, false, true, false, false, true, LockButtons.Assignment);
				this.ControlEnabled(true, LockButtons.Assignment);
				this.comboBoxContract_SelectedIndexChanged(sender, e);
				//this.dateTimePickerAssignedAt.Value = DateTime.Now;
				//this.dateTimePickerContractDate.Value = DateTime.Now;
				this.comboBoxAssignReason_SelectedIndexChanged(sender, e);
				//if (mainForm.DataBaseTypes == DBTypes.MsSql)
				//{
				//	this.numBoxNumHoliday.Text = "20";
				//}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonAssignmentEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				Op = Operations.EditAssignment;
				if (this.dataGridViewAssignment.CurrentRow != null)
				{
					IsAssignmentEdit = true;

					this.EnableButtons(false, false, true, false, false, true, LockButtons.Assignment);
					this.ControlEnabled(true, LockButtons.Assignment);
					this.comboBoxContract_SelectedIndexChanged(sender, e);
					this.comboBoxAssignReason_SelectedIndexChanged(sender, e);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonAssignmentDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.dataGridViewAssignment.CurrentRow != null)
				{
					Dictionary<string, object> pDict = new Dictionary<string, object>();
					Dictionary<string, object> structureDict = new Dictionary<string, object>();
					Dictionary<string, object> oldStructureDict = new Dictionary<string, object>();
					Dictionary<string, object> posDict = new Dictionary<string, object>();
					Dictionary<string, object> isActiveDict = new Dictionary<string, object>();

					bool IsValid = false;

					if (this.radioButtonAssignment.Checked && this.dtAssignment.Rows.Count > 1)
					{
						MessageBox.Show("Не можете да изтриете назначението. По този договор вече има сключено допълнително споразумение.");
						return;
					}
					if (MessageBox.Show(this, "Сигурни ли сте че искате да премахнете назначението?", "Изтриване", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						DataRow rowPosition = this.dtPosition.Rows.Find(this.dataGridViewAssignment.CurrentRow.Cells["PositionID"].Value);
						DataRow DelRow = this.dtAssignment.Rows.Find(this.dataGridViewAssignment.CurrentRow.Cells["ID"].Value);
						if (DelRow == null)
						{
							MessageBox.Show("Грешка при изтриване на назначение", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
							return;
						}
						if (rowPosition != null)
						{ //if the assignment was made on a good position, we have to check if the assingment is active
							int isactive;

							try
							{
								isactive = int.Parse(this.dataGridViewAssignment.CurrentRow.Cells["IsActive"].Value.ToString());
							}
							catch (System.Exception ex)
							{
								MessageBox.Show(ex.Message, "Не може да се определи коректно вида ативнo или неактивно назначение)");
								return;
							}
							#region isactive
							if (isactive == 1)
							{
								float change;
								try
								{
									change = float.Parse(DelRow["staff"].ToString());
								}
								catch (System.FormatException)
								{
									change = 1;
									MessageBox.Show("Некоректно зададена заемана щатна бройка, при освобождаването ще се освободи цяла щатна бройка.");
								}


								int id;
								try
								{
									id = int.Parse(rowPosition["ID"].ToString());
								}
								catch (System.Exception ex)
								{
									MessageBox.Show(ex.Message, "Грешен идентификатор на реда. Не може да се изтрие");
									return;
								}

								if (this.dtAssignment.Rows.Count == 1) //if we delete main assignment - free the position
								{
									//update firmpersonal
									structureDict.Add("free", "'free + " + change.ToString());
									structureDict.Add("busy", "'busy - " + change.ToString());
									//updete person
									pDict.Add("nodeID", "0");

									IsValid = this.dataAdapter.UniversalUpdateObject(TableNames.FirmPersonal3, "id", structureDict, this.dataGridViewAssignment.CurrentRow.Cells["positionid"].Value.ToString(), TransactionComnmand.BEGIN_TRANSACTION);
									if (IsValid == false)
									{
										MessageBox.Show("Грешка при обновяване на структурата на организацията", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
										return;
									}
									//Ако трием основно нaзначение трябва да се зачисти и таблицата за отпуските за да не остава боклук
									IsValid = this.dataAdapter.UniversalDelete(TableNames.YearHoliday, this.parent.ToString(), "parent", TransactionComnmand.USE_TRANSACTION);
									if (IsValid == false)
									{
										MessageBox.Show("Грешка при обновяване на данни за отпуски", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
										return;
									}

									//Ако трием основно нaзначение трябва да се зачисти и таблицата за историята
									IsValid = this.dataAdapter.UniversalDelete(TableNames.NotesTable, this.parent.ToString(), "par", TransactionComnmand.USE_TRANSACTION);
									if (IsValid == false)
									{
										MessageBox.Show("Грешка при обновяване на данни за история", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
										return;
									}
									IsValid = this.dataAdapter.UniversalUpdateParam(TableNames.Person, "id", pDict, this.parent.ToString(), TransactionComnmand.USE_TRANSACTION);
									if (IsValid == false)
									{
										MessageBox.Show("Грешка при обновяване на лични данни", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
										return;
									}
									IsValid = this.dataAdapter.UniversalDelete(TableNames.PersonAssignment, this.dataGridViewAssignment.CurrentRow.Cells["ID"].Value.ToString(), "id", TransactionComnmand.COMMIT_TRANSACTION);
									if (IsValid == false)
									{
										MessageBox.Show("Грешка при изтриване на назначение", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
										return;
									}
									this.dtYearHoliday.Rows.Clear();
									rowPosition["Free"] = (float.Parse(rowPosition["Free"].ToString()) + change).ToString();
									rowPosition["Busy"] = (float.Parse(rowPosition["Busy"].ToString()) - change).ToString();
									this.dtAssignment.Rows.Remove(this.dtAssignment.Rows.Find(this.dataGridViewAssignment.CurrentRow.Cells["ID"].Value));
								}
								else
								{ //тук трием активно допълнително споразумение
									DataView vuePrevAssignmet;
									int previd;
									IsValid = int.TryParse(this.dataGridViewAssignment.CurrentRow.Cells["prevassignmentid"].Value.ToString(), out previd);
									if (IsValid == false)
									{
										if (MessageBox.Show("Грешка при определяне на предходна длъжност. Служителя няма да се прехвърли към предходната си длъжност. Желатете или да продължите?", "Въпрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
											return;
									}
									//										DataView PrevAssignment = new DataView(this.dtAssignment, "parent = " + this.paren + " and id = " + previd, "id", DataViewRowState.CurrentRows);

									structureDict.Add("free", "'free + " + change.ToString());
									structureDict.Add("busy", "'busy - " + change.ToString());

									vuePrevAssignmet = new DataView(this.dtAssignment, "id = " + previd.ToString(), "id", DataViewRowState.CurrentRows);

									if (vuePrevAssignmet.Count <= 0 || vuePrevAssignmet.Count > 1)
									{//Тук влизаме само ако предхордото назначение по някаква причина е сбозено!
										if (MessageBox.Show("Грешка при определяне на предходна длъжност. Служителя няма да се прехвърли към предходната си длъжност. Желатете или да продължите?", "Въпрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
											return;
										pDict.Add("nodeID", "0");

										IsValid = this.dataAdapter.UniversalUpdateParam(TableNames.Person, "id", pDict, this.parent.ToString(), TransactionComnmand.BEGIN_TRANSACTION);
										if (IsValid == false)
										{
											MessageBox.Show("Грешка при обновяване на лични данни", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
											return;
										}
										IsValid = this.dataAdapter.UniversalUpdateObject(TableNames.FirmPersonal3, "id", structureDict, this.dataGridViewAssignment.CurrentRow.Cells["positionid"].Value.ToString(), TransactionComnmand.USE_TRANSACTION);
										if (IsValid == false)
										{
											MessageBox.Show("Грешка при обновяване на данни за длъжност", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
											return;
										}
										IsValid = this.dataAdapter.UniversalDelete(TableNames.PersonAssignment, this.dataGridViewAssignment.CurrentRow.Cells["ID"].Value.ToString(), "id", TransactionComnmand.COMMIT_TRANSACTION);
										if (IsValid == false)
										{
											MessageBox.Show("Грешка при изтриване на назначение", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
											return;
										}

										rowPosition["Free"] = (float.Parse(rowPosition["Free"].ToString()) + change).ToString();
										rowPosition["Busy"] = (float.Parse(rowPosition["Busy"].ToString()) - change).ToString();
										this.dtAssignment.Rows.Remove(this.dtAssignment.Rows.Find(this.dataGridViewAssignment.CurrentRow.Cells["ID"].Value));
									}
									else
									{ //открито е предходното споразумение/назначение
										DataRow rowOldPosition = this.dtPosition.Rows.Find(vuePrevAssignmet[0]["positionid"]);

										pDict.Add("nodeID", rowOldPosition["par"].ToString());

										IsValid = this.dataAdapter.UniversalUpdateParam(TableNames.Person, "id", pDict, this.parent.ToString(), TransactionComnmand.BEGIN_TRANSACTION);
										if (IsValid == false)
										{
											MessageBox.Show("Грешка при обновяване на лични данни", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
											return;
										}
										IsValid = this.dataAdapter.UniversalUpdateObject(TableNames.FirmPersonal3, "id", structureDict, this.dataGridViewAssignment.CurrentRow.Cells["positionid"].Value.ToString(), TransactionComnmand.USE_TRANSACTION);
										if (IsValid == false)
										{
											MessageBox.Show("Грешка при обновяване на данни за длъжност", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
											return;
										}
										oldStructureDict.Add("free", "'free - " + vuePrevAssignmet[0]["staff"].ToString());
										oldStructureDict.Add("busy", "'busy + " + vuePrevAssignmet[0]["staff"].ToString());
										IsValid = this.dataAdapter.UniversalUpdateObject(TableNames.FirmPersonal3, "id", oldStructureDict, vuePrevAssignmet[0]["positionid"].ToString(), TransactionComnmand.USE_TRANSACTION);
										if (IsValid == false)
										{
											MessageBox.Show("Грешка при обновяване на данни за длъжност", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
											return;
										}
										isActiveDict.Add("IsActive", "1"); //активираме старото назначение
										IsValid = this.dataAdapter.UniversalUpdateParam(TableNames.PersonAssignment, "id", isActiveDict, vuePrevAssignmet[0]["id"].ToString(), TransactionComnmand.USE_TRANSACTION);
										if (IsValid == false)
										{
											MessageBox.Show("Грешка при изтриване на назначение", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
											return;
										}
										IsValid = this.dataAdapter.UniversalDelete(TableNames.PersonAssignment, this.dataGridViewAssignment.CurrentRow.Cells["ID"].Value.ToString(), "id", TransactionComnmand.COMMIT_TRANSACTION);
										if (IsValid == false)
										{
											MessageBox.Show("Грешка при изтриване на назначение", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
											return;
										}

										rowPosition["Free"] = (float.Parse(rowPosition["Free"].ToString()) + change).ToString();
										rowPosition["Busy"] = (float.Parse(rowPosition["Busy"].ToString()) - change).ToString();

										rowOldPosition["Free"] = (float.Parse(rowOldPosition["Free"].ToString()) - float.Parse(vuePrevAssignmet[0]["staff"].ToString())).ToString();
										rowOldPosition["Busy"] = (float.Parse(rowOldPosition["Busy"].ToString()) + float.Parse(vuePrevAssignmet[0]["staff"].ToString())).ToString();

										this.dtAssignment.Rows.Remove(this.dtAssignment.Rows.Find(this.dataGridViewAssignment.CurrentRow.Cells["ID"].Value));
									}
								}

								this.positionID = this.oldPositionID = 0;
							}
							#endregion
							else
							{//not active assignemnt
								int id;
								try
								{
									id = int.Parse(rowPosition["ID"].ToString());
								}
								catch (System.Exception ex)
								{
									MessageBox.Show(ex.Message, "Грешен идентификатор на реда. Не може да се изтрие");
									return;
								}

								if (this.dtAssignment.Rows.Count == 1) //if we delete main assignment - free the position
								{
									//updete person
									pDict.Add("nodeID", "0");
									pDict.Add("hiredat", "'NULL");
									//Ако трием основно нaзначение трябва да се зачисти и таблицата за отпуските за да не остава боклук
									IsValid = this.dataAdapter.UniversalDelete(TableNames.YearHoliday, this.parent.ToString(), "parent", TransactionComnmand.BEGIN_TRANSACTION);
									if (IsValid == false)
									{
										MessageBox.Show("Грешка при обновяване на данни за отпуски", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
										return;
									}
									IsValid = this.dataAdapter.UniversalUpdateParam(TableNames.Person, "id", pDict, this.parent.ToString(), TransactionComnmand.USE_TRANSACTION);
									if (IsValid == false)
									{
										MessageBox.Show("Грешка при обновяване на лични данни", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
										return;
									}
									IsValid = this.dataAdapter.UniversalDelete(TableNames.PersonAssignment, this.dataGridViewAssignment.CurrentRow.Cells["ID"].Value.ToString(), "id", TransactionComnmand.COMMIT_TRANSACTION);
									if (IsValid == false)
									{
										MessageBox.Show("Грешка при изтриване на назначение", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
										return;
									}
									this.dtYearHoliday.Rows.Clear();

									this.dtAssignment.Rows.Remove(this.dtAssignment.Rows.Find(this.dataGridViewAssignment.CurrentRow.Cells["ID"].Value));
								}
								else
								{
									DataView vueNextAssignmet;
									int nextid;

									int.TryParse(this.dataGridViewAssignment.CurrentRow.Cells["prevassignmentid"].Value.ToString(), out nextid);

									vueNextAssignmet = new DataView(this.dtAssignment, "prevassignmentid = " + nextid.ToString(), "id", DataViewRowState.CurrentRows);

									if (vueNextAssignmet.Count <= 0 || vueNextAssignmet.Count > 1)
									{
										IsValid = this.dataAdapter.UniversalDelete(TableNames.PersonAssignment, this.dataGridViewAssignment.CurrentRow.Cells["ID"].Value.ToString(), "id");
										if (IsValid == false)
										{
											MessageBox.Show("Грешка при изтриване на назначение", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
											return;
										}
										this.dtAssignment.Rows.Remove(this.dtAssignment.Rows.Find(this.dataGridViewAssignment.CurrentRow.Cells["ID"].Value));
									}
									else
									{
										DataRow rowNextPosition = this.dtPosition.Rows.Find(vueNextAssignmet[0]["id"]);

										pDict.Clear();
										pDict.Add("prevassignmentid", this.dataGridViewAssignment.CurrentRow.Cells["prevassignmentid"].Value.ToString());
										IsValid = this.dataAdapter.UniversalUpdateParam(TableNames.PersonAssignment, "id", pDict, vueNextAssignmet[0]["id"].ToString(), TransactionComnmand.BEGIN_TRANSACTION);
										if (IsValid == false)
										{
											//catch(Exception ex)
											//just make a log here later
											//MessageBox.Show("Грешка при обновяване на данни за длъжност", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
											//return;
										}
										IsValid = this.dataAdapter.UniversalDelete(TableNames.PersonAssignment, this.dataGridViewAssignment.CurrentRow.Cells["ID"].Value.ToString(), "id", TransactionComnmand.COMMIT_TRANSACTION);
										if (IsValid == false)
										{
											MessageBox.Show("Грешка при изтриване на назначение", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
											return;
										}

										this.dtAssignment.Rows.Remove(this.dtAssignment.Rows.Find(this.dataGridViewAssignment.CurrentRow.Cells["ID"].Value));
									}
								}
								this.positionID = this.oldPositionID = 0;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonAssignmentSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.IsAssignment && !this.IsAssignmentEdit)
				{
					this.SaveAss();
				}
				else if (!this.IsAssignment && !this.IsAssignmentEdit)
				{
					this.SaveAddAss();
				}
				else
				{
					this.SaveEditAss();
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonAssignmentCancel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (Op == Operations.AddAssignment)  // Ако поерацията е била по добавяне зачиства боклука
				{
					this.textBoxContractNumber.Text = "";
					this.numBoxBaseSalary.Text = "";
					this.textBoxSalaryAddon.Text = "";
					this.textBoxClassPercent.Text = "";
					this.dateTimePickerAssignedAt.Text = "";
					this.dateTimePickerContractExpiry.Text = "";
				}
				Op = Operations.ViewPersonData;
				this.ControlEnabled(false, LockButtons.Assignment);
				this.EnableButtons(true, true, false, true, true, false, LockButtons.Assignment);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private float getWorkTimeStaff()
		{
			float staff = -1;
			try
			{
				DataRow R;
				if (this.comboBoxWorkTime.SelectedItem is DataRowView)
				{
					string substitule = this.mainform.nomenclaatureData.dtReasonAssignment.Rows[this.comboBoxAssignReason.SelectedIndex]["substitute"].ToString(); //проверява дали новата му назначение е като заместник
					R = ((DataRowView)this.comboBoxWorkTime.SelectedItem).Row;
					if (float.TryParse(R["staff"].ToString(), out staff) == false)
						return -1;
					if (substitule == "1")
						staff = 0;
				}
				else
				{
					return -1;
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				return -1;
			}
			return staff;
		}

		private int checkForFree(double change)
		{
			try
			{
				string prostitute = this.mainform.nomenclaatureData.dtReasonAssignment.Rows[this.comboBoxAssignReason.SelectedIndex]["substitute"].ToString(); //проверява дали новата му назначение е като заместник
				if (prostitute == "1")
				{
					return 1; //If the new assignemet is as a substitutor - return always true - assignment as substitute is always possible
				}
				else
				{
					string cs;
					mainForm.GetConnString(out cs);
					Entities data = new Entities(cs);
					var rowPosition = data.HR_FirmPersonal3.FirstOrDefault(a => a.id == this.positionID);

					float currentFree, desiredFree;
					if (rowPosition != null)
					{
						double staff = 0;
						double busy = 0;
						double.TryParse(rowPosition.StaffCount, out staff);

						var lstAssigned = data.HR_PersonAssignment.Where(a => a.isActive == 1 && a.positionID == this.positionID).ToList();
						foreach (var ass in lstAssigned)
						{
							if (ass.worktime.Contains("8"))
							{
								busy += 1;
							}
							else if (ass.worktime.Contains("6"))
							{
								busy += 0.75;
							}
							else
							{
								busy += 0.5;
							}
						}

						desiredFree = this.getWorkTimeStaff();
						if (desiredFree < 0)
							return -1;

						double free = staff - busy;

						if (free >= desiredFree)
							return 1;
						else
							return 0;
					}
					else
					{
						//MessageBox.Show("Некоректно избрана длъжност", "Грешка");
						return -1;
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				return -1;
			}
		}

		private void CreateTemporaryContract()
		{
			string connectionString;
			if (mainForm.GetConnString(out connectionString) == false)
				return;
			using (var data = new Entities(connectionString))
			{
				HR_PersonAssignment ass = new HR_PersonAssignment();

				//this.ValidateAssignmentEntity(ass);

			}
		}

		private void SaveAss()
		{
			string cs = "";
			mainForm.GetConnString(out cs);
			var data = new Entities(cs);

			HR_PersonAssignment ass = new HR_PersonAssignment();

			this.ValidateAssignment(ass);
			int checkres;
			bool IsValid = true;

			var person = data.HR_Person.FirstOrDefault(a => a.id == this.parent);
			if (person == null)
			{
				MessageBox.Show("Грешка при определяне на личните данни");
				return;
			}

			checkres = this.checkForFree(0);

			if (checkres == 0)
			{
				if (MessageBox.Show("За съответната длъжност няма свободна щатна бройка. Сигурни ли сте че искате да сключите назначението?", "Няма свободни щатни бройки", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
				{
					Op = Operations.ViewPersonData;
					this.ControlEnabled(false, LockButtons.Assignment);
					this.EnableButtons(true, true, false, true, true, false, LockButtons.Assignment);
					IsAssignmentEdit = false;
					return;
				}
			}
			else if (checkres < 0)
			{
				MessageBox.Show("Грешка при избрана длъжност, работно време или грешка в структурата на организацията. Назначение не може да бъде създадено.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Op = Operations.ViewPersonData;
				this.ControlEnabled(false, LockButtons.Assignment);
				this.EnableButtons(true, true, false, true, true, false, LockButtons.Assignment);
				IsAssignmentEdit = false;
				return;
			}

			ass.exported = 0;
			ass.isActive = 1;
			ass.parentContractId = this.textBoxContractNumber.Text;
			ass.PrevAssignmentID = 0;

			HR_NotesTable notes = new HR_NotesTable();
			notes.date = DateTime.Now;
			notes.Text = "Назначен на " + this.dateTimePickerAssignedAt.Text;
			notes.type = "Назначение";
			notes.par = this.parent;
			data.HR_NotesTable.AddObject(notes);
			data.HR_PersonAssignment.AddObject(ass);

			bool hol = false;
			int total = 0, left = 0;
			float a_day = 0, a_month = 0;
			int month_rest = 0, day_rest = 0;
			day_rest = 30 - this.dateTimePickerAssignedAt.Value.Day;
			month_rest = 12 - this.dateTimePickerAssignedAt.Value.Month;

			hol = int.TryParse(this.numBoxNumHoliday.Text, out total);
			if (hol == false)
			{
				total = 0;
			}
			else
			{
				hol = int.TryParse(this.numBoxAddNumHoliday.Text, out left);
				if (hol)
					total += left;
				left = 0;
			}
			// If total is not null - calculating a month holiday and a day holiday
			/*
			 * отпуск за един ден = (полагаем)/360
				отпуск за месец = (полагаем)/12
				Пропорцианалоно отпуск = (Остатък месеци) * (отпуск за месец) + (остатък дни) * (отпуск за ден)
			 * */

			if (total > 0)
			{
				a_day = (float)total / 360;
				a_month = (float)total / 12;

				if (this.dateTimePickerAssignedAt.Value.Year == DateTime.Now.Year) // Ако служителя е назанчен текущата година се добавя само частичен отпуск
				{
					//Пропорцианалоно отпуск = (Остатък месеци) * (отпуск за месец) + (остатък дни) * (отпуск за ден)
					//if (mainForm.DataBaseTypes == DBTypes.MsSql)
					//	if (day_rest < 0.5)
					//	{
					//		day_rest = 0;
					//	}
					double leftt = month_rest * a_month + day_rest * a_day;

					// Закръгляне
					leftt = Math.Round(leftt);
					left = (int)leftt;
				}
				else
				{//Add the whole number of days
					left = total;
				}
			}
			HR_Year_Holiday yh = new HR_Year_Holiday();

			yh.leftover = left;
			yh.total = total;
			int Yea = 0;
			int.TryParse(this.Year, out Yea);
			yh.year = Yea;
			yh.parent = this.parent;
			data.HR_Year_Holiday.AddObject(yh);

			person.nodeID = this.nodeID;

			try
			{
				data.SaveChanges();
			}
			catch(Exception ex)
			{
				MessageBox.Show("Грешка при назначение", ex.Message);
				return;
			}

			this.RefreshNotesDataSource(false);
			this.RefreshAbsenceDataSource(false);
			this.RefreshAssignmentDataSource(false);

			this.dateTimePickerPostypilNa.Value = this.dateTimePickerAssignedAt.Value;

			CalculatePersonalExperience();

			Op = Operations.ViewPersonData;
			this.ControlEnabled(false, LockButtons.Assignment);
			this.EnableButtons(true, true, false, true, true, false, LockButtons.Assignment);
			this.EnableButtons(true, true, false, true, true, false, LockButtons.Absence);
			this.EnableButtons(true, true, false, true, true, false, LockButtons.Penalty);
			IsAssignmentEdit = false;
		}

		private void SaveAddAss()
		{
			string cs = "";
			mainForm.GetConnString(out cs);
			var data = new Entities(cs);

			HR_PersonAssignment ass = new HR_PersonAssignment();
			var prevAss = data.HR_PersonAssignment.FirstOrDefault(a => a.isActive == 1 && a.parent == this.parent);
			if (prevAss == null)
			{
				MessageBox.Show("Грешка в данните за предходно назначение или предходното назначение не е активно.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			//first parse all data needed for all operations

			DataRow rowH = null; //row for the holliday table
			double staff = 0, oldstaff = 0;
			string substitute, oldsubstitute;
			
			int checkres = 0;
			bool IsValid = true;

			if (prevAss.positionID == null || prevAss.positionID == 0)
			{
				MessageBox.Show("Грешка в данните за предходно назначение.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			staff = this.getWorkTimeStaff();
			oldstaff = (double)prevAss.staff;

			substitute = this.mainform.nomenclaatureData.dtReasonAssignment.Rows[this.comboBoxAssignReason.SelectedIndex]["substitute"].ToString(); //проверява дали новата му назначение е като заместник
			oldsubstitute = prevAss.substitute.ToString(); //проверява дали новата му назначение е като заместник
			if (oldsubstitute != "1" && oldsubstitute != "0")
				oldsubstitute = "0";
			if (substitute != "1" && substitute != "0")
				substitute = "0";

			this.ValidateAssignment(ass);

			if (staff == -1)
			{
				MessageBox.Show("Грешка в данните за щатна бройка предходно назначение.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			#region Structure positions
			//There are three topics that need to be solved here regarding potential position change, potential worktime change and potential substitution change
			//The worktime change takes special effect only if the position remains the same
			//The substitution change takes special effect only if there is no position change
			//if thre is position change everything takes effect
			if (this.positionID == this.oldPositionID && staff == oldstaff)
			{
				//do nothing - no change needed
			}
			else if (this.positionID == this.oldPositionID)
			{
				double staffchange = staff - oldstaff;
				if (staff > oldstaff)
				{
					checkres = this.checkForFree(staff - oldstaff);
					if (checkres == 0)
					{
						if (MessageBox.Show("За съответната длъжност няма свободна щатна бройка. Сигурни ли сте че искате да сключите назначението?", "Няма свободни щатни бройки", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
						{
							Op = Operations.ViewPersonData;
							this.ControlEnabled(false, LockButtons.Assignment);
							this.EnableButtons(true, true, false, true, true, false, LockButtons.Assignment);
							IsAssignmentEdit = false;
							return;
						}
					}
				}
			}
			else
			{
				checkres = this.checkForFree(0);
				if (checkres == 0)
				{
					if (MessageBox.Show("За съответната длъжност няма свободна щатна бройка. Сигурни ли сте че искате да сключите назначението?", "Няма свободни щатни бройки", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
					{
						Op = Operations.ViewPersonData;
						this.ControlEnabled(false, LockButtons.Assignment);
						this.EnableButtons(true, true, false, true, true, false, LockButtons.Assignment);
						IsAssignmentEdit = false;
						return;
					}
				}
				else if (checkres < 0)
				{
					MessageBox.Show("Грешка при избрана длъжност, работно време или грешка в структурата на организацията.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Op = Operations.ViewPersonData;
					this.ControlEnabled(false, LockButtons.Assignment);
					this.EnableButtons(true, true, false, true, true, false, LockButtons.Assignment);
					IsAssignmentEdit = false;
					return;
				}
			}
			#endregion

			ass.exported = 0;
			ass.isActive = 1;
			prevAss.isActive = 0;
			ass.parentContractId = prevAss.parentContractId;
			ass.PrevAssignmentID = prevAss.id;

			var notes = new HR_NotesTable();
			notes.date = DateTime.Now;
			notes.Text = "Сключил допълнително споразумение на " + this.dateTimePickerAssignedAt.Text;
			notes.type = "Споразумение";
			notes.typedocument = this.comboBoxAssignReason.Text;
			notes.par = this.parent;
			data.HR_NotesTable.AddObject(notes);
			data.HR_PersonAssignment.AddObject(ass);

			//Проверява дали таблицата за отпуските съществува
			#region ОТПУСКИ
			int Yea = 0;
			int.TryParse(this.Year, out Yea);
			var yh = data.HR_Year_Holiday.FirstOrDefault(a => a.parent == this.parent && a.year == Yea);
			if (yh == null)
			{
				MessageBox.Show("Липсват данни за отпуски на служителя. Моля проверете дали е направено годишно приключване за предходната година.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				int left = 0, new_total = 0, total = 0;
				float a_day = 0, a_month = 0;
				int month_rest = 0, day_rest = 0;
				day_rest = 30 - DateTime.Now.Day;
				if (day_rest < 0)
				{ //in case the correnction is made on 31-st date
					day_rest = 0;
				}
				month_rest = 12 - DateTime.Now.Month;
				try
				{
					new_total = int.Parse(this.numBoxNumHoliday.Text);
				}
				catch (System.FormatException)
				{
					//MessageBox.Show("Некоректно зададен брой дни отпуск","Грешка при въвеждане");
				}
				try
				{
					new_total += int.Parse(this.numBoxAddNumHoliday.Text);
				}
				catch (System.FormatException)
				{
					//MessageBox.Show("Некоректно зададен брой дни отпуск","Грешка при въвеждане");
				}
				// If total is not null - calculating a month holiday and a day holiday
				/*
				 * отпуск за един ден = (полагаем)/360
					отпуск за месец = (полагаем)/12
					Пропорцианалоно отпуск = (Остатък месеци) * (отпуск за месец) + (остатък дни) * (отпуск за ден)
				 * */

				if (new_total > 0)
				{
					a_day = (float)new_total / 360;
					a_month = (float)new_total / 12;
				}

				if (this.dtYearHoliday.Rows.Count > 0)
				{
					foreach (DataRow rrz in this.dtYearHoliday.Rows)
					{
						if (rrz["year"].ToString() == this.Year)
						{
							rowH = rrz;
							break;
						}
					}

					if (rowH != null)
					{
						if ((int)rowH["Total"] != new_total) //Ако съществува проверява дали ще има някаква корекция върху отпуските
						{
							left = (int)rowH["leftover"];
							total = (int)rowH["total"];

							var change = new_total - total;
							//Пропорцианално отпуск = (Остатък месеци) * (отпуск за месец) + (остатък дни) * (отпуск за ден)
							double leftt = (month_rest * 30 + day_rest * 1) / 365 * change + left;
							// Закръгляне
							leftt = Math.Round(leftt);
							left = (int)leftt;

							yh.leftover = left;
							yh.total = new_total;
							yh.year = Yea;
							yh.parent = this.parent;
						}
					}
					else
					{
						MessageBox.Show("Липсват данни за отпуски на служителя. Моля проверете дали е направено годишно приключване за предходната година.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
				else
				{
					MessageBox.Show("Липсват данни за отпуски на служителя. Моля проверете дали е направено годишно приключване за предходната година.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			#endregion
			var person = ass.HR_Person;
			person.nodeID = this.nodeID;

			this.RefreshAbsenceDataSource(false);
			this.RefreshAssignmentDataSource(false);
			this.RefreshNotesDataSource(false);			
			
			this.dateTimePickerPostypilNa.Value = this.dateTimePickerAssignedAt.Value;
			CalculatePersonalExperience();			

			Op = Operations.ViewPersonData;
			this.ControlEnabled(false, LockButtons.Assignment);
			this.EnableButtons(true, true, false, true, true, false, LockButtons.Assignment);
			this.EnableButtons(true, true, false, true, true, false, LockButtons.Absence);
			this.EnableButtons(true, true, false, true, true, false, LockButtons.Penalty);
			IsAssignmentEdit = false;
		}

		private void SaveEditAss()
		{
			string cs = "";
			mainForm.GetConnString(out cs);
			var data = new Entities(cs);

			//HR_PersonAssignment ass = new HR_PersonAssignment();
			var prevAss = data.HR_PersonAssignment.FirstOrDefault(a => a.isActive == 1 && a.parent == this.parent);
	
			double staff = 0, oldstaff = 0;
			string substitute, oldsubstitute;
			bool tesparse;
			int checkres = 0;
			bool IsValid = true;
			if (prevAss == null)
			{
				MessageBox.Show("Грешка в данните за предходно назначение или предходното назначение не е активно.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			else
			{
				this.oldPositionID = (int)prevAss.positionID;				

				staff = this.getWorkTimeStaff();				

				substitute = this.mainform.nomenclaatureData.dtReasonAssignment.Rows[this.comboBoxAssignReason.SelectedIndex]["substitute"].ToString(); //проверява дали новата му назначение е като заместник
				oldsubstitute = vueAssignment[0]["substitute"].ToString(); //проверява дали новата му назначение е като заместник
				if (oldsubstitute != "1" && oldsubstitute != "0")
					oldsubstitute = "0";
				if (substitute != "1" && substitute != "0")
					substitute = "0";
			}

			this.ValidateAssignment(prevAss);


			if (staff == -1)
			{
				MessageBox.Show("Грешка в данните за щатна бройка предходно назначение.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			#region Structure positions

			//There are three topics that need to be solved here regarding potential position change, potential worktime change and potential substitution change
			//The worktime change takes special effect only if the position remains the same
			//The substitution change takes special effect only if there is no position change
			//if thre is position change everything takes effect
			if (this.positionID == this.oldPositionID && staff == oldstaff)
			{
				//do nothing - no change needed
			}
			else if (this.positionID == this.oldPositionID)
			{
				double staffchange = staff - oldstaff;
				if (staff > oldstaff)
				{
					checkres = this.checkForFree(staff - oldstaff);
					if (checkres == 0)
					{
						if (MessageBox.Show("За съответната длъжност няма свободна щатна бройка. Сигурни ли сте че искате да сключите назначението?", "Няма свободни щатни бройки", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
						{
							Op = Operations.ViewPersonData;
							this.ControlEnabled(false, LockButtons.Assignment);
							this.EnableButtons(true, true, false, true, true, false, LockButtons.Assignment);
							IsAssignmentEdit = false;
							return;
						}
					}
				}
			}
			else
			{
				checkres = this.checkForFree(0);
				if (checkres == 0)
				{
					if (MessageBox.Show("За съответната длъжност няма свободна щатна бройка. Сигурни ли сте че искате да сключите назначението?", "Няма свободни щатни бройки", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
					{
						Op = Operations.ViewPersonData;
						this.ControlEnabled(false, LockButtons.Assignment);
						this.EnableButtons(true, true, false, true, true, false, LockButtons.Assignment);
						IsAssignmentEdit = false;
						return;
					}
				}
				else if (checkres < 0)
				{
					MessageBox.Show("Грешка при избрана длъжност, работно време или грешка в структурата на организацията.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					Op = Operations.ViewPersonData;
					this.ControlEnabled(false, LockButtons.Assignment);
					this.EnableButtons(true, true, false, true, true, false, LockButtons.Assignment);
					IsAssignmentEdit = false;
					return;
				}
			}

			#endregion
			var person = prevAss.HR_Person;
			person.nodeID = this.nodeID;			

			this.dateTimePickerPostypilNa.Value = this.dateTimePickerAssignedAt.Value;
			CalculatePersonalExperience();

			try
			{
				data.SaveChanges();
			}
			catch(Exception ex)
			{
				MessageBox.Show("Грешка при запис на данните.", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Op = Operations.ViewPersonData;
				this.ControlEnabled(false, LockButtons.Assignment);
				this.EnableButtons(true, true, false, true, true, false, LockButtons.Assignment);
				IsAssignmentEdit = false;
				return;
			}

			this.RefreshAssignmentDataSource(false);

			this.Refresh();
			Op = Operations.ViewPersonData;
			this.ControlEnabled(false, LockButtons.Assignment);
			this.EnableButtons(true, true, false, true, true, false, LockButtons.Assignment);
			this.EnableButtons(true, true, false, true, true, false, LockButtons.Absence);
			this.EnableButtons(true, true, false, true, true, false, LockButtons.Penalty);
			IsAssignmentEdit = false;
		}

		private void radioButtonAdditional_CheckedChanged(object sender, System.EventArgs e)
		{
			try
			{
				string cond;
				if (this.radioButtonAdditional.Checked)
				{
					this.buttonAssignment.Text = "   Споразумение";
					this.IsAssignment = false;
					this.tabPageAssignment.Text = "Допълнителни Споразумения";
					this.toolTip1.SetToolTip(this.buttonAssignment, "Сключване на допълнително споразумение");

					cond = "isadditionalassignment = " + (1).ToString(); //За допълнително споразумение

					this.vueAssignment = new DataView(this.dtAssignment, cond, "assignedat", dvrs);
				}
				else
				{
					this.buttonAssignment.Text = "   Назначаване";
					this.IsAssignment = true;
					this.tabPageAssignment.Text = "Назначаване";
					this.toolTip1.SetToolTip(this.buttonAssignment, "Назначаване на длъжност");
					cond = "isadditionalassignment = " + (0).ToString(); //За назначение
					this.vueAssignment = new DataView(this.dtAssignment, cond, "assignedat", dvrs);
				}
				this.dataGridViewAssignment.DataSource = this.vueAssignment;
				for (int i = 0; i < this.vueAssignment.Count; i++)
				{
					if (this.vueAssignment[i]["isactive"].ToString() == "1")
					{
						this.dataGridViewAssignment.CurrentCell = this.dataGridViewAssignment.Rows[i].Cells["position"];
						this.dataGridViewAssignment.Rows[i].Selected = true;
						this.dataGridViewAssignment_Click(this, null);
						break;
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		//private void ValidateAssignmentEntityHR_perso)
		//{

		//	Dict.Add("Parent", this.parent.ToString());

		//	try
		//	{
		//		this.positionID = int.Parse(vuePosition[this.comboBoxPosition.SelectedIndex - 1]["id"].ToString());
		//	}
		//	catch
		//	{
		//		MessageBox.Show("Не може да запишете назначение без да има избрана длъжност");
		//		throw;
		//	}

		//	Dict.Add("PositionID", this.positionID.ToString());

		//	Dict.Add("ModifiedByUser", this.User);

		//	if (this.radioButtonAdditional.Checked)
		//	{
		//		Dict.Add("IsAdditionalAssignment", "1");
		//	}
		//	else
		//	{
		//		Dict.Add("IsAdditionalAssignment", "0");
		//	}

		//	if (this.comboBoxLevel1.SelectedIndex == -1)
		//	{
		//		Dict.Add("Level1", " ");
		//		Dict.Add("Level1Eng", " ");
		//	}
		//	else
		//	{
		//		DataRow row = ((DataRowView)this.comboBoxLevel1.SelectedItem).Row;
		//		Dict.Add("Level1", row["Level"].ToString());
		//		Dict.Add("Level1Eng", row["leveleng"].ToString());
		//	}

		//	if (this.comboBoxLevel2.SelectedIndex == -1)
		//	{
		//		Dict.Add("Level2", " ");
		//		Dict.Add("Level2Eng", " ");
		//	}
		//	else
		//	{
		//		DataRow row = ((DataRowView)this.comboBoxLevel2.SelectedItem).Row;
		//		Dict.Add("Level2", row["Level"].ToString());
		//		Dict.Add("Level2Eng", row["leveleng"].ToString());
		//	}

		//	if (this.comboBoxLevel3.SelectedIndex == -1)
		//	{
		//		Dict.Add("Level3", " ");
		//		Dict.Add("Level3Eng", " ");
		//	}
		//	else
		//	{
		//		DataRow row = ((DataRowView)this.comboBoxLevel3.SelectedItem).Row;
		//		Dict.Add("Level3", row["Level"].ToString());
		//		Dict.Add("Level3Eng", row["leveleng"].ToString());
		//	}

		//	if (this.comboBoxLevel4.SelectedIndex == -1)
		//	{
		//		Dict.Add("Level4Eng", " ");
		//		Dict.Add("Level4", " ");
		//	}
		//	else
		//	{
		//		DataRow row = ((DataRowView)this.comboBoxLevel4.SelectedItem).Row;
		//		Dict.Add("Level4", row["Level"].ToString());
		//		Dict.Add("Level4Eng", row["leveleng"].ToString());
		//	}
		//	if (this.comboBoxPosition.SelectedIndex == -1)
		//	{
		//		Dict.Add("Position", " ");
		//		Dict.Add("PositionEng", " ");
		//	}
		//	else
		//	{
		//		DataRow row = ((DataRowView)this.comboBoxPosition.SelectedItem).Row;
		//		Dict.Add("Position", row["positionname"].ToString());
		//		Dict.Add("PositionEng", row["positioneng"].ToString());
		//	}

		//	if (this.comboBoxContract.SelectedIndex == -1)
		//	{
		//		Dict.Add("Contract", " ");
		//	}
		//	else
		//	{
		//		Dict.Add("Contract", this.comboBoxContract.SelectedItem.ToString());
		//	}

		//	if (this.comboBoxEkdaDegree.SelectedIndex == -1 || this.comboBoxEkdaDegree.SelectedIndex == 0)
		//	{
		//		Dict.Add("ekdapaydegree", "0");
		//	}
		//	else
		//	{
		//		Dict.Add("ekdapaydegree", this.comboBoxEkdaDegree.SelectedItem.ToString());
		//	}

		//	if (this.comboBoxWorkTime.SelectedIndex == -1)
		//	{
		//		Dict.Add("WorkTime", " ");
		//		Dict.Add("Staff", "1");
		//	}
		//	else
		//	{
		//		//DataRow R;
		//		Dict.Add("WorkTime", this.comboBoxWorkTime.Text);
		//		Dict.Add("Staff", "1");
		//		//if (this.comboBoxWorkTime.SelectedItem is DataRowView)
		//		//{
		//		//	R = ((DataRowView)this.comboBoxWorkTime.SelectedItem).Row;
		//		//	if (mainForm.DataBaseTypes == DBTypes.MsSql)
		//		//	{
		//		//		Dict.Add("Staff", R["staff"]);
		//		//	}
		//		//	else
		//		//	{
		//		//		Dict.Add("Staff", R["staff"].ToString());
		//		//	}

		//		//}
		//		//else
		//		//{
		//		//	if (mainForm.DataBaseTypes == DBTypes.MsSql)
		//		//	{
		//		//		Dict.Add("Staff", 1);
		//		//	}
		//		//	else
		//		//	{
		//		//		Dict.Add("Staff", "1");
		//		//	}
		//		//}
		//	}

		//	if (this.comboBoxAssignReason.Text == "")
		//	{
		//		Dict.Add("AssignReason", "");
		//		Dict.Add("Substitute", "0");
		//		Dict.Add("PContractReasonCode", "0");
		//	}
		//	else
		//	{
		//		Dict.Add("AssignReason", this.comboBoxAssignReason.Text);
		//		try
		//		{
		//			string subs = this.mainform.nomenclaatureData.dtReasonAssignment.Rows[this.comboBoxAssignReason.SelectedIndex]["substitute"].ToString();
		//			if (subs != "0" && subs != "1")
		//			{
		//				Dict.Add("Substitute", "0");
		//			}
		//			else
		//			{
		//				Dict.Add("Substitute", subs);
		//			}
		//		}
		//		catch
		//		{
		//			Dict.Add("Substitute", "0");
		//		}
		//		Dict.Add("PContractReasonCode", this.mainform.nomenclaatureData.dtReasonAssignment.Rows[this.comboBoxAssignReason.SelectedIndex]["pcontractreasoncode"].ToString());
		//	}

		//	if (this.comboBoxPosition.SelectedIndex - 1 >= 0)
		//	{
		//		Dict.Add("EKDACode", this.vuePosition[this.comboBoxPosition.SelectedIndex - 1]["EKDACode"].ToString());
		//		Dict.Add("EKDALevel", this.vuePosition[this.comboBoxPosition.SelectedIndex - 1]["EKDALevel"].ToString());
		//		Dict.Add("NKPCode", this.vuePosition[this.comboBoxPosition.SelectedIndex - 1]["NKPCode"].ToString());
		//		Dict.Add("NKPLevel", this.vuePosition[this.comboBoxPosition.SelectedIndex - 1]["NKPLevel"].ToString());
		//		Dict.Add("Rang", this.vuePosition[this.comboBoxPosition.SelectedIndex - 1]["Rang"].ToString());
		//	}

		//	//Dict.Add("AssignedAt", DataAction.ConvertDateTimeToMySql(this.dateTimePickerAssignedAt.Value, mainForm.DataBaseTypes));
		//	//Dict.Add("ParentContractDate", DataAction.ConvertDateTimeToMySql(this.dateTimePickerContractDate.Value, mainForm.DataBaseTypes));
		//	//Dict.Add("ContractExpiry", DataAction.ConvertDateTimeToMySql(dateTimePickerContractExpiry.Value, mainForm.DataBaseTypes));
		//	//Dict.Add("TestContractDate", DataAction.ConvertDateTimeToMySql(dateTimePickerTestPeriod.Value, mainForm.DataBaseTypes));
		//	Dict.Add("AssignedAt", this.dateTimePickerAssignedAt.Value);
		//	Dict.Add("ParentContractDate", this.dateTimePickerContractDate.Value);
		//	Dict.Add("ContractExpiry", dateTimePickerContractExpiry.Value);
		//	Dict.Add("TestContractDate", dateTimePickerTestPeriod.Value);
		//	Dict.Add("ContractNumber", this.textBoxContractNumber.Text);
		//	Dict.Add("BaseSalary", this.numBoxBaseSalary.Text);
		//	Dict.Add("SalaryAddon", this.textBoxSalaryAddon.Text);
		//	Dict.Add("ClassPercent", this.textBoxClassPercent.Text);
		//	Dict.Add("MonthlyAddon", this.numBoxMonthlyAddon.Text);
		//	if (this.numBoxNumHoliday.Text == "")
		//	{
		//		Dict.Add("NumHoliday", "0");
		//	}
		//	else
		//	{
		//		Dict.Add("NumHoliday", this.numBoxNumHoliday.Text);
		//	}

		//	if (this.numBoxAddNumHoliday.Text == "")
		//	{
		//		Dict.Add("AdditionalHoliday", "0");
		//	}
		//	else
		//	{
		//		Dict.Add("AdditionalHoliday", this.numBoxAddNumHoliday.Text);
		//	}

		//	if (this.numBoxAssignmentExpY.Text == "")
		//	{
		//		Dict.Add("Years", "0");
		//	}
		//	else
		//	{
		//		try
		//		{
		//			Dict.Add("Years", this.numBoxAssignmentExpY.Text);
		//		}
		//		catch (System.Exception)
		//		{
		//			Dict.Add("Years", "0");
		//		}
		//	}
		//	if (this.numBoxAssignmentExtM.Text == "")
		//	{
		//		Dict.Add("Months", "0");
		//	}
		//	else
		//	{
		//		try
		//		{
		//			Dict.Add("Months", this.numBoxAssignmentExtM.Text);
		//		}
		//		catch (System.Exception)
		//		{
		//			Dict.Add("Months", "0");
		//		}
		//	}

		//	if (this.numBoxAssignmentExpD.Text == "")
		//	{
		//		Dict.Add("Days", "0");
		//	}
		//	else
		//	{
		//		try
		//		{
		//			Dict.Add("Days", this.numBoxAssignmentExpD.Text);
		//		}
		//		catch (System.Exception)
		//		{
		//			Dict.Add("Days", "0");
		//		}
		//	}

		//	if (this.comboBoxLaw.SelectedIndex == -1)
		//	{
		//		Dict.Add("Law", "");
		//	}
		//	else
		//	{
		//		Dict.Add("Law", this.comboBoxLaw.Text);
		//	}

		//	Dict.Add("YearlyAddon", this.comboBoxYearlyAddon.Text);

		//	Dict.Add("TutorName", this.comboBoxTutorName.Text);
		//	Dict.Add("TutorAbsenceReason", this.comboBoxTutorAbsenceReason.Text);

		//}

		private void ValidateAssignment(HR_PersonAssignment ass)
		{
			ass.parent = this.parent;
			try
			{
				ass.positionID = int.Parse(vuePosition[this.comboBoxPosition.SelectedIndex - 1]["id"].ToString());
			}
			catch
			{
				MessageBox.Show("Не може да запишете назначение без да има избрана длъжност");
				throw;
			}
			ass.modifiedByUser = this.User;


			if (this.radioButtonAdditional.Checked)
			{
				ass.IsAdditionalAssignment = 1;
			}
			else
			{
				ass.IsAdditionalAssignment = 0;
			}

			if (this.comboBoxLevel1.SelectedIndex == -1)
			{
				MessageBox.Show("Не може да запишете назначение без да има избрана структура");
				throw new Exception();
			}
			else
			{
				DataRow row = ((DataRowView)this.comboBoxLevel1.SelectedItem).Row;
				ass.level1 = row["Level"].ToString();
				ass.level1eng = row["leveleng"].ToString();
			}

			if (this.comboBoxLevel2.SelectedIndex != -1)
			{
				DataRow row = ((DataRowView)this.comboBoxLevel2.SelectedItem).Row;
				ass.level2 = row["Level"].ToString();
				ass.level2eng = row["leveleng"].ToString();
			}

			if (this.comboBoxLevel3.SelectedIndex != -1)
			{
				DataRow row = ((DataRowView)this.comboBoxLevel3.SelectedItem).Row;
				ass.level3 = row["Level"].ToString();
				ass.level3eng = row["leveleng"].ToString();
			}

			if (this.comboBoxLevel4.SelectedIndex != -1)
			{
				DataRow row = ((DataRowView)this.comboBoxLevel4.SelectedItem).Row;
				ass.level4 = row["Level"].ToString();
				ass.level4eng = row["leveleng"].ToString();
			}

			if (this.comboBoxPosition.SelectedIndex == -1)
			{
				DataRow row = ((DataRowView)this.comboBoxPosition.SelectedItem).Row;
				ass.position = row["positionname"].ToString();
				ass.positioneng = row["positioneng"].ToString();
			}

			if (this.comboBoxContract.SelectedIndex != -1)
			{
				ass.contract = this.comboBoxContract.SelectedItem.ToString();
			}

			if (this.comboBoxEkdaDegree.SelectedIndex == -1 || this.comboBoxEkdaDegree.SelectedIndex == 0)
			{
				ass.ekdaPayDegree = 0;
			}
			else
			{
				int pd = 0;
				int.TryParse(this.comboBoxEkdaDegree.SelectedItem.ToString(), out pd);
				ass.ekdaPayDegree = pd;
			}

			if (this.comboBoxWorkTime.SelectedIndex == -1)
			{
				ass.staff = 1;
				ass.worktime = "";
			}
			else
			{
				DataRow row = ((DataRowView)this.comboBoxWorkTime.SelectedItem).Row;
				ass.worktime = row["level"].ToString();
				double sta = 0;
				double.TryParse(row["staff"].ToString(), out sta);
				ass.staff = sta;
			}

			if (this.comboBoxAssignReason.Text == "")
			{
				ass.assignReason = "";
				ass.substitute = 0;
				ass.pcontractreasoncode = "0";
			}
			else
			{
				ass.assignReason = this.comboBoxAssignReason.Text;
				try
				{
					string subs = this.mainform.nomenclaatureData.dtReasonAssignment.Rows[this.comboBoxAssignReason.SelectedIndex]["substitute"].ToString();

					if (subs != "0" && subs != "1")
					{
						ass.substitute = 0;
					}
					else
					{
						int sus = 0;
						int.TryParse(subs, out sus);
						ass.substitute = sus;
					}
				}
				catch
				{
					ass.substitute = 0;
				}
				ass.pcontractreasoncode = this.mainform.nomenclaatureData.dtReasonAssignment.Rows[this.comboBoxAssignReason.SelectedIndex]["pcontractreasoncode"].ToString();
			}

			if (this.comboBoxPosition.SelectedIndex - 1 >= 0)
			{
				ass.EKDACode = this.vuePosition[this.comboBoxPosition.SelectedIndex - 1]["EKDACode"].ToString();
				ass.EKDALevel = this.vuePosition[this.comboBoxPosition.SelectedIndex - 1]["EKDALevel"].ToString();
				ass.nkpCode = this.vuePosition[this.comboBoxPosition.SelectedIndex - 1]["NKPCode"].ToString();
				ass.nkpLevel = this.vuePosition[this.comboBoxPosition.SelectedIndex - 1]["NKPLevel"].ToString();
				ass.Rang = this.vuePosition[this.comboBoxPosition.SelectedIndex - 1]["Rang"].ToString();
			}

			ass.assignedAt = this.dateTimePickerAssignedAt.Value;
			ass.ParentContractDate = this.dateTimePickerContractDate.Value;
			ass.contractExpiry = dateTimePickerContractExpiry.Value;
			ass.TestContractDate = dateTimePickerTestPeriod.Value;
			ass.contractNumber = this.textBoxContractNumber.Text;
			double bs = 0;
			double.TryParse(this.numBoxBaseSalary.Text, out bs);
			ass.baseSalary = bs;
			ass.salaryAddon = this.textBoxSalaryAddon.Text;
			ass.classPercent = this.textBoxClassPercent.Text;
			ass.MonthlyAddon = this.numBoxMonthlyAddon.Text;

			ass.NumHoliday = this.numBoxNumHoliday.Text;
			int anh = 0;
			int.TryParse(this.numBoxAddNumHoliday.Text, out anh);
			ass.AdditionalHoliday = anh;
			int ye = 0, mo = 0, day = 0;
			int.TryParse(this.numBoxAssignmentExpY.Text, out ye);
			int.TryParse(this.numBoxAssignmentExtM.Text, out mo);
			int.TryParse(this.numBoxAssignmentExpD.Text, out day);
			ass.years = ye;
			ass.months = mo;
			ass.days = day;
			ass.law = this.comboBoxLaw.Text;
			ass.YearlyAddon = this.comboBoxYearlyAddon.Text;
			ass.tutorname = this.comboBoxTutorName.Text;
			ass.tutorabsencereason = this.comboBoxTutorAbsenceReason.Text;
		}

		private void RefreshAssignmentDataSource(bool IsFormLoad)
		{
			try
			{
				this.dtAssignment = this.dataAdapter.SelectWhere(TableNames.PersonAssignment, "*", "WHERE parent = " + this.parent + " order by assignedat");
				DataTable positions = this.dataAdapter.SelectWhere(TableNames.FirmPersonal3, "*", " ORDER BY id");

				if (this.dtAssignment == null || positions == null)
				{
					MessageBox.Show("Грешка при зареждане на таблицата за назначенията", ErrorMessages.NoConnection);
					this.Close();
				}
				positions.PrimaryKey = new DataColumn[] { positions.Columns["ID"] };
				this.dtAssignment.PrimaryKey = new DataColumn[] { this.dtAssignment.Columns["ID"] };

				if (dtAssignment.Rows.Count > 1) //if we have an additional assignment
				{
					this.radioButtonAdditional.Checked = true;
				}
				string cond;
				if (this.radioButtonAdditional.Checked)
				{
					this.IsAssignment = false;
					this.tabPageAssignment.Text = "Допълнителни Споразумения";
					cond = "isadditionalassignment = " + (1).ToString(); //За допълнително споразумение

					this.vueAssignment = new DataView(this.dtAssignment, cond, "assignedat", dvrs);
				}
				else
				{
					this.IsAssignment = true;
					this.tabPageAssignment.Text = "Назначаване";
					cond = "isadditionalassignment = " + (0).ToString(); //За назначение
					this.vueAssignment = new DataView(this.dtAssignment, cond, "assignedat", dvrs);
				}

				TabPage tab;
				tab = this.tabControlCardNew.SelectedTab;
				this.tabControlCardNew.SelectedTab = this.tabControlCardNew.TabPages["TabPageAssignment"];
				if (this.tabControlCardNew.SelectedTab != null)
				{
					this.dataGridViewAssignment.DataSource = this.vueAssignment;
					this.dtAssignment.TableName = TableNames.PersonAssignment;
					this.dataGridViewAssignment.ClearSelection();

					this.JustifyGridView(this.dataGridViewAssignment, TableNames.Compare(TableNames.PersonAssignment));

					if (this.dtAssignment.Rows.Count > 0)
					{
						for (int i = 0; i < this.dtAssignment.Rows.Count; i++)
						{
							if (this.dtAssignment.Rows[i]["IsActive"].ToString() == "1")
							{
								try
								{
									DataRow rowposition = positions.Rows.Find(this.dtAssignment.Rows[i]["positionID"]);
									if (rowposition != null)
									{//loadning the characteristic here
										this.textBoxNKPCode2.Text = rowposition["NKPCode"].ToString();
										this.textBoxNKPClass.Text = rowposition["NKPClass"].ToString();
										this.textBoxBasicDuties.Text = rowposition["BasicDuties"].ToString();
										this.textBoxBasicResponsibilities.Text = rowposition["BasicResponsibilities"].ToString();
										this.textBoxCompetence.Text = rowposition["Competence"].ToString();
										this.textBoxConnections.Text = rowposition["Connections"].ToString();
										this.textBoxRequirements.Text = rowposition["Requirements"].ToString();
									}
								}
								catch (System.Exception exc)
								{
									MessageBox.Show(exc.Message);
								}
								try
								{
									this.oldPositionID = this.positionID = int.Parse(this.dtAssignment.Rows[i]["PositionID"].ToString());
								}
								catch (System.Exception)
								{
									this.positionID = this.oldPositionID = 0;
								}
								break;
							}
						}
					}
					this.radioButtonAdditional_CheckedChanged(this, new EventArgs());
				}
				this.tabControlCardNew.SelectedTab = tab;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void dataGridViewAssignment_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.dataGridViewAssignment.CurrentRow == null)
					return;
				this.dataGridViewAssignment.CurrentRow.Selected = true;

				int index = this.comboBoxLevel1.FindString(this.dataGridViewAssignment.CurrentRow.Cells["level1"].Value.ToString());
				this.SetComboIndex(this.comboBoxLevel1, index);

				index = this.comboBoxLevel2.FindString(this.dataGridViewAssignment.CurrentRow.Cells["level2"].Value.ToString());
				this.SetComboIndex(this.comboBoxLevel2, index);

				index = this.comboBoxLevel3.FindString(this.dataGridViewAssignment.CurrentRow.Cells["level3"].Value.ToString());
				this.SetComboIndex(this.comboBoxLevel3, index);

				index = this.comboBoxLevel4.FindString(this.dataGridViewAssignment.CurrentRow.Cells["level4"].Value.ToString());
				this.SetComboIndex(this.comboBoxLevel4, index);


				//Here we search for the id (which is recorded when the combobox datasource is filled)
				//and assign the corresponding index
				DataRow Rowp = this.dtComboPosiiton.Rows.Find(this.dataGridViewAssignment.CurrentRow.Cells["positionID"].Value);

				if (Rowp == null)
				{
					if (this.comboBoxPosition.Items.Count > 0)
					{
						this.SetComboIndex(this.comboBoxPosition, 0);
					}
				}
				else
				{
					try
					{
						index = int.Parse(Rowp["Index"].ToString());
					}
					catch
					{
						index = 0;
					}
					this.SetComboIndex(this.comboBoxPosition, index);
				}

				index = this.comboBoxContract.FindString(this.dataGridViewAssignment.CurrentRow.Cells["contract"].Value.ToString());
				this.SetComboIndex(this.comboBoxContract, index);

				index = this.comboBoxWorkTime.FindString(this.dataGridViewAssignment.CurrentRow.Cells["worktime"].Value.ToString());
				this.SetComboIndex(this.comboBoxWorkTime, index);

				index = this.comboBoxAssignReason.FindString(this.dataGridViewAssignment.CurrentRow.Cells["assignreason"].Value.ToString());
				this.SetComboIndex(this.comboBoxAssignReason, index);

				index = this.comboBoxLaw.FindString(this.dataGridViewAssignment.CurrentRow.Cells["law"].Value.ToString());
				this.SetComboIndex(this.comboBoxLaw, index);

				index = this.comboBoxEkdaDegree.FindString(this.dataGridViewAssignment.CurrentRow.Cells["ekdaPayDegree"].Value.ToString());
				this.SetComboIndex(this.comboBoxEkdaDegree, index);

				this.comboBoxYearlyAddon.Text = this.dataGridViewAssignment.CurrentRow.Cells["yearlyaddon"].Value.ToString();
				this.textBoxContractNumber.Text = this.dataGridViewAssignment.CurrentRow.Cells["contractnumber"].Value.ToString();
				this.textBoxClassPercent.Text = this.dataGridViewAssignment.CurrentRow.Cells["classpercent"].Value.ToString();
				this.numBoxAssignmentExpD.Text = this.dataGridViewAssignment.CurrentRow.Cells["days"].Value.ToString();
				this.numBoxAssignmentExpY.Text = this.dataGridViewAssignment.CurrentRow.Cells["years"].Value.ToString();
				this.numBoxAssignmentExtM.Text = this.dataGridViewAssignment.CurrentRow.Cells["months"].Value.ToString();
				this.numBoxBaseSalary.Text = this.dataGridViewAssignment.CurrentRow.Cells["basesalary"].Value.ToString();
				this.numBoxNumHoliday.Text = this.dataGridViewAssignment.CurrentRow.Cells["numholiday"].Value.ToString();
				this.numBoxAddNumHoliday.Text = this.dataGridViewAssignment.CurrentRow.Cells["additionalHoliday"].Value.ToString();
				this.numBoxMonthlyAddon.Text = this.dataGridViewAssignment.CurrentRow.Cells["monthlyaddon"].Value.ToString();
				this.textBoxSalaryAddon.Text = this.dataGridViewAssignment.CurrentRow.Cells["salaryaddon"].Value.ToString();

				this.comboBoxTutorAbsenceReason.Text = this.dataGridViewAssignment.CurrentRow.Cells["TutorAbsenceReason"].Value.ToString();

				if (this.dataGridViewAssignment.CurrentRow.Cells["TutorName"].Value.ToString() == "")
				{
					ArrayList TempArr = new ArrayList();
					TempArr.Add("");
					this.comboBoxTutorName.DataSource = TempArr;
				}
				this.comboBoxTutorName.Text = this.dataGridViewAssignment.CurrentRow.Cells["TutorName"].Value.ToString();

				try
				{
					this.dateTimePickerAssignedAt.Value = (DateTime)this.dataGridViewAssignment.CurrentRow.Cells["AssignedAt"].Value;
				}
				catch (Exception)
				{
					//ErrorLog.WriteException(ex, ex.Message);
					//MessageBox.Show(ex.Message);
				}

				try
				{
					this.dateTimePickerTestPeriod.Value = (DateTime)this.dataGridViewAssignment.CurrentRow.Cells["TestContractDate"].Value;
				}
				catch (Exception)
				{
					//ErrorLog.WriteException(ex, ex.Message);
					//MessageBox.Show(ex.Message);
				}

				try
				{
					this.dateTimePickerContractDate.Value = (DateTime)this.dataGridViewAssignment.CurrentRow.Cells["ParentContractDate"].Value;
				}
				catch (Exception)
				{
					//ErrorLog.WriteException(ex, ex.Message);
					//MessageBox.Show(ex.Message);
				}

				try
				{
					this.dateTimePickerContractExpiry.Value = (DateTime)this.dataGridViewAssignment.CurrentRow.Cells["ContractExpiry"].Value;
				}
				catch (Exception)
				{
					//ErrorLog.WriteException(ex, ex.Message);
					//MessageBox.Show(ex.Message);
				}


			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void CalcExperience()
		{
			try
			{
				if (this.dtAssignment.Rows.Count > 0)
				{   //Трудов стаж					
					DateTime AssignDate = Convert.ToDateTime(this.dtAssignment.Rows[0]["AssignedAt"]);
					//int years = (int)this.dtAssignment.Rows[0]["Years"];
					if (DateTime.Compare(this.dateTimePickerAssignedAt.Value, AssignDate) >= 0)
					{
						int AssY, AssM, AssD, CYear, CDay, CMonth, TY, TM, TD;

						AssY = AssignDate.Year;
						AssM = AssignDate.Month;
						AssD = AssignDate.Day;
						CYear = this.dateTimePickerAssignedAt.Value.Year - AssY;
						if ((CMonth = this.dateTimePickerAssignedAt.Value.Month - AssM) < 0)
						{
							CYear--;
							CMonth += 12;
						}
						if ((CDay = this.dateTimePickerAssignedAt.Value.Day - AssD) <= 0)
						{
							CDay += 30;
							CMonth--;
							if (CMonth < 0)
							{
								CMonth += 12;
								CYear--;
							}
						}
						TY = TM = TD = 0;
						try
						{

							TY = CYear + (int)this.dtAssignment.Rows[0]["Years"];
							TM = CMonth + (int)this.dtAssignment.Rows[0]["Months"];
							TD = CDay + (int)this.dtAssignment.Rows[0]["Days"];
						}
						catch
						{
						}
						if (TD >= 30)
						{
							TM++;
							TD -= 30;
						}
						if (TM >= 12)
						{
							TM -= 12;
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
						MessageBox.Show("Моля проверете дали датата на компютъра е вярна");
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void TreeLoad()
		{
			try
			{
				this.dvrs = DataViewRowState.CurrentRows;
				this.vueAdministration = new DataView(dtTree, "par = 0", "level", dvrs);

				dtLevel1.Clear();
				DataRow rl = dtLevel1.NewRow();
				rl["level"] = "";
				rl["leveleng"] = "";
				dtLevel1.Rows.Add(rl);

				for (int i = 0; i < vueAdministration.Count; i++)
				{
					rl = this.dtLevel1.NewRow();
					rl["level"] = vueAdministration[i]["level"];
					rl["leveleng"] = vueAdministration[i]["leveleng"];
					dtLevel1.Rows.Add(rl);
				}

				this.comboBoxLevel1.DataSource = dtLevel1;
				this.comboBoxLevel1.DisplayMember = "level";
				//			object sender = new object();
				//			System.EventArgs e = new System.EventArgs();
				//			comboBoxLevel1_SelectedIndexChanged(sender, e );
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void comboBoxLevel1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				string cond;
				this.dtLevel2.Clear();
				this.dtLevel3.Clear();
				this.dtLevel4.Clear();

				this.comboBoxLevel2.Text = "";
				this.comboBoxLevel3.Text = "";
				this.comboBoxLevel4.Text = "";

				if (this.comboBoxLevel1.SelectedIndex > 0)
				{
					cond = "par = " + this.vueAdministration[this.comboBoxLevel1.SelectedIndex - 1]["id"].ToString();

					this.vueDirection = new DataView(dtTree, cond, "level", dvrs);

					DataRow rl = this.dtLevel2.NewRow();
					rl["level"] = "";
					rl["leveleng"] = "";
					this.dtLevel2.Rows.Add(rl);

					for (int i = 0; i < this.vueDirection.Count; i++)
					{
						rl = dtLevel2.NewRow();
						rl["level"] = this.vueDirection[i]["level"];
						rl["leveleng"] = this.vueDirection[i]["leveleng"];
						this.dtLevel2.Rows.Add(rl);
					}
					this.comboBoxLevel2.DataSource = this.dtLevel2;
					this.comboBoxLevel2.DisplayMember = "level";

					this.vuePosition = new DataView(dtPosition, cond, "id", dvrs);

					this.dtComboPosiiton.Rows.Clear();
					DataRow rowe = this.dtComboPosiiton.NewRow(); //the next four rows are added in order to 
					rowe["PositionName"] = ""; //assure that there will be one blank field in ComboBoxPosition
					rowe["PositionCode"] = 0;
					rowe["Positioneng"] = "";
					rowe["Index"] = 0;
					this.dtComboPosiiton.Rows.Add(rowe);
					for (int i = 0; i < this.vuePosition.Count; i++)
					{
						DataRow rowp = this.dtComboPosiiton.NewRow();
						rowp["PositionName"] = this.vuePosition[i]["nameOfPosition"];
						rowp["PositionCode"] = this.vuePosition[i]["id"];
						rowp["positioneng"] = this.vuePosition[i]["positioneng"];
						rowp["Index"] = i + 1; //The counter starts from 0, but we must add one because of the leading
											   //empty row
						this.dtComboPosiiton.Rows.Add(rowp);
					}
					this.comboBoxPosition.DataSource = this.dtComboPosiiton;
					this.comboBoxPosition.DisplayMember = "PositionName";
					this.nodeID = (int)this.vueAdministration[this.comboBoxLevel1.SelectedIndex - 1]["id"];
				}
				else
				{
					this.nodeID = 0;
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void comboBoxLevel2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				string cond;
				this.dtLevel3.Clear();
				this.dtLevel4.Clear();
				this.comboBoxLevel3.Text = "";
				this.comboBoxLevel4.Text = "";

				if (this.comboBoxLevel2.SelectedIndex > 0)
				{
					cond = "par = " + this.vueDirection[this.comboBoxLevel2.SelectedIndex - 1]["id"].ToString();

					vueDepartment = new DataView(dtTree, cond, "level", dvrs);
					DataRow rl = this.dtLevel3.NewRow();
					rl["level"] = "";
					rl["leveleng"] = "";
					this.dtLevel3.Rows.Add(rl);

					for (int i = 0; i < this.vueDepartment.Count; i++)
					{
						rl = this.dtLevel3.NewRow();
						rl["level"] = vueDepartment[i]["level"];
						rl["leveleng"] = vueDepartment[i]["leveleng"];
						this.dtLevel3.Rows.Add(rl);
					}
					this.comboBoxLevel3.DataSource = this.dtLevel3;
					this.comboBoxLevel3.DisplayMember = "level";

					this.nodeID = (int)this.vueDirection[this.comboBoxLevel2.SelectedIndex - 1]["id"];
				}
				else if (this.comboBoxLevel1.SelectedIndex > 0)
				{
					this.nodeID = (int)this.vueAdministration[this.comboBoxLevel1.SelectedIndex - 1]["id"];
				}
				else
				{
					this.nodeID = 0;
				}
				cond = "par = " + this.nodeID;
				vuePosition = new DataView(dtPosition, cond, "id", dvrs);

				this.dtComboPosiiton.Rows.Clear();
				DataRow rowe = this.dtComboPosiiton.NewRow(); //the next four rows are added in order to 
				rowe["PositionName"] = ""; //assure that there will be one blank field in ComboBoxPosition
				rowe["PositionCode"] = 0;
				rowe["Positioneng"] = "";
				rowe["Index"] = 0;
				this.dtComboPosiiton.Rows.Add(rowe);
				for (int i = 0; i < this.vuePosition.Count; i++)
				{
					DataRow rowp = this.dtComboPosiiton.NewRow();
					rowp["PositionName"] = vuePosition[i]["nameOfPosition"];
					rowp["PositionCode"] = vuePosition[i]["id"];
					rowp["PositionEng"] = vuePosition[i]["positioneng"];
					rowp["Index"] = i + 1; //The counter starts from 0, but we must add one because of the leading
										   //empty row
					this.dtComboPosiiton.Rows.Add(rowp);
				}
				this.comboBoxPosition.DataSource = this.dtComboPosiiton;
				this.comboBoxPosition.DisplayMember = "PositionName";
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void comboBoxLevel3_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				string cond;
				this.dtLevel4.Clear();
				this.comboBoxLevel4.Text = "";

				if (this.comboBoxLevel3.SelectedIndex > 0)
				{
					cond = "par = " + this.vueDepartment[this.comboBoxLevel3.SelectedIndex - 1]["id"].ToString();

					vueSector = new DataView(dtTree, cond, "level", dvrs);
					DataRow rl = this.dtLevel4.NewRow();
					rl["level"] = "";
					rl["leveleng"] = "";
					this.dtLevel4.Rows.Add(rl);

					for (int i = 0; i < this.vueSector.Count; i++)
					{
						rl = this.dtLevel4.NewRow();
						rl["level"] = this.vueSector[i]["level"];
						rl["leveleng"] = this.vueSector[i]["leveleng"];
						this.dtLevel4.Rows.Add(rl);
					}
					this.comboBoxLevel4.DataSource = this.dtLevel4;
					this.comboBoxLevel4.DisplayMember = "level";
					this.nodeID = (int)this.vueDepartment[this.comboBoxLevel3.SelectedIndex - 1]["id"];
				}
				else if (this.comboBoxLevel2.SelectedIndex > 0)
				{
					this.nodeID = (int)this.vueDirection[this.comboBoxLevel2.SelectedIndex - 1]["id"];
				}
				else if (this.comboBoxLevel1.SelectedIndex > 0)
				{
					this.nodeID = (int)this.vueAdministration[this.comboBoxLevel1.SelectedIndex - 1]["id"];
				}
				else
				{
					this.nodeID = 0;
				}
				cond = "par = " + this.nodeID;
				vuePosition = new DataView(dtPosition, cond, "id", dvrs);

				this.dtComboPosiiton.Rows.Clear();
				DataRow rowe = this.dtComboPosiiton.NewRow(); //the next four rows are added in order to 
				rowe["PositionName"] = ""; //assure that there will be one blank field in ComboBoxPosition
				rowe["PositionCode"] = 0;
				rowe["positioneng"] = "";
				rowe["Index"] = 0;
				this.dtComboPosiiton.Rows.Add(rowe);
				for (int i = 0; i < this.vuePosition.Count; i++)
				{
					DataRow rowp = this.dtComboPosiiton.NewRow();
					rowp["PositionName"] = vuePosition[i]["nameOfPosition"];
					rowp["PositionCode"] = vuePosition[i]["id"];
					rowp["Positioneng"] = vuePosition[i]["positioneng"];
					rowp["Index"] = i + 1; //The counter starts from 0, but we must add one because of the leading
										   //empty row
					this.dtComboPosiiton.Rows.Add(rowp);
				}
				this.comboBoxPosition.DataSource = this.dtComboPosiiton;
				this.comboBoxPosition.DisplayMember = "PositionName";
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void comboBoxPosition_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if (this.comboBoxPosition.SelectedIndex > 0)
				{
					try
					{
						this.positionID = int.Parse(vuePosition[this.comboBoxPosition.SelectedIndex - 1]["id"].ToString());
					}
					catch
					{
						this.positionID = 0;
					}
					this.textBoxNKPLevel.Text = vuePosition[this.comboBoxPosition.SelectedIndex - 1]["nkplevel"].ToString();
					this.textBoxNKPCode.Text = vuePosition[this.comboBoxPosition.SelectedIndex - 1]["nkpcode"].ToString();
				}
				else
				{
					this.textBoxNKPCode.Text = this.textBoxNKPLevel.Text = "";
					this.positionID = 0;
				}
				this.comboBoxAssignReason_SelectedIndexChanged(sender, e);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void comboBoxLevel4_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				string cond;

				if (this.comboBoxLevel4.SelectedIndex > 0)
				{
					this.nodeID = (int)this.vueSector[this.comboBoxLevel4.SelectedIndex - 1]["id"];
				}
				else if (this.comboBoxLevel3.SelectedIndex > 0)
				{
					this.nodeID = (int)this.vueDepartment[this.comboBoxLevel3.SelectedIndex - 1]["id"];
				}
				else if (this.comboBoxLevel2.SelectedIndex > 0)
				{
					this.nodeID = (int)this.vueDirection[this.comboBoxLevel2.SelectedIndex - 1]["id"];
				}
				else if (this.comboBoxLevel1.SelectedIndex > 0)
				{
					this.nodeID = (int)this.vueAdministration[this.comboBoxLevel1.SelectedIndex - 1]["id"];
				}
				else
				{
					this.nodeID = 0;
				}
				cond = "par = " + this.nodeID;
				vuePosition = new DataView(dtPosition, cond, "id", dvrs);

				this.dtComboPosiiton.Rows.Clear();
				DataRow rowe = this.dtComboPosiiton.NewRow(); //the next four rows are added in order to 
				rowe["PositionName"] = ""; //assure that there will be one blank field in ComboBoxPosition
				rowe["PositionCode"] = 0;
				rowe["positioneng"] = "";
				rowe["Index"] = 0;
				this.dtComboPosiiton.Rows.Add(rowe);
				for (int i = 0; i < this.vuePosition.Count; i++)
				{
					DataRow rowp = this.dtComboPosiiton.NewRow();
					rowp["PositionName"] = vuePosition[i]["nameOfPosition"];
					rowp["PositionCode"] = vuePosition[i]["id"];
					rowp["positioneng"] = vuePosition[i]["positioneng"];
					rowp["Index"] = i + 1; //The counter starts from 0, but we must add one because of the leading
										   //empty row
					this.dtComboPosiiton.Rows.Add(rowp);
				}
				this.comboBoxPosition.DataSource = this.dtComboPosiiton;
				this.comboBoxPosition.DisplayMember = "PositionName";
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void numBoxAssignmentExpY_TextChanged(object sender, System.EventArgs e)
		{
			try
			{
				if (Op == Operations.AddAssignment)
				{
					if (mainform.nomenclaatureData.dtOptions.Rows[0]["classcoef"].ToString() != "0" && this.comboBoxLaw.Text.ToLower() == "трудово")
					{
						float assexpy;
						try
						{
							assexpy = float.Parse(this.numBoxAssignmentExpY.Text);
						}
						catch
						{
							assexpy = 0;
						}
						try
						{
							assexpy = assexpy * float.Parse(this.mainform.nomenclaatureData.dtOptions.Rows[0]["classcoef"].ToString());
						}
						catch
						{
							assexpy = 0;
						}
						if (assexpy >= 1)
						{
							this.textBoxClassPercent.Text = assexpy.ToString();
						}
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void comboBoxContract_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if (Op == Operations.AddAssignment)
				{
					switch (this.comboBoxContract.SelectedIndex)
					{
						case 0:
							this.dateTimePickerContractExpiry.Enabled = false;
							this.dateTimePickerTestPeriod.Enabled = false;
							this.dateTimePickerContractExpiry.Value = this.dateTimePickerContractExpiry.MinDate;
							this.dateTimePickerTestPeriod.Value = this.dateTimePickerTestPeriod.MinDate;
							break;
						case 1:
							this.dateTimePickerContractExpiry.Enabled = false;
							this.dateTimePickerContractExpiry.Value = this.dateTimePickerContractExpiry.MinDate;
							this.dateTimePickerTestPeriod.Enabled = true;
							this.dateTimePickerTestPeriod.Value = this.dateTimePickerAssignedAt.Value.AddMonths(6);
							break;
						case 2:
							this.dateTimePickerContractExpiry.Enabled = true;
							this.dateTimePickerContractExpiry.Value = this.dateTimePickerAssignedAt.Value.AddMonths(6);
							this.dateTimePickerTestPeriod.Enabled = false;
							this.dateTimePickerTestPeriod.Value = this.dateTimePickerTestPeriod.MinDate;
							break;
						case 3:
							this.dateTimePickerContractExpiry.Enabled = true;
							this.dateTimePickerContractExpiry.Value = this.dateTimePickerAssignedAt.Value.AddMonths(6);
							this.dateTimePickerTestPeriod.Enabled = true;
							this.dateTimePickerTestPeriod.Value = this.dateTimePickerAssignedAt.Value.AddMonths(6);
							break;
						default:
							this.dateTimePickerContractExpiry.Enabled = false;
							this.dateTimePickerContractExpiry.Value = this.dateTimePickerContractExpiry.MinDate;
							this.dateTimePickerTestPeriod.Enabled = false;
							this.dateTimePickerTestPeriod.Value = this.dateTimePickerTestPeriod.MinDate;
							break;
					}
				}
				else if (this.Op == Operations.EditAssignment)
				{
					switch (this.comboBoxContract.SelectedIndex)
					{
						case 0:
							this.dateTimePickerContractExpiry.Enabled = false;
							this.dateTimePickerTestPeriod.Enabled = false;
							break;
						case 1:
							this.dateTimePickerContractExpiry.Enabled = false;
							this.dateTimePickerTestPeriod.Enabled = true;
							break;
						case 2:
							this.dateTimePickerContractExpiry.Enabled = true;
							this.dateTimePickerTestPeriod.Enabled = false;
							break;
						case 3:
							this.dateTimePickerContractExpiry.Enabled = true;
							this.dateTimePickerTestPeriod.Enabled = true;
							break;
						default:
							this.dateTimePickerContractExpiry.Enabled = false;
							this.dateTimePickerTestPeriod.Enabled = false;
							break;
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void dateTimePickerAssignedAt_ValueChanged(object sender, System.EventArgs e)
		{
			try
			{
				if (radioButtonAdditional.Checked == true && (Op == Operations.EditAssignment || Op == Operations.AddAssignment))
				{
					CalcExperience();
					//				DateTime AssignDate = Convert.ToDateTime( this.dtAssignment.Rows[0]["AssignedAt"] );
					//				int compareValue = this.dateTimePickerAssignedAt.Value.CompareTo(AssignDate);
					//				if(compareValue >= 0 && compareValue != 1 )
					//				{
					//					CalcExperience();
					//				}
					//				else if(compareValue == 1)
					//				{
					//					MessageBox.Show("Некоректно въведена дата.");
					//				}
					//				else
					//				{
					//					MessageBox.Show("Не можете да сключите допълнително споразумение с дата предхождаща датата на назначаване на служителя - " + AssignDate.ToShortDateString(), "Некоректна дата");
					//					this.dateTimePickerAssignedAt.Value = AssignDate;
					//				}
				}
				if(this.comboBoxContract.SelectedIndex == 1 || this.comboBoxContract.SelectedIndex == 3)
				{
					this.dateTimePickerTestPeriod.Value = this.dateTimePickerAssignedAt.Value.AddMonths(6);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonSelectPosition_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (vuePosition != null)
				{
					vuePosition.Table.TableName = "Positions";
					FormChoose form = new FormChoose(vuePosition, "длъжност");

					form.ShowDialog();
					if (form.DialogResult == DialogResult.OK)
					{
						if (form.dataGridView1.CurrentRow != null)
						{
							this.comboBoxPosition.SelectedIndex = this.comboBoxPosition.FindString(form.dataGridView1.CurrentRow.Cells["nameofposition"].Value.ToString());
						}
					}
				}
				else
				{
					MessageBox.Show("Не сте избрали звено от организацията");
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonExpCalculator_Click(object sender, System.EventArgs e)
		{
			try
			{
				ExpCalculator form = new ExpCalculator();
				form.ShowDialog();
				if (form.DialogResult == DialogResult.OK)
				{
					this.numBoxAssignmentExpD.Text = form.EndExp.Days.ToString();
					this.numBoxAssignmentExpY.Text = form.EndExp.Years.ToString();
					this.numBoxAssignmentExtM.Text = form.EndExp.Months.ToString();
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void comboBoxAssignReason_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.comboBoxPosition.SelectedIndex > 0)
				{
					DataView reasons = new DataView((DataTable)this.comboBoxAssignReason.DataSource);
					if (this.comboBoxAssignReason.SelectedIndex > 0)
					{
						if (Op == Operations.AddAssignment || Op == Operations.EditAssignment)
						{
							if (reasons[this.comboBoxAssignReason.SelectedIndex]["substitute"].ToString() == "1")
							{
								this.comboBoxTutorName.Enabled = true;
								this.comboBoxTutorAbsenceReason.Enabled = true;
								DataTable dtTutors = this.dataAdapter.SelectWhere(TableNames.Person, "*", " LEFT JOIN " + TableNames.PersonAssignment + " ON " + TableNames.Person + ".id = " + TableNames.PersonAssignment + ".parent WHERE " + TableNames.PersonAssignment + ".isactive = 1 AND " + TableNames.PersonAssignment + ".positionid = " + this.dtComboPosiiton.Rows[this.comboBoxPosition.SelectedIndex]["PositionCode"].ToString());
								ArrayList TempArr = new ArrayList();
								TempArr.Add("");
								foreach (DataRow row in dtTutors.Rows)
								{
									TempArr.Add(row["name"].ToString());
								}
								this.comboBoxTutorName.DataSource = TempArr;
								int idx;
								if (this.dataGridViewAssignment.CurrentRow != null)
								{
									idx = this.comboBoxTutorName.FindStringExact(this.dataGridViewAssignment.CurrentRow.Cells["tutorname"].Value.ToString());
									if (idx < 0)
									{
										idx = 0;
									}
									this.comboBoxTutorName.SelectedIndex = idx;
								}
							}
							else
							{
								this.comboBoxTutorAbsenceReason.Enabled = false;
								this.comboBoxTutorName.Enabled = false;
							}
						}
						else
						{
							if (reasons[this.comboBoxAssignReason.SelectedIndex]["substitute"].ToString() == "1")
							{
								DataTable dtTutors = this.dataAdapter.SelectWhere(TableNames.Person, "*", " LEFT JOIN " + TableNames.PersonAssignment + " ON " + TableNames.Person + ".id = " + TableNames.PersonAssignment + ".parent WHERE " + TableNames.PersonAssignment + ".isactive = 1 AND " + TableNames.PersonAssignment + ".positionid = " + this.dtComboPosiiton.Rows[this.comboBoxPosition.SelectedIndex]["PositionCode"].ToString());
								this.comboBoxTutorName.DataSource = dtTutors;
								this.comboBoxTutorName.DisplayMember = "Name";
							}
							this.comboBoxTutorAbsenceReason.Enabled = false;
							this.comboBoxTutorName.Enabled = false;
						}
					}
					else
					{
						this.comboBoxTutorName.Text = "";
						this.comboBoxTutorAbsenceReason.Text = "";
						this.comboBoxTutorAbsenceReason.Enabled = false;
						this.comboBoxTutorName.Enabled = false;
					}
				}
				else
				{
					this.comboBoxTutorName.Text = "";
					this.comboBoxTutorAbsenceReason.Text = "";
					this.comboBoxTutorAbsenceReason.Enabled = false;
					this.comboBoxTutorName.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonAssignmentExcel_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.dataGridViewAssignment.Rows.Count > 0)
				{
					ExcelExpo Ex = new ExcelExpo();
					Ex.ExportView(this.dataGridViewAssignment, (DataView)this.dataGridViewAssignment.DataSource, "Назначения на " + this.textBoxNames.Text);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void Salary_Changed(object sender, EventArgs e)
		{
			try
			{
				double c1, c2, c3, c4, res;
				double.TryParse(this.numBoxBaseSalary.Text, out c1);
				double.TryParse(this.textBoxSalaryAddon.Text, out c2);
				double.TryParse(this.numBoxMonthlyAddon.Text, out c3);
				double.TryParse(this.textBoxClassPercent.Text, out c4);
				//float.TryParse(this.textBoxClassPercent.Text, out c5);

				res = (c1 + c1 * c4 / 100 + c2 + c3);
				res = Math.Round(res, 2);
				this.numBoxBruto.Text = res.ToString();
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		#endregion

		#region Absence functions

		enum AbsencePositions
		{
			//Полагаем годишен отпуск
			//Болнични
			//Неплатен отпуск
			//Платен отпуск
			//Отглеждане на дете
			//Болнични след раждане
			//Командировка
			//Полагаем отпуск минали години
			//Обучение
			//Прекратяване на отпуск
			//Полагаем отпуск ТЕЛК
			//Полагаем отпуск обучение
			//Полагаем отпуск друг

			Empty = 0,
			YearlyPaidHoliday,
			Sickness,
			UnpaidHoliday,
			PaidHoliday,
			Motherhood,
			MotherhoodSickness,
			BusinessTravel,
			YearlyPaidHolidayPastYears,
			Education,
			Cancellation,
			TELK,
			PaidEducation,
			PaidOther,
		}

		private void buttonAbsenceAdd_Click(object sender, System.EventArgs e)
		{
			try
			{
				Op = Operations.AddAbsence;
				this.IsAbsenceEdit = false;
				this.EnableButtons(false, false, true, false, false, true, LockButtons.Absence);
				this.ControlEnabled(true, LockButtons.Absence);

				this.dateTimePickerAbsenceToData.Value = DateTime.Now;
				this.dateTimePickerAbsenceOrderFormData.Value = DateTime.Now;
				this.dateTimePickerAbsenceFromData.Value = DateTime.Now;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonAbsenceEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.dataGridViewAbsence.CurrentRow != null)
				{
					Op = Operations.EditAbsence;
					IsAbsenceEdit = true;
					this.EnableButtons(false, false, true, false, false, true, LockButtons.Absence);
					this.ControlEnabled(true, LockButtons.Absence);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonAbsenceSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				Dictionary<string, object> Dict = new Dictionary<string, object>();
				Dictionary<string, object> hDict = new Dictionary<string, object>(); //used only when there is paid holiday

				bool IsValid = false;
				DataRow rowZ = null;

				int otpusk = 0, telk = 0;
				int left = 0;

				if (this.ValidateAbsenceData(Dict) == false)
				{
					return;
				}

				try
				{
					if (this.comboBoxAbsenceTypeAbsence.SelectedIndex != -1)
					{
						#region Paid holiday
						if (HolidayCalculation(hDict, ref rowZ, ref otpusk, ref left, true, "leftover", (int)AbsencePositions.YearlyPaidHoliday, "Полагаем годишен отпуск") == false)
						{
							return;
						}

						if (HolidayCalculation(hDict, ref rowZ, ref otpusk, ref left, true, "telk", (int)AbsencePositions.TELK, "Полагаем отпуск ТЕЛК") == false)
						{
							return;
						}

						if (HolidayCalculation(hDict, ref rowZ, ref otpusk, ref left, false, "unpayed", (int)AbsencePositions.UnpaidHoliday, "Неплатен отпуск") == false)
						{
							return;
						}

						if (HolidayCalculation(hDict, ref rowZ, ref otpusk, ref left, false, "education", (int)AbsencePositions.PaidEducation, "Полагаем отпуск обучение") == false)
						{
							return;
						}

						if (HolidayCalculation(hDict, ref rowZ, ref otpusk, ref left, false, "additional", (int)AbsencePositions.PaidOther, "Полагаем отпуск друг") == false)
						{
							return;
						}
						#endregion

						#region telk
						//if (this.comboBoxAbsenceTypeAbsence.SelectedIndex == 9 || (this.dataGridViewAbsence.CurrentRow != null && this.dataGridViewAbsence.CurrentRow.Cells["typeabsence"].Value.ToString() == "Полагаем отпуск ТЕЛК" && Op == Operations.EditAbsence))
						//{
						//    // При платени отпуски
						//    string temp = this.comboBoxAbsenceForYear.Text;

						//    foreach (DataRow rz in this.dtYearHoliday.Rows)
						//    {
						//        if (rz["year"].ToString() == temp)
						//        {
						//            rowZ = rz;
						//            break;
						//        }
						//    }
						//    if (rowZ != null)
						//    {
						//        try
						//        {
						//            left = int.Parse(rowZ["telk"].ToString());
						//        }
						//        catch
						//        {
						//            left = 0;
						//        }
						//        try
						//        {
						//            if (this.Op == Operations.EditAbsence)
						//            {
						//                int new_days = int.Parse(this.numBoxAbsenceWorkDays.Text);

						//                // Towa ima smiyla na slednoto - 
						//                //1wo dotuk se stiga samo ako e izbrano - Polagaem godishen otpusk
						//                // Toest ako staroto e bilo polagaem godieshen otpusk samo togawa moje da redaktirame - w protiwen sluchay prosto izwajdame dnite ot ostawashite
						//                if (this.dataGridViewAbsence.CurrentRow.Cells["typeabsence"].Value.ToString() == "Полагаем отпуск ТЕЛК" && this.comboBoxAbsenceTypeAbsence.Text != "Полагаем отпуск ТЕЛК")
						//                {
						//                    int old_days = 0;
						//                    int.TryParse(this.dataGridViewAbsence.CurrentRow.Cells["countdays"].Value.ToString(), out old_days);
						//                    telk = left + old_days;
						//                }
						//                else if (this.dataGridViewAbsence.CurrentRow.Cells["typeabsence"].Value.ToString() != "Полагаем отпуск ТЕЛК" && this.comboBoxAbsenceTypeAbsence.Text == "Полагаем отпуск ТЕЛК")// towa e kogato pri redakciq ot neplaten se preminawa w platen otpusk!
						//                {
						//                    telk = left - int.Parse(this.numBoxAbsenceWorkDays.Text);
						//                }
						//            }
						//            else
						//            {
						//                telk = left - int.Parse(this.numBoxAbsenceWorkDays.Text);
						//            }
						//        }
						//        catch (Exception ex)
						//        {
						//            ErrorLog.WriteException(ex, ex.Message);
						//            MessageBox.Show(ex.Message);
						//            telk = 0;
						//        }
						//    }
						//    else
						//    {
						//        MessageBox.Show("Несъществува история на отпуски за година " + temp + ". Проверете датата или добавете годината в историята на отпуски");
						//        return;
						//    }
						//    if (telk < 0)
						//    {
						//        MessageBox.Show("Няма достатъчно отпуск за съответната година");
						//        return;
						//    }
						//    else
						//    {
						//        rowZ["telk"] = telk;
						//        hDict.Add("telk", telk.ToString());
						//    }
						//}
						#endregion

						#region UnpayedHoliday
						//if (this.comboBoxAbsenceTypeAbsence.SelectedIndex == 3  || 
						//    ((this.dataGridViewAbsence.CurrentRow != null && this.dataGridViewAbsence.CurrentRow.Cells["typeabsence"].Value.ToString() == "Полагаем годишен отпуск")
						//    || (this.dataGridViewAbsence.CurrentRow != null && this.dataGridViewAbsence.CurrentRow.Cells["typeabsence"].Value.ToString() == "Полагаем отпуск ТЕЛК")
						//    && Op == Operations.EditAbsence))
						//{
						//    // При платени отпуски
						//    string temp = this.comboBoxAbsenceForYear.Text;

						//    foreach (DataRow rz in this.dtYearHoliday.Rows)
						//    {
						//        if (rz["year"].ToString() == temp)
						//        {
						//            rowZ = rz;
						//            break;
						//        }
						//    }
						//    if (rowZ != null)
						//    {
						//        try
						//        {
						//            left = int.Parse(rowZ["unpayed"].ToString());
						//        }
						//        catch
						//        {
						//            left = 0;
						//        }
						//        try
						//        {
						//            if (this.Op == Operations.EditAbsence)
						//            {
						//                int new_days = int.Parse(this.numBoxAbsenceWorkDays.Text);

						//                // Towa ima smiyla na slednoto - 
						//                //1wo dotuk se stiga samo ako e izbrano - Polagaem godishen otpusk
						//                // Toest ako staroto e bilo неплатен otpusk samo togawa moje da redaktirame - w protiwen sluchay prosto izwajdame dnite ot ostawashite
						//                if (this.dataGridViewAbsence.CurrentRow.Cells["typeabsence"].Value.ToString() != "Неплатен отпуск" && this.comboBoxAbsenceTypeAbsence.Text == "Неплатен отпуск") // towa e kogato pri redakciq ot neplaten se preminawa w platen otpusk!
						//                {
						//                    otpusk = left - int.Parse(this.numBoxAbsenceWorkDays.Text);
						//                }
						//                else if (this.dataGridViewAbsence.CurrentRow.Cells["typeabsence"].Value.ToString() == "Неплатен отпуск" && this.comboBoxAbsenceTypeAbsence.Text == "Неплатен отпуск") // When we edit a holiday size here
						//                {
						//                    int olddays = int.Parse(this.dataGridViewAbsence.CurrentRow.Cells["countdays"].Value.ToString());

						//                    otpusk = left - (new_days - olddays);
						//                    //otpusk = left - int.Parse(this.numBoxAbsenceWorkDays.Text);
						//                }
						//            }
						//            else
						//            {
						//                otpusk = left - int.Parse(this.numBoxAbsenceWorkDays.Text);
						//            }
						//        }
						//        catch (Exception ex)
						//        {
						//            ErrorLog.WriteException(ex, ex.Message);
						//            MessageBox.Show(ex.Message);
						//            otpusk = 0;
						//        }
						//    }
						//    else
						//    {
						//        MessageBox.Show("Несъществува история на отпуски за година " + temp + ". Проверете датата или добавете годината в историята на отпуски");
						//        return;
						//    }

						//    rowZ["Unpayed"] = otpusk;
						//    hDict.Add("Unpayed", otpusk.ToString());

						//}
						#endregion

						#region Education holiday
						//if (this.comboBoxAbsenceTypeAbsence.SelectedIndex == 11 || (this.dataGridViewAbsence.CurrentRow != null && this.dataGridViewAbsence.CurrentRow.Cells["typeabsence"].Value.ToString() == "Полагаем отпуск обучение" && Op == Operations.EditAbsence))
						//{
						//    // При платени отпуски
						//    string temp = this.comboBoxAbsenceForYear.Text;

						//    foreach (DataRow rz in this.dtYearHoliday.Rows)
						//    {
						//        if (rz["year"].ToString() == temp)
						//        {
						//            rowZ = rz;
						//            break;
						//        }
						//    }
						//    if (rowZ != null)
						//    {
						//        try
						//        {
						//            left = int.Parse(rowZ["education"].ToString());
						//        }
						//        catch
						//        {
						//            left = 0;
						//        }
						//        try
						//        {
						//            if (this.Op == Operations.EditAbsence)
						//            {
						//                int new_days = int.Parse(this.numBoxAbsenceWorkDays.Text);

						//                // Towa ima smiyla na slednoto - 
						//                //1wo dotuk se stiga samo ako e izbrano - Polagaem godishen otpusk
						//                // Toest ako staroto e bilo polagaem godieshen otpusk samo togawa moje da redaktirame - w protiwen sluchay prosto izwajdame dnite ot ostawashite
						//                if (this.dataGridViewAbsence.CurrentRow.Cells["typeabsence"].Value.ToString() == "Полагаем отпуск обучение" && this.comboBoxAbsenceTypeAbsence.Text != "Полагаем отпуск обучение")
						//                {
						//                    int old_days = 0;
						//                    int.TryParse(this.dataGridViewAbsence.CurrentRow.Cells["countdays"].Value.ToString(), out old_days);
						//                    otpusk = left + old_days;
						//                }
						//                else if (this.dataGridViewAbsence.CurrentRow.Cells["typeabsence"].Value.ToString() != "Полагаем отпуск обучение" && this.comboBoxAbsenceTypeAbsence.Text == "Полагаем отпуск обучение") // towa e kogato pri redakciq ot neplaten se preminawa w platen otpusk!
						//                {
						//                    otpusk = left - int.Parse(this.numBoxAbsenceWorkDays.Text);
						//                }
						//                else if (this.dataGridViewAbsence.CurrentRow.Cells["typeabsence"].Value.ToString() == "Полагаем отпуск обучение" && this.comboBoxAbsenceTypeAbsence.Text == "Полагаем отпуск обучение") // When we edit a holiday size here
						//                {
						//                    int olddays = int.Parse(this.dataGridViewAbsence.CurrentRow.Cells["countdays"].Value.ToString());

						//                    otpusk = left - (new_days - olddays);
						//                    //otpusk = left - int.Parse(this.numBoxAbsenceWorkDays.Text);
						//                }
						//            }
						//            else
						//            {
						//                otpusk = left - int.Parse(this.numBoxAbsenceWorkDays.Text);
						//            }
						//        }
						//        catch (Exception ex)
						//        {
						//            ErrorLog.WriteException(ex, ex.Message);
						//            MessageBox.Show(ex.Message);
						//            otpusk = 0;
						//        }
						//    }
						//    else
						//    {
						//        MessageBox.Show("Несъществува история на отпуски за година " + temp + ". Проверете датата или добавете годината в историята на отпуски");
						//        return;
						//    }
						//    if (otpusk < 0)
						//    {
						//        MessageBox.Show("Няма достатъчно отпуск за съответната година");
						//        return;
						//    }
						//    else
						//    {
						//        rowZ["education"] = otpusk;
						//        hDict.Add("education", otpusk.ToString());
						//    }
						//}
						#endregion

						#region Additional holiday
						//if (this.comboBoxAbsenceTypeAbsence.SelectedIndex == 12 || (this.dataGridViewAbsence.CurrentRow != null && this.dataGridViewAbsence.CurrentRow.Cells["typeabsence"].Value.ToString() == "Полагаем отпуск друг" && Op == Operations.EditAbsence))
						//{
						//    // При платени отпуски
						//    string temp = this.comboBoxAbsenceForYear.Text;

						//    foreach (DataRow rz in this.dtYearHoliday.Rows)
						//    {
						//        if (rz["year"].ToString() == temp)
						//        {
						//            rowZ = rz;
						//            break;
						//        }
						//    }
						//    if (rowZ != null)
						//    {
						//        try
						//        {
						//            left = int.Parse(rowZ["additional"].ToString());
						//        }
						//        catch
						//        {
						//            left = 0;
						//        }
						//        try
						//        {
						//            if (this.Op == Operations.EditAbsence)
						//            {
						//                int new_days = int.Parse(this.numBoxAbsenceWorkDays.Text);

						//                // Towa ima smiyla na slednoto - 
						//                //1wo dotuk se stiga samo ako e izbrano - Polagaem godishen otpusk
						//                // Toest ako staroto e bilo polagaem godieshen otpusk samo togawa moje da redaktirame - w protiwen sluchay prosto izwajdame dnite ot ostawashite
						//                if (this.dataGridViewAbsence.CurrentRow.Cells["typeabsence"].Value.ToString() == "Полагаем отпуск друг" && this.comboBoxAbsenceTypeAbsence.Text != "Полагаем отпуск друг")
						//                {
						//                    int old_days = 0;
						//                    int.TryParse(this.dataGridViewAbsence.CurrentRow.Cells["countdays"].Value.ToString(), out old_days);
						//                    otpusk = left + old_days;
						//                }
						//                else if (this.dataGridViewAbsence.CurrentRow.Cells["typeabsence"].Value.ToString() != "Полагаем отпуск друг" && this.comboBoxAbsenceTypeAbsence.Text == "Полагаем отпуск друг") // towa e kogato pri redakciq ot neplaten se preminawa w platen otpusk!
						//                {
						//                    otpusk = left - int.Parse(this.numBoxAbsenceWorkDays.Text);
						//                }
						//                else if (this.dataGridViewAbsence.CurrentRow.Cells["typeabsence"].Value.ToString() == "Полагаем отпуск друг" && this.comboBoxAbsenceTypeAbsence.Text == "Полагаем отпуск друг") // When we edit a holiday size here
						//                {
						//                    int olddays = int.Parse(this.dataGridViewAbsence.CurrentRow.Cells["countdays"].Value.ToString());

						//                    otpusk = left - (new_days - olddays);
						//                    //otpusk = left - int.Parse(this.numBoxAbsenceWorkDays.Text);
						//                }
						//            }
						//            else
						//            {
						//                otpusk = left - int.Parse(this.numBoxAbsenceWorkDays.Text);
						//            }
						//        }
						//        catch (Exception ex)
						//        {
						//            ErrorLog.WriteException(ex, ex.Message);
						//            MessageBox.Show(ex.Message);
						//            otpusk = 0;
						//        }
						//    }
						//    else
						//    {
						//        MessageBox.Show("Несъществува история на отпуски за година " + temp + ". Проверете датата или добавете годината в историята на отпуски");
						//        return;
						//    }
						//    if (otpusk < 0)
						//    {
						//        MessageBox.Show("Няма достатъчно отпуск за съответната година");
						//        return;
						//    }
						//    else
						//    {
						//        rowZ["additional"] = otpusk;
						//        hDict.Add("additional", otpusk.ToString());
						//    }
						//}
						#endregion
					}

					if (!IsAbsenceEdit)
					{
						// Towa e pri dobawqne na now red
						try
						{
							Dictionary<string, object> nDict = new Dictionary<string, object>();
							//nDict.Add("Date", DataAction.ConvertDateToMySqlU(DateTime.Now, mainForm.DataBaseTypes));
							AddNotesRecord(nDict);

							if (this.comboBoxAbsenceTypeAbsence.SelectedIndex == (int)AbsencePositions.Cancellation)
							{
								CalculateCancellation();
								this.dtYearHoliday = this.dataAdapter.SelectWhere(TableNames.YearHoliday, "*", " WHERE parent = " + this.parent + " ORDER by Year");
								this.dataGridViewYears.DataSource = null;
								this.dataGridViewYears.DataSource = this.dtYearHoliday;
								this.dtYearHoliday.TableName = TableNames.YearHoliday;
								this.JustifyGridView(this.dataGridViewYears, TableNames.Compare(TableNames.YearHoliday));
								this.dataGridViewYears.Refresh();
							}

							int tempid = this.dataAdapter.UniversalInsertParam(TableNames.Absence, Dict, "id", TransactionComnmand.BEGIN_TRANSACTION);
							if (tempid < 0)
							{
								MessageBox.Show("Грешка при добавяне на отпуск", ErrorMessages.NoConnection);
								return;
							}
							Dict.Add("ID", tempid.ToString());

							if (hDict.Count > 0)
							{ //ако е платен отпуск и има достатъчно на брой дни
								IsValid = this.dataAdapter.UniversalUpdateParam(TableNames.YearHoliday, "id", hDict, rowZ["id"].ToString(), TransactionComnmand.USE_TRANSACTION);
								if (IsValid == false)
								{
									MessageBox.Show("Грешка при добавяне на отпуск", ErrorMessages.NoConnection);
									return;
								}
								//this.dataGridViewYears.CurrentRow.Cells["leftover"].Value = otpusk.ToString();
								//this.dtYearHoliday.Rows[this.dataGridViewYears.CurrentRowIndex]["leftover"] = otpusk;
							}

							tempid = this.dataAdapter.UniversalInsertParam(TableNames.NotesTable, nDict, "id", TransactionComnmand.COMMIT_TRANSACTION);

							if (tempid < 0)
							{
								MessageBox.Show("Грешка при добавяне на отпуск", ErrorMessages.NoConnection);
								return;
							}

							nDict.Add("ID", tempid.ToString());
							this.AddDictToTable(nDict, this.dtNotes);
							this.AddDictToTableObject(Dict, this.dtAbsence);
						}
						catch (Exception ex)
						{
							ErrorLog.WriteException(ex, ex.Message);
							MessageBox.Show(ex.Message);
						}
					}
					else
					{
						// Towa e pri update				
						try
						{
							if (hDict.Count > 0)
							{
								IsValid = this.dataAdapter.UniversalUpdateParam(TableNames.Absence, "id", Dict, this.dataGridViewAbsence.CurrentRow.Cells["id"].Value.ToString(), TransactionComnmand.BEGIN_TRANSACTION);
								if (IsValid == false)
								{
									MessageBox.Show("Грешка при редакция на назначение", ErrorMessages.NoConnection);
									return;
								}
								IsValid = this.dataAdapter.UniversalUpdateParam(TableNames.YearHoliday, "id", hDict, this.dataGridViewYears.CurrentRow.Cells["id"].Value.ToString(), TransactionComnmand.COMMIT_TRANSACTION);
								if (IsValid == false)
								{
									MessageBox.Show("Грешка при редакция на назначение", ErrorMessages.NoConnection);
									return;
								}
								if (hDict.ContainsKey("leftover"))
								{
									rowZ["leftover"] = otpusk;
								}
								if (hDict.ContainsKey("telk"))
								{
									rowZ["telk"] = telk;
								}
							}
							else
							{
								IsValid = this.dataAdapter.UniversalUpdateParam(TableNames.Absence, "id", Dict, this.dataGridViewAbsence.CurrentRow.Cells["id"].Value.ToString(), TransactionComnmand.NO_TRANSACTION);
								if (IsValid == false)
								{
									MessageBox.Show("Грешка при редакция на назначение", ErrorMessages.NoConnection);
									return;
								}
							}

							DataRow row = this.dtAbsence.Rows.Find(this.dataGridViewAbsence.CurrentRow.Cells["id"].Value);
							if (row != null)
							{
								this.UpdateDictToRowObject(Dict, row);
							}
						}
						catch (Exception ex)
						{
							ErrorLog.WriteException(ex, ex.Message);
							MessageBox.Show(ex.Message);
						}
					}
				}
				catch (Exception ex)
				{
					ErrorLog.WriteException(ex, ex.Message);
					MessageBox.Show(ex.Message);
				}

				Op = Operations.ViewPersonData;
				this.ControlEnabled(false, LockButtons.Absence);
				this.EnableButtons(true, true, false, true, true, false, LockButtons.Absence);
				IsAbsenceEdit = false;
				this.CalculatetotalVacation();
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void CalculateCancellation()
		{
			try
			{
				DateTime DateStart;
				DateStart = new DateTime(this.dateTimePickerAbsenceFromData.Value.Year, this.dateTimePickerAbsenceFromData.Value.Month, this.dateTimePickerAbsenceFromData.Value.Day);

				HolidayPlan.CalendarRow.CalculateCancellation(DateStart, mainform.EntityConectionString, this.parent);
				this.dataGridViewYears.Refresh();
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void AddNotesRecord(Dictionary<string, object> nDict)
		{
			nDict.Add("Date", DateTime.Now);
			nDict.Add("Text", "Отсъствал от " + this.dateTimePickerAbsenceFromData.Text + " до " + this.dateTimePickerAbsenceToData.Text);
			nDict.Add("Type", "Отсъствие");
			nDict.Add("TypeDocument", this.comboBoxAbsenceTypeAbsence.Text);
			nDict.Add("Par", this.parent.ToString());
		}

		private bool HolidayCalculation(Dictionary<string, object> hDict, ref DataRow rowZ, ref int otpusk, ref int left, bool ToReturn, string FieldName, int Position, string Value)
		{
			if (this.comboBoxAbsenceTypeAbsence.SelectedIndex == Position || (this.dataGridViewAbsence.CurrentRow != null && this.dataGridViewAbsence.CurrentRow.Cells["typeabsence"].Value.ToString() == Value && Op == Operations.EditAbsence))
			{
				// При платени отпуски
				string temp = this.comboBoxAbsenceForYear.Text;
				try
				{
					if (DateTime.Now.Year != int.Parse(temp) && IsAbsenceEdit == false)
					{
						if (DialogResult.No == MessageBox.Show("Потвърдете желанието че искате да вземете " + Value + " за " + temp + " година", "Потвърждение", MessageBoxButtons.YesNo))
						{
							return false;
						}
					}
				}
				catch
				{
					MessageBox.Show("Некоректно зададена година! Поправете годината!");
					return false;
				}
				foreach (DataRow rz in this.dtYearHoliday.Rows)
				{
					if (rz["year"].ToString() == temp)
					{
						rowZ = rz;
						break;
					}
				}
				if (rowZ != null)
				{
					try
					{
						left = int.Parse(rowZ[FieldName].ToString());
					}
					catch
					{
						left = 0;
					}

					try
					{
						if (this.Op == Operations.EditAbsence)
						{
							int new_days = int.Parse(this.numBoxAbsenceWorkDays.Text);

							// Towa ima smiyla na slednoto - 
							//1wo dotuk se stiga samo ako e izbrano - Polagaem godishen otpusk
							// Toest ako staroto e bilo polagaem godieshen otpusk samo togawa moje da redaktirame - w protiwen sluchay prosto izwajdame dnite ot ostawashite
							if (this.dataGridViewAbsence.CurrentRow.Cells["typeabsence"].Value.ToString() == Value && this.comboBoxAbsenceTypeAbsence.Text != Value)
							{
								int old_days = 0;
								int.TryParse(this.dataGridViewAbsence.CurrentRow.Cells["countdays"].Value.ToString(), out old_days);
								otpusk = left + old_days;
							}
							else if (this.dataGridViewAbsence.CurrentRow.Cells["typeabsence"].Value.ToString() != Value && this.comboBoxAbsenceTypeAbsence.Text == Value) // towa e kogato pri redakciq ot neplaten se preminawa w platen otpusk!
							{
								otpusk = left - int.Parse(this.numBoxAbsenceWorkDays.Text);
							}
							else if (this.dataGridViewAbsence.CurrentRow.Cells["typeabsence"].Value.ToString() == Value && this.comboBoxAbsenceTypeAbsence.Text == Value) // When we edit a holiday size here
							{
								int olddays = int.Parse(this.dataGridViewAbsence.CurrentRow.Cells["countdays"].Value.ToString());

								otpusk = left - (new_days - olddays);
							}
						}
						else
						{
							otpusk = left - int.Parse(this.numBoxAbsenceWorkDays.Text);
						}
					}
					catch (Exception ex)
					{
						ErrorLog.WriteException(ex, ex.Message);
						MessageBox.Show(ex.Message);
						otpusk = 0;
					}
				}
				else if (ToReturn)
				{
					MessageBox.Show("Несъществува история на отпуски за година " + temp + ". Проверете датата или добавете годината в историята на отпуски");
					return false;
				}
				if (otpusk < 0 && ToReturn)
				{
					MessageBox.Show("Няма достатъчно отпуск за съответната година");
					return false;
				}
				else
				{
					rowZ[FieldName] = otpusk;
					hDict.Add(FieldName, otpusk.ToString());
				}
			}
			return true;
		}

		private void buttonAbsenceDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.dataGridViewAbsence.CurrentRow != null)
				{
					if (MessageBox.Show(this, "Сигурни ли сте че искате да изтриете отсъствието " + this.dataGridViewAbsence.CurrentRow.Cells["typeabsence"].Value.ToString(), "Изтриване", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						Dictionary<string, object> hDict = new Dictionary<string, object>();
						string n = TableNames.Absence;

						AbsencePositions pos = new AbsencePositions();
						pos = (AbsencePositions)this.comboBoxAbsenceTypeAbsence.SelectedIndex;

						switch (pos)
						{
							case AbsencePositions.YearlyPaidHoliday:
								DeleteAndReturn(hDict, "leftover");
								break;
							case AbsencePositions.TELK:
								DeleteAndReturn(hDict, "telk");
								break;
							case AbsencePositions.PaidEducation:
								DeleteAndReturn(hDict, "education");
								break;
							case AbsencePositions.PaidOther:
								DeleteAndReturn(hDict, "additional");
								break;
							case AbsencePositions.UnpaidHoliday:
								DeleteAndReturn(hDict, "unpayed");
								break;
						}

						this.dataAdapter.UniversalDelete(TableNames.Absence, this.dataGridViewAbsence.CurrentRow.Cells["id"].Value.ToString(), "id");
						DataRow del = this.dtAbsence.Rows.Find(this.dataGridViewAbsence.CurrentRow.Cells["id"].Value);

						if (del != null)
							this.dtAbsence.Rows.Remove(del);

						this.dataGridViewYears.Refresh();
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void DeleteAndReturn(Dictionary<string, object> hDict, string FieldName)
		{
			// При платени отпуски
			string temp = this.comboBoxAbsenceForYear.Text;
			DataRow rowZ = null;
			int rets, leftover;
			foreach (DataRow rz in this.dtYearHoliday.Rows)
			{
				if (rz["year"].ToString() == temp)
				{
					rowZ = rz;
					break;
				}
			}
			if (rowZ != null)
			{
				rets = 0;
				leftover = 0;
				if (int.TryParse(this.dataGridViewAbsence.CurrentRow.Cells["countdays"].Value.ToString(), out rets) && int.TryParse(rowZ[FieldName].ToString(), out leftover))
				{
					leftover += rets;
					hDict.Add(FieldName, leftover.ToString());
				}
				this.dataAdapter.UniversalUpdateParam(TableNames.YearHoliday, "id", hDict, rowZ["id"].ToString(), TransactionComnmand.NO_TRANSACTION);
				rowZ[FieldName] = leftover;
			}
		}

		//private void DeleteAndReturnUnpaid(Dictionary<string, object> hDict, string FieldName)
		//{
		//    // При платени отпуски
		//    string temp = this.comboBoxAbsenceForYear.Text;
		//    DataRow rowZ = null;
		//    int rets, leftover;
		//    foreach (DataRow rz in this.dtYearHoliday.Rows)
		//    {
		//        if (rz["year"].ToString() == temp)
		//        {
		//            rowZ = rz;
		//            break;
		//        }
		//    }
		//    if (rowZ != null)
		//    {
		//        DataTable dtUnpaydedForYear = this.dataAdapter.SelectWhere(TableNames.Absence, "*", "WHERE parent = " + this.parent + " AND year = " + rowZ["year"]);
		//        int TotalUnpayedUsed = 0;
		//        foreach (DataRow row in dtUnpaydedForYear.Rows)
		//        {
		//            int us = 0;
		//            int.TryParse(row["countdays"].ToString(), out us);
		//            TotalUnpayedUsed += us;
		//        }

		//        int UnpayedLeft = 0;
		//        int.TryParse(rowZ[FieldName].ToString(), out UnpayedLeft);

		//        if (UnpayedLeft >= 0)
		//        {
		//            rets = 0;
		//            UnpayedLeft = 0;
		//            if (int.TryParse(this.dataGridViewAbsence.CurrentRow.Cells["countdays"].Value.ToString(), out rets))
		//            {
		//                UnpayedLeft += rets;
		//                hDict.Add(FieldName, UnpayedLeft.ToString());
		//            }
		//            this.dataAdapter.UniversalUpdateObject(TableNames.YearHoliday, rowZ["id"].ToString(), hDict);
		//            rowZ[FieldName] = UnpayedLeft;
		//        }
		//        else if(


		//    }
		//}

		private void buttonAbsenceCancel_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (Op == Operations.AddAbsence)  //Ако операцията е бил по добавяне зачиства боклука
				{
					this.textBoxAbsenceAttachment7.Text = "";
					this.dateTimePickerAbsenceFromData.Text = "";
					this.dateTimePickerAbsenceToData.Text = "";
					this.dateTimePickerAbsenceOrderFormData.Text = "";
				}

				Op = Operations.ViewPersonData;
				this.ControlEnabled(false, LockButtons.Absence);
				this.EnableButtons(true, true, false, true, true, false, LockButtons.Absence);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}

		}

		private bool ValidateAbsenceData(Dictionary<string, object> Dict)
		{
			try
			{
				Dict.Add("OrderFromDate", this.dateTimePickerAbsenceOrderFormData.Value);
				Dict.Add("FromDate", this.dateTimePickerAbsenceFromData.Value.Date);
				Dict.Add("ToDate", this.dateTimePickerAbsenceToData.Value.Date);

				if (this.numBoxAbsenceWorkDays.Text == "")
				{
					Dict.Add("CountDays", 0.ToString());
				}
				else
				{
					Dict.Add("CountDays", this.numBoxAbsenceWorkDays.Text);
				}

				if (this.numBoxAbsenceCalendarDays.Text == "")
				{
					Dict.Add("calendardays", 0.ToString());
				}
				else
				{
					Dict.Add("calendardays", this.numBoxAbsenceCalendarDays.Text);
				}
				Dict.Add("TypeAbsence", this.comboBoxAbsenceTypeAbsence.SelectedItem.ToString());

				//int test;
				//if (Int32.TryParse(this.textBoxAbsenceNumberOrder.Text, out test) == false)
				//{
				//this.textBoxAbsenceNumberOrder.Text = "0";
				//}

				Dict.Add("NumberOrder", this.textBoxAbsenceNumberOrder.Text);
				Dict.Add("Reason", this.textBoxAbsenceNotes.Text);

				Dict.Add("Parent", this.parent.ToString());
				Dict.Add("ModifiedByUser", this.User);
				if (this.comboBoxAbsenceForYear.SelectedIndex > -1)
				{
					Dict.Add("Year", this.comboBoxAbsenceForYear.Text.ToString());
				}
				else
				{
					Dict.Add("Year", "");
				}

				if (this.comboBoxAbsenceTypeAbsence.SelectedIndex == 2)
				{
					Dict.Add("issuedatdate", this.dateTimePickerAbsenceSicknessIssuedAtDate.Value);
					Dict.Add("sicknessduration", this.comboBoxAbsenceSicknessDuration.Text);
					Dict.Add("attachment7", this.textBoxAbsenceAttachment7.Text);
					Dict.Add("declaration39", this.textBoxAbsenceDec39.Text);
					Dict.Add("mkb", this.textBoxAbsenceMKB.Text);
					Dict.Add("napdocs", this.textBoxAbsenceNAPDocs.Text);
					Dict.Add("additionaldocs", this.textBoxAbsenceAdditionalDocs.Text);
					Dict.Add("reasons", this.textBoxAbsenceReasons.Text);

					//if (int.TryParse(this.textBoxAbsenceSicknessNumber.Text, out test) == false)
					//{
					//	MessageBox.Show("Номер на болнични трябва да бъде число");
					//	return false;
					//}
					Dict.Add("sicknessnumber", this.textBoxAbsenceSicknessNumber.Text);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				return false;
			}
			return true;
		}

		private void RefreshAbsenceDataSource(bool IsFormLoad)
		{
			try
			{
				TabPage tab;
				this.dtAbsence = this.dataAdapter.SelectWhere(TableNames.Absence, "*", "WHERE parent = " + this.parent);
				this.dtYearHoliday = this.dataAdapter.SelectWhere(TableNames.YearHoliday, "*", " WHERE parent = " + this.parent + " ORDER by Year");
				if (this.dtAbsence == null || this.dtYearHoliday == null)
				{
					MessageBox.Show("Грешка при зареждане на таблицата за отсъствия", ErrorMessages.NoConnection);
					this.Close();
				}
				this.dtAbsence.PrimaryKey = new DataColumn[] { this.dtAbsence.Columns["ID"] };
				this.dtYearHoliday.PrimaryKey = new DataColumn[] { this.dtYearHoliday.Columns["id"] };


				tab = this.tabControlCardNew.SelectedTab;
				this.tabControlCardNew.SelectedTab = this.tabControlCardNew.TabPages["TabPageAbsence"];

				if (this.tabControlCardNew.SelectedTab != null)
				{
					this.dataGridViewAbsence.DataSource = this.dtAbsence;
					this.dtAbsence.TableName = TableNames.Absence;

					this.dataGridViewAbsence.ClearSelection();

					this.dataGridViewYears.DataSource = this.dtYearHoliday;
					if (dtYearHoliday.Rows.Count > 0)
					{
						this.dataGridViewYears.Rows[this.dtYearHoliday.Rows.Count - 1].Selected = true;
					}
					//this.da

					this.comboBoxAbsenceForYear.DataSource = this.dtYearHoliday;
					this.comboBoxAbsenceForYear.DisplayMember = "year";
					this.dtYearHoliday.TableName = TableNames.YearHoliday;
					if (dtYearHoliday.Rows.Count > 0)
					{
						this.SetComboIndex(this.comboBoxAbsenceForYear, this.dtYearHoliday.Rows.Count - 1);
					}

					this.CalculatetotalVacation();

					this.JustifyGridView(this.dataGridViewAbsence, TableNames.Compare(TableNames.Absence));
					this.JustifyGridView(this.dataGridViewYears, TableNames.Compare(TableNames.YearHoliday));

					this.textBoxAbsenceAttachment7.Text = "";
					this.dateTimePickerAbsenceFromData.Text = "";
					this.dateTimePickerAbsenceToData.Text = "";
					this.dateTimePickerAbsenceOrderFormData.Text = "";
					this.comboBoxAbsenceTypeAbsence.SelectedIndex = 0;
				}
				this.tabControlCardNew.SelectedTab = tab;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}

		}

		private void dataGridViewAbsence_SelectionChanged(object sender, EventArgs e)
		{
			if (this.GridSelect)
				this.dataGridViewAbsence_Click(sender, e);
		}

		private void dataGridViewAbsence_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.dataGridViewAbsence.CurrentRow == null)
					return;

				int index = this.comboBoxAbsenceTypeAbsence.FindString(this.dataGridViewAbsence.CurrentRow.Cells["typeabsence"].Value.ToString());
				this.SetComboIndex(this.comboBoxAbsenceTypeAbsence, index);

				index = this.comboBoxAbsenceForYear.FindString(this.dataGridViewAbsence.CurrentRow.Cells["year"].Value.ToString());
				this.SetComboIndex(this.comboBoxAbsenceForYear, index);

				index = this.comboBoxAbsenceSicknessDuration.FindString(this.dataGridViewAbsence.CurrentRow.Cells["sicknessduration"].Value.ToString());
				this.SetComboIndex(this.comboBoxAbsenceSicknessDuration, index);

				try
				{
					this.dateTimePickerAbsenceFromData.Value = (DateTime)this.dataGridViewAbsence.CurrentRow.Cells["fromdate"].Value;
				}
				catch (Exception)
				{
				}
				try
				{
					this.dateTimePickerAbsenceToData.Value = (DateTime)this.dataGridViewAbsence.CurrentRow.Cells["todate"].Value;
				}
				catch (Exception)
				{
				}
				try
				{
					this.dateTimePickerAbsenceOrderFormData.Value = (DateTime)this.dataGridViewAbsence.CurrentRow.Cells["orderfromdate"].Value;
				}
				catch (Exception)
				{
				}
				try
				{
					this.dateTimePickerAbsenceSicknessIssuedAtDate.Value = (DateTime)this.dataGridViewAbsence.CurrentRow.Cells["issuedatdate"].Value;
				}
				catch (Exception)
				{
				}

				this.numBoxAbsenceWorkDays.Text = this.dataGridViewAbsence.CurrentRow.Cells["countdays"].Value.ToString();
				this.numBoxAbsenceCalendarDays.Text = this.dataGridViewAbsence.CurrentRow.Cells["calendardays"].Value.ToString();

				this.textBoxAbsenceNumberOrder.Text = this.dataGridViewAbsence.CurrentRow.Cells["numberorder"].Value.ToString();
				this.textBoxAbsenceNotes.Text = this.dataGridViewAbsence.CurrentRow.Cells["reason"].Value.ToString();
				this.textBoxAbsenceAttachment7.Text = this.dataGridViewAbsence.CurrentRow.Cells["attachment7"].Value.ToString();
				this.textBoxAbsenceDec39.Text = this.dataGridViewAbsence.CurrentRow.Cells["declaration39"].Value.ToString();
				this.textBoxAbsenceMKB.Text = this.dataGridViewAbsence.CurrentRow.Cells["mkb"].Value.ToString();
				this.textBoxAbsenceReasons.Text = this.dataGridViewAbsence.CurrentRow.Cells["reasons"].Value.ToString();
				this.textBoxAbsenceNAPDocs.Text = this.dataGridViewAbsence.CurrentRow.Cells["napdocs"].Value.ToString();
				this.textBoxAbsenceAdditionalDocs.Text = this.dataGridViewAbsence.CurrentRow.Cells["additionaldocs"].Value.ToString();
				this.textBoxAbsenceSicknessNumber.Text = this.dataGridViewAbsence.CurrentRow.Cells["sicknessnumber"].Value.ToString();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonHistory_Click(object sender, System.EventArgs e)
		{
			try
			{
				CommonNomenclature form = new CommonNomenclature(TableNames.YearHoliday, "Отпуски по години", this.dtYearHoliday, this.mainform, this.parent);
				//5formYearAdd form = new formYearAdd(this.parent, TableNames.YearHoliday, this.dtYearHoliday,	this.mainform);
				form.ShowDialog();

				this.CalculatetotalVacation();
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void comboBoxAbsenceTypeAbsence_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if (this.comboBoxAbsenceTypeAbsence.SelectedIndex != (int)AbsencePositions.YearlyPaidHoliday)
				{
					this.comboBoxAbsenceForYear.Enabled = false;
					if (this.comboBoxAbsenceTypeAbsence.SelectedIndex == (int)AbsencePositions.Cancellation)
					{
						this.numBoxAbsenceCalendarDays.Enabled = false;
						this.numBoxAbsenceWorkDays.Enabled = false;
						this.dateTimePickerAbsenceToData.Enabled = false;
						this.dateTimePickerAbsenceSicknessIssuedAtDate.Enabled = false;
						this.comboBoxAbsenceSicknessDuration.Enabled = false;
						this.textBoxAbsenceAdditionalDocs.Enabled = false;
						this.textBoxAbsenceAttachment7.Enabled = false;
						this.textBoxAbsenceDec39.Enabled = false;
						this.textBoxAbsenceMKB.Enabled = false;
						this.textBoxAbsenceReasons.Enabled = false;
						this.textBoxAbsenceNAPDocs.Enabled = false;
						this.textBoxAbsenceSicknessNumber.Enabled = false;
					}
					else if (this.comboBoxAbsenceTypeAbsence.SelectedIndex == (int)AbsencePositions.Sickness || (int)AbsencePositions.MotherhoodSickness == this.comboBoxAbsenceTypeAbsence.SelectedIndex)
					{
						this.numBoxAbsenceCalendarDays.Enabled = true;
						this.numBoxAbsenceWorkDays.Enabled = true;
						this.dateTimePickerAbsenceToData.Enabled = true;
						this.dateTimePickerAbsenceSicknessIssuedAtDate.Enabled = true;
						this.comboBoxAbsenceSicknessDuration.Enabled = true;
						this.textBoxAbsenceAdditionalDocs.Enabled = true;
						this.textBoxAbsenceAttachment7.Enabled = true;
						this.textBoxAbsenceDec39.Enabled = true;
						this.textBoxAbsenceMKB.Enabled = true;
						this.textBoxAbsenceReasons.Enabled = true;
						this.textBoxAbsenceNAPDocs.Enabled = true;
						this.textBoxAbsenceSicknessNumber.Enabled = true;
					}
					else
					{
						this.numBoxAbsenceCalendarDays.Enabled = true;
						this.numBoxAbsenceWorkDays.Enabled = true;
						this.dateTimePickerAbsenceToData.Enabled = true;
						this.dateTimePickerAbsenceSicknessIssuedAtDate.Enabled = false;
						this.comboBoxAbsenceSicknessDuration.Enabled = false;
						this.textBoxAbsenceAdditionalDocs.Enabled = false;
						this.textBoxAbsenceAttachment7.Enabled = false;
						this.textBoxAbsenceDec39.Enabled = false;
						this.textBoxAbsenceMKB.Enabled = false;
						this.textBoxAbsenceReasons.Enabled = false;
						this.textBoxAbsenceNAPDocs.Enabled = false;
						this.textBoxAbsenceSicknessNumber.Enabled = false;
					}
				}
				else
				{
					this.numBoxAbsenceCalendarDays.Enabled = true;
					this.numBoxAbsenceWorkDays.Enabled = true;
					this.comboBoxAbsenceForYear.Enabled = true;
					this.dateTimePickerAbsenceToData.Enabled = true;
					this.dateTimePickerAbsenceSicknessIssuedAtDate.Enabled = false;
					this.comboBoxAbsenceSicknessDuration.Enabled = false;
					this.textBoxAbsenceAdditionalDocs.Enabled = false;
					this.textBoxAbsenceAttachment7.Enabled = false;
					this.textBoxAbsenceDec39.Enabled = false;
					this.textBoxAbsenceMKB.Enabled = false;
					this.textBoxAbsenceReasons.Enabled = false;
					this.textBoxAbsenceNAPDocs.Enabled = false;
					this.textBoxAbsenceSicknessNumber.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void CalculatetotalVacation()
		{
			try
			{
				int sum = 0;
				foreach (DataRow row in this.dtYearHoliday.Rows)
				{
					try
					{
						sum += (int)row["leftover"];
					}
					catch (FormatException)
					{
					}
				}
				this.labelVacationLeft.Text = sum.ToString();
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonAbsenceExcel_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.dataGridViewAbsence.Rows.Count > 0)
				{
					ExcelExpo Ex = new ExcelExpo();
					DataView vue = new DataView(this.dtAbsence, "", "", DataViewRowState.CurrentRows);
					Ex.ExportView(this.dataGridViewAbsence, vue, "Отсъствия на " + this.textBoxNames.Text);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void dateTimePickerAbsenceFromData_ValueChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.comboBoxAbsenceTypeAbsence.SelectedIndex == (int)AbsencePositions.Cancellation)
				{
					this.numBoxAbsenceCalendarDays.Text = "0";
					this.numBoxAbsenceWorkDays.Text = "0";
				}
				else
				{

					DateTime DateStart, DateEnd;
					DateStart = new DateTime(this.dateTimePickerAbsenceFromData.Value.Year, this.dateTimePickerAbsenceFromData.Value.Month, this.dateTimePickerAbsenceFromData.Value.Day);

					DateEnd = new DateTime(this.dateTimePickerAbsenceToData.Value.Year, this.dateTimePickerAbsenceToData.Value.Month, this.dateTimePickerAbsenceToData.Value.Day);
					if (DateStart > DateEnd)
						return;

					int workdays = HolidayPlan.CalendarRow.GetCountWorkDays(DateStart, DateEnd, mainform.EntityConectionString);
					TimeSpan span = DateEnd.Subtract(DateStart);
					int caldays = span.Days + 1;

					this.numBoxAbsenceCalendarDays.Text = caldays.ToString();
					this.numBoxAbsenceWorkDays.Text = workdays.ToString();
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}
		#endregion

		#region Penalty functions

		private void buttonPenaltyAdd_Click(object sender, System.EventArgs e)
		{
			try
			{
				foreach (TabPage tp in this.tabControlCardNew.TabPages)
				{
					if (this.tabPagePenalty != tp)
					{
						tp.Enabled = false;
					}
				}

				Op = Operations.AddPenalty;
				if (dataGridViewPenalties.CurrentRow != null)
					this.dataGridViewPenalties.ClearSelection();
				this.EnableButtons(false, false, true, false, false, true, LockButtons.Penalty);
				//this.ClearControls( false );
				this.ControlEnabled(true, LockButtons.Penalty);

				this.dateTimePickerPenaltyFromDate.Value = DateTime.Now;
				this.dateTimePickerPenaltyToDate.Value = DateTime.Now;
				this.dateTimePickerPenaltyOrderDate.Value = DateTime.Now;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonPebaltyEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				Op = Operations.EditPenalty;
				if (this.dataGridViewPenalties.CurrentRow != null)
				{
					this.EnableButtons(false, false, true, false, false, true, LockButtons.Penalty);
					this.ControlEnabled(true, LockButtons.Penalty);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonPenaltyCancel_Click(object sender, System.EventArgs e)
		{
			try
			{
				foreach (TabPage tp in this.tabControlCardNew.TabPages)
				{
					tp.Enabled = true;
				}
				if (Op == Operations.AddPenalty)  // Трбва да се провери преди смяната на операцията
				{
					this.textBoxPenaltyNumberOrder.Text = "";
					this.dateTimePickerPenaltyOrderDate.Text = "";
				}
				Op = Operations.ViewPersonData;
				this.ControlEnabled(false, LockButtons.Penalty);
				this.EnableButtons(true, true, false, true, true, false, LockButtons.Penalty);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonPenaltySave_Click(object sender, System.EventArgs e)
		{
			PenaltySave();
		}

		private bool PenaltySave()
		{
			try
			{
				bool result;
				Dictionary<string, object> Dict = new Dictionary<string, object>();
				result = this.ValidatePenaltyData(Dict);
				if (result == true)
				{
					foreach (TabPage tp in this.tabControlCardNew.TabPages)
					{
						tp.Enabled = true;
					}

					if (Op == Operations.AddPenalty)
					{
						Dictionary<string, object> nDict = new Dictionary<string, object>();
						int id;

						//nDict.Add("Date", DataAction.ConvertDateToMySqlU(DateTime.Now, mainForm.DataBaseTypes));
						nDict.Add("Date", DateTime.Now);
						if (this.radioButtonBonuses.Checked)
						{
							nDict.Add("Text", "Награден на " + this.dateTimePickerPenaltyFromDate.Text);
							nDict.Add("Type", "Награда");
						}
						else
						{
							nDict.Add("Text", "Наказан на " + this.dateTimePickerPenaltyFromDate.Text);
							nDict.Add("Type", "Наказание");
						}
						nDict.Add("TypeDocument", this.comboBoxTypePenalty.Text);
						nDict.Add("Par", this.parent.ToString());

						id = this.dataAdapter.UniversalInsertParam(TableNames.NotesTable, nDict, "id", TransactionComnmand.BEGIN_TRANSACTION);
						if (id > 0)
						{
							nDict.Add("ID", id.ToString());
							id = this.dataAdapter.UniversalInsertParam(TableNames.Penalty, Dict, "id", TransactionComnmand.COMMIT_TRANSACTION);
							if (id > 0)
							{
								Dict.Add("ID", id.ToString());
								this.AddDictToTable(Dict, this.dtPenalty);
								this.AddDictToTable(nDict, this.dtNotes);
							}
						}
					}
					else
					{
						DataRow row = this.dtPenalty.Rows.Find(this.dataGridViewPenalties.CurrentRow.Cells["id"].Value);
						if (row != null)
						{
							if (this.dataAdapter.UniversalUpdateParam(TableNames.Penalty, "id", Dict, row["id"].ToString(), TransactionComnmand.NO_TRANSACTION))
								this.UpdateDictToRow(Dict, row);
						}
					}
					Op = Operations.ViewPersonData;
					this.ControlEnabled(false, LockButtons.Penalty);
					this.EnableButtons(true, true, false, true, true, false, LockButtons.Penalty);
					this.Refresh();
				}
				return result;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		private void buttonPenaltyDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.dataGridViewPenalties.CurrentRow != null)
				{
					if (MessageBox.Show(this, "Сигурни ли сте че искате да изтриете наказанието " + this.dataGridViewPenalties.CurrentRow.Cells["typepenalty"].Value.ToString(), "Изтриване", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						this.dataAdapter.UniversalDelete(TableNames.Penalty, this.dataGridViewPenalties.CurrentRow.Cells["id"].Value.ToString(), "id");
						dtPenalty.Rows.Remove(this.dtPenalty.Rows.Find(this.dataGridViewPenalties.CurrentRow.Cells["id"].Value));
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private bool ValidatePenaltyData(Dictionary<string, object> Dict)
		{
			try
			{
				Dict.Add("Parent", this.parent.ToString());
				Dict.Add("ModifiedByUser", this.User);
				if (this.textBoxPenaltyNumberOrder.Text == "")
				{
					Dict.Add("NumberOrder", "");
				}
				else
				{
					try
					{
						Dict.Add("NumberOrder", this.textBoxPenaltyNumberOrder.Text);
					}
					catch
					{
						Dict.Add("NumberOrder", "");
					}
				}
				if (this.comboBoxPenaltyReason.SelectedIndex <= 0)
				{
					Dict.Add("Reason", " ");
				}
				else
				{
					Dict.Add("Reason", this.comboBoxPenaltyReason.Text);
				}

				if (this.comboBoxTypePenalty.SelectedIndex <= 0)
				{
					Dict.Add("TypePenalty", " ");
				}
				else
				{
					Dict.Add("TypePenalty", this.comboBoxTypePenalty.Text);
				}

				//Dict.Add("OrderDate", DataAction.ConvertDateToMySqlU(this.dateTimePickerPenaltyOrderDate.Value, mainForm.DataBaseTypes));
				//Dict.Add("FromDate", DataAction.ConvertDateToMySqlU(this.dateTimePickerPenaltyFromDate.Value, mainForm.DataBaseTypes));
				//Dict.Add("ToDate", DataAction.ConvertDateToMySqlU(this.dateTimePickerPenaltyToDate.Value, mainForm.DataBaseTypes));
				Dict.Add("OrderDate", this.dateTimePickerPenaltyOrderDate.Value);
				Dict.Add("FromDate", this.dateTimePickerPenaltyFromDate.Value);
				Dict.Add("ToDate", this.dateTimePickerPenaltyToDate.Value);

				if (this.radioButtonBonuses.Checked)
				{
					Dict.Add("IsBonus", true.ToString());
				}
				else
				{
					Dict.Add("IsBonus", false.ToString());
				}
				return true;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		private void RefreshPenaltyDataSource(bool IsFormLoad)
		{
			try
			{
				this.dtPenalty = this.dataAdapter.SelectWhere(TableNames.Penalty, "*", " WHERE parent = " + this.parent);
				if (this.dtPenalty == null)
				{
					MessageBox.Show("Грешка при зареждане на таблицата за наказания и награди", ErrorMessages.NoConnection);
					this.Close();
				}
				this.radioButtonPenalties.Checked = true;
				this.vuePenalty = new DataView(this.dtPenalty, "isBonus = 'false'", "id", DataViewRowState.CurrentRows);

				TabPage tab = this.tabControlCardNew.SelectedTab;
				this.tabControlCardNew.SelectedTab = this.tabControlCardNew.TabPages["TabpagePenalty"];
				if (this.tabControlCardNew.SelectedTab != null)
				{
					this.dtPenalty.PrimaryKey = new DataColumn[] { this.dtPenalty.Columns["ID"] };
					this.dataGridViewPenalties.DataSource = this.vuePenalty;
					this.dataGridViewPenalties.ClearSelection();

					this.dtPenalty.TableName = TableNames.Penalty;
					JustifyGridView(this.dataGridViewPenalties, TableNames.Compare(TableNames.Penalty));

					this.textBoxPenaltyNumberOrder.Text = "";
					this.dateTimePickerPenaltyOrderDate.Text = "";
				}
				this.tabControlCardNew.SelectedTab = tab;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void dataGridPenalty_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (dataGridViewPenalties.CurrentRow == null)
					return;

				int index = this.comboBoxTypePenalty.FindString(this.dataGridViewPenalties.CurrentRow.Cells["typepenalty"].Value.ToString());
				this.SetComboIndex(this.comboBoxTypePenalty, index);

				index = this.comboBoxPenaltyReason.FindString(this.dataGridViewPenalties.CurrentRow.Cells["reason"].Value.ToString());
				this.SetComboIndex(this.comboBoxPenaltyReason, index);

				this.textBoxPenaltyNumberOrder.Text = this.dataGridViewPenalties.CurrentRow.Cells["numberorder"].Value.ToString();

				this.dateTimePickerPenaltyFromDate.Value = (DateTime)this.dataGridViewPenalties.CurrentRow.Cells["fromdate"].Value;
				this.dateTimePickerPenaltyToDate.Value = (DateTime)this.dataGridViewPenalties.CurrentRow.Cells["todate"].Value;
				this.dateTimePickerPenaltyOrderDate.Value = (DateTime)this.dataGridViewPenalties.CurrentRow.Cells["orderdate"].Value;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void radioButtonBonuses_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				DataRow rowGroupBoxPenalty = this.dtControlLabels.Rows.Find(this.groupBoxPenalty.Name);
				DataRow rowGroupBoxPenaltyGrid = this.dtControlLabels.Rows.Find(this.groupBoxPenaltyGrid.Name);
				DataRow rowButtonPenaltyReason = this.dtControlLabels.Rows.Find(this.buttonPenaltyReason.Name);
				DataRow rowButtonTypePenalty = this.dtControlLabels.Rows.Find(this.buttonTypePenalty.Name);

				string cond;
				if (this.radioButtonBonuses.Checked)
				{
					this.buttonPenaltyAdd.Text = "   Награда";
					this.tabPagePenalty.Text = "Награди";
					this.toolTip1.SetToolTip(this.buttonPenaltyAdd, "Въвеждане на нова награда");
					cond = "isbonus = 'true'"; //За награди
					this.vuePenalty = new DataView(this.dtPenalty, cond, "id", dvrs);


					if (rowGroupBoxPenalty != null)
					{
						try
						{
							this.groupBoxPenalty.Text = rowGroupBoxPenalty["alternate_text"].ToString();
						}
						catch
						{
							this.groupBoxPenalty.Text = "Данни за награда";
						}
					}
					else
					{
						this.groupBoxPenalty.Text = "Данни за награда";
					}

					if (rowGroupBoxPenaltyGrid != null)
					{
						try
						{
							this.groupBoxPenaltyGrid.Text = rowButtonPenaltyReason["alternate_text"].ToString();
						}
						catch
						{
							this.groupBoxPenaltyGrid.Text = "Данни за получени награди от служителя";
						}
					}
					else
					{
						this.groupBoxPenaltyGrid.Text = "Данни за получени награди от служителя";
					}

					if (rowButtonPenaltyReason != null)
					{
						try
						{
							this.toolTip1.SetToolTip(this.buttonPenaltyReason, rowButtonPenaltyReason["alternate_tooltip"].ToString());
						}
						catch
						{
							this.toolTip1.SetToolTip(this.buttonPenaltyReason, "Номенклатура основания награда");
						}
					}
					else
					{
						this.toolTip1.SetToolTip(this.buttonPenaltyReason, "Номенклатура основания награда");
					}

					if (rowButtonTypePenalty != null)
					{
						try
						{
							this.toolTip1.SetToolTip(this.buttonTypePenalty, rowButtonTypePenalty["alternate_tooltip"].ToString());
						}
						catch
						{
							this.toolTip1.SetToolTip(this.buttonTypePenalty, "Номенклатура видове награди");
						}
					}
					else
					{
						this.toolTip1.SetToolTip(this.buttonTypePenalty, "Номенклатура видове награди");
					}

					this.comboBoxPenaltyReason.DataSource = this.mainform.nomenclaatureData.arrBonusReason;
					this.comboBoxTypePenalty.DataSource = this.mainform.nomenclaatureData.arrTypeBonus;
				}
				else
				{
					this.buttonPenaltyAdd.Text = "   Наказание";
					this.tabPagePenalty.Text = "Наказания";
					this.toolTip1.SetToolTip(this.buttonPenaltyAdd, "Въвеждане на ново наказание");
					cond = "isbonus = 'false'"; //За награди
					this.vuePenalty = new DataView(this.dtPenalty, cond, "id", dvrs);

					if (rowGroupBoxPenalty != null)
					{
						this.groupBoxPenalty.Text = rowGroupBoxPenalty["client_text"].ToString();
					}
					else
					{
						this.groupBoxPenalty.Text = "Данни за наказание";
					}

					if (rowGroupBoxPenaltyGrid != null)
					{
						this.groupBoxPenaltyGrid.Text = rowButtonPenaltyReason["client_text"].ToString();
					}
					else
					{
						this.groupBoxPenaltyGrid.Text = "Данни за наложени наказания на служителя";
					}

					if (rowButtonPenaltyReason != null)
					{
						this.toolTip1.SetToolTip(this.buttonPenaltyReason, rowButtonPenaltyReason["tooltip"].ToString());
					}
					else
					{
						this.toolTip1.SetToolTip(this.buttonPenaltyReason, "Номенклатура основания наказание");
					}

					if (rowButtonTypePenalty != null)
					{
						this.toolTip1.SetToolTip(this.buttonTypePenalty, rowButtonTypePenalty["tooltip"].ToString());
					}
					else
					{
						this.toolTip1.SetToolTip(this.buttonTypePenalty, "Номенклатура видове наказания");
					}
					this.comboBoxPenaltyReason.DataSource = this.mainform.nomenclaatureData.arrPenaltyReason;
					this.comboBoxTypePenalty.DataSource = this.mainform.nomenclaatureData.arrTypePenalty;
				}
				this.dataGridViewPenalties.DataSource = this.vuePenalty;
				this.dataGridViewPenalties.ClearSelection();
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonPenaltiesExcel_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.dataGridViewPenalties.Rows.Count > 0)
				{
					ExcelExpo Ex = new ExcelExpo();
					Ex.ExportView(this.dataGridViewPenalties, (DataView)this.dataGridViewPenalties.DataSource, "Наказания на " + this.textBoxNames.Text);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}
		#endregion

		#region Rang functions

		private void buttonRangNew_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (TabPage tp in this.tabControlCardNew.TabPages)
				{
					if (this.tabPageMilitaryRang != tp)
					{
						tp.Enabled = false;
					}
				}

				Op = Operations.AddRang;
				if (dataGridViewRang.CurrentRow != null)
					this.dataGridViewRang.ClearSelection();
				this.EnableButtons(false, false, true, false, false, true, LockButtons.Rang);

				this.ControlEnabled(true, LockButtons.Rang);

				this.dateTimePickerRangDate.Value = DateTime.Now;
				this.dateTimePickerRangOrderDate.Value = DateTime.Now;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonRangEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				Op = Operations.EditRang;
				if (this.dataGridViewRang.CurrentRow != null)
				{
					this.EnableButtons(false, false, true, false, false, true, LockButtons.Rang);
					this.ControlEnabled(true, LockButtons.Rang);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonRangCancel_Click(object sender, System.EventArgs e)
		{
			try
			{
				foreach (TabPage tp in this.tabControlCardNew.TabPages)
				{
					tp.Enabled = true;
				}
				if (Op == Operations.AddRang)  // Трбва да се провери преди смяната на операцията
				{
					this.textBoxRangOrderNumber.Text = "";
					this.dateTimePickerRangDate.Text = "";
					this.dateTimePickerRangOrderDate.Text = "";
					this.comboBoxNSORang.Text = "";
				}
				Op = Operations.ViewPersonData;
				this.ControlEnabled(false, LockButtons.Rang);
				this.EnableButtons(true, true, false, true, true, false, LockButtons.Rang);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonRangSave_Click(object sender, System.EventArgs e)
		{
			RangSave();
		}

		private bool RangSave()
		{
			try
			{
				bool result;
				DataView vueRang;
				Dictionary<string, object> Dict = new Dictionary<string, object>();
				Dictionary<string, object> activeDict = new Dictionary<string, object>();
				Dictionary<string, object> personDict = new Dictionary<string, object>();
				result = this.ValidateRangData(Dict);
				if (result == true)
				{
					foreach (TabPage tp in this.tabControlCardNew.TabPages)
					{
						tp.Enabled = true;
					}

					if (Op == Operations.AddRang)
					{
						vueRang = new DataView(this.dtRang, "isactive = '1'", "id", DataViewRowState.CurrentRows);

						Dictionary<string, object> nDict = new Dictionary<string, object>();
						int id;

						//nDict.Add("Date", DataAction.ConvertDateToMySqlU(DateTime.Now, mainForm.DataBaseTypes));
						nDict.Add("Date", DateTime.Now);

						nDict.Add("Text", "Сменен ранг на " + this.dateTimePickerRangOrderDate.Text + " с ранг " + this.comboBoxNSORang.Text);
						nDict.Add("Type", "Военен ранг");

						nDict.Add("TypeDocument", "Военен ранг");
						nDict.Add("Par", this.parent.ToString());



						if (vueRang.Count > 0 && vueRang[0]["rangweight"].ToString() == Dict["RangWeight"].ToString())
						{
							Dict.Add("israngupdate", "0");
						}
						else
						{
							Dict.Add("israngupdate", "1");
						}

						id = this.dataAdapter.UniversalInsertParam(TableNames.NotesTable, nDict, "id", TransactionComnmand.BEGIN_TRANSACTION);
						if (id > 0)
						{
							Dict.Add("IsActive", "1");
							activeDict.Add("IsActive", "0");
							for (int r = 0; r < vueRang.Count; r++)
							{

								this.dataAdapter.UniversalUpdateParam(TableNames.MilitaryRang, "id", activeDict, vueRang[r]["id"].ToString(), TransactionComnmand.USE_TRANSACTION);
							} //deasctivate all old rangs

							nDict.Add("ID", id.ToString());
							personDict.Add("MilitaryRang", Dict["MilitaryRang"]);

							int idx = this.comboBoxMilitaryRang.FindString(Dict["MilitaryRang"].ToString());
							this.SetComboIndex(this.comboBoxMilitaryRang, idx);

							this.dataAdapter.UniversalUpdateParam(TableNames.Person, "id", personDict, this.parent.ToString(), TransactionComnmand.USE_TRANSACTION);

							id = this.dataAdapter.UniversalInsertParam(TableNames.MilitaryRang, Dict, "id", TransactionComnmand.COMMIT_TRANSACTION);
							if (id > 0)
							{
								Dict.Add("ID", id.ToString());
								this.AddDictToTable(Dict, this.dtRang);
								this.AddDictToTable(nDict, this.dtNotes);
							}
						}
					}
					else
					{
						DataRow row = this.dtRang.Rows.Find(this.dataGridViewRang.CurrentRow.Cells["id"].Value);
						if (row != null)
						{
							if (this.dataAdapter.UniversalUpdateParam(TableNames.MilitaryRang, "id", Dict, row["id"].ToString(), TransactionComnmand.NO_TRANSACTION))
								this.UpdateDictToRow(Dict, row);
						}
					}
					Op = Operations.ViewPersonData;
					this.ControlEnabled(false, LockButtons.Rang);
					this.EnableButtons(true, true, false, true, true, false, LockButtons.Rang);
					this.Refresh();
				}
				return result;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		private void buttonRangDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.dataGridViewRang.CurrentRow != null)
				{
					if (MessageBox.Show(this, "Сигурни ли сте че искате да изтриете повишението в ранг ", "Изтриване", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						this.dataAdapter.UniversalDelete(TableNames.MilitaryRang, this.dataGridViewRang.CurrentRow.Cells["id"].Value.ToString(), "id");
						this.dtRang.Rows.Remove(this.dtRang.Rows.Find(this.dataGridViewRang.CurrentRow.Cells["id"].Value));
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private bool ValidateRangData(Dictionary<string, object> Dict)
		{
			try
			{
				Dict.Add("Parent", this.parent.ToString());
				Dict.Add("ModifiedByUser", this.User);

				if (this.comboBoxNSORang.SelectedIndex < 0)
				{
					Dict.Add("MilitaryRang", " ");
					Dict.Add("RangWeight", "0");
					Dict.Add("MilitaryDegree", " ");
				}
				else
				{
					Dict.Add("MilitaryRang", this.comboBoxNSORang.Text);
					Dict.Add("MilitaryDegree", this.comboBoxNSODegree.Text);
					string weight = this.mainform.nomenclaatureData.dtMilitaryRang.Rows[this.comboBoxNSORang.SelectedIndex]["englevel"].ToString(); //get rang weight form here
					if (weight == "")
					{
						Dict.Add("RangWeight", "0");
					}
					else
					{
						Dict.Add("RangWeight", weight);
					}
				}

				if (this.textBoxRangOrderNumber.Text == "")
				{
					Dict.Add("RangOrderNumber", "");
				}
				else
				{
					try
					{
						Dict.Add("RangOrderNumber", this.textBoxRangOrderNumber.Text);
					}
					catch
					{
						Dict.Add("RangOrderNumber", "");
					}
				}

				//Dict.Add("RangOrderDate", DataAction.ConvertDateToMySqlU(this.dateTimePickerRangOrderDate.Value, mainForm.DataBaseTypes));
				//Dict.Add("RangOrderValidFrom", DataAction.ConvertDateToMySqlU(this.dateTimePickerRangValidFrom.Value, mainForm.DataBaseTypes));
				Dict.Add("RangOrderDate", this.dateTimePickerRangOrderDate.Value);
				Dict.Add("RangOrderValidFrom", this.dateTimePickerRangValidFrom.Value);
				if (this.idAssignment != 0)
				{
					Dict.Add("idassignment", this.idAssignment.ToString());
				}
				return true;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		private void RefreshRangDataSource(bool IsFormLoad)
		{
			try
			{
				this.idAssignment = 0;
				this.dtRang = this.dataAdapter.SelectWhere(TableNames.MilitaryRang, "*", " WHERE parent = " + this.parent);
				if (this.dtRang == null)
				{
					MessageBox.Show("Грешка при зареждане на таблицата за военни звания", ErrorMessages.NoConnection);
					this.Close();
					return;
				}

				TabPage tab = this.tabControlCardNew.SelectedTab;
				this.tabControlCardNew.SelectedTab = this.tabControlCardNew.TabPages["TabpageMilitaryRang"];
				if (this.tabControlCardNew.SelectedTab != null)
				{
					this.dtRang.PrimaryKey = new DataColumn[] { this.dtRang.Columns["ID"] };
					this.dataGridViewRang.DataSource = this.dtRang;
					this.dataGridViewRang.ClearSelection();

					this.dtRang.TableName = TableNames.MilitaryRang;
					JustifyGridView(this.dataGridViewRang, TableNames.Compare(TableNames.MilitaryRang));

					this.textBoxRangOrderNumber.Text = "";
					this.dateTimePickerRangOrderDate.Value = DateTime.Now;
					this.dateTimePickerRangValidFrom.Value = DateTime.Now;

					for (int i = 0; i < this.dtRang.Rows.Count; i++)
					{
						if (this.dtRang.Rows[i]["isactive"].ToString() == "1")
						{
							this.dataGridViewRang.CurrentCell = this.dataGridViewRang.Rows[i].Cells["militaryrang"];
							this.dataGridViewRang.Rows[i].Selected = true;
							this.dataGridRang_Click(this, null);
							break;
						}
					}
				}
				this.tabControlCardNew.SelectedTab = tab;


			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void dataGridRang_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (dataGridViewRang.CurrentRow == null)
					return;

				int index = this.comboBoxNSORang.FindString(this.dataGridViewRang.CurrentRow.Cells["militaryrang"].Value.ToString());
				this.SetComboIndex(this.comboBoxNSORang, index);

				index = this.comboBoxNSODegree.FindString(this.dataGridViewRang.CurrentRow.Cells["militarydegree"].Value.ToString());
				this.SetComboIndex(this.comboBoxNSODegree, index);

				this.textBoxRangOrderNumber.Text = this.dataGridViewRang.CurrentRow.Cells["rangordernumber"].Value.ToString();

				this.dateTimePickerRangOrderDate.Value = (DateTime)this.dataGridViewRang.CurrentRow.Cells["rangorderdate"].Value;
				this.dateTimePickerRangValidFrom.Value = (DateTime)this.dataGridViewRang.CurrentRow.Cells["rangordervalidfrom"].Value;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonRangExcel_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.dataGridViewRang.Rows.Count > 0)
				{
					ExcelExpo Ex = new ExcelExpo();
					DataView vue = new DataView((DataTable)this.dataGridViewRang.DataSource, "1 = 1", "id", DataViewRowState.CurrentRows);
					Ex.ExportView(this.dataGridViewRang, vue, "Звания на " + this.textBoxNames.Text);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonMilitaryAssignmentLink_Click(object sender, EventArgs e)
		{
			FormChoose form = new FormChoose(this.dtAssignment, "Избор на назначение");
			if (this.Op == Operations.EditRang)
			{
				for (int i = 0; i < this.dtAssignment.Rows.Count; i++)
				{
					if (this.dtAssignment.Rows[i]["id"].ToString() == this.dataGridViewRang.CurrentRow.Cells["idassignment"].Value.ToString()) //select the current row
					{
						form.dataGridView1.CurrentCell = form.dataGridView1.Rows[i].Cells["id"];
						form.dataGridView1.Rows[i].Selected = true;
						break;
					}
					else
					{
						form.dataGridView1.ClearSelection();
					}
				}
			}
			else
			{
				form.dataGridView1.ClearSelection();
			}
			if (form.ShowDialog() == DialogResult.OK)
			{
				int idas;
				if (int.TryParse(form.dataGridView1.CurrentRow.Cells["id"].Value.ToString(), out idas))
				{
					this.idAssignment = idas;
				}
			}
		}

		#endregion

		#region Card functions

		private void buttonCardNew_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (TabPage tp in this.tabControlCardNew.TabPages)
				{
					if (this.tabPageCards != tp)
					{
						tp.Enabled = false;
					}
				}

				Op = Operations.AddCard;
				if (dataGridViewCards.CurrentRow != null)
					this.dataGridViewCards.ClearSelection();
				this.EnableButtons(false, false, true, false, false, true, LockButtons.Card);

				this.ControlEnabled(true, LockButtons.Card);

				this.dateTimePickerCardIssue.Value = DateTime.Now;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonCardEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				Op = Operations.EditCard;
				if (this.dataGridViewCards.CurrentRow != null)
				{
					this.EnableButtons(false, false, true, false, false, true, LockButtons.Card);
					this.ControlEnabled(true, LockButtons.Card);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonCardCancel_Click(object sender, System.EventArgs e)
		{
			try
			{
				foreach (TabPage tp in this.tabControlCardNew.TabPages)
				{
					tp.Enabled = true;
				}
				if (Op == Operations.AddCard)  // Трбва да се провери преди смяната на операцията
				{
					this.textBoxCardNumber.Text = "";
					this.textBoxCardSeries.Text = "";
					this.textBoxCardSign.Text = "";
					this.dateTimePickerCardIssue.Text = "";
					this.comboBoxCardMilitaryRang.Text = "";
					this.comboBoxCardMilitaryRangEng.Text = "";
				}
				Op = Operations.ViewPersonData;
				this.ControlEnabled(false, LockButtons.Card);
				this.EnableButtons(true, true, false, true, true, false, LockButtons.Card);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonCardSave_Click(object sender, System.EventArgs e)
		{
			CardSave();
		}

		private bool CardSave()
		{
			try
			{
				bool result;
				DataView vueRang;
				Dictionary<string, object> Dict = new Dictionary<string, object>();
				Dictionary<string, object> activeDict = new Dictionary<string, object>();
				Dictionary<string, object> personDict = new Dictionary<string, object>();

				foreach (TabPage tp in this.tabControlCardNew.TabPages)
				{
					tp.Enabled = true;
				}

				var data = new Entities(mainform.EntityConectionString);

				if (Op == Operations.AddCard)
				{
					var currentCards = data.HR_Cards.Where(a => a.parent == this.parent).ToList();
					foreach (var car in currentCards)
					{
						car.isactive = false;
					}
					data.SaveChanges();
					var card = new HR_Cards();
					card.parent = this.parent;
					card.isactive = true;
					card.MilitaryDegree = this.comboBoxCardMilitaryRang.Text;
					card.MilitaryDegreeEng = this.comboBoxCardMilitaryRangEng.Text;
					card.CardIssueDate = this.dateTimePickerCardIssue.Value;
					card.CardNumber = this.textBoxCardNumber.Text;
					card.CardSeries = this.textBoxCardSeries.Text;
					card.CardSign = this.textBoxCardSign.Text;
					data.HR_Cards.AddObject(card);
					try
					{
						data.SaveChanges();

					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "Грешка при добавяне на данните");
						return false;
					}
				}
				else
				{
					int cid = (int)this.dataGridViewCards.CurrentRow.Cells["id"].Value;
					var card = data.HR_Cards.FirstOrDefault(a => a.id == cid);
					if (card == null)
					{
						MessageBox.Show("Грешка при актуализация на данните");
						return false;
					}
					else
					{
						card.MilitaryDegree = this.comboBoxCardMilitaryRang.Text;
						card.MilitaryDegreeEng = this.comboBoxCardMilitaryRangEng.Text;
						card.CardIssueDate = this.dateTimePickerCardIssue.Value;
						card.CardNumber = this.textBoxCardNumber.Text;
						card.CardSeries = this.textBoxCardSeries.Text;
						card.CardSign = this.textBoxCardSign.Text;
						try
						{
							data.SaveChanges();
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message, "Грешка при актуализация на данните");
							return false;
						}
					}
				}
				Op = Operations.ViewPersonData;
				this.ControlEnabled(false, LockButtons.Card);
				this.EnableButtons(true, true, false, true, true, false, LockButtons.Card);
				this.Refresh();
				this.RefreshCardDataSource(false);

				return true;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		private void buttonCardDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.dataGridViewCards.CurrentRow != null)
				{
					if (MessageBox.Show(this, "Сигурни ли сте че искате да изтриете данните за служебна карта ", "Изтриване", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						this.dataAdapter.UniversalDelete(TableNames.Cards, this.dataGridViewCards.CurrentRow.Cells["id"].Value.ToString(), "id");
						this.dtCards.Rows.Remove(this.dtCards.Rows.Find(this.dataGridViewCards.CurrentRow.Cells["id"].Value));

						var data = new Entities(mainform.EntityConectionString);
						var lstCards = data.HR_Cards.Where(a => a.parent == this.parent).ToList().OrderBy(a => a.CardIssueDate).ToList();
						if (lstCards.Any(a => a.isactive == true) || lstCards.Count == 0)
						{
							return;
						}
						var card = lstCards.LastOrDefault();
						if (card != null)
						{
							card.isactive = true;
							try
							{
								data.SaveChanges();
							}
							catch (Exception ex)
							{
								MessageBox.Show(ex.Message,
									"Грешка при активиране на по-стара карта. Служителя остава без активна карта.");
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void RefreshCardDataSource(bool IsFormLoad)
		{
			try
			{
				this.idAssignment = 0;
				this.dtCards = this.dataAdapter.SelectWhere(TableNames.Cards, "*", " WHERE parent = " + this.parent);
				if (this.dtCards == null)
				{
					MessageBox.Show("Грешка при зареждане на таблицата за служебни карти", ErrorMessages.NoConnection);
					this.Close();
					return;
				}

				TabPage tab = this.tabControlCardNew.SelectedTab;
				this.tabControlCardNew.SelectedTab = this.tabControlCardNew.TabPages["TabpageCards"];
				if (this.tabControlCardNew.SelectedTab != null)
				{
					this.dtCards.PrimaryKey = new DataColumn[] { this.dtCards.Columns["ID"] };
					this.dataGridViewCards.DataSource = this.dtCards;
					this.dataGridViewCards.ClearSelection();

					this.dtCards.TableName = TableNames.Cards;
					JustifyGridView(this.dataGridViewCards, TableNames.Compare(TableNames.Cards));

					this.textBoxCardNumber.Text = "";
					this.textBoxCardSeries.Text = "";
					this.textBoxCardSign.Text = "";
					this.dateTimePickerCardIssue.Text = "";
					this.comboBoxCardMilitaryRang.Text = "";
					this.comboBoxCardMilitaryRangEng.Text = "";

					for (int i = 0; i < this.dtCards.Rows.Count; i++)
					{
						if ((bool)this.dtCards.Rows[i]["isactive"] == true)
						{
							this.dataGridViewCards.CurrentCell = this.dataGridViewCards.Rows[i].Cells["cardnumber"];
							this.dataGridViewCards.Rows[i].Selected = true;
							this.dataGridCard_Click(this, null);
							break;
						}
					}
				}
				this.tabControlCardNew.SelectedTab = tab;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void dataGridCard_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (dataGridViewCards.CurrentRow == null)
					return;

				int index = this.comboBoxCardMilitaryRang.FindString(this.dataGridViewCards.CurrentRow.Cells["militarydegree"].Value.ToString());
				this.SetComboIndex(this.comboBoxCardMilitaryRang, index);

				index = this.comboBoxCardMilitaryRangEng.FindString(this.dataGridViewCards.CurrentRow.Cells["militarydegreeeng"].Value.ToString());
				this.SetComboIndex(this.comboBoxCardMilitaryRangEng, index);

				this.textBoxCardNumber.Text = this.dataGridViewCards.CurrentRow.Cells["cardnumber"].Value.ToString();
				this.textBoxCardSeries.Text = this.dataGridViewCards.CurrentRow.Cells["cardseries"].Value.ToString();
				this.textBoxCardSign.Text = this.dataGridViewCards.CurrentRow.Cells["cardsign"].Value.ToString();

				this.dateTimePickerCardIssue.Value = (DateTime)this.dataGridViewCards.CurrentRow.Cells["cardissuedate"].Value;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonCardExcel_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.dataGridViewRang.Rows.Count > 0)
				{
					ExcelExpo Ex = new ExcelExpo();
					DataView vue = new DataView((DataTable)this.dataGridViewRang.DataSource, "1 = 1", "id", DataViewRowState.CurrentRows);
					Ex.ExportView(this.dataGridViewRang, vue, "Звания на " + this.textBoxNames.Text);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}
		#endregion

		#region Personal functions

		private void PersonalDataChanged(object sender, System.EventArgs e)
		{
			this.PersonalDataChangedValue = true;
		}

		private void numBoxEgn_TextChanged(object sender, EventArgs e)
		{
			try
			{
				int boll = 0;
				this.PersonalDataChangedValue = true;
				DataLayer.DataAction daa = new DataLayer.DataAction(this.mainform.connString);
				if (this.numBoxEgn.Text.Length == 10)
				{
					int.TryParse(this.mainform.nomenclaatureData.dtOptions.Rows[0]["firedsignal"].ToString(), out boll);
					DataTable dtp = daa.SelectWhere(TableNames.Person, "*", "WHERE egn = '" + this.numBoxEgn.Text + "'");
					if (dtp != null)
					{
						if ((dtp.Rows.Count > 0) && (dtp.Rows[0]["id"].ToString() != this.parent.ToString()) && (this.Op == Operations.AddNewPerson))
						{
							if (MessageBox.Show("Лице с ЕГН " + this.numBoxEgn.Text + " вече фигурира в регистъра. Искате ли да заредите данните му автоматично?", "Въпрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
							{
								this.parent = (int)dtp.Rows[0]["id"];
								LoadPersonalData();
								this.parent = 0;
							}
						}
					}
					try
					{
						this.dateTimePickerBirthDate.Value = new DateTime(int.Parse(this.numBoxEgn.Text.Substring(0, 2)) + 1900, int.Parse(this.numBoxEgn.Text.Substring(2, 2)), int.Parse(this.numBoxEgn.Text.Substring(4, 2)));
					}
					catch (Exception)
					{
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private bool validateSyscoID()
		{
			try
			{
				DataTable dtp = this.dataAdapter.SelectWhere(TableNames.Person, "*", "WHERE id_sysco = '" + this.textBoxOther1.Text + "'");
				if (dtp != null)
				{
					if ((dtp.Rows.Count > 0) && (dtp.Rows[0]["id"].ToString() != this.parent.ToString()))
					{
						MessageBox.Show("Сискосет номер " + this.textBoxOther1.Text + " вече фигурира в регистъра. Данните няма да бъдат записани. Моля, коригирайте го и опитайте отново.");
						return false;
					}
				}
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		private void comboBoxEGN_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				this.PersonalDataChangedValue = true;
				this.labelEGN.Text = this.comboBoxEGN.Text + ":";
				if (this.comboBoxEGN.SelectedIndex == 1)
				{
					this.dateTimePickerBirthDate.Enabled = true;
				}
				else
				{
					this.dateTimePickerBirthDate.Enabled = false;
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private bool ValidatePerson(Dictionary<string, object> Dict)
		{
			try
			{
				if (this.numBoxEgn.Text == "")
				{
					MessageBox.Show("Необходимо е да въведете ЕГН на лицето. \n Личните данни няма да бъдат записани.", "Грешка при въвеждане");
					return false;
				}
				else
				{
					try
					{
						Dict.Add("Egn", this.numBoxEgn.Text);
						if (this.comboBoxEGN.SelectedIndex == 0)
						{
							Dict.Add("bornDate", new DateTime(int.Parse(Dict["Egn"].ToString().Substring(0, 2)) + 1900, int.Parse(Dict["Egn"].ToString().Substring(2, 2)), int.Parse(Dict["Egn"].ToString().Substring(4, 2))));
						}
						else
						{
							Dict.Add("bornDate", this.dateTimePickerBirthDate.Value);
						}
					}
					catch (System.ArgumentOutOfRangeException)
					{
						MessageBox.Show("Въведеното ЕГН е некоректно. \n Личните данни няма да бъдат записани.", "Грешка при въвеждане");
						return false;
					}
				}
				if (this.textBoxNames.Text == "")
				{
					MessageBox.Show("Необходимо е да въведете име на лицето. \n Личните данни няма да бъдат записани.", "Грешка при въвеждане");
					return false;
				}
				Dict.Add("Name", this.textBoxNames.Text);

				if (this.textBoxBornTown.Text == "")
				{
					Dict.Add("BornTown", " ");
				}
				else
				{
					Dict.Add("BornTown", this.textBoxBornTown.Text);
				}

				Dict.Add("ModifiedByUser", this.User);
				Dict.Add("DiplomDate", this.textBoxDiplom.Text);
				if (this.comboBoxEducation.SelectedIndex == -1)
				{
					Dict.Add("Education", "");
					Dict.Add("EngEducation", "");
				}
				else
				{
					DataRow R;
					Dict.Add("Education", this.comboBoxEducation.Text);
					if (this.comboBoxEducation.SelectedItem is DataRowView)
					{
						R = ((DataRowView)this.comboBoxEducation.SelectedItem).Row;
						Dict.Add("EngEducation", R["englevel"].ToString());
					}
					else
					{
						Dict.Add("EngEducation", "");
					}
				}

				if (this.comboBoxFamilyStatus.SelectedIndex == -1)
				{
					Dict.Add("FamilyStatus", "");
				}
				else
				{
					Dict.Add("FamilyStatus", this.comboBoxFamilyStatus.SelectedItem.ToString());
				}

				if (this.radioButtonAssignment.Checked)
				{
					Dict.Add("HiredAt", this.dateTimePickerPostypilNa.Value);
				}

				Dict.Add("Kwartal", this.textBoxKwartal.Text);
				Dict.Add("Street", this.textBoxCurrentAddress.Text);
				Dict.Add("Country", this.textBoxCountry.Text);

				if (this.comboBoxMilitaryRang.SelectedIndex == -1)
				{
					Dict.Add("MilitaryRang", "");
				}
				else
				{
					object Item = this.comboBoxMilitaryRang.SelectedItem;
					if (Item is DataRowView)
					{
						DataRowView r = (DataRowView)Item;
						if (r["level"].ToString() != "")
							Dict.Add("MilitaryRang", r["level"].ToString());
					}
					else
					{
						Dict.Add("MilitaryRang", this.comboBoxMilitaryRang.SelectedItem.ToString());
					}
				}

				if (this.comboBoxMilitaryStatus.SelectedIndex == -1)
				{
					Dict.Add("MilitaryStatus", "");
				}
				else
				{
					Dict.Add("MilitaryStatus", this.comboBoxMilitaryStatus.SelectedItem.ToString());
				}

				Dict.Add("PCard", this.numBoxPcCard.Text);

				Dict.Add("PCardPublish", this.dateTimePickerPCCardPublished.Value);

				Dict.Add("pcardExpiry", this.dateTimePickerPCardExpiry.Value);

				Dict.Add("PublishedBy", this.textBoxPublishedFrom.Text);
				Dict.Add("Region", this.textBoxRegion.Text);

				if (this.comboBoxScienceLevel.SelectedIndex == -1)
				{
					Dict.Add("ScienceLevel", "");
				}
				else
				{
					Dict.Add("ScienceLevel", this.comboBoxScienceLevel.SelectedItem.ToString());
				}

				if (this.comboBoxScience.SelectedIndex == -1)
				{
					Dict.Add("ScienceTitle", "");
				}
				else
				{
					Dict.Add("ScienceTitle", this.comboBoxScience.SelectedItem.ToString());
				}

				Dict.Add("Phone", this.textBoxTelephone.Text);
				Dict.Add("Town", this.textBoxTown.Text);

				if (this.comboBoxSex.SelectedIndex == -1)
				{
					Dict.Add("Sex", "");
				}
				else
				{
					Dict.Add("Sex", this.comboBoxSex.SelectedItem.ToString());
				}

				if (this.comboBoxReceivedAddon.SelectedIndex == -1)
				{
					Dict.Add("ReceivedAddon", "Неполучени");
				}
				else
				{
					Dict.Add("ReceivedAddon", this.comboBoxReceivedAddon.Text);
				}

				if (this.comboBoxSpecialSkills.SelectedIndex == -1)
				{
					Dict.Add("languages", "");
				}
				else
				{
					Dict.Add("languages", this.comboBoxSpecialSkills.Text);
				}

				Dict.Add("Rang", this.comboBoxRang.Text);

				Dict.Add("Speciality", this.textBoxSpeciality.Text);
				Dict.Add("Other", this.textBoxOther.Text);
				Dict.Add("EgnLnch", this.comboBoxEGN.SelectedIndex.ToString());
				Dict.Add("EngName", this.textBoxEngName.Text);
				Dict.Add("Other1", this.textBoxOther1.Text);
				Dict.Add("Other2", this.textBoxOther2.Text);
				Dict.Add("Other3", this.textBoxOther3.Text);
				Dict.Add("Other4", this.textBoxOther4.Text);
				Dict.Add("Other5", this.textBoxWorkBook.Text);
				Dict.Add("workbook", this.textBoxWorkBook.Text);
				Dict.Add("workbookdate", this.dateTimePickerWorkBook.Value);

				int y = 0, m = 0, d = 0;

				int.TryParse(this.numBoxExpTotalY.Text, out y);
				int.TryParse(this.numBoxExpTotalM.Text, out m);
				int.TryParse(this.numBoxExpTotalD.Text, out d);

				Dict.Add("TotalExpY", y);
				Dict.Add("TotalExpM", m);
				Dict.Add("TotalExpD", d);

				if (this.textBoxResponsibleFor.Text != "")
				{
					Dict.Add("responsiblefor", this.textBoxResponsibleFor.Text);
				}
				return true;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		private void buttonPicture_Click(object sender, System.EventArgs e)
		{
			try
			{
				string fileName;
				long m_lImageFileLength;
				byte[] m_barrImg;
				FileStream fs;
				OpenFileDialog of = new OpenFileDialog();
				try
				{

					if (of.ShowDialog() == DialogResult.OK)
					{
						fileName = of.FileName;
						FileInfo fiImage = new FileInfo(fileName);
						m_lImageFileLength = fiImage.Length;
						if (m_lImageFileLength > 1000000)
						{
							MessageBox.Show("Файлът е по-голям от допустимия размер! ");
							fiImage = null;
							return;
						}

						this.pictureBox1.Image = Image.FromFile(fileName);

						fs = new FileStream(fileName, FileMode.Open,
							FileAccess.Read, FileShare.Read);
						m_barrImg = new byte[Convert.ToInt32(m_lImageFileLength)];
						int iBytesRead = fs.Read(m_barrImg, 0,
							Convert.ToInt32(m_lImageFileLength));
						fs.Close();

						DataTable dtp = new DataTable();
						dtp = this.dataAdapter.SelectWhere(TableNames.Pictures, "*", " WHERE parent = '" + this.parent + "'");
						if (dtp == null)
						{
							MessageBox.Show("Грешка при зареждане на снимка", ErrorMessages.NoConnection);
							this.Close();
						}
						if (dtp.Rows.Count > 0)
						{
							this.dataAdapter.UpdatePicture(TableNames.Pictures, this.parent, m_barrImg);
						}
						else
						{
							this.dataAdapter.InsertPicture(TableNames.Pictures, this.parent, m_barrImg);
						}
						m_barrImg = null;
						fs = null;
					}
				}
				catch (Exception ex)
				{
					ErrorLog.WriteException(ex, ex.Message);
					m_barrImg = null;
					fs = null;
					//fs.Close();
					MessageBox.Show(ex.Message);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonDeletePicture_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.dataAdapter.UniversalDelete(TableNames.Pictures, this.parent.ToString(), "parent");
				this.pictureBox1.Image = null;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void RefreshLangugeDataSource(bool IsFormLoad)
		{
			try
			{
				this.dtLanguages = this.dataAdapter.SelectWhere(TableNames.LanguageLevel, "*", " WHERE parent = '" + this.parent + "'");
				if (this.dtNotes == null)
				{
					MessageBox.Show("Грешка при зареждане на данни за чужди езици", ErrorMessages.NoConnection);
					this.Close();
				}
				this.dtLanguages.PrimaryKey = new DataColumn[] { this.dtLanguages.Columns["ID"] };

				this.dtLanguages.TableName = TableNames.LanguageLevel;
				TabPage tab = this.tabControlCardNew.SelectedTab;
				this.tabControlCardNew.SelectedTab = this.tabControlCardNew.TabPages[0];

				this.dataGridViewLanguages.DataSource = this.dtLanguages;
				this.dataGridViewLanguages.ClearSelection();

				this.tabControlCardNew.SelectedTab = tab;

				JustifyGridView(dataGridViewLanguages, TableNames.Compare(TableNames.LanguageLevel));
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonLanguageEdit_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.dataGridViewLanguages.CurrentRow != null)
				{
					FormLanguage form = new FormLanguage();
					int index = 0;
					form.comboBoxLanguage.DataSource = this.mainform.nomenclaatureData.arrLanguages;
					form.comboBoxLanguageLevel.DataSource = this.mainform.nomenclaatureData.arrLanguageKnowledge;
					index = form.comboBoxLanguage.FindString(this.dataGridViewLanguages.CurrentRow.Cells["language"].Value.ToString());
					this.SetComboIndex(form.comboBoxLanguage, index);
					index = form.comboBoxLanguageLevel.FindString(this.dataGridViewLanguages.CurrentRow.Cells["level"].Value.ToString());
					this.SetComboIndex(form.comboBoxLanguageLevel, index);
					if (form.ShowDialog() == DialogResult.OK)
					{
						Dictionary<string, object> Dict = new Dictionary<string, object>();
						Dict.Add("language", form.comboBoxLanguage.Text);
						Dict.Add("level", form.comboBoxLanguageLevel.Text);
						if (this.dataAdapter.UniversalUpdateParam(TableNames.LanguageLevel, "id", Dict, this.dataGridViewLanguages.CurrentRow.Cells["id"].Value.ToString(), TransactionComnmand.NO_TRANSACTION))
						{
							this.dataGridViewLanguages.CurrentRow.Cells["language"].Value = form.comboBoxLanguage.Text;
							this.dataGridViewLanguages.CurrentRow.Cells["level"].Value = form.comboBoxLanguageLevel.Text;
						}
						else
						{
							MessageBox.Show("Грешка при редакция на език");
						}
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonLanguageAdd_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.parent > 0)
				{
					FormLanguage form = new FormLanguage();
					form.comboBoxLanguage.DataSource = this.mainform.nomenclaatureData.arrLanguages;
					form.comboBoxLanguageLevel.DataSource = this.mainform.nomenclaatureData.arrLanguageKnowledge;

					if (form.ShowDialog() == DialogResult.OK)
					{
						Dictionary<string, object> Dict = new Dictionary<string, object>();
						Dict.Add("language", form.comboBoxLanguage.Text);
						Dict.Add("level", form.comboBoxLanguageLevel.Text);
						Dict.Add("parent", this.parent.ToString());
						int id = this.dataAdapter.UniversalInsertParam(TableNames.LanguageLevel, Dict, "id", TransactionComnmand.NO_TRANSACTION);
						if (id > 0)
						{
							Dict.Add("ID", id.ToString());
							this.AddDictToTable(Dict, this.dtLanguages);
						}
						else
						{
							MessageBox.Show("Грешка при редакция на език");
						}
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonLanguageDelete_Click(object sender, EventArgs e)
		{
			if (this.dataGridViewLanguages.CurrentRow != null)
			{
				if (MessageBox.Show("Сигурни ли сте че искате да изтриете език " + this.dataGridViewLanguages.CurrentRow.Cells["language"].Value.ToString() + "?", "Въпрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					if (this.dataAdapter.UniversalDelete(TableNames.LanguageLevel, this.dataGridViewLanguages.CurrentRow.Cells["id"].Value.ToString(), "id"))
					{
						DataRow row = this.dtLanguages.Rows.Find(this.dataGridViewLanguages.CurrentRow.Cells["id"].Value);
						if (row != null)
							this.dtLanguages.Rows.Remove(row);
					}
					else
					{
						MessageBox.Show("Грешка при изтриване на език");
					}
				}
			}
		}

		#endregion

		#region Fired functions

		private void ValidateFiredData(Dictionary<string, object> Dict)
		{
			try
			{
				Dict.Add("Parent", this.parent.ToString());
				Dict.Add("ModifiedByUser", this.User);
				if (this.IsFiredEdit)
				{
					try
					{
						Dict.Add("ID", this.dataGridViewFired.CurrentRow.Cells["id"].Value.ToString());
					}
					catch (System.Exception ex)
					{
						MessageBox.Show(ex.Message, "Не може коректно да се определи идентификатора на реда");
						Dict.Add("ID", "0");
					}
				}

				if (this.comboBoxFiredReason.SelectedIndex <= 0)
				{
					Dict.Add("Reason", " ");
				}
				else
				{
					Dict.Add("Reason", this.comboBoxFiredReason.Text);
				}
				//Dict.Add("FromDate", DataAction.ConvertDateToMySqlU(this.dateTimePickerFiredFromDate.Value, mainForm.DataBaseTypes));
				Dict.Add("FromDate", this.dateTimePickerFiredFromDate.Value);
				Dict.Add("FireOrder", this.textBoxFireOrder.Text);
				//Dict.Add("FireOrderDate", DataAction.ConvertDateToMySqlU(this.dateTimePickerFireOdredDate.Value, mainForm.DataBaseTypes));
				Dict.Add("FireOrderDate", this.dateTimePickerFireOdredDate.Value);
				int cntd = 0;
				foreach (DataRow row in this.dtYearHoliday.Rows)
				{
					try
					{
						cntd += int.Parse(row["leftover"].ToString());
					}
					catch
					{
						cntd += 0;
					}
				}
				Dict.Add("CountDays", cntd.ToString());
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonFiredNew_Click(object sender, System.EventArgs e)
		{
			try
			{
				foreach (TabPage tp in this.tabControlCardNew.TabPages)
				{
					if (this.tabPageFired != tp)
					{
						tp.Enabled = false;
					}
				}

				Op = Operations.AddFired;
				if (dataGridViewFired.CurrentRow != null)
					this.dataGridViewFired.ClearSelection();
				this.IsFiredEdit = false;
				this.EnableButtons(false, false, true, false, false, true, LockButtons.Fired);
				//this.ClearControls( false );
				this.ControlEnabled(true, LockButtons.Fired);

				this.dateTimePickerFiredFromDate.Value = DateTime.Now;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonFiredEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				Op = Operations.EditFired;
				if (this.dataGridViewFired.CurrentRow != null)
				{
					IsFiredEdit = true;
					foreach (TabPage tp in this.tabControlCardNew.TabPages)
					{
						if (this.tabPageFired != tp)
						{
							tp.Enabled = false;
						}
					}
					this.EnableButtons(false, false, true, false, false, true, LockButtons.Fired);
					this.ControlEnabled(true, LockButtons.Fired);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonFiredCancel_Click(object sender, System.EventArgs e)
		{
			try
			{
				foreach (TabPage tp in this.tabControlCardNew.TabPages)
				{
					tp.Enabled = true;
				}
				Op = Operations.ViewPersonData;
				this.ControlEnabled(false, LockButtons.Fired);
				this.EnableButtons(true, true, false, true, true, false, LockButtons.Fired);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonFiredSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				Dictionary<string, object> Dict = new Dictionary<string, object>();
				foreach (TabPage tp in this.tabControlCardNew.TabPages)
				{
					tp.Enabled = true;
				}

				this.ValidateFiredData(Dict);

				if (Op == Operations.AddFired)
				{
					int idx;
					Dictionary<string, object> nDict = new Dictionary<string, object>();
					//nDict.Add("Date", DataAction.ConvertDateToMySqlU(DateTime.Now, mainForm.DataBaseTypes));
					nDict.Add("Date", DateTime.Now);
					nDict.Add("Text", "Освободен на " + this.dateTimePickerFiredFromDate.Text);
					nDict.Add("Type", "Прекратяване");
					nDict.Add("TypeDocument", this.comboBoxFiredReason.Text);
					nDict.Add("Par", this.parent.ToString());

					idx = this.dataAdapter.UniversalInsertParam(TableNames.Fired, Dict, "id", TransactionComnmand.BEGIN_TRANSACTION);
					if (idx > 0)
					{
						Dict.Add("ID", idx.ToString());
						idx = this.dataAdapter.UniversalInsertParam(TableNames.NotesTable, nDict, "id", TransactionComnmand.COMMIT_TRANSACTION);
						if (idx > 0)
						{
							nDict.Add("ID", idx.ToString());
							this.AddDictToTable(Dict, this.dtFired);
							this.AddDictToTable(nDict, this.dtNotes);
						}
					}
				}
				else
				{
					DataRow row = this.dtFired.Rows.Find(this.dataGridViewFired.CurrentRow.Cells["id"].Value);
					if (row != null)
					{
						string id;
						id = Dict["ID"].ToString();
						Dict.Remove("ID");

						if (this.dataAdapter.UniversalUpdateParam(TableNames.Fired, "id", Dict, id, TransactionComnmand.NO_TRANSACTION))
						{
							Dict.Add("ID", id);
							this.UpdateDictToRow(Dict, row);
						}
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
			finally
			{
				Op = Operations.ViewPersonData;
				this.ControlEnabled(false, LockButtons.Fired);
				this.EnableButtons(true, true, false, true, true, false, LockButtons.Fired);
			}
		}

		private void buttonFiredDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.dataGridViewFired.CurrentRow != null)
				{
					if (MessageBox.Show(this, "Сигурни ли сте че искате да изтриете прекратяването " + this.dataGridViewFired.CurrentRow.Cells["reason"].Value.ToString(), "Изтриване", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						if (this.dataAdapter.UniversalDelete(TableNames.Fired, this.dataGridViewFired.CurrentRow.Cells["id"].Value.ToString(), "id"))
						{
							this.dataGridViewFired.Rows.Remove(this.dataGridViewFired.CurrentRow);
						}
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonFire_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.dataGridViewFired.CurrentRow != null && this.dtAssignment.Rows.Count > 0)
				{
					if (MessageBox.Show(this, "Сигурни ли сте че искате да прекратите трудовият договор?", "Прекратяване", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						string cs = "";
						mainForm.GetConnString(out cs);
						var data = new Entities(cs);

						var fired = data.HR_Fired.Where(a => a.parent == this.parent).OrderByDescending(a => a.id).FirstOrDefault();
						HR_PersonAssignment ass;
						try
						{
							ass = data.HR_PersonAssignment.Where(a => a.isActive == 1 && a.parent == this.parent).SingleOrDefault();
						}
						catch (Exception ex)
						{
							MessageBox.Show("Открито е повече от едно активно назначение.", ex.Message);
							return;
						}

						var person = data.HR_Person.FirstOrDefault(a => a.id == this.parent);

						if (fired == null)
						{
							MessageBox.Show("Не е е създаден документ за прекратяване на договора.");
							return;
						}
						if (ass == null)
						{
							MessageBox.Show("Не е открито активно назначение.");
							return;
						}
						if (person == null)
						{
							MessageBox.Show("Не е открит служител - възможна грешка в досието.");
							return;
						}

						fired.level1 = ass.level1;
						fired.level2 = ass.level2;
						fired.level3 = ass.level3;
						fired.level4 = ass.level4;
						fired.position = ass.position;
						fired.baseSalary = ass.baseSalary.ToString();
						person.fired = 1;
						data.SaveChanges();
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void RefreshFiredDataSource(bool IsFormLoad)
		{
			try
			{
				this.dtFired = this.dataAdapter.SelectWhere(TableNames.Fired, "*", " WHERE parent = " + this.parent.ToString());
				if (dtFired == null)
				{
					MessageBox.Show("Грешка при зареждане на таблицата за прекратявания", ErrorMessages.NoConnection);
					this.Close();
				}
				TabPage tab = this.tabControlCardNew.SelectedTab;
				this.tabControlCardNew.SelectedTab = this.tabControlCardNew.TabPages["TabpageFired"];
				if (this.tabControlCardNew.SelectedTab != null)
				{
					this.GridSelect = false;
					this.dtFired.PrimaryKey = new DataColumn[] { this.dtFired.Columns["ID"] };
					this.dataGridViewFired.DataSource = this.dtFired;
					this.dataGridViewFired.ClearSelection();

					this.dtFired.TableName = TableNames.Fired;
					JustifyGridView(dataGridViewFired, TableNames.Compare(TableNames.Fired));
				}
				this.tabControlCardNew.SelectedTab = tab;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void dataGridFired_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (dataGridViewFired.CurrentRow == null)
					return;

				int index = this.comboBoxFiredReason.FindString(this.dataGridViewFired.CurrentRow.Cells["reason"].Value.ToString());
				this.SetComboIndex(this.comboBoxFiredReason, index);

				this.textBoxFireOrder.Text = this.dataGridViewFired.CurrentRow.Cells["fireorder"].Value.ToString();

				this.dateTimePickerFiredFromDate.Value = (DateTime)this.dataGridViewFired.CurrentRow.Cells["FromDate"].Value;
				DateTime tempdate;

				if (DateTime.TryParse(this.dataGridViewFired.CurrentRow.Cells["FireOrderDate"].Value.ToString(), out tempdate))
				{
					this.dateTimePickerFireOdredDate.Value = tempdate;
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonFiredExcel_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.dataGridViewFired.Rows.Count > 0)
				{
					ExcelExpo Ex = new ExcelExpo();
					DataView vue = new DataView(this.dtFired, "", "", DataViewRowState.CurrentRows);
					Ex.ExportView(this.dataGridViewFired, vue, "Прекратени на " + this.textBoxNames.Text);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonResotre_Click(object sender, EventArgs e)
		{
			if (this.dtAssignment.Rows.Count > 0)
			{
				DataView vueRel;
				DataViewRowState dvrs = DataViewRowState.CurrentRows;
				string cong = "1 = 1";
				vueRel = new DataView(dtAssignment, cong, "id desc", dvrs);
				int AssignmentID = 0, NodeID = 0;
				if (vueRel != null)
				{
					try
					{
						AssignmentID = int.Parse(vueRel[0]["id"].ToString());
						DataRow rowPosition = this.dtPosition.Rows.Find(vueRel[0]["PositionID"]);

						if (rowPosition == null)
						{
							MessageBox.Show("Длъжността на която е бил назначен служителят не може да бъде открита. Възстановяването не може да бъде извършено");
							return;
						}

						NodeID = (int)rowPosition["par"];
						if (NodeID == 0)
						{
							MessageBox.Show("Длъжността на която е бил назначен служителят не може да бъде открита. Възстановяването не може да бъде извършено");
							return;
						}
					}
					catch
					{
						MessageBox.Show("Длъжността на която е бил назначен служителят не може да бъде открита. Възстановяването не може да бъде извършено");
						return;
					}
				}

				Dictionary<string, object> pDict = new Dictionary<string, object>();
				Dictionary<string, object> iDict = new Dictionary<string, object>();
				bool Save = false;

				pDict.Add("nodeID", NodeID);
				pDict.Add("fired", "0");
				iDict.Add("IsActive", "1");

				Save = this.dataAdapter.UniversalUpdateParam(TableNames.Person, "id", pDict, this.parent.ToString(), TransactionComnmand.BEGIN_TRANSACTION);
				if (Save == false)
				{
					MessageBox.Show("Грешка при възстановяване на договор", ErrorMessages.NoConnection);
					return;
				}
				Save = this.dataAdapter.UniversalUpdateParam(TableNames.PersonAssignment, "id", iDict, AssignmentID.ToString(), TransactionComnmand.COMMIT_TRANSACTION); //updateActivation
				if (Save == false)
				{
					MessageBox.Show("Грешка при възстановяване на договор", ErrorMessages.NoConnection);
					return;
				}
				Op = Operations.FirePerson;
				this.Save_Person(sender, e);
				MessageBox.Show("Служителя е възстоновен успешно");
			}
		}
		#endregion

		#region Attestations

		private void RefreshAttestationsDataSource(bool IsFormLoad)
		{
			try
			{
				this.dtAttestations = this.dataAdapter.SelectWhere(TableNames.Attestations, "*", "WHERE par = " + this.parent);
				if (this.dtAttestations == null)
				{
					MessageBox.Show("Грешка при зареждане на таблицата за атестации", ErrorMessages.NoConnection);
					this.Close();
				}
				TabPage tab = this.tabControlCardNew.SelectedTab;
				this.tabControlCardNew.SelectedTab = this.tabControlCardNew.TabPages["TabpageAtestacii"];
				if (this.tabControlCardNew.SelectedTab != null)
				{
					this.dtAttestations.PrimaryKey = new DataColumn[] { this.dtAttestations.Columns["ID"] };
					this.dataGridViewAttestations.DataSource = this.dtAttestations;
					this.dataGridViewAttestations.ClearSelection();

					this.dtAttestations.TableName = "attestation";

					this.JustifyGridView(this.dataGridViewAttestations, TableNames.Compare(TableNames.Attestations));
				}
				this.tabControlCardNew.SelectedTab = tab;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonAtestationsAdd_Click(object sender, System.EventArgs e)
		{
			try
			{
				foreach (TabPage tp in this.tabControlCardNew.TabPages)
				{
					if (this.tabPageAtestacii != tp)
					{
						tp.Enabled = false;
					}
				}

				Op = Operations.AddAttestation;

				if (dataGridViewAttestations.CurrentRow != null)
					this.dataGridViewAttestations.ClearSelection();
				this.EnableButtons(false, false, true, false, false, true, LockButtons.Attestation);
				this.ControlEnabled(true, LockButtons.Attestation);

				this.dateTimePickerTestPeriod.Value = DateTime.Now;
				this.dateTimePickerWorkPlan.Value = DateTime.Now;
				this.dateTimePickerRangDate.Value = DateTime.Now;
				this.dateTimePickerPositionDate.Value = DateTime.Now;
				this.dateTimePickerObjectionDate.Value = DateTime.Now;
				this.dateTimePickerMiddleMeetingDate.Value = DateTime.Now;
				this.dateTimePickerFinalMeeting.Value = DateTime.Now;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonAtestationsEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				Op = Operations.EditAttestation;
				if (this.dataGridViewAttestations.CurrentRow != null)
				{
					this.EnableButtons(false, false, true, false, false, true, LockButtons.Attestation);
					this.ControlEnabled(true, LockButtons.Attestation);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonatestationsCancel_Click(object sender, System.EventArgs e)
		{
			try
			{
				foreach (TabPage tp in this.tabControlCardNew.TabPages)
				{
					tp.Enabled = true;
				}
				if (Op == Operations.AddAttestation)  // Трбва да се провери преди смяната на операцията
				{
				}
				Op = Operations.ViewPersonData;
				this.ControlEnabled(false, LockButtons.Attestation);
				this.EnableButtons(true, true, false, true, true, false, LockButtons.Attestation);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonatestationsDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.dataGridViewAttestations.CurrentRow != null)
				{
					if (MessageBox.Show(this, "Сигурни ли сте че искате да изтриете атестацията " + this.dataGridViewAttestations.CurrentRow.Cells["year"].Value.ToString(), "Изтриване", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						if (this.dataAdapter.UniversalDelete(TableNames.Attestations, this.dataGridViewAttestations.CurrentRow.Cells["id"].Value.ToString(), "id"))
						{
							DataRow row = this.dtAttestations.Rows.Find(this.dataGridViewAttestations.CurrentRow.Cells["id"].Value);
							if (row != null)
								dtAttestations.Rows.Remove(row);
						}
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private bool ValidateAttestationData(Dictionary<string, object> Dict)
		{
			try
			{
				Dict.Add("par", this.parent.ToString());

				Dict.Add("BossName", this.textBoxBoss.Text);
				Dict.Add("ControllingBossName", this.textBoxControllingBoss.Text);
				//Dict.Add("FinalMeetingDate", DataAction.ConvertDateToMySqlU(this.dateTimePickerFinalMeeting.Value, mainForm.DataBaseTypes));
				Dict.Add("FinalMeetingDate", this.dateTimePickerFinalMeeting.Value);

				if (this.checkBoxFinalMeeting.Checked == true)
				{
					Dict.Add("HasFinalMeeting", "да");
				}
				else
				{
					Dict.Add("HasFinalMeeting", "не");
				}

				if (this.checkBoxMiddleMeetingDate.Checked == true)
				{
					Dict.Add("hasMiddleMeeting", "да");
				}
				else
				{
					Dict.Add("hasMiddleMeeting", "не");
				}

				if (this.checkBoxObjection.Checked == true)
				{
					Dict.Add("HasObjection", "да");
				}
				else
				{
					Dict.Add("HasObjection", "не");
				}

				if (this.checkBoxPosition.Checked == true)
				{
					Dict.Add("hasPositionUpdate", "да");
				}
				else
				{
					Dict.Add("hasPositionUpdate", "не");
				}

				if (this.checkBoxRang.Checked == true)
				{
					Dict.Add("hasRangUpdate", "да");
				}
				else
				{
					Dict.Add("hasRangUpdate", "не");
				}

				if (this.checkBoxHasTraining.Checked == true)
				{
					Dict.Add("hasTraining", "да");
				}
				else
				{
					Dict.Add("hasTraining", "не");
				}

				if (this.checkBoxhasWorkPlan.Checked == true)
				{
					Dict.Add("hasWorkPlan", "да");
				}
				else
				{
					Dict.Add("hasWorkPlan", "не");
				}

				//Dict.Add("MiddleMeetingDate", DataAction.ConvertDateToMySqlU(this.dateTimePickerMiddleMeetingDate.Value, mainForm.DataBaseTypes));
				Dict.Add("MiddleMeetingDate", this.dateTimePickerMiddleMeetingDate.Value);
				Dict.Add("NewRang", this.comboBoxNewRang.Text);
				//Dict.Add("ObjectionDate", DataAction.ConvertDateToMySqlU(this.dateTimePickerObjectionDate.Value, mainForm.DataBaseTypes));
				Dict.Add("ObjectionDate", this.dateTimePickerObjectionDate.Value);
				Dict.Add("Others", this.textBoxAttestationsOther.Text);
				Dict.Add("PositionUpdateDate", this.dateTimePickerPositionDate.Value);
				Dict.Add("RangUpdateData", this.dateTimePickerRangDate.Value);
				try
				{
					Dict.Add("TotalMark", this.comboBoxTotalMark.Text);
				}
				catch (FormatException)
				{
					Dict.Add("TotalMark", "0");
				}
				Dict.Add("TrainingData", this.textBoxTrainingData.Text);
				Dict.Add("WorkPlanDate", this.dateTimePickerWorkPlan.Value);
				try
				{
					Dict.Add("Year", this.numBoxYear.Text);
				}
				catch (FormatException)
				{
					Dict.Add("Year", "0");
				}
				return true;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		private void dataGridAttestations_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.dataGridViewAttestations.CurrentRow == null)
					return;

				int index = this.comboBoxTotalMark.FindString(this.dataGridViewAttestations.CurrentRow.Cells["TotalMark"].Value.ToString());
				this.SetComboIndex(this.comboBoxTotalMark, index);

				index = this.comboBoxNewRang.FindString(this.dataGridViewAttestations.CurrentRow.Cells["NewRang"].Value.ToString());
				this.SetComboIndex(this.comboBoxNewRang, index);

				this.numBoxYear.Text = this.dataGridViewAttestations.CurrentRow.Cells["Year"].Value.ToString();
				this.textBoxTrainingData.Text = this.dataGridViewAttestations.CurrentRow.Cells["TrainingData"].Value.ToString();
				this.textBoxOther.Text = this.dataGridViewAttestations.CurrentRow.Cells["Others"].Value.ToString();
				this.textBoxBoss.Text = this.dataGridViewAttestations.CurrentRow.Cells["BossName"].Value.ToString();
				this.textBoxControllingBoss.Text = this.dataGridViewAttestations.CurrentRow.Cells["ControllingBossName"].Value.ToString();
				this.textBoxAttestationsOther.Text = this.dataGridViewAttestations.CurrentRow.Cells["others"].Value.ToString();

				this.dateTimePickerWorkPlan.Value = (DateTime)this.dataGridViewAttestations.CurrentRow.Cells["WorkPlanDate"].Value;
				this.dateTimePickerMiddleMeetingDate.Value = (DateTime)this.dataGridViewAttestations.CurrentRow.Cells["MiddleMeetingDate"].Value;
				this.dateTimePickerFinalMeeting.Value = (DateTime)this.dataGridViewAttestations.CurrentRow.Cells["FinalMeetingDate"].Value;
				this.dateTimePickerObjectionDate.Value = (DateTime)this.dataGridViewAttestations.CurrentRow.Cells["ObjectionDate"].Value;
				this.dateTimePickerPositionDate.Value = (DateTime)this.dataGridViewAttestations.CurrentRow.Cells["PositionUpdateDate"].Value;
				this.dateTimePickerRangDate.Value = (DateTime)this.dataGridViewAttestations.CurrentRow.Cells["RangUpdateData"].Value;

				if (this.dataGridViewAttestations.CurrentRow.Cells["hasWorkPlan"].Value.ToString() == "да")
				{
					this.checkBoxhasWorkPlan.Checked = true;
				}
				else
				{
					this.checkBoxhasWorkPlan.Checked = false;
				}
				if (this.dataGridViewAttestations.CurrentRow.Cells["hasMiddleMeeting"].Value.ToString() == "да")
				{
					this.checkBoxMiddleMeetingDate.Checked = true;
				}
				else
				{
					this.checkBoxMiddleMeetingDate.Checked = false;
				}
				if (this.dataGridViewAttestations.CurrentRow.Cells["hasFinalMeeting"].Value.ToString() == "да")
				{
					this.checkBoxFinalMeeting.Checked = true;
				}
				else
				{
					this.checkBoxFinalMeeting.Checked = false;
				}
				if (this.dataGridViewAttestations.CurrentRow.Cells["hasObjection"].Value.ToString() == "да")
				{
					this.checkBoxObjection.Checked = true;
				}
				else
				{
					this.checkBoxObjection.Checked = false;
				}
				if (this.dataGridViewAttestations.CurrentRow.Cells["hasRangUpdate"].Value.ToString() == "да")
				{
					this.checkBoxRang.Checked = true;
				}
				else
				{
					this.checkBoxRang.Checked = false;
				}
				if (this.dataGridViewAttestations.CurrentRow.Cells["hasTraining"].Value.ToString() == "да")
				{
					this.checkBoxHasTraining.Checked = true;
				}
				else
				{
					this.checkBoxHasTraining.Checked = false;
				}
				if (this.dataGridViewAttestations.CurrentRow.Cells["hasPositionUpdate"].Value.ToString() == "да")
				{
					this.checkBoxPosition.Checked = true;
				}
				else
				{
					this.checkBoxPosition.Checked = false;
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonAtestationsSave_Click(object sender, System.EventArgs e)
		{
			AttestationsSave();
		}

		private bool AttestationsSave()
		{
			try
			{
				bool result;
				Dictionary<string, object> Dict = new Dictionary<string, object>();
				result = this.ValidateAttestationData(Dict);
				if (result == true)
				{
					foreach (TabPage tp in this.tabControlCardNew.TabPages)
					{
						tp.Enabled = true;
					}

					if (Op == Operations.AddAttestation)
					{
						Dictionary<string, object> nDict = new Dictionary<string, object>();
						int id;

						nDict.Add("Date", DateTime.Now);
						nDict.Add("Text", "Атестиран на " + this.dateTimePickerPenaltyFromDate.Text);
						nDict.Add("Type", "Атестация");
						nDict.Add("TypeDocument", "Атестцационна оценка " + this.comboBoxTotalMark.Text);
						nDict.Add("Par", this.parent.ToString());

						id = this.dataAdapter.UniversalInsertParam(TableNames.Attestations, Dict, "id", TransactionComnmand.BEGIN_TRANSACTION);
						if (id > 0)
						{
							Dict.Add("ID", id.ToString());
							id = this.dataAdapter.UniversalInsertParam(TableNames.NotesTable, nDict, "id", TransactionComnmand.COMMIT_TRANSACTION);
							nDict.Add("ID", id.ToString());
							if (id > 0)
							{

								this.AddDictToTable(Dict, this.dtAttestations);
								this.AddDictToTable(nDict, this.dtNotes);
							}
							else
							{
								MessageBox.Show("Грешка при добавяне на атестация", ErrorMessages.NoConnection);
							}
						}
						else
						{
							MessageBox.Show("Грешка при добавяне на атестация", ErrorMessages.NoConnection);
						}
					}
					else
					{
						string id = this.dataGridViewAttestations.CurrentRow.Cells["id"].Value.ToString();
						DataRow row = this.dtAttestations.Rows.Find(id);
						if (row != null)
						{
							if (this.dataAdapter.UniversalUpdateParam(TableNames.Attestations, "id", Dict, id, TransactionComnmand.NO_TRANSACTION))
							{
								Dict.Add("ID", id);
								this.UpdateDictToRow(Dict, row);
							}
							else
							{
								MessageBox.Show("Грешка при редакция на атестация", ErrorMessages.NoConnection);
							}
						}
					}

					Op = Operations.ViewPersonData;
					this.ControlEnabled(false, LockButtons.Attestation);
					this.EnableButtons(true, true, false, true, true, false, LockButtons.Attestation);
					this.Refresh();
				}
				return result;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		private void buttonAttestationsExcel_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.dataGridViewAttestations.Rows.Count > 0)
				{
					ExcelExpo Ex = new ExcelExpo();
					DataView vue = new DataView(this.dtAttestations, "", "", DataViewRowState.CurrentRows);
					Ex.ExportView(this.dataGridViewAttestations, vue, "Атестации на " + this.textBoxNames.Text);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}
		#endregion

		#region Education

		private void buttonEducationSave_Click(object sender, System.EventArgs e)
		{
			EducationsSave();
		}

		private void buttonEducationCancel_Click(object sender, System.EventArgs e)
		{
			try
			{
				foreach (TabPage tp in this.tabControlCardNew.TabPages)
				{
					tp.Enabled = true;
				}
				if (Op == Operations.AddEducation)  // Трбва да се провери преди смяната на операцията
				{
				}
				Op = Operations.ViewPersonData;
				this.ControlEnabled(false, LockButtons.Education);
				this.EnableButtons(true, true, false, true, true, false, LockButtons.Education);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonEducationDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.dataGridViewEducations.CurrentRow != null)
				{
					if (MessageBox.Show(this, "Сигурни ли сте че искате да изтриете обучението " + this.dataGridViewEducations.CurrentRow.Cells["theme"].Value.ToString(), "Изтриване", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						if (this.dataAdapter.UniversalDelete(TableNames.Educations, this.dataGridViewEducations.CurrentRow.Cells["id"].Value.ToString(), "id"))
						{
							DataRow row = this.dtEducations.Rows.Find(this.dataGridViewEducations.CurrentRow.Cells["id"].Value);
							dtEducations.Rows.Remove(row);
						}
						else
						{
							MessageBox.Show("Грешка при изтриване на обучение", ErrorMessages.NoConnection);
						}
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private bool EducationsSave()
		{
			try
			{
				bool result;
				Dictionary<string, object> Dict = new Dictionary<string, object>();
				result = this.ValidateEducationData(Dict);
				if (result == true)
				{
					foreach (TabPage tp in this.tabControlCardNew.TabPages)
					{
						tp.Enabled = true;
					}

					if (Op == Operations.AddEducation)
					{
						Dictionary<string, object> nDict = new Dictionary<string, object>();
						int id;

						nDict.Add("Date", DateTime.Now);
						nDict.Add("Text", string.Format("Преминал обучение по {0} на {1}", Dict["Theme"], this.dateTimePickerEducationToDate.Text));
						nDict.Add("Type", "Обучение");
						nDict.Add("TypeDocument", string.Format("Преминал обучение по {0} на {1}", Dict["Theme"], this.dateTimePickerEducationToDate.Text));
						nDict.Add("Par", this.parent.ToString());

						id = this.dataAdapter.UniversalInsertParam(TableNames.Educations, Dict, "id", TransactionComnmand.BEGIN_TRANSACTION);
						if (id > 0)
						{
							Dict.Add("ID", id.ToString());
							id = this.dataAdapter.UniversalInsertParam(TableNames.NotesTable, nDict, "id", TransactionComnmand.COMMIT_TRANSACTION);

							if (id > 0)
							{
								nDict.Add("ID", id.ToString());
								this.AddDictToTable(nDict, this.dtNotes);
								this.AddDictToTable(Dict, this.dtEducations);
							}
							else
							{
								MessageBox.Show("Грешка при добавяне на обучение", ErrorMessages.NoConnection);
							}
						}
						else
						{
							MessageBox.Show("Грешка при добавяне на обучение", ErrorMessages.NoConnection);
						}
					}
					else if (Op == Operations.EditEducation)
					{
						DataRow row = this.dtEducations.Rows.Find(this.dataGridViewEducations.CurrentRow.Cells["id"].Value);
						if (row != null)
						{

							if (this.dataAdapter.UniversalUpdateParam(TableNames.Educations, "id", Dict, row["id"].ToString(), TransactionComnmand.NO_TRANSACTION))
								this.UpdateDictToRow(Dict, row);
							else
							{
								MessageBox.Show("Грешка при редакция на обучение", ErrorMessages.NoConnection);
							}
						}
					}

					Op = Operations.ViewPersonData;
					this.ControlEnabled(false, LockButtons.Education);
					this.EnableButtons(true, true, false, true, true, false, LockButtons.Education);
				}
				return result;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		private void buttonEducationAdd_Click(object sender, System.EventArgs e)
		{
			try
			{
				foreach (TabPage tp in this.tabControlCardNew.TabPages)
				{
					if (this.tabPageEducation != tp)
					{
						tp.Enabled = false;
					}
				}

				Op = Operations.AddEducation;
				if (dataGridViewEducations.CurrentRow != null)
					this.dataGridViewEducations.ClearSelection();

				this.EnableButtons(false, false, true, false, false, true, LockButtons.Education);

				this.ControlEnabled(true, LockButtons.Education);

				this.dateTimePickerEducationFromDate.Value = DateTime.Now;
				this.dateTimePickerEducationToDate.Value = DateTime.Now;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonEducationEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				Op = Operations.EditEducation;
				if (this.dataGridViewEducations.CurrentRow != null)
				{
					this.EnableButtons(false, false, true, false, false, true, LockButtons.Education);
					this.ControlEnabled(true, LockButtons.Education);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonEducationCatalog_Click(object sender, System.EventArgs e)
		{

		}

		private void RefreshEducationsDataSource(bool IsFormLoad)
		{
			try
			{

				this.dtEducations = this.dataAdapter.SelectWhere(TableNames.Educations, "*", "WHERE personid = " + this.parent);
				this.dtEducations.PrimaryKey = new DataColumn[] { this.dtEducations.Columns["ID"] };

				if (this.dtEducations == null)
				{
					MessageBox.Show("Грешка при зареждане на таблицата за обучения", ErrorMessages.NoConnection);
					this.Close();
				}

				TabPage tab = this.tabControlCardNew.SelectedTab;
				this.tabControlCardNew.SelectedTab = this.tabControlCardNew.TabPages["TabpageEducation"];
				if (this.tabControlCardNew.SelectedTab != null)
				{
					this.dataGridViewEducations.DataSource = this.dtEducations;
					this.dataGridViewEducations.ClearSelection();

					this.dtEducations.TableName = TableNames.Educations;
					JustifyGridView(dataGridViewEducations, TableNames.Compare(TableNames.Educations));
				}
				this.tabControlCardNew.SelectedTab = tab;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private bool ValidateEducationData(Dictionary<string, object> Dict)
		{
			try
			{
				Dict.Add("PersonId", this.parent.ToString());
				Dict.Add("Area", this.textBoxEducationArea.Text);
				Dict.Add("Theme", this.textBoxEducationTheme.Text);
				if (Dict["Theme"].ToString() == "".ToString())
				{
					MessageBox.Show("Трябва да въведете тема на обучението");
					return false;
				}
				Dict.Add("Code", this.textBoxEducationCode.Text);
				Dict.Add("CertificateData", this.textBoxEducationCertificate.Text);
				Dict.Add("EducationOrganisation", this.textBoxEducationOrganisation.Text);
				Dict.Add("EducationPlace", this.textBoxEducationPlace.Text);

				Dict.Add("FromDate", this.dateTimePickerEducationFromDate.Value);
				Dict.Add("ToDate", this.dateTimePickerEducationToDate.Value);

				Dict.Add("EducationDays", this.numBoxEducationDays.Text);
				Dict.Add("EducationHours", this.numBoxEducationHours.Text);
				Dict.Add("Price", this.numBoxEducationPrice.Text);
				return true;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		private void dataGridViewEducations_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.dataGridViewEducations.CurrentRow == null)
					return;

				this.numBoxEducationDays.Text = this.dataGridViewEducations.CurrentRow.Cells["EducationDays"].Value.ToString();
				this.numBoxEducationHours.Text = this.dataGridViewEducations.CurrentRow.Cells["EducationHours"].Value.ToString();
				this.numBoxEducationPrice.Text = this.dataGridViewEducations.CurrentRow.Cells["Price"].Value.ToString();

				this.textBoxEducationArea.Text = this.dataGridViewEducations.CurrentRow.Cells["Area"].Value.ToString();
				this.textBoxEducationCertificate.Text = this.dataGridViewEducations.CurrentRow.Cells["CertificateData"].Value.ToString();
				this.textBoxEducationCode.Text = this.dataGridViewEducations.CurrentRow.Cells["Code"].Value.ToString();
				this.textBoxEducationOrganisation.Text = this.dataGridViewEducations.CurrentRow.Cells["EducationOrganisation"].Value.ToString();
				this.textBoxEducationPlace.Text = this.dataGridViewEducations.CurrentRow.Cells["EducationPlace"].Value.ToString();
				this.textBoxEducationTheme.Text = this.dataGridViewEducations.CurrentRow.Cells["Theme"].Value.ToString();

				this.dateTimePickerEducationFromDate.Value = (DateTime)this.dataGridViewEducations.CurrentRow.Cells["FromDate"].Value;
				this.dateTimePickerEducationToDate.Value = (DateTime)this.dataGridViewEducations.CurrentRow.Cells["ToDate"].Value;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonEducationsExcel_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.dataGridViewEducations.Rows.Count > 0)
				{
					ExcelExpo Ex = new ExcelExpo();
					DataView vue = new DataView(this.dtEducations, "", "", DataViewRowState.CurrentRows);
					Ex.ExportView(this.dataGridViewEducations, vue, "Обучения на " + this.textBoxNames.Text);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}
		#endregion

		#region Other Functions
		private void buttonОК_Click(object sender, EventArgs e)
		{
			try
			{
				if (Save_Person(sender, e))
					this.Close();
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void DisableAllButtons(bool IsFiredd)
		{
			try
			{
				if (IsFiredd) // Ako slujitelq e uwolnen - disable na wsichki butoni
				{
					foreach (Control ctrl in this.Controls)
					{
						if (ctrl is Button)
						{
							if (ctrl.Name == "buttonAssignmentPrint" || ctrl.Name == "buttonFiredPrint" || ctrl.Name == "buttonAbsencePrint")
							{
								ctrl.Enabled = true;
							}
							else
							{
								ctrl.Enabled = false;
							}
						}
					}
					foreach (TabPage page in this.tabControlCardNew.TabPages)
					{
						foreach (Control ctrlGroup in page.Controls)
						{
							if (ctrlGroup is GroupBox)
							{

								foreach (Control btn in ctrlGroup.Controls)
								{
									if (btn is Button)
									{
										if (btn.Name == "buttonAssignmentPrint" || btn.Name == "buttonFiredPrint" || btn.Name == "buttonAbsencePrint" || btn.Name == "buttonFiredRestore")
										{
											btn.Enabled = true;
										}
										else
										{
											btn.Enabled = false;
										}
									}
								}
							}
							else if (ctrlGroup is Button)
							{
								if (ctrlGroup.Name == "buttonAssignmentPrint" || ctrlGroup.Name == "buttonFiredPrint" || ctrlGroup.Name == "buttonAbsencePrint" || ctrlGroup.Name == "buttonFiredRestore")
								{
									ctrlGroup.Enabled = true;
								}
								else
								{
									ctrlGroup.Enabled = false;
								}
							}
						}
					}
					this.buttonCancel.Enabled = true;
					this.buttonHistory.Enabled = false; // Mnogo e nawytre w groupboxowete i trqbwa da se puska oshte foreachowe da se dostigne
					this.buttonAssignmentPrint.Enabled = true;
					this.buttonAtestationsPrint.Enabled = true;
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				bool result = Save_Person(sender, e);
				if (Op == Operations.AddNewPerson && result)
				{
					Op = Operations.ViewPersonData;
					this.EnableButtons(true, true, false, true, true, false, LockButtons.Absence);
					this.EnableButtons(true, true, false, true, true, false, LockButtons.Assignment);
					this.EnableButtons(true, true, false, true, true, false, LockButtons.Attestation);
					this.EnableButtons(true, true, false, true, true, false, LockButtons.Fired);
					this.EnableButtons(true, true, false, true, true, false, LockButtons.Notes);
					this.EnableButtons(true, true, false, true, true, false, LockButtons.Education);
					this.EnableButtons(true, true, false, true, true, false, LockButtons.Penalty);
					this.EnableButtons(true, true, false, true, true, false, LockButtons.Rang);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private bool Save_Person(object sender, System.EventArgs e)
		{
			try
			{
				bool result;
				Dictionary<string, object> Dict = new Dictionary<string, object>();
				result = this.ValidatePerson(Dict);
				if (result == true)
				{
					switch (Op)
					{
						case Operations.AddNewPerson:
							{
								Dict.Add("fired", "0");
								Dict.Add("nodeid", "0");

								this.parent = this.dataAdapter.UniversalInsertParam(TableNames.Person, Dict, "id", TransactionComnmand.NO_TRANSACTION);
								if (this.parent < 0)
								{
									MessageBox.Show("Грешка при запис на данни");
									return false;
								}

								if (this.mainform.dtKartoteka != null)
								{
									Dict.Add("ID", this.parent.ToString());
									DataRow row = this.mainform.dtKartoteka.NewRow();

									row["id"] = Dict["ID"];
									row["egn"] = Dict["Egn"];
									row["name"] = Dict["Name"];

									this.mainform.dtKartoteka.Rows.Add(row);
									PersonalDataChangedValue = false;
								}
								this.Text = string.Format("{0} {1}", this.Text, Dict["Name"]);
								//if (mainForm.DataBaseTypes == DBTypes.MsSql)
								//{
								//    Dictionary<string, string> mesDict = new Dictionary<string, string>();
								//}
								break;
							}
						case Operations.ViewPersonData:
							{
								if (PersonalDataChangedValue)
								{
									if (this.dataAdapter.UniversalUpdateParam(TableNames.Person, "id", Dict, this.parent.ToString(), TransactionComnmand.NO_TRANSACTION))
									{
										Dict.Add("ID", this.parent.ToString());
									}
									else
									{
										MessageBox.Show("Грешка при запис на данните");
										break;
									}
									PersonalDataChangedValue = false;
									MessageBox.Show("Данните за лицето са записани");
								}
								break;
							}
						case Operations.FirePerson:
							{
								break;
							}
						case Operations.AddPenalty:
							{
								this.buttonPenaltyAdd_Click(sender, e);
								break;
							}
						case Operations.EditPenalty:
							{
								this.buttonPenaltyAdd_Click(sender, e);
								break;
							}
						case Operations.AddAbsence:
							{
								this.buttonAbsenceSave_Click(sender, e);
								break;
							}
						case Operations.EditAbsence:
							{
								this.buttonAbsenceSave_Click(sender, e);
								break;
							}
						case Operations.AddAssignment:
							{
								this.buttonAssignmentSave_Click(sender, e);
								break;
							}
						case Operations.EditAssignment:
							{
								this.buttonAssignmentSave_Click(sender, e);
								break;
							}
						case Operations.AddEducation:
							{
								this.buttonEducationSave_Click(sender, e);
								break;
							}
						case Operations.EditEducation:
							{
								this.buttonEducationSave_Click(sender, e);
								break;
							}
						case Operations.AddNote:
							this.buttonNotesSave_Click(sender, e);
							break;
						case Operations.EditNotes:
							this.buttonNotesSave_Click(sender, e);
							break;
						case Operations.AddRang:
							this.buttonRangSave_Click(sender, e);
							break;
						case Operations.EditRang:
							this.buttonRangSave_Click(sender, e);
							break;

					}
				}
				return result;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		private void EnableButtons(bool add, bool edit, bool save, bool delete, bool print, bool cancel, LockButtons Enum)
		{
			try
			{
				switch (Enum)
				{
					case LockButtons.Penalty:
						{
							this.buttonPenaltyAdd.Enabled = add;
							this.buttonPebaltyEdit.Enabled = edit;
							this.buttonPenaltySave.Enabled = save;
							this.buttonPenaltyDelete.Enabled = delete;
							this.buttonPenaltyCancel.Enabled = cancel;
							this.buttonPenaltyPrint.Enabled = print;
							this.buttonPenaltiesExcel.Enabled = add;
							break;
						}
					case LockButtons.Absence:
						{
							this.buttonAbsenceAdd.Enabled = add;
							this.buttonAbsenceEdit.Enabled = edit;
							this.buttonAbsenceSave.Enabled = save;
							this.buttonAbsenceDelete.Enabled = delete;
							this.buttonAbsenceCancel.Enabled = cancel;
							this.buttonAbsencePrint.Enabled = print;
							this.buttonAbsenceExcel.Enabled = add;
							break;
						}
					case LockButtons.Assignment:
						{
							this.buttonAssignment.Enabled = add;
							this.buttonAssignmentEdit.Enabled = edit;
							this.buttonAssignmentSave.Enabled = save;
							this.buttonAssignmentDelete.Enabled = delete;
							this.buttonAssignmentPrint.Enabled = print;
							this.buttonAssignmentCancel.Enabled = cancel;
							this.buttonAssignmentExcel.Enabled = add;
							break;
						}
					case LockButtons.Fired:
						{
							this.buttonFiredNew.Enabled = add;
							this.buttonFiredEdit.Enabled = edit;
							this.buttonFiredSave.Enabled = save;
							this.buttonFiredDelete.Enabled = delete;
							this.buttonFiredPrint.Enabled = print;
							this.buttonFiredCancel.Enabled = cancel;
							this.buttonFire.Enabled = !this.IsFiredd;
							this.buttonFiredExcel.Enabled = add;
							this.buttonFiredRestore.Enabled = this.IsFiredd;
							break;
						}
					case LockButtons.Attestation:
						{
							this.buttonAtestationsAdd.Enabled = add;
							this.buttonatestationsCancel.Enabled = cancel;
							this.buttonatestationsDelete.Enabled = delete;
							this.buttonAtestationsEdit.Enabled = edit;
							this.buttonAtestationsPrint.Enabled = print;
							this.buttonAtestationsSave.Enabled = save;
							this.buttonAttestationsExcel.Enabled = add;
							break;
						}
					case LockButtons.Education:
						{
							this.buttonEducationAdd.Enabled = add;
							this.buttonEducationCancel.Enabled = cancel;
							this.buttonEducationDelete.Enabled = delete;
							this.buttonEducationEdit.Enabled = edit;
							this.buttonEducationPrint.Enabled = print;
							this.buttonEducationSave.Enabled = save;
							this.buttonEducationsExcel.Enabled = add;
							break;
						}
					case LockButtons.Notes:
						{
							this.buttonNotesAdd.Enabled = add;
							this.buttonNotesEdit.Enabled = edit;
							this.buttonNotesSave.Enabled = save;
							this.buttonNotesDelete.Enabled = delete;
							this.buttonNotesCancel.Enabled = cancel;
							this.buttonNotesPrint.Enabled = print;
							this.buttonHistoryExcel.Enabled = add;
							break;
						}
					case LockButtons.Rang:
						{
							this.buttonRangNew.Enabled = add;
							this.buttonRangEdit.Enabled = edit;
							this.buttonRangSave.Enabled = save;
							this.buttonRangDelete.Enabled = delete;
							this.buttonRangCancel.Enabled = cancel;
							this.buttonRangPrint.Enabled = print;
							this.buttonRangExcel.Enabled = add;
							break;
						}
					case LockButtons.Card:
						{
							this.buttonCardNew.Enabled = add;
							this.buttonCardEdit.Enabled = edit;
							this.buttonCardSave.Enabled = save;
							this.buttonCardDelete.Enabled = delete;
							this.buttonCardCancel.Enabled = cancel;
							this.buttonCardPrint.Enabled = print;
							this.buttonCardExcel.Enabled = add;
							break;
						}

				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void ControlEnabled(bool IsEnabled, LockButtons Enum)
		{
			try
			{
				switch (Enum)
				{
					case LockButtons.Penalty:
						{
							foreach (Control ctrl in this.tabPagePenalty.Controls)
							{
								if (ctrl.GetType().Name != "Button" && ctrl.GetType().Name != "RadioButton")
								{
									ctrl.Enabled = IsEnabled;
								}
								else if (ctrl.GetType().Name == "RadioButton")
								{
									ctrl.Enabled = !IsEnabled;
								}
							}

							this.groupBoxPenaltyGrid.Enabled = !IsEnabled;
							break;
						}
					case LockButtons.Absence:
						{
							foreach (Control ctrl in this.tabPageAbsence.Controls)
							{
								if (ctrl.GetType().Name != "Button")
								{
									ctrl.Enabled = IsEnabled;
								}
							}
							this.groupBoxAbsenceGrid.Enabled = !IsEnabled;
							this.comboBoxAbsenceTypeAbsence_SelectedIndexChanged(this, null);
							break;
						}
					case LockButtons.Fired:
						{
							foreach (Control ctrl in this.tabPageFired.Controls)
							{
								if (ctrl.GetType().Name != "Button")
								{
									ctrl.Enabled = IsEnabled;
								}
							}
							this.groupBoxFired.Enabled = !IsEnabled;
							break;
						}
					case LockButtons.Assignment:
						{
							foreach (Control ctrl in this.tabPageAssignment.Controls)
							{
								if (ctrl.GetType().Name != "Button")
								{
									ctrl.Enabled = IsEnabled;
								}
							}

							this.groupBoxAssignmentGrid.Enabled = !IsEnabled;
							this.radioButtonAdditional.Enabled = !IsEnabled;
							this.radioButtonAssignment.Enabled = !IsEnabled;

							break;
						}
					case LockButtons.Notes:
						{
							foreach (Control ctrl in this.tabPageNotes.Controls)
							{
								if (ctrl.GetType().Name != "Button")
								{
									ctrl.Enabled = IsEnabled;
								}
							}
							this.groupBoxNotesGrid.Enabled = !IsEnabled;
							this.groupBoxNotesFilter.Enabled = !IsEnabled;
							this.comboBoxNotesFilter.Enabled = !IsEnabled;
							break;
						}
					case LockButtons.Attestation:
						{
							foreach (Control ctrl in this.tabPageAtestacii.Controls)
							{
								if (ctrl.GetType().Name != "Button")
								{
									ctrl.Enabled = IsEnabled;
								}
							}

							this.groupBoxAttestationRegister.Enabled = !IsEnabled;
							break;
						}
					case LockButtons.Education:
						{
							foreach (Control ctrl in this.tabPageEducation.Controls)
							{
								if (ctrl.GetType().Name != "Button")
								{
									ctrl.Enabled = IsEnabled;
								}
							}

							this.groupBoxEducationHistory.Enabled = !IsEnabled;
							break;
						}
					case LockButtons.Rang:
						{
							foreach (Control ctrl in this.tabPageMilitaryRang.Controls)
							{
								if (ctrl.GetType().Name != "Button")
								{
									ctrl.Enabled = IsEnabled;
								}
							}

							this.groupBoxRangHistory.Enabled = !IsEnabled;
							break;
						}
					case LockButtons.Card:
						{
							foreach (Control ctrl in this.tabPageCards.Controls)
							{
								if (ctrl.GetType().Name != "Button")
								{
									ctrl.Enabled = IsEnabled;
								}
							}

							this.groupBoxCardHistory.Enabled = !IsEnabled;
							break;
						}
				}
				EnableTabs(!IsEnabled);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		enum HTStates
		{
			Wait,
			Deliver,
			Sell,
		};

		private void CalculateHT(int d1, int s1, int d2, int s2, int d3, int s3)
		{
			int time = 0, start1 = 0, start2 = 0;
			int B = 0, S = 0, G = 0;
			HTStates state1, state2;
			state1 = state2 = HTStates.Wait;

			while (time < 591)
			{
				// Finish operations
				if (state1 == HTStates.Deliver)
				{
					if (time - start1 - d1 == 0)
					{
						S++;
						state1 = HTStates.Sell;
						start1 = time;
					}
				}

				if (state2 == HTStates.Deliver)
				{
					if (time - start2 - d2 == 0)
					{
						S++;
						state2 = HTStates.Sell;
						start2 = time;
					}
				}

				if (state1 == HTStates.Sell)
				{
					if (time - start1 - s1 == 0)
					{
						G++;
						state1 = HTStates.Wait;
					}
				}

				if (state2 == HTStates.Sell)
				{
					if (time - start2 - s2 == 0)
					{
						G++;
						state2 = HTStates.Wait;
					}
				}

				//New operations
				if (state2 == HTStates.Wait && state1 != HTStates.Deliver)
				{
					state2 = HTStates.Deliver;
					start2 = time;
					B++;
				}

				if (state1 == HTStates.Wait && state2 != HTStates.Deliver)
				{
					state1 = HTStates.Deliver;
					start1 = time;
					B++;
				}

				time++;
			}
			MessageBox.Show(string.Format(" 2 ppl B {0} S {1} G {2}", B, S, G));
		}

		private void CalculateHT3(int d1, int s1, int d2, int s2, int d3, int s3)
		{
			int time = 0, start1 = 0, start2 = 0, start3 = 0;
			int B = 0, S = 0, G = 0;
			HTStates state1, state2, state3;
			state1 = state2 = state3 = HTStates.Wait;

			while (time < 591)
			{
				// Finish operations
				if (state1 == HTStates.Deliver)
				{
					if (time - start1 - d1 == 0)
					{
						S++;
						state1 = HTStates.Sell;
						start1 = time;
					}
				}

				if (state2 == HTStates.Deliver)
				{
					if (time - start2 - d2 == 0)
					{
						S++;
						state2 = HTStates.Sell;
						start2 = time;
					}
				}

				if (state3 == HTStates.Deliver)
				{
					if (time - start3 - d3 == 0)
					{
						S++;
						state3 = HTStates.Sell;
						start3 = time;
					}
				}

				if (state1 == HTStates.Sell)
				{
					if (time - start1 - s1 == 0)
					{
						G++;
						state1 = HTStates.Wait;
					}
				}

				if (state2 == HTStates.Sell)
				{
					if (time - start2 - s2 == 0)
					{
						G++;
						state2 = HTStates.Wait;
					}
				}

				if (state3 == HTStates.Sell)
				{
					if (time - start3 - s3 == 0)
					{
						G++;
						state3 = HTStates.Wait;
					}
				}

				//New operations
				if (state3 == HTStates.Wait && state1 != HTStates.Deliver && state2 != HTStates.Deliver)
				{
					state3 = HTStates.Deliver;
					start3 = time;
					B++;
				}

				if (state2 == HTStates.Wait && state1 != HTStates.Deliver && state3 != HTStates.Deliver)
				{
					state2 = HTStates.Deliver;
					start2 = time;
					B++;
				}

				if (state1 == HTStates.Wait && state2 != HTStates.Deliver && state3 != HTStates.Deliver)
				{
					state1 = HTStates.Deliver;
					start1 = time;
					B++;
				}

				time++;
			}
			MessageBox.Show(string.Format("3 ppl B {0} S {1} G {2}", B, S, G));
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			//this.CalculateHT(15, 57, 43, 101, 122, 244);
			//this.CalculateHT3(15, 57, 43, 101, 122, 244);
			this.Close();
		}

		private void PersonalDataForm_Load(object sender, System.EventArgs e)
		{
			try
			{
				LoadNomenklatures();

				this.GridSelect = false;

				if (Op == Operations.ViewPersonData)
				{
					this.RefreshAssignmentDataSource(true);
					this.ControlEnabled(false, LockButtons.Assignment);
					this.EnableButtons(true, true, false, true, true, false, LockButtons.Assignment);
					LoadPersonalData();

					this.RefreshPenaltyDataSource(true);
					this.ControlEnabled(false, LockButtons.Penalty);
					this.EnableButtons(true, true, false, true, true, false, LockButtons.Penalty);

					this.RefreshAbsenceDataSource(true);
					this.ControlEnabled(false, LockButtons.Absence);
					this.EnableButtons(true, true, false, true, true, false, LockButtons.Absence);

					this.RefreshFiredDataSource(true);
					this.ControlEnabled(false, LockButtons.Fired);
					this.EnableButtons(true, true, false, true, true, false, LockButtons.Fired);

					this.RefreshNotesDataSource(true);
					this.ControlEnabled(false, LockButtons.Notes);
					this.EnableButtons(true, true, false, true, true, false, LockButtons.Notes);

					this.RefreshAttestationsDataSource(true);
					this.ControlEnabled(false, LockButtons.Attestation);
					this.EnableButtons(true, true, false, true, true, false, LockButtons.Attestation);

					this.RefreshEducationsDataSource(true);
					this.ControlEnabled(false, LockButtons.Education);
					this.EnableButtons(true, true, false, true, true, false, LockButtons.Education);


					this.RefreshRangDataSource(true);
					this.ControlEnabled(false, LockButtons.Rang);
					this.EnableButtons(true, true, false, true, true, false, LockButtons.Rang);

					this.RefreshCardDataSource(true);
					this.ControlEnabled(false, LockButtons.Card);
					this.EnableButtons(true, true, false, true, true, false, LockButtons.Card);

					this.RefreshLangugeDataSource(true);
				}
				else
				{
					this.RefreshPenaltyDataSource(true);
					this.RefreshAbsenceDataSource(true);
					this.RefreshNotesDataSource(true);
					this.RefreshAssignmentDataSource(true);
					this.RefreshAttestationsDataSource(true);

					this.RefreshLangugeDataSource(true);

					this.RefreshRangDataSource(true);


					this.ControlEnabled(false, LockButtons.Penalty);
					this.ControlEnabled(false, LockButtons.Assignment);
					this.ControlEnabled(false, LockButtons.Absence);
					this.ControlEnabled(false, LockButtons.Fired);
					this.ControlEnabled(false, LockButtons.Notes);
					this.ControlEnabled(false, LockButtons.Attestation);
					this.ControlEnabled(false, LockButtons.Education);
					this.ControlEnabled(false, LockButtons.Notes);
					this.ControlEnabled(false, LockButtons.Rang);
					this.ControlEnabled(false, LockButtons.Card);
					this.EnableButtons(false, false, false, false, false, false, LockButtons.Penalty);
					this.EnableButtons(false, false, false, false, false, false, LockButtons.Absence);
					this.EnableButtons(false, false, false, false, false, false, LockButtons.Assignment);
					this.EnableButtons(false, false, false, false, false, false, LockButtons.Fired);
					this.EnableButtons(false, false, false, false, false, false, LockButtons.Attestation);
					this.EnableButtons(false, false, false, false, false, false, LockButtons.Education);
					this.EnableButtons(false, false, false, false, false, false, LockButtons.Notes);
					this.EnableButtons(false, false, false, false, false, false, LockButtons.Rang);
					this.EnableButtons(false, false, false, false, false, false, LockButtons.Card);
				}
				this.DisableAllButtons(this.IsFiredd);
				this.GridSelect = true;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				this.GridSelect = true;
			}
		}

		private void LoadPersonalData()
		{
			try
			{
				DataTable dtPerson;
				string arg = "0";

				dtPerson = this.dataAdapter.SelectWhere(TableNames.Person, "*", "WHERE id = " + this.parent.ToString());
				this.dtNotes = this.dataAdapter.SelectWhere(TableNames.NotesTable, "*", " WHERE par = '" + this.parent + "'");
				if (dtPerson == null || this.dtNotes == null)
				{
					MessageBox.Show("Грешка при зареждане на лични данни", ErrorMessages.NoConnection);
					this.Close();
				}

				#region Loading Personal Info
				try
				{
					this.numBoxEgn.Text = dtPerson.Rows[0]["egn"].ToString();
					this.textBoxNames.Text = dtPerson.Rows[0]["name"].ToString();
					this.textBoxDiplom.Text = dtPerson.Rows[0]["diplomdate"].ToString();
					this.textBoxKwartal.Text = dtPerson.Rows[0]["kwartal"].ToString();
					this.textBoxPublishedFrom.Text = dtPerson.Rows[0]["publishedby"].ToString();
					this.textBoxTelephone.Text = dtPerson.Rows[0]["phone"].ToString();

					int y = 0, m = 0, d = 0;
					int.TryParse(dtPerson.Rows[0]["TotalExpY"].ToString(), out y);
					int.TryParse(dtPerson.Rows[0]["TotalExpM"].ToString(), out m);
					int.TryParse(dtPerson.Rows[0]["TotalExpD"].ToString(), out d);

					this.numBoxExpTotalY.Text = y.ToString();
					this.numBoxExpTotalM.Text = m.ToString();
					this.numBoxExpTotalD.Text = d.ToString();					

					this.numBoxPcCard.Text = dtPerson.Rows[0]["pcard"].ToString();
					try
					{
						this.dateTimePickerPCCardPublished.Value = (DateTime)dtPerson.Rows[0]["pcardpublish"];
					}
					catch (InvalidCastException)
					{
						this.dateTimePickerPCCardPublished.Value = DateTime.Now;
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
						this.dateTimePickerPCCardPublished.Value = DateTime.Now;
					}

					try
					{
						this.dateTimePickerPCardExpiry.Value = (DateTime)dtPerson.Rows[0]["pcardExpiry"];
					}
					catch (InvalidCastException)
					{
						this.dateTimePickerPCardExpiry.Value = DateTime.Now;
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
						this.dateTimePickerPCardExpiry.Value = DateTime.Now;
					}

					try
					{
						this.dateTimePickerPostypilNa.Value = (DateTime)dtPerson.Rows[0]["hiredat"];
					}
					catch (InvalidCastException)
					{
						this.dateTimePickerPostypilNa.Value = DateTime.Now;
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
						this.dateTimePickerPostypilNa.Value = DateTime.Now;
					}

					try
					{
						this.dateTimePickerBirthDate.Value = (DateTime)dtPerson.Rows[0]["borndate"];
					}
					catch (InvalidCastException)
					{
						this.dateTimePickerBirthDate.Value = DateTime.Now;
					}
					catch (Exception ex)
					{
						ErrorLog.WriteException(ex, ex.Message);
						MessageBox.Show(ex.Message);
						this.dateTimePickerBirthDate.Value = DateTime.Now;
					}
					try
					{
						this.dateTimePickerWorkBook.Value = (DateTime)dtPerson.Rows[0]["workbookdate"];
					}
					catch (InvalidCastException)
					{
						this.dateTimePickerWorkBook.Value = DateTime.Now;
					}
					catch (Exception ex)
					{
						ErrorLog.WriteException(ex, ex.Message);
						MessageBox.Show(ex.Message);
						this.dateTimePickerWorkBook.Value = DateTime.Now;
					}

					this.textBoxBornTown.Text = dtPerson.Rows[0]["borntown"].ToString();
					this.textBoxSpeciality.Text = dtPerson.Rows[0]["Speciality"].ToString();
					this.textBoxCountry.Text = dtPerson.Rows[0]["country"].ToString();
					this.textBoxRegion.Text = dtPerson.Rows[0]["region"].ToString();
					this.textBoxTown.Text = dtPerson.Rows[0]["town"].ToString();
					this.textBoxOther.Text = dtPerson.Rows[0]["other"].ToString();
					this.textBoxEngName.Text = dtPerson.Rows[0]["engname"].ToString();
					this.textBoxCurrentAddress.Text = dtPerson.Rows[0]["street"].ToString();
					this.textBoxOther1.Text = dtPerson.Rows[0]["other1"].ToString();


					this.textBoxOther2.Text = dtPerson.Rows[0]["other2"].ToString();
					this.textBoxOther3.Text = dtPerson.Rows[0]["other3"].ToString();
					this.textBoxOther4.Text = dtPerson.Rows[0]["other4"].ToString();
					this.textBoxWorkBook.Text = dtPerson.Rows[0]["other5"].ToString();

					arg = "";
					if (dtPerson.Rows[0]["familystatus"].ToString() != "")
					{
						arg = dtPerson.Rows[0]["familystatus"].ToString();
					}
					if (arg == "")
					{
						arg = "0";
					}
					int index = this.comboBoxFamilyStatus.FindStringExact(arg);
					if (index > -1)
					{
						this.comboBoxFamilyStatus.SelectedIndex = index;
					}
					index = 0;
					arg = "";

					if (dtPerson.Rows[0]["sciencelevel"].ToString() != "")
					{
						arg = dtPerson.Rows[0]["sciencelevel"].ToString();
					}
					if (arg == "")
					{
						arg = "0";
					}
					index = this.comboBoxScienceLevel.FindStringExact(arg);
					if (index > -1)
					{
						this.comboBoxScienceLevel.SelectedIndex = index;
					}
					index = 0;
					arg = "";

					if (dtPerson.Rows[0]["sciencetitle"].ToString() != "")
					{
						arg = dtPerson.Rows[0]["sciencetitle"].ToString();
					}
					if (arg == "")
					{
						arg = "0";
					}
					index = this.comboBoxScience.FindStringExact(arg);
					if (index > -1)
					{
						this.comboBoxScience.SelectedIndex = index;
					}
					index = 0;
					arg = "";

					if (dtPerson.Rows[0]["militaryrang"].ToString() != "")
					{
						arg = dtPerson.Rows[0]["militaryrang"].ToString();
					}
					if (arg == "")
					{
						arg = "0";
					}
					index = this.comboBoxMilitaryRang.FindStringExact(arg);
					if (index > -1)
					{
						this.comboBoxMilitaryRang.SelectedIndex = index;
					}
					index = 0;
					arg = "";
					if (dtPerson.Rows[0]["education"] != null)
					{
						arg = dtPerson.Rows[0]["education"].ToString();
					}
					if (arg == "")
					{
						arg = "0";
					}
					index = this.comboBoxEducation.FindString(arg);
					if (index > -1)
					{
						this.comboBoxEducation.SelectedIndex = index;
					}
					index = 0;
					arg = "";

					if (dtPerson.Rows[0]["sex"].ToString() != "")
					{
						index = this.comboBoxSex.FindStringExact(dtPerson.Rows[0]["sex"].ToString());
					}
					if (index > -1)
					{
						this.comboBoxSex.SelectedIndex = index;
					}
					index = 0;

					if (dtPerson.Rows[0]["ReceivedAddon"] != null)
					{
						index = this.comboBoxReceivedAddon.FindStringExact(dtPerson.Rows[0]["ReceivedAddon"].ToString());
					}
					if (index > -1)
					{
						this.comboBoxReceivedAddon.SelectedIndex = index;
					}
					else
					{
						index = 0;
						this.comboBoxReceivedAddon.SelectedIndex = index;
					}
					index = 0;

					if (dtPerson.Rows[0]["Rang"] != null)
					{
						index = this.comboBoxRang.FindStringExact(dtPerson.Rows[0]["Rang"].ToString());
					}
					if (index > -1)
					{
						this.comboBoxRang.SelectedIndex = index;
					}
					else
					{
						index = 0;
						this.comboBoxRang.SelectedIndex = index;
					}
					index = 0;

					if (dtPerson.Rows[0]["MilitaryStatus"] != null)
					{
						index = this.comboBoxMilitaryStatus.FindStringExact(dtPerson.Rows[0]["MilitaryStatus"].ToString());
					}
					if (index > -1)
					{
						this.comboBoxMilitaryStatus.SelectedIndex = index;
					}
					else
					{
						index = 0;
						this.comboBoxMilitaryStatus.SelectedIndex = index;
					}
					index = 0;

					if (dtPerson.Rows[0]["languages"].ToString() != null)
					{
						index = this.comboBoxSpecialSkills.FindStringExact(dtPerson.Rows[0]["languages"].ToString());
					}
					if (index > -1)
					{
						this.comboBoxSpecialSkills.SelectedIndex = index;
					}
					index = 0;

					if (dtPerson.Rows[0]["egnlnch"] != null)
					{
						int.TryParse(dtPerson.Rows[0]["egnlnch"].ToString(), out index);
						if (index != 0)
						{
							this.comboBoxEGN.SelectedIndex = 1;
						}
						else
						{
							this.comboBoxEGN.SelectedIndex = 0;
						}
					}
					else
					{
						this.comboBoxEGN.SelectedIndex = 0;
					}

					if (this.dtAssignment.Rows.Count > 0)
					{
						//Трудов стаж	
						CalculatePersonalExperience();
						this.dateTimePickerPostypilNa.Value = (DateTime)this.dtAssignment.Rows[0]["AssignedAt"];
					}
					this.Text += " " + this.textBoxNames.Text;
				}
				//catch(SystemOut
				catch (System.ArgumentException e)
				{
					MessageBox.Show(e.Message, "Липсващи данни за лицето", MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.Close();
				}

				#endregion

				#region Loading Picture

				try
				{
					byte[] img = null;
					img = this.dataAdapter.SelectPicture(TableNames.Pictures, this.parent);
					if (img != null)
					{
						MemoryStream stream = new MemoryStream(img);
						pictureBox1.Image = Image.FromStream(stream);
					}
				}
				catch
				{ }
				#endregion

				PersonalDataChangedValue = false;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void CalculatePersonalExperience()
		{
			try
			{
				DataViewRowState dvrs = DataViewRowState.CurrentRows;
				DataView vueCorrection;
				this.numBoxStartYear.Text = dtAssignment.Rows[0]["years"].ToString();
				this.numBoxStartMonth.Text = dtAssignment.Rows[0]["months"].ToString();
				this.numBoxStartDay.Text = dtAssignment.Rows[0]["days"].ToString();

				DateTime AssignDate = Convert.ToDateTime(this.dtAssignment.Rows[0]["AssignedAt"]);
				//int years = (int)this.dtAssignment.Rows[0]["Years"];
				if (DateTime.Compare(DateTime.Now, AssignDate) == 1)
				{
					int AssY, AssM, AssD, CYear, CDay, CMonth, TY, TM, TD;
					// We are calculating correction here
					int CorrY = 0, CorrM = 0, CorrD = 0, CorrDays = 0;
					string cond = "isactive = 1";

					vueCorrection = new DataView(dtAssignment, cond, "id", dvrs);
					if (vueCorrection.Count > 0)
					{
						int.TryParse(vueCorrection[0]["ExperienceCorrection"].ToString(), out CorrDays);

						if (CorrDays > 0)
						{
							CorrY = CorrDays / 365;
							CorrDays -= CorrY * 365;
							CorrM = CorrDays / 30;
							CorrDays -= CorrM * 30;
							CorrD = CorrDays;
						}

						AssY = AssignDate.Year;
						AssM = AssignDate.Month;
						AssD = AssignDate.Day;
						CYear = DateTime.Now.Year - AssY - CorrY;
						if ((CMonth = DateTime.Now.Month - AssM - CorrM) < 0)
						{
							CYear--;
							CMonth += 12;
						}
						if ((CDay = DateTime.Now.Day - AssD - CorrD) < 0)
						{
							CDay += 30;
							CMonth--;
							if (CMonth < 0)
							{
								CMonth += 12;
								CYear--;
							}
						}
						TY = TM = TD = 0;
						try
						{
							TY = CYear + int.Parse(this.dtAssignment.Rows[0]["Years"].ToString());
						}
						catch (System.FormatException)
						{
							TY = 0;
						}

						try
						{
							TM = CMonth + int.Parse(this.dtAssignment.Rows[0]["Months"].ToString());
						}
						catch (System.FormatException)
						{
							TM = 0;
						}
						try
						{
							TD = CDay + int.Parse(this.dtAssignment.Rows[0]["Days"].ToString());
						}
						catch (System.FormatException)
						{
							TD = 0;
						}
						if (TD >= 30)
						{
							TM++;
							TD -= 30;
						}
						if (TM >= 12)
						{
							TM -= 12;
							TY++;
						}

						this.numBoxTotalYear.Text = TY.ToString();
						this.numBoxOrgYear.Text = CYear.ToString();
						this.numBoxTotalMonth.Text = TM.ToString();
						this.numBoxOrgMonth.Text = CMonth.ToString();
						this.numBoxTotalDay.Text = TD.ToString();
						this.numBoxOrgDay.Text = CDay.ToString();
					}
				}
				else
				{
					//MessageBox.Show("Грешка при изчисляване на трудов стаж. Моля, проверете дали датата на компютъра е вярна");
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void LoadNomenklatures()
		{
			try
			{
				//			int index;
				DataTable dts;

				#region Loading Year from Database
				DataTable tab = this.dataAdapter.SelectWhere(TableNames.Year, "*", "ORDER BY id");
				if (tab == null)
				{
					MessageBox.Show("Грешка при зареждане на номенклатури", ErrorMessages.NoConnection);
					this.Close();
				}
				this.Year = tab.Rows[0][0].ToString();
				#endregion

				#region Loading Personal Info nomenklature

				this.comboBoxFamilyStatus.DataSource = this.mainform.nomenclaatureData.arrFamilyStatus;

				this.comboBoxScienceLevel.DataSource = this.mainform.nomenclaatureData.arrScienceLevel;
				this.comboBoxScience.DataSource = this.mainform.nomenclaatureData.arrScienceTitle;
				this.comboBoxMilitaryRang.DataSource = this.mainform.nomenclaatureData.dtMilitaryDegree;
				this.comboBoxNSODegree.DataSource = this.mainform.nomenclaatureData.dtMilitaryDegree;
				this.comboBoxMilitaryRang.DisplayMember = "level";
				this.comboBoxEducation.DataSource = this.mainform.nomenclaatureData.dtEducation;
				this.comboBoxEducation.DisplayMember = "level";
				this.comboBoxSex.DataSource = this.mainform.nomenclaatureData.arrSex;
				this.comboBoxMilitaryStatus.DataSource = this.mainform.nomenclaatureData.arrMilitaryStatus;
				this.comboBoxSpecialSkills.DataSource = this.mainform.nomenclaatureData.arrSpecialSkills;
				this.comboBoxEGN.SelectedIndex = 0;

				dts = this.dataAdapter.SelectWhere(TableNames.JoinNomenklature, "*", "WHERE descriptor = 'language'");
				if (dts == null)
				{
					MessageBox.Show("Грешка при зареждане на номенклатури", ErrorMessages.NoConnection);
					this.Close();
				}

				this.dateTimePickerPostypilNa.Value = DateTime.Now;
				this.dateTimePickerPCCardPublished.Value = DateTime.Now;
				this.dateTimePickerBirthDate.Value = DateTime.Now;
				this.dateTimePickerWorkBook.Value = DateTime.Now;
				this.dateTimePickerPostypilNa.Enabled = false;

				#endregion

				#region Loading Assignment Info

				this.dtLevel1 = new DataTable();
				this.dtLevel2 = new DataTable();
				this.dtLevel3 = new DataTable();
				this.dtLevel4 = new DataTable();

				this.dtComboPosiiton.Columns.Add("PositionEng");
				this.dtComboPosiiton.Columns.Add("PositionName");
				this.dtComboPosiiton.Columns.Add("PositionCode");
				this.dtComboPosiiton.Columns.Add("Index");
				this.dtComboPosiiton.PrimaryKey = new DataColumn[] { this.dtComboPosiiton.Columns["PositionCode"] };

				this.dtLevel1.Columns.Add("Level");
				this.dtLevel1.Columns.Add("LevelEng");
				this.dtLevel1.PrimaryKey = new DataColumn[] { this.dtLevel1.Columns["level"] };

				this.dtLevel2.Columns.Add("Level");
				this.dtLevel2.Columns.Add("LevelEng");
				this.dtLevel2.PrimaryKey = new DataColumn[] { this.dtLevel2.Columns["level"] };

				this.dtLevel3.Columns.Add("Level");
				this.dtLevel3.Columns.Add("LevelEng");
				this.dtLevel3.PrimaryKey = new DataColumn[] { this.dtLevel3.Columns["level"] };

				this.dtLevel4.Columns.Add("Level");
				this.dtLevel4.Columns.Add("LevelEng");
				this.dtLevel4.PrimaryKey = new DataColumn[] { this.dtLevel4.Columns["level"] };

				this.comboBoxContract.DataSource = this.mainform.nomenclaatureData.arrContract;
				this.comboBoxAssignReason.DataSource = this.mainform.nomenclaatureData.dtReasonAssignment;
				this.comboBoxAssignReason.DisplayMember = "level";
				this.comboBoxWorkTime.DataSource = this.mainform.nomenclaatureData.dtWorkTime;
				this.comboBoxWorkTime.DisplayMember = "level";
				this.comboBoxLaw.DataSource = this.mainform.nomenclaatureData.arrLaw;
				this.comboBoxYearlyAddon.DataSource = this.mainform.nomenclaatureData.arrYearlyAddon;
				this.comboBoxRang.DataSource = this.mainform.nomenclaatureData.arrRang;

				this.labelLevel1.Text = "Администрация";
				this.labelLevel2.Text = "Дирекция";
				this.labelLevel3.Text = "Отдел";
				this.labelLevel4.Text = "Сектор";

				this.TreeLoad();

				this.dtPosition = this.dataAdapter.SelectWhere(TableNames.FirmPersonal3, "*", " ORDER BY id");
				if (this.dtPosition == null)
				{
					MessageBox.Show("Грешка при зареждане на номенклатури", ErrorMessages.NoConnection);
					this.Close();
				}
				this.dtPosition.PrimaryKey = new DataColumn[] { this.dtPosition.Columns["ID"] };

				this.dateTimePickerAssignedAt.Value = DateTime.Now;
				this.dateTimePickerContractExpiry.Value = DateTime.Now;
				this.dateTimePickerTestPeriod.Value = DateTime.Now;
				this.dateTimePickerContractDate.Value = DateTime.Now;

				#endregion

				#region Loading Absence Info
				this.dateTimePickerAbsenceFromData.Value = DateTime.Now;
				this.dateTimePickerAbsenceOrderFormData.Value = DateTime.Now;
				this.dateTimePickerAbsenceToData.Value = DateTime.Now;
				this.dateTimePickerAbsenceSicknessIssuedAtDate.Value = DateTime.Now;
				#endregion

				#region Loading Penalty Info
				this.comboBoxPenaltyReason.DataSource = this.mainform.nomenclaatureData.arrPenaltyReason;
				this.comboBoxTypePenalty.DataSource = this.mainform.nomenclaatureData.arrTypePenalty;
				this.dateTimePickerPenaltyOrderDate.Value = DateTime.Now;
				this.dateTimePickerPenaltyFromDate.Value = DateTime.Now;
				this.dateTimePickerPenaltyToDate.Value = DateTime.Now;
				this.radioButtonPenalties.Checked = true;

				#endregion

				#region Loading Notes Info
				#endregion

				#region Loading Fired Info
				this.dateTimePickerFiredFromDate.Value = DateTime.Now;
				this.comboBoxFiredReason.DataSource = this.mainform.nomenclaatureData.arrReasonFired;
				#endregion

				#region Loading Attestations Info
				this.dateTimePickerWorkPlan.Value = DateTime.Now;
				this.dateTimePickerMiddleMeetingDate.Value = DateTime.Now;
				this.dateTimePickerFinalMeeting.Value = DateTime.Now;
				this.dateTimePickerRangDate.Value = DateTime.Now;
				this.dateTimePickerPositionDate.Value = DateTime.Now;
				this.dateTimePickerObjectionDate.Value = DateTime.Now;
				this.comboBoxNewRang.DataSource = this.mainform.nomenclaatureData.arrRang;
				#endregion

				#region loading Labels
				DataSet dsLabels = new DataSet();

				try
				{
					if (File.Exists(System.Windows.Forms.Application.StartupPath + @"\XMLLabels\PersonInfo.xml"))
					{
						dsLabels.ReadXml(System.Windows.Forms.Application.StartupPath + @"\XMLLabels\PersonInfo.xml", System.Data.XmlReadMode.Auto);
						this.dtControlLabels = dsLabels.Tables[dsLabels.Tables.Count - 1];
						this.dtControlLabels.PrimaryKey = new DataColumn[] { this.dtControlLabels.Columns["program_name"] };

						this.LoadXMLLabels(this.Controls, this.dtControlLabels);
					}
				}
				catch (System.IO.FileNotFoundException)
				{
				}
				catch (System.Exception ex)
				{
					MessageBox.Show(ex.Message);
				}

				try
				{
					if (File.Exists(System.Windows.Forms.Application.StartupPath + @"\XMLLabels\PersonTabs.xml"))
					{
						DataTable dtTabs = new DataTable();
						dsLabels = new DataSet();
						dsLabels.ReadXml(System.Windows.Forms.Application.StartupPath + @"\XMLLabels\PersonTabs.xml", System.Data.XmlReadMode.Auto);
						dtTabs = dsLabels.Tables["basicquery"];
						foreach (TabPage tp in this.tabControlCardNew.TabPages)
						{
							foreach (DataRow Row in dtTabs.Rows)
							{
								if (Row["value"].ToString().ToLower() == tp.Name.ToLower())
								{
									if (Row["visible"].ToString() == "true")
										tp.Show();
									else
										this.tabControlCardNew.TabPages.Remove(tp);
								}
							}
						}

					}
				}
				catch (System.IO.FileNotFoundException)
				{
				}
				catch (System.Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
				#endregion

				#region loading rangs
				this.comboBoxNSORang.DataSource = this.mainform.nomenclaatureData.dtMilitaryRang;
				this.comboBoxNSORang.DisplayMember = "level";
				this.comboBoxNSODegree.DataSource = this.mainform.nomenclaatureData.dtMilitaryDegree;
				this.comboBoxNSODegree.DisplayMember = "level";

				#endregion

				#region loading Cards
				this.comboBoxCardMilitaryRang.DataSource = this.mainform.nomenclaatureData.dtMilitaryRang;
				this.comboBoxCardMilitaryRang.DisplayMember = "level";
				this.comboBoxCardMilitaryRangEng.DataSource = this.mainform.nomenclaatureData.arrNatoDegree;
				this.comboBoxCardMilitaryRangEng.DisplayMember = "level";
				#endregion
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void LoadXMLLabels(System.Windows.Forms.Control.ControlCollection collection, DataTable dtControls)
		{
			try
			{
				foreach (Control con in collection)
				{
					DataRow row = dtControls.Rows.Find(con.Name.ToString());
					if (row != null)
					{
						bool visible;
						con.Text = row["Client_Text"].ToString();
						string tooltip = row["tooltip"].ToString();
						if (tooltip != null && tooltip != "")
						{
							this.toolTip1.SetToolTip(con, row["tooltip"].ToString());
						}
						bool.TryParse(row["Visible"].ToString(), out visible);
						con.Visible = visible;
					}
					this.LoadXMLLabels(con.Controls, dtControls);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonPrintD_Click(object sender, System.EventArgs e)
		{
			try
			{
				OpenFileDialog openFileDialog1 = new OpenFileDialog();

				openFileDialog1.InitialDirectory = "";
				openFileDialog1.Filter = "Word Document (*.docx)|*.docx|Word Document (*.doc)|*.doc|Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
				openFileDialog1.FilterIndex = 1;
				openFileDialog1.RestoreDirectory = true;
				openFileDialog1.Multiselect = false;
				openFileDialog1.Title = "Изберете шаблон за печат";

				if (this.dataGridViewAssignment.CurrentRow == null)
				{
					MessageBox.Show("Няма валидно назначение за печат!");
					return;
				}
				try
				{
					if (openFileDialog1.ShowDialog() == DialogResult.OK)
					{
						string path;
						string filename;
						filename = System.IO.Path.GetFileName(openFileDialog1.FileName);
						path = System.IO.Path.GetDirectoryName(openFileDialog1.FileName);
						DirectoryInfo inf = new DirectoryInfo(path + @"\PrintedDocuments");
						if (!Directory.Exists(path + @"\PrintedDocuments"))
						{
							inf = Directory.CreateDirectory(path + @"\PrintedDocuments");
							if (inf.Exists == false)
							{
								MessageBox.Show("Не може да се отвори папката за шаблони на документи.");
								return;
							}
						}
						string destname;
						try
						{
							destname = inf.FullName + @"\" + this.textBoxNames.Text + " " + DateTime.Now.ToShortDateString();
							destname = destname.Replace("/", ".");
							destname += " " + filename;
							File.Copy(openFileDialog1.FileName, destname, true);
						}
						catch (Exception ex)
						{
							ErrorLog.WriteException(ex, ex.Message);
							MessageBox.Show("Не може да се отвори папката за шаблони на документи.");
							return;
						}

						this.PrintWord(destname);
					}
				}
				catch (System.Exception ex)
				{
					MessageBox.Show(ex.Message);
					MessageBox.Show(ex.GetType().ToString());
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void EnableTabs(bool IsEnabled)
		{
			try
			{
				for (int i = 0; i < this.tabControlCardNew.TabPages.Count; i++)
				{
					if (i != this.tabControlCardNew.SelectedIndex)
					{
						this.tabControlCardNew.TabPages[i].Enabled = IsEnabled;
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void JustifyGridView(DataGridView dgv, TableEnum tablename)
		{
			try
			{
				switch (tablename)
				{
					#region Assignment
					case TableEnum.ePersonAssignment:
						foreach (DataGridViewColumn Col in dgv.Columns)
						{
							switch (Col.Name.ToLower())
							{
								case "receivedaddon":
									Col.HeaderText = "Пари за дрехи";
									Col.Visible = true;
									break;
								case "level1":
									Col.HeaderText = "Администрация";
									Col.Visible = true;
									break;
								case "level2":
									Col.HeaderText = "Дирекция";
									Col.Visible = true;
									break;
								case "level3":
									Col.HeaderText = "Отдел";
									Col.Visible = true;
									break;
								case "level4":
									Col.HeaderText = "Сектор";
									Col.Visible = true;
									break;
								case "position":
									Col.HeaderText = "Длъжност";
									Col.Visible = true;
									break;
								case "contract":
									Col.HeaderText = "Договор";
									Col.Visible = true;
									break;
								case "worktime":
									Col.HeaderText = "Работно време";
									Col.Visible = true;
									break;
								case "assignedat":
									Col.HeaderText = "Назначен на";
									Col.Visible = true;
									break;
								case "staff":
									Col.HeaderText = "Щат";
									Col.Visible = true;
									break;
								default:
									Col.Visible = false;
									break;
							}
						}
						break;
					#endregion
					#region Absence
					case TableEnum.eAbsence:
						foreach (DataGridViewColumn Col in dgv.Columns)
						{
							switch (Col.Name.ToLower())
							{
								case "year":
									{
										Col.HeaderText = "Година";
										Col.Visible = true;
										break;
									}
								case "fromdate":
									{
										Col.HeaderText = "От дата";
										Col.Visible = true;
										break;
									}
								case "todate":
									{
										Col.HeaderText = "До дата";
										Col.Visible = true;
										break;
									}
								case "countdays":
									{
										Col.HeaderText = "Брой дни";
										Col.Visible = true;
										break;
									}
								case "calendardays":
									{
										Col.HeaderText = "Календарни дни";
										Col.Visible = true;
										break;
									}
								//CalendarDays
								case "typeabsence":
									{
										Col.HeaderText = "Вид отсъствие";
										Col.Visible = true;
										break;
									}

								default:
									{
										Col.Visible = false;
										break;
									}
							}
						}
						break;
					#endregion
					#region YearHoliday
					case TableEnum.eYearHoliday:
						foreach (DataGridViewColumn Col in dgv.Columns)
						{
							switch (Col.Name.ToLower())
							{
								case "year":
									Col.HeaderText = "Година";
									Col.Visible = true;
									break;
								case "leftover":
									Col.HeaderText = "Остатък";
									Col.Visible = true;
									break;
								case "total":
									Col.HeaderText = "Полагаем";
									Col.Visible = true;
									break;
								case "telk":
									Col.HeaderText = "ТЕЛК";
									Col.Visible = true;
									break;
								case "unpayed":
									Col.HeaderText = "Неплатен отпуск";
									Col.Visible = true;
									break;
								case "additional":
									Col.HeaderText = "Допълнителен платен отпуск";
									Col.Visible = true;
									break;
								case "education":
									Col.HeaderText = "Обучение";
									Col.Visible = true;
									break;
								default:
									{
										Col.Visible = false;
										break; ;
									}
							}
						}
						break;
					#endregion
					#region Penalty
					case TableEnum.ePenalty:

						foreach (DataGridViewColumn Col in dgv.Columns)
						{
							switch (Col.Name.ToLower())
							{
								case "typepenalty":
									{
										Col.HeaderText = "Вид";
										Col.Visible = true;
										break;
									}
								case "fromdate":
									{
										Col.HeaderText = "Валидно от";
										Col.Visible = true;
										break;
									}
								case "todate":
									{
										Col.HeaderText = "Валидно до";
										Col.Visible = true;
										break;
									}
								case "reason":
									{
										Col.HeaderText = "Основание";
										Col.Visible = true;
										break;
									}
								case "numberorder":
									{
										Col.HeaderText = "Номер на заповед";
										Col.Visible = true;
										break;
									}
								case "orderdate":
									{
										Col.HeaderText = "Дата на постановлението";
										Col.Visible = true;
										break;
									}
								default:
									{
										Col.Visible = false;
										break; ;
									}
							}
						}
						break;
					#endregion
					#region Fired
					case TableEnum.eFired:
						foreach (DataGridViewColumn Col in dgv.Columns)
						{
							switch (Col.Name.ToLower())
							{
								case "fromdate":
									{
										Col.HeaderText = "Считано от";
										Col.Visible = true;
										break;
									}
								case "fireorder":
									{
										Col.HeaderText = "Номер заповед";
										Col.Visible = true;
										break;
									}
								case "fireorderdate":
									{
										Col.HeaderText = "Заповед дата";
										Col.Visible = true;
										break;
									}
								case "reason":
									{
										Col.HeaderText = "Основание";
										Col.Visible = true;
										break;
									}
								default:
									{
										Col.Visible = false;
										break; ;
									}
							}
						}
						break;
					#endregion
					#region Notes
					case TableEnum.eNotesTable:
						foreach (DataGridViewColumn Col in dgv.Columns)
						{
							switch (Col.Name.ToLower())
							{
								case "text":
									{
										Col.HeaderText = "Текст";
										Col.Visible = true;
										break;
									}
								case "date":
									{
										Col.HeaderText = "Дата";
										Col.Visible = true;
										break;
									}
								case "type":
									{
										Col.HeaderText = "Тип";
										Col.Visible = true;
										break;
									}
								case "typedocument":
									{
										Col.HeaderText = "Вид документ";
										Col.Visible = true;
										break;
									}
								default:
									{
										Col.Visible = false;
										break; ;
									}
							}
						}
						break;
					#endregion
					#region Attestations
					case TableEnum.eAttestations:
						foreach (DataGridViewColumn Col in dgv.Columns)
						{
							switch (Col.Name.ToLower())
							{
								case "year":
									{
										Col.HeaderText = "Година";
										Col.Visible = true;
										break;
									}
								case "totalmark":
									{
										Col.HeaderText = "Оценка";
										Col.Visible = true;
										break;
									}
								case "hasworkplan":
									{
										Col.HeaderText = "Работен план";
										Col.Visible = true;
										break;
									}
								case "hasmiddlemeeting":
									{
										Col.HeaderText = "Междинна среща";
										Col.Visible = true;
										break;
									}
								case "hastraining":
									{
										Col.HeaderText = "Обучение";
										Col.Visible = true;
										break;
									}
								case "hasrangupdate":
									{
										Col.HeaderText = "Повишение в ранг";
										Col.Visible = true;
										break;
									}
								case "forrangupdate":
									{
										Col.HeaderText = "За повишение";
										Col.Visible = true;
										break;
									}
								default:
									{
										Col.Visible = false;
										break; ;
									}
							}
						}
						break;
					#endregion
					#region LanguageLevel
					case TableEnum.eLanguageLevel:
						foreach (DataGridViewColumn Col in dgv.Columns)
						{
							switch (Col.Name.ToLower())
							{
								case "level":
									Col.HeaderText = "Степен на владеене";
									Col.Visible = true;
									break;
								case "language":
									Col.HeaderText = "Чужд език";
									Col.Visible = true;
									break;
								default:
									Col.Visible = false;
									break; ;
							}
						}
						break;
					#endregion
					#region Educations
					case TableEnum.eEducations:
						foreach (DataGridViewColumn Col in dgv.Columns)
						{
							switch (Col.Name.ToLower())
							{
								case "area":

									Col.HeaderText = "Област";
									Col.Visible = true;
									break;

								case "theme":

									Col.HeaderText = "Тема";
									Col.Visible = true;
									break;

								case "code":

									Col.HeaderText = "Код";
									Col.Visible = true;
									break;

								case "educationdays":

									Col.HeaderText = "Брой дни";
									Col.Visible = true;
									break;

								case "educationhours":
									{
										Col.HeaderText = "Брой часове";
										Col.Visible = true;
										break;
									}
								case "price":
									Col.HeaderText = "Цена";
									Col.Visible = true;
									break;
								case "certificatedata":
									{
										Col.HeaderText = "Данни за сертификат";
										Col.Visible = true;
										break;
									}
								case "educationplace":
									{
										Col.HeaderText = "Място на провеждане";
										Col.Visible = true;
										break;
									}
								case "fromdate":
									{
										Col.HeaderText = "От дата";
										Col.Visible = true;
										break;
									}
								case "todate":
									{
										Col.HeaderText = "До дата";
										Col.Visible = true;
										break;
									}
								case "educationorganisation":
									{
										Col.HeaderText = "Обучаваща организация";
										Col.Visible = true;
										break;
									}
								default:
									Col.Visible = false;
									break; ;
							}
						}
						break;
					#endregion
					#region MilitaryRangs
					case TableEnum.eMilitaryRangs:
						foreach (DataGridViewColumn Col in dgv.Columns)
						{
							switch (Col.Name.ToLower())
							{
								case "militaryrang":
									Col.HeaderText = "Военен ранг";
									Col.Visible = true;
									break;
								case "rangordernumber":
									Col.HeaderText = "Номер на заповед";
									Col.Visible = true;
									break;
								case "rangorderdate":
									Col.HeaderText = "Дата на заповедта";
									Col.Visible = true;
									break;
								case "rangordervalidfrom":
									Col.HeaderText = "В сила от";
									Col.Visible = true;
									break;
								default:
									Col.Visible = false;
									break; ;
							}
						}
						break;
					#endregion
					#region Cards
					case TableEnum.eCards:
						foreach (DataGridViewColumn Col in dgv.Columns)
						{
							switch (Col.Name.ToLower())
							{
								case "cardnumber":
									Col.HeaderText = "Номер на карта";
									Col.Visible = true;
									break;
								case "cardseries":
									Col.HeaderText = "Серия на карта";
									Col.Visible = true;
									break;
								case "cardsign":
									Col.HeaderText = "Номер на знак";
									Col.Visible = true;
									break;
								case "cardissuedate":
									Col.HeaderText = "Дата на издаване";
									Col.Visible = true;
									break;
								default:
									Col.Visible = false;
									break; ;
							}
						}
						break;
						#endregion
				}
			}
			catch (System.Exception e)
			{
				MessageBox.Show(e.Message);
			}
		}

		private void tabControl1_SelectedIndexChanging(object sender, NewTabControl.TabPageChangeEventArgs e)
		{
			if (e.NextTab.Enabled == false)
				MessageBox.Show("Не може да сменяте страницата на досието по време на редакция.");
		}

		private int NumDigits(int number)
		{
			try
			{
				if (number == 0)
					return 1;
				int digits = 0;
				while (number > 0)
				{
					number = number / 10;
					digits++;
				}
				return digits;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				return -1;
			}
		}

		private string ConvetToHundreds(int number)
		{
			try
			{
				string Temp = new string("".ToCharArray());
				int digits = 0, dTot = 0;
				dTot = this.NumDigits(number);
				if (dTot < 0)
					return "";
				digits = dTot;

				if (digits == 3)
				{
					int smth;
					smth = number / 100;
					switch (smth)
					{
						case 0:
							{
								digits--;
								break;
							}
						case 1:
							{
								Temp += "сто";
								digits--;
								break;
							}
						case 2:
							{
								Temp += "двеста";
								digits--;
								break;
							}
						case 3:
							{
								Temp += "триста";
								digits--;
								break;
							}
						case 4:
							{
								Temp += "четири";
								break;
							}
						case 5:
							{
								Temp += "пет";
								break;
							}
						case 6:
							{
								Temp += "шест";
								break;
							}
						case 7:
							{
								Temp += "седем";
								break;
							}
						case 8:
							{
								Temp += "осем";
								break;
							}
						case 9:
							{
								Temp += "девет";
								break;
							}
					}
					if (digits == 3)
					{
						Temp += "стотин";
						digits--;
					}
					number = number - (number / 100) * 100;

				}
				if (digits == 2)
				{
					int num1 = 0, num2 = 0;
					num1 = number / 10;
					num2 = number - (number / 10) * 10;
					if (num1 == 1)
					{
						switch (num2)
						{
							case 0:
								{
									if (dTot == 3)
									{
										Temp += " и";
									}
									Temp += " десет";
									break;
								}
							case 1:
								{
									if (dTot == 3)
									{
										Temp += " и";
									}
									Temp += " единадесет";
									digits--;
									break;
								}
							case 2:
								{
									if (dTot == 3)
									{
										Temp += " и";
									}
									Temp += " дванадесет";
									digits--;
									break;
								}
							case 3:
								{
									if (dTot == 3)
									{
										Temp += " и";
									}
									Temp += " три";
									break;
								}
							case 4:
								{
									if (dTot == 3)
									{
										Temp += " и";
									}
									Temp += " четири";
									break;
								}
							case 5:
								{
									if (dTot == 3)
									{
										Temp += " и";
									}
									Temp += " пет";
									break;
								}
							case 6:
								{
									if (dTot == 3)
									{
										Temp += " и";
									}
									Temp += " шест";
									break;
								}
							case 7:
								{
									if (dTot == 3)
									{
										Temp += " и";
									}
									Temp += " седем";
									break;
								}
							case 8:
								{
									if (dTot == 3)
									{
										Temp += " и";
									}
									Temp += " осем";
									break;
								}
							case 9:
								{
									if (dTot == 3)
									{
										Temp += " и";
									}
									Temp += " девет";
									break;
								}
						}
						if (digits == 2)
						{
							Temp += "надесет";
						}
						digits = 0;
					}
					else
					{
						if (num1 != 0)
						{
							switch (num1)
							{
								case 2:
									{
										if (dTot == 3 && (number - (number / 10) * 10) == 0)
										{
											Temp += " и";
										}
										Temp += " два";
										break;
									}
								case 3:
									{
										if (dTot == 3 && (number - (number / 10) * 10) == 0)
										{
											Temp += " и";
										}
										Temp += " три";
										break;
									}
								case 4:
									{
										if (dTot == 3 && (number - (number / 10) * 10) == 0)
										{
											Temp += " и";
										}
										Temp += " четири";
										break;
									}
								case 5:
									{
										if (dTot == 3 && (number - (number / 10) * 10) == 0)
										{
											Temp += " и";
										}
										Temp += " пет";
										break;
									}
								case 6:
									{
										if (dTot == 3 && (number - (number / 10) * 10) == 0)
										{
											Temp += " и";
										}
										Temp += " шест";
										break;
									}
								case 7:
									{
										if (dTot == 3 && (number - (number / 10) * 10) == 0)
										{
											Temp += " и";
										}
										Temp += " седем";
										break;
									}
								case 8:
									{
										if (dTot == 3 && (number - (number / 10) * 10) == 0)
										{
											Temp += " и";
										}
										Temp += " осем";
										break;
									}
								case 9:
									{
										if (dTot == 3 && (number - (number / 10) * 10) == 0)
										{
											Temp += " и";
										}
										Temp += " девет";
										break;
									}
							}
							Temp += "десет";
							digits--;
						}
						else
						{
							digits--;
						}
					}
				}
				if (digits == 1)
				{
					number = number - (number / 10) * 10;
					switch (number)
					{
						case 0:
							{
								if (dTot == 1)
									Temp += " нула";
								break;
							}
						case 1:
							{
								if (dTot == 3 || dTot == 2)
								{
									Temp += " и";
								}
								Temp += " едно";
								break;
							}
						case 2:
							{
								if (dTot == 3 || dTot == 2)
								{
									Temp += " и";
								}
								Temp += " две";
								break;
							}
						case 3:
							{
								if (dTot == 3 || dTot == 2)
								{
									Temp += " и";
								}
								Temp += " три";
								break;
							}
						case 4:
							{
								if (dTot == 3 || dTot == 2)
								{
									Temp += " и";
								}
								Temp += " четири";
								break;
							}
						case 5:
							{
								if (dTot == 3 || dTot == 2)
								{
									Temp += " и";
								}
								Temp += " пет";
								break;
							}
						case 6:
							{
								if (dTot == 3 || dTot == 2)
								{
									Temp += " и";
								}
								Temp += " шест";
								break;
							}
						case 7:
							{
								if (dTot == 3 || dTot == 2)
								{
									Temp += " и";
								}
								Temp += " седем";
								break;
							}
						case 8:
							{
								if (dTot == 3 || dTot == 2)
								{
									Temp += " и";
								}
								Temp += " осем";
								break;
							}
						case 9:
							{
								if (dTot == 3 || dTot == 2)
								{
									Temp += " и";
								}
								Temp += " девет";
								break;
							}
					}
				}
				return Temp;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				return "";
			}
		}

		private string ConvertNumberToString(string numtos)
		{
			try
			{
				string[] fsplit = numtos.Split(new char[] { '.', ',' });
				int number = 0;

				int smallnum = 0, bignum = 0;
				string Temp = new string("".ToCharArray());

				if (fsplit.Length >= 1)
				{
					int.TryParse(fsplit[0], out number);
				}
				int position = this.NumDigits(number);
				if (position < 0)
					return "";
				if (position > 3)
				{
					bignum = (number / 1000) * 1000;
					smallnum = number - bignum;
					bignum /= 1000;
					Temp = this.ConvetToHundreds(smallnum);
					string[] arr = Temp.Split(new char[] { ' ' }, 100);
					if (arr.Length <= 2)
					{
						Temp = Temp.Insert(0, " и ");
					}
					if (bignum == 1)
					{
						Temp = Temp.Insert(0, "хиляда ");
					}
					else
					{
						Temp = Temp.Insert(0, " хиляди ");
						Temp = Temp.Insert(0, this.ConvetToHundreds(bignum));
					}
				}
				else
				{
					Temp = this.ConvetToHundreds(number);
				}
				if (fsplit.Length > 1)
				{
					int.TryParse(fsplit[1], out number);
					Temp += " и " + this.ConvetToHundreds(number) + " стотинки";
				}
				return Temp;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				return "";
			}
		}

		private string ConvertMonthDifference(DateTime dateStart, DateTime dateEnd)
		{
			try
			{
				int year = dateEnd.Year - dateStart.Year;
				int month = dateEnd.Month - dateStart.Month;
				int total = year * 12 + month;
				return total.ToString();
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				return "";
			}
		}

		private int FindYearHolidayIndex()
		{
			for (int i = 0; i < this.dtYearHoliday.Rows.Count; i++)
			{
				if (this.dtYearHoliday.Rows[i]["year"].ToString() == this.comboBoxAbsenceForYear.Text)
				{
					return i;
				}
			}
			return -1;
		}

		private void PrintWord(string DocName)
		{
			try
			{
				var WordApp = new Word.Application();
				Word.Document aDoc = null;
				try
				{
					object Filename = DocName;

					DataTable firmpersonal = new DataTable();
					if (this.dataGridViewAssignment.CurrentRow == null)
					{
						MessageBox.Show("Няма избрано назначение", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
					firmpersonal = this.dataAdapter.SelectWhere(TableNames.FirmPersonal3, "*", "WHERE id = " + this.dataGridViewAssignment.CurrentRow.Cells["positionid"].Value.ToString());
					DataTable fired = this.dataAdapter.SelectWhere(TableNames.Fired, "*", "");

					//DataTable YearHoliday = this.dataAdapter.SelectWhere(TableNames.YearHoliday, "*", "WHERE parent = " + this.parent);

					if ((firmpersonal == null) || (fired == null))
					{
						MessageBox.Show("Грешка при зареждане на структурата на организацията");
						return;
					}

					aDoc = WordApp.Documents.Open(ref Filename, ref missing, ref vk_false, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing/*, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing*/);

					try
					{
						bool probably = false, sure = false;
						//Ред за предходно назначение
						DataRow PrevAssRow = this.dtAssignment.Rows.Find(this.dataGridViewAssignment.CurrentRow.Cells["PrevAssignmentID"].Value);
						DataRow Row = this.mainform.nomenclaatureData.dtAdminTable.Rows[0];

						DataRow firedR = null;
						if (fired.Rows.Count - 1 > 0)
						{
							firedR = fired.Rows[fired.Rows.Count - 1];
						}

						Range OldRng = null;

						foreach (Range rng in aDoc.Words)
						{
							if (probably == true)
							{
								NumericConversions NC = new NumericConversions();
								probably = false;

								#region Replacements
								sure = true;
								switch (rng.Text)
								{
									#region Personal Data
									case "1":
										{
											rng.Text = this.numBoxEgn.Text;
											break;
										}
									case "2":
										{
											rng.Text = this.textBoxNames.Text;
											break;
										}
									case "3":
										{
											rng.Text = this.textBoxBornTown.Text;
											break;
										}
									case "4":
										{
											rng.Text = this.textBoxCountry.Text;
											break;
										}
									case "5":
										{
											rng.Text = this.textBoxRegion.Text;
											break;
										}
									case "6":
										{
											rng.Text = this.textBoxTown.Text;
											break;
										}
									case "7":
										{
											rng.Text = this.textBoxKwartal.Text;
											break;
										}
									case "10":
										{
											rng.Text = this.textBoxTelephone.Text;
											break;
										}
									case "11":
										{
											rng.Text = this.numBoxPcCard.Text;
											break;
										}
									case "12":
										{
											rng.Text = this.dateTimePickerPCCardPublished.Text;
											break;
										}
									case "13":
										{
											rng.Text = this.textBoxPublishedFrom.Text;
											break;
										}
									case "14":
										{
											rng.Text = this.comboBoxFamilyStatus.Text;
											break;
										}
									case "15":
										{
											rng.Text = this.comboBoxEducation.Text;
											break;
										}
									case "16":
										{
											rng.Text = this.textBoxDiplom.Text;
											break;
										}
									case "18":
										{
											rng.Text = this.comboBoxRang.Text;
											break;
										}
									case "19":
										{
											rng.Text = this.comboBoxScience.Text;
											break;
										}
									case "20":
										{
											rng.Text = this.comboBoxScienceLevel.Text;
											break;
										}
									case "21":
										{
											rng.Text = this.comboBoxMilitaryRang.Text;
											break;
										}
									case "22":
										rng.Text = this.comboBoxMilitaryStatus.Text;
										break;
									case "123":
										rng.Text = this.textBoxSpeciality.Text;
										break;
									case "149":
										rng.Text = this.textBoxOther.Text;
										break;
									case "154":
										rng.Text = this.textBoxCurrentAddress.Text;
										break;
									case "159":
										rng.Text = this.textBoxEngName.Text;
										break;
									case "160":
										rng.Text = this.textBoxOther1.Text;
										break;
									case "161":
										rng.Text = this.textBoxOther2.Text;
										break;
									case "162":
										rng.Text = this.textBoxOther3.Text;
										break;
									case "163":
										rng.Text = this.textBoxOther4.Text;
										break;
									case "164":
										if (this.comboBoxEducation.SelectedIndex > 0)
										{//if we have somethig selected and not the empty field
											if (this.mainform.nomenclaatureData.dtEducation.Rows.Count > 0)
											{
												rng.Text = this.mainform.nomenclaatureData.dtEducation.Rows[this.comboBoxEducation.SelectedIndex]["englevel"].ToString();
											}
										}
										else
										{
											rng.Text = "";
										}
										break;
									case "195":
										{
											rng.Text = this.comboBoxNSORang.Text;
										}
										break;
									#endregion
									#region Assignment
									case "25":
										rng.Text = this.dateTimePickerPostypilNa.Text;
										break;
									case "26":
										rng.Text = this.numBoxAssignmentExpY.Text;
										break;
									case "91":
										rng.Text = this.numBoxAssignmentExtM.Text;
										break;
									case "92":
										rng.Text = this.numBoxAssignmentExpD.Text;
										break;
									case "130":
										rng.Text = this.comboBoxLevel1.Text;
										break;
									case "27":
										rng.Text = this.comboBoxLevel2.Text;
										break;
									case "28":
										{
											rng.Text = this.comboBoxLevel3.Text;
											break;
										}
									case "29":
										{
											rng.Text = this.comboBoxLevel4.Text;
											break;
										}
									case "177":
										{
											rng.Text = this.dataGridViewAssignment.CurrentRow.Cells["level1eng"].Value.ToString();
											break;
										}
									case "178":
										{
											rng.Text = this.dataGridViewAssignment.CurrentRow.Cells["level2eng"].Value.ToString();
											break;
										}
									case "179":
										{
											rng.Text = this.dataGridViewAssignment.CurrentRow.Cells["level3eng"].Value.ToString();
											break;
										}
									case "180":
										{
											rng.Text = this.dataGridViewAssignment.CurrentRow.Cells["level4eng"].Value.ToString();
											break;
										}
									case "30":
										{
											rng.Text = this.comboBoxPosition.Text;
											break;
										}
									case "31":
										{
											rng.Text = this.comboBoxContract.Text;
											break;
										}
									case "32":
										{
											rng.Text = this.comboBoxWorkTime.Text;
											break;
										}
									case "33":
										{
											rng.Text = this.dateTimePickerAssignedAt.Text;
											break;
										}
									case "34":
										{
											rng.Text = this.comboBoxAssignReason.Text;
											break;
										}
									case "36":
										{
											rng.Text = this.textBoxContractNumber.Text;
											break;
										}
									case "37":
										rng.Text = this.dateTimePickerContractExpiry.Text;
										break;
									case "39":
										rng.Text = this.numBoxBaseSalary.Text;
										break;
									case "171":
										rng.Text = NC.changeNumericToWords(this.numBoxBaseSalary.Text).ToLower();
										break;
									case "40":
										rng.Text = this.textBoxSalaryAddon.Text;
										break;
									case "41":
										rng.Text = this.textBoxClassPercent.Text;
										break;
									case "93":
										rng.Text = this.dataGridViewAssignment.CurrentRow.Cells["ParentContractID"].Value.ToString();
										break;
									case "94":
										DateTime tmp = new DateTime();
										if (DateTime.TryParse(this.dataGridViewAssignment.CurrentRow.Cells["ParentContractDate"].Value.ToString(), out tmp))
										{
											rng.Text = tmp.ToShortDateString();
										}
										else
										{
											rng.Text = "";
										}
										break;
									case "95":
										rng.Text = this.dataGridViewAssignment.CurrentRow.Cells["EKDALevel"].Value.ToString();
										break;
									case "96":
										{
											rng.Text = this.dataGridViewAssignment.CurrentRow.Cells["EKDACode"].Value.ToString();
											break;
										}
									case "97":
										{
											rng.Text = this.textBoxNKPLevel.Text;
											break;
										}
									case "98":
										{
											rng.Text = this.textBoxNKPCode.Text;
											break;
										}
									case "99":
										{
											rng.Text = this.numBoxMonthlyAddon.Text;
											break;
										}
									case "172":
										{
											rng.Text = NC.changeNumericToWords(this.numBoxMonthlyAddon.Text).ToLower();
											break;
										}
									case "173":
										{
											try
											{
												string sal = this.ConvertNumberToString(this.numBoxMonthlyAddon.Text);
												rng.Text = sal;
											}
											catch
											{
												rng.Text = "";
											}
											break;
										}
									case "100":
										{
											rng.Text = this.numBoxNumHoliday.Text;
											break;
										}
									case "118":
										{
											if (this.numBoxAddNumHoliday.Text != "")
											{
												rng.Text = "+ " + this.numBoxAddNumHoliday.Text;
											}
											else
											{
												rng.Text = "";
											}
											break;
										}
									case "122":
										{
											if (this.comboBoxSex.Text == "Мъж")
											{
												rng.Text = "";
											}
											else
											{
												rng.Text = "а";
												Range Del = rng.Previous(ref missing, ref missing);
												Del.Text = "";
											}
											break;
										}
									case "119":
										{
											int norm, add;
											try
											{
												norm = int.Parse(this.numBoxNumHoliday.Text);
											}
											catch (System.FormatException)
											{
												norm = 0;
											}
											try
											{
												add = int.Parse(this.numBoxAddNumHoliday.Text);
											}
											catch (System.FormatException)
											{
												add = 0;
											}
											norm += add;

											rng.Text = norm.ToString();
											break;
										}
									case "101":
										{
											rng.Text = this.dataGridViewAssignment.CurrentRow.Cells["Rang"].Value.ToString();
											break;
										}
									case "102":
										{
											rng.Text = this.numBoxAssignmentExpY.Text;
											break;
										}
									case "174":
										{
											rng.Text = NC.changeNumericToWords(this.numBoxAssignmentExpY.Text).ToLower();
											break;
										}
									case "175":
										{
											try
											{
												string sal = this.ConvertNumberToString(this.numBoxAssignmentExpY.Text);
												rng.Text = sal;
											}
											catch
											{
												rng.Text = "";
											}
											break;
										}
									case "103":
										{
											rng.Text = this.numBoxAssignmentExtM.Text;
											break;
										}
									case "104":
										{
											rng.Text = this.numBoxAssignmentExpD.Text;
											break;
										}
									case "190":
										rng.Text = this.numBoxOrgYear.Text;
										break;
									case "191":
										rng.Text = this.numBoxOrgMonth.Text;
										break;
									case "192":
										rng.Text = this.numBoxOrgDay.Text;
										break;
									case "105":
										rng.Text = this.dtAssignment.Rows[0]["ContractNumber"].ToString();
										break;
									case "176":
										try
										{
											DateTime cd = new DateTime();
											cd = DateTime.Parse(this.dtAssignment.Rows[0]["ParentContractDate"].ToString());
											rng.Text = cd.ToShortDateString();
										}
										catch
										{
											rng.Text = "";
										}
										break;
									case "106":
										rng.Text = this.dateTimePickerContractDate.Text;
										break;
									case "107":
										{
											rng.Text = this.dateTimePickerTestPeriod.Text;
											break;

										}
									case "120":
										{
											try
											{
												string sal = this.ConvertNumberToString(this.numBoxBaseSalary.Text);
												rng.Text = sal;
											}
											catch
											{
												rng.Text = "";
											}
											break;
										}
									case "121":
										{
											rng.Text = this.ConvertMonthDifference(this.dateTimePickerAssignedAt.Value, dateTimePickerTestPeriod.Value);
											break;
										}
									case "111":
										{
											DateTime AssignDate = Convert.ToDateTime(this.dtAssignment.Rows[0]["AssignedAt"].ToString());
											rng.Text = AssignDate.ToString("dd.MM.yyyy") + "г.";
											break;
										}
									case "108":
										{
											if (this.comboBoxLevel2.Text != "")
											{
												rng.Text = "\\par\r\n в " + this.comboBoxLevel2.Text;
											}
											else if (this.comboBoxLevel1.Text != "")
											{
												rng.Text = "\\par\r\n в " + this.comboBoxLevel1.Text;
											}
											break;
										}
									case "109":
										{
											if (this.comboBoxLevel3.Text != "")
											{
												rng.Text = "\\par\r\n в " + this.comboBoxLevel3.Text;
											}
											else
											{
												rng.Text = "";
											}
											break;
										}
									case "110":
										{
											if (this.comboBoxLevel4.Text != "")
											{
												rng.Text = "\\par\r\n в " + this.comboBoxLevel4.Text;
											}
											else
											{
												rng.Text = "";
											}
											break;
										}
									case "112":
										{
											if (this.comboBoxLevel2.Text != "")
											{
												rng.Text = "\\par\r\n " + this.comboBoxLevel2.Text;
											}
											else if (this.comboBoxLevel1.Text != "")
											{
												rng.Text = "\\par\r\n " + this.comboBoxLevel1.Text;
											}
											else
											{
												rng.Text = "";
											}
											break;
										}
									case "113":
										{
											if (this.comboBoxLevel3.Text != "")
											{
												rng.Text = "\\par\r\n " + this.comboBoxLevel3.Text;
											}
											else
											{
												rng.Text = "";
											}
											break;
										}
									case "114":
										{
											if (this.comboBoxLevel4.Text != "")
											{
												rng.Text = "\\par\r\n " + this.comboBoxLevel4.Text;
											}
											else
											{
												rng.Text = "";
											}
											break;
										}
									case "115":
										{
											if (this.comboBoxLevel2.Text != "")
											{
												rng.Text = "в " + this.comboBoxLevel2.Text;
											}
											else if (this.comboBoxLevel1.Text != "")
											{
												rng.Text = "в " + this.comboBoxLevel1.Text;
											}
											else
											{
												rng.Text = "";
											}
											break;
										}
									case "116":
										{
											if (this.comboBoxLevel3.Text != "")
											{
												rng.Text = "в " + this.comboBoxLevel3.Text;
											}
											else
											{
												rng.Text = "";
											}
											break;
										}
									case "117":
										{
											if (this.comboBoxLevel4.Text != "")
											{
												rng.Text = "в " + this.comboBoxLevel4.Text;
											}
											else
											{
												rng.Text = "";
											}
											break;
										}
									case "138":
										{
											rng.Text = this.comboBoxLevel1.Text;
											break;
										}
									case "139":
										{
											rng.Text = this.comboBoxLevel2.Text;
											break;
										}
									case "140":
										{
											rng.Text = this.comboBoxLevel3.Text;
											break;
										}
									case "141":
										{
											rng.Text = this.comboBoxLevel4.Text;
											break;
										}
									case "134":
										{
											if (PrevAssRow != null && PrevAssRow["Level1"].ToString() != "")
											{
												rng.Text = PrevAssRow["Level1"].ToString();
											}
											else
											{
												rng.Text = "";
											}
											break;
										}
									case "135":
										{
											if (PrevAssRow != null && PrevAssRow["Level2"].ToString() != "")
											{
												rng.Text = ", " + PrevAssRow["Level2"].ToString();
											}
											else
											{
												rng.Text = "";
											}
											break;
										}
									case "136":
										{
											if (PrevAssRow != null && PrevAssRow["Level3"].ToString() != "")
											{
												rng.Text = ", " + PrevAssRow["Level3"].ToString();
											}
											else
											{
												rng.Text = "";
											}
											break;
										}
									case "137":
										{
											if (PrevAssRow != null && PrevAssRow["Level4"].ToString() != "")
											{
												rng.Text = ", " + PrevAssRow["Level4"].ToString();
											}
											else
											{
												rng.Text = "";
											}
											break;
										}
									case "142":
										{
											if (PrevAssRow != null)
											{
												rng.Text = PrevAssRow["Position"].ToString();
											}
											break;
										}
									case "145":
										{
											float classpercent, basesalary, result;
											try
											{
												if (this.numBoxBaseSalary.Text != "")
												{
													basesalary = float.Parse(this.numBoxBaseSalary.Text);
												}
												else
												{
													basesalary = 0;
												}
											}
											catch (Exception exc)
											{
												basesalary = 0;
												MessageBox.Show(exc.Message + exc.GetType().ToString(), "Некоректно въведена основна заплата");
											}
											try
											{
												if (this.textBoxClassPercent.Text != "")
												{
													classpercent = float.Parse(this.textBoxClassPercent.Text);
												}
												else
												{
													classpercent = 0;
												}
											}
											catch (Exception exc)
											{
												classpercent = 0;
												MessageBox.Show(exc.Message + exc.GetType().ToString(), "Некоректно въведен % прослужено време");
											}
											result = basesalary * classpercent / 100;
											string res = string.Format("{0:f}", result);
											rng.Text = res;
											break;
										}
									case "146":
										{
											float classpercent, basesalary, addon, result;
											try
											{
												if (this.numBoxBaseSalary.Text != "")
												{
													basesalary = float.Parse(this.numBoxBaseSalary.Text);
												}
												else
												{
													basesalary = 0;
												}
											}
											catch (Exception exc)
											{
												basesalary = 0;
												MessageBox.Show(exc.Message + exc.GetType().ToString(), "Некоректно въведена основна заплата.");
											}
											try
											{
												if (this.textBoxSalaryAddon.Text != "")
												{
													addon = float.Parse(this.textBoxSalaryAddon.Text);
												}
												else
												{
													addon = 0;
												}
											}
											catch (Exception exc)
											{
												addon = 0;
												MessageBox.Show(exc.Message + exc.GetType().ToString(), "Некоректно въведени добавки към основната заплата.");
											}
											try
											{
												if (this.textBoxClassPercent.Text != "")
												{
													classpercent = float.Parse(this.textBoxClassPercent.Text);
												}
												else
												{
													classpercent = 0;
												}
											}
											catch (Exception exc)
											{
												classpercent = 0;
												MessageBox.Show(exc.Message + exc.GetType().ToString(), "Некоректно въведен % прослужено време.");
											}
											result = (basesalary + addon) * classpercent / 100;
											string res = string.Format("{0:f}", result);
											rng.Text = res;
											break;
										}
									case "147":
										{
											float classpercent, basesalary, addon, result;
											try
											{
												if (this.numBoxBaseSalary.Text != "")
												{
													basesalary = float.Parse(this.numBoxBaseSalary.Text);
												}
												else
												{
													basesalary = 0;
												}
											}
											catch (Exception exc)
											{
												basesalary = 0;
												MessageBox.Show(exc.Message + exc.GetType().ToString(), "Некоректно въведена основна заплата.");
											}
											try
											{
												if (this.textBoxSalaryAddon.Text != "")
												{
													addon = float.Parse(this.textBoxSalaryAddon.Text);
												}
												else
												{
													addon = 0;
												}
											}
											catch (Exception exc)
											{
												addon = 0;
												MessageBox.Show(exc.Message + exc.GetType().ToString(), "Некоректно въведени добавки към основната заплата.");
											}
											try
											{
												if (this.textBoxClassPercent.Text != "")
												{
													classpercent = float.Parse(this.textBoxClassPercent.Text);
												}
												else
												{
													classpercent = 0;
												}
											}
											catch (Exception exc)
											{
												classpercent = 0;
												MessageBox.Show(exc.Message + exc.GetType().ToString(), "Некоректно въведен % прослужено време.");
											}
											result = ((basesalary * addon / 100) + basesalary) * classpercent / 100;
											string res = string.Format("{0:f}", result);
											rng.Text = res;
											break;
										}
									case "148":
										{
											rng.Text = this.comboBoxYearlyAddon.Text;
											break;
										}
									case "157":
										{
											rng.Text = this.comboBoxTutorName.Text;
											break;
										}
									case "158":
										rng.Text = this.comboBoxTutorAbsenceReason.Text;
										break;
									case "165":
										if (this.comboBoxPosition.SelectedIndex > 0)
										{//if we have somethig selected and not the empty field
											if (this.comboBoxPosition.DataSource != null)
											{
												DataTable tab = (DataTable)this.comboBoxPosition.DataSource;

												rng.Text = tab.Rows[this.comboBoxPosition.SelectedIndex]["positioneng"].ToString();
											}
										}
										else
										{
											rng.Text = "";
										}
										break;
									#endregion
									#region Absence
									case "44":
										{
											rng.Text = this.dateTimePickerAbsenceFromData.Text;
											break;
										}
									case "45":
										{
											rng.Text = this.dateTimePickerAbsenceToData.Text;
											break;
										}
									case "46":
										{
											rng.Text = this.numBoxAbsenceWorkDays.Text;
											break;
										}
									case "47":
										{
											rng.Text = this.comboBoxAbsenceTypeAbsence.Text;
											break;
										}
									case "48":
										{
											rng.Text = this.textBoxAbsenceAttachment7.Text;
											break;
										}
									case "49":
										{
											rng.Text = this.textBoxAbsenceNumberOrder.Text;
											break;
										}
									case "50":
										{
											rng.Text = this.dateTimePickerAbsenceOrderFormData.Text;
											break;
										}
									case "52":
										{
											rng.Text = this.comboBoxAbsenceForYear.Text;
											break;
										}
									case "56":
										{
											int index = this.FindYearHolidayIndex();
											rng.Text = this.dtYearHoliday.Rows[index]["total"].ToString();
											break;
										}
									case "57":
										{
											int index = this.FindYearHolidayIndex();
											if (index >= 0)
											{
												int total = 0, leftover = 0;
												int.TryParse(dtYearHoliday.Rows[index]["total"].ToString(), out total);
												int.TryParse(dtYearHoliday.Rows[index]["leftover"].ToString(), out leftover);
												rng.Text = (total - leftover).ToString();
											}
											break;
										}
									case "58":
										{
											int index = this.FindYearHolidayIndex();
											rng.Text = this.dtYearHoliday.Rows[index]["leftover"].ToString();
											break;
										}
									case "152":
										{
											string HolidayHistory;
											HolidayHistory = "Година\tОстатък" + "\n";
											foreach (DataRow Riggin in this.dtYearHoliday.Rows)
											{
												HolidayHistory += Riggin["year"] + "\t\t" + Riggin["leftover"] + "\n";
											}
											rng.Text = HolidayHistory;
											break;
										}
									case "186":
										{
											int index = this.FindYearHolidayIndex();
											rng.Text = this.dtYearHoliday.Rows[index]["unpayed"].ToString();
											break;
										}
									case "187":
										{
											int index = this.FindYearHolidayIndex();
											rng.Text = this.dtYearHoliday.Rows[index]["additional"].ToString();
											break;
										}
									case "188":
										{
											int index = this.FindYearHolidayIndex();
											rng.Text = this.dtYearHoliday.Rows[index]["education"].ToString();
											break;
										}
									case "189":
										{
											int index = this.FindYearHolidayIndex();
											rng.Text = this.dtYearHoliday.Rows[index]["telk"].ToString();
											break;
										}
									#endregion
									#region Penalty
									case "59":
										{
											rng.Text = this.dateTimePickerPenaltyFromDate.Text;
											break;
										}
									case "60":
										{
											rng.Text = this.comboBoxPenaltyReason.Text;
											break;
										}
									case "61":
										{
											rng.Text = this.textBoxPenaltyNumberOrder.Text;
											break;
										}
									case "62":
										{
											rng.Text = this.dateTimePickerPenaltyOrderDate.Text;
											break;
										}
									#endregion
									case "63":
										{
											//rng.Text = this.textBoxNotes.Text;
											break;
										}
									#region AdminInfo
									case "42":
										{
											rng.Text = Row["NKIDLevel"].ToString();
											break;
										}
									case "43":
										{
											rng.Text = Row["NKIDCode"].ToString();
											break;
										}
									case "64":
										{
											rng.Text = Row["firmname"].ToString();
											break;
										}
									case "65":
										{
											rng.Text = Row["type"].ToString();
											break;
										}
									case "66":
										{
											rng.Text = Row["kind"].ToString();
											break;
										}
									case "67":
										{
											rng.Text = Row["region"].ToString();
											break;
										}
									case "68":
										{
											rng.Text = Row["town"].ToString();
											break;
										}
									case "69":
										{
											rng.Text = Row["postalcode"].ToString();
											break;
										}
									case "70":
										{
											rng.Text = Row["addressdata"].ToString();
											break;
										}
									case "71":
										{
											rng.Text = Row["email"].ToString();
											break;
										}
									case "72":
										{
											rng.Text = Row["phone"].ToString();
											break;
										}
									case "73":
										{
											rng.Text = Row["nominalemployees"].ToString();
											break;
										}
									case "74":
										{
											rng.Text = Row["securenumber"].ToString();
											break;
										}
									case "75":
										{
											rng.Text = Row["directorname"].ToString();
											break;
										}
									case "76":
										{
											rng.Text = Row["egndirector"].ToString();
											break;
										}
									case "77":
										{
											rng.Text = Row["directorlsys"].ToString();
											break;
										}
									case "78":
										{
											rng.Text = Row["egndirectorlsys"].ToString();
											break;
										}
									case "79":
										{
											rng.Text = Row["mainaccountantname"].ToString();
											break;
										}
									case "80":
										{
											rng.Text = Row["egnmainaccountant"].ToString();
											break;
										}
									case "81":
										{
											rng.Text = Row["mainconsult"].ToString();
											break;
										}
									case "82":
										{
											rng.Text = Row["egnmainconsult"].ToString();
											break;
										}
									case "83":
										{
											rng.Text = Row["trz"].ToString();
											break;
										}
									case "84":
										{
											rng.Text = Row["egntrz"].ToString();
											break;
										}
									case "85":
										{
											rng.Text = Row["bankname"].ToString();
											break;
										}
									case "86":
										{
											rng.Text = Row["bankaccount"].ToString();
											break;
										}
									case "87":
										{
											rng.Text = Row["bankcode"].ToString();
											break;
										}
									case "88":
										{
											rng.Text = System.DateTime.Now.Date.ToString();
											break;
										}
									case "89":
										{
											rng.Text = Row["bulstat"].ToString();
											break;
										}
									case "90":
										{
											rng.Text = Row["taxNum"].ToString();
											break;
										}
									case "143":
										{
											rng.Text = Row["NKIDCode"].ToString();
											break;
										}
									case "144":
										{
											rng.Text = Row["NKIDLevel"].ToString();
											break;
										}
									#endregion
									#region DLHak
									case "124":
										{
											if (firmpersonal.Rows.Count > 0 && firmpersonal.Rows[0]["NKPCode"].ToString().Length > 0)
											{
												int pic = int.Parse(firmpersonal.Rows[0]["NKPCode"].ToString().Substring(0, 1));
												rng.Text = this.mainform.nomenclaatureData.arrNKPClass[pic].ToString();
											}
											else
											{
												rng.Text = "";
											}
											break;
										}
									case "125":
										{
											if (firmpersonal.Rows.Count > 0)
											{
												rng.Text = firmpersonal.Rows[0]["BasicDuties"].ToString();
											}
											break;
										}
									case "126":
										{
											if (firmpersonal.Rows.Count > 0)
											{
												rng.Text = firmpersonal.Rows[0]["BasicResponsibilities"].ToString();
											}
											break;
										}
									case "127":
										{
											if (firmpersonal.Rows.Count > 0)
											{
												rng.Text = firmpersonal.Rows[0]["Connections"].ToString();
											}
											break;
										}
									case "128":
										{
											if (firmpersonal.Rows.Count > 0)
											{
												rng.Text = firmpersonal.Rows[0]["Competence"].ToString();
											}
											break;
										}
									case "129":
										{
											if (firmpersonal.Rows.Count > 0)
											{
												rng.Text = firmpersonal.Rows[0]["Requirements"].ToString();
											}
											break;
										}
									#endregion
									#region Fired
									case "131":
										{
											rng.Text = this.textBoxFireOrder.Text;
											break;
										}
									case "132":
										{
											rng.Text = this.dateTimePickerFiredFromDate.Text;
											break;
										}
									case "133":
										{
											rng.Text = this.dateTimePickerFiredFromDate.Text;
											break;
										}
									case "150":
										{
											rng.Text = this.comboBoxFiredReason.Text;
											break;
										}
									case "151":
										{
											if (firedR != null)
											{
												rng.Text = firedR["countDays"].ToString();
											}
											else
											{
												rng.Text = "";
											}
											break;
										}
									case "181":
										if (firedR != null)
										{
											rng.Text = firedR["level1"].ToString();
										}
										else
										{
											rng.Text = "";
										}
										break;
									case "182":
										if (firedR != null)
										{
											rng.Text = firedR["level2"].ToString();
										}
										else
										{
											rng.Text = "";
										}
										break;
									case "183":
										if (firedR != null)
										{
											rng.Text = firedR["level3"].ToString();
										}
										else
										{
											rng.Text = "";
										}
										break;
									case "184":
										if (firedR != null)
										{
											rng.Text = firedR["level4"].ToString();
										}
										else
										{
											rng.Text = "";
										}
										break;
									case "185":
										if (firedR != null)
										{
											rng.Text = firedR["position"].ToString();
										}
										else
										{
											rng.Text = "";
										}
										break;
									#endregion
									#endregion
									default:
										{
											OldRng.Text = "<";
											sure = false;
											break;
										}
								}
							}
							object two = 1;

							if (probably == false && rng.Text == "<")
							{
								probably = true;
								if (rng.Previous(ref missing, ref missing).ToString() != " ")
								{
									rng.Text = "";
								}
								else
								{
									rng.Text = "";
								}
								OldRng = rng;
							}
							if (sure == true && rng.Text != null && rng.Text.ToString().TrimEnd(' ') == ">")
							{
								if (rng.Text.ToString() != rng.Text.ToString().Trim())
								{
									rng.Text = " ";
								}
								else
								{
									rng.Text = " ";
								}
								sure = false;
							}

							if (sure == true && rng.Text != null && rng.Text.ToString().Trim() == ".>")
							{
								rng.Text = ".";
								sure = false;
							}
							if (sure == true && rng.Text != null && rng.Text.ToString().StartsWith(">"))
							{
								rng.Text = rng.Text.TrimStart('>');
								sure = false;
							}
							if (sure == true && rng.Text != null && rng.Text.ToString().TrimStart('"').StartsWith(">"))
							{
								rng.Text = rng.Text.Replace(">", "");
								sure = false;
							}
						}
						aDoc.Save();
						WordApp.Visible = true;
						System.Runtime.InteropServices.Marshal.ReleaseComObject(aDoc);
						System.Runtime.InteropServices.Marshal.ReleaseComObject(WordApp);
					}
					catch (System.Exception ex)
					{
						aDoc.Close(ref vk_false, ref missing, ref missing);
						System.Runtime.InteropServices.Marshal.ReleaseComObject(aDoc);
						aDoc = null;
						WordApp.Quit(ref vk_false, ref missing, ref missing);
						System.Runtime.InteropServices.Marshal.ReleaseComObject(WordApp);
						WordApp = null;
						MessageBox.Show(ex.Message);
						MessageBox.Show(ex.GetType().ToString());
						return;
					}
				}
				catch (System.Exception ex)
				{
					if (aDoc != null)
					{
						aDoc.Close(ref vk_false, ref missing, ref missing);
						System.Runtime.InteropServices.Marshal.ReleaseComObject(aDoc);
						aDoc = null;
					}
					if (WordApp != null)
					{
						WordApp.Quit(ref vk_false, ref missing, ref missing);
						System.Runtime.InteropServices.Marshal.ReleaseComObject(WordApp);
						WordApp = null;
					}
					MessageBox.Show(ex.Message + ex.GetType().ToString(), "Нa компютъра няма инсталиран Microsoft Word");
					return;
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void AddDictToTable(Dictionary<string, object> Dict, DataTable dta)
		{
			try
			{
				DataRow row = dta.NewRow();
				foreach (KeyValuePair<string, object> kvp in Dict)
				{
					row[kvp.Key] = Dict[kvp.Key];
				}
				dta.Rows.Add(row);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void AddDictToTableObject(Dictionary<string, object> Dict, DataTable dta)
		{
			try
			{
				DataRow row = dta.NewRow();
				foreach (KeyValuePair<string, object> kvp in Dict)
				{
					row[kvp.Key] = Dict[kvp.Key];
				}
				dta.Rows.Add(row);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void UpdateDictToRow(Dictionary<string, object> Dict, DataRow row)
		{
			try
			{
				foreach (KeyValuePair<string, object> kvp in Dict)
				{
					row[kvp.Key] = Dict[kvp.Key];
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void UpdateDictToRowObject(Dictionary<string, object> Dict, DataRow row)
		{
			try
			{
				foreach (KeyValuePair<string, object> kvp in Dict)
				{
					row[kvp.Key] = Dict[kvp.Key];
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void UpdateKartotekaTable(Dictionary<string, string> Dict, int id)
		{
			try
			{
				DataTable dt = this.mainform.formKartoteka.GridDataSource;
				DataRow row = dt.Rows.Find(id);
				foreach (DataColumn col in dt.Columns)
				{
					if (Dict.ContainsKey(col.ColumnName))
					{
						row[col.ColumnName] = Dict[col.ColumnName];
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void InsertInKartotekaTable(Dictionary<string, string> Dict, int id)
		{
			try
			{
				DataTable dt = this.mainform.formKartoteka.GridDataSource;
				DataRow row = dt.Rows.Find(id);
				bool add = false; //if nothing added this will stay false
				foreach (DataColumn col in dt.Columns)
				{
					if (Dict.ContainsKey(col.ColumnName))
					{
						row[col.ColumnName] = Dict[col.ColumnName];
						add = true;
					}
				}
				if (add)
				{
					dt.Rows.Add(row);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void SetComboIndex(ComboBox combo, int index)
		{
			if (combo.Items.Count > 0)
			{
				if (index > -1)
				{
					combo.SelectedIndex = index;
				}
				else
				{
					combo.SelectedIndex = 0;
				}
			}
		}

		private void DisableControls(Control cont)
		{
			try
			{
				if (cont.Controls.Count > 0)
				{
					foreach (Control ctrl in cont.Controls)
					{
						if (ctrl.Controls.Count > 0)
						{
							DisableControls(ctrl);
						}
						else
						{
							ctrl.Enabled = false;
						}
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		#endregion

		#region Notes

		private void buttonNotesAdd_Click(object sender, System.EventArgs e)
		{
			try
			{
				foreach (TabPage tp in this.tabControlCardNew.TabPages)
				{
					if (this.tabPageNotes != tp)
					{
						tp.Enabled = false;
					}
				}

				Op = Operations.AddNote;

				if (dataGridViewNotes.CurrentRow != null)
					this.dataGridViewNotes.ClearSelection();
				this.EnableButtons(false, false, true, false, false, true, LockButtons.Notes);
				this.ControlEnabled(true, LockButtons.Notes);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonNotesEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				Op = Operations.EditNotes;
				if (this.dataGridViewNotes.CurrentRow != null)
				{
					this.EnableButtons(false, false, true, false, false, true, LockButtons.Notes);
					this.ControlEnabled(true, LockButtons.Notes);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonNotesCancel_Click(object sender, System.EventArgs e)
		{
			try
			{
				foreach (TabPage tp in this.tabControlCardNew.TabPages)
				{
					tp.Enabled = true;
				}
				if (Op == Operations.AddNote)  // Трбва да се провери преди смяната на операцията
				{
					this.textBoxNoteText.Text = "";
					this.comboBoxNoteType.SelectedIndex = 0;
				}
				Op = Operations.ViewPersonData;
				this.ControlEnabled(false, LockButtons.Notes);
				this.EnableButtons(true, true, false, true, true, false, LockButtons.Notes);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonNotesSave_Click(object sender, System.EventArgs e)
		{
			NotesSave();
		}

		private bool NotesSave()
		{
			try
			{
				bool result;
				Dictionary<string, object> Dict = new Dictionary<string, object>();
				result = this.ValidateNotesData(Dict);
				if (result == true)
				{
					foreach (TabPage tp in this.tabControlCardNew.TabPages)
					{
						tp.Enabled = true;
					}

					if (Op == Operations.AddNote)
					{
						int id = this.dataAdapter.UniversalInsertParam(TableNames.NotesTable, Dict, "id", TransactionComnmand.NO_TRANSACTION);
						Dict.Add("ID", id.ToString());
						if (id > 0)
							this.AddDictToTable(Dict, this.dtNotes);
						else
						{
							MessageBox.Show("Грешка при добаявне на бележка", ErrorMessages.NoConnection);
						}
					}
					else
					{
						DataRow row = this.dtNotes.Rows.Find(this.dataGridViewNotes.CurrentRow.Cells["id"].Value);
						if (row != null)
						{
							if (this.dataAdapter.UniversalUpdateParam(TableNames.NotesTable, "id", Dict, this.dataGridViewNotes.CurrentRow.Cells["id"].Value.ToString(), TransactionComnmand.NO_TRANSACTION))
								this.UpdateDictToRow(Dict, row);
							else
							{
								MessageBox.Show("Грешка при редакция на бележка", ErrorMessages.NoConnection);
							}
						}
					}

					Op = Operations.ViewPersonData;
					this.ControlEnabled(false, LockButtons.Notes);
					this.EnableButtons(true, true, false, true, true, false, LockButtons.Notes);
					this.Refresh();
				}
				return result;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		private void buttonNotesDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.dataGridViewNotes.CurrentRow != null)
				{
					if (MessageBox.Show(this, "Сигурни ли сте че искате да изтриете бележката " + this.dataGridViewNotes.CurrentRow.Cells["text"].Value.ToString(), "Изтриване", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						this.dataAdapter.UniversalDelete(TableNames.NotesTable, this.dataGridViewNotes.CurrentRow.Cells["id"].Value.ToString(), "id");
						dtNotes.Rows.Remove(dtNotes.Rows.Find(this.dataGridViewNotes.CurrentRow.Cells["id"].Value));
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private bool ValidateNotesData(Dictionary<string, object> Dict)
		{
			try
			{
				Dict.Add("Par", this.parent.ToString());
				Dict.Add("ModifiedByUser", this.User);
				Dict.Add("Text", this.textBoxNoteText.Text);
				Dict.Add("Date", this.dateTimePickerNotes.Value);
				Dict.Add("Type", this.comboBoxNoteType.Text);
				Dict.Add("TypeDocument", this.textBoxNoteTypeDocument.Text);

				return true;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		private void RefreshNotesDataSource(bool IsFormLoad)
		{
			try
			{
				this.dtNotes = this.dataAdapter.SelectWhere(TableNames.NotesTable, "*", " WHERE par = '" + this.parent + "'");
				if (this.dtNotes == null)
				{
					MessageBox.Show("Грешка при зареждане на история", ErrorMessages.NoConnection);
					this.Close();
				}
				this.dtNotes.PrimaryKey = new DataColumn[] { this.dtNotes.Columns["ID"] };

				this.dtNotes.TableName = TableNames.NotesTable;
				TabPage tab = this.tabControlCardNew.SelectedTab;
				this.tabControlCardNew.SelectedTab = this.tabControlCardNew.TabPages["TabpageNotes"];
				if (this.tabControlCardNew.SelectedTab != null)
				{
					this.dataGridViewNotes.DataSource = this.dtNotes;
					this.dataGridViewNotes.ClearSelection();

					JustifyGridView(dataGridViewNotes, TableNames.Compare(TableNames.NotesTable));

					this.textBoxNoteText.Text = "";
					this.comboBoxNoteType.SelectedIndex = 0;
				}
				this.tabControlCardNew.SelectedTab = tab;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void comboBoxNotesFilter_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.comboBoxNotesFilter.SelectedIndex != 0)
				{
					if (this.dtNotes.Rows.Count > 0)
					{
						this.vueNotes = new DataView(dtNotes, "type = '" + this.comboBoxNotesFilter.Text + "'", "id", DataViewRowState.CurrentRows);
						this.dataGridViewNotes.DataSource = this.vueNotes;
					}
				}
				else
				{
					this.dataGridViewNotes.DataSource = this.dtNotes;
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void dataGridNotes_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (dataGridViewNotes.CurrentRow == null)
					return;
				DataRow row = this.dtNotes.Rows.Find(dataGridViewNotes.CurrentRow.Cells["id"].Value);

				this.textBoxNoteText.Text = row["text"].ToString();
				this.textBoxNoteTypeDocument.Text = row["typedocument"].ToString();
				this.dateTimePickerNotes.Value = (DateTime)row["date"];
				int index;
				index = this.comboBoxNoteType.FindString(row["type"].ToString());
				this.SetComboIndex(this.comboBoxNoteType, index);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonHistoryExcel_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.dataGridViewNotes.Rows.Count > 0)
				{
					ExcelExpo Ex = new ExcelExpo();
					DataView vue = new DataView(this.dtNotes, "", "", DataViewRowState.CurrentRows);
					Ex.ExportView(this.dataGridViewNotes, vue, "История на " + this.textBoxNames.Text);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		#endregion

		#region Nomenklature buttons

		private void buttonNomenkEducation_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.comboBoxEducation.SelectedIndex = -1;
				this.mainform.menuNomenklaturi_Education_Click(null, null);
				this.comboBoxEducation.DataSource = null;
				this.comboBoxEducation.DataSource = this.mainform.nomenclaatureData.dtEducation;
				this.comboBoxEducation.DisplayMember = "level";
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonNomenkScienceLevel_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.comboBoxScienceLevel.SelectedIndex = -1;
				this.mainform.menuNomenklaturi_ScienceDegree_Click(null, null);
				this.comboBoxScienceLevel.DataSource = null;
				this.comboBoxScienceLevel.DataSource = this.mainform.nomenclaatureData.arrScienceLevel;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonNomenkFamilyStatus_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.comboBoxFamilyStatus.SelectedIndex = -1;
				this.mainform.menuNomenklaturi_FamilyStatus_Click(null, null);
				this.comboBoxFamilyStatus.DataSource = null;
				this.comboBoxFamilyStatus.DataSource = this.mainform.nomenclaatureData.arrFamilyStatus;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonNomenkMilitaryRang_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.comboBoxMilitaryRang.SelectedIndex = -1;
				this.mainform.menuNomenklaturi_MilitaryRang_Click(sender, e);
				this.comboBoxMilitaryRang.DataSource = null;
				this.comboBoxMilitaryRang.DataSource = this.mainform.nomenclaatureData.dtMilitaryDegree;
				this.comboBoxMilitaryRang.DisplayMember = "level";
				this.comboBoxNSORang.DataSource = this.mainform.nomenclaatureData.dtMilitaryRang;
				this.comboBoxNSORang.DisplayMember = "level";
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonRangdegreeNomenklature_Click(object sender, EventArgs e)
		{
			try
			{
				this.comboBoxNSODegree.SelectedIndex = -1;

				JoinNomenklature2 form = new JoinNomenklature2(TableNames.JoinNomenklature, "Военен ранг", this.mainform.nomenclaatureData.dtMilitaryDegree, this.mainform, "militarydegree");
				form.ShowDialog();

				this.comboBoxNSODegree.DataSource = null;
				this.comboBoxNSODegree.DataSource = this.mainform.nomenclaatureData.dtMilitaryDegree;
				this.comboBoxNSODegree.DisplayMember = "level";
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}

		}

		private void buttonNomenkScienceTitle_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.comboBoxScience.SelectedIndex = -1;
				this.mainform.menuNomenklaturi_ScienceTitle_Click(null, null);
				this.comboBoxScience.DataSource = null;
				this.comboBoxScience.DataSource = this.mainform.nomenclaatureData.arrScienceTitle;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonNomenklatureLanguageLevel_Click(object sender, System.EventArgs e)
		{
			try
			{
				JoinNomenklature form = new JoinNomenklature(TableNames.JoinNomenklature, "Ниво на владеене", this.mainform.nomenclaatureData.arrLanguageKnowledge, this.mainform, "LanguageKnowledge");
				form.ShowDialog();
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonReasonFired_Click(object sender, System.EventArgs e)
		{
			try
			{
				JoinNomenklature form = new JoinNomenklature(TableNames.JoinNomenklature, "Основнания за освобождаване", this.mainform.nomenclaatureData.arrReasonFired, this.mainform, "reasonfired");
				this.comboBoxFiredReason.SelectedIndex = -1;

				form.ShowDialog();
				if (form.DialogResult == DialogResult.OK)
				{
					this.comboBoxFiredReason.DataSource = null;
					this.comboBoxFiredReason.DataSource = this.mainform.nomenclaatureData.arrReasonFired;
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonNomenklatureRang_Click(object sender, System.EventArgs e)
		{
			try
			{
				JoinNomenklature form = new JoinNomenklature(TableNames.JoinNomenklature, "Основнания за назначаване", this.mainform.nomenclaatureData.arrRang, this.mainform, "Rang");
				this.comboBoxRang.SelectedIndex = -1;
				form.ShowDialog();
				if (form.DialogResult == DialogResult.OK)
				{
					this.comboBoxRang.DataSource = null;
					this.comboBoxRang.DataSource = this.mainform.nomenclaatureData.arrRang;
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonReasonAssignment_Click(object sender, System.EventArgs e)
		{
			try
			{
				CommonNomenclature form = new CommonNomenclature(TableNames.ReasonAssignment, "Основнания за назначаване", this.mainform.nomenclaatureData.dtReasonAssignment, this.mainform);
				this.comboBoxAssignReason.SelectedIndex = -1;
				form.ShowDialog();
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonPenaltyReason_Click(object sender, System.EventArgs e)
		{
			try
			{
				JoinNomenklature form;
				if (this.radioButtonBonuses.Checked)
				{
					form = new JoinNomenklature(TableNames.JoinNomenklature, "Основнания за награждаване", this.mainform.nomenclaatureData.arrBonusReason, this.mainform, "BonusReason");
				}
				else
				{
					form = new JoinNomenklature(TableNames.JoinNomenklature, "Основнания за наказание", this.mainform.nomenclaatureData.arrPenaltyReason, this.mainform, "PenaltyReason");
				}
				this.comboBoxPenaltyReason.SelectedIndex = -1;
				form.ShowDialog();
				if (form.DialogResult == DialogResult.OK)
				{
					this.comboBoxPenaltyReason.DataSource = null;
					if (this.radioButtonBonuses.Checked)
						this.comboBoxPenaltyReason.DataSource = this.mainform.nomenclaatureData.arrBonusReason;
					else
						this.comboBoxPenaltyReason.DataSource = this.mainform.nomenclaatureData.arrPenaltyReason;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonTypePenalty_Click(object sender, System.EventArgs e)
		{
			try
			{
				JoinNomenklature form;
				if (this.radioButtonBonuses.Checked)
					form = new JoinNomenklature(TableNames.JoinNomenklature, "Видове награди", this.mainform.nomenclaatureData.arrTypeBonus, this.mainform, "TypeBonus");
				else
					form = new JoinNomenklature(TableNames.JoinNomenklature, "Видове наказания", this.mainform.nomenclaatureData.arrTypePenalty, this.mainform, "TypePenalty");
				this.comboBoxTypePenalty.SelectedIndex = -1;
				form.ShowDialog();
				if (form.DialogResult == DialogResult.OK)
				{
					this.comboBoxTypePenalty.DataSource = null;
					if (this.radioButtonBonuses.Checked)
						this.comboBoxTypePenalty.DataSource = this.mainform.nomenclaatureData.arrTypeBonus;
					else
						this.comboBoxTypePenalty.DataSource = this.mainform.nomenclaatureData.arrTypePenalty;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonNomenklatureSpecialSkills_Click(object sender, EventArgs e)
		{
			try
			{
				JoinNomenklature form = new JoinNomenklature(TableNames.JoinNomenklature, "Специални умения", this.mainform.nomenclaatureData.arrSpecialSkills, this.mainform, "SpecialSkills");
				this.comboBoxSpecialSkills.SelectedIndex = -1;
				form.ShowDialog();
				if (form.DialogResult == DialogResult.OK)
				{
					this.comboBoxSpecialSkills.DataSource = null;
					this.comboBoxSpecialSkills.DataSource = this.mainform.nomenclaatureData.arrSpecialSkills;
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonForeignLanguages_Click(object sender, EventArgs e)
		{
			try
			{
				JoinNomenklature form = new JoinNomenklature(TableNames.JoinNomenklature, "Чужди езици", this.mainform.nomenclaatureData.arrLanguages, this.mainform, "language");
				form.ShowDialog();
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonAttached_Click(object sender, EventArgs e)
		{
			try
			{
				FormAttached form = new FormAttached(TableNames.AttachedDocuments, "", this.parent.ToString(), this.mainform.connString);
				form.ShowDialog();
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}
		#endregion

		#region Helper functions
		private void button1_Click(object sender, EventArgs e)
		{
			DataTable dtPA, dtP, dtMR;

			dtPA = this.dataAdapter.SelectWhere(TableNames.PersonAssignment, "*", "where israngupdate = 1");
			dtPA.Columns["parent"].ColumnName = "par";
			dtP = this.dataAdapter.SelectWhere(TableNames.Person, "*", "");
			dtMR = this.dataAdapter.SelectWhere(TableNames.MilitaryRang, "*", "");
			dtMR.Columns["parent"].ColumnName = "par";

			try
			{
				DataView vuePA, vueMR;
				foreach (DataRow pRow in dtP.Rows)
				{
					vuePA = new DataView(dtPA, "par = " + pRow["id"].ToString(), "id", DataViewRowState.CurrentRows);
					for (int i = 0; i < vuePA.Count; i++)
					{
						DataRow milRow = dtMR.NewRow();
						Dictionary<string, object> Dict = new Dictionary<string, object>();

						Dict.Add("militaryrang", vuePA[i]["militaryrang"].ToString());
						Dict.Add("rangordernumber", vuePA[i]["rangordernumber"].ToString());
						Dict.Add("rangorderdate", (DateTime)vuePA[i]["rangorderdate"]);
						Dict.Add("rangordervalidfrom", (DateTime)vuePA[i]["rangordervalidfrom"]);
						Dict.Add("idassignment", vuePA[i]["id"].ToString());
						Dict.Add("parent", vuePA[i]["par"].ToString());

						int id = this.dataAdapter.UniversalInsertParam(TableNames.MilitaryRang, Dict, "id", TransactionComnmand.NO_TRANSACTION);
						Dict.Remove("parent");
						Dict.Add("par", vuePA[i]["par"].ToString());
						Dict.Add("ID", id.ToString());

						this.AddDictToTable(Dict, dtMR);
					}

					if (vuePA.Count > 0)
					{
						vueMR = new DataView(dtMR, "par = " + pRow["id"].ToString(), "rangordervalidfrom", DataViewRowState.CurrentRows);
						Dictionary<string, object> activeDict = new Dictionary<string, object>();
						activeDict.Add("isactive", "1");
						this.dataAdapter.UniversalUpdateParam(TableNames.MilitaryRang, "id", activeDict, vueMR[vueMR.Count - 1]["id"].ToString(), TransactionComnmand.NO_TRANSACTION);
						vueMR[vueMR.Count - 1]["isactive"] = "1";

						Dictionary<string, object> personDict = new Dictionary<string, object>();
						personDict.Add("militaryrang", vueMR[vueMR.Count - 1]["militaryrang"].ToString());
						this.dataAdapter.UniversalUpdateParam(TableNames.Person, "id", personDict, pRow["id"].ToString(), TransactionComnmand.NO_TRANSACTION);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			return;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			DataTable dtPA, dtP;

			dtPA = this.dataAdapter.SelectWhere(TableNames.PersonAssignment, "*", "");
			dtPA.Columns["parent"].ColumnName = "par";
			dtP = this.dataAdapter.SelectWhere(TableNames.Person, "*", "");

			try
			{
				DataView vuePA;
				foreach (DataRow pRow in dtP.Rows)
				{
					vuePA = new DataView(dtPA, "par = " + pRow["id"].ToString(), "assignedat", DataViewRowState.CurrentRows);
					Dictionary<string, object> activeDict = new Dictionary<string, object>();
					activeDict.Add("isactive", "0");
					for (int i = 0; i < vuePA.Count; i++)
					{
						this.dataAdapter.UniversalUpdateParam(TableNames.PersonAssignment, "id", activeDict, vuePA[i]["id"].ToString(), TransactionComnmand.NO_TRANSACTION);
					}//deactivete all
					activeDict["isactive"] = "1";

					if (vuePA.Count > 0)
					{
						this.dataAdapter.UniversalUpdateParam(TableNames.PersonAssignment, "id", activeDict, vuePA[vuePA.Count - 1]["id"].ToString(), TransactionComnmand.NO_TRANSACTION);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			return;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			DataTable dtP, dtMR;

			dtP = this.dataAdapter.SelectWhere(TableNames.Person, "*", "where fired = 0");
			dtMR = this.dataAdapter.SelectWhere(TableNames.MilitaryRang, "*", "");
			dtMR.Columns["parent"].ColumnName = "par";

			try
			{
				foreach (DataRow Row in dtP.Rows)
				{
					Dictionary<string, object> actDict = new Dictionary<string, object>();
					actDict.Add("isactive", "0");
					DataView vueMR = new DataView(dtMR, "par = " + Row["id"].ToString(), "rangorderdate", DataViewRowState.CurrentRows);
					for (int i = 0; i < vueMR.Count - 1; i++)
					{
						this.dataAdapter.UniversalUpdateParam(TableNames.MilitaryRang, "id", actDict, vueMR[i]["id"].ToString(), TransactionComnmand.NO_TRANSACTION);
					}
				}
			}
			//try
			//{
			//    DataView vuePA, vueFP3;
			//    foreach (DataRow pRow in dtP.Rows)
			//    {
			//        string fp3id = "";
			//        vuePA = new DataView(dtPA, "par = " + pRow["id"].ToString() + " and isactive = 1", "id", DataViewRowState.CurrentRows);
			//        if (vuePA.Count == 0)
			//        {
			//            continue;
			//        }
			//        else if (vuePA.Count > 1)
			//        {
			//            continue;
			//        }
			//        else
			//        {
			//            fp3id = vuePA[0]["positionid"].ToString();
			//        }
			//        if (fp3id == "")
			//        {
			//            continue;
			//        }

			//        vueFP3 = new DataView(dtFP3, "id = " + fp3id, "id", DataViewRowState.CurrentRows);
			//        if (vueFP3.Count == 0 || vueFP3.Count > 1)
			//            continue;

			//        Dictionary<string, string> nodeDict = new Dictionary<string, string>();
			//        nodeDict.Add("nodeid", vueFP3[0]["par"].ToString()) ;
			//    }
			//}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			return;
		}

		private void button6_Click(object sender, EventArgs e)
		{
			DataTable dtPA = new DataTable();
			dtPA = this.dataAdapter.SelectWhere(TableNames.PersonAssignment, "*", "WHERE contract = 'Безсрочен със срок на изпитване' AND isactive = 1");
			foreach (DataRow row in dtPA.Rows)
			{
				DateTime NewTestDate = new DateTime();
				DataTable dtHol = new DataTable();
				int countdays = 0;

				dtHol = this.dataAdapter.SelectWhere(TableNames.Absence, "*", "WHERE parent = " + row["parent"].ToString());
				if (DateTime.TryParse(row["assignedat"].ToString(), out NewTestDate))
				{
					NewTestDate = NewTestDate.AddMonths(6);

				}
				else
				{
					continue;
				}

				foreach (DataRow rh in dtHol.Rows)
				{
					DateTime startdate = new DateTime();
					if (DateTime.TryParse(rh["fromdate"].ToString(), out startdate))
					{
						if (startdate < NewTestDate)
						{
							int cnt;
							if (int.TryParse(rh["countdays"].ToString(), out cnt))
							{
								countdays += cnt;
							}
						}
					}
					else
					{
						continue;
					}

				}

				NewTestDate = NewTestDate.AddDays(countdays);

				Dictionary<string, object> Dict = new Dictionary<string, object>();
				Dict.Add("testcontractdate", NewTestDate);

				this.dataAdapter.UniversalUpdateParam(TableNames.PersonAssignment, "id", Dict, row["id"].ToString(), TransactionComnmand.NO_TRANSACTION);

			}
			//WPFHoliday.Window1 form = new WPFHoliday.Window1();
			//ElementHost.EnableModelessKeyboardInterop( form);
			//form.ShowDialog();
		}

		private void button7_Click(object sender, EventArgs e)
		{
			DataTable dtUpdate;
			dtUpdate = this.dataAdapter.SelectWhere(TableNames.PersonAssignment, "*", "WHERE assignedat > '2011-08-17' and isactive = 1");
			foreach (DataRow row in dtUpdate.Rows)
			{
				DataTable dtPer;
				dtPer = this.dataAdapter.SelectWhere(TableNames.Person, "*", "WHERE id = " + row["parent"].ToString());
				if (dtPer.Rows.Count == 1)
				{
					Dictionary<string, object> mesDict = new Dictionary<string, object>();
					string message = "";
					message += string.Format("Служителят {0} е назначен в {1} {2} {3} {4}", dtPer.Rows[0]["name"].ToString(), row["Level1"].ToString(), row["Level2"].ToString(), row["Level3"].ToString(), row["Level4"].ToString());
					message = message.Trim();
					message += string.Format(" на длъжност {0}", row["Position"].ToString());
					mesDict.Add("Message", message);
					mesDict.Add("Date", (DateTime)row["assignedat"]);
					mesDict.Add("id_user", dtPer.Rows[0]["id_sysco"].ToString());
					mesDict.Add("isreaded", "False");
					this.dataAdapter.UniversalInsertParam("TM_Messages", mesDict, "id_message", TransactionComnmand.NO_TRANSACTION);
				}
				else if (dtPer.Rows.Count > 1)
				{
					break;
				}
			}
		}

		private void SuspiciousHolidays(object sender, EventArgs e)
		{
			List<string> lstnames = new List<string>();
			try
			{
				DataTable dtYH = this.dataAdapter.SelectWhere(TableNames.YearHoliday, "*", "WHERE year = 2013");
				DataTable person = this.dataAdapter.SelectWhere(TableNames.Person, "id, name, hiredat", "WHERE fired = 0");
				DataTable dtAssignment = this.dataAdapter.SelectWhere(TableNames.PersonAssignment, "id, parent as par, isadditionalassignment, assignedat ", "WHERE isadditionalassignment = 0");

				foreach (DataRow ryh in dtYH.Rows)
				{
					DataTable dtAbsence1 = this.dataAdapter.SelectWhere(TableNames.Absence, "*", "WHERE typeAbsence = 'Полагаем годишен отпуск' and year = 2013 and parent = " + ryh["parent"].ToString());
					DataTable dtperson1 = this.dataAdapter.SelectWhere(TableNames.Person, "*", "WHERE fired = 0 and id = " + ryh["parent"].ToString());
					DataView vuea = new DataView(dtAssignment, "par = " + ryh["parent"], "id", DataViewRowState.CurrentRows);

					int newtotal = 0, total = 0, leftover = 0;
					foreach (DataRow Row in dtAbsence1.Rows)
					{
						newtotal += (int)Row["countdays"];
					}

					total = int.Parse(ryh["total"].ToString());
					leftover = int.Parse(ryh["leftover"].ToString());


					if (vuea.Count > 0 && dtperson1.Rows.Count > 0)
					{
						DateTime dthir;
						try
						{
							dthir = DateTime.Parse(vuea[0]["assignedat"].ToString());
						}
						catch
						{
							continue;
						}
						if (dthir.Year == 2012)
						{
							float a_day = 0, a_month = 0, day_rest = 0, month_rest = 0, left = 0;
							day_rest = 30 - dthir.Day;
							month_rest = 12 - dthir.Month;
							if (total > 0)
							{
								a_day = (float)total / 360;
								a_month = (float)total / 12;

								if (this.dateTimePickerAssignedAt.Value.Year == DateTime.Now.Year) // Ако служителя е назанчен текущата година се добавя само частичен отпуск
								{
									//Пропорцианалоно отпуск = (Остатък месеци) * (отпуск за месец) + (остатък дни) * (отпуск за ден)
									//if (mainForm.DataBaseTypes == DBTypes.MsSql)
									//	if (day_rest < 0.5)
									//	{
									//		day_rest = 0;
									//	}
									double leftt = month_rest * a_month + day_rest * a_day;

									// Закръгляне
									leftt = Math.Round(leftt);
									left = (int)leftt;
								}
								else
								{//Add the whole number of days
									left = total;
								}
							}
							if ((newtotal != (left - leftover)))// || (total == 0))
							{
								lstnames.Add(ryh["parent"].ToString() + " " + dtperson1.Rows[0]["name"]);
								if (total == 0)
								{
									//MessageBox.Show(ryh["parent"].ToString() + " " + dtperson1.Rows[0]["name"]);
								}
							}
						}
						else
						{
							if ((newtotal != (total - leftover)))// || (total == 0))
							{
								lstnames.Add(ryh["parent"].ToString() + " " + dtperson1.Rows[0]["name"]);
								if (total == 0)
								{
									//MessageBox.Show(ryh["parent"].ToString() + " " + dtperson1.Rows[0]["name"]);
								}

							}
						}
					}
					else
					{
						continue;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			return;
		}

		private void CheckPersonHolidays()
		{
			string connstring;
			mainForm.GetConnString(out connstring);
			Entities context = new Entities(connstring);

			List<string> lstnames = new List<string>();

			var Year = DateTime.Now.Year.ToString();

			var dtYH = (from p in context.HR_Person
						from y in context.HR_Year_Holiday
						where p.fired == 0
								&& p.id == y.parent
								&& y.year == DateTime.Now.Year
								&& p.id == this.parent
						select y).ToList();

			//var person = this.context.HR_Person.Where(p => p.fired == 1).ToList();
			var persons = (from p in context.HR_Person
						   from a in context.HR_PersonAssignment
						   where p.fired == 0
								   && p.id == this.parent
						   select new
						   {
							   ID = p.id,
							   Name = p.name,
							   HiredAt = p.hiredAt,
							   NH = a.AdditionalHoliday,
							   AH = a.NumHoliday
						   }).ToList();

			//var dtAssignment = this.context.HR_personassignment.Where(a => a.isActive == 1).ToList();

			foreach (var yh in dtYH)
			{
				var absencesForYesar = context.HR_Absence.Where(a => a.Year == Year && a.parent == yh.parent && a.typeAbsence == "Полагаем годишен отпуск").ToList();

				var used = absencesForYesar.Sum(a => a.countDays);

				var pers = persons.Find(p => p.ID == yh.parent);

				var pTotal = int.Parse(pers.AH) + pers.NH;

				int cl = (int)yh.total - (int)used;

				if (pers.HiredAt.Value.Year.ToString() == Year)
				{
					DateTime dthir = pers.HiredAt.Value;
					float a_day = 0, a_month = 0, day_rest = 0, month_rest = 0, left = 0;
					day_rest = 30 - dthir.Day;
					month_rest = 12 - dthir.Month;

					if (pTotal > 0)
					{
						a_day = (float)pTotal / 360;
						a_month = (float)pTotal / 12;


						//Пропорцианалоно отпуск = (Остатък месеци) * (отпуск за месец) + (остатък дни) * (отпуск за ден)
						double leftt = month_rest * a_month + day_rest * a_day;
						leftt = Math.Round(leftt);
						left = (int)leftt;

						cl = (int)left - (int)used;

						if (yh.leftover != cl)
						{
							MessageBox.Show("В досието на лицето има разминаване на дните полагаем отпуск или разминаване между размера полагаем отпуск в графа отпуски и зададения полагаем отуск по договор. Моля, проверете дали всичко е наред.");
						}
					}
					else
					{
						MessageBox.Show("В досието на лицето има разминаване на дните полагаем отпуск или разминаване между размера полагаем отпуск в графа отпуски и зададения полагаем отуск по договор. Моля, проверете дали всичко е наред.");
					}
				}
				else
				{
					if (yh.total != pTotal || pTotal == 0)
					{
						MessageBox.Show("В досието на лицето има разминаване на дните полагаем отпуск или разминаване между размера полагаем отпуск в графа отпуски и зададения полагаем отуск по договор. Моля, проверете дали всичко е наред.");

					}
				}
			}
		}

		private void FitPersonsToTheirPositions(object sender, EventArgs e)
		{
			DataTable dtTree = dataAdapter.SelectWhere(TableNames.NewTree2, "*", "");
			string Join = string.Format(" LEFT JOIN {0} on {1}.id = {0}.parent WHERE isactive = 1 and fired = 0", TableNames.PersonAssignment, TableNames.Person);
			DataTable dtPerson = dataAdapter.SelectWhere(TableNames.Person, string.Format("name, nodeid, {0}.id, {1}.id as idass, {0}.fired, {1}.isactive, {1}.positionid", TableNames.Person, TableNames.PersonAssignment), Join);
			DataTable dtFirmPersonal3 = dataAdapter.SelectWhere(TableNames.FirmPersonal3, "*", "");

			foreach (DataRow r in dtPerson.Rows)
			{
				Dictionary<string, object> pDict = new Dictionary<string, object>();
				Dictionary<string, object> aDict = new Dictionary<string, object>();

				List<string> levels = new List<string>();

				DataView vuePositions = new DataView(dtFirmPersonal3, "id = " + r["positionid"].ToString(), "id", DataViewRowState.CurrentRows);

				if (vuePositions.Count > 0)
				{
					DataView vueTree = new DataView(dtTree, "id = " + vuePositions[0]["par"].ToString(), "id", DataViewRowState.CurrentRows);
					if (vueTree.Count > 0)
					{
						levels.Add(vueTree[0]["level"].ToString());
						vueTree = new DataView(dtTree, "id = " + vueTree[0]["par"].ToString(), "id", DataViewRowState.CurrentRows);
						if (vueTree.Count > 0)
						{
							levels.Add(vueTree[0]["level"].ToString());
							vueTree = new DataView(dtTree, "id = " + vueTree[0]["par"].ToString(), "id", DataViewRowState.CurrentRows);
							if (vueTree.Count > 0)
							{
								levels.Add(vueTree[0]["level"].ToString());
								vueTree = new DataView(dtTree, "id = " + vueTree[0]["par"].ToString(), "id", DataViewRowState.CurrentRows);
								if (vueTree.Count > 0)
								{
									levels.Add(vueTree[0]["level"].ToString());
									vueTree = new DataView(dtTree, "id = " + vueTree[0]["par"].ToString(), "id", DataViewRowState.CurrentRows);
								}
							}
						}
					}
				}

				switch (levels.Count)
				{
					case 1:
						pDict.Add("nodeid", vuePositions[0]["par"].ToString());
						aDict.Add("level1", levels[0]);
						dataAdapter.UniversalUpdateParam(TableNames.Person, "id", pDict, r["id"].ToString(), TransactionComnmand.NO_TRANSACTION);
						dataAdapter.UniversalUpdateParam(TableNames.PersonAssignment, "id", aDict, r["idass"].ToString(), TransactionComnmand.NO_TRANSACTION);
						break;
					case 2:
						pDict.Add("nodeid", vuePositions[0]["par"].ToString());
						aDict.Add("level2", levels[0]);
						aDict.Add("level1", levels[1]);
						dataAdapter.UniversalUpdateParam(TableNames.Person, "id", pDict, r["id"].ToString(), TransactionComnmand.NO_TRANSACTION);
						dataAdapter.UniversalUpdateParam(TableNames.PersonAssignment, "id", aDict, r["idass"].ToString(), TransactionComnmand.NO_TRANSACTION);
						break;
					case 3:
						pDict.Add("nodeid", vuePositions[0]["par"].ToString());
						aDict.Add("level3", levels[0]);
						aDict.Add("level2", levels[1]);
						aDict.Add("level1", levels[2]);
						dataAdapter.UniversalUpdateParam(TableNames.Person, "id", pDict, r["id"].ToString(), TransactionComnmand.NO_TRANSACTION);
						dataAdapter.UniversalUpdateParam(TableNames.PersonAssignment, "id", aDict, r["idass"].ToString(), TransactionComnmand.NO_TRANSACTION);
						break;
					case 4:
						pDict.Add("nodeid", vuePositions[0]["par"].ToString());
						aDict.Add("level4", levels[0]);
						aDict.Add("level3", levels[1]);
						aDict.Add("level2", levels[2]);
						aDict.Add("level1", levels[3]);
						dataAdapter.UniversalUpdateParam(TableNames.Person, "id", pDict, r["id"].ToString(), TransactionComnmand.NO_TRANSACTION);
						dataAdapter.UniversalUpdateParam(TableNames.PersonAssignment, "id", aDict, r["idass"].ToString(), TransactionComnmand.NO_TRANSACTION);
						break;

				}
			}
		}

		private void FixAssignmentDates(object sender, EventArgs e)
		{

			HolidayPlan.CalendarRow.FixAssignmentDates(mainform.EntityConectionString);
		}
		#endregion

		private void dateTimePickerPCCardPublished_ValueChanged(object sender, EventArgs e)
		{
			this.dateTimePickerPCardExpiry.Value = this.dateTimePickerPCCardPublished.Value.AddYears(10);
			this.PersonalDataChangedValue = true;
		}
	}
}
