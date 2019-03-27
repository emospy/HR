using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.IO;

namespace WindowsApplication1
{
	public class Form1 : System.Windows.Forms.Form
	{

		MySqlConnection conn;
		MySqlCommand comm;
		MySqlDataAdapter da;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		//private Excel.Application ExcelObj;
		private object opt = System.Reflection.Missing.Value;

		private System.Windows.Forms.DataGrid dataGrid1;
		private System.Windows.Forms.Button buttonGetPositions;
		private System.Windows.Forms.Button buttonGetFirmStructure;
		private System.Windows.Forms.TextBox textBoxRows;
		private System.Windows.Forms.Button buttonGetAssignments;
		private System.Windows.Forms.Button buttonGetPersonalInfo;
		private System.Windows.Forms.Button buttonAssignVacations;
		private System.Windows.Forms.Button buttonStrapTables;
		private System.Windows.Forms.Button buttonGetSickness;
		private System.Windows.Forms.Button buttonGetClassifier;
		private System.Windows.Forms.Button buttonGetPersons;
		private Button buttonGetSyscoStaffAndHolidays;
		private Button buttonGetDis;

		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			///			
			///			ExcelObj = new Excel.Application();
			///
			///			//  See if the Excel Application Object was successfully constructed
			///			if (ExcelObj == null)
			///			{
			///				MessageBox.Show("ERROR: EXCEL couldn't be started!");
			///				System.Windows.Forms.Application.Exit();
			///			}
			///
			System.Globalization.CultureInfo cultureEn = new System.Globalization.CultureInfo("en-GB");
			System.Threading.Thread.CurrentThread.CurrentCulture = cultureEn;

			string connString = "Database=hrdb;Data Source=localhost;User Id=root;Password=tess;charset=utf8;";
			conn = new MySqlConnection();
			conn.ConnectionString = connString;
			comm = new MySqlCommand();
			comm.Connection = conn;

			//  Make the Application Visible

