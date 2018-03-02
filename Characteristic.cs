using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace HR
{
	/// <summary>
	/// Summary description for Characteristic.
	/// </summary>
	public class formCharacteristicAdd : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBoxBasicDuties;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button Cancel;
		private System.Windows.Forms.TextBox textBoxBasicResponsibilities;
		private System.Windows.Forms.TextBox textBoxConnections;
		private System.Windows.Forms.TextBox textBoxCompetence;
		private System.Windows.Forms.TextBox textBoxRequirements;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textBoxNKPClass;
		private System.Windows.Forms.TextBox textBoxNKPCode;
		private mainForm main;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;


		/// <summary>
		/// Characteristic Constructor
		/// </summary>
		public formCharacteristicAdd(mainForm mf)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.main = mf;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formCharacteristicAdd));
			this.textBoxBasicDuties = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxBasicResponsibilities = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxConnections = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBoxCompetence = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxRequirements = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.Cancel = new System.Windows.Forms.Button();
			this.textBoxNKPClass = new System.Windows.Forms.TextBox();
			this.textBoxNKPCode = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textBoxBasicDuties
			// 
			this.textBoxBasicDuties.Location = new System.Drawing.Point(5, 88);
			this.textBoxBasicDuties.MaxLength = 64000;
			this.textBoxBasicDuties.Multiline = true;
			this.textBoxBasicDuties.Name = "textBoxBasicDuties";
			this.textBoxBasicDuties.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxBasicDuties.Size = new System.Drawing.Size(967, 87);
			this.textBoxBasicDuties.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(5, 64);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(967, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "I. ОСНОВНИ ДЛЪЖНОСТНИ ЗАДЪЛЖЕНИЯ:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(5, 178);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(416, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "II. ОСНОВНИ ОТГОВОРНОСТИ, ПРИСЪЩИ ЗА ДЛЪЖНОСТТА:";
			// 
			// textBoxBasicResponsibilities
			// 
			this.textBoxBasicResponsibilities.Location = new System.Drawing.Point(5, 197);
			this.textBoxBasicResponsibilities.Multiline = true;
			this.textBoxBasicResponsibilities.Name = "textBoxBasicResponsibilities";
			this.textBoxBasicResponsibilities.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxBasicResponsibilities.Size = new System.Drawing.Size(967, 87);
			this.textBoxBasicResponsibilities.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(2, 287);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(416, 16);
			this.label3.TabIndex = 4;
			this.label3.Text = "III. ОРГАНИЗАЦИОННИ ВРЪЗКИ И ВЗАИМООТНОШЕНИЯ:";
			// 
			// textBoxConnections
			// 
			this.textBoxConnections.Location = new System.Drawing.Point(5, 306);
			this.textBoxConnections.Multiline = true;
			this.textBoxConnections.Name = "textBoxConnections";
			this.textBoxConnections.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxConnections.Size = new System.Drawing.Size(967, 87);
			this.textBoxConnections.TabIndex = 3;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(2, 396);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(466, 16);
			this.label4.TabIndex = 6;
			this.label4.Text = "IV. НЕОБХОДИМА КОМПЕТЕНТНОСТ ЗА ИЗПЪЛНЕНИЕ НА ДЛЪЖНОСТТА:";
			// 
			// textBoxCompetence
			// 
			this.textBoxCompetence.Location = new System.Drawing.Point(5, 415);
			this.textBoxCompetence.Multiline = true;
			this.textBoxCompetence.Name = "textBoxCompetence";
			this.textBoxCompetence.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxCompetence.Size = new System.Drawing.Size(967, 87);
			this.textBoxCompetence.TabIndex = 5;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(5, 505);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(482, 16);
			this.label5.TabIndex = 8;
			this.label5.Text = "V. ИЗИСКВАНИЯ ЗА ЗАЕМАНЕ НА ДЛЪЖНОСТТА:";
			// 
			// textBoxRequirements
			// 
			this.textBoxRequirements.Location = new System.Drawing.Point(6, 524);
			this.textBoxRequirements.Multiline = true;
			this.textBoxRequirements.Name = "textBoxRequirements";
			this.textBoxRequirements.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxRequirements.Size = new System.Drawing.Size(966, 87);
			this.textBoxRequirements.TabIndex = 7;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(6, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(416, 16);
			this.label6.TabIndex = 9;
			this.label6.Text = "Клас по НКПД:";
			// 
			// button1
			// 
			this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
			this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button1.Location = new System.Drawing.Point(319, 627);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(130, 23);
			this.button1.TabIndex = 11;
			this.button1.Text = "   Запис";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Cancel
			// 
			this.Cancel.Image = ((System.Drawing.Image)(resources.GetObject("Cancel.Image")));
			this.Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.Cancel.Location = new System.Drawing.Point(535, 627);
			this.Cancel.Name = "Cancel";
			this.Cancel.Size = new System.Drawing.Size(130, 23);
			this.Cancel.TabIndex = 12;
			this.Cancel.Text = "   Отказ";
			this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
			// 
			// textBoxNKPClass
			// 
			this.textBoxNKPClass.Location = new System.Drawing.Point(6, 24);
			this.textBoxNKPClass.Name = "textBoxNKPClass";
			this.textBoxNKPClass.ReadOnly = true;
			this.textBoxNKPClass.Size = new System.Drawing.Size(800, 20);
			this.textBoxNKPClass.TabIndex = 13;
			// 
			// textBoxNKPCode
			// 
			this.textBoxNKPCode.Location = new System.Drawing.Point(812, 24);
			this.textBoxNKPCode.Name = "textBoxNKPCode";
			this.textBoxNKPCode.ReadOnly = true;
			this.textBoxNKPCode.Size = new System.Drawing.Size(160, 20);
			this.textBoxNKPCode.TabIndex = 14;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(812, 8);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 16);
			this.label7.TabIndex = 15;
			this.label7.Text = "Код по НКПД";
			// 
			// formCharacteristicAdd
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(984, 662);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textBoxNKPCode);
			this.Controls.Add(this.textBoxNKPClass);
			this.Controls.Add(this.Cancel);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.textBoxRequirements);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.textBoxCompetence);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBoxBasicResponsibilities);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxBasicDuties);
			this.Controls.Add(this.textBoxConnections);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "formCharacteristicAdd";
			this.ShowInTaskbar = false;
			this.Text = "Длъжностна характеристика на длъжността ";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void Cancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}		

		/// <summary>
		/// Getting the data for each one from controls from the datarow form parent form		
		/// </summary>
		public void SetControlData(DataRow row)
		{
			int i;
			try
			{
				if (row["NKPCode"].ToString().Length > 2)
				{
					bool res = int.TryParse(row["NKPCode"].ToString().Substring(0, 1), out i);
					if (res == true && i > 0 && i < 11)
					{
						this.textBoxNKPClass.Text = this.main.nomenclaatureData.arrNKPClass[i].ToString();
					}
				}
			}
			catch (System.FormatException ex)
			{
				MessageBox.Show(ex.Message);
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			//this.textBoxNKPClass.Text = row["NKPClass"].ToString();

			//To add code recognizing the NKPClass here
			//
			this.textBoxNKPCode.Text = row["NKPCode"].ToString();
			this.textBoxBasicDuties.Text = row["BasicDuties"].ToString();
			this.textBoxBasicResponsibilities.Text = row["BasicResponsibilities"].ToString();
			this.textBoxCompetence.Text = row["Competence"].ToString();
			this.textBoxConnections.Text = row["Connections"].ToString();
			this.textBoxRequirements.Text = row["Requirements"].ToString();
		}

		/// <summary>
		///Retrieving the text from controls for the parent form
		/// </summary>
		public void GetControlData(DataRow row)
		{
			row["NKPClass"] = this.textBoxNKPClass.Text;
			row["BasicDuties"] = this.textBoxBasicDuties.Text;
			row["BasicResponsibilities"] = this.textBoxBasicResponsibilities.Text;
			row["Competence"] = this.textBoxCompetence.Text;
			row["Connections"]= this.textBoxConnections.Text;
			row["Requirements"] = this.textBoxRequirements.Text;
		}
	}
}
