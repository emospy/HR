using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace HR
{
	/// <summary>
	/// Summary description for ExpCalculator.
	/// </summary>
	public class ExpCalculator : System.Windows.Forms.Form
	{
		/// <summary>
		/// Public member for setting startup values in from numboxes
		/// </summary>
		public Experience StartExp;
		/// <summary>
		/// Public member for receiving results of calcualtions
		/// </summary>
		public Experience EndExp;
		#region Items
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label39;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private BugBox.NumBox numBoxDayFrom;
		private BugBox.NumBox numBoxMonthFrom;
		private BugBox.NumBox numBoxYearFrom;
		private BugBox.NumBox numBoxYearTo;
		private BugBox.NumBox numBoxDayTo;
		private BugBox.NumBox numBoxMonthTo;
		private System.Windows.Forms.Button buttonCalc;
		private System.Windows.Forms.ComboBox comboBoxFrom;
		private System.Windows.Forms.ComboBox comboBoxTo;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOk;
		#endregion
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// A form for visualizing ecperience calculator transformer
		/// </summary>
		public ExpCalculator()
		{			
			InitializeComponent();
			this.StartExp = new Experience();
			this.EndExp = new Experience();
		}

		/// <summary>
		/// A form for visualizing ecperience calculator transformer with startup values
		/// </summary>
		/// <param name="StartYears"></param>
		/// <param name="StartMonths"></param>
		/// <param name="StartDays"></param>
		public ExpCalculator(int StartYears, int StartMonths, int StartDays)
		{
			InitializeComponent();
			this.StartExp = new Experience(StartYears, StartMonths, StartDays);
			this.EndExp = new Experience();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ExpCalculator));
			this.label1 = new System.Windows.Forms.Label();
			this.comboBoxFrom = new System.Windows.Forms.ComboBox();
			this.label39 = new System.Windows.Forms.Label();
			this.numBoxDayFrom = new BugBox.NumBox();
			this.numBoxMonthFrom = new BugBox.NumBox();
			this.numBoxYearFrom = new BugBox.NumBox();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBoxTo = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.numBoxYearTo = new BugBox.NumBox();
			this.numBoxDayTo = new BugBox.NumBox();
			this.numBoxMonthTo = new BugBox.NumBox();
			this.buttonCalc = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 32);
			this.label1.TabIndex = 0;
			this.label1.Text = "Преобразуване от категория :";
			// 
			// comboBoxFrom
			// 
			this.comboBoxFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.comboBoxFrom.Items.AddRange(new object[] {
															  "  I",
															  "  II",
															  "  III"});
			this.comboBoxFrom.Location = new System.Drawing.Point(112, 24);
			this.comboBoxFrom.Name = "comboBoxFrom";
			this.comboBoxFrom.Size = new System.Drawing.Size(48, 21);
			this.comboBoxFrom.TabIndex = 1;
			// 
			// label39
			// 
			this.label39.Location = new System.Drawing.Point(184, 8);
			this.label39.Name = "label39";
			this.label39.Size = new System.Drawing.Size(120, 16);
			this.label39.TabIndex = 56;
			this.label39.Text = "  ГГ        ММ        ДД";
			// 
			// numBoxDayFrom
			// 
			this.numBoxDayFrom.Location = new System.Drawing.Point(264, 24);
			this.numBoxDayFrom.MaxLength = 2;
			this.numBoxDayFrom.Name = "numBoxDayFrom";
			this.numBoxDayFrom.Size = new System.Drawing.Size(36, 20);
			this.numBoxDayFrom.TabIndex = 55;
			this.numBoxDayFrom.Text = "0";
			// 
			// numBoxMonthFrom
			// 
			this.numBoxMonthFrom.Location = new System.Drawing.Point(224, 24);
			this.numBoxMonthFrom.MaxLength = 2;
			this.numBoxMonthFrom.Name = "numBoxMonthFrom";
			this.numBoxMonthFrom.Size = new System.Drawing.Size(36, 20);
			this.numBoxMonthFrom.TabIndex = 54;
			this.numBoxMonthFrom.Text = "0";
			// 
			// numBoxYearFrom
			// 
			this.numBoxYearFrom.Location = new System.Drawing.Point(184, 24);
			this.numBoxYearFrom.MaxLength = 3;
			this.numBoxYearFrom.Name = "numBoxYearFrom";
			this.numBoxYearFrom.Size = new System.Drawing.Size(36, 20);
			this.numBoxYearFrom.TabIndex = 53;
			this.numBoxYearFrom.Text = "0";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(184, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 16);
			this.label2.TabIndex = 56;
			this.label2.Text = "  ГГ        ММ        ДД";
			// 
			// comboBoxTo
			// 
			this.comboBoxTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.comboBoxTo.Items.AddRange(new object[] {
															"  I",
															"  II",
															"  III"});
			this.comboBoxTo.Location = new System.Drawing.Point(112, 80);
			this.comboBoxTo.Name = "comboBoxTo";
			this.comboBoxTo.Size = new System.Drawing.Size(48, 21);
			this.comboBoxTo.TabIndex = 61;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 32);
			this.label3.TabIndex = 60;
			this.label3.Text = "Преобразуване в категория :";
			// 
			// numBoxYearTo
			// 
			this.numBoxYearTo.Location = new System.Drawing.Point(184, 80);
			this.numBoxYearTo.MaxLength = 3;
			this.numBoxYearTo.Name = "numBoxYearTo";
			this.numBoxYearTo.ReadOnly = true;
			this.numBoxYearTo.Size = new System.Drawing.Size(36, 20);
			this.numBoxYearTo.TabIndex = 53;
			this.numBoxYearTo.Text = "0";
			// 
			// numBoxDayTo
			// 
			this.numBoxDayTo.Location = new System.Drawing.Point(264, 80);
			this.numBoxDayTo.MaxLength = 2;
			this.numBoxDayTo.Name = "numBoxDayTo";
			this.numBoxDayTo.ReadOnly = true;
			this.numBoxDayTo.Size = new System.Drawing.Size(36, 20);
			this.numBoxDayTo.TabIndex = 55;
			this.numBoxDayTo.Text = "0";
			// 
			// numBoxMonthTo
			// 
			this.numBoxMonthTo.Location = new System.Drawing.Point(224, 80);
			this.numBoxMonthTo.MaxLength = 2;
			this.numBoxMonthTo.Name = "numBoxMonthTo";
			this.numBoxMonthTo.ReadOnly = true;
			this.numBoxMonthTo.Size = new System.Drawing.Size(36, 20);
			this.numBoxMonthTo.TabIndex = 54;
			this.numBoxMonthTo.Text = "0";
			// 
			// buttonCalc
			// 
			this.buttonCalc.Image = ((System.Drawing.Image)(resources.GetObject("buttonCalc.Image")));
			this.buttonCalc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonCalc.Location = new System.Drawing.Point(216, 120);
			this.buttonCalc.Name = "buttonCalc";
			this.buttonCalc.Size = new System.Drawing.Size(88, 23);
			this.buttonCalc.TabIndex = 62;
			this.buttonCalc.Text = "   Изчисли";
			this.buttonCalc.Click += new System.EventHandler(this.buttonCalc_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.Image = ((System.Drawing.Image)(resources.GetObject("buttonCancel.Image")));
			this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonCancel.Location = new System.Drawing.Point(112, 120);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(88, 23);
			this.buttonCancel.TabIndex = 62;
			this.buttonCancel.Text = "   Изход";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonOk
			// 
			this.buttonOk.Image = ((System.Drawing.Image)(resources.GetObject("buttonOk.Image")));
			this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonOk.Location = new System.Drawing.Point(8, 120);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new System.Drawing.Size(88, 23);
			this.buttonOk.TabIndex = 62;
			this.buttonOk.Text = "    Използвай";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// ExpCalculator
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(312, 157);
			this.Controls.Add(this.buttonCalc);
			this.Controls.Add(this.comboBoxTo);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label39);
			this.Controls.Add(this.numBoxDayFrom);
			this.Controls.Add(this.numBoxMonthFrom);
			this.Controls.Add(this.numBoxYearFrom);
			this.Controls.Add(this.comboBoxFrom);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.numBoxYearTo);
			this.Controls.Add(this.numBoxDayTo);
			this.Controls.Add(this.numBoxMonthTo);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOk);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ExpCalculator";
			this.Text = "Клакулатор на трудов стаж";
			this.Load += new System.EventHandler(this.ExpCalculator_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonCalc_Click(object sender, System.EventArgs e)
		{
			if(this.comboBoxFrom.SelectedIndex < 0 || this.comboBoxTo.SelectedIndex < 0)
			{
				MessageBox.Show("Не сте избрали категории за преобразуване.");
				return;
			}
			
			int nominator, denominator;
			int y, m, d;
			nominator = 3 + this.comboBoxTo.SelectedIndex;
			denominator = 3 + this.comboBoxFrom.SelectedIndex;

			try
			{
				y = int.Parse( this.numBoxYearFrom.Text);
			}
			catch(System.FormatException)
			{
				y = 0;
			}
			try
			{
				m = int.Parse( this.numBoxMonthFrom.Text);
			}
			catch(System.FormatException)
			{
				m = 0;

			}
			try
			{
				d = int.Parse( this.numBoxDayFrom.Text);
			}
			catch(System.FormatException)
			{
				d = 0;
			}			
			this.StartExp = new Experience(y, m, d);

			this.EndExp = this.StartExp.ConvertToCategory(nominator, denominator);
            
			this.numBoxDayTo.Text = this.EndExp.Days.ToString();
			this.numBoxMonthTo.Text = this.EndExp.Months.ToString();
			this.numBoxYearTo.Text = this.EndExp.Years.ToString();
		}

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void ExpCalculator_Load(object sender, System.EventArgs e)
		{
			this.numBoxDayFrom.Text = this.StartExp.Days.ToString();
			this.numBoxDayTo.Text = this.EndExp.Days.ToString();
			this.numBoxMonthFrom.Text = this.StartExp.Months.ToString();
			this.numBoxMonthTo.Text = this.EndExp.Months.ToString();
			this.numBoxYearFrom.Text = this.StartExp.Years.ToString();
			this.numBoxYearTo.Text = this.EndExp.Years.ToString();
		}
	}
}