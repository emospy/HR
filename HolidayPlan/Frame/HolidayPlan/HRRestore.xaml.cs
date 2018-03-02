using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace HolidayPlan
{
	/// <summary>
	/// Interaction logic for HRRestore.xaml
	/// </summary>
	public partial class HRRestore : Window
	{
		string servername, user, pass;
		string database;
		string connString;
		List<string> lstBackups = new List<string>();
		string backUpDir;
		ServerConnection sconn;

		public HRRestore(string Server, string db, string connstring, string user, string pass)
		{
			InitializeComponent();
			this.servername = Server;
			this.database = db;
			this.connString = connstring;

			this.user = user;
			this.pass = pass;

			this.sconn = new ServerConnection(Server, user, pass);
			Server srv = new Server(sconn);
			this.backUpDir = string.Format(@"{0}\{1}", srv.BackupDirectory, database);


			SqlConnection conn = new SqlConnection(this.connString);
			SqlCommand comm = new SqlCommand();
			SqlDataAdapter da = new SqlDataAdapter();
			DataTable dt = new DataTable();
			SqlDataReader rdr = null;
			da.SelectCommand = comm;
			comm.Connection = conn;

			conn.Open();
			comm.CommandText = string.Format("exec xp_dirtree '{0}\', 1, 1", backUpDir);
			rdr = comm.ExecuteReader();
			while (rdr.Read())
			{
				lstBackups.Add(rdr["subdirectory"].ToString());
			}
			conn.Close();

			lstBackups.Sort();
			this.dgBackups.ItemsSource = lstBackups;
		}

		private void btnRestoreBackup_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.dgBackups.SelectedItem == null)
				{
					MessageBox.Show("Моля, изберете архив за възстановяване");
					return;
				}

				BackupDeviceItem bk = new BackupDeviceItem(this.backUpDir + @"\" + (string)this.dgBackups.SelectedItem, DeviceType.File);
				Server srv = new Server(sconn);
				Restore res = new Restore();
				res.NoRecovery = false;
				res.Devices.Add(bk);
				res.Database = this.database;
				
				srv.KillAllProcesses(this.database);
				srv.Databases[this.database].Drop();
				res.SqlRestore(srv);
				MessageBox.Show("Успешно възстановване");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Проблем при възстановяване на базата дани. Възможно е в момента базата данни да е в неопределено състояние. Моля, свържете се с поддръжката.");
				MessageBox.Show(ex.Message);
			}
		}
	}
}
