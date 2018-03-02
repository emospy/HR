using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using DataLayer;

namespace HR
{
	/// <summary>
	/// Summary description for formPosition.
	/// </summary>
	public class formPosition : System.Windows.Forms.Form
	{
		private string PositionName;
		private string NKPCode;
		private string NKPLevel;
		private FormStructureNew form;
		private TreeNode node;
		//private int id;
		private DataTable dtPositions;
		private mainForm main;
		private DataAction action;
		private DataTable dtTree;
		private DataAction daa;

		#region Items
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public BugBox.NumBox numBoxShtatCount;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Label labelLevel2;
		private System.Windows.Forms.Label labelLevel3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public System.Windows.Forms.ComboBox comboBoxPosition;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label17;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public System.Windows.Forms.TextBox textBoxKVS;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public BugBox.NumBox numBoxNumberMonths;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public System.Windows.Forms.TextBox textBoxSecurity;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public System.Windows.Forms.TextBox textBoxAdditionNumber;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public System.Windows.Forms.TextBox textBoxOtherRequirements;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public System.Windows.Forms.TextBox textBoxNotes;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public System.Windows.Forms.ComboBox comboBoxTypePosition;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ToolTip toolTipLevel1;
		private System.Windows.Forms.Label labelLevel4;
		private System.Windows.Forms.Label labelLevel1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public System.Windows.Forms.TextBox textBoxEducation;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public System.Windows.Forms.TextBox textBoxRang;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public System.Windows.Forms.TextBox textBoxPorNum;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public System.Windows.Forms.TextBox textBoxPMS;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public System.Windows.Forms.TextBox textBoxVOS;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public System.Windows.Forms.TextBox textBoxEKDACode;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public System.Windows.Forms.TextBox textBoxEKDALevel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public System.Windows.Forms.TextBox textBoxLaw;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public System.Windows.Forms.TextBox textBoxNKPCode;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public System.Windows.Forms.TextBox textBoxNKPLevel;
		private System.Windows.Forms.Label label20;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public System.Windows.Forms.TextBox textBoxMinSalary;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public System.Windows.Forms.TextBox textBoxMaxSalary;
		private System.Windows.Forms.Label label12;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public System.Windows.Forms.TextBox textBoxExperience;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.TextBox textBoxLevel3;
		private System.Windows.Forms.TextBox textBoxLevel4;
		private System.Windows.Forms.TextBox textBoxLevel1;
		private System.Windows.Forms.TextBox textBoxLevel2;
		private System.Windows.Forms.Button buttonAddPosition;
		private System.Windows.Forms.Button buttonSelectPosition;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.GroupBox groupBox2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public BugBox.NumBox numBoxStartPayment;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public BugBox.NumBox numBoxBasePayment;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public BugBox.NumBox numBoxAddon;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public BugBox.NumBox numBoxScienceAddon;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public BugBox.NumBox numBoxOtherAddon;
		private System.Windows.Forms.Label labelFree;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public BugBox.NumBox numBoxFree;
		private System.Windows.Forms.Label labelBusy;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public BugBox.NumBox numBoxBusy;
        private Label label28;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public TextBox textBoxPositionEng;
		private Label label29;
		public ComboBox comboBoxEkdaPayLevel;
		private System.ComponentModel.IContainer components;
		#endregion

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public formPosition( FormStructureNew form, mainForm main, TreeNode node)
		{
			
			InitializeComponent();

			this.main = main;
			this.action = new DataAction(this.main.connString );
			this.form = form;
			this.node = node;
			//this.id = id;
		}

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public formPosition( FormStructureNew form, mainForm main, TreeNode node, string PositionName, string NKPCode, string NKPLevel)
		{
			
			InitializeComponent();

			this.main = main;
			this.action = new DataAction( this.main.connString );
			this.form = form;
			this.node = node;
			//this.id = id;
			this.PositionName = PositionName;
			this.NKPCode = NKPCode;
			this.NKPLevel = NKPLevel;
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formPosition));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.numBoxShtatCount = new BugBox.NumBox();
			this.label5 = new System.Windows.Forms.Label();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.labelLevel2 = new System.Windows.Forms.Label();
			this.labelLevel3 = new System.Windows.Forms.Label();
			this.textBoxLevel3 = new System.Windows.Forms.TextBox();
			this.textBoxLevel4 = new System.Windows.Forms.TextBox();
			this.comboBoxPosition = new System.Windows.Forms.ComboBox();
			this.textBoxKVS = new System.Windows.Forms.TextBox();
			this.numBoxNumberMonths = new BugBox.NumBox();
			this.textBoxSecurity = new System.Windows.Forms.TextBox();
			this.textBoxAdditionNumber = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.textBoxOtherRequirements = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.textBoxNotes = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.comboBoxTypePosition = new System.Windows.Forms.ComboBox();
			this.label22 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label28 = new System.Windows.Forms.Label();
			this.textBoxPositionEng = new System.Windows.Forms.TextBox();
			this.labelBusy = new System.Windows.Forms.Label();
			this.numBoxBusy = new BugBox.NumBox();
			this.labelFree = new System.Windows.Forms.Label();
			this.numBoxFree = new BugBox.NumBox();
			this.buttonSelectPosition = new System.Windows.Forms.Button();
			this.buttonAddPosition = new System.Windows.Forms.Button();
			this.textBoxRang = new System.Windows.Forms.TextBox();
			this.textBoxPorNum = new System.Windows.Forms.TextBox();
			this.textBoxPMS = new System.Windows.Forms.TextBox();
			this.textBoxVOS = new System.Windows.Forms.TextBox();
			this.textBoxEKDACode = new System.Windows.Forms.TextBox();
			this.textBoxEKDALevel = new System.Windows.Forms.TextBox();
			this.textBoxLaw = new System.Windows.Forms.TextBox();
			this.textBoxNKPCode = new System.Windows.Forms.TextBox();
			this.textBoxNKPLevel = new System.Windows.Forms.TextBox();
			this.label20 = new System.Windows.Forms.Label();
			this.textBoxMinSalary = new System.Windows.Forms.TextBox();
			this.textBoxMaxSalary = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.textBoxExperience = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.textBoxEducation = new System.Windows.Forms.TextBox();
			this.textBoxLevel1 = new System.Windows.Forms.TextBox();
			this.textBoxLevel2 = new System.Windows.Forms.TextBox();
			this.labelLevel4 = new System.Windows.Forms.Label();
			this.labelLevel1 = new System.Windows.Forms.Label();
			this.toolTipLevel1 = new System.Windows.Forms.ToolTip(this.components);
			this.numBoxStartPayment = new BugBox.NumBox();
			this.numBoxBasePayment = new BugBox.NumBox();
			this.numBoxAddon = new BugBox.NumBox();
			this.numBoxScienceAddon = new BugBox.NumBox();
			this.numBoxOtherAddon = new BugBox.NumBox();
			this.label23 = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.label25 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.label27 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.comboBoxEkdaPayLevel = new System.Windows.Forms.ComboBox();
			this.label29 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 96);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(176, 23);
			this.label1.TabIndex = 9;
			this.label1.Text = "Наименование на длъжност";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(487, 135);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(144, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Необходимо образование";
			// 
			// numBoxShtatCount
			// 
			this.numBoxShtatCount.Location = new System.Drawing.Point(16, 190);
			this.numBoxShtatCount.Name = "numBoxShtatCount";
			this.numBoxShtatCount.Size = new System.Drawing.Size(80, 20);
			this.numBoxShtatCount.TabIndex = 0;
			this.numBoxShtatCount.TextChanged += new System.EventHandler(this.numBoxShtatCount_TextChanged);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 174);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 16);
			this.label5.TabIndex = 20;
			this.label5.Text = "Щатна бройка";
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Image")));
			this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonCancel.Location = new System.Drawing.Point(385, 476);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 10;
			this.buttonCancel.Text = "Отказ";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.Image")));
			this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonSave.Location = new System.Drawing.Point(233, 476);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 9;
			this.buttonSave.Text = " Запис";
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// labelLevel2
			// 
			this.labelLevel2.Location = new System.Drawing.Point(16, 56);
			this.labelLevel2.Name = "labelLevel2";
			this.labelLevel2.Size = new System.Drawing.Size(100, 16);
			this.labelLevel2.TabIndex = 50;
			this.labelLevel2.Text = "Отдел";
			// 
			// labelLevel3
			// 
			this.labelLevel3.Location = new System.Drawing.Point(336, 56);
			this.labelLevel3.Name = "labelLevel3";
			this.labelLevel3.Size = new System.Drawing.Size(100, 16);
			this.labelLevel3.TabIndex = 51;
			this.labelLevel3.Text = "Сектор";
			// 
			// textBoxLevel3
			// 
			this.textBoxLevel3.Location = new System.Drawing.Point(16, 72);
			this.textBoxLevel3.Name = "textBoxLevel3";
			this.textBoxLevel3.ReadOnly = true;
			this.textBoxLevel3.Size = new System.Drawing.Size(312, 20);
			this.textBoxLevel3.TabIndex = 5;
			this.textBoxLevel3.TabStop = false;
			// 
			// textBoxLevel4
			// 
			this.textBoxLevel4.Location = new System.Drawing.Point(336, 72);
			this.textBoxLevel4.Name = "textBoxLevel4";
			this.textBoxLevel4.ReadOnly = true;
			this.textBoxLevel4.Size = new System.Drawing.Size(328, 20);
			this.textBoxLevel4.TabIndex = 54;
			this.textBoxLevel4.TabStop = false;
			// 
			// comboBoxPosition
			// 
			this.comboBoxPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxPosition.Location = new System.Drawing.Point(16, 112);
			this.comboBoxPosition.Name = "comboBoxPosition";
			this.comboBoxPosition.Size = new System.Drawing.Size(412, 21);
			this.comboBoxPosition.TabIndex = 0;
			this.comboBoxPosition.SelectedIndexChanged += new System.EventHandler(this.comboBoxPosition_SelectedIndexChanged);
			// 
			// textBoxKVS
			// 
			this.textBoxKVS.Location = new System.Drawing.Point(280, 190);
			this.textBoxKVS.Name = "textBoxKVS";
			this.textBoxKVS.Size = new System.Drawing.Size(80, 20);
			this.textBoxKVS.TabIndex = 3;
			// 
			// numBoxNumberMonths
			// 
			this.numBoxNumberMonths.Location = new System.Drawing.Point(368, 190);
			this.numBoxNumberMonths.Name = "numBoxNumberMonths";
			this.numBoxNumberMonths.Size = new System.Drawing.Size(88, 20);
			this.numBoxNumberMonths.TabIndex = 4;
			// 
			// textBoxSecurity
			// 
			this.textBoxSecurity.Location = new System.Drawing.Point(464, 190);
			this.textBoxSecurity.Name = "textBoxSecurity";
			this.textBoxSecurity.Size = new System.Drawing.Size(104, 20);
			this.textBoxSecurity.TabIndex = 5;
			// 
			// textBoxAdditionNumber
			// 
			this.textBoxAdditionNumber.Location = new System.Drawing.Point(576, 190);
			this.textBoxAdditionNumber.Name = "textBoxAdditionNumber";
			this.textBoxAdditionNumber.Size = new System.Drawing.Size(88, 20);
			this.textBoxAdditionNumber.TabIndex = 6;
			this.textBoxAdditionNumber.Text = "3";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(288, 174);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 16);
			this.label4.TabIndex = 68;
			this.label4.Text = "КВС";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(376, 174);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(80, 16);
			this.label6.TabIndex = 68;
			this.label6.Text = "Брой месеци";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(464, 174);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(104, 16);
			this.label7.TabIndex = 68;
			this.label7.Text = "Ниво на сигурност";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(576, 174);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(100, 16);
			this.label11.TabIndex = 68;
			this.label11.Text = "Приложение Н=";
			// 
			// textBoxOtherRequirements
			// 
			this.textBoxOtherRequirements.Location = new System.Drawing.Point(16, 230);
			this.textBoxOtherRequirements.Name = "textBoxOtherRequirements";
			this.textBoxOtherRequirements.Size = new System.Drawing.Size(304, 20);
			this.textBoxOtherRequirements.TabIndex = 7;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(16, 214);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(100, 16);
			this.label15.TabIndex = 68;
			this.label15.Text = "Други изисквания";
			// 
			// textBoxNotes
			// 
			this.textBoxNotes.Location = new System.Drawing.Point(328, 230);
			this.textBoxNotes.Name = "textBoxNotes";
			this.textBoxNotes.Size = new System.Drawing.Size(336, 20);
			this.textBoxNotes.TabIndex = 8;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(328, 214);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(100, 16);
			this.label17.TabIndex = 68;
			this.label17.Text = "Забележка";
			// 
			// comboBoxTypePosition
			// 
			this.comboBoxTypePosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxTypePosition.Items.AddRange(new object[] {
            "Постоянна ",
            "Сезонна"});
			this.comboBoxTypePosition.Location = new System.Drawing.Point(488, 113);
			this.comboBoxTypePosition.Name = "comboBoxTypePosition";
			this.comboBoxTypePosition.Size = new System.Drawing.Size(176, 21);
			this.comboBoxTypePosition.TabIndex = 3;
			this.comboBoxTypePosition.SelectedIndexChanged += new System.EventHandler(this.comboBoxTypePosition_SelectedIndexChanged);
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(488, 97);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(100, 16);
			this.label22.TabIndex = 75;
			this.label22.Text = "Вид длъжност";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.comboBoxEkdaPayLevel);
			this.groupBox1.Controls.Add(this.label29);
			this.groupBox1.Controls.Add(this.label28);
			this.groupBox1.Controls.Add(this.textBoxPositionEng);
			this.groupBox1.Controls.Add(this.labelBusy);
			this.groupBox1.Controls.Add(this.numBoxBusy);
			this.groupBox1.Controls.Add(this.labelFree);
			this.groupBox1.Controls.Add(this.numBoxFree);
			this.groupBox1.Controls.Add(this.buttonSelectPosition);
			this.groupBox1.Controls.Add(this.buttonAddPosition);
			this.groupBox1.Controls.Add(this.textBoxRang);
			this.groupBox1.Controls.Add(this.textBoxPorNum);
			this.groupBox1.Controls.Add(this.textBoxPMS);
			this.groupBox1.Controls.Add(this.textBoxVOS);
			this.groupBox1.Controls.Add(this.textBoxEKDACode);
			this.groupBox1.Controls.Add(this.textBoxEKDALevel);
			this.groupBox1.Controls.Add(this.textBoxLaw);
			this.groupBox1.Controls.Add(this.textBoxNKPCode);
			this.groupBox1.Controls.Add(this.textBoxNKPLevel);
			this.groupBox1.Controls.Add(this.label20);
			this.groupBox1.Controls.Add(this.textBoxMinSalary);
			this.groupBox1.Controls.Add(this.textBoxMaxSalary);
			this.groupBox1.Controls.Add(this.label12);
			this.groupBox1.Controls.Add(this.textBoxExperience);
			this.groupBox1.Controls.Add(this.label13);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.label14);
			this.groupBox1.Controls.Add(this.label21);
			this.groupBox1.Controls.Add(this.label16);
			this.groupBox1.Controls.Add(this.label18);
			this.groupBox1.Controls.Add(this.label19);
			this.groupBox1.Controls.Add(this.textBoxEducation);
			this.groupBox1.Controls.Add(this.textBoxLevel1);
			this.groupBox1.Controls.Add(this.textBoxLevel2);
			this.groupBox1.Controls.Add(this.labelLevel4);
			this.groupBox1.Controls.Add(this.labelLevel1);
			this.groupBox1.Controls.Add(this.labelLevel2);
			this.groupBox1.Controls.Add(this.labelLevel3);
			this.groupBox1.Controls.Add(this.textBoxLevel3);
			this.groupBox1.Controls.Add(this.textBoxLevel4);
			this.groupBox1.Controls.Add(this.textBoxSecurity);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.numBoxShtatCount);
			this.groupBox1.Controls.Add(this.textBoxNotes);
			this.groupBox1.Controls.Add(this.label17);
			this.groupBox1.Controls.Add(this.comboBoxTypePosition);
			this.groupBox1.Controls.Add(this.label22);
			this.groupBox1.Controls.Add(this.label11);
			this.groupBox1.Controls.Add(this.textBoxAdditionNumber);
			this.groupBox1.Controls.Add(this.textBoxOtherRequirements);
			this.groupBox1.Controls.Add(this.label15);
			this.groupBox1.Controls.Add(this.comboBoxPosition);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.numBoxNumberMonths);
			this.groupBox1.Controls.Add(this.textBoxKVS);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(8, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(680, 416);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Данни за длъжността";
			// 
			// label28
			// 
			this.label28.Location = new System.Drawing.Point(16, 136);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(176, 15);
			this.label28.TabIndex = 125;
			this.label28.Text = "Position";
			// 
			// textBoxPositionEng
			// 
			this.textBoxPositionEng.Location = new System.Drawing.Point(15, 151);
			this.textBoxPositionEng.Name = "textBoxPositionEng";
			this.textBoxPositionEng.ReadOnly = true;
			this.textBoxPositionEng.Size = new System.Drawing.Size(466, 20);
			this.textBoxPositionEng.TabIndex = 124;
			// 
			// labelBusy
			// 
			this.labelBusy.Location = new System.Drawing.Point(192, 174);
			this.labelBusy.Name = "labelBusy";
			this.labelBusy.Size = new System.Drawing.Size(80, 16);
			this.labelBusy.TabIndex = 123;
			this.labelBusy.Text = "Заети";
			// 
			// numBoxBusy
			// 
			this.numBoxBusy.Location = new System.Drawing.Point(192, 190);
			this.numBoxBusy.Name = "numBoxBusy";
			this.numBoxBusy.Size = new System.Drawing.Size(80, 20);
			this.numBoxBusy.TabIndex = 2;
			// 
			// labelFree
			// 
			this.labelFree.Location = new System.Drawing.Point(104, 174);
			this.labelFree.Name = "labelFree";
			this.labelFree.Size = new System.Drawing.Size(80, 16);
			this.labelFree.TabIndex = 121;
			this.labelFree.Text = "Свободни";
			// 
			// numBoxFree
			// 
			this.numBoxFree.Location = new System.Drawing.Point(104, 190);
			this.numBoxFree.Name = "numBoxFree";
			this.numBoxFree.Size = new System.Drawing.Size(80, 20);
			this.numBoxFree.TabIndex = 1;
			// 
			// buttonSelectPosition
			// 
			this.buttonSelectPosition.Image = ((System.Drawing.Image)(resources.GetObject("buttonSelectPosition.Image")));
			this.buttonSelectPosition.Location = new System.Drawing.Point(437, 112);
			this.buttonSelectPosition.Name = "buttonSelectPosition";
			this.buttonSelectPosition.Size = new System.Drawing.Size(21, 21);
			this.buttonSelectPosition.TabIndex = 1;
			this.toolTipLevel1.SetToolTip(this.buttonSelectPosition, "Избор на длъжност");
			this.buttonSelectPosition.Click += new System.EventHandler(this.buttonSelectPosition_Click);
			// 
			// buttonAddPosition
			// 
			this.buttonAddPosition.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddPosition.Image")));
			this.buttonAddPosition.Location = new System.Drawing.Point(461, 112);
			this.buttonAddPosition.Name = "buttonAddPosition";
			this.buttonAddPosition.Size = new System.Drawing.Size(21, 21);
			this.buttonAddPosition.TabIndex = 2;
			this.toolTipLevel1.SetToolTip(this.buttonAddPosition, "Добавяне на длъжност");
			this.buttonAddPosition.Click += new System.EventHandler(this.buttonAddPosition_Click);
			// 
			// textBoxRang
			// 
			this.textBoxRang.Location = new System.Drawing.Point(192, 390);
			this.textBoxRang.Name = "textBoxRang";
			this.textBoxRang.ReadOnly = true;
			this.textBoxRang.Size = new System.Drawing.Size(168, 20);
			this.textBoxRang.TabIndex = 117;
			this.textBoxRang.TabStop = false;
			// 
			// textBoxPorNum
			// 
			this.textBoxPorNum.Location = new System.Drawing.Point(16, 390);
			this.textBoxPorNum.Name = "textBoxPorNum";
			this.textBoxPorNum.ReadOnly = true;
			this.textBoxPorNum.Size = new System.Drawing.Size(168, 20);
			this.textBoxPorNum.TabIndex = 108;
			this.textBoxPorNum.TabStop = false;
			// 
			// textBoxPMS
			// 
			this.textBoxPMS.Location = new System.Drawing.Point(520, 350);
			this.textBoxPMS.Name = "textBoxPMS";
			this.textBoxPMS.ReadOnly = true;
			this.textBoxPMS.Size = new System.Drawing.Size(144, 20);
			this.textBoxPMS.TabIndex = 107;
			this.textBoxPMS.TabStop = false;
			// 
			// textBoxVOS
			// 
			this.textBoxVOS.Location = new System.Drawing.Point(368, 350);
			this.textBoxVOS.Name = "textBoxVOS";
			this.textBoxVOS.ReadOnly = true;
			this.textBoxVOS.Size = new System.Drawing.Size(144, 20);
			this.textBoxVOS.TabIndex = 106;
			this.textBoxVOS.TabStop = false;
			// 
			// textBoxEKDACode
			// 
			this.textBoxEKDACode.Location = new System.Drawing.Point(324, 310);
			this.textBoxEKDACode.Name = "textBoxEKDACode";
			this.textBoxEKDACode.ReadOnly = true;
			this.textBoxEKDACode.Size = new System.Drawing.Size(112, 20);
			this.textBoxEKDACode.TabIndex = 103;
			this.textBoxEKDACode.TabStop = false;
			// 
			// textBoxEKDALevel
			// 
			this.textBoxEKDALevel.Location = new System.Drawing.Point(16, 310);
			this.textBoxEKDALevel.Name = "textBoxEKDALevel";
			this.textBoxEKDALevel.ReadOnly = true;
			this.textBoxEKDALevel.Size = new System.Drawing.Size(304, 20);
			this.textBoxEKDALevel.TabIndex = 102;
			this.textBoxEKDALevel.TabStop = false;
			// 
			// textBoxLaw
			// 
			this.textBoxLaw.Location = new System.Drawing.Point(368, 390);
			this.textBoxLaw.Name = "textBoxLaw";
			this.textBoxLaw.ReadOnly = true;
			this.textBoxLaw.Size = new System.Drawing.Size(144, 20);
			this.textBoxLaw.TabIndex = 118;
			this.textBoxLaw.TabStop = false;
			// 
			// textBoxNKPCode
			// 
			this.textBoxNKPCode.Location = new System.Drawing.Point(552, 270);
			this.textBoxNKPCode.Name = "textBoxNKPCode";
			this.textBoxNKPCode.ReadOnly = true;
			this.textBoxNKPCode.Size = new System.Drawing.Size(112, 20);
			this.textBoxNKPCode.TabIndex = 101;
			this.textBoxNKPCode.TabStop = false;
			// 
			// textBoxNKPLevel
			// 
			this.textBoxNKPLevel.Location = new System.Drawing.Point(16, 270);
			this.textBoxNKPLevel.Name = "textBoxNKPLevel";
			this.textBoxNKPLevel.ReadOnly = true;
			this.textBoxNKPLevel.Size = new System.Drawing.Size(528, 20);
			this.textBoxNKPLevel.TabIndex = 100;
			this.textBoxNKPLevel.TabStop = false;
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(368, 374);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(120, 16);
			this.label20.TabIndex = 116;
			this.label20.Text = "Вид правоотношение";
			// 
			// textBoxMinSalary
			// 
			this.textBoxMinSalary.Location = new System.Drawing.Point(16, 350);
			this.textBoxMinSalary.Name = "textBoxMinSalary";
			this.textBoxMinSalary.ReadOnly = true;
			this.textBoxMinSalary.Size = new System.Drawing.Size(168, 20);
			this.textBoxMinSalary.TabIndex = 104;
			this.textBoxMinSalary.TabStop = false;
			// 
			// textBoxMaxSalary
			// 
			this.textBoxMaxSalary.Location = new System.Drawing.Point(192, 350);
			this.textBoxMaxSalary.Name = "textBoxMaxSalary";
			this.textBoxMaxSalary.ReadOnly = true;
			this.textBoxMaxSalary.Size = new System.Drawing.Size(168, 20);
			this.textBoxMaxSalary.TabIndex = 105;
			this.textBoxMaxSalary.TabStop = false;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(324, 294);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(100, 16);
			this.label12.TabIndex = 99;
			this.label12.Text = "Длъжностно ниво";
			// 
			// textBoxExperience
			// 
			this.textBoxExperience.Location = new System.Drawing.Point(520, 390);
			this.textBoxExperience.Name = "textBoxExperience";
			this.textBoxExperience.ReadOnly = true;
			this.textBoxExperience.Size = new System.Drawing.Size(144, 20);
			this.textBoxExperience.TabIndex = 119;
			this.textBoxExperience.TabStop = false;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(16, 294);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(144, 16);
			this.label13.TabIndex = 98;
			this.label13.Text = "Длъжност по ЕКДА";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(552, 254);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(100, 16);
			this.label10.TabIndex = 97;
			this.label10.Text = "Код по НКПД";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 254);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(144, 23);
			this.label3.TabIndex = 96;
			this.label3.Text = "Професия по НКПД";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(368, 334);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100, 16);
			this.label8.TabIndex = 110;
			this.label8.Text = "ВОС";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(520, 334);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(100, 16);
			this.label9.TabIndex = 109;
			this.label9.Text = "ПМС";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(16, 374);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(100, 16);
			this.label14.TabIndex = 112;
			this.label14.Text = "Пореден номер";
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(520, 374);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(120, 16);
			this.label21.TabIndex = 111;
			this.label21.Text = "Опит";
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(16, 334);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(112, 16);
			this.label16.TabIndex = 115;
			this.label16.Text = "Минимална заплата";
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(192, 334);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(120, 16);
			this.label18.TabIndex = 114;
			this.label18.Text = "Максимална заплата";
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(192, 374);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(100, 16);
			this.label19.TabIndex = 113;
			this.label19.Text = "Ранг";
			// 
			// textBoxEducation
			// 
			this.textBoxEducation.Location = new System.Drawing.Point(488, 151);
			this.textBoxEducation.Name = "textBoxEducation";
			this.textBoxEducation.ReadOnly = true;
			this.textBoxEducation.Size = new System.Drawing.Size(176, 20);
			this.textBoxEducation.TabIndex = 1;
			this.textBoxEducation.TabStop = false;
			// 
			// textBoxLevel1
			// 
			this.textBoxLevel1.Location = new System.Drawing.Point(16, 32);
			this.textBoxLevel1.Name = "textBoxLevel1";
			this.textBoxLevel1.ReadOnly = true;
			this.textBoxLevel1.Size = new System.Drawing.Size(312, 20);
			this.textBoxLevel1.TabIndex = 84;
			this.textBoxLevel1.TabStop = false;
			// 
			// textBoxLevel2
			// 
			this.textBoxLevel2.Location = new System.Drawing.Point(336, 32);
			this.textBoxLevel2.Name = "textBoxLevel2";
			this.textBoxLevel2.ReadOnly = true;
			this.textBoxLevel2.Size = new System.Drawing.Size(328, 20);
			this.textBoxLevel2.TabIndex = 82;
			this.textBoxLevel2.TabStop = false;
			// 
			// labelLevel4
			// 
			this.labelLevel4.Location = new System.Drawing.Point(12, 16);
			this.labelLevel4.Name = "labelLevel4";
			this.labelLevel4.Size = new System.Drawing.Size(100, 16);
			this.labelLevel4.TabIndex = 83;
			this.labelLevel4.Text = "Администрация";
			// 
			// labelLevel1
			// 
			this.labelLevel1.Location = new System.Drawing.Point(336, 16);
			this.labelLevel1.Name = "labelLevel1";
			this.labelLevel1.Size = new System.Drawing.Size(100, 16);
			this.labelLevel1.TabIndex = 81;
			this.labelLevel1.Text = "Дирекция";
			// 
			// numBoxStartPayment
			// 
			this.numBoxStartPayment.Location = new System.Drawing.Point(16, 32);
			this.numBoxStartPayment.Name = "numBoxStartPayment";
			this.numBoxStartPayment.Size = new System.Drawing.Size(128, 20);
			this.numBoxStartPayment.TabIndex = 0;
			// 
			// numBoxBasePayment
			// 
			this.numBoxBasePayment.Location = new System.Drawing.Point(152, 32);
			this.numBoxBasePayment.Name = "numBoxBasePayment";
			this.numBoxBasePayment.Size = new System.Drawing.Size(128, 20);
			this.numBoxBasePayment.TabIndex = 1;
			// 
			// numBoxAddon
			// 
			this.numBoxAddon.Location = new System.Drawing.Point(288, 32);
			this.numBoxAddon.Name = "numBoxAddon";
			this.numBoxAddon.Size = new System.Drawing.Size(120, 20);
			this.numBoxAddon.TabIndex = 2;
			// 
			// numBoxScienceAddon
			// 
			this.numBoxScienceAddon.Location = new System.Drawing.Point(416, 32);
			this.numBoxScienceAddon.Name = "numBoxScienceAddon";
			this.numBoxScienceAddon.Size = new System.Drawing.Size(120, 20);
			this.numBoxScienceAddon.TabIndex = 3;
			// 
			// numBoxOtherAddon
			// 
			this.numBoxOtherAddon.Location = new System.Drawing.Point(544, 32);
			this.numBoxOtherAddon.Name = "numBoxOtherAddon";
			this.numBoxOtherAddon.Size = new System.Drawing.Size(120, 20);
			this.numBoxOtherAddon.TabIndex = 4;
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(16, 16);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(128, 16);
			this.label23.TabIndex = 112;
			this.label23.Text = "Начално :";
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(152, 16);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(128, 16);
			this.label24.TabIndex = 112;
			this.label24.Text = "Основно :";
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(288, 16);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(100, 16);
			this.label25.TabIndex = 112;
			this.label25.Text = "Увеличение :";
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(416, 16);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(100, 16);
			this.label26.TabIndex = 112;
			this.label26.Text = "Научна степен :";
			// 
			// label27
			// 
			this.label27.Location = new System.Drawing.Point(544, 16);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(100, 16);
			this.label27.TabIndex = 112;
			this.label27.Text = "Други :";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.numBoxOtherAddon);
			this.groupBox2.Controls.Add(this.label27);
			this.groupBox2.Controls.Add(this.label25);
			this.groupBox2.Controls.Add(this.numBoxScienceAddon);
			this.groupBox2.Controls.Add(this.label26);
			this.groupBox2.Controls.Add(this.label24);
			this.groupBox2.Controls.Add(this.label23);
			this.groupBox2.Controls.Add(this.numBoxBasePayment);
			this.groupBox2.Controls.Add(this.numBoxStartPayment);
			this.groupBox2.Controls.Add(this.numBoxAddon);
			this.groupBox2.Location = new System.Drawing.Point(8, 414);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(680, 56);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Данни за възнаграждение";
			// 
			// comboBoxEkdaPayLevel
			// 
			this.comboBoxEkdaPayLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxEkdaPayLevel.Items.AddRange(new object[] {
            "Постоянна ",
            "Сезонна"});
			this.comboBoxEkdaPayLevel.Location = new System.Drawing.Point(442, 309);
			this.comboBoxEkdaPayLevel.Name = "comboBoxEkdaPayLevel";
			this.comboBoxEkdaPayLevel.Size = new System.Drawing.Size(218, 21);
			this.comboBoxEkdaPayLevel.TabIndex = 126;
			// 
			// label29
			// 
			this.label29.Location = new System.Drawing.Point(441, 294);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(219, 16);
			this.label29.TabIndex = 127;
			this.label29.Text = "Ниво на основната месечна заплата";
			// 
			// formPosition
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(692, 508);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.groupBox2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "formPosition";
			this.ShowInTaskbar = false;
			this.Text = "Добавяне на длъжност";
			this.Load += new System.EventHandler(this.formPosition_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private int GetDeepOfNodes( TreeNode node )
		{
			if(node != null)
			{
				for( int i = 0; i < 4; i++ )
				{
					if( node.Parent == null )
					{
						return i;
					}
					else
					{
						node = node.Parent;
					}
				}
			}
			return 0;
		}

		private void formPosition_Load(object sender, System.EventArgs e)
		{
			int deep;
			if(node == null)
			{
				MessageBox.Show("Не сте избрали звено, в което да бъде добавена длъжността");
				this.DialogResult = DialogResult.Cancel;
				this.Close();				
				return;
			}
			this.daa = new DataAction(this.main.connString);

			this.dtPositions = this.daa.SelectWhere(TableNames.GlobalPositions, "*", "order by positionName, nkpcode");
			if (this.dtPositions == null)
			{
				MessageBox.Show("Грешка при зареждането на данни за длъжностите", ErrorMessages.NoConnection);
				this.DialogResult = DialogResult.Cancel;
				this.Close();
			}
			this.numBoxShtatCount.IsFloat = true;
			this.numBoxFree.IsFloat = true;
			this.numBoxBusy.IsFloat = true;
            this.labelLevel4.Text = "Сектор";
            this.labelLevel1.Text = "Администрация";
            this.labelLevel2.Text = "Дирекция";
            this.labelLevel3.Text = "Отдел";
			this.dtTree = this.main.nomenclaatureData.dtTreeTable;

			deep = this.GetDeepOfNodes(node);

			switch (deep)
			{
				case 0:
					{
						this.textBoxLevel1.Text = node.Text;
						break;
					}
				case 1:
					{
						this.textBoxLevel2.Text = node.Text;
						this.textBoxLevel1.Text = node.Parent.Text;
						break;
					}
				case 2:
					{
						this.textBoxLevel3.Text = node.Text;
						this.textBoxLevel2.Text = node.Parent.Text;
						this.textBoxLevel1.Text = node.Parent.Parent.Text;
						break;
					}
				case 3:
					{
						this.textBoxLevel4.Text = node.Text;
						this.textBoxLevel3.Text = node.Parent.Text;
						this.textBoxLevel2.Text = node.Parent.Parent.Text;
						this.textBoxLevel1.Text = node.Parent.Parent.Parent.Text;
						break;
					}
			}

			this.comboBoxPosition.DataSource = this.dtPositions;
			this.comboBoxPosition.DisplayMember = "PositionName";

			int index = 0;
			foreach (DataRow row in this.dtPositions.Rows)
			{
				if (row["PositionName"].ToString() == this.PositionName && row["NKPCode"].ToString() == this.NKPCode && row["NKPLevel"].ToString() == this.NKPLevel)
				{
					break;
				}
				index++;
			}
			if (index >= this.dtPositions.Rows.Count)
			{
				index = this.dtPositions.Rows.Count - 1;
			}
			if (index >= 0)
				this.comboBoxPosition.SelectedIndex = index;

			this.comboBoxTypePosition.SelectedIndex = 0;
		}

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}	

		private void buttonSave_Click(object sender, System.EventArgs e)
		{		
//			if(this.comboBoxPosition.SelectedIndex != 0)
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void comboBoxPosition_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int RowNum = this.comboBoxPosition.SelectedIndex;
			this.textBoxEducation.Text = this.dtPositions.Rows[RowNum]["Education"].ToString();
			this.textBoxEKDACode.Text = this.dtPositions.Rows[RowNum]["EKDACode"].ToString();
			this.textBoxEKDALevel.Text = this.dtPositions.Rows[RowNum]["EKDALevel"].ToString();
			this.textBoxNKPCode.Text = this.dtPositions.Rows[RowNum]["NKPCode"].ToString();
			this.textBoxNKPLevel.Text = this.dtPositions.Rows[RowNum]["NKPLevel"].ToString();
			this.textBoxMaxSalary.Text = this.dtPositions.Rows[RowNum]["MaxSalary"].ToString();
			this.textBoxMinSalary.Text = this.dtPositions.Rows[RowNum]["MinSalary"].ToString();
			this.textBoxVOS.Text = this.dtPositions.Rows[RowNum]["Vos"].ToString();
			this.textBoxLaw.Text = this.dtPositions.Rows[RowNum]["Law"].ToString();
			this.textBoxRang.Text = this.dtPositions.Rows[RowNum]["Rang"].ToString();
			this.textBoxMinSalary.Text = this.dtPositions.Rows[RowNum]["MinSalary"].ToString();
			this.textBoxPorNum.Text = this.dtPositions.Rows[RowNum]["PorNum"].ToString();	
			this.textBoxExperience.Text = this.dtPositions.Rows[RowNum]["Experience"].ToString();
			this.textBoxPMS.Text = this.dtPositions.Rows[RowNum]["PMS"].ToString();
            this.textBoxPositionEng.Text = this.dtPositions.Rows[RowNum]["engposition"].ToString();
		}

		private void comboBoxTypePosition_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(comboBoxTypePosition.SelectedIndex == 0)
			{
				this.numBoxNumberMonths.Enabled = false;
			}
			else
			{
				this.numBoxNumberMonths.Enabled = true;
			}
		}

		private void numBoxShtatCount_TextChanged(object sender, System.EventArgs e)
		{
			if(this.comboBoxTypePosition.SelectedIndex == 0) //Permanent position
			{
				try
				{
					this.numBoxNumberMonths.Text = (int.Parse(this.numBoxShtatCount.Text) * 12).ToString();
				}
				catch
				{
				}
			}
		}

		private void buttonAddPosition_Click(object sender, System.EventArgs e)
		{
			GlobalPositions form = new GlobalPositions(this.main);
			form.ShowDialog();
			this.formPosition_Load(sender, e);
		}

		private void buttonSelectPosition_Click(object sender, System.EventArgs e)
		{
			dtPositions.TableName = "GlobalPositions";
			FormChoose form = new FormChoose(this.dtPositions, "длъжност");
			form.ShowDialog();
			if(form.DialogResult == DialogResult.OK)
			{
				int index = 0;
				foreach(DataRow row in this.dtPositions.Rows)
				{
					if (row["PositionName"].ToString() == form.dataGridView1.CurrentRow.Cells["PositionName"].Value.ToString() && row["NKPCode"].ToString() == form.dataGridView1.CurrentRow.Cells["NKPCode"].Value.ToString() && row["NKPLevel"].ToString() == form.dataGridView1.CurrentRow.Cells["NKPLevel"].Value.ToString())
					{					
						break;
					}
					index ++;
				}
				if(index >= this.dtPositions.Rows.Count)
				{
					index = 0;
				}
				if(index >=0)
					this.comboBoxPosition.SelectedIndex = index;
			}
		}		
	}
}
