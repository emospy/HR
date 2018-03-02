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
	public class StatisticPersonal : System.Windows.Forms.Form
	{
		mainForm main;
		internal ArrayList arrColumn;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button buttonFind;
		private CheckedComboBox.CheckedCombo checkedComboEducation;
		private CheckedComboBox.CheckedCombo checkedComboMilitaryStatus;
		private CheckedComboBox.CheckedCombo checkedComboLanguage;
		private CheckedComboBox.CheckedCombo checkedComboProfession;
		private CheckedComboBox.CheckedCombo checkedComboCategory;
		private CheckedComboBox.CheckedCombo checkedComboFamilyStatus;
		private CheckedComboBox.CheckedCombo checkedComboCountry;
		private System.ComponentModel.IContainer components;
		private bool IsTotalStat;
		private BugBox.NumBox numBoxYounger;
		private BugBox.NumBox numBoxOlder;
		internal System.Windows.Forms.CheckBox checkBoxAge;
		private System.Windows.Forms.Label labelYounger;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ToolTip toolTip1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public DataTable dt1;
		private CheckedComboBox.CheckedCombo checkedComboRecieve;
		private CheckedComboBox.CheckedCombo checkedComboSex;
		private System.Windows.Forms.Button buttonExit;
		internal bool IsFiredd;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public StatisticPersonal( mainForm main, bool IsTotalStat, bool IsFiredd)
		{
			this.IsFiredd = IsFiredd;
			this.IsTotalStat = IsTotalStat;
			this.main = main;
			InitializeComponent();

			if( this.IsTotalStat )
			{
				this.buttonFind.Text = "Избери";
			}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(StatisticPersonal));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkedComboSex = new CheckedComboBox.CheckedCombo();
			this.checkedComboRecieve = new CheckedComboBox.CheckedCombo();
			this.checkedComboCountry = new CheckedComboBox.CheckedCombo();
			this.checkedComboFamilyStatus = new CheckedComboBox.CheckedCombo();
			this.checkedComboCategory = new CheckedComboBox.CheckedCombo();
			this.checkedComboProfession = new CheckedComboBox.CheckedCombo();
			this.checkedComboLanguage = new CheckedComboBox.CheckedCombo();
			this.checkedComboMilitaryStatus = new CheckedComboBox.CheckedCombo();
			this.checkedComboEducation = new CheckedComboBox.CheckedCombo();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.labelYounger = new System.Windows.Forms.Label();
			this.checkBoxAge = new System.Windows.Forms.CheckBox();
			this.numBoxYounger = new BugBox.NumBox();
			this.numBoxOlder = new BugBox.NumBox();
			this.buttonFind = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.buttonExit = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkedComboSex);
			this.groupBox1.Controls.Add(this.checkedComboRecieve);
			this.groupBox1.Controls.Add(this.checkedComboCountry);
			this.groupBox1.Controls.Add(this.checkedComboFamilyStatus);
			this.groupBox1.Controls.Add(this.checkedComboCategory);
			this.groupBox1.Controls.Add(this.checkedComboProfession);
			this.groupBox1.Controls.Add(this.checkedComboLanguage);
			this.groupBox1.Controls.Add(this.checkedComboMilitaryStatus);
			this.groupBox1.Controls.Add(this.checkedComboEducation);
			this.groupBox1.Location = new System.Drawing.Point(8, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(456, 272);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Избор на условия за служителите";
			// 
			// checkedComboSex
			// 
			this.checkedComboSex.Checked = false;
			this.checkedComboSex.Column = "Person.Sex";
			this.checkedComboSex.Data = null;
			this.checkedComboSex.Location = new System.Drawing.Point(8, 168);
			this.checkedComboSex.Name = "checkedComboSex";
			this.checkedComboSex.Size = new System.Drawing.Size(432, 24);
			this.checkedComboSex.TabIndex = 6;
			this.checkedComboSex.TextCombo = "Пол";
			// 
			// checkedComboRecieve
			// 
			this.checkedComboRecieve.Checked = false;
			this.checkedComboRecieve.Column = "Person.ReceivedAddon";
			this.checkedComboRecieve.Data = null;
			this.checkedComboRecieve.Location = new System.Drawing.Point(8, 144);
			this.checkedComboRecieve.Name = "checkedComboRecieve";
			this.checkedComboRecieve.Size = new System.Drawing.Size(432, 24);
			this.checkedComboRecieve.TabIndex = 5;
			this.checkedComboRecieve.TextCombo = "Получени пари за дрехи";
			// 
			// checkedComboCountry
			// 
			this.checkedComboCountry.Checked = false;
			this.checkedComboCountry.Column = "Person.country";
			this.checkedComboCountry.Data = null;
			this.checkedComboCountry.Location = new System.Drawing.Point(8, 120);
			this.checkedComboCountry.Name = "checkedComboCountry";
			this.checkedComboCountry.Size = new System.Drawing.Size(432, 24);
			this.checkedComboCountry.TabIndex = 4;
			this.checkedComboCountry.TextCombo = "Родна страна";
			// 
			// checkedComboFamilyStatus
			// 
			this.checkedComboFamilyStatus.Checked = false;
			this.checkedComboFamilyStatus.Column = "Person.familystatus";
			this.checkedComboFamilyStatus.Data = null;
			this.checkedComboFamilyStatus.Location = new System.Drawing.Point(8, 96);
			this.checkedComboFamilyStatus.Name = "checkedComboFamilyStatus";
			this.checkedComboFamilyStatus.Size = new System.Drawing.Size(432, 24);
			this.checkedComboFamilyStatus.TabIndex = 3;
			this.checkedComboFamilyStatus.TextCombo = "Семеен статус";
			// 
			// checkedComboCategory
			// 
			this.checkedComboCategory.Checked = false;
			this.checkedComboCategory.Column = "Person.category";
			this.checkedComboCategory.Data = null;
			this.checkedComboCategory.Location = new System.Drawing.Point(8, 216);
			this.checkedComboCategory.Name = "checkedComboCategory";
			this.checkedComboCategory.Size = new System.Drawing.Size(432, 24);
			this.checkedComboCategory.TabIndex = 8;
			this.checkedComboCategory.TextCombo = "Категория";
			this.checkedComboCategory.Visible = false;
			// 
			// checkedComboProfession
			// 
			this.checkedComboProfession.Checked = false;
			this.checkedComboProfession.Column = "Person.profession";
			this.checkedComboProfession.Data = null;
			this.checkedComboProfession.Location = new System.Drawing.Point(8, 192);
			this.checkedComboProfession.Name = "checkedComboProfession";
			this.checkedComboProfession.Size = new System.Drawing.Size(432, 24);
			this.checkedComboProfession.TabIndex = 7;
			this.checkedComboProfession.TextCombo = "Професия";
			this.checkedComboProfession.Visible = false;
			// 
			// checkedComboLanguage
			// 
			this.checkedComboLanguage.Checked = false;
			this.checkedComboLanguage.Column = "languagelevel.language";
			this.checkedComboLanguage.Data = null;
			this.checkedComboLanguage.Location = new System.Drawing.Point(8, 72);
			this.checkedComboLanguage.Name = "checkedComboLanguage";
			this.checkedComboLanguage.Size = new System.Drawing.Size(432, 24);
			this.checkedComboLanguage.TabIndex = 2;
			this.checkedComboLanguage.TextCombo = "Чужд език";
			// 
			// checkedComboMilitaryStatus
			// 
			this.checkedComboMilitaryStatus.Checked = false;
			this.checkedComboMilitaryStatus.Column = "Person.militaryrang";
			this.checkedComboMilitaryStatus.Data = null;
			this.checkedComboMilitaryStatus.Location = new System.Drawing.Point(8, 48);
			this.checkedComboMilitaryStatus.Name = "checkedComboMilitaryStatus";
			this.checkedComboMilitaryStatus.Size = new System.Drawing.Size(432, 24);
			this.checkedComboMilitaryStatus.TabIndex = 1;
			this.checkedComboMilitaryStatus.TextCombo = "Военен ранг";
			// 
			// checkedComboEducation
			// 
			this.checkedComboEducation.Checked = false;
			this.checkedComboEducation.Column = "Person.education";
			this.checkedComboEducation.Data = null;
			this.checkedComboEducation.Location = new System.Drawing.Point(8, 24);
			this.checkedComboEducation.Name = "checkedComboEducation";
			this.checkedComboEducation.Size = new System.Drawing.Size(432, 24);
			this.checkedComboEducation.TabIndex = 0;
			this.checkedComboEducation.TextCombo = "Образование";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.labelYounger);
			this.groupBox2.Controls.Add(this.checkBoxAge);
			this.groupBox2.Controls.Add(this.numBoxYounger);
			this.groupBox2.Controls.Add(this.numBoxOlder);
			this.groupBox2.Location = new System.Drawing.Point(8, 296);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(456, 64);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Справка по възраст";
			this.toolTip1.SetToolTip(this.groupBox2, "Напишете интервалът в който искате да напрвите справка");
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(264, 8);
			this.label1.Name = "label1";
			this.label1.TabIndex = 8;
			this.label1.Text = "До (години) :";
			// 
			// labelYounger
			// 
			this.labelYounger.Location = new System.Drawing.Point(144, 8);
			this.labelYounger.Name = "labelYounger";
			this.labelYounger.Size = new System.Drawing.Size(100, 16);
			this.labelYounger.TabIndex = 7;
			this.labelYounger.Text = "От (години):";
			// 
			// checkBoxAge
			// 
			this.checkBoxAge.Location = new System.Drawing.Point(8, 16);
			this.checkBoxAge.Name = "checkBoxAge";
			this.checkBoxAge.Size = new System.Drawing.Size(128, 40);
			this.checkBoxAge.TabIndex = 0;
			this.checkBoxAge.Text = "Навършени години";
			this.checkBoxAge.CheckedChanged += new System.EventHandler(this.checkBoxAge_CheckedChanged);
			// 
			// numBoxYounger
			// 
			this.numBoxYounger.Location = new System.Drawing.Point(144, 32);
			this.numBoxYounger.Name = "numBoxYounger";
			this.numBoxYounger.TabIndex = 1;
			this.numBoxYounger.Text = "";
			// 
			// numBoxOlder
			// 
			this.numBoxOlder.Location = new System.Drawing.Point(264, 32);
			this.numBoxOlder.Name = "numBoxOlder";
			this.numBoxOlder.TabIndex = 2;
			this.numBoxOlder.Text = "";
			// 
			// buttonFind
			// 
			this.buttonFind.Image = ((System.Drawing.Image)(resources.GetObject("buttonFind.Image")));
			this.buttonFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonFind.Location = new System.Drawing.Point(168, 368);
			this.buttonFind.Name = "buttonFind";
			this.buttonFind.Size = new System.Drawing.Size(88, 24);
			this.buttonFind.TabIndex = 2;
			this.buttonFind.Text = "   Намери";
			this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
			// 
			// buttonExit
			// 
			this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonExit.Image = ((System.Drawing.Image)(resources.GetObject("buttonExit.Image")));
			this.buttonExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonExit.Location = new System.Drawing.Point(272, 368);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(80, 23);
			this.buttonExit.TabIndex = 3;
			this.buttonExit.Text = " Изход";
			// 
			// StatisticPersonal
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonExit;
			this.ClientSize = new System.Drawing.Size(472, 398);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonFind);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "StatisticPersonal";
			this.ShowInTaskbar = false;
			this.Text = "Лични справки";
			this.Load += new System.EventHandler(this.StatisticPersonal_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void StatisticPersonal_Load(object sender, System.EventArgs e)
		{
			#region LoadNomenklatures
 
			this.checkedComboCategory.combobox.DataSource = this.main.nomenclaatureData.arrCategory;
			//this.checkedComboCountry.combobox.DataSource = this.main.nomenclaatureData.arrCountrys;
			foreach( string country in this.main.nomenclaatureData.arrCountrys ) 
			{
				this.checkedComboCountry.combobox.Items.Add( country );
			}
			this.checkedComboEducation.combobox.DataSource = this.main.nomenclaatureData.arrEducation;
			this.checkedComboFamilyStatus.combobox.DataSource = this.main.nomenclaatureData.arrFamilyStatus;
			this.checkedComboLanguage.combobox.DataSource = this.main.nomenclaatureData.arrLanguages;
			this.checkedComboMilitaryStatus.combobox.DataSource = this.main.nomenclaatureData.arrMilitaryRang;
			this.checkedComboProfession.combobox.DataSource = this.main.nomenclaatureData.arrProfession;
			this.checkedComboSex.combobox.DataSource = this.main.nomenclaatureData.arrSex;
			this.checkedComboRecieve.combobox.Items.Add( "Получени" );
			this.checkedComboRecieve.combobox.Items.Add( "Неполучени" );
			#endregion
		}

		private void buttonFind_Click(object sender, System.EventArgs e)
		{
			string additional = "";
			bool IsOnlyYears = true;
			bool ShowEgn = false;
			arrColumn = new ArrayList();
			ArrayList arrValues = new ArrayList();
			DataLayer.DataStatistics stat = new DataLayer.DataStatistics( this.main.connString );
			foreach( Control ctrl in this.groupBox1.Controls )
			{
				if( ctrl is CheckedComboBox.CheckedCombo )
				{
					if( ((CheckedComboBox.CheckedCombo)ctrl).Checked )
					{
						/* In AccsesibilityName se namira dannite za syotwetnata kolona
						a wyw accessible description se namira syotwetno izbranata stoynost 
						w combobox'a
						*/
						IsOnlyYears = false;
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
			if( this.checkBoxAge.Checked )
			{
				string temp = "";
				if( this.numBoxYounger.Text != "" || this.numBoxOlder.Text != "" )
				{
					ShowEgn = true;
					if( this.numBoxYounger.Text != "" && this.numBoxOlder.Text != "" )
					{
						temp =  " DATEDIFF( CURRENT_DATE, person.bornDate )/365 <" +this.numBoxOlder.Text + " and DATEDIFF( CURRENT_DATE, person.bornDate )/365 > "+ this.numBoxYounger.Text;
					}
					else
					{
						if( this.numBoxOlder.Text != "" )
						{
							temp =  " DATEDIFF( CURRENT_DATE, person.bornDate )/365 <" +this.numBoxOlder.Text;
						}
						else
						{
							temp =  " DATEDIFF( CURRENT_DATE, person.bornDate )/365 > "+ this.numBoxYounger.Text;
						}
					}
					if( !IsOnlyYears )
					{
						additional = " and " + temp;
					}
					else
					{
						additional = temp;
					}
				}
				else
				{
					MessageBox.Show( "Попълнете интервал от години по който ще се прави справка" );
				}
			}
			if( arrColumn.Count == 0 && additional == "" )
			{
				MessageBox.Show( "Изберете критерии за справка!" );
			}
			else
			{
				this.dt1 = stat.FindPersonBy( "person", arrColumn, arrValues, additional, this.IsFiredd, ShowEgn ) ;
			
				if( this.dt1.Rows.Count > 0 )
				{
					if( !this.IsTotalStat )
					{
						MessageBox.Show( "Намерени са :" + this.dt1.Rows.Count.ToString() +" човека" );
						main.formKartoteka = new KartotekaLichenSystaw( main, this.dt1, "Резултати от справката", this.IsFiredd );
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
		}

		private void checkBoxAge_CheckedChanged(object sender, System.EventArgs e)
		{
			if( this.checkBoxAge.Checked )
			{
				this.numBoxOlder.Enabled = true;
				this.numBoxYounger.Enabled = true;
			}
			else
			{
				this.numBoxOlder.Enabled = false;
				this.numBoxYounger.Enabled = false;

			}
		}

		private void buttonExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
