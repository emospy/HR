using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace LichenSystaw2004
{
	/// <summary>
	/// Summary description for Statistic.
	/// </summary>
	public class formStatistic : System.Windows.Forms.Form
	{
		#region Items

		#endregion

		DataTable dtPersonal = new DataTable();
		DataTable dtAssignment = new DataTable();
		DataTable dtAbsence = new DataTable();
		DataTable dtPenalty = new DataTable();
		bool IsFiredd;
		mainForm main;
		private System.Windows.Forms.GroupBox groupBoxPersonal;
		private System.Windows.Forms.GroupBox groupBoxAssignment;
		private System.Windows.Forms.GroupBox groupBoxAbsence;
		private System.Windows.Forms.GroupBox groupBoxPenalty;
		private System.Windows.Forms.CheckBox checkBoxPersonal;
		private System.Windows.Forms.CheckBox checkBoxAssignment;
		private System.Windows.Forms.CheckBox checkBoxAbsence;
		private System.Windows.Forms.CheckBox checkBoxPenalty;
		private System.Windows.Forms.Button buttonPersonal;
		private System.Windows.Forms.Button buttonAssignment;
		private System.Windows.Forms.Button buttonAbsence;
		private System.Windows.Forms.Button buttonPenalty;
		private System.Windows.Forms.Button buttonFind;
		private System.ComponentModel.Container components = null;
		StatisticPersonal formPersonal;
		StatisticAssignment formAssignment;
		StatisticAbsence formAbsence;
		StatisticPenalty formPenalty;
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.CheckBox checkBoxExportToExcel;
		bool IsRunFromKartoteka = false;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public formStatistic(mainForm main, bool IsRunFromKartoteka, bool IsFiredd )
		{
			this.main = main;
			this.IsFiredd = IsFiredd;
			this.IsRunFromKartoteka = IsRunFromKartoteka;
			InitializeComponent();
			formPersonal = new StatisticPersonal( this.main, true, IsFiredd );
			formAssignment = new StatisticAssignment( this.main, true, IsFiredd);
			formAbsence = new StatisticAbsence( this.main, true);
			formPenalty = new StatisticPenalty( this.main, true );
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(formStatistic));
			this.buttonFind = new System.Windows.Forms.Button();
			this.groupBoxPersonal = new System.Windows.Forms.GroupBox();
			this.buttonPersonal = new System.Windows.Forms.Button();
			this.checkBoxPersonal = new System.Windows.Forms.CheckBox();
			this.groupBoxAssignment = new System.Windows.Forms.GroupBox();
			this.buttonAssignment = new System.Windows.Forms.Button();
			this.checkBoxAssignment = new System.Windows.Forms.CheckBox();
			this.groupBoxAbsence = new System.Windows.Forms.GroupBox();
			this.buttonAbsence = new System.Windows.Forms.Button();
			this.checkBoxAbsence = new System.Windows.Forms.CheckBox();
			this.groupBoxPenalty = new System.Windows.Forms.GroupBox();
			this.buttonPenalty = new System.Windows.Forms.Button();
			this.checkBoxPenalty = new System.Windows.Forms.CheckBox();
			this.buttonExit = new System.Windows.Forms.Button();
			this.checkBoxExportToExcel = new System.Windows.Forms.CheckBox();
			this.groupBoxPersonal.SuspendLayout();
			this.groupBoxAssignment.SuspendLayout();
			this.groupBoxAbsence.SuspendLayout();
			this.groupBoxPenalty.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonFind
			// 
			this.buttonFind.Image = ((System.Drawing.Image)(resources.GetObject("buttonFind.Image")));
			this.buttonFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonFind.Location = new System.Drawing.Point(232, 232);
			this.buttonFind.Name = "buttonFind";
			this.buttonFind.Size = new System.Drawing.Size(96, 40);
			this.buttonFind.TabIndex = 5;
			this.buttonFind.Text = "Намери";
			this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
			// 
			// groupBoxPersonal
			// 
			this.groupBoxPersonal.Controls.Add(this.buttonPersonal);
			this.groupBoxPersonal.Controls.Add(this.checkBoxPersonal);
			this.groupBoxPersonal.Location = new System.Drawing.Point(8, 8);
			this.groupBoxPersonal.Name = "groupBoxPersonal";
			this.groupBoxPersonal.TabIndex = 0;
			this.groupBoxPersonal.TabStop = false;
			this.groupBoxPersonal.Text = "Справка според лични данни";
			// 
			// buttonPersonal
			// 
			this.buttonPersonal.Image = ((System.Drawing.Image)(resources.GetObject("buttonPersonal.Image")));
			this.buttonPersonal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonPersonal.Location = new System.Drawing.Point(32, 64);
			this.buttonPersonal.Name = "buttonPersonal";
			this.buttonPersonal.Size = new System.Drawing.Size(136, 23);
			this.buttonPersonal.TabIndex = 1;
			this.buttonPersonal.Text = "Избери критерии";
			this.buttonPersonal.Click += new System.EventHandler(this.buttonPersonal_Click);
			// 
			// checkBoxPersonal
			// 
			this.checkBoxPersonal.Location = new System.Drawing.Point(16, 24);
			this.checkBoxPersonal.Name = "checkBoxPersonal";
			this.checkBoxPersonal.Size = new System.Drawing.Size(160, 32);
			this.checkBoxPersonal.TabIndex = 0;
			this.checkBoxPersonal.Text = "Добави личните справки към общата справка";
			// 
			// groupBoxAssignment
			// 
			this.groupBoxAssignment.Controls.Add(this.buttonAssignment);
			this.groupBoxAssignment.Controls.Add(this.checkBoxAssignment);
			this.groupBoxAssignment.Location = new System.Drawing.Point(224, 8);
			this.groupBoxAssignment.Name = "groupBoxAssignment";
			this.groupBoxAssignment.Size = new System.Drawing.Size(208, 100);
			this.groupBoxAssignment.TabIndex = 1;
			this.groupBoxAssignment.TabStop = false;
			this.groupBoxAssignment.Text = "Справка според назначения";
			// 
			// buttonAssignment
			// 
			this.buttonAssignment.Image = ((System.Drawing.Image)(resources.GetObject("buttonAssignment.Image")));
			this.buttonAssignment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonAssignment.Location = new System.Drawing.Point(32, 64);
			this.buttonAssignment.Name = "buttonAssignment";
			this.buttonAssignment.Size = new System.Drawing.Size(136, 23);
			this.buttonAssignment.TabIndex = 2;
			this.buttonAssignment.Text = "Избери критерии";
			this.buttonAssignment.Click += new System.EventHandler(this.buttonAssignment_Click);
			// 
			// checkBoxAssignment
			// 
			this.checkBoxAssignment.Location = new System.Drawing.Point(16, 16);
			this.checkBoxAssignment.Name = "checkBoxAssignment";
			this.checkBoxAssignment.Size = new System.Drawing.Size(176, 40);
			this.checkBoxAssignment.TabIndex = 1;
			this.checkBoxAssignment.Text = "Добави справката за назначения към общата справка";
			// 
			// groupBoxAbsence
			// 
			this.groupBoxAbsence.Controls.Add(this.buttonAbsence);
			this.groupBoxAbsence.Controls.Add(this.checkBoxAbsence);
			this.groupBoxAbsence.Location = new System.Drawing.Point(8, 120);
			this.groupBoxAbsence.Name = "groupBoxAbsence";
			this.groupBoxAbsence.Size = new System.Drawing.Size(200, 104);
			this.groupBoxAbsence.TabIndex = 2;
			this.groupBoxAbsence.TabStop = false;
			this.groupBoxAbsence.Text = "Справка според отпуски";
			// 
			// buttonAbsence
			// 
			this.buttonAbsence.Image = ((System.Drawing.Image)(resources.GetObject("buttonAbsence.Image")));
			this.buttonAbsence.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonAbsence.Location = new System.Drawing.Point(32, 72);
			this.buttonAbsence.Name = "buttonAbsence";
			this.buttonAbsence.Size = new System.Drawing.Size(136, 23);
			this.buttonAbsence.TabIndex = 2;
			this.buttonAbsence.Text = "Избери критерии";
			this.buttonAbsence.Click += new System.EventHandler(this.buttonAbsence_Click);
			// 
			// checkBoxAbsence
			// 
			this.checkBoxAbsence.Location = new System.Drawing.Point(16, 24);
			this.checkBoxAbsence.Name = "checkBoxAbsence";
			this.checkBoxAbsence.Size = new System.Drawing.Size(160, 40);
			this.checkBoxAbsence.TabIndex = 1;
			this.checkBoxAbsence.Text = "Добави справката за отпуски към общата справка";
			// 
			// groupBoxPenalty
			// 
			this.groupBoxPenalty.Controls.Add(this.buttonPenalty);
			this.groupBoxPenalty.Controls.Add(this.checkBoxPenalty);
			this.groupBoxPenalty.Location = new System.Drawing.Point(224, 120);
			this.groupBoxPenalty.Name = "groupBoxPenalty";
			this.groupBoxPenalty.Size = new System.Drawing.Size(208, 104);
			this.groupBoxPenalty.TabIndex = 3;
			this.groupBoxPenalty.TabStop = false;
			this.groupBoxPenalty.Text = "Справка според наказания";
			// 
			// buttonPenalty
			// 
			this.buttonPenalty.Image = ((System.Drawing.Image)(resources.GetObject("buttonPenalty.Image")));
			this.buttonPenalty.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonPenalty.Location = new System.Drawing.Point(32, 72);
			this.buttonPenalty.Name = "buttonPenalty";
			this.buttonPenalty.Size = new System.Drawing.Size(136, 23);
			this.buttonPenalty.TabIndex = 2;
			this.buttonPenalty.Text = "Избери критерии";
			this.buttonPenalty.Click += new System.EventHandler(this.buttonPenalty_Click);
			// 
			// checkBoxPenalty
			// 
			this.checkBoxPenalty.Location = new System.Drawing.Point(16, 24);
			this.checkBoxPenalty.Name = "checkBoxPenalty";
			this.checkBoxPenalty.Size = new System.Drawing.Size(160, 40);
			this.checkBoxPenalty.TabIndex = 1;
			this.checkBoxPenalty.Text = "Добави справката за наказания към общата справка";
			// 
			// buttonExit
			// 
			this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonExit.Location = new System.Drawing.Point(424, 264);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.TabIndex = 17;
			this.buttonExit.Text = "Exit";
			this.buttonExit.Visible = false;
			this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
			// 
			// checkBoxExportToExcel
			// 
			this.checkBoxExportToExcel.Location = new System.Drawing.Point(16, 240);
			this.checkBoxExportToExcel.Name = "checkBoxExportToExcel";
			this.checkBoxExportToExcel.Size = new System.Drawing.Size(192, 24);
			this.checkBoxExportToExcel.TabIndex = 4;
			this.checkBoxExportToExcel.Text = "Прехвърли в ексел резултата";
			// 
			// formStatistic
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonExit;
			this.ClientSize = new System.Drawing.Size(448, 294);
			this.Controls.Add(this.checkBoxExportToExcel);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.groupBoxPenalty);
			this.Controls.Add(this.groupBoxAbsence);
			this.Controls.Add(this.groupBoxAssignment);
			this.Controls.Add(this.groupBoxPersonal);
			this.Controls.Add(this.buttonFind);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Location = new System.Drawing.Point(100, 0);
			this.Name = "formStatistic";
			this.Text = "Общи справки";
			this.groupBoxPersonal.ResumeLayout(false);
			this.groupBoxAssignment.ResumeLayout(false);
			this.groupBoxAbsence.ResumeLayout(false);
			this.groupBoxPenalty.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public bool IsIdInDataTable(string ID, DataTable dt)
		{
			foreach(DataRow row in dt.Rows)
			{
				if( ID == row[ 0 ].ToString() )
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Required designer variable.
		/// </summary>
		public DataTable MinEnabledTable()
		{
			int min = 9999999;
			int table = 0;
			if( this.dtPersonal.Rows.Count > 0 && this.checkBoxPersonal.Checked )
			{
				min = this.dtPersonal.Rows.Count;
				table = 1;
			}
			if( this.dtAssignment.Rows.Count > 0 && this.checkBoxAssignment.Checked )
			{
				if( min > this.dtAssignment.Rows.Count )
				{
					min = this.dtAssignment.Rows.Count;
					table = 2;
				}
			}
			if( this.dtAbsence.Rows.Count > 0 && this.checkBoxAbsence.Checked )
			{
				if( min > this.dtAbsence.Rows.Count )
				{
					min = this.dtAbsence.Rows.Count;
					table = 3;
				}
			}
			if( this.dtPenalty.Rows.Count > 0 && this.checkBoxPenalty.Checked )
			{
				if( min > this.dtPenalty.Rows.Count )
				{
					min = this.dtPenalty.Rows.Count;
					table = 4;
				}
			}
			switch( table )
			{					
				case 1: 
				{
					return this.dtPersonal;
				}
				case 2:
				{
					return this.dtAssignment;
				}					
				case 3: 
				{
					return this.dtAbsence;
				}					
				case 4: 
				{
					return this.dtPenalty;				
				}
			}
			DataTable dt = new DataTable();
			return dt;
		}

		private void buttonFind_Click(object sender, System.EventArgs e)
		{
			ArrayList arrID = new ArrayList();

			string ID = "";
			DataTable dtSmall = this.MinEnabledTable();
			bool IsInAllTables = false;
			foreach( DataRow row in dtSmall.Rows )
			{
				IsInAllTables = false;
				ID = row[ 0 ].ToString();

				if( this.dtPersonal.Rows.Count > 0 && this.checkBoxPersonal.Checked )
				{
					
					if( !IsIdInDataTable( row[0].ToString(), this.dtPersonal ) )
					{
						continue;
					}
					else
					{
						IsInAllTables = true;
					}
				}

				if( this.dtAbsence.Rows.Count > 0 && this.checkBoxAbsence.Checked )
				{
					if( !IsIdInDataTable( row[0].ToString(), this.dtAbsence ) )
					{
						continue;
					}
					else
					{
						IsInAllTables = true;
					}
				}

				if( this.dtAssignment.Rows.Count > 0 && this.checkBoxAssignment.Checked )
				{
					if( !IsIdInDataTable( row[0].ToString(), this.dtAssignment ) )
					{
						continue;
					}
					else
					{
						IsInAllTables = true;
					}
				}

				if( this.dtPenalty.Rows.Count > 0 && this.checkBoxPenalty.Checked )
				{
					if( !IsIdInDataTable( row[0].ToString(), this.dtPenalty ) )
					{
						continue;
					}
					else
					{
						IsInAllTables = true;
					}
				}
              
				if( IsInAllTables )
				{
					arrID.Add( ID );
				}
			}
			string IsFire = "0";
			if( this.IsFiredd )
			{
				IsFire = "1";
			}
			DataTable dt1;
			DataLayer.DataAction dAction = new DataLayer.DataAction( "person", this.main.connString );
			ArrayList arrColumns = new ArrayList();
			if(formPersonal.arrColumn != null)
			{
				arrColumns.InsertRange( arrColumns.Count , formPersonal.arrColumn );
			}
			if(formPenalty.arrColumn != null)
			{
				arrColumns.InsertRange( arrColumns.Count , formPenalty.arrColumn );
			}
			if(formAbsence.arrColumn != null)
			{
				arrColumns.InsertRange( arrColumns.Count , formAbsence.arrColumn );
			}
			if(formAssignment.arrColumnAdd != null)
			{
				arrColumns.InsertRange( arrColumns.Count , formAssignment.arrColumnAdd );
				arrColumns.Add( "Personassignment.level1" );
				arrColumns.Add( "Personassignment.level2" );
				arrColumns.Add( "Personassignment.level3" );
				arrColumns.Add( "Personassignment.level4" );
			}
			if( this.checkBoxAbsence.Checked == false && this.checkBoxAssignment.Checked == false && 
				this.checkBoxPersonal.Checked == false && this.checkBoxPenalty.Checked == false )
			{ // Samo ako ne e izbrano nito edin kriteriy - togawa pokazway wsichki slujiteli

				dt1 = dAction.SelectWhere( "person", new string[] {"*"}, 1, "WHERE fired = "+ IsFire );
				dt1.PrimaryKey = new DataColumn[]{dt1.Columns["ID"]};
			}
			else
			{
				if( formPersonal != null )
				{
					if( formPersonal.checkBoxAge.Checked )
					{
						arrColumns.Add( "Person.egn" );
					}
				}
				dt1 = dAction.SelectAllPersonBySpecificID( arrID );
			}
			
			if( this.IsRunFromKartoteka )
			{
				if( !this.checkBoxExportToExcel.Checked )
					this.main.formKartoteka.dataGrid1.DataSource = dt1;
				else
				{
					// Tuka trqbwa da se wika eksporta to excel
					ExcelExpo expo = new ExcelExpo();
					
					if(arrColumns.Count != 0)
					{
						expo.ExtractCustom( this.main, dt1, arrColumns );
					}
					else
					{
						MessageBox.Show("Не сте избрали критерии за търсене");
					}
				}
			}
			else
			{
				if( !this.checkBoxExportToExcel.Checked )
				{
					KartotekaLichenSystaw kartoteka = new KartotekaLichenSystaw( this.main, dt1, "Списък на всички служители според съответните критерии в справката", false );
					kartoteka.ShowDialog();
				}
				else
				{
					// Tuka trqbwa da se wika eksporta to excel
					ExcelExpo expo = new ExcelExpo();
					
					if(arrColumns.Count != 0)
					{
						expo.ExtractCustom( this.main, dt1, arrColumns );
					}
					else
					{
						MessageBox.Show("Не сте избрали критерии за търсене");
					}					
				}
			}
		}
     
		private void buttonPersonal_Click(object sender, System.EventArgs e)
		{
			
			formPersonal.ShowDialog();
			if( this.formPersonal.dt1 != null )
			{
				this.checkBoxPersonal.Checked = true;
				this.dtPersonal = formPersonal.dt1;
			}
		}

		private void buttonAssignment_Click(object sender, System.EventArgs e)
		{
			
			formAssignment.ShowDialog();
			if( formAssignment.dt1 != null )
			{
				checkBoxAssignment.Checked = true;
				this.dtAssignment = formAssignment.dt1;
			}
		}

		private void buttonAbsence_Click(object sender, System.EventArgs e)
		{
			
			formAbsence.ShowDialog();
			if( formAbsence.dt1 != null )
			{
				checkBoxAbsence.Checked = true;
				this.dtAbsence = formAbsence.dt1;
			}
			
		}

		private void buttonPenalty_Click(object sender, System.EventArgs e)
		{
			
			formPenalty.ShowDialog();
			if( formPenalty.dt1 != null )
			{
				this.checkBoxPenalty.Checked = true;
				this.dtPenalty = formPenalty.dt1;
			}
		}

		private void buttonExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
