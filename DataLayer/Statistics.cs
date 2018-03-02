using System;
using System.Data;
using System.Collections;
using System.Data.Common;
using System.Windows.Forms;
//using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace DataLayer
{
    /// <summary>
    /// Summary description for Statistics.
    /// </summary>
    public class DataStatistics
    {
        DataTable dt;
        DbCommand comm;
        DbConnection conn;
        DataAdapter da;
        

        public string JoinClause
        {
            get { return _join_clause; }
            set { _join_clause = value; }
        }

        public string WhereClause
        {
            get { return _where_clause; }
            set { _where_clause = value; }
        }

        void Constructor(string connString)
        {
            dt = new DataTable();
           
                    conn = new SqlConnection();
                    comm = new SqlCommand();
               
            conn.ConnectionString = connString;
            comm.Connection = conn;
        }

        private string _join_clause;
        private string _where_clause;

        public DataStatistics(string connString)
        {
            Constructor(connString);
        }

        public void FindPersonBy(string table, ArrayList coulmn, ArrayList values, string additional, bool IsActive, bool ShowEgn, ArrayList arrInvert)
        {
            System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
            System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
            string egn = "";
            string and = "' AND ";
            DataSet ds = new DataSet();
            if (ShowEgn)
            {
                egn = ", " + TableNames.Person + ".egn ";
            }
            for (int i = 0; i < coulmn.Count && i < values.Count; i++)
            {
                if (i == coulmn.Count - 1)
                {
                    and = "'";
                }
                string add = "";
                if (((bool)arrInvert[i]) == true)
                    add = " NOT ";
                if (values[i].ToString() != "")
                {
                    sb1.AppendFormat("{0} {1} like '{2}{3}", coulmn[i], add, values[i], and);
                }
                else
                {
                    sb1.AppendFormat("{0} {1} like '.%{2}", coulmn[i], add, and);
                }
                sb2.Append("," + coulmn[i]);

            }

            sb1.Append(additional);

            

            JoinClause = string.Format(" left join {1} on {0}.ID = {1}.parent ", TableNames.Person, TableNames.PersonAssignment);
            WhereClause = string.Format("{0}", sb1.ToString());
        }

        public void FindPersonByAssignment(string table, ArrayList coulmn, ArrayList values, string additional, ArrayList addColumn, bool IsFired, bool ActiveOnly, ArrayList arrInvert)
        {
            System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
            System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
            ArrayList arr = new ArrayList();
            DataTable dt1 = new DataTable();
            DataSet ds = new DataSet();
            string and = "' AND ";

            foreach (string str in addColumn)
            {
                sb2.Append(", " + str);
            }
            try
            {
                for (int i = 0; (i < coulmn.Count) && (i < values.Count); i++)
                {

                    if (i == coulmn.Count - 1)
                    {
                        and = "'";
                    }
                    string add = "";
                    if (((bool)arrInvert[i]) == true)
                        add = " NOT ";
                    if (values[i].ToString() != "")
                    {
                        sb1.AppendFormat(" {0} {1} like '{2}{3}", coulmn[i], add, values[i], and);
                    }
                    else
                    {
                        sb1.AppendFormat("{0} {1} like '.%{2}", coulmn[i], add, and);
                    }
                    sb2.Append("," + coulmn[i]);
                }
               

                if (values.Count == 0)
                {
                    WhereClause = string.Format("{0}", additional); ;
                }
                else if (coulmn.Count == 0)
                {
                    WhereClause = string.Format(" {0}{1} )", sb1.ToString(), additional);
                }
                else
                {
                    if (additional != "")
                    {
                        WhereClause = string.Format(" ( {0} {1} ) ", sb1.ToString(), additional);
                    }
                    else
                    {
                        WhereClause = string.Format(" ( {0}) ", sb1.ToString());
                    }
                }

                JoinClause = string.Format(" left join {1} on {0}.ID = {2}.parent ", TableNames.Person, table, table);
                if (ActiveOnly == false)
                {
                    if (WhereClause != "")
                    {
                        WhereClause += " AND IsAdditionalAssignment = 0 ";
                    }
                    else
                    {
                        WhereClause += " IsAdditionalAssignment = 0 ";
                    }
                }
                else
                {
                    if (WhereClause != "")
                    {
                        WhereClause += string.Format(" AND {0}.IsActive = 1", TableNames.PersonAssignment);
                    }
                    else
                    {
                        WhereClause = string.Format(" {0}.IsActive = 1", TableNames.PersonAssignment);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public void FindPersonByPenalty(string table, ArrayList coulmn, ArrayList values, ArrayList coulmnView, string additional, bool IsFired, ArrayList arrInvert)
        {
            System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
            System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
            System.Text.StringBuilder sb3 = new System.Text.StringBuilder();
            ArrayList arr = new ArrayList();
            DataTable dt1 = new DataTable();
            DataSet ds = new DataSet();
            
            try
            {
                string and = "' AND ";
                for (int i = 0; i < coulmn.Count; i++)
                {

                    if (i == coulmn.Count - 1)
                    {
                        and = "'";
                    }
                    string add = "";
                    if (((bool)arrInvert[i]) == true)
                        add = " NOT ";
                    if (values[i].ToString() != "")
                    {
                        sb1.AppendFormat(" {0} {1} like '{2}{3}", coulmn[i], add, values[i], and);
                    }
                    else
                    {
                        sb1.AppendFormat("{0} {1} like '.%{2}", coulmn[i], add, and);
                    }
                    sb2.Append("," + coulmn[i]);
                }

                for (int i = 0; i < coulmnView.Count; i++)
                {
                    sb3.Append("," + coulmnView[i]);
                }
                JoinClause = string.Format(" left join {1} on {0}.ID = {2}.parent ", TableNames.Person, table, table);
                WhereClause = string.Format(" {0}{1} ", sb1.ToString(), additional);

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                return;
            }
            return;
        }

        public void FindPersonByFired(string table, ArrayList coulmn, ArrayList values, string additional, bool IsFired, ArrayList arrInvert)
        {
            System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
            System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
            ArrayList arr = new ArrayList();
            DataTable dt1 = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                string and = "' AND ";
                for (int i = 0; i < coulmn.Count; i++)
                {
                    if (i == coulmn.Count - 1)
                    {
                        and = "'";
                    }
                    string add = "";
                    if (((bool)arrInvert[i]) == true)
                        add = " NOT ";
                    if (values[i].ToString() != "")
                    {
                        sb1.AppendFormat("  {0} {1} like '{2}{3}", coulmn[i], add, values[i], and);
                    }
                    else
                    {
                        sb1.AppendFormat("{0} {1} like '.%{2}", coulmn[i], add, and);
                    }
                    sb2.Append("," + coulmn[i]);
                }

                if (coulmn.Count == 0)
                {
                    this.WhereClause = string.Format(" {0} {1} ", sb1.ToString(), additional);
                }
                else
                {
                    if (additional != "")
                    {
                        this.WhereClause = string.Format(" ( {0}  {1} ) ", sb1.ToString(), additional);
                    }
                    else
                    {
                        this.WhereClause = string.Format(" ( {0} ) ", sb1.ToString());
                    }
                }
                this.JoinClause = string.Format(" left join {1} on {0}.ID = {2}.parent ", TableNames.Person, table, table);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                ErrorLog.WriteMessage(exc.Message);
            }
        }

        public void FindPersonByAbsence(string table, ArrayList coulmn, ArrayList values, string additional, bool IsFired, ArrayList arrInvert)
        {
            System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
            System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
            ArrayList arr = new ArrayList();
            DataTable dt1 = new DataTable();
            DataSet ds = new DataSet();
            
            try
            {
                string and = "') AND ";
                for (int i = 0; i < values.Count && i < coulmn.Count; i++)
                {

                    if (i == coulmn.Count - 1)
                    {
                        and = "'";
                    }
                    string add = "";
                    if (((bool)arrInvert[i]) == true)
                        add = " NOT ";
                    if (values[i].ToString() != "")
                    {
                        sb1.AppendFormat("{0} {1} like '{2}{3}", coulmn[i], add, values[i], and);
                    }
                    else
                    {
                        sb1.AppendFormat("{0} {1} like '.%{2}", coulmn[i], add, and);
                    }
                    sb2.Append("," + coulmn[i]);
                }

                if (values.Count == 0)
                {
                    WhereClause = string.Format("{0}", additional); ;
                }
                else if (coulmn.Count == 0)
                {
                    WhereClause = string.Format("{0} {1} )", sb1.ToString(), additional);
                }
                else
                {
                    if (additional != "")
                    {
                        WhereClause = string.Format("({0}) AND {1}", sb1.ToString(), additional);
                    }
                    else
                    {
                        WhereClause = string.Format("({0}) ", sb1.ToString());
                    }
                }
                JoinClause = string.Format(" left join {1} on {0}.ID = {2}.parent ", TableNames.Person, table, table);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public void FindPersonByAtestation(string table, ArrayList coulmn, ArrayList values, string additional, bool IsFired, bool IsCompareIncl, int year)
        {
            System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
            System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
            ArrayList arr = new ArrayList();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataSet ds1 = new DataSet();
            DataSet ds2 = new DataSet();
            DataSet ds3 = new DataSet();
            ArrayList arrId = new ArrayList();
            try
            {
                string fired;
                if (IsFired)
                {
                    fired = "1";
                }
                else
                {
                    fired = "0";
                }
                string and = "') AND ";
                for (int i = 0; i < coulmn.Count; i++)
                {
                    if (i == coulmn.Count - 1)
                    {
                        and = "'";
                    }
                    if (values[i].ToString() != "")
                    {
                        sb1.AppendFormat(" ( {0} like '{1}{2}", coulmn[i], values[i], and);
                    }
                    else
                    {
                        sb1.AppendFormat("{0} rlike '.*{1}", coulmn[i], and);
                    }
                    sb2.Append("," + coulmn[i]);
                }

                if (IsCompareIncl)
                {
                    this.comm.CommandText = string.Format("SELECT {0}.ID, {0}.name , {1}.id, {1}.totalmark FROM {0} left join {2} on {0}.ID = {3}.par  WHERE {1}.Year = '{4}'  AND {0}.fired = {5} group by {0}.ID", TableNames.Person, TableNames.Attestations, table, table, year.ToString(), fired);
                    CreateDataAdapter();
                    da.Fill(ds2);
                    dt2 = ds2.Tables[0];
                    this.comm.CommandText = string.Format("SELECT {0}.ID, {0}.name , {1}.id, {1}.totalmark FROM {0} left join {2} on {0}.ID = {3}.par WHERE {1}.Year = '{4}'  AND {0}.fired = {5} group by {0}.ID", TableNames.Person, TableNames.Attestations, table, table, (year - 1).ToString(), fired);
                    da.Fill(ds3);
                    dt3 = ds3.Tables[0];
                    foreach (DataRow row in dt2.Rows)
                    {
                        foreach (DataRow row2 in dt2.Rows)
                        {
                            if ((int)row[0] == (int)row2[0])
                                if ((uint)row[3] > (uint)row2[3])
                                {
                                    arrId.Add(row);
                                    break;
                                }
                        }
                    }
                    ds2.Dispose();
                    ds3.Dispose();


                }
                this.comm.CommandText = string.Format("SELECT {0}.ID, {0}.name, {1}.id {2} FROM {0} left join {3} on {0}.ID = {4}.par {5}{6}  AND {0}.fired = {7} group by {0}.ID", TableNames.Person, TableNames.Attestations, sb2.ToString(), table, table, sb1.ToString(), additional, fired);

                CreateDataAdapter();
                da.Fill(ds1);
                dt1 = ds1.Tables[0];
                if (IsCompareIncl)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        bool Exists = false;
                        for (int y = 0; y < arrId.Count; y++)
                        {
                            if (dt1.Rows[i][0] == ((DataRow)arrId[y])[0])
                            {
                                Exists = true;
                            }
                        }
                        if (!Exists)
                        {
                            dt1.Rows.RemoveAt(i);
                        }

                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public void FindPersonByMilitaryRang(string table, ArrayList coulmn, ArrayList values, string additional, bool IsFired, ArrayList arrInvert, bool isactive)
        {
            System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
            System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
            ArrayList arr = new ArrayList();
            DataTable dt1 = new DataTable();
            DataSet ds = new DataSet();

            try
            {
                string and = "' AND ";
                for (int i = 0; i < values.Count; i++)
                {

                    if (i == coulmn.Count - 1)
                    {
                        and = "'";
                    }
                    string add = "";
                    if (((bool)arrInvert[i]) == true)
                        add = " NOT ";
                    if (values[i].ToString() != "")
                    {
                        sb1.AppendFormat(" {0} {1} like '{2}{3}", coulmn[i], add, values[i], and);
                    }
                    else
                    {
                        sb1.AppendFormat("{0} {1} like '.%{2}", coulmn[i], add, and);
                    }
                    sb2.Append("," + coulmn[i]);
                }

                //JoinClause = string.Format(" left join {1} on {0}.ID = {1}.parent and {1}.isactive = 1 ", TableNames.Person, table);
                JoinClause = string.Format(" left join {1} on {0}.ID = {1}.parent ", TableNames.Person, table);
                if (isactive)
                {
                    JoinClause += string.Format(" and {0}.isactive = 1 ", table);
                }
                WhereClause = string.Format(" {0}{1} ", sb1.ToString(), additional);

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                return;
            }
            return;
        }

        void CreateDataAdapter(string selectCommand)
        {
           
                
                    this.da = new SqlDataAdapter(selectCommand, (SqlConnection)this.conn);
             
        }

        void CreateDataAdapter()
        {
           
               
                    this.da = new SqlDataAdapter((SqlCommand)this.comm);
                  
        }
    }
}
