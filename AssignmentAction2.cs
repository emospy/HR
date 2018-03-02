using System;
using System.Data;
using System.Windows.Forms;

namespace DataLayer
{
	public class AssignmentAction
	{
		MySql.Data.MySqlClient.MySqlCommand comm;
		MySql.Data.MySqlClient.MySqlConnection conn;		
		MySql.Data.MySqlClient.MySqlDataAdapter da;
		MySql.Data.MySqlClient.MySqlCommandBuilder cb;
		
		string table;
		public void DeleteRow( string ID, string ID2 )
		{
			this.comm.CommandText = "DELETE FROM "+ this.table + " WHERE ID=" + ID + "'";
			try
			{
				this.comm.Connection.Open();			
				this.comm.ExecuteNonQuery();
			}
			catch(MySql.Data.MySqlClient.MySqlException e)
			{
				MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			this.comm.Connection.Close();
		}
		public void UpdateDataAdapter( DataTable dt )
		{
			this.da = new MySql.Data.MySqlClient.MySqlDataAdapter( "SELECT * FROM "+ this.table , this.conn );
			this.cb = new MySql.Data.MySqlClient.MySqlCommandBuilder( this.da );
			try
			{
				this.conn.Open();			
				this.da.Update( dt );
			}
			catch(MySql.Data.MySqlClient.MySqlException e)
			{
				MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			this.conn.Close();
		}

		public void UpdateAssignment( AssignmentPackage package)
		{
			string command;
			
			command = "UPDATE " + this.table + 
			" SET level1 = '" + package.Level1 +
			"',level2 = '" + package.Level2 + 
			"',level3 = '" + package.Level3 + 
			"',position = '" + package.Position +
			"',contract = '" + package.Contract + 
			"',worktime = '" + package.WorkTime + 
			"',assignedat = '" + package.AssignedAt.Year + "-" + package.AssignedAt.Month + "-" + package.AssignedAt.Day + 
			"',assignreason = '" + package.AssignReason + 
			"',staff = '" + package.Staff + 
			"',contractnumber = '" + package.ContractNumber + 
			"',contractexpiry = '" + package.ContractExpiry.Year + "-" + package.ContractExpiry.Month + "-" + package.ContractExpiry.Day + 
			"',numberkids = '" + package.NumberKids + 
			"',basesalary = '" + package.BaseSalary + 
			"',salaryaddon = '" + package.SalaryAddon + 
			"',classpercent = '" + package.ClassPercent + 
			"',modifiedByUser = '" + package.User + 
			"',Years = '" + package.Years.ToString() +
			"',Months = '" + package.Months.ToString() +
			"',Days = '" + package.Days.ToString() +
			"' WHERE id = " + package.ID.ToString();

			comm.CommandText = command;
			try
			{
				comm.Connection.Open();
				comm.ExecuteNonQuery();
			}
			catch(MySql.Data.MySqlClient.MySqlException e)
			{
				MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			comm.Connection.Close();	
		
		}
		public DataTable SelectBasicDataFromFirmPersonal( int ID )
		{
			//Чрез тази функция се взимат основните данни за попълването на датагрида
			// Връща всички видове длъжности в Организацията
			// споразумения
			DataTable dt = new DataTable();

			this.comm.CommandText = @"SELECT * FROM " + this.table + " WHERE parent='" + ID.ToString() +"'";
			this.da = new MySql.Data.MySqlClient.MySqlDataAdapter( this.comm );
			try
			{
				this.da.Fill( dt );
			}
			catch(MySql.Data.MySqlClient.MySqlException e)
			{
				MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return dt;
		}

		public DataTable SelectBasicDataForPersonAssignment( int ID, bool IsAssignment)
		{
			//Чрез тази функция се взимат основните данни за попълването на датагрида
			// Връща или всички назначения на човека, или всички допълнителни 
			// споразумения
			DataTable dt = new DataTable();
			int t=0;
			if( IsAssignment )
			{
				t=1;
			}
			this.comm.CommandText = "SELECT * FROM " + this.table + " WHERE parent=" + ID.ToString() +" AND IsAdditionalAssignment = " + t.ToString();
			this.da = new MySql.Data.MySqlClient.MySqlDataAdapter( this.comm );
			try
			{
				this.da.Fill( dt );
			}
			catch(MySql.Data.MySqlClient.MySqlException e)
			{
				MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return dt;
		}

		public AssignmentAction(string usedTable, string connString )
		{
			this.table = usedTable;
			conn = new MySql.Data.MySqlClient.MySqlConnection();
			conn.ConnectionString = connString;
			comm = new MySql.Data.MySqlClient.MySqlCommand();
			comm.Connection = conn;

		}	
	}
}

//create table PersonAssignment ( contractType varchar(255), nkidCode varchar(255), nkidLevel varchar(255), nkpCode varchar(255), nkpLevel varchar(255), nkdsCode varchar(255), nkdsLevel text, classPercent varchar(255), salaryAddon varchar(255), baseSalary varchar(255), numberKids varchar(255), contractExpiry datetime, contractNumber varchar(255), staff varchar(255), assignReason varchar(255), assignedAt datetime, worktime varchar(255), contract varchar(255), position varchar(255), level3 varchar(255), level2 varchar(255), level1 varchar(255), IsAdditionalAssignment tinyint, parent int, isActive tinyint, id int not null auto_increment, primary key (id) );