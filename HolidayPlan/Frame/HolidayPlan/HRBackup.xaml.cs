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
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace HolidayPlan
{
	/// <summary>
	/// Interaction logic for Backup.xaml
	/// </summary>
	public partial class HRBackup : Window
	{
		ServerConnection sconn;
		string database;
		string connString;
		List<string> lstBackups = new List<string>();
		string backUpDir;

		string servername, user, pass;
		
		public HRBackup(string Server, string db, string connstring, string user, string pass)
		{
			InitializeComponent();
			sconn = new ServerConnection(Server, user, pass);
			this.database = db;
			this.connString = connstring;

			this.servername = Server;
			this.user = user;
			this.pass = pass;

			Server srv = new Server(sconn);
			this.backUpDir = string.Format(@"{0}\{1}", srv.BackupDirectory, database) ;

			
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
			this.txtBackupName.Text = string.Format("HRBackup{0}{1:00}{2:00}",DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);
		}

		private void btnCreateBackup_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				bool over = false;
				sconn = new ServerConnection(this.servername, this.user, this.pass);
				Server server = new Server(sconn);
				Backup backup = new Backup();
				string bname = this.backUpDir + @"\" + this.txtBackupName.Text + ".bak";
				if (this.dgBackups.Items.Contains(bname) == true)
				{
					if (MessageBox.Show("Такъв архив вече съществува. Желаете ли да го презапишете?", "Въпрос", MessageBoxButton.YesNo) == MessageBoxResult.No)
					{
						return;
					}
					over = true;
				}
				
				backup.Devices.AddDevice(bname, DeviceType.File);
				backup.Database = this.database;
				backup.Action = BackupActionType.Database;
				backup.BackupSetDescription = "Backup" + DateTime.Now.ToString();
				backup.BackupSetName = "HRBackup";
				backup.Incremental = false;
				backup.LogTruncation = BackupTruncateLogType.Truncate;
				backup.Initialize = true; // supposed to overwrite
				backup.SqlBackup(server);
				if (over == false)
				{
					lstBackups.Add(bname);
					this.dgBackups.Items.Refresh();
				}
				MessageBox.Show("Успешно архивиране");
			}
			catch(Exception ex)
			{
				MessageBox.Show("Неуспешно създаване на архив");
				MessageBox.Show(ex.Message);
			}
		}
	}
}
