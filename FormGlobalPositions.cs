using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using DataLayer;
using System.Collections.Generic;

namespace HR
{
	/// <summary>
	/// Summary description for GlobalPositions.
	/// </summary>
	public class GlobalPositions : System.Windows.Forms.Form
	{
		private mainForm main;
		private DataTable dtPositions, dtEKDA;
		private DataAction da;
		private bool IsEdit;
		private bool IsLoad;

		#region Items

		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.Button buttonEdit;
		private System.Windows.Forms.Button buttonDelete;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.GroupBox groupBox1;
		private GroupBox groupBox2;
		private TextBox textBoxPositionEng;
		private Label label15;
		private TextBox textBoxPorNum;
		private BugBox.NumBox numBoxMaxSalary;
		private BugBox.NumBox numBoxMinSalary;
		private TextBox textBoxVOS;
		private TextBox textBoxPMS;
		private TextBox textBoxPositionName;
		private Label label1;
		private ComboBox comboBoxNKPCode;
		private ComboBox comboBoxNKPLevel;
		private ComboBox comboBoxEKDACode;
		private ComboBox comboBoxEKDALevel;
		private Label label2;
		private Label label3;
		private Label label4;
		private Label label5;
		private Label label6;
		private Label label7;
		private Label label8;
		private Label label9;
		private Label label10;
		private ComboBox comboBoxLaw;
		private Label label11;
		private ComboBox comboBoxEducation;
		private Label label12;
		private Label label13;
		private Label label14;
		private ComboBox comboBoxRang;
		private ComboBox comboBoxExperience;
		private DataGridView dataGridView1;
		private System.ComponentModel.IContainer components;
		#endregion

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public GlobalPositions(mainForm main)
		{
			try
			{
				InitializeComponent();

				this.main = main;
				da = new DataAction(main.connString);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GlobalPositions));
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonExit = new System.Windows.Forms.Button();
			this.buttonEdit = new System.Windows.Forms.Button();
			this.buttonDelete = new System.Windows.Forms.Button();
			this.buttonAdd = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textBoxPositionEng = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.textBoxPorNum = new System.Windows.Forms.TextBox();
			this.numBoxMaxSalary = new BugBox.NumBox();
			this.numBoxMinSalary = new BugBox.NumBox();
			this.textBoxVOS = new System.Windows.Forms.TextBox();
			this.textBoxPMS = new System.Windows.Forms.TextBox();
			this.textBoxPositionName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBoxNKPCode = new System.Windows.Forms.ComboBox();
			this.comboBoxNKPLevel = new System.Windows.Forms.ComboBox();
			this.comboBoxEKDACode = new System.Windows.Forms.ComboBox();
			this.comboBoxEKDALevel = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.comboBoxLaw = new System.Windows.Forms.ComboBox();
			this.label11 = new System.Windows.Forms.Label();
			this.comboBoxEducation = new System.Windows.Forms.ComboBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.comboBoxRang = new System.Windows.Forms.ComboBox();
			this.comboBoxExperience = new System.Windows.Forms.ComboBox();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonSave
			// 
			this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonSave.Image = ((System.Drawing.Image)(resources.GetObject("buttonSave.Image")));
			this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonSave.Location = new System.Drawing.Point(349, 675);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(130, 23);
			this.buttonSave.TabIndex = 2;
			this.buttonSave.Text = "Запис";
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// buttonExit
			// 
			this.buttonExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonExit.Image = ((System.Drawing.Image)(resources.GetObject("buttonExit.Image")));
			this.buttonExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonExit.Location = new System.Drawing.Point(19, 675);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(130, 23);
			this.buttonExit.TabIndex = 0;
			this.buttonExit.Text = "Изход";
			this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
			// 
			// buttonEdit
			// 
			this.buttonEdit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonEdit.Image = ((System.Drawing.Image)(resources.GetObject("buttonEdit.Image")));
			this.buttonEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonEdit.Location = new System.Drawing.Point(679, 675);
			this.buttonEdit.Name = "buttonEdit";
			this.buttonEdit.Size = new System.Drawing.Size(130, 23);
			this.buttonEdit.TabIndex = 4;
			this.buttonEdit.Text = "Коригирай";
			this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
			// 
			// buttonDelete
			// 
			this.buttonDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonDelete.Image = ((System.Drawing.Image)(resources.GetObject("buttonDelete.Image")));
			this.buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonDelete.Location = new System.Drawing.Point(514, 675);
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.Size = new System.Drawing.Size(130, 23);
			this.buttonDelete.TabIndex = 3;
			this.buttonDelete.Text = "Изтрий";
			this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
			// 
			// buttonAdd
			// 
			this.buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonAdd.Image = ((System.Drawing.Image)(resources.GetObject("buttonAdd.Image")));
			this.buttonAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonAdd.Location = new System.Drawing.Point(844, 675);
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.Size = new System.Drawing.Size(130, 23);
			this.buttonAdd.TabIndex = 5;
			this.buttonAdd.Text = "Добави";
			this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.buttonCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Image")));
			this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonCancel.Location = new System.Drawing.Point(184, 675);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(130, 23);
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "Отказ";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.dataGridView1);
			this.groupBox1.Location = new System.Drawing.Point(7, 224);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(979, 445);
			this.groupBox1.TabIndex = 21;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Списък на длъжности";
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToResizeRows = false;
			this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView1.Location = new System.Drawing.Point(3, 16);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new System.Drawing.Size(973, 426);
			this.dataGridView1.StandardTab = true;
			this.dataGridView1.TabIndex = 0;
			this.dataGridView1.TabStop = false;
			this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
			this.dataGridView1.Click += new System.EventHandler(this.dataGrid1_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.groupBox2.Controls.Add(this.textBoxPositionEng);
			this.groupBox2.Controls.Add(this.label15);
			this.groupBox2.Controls.Add(this.textBoxPorNum);
			this.groupBox2.Controls.Add(this.numBoxMaxSalary);
			this.groupBox2.Controls.Add(this.numBoxMinSalary);
			this.groupBox2.Controls.Add(this.textBoxVOS);
			this.groupBox2.Controls.Add(this.textBoxPMS);
			this.groupBox2.Controls.Add(this.textBoxPositionName);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.comboBoxNKPCode);
			this.groupBox2.Controls.Add(this.comboBoxNKPLevel);
			this.groupBox2.Controls.Add(this.comboBoxEKDACode);
			this.groupBox2.Controls.Add(this.comboBoxEKDALevel);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.comboBoxLaw);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.comboBoxEducation);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Controls.Add(this.label14);
			this.groupBox2.Controls.Add(this.comboBoxRang);
			this.groupBox2.Controls.Add(this.comboBoxExperience);
			this.groupBox2.Location = new System.Drawing.Point(13, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(966, 215);
			this.groupBox2.TabIndex = 24;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Данни за длъжност";
			// 
			// textBoxPositionEng
			// 
			this.textBoxPositionEng.Location = new System.Drawing.Point(8, 69);
			this.textBoxPositionEng.Name = "textBoxPositionEng";
			this.textBoxPositionEng.Size = new System.Drawing.Size(631, 20);
			this.textBoxPositionEng.TabIndex = 3;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(8, 53);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(160, 16);
			this.label15.TabIndex = 53;
			this.label15.Text = "Position name";
			// 
			// textBoxPorNum
			// 
			this.textBoxPorNum.Location = new System.Drawing.Point(649, 30);
			this.textBoxPorNum.Name = "textBoxPorNum";
			this.textBoxPorNum.Size = new System.Drawing.Size(150, 20);
			this.textBoxPorNum.TabIndex = 1;
			// 
			// numBoxMaxSalary
			// 
			this.numBoxMaxSalary.Location = new System.Drawing.Point(809, 190);
			this.numBoxMaxSalary.Name = "numBoxMaxSalary";
			this.numBoxMaxSalary.Size = new System.Drawing.Size(150, 20);
			this.numBoxMaxSalary.TabIndex = 14;
			// 
			// numBoxMinSalary
			// 
			this.numBoxMinSalary.Location = new System.Drawing.Point(649, 190);
			this.numBoxMinSalary.Name = "numBoxMinSalary";
			this.numBoxMinSalary.Size = new System.Drawing.Size(150, 20);
			this.numBoxMinSalary.TabIndex = 13;
			// 
			// textBoxVOS
			// 
			this.textBoxVOS.Location = new System.Drawing.Point(649, 69);
			this.textBoxVOS.Name = "textBoxVOS";
			this.textBoxVOS.Size = new System.Drawing.Size(310, 20);
			this.textBoxVOS.TabIndex = 4;
			// 
			// textBoxPMS
			// 
			this.textBoxPMS.Location = new System.Drawing.Point(809, 30);
			this.textBoxPMS.Name = "textBoxPMS";
			this.textBoxPMS.Size = new System.Drawing.Size(150, 20);
			this.textBoxPMS.TabIndex = 2;
			this.textBoxPMS.Text = "137";
			// 
			// textBoxPositionName
			// 
			this.textBoxPositionName.Location = new System.Drawing.Point(8, 30);
			this.textBoxPositionName.Name = "textBoxPositionName";
			this.textBoxPositionName.Size = new System.Drawing.Size(631, 20);
			this.textBoxPositionName.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 14);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(160, 16);
			this.label1.TabIndex = 40;
			this.label1.Text = "Наименование на длъжност";
			// 
			// comboBoxNKPCode
			// 
			this.comboBoxNKPCode.Location = new System.Drawing.Point(649, 148);
			this.comboBoxNKPCode.Name = "comboBoxNKPCode";
			this.comboBoxNKPCode.Size = new System.Drawing.Size(310, 21);
			this.comboBoxNKPCode.TabIndex = 8;
			this.comboBoxNKPCode.SelectedIndexChanged += new System.EventHandler(this.comboBoxNKPCode_SelectedIndexChanged);
			// 
			// comboBoxNKPLevel
			// 
			this.comboBoxNKPLevel.Location = new System.Drawing.Point(8, 148);
			this.comboBoxNKPLevel.Name = "comboBoxNKPLevel";
			this.comboBoxNKPLevel.Size = new System.Drawing.Size(631, 21);
			this.comboBoxNKPLevel.TabIndex = 7;
			this.comboBoxNKPLevel.SelectedIndexChanged += new System.EventHandler(this.comboBoxNKP_SelectedIndexChanged);
			// 
			// comboBoxEKDACode
			// 
			this.comboBoxEKDACode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxEKDACode.Location = new System.Drawing.Point(649, 108);
			this.comboBoxEKDACode.Name = "comboBoxEKDACode";
			this.comboBoxEKDACode.Size = new System.Drawing.Size(310, 21);
			this.comboBoxEKDACode.TabIndex = 6;
			this.comboBoxEKDACode.SelectedIndexChanged += new System.EventHandler(this.comboBoxEKDACode_SelectedIndexChanged);
			// 
			// comboBoxEKDALevel
			// 
			this.comboBoxEKDALevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxEKDALevel.Location = new System.Drawing.Point(8, 108);
			this.comboBoxEKDALevel.Name = "comboBoxEKDALevel";
			this.comboBoxEKDALevel.Size = new System.Drawing.Size(631, 21);
			this.comboBoxEKDALevel.TabIndex = 5;
			this.comboBoxEKDALevel.SelectedIndexChanged += new System.EventHandler(this.comboBoxEKDA_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(646, 14);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 16);
			this.label2.TabIndex = 44;
			this.label2.Text = "Пореден номер";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(806, 14);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 16);
			this.label3.TabIndex = 45;
			this.label3.Text = "ПМС";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(646, 53);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(40, 16);
			this.label4.TabIndex = 39;
			this.label4.Text = "ВОС";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(726, 92);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 16);
			this.label5.TabIndex = 33;
			this.label5.Text = "Ниво";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 92);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 16);
			this.label6.TabIndex = 34;
			this.label6.Text = "ЕКДА";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 132);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 16);
			this.label7.TabIndex = 35;
			this.label7.Text = "НКПД";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(726, 132);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100, 16);
			this.label8.TabIndex = 37;
			this.label8.Text = "Код";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(8, 173);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(100, 16);
			this.label9.TabIndex = 38;
			this.label9.Text = "Ранг";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(169, 173);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(120, 16);
			this.label10.TabIndex = 36;
			this.label10.Text = "Професионален опит";
			// 
			// comboBoxLaw
			// 
			this.comboBoxLaw.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxLaw.Location = new System.Drawing.Point(489, 189);
			this.comboBoxLaw.Name = "comboBoxLaw";
			this.comboBoxLaw.Size = new System.Drawing.Size(150, 21);
			this.comboBoxLaw.TabIndex = 12;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(486, 173);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(120, 16);
			this.label11.TabIndex = 32;
			this.label11.Text = "Вид правоотношение";
			// 
			// comboBoxEducation
			// 
			this.comboBoxEducation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxEducation.Location = new System.Drawing.Point(329, 189);
			this.comboBoxEducation.Name = "comboBoxEducation";
			this.comboBoxEducation.Size = new System.Drawing.Size(150, 21);
			this.comboBoxEducation.TabIndex = 11;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(326, 173);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(100, 16);
			this.label12.TabIndex = 43;
			this.label12.Text = "Образование";
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(646, 173);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(112, 16);
			this.label13.TabIndex = 41;
			this.label13.Text = "Минимална заплата";
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(806, 173);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(120, 16);
			this.label14.TabIndex = 42;
			this.label14.Text = "Максимална заплата";
			// 
			// comboBoxRang
			// 
			this.comboBoxRang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxRang.Location = new System.Drawing.Point(8, 189);
			this.comboBoxRang.Name = "comboBoxRang";
			this.comboBoxRang.Size = new System.Drawing.Size(150, 21);
			this.comboBoxRang.TabIndex = 9;
			// 
			// comboBoxExperience
			// 
			this.comboBoxExperience.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxExperience.Location = new System.Drawing.Point(169, 189);
			this.comboBoxExperience.Name = "comboBoxExperience";
			this.comboBoxExperience.Size = new System.Drawing.Size(150, 21);
			this.comboBoxExperience.TabIndex = 10;
			// 
			// GlobalPositions
			// 
			this.AcceptButton = this.buttonSave;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(992, 706);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonEdit);
			this.Controls.Add(this.buttonDelete);
			this.Controls.Add(this.buttonAdd);
			this.Controls.Add(this.buttonSave);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "GlobalPositions";
			this.ShowInTaskbar = false;
			this.Text = "Длъжности в организацията";
			this.Load += new System.EventHandler(this.GlobalPositionsAdd_Load);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				Dictionary<string, object> Dict = new Dictionary<string, object>();
				bool result = this.ValidateGlobalPosition(Dict);
				if (result)
				{
					if (!IsEdit)
					{
						int id = -1;
						id = this.da.UniversalInsertParam(TableNames.GlobalPositions, Dict, "id", TransactionComnmand.NO_TRANSACTION);
						if (id > 0)
						{
							Dict.Add("ID", id.ToString());
							this.AddPackageToTable(Dict);
						}
						else
						{
							MessageBox.Show("Грешка при добавяне на длъжност", ErrorMessages.NoConnection);
						}
					}
					else  //Pri update
					{
						if (this.dataGridView1.CurrentRow != null)
						{
							DataTable dtFP3 = new DataTable();
							DataTable dtPA = new DataTable();

							dtFP3 = this.da.SelectWhere(TableNames.FirmPersonal3, "*", "WHERE globalpositionid = " + this.dataGridView1.CurrentRow.Cells["id"].Value.ToString());
							dtPA = this.da.SelectWhere(TableNames.PersonAssignment, "*", "WHERE isactive = 1");
							if(dtFP3.Rows.Count > 0)
							{
								Dictionary<string, object> fpDict = new Dictionary<string, object>();
								Dictionary<string, object> posDict = new Dictionary<string, object>();
								fpDict.Add("Education", this.comboBoxEducation.Text);
								fpDict.Add("nameofposition", this.textBoxPositionName.Text);
								fpDict.Add("EKDACode", this.comboBoxEKDACode.Text);
								fpDict.Add("EKDALevel", this.comboBoxEKDALevel.Text);
								fpDict.Add("NKPCode", this.comboBoxNKPCode.Text);
								fpDict.Add("NKPLevel", this.comboBoxNKPLevel.Text);
								fpDict.Add("MaxSalary", this.numBoxMaxSalary.Text);
								fpDict.Add("MinSalary", this.numBoxMinSalary.Text);
								fpDict.Add("Vos", this.textBoxVOS.Text);
								fpDict.Add("Law", this.comboBoxLaw.Text);
								fpDict.Add("Rang", this.comboBoxRang.Text);
								fpDict.Add("PorNum", this.textBoxPorNum.Text);
								fpDict.Add("Experience", this.comboBoxExperience.Text);
								fpDict.Add("PMS", this.textBoxPMS.Text);
								fpDict.Add("positioneng", this.textBoxPositionEng.Text);

								posDict.Add("position", this.textBoxPositionName.Text);
								posDict.Add("NKPCode", this.comboBoxNKPCode.Text);
								posDict.Add("NKPLevel", this.comboBoxNKPLevel.Text);
								
								this.da.UniversalUpdateParam(TableNames.GlobalPositions, "id", Dict, this.dataGridView1.CurrentRow.Cells["id"].Value.ToString(), TransactionComnmand.BEGIN_TRANSACTION);
								foreach (DataRow R in dtFP3.Rows)
								{
									try
									{
										DataView vueAss = new DataView(dtPA, "positionid = " + R["id"].ToString(), "id", DataViewRowState.CurrentRows);
										//if(vueAss.Count > 1)
										//{
										//    MessageBox.Show("Грешка в базата данни. Не може да има повече от една длъжност с еднакъв служебен идентификатор");
										//    this.da.UniversalUpdate(TableNames.PersonAssignment, "", null, TransactionComnmand.ROLLBACK_TRANSACION);
										//    return;
										//}
										this.da.UniversalUpdateParam(TableNames.FirmPersonal3, "id", fpDict, R["id"].ToString(), TransactionComnmand.USE_TRANSACTION);
										for(int i = 0; i < vueAss.Count; i ++)
										{
											this.da.UniversalUpdateParam(TableNames.PersonAssignment, "id", posDict, vueAss[i]["id"].ToString(), TransactionComnmand.USE_TRANSACTION);
										}
									}
									catch (Exception ex)
									{
										ErrorLog.WriteException(ex, ex.Message);
										MessageBox.Show(ex.Message);
									}
								}
								this.da.UniversalUpdateParam(TableNames.GlobalPositions, "id", null, "", TransactionComnmand.COMMIT_TRANSACTION);
							}
							else
							{
								this.da.UniversalUpdateParam(TableNames.GlobalPositions, "id", Dict, this.dataGridView1.CurrentRow.Cells["id"].Value.ToString(), TransactionComnmand.NO_TRANSACTION);
							}
						}
					}
				}
				else
				{
					MessageBox.Show("Грешка при редакция на длъжност", "Грешка");
				}
				this.ControlEnabled(false);
				this.EnableButtons(true, true, false, true, false);
				this.Refresh();
				IsEdit = false;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private bool ValidateGlobalPosition(Dictionary<string, object> Dict)
		{
			try
			{
				if ("" == this.textBoxPositionName.Text.Trim(" ".ToCharArray()))
				{
					MessageBox.Show("Не може да оставите полето за длъжността празно.");
					return false;
				}
				Dict.Add("PositionName", this.textBoxPositionName.Text);
				Dict.Add("Rang", this.comboBoxRang.Text);
				Dict.Add("NKPCode", this.comboBoxNKPCode.Text);
				Dict.Add("NKPLevel", this.comboBoxNKPLevel.Text);
				Dict.Add("Education", this.comboBoxEducation.Text);
				Dict.Add("EKDACode", this.comboBoxEKDACode.Text);
				Dict.Add("EKDALevel", this.comboBoxEKDALevel.Text);
				Dict.Add("Experience", this.comboBoxExperience.Text);
				Dict.Add("Law", this.comboBoxLaw.Text);
				Dict.Add("MaxSalary", this.numBoxMaxSalary.Text);
				Dict.Add("MinSalary", this.numBoxMinSalary.Text);
				Dict.Add("PMS", this.textBoxPMS.Text);
				Dict.Add("PorNum", this.textBoxPorNum.Text);
				Dict.Add("VOS", this.textBoxVOS.Text);
				Dict.Add("EngPosition", this.textBoxPositionEng.Text);
				return true;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		private void EnableButtons(bool add, bool edit, bool save, bool delete, bool cancel)
		{
			try
			{
				this.buttonAdd.Enabled = add;
				this.buttonEdit.Enabled = edit;
				this.buttonSave.Enabled = save;
				this.buttonDelete.Enabled = delete;
				this.buttonCancel.Enabled = cancel;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void ControlEnabled( bool IsEnabled)
		{
			try
			{
				foreach (Control ctrl in this.Controls)
				{
					if (ctrl.GetType().Name != "Button")
					{
						ctrl.Enabled = IsEnabled;
					}
				}
				this.groupBox1.Enabled = !IsEnabled;
				this.dataGridView1.Enabled = !IsEnabled;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.EnableButtons(true, true, false, true, false);
				this.ControlEnabled(false);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonEdit_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.dataGridView1.CurrentRow != null)
				{
					IsEdit = true;
					this.EnableButtons(false, false, true, false, true);
					this.ControlEnabled(true);
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void UpdatePackage(Dictionary<string, object> Dict)
		{
			try
			{
				DataRow row = this.dtPositions.Rows.Find(Dict["ID"]);
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

		private void AddPackageToTable(Dictionary<string, object> Dict)
		{
			try
			{
				DataRow row = this.dtPositions.NewRow();

				foreach (KeyValuePair<string, object> kvp in Dict)
				{
					row[kvp.Key] = Dict[kvp.Key];
				}
				this.dtPositions.Rows.Add(row);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonAdd_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.IsEdit = false;
				this.EnableButtons(false, false, true, false, true);
				this.ControlEnabled(true);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void GlobalPositionsAdd_Load(object sender, System.EventArgs e)
		{
			try
			{
				this.IsLoad = true;
				this.dtPositions = this.da.SelectWhere(TableNames.GlobalPositions, "*", "order by PositionName, EKDALevel, NKPCode");
				this.dtPositions.PrimaryKey = new DataColumn[] {this.dtPositions.Columns["id"]};
				this.dtEKDA = da.SelectWhere(TableNames.Ekda, "*", "");
				if ((this.dtPositions == null) || (this.dtEKDA == null))
				{
					MessageBox.Show("Грешка при зареждането на данни за длъжностите", ErrorMessages.NoConnection);
					this.Close();
				}
				this.dtPositions.PrimaryKey = new DataColumn[] { this.dtPositions.Columns["ID"] };

				this.dataGridView1.DataSource = this.dtPositions;
				this.EnableButtons(true, true, false, true, false);
				this.ControlEnabled(false);
				this.comboBoxEducation.DataSource = this.main.nomenclaatureData.dtEducation;
				this.comboBoxEducation.DisplayMember = "level";
				this.comboBoxNKPLevel.DataSource = this.main.nomenclaatureData.arrNKPlevel;
				this.comboBoxNKPCode.DataSource = this.main.nomenclaatureData.arrNKPCode;
				this.comboBoxEKDACode.DataSource = this.main.nomenclaatureData.arrNKDSCode;
				this.comboBoxEKDALevel.DataSource = this.main.nomenclaatureData.arrNKDSlevel;
				this.comboBoxLaw.DataSource = this.main.nomenclaatureData.arrLaw;
				this.comboBoxExperience.DataSource = this.main.nomenclaatureData.arrExperience;
				this.comboBoxRang.DataSource = this.main.nomenclaatureData.arrRang;
				this.dataGridView1.ClearSelection();

				this.JustifyGrid();
				this.IsLoad = false;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonDelete_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.dataGridView1.CurrentRow != null)
				{
					DataTable dtFp3 = this.da.SelectWhere(TableNames.FirmPersonal3, "*", "WHERE globalpositionid = " + this.dataGridView1.CurrentRow.Cells["id"].Value.ToString());
					if (dtFp3.Rows.Count > 0)
					{
						MessageBox.Show(this, "Тази длъжност съществува в структурата на организацията и не може да бъде изтрита", "Изтриване", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						this.EnableButtons(true, true, false, true, false);
						return;
					}
					if (MessageBox.Show(this, "Сигурни ли сте че искате да премахнете длъжността " + this.dataGridView1.CurrentRow.Cells["positionname"].Value.ToString(), "Изтриване", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						if (this.da.UniversalDelete(TableNames.GlobalPositions, this.dataGridView1.CurrentRow.Cells["id"].Value.ToString(), "id"))
						{
							this.dataGridView1.Rows.Remove(this.dataGridView1.CurrentRow);
						}
						else
						{
							MessageBox.Show("Грешка при изтриване на длъжност", ErrorMessages.NoConnection);
						}
						this.EnableButtons(true, true, false, true, false);
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void buttonExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

        private void SetComboIndex(ComboBox combo, int index)
        {
            if( combo.Items.Count > 0 )
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

		private void dataGrid1_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (this.dataGridView1.CurrentRow == null || this.IsLoad)
					return;

				this.textBoxPMS.Text = this.dataGridView1.CurrentRow.Cells["PMS"].Value.ToString();
				this.textBoxPorNum.Text = this.dataGridView1.CurrentRow.Cells["PorNum"].Value.ToString();
				this.textBoxPositionName.Text = this.dataGridView1.CurrentRow.Cells["PositionName"].Value.ToString();
				this.textBoxVOS.Text = this.dataGridView1.CurrentRow.Cells["VOS"].Value.ToString();
				this.textBoxPositionEng.Text = this.dataGridView1.CurrentRow.Cells["engposition"].Value.ToString();

				this.numBoxMaxSalary.Text = this.dataGridView1.CurrentRow.Cells["MaxSalary"].Value.ToString();
				this.numBoxMinSalary.Text = this.dataGridView1.CurrentRow.Cells["MinSalary"].Value.ToString();

				int index = this.comboBoxEducation.FindString(this.dataGridView1.CurrentRow.Cells["Education"].Value.ToString());
				this.SetComboIndex(this.comboBoxEducation, index);

				index = this.comboBoxEKDALevel.FindString(this.dataGridView1.CurrentRow.Cells["EKDALevel"].Value.ToString());
				this.SetComboIndex(this.comboBoxEKDALevel, index);

				index = this.comboBoxLaw.FindString(this.dataGridView1.CurrentRow.Cells["Law"].Value.ToString());
				this.SetComboIndex(this.comboBoxLaw, index);

				index = this.comboBoxNKPCode.FindString(this.dataGridView1.CurrentRow.Cells["NKPCode"].Value.ToString());
				this.SetComboIndex(this.comboBoxNKPCode, index);

				index = this.comboBoxRang.FindString(this.dataGridView1.CurrentRow.Cells["Rang"].Value.ToString());
				this.SetComboIndex(this.comboBoxRang, index);

				index = this.comboBoxExperience.FindString(this.dataGridView1.CurrentRow.Cells["Experience"].Value.ToString());
				this.SetComboIndex(this.comboBoxExperience, index);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}				

		private void comboBoxEKDA_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				if (this.comboBoxEKDALevel.SelectedIndex > 0)
				{
					this.SetComboIndex(this.comboBoxEKDACode, this.comboBoxEKDALevel.SelectedIndex);

					this.numBoxMaxSalary.Text = this.dtEKDA.Rows[this.comboBoxEKDALevel.SelectedIndex - 1]["maxSalary"].ToString();
					this.numBoxMinSalary.Text = this.dtEKDA.Rows[this.comboBoxEKDALevel.SelectedIndex - 1]["MinSalary"].ToString();
					this.textBoxPorNum.Text = this.dtEKDA.Rows[this.comboBoxEKDALevel.SelectedIndex - 1]["PorNum"].ToString();

					int index;
					index = this.comboBoxLaw.FindString(this.dtEKDA.Rows[this.comboBoxEKDALevel.SelectedIndex - 1]["law"].ToString());
					SetComboIndex(comboBoxLaw, index);


					index = this.comboBoxEducation.FindString(this.dtEKDA.Rows[this.comboBoxEKDALevel.SelectedIndex - 1]["education"].ToString());
					SetComboIndex(comboBoxEducation, index);

					index = this.comboBoxExperience.FindString(this.dtEKDA.Rows[this.comboBoxEKDALevel.SelectedIndex - 1]["experience"].ToString());
					SetComboIndex(comboBoxExperience, index);

					index = this.comboBoxRang.FindString(this.dtEKDA.Rows[this.comboBoxEKDALevel.SelectedIndex - 1]["rang"].ToString());
					SetComboIndex(comboBoxRang, index);
				}
				else
				{
					this.comboBoxEKDACode.SelectedIndex = this.comboBoxEKDALevel.SelectedIndex;
					if (this.IsEdit)
					{
						this.numBoxMaxSalary.Text = "";
						this.numBoxMinSalary.Text = "";
						this.comboBoxExperience.SelectedIndex = 0;
						this.comboBoxRang.SelectedIndex = 0;
						this.comboBoxLaw.SelectedIndex = 0;
						this.comboBoxEducation.SelectedIndex = 0;
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void comboBoxEKDACode_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				this.SetComboIndex(this.comboBoxEKDALevel, this.comboBoxEKDACode.SelectedIndex);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void comboBoxNKP_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				this.SetComboIndex(this.comboBoxNKPCode, this.comboBoxNKPLevel.SelectedIndex);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void comboBoxNKPCode_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				this.SetComboIndex(this.comboBoxNKPLevel, this.comboBoxNKPCode.SelectedIndex);
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void JustifyGrid()
		{
			try
			{
				foreach(DataGridViewColumn columnStyle in this.dataGridView1.Columns)				
				{	
					switch( columnStyle.Name.ToLower())
					{
						case "positionname":
						{
							columnStyle.HeaderText = "Длъжност"; 
							columnStyle.Visible = true;
							break;
						}						
						case "ekdacode":
						{
							columnStyle.HeaderText = "Длъжностно ниво"; 
							columnStyle.Visible = true;
							break;
						}						
						case "nkpcode":
						{
							columnStyle.HeaderText = "Код по НКПД"; 
							columnStyle.Visible = true;
							break;
						}
						case "nkplevel":
						{
							columnStyle.HeaderText = "Длъжност по НКПД"; 
							columnStyle.Visible = true;
							break;
						}
						case "rang":
						{
							columnStyle.HeaderText = "Ранг"; 
							columnStyle.Visible = true;
							break;
						}
						case "experience":
						{
							columnStyle.HeaderText = "Опит"; 
							columnStyle.Visible = true;
							break;
						}
						case "education":
						{
							columnStyle.HeaderText = "Образование"; 
							columnStyle.Visible = true;
							break;
						}
						case "minsalary":
						{
							columnStyle.HeaderText = "Минимална заплата"; 
							columnStyle.Visible = true;
							break;
						}
						case "maxsalary":
						{
							columnStyle.HeaderText = "Максимална заплата"; 
							columnStyle.Visible = true;
							break;
						}
						case "law":
						{
							columnStyle.HeaderText = "Правоотношение"; 
							columnStyle.Visible = true;
							break;
						}
                        case "engposition":
                        {
                            columnStyle.HeaderText = "Position name";
                            columnStyle.Visible = true;
                            break;
                        }
						default :
						{
							columnStyle.Visible = false;
							break;
						}
					}

				}
			}
			catch(System.Exception)
			{
				MessageBox.Show("Some Error Justifying grid");
			}
		}

		private void dataGridView1_SelectionChanged(object sender, EventArgs e)
		{
			this.dataGrid1_Click(sender, e);
		}
	}
}