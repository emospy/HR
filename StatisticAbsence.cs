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
	public class StatisticAbsence : System.Windows.Forms.Form
	{
		internal ArrayList arrColumn;
		mainForm main;
		private bool IsTotalStat;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public DataTable dt1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button buttonFind;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private CheckedComboBox.CheckedCombo checkedComboTypeAbsence;
		private CheckedNumBox.CheckedNumBox checkedNumBoxNumberOrder;
		private System.Windows.Forms.CheckBox checkBoxFrom;
		private System.Windows.Forms.CheckBox checkBoxTo;
		private System.Windows.Forms.DateTimePicker dateTimePickerTo2;
		private System.Windows.Forms.DateTimePicker dateTimePickerTo1;
		private System.Windows.Forms.DateTimePicker dateTimePickerFrom2;
		private System.Windows.Forms.DateTimePicker dateTimePickerFrom1;
		private System.Windows.Forms.Button buttonExit;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public StatisticAbsence( mainForm main, bool IsTotalStat)
		{
			this.IsTotalStat = IsTotalStat;
            this.main = main;
			InitializeComponent();
			this.dateTimePickerFrom1.Enabled = this.checkBoxFrom.Checked;
			this.dateTimePickerFrom2.Enabled = this.checkBoxFrom.Checked;
			this.dateTimePickerTo1.Enabled = this.checkBoxTo.Checked;
			this.dateTimePickerTo2.Enabled = this.checkBoxTo.Checked;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(StatisticAbsence));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkedComboTypeAbsence = new CheckedComboBox.CheckedCombo();
			this.checkedNumBoxNumberOrder = new CheckedNumBox.CheckedNumBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkBoxTo = new System.Windows.Forms.CheckBox();
			this.checkBoxFrom = new System.Windows.Forms.CheckBox();
			this.dateTimePickerTo2 = new System.Windows.Forms.DateTimePicker();
			this.dateTimePickerTo1 = new System.Windows.Forms.DateTimePicker();
			this.dateTimePickerFrom2 = new System.Windows.Forms.DateTimePicker();
			this.dateTimePickerFrom1 = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonFind = new System.Windows.Forms.Button();
			this.buttonExit = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkedComboTypeAbsence);
			this.groupBox1.Controls.Add(this.checkedNumBoxNumberOrder);
			this.groupBox1.Location = new System.Drawing.Point(8, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(456, 264);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Избор на условия";
			// 
			// checkedComboTypeAbsence
			// 
			this.checkedComboTypeAbsence.Checked = false;
			this.checkedComboTypeAbsence.Column = "absence.TypeAbsence";
			this.checkedComboTypeAbsence.Data = null;
			this.checkedComboTypeAbsence.Location = new System.Drawing.Point(8, 32);
			this.checkedComboTypeAbsence.Name = "checkedComboTypeAbsence";
			this.checkedComboTypeAbsence.Size = new System.Drawing.Size(432, 24);
			this.checkedComboTypeAbsence.TabIndex = 0;
			this.checkedComboTypeAbsence.TextCombo = "Вид на отсъствието";
			// 
			// checkedNumBoxNumberOrder
			// 
			this.checkedNumBoxNumberOrder.Checked = false;
			this.checkedNumBoxNumberOrder.Column = "absence.NumberOrder";
			this.checkedNumBoxNumberOrder.Data = null;
			this.checkedNumBoxNumberOrder.Location = new System.Drawing.Point(8, 56);
			this.checkedNumBoxNumberOrder.Name = "checkedNumBoxNumberOrder";
			this.checkedNumBoxNumberOrder.Size = new System.Drawing.Size(432, 24);
			this.checkedNumBoxNumberOrder.TabIndex = 1;
			this.checkedNumBoxNumberOrder.TextCombo = "Номер на заповед";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkBoxTo);
			this.groupBox2.Controls.Add(this.checkBoxFrom);
			this.groupBox2.Controls.Add(this.dateTimePickerTo2);
			this.groupBox2.Controls.Add(this.dateTimePickerTo1);
			this.groupBox2.Controls.Add(this.dateTimePickerFrom2);
			this.groupBox2.Controls.Add(this.dateTimePickerFrom1);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Location = new System.Drawing.Point(8, 280);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(408, 128);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Хронологичност";
			// 
			// checkBoxTo
			// 
			this.checkBoxTo.Enabled = false;
			this.checkBoxTo.Location = new System.Drawing.Point(128, 72);
			this.checkBoxTo.Name = "checkBoxTo";
			this.checkBoxTo.Size = new System.Drawing.Size(224, 24);
			this.checkBoxTo.TabIndex = 3;
			this.checkBoxTo.Text = "Отсъствието завършва в интеревала";
			this.checkBoxTo.CheckedChanged += new System.EventHandler(this.checkBoxTo_CheckedChanged);
			// 
			// checkBoxFrom
			// 
			this.checkBoxFrom.Location = new System.Drawing.Point(128, 8);
			this.checkBoxFrom.Name = "checkBoxFrom";
			this.checkBoxFrom.Size = new System.Drawing.Size(224, 24);
			this.checkBoxFrom.TabIndex = 0;
			this.checkBoxFrom.Text = "Отсъствието започва в интервала";
			this.checkBoxFrom.CheckedChanged += new System.EventHandler(this.checkBoxFrom_CheckedChanged);
			// 
			// dateTimePickerTo2
			// 
			this.dateTimePickerTo2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerTo2.Location = new System.Drawing.Point(208, 96);
			this.dateTimePickerTo2.Name = "dateTimePickerTo2";
			this.dateTimePickerTo2.Size = new System.Drawing.Size(168, 20);
			this.dateTimePickerTo2.TabIndex = 5;
			// 
			// dateTimePickerTo1
			// 
			this.dateTimePickerTo1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerTo1.Location = new System.Drawing.Point(8, 96);
			this.dateTimePickerTo1.Name = "dateTimePickerTo1";
			this.dateTimePickerTo1.Size = new System.Drawing.Size(160, 20);
			this.dateTimePickerTo1.TabIndex = 4;
			// 
			// dateTimePickerFrom2
			// 
			this.dateTimePickerFrom2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerFrom2.Location = new System.Drawing.Point(208, 48);
			this.dateTimePickerFrom2.Name = "dateTimePickerFrom2";
			this.dateTimePickerFrom2.Size = new System.Drawing.Size(168, 20);
			this.dateTimePickerFrom2.TabIndex = 2;
			// 
			// dateTimePickerFrom1
			// 
			this.dateTimePickerFrom1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerFrom1.Location = new System.Drawing.Point(8, 48);
			this.dateTimePickerFrom1.Name = "dateTimePickerFrom1";
			this.dateTimePickerFrom1.Size = new System.Drawing.Size(160, 20);
			this.dateTimePickerFrom1.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(216, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(144, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "До дата";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(144, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "От дата";
			// 
			// buttonFind
			// 
			this.buttonFind.Image = ((System.Drawing.Image)(resources.GetObject("buttonFind.Image")));
			this.buttonFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonFind.Location = new System.Drawing.Point(424, 296);
			this.buttonFind.Name = "buttonFind";
			this.buttonFind.TabIndex = 2;
			this.buttonFind.Text = "   Намери";
			this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
			// 
			// buttonExit
			// 
			this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonExit.Image = ((System.Drawing.Image)(resources.GetObject("buttonExit.Image")));
			this.buttonExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonExit.Location = new System.Drawing.Point(424, 328);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(80, 23);
			this.buttonExit.TabIndex = 3;
			this.buttonExit.Text = " Изход";
			// 
			// StatisticAbsence
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonExit;
			this.ClientSize = new System.Drawing.Size(512, 414);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonFind);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "StatisticAbsence";
			this.ShowInTaskbar = false;
			this.Text = "Справки по отсъствия";
			this.Load += new System.EventHandler(this.StatisticPersonal_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void StatisticPersonal_Load(object sender, System.EventArgs e)
		{

			string[] str = new string[]{"Болнични","Полагаем годишен отпуск","Неплатен отпуск","Отглеждане на дете","Командировка","Полагаем отпуск минали години"};
			foreach(string s in str)
			{
				this.checkedComboTypeAbsence.combobox.Items.Add( s );
			}
			
		
		}

		private void buttonFind_Click(object sender, System.EventArgs e)
		{
			arrColumn = new ArrayList();
			ArrayList arrValues = new ArrayList();
			DataLayer.DataStatistics stat = new DataLayer.DataStatistics( this.main.connString );
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
            string additional = "";
			string dat1 = mainForm.ConvertDateTimeToMySql(this.dateTimePickerFrom1.Value);
			
			string dat2 = mainForm.ConvertDateTimeToMySql(this.dateTimePickerFrom2.Value);

			if( this.checkBoxFrom.Checked )
			{
				if( arrColumn.Count == 0 )
				{
					additional = " WHERE ( absence.FromDate >= " + dat1 + " AND absence.FromDate <= " + dat2 + ") OR (absence.ToDate >= " + dat1 + " AND absence.ToDate <= " + dat2 +") OR (absence.FromDate <= " + dat1 + " AND absence.ToDate >= " + dat2 + ")";
					
				}
				else
				{
					additional = " AND (absence.FromDate >= " + dat1 + " AND absence.FromDate <= " + dat2 + ") OR (absence.ToDate >= " + dat1 + " AND absence.ToDate <= " + dat2 + ") OR ( absence.FromDate <= " + dat1 + " AND absence.ToDate >= " + dat2 + ")";
				}
			}
			string dat3 = mainForm.ConvertDateTimeToMySql(this.dateTimePickerTo1.Value);

			string dat4 = mainForm.ConvertDateTimeToMySql(this.dateTimePickerTo2.Value);

			if( this.checkBoxTo.Checked )
			{
					additional += " AND absence.ToDate BETWEEN " + dat3 + " AND " + dat4 ;
			}
			this.dt1 = stat.FindPersonByAbsence( "Absence", arrColumn, arrValues, additional) ;
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

		private void checkBoxFrom_CheckedChanged(object sender, System.EventArgs e)
		{
		    this.dateTimePickerFrom1.Enabled = checkBoxFrom.Checked;
            this.dateTimePickerFrom2.Enabled = checkBoxFrom.Checked;
		}

		private void checkBoxTo_CheckedChanged(object sender, System.EventArgs e)
		{
		    this.dateTimePickerTo1.Enabled = checkBoxTo.Checked;
			this.dateTimePickerTo2.Enabled = checkBoxTo.Checked;
		}

		private void buttonExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