			//			ExcelObj.Visible = true;

		}


		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.buttonGetPositions = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.buttonGetFirmStructure = new System.Windows.Forms.Button();
			this.buttonGetAssignments = new System.Windows.Forms.Button();
			this.textBoxRows = new System.Windows.Forms.TextBox();
			this.dataGrid1 = new System.Windows.Forms.DataGrid();
			this.buttonGetPersonalInfo = new System.Windows.Forms.Button();
			this.buttonAssignVacations = new System.Windows.Forms.Button();
			this.buttonStrapTables = new System.Windows.Forms.Button();
			this.buttonGetSickness = new System.Windows.Forms.Button();
			this.buttonGetClassifier = new System.Windows.Forms.Button();
			this.buttonGetPersons = new System.Windows.Forms.Button();
			this.buttonGetSyscoStaffAndHolidays = new System.Windows.Forms.Button();
			this.buttonGetDis = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonGetPositions
			// 
			this.buttonGetPositions.Location = new System.Drawing.Point(8, 11);
			this.buttonGetPositions.Name = "buttonGetPositions";
			this.buttonGetPositions.Size = new System.Drawing.Size(128, 23);
			this.buttonGetPositions.TabIndex = 0;
			this.buttonGetPositions.Text = "Get Positions";
			this.buttonGetPositions.Click += new System.EventHandler(this.buttonGetPositions_Click);
			// 
			// buttonGetFirmStructure
			// 
			this.buttonGetFirmStructure.Location = new System.Drawing.Point(152, 11);
			this.buttonGetFirmStructure.Name = "buttonGetFirmStructure";
			this.buttonGetFirmStructure.Size = new System.Drawing.Size(152, 23);
			this.buttonGetFirmStructure.TabIndex = 1;
			this.buttonGetFirmStructure.Text = "Get Firm Structure";
			this.buttonGetFirmStructure.Click += new System.EventHandler(this.buttonGetFirmStructure_Click);
			// 
			// buttonGetAssignments
			// 
			this.buttonGetAssignments.Location = new System.Drawing.Point(320, 11);
			this.buttonGetAssignments.Name = "buttonGetAssignments";
			this.buttonGetAssignments.Size = new System.Drawing.Size(168, 23);
			this.buttonGetAssignments.TabIndex = 2;
			this.buttonGetAssignments.Text = "Get Assignments";
			this.buttonGetAssignments.Click += new System.EventHandler(this.buttonGetAssignments_Click);
			// 
			// textBoxRows
			// 
			this.textBoxRows.Location = new System.Drawing.Point(8, 40);
			this.textBoxRows.Name = "textBoxRows";
			this.textBoxRows.Size = new System.Drawing.Size(128, 20);
			this.textBoxRows.TabIndex = 3;
			// 
			// dataGrid1
			// 
			this.dataGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			| System.Windows.Forms.AnchorStyles.Left)
			| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGrid1.DataMember = "";
			this.dataGrid1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGrid1.Location = new System.Drawing.Point(8, 104);
			this.dataGrid1.Name = "dataGrid1";
			this.dataGrid1.Size = new System.Drawing.Size(656, 320);
			this.dataGrid1.TabIndex = 4;
			// 
			// buttonGetPersonalInfo
			// 
			this.buttonGetPersonalInfo.Location = new System.Drawing.Point(504, 8);
			this.buttonGetPersonalInfo.Name = "buttonGetPersonalInfo";
			this.buttonGetPersonalInfo.Size = new System.Drawing.Size(152, 23);
			this.buttonGetPersonalInfo.TabIndex = 5;
			this.buttonGetPersonalInfo.Text = "Get Personal Info";
			this.buttonGetPersonalInfo.Click += new System.EventHandler(this.buttonGetPersonalInfo_Click);
			// 
			// buttonAssignVacations
			// 
			this.buttonAssignVacations.Location = new System.Drawing.Point(152, 40);
			this.buttonAssignVacations.Name = "buttonAssignVacations";
			this.buttonAssignVacations.Size = new System.Drawing.Size(152, 23);
			this.buttonAssignVacations.TabIndex = 6;
			this.buttonAssignVacations.Text = "Assign Vacations";
			this.buttonAssignVacations.Click += new System.EventHandler(this.buttonAssignVacations_Click);
			// 
			// buttonStrapTables
			// 
			this.buttonStrapTables.Location = new System.Drawing.Point(320, 40);
			this.buttonStrapTables.Name = "buttonStrapTables";
			this.buttonStrapTables.Size = new System.Drawing.Size(168, 23);
			this.buttonStrapTables.TabIndex = 7;
			this.buttonStrapTables.Text = "StrapTables";
			this.buttonStrapTables.Click += new System.EventHandler(this.buttonStrapTables_Click);
			// 
			// buttonGetSickness
			// 
			this.buttonGetSickness.Location = new System.Drawing.Point(504, 40);
			this.buttonGetSickness.Name = "buttonGetSickness";
			this.buttonGetSickness.Size = new System.Drawing.Size(152, 23);
			this.buttonGetSickness.TabIndex = 8;
			this.buttonGetSickness.Text = "Get Sickness";
			this.buttonGetSickness.Click += new System.EventHandler(this.buttonGetSickness_Click);
			// 
			// buttonGetClassifier
			// 
			this.buttonGetClassifier.Location = new System.Drawing.Point(8, 72);
			this.buttonGetClassifier.Name = "buttonGetClassifier";
			this.buttonGetClassifier.Size = new System.Drawing.Size(128, 23);
			this.buttonGetClassifier.TabIndex = 9;
			this.buttonGetClassifier.Text = "GetClassifierNKP";
			this.buttonGetClassifier.Click += new System.EventHandler(this.buttonGetClassifier_Click);
			// 
			// buttonGetPersons
			// 
			this.buttonGetPersons.Location = new System.Drawing.Point(152, 72);
			this.buttonGetPersons.Name = "buttonGetPersons";
			this.buttonGetPersons.Size = new System.Drawing.Size(152, 23);
			this.buttonGetPersons.TabIndex = 10;
			this.buttonGetPersons.Text = "Get Persons";
			this.buttonGetPersons.Click += new System.EventHandler(this.buttonGetPersons_Click);
			// 
			// buttonGetSyscoStaffAndHolidays
			// 
			this.buttonGetSyscoStaffAndHolidays.Location = new System.Drawing.Point(320, 69);
			this.buttonGetSyscoStaffAndHolidays.Name = "buttonGetSyscoStaffAndHolidays";
			this.buttonGetSyscoStaffAndHolidays.Size = new System.Drawing.Size(152, 23);
			this.buttonGetSyscoStaffAndHolidays.TabIndex = 11;
			this.buttonGetSyscoStaffAndHolidays.Text = "Get Sysco";
			this.buttonGetSyscoStaffAndHolidays.Click += new System.EventHandler(this.buttonGetSyscoStaffAndHolidays_Click);
			// 
			// buttonGetDis
			// 
			this.buttonGetDis.Location = new System.Drawing.Point(504, 69);
			this.buttonGetDis.Name = "buttonGetDis";
			this.buttonGetDis.Size = new System.Drawing.Size(152, 23);
			this.buttonGetDis.TabIndex = 12;
			this.buttonGetDis.Text = "Get Dis";
			this.buttonGetDis.Click += new System.EventHandler(this.buttonGetDis_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(680, 446);
			this.Controls.Add(this.buttonGetDis);
			this.Controls.Add(this.buttonGetSyscoStaffAndHolidays);
			this.Controls.Add(this.buttonGetPersons);
			this.Controls.Add(this.buttonGetClassifier);
			this.Controls.Add(this.buttonGetSickness);
			this.Controls.Add(this.buttonStrapTables);
			this.Controls.Add(this.buttonAssignVacations);
			this.Controls.Add(this.buttonGetPersonalInfo);
			this.Controls.Add(this.dataGrid1);
			this.Controls.Add(this.textBoxRows);
			this.Controls.Add(this.buttonGetAssignments);
			this.Controls.Add(this.buttonGetFirmStructure);
			this.Controls.Add(this.buttonGetPositions);
			this.Name = "Form1";
			this.Text = "Excel Converter";
			((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.Run(new Form1());
		}

		//		private void button1_Click(object sender, System.EventArgs ec)
		//		{			

		//		}

		private int WriteTreeNode(string level, int par, string code)
		{
			comm = new MySql.Data.MySqlClient.MySqlCommand();
			comm.Connection = this.conn;

			comm.CommandText = "INSERT INTO newtree2 (level, par, code) VALUES ('" + level + "'," + par + ",'" + code + "')";
			try
			{
				comm.Connection.Open();
				comm.ExecuteNonQuery();
			}
			catch (MySql.Data.MySqlClient.MySqlException e)
			{
				MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			comm.Connection.Close();
			return this.GetLastId();
		}

		public int GetLastId()
		{
			DataTable dt = new DataTable();

			this.comm.CommandText = @"SELECT LAST_INSERT_ID()";
			this.da = new MySql.Data.MySqlClient.MySqlDataAdapter(this.comm);
			try
			{
				this.da.Fill(dt);
			}
			catch (MySql.Data.MySqlClient.MySqlException e)
			{
				MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return int.Parse(dt.Rows[0][0].ToString());
		}


		private int WritePosition(string commandtext)
		{
			comm = new MySql.Data.MySqlClient.MySqlCommand();
			comm.Connection = this.conn;

			comm.CommandText = commandtext;
			try
			{
				comm.Connection.Open();
				comm.ExecuteNonQuery();
			}
			catch (MySql.Data.MySqlClient.MySqlException e)
			{
				MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			comm.Connection.Close();
			return this.GetLastId();
		}

		private void buttonGetPositions_Click(object sender, System.EventArgs e)
		{
			Excel.Worksheet xlsheet;
			Excel.Workbook xlwkbook;

			this.openFileDialog1.FileName = "*.xls";
			if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				xlwkbook = (Excel.Workbook)System.Runtime.InteropServices.Marshal.BindToMoniker(openFileDialog1.FileName);
				xlsheet = (Excel.Worksheet)xlwkbook.ActiveSheet;

				Excel.Range oRng;
				int maxrows = int.Parse(this.textBoxRows.Text);
				string level;

				for (int i = 1; i <= maxrows; i++)
				{
					string query = "INSERT INTO globalPositions (PositionName, EKDACode, PorNum, Law, NKPCode, Education, Rang, Experience, MinSalary, MaxSalary) VALUES('";
					try
					{
						try
						{
							oRng = (Excel.Range)xlsheet.Cells[i, 1]; //PositionName
							level = oRng.get_Value(Missing.Value).ToString();
						}
						catch (System.NullReferenceException)
						{
							level = "";
						}
						query += level + "','";

						try
						{
							oRng = (Excel.Range)xlsheet.Cells[i, 3]; //EKDACode
							level = oRng.get_Value(Missing.Value).ToString();
						}
						catch (System.NullReferenceException)
						{
							level = "";
						}
						query += level + "','";

						try
						{
							oRng = (Excel.Range)xlsheet.Cells[i, 4]; //PorNum
							level = oRng.get_Value(Missing.Value).ToString();
						}
						catch (System.NullReferenceException)
						{
							level = "";
						}
						query += level + "','";

						try
						{
							oRng = (Excel.Range)xlsheet.Cells[i, 5]; //Law
							level = oRng.get_Value(Missing.Value).ToString();
						}
						catch (System.NullReferenceException)
						{
							level = "";
						}
						query += level + "','";

						try
						{
							oRng = (Excel.Range)xlsheet.Cells[i, 6]; // NKPCode
							level = oRng.get_Value(Missing.Value).ToString();
						}
						catch (System.NullReferenceException)
						{
							level = "";
						}
						query += level + "','";

						try
						{
							oRng = (Excel.Range)xlsheet.Cells[i, 7]; //Education
							level = oRng.get_Value(Missing.Value).ToString();
						}
						catch (System.NullReferenceException)
						{
							level = "";
						}
						query += level + "','";

						try
						{
							oRng = (Excel.Range)xlsheet.Cells[i, 8]; //Rang
							level = oRng.get_Value(Missing.Value).ToString();
						}
						catch (System.NullReferenceException)
						{
							level = "";
						}
						query += level + "','";

						try
						{
							oRng = (Excel.Range)xlsheet.Cells[i, 9]; //Experience
							level = oRng.get_Value(Missing.Value).ToString();
						}
						catch (System.NullReferenceException)
						{
							level = "";
						}
						query += level + "','";

						try
						{
							oRng = (Excel.Range)xlsheet.Cells[i, 10]; //MinSalary
							level = oRng.get_Value(Missing.Value).ToString();
						}
						catch (System.NullReferenceException)
						{
							level = "";
						}
						query += level + "','";

						try
						{
							oRng = (Excel.Range)xlsheet.Cells[i, 11]; //MaxSalary
							level = oRng.get_Value(Missing.Value).ToString();
						}
						catch (System.NullReferenceException)
						{
							level = "";
						}
						query += level + "')";
						this.WritePosition(query);
					}
					catch (System.NullReferenceException ex)
					{
						MessageBox.Show(ex.Message);
					}
				}
			}
			DataTable dtPositions = new DataTable();

			comm.CommandText = @"SELECT * FROM globalpositions";

			MySqlDataAdapter da = new MySql.Data.MySqlClient.MySqlDataAdapter(comm);
			try
			{
				da.Fill(dtPositions);
				this.dataGrid1.DataSource = dtPositions;
			}
			catch (MySql.Data.MySqlClient.MySqlException ex)
			{
				MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void buttonGetFirmStructure_Click(object sender, System.EventArgs e)
		{
			int[] par = new int[4];
			int currentLevel = 0;
			//int type; //постоянна/сезонна
			//Excel Application Object

			//Excel.Application oExcelApp;
			Excel.Worksheet xlsheet;
			Excel.Workbook xlwkbook;
			//this.Activate();
			//Get reference to Excel.Application from the ROT.
			//oExcelApp =  (Excel.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application");
			this.openFileDialog1.FileName = "*.xls";
			if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				xlwkbook = (Excel.Workbook)System.Runtime.InteropServices.Marshal.BindToMoniker(openFileDialog1.FileName);
				xlsheet = (Excel.Worksheet)xlwkbook.ActiveSheet;

				DataTable dtPositions = new DataTable();
				DataTable dt = new DataTable();

				comm.CommandText = @"SELECT * FROM globalpositions";

				MySqlDataAdapter da = new MySql.Data.MySqlClient.MySqlDataAdapter(comm);
				try
				{
					da.Fill(dtPositions);
				}
				catch (MySql.Data.MySqlClient.MySqlException ex)
				{
					MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

				Excel.Range oRng;
				string Level, Code;
				int max;
				try
				{
					max = int.Parse(this.textBoxRows.Text);
				}
				catch (System.FormatException)
				{
					MessageBox.Show("Enter value for rows");
					return;
				}

				for (int i = 1; i <= max; i++)
				{
					Level = "";
					int j;
					for (j = 2; j <= 6; j++)
					{
						oRng = (Excel.Range)xlsheet.Cells[i, j];
						try
						{
							Level = oRng.get_Value(Missing.Value).ToString();
						}
						catch (System.NullReferenceException)
						{
						}
						if (Level != "")
						{
							break;
						}
					}
					switch (j)
					{
						case 2:
							{
								//type = 0;
								currentLevel = 0;
								oRng = (Excel.Range)xlsheet.Cells[i, 1];
								try
								{
									Code = oRng.get_Value(Missing.Value).ToString();
								}
								catch (System.NullReferenceException)
								{
									Code = "";
								}
								par[currentLevel] = this.WriteTreeNode(Level, 0, Code);
								break;
							}
						case 3:
							{
								//type = 0;
								currentLevel = 1;
								oRng = (Excel.Range)xlsheet.Cells[i, 1];
								try
								{
									Code = oRng.get_Value(Missing.Value).ToString();
								}
								catch (System.NullReferenceException)
								{
									Code = "";
								}
								par[currentLevel] = this.WriteTreeNode(Level, par[currentLevel - 1], Code);
								break;
							}
						case 4:
							{
								//type = 0;
								currentLevel = 2;
								oRng = (Excel.Range)xlsheet.Cells[i, 1];
								try
								{
									Code = oRng.get_Value(Missing.Value).ToString();
								}
								catch (System.NullReferenceException)
								{
									Code = "";
								}
								par[currentLevel] = this.WriteTreeNode(Level, par[currentLevel - 1], Code);
								break;
							}
						case 5:
							{
								//type = 0;
								currentLevel = 3;
								oRng = (Excel.Range)xlsheet.Cells[i, 1];
								try
								{
									Code = oRng.get_Value(Missing.Value).ToString();
								}
								catch (System.NullReferenceException)
								{
									Code = "";
								}
								par[currentLevel] = this.WriteTreeNode(Level, par[currentLevel - 1], Code);
								break;
							}
						case 6:
							{
								bool found = false;
								if (Level == "***")
								{
									//type = 1;
									break;
								}
								oRng = (Excel.Range)xlsheet.Cells[i, 18];
								try
								{
									Code = oRng.get_Value(Missing.Value).ToString();
								}
								catch (System.NullReferenceException)
								{
									MessageBox.Show("Incorrect Link positions data");
									return;
								}

								int p;
								for (p = 0; p < dtPositions.Rows.Count; p++)
								{
									if (dtPositions.Rows[p]["id"].ToString() == Code.ToString())
									{
										found = true;
										break;
									}
								}
								if (found)
								{
									string staff; //, kvs, nummonths, security, add, other, note, nkpCode, EKDACode, id;
									try
									{
										oRng = (Excel.Range)xlsheet.Cells[i, 6];
										staff = oRng.get_Value(Missing.Value).ToString();
									}
									catch (System.NullReferenceException)
									{
										MessageBox.Show("Incorrect Identifier");
										return;
									}
									try
									{
										oRng = (Excel.Range)xlsheet.Cells[i, 8];
										staff = oRng.get_Value(Missing.Value).ToString();
									}
									catch (System.NullReferenceException)
									{
										MessageBox.Show("Incorrect staff count");
										return;
									}

									//								try
									//								{
									//									oRng = (Excel.Range)xlsheet.Cells[i,7];
									//									kvs = oRng.get_Value(Missing.Value).ToString();			
									//								}
									//								catch(System.NullReferenceException)
									//								{
									//									kvs = "";
									//								}
									//								oRng = (Excel.Range)xlsheet.Cells[i,8];
									//								nummonths = oRng.get_Value(Missing.Value).ToString();
									//								
									//								oRng = (Excel.Range)xlsheet.Cells[i,9];
									//								try
									//								{
									//									security = oRng.get_Value(Missing.Value).ToString();
									//								}
									//								catch(System.NullReferenceException)
									//								{
									//									security = "";
									//								}
									//								oRng = (Excel.Range)xlsheet.Cells[i,13];
									//								add = oRng.get_Value(Missing.Value).ToString();
									//								oRng = (Excel.Range)xlsheet.Cells[i,20];
									//								try
									//								{
									//									other = oRng.get_Value(Missing.Value).ToString();
									//								}
									//								catch ( System.NullReferenceException)
									//								{
									//									other = "";
									//								}
									//
									//								oRng = (Excel.Range)xlsheet.Cells[i,23];
									//								try
									//								{
									//									note = oRng.get_Value(Missing.Value).ToString();						
									//								}
									//								catch(System.NullReferenceException)
									//								{
									//									note = "";
									//								}

									this.comm.CommandText = "INSERT INTO firmpersonal3 (id,NameOfPosition, StaffCount, EKDACode, EKDALevel, PorNum, Law, NKPCode, NKPLevel, Education, Rang, Experience, MinSalary, MaxSalary, Free, Busy, par)" + " VALUES( '" + Level + "','" + dtPositions.Rows[p]["PositionName"].ToString() + "','" + staff + "','" + dtPositions.Rows[p]["EKDACode"].ToString() + "','" + dtPositions.Rows[p]["EKDALevel"].ToString() + "','" + dtPositions.Rows[p]["PorNum"].ToString() + "','" + dtPositions.Rows[p]["Law"].ToString() + "','" + dtPositions.Rows[p]["NKPCode"].ToString() + "','" + dtPositions.Rows[p]["NKPLevel"].ToString() + "','" + dtPositions.Rows[p]["Education"].ToString() + "','" + dtPositions.Rows[p]["Rang"].ToString() + "','" + dtPositions.Rows[p]["Experience"].ToString() + "','" + dtPositions.Rows[p]["MinSalary"].ToString() + "','" + dtPositions.Rows[p]["MaxSalary"].ToString() + "'," + 0.ToString() + "," + staff + "," + par[currentLevel].ToString() + ")";
									this.WritePosition(this.comm.CommandText);
								}
								break;
							}
						default:
							{
								break;
							}
					}
				}
			}
			DataTable dtTree = new DataTable();

			comm.CommandText = @"SELECT * FROM newtree2";

			MySqlDataAdapter dab = new MySql.Data.MySqlClient.MySqlDataAdapter(comm);
			try
			{
				da.Fill(dtTree);
				this.dataGrid1.DataSource = dtTree;
			}
			catch (MySql.Data.MySqlClient.MySqlException ex)
			{
				MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void buttonGetAssignments_Click(object sender, System.EventArgs e)
		{
			Excel.Worksheet xlsheet;
			Excel.Workbook xlwkbook;

			this.openFileDialog1.FileName = "*.xls";
			if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				xlwkbook = (Excel.Workbook)System.Runtime.InteropServices.Marshal.BindToMoniker(openFileDialog1.FileName);
				xlsheet = (Excel.Worksheet)xlwkbook.ActiveSheet;

				Excel.Range oRng;
				string Level, Code;
				int max;
				try
				{
					max = int.Parse(this.textBoxRows.Text);
				}
				catch (System.FormatException)
				{
					MessageBox.Show("Enter value for rows");
					return;
				}

				DataTable dtTree = new DataTable();

				comm.CommandText = @"SELECT * FROM newtree2";

				MySqlDataAdapter dab = new MySql.Data.MySqlClient.MySqlDataAdapter(comm);
				try
				{
					dab.Fill(dtTree);
				}
				catch (MySql.Data.MySqlClient.MySqlException ex)
				{
					MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

				DataTable dtFP = new DataTable();

				comm.CommandText = @"SELECT * FROM firmpersonal3";

				MySqlDataAdapter dac = new MySql.Data.MySqlClient.MySqlDataAdapter(comm);
				try
				{
					dac.Fill(dtFP);
				}
				catch (MySql.Data.MySqlClient.MySqlException ex)
				{
					MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

				for (int i = 1; i <= max; i++)
				{
					Level = "";
					oRng = (Excel.Range)xlsheet.Cells[i, 3];
					try
					{
						Level = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						continue;
					}
					if (Level == "")
					{
						continue;
					}
					oRng = (Excel.Range)xlsheet.Cells[i, 1];
					try
					{
						Code = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						Code = "";
						MessageBox.Show("No Code at row" + i.ToString());
						continue;
					}

					bool PosFound = false;
					int j;

					for (j = 0; j < dtFP.Rows.Count; j++)
					{
						if (dtFP.Rows[j]["id"].ToString() == Code)
						{
							PosFound = true;
							break;
						}
					}

					ArrayList Struscture = new ArrayList();
					string[] Schmock = new string[] { "", "", "", "" };
					int ppp, srch, parent;
					if (PosFound)
					{
						try
						{
							srch = int.Parse(dtFP.Rows[j]["par"].ToString());
						}
						catch (System.FormatException)
						{
							srch = 0;
							MessageBox.Show("Incorrect parent");
						}
						do
						{
							for (ppp = 0; ppp < dtTree.Rows.Count; ppp++)
							{
								if (srch == (int)dtTree.Rows[ppp]["id"])
								{
									Struscture.Add(dtTree.Rows[ppp]["level"].ToString());
									break;
								}
							}
							srch = (int)dtTree.Rows[ppp]["par"];
						}
						while (srch != 0);
						int ccm = 0;
						for (int scm = Struscture.Count - 1; scm >= 0; scm--, ccm++)
						{
							Schmock[scm] = Struscture[ccm].ToString();
						}

						string sex;
						Level = Level.TrimEnd(new char[] { ' ' });
						if (Level.EndsWith("a"))
						{
							sex = "Жена";
						}
						else if (Level.EndsWith("в") || Level.EndsWith("ч") || Level.EndsWith("и"))
						{
							sex = "Мъж";
						}
						else
						{
							sex = "Жена";
						}

						string ClassPercent, assignReason, assignedAt, MonthlyAddon, basesalary;
						assignedAt = "2006-08-01";

						string insertPerson = "INSERT INTO person (egn, name, country, sex, positionid, fired, hiredat, pcardpublish, borndate)" + " VALUES( '" + "1111111111" + "','" + Level + "','" + " БЪЛГАРИЯ" + "','" + sex + "','" + dtFP.Rows[j]["par"].ToString() + "','0','" + assignedAt + "','1850-11-11', '1850-11-11')";
						parent = this.WritePosition(insertPerson);


						oRng = (Excel.Range)xlsheet.Cells[i, 14];
						try
						{
							ClassPercent = oRng.get_Value(Missing.Value).ToString();
						}
						catch (System.NullReferenceException)
						{
							ClassPercent = "";
						}

						oRng = (Excel.Range)xlsheet.Cells[i, 16];
						try
						{
							MonthlyAddon = oRng.get_Value(Missing.Value).ToString();
						}
						catch (System.NullReferenceException)
						{
							MonthlyAddon = "";
						}

						oRng = (Excel.Range)xlsheet.Cells[i, 13];
						try
						{
							basesalary = oRng.get_Value(Missing.Value).ToString();
						}
						catch (System.NullReferenceException)
						{
							basesalary = "";
						}

						if (dtFP.Rows[j]["law"].ToString() == "трудово")
						{
							assignReason = "чл. 119, във връзка с чл.123 ал.1 т.4 от Кодекса на труда";
						}
						else
						{
							assignReason = "чл. 87а от Закона за държавния служител";
						}

						string insertAssignment = "INSERT INTO personassignment (contracttype, nkpCode, nkpLevel, EKDACode, EKDALevel, ClassPercent, assignReason, assignedAt, worktime, parent, isactive, position, level1, level2, level3, level4, isadditionalAssignment, positionid, law, monthlyAddon, rang, basesalary, testcontractdate, contractexpiry, parentcontractdate, contract)" + " VALUES('Безсрочен', '" + dtFP.Rows[j]["nkpCode"].ToString() + "','" + dtFP.Rows[j]["nkplevel"].ToString() + "','" + dtFP.Rows[j]["ekdaCode"].ToString() + "','" + dtFP.Rows[j]["EKDALevel"].ToString() + "','" + ClassPercent + "','" + assignReason + "','" + assignedAt + "','Пълно','" + parent.ToString() + "','1','" + dtFP.Rows[j]["nameOfPosition"].ToString() + "','" + Schmock[0] + "','" + Schmock[1] + "','" + Schmock[2] + "','" + Schmock[3] + "','0','" + dtFP.Rows[j]["id"].ToString() + "','" + dtFP.Rows[j]["law"].ToString() + "','" + MonthlyAddon + "','" + dtFP.Rows[j]["Rang"].ToString() + "','" + basesalary + "','" + "1850-11-11" + "','" + "1850-11-11" + "','" + assignedAt + "','" + "Безсрочен" + "')";
						this.WritePosition(insertAssignment);
					}
				}
			}

			DataTable dtPerson = new DataTable();

			comm.CommandText = @"SELECT * FROM person";

			MySqlDataAdapter dad = new MySql.Data.MySqlClient.MySqlDataAdapter(comm);
			try
			{
				dad.Fill(dtPerson);
				this.dataGrid1.DataSource = dtPerson;
			}
			catch (MySql.Data.MySqlClient.MySqlException ex)
			{
				MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#region comments - old conversions
		//private void buttonGetPersonalInfo_Click(object sender, System.EventArgs e)
		//{
		//    Excel.Worksheet xlsheet;
		//    Excel.Workbook  xlwkbook;	

		//    this.openFileDialog1.FileName = "*.xls";
		//    if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
		//    {
		//        xlwkbook = (Excel.Workbook) System.Runtime.InteropServices.Marshal.BindToMoniker(openFileDialog1.FileName);
		//        xlsheet = (Excel.Worksheet) xlwkbook.ActiveSheet;				

		//        Excel.Range oRng;
		//        string Level;
		//        int max;
		//        try
		//        {
		//            max = int.Parse(this.textBoxRows.Text);
		//        }
		//        catch(System.FormatException)
		//        {
		//            MessageBox.Show("Enter value for rows");
		//            return;
		//        }

		//        DataTable dtPerson = new DataTable();			

		//        comm.CommandText = @"SELECT * FROM person";			

		//        MySqlDataAdapter da = new MySql.Data.MySqlClient.MySqlDataAdapter( comm );
		//        try
		//        {
		//            da.Fill( dtPerson );					
		//        }
		//        catch(MySql.Data.MySqlClient.MySqlException ex)
		//        {
		//            MessageBox.Show(ex.Message,"Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//        }

		//        DataTable dtAssignment = new DataTable();			

		//        comm.CommandText = @"SELECT * FROM personassignment";

		//        MySqlDataAdapter dab = new MySql.Data.MySqlClient.MySqlDataAdapter( comm );
		//        try
		//        {
		//            dab.Fill( dtAssignment );
		//        }
		//        catch(MySql.Data.MySqlClient.MySqlException ex)
		//        {
		//            MessageBox.Show(ex.Message,"Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//        }

		//        for( int i = 2; i <= max; i++)
		//        {
		//            string egn, town, education, address, diplom, speciality, years, months, days, municipality, region, lkdate, lknom, lkmvr, gender ;
		//            Level = "";
		//            oRng = (Excel.Range)xlsheet.Cells[i,2];
		//            try
		//            {
		//                Level = oRng.get_Value(Missing.Value).ToString();
		//            }
		//            catch(System.NullReferenceException)
		//            {
		//                continue;
		//            }
		//            if(Level == "")
		//            {
		//                continue;
		//            }

		//            bool found = false;

		//            int currentRow;
		//            for(currentRow = 0; currentRow < dtPerson.Rows.Count; currentRow++)
		//            {
		//                if(dtPerson.Rows[currentRow]["name"].ToString() == Level)
		//                {
		//                    found = true;
		//                    break;
		//                }
		//            }
		//            if(found == false)
		//            {
		//                MessageBox.Show(Level + "not found");
		//                continue;
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i,3];
		//            try
		//            {
		//                egn = oRng.get_Value(Missing.Value).ToString();	
		//            }
		//            catch(System.NullReferenceException)
		//            {
		//                egn = "";
		//                MessageBox.Show("No Code at row" + i.ToString());
		//                continue;
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i,4];
		//            try
		//            {
		//                town = oRng.get_Value(Missing.Value).ToString();	
		//            }
		//            catch(System.NullReferenceException)
		//            {
		//                town = "";
		//                MessageBox.Show("No Code at row" + i.ToString());
		//                continue;
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i,5];
		//            try
		//            {
		//                address = oRng.get_Value(Missing.Value).ToString();	
		//            }
		//            catch(System.NullReferenceException)
		//            {
		//                address = "";
		//                MessageBox.Show("No Code at row" + i.ToString());
		//                continue;
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i,6];
		//            try
		//            {
		//                diplom = oRng.get_Value(Missing.Value).ToString();	
		//            }
		//            catch(System.NullReferenceException)
		//            {
		//                diplom = "";
		//                MessageBox.Show("No Code at row" + i.ToString());
		//                continue;
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i,7];
		//            try
		//            {
		//                diplom += " " + oRng.get_Value(Missing.Value).ToString();	
		//            }
		//            catch(System.NullReferenceException)
		//            {
		//                diplom += "";
		//                MessageBox.Show("No Code at row" + i.ToString());
		//                continue;
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i,9];
		//            try
		//            {
		//                diplom += " " + oRng.get_Value(Missing.Value).ToString();	
		//            }
		//            catch(System.NullReferenceException)
		//            {
		//                diplom += "";
		//                MessageBox.Show("No Code at row" + i.ToString());
		//                continue;
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i,8];
		//            try
		//            {
		//                education = " " + oRng.get_Value(Missing.Value).ToString();	
		//            }
		//            catch(System.NullReferenceException)
		//            {
		//                education = "";
		//                MessageBox.Show("No Code at row" + i.ToString());
		//                continue;
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i,10];
		//            try
		//            {
		//                speciality = " " + oRng.get_Value(Missing.Value).ToString();	
		//            }
		//            catch(System.NullReferenceException)
		//            {
		//                speciality = "";
		//                MessageBox.Show("No Code at row" + i.ToString());
		//                continue;
		//            }

		//            string updatePerson = @"Update person set " + 
		//                "egn = '" + egn +						
		//                "', town = '" + town +
		//                "', education = '" + education +
		//                "', kwartal = '" + address +
		//                "', diplomdate = '" + diplom +
		//                "', speciality = '" + speciality +
		//                "' WHERE id = " + dtPerson.Rows[currentRow]["id"].ToString();
		//            this.WritePosition(updatePerson);

		//            oRng = (Excel.Range)xlsheet.Cells[i,11];
		//            try
		//            {
		//                years = oRng.get_Value(Missing.Value).ToString();	
		//            }
		//            catch(System.NullReferenceException)
		//            {
		//                years = "0";							
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i,12];
		//            try
		//            {
		//                months = oRng.get_Value(Missing.Value).ToString();	
		//            }
		//            catch(System.NullReferenceException)
		//            {
		//                months = "0";							
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i,13];
		//            try
		//            {
		//                days = oRng.get_Value(Missing.Value).ToString();	
		//            }
		//            catch(System.NullReferenceException)
		//            {
		//                days = "0";							
		//            }

		//            string insertAssignment = "UPDATE personassignment SET " +
		//                "years = '" + years +
		//                "', months = '" + months +
		//                "', days = '" + days +
		//                "' WHERE parent = " + dtPerson.Rows[currentRow]["id"].ToString();
		//            this.WritePosition(insertAssignment);
		//        }				
		//    }

		//    DataTable dtPersons = new DataTable();			

		//    comm.CommandText = @"SELECT * FROM person";			

		//    MySqlDataAdapter dad = new MySql.Data.MySqlClient.MySqlDataAdapter( comm );
		//    try
		//    {
		//        dad.Fill( dtPersons );
		//        this.dataGrid1.DataSource = dtPersons;
		//    }
		//    catch(MySql.Data.MySqlClient.MySqlException ex)
		//    {
		//        MessageBox.Show(ex.Message,"Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//    }		
		//}
		#endregion

		//private void buttonGetPersonalInfo_Click(object sender, System.EventArgs e)
		//{
		//    Excel.Worksheet xlsheet;
		//    Excel.Workbook xlwkbook;

		//    this.openFileDialog1.FileName = "*.xls";
		//    if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
		//    {
		//        xlwkbook = (Excel.Workbook)System.Runtime.InteropServices.Marshal.BindToMoniker(openFileDialog1.FileName);
		//        xlsheet = (Excel.Worksheet)xlwkbook.ActiveSheet;

		//        Excel.Range oRng;
		//        int max;
		//        try
		//        {
		//            max = int.Parse(this.textBoxRows.Text);
		//        }
		//        catch (System.FormatException)
		//        {
		//            MessageBox.Show("Enter value for rows");
		//            return;
		//        }

		//        DataTable dtPerson = new DataTable();

		//        comm.CommandText = @"SELECT * FROM person";

		//        MySqlDataAdapter da = new MySql.Data.MySqlClient.MySqlDataAdapter(comm);
		//        try
		//        {
		//            da.Fill(dtPerson);
		//        }
		//        catch (MySql.Data.MySqlClient.MySqlException ex)
		//        {
		//            MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//        }

		//        for (int i = 2; i <= max; i++)
		//        {
		//            //other1 - userno; other2 - password; other3 - cell phone
		//            string other1, other2, phone, other3, hiredat, other4, other5, name, borndate;
		//            string education, address;

		//            oRng = (Excel.Range)xlsheet.Cells[i, 1];
		//            try
		//            {
		//                other1 = oRng.get_Value(Missing.Value).ToString();
		//            }
		//            catch (System.NullReferenceException)
		//            {
		//                continue;
		//            }                   

		//            oRng = (Excel.Range)xlsheet.Cells[i, 2];
		//            try
		//            {
		//                name = oRng.get_Value(Missing.Value).ToString();
		//                name += " ";
		//            }
		//            catch (System.NullReferenceException)
		//            {
		//                name = "";                        
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i, 3];
		//            try
		//            {
		//                name += oRng.get_Value(Missing.Value).ToString();
		//                name += " ";
		//            }
		//            catch (System.NullReferenceException)
		//            {
		//                name += "";
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i, 4];
		//            try
		//            {
		//                name += oRng.get_Value(Missing.Value).ToString();
		//            }
		//            catch (System.NullReferenceException)
		//            {
		//                name += "";
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i, 5];
		//            try
		//            {
		//                other2 = oRng.get_Value(Missing.Value).ToString();
		//            }
		//            catch (System.NullReferenceException)
		//            {
		//                other2 = "";
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i, 9];
		//            try
		//            {
		//                borndate = oRng.get_Value(Missing.Value).ToString();
		//            }
		//            catch (System.NullReferenceException)
		//            {
		//                borndate = "";
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i, 10];
		//            try
		//            {
		//                address = oRng.get_Value(Missing.Value).ToString();
		//            }
		//            catch (System.NullReferenceException)
		//            {
		//                address = "";
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i, 11];
		//            try
		//            {
		//                phone = oRng.get_Value(Missing.Value).ToString();
		//            }
		//            catch (System.NullReferenceException)
		//            {
		//                phone = "";
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i, 12];
		//            try
		//            {
		//                other3 = oRng.get_Value(Missing.Value).ToString();
		//            }
		//            catch (System.NullReferenceException)
		//            {
		//                other3 = "";
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i, 13];
		//            try
		//            {
		//                hiredat =  oRng.get_Value(Missing.Value).ToString();
		//            }
		//            catch (System.NullReferenceException)
		//            {
		//                hiredat = "";
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i, 14];
		//            try
		//            {
		//                if(oRng.get_Value(Missing.Value).ToString() != "NULL")
		//                    continue;
		//            }
		//            catch (System.NullReferenceException)
		//            {                        
		//                continue;
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i, 22];
		//            try
		//            {
		//                education = oRng.get_Value(Missing.Value).ToString();
		//            }
		//            catch (System.NullReferenceException)
		//            {
		//                education = "";
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i, 24];
		//            try
		//            {
		//                other4 = oRng.get_Value(Missing.Value).ToString();
		//            }
		//            catch (System.NullReferenceException)
		//            {
		//                other4 = "";
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i, 25];
		//            try
		//            {
		//                other5 = oRng.get_Value(Missing.Value).ToString();
		//            }
		//            catch (System.NullReferenceException)
		//            {
		//                other5 = "";
		//            }

		//            string insertperson = "INSERT INTO person (other1, other2, phone, other3, other4, other5, name, education, kwartal)";
		//            insertperson += @"VALUES('" + other1 + "','" + other2 + "','" + phone + "','" + other3 + "','" + other4 + "','" +  other5 + "','" + name + "','" + education + "','" + address + "')";
		//            this.comm.CommandText = insertperson;
		//            this.comm.Connection.Open();
		//            this.comm.ExecuteNonQuery();
		//            this.comm.Connection.Close();
		//        }
		//    }

		//    DataTable dtPersons = new DataTable();

		//    comm.CommandText = @"SELECT * FROM person";

		//    MySqlDataAdapter dad = new MySql.Data.MySqlClient.MySqlDataAdapter(comm);
		//    try
		//    {
		//        dad.Fill(dtPersons);
		//        this.dataGrid1.DataSource = dtPersons;
		//    }
		//    catch (MySql.Data.MySqlClient.MySqlException ex)
		//    {
		//        MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//    }
		//}

		private void buttonGetPersonalInfo_Click(object sender, System.EventArgs e)
		{
			Excel.Worksheet xlsheet;
			Excel.Workbook xlwkbook;

			Excel.Worksheet xlsheet2;
			Excel.Workbook xlwkbook2;

			this.openFileDialog1.FileName = "*.xls";
			this.openFileDialog1.ShowDialog();

			xlwkbook = (Excel.Workbook)System.Runtime.InteropServices.Marshal.BindToMoniker(openFileDialog1.FileName);
			xlsheet = (Excel.Worksheet)xlwkbook.ActiveSheet;

			this.openFileDialog1.ShowDialog();

			xlwkbook2 = (Excel.Workbook)System.Runtime.InteropServices.Marshal.BindToMoniker(openFileDialog1.FileName);
			xlsheet2 = (Excel.Worksheet)xlwkbook2.ActiveSheet;

			Dictionary<string, string> DictHol = new Dictionary<string, string>();

			Excel.Range oRng;
			int max;
			try
			{
				max = int.Parse(this.textBoxRows.Text);
			}
			catch (System.FormatException)
			{
				MessageBox.Show("Enter value for rows");
				return;
			}

			for (int i = 6; i < 90; i++)
			{
				string egn;

				oRng = (Excel.Range)xlsheet2.Cells[i, 2];
				try
				{
					egn = oRng.get_Value(Missing.Value).ToString();
				}
				catch (System.NullReferenceException)
				{
					continue;
				}

				egn = egn.Trim();
				string salary;


				oRng = (Excel.Range)xlsheet2.Cells[i, 4];
				try
				{
					salary = oRng.get_Value(Missing.Value).ToString();
				}
				catch (System.NullReferenceException)
				{
					continue;
				}

				DictHol.Add(egn, salary);
			}

			for (int i = 2; i <= max; i++)
			{
				//other1 - userno; other2 - password; other3 - cell phone
				string name;

				oRng = (Excel.Range)xlsheet.Cells[i, 22];
				try
				{
					name = oRng.get_Value(Missing.Value).ToString();
				}
				catch (System.NullReferenceException)
				{
					continue;
				}



				xlsheet.Cells[i, 14] = DictHol[name];

				//date = date.Trim();

				//DateTime startDate = DateTime.Parse(date);
				//DateTime toDate = new DateTime(2012, 01, 01);

				//int years, months, days;

				//CalcuateExperience(startDate, out years, out months, out days);

				//string cexp = string.Format("{0:00}{1:00}{2:00}", years, months, days);

				//xlsheet.Cells[i, 26] = cexp;

			}
		}

		void CalcuateExperience(DateTime startDate, out int Y, out int m, out int d)
		{
			Y = m = d = 0;
			try
			{
				DateTime AssignDate = startDate;
				DateTime toDate = new DateTime(2012, 01, 01);


				//int years = (int)this.dtAssignment.Rows[0]["Years"];
				if (DateTime.Compare(toDate, AssignDate) == 1)
				{
					int AssY, AssM, AssD, CYear, CDay, CMonth;
					// We are calculating correction here
					int CorrY = 0, CorrM = 0, CorrD = 0, CorrDays = 0;

					AssY = AssignDate.Year;
					AssM = AssignDate.Month;
					AssD = AssignDate.Day;
					CYear = toDate.Year - AssY - CorrY;
					if ((CMonth = toDate.Month - AssM - CorrM) < 0)
					{
						CYear--;
						CMonth += 12;
					}
					if ((CDay = toDate.Day - AssD - CorrD) < 0)
					{
						CDay += 30;
						CMonth--;
						if (CMonth < 0)
						{
							CMonth += 12;
							CYear--;
						}
					}
					Y = CYear;
					m = CMonth;
					d = CDay;
				}
				else
				{
					Y = m = d = 0;
					return;
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		//Sysco vacations
		//private void buttonAssignVacations_Click(object sender, System.EventArgs e)
		//{
		//    Excel.Worksheet xlsheet;
		//    Excel.Workbook xlwkbook;

		//    this.openFileDialog1.FileName = "*.xls";
		//    if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
		//    {
		//        xlwkbook = (Excel.Workbook)System.Runtime.InteropServices.Marshal.BindToMoniker(openFileDialog1.FileName);
		//        xlsheet = (Excel.Worksheet)xlwkbook.ActiveSheet;

		//        Excel.Range oRng;
		//        //string Level;
		//        int max;
		//        try
		//        {
		//            max = int.Parse(this.textBoxRows.Text);
		//        }
		//        catch (System.FormatException)
		//        {
		//            MessageBox.Show("Enter value for rows");
		//            return;
		//        }

		//        DataTable dtAbsence = new DataTable();

		//        comm.CommandText = @"SELECT * FROM HR_absence";

		//        SqlConnection scon = new SqlConnection("Data Source=192.168.0.39;Initial Catalog=syscodb;uid=root;Password=tessla;");
		//        SqlCommand scom = new SqlCommand();
		//        scom.Connection = scon;

		//        for (int i = 4; i <= max; i++)
		//        {
		//            string nom, sicknum, app7, app39, otherapp, type, mkb, reasons, nap, notes;
		//            //Level = "";
		//            oRng = (Excel.Range)xlsheet.Cells[i, 1];
		//            try
		//            {
		//                nom = oRng.Value[Missing.Value].ToString();
		//            }
		//            catch (System.NullReferenceException)
		//            {
		//                continue;
		//            }
		//            if (nom == "" || nom == "0")
		//            {
		//                continue;
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i, 4];
		//            try
		//            {
		//                sicknum = oRng.Value[Missing.Value].ToString();
		//            }
		//            catch (System.NullReferenceException)
		//            {
		//                sicknum = "";
		//                //MessageBox.Show("No Code at row" + i.ToString());
		//                //continue;
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i, 8];
		//            try
		//            {
		//                app7 = oRng.get_Value(Missing.Value).ToString();
		//            }
		//            catch (System.NullReferenceException)
		//            {
		//                app7 = "";
		//                //MessageBox.Show("No Code at row" + i.ToString());
		//                //continue;
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i, 9];
		//            try
		//            {
		//                app39 = oRng.get_Value(Missing.Value).ToString();
		//            }
		//            catch (System.NullReferenceException)
		//            {
		//                app39 = "";
		//                //MessageBox.Show("No Code at row" + i.ToString());
		//                //continue;
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i, 10];
		//            try
		//            {
		//                otherapp = oRng.get_Value(Missing.Value).ToString();
		//            }
		//            catch (System.NullReferenceException)
		//            {
		//                otherapp = "";
		//                //MessageBox.Show("No Code at row" + i.ToString());
		//                //continue;
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i, 13];
		//            try
		//            {
		//                type = oRng.get_Value(Missing.Value).ToString();
		//            }
		//            catch (System.NullReferenceException)
		//            {
		//                type = "";
		//                //MessageBox.Show("No Code at row" + i.ToString());
		//                //continue;
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i, 16];
		//            try
		//            {
		//                mkb = oRng.get_Value(Missing.Value).ToString();
		//            }
		//            catch (System.NullReferenceException)
		//            {
		//                mkb = "";
		//                //MessageBox.Show("No Code at row" + i.ToString());
		//                //continue;
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i, 17];
		//            try
		//            {
		//                reasons = oRng.Value[Missing.Value].ToString();
		//            }
		//            catch (System.NullReferenceException)
		//            {
		//                reasons = "";
		//                //MessageBox.Show("No Code at row" + i.ToString());
		//                //continue;
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i, 18];
		//            try
		//            {
		//                nap = oRng.Value[Missing.Value].ToString();
		//            }
		//            catch (System.NullReferenceException)
		//            {
		//                nap = "";
		//                //MessageBox.Show("No Code at row" + i.ToString());
		//                //continue;
		//            }

		//            oRng = (Excel.Range)xlsheet.Cells[i, 19];
		//            try
		//            {
		//                notes = oRng.Value[Missing.Value].ToString();
		//            }
		//            catch (System.NullReferenceException)
		//            {
		//                notes = "";
		//                //MessageBox.Show("No Code at row" + i.ToString());
		//                //continue;
		//            }

		//            string InsertLevel = @"UPDATE HR_absence SET " +
		//                " sicknessnumber = '" + sicknum +
		//                "', attachment7 = '" + app7 +
		//                "', declaration39 = '" + app39 +
		//                "', additionaldocs = '" + otherapp +
		//                "', sicknessduration = '" + type +
		//                "', mkb = '" + mkb +
		//                "', reasons = '" + reasons +
		//                "', napdocs = '" + nap +
		//                "' WHERE numberorder = " + nom + " AND typeabsence = 'Болнични'";

		//            //int id = this.WritePosition(InsertLevel);
		//            scom.CommandText = InsertLevel;
		//            try
		//            {
		//                scom.Connection.Open();
		//                scom.ExecuteNonQuery();
		//            }
		//            catch (MySql.Data.MySqlClient.MySqlException ex)
		//            {
		//                MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//            }
		//            scom.Connection.Close();
		//        }
		//        scom.CommandText = "select * from hr_absence";
		//        SqlDataAdapter da = new SqlDataAdapter(scom);

		//        try
		//        {
		//            da.Fill(dtAbsence);
		//            this.dataGrid1.DataSource = dtAbsence;
		//        }
		//        catch (MySql.Data.MySqlClient.MySqlException ex)
		//        {
		//            MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//        }
		//    }
		//}

		private void buttonAssignVacations_Click(object sender, System.EventArgs e)
		{
			Excel.Worksheet xlsheet;
			Excel.Workbook xlwkbook;

			this.openFileDialog1.FileName = "*.xls";
			if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				xlwkbook = (Excel.Workbook)System.Runtime.InteropServices.Marshal.BindToMoniker(openFileDialog1.FileName);
				xlsheet = (Excel.Worksheet)xlwkbook.ActiveSheet;

				Excel.Range oRng;
				//string Level;
				int max;
				try
				{
					max = int.Parse(this.textBoxRows.Text);
				}
				catch (System.FormatException)
				{
					MessageBox.Show("Enter value for rows");
					return;
				}

				using (HRDBEntities db = new HRDBEntities())
				{
					List<person> lstPerson = (from p in db.person
											  where p.fired == 0
											  select p).ToList();

					for (int i = 1; i <= max; i++)
					{
						string name = "";
						string total, leftover, year = "";

						//get next name
						do
						{
							oRng = (Excel.Range)xlsheet.Cells[i, 2];
							try
							{
								name = oRng.get_Value(Missing.Value).ToString();
							}
							catch (System.NullReferenceException)
							{
								i++;
								continue;
							}
						} while (i <= max && name == "");

						//lookup the name
						person per = lstPerson.Find(p => p.name.Trim().ToLower() == name.Trim().ToLower());
						if (per == null)
						{
							//add a coloring here
							continue;
						}

						//look for an years data
						i++;
						do
						{
							oRng = (Excel.Range)xlsheet.Cells[i, 6];
							try
							{
								year = oRng.get_Value(Missing.Value).ToString();
							}
							catch (System.NullReferenceException)
							{
								year = "";
								continue;
							}
							oRng = (Excel.Range)xlsheet.Cells[i, 11];
							try
							{
								total = oRng.get_Value(Missing.Value).ToString();
							}
							catch (System.NullReferenceException)
							{
								i++;
								continue;
							}
							oRng = (Excel.Range)xlsheet.Cells[i, 13];
							try
							{
								leftover = oRng.get_Value(Missing.Value).ToString();
							}
							catch (System.NullReferenceException)
							{
								i++;
								continue;
							}

							int y, t, l;
							if (int.TryParse(year, out y) && int.TryParse(total, out t) && int.TryParse(leftover, out l))
							{
								year_holiday yh = new year_holiday();
								yh.year = y;
								yh.parent = per.id;
								yh.leftover = l;
								yh.TELK = 0;
								yh.total = t;
								db.year_holiday.AddObject(yh);
							}

							i++;
						} while (year != "" && i < max);
					}
					db.SaveChanges();
				}

			}
		}

		private void buttonStrapTables_Click(object sender, System.EventArgs e)
		{
			Excel.Worksheet xlsheet;
			Excel.Workbook xlwkbook;

			if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				xlwkbook = (Excel.Workbook)System.Runtime.InteropServices.Marshal.BindToMoniker(openFileDialog1.FileName);
				xlsheet = (Excel.Worksheet)xlwkbook.ActiveSheet;

				Excel.Range oRng;
				int maxrows = int.Parse(this.textBoxRows.Text);
				string level;
				FileStream fs;

				if (this.openFileDialog1.ShowDialog() == DialogResult.Cancel)
					return;

				fs = new FileStream(openFileDialog1.FileName, FileMode.Append, FileAccess.Write, FileShare.None);
				StreamWriter sw = new StreamWriter(fs);

				for (int i = 1; i <= maxrows; i++)
				{
					try
					{
						oRng = (Excel.Range)xlsheet.Cells[i, 1]; //Number
						level = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						level = "";
					}

					sw.Write(level + "0,00;  ");

					try
					{
						oRng = (Excel.Range)xlsheet.Cells[i, 2]; //Value
						level = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						level = "";
					}

					sw.Write(level + ",00;  (Entry #" + (i + 1).ToString() + ")");
					sw.Write(sw.NewLine);
				}
				sw.Flush();
				sw.Close();
			}
		}

		private void buttonGetPersons_Click(object sender, System.EventArgs e)
		{
			Excel.Worksheet xlsheet;
			Excel.Workbook xlwkbook;

			this.openFileDialog1.FileName = "*.xls";
			if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				xlwkbook = (Excel.Workbook)System.Runtime.InteropServices.Marshal.BindToMoniker(openFileDialog1.FileName);
				xlsheet = (Excel.Worksheet)xlwkbook.ActiveSheet;

				Excel.Range oRng;
				//string Level;
				int max;
				try
				{
					max = int.Parse(this.textBoxRows.Text);
				}
				catch (System.FormatException)
				{
					MessageBox.Show("Enter value for rows");
					return;
				}

				DataTable dtPerson = new DataTable();

				comm.CommandText = @"SELECT * FROM person";

				MySqlDataAdapter da = new MySql.Data.MySqlClient.MySqlDataAdapter(comm);
				try
				{
					da.Fill(dtPerson);
				}
				catch (MySql.Data.MySqlClient.MySqlException ex)
				{
					MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

				DataTable dtAssignment = new DataTable();

				comm.CommandText = @"SELECT * FROM personassignment";

				MySqlDataAdapter dab = new MySql.Data.MySqlClient.MySqlDataAdapter(comm);
				try
				{
					dab.Fill(dtAssignment);
				}
				catch (MySql.Data.MySqlClient.MySqlException ex)
				{
					MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

				for (int i = 2; i <= max; i++)
				{
					string egn, town, address, years, months, days, region, lkdate, lknom, lkmvr, gender, name, assignedat, temp1, temp2;
					//Level = "";
					oRng = (Excel.Range)xlsheet.Cells[i, 2];
					try
					{
						name = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						continue;
					}
					if (name == "")
					{
						continue;
					}

					bool found = false;

					//					int currentRow;
					//					for(currentRow = 0; currentRow < dtPerson.Rows.Count; currentRow++)
					//					{
					//						if(dtPerson.Rows[currentRow]["name"].ToString() == Level)
					//						{
					//							found = true;
					//							break;
					//						}
					//					}
					//					if(found == false)
					//					{
					//						MessageBox.Show(Level + "not found");
					//						continue;
					//					}

					oRng = (Excel.Range)xlsheet.Cells[i, 3];
					try
					{
						egn = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						egn = "";
						//MessageBox.Show("No Code at row" + i.ToString());
						//continue;
					}

					oRng = (Excel.Range)xlsheet.Cells[i, 4];
					try
					{
						town = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						town = "";
						//MessageBox.Show("No Code at row" + i.ToString());
						//continue;
					}

					oRng = (Excel.Range)xlsheet.Cells[i, 6];
					try
					{
						region = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						region = "";
						//MessageBox.Show("No Code at row" + i.ToString());
						//continue;
					}

					oRng = (Excel.Range)xlsheet.Cells[i, 7];
					try
					{
						address = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						address = "";
						//MessageBox.Show("No Code at row" + i.ToString());
						//continue;
					}

					oRng = (Excel.Range)xlsheet.Cells[i, 8];
					try
					{
						lknom = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						lknom = "";
						//MessageBox.Show("No Code at row" + i.ToString());
						//continue;
					}

					oRng = (Excel.Range)xlsheet.Cells[i, 9];
					try
					{
						lkdate = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						lkdate = "";
						//MessageBox.Show("No Code at row" + i.ToString());
						//continue;
					}

					oRng = (Excel.Range)xlsheet.Cells[i, 10];
					try
					{
						lkmvr = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						lkmvr = "";
						//MessageBox.Show("No Code at row" + i.ToString());
						//continue;
					}

					oRng = (Excel.Range)xlsheet.Cells[i, 12];
					try
					{
						assignedat = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						assignedat = "";
						//MessageBox.Show("No Code at row" + i.ToString());
						//continue;
					}

					oRng = (Excel.Range)xlsheet.Cells[i, 11];
					try
					{
						temp1 = oRng.get_Value(Missing.Value).ToString();
						if (temp1 == "М" || temp1 == "M")
						{
							gender = "Мъж";
						}
						else
						{
							gender = "Жена";
						}
					}
					catch (System.NullReferenceException)
					{
						gender = "";
						MessageBox.Show("No Code at row" + i.ToString());
						continue;
					}

					DateTime dataaa;

					try
					{
						dataaa = DateTime.Parse(lkdate);
					}
					catch
					{
						dataaa = new DateTime(1800, 1, 1);
					}

					DateTime asss;

					try
					{
						asss = DateTime.Parse(assignedat);
					}
					catch
					{
						asss = new DateTime(1800, 1, 1);
					}

					string InsertPerson = @"INSERT INTO person (egn, name, town, kwartal, region, sex, pcardpublish, pcard, publishedby, borndate, hiredat, fired )" +
											 "VALUES( '" + egn + "','" + name + "','" + town + "','" + address + "','" +
											region + "','" + gender + "','" + dataaa.Year + "-" + dataaa.Month + "-" + dataaa.Day + "','" + lknom + "','" + lkmvr + "','" +
											"19" + egn.Substring(0, 2) + "-" + egn.Substring(2, 2) + "-" + egn.Substring(4, 2) + "','"
											+ asss.Year + "-" + asss.Month + "-" + asss.Day + "','0" + "')";


					int id = this.WritePosition(InsertPerson);

					string totalsst, insst;
					int tot, inn, sum, y, m, d;
					oRng = (Excel.Range)xlsheet.Cells[i, 13];
					try
					{
						totalsst = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						totalsst = "0";
					}

					oRng = (Excel.Range)xlsheet.Cells[i, 14];
					try
					{
						insst = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						insst = "0";
					}

					years = "0";
					months = "0";
					days = "0";

					if (totalsst != "0" && insst != "0")
					{
						totalsst = totalsst.PadLeft(6, '0');
						insst = insst.PadLeft(6, '0');
						try
						{
							tot = int.Parse(totalsst.Substring(4, 2));
						}
						catch
						{
							tot = 0;
						}
						try
						{
							tot = int.Parse(totalsst.Substring(2, 2)) * 30 + tot;
						}
						catch
						{
						}
						try
						{
							tot = int.Parse(totalsst.Substring(0, 2)) * 365 + tot;
						}
						catch
						{
						}

						try
						{
							inn = int.Parse(insst.Substring(4, 2));
						}
						catch
						{
							inn = 0;
						}
						try
						{
							inn = int.Parse(insst.Substring(2, 2)) * 30 + inn;
						}
						catch
						{
						}
						try
						{
							inn = int.Parse(insst.Substring(0, 2)) * 365 + inn;
						}
						catch
						{
						}

						sum = tot - inn;
						y = sum / 365;
						sum -= y * 365;
						m = sum / 30;
						sum -= m * 30;
						d = sum;
						years = y.ToString();
						months = m.ToString();
						days = d.ToString();
					}


					string insertAssignment = @"INSERT INTO personassignment (parent, years, months, days, assignedat, isactive, isadditionalassignment ) " +
							"VALUES( '" + id + "','" + years + "','" + months + "','" + days + "','" + asss.Year + "-" + asss.Month + "-" + asss.Day + "','1','0" + "')";
					this.WritePosition(insertAssignment);
				}
			}

			DataTable dtPersons = new DataTable();

			comm.CommandText = @"SELECT * FROM person";

			MySqlDataAdapter dad = new MySql.Data.MySqlClient.MySqlDataAdapter(comm);
			try
			{
				dad.Fill(dtPersons);
				this.dataGrid1.DataSource = dtPersons;
			}
			catch (MySql.Data.MySqlClient.MySqlException ex)
			{
				MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void buttonGetClassifier_Click(object sender, System.EventArgs e)
		{
			Excel.Worksheet xlsheet;
			Excel.Workbook xlwkbook;

			this.openFileDialog1.FileName = "*.xls";
			if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				xlwkbook = (Excel.Workbook)System.Runtime.InteropServices.Marshal.BindToMoniker(openFileDialog1.FileName);
				xlsheet = (Excel.Worksheet)xlwkbook.ActiveSheet;

				Excel.Range oRng;
				//string Level;
				int max;
				try
				{
					max = int.Parse(this.textBoxRows.Text);
				}
				catch (System.FormatException)
				{
					MessageBox.Show("Enter value for rows");
					return;
				}

				DataTable dtPerson = new DataTable();

				comm.CommandText = @"SELECT * FROM nkp";

				MySqlDataAdapter da = new MySql.Data.MySqlClient.MySqlDataAdapter(comm);
				try
				{
					da.Fill(dtPerson);
				}
				catch (MySql.Data.MySqlClient.MySqlException ex)
				{
					MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

				DataTable dtAssignment = new DataTable();

				for (int i = 1; i <= max; i++)
				{
					string level, code1, code2, code;
					//Level = "";
					oRng = (Excel.Range)xlsheet.Cells[i, 1];
					try
					{
						code1 = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						continue;
					}
					if (code1 == "")
					{
						continue;
					}

					oRng = (Excel.Range)xlsheet.Cells[i, 2];
					try
					{
						code2 = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						code2 = "";
						//MessageBox.Show("No Code at row" + i.ToString());
						continue;
					}

					while (code2.Length < 4)
					{
						code2 = "0" + code2;
					}

					oRng = (Excel.Range)xlsheet.Cells[i, 3];
					try
					{
						level = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						level = "";
						//MessageBox.Show("No Code at row" + i.ToString());
						continue;
					}
					code = code1 + code2;

					string InsertLevel = @"INSERT INTO nkp (level,code) VALUES( '" + level + "','" + code + "')";

					int id = this.WritePosition(InsertLevel);
				}
			}

			DataTable dtPersons = new DataTable();

			comm.CommandText = @"SELECT * FROM nkp";

			MySqlDataAdapter dad = new MySql.Data.MySqlClient.MySqlDataAdapter(comm);
			try
			{
				dad.Fill(dtPersons);
				this.dataGrid1.DataSource = dtPersons;
			}
			catch (MySql.Data.MySqlClient.MySqlException ex)
			{
				MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void buttonGetSyscoStaffAndHolidays_Click(object sender, EventArgs e)
		{
			Excel.Worksheet xlsheet;
			Excel.Workbook xlwkbook;
			DataTable dtPerson = new DataTable();
			DataTable dtHoliday = new DataTable();
			DataTable dtExisting = new DataTable();

			string connSQL = "Data Source=192.168.0.39;Initial Catalog= syscodb;uid=root;Password=tessla;";
			SqlConnection sqconn = new SqlConnection(connSQL);
			SqlCommand sqcomm = new SqlCommand("SELECT * FROM hr_person", sqconn);
			SqlDataAdapter sqda = new SqlDataAdapter(sqcomm);

			dtExisting.Columns.Add(new DataColumn("sysco_id"));
			dtExisting.Columns.Add(new DataColumn("totalHoliday"));
			dtExisting.Columns.Add(new DataColumn("usedHoliday"));
			dtExisting.Columns.Add(new DataColumn("leftHoliday"));
			dtExisting.Columns.Add(new DataColumn("2010Holiday"));
			dtExisting.Columns.Add(new DataColumn("2009Holiday"));
			dtExisting.Columns.Add(new DataColumn("Jan"));
			dtExisting.Columns.Add(new DataColumn("Feb"));
			dtExisting.Columns.Add(new DataColumn("Mar"));
			dtExisting.Columns.Add(new DataColumn("Apr"));
			dtExisting.Columns.Add(new DataColumn("May"));
			dtExisting.Columns.Add(new DataColumn("Jun"));


			sqcomm.CommandText = "SELECT * FROM hr_person";
			sqda.SelectCommand.CommandText = "SELECT * FROM hr_person";
			try
			{
				sqda.Fill(dtPerson);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			this.openFileDialog1.FileName = "*.xls";
			if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				#region Excel
				xlwkbook = (Excel.Workbook)System.Runtime.InteropServices.Marshal.BindToMoniker(openFileDialog1.FileName);
				xlsheet = (Excel.Worksheet)xlwkbook.ActiveSheet;

				Excel.Range oRng;

				int max;
				try
				{
					max = int.Parse(this.textBoxRows.Text);
				}
				catch (System.FormatException)
				{
					MessageBox.Show("Enter value for rows");
					return;
				}

				for (int i = 2; i <= max; i++)
				{
					DataRow row = dtExisting.NewRow();
					string gstr;
					oRng = (Excel.Range)xlsheet.Cells[i, 2];
					try
					{
						gstr = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						continue;
					}
					if (gstr == "")
					{
						continue;
					}
					row["sysco_id"] = gstr;

					int value;
					oRng = (Excel.Range)xlsheet.Cells[i, 5];
					try
					{
						gstr = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						gstr = "0";
					}
					if (int.TryParse(gstr, out value))
						row["totalHoliday"] = gstr;
					else
						row["totalHoliday"] = 0;


					oRng = (Excel.Range)xlsheet.Cells[i, 20];
					try
					{
						gstr = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						gstr = "0";
					}
					if (int.TryParse(gstr, out value))
						row["usedHoliday"] = gstr;
					else
						row["usedHoliday"] = 0;

					oRng = (Excel.Range)xlsheet.Cells[i, 21];
					try
					{
						gstr = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						gstr = "0";
					}
					if (int.TryParse(gstr, out value))
						row["leftHoliday"] = gstr;
					else
						row["leftHoliday"] = 0;

					oRng = (Excel.Range)xlsheet.Cells[i, 22];
					try
					{
						gstr = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						gstr = "0";
					}
					if (int.TryParse(gstr, out value))
						row["2010Holiday"] = gstr;
					else
						row["2010Holiday"] = 0;

					oRng = (Excel.Range)xlsheet.Cells[i, 23];
					try
					{
						gstr = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						gstr = "0";
					}
					if (int.TryParse(gstr, out value))
						row["2009Holiday"] = gstr;
					else
						row["2009Holiday"] = 0;

					oRng = (Excel.Range)xlsheet.Cells[i, 7];
					try
					{
						gstr = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						gstr = "0";
					}
					if (int.TryParse(gstr, out value))
						row["Jan"] = gstr;
					else
						row["Jan"] = 0;

					oRng = (Excel.Range)xlsheet.Cells[i, 8];
					try
					{
						gstr = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						gstr = "0";
					}
					if (int.TryParse(gstr, out value))
						row["Feb"] = gstr;
					else
						row["Feb"] = 0;

					oRng = (Excel.Range)xlsheet.Cells[i, 9];
					try
					{
						gstr = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						gstr = "0";
					}
					if (int.TryParse(gstr, out value))
						row["Mar"] = gstr;
					else
						row["Mar"] = 0;

					oRng = (Excel.Range)xlsheet.Cells[i, 10];
					try
					{
						gstr = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						gstr = "0";
					}
					if (int.TryParse(gstr, out value))
						row["Apr"] = gstr;
					else
						row["Apr"] = 0;

					oRng = (Excel.Range)xlsheet.Cells[i, 11];
					try
					{
						gstr = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						gstr = "0";
					}
					if (int.TryParse(gstr, out value))
						row["May"] = gstr;
					else
						row["May"] = 0;

					oRng = (Excel.Range)xlsheet.Cells[i, 12];
					try
					{
						gstr = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						gstr = "0";
					}
					if (int.TryParse(gstr, out value))
						row["Jun"] = gstr;
					else
						row["Jun"] = 0;

					//////////////////////////
					dtExisting.Rows.Add(row);
				}
				#endregion

				//sqcomm.CommandText = @"SELECT * FROM hr_person";
				//sqda.SelectCommand.CommandText = "SELECT * FROM hr_person";
				//try
				//{
				//    sqda.Fill(dtPerson);
				//}
				//catch (Exception ex)
				//{
				//    MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
				//}

				dtPerson.Columns.Add("found");
				foreach (DataRow r in dtPerson.Rows)
				{
					r["found"] = false;
				}

				foreach (DataRow r in dtExisting.Rows)
				{
					DataView vue = new DataView(dtPerson, "id_sysco = " + r["sysco_id"], "id_sysco", DataViewRowState.CurrentRows);
					if (vue.Count == 1)
					{
						vue[0]["found"] = true;

						sqda.SelectCommand.CommandText = "select * from hr_absence where parent = " + vue[0]["id"].ToString();
						sqda.Fill(dtHoliday);

						#region Insert Holidays
						if (dtHoliday.Rows.Count == 0)
						{
							int test;
							if (int.TryParse(r["Jan"].ToString(), out test))
							{
								if (test > 0)
								{
									string command = "INSERT INTO hr_absence (orderfromdate, fromdate, todate, countdays, typeabsence, numberorder, reason, parent, modifiedbyuser, year)";
									command += @"VALUES('01-01-2011', '01-01-2011', '" + r["Jan"].ToString() + "-01-2011','" + r["Jan"].ToString() + "', 'Полагаем годишен отпуск', 0, '', '" + vue[0]["id"].ToString() + "','AZ', '2011')";

									sqcomm.CommandText = command;
									try
									{
										sqcomm.Connection.Open();
										sqcomm.ExecuteNonQuery();
									}
									catch (SqlException exx)
									{
										MessageBox.Show(exx.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
									}
									sqcomm.Connection.Close();
								}
							}

							if (int.TryParse(r["Feb"].ToString(), out test))
							{
								if (test > 0)
								{
									string command = "INSERT INTO hr_absence (orderfromdate, fromdate, todate, countdays, typeabsence, numberorder, reason, parent, modifiedbyuser, year)";
									command += @"VALUES('01-02-2011', '01-02-2011', '" + r["Feb"].ToString() + "-02-2011','" + r["Feb"].ToString() + "', 'Полагаем годишен отпуск', 0, '', '" + vue[0]["id"].ToString() + "','AZ', '2011')";
									sqcomm.CommandText = command;
									try
									{
										sqcomm.Connection.Open();
										sqcomm.ExecuteNonQuery();
									}
									catch (SqlException exx)
									{
										MessageBox.Show(exx.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
									}
									sqcomm.Connection.Close();
								}
							}

							if (int.TryParse(r["Mar"].ToString(), out test))
							{
								if (test > 0)
								{
									string command = "INSERT INTO hr_absence (orderfromdate, fromdate, todate, countdays, typeabsence, numberorder, reason, parent, modifiedbyuser, year)";
									command += @"VALUES('01-03-2011', '01-03-2011', '" + r["Mar"].ToString() + "-03-2011','" + r["Mar"].ToString() + "', 'Полагаем годишен отпуск', 0, '', '" + vue[0]["id"].ToString() + "','AZ', '2011')";
									sqcomm.CommandText = command;
									try
									{
										sqcomm.Connection.Open();
										sqcomm.ExecuteNonQuery();
									}
									catch (MySql.Data.MySqlClient.MySqlException exx)
									{
										MessageBox.Show(exx.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
									}
									sqcomm.Connection.Close();
								}
							}

							if (int.TryParse(r["Apr"].ToString(), out test))
							{
								if (test > 0)
								{
									string command = "INSERT INTO hr_absence (orderfromdate, fromdate, todate, countdays, typeabsence, numberorder, reason, parent, modifiedbyuser, year)";
									command += @" VALUES('01-04-2011', '01-04-2011', '" + r["Apr"].ToString() + "-04-2011','" + r["Apr"].ToString() + "', 'Полагаем годишен отпуск', 0, '', '" + vue[0]["id"].ToString() + "','AZ', '2011')";
									sqcomm.CommandText = command;
									try
									{
										sqcomm.Connection.Open();
										sqcomm.ExecuteNonQuery();
									}
									catch (MySql.Data.MySqlClient.MySqlException exx)
									{
										MessageBox.Show(exx.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
									}
									sqcomm.Connection.Close();
								}
							}

							if (int.TryParse(r["May"].ToString(), out test))
							{
								if (test > 0)
								{
									string command = "INSERT INTO hr_absence (orderfromdate, fromdate, todate, countdays, typeabsence, numberorder, reason, parent, modifiedbyuser, year)";
									command += @"VALUES('01-05-2011', '01-05-2011', '" + r["May"].ToString() + "-05-2011','" + r["May"].ToString() + "', 'Полагаем годишен отпуск', 0, '', '" + vue[0]["id"].ToString() + "','AZ', '2011')";
									sqcomm.CommandText = command;
									try
									{
										sqcomm.Connection.Open();
										sqcomm.ExecuteNonQuery();
									}
									catch (MySql.Data.MySqlClient.MySqlException exx)
									{
										MessageBox.Show(exx.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
									}
									sqcomm.Connection.Close();
								}
							}

							if (int.TryParse(r["Jun"].ToString(), out test))
							{
								if (test > 0)
								{
									string command = "INSERT INTO hr_absence (orderfromdate, fromdate, todate, countdays, typeabsence, numberorder, reason, parent, modifiedbyuser, year)";
									command += @"VALUES('01-06-2011', '01-06-2011', '" + r["Jun"].ToString() + "-06-2011','" + r["Jun"].ToString() + "', 'Полагаем годишен отпуск', 0, '', '" + vue[0]["id"].ToString() + "','AZ', '2011')";
									sqcomm.CommandText = command;
									try
									{
										sqcomm.Connection.Open();
										sqcomm.ExecuteNonQuery();
									}
									catch (MySql.Data.MySqlClient.MySqlException exx)
									{
										MessageBox.Show(exx.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
									}
									sqcomm.Connection.Close();
								}
							}
						}
						#endregion
					}
					else if (vue.Count > 1)
					{
						MessageBox.Show("Duplicated id " + r["sysco_id"].ToString());
					}
				}

				foreach (DataRow r in dtPerson.Rows)
				{
					if (r["found"].ToString() == "False")
					{
						string command = "UPDATE hr_person SET fired = 1 WHERE id = " + r["id"].ToString();

						sqcomm.CommandText = command;
						try
						{
							sqcomm.Connection.Open();
							sqcomm.ExecuteNonQuery();
						}
						catch (MySql.Data.MySqlClient.MySqlException exx)
						{
							MessageBox.Show(exx.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						sqcomm.Connection.Close();
					}
				}
			}

			//DataTable dtPersons = new DataTable();

			//comm.CommandText = @"SELECT * FROM nkp";

			//MySqlDataAdapter dad = new MySql.Data.MySqlClient.MySqlDataAdapter(comm);
			//try
			//{
			//    dad.Fill(dtPersons);
			//    this.dataGrid1.DataSource = dtPersons;
			//}
			//catch (MySql.Data.MySqlClient.MySqlException ex)
			//{
			//    MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
			//}				

		}

		private void buttonGetSickness_Click(object sender, EventArgs e)
		{
			Excel.Worksheet xlsheet;
			Excel.Workbook xlwkbook;

			this.openFileDialog1.FileName = "*.xls";
			if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				xlwkbook = (Excel.Workbook)System.Runtime.InteropServices.Marshal.BindToMoniker(openFileDialog1.FileName);
				xlsheet = (Excel.Worksheet)xlwkbook.ActiveSheet;

				Excel.Range oRng;
				//string Level;
				int max;
				try
				{
					max = int.Parse(this.textBoxRows.Text);
				}
				catch (System.FormatException)
				{
					MessageBox.Show("Enter value for rows");
					return;
				}

				DataTable dtAbsence = new DataTable();

				comm.CommandText = @"SELECT * FROM HR_absence";

				SqlConnection scon = new SqlConnection("Data Source=192.168.0.39;Initial Catalog=syscodb;uid=root;Password=tessla;");
				SqlCommand scom = new SqlCommand();
				scom.Connection = scon;
				scom.CommandText = "select * from hr_absence";
				SqlDataAdapter da = new SqlDataAdapter(scom);

				try
				{
					da.Fill(dtAbsence);
				}
				catch (MySql.Data.MySqlClient.MySqlException ex)
				{
					MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

				for (int i = 4; i <= max; i++)
				{
					string nom, sicknum, app7, app39, otherapp, type, mkb, reasons, nap, notes;
					//Level = "";
					oRng = (Excel.Range)xlsheet.Cells[i, 1];
					try
					{
						nom = oRng.Value[Missing.Value].ToString();
					}
					catch (System.NullReferenceException)
					{
						continue;
					}
					if (nom == "" || nom == "0")
					{
						continue;
					}

					oRng = (Excel.Range)xlsheet.Cells[i, 4];
					try
					{
						sicknum = oRng.Value[Missing.Value].ToString();
					}
					catch (System.NullReferenceException)
					{
						sicknum = "";
						//MessageBox.Show("No Code at row" + i.ToString());
						//continue;
					}

					oRng = (Excel.Range)xlsheet.Cells[i, 8];
					try
					{
						app7 = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						app7 = "";
						//MessageBox.Show("No Code at row" + i.ToString());
						//continue;
					}

					oRng = (Excel.Range)xlsheet.Cells[i, 9];
					try
					{
						app39 = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						app39 = "";
						//MessageBox.Show("No Code at row" + i.ToString());
						//continue;
					}

					oRng = (Excel.Range)xlsheet.Cells[i, 10];
					try
					{
						otherapp = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						otherapp = "";
						//MessageBox.Show("No Code at row" + i.ToString());
						//continue;
					}

					oRng = (Excel.Range)xlsheet.Cells[i, 13];
					try
					{
						type = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						type = "";
						//MessageBox.Show("No Code at row" + i.ToString());
						//continue;
					}

					oRng = (Excel.Range)xlsheet.Cells[i, 16];
					try
					{
						mkb = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						mkb = "";
						//MessageBox.Show("No Code at row" + i.ToString());
						//continue;
					}

					oRng = (Excel.Range)xlsheet.Cells[i, 17];
					try
					{
						reasons = oRng.Value[Missing.Value].ToString();
					}
					catch (System.NullReferenceException)
					{
						reasons = "";
						//MessageBox.Show("No Code at row" + i.ToString());
						//continue;
					}

					oRng = (Excel.Range)xlsheet.Cells[i, 18];
					try
					{
						nap = oRng.Value[Missing.Value].ToString();
					}
					catch (System.NullReferenceException)
					{
						nap = "";
						//MessageBox.Show("No Code at row" + i.ToString());
						//continue;
					}

					oRng = (Excel.Range)xlsheet.Cells[i, 19];
					try
					{
						notes = oRng.Value[Missing.Value].ToString();
					}
					catch (System.NullReferenceException)
					{
						notes = "";
						//MessageBox.Show("No Code at row" + i.ToString());
						//continue;
					}

					string InsertLevel = @"UPDATE HR_absence SET " +
						" sicknessnumber = '" + sicknum +
						"', attachment7 = '" + app7 +
						"', declaration39 = '" + app39 +
						"', additionaldocs = '" + otherapp +
						"', sicknessduration = '" + type +
						"', mkb = '" + mkb +
						"', reasons = '" + reasons +
						"', napdocs = '" + nap +
						"' WHERE numberorder = " + nom + " AND typeabsence = 'Болнични'";

					//int id = this.WritePosition(InsertLevel);
					scom.CommandText = InsertLevel;
					try
					{
						scom.Connection.Open();
						scom.ExecuteNonQuery();
					}
					catch (MySql.Data.MySqlClient.MySqlException ex)
					{
						MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					scom.Connection.Close();
				}
			}

			DataTable dtPersons = new DataTable();

			comm.CommandText = @"SELECT * FROM nkp";

			MySqlDataAdapter dad = new MySql.Data.MySqlClient.MySqlDataAdapter(comm);
			try
			{
				dad.Fill(dtPersons);
				this.dataGrid1.DataSource = dtPersons;
			}
			catch (MySql.Data.MySqlClient.MySqlException ex)
			{
				MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void CheckHolidays(object sender, EventArgs e)
		{

		}


		private class DisPerson
		{

			public string egn;//un_identitycards.		
			public DateTime BirthDate; //un_identitycards.		
			public string FirstName;
			public string sirname;
			public string lastname;
			public string name;
			public int id_gender;
			//assignments
			public DateTime StartDate;
			public DateTime EndDate;
			public int id_educationForm;
			public int id_department;
			public int id_speciality;
		}

		private void buttonGetDis_Click(object sender, EventArgs e)
		{
			Excel.Worksheet xlsheet;
			Excel.Workbook xlwkbook;
			DataTable dtPerson = new DataTable();
			DataTable dtHoliday = new DataTable();
			DataTable dtExisting = new DataTable();

			
			string connSQL = "Database = dis; Data Source = e-university.tu-sofia.bg; User Id = emo; Password = BchochiB; charset = utf8;";
			conn = new MySqlConnection(connSQL);
			comm = new MySqlCommand("SELECT * FROM un_persons", conn);
			da = new MySqlDataAdapter(comm);

			comm.CommandText = "SELECT * FROM hr_person";
			da.SelectCommand.CommandText = "SELECT * FROM un_persons";
			try
			{
				da.Fill(dtPerson);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			this.openFileDialog1.FileName = "*.xls";
			if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				#region Excel
				xlwkbook = (Excel.Workbook)System.Runtime.InteropServices.Marshal.BindToMoniker(openFileDialog1.FileName);
				xlsheet = (Excel.Worksheet)xlwkbook.ActiveSheet;

				Excel.Range oRng;

				int max;
				try
				{
					max = int.Parse(this.textBoxRows.Text);
				}
				catch (System.FormatException)
				{
					MessageBox.Show("Enter value for rows");
					return;
				}

				List<DisPerson> lstExcelPersons = new List<DisPerson>();

				for (int i = 2; i <= max; i++)
				{
					DisPerson person = new DisPerson();
					//name
					string gstr;
					oRng = (Excel.Range)xlsheet.Cells[i, 6];
					try
					{
						gstr = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						continue;
					}
					if (gstr == "")
					{
						continue;
					}
					person.name = gstr;

					var NameArray = person.name.Split(new char[] { ' ' });
					if (NameArray.Count() == 2)
					{
						person.FirstName = NameArray[0];
						person.lastname = NameArray[1];
					}
					if (NameArray.Count() > 2)
					{
						person.FirstName = NameArray[0];
						person.sirname = string.Empty;
						int j = 0;
						for (j = 1; j < NameArray.Count() - 1; j++)
						{
							person.sirname += NameArray[j];
							person.sirname += " ";
						}
						person.sirname.Trim();
						person.lastname = NameArray[j];
					}

					//gender
					int value;
					oRng = (Excel.Range)xlsheet.Cells[i, 7];
					try
					{
						gstr = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						gstr = "0";
					}
					if (int.TryParse(gstr, out value) == false)
					{
						continue;
					}
					person.id_gender = value;

					//egn
					oRng = (Excel.Range)xlsheet.Cells[i, 4];
					try
					{
						gstr = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						gstr = "";
					}
					person.egn = gstr;

					DateTime date;
					oRng = (Excel.Range)xlsheet.Cells[i, 5];
					try
					{
						gstr = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						gstr = "0";
					}

					if (DateTime.TryParse(gstr, out date) == true)
					{
						person.BirthDate = date;
					}

					oRng = (Excel.Range)xlsheet.Cells[i, 8];
					try
					{
						gstr = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						gstr = "0";
					}

					if (DateTime.TryParse(gstr, out date) == true)
					{
						person.StartDate = date;
					}

					oRng = (Excel.Range)xlsheet.Cells[i, 9];
					try
					{
						gstr = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						gstr = "0";
					}

					if (DateTime.TryParse(gstr, out date) == true)
					{
						person.EndDate = date;
					}

					oRng = (Excel.Range)xlsheet.Cells[i, 10];
					try
					{
						gstr = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						gstr = "0";
					}
					if (int.TryParse(gstr, out value) == false)
					{
						continue;
					}
					person.id_educationForm = value;

					oRng = (Excel.Range)xlsheet.Cells[i, 13];
					try
					{
						gstr = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						gstr = "0";
					}
					if (int.TryParse(gstr, out value) == false)
					{
						continue;
					}
					person.id_department = value;

					oRng = (Excel.Range)xlsheet.Cells[i, 3];
					try
					{
						gstr = oRng.get_Value(Missing.Value).ToString();
					}
					catch (System.NullReferenceException)
					{
						gstr = "0";
					}
					if (int.TryParse(gstr, out value) == false)
					{
						continue;
					}
					person.id_speciality = value;

					lstExcelPersons.Add(person);
				}
				#endregion

				foreach (var person in lstExcelPersons)
				{
					//INSERT INTO tbl_name (a,b,c) VALUES(1,2,3,4,5,6,7,8,9);
					string command = string.Format("insert into un_persons (id_gender, firstname, sirname, lastname, id_userEdit, Timestamp) VALUES ({0}, '{1}', '{2}', '{3}', {4}, now())",
																		person.id_gender, person.FirstName, person.sirname, person.lastname, 998);

					comm.CommandText = command;
					try
					{
						comm.Connection.Open();
						comm.ExecuteNonQuery();
					}
					catch (MySql.Data.MySqlClient.MySqlException exx)
					{
						MessageBox.Show(exx.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					comm.Connection.Close();

					//id_studyType = 1
					//id_status = 1
					//id_acceptancereason = 1

					int id = this.GetLastId();

					command = string.Format("insert into ph_assignments (id_studyType, id_status, id_acceptancereason, id_person, id_educationform, id_department, startdate, enddate, Timestamp, id_useredit) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, '{6}-{7}-{8}', '{9}-{10}-{11}', now(), {12})",
																		1, 1, 1, id, person.id_educationForm, person.id_department, person.StartDate.Year, person.StartDate.Month, person.StartDate.Day , person.EndDate.Year, person.EndDate.Month, person.EndDate.Day, 998);

					comm.CommandText = command;
					try
					{
						comm.Connection.Open();
						comm.ExecuteNonQuery();
					}
					catch (MySql.Data.MySqlClient.MySqlException exx)
					{
						MessageBox.Show(exx.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					comm.Connection.Close();

					command = string.Format("insert into un_identitycards (id_egntype, id_person, egn, birthdate, Timestamp, id_useredit) VALUES ({0}, {1}, '{2}', '{3}-{4}-{5}', now(), {6})",
																		1, id, person.egn, person.BirthDate.Year, person.BirthDate.Month, person.BirthDate.Day,  998);

					comm.CommandText = command;
					try
					{
						comm.Connection.Open();
						comm.ExecuteNonQuery();
					}
					catch (MySql.Data.MySqlClient.MySqlException exx)
					{
						MessageBox.Show(exx.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					comm.Connection.Close();
				}
			}
		}
	}
}
