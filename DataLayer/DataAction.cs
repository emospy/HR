using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Text;
using System.Linq;
using System.Data.Common;
//using System.Data.SqlClient;
using HRDataLayer;

namespace DataLayer
{

	public enum ComparisonOperators
	{
		eEqual = 0,
		eGreater = 1,
		eLess = -1
	}

	public class DataAction
	{
		DbCommand comm;
		DbConnection conn;
		DataAdapter da;
		
		DbTransaction tran;

		public static string ConvertDateTimeToMySql(DateTime date)
		{
			string s;
			
			s = string.Format("'{0}-{1}-{2}'", date.Year, date.Month, date.Day);
			return s;
		}

		public static string DateComparison(DateTime Date, ComparisonOperators Operator, string TableName, string colName)
		{
			string Op;
			string Ops;
			if (Operator == ComparisonOperators.eEqual)
			{
				Op = " == ";
				Ops = " == ";
			}
			else if (Operator == ComparisonOperators.eGreater)
			{
				Op = " >= ";
				Ops = " > ";
			}
			else
			{
				Op = " <= ";
				Ops = " < ";
			}

			//(Year(ExpiredDate) < {2} OR ( Year(ExpiredDate) = {2} AND Month(ExpiredDate) < {3}) OR (Year(ExpiredDate) = {2} AND Month(ExpiredDate) = {3} AND Day(ExpiredDate) < {4}))
			string result = string.Format("( Year({1}.{5}) {6} {2} OR ( Year({1}.{5}) = {2} AND Month({1}.{5}) {6} {3}) OR (Year({1}.{5}) = {2} AND Month({1}.{5}) = {3} AND Day({1}.{5}) {0} {4}) )", Op, TableName, Date.Year, Date.Month, Date.Day, colName, Ops);
			return result;
		}

		public DataTable SelectWhere(string table, string columns, string whereStatement)
		{
			DataTable dt = new DataTable();
			DataSet ds = new DataSet();

			this.comm.CommandText = @"SELECT " + columns;
			this.comm.CommandText += " FROM " + table;
			this.comm.CommandText += " " + whereStatement;

			CreateDataAdapter();
			try
			{
				this.comm.Connection.Open();
				this.da.Fill(ds);
				dt = ds.Tables[0];
			}
			catch (SqlException e)
			{
				MessageBox.Show(e.Message, " Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
				dt = null;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
			this.comm.Connection.Close();
			return dt;
		}

		public DataTable OneJoin(ArrayList column, string join_clause, string where_clause, bool IsFired)
		{
			try
			{
				DataSet ds = new DataSet();
				System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
				CreateDataAdapter();

				for (int i = 0; i < column.Count; i++)
				{
					sb2.Append("," + column[i]);
				}
				//if (DataBaseType == DBTypes.MsSql)
				//{
					if (where_clause == "")
					{
						this.comm.CommandText = string.Format("SELECT {0}.name {1} FROM {0} {2} ", TableNames.Person, sb2.ToString(), join_clause);
						if (IsFired)
						{
							this.comm.CommandText += "WHERE fired = 1";
						}
						else
						{
							this.comm.CommandText += "WHERE fired = 0";
						}
					}
					else
					{
						this.comm.CommandText = string.Format("SELECT {0}.name {1} FROM {0} {2} WHERE {3}", TableNames.Person, sb2.ToString(), join_clause, where_clause);
						if (IsFired)
						{
							this.comm.CommandText += "AND fired = 1";
						}
						else
						{
							this.comm.CommandText += "AND fired = 0";
						}
					}

				try
				{
					this.da.Fill(ds);
				}
				catch (SqlException e)
				{
					MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return null;
				}
				return ds.Tables[0];
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				ErrorLog.WriteMessage(ex.Message);
				return null;
			}
		}

		public DataTable SelectAllPersonBySpecificID(ArrayList arrID, ArrayList column, bool IsFired, bool allPersons, string language, bool english)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
			DataTable dt = new DataTable();
			DataSet ds = new DataSet();
			string fired = "0";
			if (IsFired)
			{
				fired = "1";
			}
			else
			{
				fired = "0";
			}
			if (arrID.Count < 1)
			{
				if (allPersons == false)
				{
					return dt;
				}
			}
			CreateDataAdapter();

			for (int i = 0; i < column.Count; i++)
			{
				sb2.Append("," + column[i]);
			}

			string or = " OR ";
			string and = " AND ";
			int counter = 0;
			sb.Append(" ( ");
			foreach (DataLayer.IdContatiner container in arrID)
			{
				counter++;
				if (counter >= arrID.Count)
				{
					or = "";
				}
				sb.Append("( ( " + TableNames.Person + ".ID=");
				sb.Append(container.id + " ) ");
				if (container.HavePenalty)
				{
					sb.Append(and);
					sb.AppendFormat(" ( " + TableNames.Penalty + ".id = {0} )", container.penaltyId);
				}
				if (container.HaveAbsence)
				{
					sb.Append(and);
					sb.AppendFormat(" ( " + TableNames.Absence + ".id = {0} )", container.absenceId);
				}
				if (container.IsAttestationYear)
				{
					sb.Append(and);
					sb.AppendFormat(" ( " + TableNames.Attestations + ".id = {0} )", container.attestationId);
				}
				sb.Append(" ) " + or);
			}
			sb.Append(" ) ");
			// ( (person.ID = 43) AND (penalty.id = 32) AND (absence.id = 21) ) OR ()
			//this.comm.CommandText = @"SELECT * FROM person WHERE "+ sb.ToString();
			//SELECT Person.name ,Person.id,Personassignment.level1,Personassignment.level2,Personassignment.level3,Personassignment.level4 FROM person left join personassignment on person.id = personassignment.id WHERE  person.ID=30 order by id
			//SELECT Person.name ,Person.id FROM person left join languagelevel on person.id = languagelevel.parent WHERE  person.ID=30 order by id
			if (allPersons)
			{
				this.comm.CommandText = string.Format(@"SELECT {0}.ID, {0}.name, {1}.level1, {1}.level2, {1}.level3, {1}.level4, {1}.position ", TableNames.Person, TableNames.PersonAssignment);

				if (english)
				{
					this.comm.CommandText += string.Format(@",{0}.engname, {0}.engeducation, {1}.level1eng, {1}.level2eng, {1}.level3eng, {1}.level4eng, {1}.positioneng ", TableNames.Person, TableNames.PersonAssignment);
				}
				this.comm.CommandText += string.Format("{6} FROM {0} left join {2} on {0}.id = {2}.parent left join {1} on {0}.id = {1}.parent left join {3} on {0}.id = {3}.parent  left join {4} on {0}.id = {4}.parent  left join {5} on {0}.id = {5}.par WHERE {0}.fired = {7} AND {1}.isActive = 1 ", TableNames.Person, TableNames.PersonAssignment, TableNames.LanguageLevel, TableNames.Absence, TableNames.Penalty, TableNames.Attestations, sb2.ToString(), fired);

			}
			else
			{
				if (!IsFired)
				{
					this.comm.CommandText = string.Format(@"SELECT {0}.ID, {0}.name, {1}.level1, {1}.level2, {1}.level3, {1}.level4, {1}.position ", TableNames.Person, TableNames.PersonAssignment);
					if (english)
					{
						this.comm.CommandText += string.Format(@",{0}.engname, {0}.engeducation, {1}.level1eng, {1}.level2eng, {1}.level3eng, {1}.level4eng, {1}.positioneng ", TableNames.Person, TableNames.PersonAssignment);
					}
					this.comm.CommandText += string.Format("{6} FROM {0} left join {2} on {0}.id = {2}.parent left join {1} on {0}.id = {1}.parent left join {3} on {0}.id = {3}.parent  left join {4} on {0}.id = {4}.parent left join {5} on {0}.id = {5}.par WHERE {7} AND {0}.fired = {8} ", TableNames.Person, TableNames.PersonAssignment, TableNames.languageLevel, TableNames.Absence, TableNames.Penalty, TableNames.Attestations, sb2.ToString(), sb.ToString(), fired);
				}
				else
				{
					this.comm.CommandText = string.Format(@"SELECT {0}.ID, {0}.name, {1}.level1, {1}.level2, {1}.level3, {1}.level4, {1}.position  ", TableNames.Person, TableNames.PersonAssignment);
					if (english)
					{
						this.comm.CommandText += string.Format(@",{0}.engname,{0}.engeducation, {1}.level1eng, {1}.level2eng, {1}.level3eng, {1}.level4eng, {1}.positioneng ", TableNames.Person, TableNames.PersonAssignment);
					}
					this.comm.CommandText += string.Format("{6} FROM {0} left join {2} on {0}.id = {2}.parent left join {1} on {0}.id = {1}.parent left join {2} on {0}.id = {2}.parent  left join {3} on {0}.id = {3}.parent   left join {5} on {0}.id = {5}.parent left join {4} on {0}.id = {4}.par WHERE {7} AND {0}.fired = {8} ", TableNames.Person, TableNames.PersonAssignment, TableNames.languageLevel, TableNames.Absence, TableNames.Penalty, TableNames.Attestations, TableNames.Fired, sb2.ToString(), sb.ToString(), fired); /// Testttttt
				}
			}
			try
			{
				this.da.Fill(ds);
				dt = ds.Tables[0];
				if (language != "")
				{
					foreach (DataRow row in dt.Rows)
					{
						row["language"] = language;
					}
				}
			}
			catch (SqlException e)
			{
				MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return dt;
		}

		//public DataTable SelectTutors(string positionid)
		//{
		//    DataTable dt = new DataTable();
		//    DataSet ds = new DataSet();

		//    this.comm.CommandText = string.Format(@"SELECT * from {0} LEFT JOIN  {1} ON {0}.id = {1}.parent WHERE {1}.isactive = 1 AND {1}.positionid = {2}", TableNames.Person, TableNames.PersonAssignment, positionid);

		//    CreateDataAdapter();
		//    try
		//    {
		//        this.comm.Connection.Open();
		//        this.da.Fill(ds);
		//        dt = ds.Tables[0];
		//    }
		//    catch (SqlException e)
		//    {
		//        MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//    }
		//    this.comm.Connection.Close();
		//    return dt;
		//}

		public DataTable SelectPSR()
		{
			//Селектира обединение от таблиците с личните данни и данните за назначенията.
			DataTable dt = new DataTable();
			DataSet ds = new DataSet();
			
			this.comm.CommandText = string.Format("select HR_PErson.*, HR_PersonAssignment.*,HR_personassignment.positionid as posid, HR_firmpersonal3.ekdapaylevel " +
			                                      "from HR_Person left join HR_personassignment on HR_person.id = HR_personassignment.parent " +
												  "left join HR_firmpersonal3 on HR_personassignment.positionid = HR_firmpersonal3.id " +
			                                      " WHERE HR_personassignment.isactive = 1");

			CreateDataAdapter();
			try
			{
				this.da.Fill(ds);
				dt = ds.Tables[0];
			}
			catch (SqlException e)
			{
				MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return null;
			}
			return dt;
		}

		void CreateDataAdapter()
		{


			this.da = new SqlDataAdapter((SqlCommand)this.comm);


		}

		void CreateDataAdapter(string selectCommand)
		{
			this.da = new SqlDataAdapter(selectCommand, (SqlConnection)this.conn);
		}

		public int GetLastInsertID(string table, string column)
		{
			DataSet ds = new DataSet();
			int i = 0;
			CreateDataAdapter();
			this.comm.CommandText = string.Format(@"SELECT MAX({0}) from {1}", column, table);
			try
			{
				this.da.Fill(ds);
			}
			catch (SqlException e)
			{
				MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return -1;
			}
			try
			{
				i = int.Parse(ds.Tables[0].Rows[0][0].ToString());
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, "Не може да се прочете коректно идентификатора на ред от базата данни.");
				i = -1;
			}
			return i;
		}

		public int GetLastInsertID()
		{
			DataSet ds = new DataSet();
			int i = 0;
			CreateDataAdapter();
			this.comm.CommandText = @"SELECT LAST_INSERT_ID()";
			try
			{
				this.da.Fill(ds);
			}
			catch (SqlException e)
			{
				MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return -1;
			}
			try
			{
				i = int.Parse(ds.Tables[0].Rows[0][0].ToString());
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, "Не може да се прочете коректно идентификатора на ред от базата данни.");
				i = -1;
			}
			return i;
		}

