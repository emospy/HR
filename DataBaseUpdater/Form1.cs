using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
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
		private static void Main()
		{
			//RegistryKey rk = Registry.LocalMachine;
			//RegistryKey sh1 = rk.OpenSubKey(@"Software\Човешки Ресурси)";
			DataTable dt = new DataTable();
			DataTable dtTables = new DataTable();
			string path, password, database, host, connectionString, user;
			path = "";
			//try
			//{
			//    path = sh1.GetValue(")".ToString();
			//}
			//catch (Exception excc)
			//{
			//    MessageBox.Show(excc.Message);
			//}
			DataSet ds = new DataSet();
			string ExceptionCarry;
			try
			{
				if (File.Exists("Config.xml"))
				{
					//ds.ReadXml(path + "\\Config.xml", XmlReadMode.InferSchema);//0895752710 Manev
					ds.ReadXml("Config.xml", XmlReadMode.InferSchema);
					password = ds.Tables[0].Rows[0]["password"].ToString();
					database = ds.Tables[0].Rows[0]["database"].ToString();
					host = ds.Tables[0].Rows[0]["Host"].ToString();
					connectionString = "Database=" + database + ";Data Source = " + host + "; User Id=sa;Password=" + password;
					
					//connectionString = "Database=hrdb;Data Source = localhost; User Id=root;Password=tess;charset=utf8;";
					SqlConnection conn = new SqlConnection(connectionString);
					SqlCommand comm = new SqlCommand();
					SqlDataAdapter da = new SqlDataAdapter("select * from hr_year", conn);
					comm.Connection = conn;
					
					try
					{
						comm.CommandText = "ALTER TABLE HR_plannedHolidays ADD Leftover int null";
						conn.Open();
						comm.ExecuteNonQuery();
						conn.Close();
					}
					catch (SqlException ex)
					{
						ExceptionCarry = ex.Message;
						conn.Close();
					}

                    try
                    {
                        comm.CommandText = "ALTER TABLE HR_personassignment ALTER COLUMN basesalary float";
                        conn.Open();
                        comm.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (SqlException ex)
                    {
                        ExceptionCarry = ex.Message;
                        conn.Close();
                    }

                    try
                    {
                        comm.CommandText = "ALTER TABLE HR_newtree2 ALTER COLUMN TreeOrder int null";
                        conn.Open();
                        comm.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (SqlException ex)
                    {
                        ExceptionCarry = ex.Message;
                        conn.Close();
                    }

					
				}
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message);
				System.Diagnostics.Debug.Write("\\n" + exc.Message);
			}
		}


		//private static DataTable SelectAss(string table, MySqlCommand comm)
		//{
		//	DataTable dt = new DataTable();
		//	SqlDataAdapter da;
		//	da = new MySql.Data.MySqlClient.MySqlDataAdapter(comm);
		//	da.SelectCommand = comm;
		//	comm.CommandText = "SELECT personassignment.id, year_holiday.total FROM personassignment left join year_holiday on personassignment.parent = year_holiday.parent and year = 2012 and isactive = 1";
		//	try
		//	{
		//		da.Fill(dt);
		//	}
		//	catch (MySql.Data.MySqlClient.MySqlException e)
		//	{
		//		MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//	}
		//	return dt;
		//}

		//private static DataTable SelectHol(string table, MySqlCommand comm)
		//{
		//	DataTable dt = new DataTable();
		//	MySqlDataAdapter da;
		//	da = new MySql.Data.MySqlClient.MySqlDataAdapter(comm);
		//	da.SelectCommand = comm;
		//	comm.CommandText = "SELECT * FROM " + table + " ORDER BY id";
		//	try
		//	{
		//		da.Fill(dt);
		//	}
		//	catch (MySql.Data.MySqlClient.MySqlException e)
		//	{
		//		MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//	}
		//	return dt;
		//}

		//private static DataTable SelectAllFromTable(string table, MySqlCommand comm)
		//{
		//	DataTable dt = new DataTable();
		//	MySqlDataAdapter da;
		//	da = new MySql.Data.MySqlClient.MySqlDataAdapter(comm);
		//	da.SelectCommand = comm;
		//	comm.CommandText = "SELECT * FROM " + table + " ORDER BY id";
		//	try
		//	{
		//		da.Fill(dt);
		//	}
		//	catch (MySql.Data.MySqlClient.MySqlException e)
		//	{
		//		MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//	}
		//	return dt;
		//}

		//private static void FixStaff(float newfree, float newbusy, int id, MySqlCommand comm)
		//{
		//	string command;

		//	command = "UPDATE firmpersonal3 set free = " + newfree.ToString() + ", busy = " + newbusy.ToString() + " where id = " + id.ToString();

		//	comm.CommandText = command;
		//	try
		//	{
		//		comm.Connection.Open();
		//		comm.ExecuteNonQuery();
		//	}
		//	catch (MySql.Data.MySqlClient.MySqlException e)
		//	{
		//		MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//	}
		//	comm.Connection.Close();
		//}

		//static private int WritePosition(string commandtext)
		//{
		//	MySqlCommand comm = new MySql.Data.MySqlClient.MySqlCommand();
		//	comm.Connection = conn;

		//	comm.CommandText = commandtext;
		//	try
		//	{
		//		comm.Connection.Open();
		//		comm.ExecuteNonQuery();
		//	}
		//	catch (MySql.Data.MySqlClient.MySqlException e)
		//	{
		//		MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//	}
		//	comm.Connection.Close();
		//	return 0;
		//}

		//private static void ExecuteScript(MySqlCommand comm, MySqlConnection conn, string filename)
		//{
		//	string ExceptionCarry;
		//	FileInfo file = new FileInfo(filename);

		//	string script = file.OpenText().ReadToEnd();

		//	try
		//	{
		//		comm.CommandText = script;
		//		conn.Open();
		//		comm.ExecuteNonQuery();
		//		conn.Close();
		//		file.OpenText().Close();
		//	}
		//	catch (MySqlException ex)
		//	{
		//		ExceptionCarry = ex.Message;
		//		conn.Close();
		//		file.OpenText().Close();
		//	}
		//}
	}
}
