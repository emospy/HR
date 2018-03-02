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
	public class StatisticPenalty : System.Windows.Forms.Form
	{
		mainForm main;
		internal ArrayList arrColumn;
		internal ArrayList arrColumnView;
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
		private System.Windows.Forms.CheckBox checkBoxPenaltyDate;
		private System.Windows.Forms.DateTimePicker dateTimePickerPenaltyDate2;
		private System.Windows.Forms.DateTimePicker dateTimePickerPenaltyDate1;
		private System.Windows.Forms.CheckBox checkBoxFormDate;
		private System.Windows.Forms.DateTimePicker dateTimePickerFormDate2;
		private System.Windows.Forms.DateTimePicker dateTimePickerFormDate1;
		private CheckedComboBox.CheckedCombo checkedComboReason;
		private CheckedComboBox.CheckedCombo checkedComboTypeReason;
		private System.Windows.Forms.Button buttonExit;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public StatisticPenalty( mainForm main, bool IsTotalStat)
		{
            this.main = main;
			this.IsTotalStat = IsTotalStat;
			InitializeComponent();
			this.dateTimePickerFormDate1.Enabled = this.checkBoxFormDate.Checked;
			this.dateTimePickerFormDate2.Enabled = this.checkBoxFormDate.Checked;
			this.dateTimePickerPenaltyDate1.Enabled = this.checkBoxPenaltyDate.Checked;
			this.dateTimePickerPenaltyDate2.Enabled = this.checkBoxPenaltyDate.Checked;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(StatisticPenalty));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkedComboTypeReason = new CheckedComboBox.CheckedCombo();
			this.checkedComboReason = new CheckedComboBox.CheckedCombo();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.checkBoxFormDate = new System.Windows.Forms.CheckBox();
			this.checkBoxPenaltyDate = new System.Windows.Forms.CheckBox();
			this.dateTimePickerFormDate2 = new System.Windows.Forms.DateTimePicker();
			this.dateTimePickerFormDate1 = new System.Windows.Forms.DateTimePicker();
			this.dateTimePickerPenaltyDate2 = new System.Windows.Forms.DateTimePicker();
			this.dateTimePickerPenaltyDate1 = new System.Windows.Forms.DateTimePicker();
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
			this.groupBox1.Controls.Add(this.checkedComboTypeReason);
			this.groupBox1.Controls.Add(this.checkedComboReason);
			this.groupBox1.Location = new System.Drawing.Point(8, 16);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(456, 184);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Избор на условия";
			// 
			// checkedComboTypeReason
			// 
			this.checkedComboTypeReason.Checked = false;
			this.checkedComboTypeReason.Column = "penalty.typePenalty";
			this.checkedComboTypeReason.Data = null;
			this.checkedComboTypeReason.Location = new System.Drawing.Point(8, 24);
			this.checkedComboTypeReason.Name = "checkedComboTypeReason";
			this.checkedComboTypeReason.Size = new System.Drawing.Size(432, 24);
			this.checkedComboTypeReason.TabIndex = 0;
			this.checkedComboTypeReason.TextCombo = "Вид наказание";
			// 
			// checkedComboReason
			// 
			this.checkedComboReason.Checked = false;
			this.checkedComboReason.Column = "penalty.reason";
			this.checkedComboReason.Data = null;
			this.checkedComboReason.Location = new System.Drawing.Point(8, 56);
			this.checkedComboReason.Name = "checkedComboReason";
			this.checkedComboReason.Size = new System.Drawing.Size(432, 24);
			this.checkedComboReason.TabIndex = 1;
			this.checkedComboReason.TextCombo = "Основание";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.checkBoxFormDate);
			this.groupBox2.Controls.Add(this.checkBoxPenaltyDate);
			this.groupBox2.Controls.Add(this.dateTimePickerFormDate2);
			this.groupBox2.Controls.Add(this.dateTimePickerFormDate1);
			this.groupBox2.Controls.Add(this.dateTimePickerPenaltyDate2);
			this.groupBox2.Controls.Add(this.dateTimePickerPenaltyDate1);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Location = new System.Drawing.Point(16, 208);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(408, 128);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Хронологичност";
			// 
			// checkBoxFormDate
			// 
			this.checkBoxFormDate.Enabled = false;
			this.checkBoxFormDate.Location = new System.Drawing.Point(128, 72);
			this.checkBoxFormDate.Name = "checkBoxFormDate";
			this.checkBoxFormDate.Size = new System.Drawing.Size(224, 24);
			this.checkBoxFormDate.TabIndex = 3;
			this.checkBoxFormDate.Text = "Наказанието започва в интервала";
			this.checkBoxFormDate.Visible = false;
			this.checkBoxFormDate.CheckedChanged += new System.EventHandler(this.checkBoxTo_CheckedChanged);
			// 
			// checkBoxPenaltyDate
			// 
			this.checkBoxPenaltyDate.Location = new System.Drawing.Point(128, 8);
			this.checkBoxPenaltyDate.Name = "checkBoxPenaltyDate";
			this.checkBoxPenaltyDate.Size = new System.Drawing.Size(144, 24);
			this.checkBoxPenaltyDate.TabIndex = 0;
			this.checkBoxPenaltyDate.Text = "Времеви интервал";
			this.checkBoxPenaltyDate.CheckedChanged += new System.EventHandler(this.checkBoxFrom_CheckedChanged);
			// 
			// dateTimePickerFormDate2
			// 
			this.dateTimePickerFormDate2.Enabled = false;
			this.dateTimePickerFormDate2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerFormDate2.Location = new System.Drawing.Point(208, 96);
			this.dateTimePickerFormDate2.Name = "dateTimePickerFormDate2";
			this.dateTimePickerFormDate2.Size = new System.Drawing.Size(168, 20);
			this.dateTimePickerFormDate2.TabIndex = 5;
			this.dateTimePickerFormDate2.Visible = false;
			// 
			// dateTimePickerFormDate1
			// 
			this.dateTimePickerFormDate1.Enabled = false;
			this.dateTimePickerFormDate1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerFormDate1.Location = new System.Drawing.Point(8, 96);
			this.dateTimePickerFormDate1.Name = "dateTimePickerFormDate1";
			this.dateTimePickerFormDate1.Size = new System.Drawing.Size(160, 20);
			this.dateTimePickerFormDate1.TabIndex = 4;
			this.dateTimePickerFormDate1.Visible = false;
			// 
			// dateTimePickerPenaltyDate2
			// 
			this.dateTimePickerPenaltyDate2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerPenaltyDate2.Location = new System.Drawing.Point(208, 48);
			this.dateTimePickerPenaltyDate2.Name = "dateTimePickerPenaltyDate2";
			this.dateTimePickerPenaltyDate2.Size = new System.Drawing.Size(168, 20);
			this.dateTimePickerPenaltyDate2.TabIndex = 2;
			// 
			// dateTimePickerPenaltyDate1
			// 
			this.dateTimePickerPenaltyDate1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dateTimePickerPenaltyDate1.Location = new System.Drawing.Point(8, 48);
			this.dateTimePickerPenaltyDate1.Name = "dateTimePickerPenaltyDate1";
			this.dateTimePickerPenaltyDate1.Size = new System.Drawing.Size(160, 20);
			this.dateTimePickerPenaltyDate1.TabIndex = 1;
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
			this.buttonFind.Location = new System.Drawing.Point(432, 272);
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
			this.buttonExit.Location = new System.Drawing.Point(432, 304);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(80, 23);
			this.buttonExit.TabIndex = 3;
			this.buttonExit.Text = " Изход";
			// 
			// StatisticPenalty
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonExit;
			this.ClientSize = new System.Drawing.Size(512, 366);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonFind);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "StatisticPenalty";
			this.ShowInTaskbar = false;
			this.Text = "Справки по наказания";
			this.Load += new System.EventHandler(this.StatisticPenalty_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		private void buttonFind_Click(object sender, System.EventArgs e)
		{
			arrColumn = new ArrayList();
			arrColumnView = new ArrayList();
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
			string dat1 = mainForm.ConvertDateTimeToMySql( dateTimePickerPenaltyDate1.Value );
			string dat2 = mainForm.ConvertDateTimeToMySql( dateTimePickerPenaltyDate2.Value );
			if( this.checkBoxPenaltyDate.Checked )
			{
				if( arrColumn.Count == 0 )
				{
					additional = " WHERE ( penalty.FromDate >= " + dat1 + " AND penalty.FromDate <= " + dat2 + ") OR (penalty.ToDate >= " + dat1 + " AND penalty.ToDate <= " + dat2 + ") OR (penalty.FromDate <= " + dat1 + "  AND penalty.ToDate >= " + dat2 + ")";
					
				}
				else
				{
					additional = " AND (penalty.FromDate >= " + dat1 + " AND penalty.FromDate <= " + dat2 + ") OR (penalty.ToDate >= " + dat1 + " AND penalty.ToDate <= " + dat2 + ") OR (penalty.FromDate <= " + dat1 + " AND penalty.ToDate >= " + dat2 + ")";
				}
				arrColumnView.Add( "penalty.FromDate" );
				arrColumnView.Add( "penalty.ToDate" );
			}
			//For now this option is not necessery
			//
//			string dat3 = this.dateTimePickerFormDate1.Value.Year + @"-" + 
//				this.dateTimePickerFormDate1.Value.Month + @"-" +
//				this.dateTimePickerFormDate1.Value.Day + " " ;
////				this.dateTimePickerFormDate1.Value.Hour.ToString()+
////				":" + this.dateTimePickerFormDate1.Value.Minute.ToString() +
////				":" + this.dateTimePickerFormDate1.Value.Second.ToString();
//			string dat4 = this.dateTimePickerFormDate2.Value.Year + @"-" + 
//				this.dateTimePickerFormDate2.Value.Month + @"-" +
//				this.dateTimePickerFormDate2.Value.Day + " " ;
////				this.dateTimePickerFormDate2.Value.Hour.ToString()+
////				":" + this.dateTimePickerFormDate2.Value.Minute.ToString() +
////				":" + this.dateTimePickerFormDate2.Value.Second.ToString();
//
//			if(  this.checkBoxFormDate.Checked)
//			{
//				if( arrColumn.Count == 0 & additional == "")
//				{
//					additional += " WHERE FromDate BETWEEN '" + dat3 + "' AND '" + dat4 + "' ";
//				}
//				else
//				{
//                    additional += " AND FromDate BETWEEN '" + dat3 + "' AND '" + dat4 + "' ";
//				}
//				//arrColumn.Add( "FromDate" );
//			}
			this.dt1 = stat.FindPersonByPenalty( "Penalty", arrColumn, arrValues, arrColumnView, additional) ;
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
			this.dateTimePickerPenaltyDate1.Enabled = this.checkBoxPenaltyDate.Checked;
			this.dateTimePickerPenaltyDate2.Enabled = this.checkBoxPenaltyDate.Checked;
		}

		private void checkBoxTo_CheckedChanged(object sender, System.EventArgs e)
		{
			this.dateTimePickerFormDate1.Enabled = this.checkBoxFormDate.Checked;
			this.dateTimePickerFormDate2.Enabled = this.checkBoxFormDate.Checked;
		}

		private void StatisticPenalty_Load(object sender, System.EventArgs e)
		{
		     this.checkedComboReason.combobox.DataSource = this.main.nomenclaatureData.arrPenaltyReason;
			  this.checkedComboTypeReason.combobox.DataSource = this.main.nomenclaatureData.arrTypePenalty;
		}

		private void buttonExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
