using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace LichenSystaw2004
{
	/// <summary>
	/// Summary description for StatisticPersonal.
	/// </summary>
	public class StatisticAssignment : System.Windows.Forms.Form
	{
		internal ArrayList arrColumnAdd;
		private ArrayList arrDepartment = new ArrayList(), arrSector = new ArrayList(), arrDirectionNum = new ArrayList(), arrDirection, arrAdministration = new ArrayList();
		private DataView vueDirection, vueDepartment, vueSector, vuePosition, vueAdministration;
		private DataTable dtTree;
		private DataTable dtPosition;
		private DataViewRowState dvrs;
		private int nodeID, administration;		
		DataLayer.PersonalAction personAction;

		mainForm main;
		private bool IsFiredd = false;
		private bool IsTotalStat;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public DataTable dt1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button buttonFind;

		private CheckedComboBox.CheckedCombo checkedComboProfession;
		private CheckedComboBox.CheckedCombo checkedComboPart;
		private CheckedComboBox.CheckedCombo checkedComboControl;
		private CheckedComboBox.CheckedCombo checkedComboDirection;
		private CheckedComboBox.CheckedCombo checkedComboContract;
		private CheckedComboBox.CheckedCombo checkedComboWorkTime;
		private CheckedComboBox.CheckedCombo checkedComboReasonAssignment;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DateTimePicker dateTimePickerAssignedAt2;
		private System.Windows.Forms.DateTimePicker dateTimePickerAssignedAt1;
		private System.Windows.Forms.DateTimePicker dateTimePickerContractExpiry1;
		private System.Windows.Forms.DateTimePicker dateTimePickerContractExpiry2;
		private System.Windows.Forms.CheckBox checkBoxContractExpiry;
		private System.Windows.Forms.CheckBox checkBoxAssignedAt;
		private CheckedComboBox.CheckedCombo checkedComboAdministration;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label labelYounger;
		private BugBox.NumBox numBoxPaymentFrom;
		private BugBox.NumBox numBoxPaymentTo;
		private System.Windows.Forms.CheckBox checkBoxPayment;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox checkBoxExp;
		private BugBox.NumBox numBoxExpFrom;
		private BugBox.NumBox numBoxExpTo;
		private System.Windows.Forms.CheckBox checkBoxExpYear;
		private CheckedComboBox.CheckedCombo checkedContractType;
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.CheckBox checkBoxTestContractExpiraty;
		private System.Windows.Forms.DateTimePicker dateTimePickerTestContractExpiry2;
		private System.Windows.Forms.DateTimePicker dateTimePickerTestContractExpiry1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private string GetPersonsFrom( string posId, ArrayList arr )
		{
			foreach(object o in arr )
			{
				posId += " person.PositionID = " + o.ToString() + " OR ";
			}
			return posId;
		}
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public StatisticAssignment( mainForm main, bool IsTotalStat, bool IsFiredd)
		{
			this.IsTotalStat = IsTotalStat;
			this.IsFiredd = IsFiredd;
            this.main = main;
			InitializeComponent();
			this.dateTimePickerContractExpiry1.Enabled = this.checkBoxContractExpiry.Checked;
			this.dateTimePickerContractExpiry2.Enabled = this.checkBoxContractExpiry.Checked;
			this.dateTimePickerAssignedAt1.Enabled = this.checkBoxAssignedAt.Checked;
			this.dateTimePickerAssignedAt2.Enabled = this.checkBoxAssignedAt.Checked;

			this.checkedComboAdministration.combobox.SelectedIndexChanged += new EventHandler(combobox4_SelectedIndexChanged);
			this.checkedComboDirection.combobox.SelectedIndexChanged += new EventHandler(combobox1_SelectedIndexChanged);
			this.checkedComboControl.combobox.SelectedIndexChanged += new EventHandler(combobox2_SelectedIndexChanged);
			this.checkedComboPart.combobox.SelectedIndexChanged += new EventHandler(combobox3_SelectedIndexChanged);
			this.checkedComboProfession.combobox.SelectedIndexChanged += new EventHandler(comboboxProfession_SelectedIndexChanged);
			if( this.IsTotalStat )
			{
				this.buttonFind.Text = "Избери";
			}
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(StatisticAssignment));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkedComboAdministration = new CheckedComboBox.CheckedCombo();
			this.checkedContractType = new CheckedComboBox.CheckedCombo();
			this.checkedComboReasonAssignment = new CheckedComboBox.CheckedCombo();
			this.checkedComboWorkTime = new CheckedComboBox.CheckedCombo();
			this.checkedComboContract = new CheckedComboBox.CheckedCombo();
			this.checkedComboProfession = new CheckedComboBox.CheckedCombo();
			this.checkedComboPart = new CheckedComboBox.CheckedCombo();
			this.checkedComboControl = new CheckedComboBox.CheckedCombo();
			this.checkedComboDirection = new CheckedComboBox.CheckedCombo();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkBoxTestContractExpiraty = new System.Windows.Forms.CheckBox();
			this.dateTimePickerTestContractExpiry2 = new System.Windows.Forms.DateTimePicker();
			this.dateTimePickerTestContractExpiry1 = new System.Windows.Forms.DateTimePicker();
			this.checkBoxAssignedAt = new System.Windows.Forms.CheckBox();
			this.checkBoxContractExpiry = new System.Windows.Forms.CheckBox();
			this.dateTimePickerContractExpiry2 = new System.Windows.Forms.DateTimePicker();
			this.dateTimePickerContractExpiry1 = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.dateTimePickerAssignedAt2 = new System.Windows.Forms.DateTimePicker();
			this.dateTimePickerAssignedAt1 = new System.Windows.Forms.DateTimePicker();
			this.buttonFind = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.labelYounger = new System.Windows.Forms.Label();
			this.checkBoxPayment = new System.Windows.Forms.CheckBox();
			this.numBoxPaymentFrom = new BugBox.NumBox();
			this.numBoxPaymentTo = new BugBox.NumBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.checkBoxExp = new System.Windows.Forms.CheckBox();
			this.numBoxExpFrom = new BugBox.NumBox();
			this.numBoxExpTo = new BugBox.NumBox();
			this.checkBoxExpYear = new System.Windows.Forms.CheckBox();
			this.buttonExit = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkedComboAdministration);
			this.groupBox1.Controls.Add(this.checkedContractType);
			this.groupBox1.Controls.Add(this.checkedComboReasonAssignment);
			this.groupBox1.Controls.Add(this.checkedComboWorkTime);
			this.groupBox1.Controls.Add(this.checkedComboContract);
			this.groupBox1.Controls.Add(this.checkedComboProfession);
			this.groupBox1.Controls.Add(this.checkedComboPart);
			this.groupBox1.Controls.Add(this.checkedComboControl);
			this.groupBox1.Controls.Add(this.checkedComboDirection);
			this.groupBox1.Location = new System.Drawing.Point(8, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(456, 256);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Избор на условия за служителите";
			// 
			// checkedComboAdministration
			// 
			this.checkedComboAdministration.Checked = false;
			this.checkedComboAdministration.Column = "";
			this.checkedComboAdministration.Data = null;
			this.checkedComboAdministration.Location = new System.Drawing.Point(8, 32);
			this.checkedComboAdministration.Name = "checkedComboAdministration";
			this.checkedComboAdministration.Size = new System.Drawing.Size(432, 24);
			this.checkedComboAdministration.TabIndex = 0;
			this.checkedComboAdministration.TextCombo = "Назначен в администрация";
			// 
			// checkedContractType
			// 
			this.checkedContractType.Checked = false;
			this.checkedContractType.Column = "personassignment.law";
			this.checkedContractType.Data = null;
			this.checkedContractType.Location = new System.Drawing.Point(8, 224);
			this.checkedContractType.Name = "checkedContractType";
			this.checkedContractType.Size = new System.Drawing.Size(432, 24);
			this.checkedContractType.TabIndex = 8;
			this.checkedContractType.TextCombo = "Трудово взаимоотнешение";
			// 
			// checkedComboReasonAssignment
			// 
			this.checkedComboReasonAssignment.Checked = false;
			this.checkedComboReasonAssignment.Column = "personassignment.assignreason";
			this.checkedComboReasonAssignment.Data = null;
			this.checkedComboReasonAssignment.Location = new System.Drawing.Point(8, 200);
			this.checkedComboReasonAssignment.Name = "checkedComboReasonAssignment";
			this.checkedComboReasonAssignment.Size = new System.Drawing.Size(432, 24);
			this.checkedComboReasonAssignment.TabIndex = 7;
			this.checkedComboReasonAssignment.TextCombo = "Основание за назначение";
			// 
			// checkedComboWorkTime
			// 
			this.checkedComboWorkTime.Checked = false;
			this.checkedComboWorkTime.Column = "personassignment.worktime";
			this.checkedComboWorkTime.Data = null;
			this.checkedComboWorkTime.Location = new System.Drawing.Point(8, 176);
			this.checkedComboWorkTime.Name = "checkedComboWorkTime";
			this.checkedComboWorkTime.Size = new System.Drawing.Size(432, 24);
			this.checkedComboWorkTime.TabIndex = 6;
			this.checkedComboWorkTime.TextCombo = "Работно време";
			// 
			// checkedComboContract
			// 
			this.checkedComboContract.Checked = false;
			this.checkedComboContract.Column = "personassignment.contract";
			this.checkedComboContract.Data = null;
			this.checkedComboContract.Location = new System.Drawing.Point(8, 152);
			this.checkedComboContract.Name = "checkedComboContract";
			this.checkedComboContract.Size = new System.Drawing.Size(432, 24);
			this.checkedComboContract.TabIndex = 5;
			this.checkedComboContract.TextCombo = "Назанчен на договор";
			// 
			// checkedComboProfession
			// 
			this.checkedComboProfession.Checked = false;
			this.checkedComboProfession.Column = "personassignment.position";
			this.checkedComboProfession.Data = null;
			this.checkedComboProfession.Location = new System.Drawing.Point(8, 128);
			this.checkedComboProfession.Name = "checkedComboProfession";
			this.checkedComboProfession.Size = new System.Drawing.Size(432, 24);
			this.checkedComboProfession.TabIndex = 4;
			this.checkedComboProfession.TextCombo = "Назначен на длъжност";
			// 
			// checkedComboPart
			// 
			this.checkedComboPart.Checked = false;
			this.checkedComboPart.Column = "";
			this.checkedComboPart.Data = null;
			this.checkedComboPart.Location = new System.Drawing.Point(8, 104);
			this.checkedComboPart.Name = "checkedComboPart";
			this.checkedComboPart.Size = new System.Drawing.Size(432, 24);
			this.checkedComboPart.TabIndex = 3;
			this.checkedComboPart.TextCombo = "Назначен в сектор";
			// 
			// checkedComboControl
			// 
			this.checkedComboControl.Checked = false;
			this.checkedComboControl.Column = "";
			this.checkedComboControl.Data = null;
			this.checkedComboControl.Location = new System.Drawing.Point(8, 80);
			this.checkedComboControl.Name = "checkedComboControl";
			this.checkedComboControl.Size = new System.Drawing.Size(432, 24);
			this.checkedComboControl.TabIndex = 2;
			this.checkedComboControl.TextCombo = "Назначен в отдел";
			// 
			// checkedComboDirection
			// 
			this.checkedComboDirection.Checked = false;
			this.checkedComboDirection.Column = "";
			this.checkedComboDirection.Data = null;
			this.checkedComboDirection.Location = new System.Drawing.Point(8, 56);
			this.checkedComboDirection.Name = "checkedComboDirection";
			this.checkedComboDirection.Size = new System.Drawing.Size(432, 24);
			this.checkedComboDirection.TabIndex = 1;
			this.checkedComboDirection.TextCombo = "Назначен в дирекция";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkBoxTestContractExpiraty);
			this.groupBox2.Controls.Add(this.dateTimePickerTestContractExpiry2);
			this.groupBox2.Controls.Add(this.dateTimePickerTestContractExpiry1);
			this.groupBox2.Controls.Add(this.checkBoxAssignedAt);
			this.groupBox2.Controls.Add(this.checkBoxContractExpiry);
			this.groupBox2.Controls.Add(this.dateTimePickerContractExpiry2);
			this.groupBox2.Controls.Add(this.dateTimePickerContractExpiry1);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.dateTimePickerAssignedAt2);
			this.groupBox2.Controls.Add(this.dateTimePickerAssignedAt1);
			this.groupBox2.Location = new System.Drawing.Point(8, 256);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(408, 192);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Хронологичност";
			// 
			// checkBoxTestContractExpiraty
			// 
			this.checkBoxTestContractExpiraty.Location = new System.Drawing.Point(16, 128);
			this.checkBoxTestContractExpiraty.Name = "checkBoxTestContractExpiraty";
			this.checkBoxTestContractExpiraty.Size = new System.Drawing.Size(336, 24);
			this.checkBoxTestContractExpiraty.TabIndex = 6;
			this.checkBoxTestContractExpiraty.Text = "Изпитателния срок на служителя изтича в този интервал";
			this.checkBoxTestContractExpiraty.CheckedChanged += new System.EventHandler(this.checkBoxTestContractExpiraty_CheckedChanged_1);
			// 
			// dateTimePickerTestContractExpiry2
			// 
			this.dateTimePickerTestContractExpiry2.Enabled = false;
			this.dateTimePickerTestContractExpiry2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerTestContractExpiry2.Location = new System.Drawing.Point(208, 152);
			this.dateTimePickerTestContractExpiry2.Name = "dateTimePickerTestContractExpiry2";
			this.dateTimePickerTestContractExpiry2.Size = new System.Drawing.Size(184, 20);
			this.dateTimePickerTestContractExpiry2.TabIndex = 8;
			// 
			// dateTimePickerTestContractExpiry1
			// 
			this.dateTimePickerTestContractExpiry1.Enabled = false;
			this.dateTimePickerTestContractExpiry1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerTestContractExpiry1.Location = new System.Drawing.Point(8, 152);
			this.dateTimePickerTestContractExpiry1.Name = "dateTimePickerTestContractExpiry1";
			this.dateTimePickerTestContractExpiry1.Size = new System.Drawing.Size(184, 20);
			this.dateTimePickerTestContractExpiry1.TabIndex = 7;
			// 
			// checkBoxAssignedAt
			// 
			this.checkBoxAssignedAt.Location = new System.Drawing.Point(16, 16);
			this.checkBoxAssignedAt.Name = "checkBoxAssignedAt";
			this.checkBoxAssignedAt.Size = new System.Drawing.Size(248, 24);
			this.checkBoxAssignedAt.TabIndex = 0;
			this.checkBoxAssignedAt.Text = "Служителя е назначен в този интервал";
			this.checkBoxAssignedAt.CheckedChanged += new System.EventHandler(this.checkBoxAssignedAt_CheckedChanged);
			// 
			// checkBoxContractExpiry
			// 
			this.checkBoxContractExpiry.Location = new System.Drawing.Point(16, 80);
			this.checkBoxContractExpiry.Name = "checkBoxContractExpiry";
			this.checkBoxContractExpiry.Size = new System.Drawing.Size(304, 24);
			this.checkBoxContractExpiry.TabIndex = 3;
			this.checkBoxContractExpiry.Text = "Договора на служителя изтича в този интервал";
			this.checkBoxContractExpiry.CheckedChanged += new System.EventHandler(this.checkBoxContractExpiry_CheckedChanged);
			// 
			// dateTimePickerContractExpiry2
			// 
			this.dateTimePickerContractExpiry2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerContractExpiry2.Location = new System.Drawing.Point(208, 104);
			this.dateTimePickerContractExpiry2.Name = "dateTimePickerContractExpiry2";
			this.dateTimePickerContractExpiry2.Size = new System.Drawing.Size(184, 20);
			this.dateTimePickerContractExpiry2.TabIndex = 5;
			// 
			// dateTimePickerContractExpiry1
			// 
			this.dateTimePickerContractExpiry1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerContractExpiry1.Location = new System.Drawing.Point(8, 104);
			this.dateTimePickerContractExpiry1.Name = "dateTimePickerContractExpiry1";
			this.dateTimePickerContractExpiry1.Size = new System.Drawing.Size(184, 20);
			this.dateTimePickerContractExpiry1.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(216, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(144, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "До дата";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(144, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "От дата";
			// 
			// dateTimePickerAssignedAt2
			// 
			this.dateTimePickerAssignedAt2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerAssignedAt2.Location = new System.Drawing.Point(208, 56);
			this.dateTimePickerAssignedAt2.Name = "dateTimePickerAssignedAt2";
			this.dateTimePickerAssignedAt2.Size = new System.Drawing.Size(184, 20);
			this.dateTimePickerAssignedAt2.TabIndex = 2;
			// 
			// dateTimePickerAssignedAt1
			// 
			this.dateTimePickerAssignedAt1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerAssignedAt1.Location = new System.Drawing.Point(8, 56);
			this.dateTimePickerAssignedAt1.Name = "dateTimePickerAssignedAt1";
			this.dateTimePickerAssignedAt1.Size = new System.Drawing.Size(184, 20);
			this.dateTimePickerAssignedAt1.TabIndex = 1;
			// 
			// buttonFind
			// 
			this.buttonFind.Image = ((System.Drawing.Image)(resources.GetObject("buttonFind.Image")));
			this.buttonFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonFind.Location = new System.Drawing.Point(512, 240);
			this.buttonFind.Name = "buttonFind";
			this.buttonFind.TabIndex = 4;
			this.buttonFind.Text = "  Намери";
			this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.labelYounger);
			this.groupBox3.Controls.Add(this.checkBoxPayment);
			this.groupBox3.Controls.Add(this.numBoxPaymentFrom);
			this.groupBox3.Controls.Add(this.numBoxPaymentTo);
			this.groupBox3.Location = new System.Drawing.Point(424, 288);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(352, 64);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Справка по заплата";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(240, 8);
			this.label3.Name = "label3";
			this.label3.TabIndex = 8;
			this.label3.Text = "До:";
			// 
			// labelYounger
			// 
			this.labelYounger.Location = new System.Drawing.Point(128, 8);
			this.labelYounger.Name = "labelYounger";
			this.labelYounger.Size = new System.Drawing.Size(100, 16);
			this.labelYounger.TabIndex = 7;
			this.labelYounger.Text = "Започва от:";
			// 
			// checkBoxPayment
			// 
			this.checkBoxPayment.Location = new System.Drawing.Point(8, 16);
			this.checkBoxPayment.Name = "checkBoxPayment";
			this.checkBoxPayment.Size = new System.Drawing.Size(112, 40);
			this.checkBoxPayment.TabIndex = 0;
			this.checkBoxPayment.Text = "Заплата в лв.";
			this.checkBoxPayment.CheckedChanged += new System.EventHandler(this.checkBoxAge_CheckedChanged);
			// 
			// numBoxPaymentFrom
			// 
			this.numBoxPaymentFrom.Location = new System.Drawing.Point(120, 32);
			this.numBoxPaymentFrom.Name = "numBoxPaymentFrom";
			this.numBoxPaymentFrom.TabIndex = 1;
			this.numBoxPaymentFrom.Text = "";
			// 
			// numBoxPaymentTo
			// 
			this.numBoxPaymentTo.Location = new System.Drawing.Point(240, 32);
			this.numBoxPaymentTo.Name = "numBoxPaymentTo";
			this.numBoxPaymentTo.TabIndex = 2;
			this.numBoxPaymentTo.Text = "";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.label4);
			this.groupBox4.Controls.Add(this.label5);
			this.groupBox4.Controls.Add(this.checkBoxExp);
			this.groupBox4.Controls.Add(this.numBoxExpFrom);
			this.groupBox4.Controls.Add(this.numBoxExpTo);
			this.groupBox4.Controls.Add(this.checkBoxExpYear);
			this.groupBox4.Location = new System.Drawing.Point(424, 368);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(352, 64);
			this.groupBox4.TabIndex = 3;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Справка по трудов стаж";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(240, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 16);
			this.label4.TabIndex = 8;
			this.label4.Text = "До:";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(128, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 16);
			this.label5.TabIndex = 7;
			this.label5.Text = "Започва от:";
			// 
			// checkBoxExp
			// 
			this.checkBoxExp.Location = new System.Drawing.Point(8, 16);
			this.checkBoxExp.Name = "checkBoxExp";
			this.checkBoxExp.Size = new System.Drawing.Size(112, 16);
			this.checkBoxExp.TabIndex = 0;
			this.checkBoxExp.Text = "Стаж";
			this.checkBoxExp.CheckedChanged += new System.EventHandler(this.checkBoxExp_CheckedChanged);
			// 
			// numBoxExpFrom
			// 
			this.numBoxExpFrom.Enabled = false;
			this.numBoxExpFrom.Location = new System.Drawing.Point(120, 32);
			this.numBoxExpFrom.Name = "numBoxExpFrom";
			this.numBoxExpFrom.TabIndex = 2;
			this.numBoxExpFrom.Text = "";
			// 
			// numBoxExpTo
			// 
			this.numBoxExpTo.Enabled = false;
			this.numBoxExpTo.Location = new System.Drawing.Point(240, 32);
			this.numBoxExpTo.Name = "numBoxExpTo";
			this.numBoxExpTo.TabIndex = 3;
			this.numBoxExpTo.Text = "";
			// 
			// checkBoxExpYear
			// 
			this.checkBoxExpYear.Checked = true;
			this.checkBoxExpYear.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxExpYear.Enabled = false;
			this.checkBoxExpYear.Location = new System.Drawing.Point(8, 40);
			this.checkBoxExpYear.Name = "checkBoxExpYear";
			this.checkBoxExpYear.Size = new System.Drawing.Size(104, 16);
			this.checkBoxExpYear.TabIndex = 1;
			this.checkBoxExpYear.Text = "Години";
			this.checkBoxExpYear.CheckedChanged += new System.EventHandler(this.checkBoxExpYear_CheckedChanged);
			// 
			// buttonExit
			// 
			this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonExit.Image = ((System.Drawing.Image)(resources.GetObject("buttonExit.Image")));
			this.buttonExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonExit.Location = new System.Drawing.Point(608, 240);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(80, 23);
			this.buttonExit.TabIndex = 5;
			this.buttonExit.Text = " Изход";
			// 
			// StatisticAssignment
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonExit;
			this.ClientSize = new System.Drawing.Size(784, 454);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.buttonFind);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.buttonExit);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "StatisticAssignment";
			this.ShowInTaskbar = false;
			this.Text = "Справки по назначения";
			this.Load += new System.EventHandler(this.StatisticPersonal_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void StatisticPersonal_Load(object sender, System.EventArgs e)
		{
			//this.checkedComboShtat.combobox.DataSource = this.main.nomenclaatureData.arrStaff;
			this.checkedContractType.combobox.DataSource = this.main.nomenclaatureData.arrLaw;
			this.checkedComboWorkTime.combobox.DataSource = this.main.nomenclaatureData.arrWorkTime;
			this.checkedComboContract.combobox.DataSource = this.main.nomenclaatureData.arrContract;

			DataLayer.PersonalAction personAction = new DataLayer.PersonalAction( "FirmPersonal", this.main.connString );
			this.checkedComboProfession.combobox.DataSource = personAction.SelectWhere( "globalpositions", new string[]{"positionName" }, 1, "" );
			this.checkedComboProfession.combobox.DisplayMember = "positionName";
			this.checkedComboReasonAssignment.combobox.DataSource = personAction.SelectWhere( "reasonassignment", new string[]{"level" }, 1, "" );
			this.checkedComboReasonAssignment.combobox.DisplayMember = "level";
			//    
			this.checkedComboAdministration.Text = "Служител в "+this.main.nomenclaatureData.FirmStructure[3];
			this.checkedComboDirection.Text = "Служител в "+this.main.nomenclaatureData.FirmStructure[0];
			this.checkedComboControl.Text = "Служител в "+this.main.nomenclaatureData.FirmStructure[1];
			this.checkedComboPart.Text = "Служител в "+this.main.nomenclaatureData.FirmStructure[2];

			//ot Person Info
			this.checkedComboDirection.combobox.Items.Add("");
			foreach(Nodes node in this.main.nomenclaatureData.arrDirection)
			{
				this.checkedComboDirection.combobox.Items.Add( node.NodeName );
			}
			this.checkedComboControl.combobox.Items.Add("");
			foreach(Nodes node in this.main.nomenclaatureData.arrControl)
			{
				this.checkedComboControl.combobox.Items.Add( node.NodeName );
			}
			this.checkedComboPart.combobox.Items.Add("");
			foreach(Nodes node in this.main.nomenclaatureData.arrTeam)
			{
				this.checkedComboPart.combobox.Items.Add( node.NodeName );
			}
			//this.checkedComboProfession.combobox.Items.Add("");
			this.personAction = new DataLayer.PersonalAction("FirmPersonal", this.main.connString );
			dtPosition = this.personAction.SelectAll("firmpersonal3");
			this.dtTree = main.nomenclaatureData.TreeTable;
			this.TreeLoad();
			////////

			this.numBoxPaymentFrom.Enabled = false;
			this.numBoxPaymentTo.Enabled = false;

			string[] str = new string[]{"Щат","1/2 Щат","1/4 Щат","Извънщатен"};
//			foreach(string s in str)
//			{
//				this.checkedComboShtat.combobox.Items.Add( s );
//			}
			
			str = new string[]{"Безсрочен","Срочен","Втори трудов договор","Изпитателен срок","Граждански договор общ","Граждански договор и кон.....","Допълнително споразумение"};
//			foreach(string s in str)
//			{
//				this.checkedComboContract.combobox.Items.Add( s );
//			}																																					 
		    str = new string[]{"Пълен работен ден","Половин работен ден","Неопределено работно време","Непоказано"};																												
//			foreach(string s in str)
//			{
//				this.checkedComboWorkTime.combobox.Items.Add( s );
//			}
		
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
			this.checkedComboAdministration.combobox.DataSource = arrDirection;
		}

		private void buttonFind_Click(object sender, System.EventArgs e)
		{
			bool IsInclude = false;
			arrColumnAdd = new ArrayList();
			ArrayList arrColumn = new ArrayList();
			ArrayList arrValues = new ArrayList();
			DataLayer.DataStatistics stat = new DataLayer.DataStatistics( this.main.connString );
			
			string cond = "par = ";
			this.arrAdministration.Clear();
			this.arrDepartment.Clear();
			this.arrDirection.Clear();
			this.arrSector.Clear();
			if( this.checkedComboAdministration.Checked && this.checkedComboAdministration.combobox.SelectedIndex > 0 )
			{
				this.administration = int.Parse( this.vueAdministration[this.checkedComboAdministration.combobox.SelectedIndex - 1]["id"].ToString());
				cond = "par = " +this.administration.ToString();
				for(int j = 0; j < this.vueAdministration.Count; j++)
				{
					this.arrAdministration.Add( vueAdministration[j]["id"] );
				}				
				#region Direction
				if( this.checkedComboDirection.Checked && this.checkedComboDirection.combobox.SelectedIndex > 0 )
				{
					this.arrDirection.Add( this.vueDirection[this.checkedComboDirection.combobox.SelectedIndex - 1]["id"].ToString() );
					#region Department_Control
					if( this.checkedComboControl.Checked &&  this.checkedComboControl.combobox.SelectedIndex > 0 )
					{
						this.arrDepartment.Add(this.vueDepartment[this.checkedComboControl.combobox.SelectedIndex - 1]["id"]);
						if( this.checkedComboPart.Checked &&  this.checkedComboPart.combobox.SelectedIndex > 0 )
						{
							this.arrSector.Add( this.vueSector[this.checkedComboPart.combobox.SelectedIndex - 1]["id"] );
						}
						else
						{
							cond = "par = " + this.vueDepartment[this.checkedComboControl.combobox.SelectedIndex - 1]["id"].ToString();
							vueSector = new DataView(dtTree, cond, "level", dvrs);
							for(int z = 0; z < this.vueSector.Count; z++)
							{
								this.arrSector.Add(vueSector[z]["id"]);
							}
						}
					}
					else
					{
						cond = "par = " + this.vueDirection[this.checkedComboDirection.combobox.SelectedIndex - 1]["id"].ToString();
						vueDepartment = new DataView(dtTree, cond, "level", dvrs);
						for(int j = 0; j < this.vueDepartment.Count; j++)
						{
							this.arrDepartment.Add( vueDepartment[j]["id"] );
							cond = "par = " + vueDepartment[j]["id"].ToString();
							vueSector = new DataView(dtTree, cond, "level", dvrs);
							for(int z = 0; z < this.vueSector.Count; z++)
							{
								this.arrSector.Add(vueSector[z]["id"]);
							}
						}
					}
					#endregion
				}
				else
				{
					vueDirection = new DataView(dtTree, cond, "level", dvrs);
					for(int i = 0; i < this.vueDirection.Count; i++)
					{
						this.arrDirection.Add(vueDirection[i]["id"]);
						cond = "par = " + vueDirection[i]["id"].ToString();
						vueDepartment = new DataView(dtTree, cond, "level", dvrs);
						for(int j = 0; j < this.vueDepartment.Count; j++)
						{
							this.arrDepartment.Add( vueDepartment[j]["id"] );
							cond = "par = " + vueDepartment[j]["id"].ToString();
							vueSector = new DataView(dtTree, cond, "level", dvrs);
							for(int z = 0; z < this.vueSector.Count; z++)
							{
								this.arrSector.Add(vueSector[z]["id"]);
							}
						}
						//this.checkedComboDirection.combobox.Items.Add(vueDirection[i]["level"]);
					}
				}
				#endregion // Direction
			}

			string posId = "";
			if( this.checkedComboPart.Checked && this.checkedComboPart.combobox.SelectedIndex > 0 )
			{
				//posId = " person.PositionID = " + arrSector[0].ToString();
				posId = GetPersonsFrom( posId, this.arrSector );
			}
			else
			{
				if( this.checkedComboControl.Checked && this.checkedComboControl.combobox.SelectedIndex > 0 )
				{
					posId = GetPersonsFrom( posId, this.arrSector );
					posId = GetPersonsFrom( posId, this.arrDepartment );
				}
				else
				{
					if( this.checkedComboDirection.Checked && this.checkedComboDirection.combobox.SelectedIndex > 0 )
					{
						posId = GetPersonsFrom( posId, this.arrSector );
						posId = GetPersonsFrom( posId, this.arrDepartment );
						posId = GetPersonsFrom( posId, this.arrDirection );
					}
					else
					{
						if( this.checkedComboAdministration.Checked && this.checkedComboAdministration.combobox.SelectedIndex > 0 )
						{
							posId = GetPersonsFrom( posId, this.arrSector );
							posId = GetPersonsFrom( posId, this.arrDepartment );
							posId = GetPersonsFrom( posId, this.arrDirection );
							posId = GetPersonsFrom( posId, this.arrAdministration );
							posId += " person.PositionID = " + this.administration + " OR ";
						}
						else
						{
							posId = "";
						}
					}
				}
			}
			posId = posId.TrimEnd( "OR ".ToCharArray() );
			if( posId != "" )
			{
				posId = " ( " + posId + " ) ";
			}
			foreach( Control ctrl in this.groupBox1.Controls )
			{
				if( ctrl is CheckedNumBox.CheckedNumBox )
				{
					if( ((CheckedNumBox.CheckedNumBox)ctrl).Checked )
					{
						arrValues.Add( ((CheckedNumBox.CheckedNumBox)ctrl).NumBox.Text );
						arrColumn.Add( ((CheckedNumBox.CheckedNumBox)ctrl).Column );
					}
				}
				if( ctrl is CheckedComboBox.CheckedCombo )
				{
					if( ((CheckedComboBox.CheckedCombo)ctrl).Checked )
					{
						if( ((CheckedComboBox.CheckedCombo)ctrl).Column != "" )
						{
							arrColumn.Add( ((CheckedComboBox.CheckedCombo)ctrl).Column );
							if( ((CheckedComboBox.CheckedCombo)ctrl).combobox.SelectedText == "" )
							{
								arrValues.Add( ((CheckedComboBox.CheckedCombo)ctrl).combobox.Text );
							}
							else
							{
								arrValues.Add( ((CheckedComboBox.CheckedCombo)ctrl).combobox.SelectedItem.ToString() );
							}
						}
						
					}
				}
			}
			if( arrValues.Count > 0 )
			{
				IsInclude = true;
			}
            string additional = "";

			string dat1 = mainForm.ConvertDateTimeToMySql( this.dateTimePickerAssignedAt1.Value );
			string dat2 = mainForm.ConvertDateTimeToMySql( dateTimePickerAssignedAt2.Value );

			if( this.checkBoxAssignedAt.Checked )
			{
				arrColumnAdd.Add( "personassignment.AssignedAt" );
				if( IsInclude )
				{
					additional = " AND personassignment.assignedat BETWEEN " + dat1 + " AND " + dat2;
				}
				else
				{
					additional = " personassignment.assignedat BETWEEN " + dat1 + " AND " + dat2;
					IsInclude = true;
				}

			}

			string dat3 = mainForm.ConvertDateTimeToMySql( this.dateTimePickerContractExpiry1.Value );

			string dat4 = mainForm.ConvertDateTimeToMySql( this.dateTimePickerContractExpiry2.Value );

			if(  this.checkBoxContractExpiry.Checked)
			{
				arrColumnAdd.Add( "personassignment.ContractExpiry" );
				if( IsInclude )
				{
					additional += " AND personassignment.contractexpiry BETWEEN " + dat3 + " AND " + dat4 + " ";
				}
				else
				{
					additional += " personassignment.contractexpiry BETWEEN " + dat3 + " AND " + dat4;
					IsInclude = true;
				}
				additional += " AND personassignment.ContractType = 'Срочен' OR  personassignment.ContractType = 'Срочен със срок на изпитване'";
				
			}

			string dat5 = mainForm.ConvertDateTimeToMySql( this.dateTimePickerTestContractExpiry1.Value );

			string dat6 = mainForm.ConvertDateTimeToMySql( this.dateTimePickerTestContractExpiry2.Value );

			if(  this.checkBoxTestContractExpiraty.Checked)
			{
				arrColumnAdd.Add( "personassignment.TestContractDate" );
				if( IsInclude )
				{
					additional += " AND personassignment.TestContractDate BETWEEN " + dat5 + " AND " + dat6 + " ";
				}
				else
				{
					additional += " personassignment.TestContractDate BETWEEN " + dat5 + " AND " + dat6;
					IsInclude = true;
				}
				additional += " AND personassignment.ContractType = 'Срочен със срок на изпитване' OR personassignment.ContractType = 'Безсрочен със срок на изпитване' ";
				
			}
			//-----------------------
			string s = "";
			if( this.checkBoxPayment.Checked )
			{
				arrColumnAdd.Add( "personassignment.baseSalary" );
				if( this.numBoxPaymentFrom.Text != "" )
				{
					s = " AND personassignment.baseSalary >= " + this.numBoxPaymentFrom.Text;
					if( IsInclude )
					{
						s = " AND personassignment.baseSalary >= " + this.numBoxPaymentFrom.Text;
					}
					else
					{
						s = " personassignment.baseSalary >= " + this.numBoxPaymentFrom.Text;
						IsInclude = true;
					}
				}
				if( this.numBoxPaymentTo.Text != "" )
				{
					
					if( IsInclude )
					{
						s += " AND personassignment.baseSalary <= " + this.numBoxPaymentTo.Text;
					}
					else
					{
						s += " personassignment.baseSalary <= " + this.numBoxPaymentTo.Text;
						IsInclude = true;
					}
				}
			}

			if( this.checkBoxExp.Checked )
			{
				arrColumnAdd.Add( "personassignment.years" );
				if( this.numBoxExpFrom.Text != "" )
				{
					s = " AND personassignment.baseSalary >= " + this.numBoxExpFrom.Text;
					if( IsInclude )
					{
						s = " AND personassignment.years >= " + this.numBoxExpFrom.Text;
					}
					else
					{
						s = " personassignment.years >= " + this.numBoxExpFrom.Text;
						IsInclude = true;
					}
				}
				if( this.numBoxExpTo.Text != "" )
				{
					
					if( IsInclude )
					{
						s += " AND personassignment.years <= " + this.numBoxExpTo.Text;
					}
					else
					{
						s += " personassignment.years <= " + this.numBoxExpTo.Text;
						IsInclude = true;
					}
				}

			}

			additional += s;
			if( posId != "" )
			{
				if( (arrColumn.Count > 0 || arrColumnAdd.Count > 0) )
				{
					additional += " AND " + posId;
				}
				else
				{
					additional += posId;
				}
			}
			if( arrColumn.Count != 0 | arrColumnAdd.Count!= 0 | posId != "" )
			{
				this.dt1 = stat.FindPersonByAssignment( "personAssignment", arrColumn, arrValues, additional, arrColumnAdd, IsFiredd );
				if( this.dt1 != null )
				{
					if( this.dt1.Rows.Count > 0 )
					{
						if( !this.IsTotalStat )
						{
							MessageBox.Show( "Намерени са :" + this.dt1.Rows.Count.ToString() +" човека" );
							main.formKartoteka = new KartotekaLichenSystaw( main, this.dt1, "Резултати от справката", false );
							main.formKartoteka.ShowDialog( this );
						}
						else
						{
							this.Close();
						}
					}
					else
					{
						MessageBox.Show( "Не са намерени хора според сътоветните критерии" );
					}
				}
				else
				{
					MessageBox.Show( "Не са намерени хора според сътоветните критерии" );
				}
			}
			else
			{
				MessageBox.Show( "Изберете критерии!" );
			}
		}

		private void checkBoxAssignedAt_CheckedChanged(object sender, System.EventArgs e)
		{
			this.dateTimePickerAssignedAt1.Enabled = this.checkBoxAssignedAt.Checked;
			this.dateTimePickerAssignedAt2.Enabled = this.checkBoxAssignedAt.Checked;
		}

		private void checkBoxContractExpiry_CheckedChanged(object sender, System.EventArgs e)
		{
			this.dateTimePickerContractExpiry1.Enabled = this.checkBoxContractExpiry.Checked;
			this.dateTimePickerContractExpiry2.Enabled = this.checkBoxContractExpiry.Checked;
		}

		private void combobox4_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.checkedComboDirection.combobox.Items.Clear();
			this.checkedComboDirection.combobox.Text = "";
			this.checkedComboDirection.combobox.Items.Add("");
			this.checkedComboControl.combobox.Items.Clear();
			this.checkedComboControl.combobox.Text = "";
			this.checkedComboControl.combobox.Items.Add("");
			this.checkedComboPart.combobox.Items.Clear();
			this.checkedComboPart.combobox.Text = "";
			this.checkedComboPart.combobox.Items.Add("");
			//this.checkedComboProfession.combobox.Items.Clear();
			//this.checkedComboProfession.combobox.Text = "";
			//this.checkedComboProfession.combobox.Items.Add("");

			this.arrDirectionNum.Clear();
			this.arrSector.Clear();
			this.arrDepartment.Clear();
			if(this.checkedComboAdministration.combobox.SelectedIndex > 0)
			{
				string cond = "par = " + this.vueAdministration[this.checkedComboAdministration.combobox.SelectedIndex - 1]["id"].ToString();
				this.administration = int.Parse( this.vueAdministration[this.checkedComboAdministration.combobox.SelectedIndex - 1]["id"].ToString());
				
				vueDirection = new DataView(dtTree, cond, "level", dvrs);
				
				for(int i = 0; i < this.vueDirection.Count; i++)
				{
					this.arrDirectionNum.Add(vueDirection[i]["id"]);
					cond = "par = " + vueDirection[i]["id"].ToString();
					vueDepartment = new DataView(dtTree, cond, "level", dvrs);
					for(int j = 0; j < this.vueDepartment.Count; j++)
					{
						this.arrDepartment.Add( vueDepartment[j]["id"] );
						cond = "par = " + vueDepartment[j]["id"].ToString();
						vueSector = new DataView(dtTree, cond, "level", dvrs);
						for(int z = 0; z < this.vueSector.Count; z++)
						{
							this.arrSector.Add(vueSector[z]["id"]);
						}
					}
					this.checkedComboDirection.combobox.Items.Add(vueDirection[i]["level"]);
				}

				vuePosition = new DataView(dtPosition, cond, "id", dvrs);
//				for(int i = 0; i < this.vuePosition.Count; i++)
//				{
//					this.checkedComboProfession.combobox.Items.Add(vuePosition[i]["nameOfPosition"]);
//				}
				this.nodeID = (int) this.vueAdministration[this.checkedComboAdministration.combobox.SelectedIndex - 1]["id"];
			}
			else
			{
				this.nodeID = 0;
			}
		}

		private void combobox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.checkedComboControl.combobox.Items.Clear();
			this.checkedComboControl.combobox.Text = "";
			this.checkedComboControl.combobox.Items.Add("");
			this.checkedComboPart.combobox.Items.Clear();
			this.checkedComboPart.combobox.Text = "";
			this.checkedComboPart.combobox.Items.Add("");
			//this.checkedComboProfession.combobox.Items.Clear();
			//this.checkedComboProfession.combobox.Text = "";
			//this.checkedComboProfession.combobox.Items.Add("");

			this.arrDirectionNum.Clear();
			this.arrDepartment.Clear();
			this.arrSector.Clear();


			if(this.checkedComboDirection.combobox.SelectedIndex > 0)
			{
				string cond = "par = " + this.vueDirection[this.checkedComboDirection.combobox.SelectedIndex - 1]["id"].ToString();
				this.arrDirectionNum.Add( this.vueDirection[this.checkedComboDirection.combobox.SelectedIndex - 1]["id"] );
                vueDepartment = new DataView(dtTree, cond, "level", dvrs);

				for(int i = 0; i < this.vueDepartment.Count; i++)
				{
					this.arrDepartment.Add( vueDepartment[i]["id"] );
					cond = "par = " + vueDepartment[i]["id"].ToString();
					vueSector = new DataView(dtTree, cond, "level", dvrs);
					for(int z = 0; z < this.vueSector.Count; z++)
					{
						this.arrSector.Add(vueSector[z]["id"]);
					}
					this.checkedComboControl.combobox.Items.Add(vueDepartment[i]["level"]);
				}

				vuePosition = new DataView(dtPosition, cond, "id", dvrs);
//				for(int i = 0; i < this.vuePosition.Count; i++)
//				{
//					this.checkedComboProfession.combobox.Items.Add(vuePosition[i]["nameOfPosition"]);
//				}
				this.nodeID = (int) this.vueDirection[this.checkedComboDirection.combobox.SelectedIndex - 1]["id"];
			}
			else
			{
				this.nodeID = 0;
			}
		}

		private void combobox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.checkedComboPart.combobox.Items.Clear();
			this.checkedComboPart.combobox.Text = "";
			this.checkedComboPart.combobox.Items.Add("");
			//this.checkedComboProfession.combobox.Items.Clear();
			//this.checkedComboProfession.combobox.Text = "";
			//this.checkedComboProfession.combobox.Items.Add("");
			this.arrDepartment.Clear();
			this.arrSector.Clear();

			if(this.checkedComboControl.combobox.SelectedIndex > 0)
			{
				string cond = "par = " + this.vueDepartment[this.checkedComboControl.combobox.SelectedIndex - 1]["id"].ToString();
				this.arrDepartment.Add(this.vueDepartment[this.checkedComboControl.combobox.SelectedIndex - 1]["id"]);
				vueSector = new DataView(dtTree, cond, "level", dvrs);

				for(int i = 0; i < this.vueSector.Count; i++)
				{
					this.arrSector.Add(vueSector[i]["id"]);
					this.checkedComboPart.combobox.Items.Add(vueSector[i]["level"]);
				}

				vuePosition = new DataView(dtPosition, cond, "id", dvrs);
//				for(int i = 0; i < this.vuePosition.Count; i++)
//				{
//					this.checkedComboProfession.combobox.Items.Add(vuePosition[i]["nameOfPosition"]);
//				}
				this.nodeID = (int) this.vueDepartment[this.checkedComboControl.combobox.SelectedIndex - 1]["id"];
			}	
			else if(this.checkedComboDirection.combobox.SelectedIndex > 0)
			{
				this.nodeID = (int) this.vueDirection[this.checkedComboDirection.combobox.SelectedIndex - 1]["id"];
			}
			else
			{
				this.nodeID = 0;
			}
		}

		private void combobox3_SelectedIndexChanged(object sender, EventArgs e)
		{
			//this.checkedComboProfession.combobox.Items.Clear();
			//this.checkedComboProfession.combobox.Text = "";
			//this.checkedComboProfession.combobox.Items.Add("");
			this.arrSector.Clear();

			if(this.checkedComboPart.combobox.SelectedIndex > 0)
			{
				string cond = "par = " + this.vueSector[this.checkedComboPart.combobox.SelectedIndex - 1]["id"].ToString();
				this.arrSector.Add( this.vueSector[this.checkedComboPart.combobox.SelectedIndex - 1]["id"] );
				vuePosition = new DataView(dtPosition, cond, "id", dvrs);
//				for(int i = 0; i < vuePosition.Count; i++)
//				{
//					this.checkedComboProfession.combobox.Items.Add(vuePosition[i]["nameOfPosition"]);
//				}
				this.nodeID = (int)this.vueSector[this.checkedComboPart.combobox.SelectedIndex - 1]["id"];
			}		
			else if(this.checkedComboControl.combobox.SelectedIndex > 0)
			{
				this.nodeID = (int) this.vueDepartment[this.checkedComboControl.combobox.SelectedIndex - 1]["id"];
			}
			else if(this.checkedComboDirection.combobox.SelectedIndex > 0)
			{
				this.nodeID = (int) this.vueDirection[this.checkedComboDirection.combobox.SelectedIndex - 1]["id"];
			}
			else
			{
				this.nodeID = 0;
			}
		}

		private void comboboxProfession_SelectedIndexChanged(object sender, EventArgs e)
		{
//			if(this.checkedComboProfession.combobox.SelectedIndex > 0)
//			{
//				this.positionID = int.Parse( vuePosition[this.checkedComboProfession.combobox.SelectedIndex -1]["id"].ToString());
//			}
//			else
//				this.positionID = 0;
		}

		private void checkBoxAge_CheckedChanged(object sender, System.EventArgs e)
		{
			if( this.checkBoxPayment.Checked )
			{
				this.numBoxPaymentFrom.Enabled = true;
				this.numBoxPaymentTo.Enabled = true;
			}
			else
			{
				this.numBoxPaymentFrom.Enabled = false;
				this.numBoxPaymentTo.Enabled = false;
			}
		}

		private void checkBoxExp_CheckedChanged(object sender, System.EventArgs e)
		{
			if( this.checkBoxExp.Checked )
			{
				this.numBoxExpTo.Enabled = true;
				this.numBoxExpFrom.Enabled = true;
				this.checkBoxExpYear.Enabled =true;
			}
			else
			{
				this.numBoxExpTo.Enabled = false;
				this.numBoxExpFrom.Enabled = false;
				this.checkBoxExpYear.Enabled =false;
			}
		}

		private void checkBoxExpYear_CheckedChanged(object sender, System.EventArgs e)
		{
			if( this.checkBoxExpYear.Checked )
			{
				this.checkBoxExpYear.Text = "Години";
			}
			else
			{
				this.checkBoxExpYear.Text = "Месеци";
			}
		}

		private void buttonExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void checkBoxTestContractExpiraty_CheckedChanged(object sender, System.EventArgs e)
		{
			
		}

		private void checkBoxTestContractExpiraty_CheckedChanged_1(object sender, System.EventArgs e)
		{
			this.dateTimePickerTestContractExpiry1.Enabled = this.checkBoxTestContractExpiraty.Checked;
			this.dateTimePickerTestContractExpiry2.Enabled = this.checkBoxTestContractExpiraty.Checked;
		}
	
	}
}