		// ctor
		void Constructor(string connString)
		{
			conn = new SqlConnection(connString);
			comm = new SqlCommand();

			//conn.ConnectionString = connString;
			comm.Connection = conn;
		}

		public DataAction(string connString)
		{
			Constructor(connString); // With defualt constrcutor mysql is used}
		}

		public DataTable SelectBase(bool fired, string orderby, DataTable dtQuery)
		{
			try
			{
				DataTable dt = new DataTable();
				DataTable dtNa = new DataTable();
				DataSet ds = new DataSet();
				DataSet dsNa = new DataSet();

				CreateDataAdapter();

				if (fired == false)
				{
					this.comm.CommandText = "SELECT ";
					this.comm.CommandText += dtQuery.Rows[0]["value"].ToString();
					if (dtQuery.Rows[0]["selectas"].ToString() != null && dtQuery.Rows[0]["selectas"].ToString() != "")
					{
						this.comm.CommandText += " as " + dtQuery.Rows[0]["selectas"].ToString();
					}

					for (int i = 1; i < dtQuery.Rows.Count; i++)
					{
						this.comm.CommandText += ", ";
						this.comm.CommandText += dtQuery.Rows[i]["value"].ToString();
						if (dtQuery.Rows[i]["selectas"].ToString() != null && dtQuery.Rows[i]["selectas"].ToString() != "")
						{
							this.comm.CommandText += " as " + dtQuery.Rows[i]["selectas"].ToString();
						}
					}
					this.comm.CommandText += " FROM " + TableNames.Person + " LEFT JOIN " + TableNames.PersonAssignment;
					this.comm.CommandText += " ON (" + TableNames.Person + ".id = " + TableNames.PersonAssignment + ".parent";
					this.comm.CommandText += ") WHERE ";
					this.comm.CommandText += TableNames.Person + ".fired = 0 and " + TableNames.PersonAssignment + ".isactive = 1";

					if (orderby != "")
					{
						this.comm.CommandText += " ORDER BY " + orderby;
					}
					try
					{
						this.da.Fill(ds);
						dt = ds.Tables[0];
					}
					catch (SqlException e)
					{
						MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}

					if (orderby != "")
					{
						this.comm.CommandText = "SELECT * FROM " + TableNames.Person + " WHERE nodeID = 0 AND fired = 0 ORDER BY " + orderby; //done here to select only the persons whisch are present in kartoteka, not fired but also not assigned
					}
					else
					{
						this.comm.CommandText = "SELECT * FROM " + TableNames.Person + " WHERE nodeID = 0 AND fired = 0"; //done here to select only the persons whisch are present in kartoteka, not fired but also not assigned
					}
					this.da.Fill(dsNa);
					dtNa = dsNa.Tables[0];

					foreach (DataRow Row in dtNa.Rows)
					{
						DataRow Ra = dt.NewRow();
						foreach (DataRow Rt in dtQuery.Rows)
						{
							if (Rt["column"].ToString() != "" && Rt["selectas"].ToString() != "")
							{
								Ra[Rt["selectas"].ToString()] = Row[Rt["column"].ToString()];
							}
							else if (Rt["column"].ToString() != "")
							{
								Ra[Rt["column"].ToString()] = Row[Rt["column"].ToString()];
							}
						}
						dt.Rows.Add(Ra);
					}
				}
				else
				{
					this.comm.CommandText = "SELECT ";
					this.comm.CommandText += dtQuery.Rows[0]["value"].ToString();
					if (dtQuery.Rows[0]["selectas"].ToString() != null && dtQuery.Rows[0]["selectas"].ToString() != "")
					{
						this.comm.CommandText += " as " + dtQuery.Rows[0]["selectas"].ToString();
					}

					for (int i = 1; i < dtQuery.Rows.Count; i++)
					{
						this.comm.CommandText += ", ";
						this.comm.CommandText += dtQuery.Rows[i]["value"].ToString();
						if (dtQuery.Rows[i]["selectas"].ToString() != null && dtQuery.Rows[i]["selectas"].ToString() != "")
						{
							this.comm.CommandText += " as " + dtQuery.Rows[i]["selectas"].ToString();
						}
					}

					this.comm.CommandText += " FROM " + TableNames.Person + " LEFT JOIN " + TableNames.Fired;
					this.comm.CommandText += " ON (" + TableNames.Person + ".id = " + TableNames.Fired + ".parent";
					this.comm.CommandText += ") WHERE ";
					this.comm.CommandText += TableNames.Person + ".fired = 1";

					if (orderby != "")
					{
						this.comm.CommandText += " ORDER BY " + orderby;
					}
					try
					{
						this.da.Fill(ds);
						dt = ds.Tables[0];
					}
					catch (SqlException e)
					{
						MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				return dt;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				return null;
			}
		}

		public void UpdatePicture(string table, int parent, byte[] img)
		{
			try
			{
				this.conn.Open();
				comm.Parameters.Clear();
				this.comm.CommandText = string.Format("UPDATE {0} SET Picture = @Picture WHERE parent = @Parent", TableNames.Pictures);
				this.comm.Prepare();
				//if (comm.Parameters.Count == 0)
				//{

				DbParameter param1 = null, param2 = null;
				//switch (type)
				//{
				//    case DBTypes.MySql:
				//        param1 = new MySqlParameter("?Parent", MySqlDbType.Int32, 4);
				//        param2 = new MySqlParameter("?Picture",MySqlDbType.LongBlob, img.Length);
				//        break;
				//    case DBTypes.MsSql:
				param1 = new SqlParameter("@Parent", SqlDbType.Int, 4);
				param2 = new SqlParameter("@Picture", SqlDbType.Image, img.Length);
				//        break;
				//}

				this.comm.Parameters.Add(param1);
				this.comm.Parameters.Add(param2);
				//}
				this.comm.Parameters[0].Value = parent;
				this.comm.Parameters[1].Value = img;

				int iresult = this.comm.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				this.conn.Close();
			}
		}

		public void InsertPicture(string table, int parent, byte[] img)
		{
			try
			{
				this.conn.Open();
				this.comm.Parameters.Clear();
				//if (comm.Parameters.Count == 0)
				//{
				this.comm.CommandText = string.Format("INSERT INTO {0} values(@Parent , @Picture)", TableNames.Pictures);
				this.comm.Prepare();

				DbParameter param1 = null, param2 = null;

				param1 = new SqlParameter("@Parent", SqlDbType.Int, 4);
				param2 = new SqlParameter("@Picture", SqlDbType.Image, img.Length);



				this.comm.Parameters.Add(param1);
				this.comm.Parameters.Add(param2);

				//}

				this.comm.Parameters[0].Value = parent;
				this.comm.Parameters[1].Value = img;

				int iresult = this.comm.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				this.conn.Close();
			}
		}

		public byte[] SelectPicture(string table, int parent)
		{
			byte[] barrImg = null;
			try
			{
				comm.CommandText = "SELECT Picture FROM " + TableNames.Pictures + " WHERE parent = " + parent;
				this.conn.Open();
				barrImg = (byte[])comm.ExecuteScalar();
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				return null;
			}
			finally
			{
				this.conn.Close();
			}
			return barrImg;
		}

		public void UpdateHoliday(int id, int left, int total, string year)
		{
			try
			{
				this.comm.Connection.Open();
				this.comm.CommandText = string.Format("UPDATE {4} set leftover = {0}, total = {1} WHERE parent = {2} AND Year = {3}", left.ToString(), total.ToString(), id.ToString(), year, TableNames.YearHoliday);
				this.comm.ExecuteNonQuery();
				this.comm.Connection.Close();
			}
			catch (Exception exc)
			{
				this.comm.Connection.Close();
				MessageBox.Show("Има повреда в таблицата за отпуски грешка: " + exc.Message);
			}
		}

		public void UpdateYear(int currentYear)
		{

			this.comm = new SqlCommand("UPDATE " + TableNames.Year + " set year = " + currentYear.ToString(), (SqlConnection)this.conn);


			try
			{
				this.conn.Open();
				this.comm.ExecuteNonQuery();
			}
			catch (SqlException e)
			{
				MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			this.conn.Close();
		}

		public void CalculateExperienceCorrection(int Year)
		{
			DataTable dtPerson = new DataTable();
			DataTable dtHoliday = new DataTable();
			DataSet dsPerson = new DataSet();
			DataSet dsHoliday = new DataSet();

			DataView vueHoliday;
			DataViewRowState dvrs = DataViewRowState.CurrentRows;
			string condition;
			CreateDataAdapter("select id, parent, experiencecorrection from " + TableNames.PersonAssignment + " where isactive = 1");
			da.Fill(dsPerson);
			dtPerson = dsPerson.Tables[0];
			CreateDataAdapter(string.Format("select id, countdays, parent as par from " + TableNames.Absence + " where Year = {0} and typeabsence = 'Неплатен отпуск'", Year.ToString()));
			da.Fill(dsHoliday);
			dtHoliday = dsHoliday.Tables[0];

			foreach (DataRow row in dtPerson.Rows)
			{
				int sumUnpaidHolidays = 0;
				condition = "par = " + row["parent"].ToString();
				vueHoliday = new DataView(dtHoliday, condition, "id", dvrs);
				for (int i = 0; i < vueHoliday.Count; i++)
				{
					sumUnpaidHolidays += (int)vueHoliday[i]["countdays"];
				}
				if (sumUnpaidHolidays > 30)
				{
					int corr = 0;
					try
					{
						corr = int.Parse(row["experiencecorrection"].ToString());
					}
					catch
					{
						corr = 0;
					}
					corr += sumUnpaidHolidays - 30;
					comm.CommandText = string.Format("update " + TableNames.PersonAssignment + " set experiencecorrection = {0} where id = {1}", corr.ToString(), row["id"].ToString());
					conn.Open();
					comm.ExecuteNonQuery();
					conn.Close();
				}
			}
		}

		public void ReloadSeasonWorkers()
		{

			this.comm = new SqlCommand("UPDATE " + TableNames.FirmPersonal3 + " set busy = 0, free = staffCount WHERE TypePosition = 'Сезонна'", (SqlConnection)this.conn);

			try
			{
				this.conn.Open();
				this.comm.ExecuteNonQuery();
				this.conn.Close();
			}
			catch (SqlException e)
			{
				MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.conn.Close();
			}
			this.conn.Close();
		}

		public void UpdateHolidayNewYear(int year)
		{
			//DataView vueStash;
			string command;
			int leftover;
			DataTable dt = new DataTable();
			DataTable oldHol = new DataTable();
			DataSet ds = new DataSet();
			DataSet dsOld = new DataSet();
			command = string.Format("SELECT parent, numHoliday, additionalHoliday from {0} where IsActive = 1", TableNames.PersonAssignment);
			comm.CommandText = command;
			try
			{
				CreateDataAdapter();
				this.da.Fill(ds);
				dt = ds.Tables[0];

				comm.Connection.Open();
				foreach (DataRow row in dt.Rows)
				{
					int total = 0;
					leftover = 0;
					try
					{
						total += int.Parse(row["numholiday"].ToString());
					}
					catch (FormatException)
					{
					}
					try
					{
						total += int.Parse(row["additionalholiday"].ToString());
					}
					catch (FormatException)
					{
					}

					comm.CommandText = "INSERT INTO " + TableNames.YearHoliday + "(parent, year, leftover, total) VALUES( " + row[0].ToString()
						+ "," + year.ToString() + ", " + (total + leftover).ToString() + ", " + total.ToString() + ")";

					if (row[1].ToString() != "" && row[0].ToString() != "")
					{
						comm.ExecuteNonQuery();
					}
				}
				comm.Connection.Close();
			}
			catch (SqlException e)
			{
				MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
				comm.Connection.Close();
			}
		}

		//public int UniversalInsert(string table, Dictionary<string, string> dList, string idcolumm)
		//{
		//    StringBuilder command = new StringBuilder();
		//    int intres = -1;

		//    command.Append("INSERT INTO ");
		//    command.Append(table); 
		//    command.Append(" (");
		//    foreach(KeyValuePair <string, string> kvp in dList)
		//    {
		//        command.Append(kvp.Key);
		//        command.Append(','); ;
		//    }

		//    command.Remove(command.Length - 1, 1); //trim the last ','
		//    command.Append(") VALUES(");

		//    foreach (KeyValuePair<string, string> kvp in dList)
		//    {
		//        command.Append('\'');
		//        //command.Append(kvp.Value.ToCharArray());
		//        command.AppendFormat(@"{0}", kvp.Value);
		//        command.Append('\'');
		//        command.Append(',');
		//    }
		//    command.Replace(@"\", @"\\");

		//    command.Remove(command.Length - 1, 1); //trim the last ','
		//    command.Append(')');

		//    try
		//    {
		//        comm.CommandText = command.ToString();
		//        comm.Connection.Open();
		//        comm.ExecuteNonQuery();
		//    }
		//    catch (Exception ex)
		//    {
		//        ErrorLog.WriteException(ex, ex.Message);
		//        MessageBox.Show(ex.Message);
		//        comm.Connection.Close();
		//        return -1;
		//    }
		//    comm.Connection.Close();
		//    intres = GetLastInsertID(table, idcolumm);
		//    return intres;
		//}

		//public int UniversalInsertObject(string table, Dictionary<string, object> dList, string idcolumm)
		//{



		//    //    foreach (KeyValuePair<string, string> kvp in dList)
		//    //    {
		//    //        command.Append("@");
		//    //        command.Append(kvp.Key);
		//    //        command.Append(',');
		//    //    }

		//    //    command.Remove(command.Length - 1, 1); //trim the last ','
		//    //    command.Append(')');

		//    //    comm.Parameters.Clear();

		//    //    foreach (KeyValuePair<string, string> kvp in dList)
		//    //    {
		//    //        SqlParameter par = new SqlParameter(kvp.Key, kvp.Value);
		//    //        comm.Parameters.Add(par);
		//    //    }

		//    //    try
		//    //    {
		//    //        this.comm.CommandText = command.ToString();
		//    //        this.conn.Open();
		//    //        comm.ExecuteNonQuery();
		//    //    }
		//    //    catch (SqlException e)
		//    //    {
		//    //        MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//    //        this.tran.Rollback();
		//    //        return -1;
		//    //    }
		//    //    catch (Exception ex)
		//    //    {
		//    //        ErrorLog.WriteException(ex, ex.Message);
		//    //        MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//    //        this.tran.Rollback();
		//    //        return -1;
		//    //    }
		//    //    finally
		//    //    {
		//    //        this.conn.Close();
		//    //    }
		//    //}
		//    //return 1;

		//    StringBuilder command = new StringBuilder();
		//    int intres = -1;

		//    if ((dList != null) && (dList.Count > 0))
		//    {
		//        command.Append("INSERT INTO ");
		//        command.Append(table);
		//        command.Append(" (");
		//        foreach (KeyValuePair<string, object> kvp in dList)
		//        {
		//            command.Append(kvp.Key);
		//            command.Append(','); ;
		//        }

		//        command.Remove(command.Length - 1, 1); //trim the last ','
		//        command.Append(") VALUES(");



		//        foreach (KeyValuePair<string, object> kvp in dList)
		//        {
		//            command.Append('\'');
		//            if (kvp.Value is DateTime)
		//            {
		//                DateTime date = (DateTime)kvp.Value;
		//                if (type == DBTypes.MsSql)
		//                {
		//                    command.AppendFormat(@"{0}", string.Format("{0}-{2}-{1} 00:00:00", date.Year, date.Month, date.Day));
		//                    command.Append('\'');
		//                    command.Append(',');
		//                }
		//                else
		//                {
		//                    command.AppendFormat(@"{0}", string.Format("{0}-{1}-{2} 00:00:00", date.Year, date.Month, date.Day));
		//                    command.Append('\'');
		//                    command.Append(',');
		//                }

		//            }
		//            else
		//            {
		//                command.AppendFormat(@"{0}", kvp.Value);
		//                command.Append('\'');
		//                command.Append(',');
		//            }

		//        }
		//        command.Replace(@"\", @"\\");

		//        command.Remove(command.Length - 1, 1); //trim the last ','
		//        command.Append(')');

		//        try
		//        {
		//            comm.CommandText = command.ToString();
		//            comm.Connection.Open();
		//            comm.ExecuteNonQuery();
		//        }
		//        catch (Exception ex)
		//        {
		//            ErrorLog.WriteException(ex, ex.Message);
		//            MessageBox.Show(ex.Message);
		//            comm.Connection.Close();
		//            return -1;
		//        }
		//        comm.Connection.Close();
		//        intres = GetLastInsertID(table, idcolumm);
		//    }
		//    return intres;
		//}

		//public int UniversalInsertObject(string table, Dictionary<string, object> dList, TransactionComnmand TC)
		//{
		//    try
		//    {
		//        StringBuilder command = new StringBuilder();
		//        int id = -1;

		//        if ((dList != null) && (dList.Count > 0))
		//        {
		//            command.Append("INSERT INTO ");
		//            command.Append(table);
		//            command.Append(" (");
		//            foreach (KeyValuePair<string, object> kvp in dList)
		//            {
		//                command.Append(kvp.Key);
		//                command.Append(','); ;
		//            }

		//            command.Remove(command.Length - 1, 1); //trim the last ','
		//            command.Append(") VALUES(");

		//            foreach (KeyValuePair<string, object> kvp in dList)
		//            {
		//                command.Append('\'');
		//                if (kvp.Value is DateTime)
		//                {
		//                    DateTime date = (DateTime)kvp.Value;
		//                    //if (type == DBTypes.MsSql)
		//                    //{
		//                    //    command.AppendFormat(@"{0}", string.Format("{0}-{2}-{1} 00:00:00", date.Year, date.Month, date.Day));
		//                    //    command.Append('\'');
		//                    //    command.Append(',');
		//                    //}
		//                    //else
		//                    //{
		//                        command.AppendFormat(@"{0}", string.Format("{0}-{1}-{2} 00:00:00", date.Year, date.Month, date.Day));
		//                        command.Append('\'');
		//                        command.Append(',');
		//                    //}

		//                }
		//                else
		//                {
		//                    command.AppendFormat(@"{0}", kvp.Value);
		//                    command.Append('\'');
		//                    command.Append(',');
		//                }
		//            }

		//            command.Replace(@"\", @"\\");

		//            command.Remove(command.Length - 1, 1); //trim the last ','
		//            command.Append(')');

		//            try
		//            {
		//                this.comm.CommandText = command.ToString();
		//                if (TC == TransactionComnmand.BEGIN_TRANSACTION)
		//                {
		//                    if (this.conn.State == ConnectionState.Open)
		//                    {
		//                        this.conn.Close();
		//                        this.conn.Open();
		//                    }
		//                    else
		//                    {
		//                        this.conn.Open();
		//                    }
		//                    this.tran = this.comm.Connection.BeginTransaction();
		//                    this.comm.Transaction = this.tran;
		//                    this.comm.Connection = this.conn;
		//                }
		//                comm.ExecuteNonQuery();
		//            }
		//            catch (SqlException e)
		//            {
		//                MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//                this.tran.Rollback();
		//                this.conn.Close();
		//                return -1;

		//            }
		//            catch (Exception ex)
		//            {
		//                ErrorLog.WriteException(ex, ex.Message);
		//                MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//                this.tran.Rollback();
		//                this.conn.Close();
		//                return -1;
		//            }

		//            id = GetLastInsertID(table, );
		//            if (id <= 0)
		//            {
		//                MessageBox.Show("Грешка при четене на идентификатор", "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//                this.tran.Rollback();
		//                this.conn.Close();
		//                return -1;
		//            }
		//        }
		//        try
		//        {
		//            if (TC == TransactionComnmand.COMMIT_TRANSACTION)// || 
		//            {
		//                this.tran.Commit();
		//                this.comm.Connection.Close();
		//            }
		//            else if (TC == TransactionComnmand.ROLLBACK_TRANSACION)
		//            {
		//                this.tran.Rollback();
		//                this.conn.Close();
		//                return -1;
		//            }
		//        }
		//        catch (Exception ex)
		//        {
		//            ErrorLog.WriteException(ex, "Грешка при четене на идентификатор");
		//            MessageBox.Show("Грешка при четене на идентификатор", "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//            this.tran.Rollback();
		//            this.conn.Close();
		//            return -1;
		//        }
		//        return id;
		//    }
		//    catch (Exception ex)
		//    {
		//        ErrorLog.WriteException(ex, ex.Message);
		//        MessageBox.Show("Грешка", "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//        this.conn.Close();
		//        return -1;
		//    }
		//}

		//public int UniversalInsertParam(string table, Dictionary<string, object> dList, string idcolumn)
		//{
		//    int intres = 0;
		//    try
		//    {                
		//        StringBuilder command = new StringBuilder();

		//        if ((dList != null) && (dList.Count > 0))
		//        {
		//            command.Append("INSERT INTO ");
		//            command.Append(table);
		//            command.Append(" (");
		//            foreach (KeyValuePair<string, object> kvp in dList)
		//            {
		//                command.Append(kvp.Key);
		//                command.Append(','); ;
		//            }

		//            command.Remove(command.Length - 1, 1); //trim the last ','
		//            command.Append(") VALUES(");

		//            foreach (KeyValuePair<string, object> kvp in dList)
		//            {
		//                command.Append("@");
		//                command.Append(kvp.Key);
		//                command.Append(',');
		//            }

		//            command.Remove(command.Length - 1, 1); //trim the last ','
		//            command.Append(')');

		//            comm.Parameters.Clear();

		//            foreach (KeyValuePair<string, object> kvp in dList)
		//            {
		//                SqlParameter par = new SqlParameter(kvp.Key, kvp.Value);
		//                comm.Parameters.Add(par);
		//            }

		//            try
		//            {
		//                this.comm.CommandText = command.ToString();
		//                this.conn.Open();
		//                comm.ExecuteNonQuery();
		//            }
		//            catch (SqlException e)
		//            {
		//                MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//                this.tran.Rollback();
		//                return -1;
		//            }
		//            catch (Exception ex)
		//            {
		//                ErrorLog.WriteException(ex, ex.Message);
		//                MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//                this.tran.Rollback();
		//                return -1;
		//            }
		//            finally
		//            {
		//                this.conn.Close();
		//            }
		//        }
		//        intres = GetLastInsertID(table, idcolumn);
		//        return 1;
		//    }
		//    catch (Exception ex)
		//    {
		//        ErrorLog.WriteException(ex, ex.Message);
		//        MessageBox.Show("Грешка", "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//        this.conn.Close();
		//        return -1;
		//    }
		//}

		public int UniversalInsertParam(string table, Dictionary<string, object> dList, string idcolumn, TransactionComnmand TC)
		{
			int intres = 0;
			try
			{
				StringBuilder command = new StringBuilder();

				if ((dList != null) && (dList.Count > 0))
				{
					command.Append("INSERT INTO ");
					command.Append(table);
					command.Append(" (");
					foreach (KeyValuePair<string, object> kvp in dList)
					{
						command.Append(kvp.Key);
						command.Append(','); ;
					}

					command.Remove(command.Length - 1, 1); //trim the last ','
					command.Append(") VALUES(");

					foreach (KeyValuePair<string, object> kvp in dList)
					{
						command.Append("@");
						command.Append(kvp.Key);
						command.Append(',');
					}

					command.Remove(command.Length - 1, 1); //trim the last ','
					command.Append(')');

					comm.Parameters.Clear();

					foreach (KeyValuePair<string, object> kvp in dList)
					{

						SqlParameter par = new SqlParameter(kvp.Key, kvp.Value);
						comm.Parameters.Add(par);

					}

					try
					{
						this.comm.CommandText = command.ToString();
						if (TC == TransactionComnmand.BEGIN_TRANSACTION)
						{
							if (this.conn.State == ConnectionState.Open)
							{
								this.conn.Close();
								this.conn.Open();
							}
							else
							{
								this.conn.Open();
							}
							this.tran = this.comm.Connection.BeginTransaction();
							this.comm.Transaction = this.tran;
							this.comm.Connection = this.conn;
						}
						else if (TC == TransactionComnmand.NO_TRANSACTION)
						{
							this.conn.Open();
						}
						comm.ExecuteNonQuery();
					}
					catch (SqlException e)
					{
						MessageBox.Show(e.Message + " " + this.comm.CommandText, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
						this.tran.Rollback();
						this.conn.Close();
						return -1;
					}
					catch (Exception ex)
					{
						ErrorLog.WriteException(ex, ex.Message);
						MessageBox.Show(ex.Message + " " + this.comm.CommandText, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
						this.tran.Rollback();
						this.conn.Close();
						return -1;
					}

					intres = GetLastInsertID(table, idcolumn);
					if (intres <= 0)
					{
						MessageBox.Show("Грешка при четене на идентификатор", "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
						this.tran.Rollback();
						this.conn.Close();
						return -1;
					}

					try
					{
						if (TC == TransactionComnmand.COMMIT_TRANSACTION)// || 
						{
							this.tran.Commit();
							this.comm.Connection.Close();
						}
						else if (TC == TransactionComnmand.ROLLBACK_TRANSACION)
						{
							this.tran.Rollback();
							this.conn.Close();
							return -1;
						}
						else if (TC == TransactionComnmand.NO_TRANSACTION)
						{
							this.conn.Close();
						}
					}
					catch (Exception ex)
					{
						ErrorLog.WriteException(ex, "Грешка при четене на идентификатор");
						MessageBox.Show("Грешка при четене на идентификатор", "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
						this.tran.Rollback();
						this.conn.Close();
						return -1;
					}
				}

				//    try
				//    {
				//        this.comm.CommandText = command.ToString();
				//        this.conn.Open();
				//        comm.ExecuteNonQuery();
				//    }
				//    catch (SqlException e)
				//    {
				//        MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
				//        this.tran.Rollback();
				//        return -1;
				//    }
				//    catch (Exception ex)
				//    {
				//        ErrorLog.WriteException(ex, ex.Message);
				//        MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
				//        this.tran.Rollback();
				//        return -1;
				//    }
				//    finally
				//    {
				//        this.conn.Close();
				//    }
				//}
				//intres = GetLastInsertID(table, idcolumn);
				//return 1;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show("Грешка", "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.conn.Close();
				return -1;
			}
			return intres;
		}

		//public bool UniversalUpdate(string table, string id, Dictionary<string, string> dList)
		//{
		//    try
		//    {
		//        StringBuilder command = new StringBuilder();
		//        bool result = true;

		//        if (dList.Count <= 0)
		//            return result;

		//        command.Append("UPDATE ");
		//        command.Append(table);
		//        command.Append(" SET ");
		//        foreach (KeyValuePair<string, string> kvp in dList)
		//        {
		//            command.Append(' ');
		//            command.Append(kvp.Key);
		//            command.Append(" = '");
		//            command.Append(kvp.Value);
		//            command.Append("',");
		//        }

		//        command.Remove(command.Length - 1, 1); //trim the last ','
		//        command.Append(" WHERE id = ");
		//        command.Append(id);

		//        comm.CommandText = command.ToString();

		//        try
		//        {
		//            comm.Connection.Open();
		//            comm.ExecuteNonQuery();
		//        }
		//        catch (Exception ex)
		//        {
		//            ErrorLog.WriteException(ex, ex.Message);
		//            result = false;
		//            MessageBox.Show(ex.Message);
		//        }
		//        comm.Connection.Close();
		//        return result;
		//    }
		//    catch (Exception ex)
		//    {
		//        ErrorLog.WriteException(ex, ex.Message);
		//        MessageBox.Show(ex.Message);
		//        return false;
		//    }
		//}
		//public bool UniversalUpdateObject(string table, string id, Dictionary<string, object> dList)
		//{
		//    try
		//    {
		//        StringBuilder command = new StringBuilder();
		//        bool result = true;

		//        if (dList.Count <= 0)
		//            return result;

		//        command.Append("UPDATE ");
		//        command.Append(table);
		//        command.Append(" SET ");
		//        foreach (KeyValuePair<string, object> kvp in dList)
		//        {
		//            command.Append(' ');
		//            command.Append(kvp.Key);
		//            if (kvp.Value is DateTime)
		//            {
		//                DateTime date = (DateTime)kvp.Value;
		//                if (type == DBTypes.MsSql)
		//                {
		//                    command.Append(" = '");
		//                    command.Append(string.Format("{0}-{2}-{1} 00:00:00", date.Year, date.Month, date.Day));
		//                    command.Append("',");
		//                }
		//                else
		//                {
		//                    command.Append(" = '");
		//                    command.Append(string.Format("{0}-{1}-{2} 00:00:00", date.Year, date.Month, date.Day));
		//                    command.Append("',");
		//                }

		//            }
		//            else
		//            {
		//                command.Append(" = '");
		//                command.Append(kvp.Value);
		//                command.Append("',");
		//            }
		//        }

		//        command.Remove(command.Length - 1, 1); //trim the last ','
		//        command.Append(" WHERE id = ");
		//        command.Append(id);

		//        comm.CommandText = command.ToString();

		//        try
		//        {
		//            comm.Connection.Open();
		//            comm.ExecuteNonQuery();
		//        }
		//        catch (Exception ex)
		//        {
		//            ErrorLog.WriteException(ex, ex.Message);
		//            result = false;
		//            MessageBox.Show(ex.Message);
		//        }
		//        comm.Connection.Close();
		//        return result;
		//    }
		//    catch (Exception ex)
		//    {
		//        ErrorLog.WriteException(ex, ex.Message);
		//        MessageBox.Show(ex.Message);
		//        return false;
		//    }
		//}

		public bool UniversalUpdateObject(string table, string idcolumn, Dictionary<string, object> dList, string id, TransactionComnmand TC)
		{
			bool result = true;
			try
			{
				StringBuilder command = new StringBuilder();

				if (dList != null && dList.Count > 0)
				{
					command.Append("UPDATE ");
					command.Append(table);
					command.Append(" SET ");
					foreach (KeyValuePair<string, object> kvp in dList)
					{
						command.Append(' ');
						command.Append(kvp.Key);

						if (kvp.Value.ToString().StartsWith("\'"))
						{
							string str;
							str = kvp.Value.ToString().Remove(0, 1);
							command.Append(" = ");
							command.Append(str);
							command.Append(',');
							continue;
						}

						if (kvp.Value is DateTime)
						{
							DateTime date = (DateTime)kvp.Value;
							//if (type == DBTypes.MsSql)
							//{
							//	command.Append(" = '");
							//	command.Append(string.Format("{0}-{2}-{1} 00:00:00", date.Year, date.Month, date.Day));
							//	command.Append("',");
							//}
							//else
							//{
								command.Append(" = '");
								command.Append(string.Format("{0}-{1}-{2} 00:00:00", date.Year, date.Month, date.Day));
								command.Append("',");
							//}
						}
						else
						{
							command.Append(" = '");
							command.Append(kvp.Value);
							command.Append("',");
						}
					}

					command.Remove(command.Length - 1, 1); //trim the last ','
					command.Append(" WHERE id = ");
					command.Append(id);

					this.comm.CommandText = command.ToString();

					try
					{
						if (TC == TransactionComnmand.BEGIN_TRANSACTION)
						{
							this.conn.Open();
							this.tran = this.conn.BeginTransaction();
							this.comm.Transaction = this.tran;
						}
						this.comm.ExecuteNonQuery();
					}
					catch (SqlException e)
					{
						MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
						this.tran.Rollback();
						this.conn.Close();
						result = false;
					}
					catch (Exception ex)
					{
						ErrorLog.WriteException(ex, ex.Message);
						MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
						this.tran.Rollback();
						this.conn.Close();
						result = false;
					}
				}

				if (TC == TransactionComnmand.COMMIT_TRANSACTION)// || 
				{
					this.tran.Commit();
					this.comm.Connection.Close();
				}
				else if (TC == TransactionComnmand.ROLLBACK_TRANSACION)
				{
					this.tran.Rollback();
					this.conn.Close();
				}

				return result;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.tran.Rollback();
				this.conn.Close();
				return false;
			}
		}

		//public bool UniversalUpdateParam(string table, string idcolumn, string id, Dictionary<string, object> dList)
		//{
		//    try
		//    {
		//        StringBuilder command = new StringBuilder();
		//        bool result = true;

		//        if (dList.Count <= 0)
		//            return result;

		//        command.Append("UPDATE ");
		//        command.Append(table);
		//        command.Append(" SET ");
		//        foreach (KeyValuePair<string, object> kvp in dList)
		//        {
		//            command.Append(' ');
		//            command.Append(kvp.Key);
		//            command.Append(" = @");
		//            command.Append(kvp.Key);
		//            command.Append(",");
		//        }

		//        command.Remove(command.Length - 1, 1); //trim the last ','
		//        command.Append(" WHERE " + idcolumn + " = ");
		//        command.Append(id);

		//        comm.CommandText = command.ToString();

		//        comm.Parameters.Clear();

		//        foreach (KeyValuePair<string, object> kvp in dList)
		//        {
		//            SqlParameter par = new SqlParameter(kvp.Key, kvp.Value);
		//            comm.Parameters.Add(par);
		//        }

		//        try
		//        {
		//            comm.Connection.Open();
		//            comm.ExecuteNonQuery();
		//        }
		//        catch (Exception ex)
		//        {
		//            ErrorLog.WriteException(ex, ex.Message);
		//            result = false;
		//            MessageBox.Show(ex.Message);
		//        }
		//        comm.Connection.Close();
		//        return result;
		//    }
		//    catch (Exception ex)
		//    {
		//        ErrorLog.WriteException(ex, ex.Message);
		//        MessageBox.Show(ex.Message);
		//        return false;
		//    }
		//}

		public bool UniversalUpdateParam(string table, string idcolumn, Dictionary<string, object> dList, string id, TransactionComnmand TC)
		{
			bool result = true;
			try
			{
				StringBuilder command = new StringBuilder();

				if (dList == null)
				{
					if (TC == TransactionComnmand.COMMIT_TRANSACTION)// || 
					{
						this.tran.Commit();
						this.comm.Connection.Close();
					}
					return result;
				}
				if (dList.Count <= 0)
					return result;

				command.Append("UPDATE ");
				command.Append(table);
				command.Append(" SET ");
				foreach (KeyValuePair<string, object> kvp in dList)
				{
					command.Append(' ');
					command.Append(kvp.Key);
					command.Append(" = @");
					command.Append(kvp.Key);
					command.Append(",");
				}

				command.Remove(command.Length - 1, 1); //trim the last ','
				command.Append(" WHERE " + idcolumn + " = ");
				command.Append(id);

				comm.CommandText = command.ToString();

				comm.Parameters.Clear();

				foreach (KeyValuePair<string, object> kvp in dList)
				{

					SqlParameter par = new SqlParameter(kvp.Key, kvp.Value);
					comm.Parameters.Add(par);

				}

				try
				{
					if (TC == TransactionComnmand.BEGIN_TRANSACTION)
					{
						if (this.conn.State == ConnectionState.Open)
						{
							this.conn.Close();
							this.conn.Open();
						}
						else
						{
							this.conn.Open();
						}
						this.tran = this.comm.Connection.BeginTransaction();
						this.comm.Transaction = this.tran;
						this.comm.Connection = this.conn;
					}
					else if (TC == TransactionComnmand.NO_TRANSACTION)
					{
						this.conn.Open();
					}
					this.comm.ExecuteNonQuery();
				}
				catch (SqlException e)
				{
					MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.tran.Rollback();
					this.conn.Close();
					result = false;
				}
				catch (Exception ex)
				{
					ErrorLog.WriteException(ex, ex.Message);
					MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.tran.Rollback();
					this.conn.Close();
					result = false;
				}

				if (TC == TransactionComnmand.COMMIT_TRANSACTION)// || 
				{
					this.tran.Commit();
					this.comm.Connection.Close();
				}
				else if (TC == TransactionComnmand.ROLLBACK_TRANSACION)
				{
					this.tran.Rollback();
					this.conn.Close();
				}
				else if (TC == TransactionComnmand.NO_TRANSACTION)
				{
					this.conn.Close();
				}

				return result;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		public bool UniversalDelete(string table, string id, string colname)
		{
			StringBuilder command = new StringBuilder();
			bool result = true;

			command.Append("DELETE FROM ");
			command.Append(table);
			command.Append(" WHERE ");
			command.Append(colname);
			command.Append(" = ");
			command.Append(id);

			comm.CommandText = command.ToString();
			try
			{
				comm.Connection.Open();
				comm.ExecuteNonQuery();
			}
			catch (SqlException e)
			{
				MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
				result = false;
			}
			comm.Connection.Close();
			return result;
		}

		public bool UniversalDelete(string table, string id, string colname, TransactionComnmand TC)
		{
			try
			{
				StringBuilder command = new StringBuilder();
				bool result = true;

				command.Append("DELETE FROM ");
				command.Append(table);
				command.Append(" WHERE ");
				command.Append(colname);
				command.Append(" = ");
				command.Append(id);

				this.comm.CommandText = command.ToString();
				try
				{
					if (TC == TransactionComnmand.BEGIN_TRANSACTION)
					{
						this.conn.Open();
						this.tran = conn.BeginTransaction();
						this.comm.Transaction = this.tran;
					}
					this.comm.ExecuteNonQuery();
				}
				catch (SqlException e)
				{
					MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.tran.Rollback();
					this.conn.Close();
					result = false;
				}
				catch (Exception ex)
				{
					ErrorLog.WriteException(ex, ex.Message);
					MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.tran.Rollback();
					this.conn.Close();
					result = false;
				}

				if (TC == TransactionComnmand.COMMIT_TRANSACTION)// || 
				{
					this.tran.Commit();
					this.comm.Connection.Close();
				}
				else if (TC == TransactionComnmand.ROLLBACK_TRANSACION)
				{
					this.tran.Rollback();
					this.conn.Close();
				}
				return result;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.tran.Rollback();
				this.conn.Close();
				return false;
			}
		}

		//public int UniversalInsert(string table, Dictionary<string, string> dList, TransactionComnmand TC)
		//{
		//    try
		//    {
		//        StringBuilder command = new StringBuilder();
		//        int id = -1;

		//        if ((dList != null) && (dList.Count > 0))
		//        {
		//            command.Append("INSERT INTO ");
		//            command.Append(table);
		//            command.Append(" (");
		//            foreach (KeyValuePair<string, string> kvp in dList)
		//            {
		//                command.Append(kvp.Key);
		//                command.Append(','); ;
		//            }

		//            command.Remove(command.Length - 1, 1); //trim the last ','
		//            command.Append(") VALUES(");

		//            foreach (KeyValuePair<string, string> kvp in dList)
		//            {
		//                if (kvp.Value.StartsWith("\'"))
		//                {
		//                    string str;
		//                    str = kvp.Value.Remove(0, 1);
		//                    command.Append(str);
		//                    command.Append(',');
		//                    continue;
		//                }
		//                command.Append('\'');
		//                command.Append(kvp.Value);
		//                command.Append('\'');
		//                command.Append(',');
		//            }

		//            command.Remove(command.Length - 1, 1); //trim the last ','
		//            command.Append(')');

		//            try
		//            {
		//                this.comm.CommandText = command.ToString();
		//                if (TC == TransactionComnmand.BEGIN_TRANSACTION)
		//                {
		//                    if (this.conn.State == ConnectionState.Open)
		//                    {
		//                        this.conn.Close();
		//                        this.conn.Open();
		//                    }
		//                    else
		//                    {
		//                        this.conn.Open();
		//                    }
		//                    this.tran = this.comm.Connection.BeginTransaction();
		//                    this.comm.Transaction = this.tran;
		//                    this.comm.Connection = this.conn;
		//                }
		//                comm.ExecuteNonQuery();
		//            }
		//            catch (SqlException e)
		//            {
		//                MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//                this.tran.Rollback();
		//                this.conn.Close();
		//                return -1;

		//            }
		//            catch (Exception ex)
		//            {
		//                ErrorLog.WriteException(ex, ex.Message);
		//                MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//                this.tran.Rollback();
		//                this.conn.Close();
		//                return -1;
		//            }

		//            id = GetLastInsertID(table, "id");
		//            if (id <= 0)
		//            {
		//                MessageBox.Show("Грешка при четене на идентификатор", "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//                this.tran.Rollback();
		//                this.conn.Close();
		//                return -1;
		//            }
		//        }
		//        try
		//        {
		//            if (TC == TransactionComnmand.COMMIT_TRANSACTION)// || 
		//            {
		//                this.tran.Commit();
		//                this.comm.Connection.Close();
		//            }
		//            else if (TC == TransactionComnmand.ROLLBACK_TRANSACION)
		//            {
		//                this.tran.Rollback();
		//                this.conn.Close();
		//                return -1;
		//            }
		//        }
		//        catch (Exception ex)
		//        {
		//            ErrorLog.WriteException(ex, "Грешка при четене на идентификатор");
		//            MessageBox.Show("Грешка при четене на идентификатор", "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//            this.tran.Rollback();
		//            this.conn.Close();
		//            return -1;
		//        }
		//        return id;
		//    }
		//    catch (Exception ex)
		//    {
		//        ErrorLog.WriteException(ex, ex.Message);
		//        MessageBox.Show("Грешка", "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);				
		//        this.conn.Close();
		//        return -1;
		//    }
		//}




		//public bool UniversalUpdate(string table, string id, Dictionary<string, string> dList, TransactionComnmand TC)
		//{
		//    bool result = true;
		//    try
		//    {
		//        StringBuilder command = new StringBuilder();

		//        if (dList != null && dList.Count > 0)
		//        {

		//            command.Append("UPDATE ");
		//            command.Append(table);
		//            command.Append(" SET ");
		//            foreach (KeyValuePair<string, string> kvp in dList)
		//            {
		//                command.Append(' ');
		//                command.Append(kvp.Key);
		//                command.Append(" = ");
		//                if (kvp.Value.StartsWith("\'"))
		//                {
		//                    string str;
		//                    str = kvp.Value.Remove(0, 1);
		//                    command.Append(str);
		//                    command.Append(',');
		//                    continue;
		//                }
		//                command.Append("'");
		//                command.Append(kvp.Value);
		//                command.Append("',");
		//            }

		//            command.Remove(command.Length - 1, 1); //trim the last ','
		//            command.Append(" WHERE id = ");
		//            command.Append(id);

		//            this.comm.CommandText = command.ToString();

		//            try
		//            {
		//                if (TC == TransactionComnmand.BEGIN_TRANSACTION)
		//                {
		//                    this.conn.Open();
		//                    this.tran = this.conn.BeginTransaction();
		//                    this.comm.Transaction = this.tran;
		//                }
		//                this.comm.ExecuteNonQuery();
		//            }
		//            catch (SqlException e)
		//            {
		//                MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//                this.tran.Rollback();
		//                this.conn.Close();
		//                result = false;
		//            }
		//            catch (Exception ex)
		//            {
		//                ErrorLog.WriteException(ex, ex.Message);
		//                MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//                this.tran.Rollback();
		//                this.conn.Close();
		//                result = false;
		//            }
		//        }

		//        if (TC == TransactionComnmand.COMMIT_TRANSACTION)// || 
		//        {
		//            this.tran.Commit();
		//            this.comm.Connection.Close();
		//        }
		//        else if (TC == TransactionComnmand.ROLLBACK_TRANSACION)
		//        {
		//            this.tran.Rollback();
		//            this.conn.Close();
		//        }
		//        return result;
		//    }
		//    catch (Exception ex)
		//    {
		//        ErrorLog.WriteException(ex, ex.Message);
		//        MessageBox.Show(ex.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
		//        this.tran.Rollback();
		//        this.conn.Close();
		//        return false;
		//    }
		//}

		public void RenameLevel(string level, string oldname, string newname)
		{
			string command;

			command = string.Format("UPDATE {4} SET {0} = '{1}' WHERE {2} = '{3}'", level, newname, level, oldname, TableNames.PersonAssignment);

			comm.CommandText = command;
			try
			{
				comm.Connection.Open();
				comm.ExecuteNonQuery();
			}
			catch (SqlException e)
			{
				MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				comm.Connection.Close();
			}
		}

		public bool UniversalUpdateWhere(string table, string where, Dictionary<string, object> dList)
		{
			try
			{
				StringBuilder command = new StringBuilder();
				bool result = true;

				if (dList.Count <= 0)
					return result;

				command.Append("UPDATE ");
				command.Append(table);
				command.Append(" SET ");
				foreach (KeyValuePair<string, object> kvp in dList)
				{
					command.Append(' ');
					command.Append(kvp.Key);
					command.Append(" = '");
					command.Append(kvp.Value);
					command.Append("',");
				}

				command.Remove(command.Length - 1, 1); //trim the last ','
				command.Append(" WHERE ");
				command.Append(where);

				comm.CommandText = command.ToString();

				try
				{
					comm.Connection.Open();
					comm.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
					ErrorLog.WriteException(ex, ex.Message);
					result = false;
					MessageBox.Show(ex.Message);
				}
				comm.Connection.Close();
				return result;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		public bool ExecuteCustom(string CustomQuery)
		{
			comm.CommandText = CustomQuery;
			try
			{
				comm.Connection.Open();
				comm.ExecuteNonQuery();
			}
			catch (SqlException e)
			{
				MessageBox.Show(e.Message, "Базата данни не е достъпна", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			finally
			{
				comm.Connection.Close();
			}
			return true;
		}

		public void UpdateHolidayNewYearShumen(string conns)
		{
			try
			{
				var data = new Entities(conns);
				var currentYear = data.HR_Year.Select(a => a).First();
				var year = currentYear.Year;
				int newyear = year + 1;

				var refDate = new DateTime(newyear, 12, 31);
				

				var lstAssignments = data.HR_PersonAssignment.Where(a => a.isActive == 1).ToList();

				foreach (var ass in lstAssignments)
				{
					var firstAssignment = data.HR_PersonAssignment.FirstOrDefault(a => a.parent == ass.parent && a.IsAdditionalAssignment == 0);
					
					
					var years = refDate.Year - firstAssignment.assignedAt.Value.Year;
					var sindik = ass.HR_Person.languages;
					if(sindik.ToLower() == "синдикален член")
					{
						if(ass.position.ToLower().Contains("асистент")
						|| ass.position.ToLower().Contains("доцент")
						|| ass.position.ToLower().Contains("професор")
						|| ass.position.ToLower().Contains("преподавател"))
						{
							ass.AdditionalHoliday = years / 4;
						}
						else
						{
							ass.AdditionalHoliday = years / 2;
						}
					}

					int nh = 0;
					int.TryParse(ass.NumHoliday, out nh);
					int total = (int)nh + (int)ass.AdditionalHoliday;
					var newYH = new HR_Year_Holiday();
					newYH.Additional = 0;
					newYH.Education = 0;
					newYH.parent = ass.parent;
					newYH.telk = 0;
					newYH.Unpayed = 30;
					newYH.year = newyear;
					newYH.total = total;
					newYH.leftover = total;

					data.HR_Year_Holiday.AddObject(newYH);
				}
				currentYear.Year = newyear;
				data.SaveChanges();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Грешка! Неуспешно приключване!");
				MessageBox.Show(ex.Message);
			}
		}
	}
}