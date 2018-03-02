using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace HR
{
	/// <summary>
	/// Summary description for AddMinSalaryForm.
	/// </summary>
//	public class FormAddMinSalary : System.Windows.Forms.Form
//	{
//		FormMinSalary owner;
//		int code;
//		bool IsSalaryEdit;
//		#region Controls
//		private System.Windows.Forms.GroupBox groupBox1;
//		private System.Windows.Forms.Label label4;
//		private System.Windows.Forms.Label label3;
//		private System.Windows.Forms.Label label2;
//		private System.Windows.Forms.Label label1;
//		private System.Windows.Forms.Button button2;
//		#endregion
//		internal System.Windows.Forms.TextBox textBoxDWN;
//		internal System.Windows.Forms.TextBox textBoxPMS;
//		internal BugBox.NumBox numBoxSalary;
//		internal System.Windows.Forms.DateTimePicker dateTimePickerValidFrom;
//		private System.Windows.Forms.Button buttonSave;
//		/// <summary>
//		/// Required designer variable.
//		/// </summary>
//		private System.ComponentModel.Container components = null;
//
//		public FormAddMinSalary(FormMinSalary parent)
//		{
//			// Required for Windows Form Designer support
//			InitializeComponent();
//
//			this.owner = parent;
//			this.Text = "Добавяне на нова минимална работна заплата";
//			IsSalaryEdit = false;
//		}
//
//		public FormAddMinSalary(FormMinSalary parent, int Code)
//		{
//			// Required for Windows Form Designer support
//			InitializeComponent();
//			this.code = Code;
//			this.owner = parent;
//			this.Text = "Добавяне на нова минимална работна заплата";
//			IsSalaryEdit = true;
//		}
//
//		/// <summary>
//		/// Clean up any resources being used.
//		/// </summary>
//		protected override void Dispose( bool disposing )
//		{
//			if( disposing )
//			{
//				if(components != null)
//				{
//					components.Dispose();
//				}
//			}
//			base.Dispose( disposing );
//		}
//
//		#region Windows Form Designer generated code
//		/// <summary>
//		/// Required method for Designer support - do not modify
//		/// the contents of this method with the code editor.
//		/// </summary>
//		private void InitializeComponent()
//		{
//			this.groupBox1 = new System.Windows.Forms.GroupBox();
//			this.label4 = new System.Windows.Forms.Label();
//			this.label3 = new System.Windows.Forms.Label();
//			this.label2 = new System.Windows.Forms.Label();
//			this.label1 = new System.Windows.Forms.Label();
//			this.textBoxDWN = new System.Windows.Forms.TextBox();
//			this.textBoxPMS = new System.Windows.Forms.TextBox();
//			this.numBoxSalary = new BugBox.NumBox();
//			this.dateTimePickerValidFrom = new System.Windows.Forms.DateTimePicker();
//			this.buttonSave = new System.Windows.Forms.Button();
//			this.button2 = new System.Windows.Forms.Button();
//			this.groupBox1.SuspendLayout();
//			this.SuspendLayout();
//			// 
//			// groupBox1
//			// 
//			this.groupBox1.Controls.Add(this.label4);
//			this.groupBox1.Controls.Add(this.label3);
//			this.groupBox1.Controls.Add(this.label2);
//			this.groupBox1.Controls.Add(this.label1);
//			this.groupBox1.Controls.Add(this.textBoxDWN);
//			this.groupBox1.Controls.Add(this.textBoxPMS);
//			this.groupBox1.Controls.Add(this.numBoxSalary);
//			this.groupBox1.Controls.Add(this.dateTimePickerValidFrom);
//			this.groupBox1.Location = new System.Drawing.Point(8, 8);
//			this.groupBox1.Name = "groupBox1";
//			this.groupBox1.Size = new System.Drawing.Size(472, 168);
//			this.groupBox1.TabIndex = 8;
//			this.groupBox1.TabStop = false;
//			this.groupBox1.Text = "Zaplata";
//			// 
//			// label4
//			// 
//			this.label4.Location = new System.Drawing.Point(318, 46);
//			this.label4.Name = "label4";
//			this.label4.TabIndex = 15;
//			this.label4.Text = "Заплата";
//			// 
//			// label3
//			// 
//			this.label3.Location = new System.Drawing.Point(32, 40);
//			this.label3.Name = "label3";
//			this.label3.TabIndex = 14;
//			this.label3.Text = "Валидна от";
//			// 
//			// label2
//			// 
//			this.label2.Location = new System.Drawing.Point(312, 112);
//			this.label2.Name = "label2";
//			this.label2.TabIndex = 13;
//			this.label2.Text = "ДВН";
//			// 
//			// label1
//			// 
//			this.label1.Location = new System.Drawing.Point(32, 112);
//			this.label1.Name = "label1";
//			this.label1.TabIndex = 12;
//			this.label1.Text = "ПМС";
//			// 
//			// textBoxDWN
//			// 
//			this.textBoxDWN.Location = new System.Drawing.Point(312, 136);
//			this.textBoxDWN.Name = "textBoxDWN";
//			this.textBoxDWN.TabIndex = 11;
//			this.textBoxDWN.Text = "";
//			// 
//			// textBoxPMS
//			// 
//			this.textBoxPMS.Location = new System.Drawing.Point(32, 136);
//			this.textBoxPMS.Name = "textBoxPMS";
//			this.textBoxPMS.TabIndex = 10;
//			this.textBoxPMS.Text = "";
//			// 
//			// numBoxSalary
//			// 
//			this.numBoxSalary.Location = new System.Drawing.Point(310, 70);
//			this.numBoxSalary.Name = "numBoxSalary";
//			this.numBoxSalary.TabIndex = 9;
//			this.numBoxSalary.Text = "0";
//			// 
//			// dateTimePickerValidFrom
//			// 
//			this.dateTimePickerValidFrom.Location = new System.Drawing.Point(30, 70);
//			this.dateTimePickerValidFrom.Name = "dateTimePickerValidFrom";
//			this.dateTimePickerValidFrom.TabIndex = 8;
//			// 
//			// buttonSave
//			// 
//			this.buttonSave.Location = new System.Drawing.Point(112, 200);
//			this.buttonSave.Name = "buttonSave";
//			this.buttonSave.TabIndex = 9;
//			this.buttonSave.Text = "Добре";
//			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
//			// 
//			// button2
//			// 
//			this.button2.Location = new System.Drawing.Point(328, 200);
//			this.button2.Name = "button2";
//			this.button2.TabIndex = 10;
//			this.button2.Text = "Изход";
//			this.button2.Click += new System.EventHandler(this.buttonCancel_Click);
//			// 
//			// FormAddMinSalary
//			// 
//			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
//			this.ClientSize = new System.Drawing.Size(488, 237);
//			this.Controls.Add(this.button2);
//			this.Controls.Add(this.buttonSave);
//			this.Controls.Add(this.groupBox1);
//			this.Name = "FormAddMinSalary";
//			this.Text = "AddMinSalaryForm";
//			this.groupBox1.ResumeLayout(false);
//			this.ResumeLayout(false);
//
//		}
//		#endregion
//
//		private void AddSalaryPackageToTable( DataLayer.SalaryPackage package)
//		{
//			DataRow row = this.owner.dt.NewRow();
//			row["id"] = package.Code;
//			row["dw"] = package.DW;
//			row["minpermonth"] = package.MinPerMonth;
//			row["pms"] = package.PMS;
//			row["valid_from"] = package.ValidFrom;
//			
//			this.owner.dt.Rows.Add( row );
//		}
//
//		private void buttonSave_Click(object sender, System.EventArgs e)
//		{
//			DataLayer.SalaryPackage package = new DataLayer.SalaryPackage();
//			this.ValidateSalaryData( package );
//
//			if( !IsSalaryEdit )
//			{
//				// Towa e pri dobawqne na now red
//				this.owner.salaryAction.InsertSalary( package );
//				package.Code = this.owner.salaryAction.GetLastId();
//				this.AddSalaryPackageToTable( package );				
//			}
//			else
//			{
//				package.Code = this.code;
//				DataRow row = this.owner.dt.Rows.Find( code );
//				if( row != null )
//				{
//					row["valid_from"] = package.ValidFrom;
//					row["PMS"] = package.PMS;
//					row["dw"] = package.DW;
//					row["minpermonth"] = package.MinPerMonth;
//					this.owner.salaryAction.UpdateSalary( package );				
//				}				
//			}				
//		}
//
//		private void ValidateSalaryData( DataLayer.SalaryPackage package )
//		{
//			package.DW = this.textBoxDWN.Text;
//			if(this.numBoxSalary.Text == "")
//			{
//				package.MinPerMonth = 0;
//			}
//			else
//			{
//				package.MinPerMonth = float.Parse(this.numBoxSalary.Text);
//			}			
//			package.PMS = this.textBoxPMS.Text;
//			package.ValidFrom = this.dateTimePickerValidFrom.Value;			
//		}
//
//		private void buttonCancel_Click(object sender, System.EventArgs e)
//		{
//			this.Close();
//		}
//	}
}
