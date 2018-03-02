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
	/// Summary description for formOptions.
	/// </summary>
	public class formOptions : System.Windows.Forms.Form
	{
		#region form variables
		private System.Windows.Forms.TextBox textBoxServer;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label labelUserName;
		private System.Windows.Forms.TextBox textBoxUserName;
		private System.Windows.Forms.Label label3;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.TextBox textBoxAcaunt;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBoxDataBase;
		private System.Windows.Forms.Label labelDatabase;
		private System.Windows.Forms.TextBox textBoxKeyActivation;
		private System.Windows.Forms.Label labelKey;
		private System.Windows.Forms.TextBox textBoxProductKey;
		private System.Windows.Forms.Label labelProductKey;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button buttonActive;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button buttonSaveXML;
		private System.Windows.Forms.Button buttonCancelXML;
		private System.Windows.Forms.TabPage tabPageSystem;
		private System.Windows.Forms.TabPage tabPagePersonal;
		private System.Windows.Forms.CheckBox checkBoxIncrementClass;
		private System.Windows.Forms.CheckBox checkBoxIncrementHoliday;
		private BugBox.NumBox numBoxClassCoef;
		private BugBox.NumBox numBoxHolidayCoef;
		private mainForm main;
		private System.Windows.Forms.CheckBox checkBoxVacancyStash;
		private System.Windows.Forms.CheckBox checkBoxFiredSignal;
        private Label label5;
        private ComboBox comboBoxPersonOrder;
		private bool IsStartup;
		#endregion
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public formOptions( mainForm main, bool IsStartup )
		{
			this.IsStartup = IsStartup;
			//
			// Required for Windows Form Designer support
			//
			this.main = main;
			InitializeComponent();

			this.numBoxClassCoef.IsFloat = true;
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formOptions));
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.textBoxAcaunt = new System.Windows.Forms.TextBox();
            this.textBoxDataBase = new System.Windows.Forms.TextBox();
            this.textBoxKeyActivation = new System.Windows.Forms.TextBox();
            this.labelUserName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelDatabase = new System.Windows.Forms.Label();
            this.textBoxProductKey = new System.Windows.Forms.TextBox();
            this.labelProductKey = new System.Windows.Forms.Label();
            this.labelKey = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonActive = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageSystem = new System.Windows.Forms.TabPage();
            this.tabPagePersonal = new System.Windows.Forms.TabPage();
            this.checkBoxFiredSignal = new System.Windows.Forms.CheckBox();
            this.checkBoxVacancyStash = new System.Windows.Forms.CheckBox();
            this.checkBoxIncrementClass = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numBoxClassCoef = new BugBox.NumBox();
            this.numBoxHolidayCoef = new BugBox.NumBox();
            this.checkBoxIncrementHoliday = new System.Windows.Forms.CheckBox();
            this.buttonCancelXML = new System.Windows.Forms.Button();
            this.buttonSaveXML = new System.Windows.Forms.Button();
            this.comboBoxPersonOrder = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageSystem.SuspendLayout();
            this.tabPagePersonal.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxServer
            // 
            this.textBoxServer.Location = new System.Drawing.Point(432, 24);
            this.textBoxServer.Name = "textBoxServer";
            this.textBoxServer.Size = new System.Drawing.Size(304, 20);
            this.textBoxServer.TabIndex = 0;
            this.toolTip1.SetToolTip(this.textBoxServer, "Пример: 192.168.1.1 или Server_name");
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(416, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Име или IP адрес на сървъра на базата данни:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(432, 88);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(304, 20);
            this.textBoxUserName.TabIndex = 1;
            this.toolTip1.SetToolTip(this.textBoxUserName, "Пример root, test");
            // 
            // textBoxAcaunt
            // 
            this.textBoxAcaunt.Location = new System.Drawing.Point(432, 120);
            this.textBoxAcaunt.Name = "textBoxAcaunt";
            this.textBoxAcaunt.Size = new System.Drawing.Size(304, 20);
            this.textBoxAcaunt.TabIndex = 2;
            this.toolTip1.SetToolTip(this.textBoxAcaunt, "Пример: Оставете празно полето за парола ако сървъра е инсталиран на windwos 98. " +
                    "В противен случай парола: \"Testing\" или паролата при инсталацията на сървъра с д" +
                    "анните");
            // 
            // textBoxDataBase
            // 
            this.textBoxDataBase.Location = new System.Drawing.Point(432, 56);
            this.textBoxDataBase.Name = "textBoxDataBase";
            this.textBoxDataBase.Size = new System.Drawing.Size(304, 20);
            this.textBoxDataBase.TabIndex = 3;
            this.toolTip1.SetToolTip(this.textBoxDataBase, "Пример mainData или Test");
            // 
            // textBoxKeyActivation
            // 
            this.textBoxKeyActivation.Location = new System.Drawing.Point(328, 88);
            this.textBoxKeyActivation.Name = "textBoxKeyActivation";
            this.textBoxKeyActivation.Size = new System.Drawing.Size(440, 20);
            this.textBoxKeyActivation.TabIndex = 0;
            this.toolTip1.SetToolTip(this.textBoxKeyActivation, "Въведете кода който ще получите при активирането на продукта");
            // 
            // labelUserName
            // 
            this.labelUserName.Location = new System.Drawing.Point(16, 88);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(408, 16);
            this.labelUserName.TabIndex = 4;
            this.labelUserName.Text = "Потребителско име за достъп до базата данни:";
            this.labelUserName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(40, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(384, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Парола за достъп на съответното потребителскио име:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.UseMnemonic = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxDataBase);
            this.groupBox1.Controls.Add(this.labelDatabase);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxServer);
            this.groupBox1.Controls.Add(this.textBoxUserName);
            this.groupBox1.Controls.Add(this.textBoxAcaunt);
            this.groupBox1.Controls.Add(this.labelUserName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(920, 352);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "My SQL Server";
            // 
            // labelDatabase
            // 
            this.labelDatabase.Location = new System.Drawing.Point(24, 56);
            this.labelDatabase.Name = "labelDatabase";
            this.labelDatabase.Size = new System.Drawing.Size(400, 16);
            this.labelDatabase.TabIndex = 8;
            this.labelDatabase.Text = "Име на базата данни:";
            this.labelDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxProductKey
            // 
            this.textBoxProductKey.Location = new System.Drawing.Point(328, 40);
            this.textBoxProductKey.Name = "textBoxProductKey";
            this.textBoxProductKey.ReadOnly = true;
            this.textBoxProductKey.Size = new System.Drawing.Size(440, 20);
            this.textBoxProductKey.TabIndex = 4;
            // 
            // labelProductKey
            // 
            this.labelProductKey.Location = new System.Drawing.Point(32, 40);
            this.labelProductKey.Name = "labelProductKey";
            this.labelProductKey.Size = new System.Drawing.Size(288, 16);
            this.labelProductKey.TabIndex = 9;
            this.labelProductKey.Text = "Код за активиране на продукта:";
            this.labelProductKey.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelKey
            // 
            this.labelKey.Location = new System.Drawing.Point(56, 88);
            this.labelKey.Name = "labelKey";
            this.labelKey.Size = new System.Drawing.Size(264, 24);
            this.labelKey.TabIndex = 10;
            this.labelKey.Text = "Ключ за активиране:";
            this.labelKey.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonActive);
            this.groupBox2.Controls.Add(this.textBoxKeyActivation);
            this.groupBox2.Controls.Add(this.labelKey);
            this.groupBox2.Controls.Add(this.textBoxProductKey);
            this.groupBox2.Controls.Add(this.labelProductKey);
            this.groupBox2.Location = new System.Drawing.Point(8, 360);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(920, 120);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Активиране на продукта";
            // 
            // buttonActive
            // 
            this.buttonActive.Image = ((System.Drawing.Image)(resources.GetObject("buttonActive.Image")));
            this.buttonActive.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonActive.Location = new System.Drawing.Point(784, 88);
            this.buttonActive.Name = "buttonActive";
            this.buttonActive.Size = new System.Drawing.Size(120, 23);
            this.buttonActive.TabIndex = 1;
            this.buttonActive.Text = "  Активирай";
            this.buttonActive.Click += new System.EventHandler(this.buttonActive_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageSystem);
            this.tabControl1.Controls.Add(this.tabPagePersonal);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(944, 520);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageSystem
            // 
            this.tabPageSystem.Controls.Add(this.groupBox2);
            this.tabPageSystem.Controls.Add(this.groupBox1);
            this.tabPageSystem.Location = new System.Drawing.Point(4, 22);
            this.tabPageSystem.Name = "tabPageSystem";
            this.tabPageSystem.Size = new System.Drawing.Size(936, 494);
            this.tabPageSystem.TabIndex = 0;
            this.tabPageSystem.Text = "Системни настройки";
            // 
            // tabPagePersonal
            // 
            this.tabPagePersonal.Controls.Add(this.label5);
            this.tabPagePersonal.Controls.Add(this.comboBoxPersonOrder);
            this.tabPagePersonal.Controls.Add(this.checkBoxFiredSignal);
            this.tabPagePersonal.Controls.Add(this.checkBoxVacancyStash);
            this.tabPagePersonal.Controls.Add(this.checkBoxIncrementClass);
            this.tabPagePersonal.Controls.Add(this.label4);
            this.tabPagePersonal.Controls.Add(this.label2);
            this.tabPagePersonal.Controls.Add(this.numBoxClassCoef);
            this.tabPagePersonal.Controls.Add(this.numBoxHolidayCoef);
            this.tabPagePersonal.Controls.Add(this.checkBoxIncrementHoliday);
            this.tabPagePersonal.Location = new System.Drawing.Point(4, 22);
            this.tabPagePersonal.Name = "tabPagePersonal";
            this.tabPagePersonal.Size = new System.Drawing.Size(936, 494);
            this.tabPagePersonal.TabIndex = 1;
            this.tabPagePersonal.Text = "Персонални настройки";
            // 
            // checkBoxFiredSignal
            // 
            this.checkBoxFiredSignal.Location = new System.Drawing.Point(16, 216);
            this.checkBoxFiredSignal.Name = "checkBoxFiredSignal";
            this.checkBoxFiredSignal.Size = new System.Drawing.Size(400, 24);
            this.checkBoxFiredSignal.TabIndex = 10;
            this.checkBoxFiredSignal.Text = "Известяване при добавяне на служител с прекратен договор";
            // 
            // checkBoxVacancyStash
            // 
            this.checkBoxVacancyStash.Location = new System.Drawing.Point(16, 184);
            this.checkBoxVacancyStash.Name = "checkBoxVacancyStash";
            this.checkBoxVacancyStash.Size = new System.Drawing.Size(400, 24);
            this.checkBoxVacancyStash.TabIndex = 9;
            this.checkBoxVacancyStash.Text = "Автоматично натрупване на отпуските за всяка година";
            // 
            // checkBoxIncrementClass
            // 
            this.checkBoxIncrementClass.Location = new System.Drawing.Point(16, 120);
            this.checkBoxIncrementClass.Name = "checkBoxIncrementClass";
            this.checkBoxIncrementClass.Size = new System.Drawing.Size(400, 24);
            this.checkBoxIncrementClass.TabIndex = 8;
            this.checkBoxIncrementClass.Text = "Автоматично увеличаване на процента за прослужено време";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(248, 32);
            this.label4.TabIndex = 7;
            this.label4.Text = "Брой дни увеличение на отпуската за всяка година";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(248, 32);
            this.label2.TabIndex = 6;
            this.label2.Text = "Коефициент за увеличение на процента за прослужено време";
            // 
            // numBoxClassCoef
            // 
            this.numBoxClassCoef.Location = new System.Drawing.Point(272, 8);
            this.numBoxClassCoef.Name = "numBoxClassCoef";
            this.numBoxClassCoef.Size = new System.Drawing.Size(144, 20);
            this.numBoxClassCoef.TabIndex = 5;
            // 
            // numBoxHolidayCoef
            // 
            this.numBoxHolidayCoef.Location = new System.Drawing.Point(272, 48);
            this.numBoxHolidayCoef.Name = "numBoxHolidayCoef";
            this.numBoxHolidayCoef.Size = new System.Drawing.Size(144, 20);
            this.numBoxHolidayCoef.TabIndex = 5;
            // 
            // checkBoxIncrementHoliday
            // 
            this.checkBoxIncrementHoliday.Location = new System.Drawing.Point(16, 152);
            this.checkBoxIncrementHoliday.Name = "checkBoxIncrementHoliday";
            this.checkBoxIncrementHoliday.Size = new System.Drawing.Size(400, 24);
            this.checkBoxIncrementHoliday.TabIndex = 8;
            this.checkBoxIncrementHoliday.Text = "Автоматично увеличаване на отпуските за всяка прослужена година";
            // 
            // buttonCancelXML
            // 
            this.buttonCancelXML.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancelXML.Image")));
            this.buttonCancelXML.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancelXML.Location = new System.Drawing.Point(472, 528);
            this.buttonCancelXML.Name = "buttonCancelXML";
            this.buttonCancelXML.Size = new System.Drawing.Size(144, 24);
            this.buttonCancelXML.TabIndex = 3;
            this.buttonCancelXML.Text = "Отказ";
            this.buttonCancelXML.Click += new System.EventHandler(this.buttonCancelXML_Click);
            // 
            // buttonSaveXML
            // 
            this.buttonSaveXML.Image = ((System.Drawing.Image)(resources.GetObject("buttonSaveXML.Image")));
            this.buttonSaveXML.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSaveXML.Location = new System.Drawing.Point(288, 528);
            this.buttonSaveXML.Name = "buttonSaveXML";
            this.buttonSaveXML.Size = new System.Drawing.Size(144, 24);
            this.buttonSaveXML.TabIndex = 2;
            this.buttonSaveXML.Text = "Запомни и затвори";
            this.buttonSaveXML.Click += new System.EventHandler(this.buttonSaveXML_Click);
            // 
            // comboBoxPersonOrder
            // 
            this.comboBoxPersonOrder.FormattingEnabled = true;
            this.comboBoxPersonOrder.Items.AddRange(new object[] {
            "Ред на въвеждане",
            "Име",
            "ЕГН"});
            this.comboBoxPersonOrder.Location = new System.Drawing.Point(272, 84);
            this.comboBoxPersonOrder.Name = "comboBoxPersonOrder";
            this.comboBoxPersonOrder.Size = new System.Drawing.Size(144, 21);
            this.comboBoxPersonOrder.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(248, 32);
            this.label5.TabIndex = 12;
            this.label5.Text = "Подреждане на картотеката на служителите по:";
            // 
            // formOptions
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(944, 558);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonCancelXML);
            this.Controls.Add(this.buttonSaveXML);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1000, 740);
            this.Name = "formOptions";
            this.ShowInTaskbar = false;
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.formOptions_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.formOptions_Closing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageSystem.ResumeLayout(false);
            this.tabPagePersonal.ResumeLayout(false);
            this.tabPagePersonal.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void buttonSaveXML_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.main.dsOptions.Tables[0].Rows[0][1] = this.textBoxServer.Text;
				this.main.dsOptions.Tables[0].Rows[0][2] = this.textBoxUserName.Text;
				this.main.dsOptions.Tables[0].Rows[0][3] = this.textBoxAcaunt.Text;
				this.main.dsOptions.Tables[0].Rows[0][4] = this.textBoxDataBase.Text;
				this.main.dsOptions.Tables[0].Rows[0][5] = this.textBoxKeyActivation.Text;                
				this.main.dsOptions.WriteXml( System.Windows.Forms.Application.StartupPath +"\\Config.xml", XmlWriteMode.WriteSchema );
				this.main.password = this.textBoxAcaunt.Text;
				this.main.database = this.textBoxDataBase.Text;
			}
			catch(Exception exc )
			{
				MessageBox.Show( "Could not save config file. Error msg:" + exc.Message );
				System.Diagnostics.Debug.Write( "\\n" + exc.Message );
			}
			if(this.IsStartup == true)
			{
				Dictionary<string, object> Dict = new Dictionary<string, object>();
				this.ValidateOptions(Dict);
				DataAction da = new DataAction(main.connString);
				if (da.UniversalUpdateParam(TableNames.Options, "id", Dict,"1", TransactionComnmand.NO_TRANSACTION) == false) //always update row 1
				{
					MessageBox.Show("Грешка при записване на настройките", ErrorMessages.NoConnection);
					return;
				}

				this.main.nomenclaatureData.dtOptions = da.SelectWhere(TableNames.Options, "*", "");
				if (this.main.nomenclaatureData.dtOptions == null)
				{
					MessageBox.Show("Грешка при зареждане на настройките на програмата. Моля рестартирайте!", ErrorMessages.NoConnection);
				}
			}
			this.Close();
		}

		private void formOptions_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.main.dsOptions.Tables[0].Rows[0][1] = this.textBoxServer.Text;
			this.main.dsOptions.Tables[0].Rows[0][2] = this.textBoxUserName.Text;
			this.main.dsOptions.Tables[0].Rows[0][3] = this.textBoxAcaunt.Text;
			this.main.dsOptions.Tables[0].Rows[0][4] = this.textBoxDataBase.Text;
			this.main.dsOptions.Tables[0].Rows[0][5] = this.textBoxKeyActivation.Text;
		}

		private void formOptions_Load(object sender, System.EventArgs e)
		{
			this.textBoxServer.Text  = this.main.dsOptions.Tables[0].Rows[0][1].ToString();
			this.textBoxUserName.Text  = this.main.dsOptions.Tables[0].Rows[0][2].ToString();
			this.textBoxAcaunt.Text  = this.main.dsOptions.Tables[0].Rows[0][3].ToString();
			this.textBoxDataBase.Text = this.main.dsOptions.Tables[0].Rows[0][4].ToString();
			this.textBoxKeyActivation.Text = this.main.dsOptions.Tables[0].Rows[0][5].ToString();
			this.textBoxProductKey.Text = Key.GenerateProductKey().ToString();
			if(this.IsStartup == true)
			{
				DataAction da = new DataAction( this.main.connString);
				DataTable tab = da.SelectWhere(TableNames.Options, "*", "");
				if (tab == null)
				{
					MessageBox.Show("Грешка при зареждане на настройките на програмата. Моля рестартирайте!", ErrorMessages.NoConnection);
					this.Close();
				}
				if(tab.Rows.Count > 0)
				{
                    int order;
                    int.TryParse(tab.Rows[0]["personorder"].ToString(), out order);
                    this.comboBoxPersonOrder.SelectedIndex = order;
					this.numBoxClassCoef.Text = tab.Rows[0]["classcoef"].ToString();
					this.numBoxHolidayCoef.Text = tab.Rows[0]["holidaycoef"].ToString();
					if(tab.Rows[0]["incrementclass"].ToString() == "1")
					{
						this.checkBoxIncrementClass.Checked = true;
					}
					else
					{
						this.checkBoxIncrementClass.Checked = false;
					}

					if(tab.Rows[0]["incrementHoliday"].ToString() == "1")
					{
						this.checkBoxIncrementHoliday.Checked = true;
					}
					else
					{
						this.checkBoxIncrementHoliday.Checked = false;
					}
					if(tab.Rows[0]["vacancystash"].ToString() == "1")
					{
						this.checkBoxVacancyStash.Checked = true;
					}
					else
					{
						this.checkBoxVacancyStash.Checked = false;
					}
					if(tab.Rows[0]["firedsignal"].ToString() == "1")
					{
						this.checkBoxFiredSignal.Checked = true;
					}
					else
					{
						this.checkBoxFiredSignal.Checked = false;
					}
				}
			}
			else
			{
				this.tabControl1.TabPages[1].Enabled = false;
			}
		}

		private void buttonActive_Click(object sender, System.EventArgs e)
		{
			bool registeredAll = false, regEmpty  = false;
			registeredAll  = Key.ActivateProduct( this.textBoxKeyActivation.Text, Key.GenerateProductKey(), Key.ProductOption.All );
			this.main.IsAtestaciiActive = Key.ActivateProduct( this.textBoxKeyActivation.Text, Key.GenerateProductKey(), Key.ProductOption.Atestacii );
			this.main.IsLearningActive = Key.ActivateProduct( this.textBoxKeyActivation.Text, Key.GenerateProductKey(), Key.ProductOption.Learning);
			regEmpty  = Key.ActivateProduct( this.textBoxKeyActivation.Text, Key.GenerateProductKey(), Key.ProductOption.Empty );

			if( registeredAll )
			{
				this.main.IsAtestaciiActive = true;
				this.main.IsLearningActive = true;
				MessageBox.Show( "Вие успешно активирахте всички функции на продукта! Приятна работа!" );
				RegistryAccess.SetStringRegistryValue("TotalIncrements", "45");
			}
			else if(  this.main.IsLearningActive )
			{
				MessageBox.Show( "Вие успешно активирахте модула обучение на продукта! Приятна работа!" );
				RegistryAccess.SetStringRegistryValue("TotalIncrements", "43");
			}
			else if( this.main.IsAtestaciiActive  )
			{
				MessageBox.Show( "Вие успешно активирахте модула атестации на продукта! Приятна работа!" );
				RegistryAccess.SetStringRegistryValue("TotalIncrements", "44");
			}
			else if( regEmpty )
			{
				MessageBox.Show( "Вие успешно активирахте продукта! Приятна работа!" );
				RegistryAccess.SetStringRegistryValue("TotalIncrements", "42");
			}
			else
			{
				MessageBox.Show( "Въведения ключ за активиране не е валиден! Опитайте отново." );
				RegistryAccess.SetStringRegistryValue("TotalIncrements", "243" );
			}
		}

		private void buttonCancelXML_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private bool ValidateOptions(Dictionary <string, object> Dict)
		{			
			if( this.numBoxClassCoef.Text =="" )
			{
				Dict.Add("ClassCoef", "0");
			}
			else
			{
				Dict.Add("ClassCoef", this.numBoxClassCoef.Text);
			}
			if( this.numBoxHolidayCoef.Text == "" )
			{
				Dict.Add("HolidayCoef", "0");
			}
			else
			{
				Dict.Add("HolidayCoef", this.numBoxHolidayCoef.Text);
			}

			if( this.checkBoxIncrementHoliday.Checked)
			{
				Dict.Add("IncrementHoliday", "1");
			}
			else
			{
				Dict.Add("IncrementHoliday", "0");
			}

			if( this.checkBoxIncrementClass.Checked)
			{
				Dict.Add("IncrementClass", "1");
			}
			else
			{
				Dict.Add("IncrementClass", "0");
			}

			if( this.checkBoxVacancyStash.Checked)
			{
				Dict.Add("VacancyStash", "1");
			}
			else
			{
				Dict.Add("VacancyStash", "0");
			}

			if( this.checkBoxFiredSignal.Checked)
			{
				Dict.Add("FiredSignal", "1");
			}
			else
			{
				Dict.Add("FiredSignal", "0");
			}

            Dict.Add("PersonOrder", this.comboBoxPersonOrder.SelectedIndex.ToString());
			return true;
		}
	}
}
