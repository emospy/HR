using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.Win32;
using System.IO;

namespace DataBaseConverter
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

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
				if (components != null) 
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
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Name = "Form1";
			this.Text = "Form1";
			//this.Load += new System.EventHandler(this.Form1_Load);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			RegistryKey rk = Registry.LocalMachine;
			RegistryKey sh1 = rk.OpenSubKey(@"Software\Човешки Ресурси");
            DataTable dt = new DataTable();
            DataTable dtTables = new DataTable();
			string path, password, database, host, connectionString, user;
			path = "";
			try
			{
				path = sh1.GetValue("").ToString();				
			}
			catch(Exception excc)
			{
				MessageBox.Show( excc.Message );
			}
			DataSet ds = new DataSet();
			string ExceptionCarry;
			try
			{
				if( File.Exists( path  + @"\Config.xml" ))
				{
					ds.ReadXml(path + "\\Config.xml", XmlReadMode.InferSchema);
					password = ds.Tables[0].Rows[0]["password"].ToString();
					database = ds.Tables[0].Rows[0]["database"].ToString();
					host = ds.Tables[0].Rows[0]["Host"].ToString();
					//connectionString = "Database=" + database + ";Data Source = " + host + "; User Id=root;Password=" + password + ";charset=utf8;";
					//connectionString = "Database=hrdb;Data Source = 81.161.245.39; User Id=root;Password=tessla;charset=utf8;";
					connectionString = "Database=hrdbr;Data Source = localhost; User Id=root;Password=tess;charset=utf8;";
					MySqlConnection conn = new MySqlConnection(connectionString);
					MySqlCommand comm = new MySqlCommand();
					MySqlDataAdapter da = new MySqlDataAdapter("select * from year", conn);
					comm.Connection = conn;

					
					da.Fill(dt);
	
					if (false)//dt.Columns.Contains("DBVer"))
					{
						switch(dt.Rows[0]["DBVer"].ToString())
						{
							case "2.0":
							//	goto v200;
								break;
							//case "2.1":
							//    goto v201;
							//    break;
						}
					}
					else
					{
						//goto v999;
						#region All old converisons
						try
						{
							comm.Connection = conn;
							comm.CommandText = "DROP TABLE category";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							conn.Close();
						}

						try
						{
							comm.Connection = conn;
							comm.CommandText = "DROP TABLE country";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							conn.Close();
						}

						try
						{
							comm.Connection = conn;
							comm.CommandText = "DROP TABLE dod";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							conn.Close();
						}

						try
						{
							comm.Connection = conn;
							comm.CommandText = "DROP TABLE dshtr";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							conn.Close();
						}

						try
						{
							comm.Connection = conn;
							comm.CommandText = "DROP TABLE dshtrtree";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							conn.Close();
						}

						try
						{
							comm.Connection = conn;
							comm.CommandText = "DROP TABLE educationcatalogue";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							conn.Close();
						}

						try
						{
							comm.Connection = conn;
							comm.CommandText = "DROP TABLE holiday";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							conn.Close();
						}

						try
						{
							comm.Connection = conn;
							comm.CommandText = "DROP TABLE kind";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							conn.Close();
						}

						try
						{
							comm.Connection = conn;
							comm.CommandText = "DROP TABLE profession";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							conn.Close();
						}

						try
						{
							comm.Connection = conn;
							comm.CommandText = "DROP TABLE region";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							conn.Close();
						}

						try
						{
							comm.Connection = conn;
							comm.CommandText = "DROP TABLE sex";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							conn.Close();
						}

						try
						{
							comm.Connection = conn;
							comm.CommandText = "DROP TABLE staff";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							conn.Close();
						}

						try
						{
							comm.Connection = conn;
							comm.CommandText = "DROP TABLE towns";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							conn.Close();
						}

						try
						{
							comm.Connection = conn;
							comm.CommandText = "DROP TABLE type";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							conn.Close();
						}

						try
						{
							comm.Connection = conn;
							comm.CommandText = "ALTER SCHEMA user  DEFAULT CHARACTER SET utf8";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							conn.Close();
						}

						try
						{
							comm.Connection = conn;
							comm.CommandText = "ALTER SCHEMA hrdb  DEFAULT CHARACTER SET utf8";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							conn.Close();
						}

						try
						{
							comm.Connection = conn;
							comm.CommandText = "SHOW TABLES";
							conn.Open();
							da.SelectCommand = comm;
							da.Fill(dtTables);
							conn.Close();
						}
						catch (MySqlException ex)
						{
							conn.Close();
						}

						foreach (DataRow row in dtTables.Rows)
						{
							try
							{
								comm.Connection = conn;
								comm.CommandText = "ALTER TABLE " + row[0].ToString() + " CHARACTER SET utf8 COLLATE utf8_general_ci";
								conn.Open();
								comm.ExecuteNonQuery();
								comm.CommandText = "ALTER TABLE " + row[0].ToString() + " ENGINE = InnoDB";
								comm.ExecuteNonQuery();
								conn.Close();
							}
							catch (MySqlException ex)
							{
								conn.Close();
								continue;
							}

							try
							{
								dt.Clear();
								comm.CommandText = "DESCRIBE " + row[0].ToString();
								conn.Open();
								da.SelectCommand = comm;
								da.Fill(dt);
								conn.Close();
								foreach (DataRow R1 in dt.Rows)
								{
									try
									{
										if (R1["Type"].ToString().ToLower() == "text" || R1["Type"].ToString().ToLower().StartsWith("varchar"))
										{
                                        comm.Connection = conn;
                                        comm.CommandText = "ALTER TABLE " + row[0].ToString() + " CHANGE COLUMN " + R1["Field"].ToString() + " " + R1["Field"].ToString() + " " + R1["Type"].ToString();
										if(R1["Type"].ToString().ToLower() != "text")
											comm.CommandText += " DEFAULT ''";
										conn.Open();
										comm.ExecuteNonQuery();
										conn.Close();
										}
										else if (R1["Type"].ToString().ToLower() == "datetime")
										{
											comm.Connection = conn;
											//ALTER TABLE `Shumen`.`person` CHANGE COLUMN `bornDate` `bornDate` DATETIME NULL DEFAULT '1800-01-01 00:00:00'  ;
											comm.CommandText = "ALTER TABLE " + row[0].ToString() + " CHANGE COLUMN " + R1["Field"].ToString() + " " + R1["Field"].ToString() + " DATETIME NULL DEFAULT NULL";
											conn.Open();
											comm.ExecuteNonQuery();
											conn.Close();

											comm.CommandText = "UPDATE " + row[0].ToString() + " SET " + R1["Field"].ToString() + " = NULL WHERE " + R1["Field"].ToString() + "= '0000-00-00 00:00:00'";
											conn.Open();
											comm.ExecuteNonQuery();
											conn.Close();
										}
									}
									catch (MySqlException ex)
									{
										conn.Close();
									}
								}
                            
							}
							catch (MySqlException ex)
							{
								conn.Close();
								continue;
							}
						}

						try
						{
							DataTable dtUser = new DataTable();
							string connectionStringUser = "Database=user;Data Source = " + host + "; User Id=root;Password=" + password + ";charset=utf8;";
							MySqlConnection connUser = new MySqlConnection(connectionStringUser);
							MySqlCommand commUser = new MySqlCommand();
							MySqlDataAdapter daUser = new MySqlDataAdapter("select * from user", connUser);
							commUser.Connection = connUser;

							daUser.Fill(dtUser);

							comm.CommandText = "CREATE TABLE `users` (`userName` varchar(45) NOT NULL DEFAULT '', `password` varchar(45) DEFAULT NULL, `typeUser` varchar(45) DEFAULT NULL, PRIMARY KEY (`userName`)) ENGINE=INNODB DEFAULT CHARSET=utf8";
							conn.Open();
							comm.ExecuteNonQuery();

							foreach (DataRow row in dtUser.Rows)
							{
								comm.CommandText = "INSERT INTO users (username, password, typeuser) VALUES('" + row["username"].ToString() + "', '" + row["password"].ToString() + "', '" + row["typeuser"].ToString() + "')";
								comm.ExecuteNonQuery();
							}
							conn.Close();

							commUser.CommandText = "DROP SCHEMA User";
							connUser.Open();
							commUser.ExecuteNonQuery();
							connUser.Close();
						}
						catch (MySqlException ex)
						{
							conn.Close();
						}

						try
						{
							comm.Connection = conn;
							comm.CommandText = "ALTER TABLE person MODIFY COLUMN militaryStatus VARCHAR(255)";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							conn.Close();
						}

						try
						{
							comm.CommandText = "UPDATE person SET militarystatus = 'Отслужил' WHERE MilitaryStatus = '1'";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							conn.Close();
						}
						try
						{
							comm.CommandText = "UPDATE person SET militarystatus = 'Неотслужил' WHERE MilitaryStatus = '0'";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							conn.Close();
						}
						try
						{
							comm.CommandText = "ALTER TABLE personassignment ADD COLUMN exported INTEGER UNSIGNED";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}
						try
						{
							comm.CommandText = "ALTER TABLE admininfo ADD COLUMN NKIDLevel TEXT";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}
						try
						{
							comm.CommandText = "ALTER TABLE admininfo MODIFY COLUMN NKIDLevel TEXT";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}
						try
						{
							comm.CommandText = "ALTER TABLE admininfo ADD COLUMN NKIDCode VARCHAR(255)";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}
						try
						{
							comm.CommandText = "ALTER TABLE personassignment ADD COLUMN pcontractreasoncode VARCHAR(45)";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE fired ADD COLUMN fireorder VARCHAR(45)";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE reasonassignment ADD COLUMN pcontractreasoncode VARCHAR(45)";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE reasonassignment ADD COLUMN substitute INTEGER";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE personassignment ADD COLUMN substitute INTEGER";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE person ADD COLUMN Other TEXT";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE person CHANGE COLUMN `positionID` `nodeID` INT(11) NULL DEFAULT NULL";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE person ADD COLUMN egnlnch INTEGER DEFAULT 0";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}
						try
						{
							comm.CommandText = "ALTER TABLE person MODIFY COLUMN egnlnch INTEGER DEFAULT 0";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE person ADD COLUMN engname varchar(255) DEFAULT ''";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}
						try
						{
							comm.CommandText = "ALTER TABLE person MODIFY COLUMN engname varchar(255) DEFAULT ''";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE person ADD COLUMN engeducation varchar(255) DEFAULT ''";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE person ADD COLUMN workbook varchar(255) DEFAULT ''";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE person ADD COLUMN `workbookdate` DATETIME NULL";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE person MODIFY COLUMN engeducation varchar(255) DEFAULT ''";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE person MODIFY COLUMN region varchar(255) DEFAULT ''";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE person MODIFY COLUMN modifiedByUser varchar(255) DEFAULT ''";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						//try
						//{
						//    comm.CommandText = "ALTER TABLE person MODIFY COLUMN fired varchar(255) DEFAULT ''";
						//    conn.Open();
						//    comm.ExecuteNonQuery();
						//    conn.Close();
						//}
						//catch (MySqlException ex)
						//{
						//    ExceptionCarry = ex.Message;
						//    conn.Close();
						//}

						//try
						//{
						//    comm.CommandText = "UPDATE person SET fired = false WHERE fired = '0'";
						//    conn.Open();
						//    comm.ExecuteNonQuery();
						//    conn.Close();
						//}
						//catch (MySqlException ex)
						//{
						//    ExceptionCarry = ex.Message;
						//    conn.Close();
						//}

						//try
						//{
						//    comm.CommandText = "UPDATE person SET fired = true WHERE fired = '1'";
						//    conn.Open();
						//    comm.ExecuteNonQuery();
						//    conn.Close();
						//}
						//catch (MySqlException ex)
						//{
						//    ExceptionCarry = ex.Message;
						//    conn.Close();
						//}

						try
						{
							comm.CommandText = "ALTER TABLE worktime ADD COLUMN staff FLOAT";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE personassignment ADD COLUMN staff varchar(45) DEFAULT 1";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE personassignment ADD COLUMN exported varchar(45) DEFAULT 0";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}
						try
						{
							comm.CommandText = "ALTER TABLE personassignment ADD COLUMN level1eng varchar(45) DEFAULT ''";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}
						try
						{
							comm.CommandText = "ALTER TABLE personassignment ADD COLUMN level2eng varchar(45) DEFAULT ''";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}
						try
						{
							comm.CommandText = "ALTER TABLE personassignment ADD COLUMN level3eng varchar(45) DEFAULT ''";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}
						try
						{
							comm.CommandText = "ALTER TABLE personassignment ADD COLUMN level4eng varchar(45) DEFAULT ''";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}
						try
						{
							comm.CommandText = "ALTER TABLE personassignment ADD COLUMN positioneng varchar(45) DEFAULT ''";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE personassignment ADD COLUMN tutorname varchar(255) DEFAULT ''";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE personassignment ADD COLUMN tutorabsencereason varchar(255) DEFAULT ''";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.Connection = conn;
							comm.CommandText = "ALTER TABLE penalty MODIFY COLUMN numberorder VARCHAR(255)";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							conn.Close();
						}
						
						try
						{
							comm.CommandText = "ALTER TABLE penalty ADD COLUMN isBonus VARCHAR(45) NULL DEFAULT '' AFTER typePenalty;";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE options ADD COLUMN vacancystash INTEGER UNSIGNED DEFAULT 0";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "update reasonassignment set substitute = 1 where id = 4";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE options ADD COLUMN firedsignal INTEGER UNSIGNED DEFAULT 0";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE options ADD COLUMN personorder VARCHAR(45) DEFAULT ''";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE education ADD COLUMN englevel VARCHAR(255) DEFAULT ''";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE globalpositions ADD COLUMN engposition VARCHAR(255) DEFAULT ''";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE notestable ADD COLUMN typedocument VARCHAR(255) DEFAULT ''";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE newtree2 ADD COLUMN leveleng VARCHAR(255) DEFAULT ''";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE newtree2 MODIFY COLUMN leveleng VARCHAR(255) DEFAULT ''";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE languagelevel ADD COLUMN id INT NOT NULL AUTO_INCREMENT  AFTER Level, ADD PRIMARY KEY (id)";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}
						catch (Exception ex)
						{
							conn.Close();
						}

						#region Code for Shumen adding salaryAddons in firmpersonal3
						try
						{
							comm.CommandText = "ALTER TABLE firmpersonal3 ADD COLUMN StartSalary VARCHAR(45)";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE firmpersonal3 MODIFY COLUMN StaffCount VARCHAR(45)";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE firmpersonal3 ADD COLUMN BaseSalary VARCHAR(45)";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE firmpersonal3 ADD COLUMN SalaryAddon VARCHAR(45)";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE firmpersonal3 ADD COLUMN ScienceAddon VARCHAR(45)";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE firmpersonal3 ADD COLUMN OtherAddon VARCHAR(45)";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE firmpersonal3 ADD COLUMN positioneng VARCHAR(255)";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE person ADD COLUMN other1 VARCHAR(255)";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE person ADD COLUMN other2 VARCHAR(255)";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE person ADD COLUMN other3 VARCHAR(255)";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE person ADD COLUMN other4 VARCHAR(255)";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE person ADD COLUMN other5 VARCHAR(255)";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}
						#endregion


						#region Code for Attestations Now working with fully new table
						//					try
						//					{
						//						comm.CommandText = "ALTER TABLE attestations ADD COLUMN hasFinalMeeting VARCHAR(45)";
						//						conn.Open();
						//						comm.ExecuteNonQuery();
						//						conn.Close();
						//					}
						//					catch(MySqlException ex)
						//					{
						//						ExceptionCarry = ex.Message;
						//						conn.Close();
						//					}
						//					try
						//					{
						//						comm.CommandText = "ALTER TABLE attestations ADD COLUMN FinalMeetingDate DATETIME";
						//						conn.Open();
						//						comm.ExecuteNonQuery();
						//						conn.Close();
						//					}
						//					catch(MySqlException ex)
						//					{
						//						ExceptionCarry = ex.Message;
						//						conn.Close();
						//					}
						#endregion

						try
						{
							comm.CommandText = "ALTER TABLE year ADD COLUMN id INTEGER DEFAULT 0";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE personassignment ADD COLUMN tutorname VARCHAR(255) DEFAULT ''";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE personassignment ADD COLUMN tutorabsencereason  VARCHAR(255) DEFAULT ''";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE nkid ADD COLUMN id INTEGER UNSIGNED NOT NULL DEFAULT NULL AUTO_INCREMENT, ADD PRIMARY KEY (`id`)";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "ALTER TABLE notestable ADD COLUMN modifiedbyuser VARCHAR(255) NOT NULL DEFAULT ''";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "CREATE TABLE AttachedDocs (id INT NOT NULL AUTO_INCREMENT , parent INT NULL , link VARCHAR(255) NULL , dateadded DATETIME NULL, typedocument VARCHAR(255) NULL , PRIMARY KEY (id) )";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}

						try
						{
							comm.CommandText = "REPAIR TABLE nkp";
							conn.Open();
							comm.ExecuteNonQuery();
							conn.Close();
						}
						catch (MySqlException ex)
						{

							ExceptionCarry = ex.Message;
							conn.Close();

						}

						try
						{
							DataTable dtPosCorr = new DataTable();
							da.SelectCommand.CommandText = "SELECT * FROM person LEFT JOIN personassignment ON (person.id = personassignment.parent AND personassignment.isactive = 1) WHERE person.fired = 0 AND personassignment.isactive IS NULL";
							da.Fill(dtPosCorr);

							foreach (DataRow R in dtPosCorr.Rows)
							{
								comm.CommandText = "UPDATE person SET positionid = 0 WHERE id = " + R["id"].ToString();
								conn.Open();
								comm.ExecuteNonQuery();
								conn.Close();
							}

						}
						catch (MySqlException ex)
						{
							ExceptionCarry = ex.Message;
							conn.Close();
						}
						catch (Exception ex)
						{
							conn.Close();
						}

						#endregion

						string[] Nom2Tables = new string[] 
						{
							"contract",
							"experience",
							"familystatus",
							"language",
							"languageknowledge",
							"law",
							"militaryrang",
							"militarystatus",
							"nkpclass",
							"penaltyreason",
							"rang",
							"reasonfired",
							"sciencelevel",
							"sciencetitle",
							"typepenalty",
							"yearlyaddon"
						};


						try
						{
							comm.CommandText = "CREATE TABLE `joinnomenklature` ( `id` int(11) NOT NULL AUTO_INCREMENT, `level` varchar(255) DEFAULT '', `descriptor` varchar(255) DEFAULT '', PRIMARY KEY (`id`)) ENGINE=MyISAM DEFAULT CHARSET=utf8";
							comm.Connection.Open();
							comm.ExecuteNonQuery();
							comm.Connection.Close();

							foreach (string S in Nom2Tables)
							{
								dt.Rows.Clear();
								da.SelectCommand.CommandText = "SELECT * FROM " + S;
								da.Fill(dt);
								foreach (DataRow row in dt.Rows)
								{
									comm.CommandText = "INSERT INTO JoinNomenklature (level, descriptor) VALUES('" + row["level"] + "','" + S + "')";
									comm.Connection.Open();
									comm.ExecuteNonQuery();
									comm.Connection.Close();
								}
							}
							foreach (string S in Nom2Tables)
							{
								comm.CommandText = "DROP TABLE " + S;
								comm.Connection.Open();
								comm.ExecuteNonQuery();
								comm.Connection.Close();
							}
						}
						catch
						{
						}
					}
					FixFuckedAssignmentCounters(comm);
				}
			}
			catch(Exception exc )
			{
				MessageBox.Show( exc.Message );
				System.Diagnostics.Debug.Write( "\\n" + exc.Message );
			}
		}

        static void FixFuckedAssignmentCounters(MySqlCommand comm)
        {
            DataTable FirmStructure;
            DataTable Assignments;

            FirmStructure = SelectAllFromTable("firmpersonal3", comm);
            Assignments = SelectAllFromTable("personassignment", comm);

            foreach (DataRow FirmRow in FirmStructure.Rows)
            {
                float busyCount = 0, busy, free, total;
                int parid;
                bool conv;
                conv = float.TryParse(FirmRow["busy"].ToString(), out busy);
                if (conv == false)
                {
                    busy = 0;
                }
                conv = float.TryParse(FirmRow["free"].ToString(), out free);
                if (conv == false)
                {
                    free = 0;
                }
                conv = float.TryParse(FirmRow["staffcount"].ToString(), out total);
                if (conv == false)
                {
                    total = 0;
                }
                conv = int.TryParse(FirmRow["id"].ToString(), out parid);
                if (conv == false)
                {
                    parid = 0;
                }
                foreach (DataRow AssRow in Assignments.Rows)
                {
                    int posid, active, substitute;
                    float staffcount;
                    conv = int.TryParse(AssRow["positionid"].ToString(), out posid);
                    if (conv == false)
                    {
                        posid = 0;
                    }
                    conv = int.TryParse(AssRow["isactive"].ToString(), out active);
                    if (conv == false)
                    {
                        active = 0;
                    }
                    conv = int.TryParse(AssRow["substitute"].ToString(), out substitute);
                    if (conv == false)
                    {
                        substitute = 0;
                    }
                    conv = float.TryParse(AssRow["staff"].ToString(), out staffcount);
                    if (conv == false)
                    {
                        staffcount = 0;
                    }
                    if (active > 0 && posid == parid && substitute == 0)
                    {
                        busyCount += staffcount;
                    }
                }
                if ((busy != busyCount) || (free != (total - busyCount)))
                {
                    float newfree, newbusy;
                    newfree = total - busyCount;
                    newbusy = busyCount;
                    FixStaff(newfree, newbusy, parid, comm);
                }
            }
        }

        static DataTable SelectAllFromTable(string table, MySqlCommand comm)
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da;
            da = new MySql.Data.MySqlClient.MySqlDataAdapter(comm);
            da.SelectCommand = comm;
            comm.CommandText = "SELECT * FROM " + table + " ORDER BY id";
            try
            {
                da.Fill(dt);
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        static void FixStaff(float newfree, float newbusy, int id, MySqlCommand comm)
        {
            string command;

            command = "UPDATE firmpersonal3 set free = " + newfree.ToString() + ", busy = " + newbusy.ToString() + " where id = " + id.ToString();

            comm.CommandText = command;
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
        }
	}
}
