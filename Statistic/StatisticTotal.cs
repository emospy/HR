using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using DataLayer;
using System.Collections.Generic;

namespace HR
{
    /// <summary>
    /// Summary description for Statistic.
    /// </summary>
    public class formStatisticTotal : System.Windows.Forms.Form
    {
        #region Items

        private ExcelExpo Ex;
        internal bool PersonalChecked = false;
        internal bool AssignmentChecked = false;
        internal bool AbsenceChecked = false;
        internal bool PenaltyChecked = false;
        internal bool FiredChecked = false;
        internal bool AtestationChecked = false;
        internal bool ActiveOnly = true;
        internal bool MilitaryRangsChecked = false;

        internal ArrayList arrColumnPenalty;
        internal ArrayList arrColumnAbsence;
        internal ArrayList arrColumnPersonal;
        internal ArrayList arrColumnAssignment;
        internal ArrayList arrColumnFired;
        internal ArrayList arrColumnAtestation;
        internal ArrayList arrColumnMilitaryRangs;

        internal ArrayList arrColumnAdd;
        private ArrayList arrDepartment = new ArrayList(), arrSector = new ArrayList(), arrDirectionNum = new ArrayList(), arrDirection, arrAdministration = new ArrayList(), arrMilitaryRangs = new ArrayList();
        private DataView vueDirection, vueDepartment, vueSector, vuePosition, vueAdministration;
        private DataTable dtTree;
        private DataTable dtPosition;

        //internal DataTable dtPenalty = new DataTable();
        //internal DataTable dtAssignment = new DataTable();
        //internal DataTable dtPersonal = new DataTable();
        //internal DataTable dtAbsence = new DataTable();
        //internal DataTable dtFired = new DataTable();
        //internal DataTable dtAtestation = new DataTable();

        private DataViewRowState dvrs;
        private int nodeID, administration;
        DataAction da;

        string NKPCode, EKDACode;
        mainForm main;
        private bool IsFiredd = false;
        //private bool IsTotalStat;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public DataTable dt1;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        //internal ArrayList arrColumnView;
        internal ArrayList arrColumn;
        private System.Windows.Forms.Button buttonFind;
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.CheckBox checkBoxExportToExcel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TabPersonalInfo;
        private System.Windows.Forms.TabPage TabPageAssignment;
        private System.Windows.Forms.TabPage tabPageAbsence;
        private System.Windows.Forms.TabPage tabPagePenalty;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxFormDate;
        private System.Windows.Forms.CheckBox checkBoxPenaltyDate;
        private System.Windows.Forms.DateTimePicker dateTimePickerFormDate2;
        private System.Windows.Forms.DateTimePicker dateTimePickerFormDate1;
        private System.Windows.Forms.DateTimePicker dateTimePickerPenaltyDate2;
        private System.Windows.Forms.DateTimePicker dateTimePickerPenaltyDate1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbPenalty;
        private CheckedCombo.CheckedCombo checkedComboTypeReason;
        private CheckedCombo.CheckedCombo checkedComboReason;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelYounger;
        private System.Windows.Forms.CheckBox checkBoxAge;
        private BugBox.NumBox numBoxYounger;
        private BugBox.NumBox numBoxOlder;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkBoxTestContractExpiraty;
        private System.Windows.Forms.DateTimePicker dateTimePickerTestContractExpiry2;
        private System.Windows.Forms.DateTimePicker dateTimePickerTestContractExpiry1;
        private System.Windows.Forms.CheckBox checkBoxAssignedAt;
        private System.Windows.Forms.CheckBox checkBoxContractExpiry;
        private System.Windows.Forms.DateTimePicker dateTimePickerContractExpiry2;
        private System.Windows.Forms.DateTimePicker dateTimePickerContractExpiry1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePickerAssignedAt2;
        private System.Windows.Forms.DateTimePicker dateTimePickerAssignedAt1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private BugBox.NumBox numBoxExpFrom;
        private BugBox.NumBox numBoxExpTo;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox checkBoxPayment;
        private BugBox.NumBox numBoxPaymentFrom;
        private BugBox.NumBox numBoxPaymentTo;
        private CheckedCombo.CheckedCombo checkedComboTypeAbsence;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox checkBoxFrom;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom2;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox gbAssignment;
        private System.Windows.Forms.GroupBox gbPersonal;
        private System.Windows.Forms.GroupBox gbAbsence;
        private CheckedCombo.CheckedCombo checkedComboSex;
        private CheckedCombo.CheckedCombo checkedComboScienceLevel;
        private CheckedCombo.CheckedCombo checkedComboCountry;
        private CheckedCombo.CheckedCombo checkedComboFamilyStatus;
        private CheckedCombo.CheckedCombo checkedComboLanguage;
        private CheckedCombo.CheckedCombo checkedComboMilitaryStatus;
        private System.Windows.Forms.CheckBox checkBoxNLK;
        private System.Windows.Forms.CheckBox checkBoxAdress;
        private CheckedCombo.CheckedCombo checkedComboReasonAssignment;
        private CheckedCombo.CheckedCombo checkedComboWorkTime;
        private CheckedCombo.CheckedCombo checkedComboContract;
        private CheckedCombo.CheckedCombo checkedComboProfessionn;
        private CheckedCombo.CheckedCombo checkedComboSector;
        private CheckedCombo.CheckedCombo checkedComboDepartment;
        private CheckedCombo.CheckedCombo checkedComboDirection;
        private CheckedCombo.CheckedCombo checkedComboAdministration;
        private CheckedCombo.CheckedCombo checkedContractType;
        private System.Windows.Forms.CheckBox checkBoxExp;
        private System.Windows.Forms.Button buttonSelectPosition;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private CheckedCombo.CheckedCombo checkedComboEducation;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private CheckedCombo.CheckedCombo checkedComboEKDA;
        private System.Windows.Forms.TabPage tabPageFired;
        private System.Windows.Forms.DateTimePicker dateTimePickerFiredFromDate;
        private CheckedCombo.CheckedCombo checkedComboxFiredReason;
        private System.Windows.Forms.GroupBox gbFired;
        private System.Windows.Forms.CheckBox checkBoxFiredFrom;
        private System.Windows.Forms.TabPage tabPageAtestacii;
        private System.Windows.Forms.GroupBox groupBoxAtestacii;
        private System.Windows.Forms.NumericUpDown numericUpDownAtestationYears;
        private System.Windows.Forms.CheckBox checkBoxAtestationEtaps;
        private System.Windows.Forms.CheckBox checkBoxAtestationRating;
        private System.Windows.Forms.CheckBox checkBoxAtestationCountRaised;
        private System.Windows.Forms.CheckBox checkBoxAtestationPersonalRaise;
        private System.Windows.Forms.NumericUpDown numericUpDownAtestationGrade;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label labelFiredMessage;
        private DateTimePicker dateTimePickerFiredТоDate;
        private Label label19;
        private CheckBox checkBoxActiveOnly;
        private CheckBox checkBoxEnglish;
        private CheckedCombo.CheckedCombo checkedComboTutorAbsenceReason;
        private Label label20;
        private TabPage tabPageRangs;
        private GroupBox gbMilitaryRangs;
        private CheckedCombo.CheckedCombo checkedComboMilitaryRang;
        private GroupBox groupBoxRangHistoty;
        private CheckBox checkBoxMilitaryRangFrom;
        private CheckBox checkBoxMilitaryRangOrderFrom;
        private DateTimePicker dateTimePickerMilitaryRangOrderTo;
        private DateTimePicker dateTimePickerMilitaryRangOrderFrom;
        private DateTimePicker dateTimePickerMilitaryRangTo;
        private DateTimePicker dateTimePickerMilitaryRangFrom;
        private Label label11;
        private Label label21;
        private Label label22;
        private Label label24;
        private CheckBox checkBoxBirthYear;
        private BugBox.NumBox numBoxBirthYear;
        private CheckBox checkBoxBirthMonth;
        private BugBox.NumBox numBoxBirthMonth;
        private CheckBox checkBoxBirthday;
        private BugBox.NumBox numBoxBirthDay;
        private CheckBox checkBoxLivingPlace;
        private CheckBox checkBoxBirthPlace;
        private TextBox textBoxLivingPlace;
        private TextBox textBoxBirthPlace;
        private TextBox textBoxRangNumberOrder;
        private CheckBox checkBoxRangNumberOrder;
        private TextBox textBoxFamily;
        private CheckBox checkBoxFamily;
        private TextBox textBoxSurName;
        private TextBox textBoxName;
        private CheckBox checkBoxSurName;
        private CheckBox checkBoxName;
        private CheckBox checkBoxSalaryAddon;
        private CheckBox checkBoxAbsenceManagement;
        private CheckBox checkBoxIDCardExpiry;
        private DateTimePicker dateTimePickerIDCardExpiresTo;
        private CheckBox checkBoxExperience;
        bool IsRunFromKartoteka = false;
        #endregion

        /// <summary>
        /// Required designer variable.
        /// </summary>
        public formStatisticTotal(mainForm main, bool IsRunFromKartoteka, bool IsFiredd)
        {
            this.main = main;
            this.IsFiredd = IsFiredd;
            this.IsRunFromKartoteka = IsRunFromKartoteka;
            this.Ex = new ExcelExpo();
            InitializeComponent();


            StatisticPersonal_Load();
            StatisticAdministration_Load();
            StatisticHoliday_Load();
            StatisticPenalty_Load();
            StatisticFired_Load();
            StatisticAtestation_Load();
            StatisticMilitaryRangs_Load();

            DataTable dtTabs = new DataTable();
            DataSet dsLabels = new DataSet();
            dsLabels.ReadXml(System.Windows.Forms.Application.StartupPath + @"\XMLLabels\PersonTabs.xml", System.Data.XmlReadMode.Auto);
            dtTabs = dsLabels.Tables["basicquery"];
            foreach (TabPage tp in this.tabControl1.TabPages)
            {
                foreach (DataRow Row in dtTabs.Rows)
                {
                    if (Row["value"].ToString().ToLower() == tp.Name.ToLower())
                    {
                        if (Row["visible"].ToString() != "true")
                            this.tabControl1.TabPages.Remove(tp);
                    }
                }
            }

            this.checkedComboProfessionn.combobox.SelectedIndexChanged += new EventHandler(combobox_SelectedIndexChanged);
            //			formPersonal = new StatisticPersonal( this.main, true, IsFiredd );
            //			formAssignment = new StatisticAssignment( this.main, true, IsFiredd);
            //			formAbsence = new StatisticAbsence( this.main, true);
            //			formPenalty = new StatisticPenalty( this.main, true );
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
        void CheckPenalty()
        {
            this.PenaltyChecked = false;

            arrColumn = new ArrayList();
            arrColumnPenalty = new ArrayList();
            ArrayList arrValues = new ArrayList();
            ArrayList arrInvert = new ArrayList();
            DataLayer.DataStatistics stat = new DataLayer.DataStatistics(this.main.connString);
            foreach (Control ctrl in this.gbPenalty.Controls)
            {
                if (ctrl is CheckedNumBox.CheckedNumBox)
                {
                    if (((CheckedNumBox.CheckedNumBox)ctrl).Checked)
                    {
                        this.PenaltyChecked = true;
                        arrValues.Add(((CheckedNumBox.CheckedNumBox)ctrl).NumBox.Text);
                        arrColumn.Add(TableNames.Prefix + ((CheckedNumBox.CheckedNumBox)ctrl).Column);
                        arrColumnPenalty.Add(((CheckedNumBox.CheckedNumBox)ctrl).Column);
                    }
                }
                if (ctrl is CheckedCombo.CheckedCombo)
                {
                    //this.AbsenceChecked = true;
                    //if (((CheckedCombo.CheckedCombo)ctrl).IsAllChecked)
                    //{
                    //    arrColumnAbsence.Add(TableNames.Prefix + ((CheckedCombo.CheckedCombo)ctrl).Column);
                    //}
                    //else
                    //{
                    //    arrColumn.Add(TableNames.Prefix + ((CheckedCombo.CheckedCombo)ctrl).Column);
                    //    arrColumnAbsence.Add(TableNames.Prefix + ((CheckedCombo.CheckedCombo)ctrl).Column);
                    //    if (((CheckedCombo.CheckedCombo)ctrl).combobox.SelectedItem.ToString() == "")
                    //    {
                    //        arrValues.Add(((CheckedCombo.CheckedCombo)ctrl).combobox.Text);
                    //    }
                    //    else
                    //    {
                    //        arrValues.Add(((CheckedCombo.CheckedCombo)ctrl).combobox.SelectedItem.ToString());
                    //    }
                    //    if (((CheckedCombo.CheckedCombo)ctrl).IsInverted == true)
                    //        arrInvert.Add(true);
                    //    else
                    //    {
                    //        arrInvert.Add(false);
                    //    }
                    //}

                    if (((CheckedCombo.CheckedCombo)ctrl).Checked)
                    {
                        this.PenaltyChecked = true;

                        if (((CheckedCombo.CheckedCombo)ctrl).IsAllChecked)
                        {
                            arrColumnPenalty.Add(TableNames.Prefix + ((CheckedCombo.CheckedCombo)ctrl).Column);
                        }
                        else
                        {
                            arrColumn.Add(TableNames.Prefix + ((CheckedCombo.CheckedCombo)ctrl).Column);
                            arrColumnPenalty.Add(TableNames.Prefix + ((CheckedCombo.CheckedCombo)ctrl).Column);

                            if ((((CheckedCombo.CheckedCombo)ctrl).combobox.SelectedIndex < 0) || ((CheckedCombo.CheckedCombo)ctrl).combobox.SelectedItem.ToString() == "")
                            {
                                arrValues.Add("");
                            }
                            else
                            {
                                arrValues.Add(((CheckedCombo.CheckedCombo)ctrl).combobox.SelectedItem.ToString());
                            }
                        }
                        if (((CheckedCombo.CheckedCombo)ctrl).IsInverted == true)
                            arrInvert.Add(true);
                        else
                        {
                            arrInvert.Add(false);
                        }
                    }

                }
            }
            string additional = "";
            string dat1 = DataAction.ConvertDateTimeToMySql(dateTimePickerPenaltyDate1.Value);
            string dat2 = DataAction.ConvertDateTimeToMySql(dateTimePickerPenaltyDate2.Value);
            if (this.checkBoxPenaltyDate.Checked)
            {
                this.PenaltyChecked = true;
                if (arrValues.Count == 0)
                {
                    additional = string.Format("( {6}.FromDate >= {0} AND {6}.FromDate <= {1}) OR ({6}.ToDate >= {2} AND {6}.ToDate <= {3}) OR ({6}.FromDate <= {4}  AND {6}.ToDate >= {5})", dat1, dat2, dat1, dat2, dat1, dat2, TableNames.Penalty);
                }
                else
                {	//if we have columns - > probably even better values - we will prepend AND to make it connect
                    additional = string.Format("AND (( {6}.FromDate >= {0} AND {6}.FromDate <= {1}) OR ({6}.ToDate >= {2} AND {6}.ToDate <= {3}) OR ({6}.FromDate <= {4} AND {6}.ToDate >= {5}) )", dat1, dat2, dat1, dat2, dat1, dat2, TableNames.Penalty);
                }
            }

            if (this.PenaltyChecked)
            {
                arrColumnPenalty.Add(TableNames.Penalty + ".FromDate");
                arrColumnPenalty.Add(TableNames.Penalty + ".ToDate");
                arrColumnPenalty.Add(TableNames.Penalty + ".orderdate");
                stat.FindPersonByPenalty(TableNames.Penalty, arrColumn, arrValues, arrColumnPenalty, additional, this.IsFiredd, arrInvert);
                this.join_clause += stat.JoinClause;
                if ((this.where_clause != "") && (stat.WhereClause != ""))
                {
                    this.where_clause += " AND ";
                }
                if (stat.WhereClause.Trim() != "")
                {
                    this.where_clause += "(";
                    this.where_clause += stat.WhereClause;
                    this.where_clause += ")";
                }
            }
        }
        string join_clause;
        string where_clause;
        void CheckPersonal()
        {
            this.PersonalChecked = false;
            //this.dtPersonal = new DataTable();
            string additional = "";
            bool IsOnlyYears = true;
            bool ShowEgn = false;
            bool IsInclude = false;
            arrColumnPersonal = new ArrayList();
            arrColumn = new ArrayList();
            ArrayList arrInvert = new ArrayList();
            ArrayList arrValues = new ArrayList();
            DataLayer.DataStatistics stat = new DataLayer.DataStatistics(this.main.connString);
            foreach (Control ctrl in this.gbPersonal.Controls)
            {
                if (ctrl is CheckedCombo.CheckedCombo)
                {
                    if (((CheckedCombo.CheckedCombo)ctrl).Checked)
                    {
                        this.PersonalChecked = true;
                        /* In AccsesibilityName se namira dannite za syotwetnata kolona
                        a wyw accessible description se namira syotwetno izbranata stoynost 
                        w combobox'a
                        */
                        if (((CheckedCombo.CheckedCombo)ctrl).IsAllChecked)
                        {
                            arrColumnPersonal.Add(TableNames.Prefix + ((CheckedCombo.CheckedCombo)ctrl).Column);
                        }
                        else
                        {
                            IsOnlyYears = false;
                            arrColumn.Add(TableNames.Prefix + ((CheckedCombo.CheckedCombo)ctrl).Column);
                            arrColumnPersonal.Add(TableNames.Prefix + ((CheckedCombo.CheckedCombo)ctrl).Column);
                            if (((CheckedCombo.CheckedCombo)ctrl).combobox.SelectedItem.ToString() == "")
                            {
                                arrValues.Add(((CheckedCombo.CheckedCombo)ctrl).combobox.Text);
                            }
                            else
                            {
                                object Item = ((CheckedCombo.CheckedCombo)ctrl).combobox.SelectedItem;
                                if (Item is DataRowView)
                                {
                                    DataRowView r = (DataRowView)Item;
                                    if (r[0].ToString() != "")
                                        arrValues.Add(r[0]);
                                }
                                else
                                {
                                    arrValues.Add(((CheckedCombo.CheckedCombo)ctrl).combobox.SelectedItem.ToString());
                                }
                            }

                            if (((CheckedCombo.CheckedCombo)ctrl).IsInverted == true)
                                arrInvert.Add(true);
                            else
                            {
                                arrInvert.Add(false);
                            }
                        }
                    }
                }
            }
            if (arrValues.Count > 0)
            {
                IsInclude = true;
            }

            #region Dates and intervals

            string dat1 = DataAction.ConvertDateTimeToMySql(this.dateTimePickerIDCardExpiresTo.Value);

            if (this.checkBoxIDCardExpiry.Checked)
            {
                this.PersonalChecked = true;
                arrColumnAdd.Add(TableNames.Person + ".pcardExpiry");
                arrColumnPersonal.Add(TableNames.Person + ".pcardExpiry");
                if (IsInclude)
                {
                    additional += "AND (";
                    //additional += DataAction.DateComparison(this.dateTimePickerIDCardExpiresTo.Value, ComparisonOperators.eGreater, TableNames.Person, "pcardExpiry");

                    additional += DataAction.DateComparison(this.dateTimePickerIDCardExpiresTo.Value, ComparisonOperators.eLess, TableNames.Person, "pcardExpiry");
                    additional += ")";
                    //additional = DataAction.DateComparison(this.dateTimePickerAssignedAt1.Value, ComparisonOperators.eGreater, TableNames.PersonAssignment, "assignedat");
                    //additional = string.Format(" AND ({2}.assignedat BETWEEN {0} AND {1} ) ", dat1, dat2, TableNames.PersonAssignment);
                }
                else
                {
                    additional += "(";
                    //additional += DataAction.DateComparison(this.dateTimePickerIDCardExpiresTo.Value, ComparisonOperators.eGreater, TableNames.Person, "pcardExpiry");

                    additional += DataAction.DateComparison(this.dateTimePickerIDCardExpiresTo.Value, ComparisonOperators.eLess, TableNames.Person, "pcardExpiry");
                    additional += ")";

                    //additional = string.Format(" ( {2}.assignedat BETWEEN {0} AND {1} ) ", dat1, dat2, TableNames.PersonAssignment);
                    IsInclude = true;
                }
            }

            #endregion

            if (this.checkBoxAge.Checked)
            {
                string temp = "";
                this.PersonalChecked = true;
                if (this.numBoxYounger.Text != "" || this.numBoxOlder.Text != "")
                {
                    //if (mainForm.DataBaseTypes == DBTypes.MySql)
                    //{
                    //	if (this.numBoxYounger.Text != "" && this.numBoxOlder.Text != "")
                    //	{
                    //		temp =
                    //			String.Format(
                    //				" DATE_SUB(CURRENT_TIMESTAMP, INTERVAL {0} YEAR) >  " + TableNames.Person + ".bornDate AND DATE_SUB(CURRENT_TIMESTAMP, INTERVAL {1} + 1 YEAR) <  " + TableNames.Person + ".bornDate",
                    //				this.numBoxYounger.Text, this.numBoxOlder.Text);
                    //		//temp =  " DATEDIFF( CURRENT_DATE,  "+ TableNames.Person + ".bornDate )/365 <" +this.numBoxOlder.Text + " and DATEDIFF( CURRENT_DATE,  "+ TableNames.Person + ".bornDate )/365 > "+ this.numBoxYounger.Text;
                    //	}
                    //	else
                    //	{
                    //		if (this.numBoxOlder.Text != "")
                    //		{
                    //			temp = string.Format(" DATEDIFF( CURRENT_TIMESTAMP,  {0}.bornDate )/365 <{1}", TableNames.Person, this.numBoxOlder.Text);
                    //		}
                    //		else
                    //		{
                    //			temp = string.Format(" DATEDIFF( CURRENT_TIMESTAMP,  {0}.bornDate )/365 > {1}", TableNames.Person, this.numBoxYounger.Text);
                    //		}
                    //	}
                    //}
                    //else if (mainForm.DataBaseTypes == DBTypes.MsSql) // Ms sql
                    //{
                    if (this.numBoxYounger.Text != "" && this.numBoxOlder.Text != "")
                    {
                        int from = int.Parse(this.numBoxYounger.Text);
                        int to = int.Parse(this.numBoxOlder.Text);
                        temp = String.Format(
    " (GETDATE() - {0}) >  " + TableNames.Person + ".borndate AND (GETDATE() - {1}) <  " + TableNames.Person + ".bornDate",
    from * 365, to * 365 + 365);
                        //temp =  " DATEDIFF( CURRENT_DATE,  "+ TableNames.Person + ".bornDate )/365 <" +this.numBoxOlder.Text + " and DATEDIFF( CURRENT_DATE,  "+ TableNames.Person + ".bornDate )/365 > "+ this.numBoxYounger.Text;
                    }
                    else
                    {
                        if (this.numBoxOlder.Text != "")
                        {
                            int to = int.Parse(this.numBoxOlder.Text);
                            temp = String.Format(
                                " (GETDATE() - {0}) <  " + TableNames.Person + ".bornDate",
                                to * 365 + 365);
                        }
                        else
                        {
                            int from = int.Parse(this.numBoxYounger.Text);
                            temp = String.Format(
                            " (GETDATE() - {0}) >  " + TableNames.Person + ".bornDate",
                            from * 365);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Попълнете интервал от години по който ще се прави справка");
                }
                if (!IsOnlyYears)
                {
                    additional = " and " + temp;
                }
                else
                {
                    additional = temp;
                }
            }

            //}
            if (checkBoxNLK.Checked)
            {
                this.arrColumnPersonal.Add(TableNames.Person + ".Pcard");
                this.arrColumnPersonal.Add(TableNames.Person + ".PCardPublish");
                this.arrColumnPersonal.Add(TableNames.Person + ".PublishedBy");
            }
            if (this.checkedComboLanguage.Checked)
            {
                this.join_clause += string.Format(" left join {0} on {0}.parent = {1}.id  ", TableNames.LanguageLevel, TableNames.Person);
            }
            if (checkBoxAdress.Checked)
            {
                this.arrColumnPersonal.Add(TableNames.Person + ".Region");
                this.arrColumnPersonal.Add(TableNames.Person + ".bornTown");
                this.arrColumnPersonal.Add(TableNames.Person + ".Town");
                this.arrColumnPersonal.Add(TableNames.Person + ".Kwartal");
                this.arrColumnPersonal.Add(TableNames.Person + ".Street");
                this.arrColumnPersonal.Add(TableNames.Person + ".NumBlockHouse");
            }

            if (this.checkBoxBirthday.Checked)
            {
                if (where_clause.Trim() != "")
                {
                    where_clause += " AND ";
                }
                this.where_clause += " ( DAYOFMONTH(borndate) = " + this.numBoxBirthDay.Text + ") ";
                this.PersonalChecked = true;
            }

            if (this.checkBoxBirthMonth.Checked)
            {
                if (where_clause.Trim() != "")
                {
                    where_clause += " AND ";
                }
                this.where_clause += " ( MONTH(borndate) = " + this.numBoxBirthMonth.Text + ") ";
                this.PersonalChecked = true;
            }

            if (this.checkBoxBirthYear.Checked)
            {
                if (where_clause.Trim() != "")
                {
                    where_clause += " AND ";
                }
                this.where_clause += " ( YEAR(borndate) = " + this.numBoxBirthYear.Text + ") ";
                this.PersonalChecked = true;
            }

            if (this.checkBoxBirthPlace.Checked)
            {
                if (where_clause.Trim() != "")
                {
                    where_clause += " AND ";
                }
                this.where_clause += " ( borntown like '" + this.textBoxBirthPlace.Text + "') ";
                this.arrColumnPersonal.Add(TableNames.Person + ".bornTown");
                this.PersonalChecked = true;
            }

            if (this.checkBoxLivingPlace.Checked)
            {
                if (where_clause.Trim() != "")
                {
                    where_clause += " AND ";
                }
                this.where_clause += " ( town like '" + this.textBoxLivingPlace.Text + "') ";
                this.arrColumnPersonal.Add(TableNames.Person + ".Town");
                this.PersonalChecked = true;
            }

            if (this.checkBoxName.Checked)
            {
                if (where_clause.Trim() != "")
                {
                    where_clause += " AND ";
                }
                this.where_clause += " ( name like '" + this.textBoxName.Text + " %') ";
                this.PersonalChecked = true;
            }

            if (this.checkBoxAge.Checked || this.checkBoxBirthMonth.Checked || this.checkBoxBirthday.Checked || this.checkBoxBirthYear.Checked)
            {
                arrColumnPersonal.Add("borndate");
            }

            if (this.checkBoxSurName.Checked)
            {
                if (where_clause.Trim() != "")
                {
                    where_clause += " AND ";
                }
                this.where_clause += " ( name like '% " + this.textBoxSurName.Text + " %') ";
                this.PersonalChecked = true;
            }

            if (this.checkBoxFamily.Checked)
            {
                if (where_clause.Trim() != "")
                {
                    where_clause += " AND ";
                }
                this.where_clause += " ( name like '% " + this.textBoxFamily.Text + "') ";
                this.PersonalChecked = true;
            }

            if (this.checkBoxEnglish.Checked)
            {
                this.arrColumnPersonal.Add(TableNames.Person + ".engeducation");
                this.arrColumnPersonal.Add(TableNames.Person + ".engname");
            }

            if (this.PersonalChecked)
            {
                stat.FindPersonBy(TableNames.Person, arrColumn, arrValues, additional, this.IsFiredd, ShowEgn, arrInvert);
                if (stat.JoinClause.ToLower().Contains((" left join " + TableNames.PersonAssignment + " on").ToLower()) == false)
                    this.join_clause += stat.JoinClause;
                if ((this.where_clause != "") && (stat.WhereClause != ""))
                {
                    this.where_clause += " AND ";
                }
                if (stat.WhereClause != "")
                {
                    this.where_clause += "(";
                    this.where_clause += stat.WhereClause;
                    this.where_clause += ")";
                }
            }


        }
        void CheckAssignment()
        {
            try
            {
                this.AssignmentChecked = false;
                bool IsInclude = false;
                arrColumnAdd = new ArrayList();
                ArrayList arrColumn = new ArrayList();
                ArrayList arrValues = new ArrayList();
                arrColumnAssignment = new ArrayList();
                ArrayList arrInvert = new ArrayList();

                DataLayer.DataStatistics stat = new DataLayer.DataStatistics(this.main.connString);

                this.arrAdministration.Clear();
                this.arrDepartment.Clear();
                this.arrDirectionNum.Clear();
                this.arrSector.Clear();

                string cond = "par = ";

                foreach (Control ctrl in this.gbAssignment.Controls)
                {
                    if (ctrl is CheckedNumBox.CheckedNumBox)
                    {
                        if (((CheckedNumBox.CheckedNumBox)ctrl).Checked)
                        {
                            this.AssignmentChecked = true;
                            break; //Ако имаме една чекната контрола, значи като цяло назначенията са чекнати 
                        }
                    }
                    if (ctrl is CheckedCombo.CheckedCombo)
                    {
                        if (((CheckedCombo.CheckedCombo)ctrl).Checked)
                        {
                            this.AssignmentChecked = true;
                            break; //Ако имаме една чекната контрола, значи като цяло назначенията са чекнати
                        }
                    }
                    if (ctrl is CheckBox)
                    {
                        if (((CheckBox)ctrl).Checked)
                        {
                            this.AssignmentChecked = true;
                            break; //Ако имаме една чекната контрола, значи като цяло назначенията са чекнати
                        }
                    }
                }

                if (this.checkBoxAssignedAt.Checked)
                {
                    this.AssignmentChecked = true;
                }
                if (this.checkBoxContractExpiry.Checked)
                {
                    this.AssignmentChecked = true;
                }
                if (this.checkBoxTestContractExpiraty.Checked)
                {
                    this.AssignmentChecked = true;
                }
                if (this.checkBoxPayment.Checked)
                {
                    this.AssignmentChecked = true;
                }
                if (this.checkBoxExp.Checked)
                {
                    this.AssignmentChecked = true;
                }
                if (this.AssignmentChecked == false)
                {
                    return;
                }

                if (this.checkedComboAdministration.Checked && this.checkedComboAdministration.SelectedIndex > 0)
                {
                    this.administration = int.Parse(this.vueAdministration[this.checkedComboAdministration.SelectedIndex - 1]["id"].ToString());
                    cond = "par = " + this.administration.ToString();
                    for (int j = 0; j < this.vueAdministration.Count; j++)
                    {
                        this.arrAdministration.Add(vueAdministration[j]["id"]);
                    }
                    #region Direction
                    if (this.checkedComboDirection.Checked && this.checkedComboDirection.SelectedIndex > 0)
                    {
                        this.arrDirection.Add(this.vueDirection[this.checkedComboDirection.SelectedIndex - 1]["id"].ToString());
                        #region Department_Control
                        if (this.checkedComboDepartment.Checked && this.checkedComboDepartment.combobox.SelectedIndex > 0)
                        {
                            this.arrDepartment.Add(this.vueDepartment[this.checkedComboDepartment.combobox.SelectedIndex - 1]["id"]);
                            if (this.checkedComboSector.Checked && this.checkedComboSector.combobox.SelectedIndex > 0)
                            {
                                this.arrSector.Add(this.vueSector[this.checkedComboSector.combobox.SelectedIndex - 1]["id"]);
                            }
                            else
                            {
                                cond = "par = " + this.vueDepartment[this.checkedComboDepartment.combobox.SelectedIndex - 1]["id"].ToString();
                                vueSector = new DataView(dtTree, cond, "level", dvrs);
                                for (int z = 0; z < this.vueSector.Count; z++)
                                {
                                    this.arrSector.Add(vueSector[z]["id"]);
                                }
                            }
                        }
                        else
                        {
                            cond = "par = " + this.vueDirection[this.checkedComboDirection.combobox.SelectedIndex - 1]["id"].ToString();
                            vueDepartment = new DataView(dtTree, cond, "level", dvrs);
                            for (int j = 0; j < this.vueDepartment.Count; j++)
                            {
                                this.arrDepartment.Add(vueDepartment[j]["id"]);
                                cond = "par = " + vueDepartment[j]["id"].ToString();
                                vueSector = new DataView(dtTree, cond, "level", dvrs);
                                for (int z = 0; z < this.vueSector.Count; z++)
                                {
                                    this.arrSector.Add(vueSector[z]["id"]);
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        vueDirection = new DataView(dtTree, cond, "level", dvrs);
                        for (int i = 0; i < this.vueDirection.Count; i++)
                        {
                            this.arrDirectionNum.Add(vueDirection[i]["id"]);
                            cond = "par = " + vueDirection[i]["id"].ToString();
                            vueDepartment = new DataView(dtTree, cond, "level", dvrs);
                            for (int j = 0; j < this.vueDepartment.Count; j++)
                            {
                                this.arrDepartment.Add(vueDepartment[j]["id"]);
                                cond = "par = " + vueDepartment[j]["id"].ToString();
                                vueSector = new DataView(dtTree, cond, "level", dvrs);
                                for (int z = 0; z < this.vueSector.Count; z++)
                                {
                                    this.arrSector.Add(vueSector[z]["id"]);
                                }
                            }
                            //this.checkedComboDirection.combobox.Items.Add(vueDirection[i]["level"]);
                        }
                    }
                    #endregion // Direction
                }
                else if (checkedComboAdministration.Checked && checkedComboAdministration.SelectedIndex == 0)
                {

                }


                foreach (Control ctrl in this.gbAssignment.Controls)
                {
                    if (ctrl is CheckedNumBox.CheckedNumBox)
                    {
                        if (((CheckedNumBox.CheckedNumBox)ctrl).Checked)
                        {
                            this.AssignmentChecked = true;
                            arrValues.Add(((CheckedNumBox.CheckedNumBox)ctrl).NumBox.Text);
                            arrColumn.Add(TableNames.Prefix + TableNames.Prefix + ((CheckedNumBox.CheckedNumBox)ctrl).Column);
                            arrColumnAssignment.Add(((CheckedNumBox.CheckedNumBox)ctrl).Column);
                        }
                    }
                    if (ctrl is CheckedCombo.CheckedCombo)
                    {
                        if (((CheckedCombo.CheckedCombo)ctrl).Checked)
                        {
                            this.AssignmentChecked = true;
                            if (((CheckedCombo.CheckedCombo)ctrl).IsAllChecked)
                            {
                                arrColumnAssignment.Add(TableNames.Prefix + ((CheckedCombo.CheckedCombo)ctrl).Column);
                            }
                            else
                            {
                                if (((CheckedCombo.CheckedCombo)ctrl).Column != "")
                                {
                                    arrColumn.Add(TableNames.Prefix + ((CheckedCombo.CheckedCombo)ctrl).Column);
                                    arrColumnAssignment.Add(TableNames.Prefix + ((CheckedCombo.CheckedCombo)ctrl).Column);
                                    if (((CheckedCombo.CheckedCombo)ctrl).IsInverted == true)
                                        arrInvert.Add(true);
                                    else
                                    {
                                        arrInvert.Add(false);
                                    }
                                    if (((CheckedCombo.CheckedCombo)ctrl).combobox.SelectedItem.ToString() == "")
                                    {
                                        if (ctrl.Name.ToString() == "checkedComboEKDA")
                                        {
                                            arrValues.Add(((CheckedCombo.CheckedCombo)ctrl).combobox.Text.Substring(0, 1) + "%");
                                        }
                                        else if (((CheckedCombo.CheckedCombo)ctrl).combobox.Text != "")
                                        {
                                            arrValues.Add(((CheckedCombo.CheckedCombo)ctrl).combobox.Text);
                                        }
                                    }
                                    else
                                    {
                                        if (ctrl.Name.ToString() == "checkedComboEKDA")
                                        {
                                            arrValues.Add(((CheckedCombo.CheckedCombo)ctrl).combobox.SelectedItem.ToString().Substring(0, 1) + "%");
                                        }
                                        else
                                        {
                                            object Item = ((CheckedCombo.CheckedCombo)ctrl).combobox.SelectedItem;
                                            if (Item is DataRowView)
                                            {
                                                DataRowView r = (DataRowView)Item;
                                                arrValues.Add(r[0]);
                                            }
                                            else
                                            {
                                                arrValues.Add(((CheckedCombo.CheckedCombo)ctrl).combobox.SelectedItem.ToString());
                                            }
                                        }
                                        //arrValues.Add(((CheckedCombo.CheckedCombo)ctrl).combobox.SelectedItem.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
                if (arrValues.Count > 0)
                {
                    IsInclude = true;
                }
                string additional = "";

                #region Dates and intervals

                string dat1 = DataAction.ConvertDateTimeToMySql(this.dateTimePickerAssignedAt1.Value);
                string dat2 = DataAction.ConvertDateTimeToMySql(dateTimePickerAssignedAt2.Value);

                if (this.checkBoxAssignedAt.Checked)
                {
                    this.AssignmentChecked = true;
                    arrColumnAdd.Add(TableNames.PersonAssignment + ".AssignedAt");
                    arrColumnAssignment.Add(TableNames.PersonAssignment + ".AssignedAt");
                    if (IsInclude)
                    {
                        additional += "AND (";
                        additional += DataAction.DateComparison(this.dateTimePickerAssignedAt1.Value, ComparisonOperators.eGreater, TableNames.PersonAssignment, "AssignedAt");

                        additional += " AND " + DataAction.DateComparison(this.dateTimePickerAssignedAt2.Value, ComparisonOperators.eLess, TableNames.PersonAssignment, "AssignedAt");
                        additional += ")";
                        //additional = DataAction.DateComparison(this.dateTimePickerAssignedAt1.Value, ComparisonOperators.eGreater, TableNames.PersonAssignment, "assignedat");
                        //additional = string.Format(" AND ({2}.assignedat BETWEEN {0} AND {1} ) ", dat1, dat2, TableNames.PersonAssignment);
                    }
                    else
                    {
                        additional += "(";
                        additional += DataAction.DateComparison(this.dateTimePickerAssignedAt1.Value, ComparisonOperators.eGreater, TableNames.PersonAssignment, "AssignedAt");

                        additional += " AND " + DataAction.DateComparison(this.dateTimePickerAssignedAt2.Value, ComparisonOperators.eLess, TableNames.PersonAssignment, "AssignedAt");
                        additional += ")";

                        //additional = string.Format(" ( {2}.assignedat BETWEEN {0} AND {1} ) ", dat1, dat2, TableNames.PersonAssignment);
                        IsInclude = true;
                    }
                }

                string dat3 = DataAction.ConvertDateTimeToMySql(this.dateTimePickerContractExpiry1.Value);

                string dat4 = DataAction.ConvertDateTimeToMySql(this.dateTimePickerContractExpiry2.Value);

                if (this.checkBoxContractExpiry.Checked)
                {
                    this.AssignmentChecked = true;
                    arrColumnAdd.Add(TableNames.PersonAssignment + ".ContractExpiry");
                    arrColumnAssignment.Add(TableNames.PersonAssignment + ".ContractExpiry");
                    if (IsInclude)
                    {
                        additional += "AND (";
                        additional += DataAction.DateComparison(this.dateTimePickerContractExpiry1.Value, ComparisonOperators.eGreater, TableNames.PersonAssignment, "ContractExpiry");

                        additional += " AND " + DataAction.DateComparison(this.dateTimePickerContractExpiry2.Value, ComparisonOperators.eLess, TableNames.PersonAssignment, "ContractExpiry");
                        additional += ")";

                        //additional += string.Format(" AND ( {2}.contractexpiry BETWEEN {0} AND {1} ) ", dat3, dat4, TableNames.PersonAssignment);
                    }
                    else
                    {

                        additional += " (";
                        additional += DataAction.DateComparison(this.dateTimePickerContractExpiry1.Value, ComparisonOperators.eGreater, TableNames.PersonAssignment, "ContractExpiry");

                        additional += " AND " + DataAction.DateComparison(this.dateTimePickerContractExpiry2.Value, ComparisonOperators.eLess, TableNames.PersonAssignment, "ContractExpiry");
                        additional += ")";

                        //additional += string.Format(" ( {2}.contractexpiry BETWEEN {0} AND {1} ) ", dat3, dat4, TableNames.PersonAssignment);
                        IsInclude = true;
                    }
                    additional += string.Format(" AND ( {0}.Contract = 'Срочен' OR {0}.Contract = 'Срочен със срок на изпитване' ) ", TableNames.PersonAssignment);
                }

                string dat5 = DataAction.ConvertDateTimeToMySql(this.dateTimePickerTestContractExpiry1.Value);

                string dat6 = DataAction.ConvertDateTimeToMySql(this.dateTimePickerTestContractExpiry2.Value);

                if (this.checkBoxTestContractExpiraty.Checked)
                {
                    this.AssignmentChecked = true;
                    arrColumnAdd.Add(TableNames.PersonAssignment + ".TestContractDate");
                    arrColumnAssignment.Add(TableNames.PersonAssignment + ".TestContractDate");
                    if (IsInclude)
                    {
                        additional += "AND (";
                        additional += DataAction.DateComparison(this.dateTimePickerTestContractExpiry1.Value, ComparisonOperators.eGreater, TableNames.PersonAssignment, "TestContractDate");

                        additional += " AND " + DataAction.DateComparison(this.dateTimePickerTestContractExpiry2.Value, ComparisonOperators.eLess, TableNames.PersonAssignment, "TestContractDate");
                        additional += ")";
                        //additional += string.Format(" AND ( {2}.TestContractDate BETWEEN {0} AND {1} ) ", dat5, dat6, TableNames.PersonAssignment);
                    }
                    else
                    {
                        additional += "(";
                        additional += DataAction.DateComparison(this.dateTimePickerTestContractExpiry1.Value, ComparisonOperators.eGreater, TableNames.PersonAssignment, "TestContractDate");

                        additional += " AND " + DataAction.DateComparison(this.dateTimePickerTestContractExpiry2.Value, ComparisonOperators.eLess, TableNames.PersonAssignment, "TestContractDate");
                        additional += ")";
                        //additional += string.Format(" ( {2}.TestContractDate BETWEEN {0} AND {1} ) ", dat5, dat6, TableNames.PersonAssignment);
                        IsInclude = true;
                    }
                    additional += string.Format(" AND ({0}.Contract = 'Срочен със срок на изпитване' OR {0}.Contract = 'Безсрочен със срок на изпитване' OR {0}.Contract = 'Безсрочен' ) ", TableNames.PersonAssignment);

                }
                //-----------------------
                string s = "";
                if (this.checkBoxPayment.Checked)
                {
                    this.AssignmentChecked = true;
                    arrColumnAdd.Add(TableNames.PersonAssignment + ".baseSalary");
                    arrColumnAssignment.Add(TableNames.PersonAssignment + ".baseSalary");
                    if (this.numBoxPaymentFrom.Text != "")
                    {
                        s = " AND " + TableNames.PersonAssignment + ".baseSalary >= " + this.numBoxPaymentFrom.Text;
                        if (IsInclude)
                        {
                            s = " AND " + TableNames.PersonAssignment + ".baseSalary >= " + this.numBoxPaymentFrom.Text;
                        }
                        else
                        {
                            s = " " + TableNames.PersonAssignment + ".baseSalary >= " + this.numBoxPaymentFrom.Text;
                            IsInclude = true;
                        }
                    }
                    if (this.numBoxPaymentTo.Text != "")
                    {

                        if (IsInclude)
                        {
                            s += " AND " + TableNames.PersonAssignment + ".baseSalary <= " + this.numBoxPaymentTo.Text;
                        }
                        else
                        {
                            s += " " + TableNames.PersonAssignment + ".baseSalary <= " + this.numBoxPaymentTo.Text;
                            IsInclude = true;
                        }
                    }
                    additional += s;
                }

                if (this.checkBoxExp.Checked)
                {
                    this.AssignmentChecked = true;
                    
					//arrColumnAdd.Add("FLOOR((DATEDIFF(day, CURRENT_TIMESTAMP,AssignedAt) + (Years * 365 + Months * 30 + Days))/365) AS years");
					//arrColumnAssignment.Add("years");					

					//arrColumnAdd.Add("case when DATEPART(DAY, CURRENT_TIMESTAMP) - DATEPART(day, HR_person.hiredat) < 1 AND DATEPART(MONTH, CURRENT_TIMESTAMP) - DATEPART(MONTH, HR_person.hiredat) - 1 < 1 then datepart(year,CURRENT_TIMESTAMP) - DATEpart(year, HR_person.hiredat) -1 when DATEPART(MONTH, CURRENT_TIMESTAMP) - DATEPART(MONTH, HR_person.hiredat) < 1 then datepart(year,CURRENT_TIMESTAMP) - DATEpart(year, HR_person.hiredat) - 1 when 1 = 1 then datepart(YEAR,CURRENT_TIMESTAMP) - DATEpart(YEAR, HR_person.hiredat) 	END AS years");
					//arrColumnAssignment.Add("years");
					arrColumnAssignment.Add("case when DATEPART(DAY, CURRENT_TIMESTAMP) - DATEPART(day, HR_person.hiredat) < 1 AND DATEPART(MONTH, CURRENT_TIMESTAMP) - DATEPART(MONTH, HR_person.hiredat) - 1 < 1 then datepart(year,CURRENT_TIMESTAMP) - DATEpart(year, HR_person.hiredat) -1 when DATEPART(MONTH, CURRENT_TIMESTAMP) - DATEPART(MONTH, HR_person.hiredat) < 1 then datepart(year,CURRENT_TIMESTAMP) - DATEpart(year, HR_person.hiredat) - 1 when 1 = 1 then datepart(YEAR,CURRENT_TIMESTAMP) - DATEpart(YEAR, HR_person.hiredat) 	END AS years");
					if (this.numBoxExpFrom.Text != "")
					{
						if (IsInclude)
						{
							s = " AND case when DATEPART(DAY, CURRENT_TIMESTAMP) - DATEPART(day, HR_person.hiredat) < 1 AND DATEPART(MONTH, CURRENT_TIMESTAMP) - DATEPART(MONTH, HR_person.hiredat) - 1 < 1 then datepart(year,CURRENT_TIMESTAMP) - DATEpart(year, HR_person.hiredat) -1 when DATEPART(MONTH, CURRENT_TIMESTAMP) - DATEPART(MONTH, HR_person.hiredat) < 1 then datepart(year,CURRENT_TIMESTAMP) - DATEpart(year, HR_person.hiredat) - 1 when 1 = 1 then datepart(YEAR,CURRENT_TIMESTAMP) - DATEpart(YEAR, HR_person.hiredat) 	END >= " + this.numBoxExpFrom.Text;
						}
						else
						{
							s = " case when DATEPART(DAY, CURRENT_TIMESTAMP) - DATEPART(day, HR_person.hiredat) < 1 AND DATEPART(MONTH, CURRENT_TIMESTAMP) - DATEPART(MONTH, HR_person.hiredat) - 1 < 1 then datepart(year,CURRENT_TIMESTAMP) - DATEpart(year, HR_person.hiredat) -1 when DATEPART(MONTH, CURRENT_TIMESTAMP) - DATEPART(MONTH, HR_person.hiredat) < 1 then datepart(year,CURRENT_TIMESTAMP) - DATEpart(year, HR_person.hiredat) - 1 when 1 = 1 then datepart(YEAR,CURRENT_TIMESTAMP) - DATEpart(YEAR, HR_person.hiredat) 	END >= " + this.numBoxExpFrom.Text;
							IsInclude = true;
						}
					}
					if (this.numBoxExpTo.Text != "")
					{

						if (IsInclude)
						{
							s += " AND case when DATEPART(DAY, CURRENT_TIMESTAMP) - DATEPART(day, HR_person.hiredat) < 1 AND DATEPART(MONTH, CURRENT_TIMESTAMP) - DATEPART(MONTH, HR_person.hiredat) - 1 < 1 then datepart(year,CURRENT_TIMESTAMP) - DATEpart(year, HR_person.hiredat) -1 when DATEPART(MONTH, CURRENT_TIMESTAMP) - DATEPART(MONTH, HR_person.hiredat) < 1 then datepart(year,CURRENT_TIMESTAMP) - DATEpart(year, HR_person.hiredat) - 1 when 1 = 1 then datepart(YEAR,CURRENT_TIMESTAMP) - DATEpart(YEAR, HR_person.hiredat) 	END <= " + this.numBoxExpTo.Text;
						}
						else
						{
							s += " case when DATEPART(DAY, CURRENT_TIMESTAMP) - DATEPART(day, HR_person.hiredat) < 1 AND DATEPART(MONTH, CURRENT_TIMESTAMP) - DATEPART(MONTH, HR_person.hiredat) - 1 < 1 then datepart(year,CURRENT_TIMESTAMP) - DATEpart(year, HR_person.hiredat) -1 when DATEPART(MONTH, CURRENT_TIMESTAMP) - DATEPART(MONTH, HR_person.hiredat) < 1 then datepart(year,CURRENT_TIMESTAMP) - DATEpart(year, HR_person.hiredat) - 1 when 1 = 1 then datepart(YEAR,CURRENT_TIMESTAMP) - DATEpart(YEAR, HR_person.hiredat) 	END <= " + this.numBoxExpTo.Text;
							IsInclude = true;
						}
					}
                    
                    additional += s;
                }
                #endregion

                if (this.checkBoxEnglish.Checked)
                {
                    this.arrColumnAssignment.Add(TableNames.PersonAssignment + ".level1eng");
                    this.arrColumnAssignment.Add(TableNames.PersonAssignment + ".level2eng");
                    this.arrColumnAssignment.Add(TableNames.PersonAssignment + ".level3eng");
                    this.arrColumnAssignment.Add(TableNames.PersonAssignment + ".level4eng");
                    this.arrColumnAssignment.Add(TableNames.PersonAssignment + ".positioneng");
                }

                if (this.checkBoxSalaryAddon.Checked)
                {
                    this.arrColumnAssignment.Add(TableNames.PersonAssignment + ".salaryaddon");
                }

                if (this.checkBoxExperience.Checked)
                {
                    this.arrColumnAssignment.Add(TableNames.PersonAssignment + ".classpercent");
                }
                //if (posId != "")
                //{
                //    if ((arrColumn.Count > 0 || arrColumnAdd.Count > 0))
                //    {
                //        additional += " AND " + posId;
                //    }
                //    else
                //    {
                //        additional += posId;
                //    }
                //}
                if ((arrColumn.Count != 0) || (arrColumnAdd.Count != 0) || (arrColumnAssignment.Count != 0))//| posId != "")
                {
                    if (this.AssignmentChecked)
                    {
                        if (this.checkedComboProfessionn.Checked)
                        {
                            if (NKPCode != null && NKPCode != "''")
                                additional += " AND " + TableNames.PersonAssignment + ".NKPCode = " + NKPCode;
                            if (EKDACode != null && EKDACode != "''")
                                additional += " AND " + TableNames.PersonAssignment + ".EKDACode = " + EKDACode + " ";
                        }
                        this.arrColumnAssignment.Add(TableNames.PersonAssignment + ".level1");
                        this.arrColumnAssignment.Add(TableNames.PersonAssignment + ".level2");
                        this.arrColumnAssignment.Add(TableNames.PersonAssignment + ".level3");
                        this.arrColumnAssignment.Add(TableNames.PersonAssignment + ".level4");
                        ActiveOnly = !this.checkBoxActiveOnly.Checked;
                        stat.FindPersonByAssignment(TableNames.PersonAssignment, arrColumn, arrValues, additional, arrColumnAdd, IsFiredd, ActiveOnly, arrInvert);
                        this.join_clause += stat.JoinClause;
                        if ((this.where_clause != "") && (stat.WhereClause != ""))
                        {
                            this.where_clause += " AND ";
                        }
                        if (stat.WhereClause != "")
                        {
                            this.where_clause += "(";
                            this.where_clause += stat.WhereClause;
                            this.where_clause += ")";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ErrorLog.WriteMessage(ex.Message);
            }
        }
        void CheckHoliday()
        {
            this.AbsenceChecked = false;
            arrColumn = new ArrayList();
            arrColumnAbsence = new ArrayList();
            ArrayList arrValues = new ArrayList();
            ArrayList arrInvert = new ArrayList();
            DataLayer.DataStatistics stat = new DataLayer.DataStatistics(this.main.connString);
            foreach (Control ctrl in this.gbAbsence.Controls)
            {
                if (ctrl is CheckedNumBox.CheckedNumBox)
                {
                    if (((CheckedNumBox.CheckedNumBox)ctrl).Checked)
                    {
                        this.AbsenceChecked = true;
                        arrValues.Add(((CheckedNumBox.CheckedNumBox)ctrl).NumBox.Text);
                        arrColumn.Add(TableNames.Prefix + ((CheckedNumBox.CheckedNumBox)ctrl).Column);
                        arrColumnAbsence.Add(((CheckedNumBox.CheckedNumBox)ctrl).Column);
                    }
                }
                if (ctrl is CheckedCombo.CheckedCombo)
                {
                    if (((CheckedCombo.CheckedCombo)ctrl).Checked)
                    {
                        this.AbsenceChecked = true;
                        if (((CheckedCombo.CheckedCombo)ctrl).IsAllChecked)
                        {
                            arrColumnAbsence.Add(TableNames.Prefix + ((CheckedCombo.CheckedCombo)ctrl).Column);
                        }
                        else
                        {
                            arrColumn.Add(TableNames.Prefix + ((CheckedCombo.CheckedCombo)ctrl).Column);
                            arrColumnAbsence.Add(TableNames.Prefix + ((CheckedCombo.CheckedCombo)ctrl).Column);
                            if (((CheckedCombo.CheckedCombo)ctrl).combobox.SelectedItem.ToString() == "")
                            {
                                arrValues.Add(((CheckedCombo.CheckedCombo)ctrl).combobox.Text);
                            }
                            else
                            {
                                arrValues.Add(((CheckedCombo.CheckedCombo)ctrl).combobox.SelectedItem.ToString());
                            }
                            if (((CheckedCombo.CheckedCombo)ctrl).IsInverted == true)
                                arrInvert.Add(true);
                            else
                            {
                                arrInvert.Add(false);
                            }
                        }
                    }
                }
            }



            string additional = "";
            string dat1 = DataAction.ConvertDateTimeToMySql(this.dateTimePickerFrom1.Value);

            string dat2 = DataAction.ConvertDateTimeToMySql(this.dateTimePickerFrom2.Value);

            if (this.checkBoxFrom.Checked)
            {
                this.AbsenceChecked = true;
                if (arrColumn.Count == 0)
                {
                    //additional = string.Format(" ( ( {2}.FromDate >= {0} AND {2}.FromDate <= {1}) OR ({2}.ToDate >= {0} AND {2}.ToDate <= {1}) OR ({2}.FromDate <= {0} AND {2}.ToDate >= {1}))", dat1, dat2, TableNames.Absence);
                    additional = " ( (";
                    additional += DataAction.DateComparison(this.dateTimePickerFrom1.Value, ComparisonOperators.eGreater, TableNames.Absence, "FromDate");
                    additional += " AND ";
                    additional += DataAction.DateComparison(this.dateTimePickerFrom2.Value, ComparisonOperators.eLess, TableNames.Absence, "FromDate");
                    additional += " ) OR ( ";
                    additional += DataAction.DateComparison(this.dateTimePickerFrom1.Value, ComparisonOperators.eGreater, TableNames.Absence, "ToDate");
                    additional += " AND ";
                    additional += DataAction.DateComparison(this.dateTimePickerFrom2.Value, ComparisonOperators.eLess, TableNames.Absence, "ToDate");
                    additional += " ) OR ( ";
                    additional += DataAction.DateComparison(this.dateTimePickerFrom1.Value, ComparisonOperators.eLess, TableNames.Absence, "FromDate");
                    additional += " AND ";
                    additional += DataAction.DateComparison(this.dateTimePickerFrom2.Value, ComparisonOperators.eGreater, TableNames.Absence, "ToDate");
                    additional += " ) ) ";
                }
                else
                {	///this is the first query, so we don't prepend AND
                    //additional = String.Format(" ( ( {2}.FromDate >= {0} AND {2}.FromDate <= {1}) OR ({2}.ToDate >= {0} AND {2}.ToDate <= {1}) OR ({2}.FromDate <= {0} AND {2}.ToDate >= {1}))", dat1, dat2, TableNames.Absence);
                    additional = " ( (";
                    additional += DataAction.DateComparison(this.dateTimePickerFrom1.Value, ComparisonOperators.eGreater, TableNames.Absence, "FromDate");
                    additional += " AND ";
                    additional += DataAction.DateComparison(this.dateTimePickerFrom2.Value, ComparisonOperators.eLess, TableNames.Absence, "FromDate");
                    additional += " ) OR ( ";
                    additional += DataAction.DateComparison(this.dateTimePickerFrom1.Value, ComparisonOperators.eGreater, TableNames.Absence, "ToDate");
                    additional += " AND ";
                    additional += DataAction.DateComparison(this.dateTimePickerFrom2.Value, ComparisonOperators.eLess, TableNames.Absence, "ToDate");
                    additional += " ) OR ( ";
                    additional += DataAction.DateComparison(this.dateTimePickerFrom1.Value, ComparisonOperators.eLess, TableNames.Absence, "FromDate");
                    additional += " AND ";
                    additional += DataAction.DateComparison(this.dateTimePickerFrom2.Value, ComparisonOperators.eGreater, TableNames.Absence, "ToDate");
                    additional += " ) ) ";
                }
            }

            if (this.checkBoxAbsenceManagement.Checked)
            {
                this.AbsenceChecked = true;
                if (additional != "")
                {
                    additional += string.Format(" AND ( {0}.OtherRequirements = '*')", TableNames.FirmPersonal3);

                }
                else
                {	///this is the first query, so we don't prepend AND
                    additional = String.Format(" ( {0}.OtherRequirements = '*')", TableNames.FirmPersonal3);
                }
                this.join_clause += string.Format(" left join {0} on {1}.positionid = {0}.id ", TableNames.FirmPersonal3, TableNames.PersonAssignment);
            }

            if (this.AbsenceChecked)
            {
                this.join_clause += string.Format(" left join {0} on {0}.parent = {1}.id ", TableNames.YearHoliday, TableNames.Person);
                this.arrColumnAbsence.Add(TableNames.Absence + ".fromdate");
                this.arrColumnAbsence.Add(TableNames.Absence + ".todate");
                this.arrColumnAbsence.Add(TableNames.Absence + ".countdays");
                this.arrColumnAbsence.Add(TableNames.YearHoliday + ".total");
                this.arrColumnAbsence.Add(TableNames.YearHoliday + ".leftover");

                stat.FindPersonByAbsence(TableNames.Absence, arrColumn, arrValues, additional, this.IsFiredd, arrInvert);
                this.join_clause += stat.JoinClause;
                if ((this.where_clause != "") && (stat.WhereClause != ""))
                {
                    this.where_clause += " AND ";
                }
                if (stat.WhereClause != "")
                {
                    this.where_clause += "(";
                    this.where_clause += stat.WhereClause + " AND " + TableNames.YearHoliday + ".year = " + main.nomenclaatureData.dtYear.Rows[0]["year"];
                    this.where_clause += ")";
                }
            }
        }

        void CheckFired()
        {
            this.FiredChecked = false;
            arrColumn = new ArrayList();
            arrColumnFired = new ArrayList();
            ArrayList arrValues = new ArrayList();
            ArrayList arrInvert = new ArrayList();
            DataLayer.DataStatistics stat = new DataLayer.DataStatistics(this.main.connString);
            foreach (Control ctrl in this.gbFired.Controls)
            {
                if (ctrl is CheckedNumBox.CheckedNumBox)
                {
                    if (((CheckedNumBox.CheckedNumBox)ctrl).Checked)
                    {
                        this.FiredChecked = true;
                        arrValues.Add(((CheckedNumBox.CheckedNumBox)ctrl).NumBox.Text);
                        arrColumn.Add(TableNames.Prefix + ((CheckedNumBox.CheckedNumBox)ctrl).Column);
                        arrColumnFired.Add(((CheckedNumBox.CheckedNumBox)ctrl).Column);
                    }
                }
                if (ctrl is CheckedCombo.CheckedCombo)
                {
                    if (((CheckedCombo.CheckedCombo)ctrl).Checked)
                    {
                        this.FiredChecked = true;
                        if (((CheckedCombo.CheckedCombo)ctrl).IsAllChecked)
                        {
                            arrColumnFired.Add(((CheckedCombo.CheckedCombo)ctrl).Column);
                        }
                        else
                        {

                            arrColumn.Add(((CheckedCombo.CheckedCombo)ctrl).Column);
                            arrColumnFired.Add(((CheckedCombo.CheckedCombo)ctrl).Column);
                            if (((CheckedCombo.CheckedCombo)ctrl).combobox.SelectedItem.ToString() == "")
                            {
                                arrValues.Add(((CheckedCombo.CheckedCombo)ctrl).combobox.Text);
                            }
                            else
                            {
                                arrValues.Add(((CheckedCombo.CheckedCombo)ctrl).combobox.SelectedItem.ToString());
                            }
                            if (((CheckedCombo.CheckedCombo)ctrl).IsInverted == true)
                                arrInvert.Add(true);
                            else
                            {
                                arrInvert.Add(false);
                            }
                        }
                    }
                }
            }
            string additional = "";
            string dat1 = DataAction.ConvertDateTimeToMySql(this.dateTimePickerFiredFromDate.Value);
            string dat2 = DataAction.ConvertDateTimeToMySql(this.dateTimePickerFiredТоDate.Value);


            if (this.checkBoxFiredFrom.Checked)
            {
                this.FiredChecked = true;
                this.arrColumnFired.Add(TableNames.Fired + ".FromDate");
                if (arrColumn.Count == 0)
                {
                    additional += " (";
                    additional += DataAction.DateComparison(this.dateTimePickerFiredFromDate.Value, ComparisonOperators.eGreater, TableNames.Fired, "FromDate");

                    additional += " AND " + DataAction.DateComparison(this.dateTimePickerFiredТоDate.Value, ComparisonOperators.eLess, TableNames.Fired, "FromDate");
                    additional += ")";

                    //additional = string.Format("  ( " + TableNames.Fired + ".FromDate BETWEEN {0} AND {1})", dat1, dat2);

                }
                else
                {
                    additional += "AND (";
                    additional += DataAction.DateComparison(this.dateTimePickerFiredFromDate.Value, ComparisonOperators.eGreater, TableNames.Fired, "FromDate");

                    additional += " AND " + DataAction.DateComparison(this.dateTimePickerFiredТоDate.Value, ComparisonOperators.eLess, TableNames.Fired, "FromDate");
                    additional += ")";
                    //additional = string.Format(" AND ( " + TableNames.Fired + ".FromDate BETWEEN {0} AND {1})", dat1, dat2);
                }
            }

            if (this.FiredChecked)
            {
                stat.FindPersonByFired(TableNames.Fired, arrColumn, arrValues, additional, this.IsFiredd, arrInvert);
                this.join_clause += stat.JoinClause;
                if ((this.where_clause != "") && (stat.WhereClause != ""))
                {
                    this.where_clause += " AND ";
                }
                if (stat.WhereClause.Trim() != "")
                {
                    this.where_clause += "(";
                    this.where_clause += stat.WhereClause;
                    this.where_clause += ")";
                }
            }
        }
        void CheckMilitaryRangs()
        {
            this.MilitaryRangsChecked = false;

            arrColumn = new ArrayList();
            arrColumnMilitaryRangs = new ArrayList();
            ArrayList arrValues = new ArrayList();
            ArrayList arrInvert = new ArrayList();
            DataLayer.DataStatistics stat = new DataLayer.DataStatistics(this.main.connString);
            foreach (Control ctrl in this.gbMilitaryRangs.Controls)
            {
                if (ctrl is CheckedNumBox.CheckedNumBox)
                {
                    if (((CheckedNumBox.CheckedNumBox)ctrl).Checked)
                    {
                        this.MilitaryRangsChecked = true;
                        arrValues.Add(((CheckedNumBox.CheckedNumBox)ctrl).NumBox.Text);
                        arrColumn.Add(TableNames.Prefix + ((CheckedNumBox.CheckedNumBox)ctrl).Column);
                        arrColumnMilitaryRangs.Add(((CheckedNumBox.CheckedNumBox)ctrl).Column);
                    }
                }

                if (ctrl is CheckedCombo.CheckedCombo)
                {
                    if (((CheckedCombo.CheckedCombo)ctrl).Checked)
                    {
                        this.MilitaryRangsChecked = true;
                        arrColumnMilitaryRangs.Add(TableNames.Prefix + ((CheckedCombo.CheckedCombo)ctrl).Column);
                        if (((CheckedCombo.CheckedCombo)ctrl).IsAllChecked)
                        {

                        }
                        else
                        {
                            arrColumn.Add(TableNames.Prefix + ((CheckedCombo.CheckedCombo)ctrl).Column);
                            if (((CheckedCombo.CheckedCombo)ctrl).combobox.SelectedItem.ToString() == "")
                            {
                                arrValues.Add(((CheckedCombo.CheckedCombo)ctrl).combobox.Text);
                            }
                            else
                            {
                                object Item = ((CheckedCombo.CheckedCombo)ctrl).combobox.SelectedItem;
                                if (Item is DataRowView)
                                {
                                    DataRowView r = (DataRowView)Item;
                                    if (r["level"].ToString() != "")
                                        arrValues.Add(r["level"]);
                                }
                                else
                                {
                                    arrValues.Add(((CheckedCombo.CheckedCombo)ctrl).combobox.SelectedItem.ToString());
                                }
                            }
                        }
                    }
                    if (((CheckedCombo.CheckedCombo)ctrl).IsInverted == true)
                        arrInvert.Add(true);
                    else
                    {
                        arrInvert.Add(false);
                    }
                }
            }
            string additional = "";
            string dat1 = DataAction.ConvertDateTimeToMySql(dateTimePickerMilitaryRangFrom.Value);
            string dat2 = DataAction.ConvertDateTimeToMySql(dateTimePickerMilitaryRangTo.Value);
            if (this.checkBoxMilitaryRangFrom.Checked)
            {
                this.MilitaryRangsChecked = true;
                if (arrValues.Count == 0)
                {
                    additional = string.Format("( {2}.RangOrderValidFrom BETWEEN {0} AND {1}) ", dat1, dat2, TableNames.MilitaryRang);
                }
                else
                {	//if we have columns - > probably even better values - we will prepend AND to make it connect
                    additional = string.Format("AND ( {2}.RangOrderValidFrom BETWEEN {0} AND {1}) ", dat1, dat2, TableNames.MilitaryRang);
                }
            }
            else
            {
                arrColumnMilitaryRangs.Add(TableNames.MilitaryRang + ".RangOrderValidFrom");
            }

            string dat3 = DataAction.ConvertDateTimeToMySql(dateTimePickerMilitaryRangOrderFrom.Value);
            string dat4 = DataAction.ConvertDateTimeToMySql(dateTimePickerMilitaryRangOrderTo.Value);

            if (this.checkBoxMilitaryRangOrderFrom.Checked)
            {
                this.MilitaryRangsChecked = true;
                if (arrValues.Count == 0)
                {
                    additional = string.Format("( {2}.RangOrderDate BETWEEN {0} AND {1}) ", dat3, dat4, TableNames.MilitaryRang);
                }
                else
                {	//if we have columns - > probably even better values - we will prepend AND to make it connect
                    additional = string.Format("AND ( {2}.RangOrderDate BETWEEN {0} AND {1}) ", dat3, dat4, TableNames.MilitaryRang);
                }
            }
            else
            {
                arrColumnMilitaryRangs.Add(TableNames.MilitaryRang + ".RangOrderDate");
            }

            if (this.checkBoxRangNumberOrder.Checked)
            {
                if (where_clause.Trim() != "")
                {
                    where_clause += " AND ";
                }
                this.where_clause += string.Format(" ( {0}.rangordernumber  like '{1}' ) ", TableNames.MilitaryRang, this.textBoxRangNumberOrder.Text);
                this.arrColumnMilitaryRangs.Add(TableNames.MilitaryRang + ".rangordernumber");
                this.MilitaryRangsChecked = true;
            }

            if (this.MilitaryRangsChecked)
            {
                stat.FindPersonByMilitaryRang(TableNames.MilitaryRang, arrColumn, arrValues, additional, this.IsFiredd, arrInvert, this.checkedComboMilitaryRang.Checked);
                this.join_clause += stat.JoinClause;
                if ((this.where_clause != "") && (stat.WhereClause.Trim() != ""))
                {
                    this.where_clause += " AND ";
                }
                if (stat.WhereClause.Trim() != "")
                {
                    this.where_clause += "(";
                    this.where_clause += stat.WhereClause;
                    this.where_clause += ")";
                }
            }
        }
        void CheckAtestation()
        {
            this.AtestationChecked = false;
            arrColumn = new ArrayList();
            arrColumnAtestation = new ArrayList();
            ArrayList arrValues = new ArrayList();
            DataLayer.DataStatistics stat = new DataLayer.DataStatistics(this.main.connString);

            string additional = "";
            additional = string.Format("  WHERE ( " + TableNames.Attestations + ".Year = {0} ) ", numericUpDownAtestationYears.Value.ToString());

            if (checkBoxAtestationRating.Checked)
            {
                this.AtestationChecked = true;
                additional += string.Format(" AND ( " + TableNames.Attestations + ".TotalMark = {0} ) ", numericUpDownAtestationGrade.Value.ToString());
                arrColumnAtestation.Add(TableNames.Attestations + ".TotalMark");
            }
            if (checkBoxAtestationPersonalRaise.Checked)
            {
                this.AtestationChecked = true;
                additional += " AND " + "( " + TableNames.Attestations + ".forRangUpdate = 'да' ) ";
                arrColumnAtestation.Add(TableNames.Attestations + ".forRangUpdate");
            }
            if (checkBoxAtestationEtaps.Checked)
            {
                this.AtestationChecked = true;
                additional += " AND " + "( " + TableNames.Attestations + ".hasWorkplan = 'да' ) ";
                additional += " AND " + "( " + TableNames.Attestations + ".hasMiddleMeeting = 'да' ) ";
                additional += " AND " + "( " + TableNames.Attestations + ".hasFinalMeeting = 'да' ) ";
            }
            if (checkBoxAtestationCountRaised.Checked)
            {
                this.AtestationChecked = true;
            }
            if (this.AtestationChecked)
            {
                arrColumnAtestation.Add(TableNames.Attestations + ".Year");
                //this.dtAtestation = stat.FindPersonByAtestation(TableNames.Attestations, arrColumn, arrValues, additional, this.IsFiredd, checkBoxAtestationCountRaised.Checked, (int)numericUpDownAtestationYears.Value);
                stat.FindPersonByAtestation(TableNames.Attestations, arrColumn, arrValues, additional, this.IsFiredd, checkBoxAtestationCountRaised.Checked, (int)numericUpDownAtestationYears.Value);
            }

        }

        private void StatisticAdministration_Load()
        {
            try
            {
                this.checkedContractType.combobox.DataSource = this.main.nomenclaatureData.arrLaw;
                this.checkedComboWorkTime.combobox.DataSource = this.main.nomenclaatureData.dtWorkTime;
                this.checkedComboWorkTime.combobox.DisplayMember = "level";
                this.checkedComboContract.combobox.DataSource = this.main.nomenclaatureData.arrContract;

                this.da = new DataAction(this.main.connString);
                this.checkedComboProfessionn.combobox.DataSource = this.da.SelectWhere(TableNames.GlobalPositions, "positionName", "");
                this.checkedComboReasonAssignment.combobox.DataSource = this.da.SelectWhere(TableNames.ReasonAssignment, "level", "");
                this.dtPosition = this.da.SelectWhere(TableNames.FirmPersonal3, "*", " ORDER BY id");
                if (this.checkedComboProfessionn.combobox.DataSource == null || this.checkedComboReasonAssignment.combobox.DataSource == null || this.dtPosition == null)
                {
                    MessageBox.Show("Грешка при зареждане на данни за длъжности", ErrorMessages.NoConnection);
                    this.Close();
                }

                this.checkedComboProfessionn.combobox.DisplayMember = "positionName";
                this.checkedComboReasonAssignment.combobox.DisplayMember = "level";
                //    
                this.checkedComboAdministration.Text = "Служител в администрация";
                this.checkedComboDirection.Text = "Служител в дирекция";
                this.checkedComboDepartment.Text = "Служител в отдел";
                this.checkedComboSector.Text = "Служител в сектор";

                //ot Person Info
                this.checkedComboDirection.combobox.Items.Add("");
                foreach (Nodes node in this.main.nomenclaatureData.arrDirection)
                {
                    this.checkedComboDirection.combobox.Items.Add(node.NodeName);
                }
                this.checkedComboDepartment.combobox.Items.Add("");
                foreach (Nodes node in this.main.nomenclaatureData.arrControl)
                {
                    this.checkedComboDepartment.combobox.Items.Add(node.NodeName);
                }
                this.checkedComboSector.combobox.Items.Add("");
                foreach (Nodes node in this.main.nomenclaatureData.arrTeam)
                {
                    this.checkedComboSector.combobox.Items.Add(node.NodeName);
                }

                this.dtTree = main.nomenclaatureData.dtTreeTable;
                this.TreeLoad();
                ////////

                this.numBoxPaymentFrom.Enabled = false;
                this.numBoxPaymentTo.Enabled = false;

                this.checkedComboAdministration.combobox.SelectedIndexChanged += new EventHandler(combobox4_SelectedIndexChanged);
                this.checkedComboDirection.combobox.SelectedIndexChanged += new EventHandler(combobox1_SelectedIndexChanged);
                this.checkedComboDepartment.combobox.SelectedIndexChanged += new EventHandler(combobox2_SelectedIndexChanged);
                this.checkedComboSector.combobox.SelectedIndexChanged += new EventHandler(combobox3_SelectedIndexChanged);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

            try
            {
                string[] str = new string[] { "Болнични", "Полагаем годишен отпуск", "Неплатен отпуск", "Платен отпуск", "Отглеждане на дете", "Командировка", "Полагаем отпуск минали години", "Обучение", "Полагаем отпуск ТЕЛК", "Полагаем отпуск обучение", "Полагаем отпуск друг" };
                foreach (string s in str)
                {
                    this.checkedComboTutorAbsenceReason.combobox.Items.Add(s);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }
        private void StatisticHoliday_Load()
        {
            try
            {
                string[] str = new string[] { "Болнични", "Полагаем годишен отпуск", "Неплатен отпуск", "Платен отпуск", "Отглеждане на дете", "Командировка", "Полагаем отпуск минали години", "Обучение", "Полагаем отпуск ТЕЛК", "Полагаем отпуск обучение", "Полагаем отпуск друг" };
                foreach (string s in str)
                {
                    this.checkedComboTypeAbsence.combobox.Items.Add(s);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }


        }
        private void StatisticPenalty_Load()
        {
            try
            {
                this.checkedComboReason.combobox.DataSource = this.main.nomenclaatureData.arrPenaltyReason;
                this.checkedComboTypeReason.combobox.DataSource = this.main.nomenclaatureData.arrTypePenalty;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private void StatisticPersonal_Load()
        {
            try
            {
                #region LoadNomenklatures
                this.checkedComboEducation.combobox.DataSource = this.main.nomenclaatureData.dtEducation;
                this.checkedComboEducation.combobox.DisplayMember = "level";
                this.checkedComboEKDA.combobox.DataSource = this.main.nomenclaatureData.arrEKDAType;
                this.checkedComboFamilyStatus.combobox.DataSource = this.main.nomenclaatureData.arrFamilyStatus;
                this.checkedComboLanguage.combobox.DataSource = this.main.nomenclaatureData.arrLanguages;
                this.checkedComboMilitaryStatus.combobox.DataSource = this.main.nomenclaatureData.dtMilitaryRang;
                this.checkedComboMilitaryStatus.combobox.DisplayMember = "level";
                this.checkedComboSex.combobox.DataSource = this.main.nomenclaatureData.arrSex;
                this.checkedComboScienceLevel.combobox.DataSource = this.main.nomenclaatureData.arrScienceTitle;
                #endregion
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private void StatisticFired_Load()
        {
            try
            {
                if (this.IsFiredd)
                {
                    this.checkBoxFiredFrom.Checked = false;
                    dateTimePickerFiredFromDate.Enabled = this.checkBoxFiredFrom.Checked;
                    this.checkedComboxFiredReason.combobox.DataSource = this.main.nomenclaatureData.arrReasonFired;
                    this.dateTimePickerFiredFromDate.Value = DateTime.Now;
                }
                else
                {
                    this.gbFired.Enabled = false;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private void StatisticAtestation_Load()
        {
            try
            {
                if (this.main.IsAtestaciiActive)
                {
                    checkBoxAtestationRating.Checked = false;
                    checkBoxAtestationCountRaised.Checked = false;
                    checkBoxAtestationPersonalRaise.Checked = false;
                    checkBoxAtestationEtaps.Checked = false;
                    numericUpDownAtestationYears.Value = DateTime.Now.Year;

                }
                else
                {
                    this.groupBoxAtestacii.Enabled = false;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private void StatisticMilitaryRangs_Load()
        {
            try
            {
                this.checkedComboMilitaryRang.combobox.DataSource = this.main.nomenclaatureData.dtMilitaryRang;
                this.checkedComboMilitaryRang.combobox.DisplayMember = "level";
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private void TreeLoad()
        {
            try
            {
                dvrs = DataViewRowState.CurrentRows;
                vueAdministration = new DataView(dtTree, "par = 0", "level", dvrs);

                this.arrDirection = new ArrayList();
                this.arrDirection.Add("");

                for (int i = 0; i < vueAdministration.Count; i++)
                {
                    arrDirection.Add(vueAdministration[i]["level"]);
                }
                this.checkedComboAdministration.combobox.DataSource = arrDirection;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
        public bool IsIdInDataTable(string ID, DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                if (ID == row[0].ToString())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
        //public DataTable MinEnabledTable()
        //{
        //    int min = 9999999;
        //    int table = 0;
        //    if( this.dtPersonal.Rows.Count > 0 )
        //    {
        //        min = this.dtPersonal.Rows.Count;
        //        table = 1;
        //    }
        //    if( this.dtAssignment.Rows.Count > 0 )
        //    {
        //        if( min > this.dtAssignment.Rows.Count )
        //        {
        //            min = this.dtAssignment.Rows.Count;
        //            table = 2;
        //        }
        //    }
        //    if( this.dtAbsence.Rows.Count > 0 )
        //    {
        //        if( min > this.dtAbsence.Rows.Count )
        //        {
        //            min = this.dtAbsence.Rows.Count;
        //            table = 3;
        //        }
        //    }
        //    if( this.dtPenalty.Rows.Count > 0  )
        //    {
        //        if( min > this.dtPenalty.Rows.Count )
        //        {
        //            min = this.dtPenalty.Rows.Count;
        //            table = 4;
        //        }
        //    }
        //    if( this.dtFired.Rows.Count > 0  )
        //    {
        //        if( min > this.dtFired.Rows.Count )
        //        {
        //            min = this.dtFired.Rows.Count;
        //            table = 5;
        //        }
        //    }
        //    if( this.dtAtestation.Rows.Count > 0  )
        //    {
        //        if( min > this.dtAtestation.Rows.Count )
        //        {
        //            min = this.dtAtestation.Rows.Count;
        //            table = 6;
        //        }
        //    }
        //    switch( table )
        //    {					
        //        case 1: 
        //        {
        //            return this.dtPersonal;
        //        }
        //        case 2:
        //        {
        //            return this.dtAssignment;
        //        }					
        //        case 3: 
        //        {
        //            return this.dtAbsence;
        //        }					
        //        case 4: 
        //        {
        //            return this.dtPenalty;				
        //        }
        //        case 5: 
        //        {
        //            return this.dtFired;				
        //        }
        //        case 6:
        //        {
        //            return this.dtAtestation;
        //        }
        //    }
        //    DataTable dt = new DataTable();
        //    return dt;
        //}

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formStatisticTotal));
            this.buttonFind = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.checkBoxExportToExcel = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TabPersonalInfo = new System.Windows.Forms.TabPage();
            this.gbPersonal = new System.Windows.Forms.GroupBox();
            this.checkBoxIDCardExpiry = new System.Windows.Forms.CheckBox();
            this.dateTimePickerIDCardExpiresTo = new System.Windows.Forms.DateTimePicker();
            this.textBoxFamily = new System.Windows.Forms.TextBox();
            this.checkBoxFamily = new System.Windows.Forms.CheckBox();
            this.textBoxSurName = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.checkBoxSurName = new System.Windows.Forms.CheckBox();
            this.checkBoxName = new System.Windows.Forms.CheckBox();
            this.textBoxLivingPlace = new System.Windows.Forms.TextBox();
            this.textBoxBirthPlace = new System.Windows.Forms.TextBox();
            this.checkBoxLivingPlace = new System.Windows.Forms.CheckBox();
            this.checkBoxBirthPlace = new System.Windows.Forms.CheckBox();
            this.checkBoxBirthYear = new System.Windows.Forms.CheckBox();
            this.numBoxBirthYear = new BugBox.NumBox();
            this.checkBoxBirthMonth = new System.Windows.Forms.CheckBox();
            this.numBoxBirthMonth = new BugBox.NumBox();
            this.checkBoxBirthday = new System.Windows.Forms.CheckBox();
            this.numBoxBirthDay = new BugBox.NumBox();
            this.checkedComboSex = new CheckedCombo.CheckedCombo();
            this.checkBoxEnglish = new System.Windows.Forms.CheckBox();
            this.checkedComboScienceLevel = new CheckedCombo.CheckedCombo();
            this.checkBoxNLK = new System.Windows.Forms.CheckBox();
            this.checkedComboCountry = new CheckedCombo.CheckedCombo();
            this.checkBoxAdress = new System.Windows.Forms.CheckBox();
            this.checkedComboFamilyStatus = new CheckedCombo.CheckedCombo();
            this.checkedComboEducation = new CheckedCombo.CheckedCombo();
            this.checkedComboLanguage = new CheckedCombo.CheckedCombo();
            this.checkedComboMilitaryStatus = new CheckedCombo.CheckedCombo();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labelYounger = new System.Windows.Forms.Label();
            this.checkBoxAge = new System.Windows.Forms.CheckBox();
            this.numBoxYounger = new BugBox.NumBox();
            this.numBoxOlder = new BugBox.NumBox();
            this.TabPageAssignment = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numBoxExpFrom = new BugBox.NumBox();
            this.numBoxExpTo = new BugBox.NumBox();
            this.checkBoxExp = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.checkBoxPayment = new System.Windows.Forms.CheckBox();
            this.numBoxPaymentFrom = new BugBox.NumBox();
            this.numBoxPaymentTo = new BugBox.NumBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkBoxTestContractExpiraty = new System.Windows.Forms.CheckBox();
            this.dateTimePickerTestContractExpiry2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerTestContractExpiry1 = new System.Windows.Forms.DateTimePicker();
            this.checkBoxAssignedAt = new System.Windows.Forms.CheckBox();
            this.checkBoxContractExpiry = new System.Windows.Forms.CheckBox();
            this.dateTimePickerContractExpiry2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerContractExpiry1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerAssignedAt2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerAssignedAt1 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.gbAssignment = new System.Windows.Forms.GroupBox();
            this.checkBoxSalaryAddon = new System.Windows.Forms.CheckBox();
            this.buttonSelectPosition = new System.Windows.Forms.Button();
            this.checkedComboTutorAbsenceReason = new CheckedCombo.CheckedCombo();
            this.checkedComboReasonAssignment = new CheckedCombo.CheckedCombo();
            this.checkedContractType = new CheckedCombo.CheckedCombo();
            this.checkedComboWorkTime = new CheckedCombo.CheckedCombo();
            this.checkBoxActiveOnly = new System.Windows.Forms.CheckBox();
            this.checkedComboContract = new CheckedCombo.CheckedCombo();
            this.checkedComboAdministration = new CheckedCombo.CheckedCombo();
            this.checkedComboProfessionn = new CheckedCombo.CheckedCombo();
            this.checkedComboEKDA = new CheckedCombo.CheckedCombo();
            this.checkedComboSector = new CheckedCombo.CheckedCombo();
            this.checkedComboDirection = new CheckedCombo.CheckedCombo();
            this.checkedComboDepartment = new CheckedCombo.CheckedCombo();
            this.tabPageAbsence = new System.Windows.Forms.TabPage();
            this.gbAbsence = new System.Windows.Forms.GroupBox();
            this.checkBoxAbsenceManagement = new System.Windows.Forms.CheckBox();
            this.checkedComboTypeAbsence = new CheckedCombo.CheckedCombo();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.checkBoxFrom = new System.Windows.Forms.CheckBox();
            this.dateTimePickerFrom2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFrom1 = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.tabPagePenalty = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxFormDate = new System.Windows.Forms.CheckBox();
            this.checkBoxPenaltyDate = new System.Windows.Forms.CheckBox();
            this.dateTimePickerFormDate1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFormDate2 = new System.Windows.Forms.DateTimePicker();
            this.label17 = new System.Windows.Forms.Label();
            this.dateTimePickerPenaltyDate2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerPenaltyDate1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.gbPenalty = new System.Windows.Forms.GroupBox();
            this.checkedComboTypeReason = new CheckedCombo.CheckedCombo();
            this.checkedComboReason = new CheckedCombo.CheckedCombo();
            this.tabPageFired = new System.Windows.Forms.TabPage();
            this.labelFiredMessage = new System.Windows.Forms.Label();
            this.gbFired = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.dateTimePickerFiredТоDate = new System.Windows.Forms.DateTimePicker();
            this.checkBoxFiredFrom = new System.Windows.Forms.CheckBox();
            this.checkedComboxFiredReason = new CheckedCombo.CheckedCombo();
            this.dateTimePickerFiredFromDate = new System.Windows.Forms.DateTimePicker();
            this.tabPageAtestacii = new System.Windows.Forms.TabPage();
            this.groupBoxAtestacii = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.checkBoxAtestationPersonalRaise = new System.Windows.Forms.CheckBox();
            this.checkBoxAtestationCountRaised = new System.Windows.Forms.CheckBox();
            this.checkBoxAtestationRating = new System.Windows.Forms.CheckBox();
            this.numericUpDownAtestationGrade = new System.Windows.Forms.NumericUpDown();
            this.checkBoxAtestationEtaps = new System.Windows.Forms.CheckBox();
            this.numericUpDownAtestationYears = new System.Windows.Forms.NumericUpDown();
            this.tabPageRangs = new System.Windows.Forms.TabPage();
            this.gbMilitaryRangs = new System.Windows.Forms.GroupBox();
            this.textBoxRangNumberOrder = new System.Windows.Forms.TextBox();
            this.checkBoxRangNumberOrder = new System.Windows.Forms.CheckBox();
            this.checkedComboMilitaryRang = new CheckedCombo.CheckedCombo();
            this.groupBoxRangHistoty = new System.Windows.Forms.GroupBox();
            this.checkBoxMilitaryRangFrom = new System.Windows.Forms.CheckBox();
            this.checkBoxMilitaryRangOrderFrom = new System.Windows.Forms.CheckBox();
            this.dateTimePickerMilitaryRangOrderTo = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerMilitaryRangOrderFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerMilitaryRangTo = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerMilitaryRangFrom = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.checkBoxExperience = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.TabPersonalInfo.SuspendLayout();
            this.gbPersonal.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.TabPageAssignment.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.gbAssignment.SuspendLayout();
            this.tabPageAbsence.SuspendLayout();
            this.gbAbsence.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tabPagePenalty.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbPenalty.SuspendLayout();
            this.tabPageFired.SuspendLayout();
            this.gbFired.SuspendLayout();
            this.tabPageAtestacii.SuspendLayout();
            this.groupBoxAtestacii.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtestationGrade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtestationYears)).BeginInit();
            this.tabPageRangs.SuspendLayout();
            this.gbMilitaryRangs.SuspendLayout();
            this.groupBoxRangHistoty.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonFind
            // 
            this.buttonFind.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonFind.Image = ((System.Drawing.Image)(resources.GetObject("buttonFind.Image")));
            this.buttonFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonFind.Location = new System.Drawing.Point(458, 670);
            this.buttonFind.Name = "buttonFind";
            this.buttonFind.Size = new System.Drawing.Size(130, 25);
            this.buttonFind.TabIndex = 12;
            this.buttonFind.Text = "Намери";
            this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonExit.Image = ((System.Drawing.Image)(resources.GetObject("buttonExit.Image")));
            this.buttonExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExit.Location = new System.Drawing.Point(598, 671);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(130, 24);
            this.buttonExit.TabIndex = 17;
            this.buttonExit.Text = "Изход";
            this.buttonExit.Visible = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // checkBoxExportToExcel
            // 
            this.checkBoxExportToExcel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.checkBoxExportToExcel.Location = new System.Drawing.Point(256, 670);
            this.checkBoxExportToExcel.Name = "checkBoxExportToExcel";
            this.checkBoxExportToExcel.Size = new System.Drawing.Size(192, 25);
            this.checkBoxExportToExcel.TabIndex = 18;
            this.checkBoxExportToExcel.Text = "Прехвърли резултата в ексел";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.TabPersonalInfo);
            this.tabControl1.Controls.Add(this.TabPageAssignment);
            this.tabControl1.Controls.Add(this.tabPageAbsence);
            this.tabControl1.Controls.Add(this.tabPagePenalty);
            this.tabControl1.Controls.Add(this.tabPageFired);
            this.tabControl1.Controls.Add(this.tabPageAtestacii);
            this.tabControl1.Controls.Add(this.tabPageRangs);
            this.tabControl1.Location = new System.Drawing.Point(0, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(988, 656);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // TabPersonalInfo
            // 
            this.TabPersonalInfo.Controls.Add(this.gbPersonal);
            this.TabPersonalInfo.Controls.Add(this.groupBox3);
            this.TabPersonalInfo.Location = new System.Drawing.Point(4, 22);
            this.TabPersonalInfo.Name = "TabPersonalInfo";
            this.TabPersonalInfo.Size = new System.Drawing.Size(980, 630);
            this.TabPersonalInfo.TabIndex = 0;
            this.TabPersonalInfo.Text = "Лични данни";
            this.TabPersonalInfo.UseVisualStyleBackColor = true;
            // 
            // gbPersonal
            // 
            this.gbPersonal.Controls.Add(this.checkBoxIDCardExpiry);
            this.gbPersonal.Controls.Add(this.dateTimePickerIDCardExpiresTo);
            this.gbPersonal.Controls.Add(this.textBoxFamily);
            this.gbPersonal.Controls.Add(this.checkBoxFamily);
            this.gbPersonal.Controls.Add(this.textBoxSurName);
            this.gbPersonal.Controls.Add(this.textBoxName);
            this.gbPersonal.Controls.Add(this.checkBoxSurName);
            this.gbPersonal.Controls.Add(this.checkBoxName);
            this.gbPersonal.Controls.Add(this.textBoxLivingPlace);
            this.gbPersonal.Controls.Add(this.textBoxBirthPlace);
            this.gbPersonal.Controls.Add(this.checkBoxLivingPlace);
            this.gbPersonal.Controls.Add(this.checkBoxBirthPlace);
            this.gbPersonal.Controls.Add(this.checkBoxBirthYear);
            this.gbPersonal.Controls.Add(this.numBoxBirthYear);
            this.gbPersonal.Controls.Add(this.checkBoxBirthMonth);
            this.gbPersonal.Controls.Add(this.numBoxBirthMonth);
            this.gbPersonal.Controls.Add(this.checkBoxBirthday);
            this.gbPersonal.Controls.Add(this.numBoxBirthDay);
            this.gbPersonal.Controls.Add(this.checkedComboSex);
            this.gbPersonal.Controls.Add(this.checkBoxEnglish);
            this.gbPersonal.Controls.Add(this.checkedComboScienceLevel);
            this.gbPersonal.Controls.Add(this.checkBoxNLK);
            this.gbPersonal.Controls.Add(this.checkedComboCountry);
            this.gbPersonal.Controls.Add(this.checkBoxAdress);
            this.gbPersonal.Controls.Add(this.checkedComboFamilyStatus);
            this.gbPersonal.Controls.Add(this.checkedComboEducation);
            this.gbPersonal.Controls.Add(this.checkedComboLanguage);
            this.gbPersonal.Controls.Add(this.checkedComboMilitaryStatus);
            this.gbPersonal.Location = new System.Drawing.Point(8, 8);
            this.gbPersonal.Name = "gbPersonal";
            this.gbPersonal.Size = new System.Drawing.Size(968, 513);
            this.gbPersonal.TabIndex = 0;
            this.gbPersonal.TabStop = false;
            this.gbPersonal.Text = "Избор на условия по лични данни";
            // 
            // checkBoxIDCardExpiry
            // 
            this.checkBoxIDCardExpiry.Location = new System.Drawing.Point(471, 19);
            this.checkBoxIDCardExpiry.Name = "checkBoxIDCardExpiry";
            this.checkBoxIDCardExpiry.Size = new System.Drawing.Size(290, 23);
            this.checkBoxIDCardExpiry.TabIndex = 30;
            this.checkBoxIDCardExpiry.Text = "Личната карта на служителя изтича до";
            this.checkBoxIDCardExpiry.CheckedChanged += new System.EventHandler(this.checkBoxIDCardExpiry_CheckedChanged);
            // 
            // dateTimePickerIDCardExpiresTo
            // 
            this.dateTimePickerIDCardExpiresTo.Enabled = false;
            this.dateTimePickerIDCardExpiresTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerIDCardExpiresTo.Location = new System.Drawing.Point(780, 22);
            this.dateTimePickerIDCardExpiresTo.Name = "dateTimePickerIDCardExpiresTo";
            this.dateTimePickerIDCardExpiresTo.Size = new System.Drawing.Size(180, 20);
            this.dateTimePickerIDCardExpiresTo.TabIndex = 32;
            // 
            // textBoxFamily
            // 
            this.textBoxFamily.Enabled = false;
            this.textBoxFamily.Location = new System.Drawing.Point(209, 455);
            this.textBoxFamily.Name = "textBoxFamily";
            this.textBoxFamily.Size = new System.Drawing.Size(100, 20);
            this.textBoxFamily.TabIndex = 29;
            // 
            // checkBoxFamily
            // 
            this.checkBoxFamily.Location = new System.Drawing.Point(8, 455);
            this.checkBoxFamily.Name = "checkBoxFamily";
            this.checkBoxFamily.Size = new System.Drawing.Size(200, 16);
            this.checkBoxFamily.TabIndex = 28;
            this.checkBoxFamily.Text = "Фамилия :";
            this.checkBoxFamily.CheckedChanged += new System.EventHandler(this.checkBoxFamily_CheckedChanged);
            // 
            // textBoxSurName
            // 
            this.textBoxSurName.Enabled = false;
            this.textBoxSurName.Location = new System.Drawing.Point(209, 429);
            this.textBoxSurName.Name = "textBoxSurName";
            this.textBoxSurName.Size = new System.Drawing.Size(100, 20);
            this.textBoxSurName.TabIndex = 27;
            // 
            // textBoxName
            // 
            this.textBoxName.Enabled = false;
            this.textBoxName.Location = new System.Drawing.Point(209, 403);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(100, 20);
            this.textBoxName.TabIndex = 26;
            // 
            // checkBoxSurName
            // 
            this.checkBoxSurName.Location = new System.Drawing.Point(8, 429);
            this.checkBoxSurName.Name = "checkBoxSurName";
            this.checkBoxSurName.Size = new System.Drawing.Size(200, 16);
            this.checkBoxSurName.TabIndex = 25;
            this.checkBoxSurName.Text = "Презиме :";
            this.checkBoxSurName.CheckedChanged += new System.EventHandler(this.checkBoxSurName_CheckedChanged);
            // 
            // checkBoxName
            // 
            this.checkBoxName.Location = new System.Drawing.Point(8, 403);
            this.checkBoxName.Name = "checkBoxName";
            this.checkBoxName.Size = new System.Drawing.Size(200, 16);
            this.checkBoxName.TabIndex = 24;
            this.checkBoxName.Text = "Име :";
            this.checkBoxName.CheckedChanged += new System.EventHandler(this.checkBoxName_CheckedChanged);
            // 
            // textBoxLivingPlace
            // 
            this.textBoxLivingPlace.Enabled = false;
            this.textBoxLivingPlace.Location = new System.Drawing.Point(209, 377);
            this.textBoxLivingPlace.Name = "textBoxLivingPlace";
            this.textBoxLivingPlace.Size = new System.Drawing.Size(100, 20);
            this.textBoxLivingPlace.TabIndex = 23;
            // 
            // textBoxBirthPlace
            // 
            this.textBoxBirthPlace.Enabled = false;
            this.textBoxBirthPlace.Location = new System.Drawing.Point(209, 351);
            this.textBoxBirthPlace.Name = "textBoxBirthPlace";
            this.textBoxBirthPlace.Size = new System.Drawing.Size(100, 20);
            this.textBoxBirthPlace.TabIndex = 22;
            // 
            // checkBoxLivingPlace
            // 
            this.checkBoxLivingPlace.Location = new System.Drawing.Point(8, 377);
            this.checkBoxLivingPlace.Name = "checkBoxLivingPlace";
            this.checkBoxLivingPlace.Size = new System.Drawing.Size(200, 16);
            this.checkBoxLivingPlace.TabIndex = 21;
            this.checkBoxLivingPlace.Text = "Местоживеене :";
            this.checkBoxLivingPlace.CheckedChanged += new System.EventHandler(this.checkBoxLivingPlace_CheckedChanged);
            // 
            // checkBoxBirthPlace
            // 
            this.checkBoxBirthPlace.Location = new System.Drawing.Point(8, 351);
            this.checkBoxBirthPlace.Name = "checkBoxBirthPlace";
            this.checkBoxBirthPlace.Size = new System.Drawing.Size(200, 16);
            this.checkBoxBirthPlace.TabIndex = 19;
            this.checkBoxBirthPlace.Text = "Месторождение :";
            this.checkBoxBirthPlace.CheckedChanged += new System.EventHandler(this.checkBoxBirthPlace_CheckedChanged);
            // 
            // checkBoxBirthYear
            // 
            this.checkBoxBirthYear.Location = new System.Drawing.Point(8, 325);
            this.checkBoxBirthYear.Name = "checkBoxBirthYear";
            this.checkBoxBirthYear.Size = new System.Drawing.Size(200, 16);
            this.checkBoxBirthYear.TabIndex = 17;
            this.checkBoxBirthYear.Text = "Рождена дата - година:";
            this.checkBoxBirthYear.CheckedChanged += new System.EventHandler(this.checkBoxBirthYear_CheckedChanged);
            // 
            // numBoxBirthYear
            // 
            this.numBoxBirthYear.Enabled = false;
            this.numBoxBirthYear.Location = new System.Drawing.Point(209, 325);
            this.numBoxBirthYear.Name = "numBoxBirthYear";
            this.numBoxBirthYear.Size = new System.Drawing.Size(72, 20);
            this.numBoxBirthYear.TabIndex = 18;
            // 
            // checkBoxBirthMonth
            // 
            this.checkBoxBirthMonth.Location = new System.Drawing.Point(8, 299);
            this.checkBoxBirthMonth.Name = "checkBoxBirthMonth";
            this.checkBoxBirthMonth.Size = new System.Drawing.Size(200, 16);
            this.checkBoxBirthMonth.TabIndex = 15;
            this.checkBoxBirthMonth.Text = "Рождена дата - месец:";
            this.checkBoxBirthMonth.CheckedChanged += new System.EventHandler(this.checkBoxBirthMonth_CheckedChanged);
            // 
            // numBoxBirthMonth
            // 
            this.numBoxBirthMonth.Enabled = false;
            this.numBoxBirthMonth.Location = new System.Drawing.Point(209, 299);
            this.numBoxBirthMonth.Name = "numBoxBirthMonth";
            this.numBoxBirthMonth.Size = new System.Drawing.Size(72, 20);
            this.numBoxBirthMonth.TabIndex = 16;
            // 
            // checkBoxBirthday
            // 
            this.checkBoxBirthday.Location = new System.Drawing.Point(8, 273);
            this.checkBoxBirthday.Name = "checkBoxBirthday";
            this.checkBoxBirthday.Size = new System.Drawing.Size(200, 16);
            this.checkBoxBirthday.TabIndex = 13;
            this.checkBoxBirthday.Text = "Рождена дата - ден:";
            this.checkBoxBirthday.CheckedChanged += new System.EventHandler(this.checkBoxBirthday_CheckedChanged);
            // 
            // numBoxBirthDay
            // 
            this.numBoxBirthDay.Enabled = false;
            this.numBoxBirthDay.Location = new System.Drawing.Point(209, 273);
            this.numBoxBirthDay.Name = "numBoxBirthDay";
            this.numBoxBirthDay.Size = new System.Drawing.Size(72, 20);
            this.numBoxBirthDay.TabIndex = 14;
            // 
            // checkedComboSex
            // 
            this.checkedComboSex.Checked = false;
            this.checkedComboSex.Column = "person.Sex";
            this.checkedComboSex.Data = null;
            this.checkedComboSex.DropDownWidth = 160;
            this.checkedComboSex.Location = new System.Drawing.Point(8, 169);
            this.checkedComboSex.Name = "checkedComboSex";
            this.checkedComboSex.SelectedIndex = -1;
            this.checkedComboSex.Size = new System.Drawing.Size(450, 23);
            this.checkedComboSex.TabIndex = 7;
            this.checkedComboSex.TextCombo = "Пол";
            // 
            // checkBoxEnglish
            // 
            this.checkBoxEnglish.Location = new System.Drawing.Point(8, 194);
            this.checkBoxEnglish.Name = "checkBoxEnglish";
            this.checkBoxEnglish.Size = new System.Drawing.Size(450, 23);
            this.checkBoxEnglish.TabIndex = 12;
            this.checkBoxEnglish.Text = "Колони на Английски";
            // 
            // checkedComboScienceLevel
            // 
            this.checkedComboScienceLevel.Checked = false;
            this.checkedComboScienceLevel.Column = "person.ScienceTitle";
            this.checkedComboScienceLevel.Data = null;
            this.checkedComboScienceLevel.DropDownWidth = 160;
            this.checkedComboScienceLevel.Location = new System.Drawing.Point(8, 144);
            this.checkedComboScienceLevel.Name = "checkedComboScienceLevel";
            this.checkedComboScienceLevel.SelectedIndex = -1;
            this.checkedComboScienceLevel.Size = new System.Drawing.Size(450, 23);
            this.checkedComboScienceLevel.TabIndex = 6;
            this.checkedComboScienceLevel.TextCombo = "Научно звание";
            // 
            // checkBoxNLK
            // 
            this.checkBoxNLK.Location = new System.Drawing.Point(8, 244);
            this.checkBoxNLK.Name = "checkBoxNLK";
            this.checkBoxNLK.Size = new System.Drawing.Size(450, 23);
            this.checkBoxNLK.TabIndex = 11;
            this.checkBoxNLK.Text = "Данни на лична карта";
            // 
            // checkedComboCountry
            // 
            this.checkedComboCountry.Checked = false;
            this.checkedComboCountry.Column = "person.country";
            this.checkedComboCountry.Data = null;
            this.checkedComboCountry.DropDownWidth = 160;
            this.checkedComboCountry.Location = new System.Drawing.Point(8, 119);
            this.checkedComboCountry.Name = "checkedComboCountry";
            this.checkedComboCountry.SelectedIndex = -1;
            this.checkedComboCountry.Size = new System.Drawing.Size(450, 23);
            this.checkedComboCountry.TabIndex = 5;
            this.checkedComboCountry.TextCombo = "Рожденна страна";
            // 
            // checkBoxAdress
            // 
            this.checkBoxAdress.Location = new System.Drawing.Point(8, 219);
            this.checkBoxAdress.Name = "checkBoxAdress";
            this.checkBoxAdress.Size = new System.Drawing.Size(450, 23);
            this.checkBoxAdress.TabIndex = 10;
            this.checkBoxAdress.Text = "Адрес";
            // 
            // checkedComboFamilyStatus
            // 
            this.checkedComboFamilyStatus.Checked = false;
            this.checkedComboFamilyStatus.Column = "person.familystatus";
            this.checkedComboFamilyStatus.Data = null;
            this.checkedComboFamilyStatus.DropDownWidth = 160;
            this.checkedComboFamilyStatus.Location = new System.Drawing.Point(8, 94);
            this.checkedComboFamilyStatus.Name = "checkedComboFamilyStatus";
            this.checkedComboFamilyStatus.SelectedIndex = -1;
            this.checkedComboFamilyStatus.Size = new System.Drawing.Size(450, 23);
            this.checkedComboFamilyStatus.TabIndex = 4;
            this.checkedComboFamilyStatus.TextCombo = "Семеен статус";
            // 
            // checkedComboEducation
            // 
            this.checkedComboEducation.Checked = false;
            this.checkedComboEducation.Column = "person.education";
            this.checkedComboEducation.Data = null;
            this.checkedComboEducation.DropDownWidth = 160;
            this.checkedComboEducation.Location = new System.Drawing.Point(8, 19);
            this.checkedComboEducation.Name = "checkedComboEducation";
            this.checkedComboEducation.SelectedIndex = -1;
            this.checkedComboEducation.Size = new System.Drawing.Size(450, 23);
            this.checkedComboEducation.TabIndex = 1;
            this.checkedComboEducation.TextCombo = "Образование";
            // 
            // checkedComboLanguage
            // 
            this.checkedComboLanguage.Checked = false;
            this.checkedComboLanguage.Column = "languagelevel.language";
            this.checkedComboLanguage.Data = null;
            this.checkedComboLanguage.DropDownWidth = 160;
            this.checkedComboLanguage.Location = new System.Drawing.Point(8, 69);
            this.checkedComboLanguage.Name = "checkedComboLanguage";
            this.checkedComboLanguage.SelectedIndex = -1;
            this.checkedComboLanguage.Size = new System.Drawing.Size(450, 23);
            this.checkedComboLanguage.TabIndex = 3;
            this.checkedComboLanguage.TextCombo = "Чужд език";
            // 
            // checkedComboMilitaryStatus
            // 
            this.checkedComboMilitaryStatus.Checked = false;
            this.checkedComboMilitaryStatus.Column = "person.militaryrang";
            this.checkedComboMilitaryStatus.Data = null;
            this.checkedComboMilitaryStatus.DropDownWidth = 160;
            this.checkedComboMilitaryStatus.Location = new System.Drawing.Point(8, 44);
            this.checkedComboMilitaryStatus.Name = "checkedComboMilitaryStatus";
            this.checkedComboMilitaryStatus.SelectedIndex = -1;
            this.checkedComboMilitaryStatus.Size = new System.Drawing.Size(450, 23);
            this.checkedComboMilitaryStatus.TabIndex = 2;
            this.checkedComboMilitaryStatus.TextCombo = "Военно звание";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.labelYounger);
            this.groupBox3.Controls.Add(this.checkBoxAge);
            this.groupBox3.Controls.Add(this.numBoxYounger);
            this.groupBox3.Controls.Add(this.numBoxOlder);
            this.groupBox3.Location = new System.Drawing.Point(8, 527);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(968, 91);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Справка по възраст";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(289, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "До:";
            // 
            // labelYounger
            // 
            this.labelYounger.Location = new System.Drawing.Point(177, 40);
            this.labelYounger.Name = "labelYounger";
            this.labelYounger.Size = new System.Drawing.Size(24, 16);
            this.labelYounger.TabIndex = 1;
            this.labelYounger.Text = "От:";
            // 
            // checkBoxAge
            // 
            this.checkBoxAge.Location = new System.Drawing.Point(8, 40);
            this.checkBoxAge.Name = "checkBoxAge";
            this.checkBoxAge.Size = new System.Drawing.Size(200, 16);
            this.checkBoxAge.TabIndex = 0;
            this.checkBoxAge.Text = "Навършени години";
            this.checkBoxAge.CheckedChanged += new System.EventHandler(this.checkBoxAge_CheckedChanged);
            // 
            // numBoxYounger
            // 
            this.numBoxYounger.Enabled = false;
            this.numBoxYounger.Location = new System.Drawing.Point(209, 40);
            this.numBoxYounger.Name = "numBoxYounger";
            this.numBoxYounger.Size = new System.Drawing.Size(72, 20);
            this.numBoxYounger.TabIndex = 2;
            // 
            // numBoxOlder
            // 
            this.numBoxOlder.Enabled = false;
            this.numBoxOlder.Location = new System.Drawing.Point(321, 40);
            this.numBoxOlder.Name = "numBoxOlder";
            this.numBoxOlder.Size = new System.Drawing.Size(64, 20);
            this.numBoxOlder.TabIndex = 4;
            // 
            // TabPageAssignment
            // 
            this.TabPageAssignment.Controls.Add(this.groupBox5);
            this.TabPageAssignment.Controls.Add(this.groupBox6);
            this.TabPageAssignment.Controls.Add(this.groupBox4);
            this.TabPageAssignment.Controls.Add(this.gbAssignment);
            this.TabPageAssignment.Location = new System.Drawing.Point(4, 22);
            this.TabPageAssignment.Name = "TabPageAssignment";
            this.TabPageAssignment.Size = new System.Drawing.Size(980, 630);
            this.TabPageAssignment.TabIndex = 1;
            this.TabPageAssignment.Text = "Данни за назначение";
            this.TabPageAssignment.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.numBoxExpFrom);
            this.groupBox5.Controls.Add(this.numBoxExpTo);
            this.groupBox5.Controls.Add(this.checkBoxExp);
            this.groupBox5.Location = new System.Drawing.Point(503, 508);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(473, 76);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Справка по трудов стаж";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(225, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 16);
            this.label6.TabIndex = 8;
            this.label6.Text = "До:";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(120, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 16);
            this.label7.TabIndex = 7;
            this.label7.Text = "От:";
            // 
            // numBoxExpFrom
            // 
            this.numBoxExpFrom.Enabled = false;
            this.numBoxExpFrom.Location = new System.Drawing.Point(120, 32);
            this.numBoxExpFrom.Name = "numBoxExpFrom";
            this.numBoxExpFrom.Size = new System.Drawing.Size(97, 20);
            this.numBoxExpFrom.TabIndex = 1;
            // 
            // numBoxExpTo
            // 
            this.numBoxExpTo.Enabled = false;
            this.numBoxExpTo.Location = new System.Drawing.Point(228, 32);
            this.numBoxExpTo.Name = "numBoxExpTo";
            this.numBoxExpTo.Size = new System.Drawing.Size(97, 20);
            this.numBoxExpTo.TabIndex = 2;
            // 
            // checkBoxExp
            // 
            this.checkBoxExp.Location = new System.Drawing.Point(8, 32);
            this.checkBoxExp.Name = "checkBoxExp";
            this.checkBoxExp.Size = new System.Drawing.Size(104, 16);
            this.checkBoxExp.TabIndex = 0;
            this.checkBoxExp.Text = "Брой години";
            this.checkBoxExp.CheckedChanged += new System.EventHandler(this.checkBoxExp_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.checkBoxPayment);
            this.groupBox6.Controls.Add(this.numBoxPaymentFrom);
            this.groupBox6.Controls.Add(this.numBoxPaymentTo);
            this.groupBox6.Location = new System.Drawing.Point(503, 408);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(473, 74);
            this.groupBox6.TabIndex = 12;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Справка по заплата";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(228, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 16);
            this.label8.TabIndex = 8;
            this.label8.Text = "До:";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(120, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 16);
            this.label9.TabIndex = 7;
            this.label9.Text = "От:";
            // 
            // checkBoxPayment
            // 
            this.checkBoxPayment.Location = new System.Drawing.Point(8, 24);
            this.checkBoxPayment.Name = "checkBoxPayment";
            this.checkBoxPayment.Size = new System.Drawing.Size(112, 40);
            this.checkBoxPayment.TabIndex = 0;
            this.checkBoxPayment.Text = "Заплата в лв.";
            this.checkBoxPayment.CheckedChanged += new System.EventHandler(this.checkBoxPayment_CheckedChanged);
            // 
            // numBoxPaymentFrom
            // 
            this.numBoxPaymentFrom.Enabled = false;
            this.numBoxPaymentFrom.Location = new System.Drawing.Point(120, 40);
            this.numBoxPaymentFrom.Name = "numBoxPaymentFrom";
            this.numBoxPaymentFrom.Size = new System.Drawing.Size(97, 20);
            this.numBoxPaymentFrom.TabIndex = 5;
            // 
            // numBoxPaymentTo
            // 
            this.numBoxPaymentTo.Enabled = false;
            this.numBoxPaymentTo.Location = new System.Drawing.Point(228, 40);
            this.numBoxPaymentTo.Name = "numBoxPaymentTo";
            this.numBoxPaymentTo.Size = new System.Drawing.Size(97, 20);
            this.numBoxPaymentTo.TabIndex = 6;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBoxTestContractExpiraty);
            this.groupBox4.Controls.Add(this.dateTimePickerTestContractExpiry2);
            this.groupBox4.Controls.Add(this.dateTimePickerTestContractExpiry1);
            this.groupBox4.Controls.Add(this.checkBoxAssignedAt);
            this.groupBox4.Controls.Add(this.checkBoxContractExpiry);
            this.groupBox4.Controls.Add(this.dateTimePickerContractExpiry2);
            this.groupBox4.Controls.Add(this.dateTimePickerContractExpiry1);
            this.groupBox4.Controls.Add(this.dateTimePickerAssignedAt2);
            this.groupBox4.Controls.Add(this.dateTimePickerAssignedAt1);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Location = new System.Drawing.Point(8, 407);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(489, 210);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Хронологичност";
            // 
            // checkBoxTestContractExpiraty
            // 
            this.checkBoxTestContractExpiraty.Location = new System.Drawing.Point(8, 145);
            this.checkBoxTestContractExpiraty.Name = "checkBoxTestContractExpiraty";
            this.checkBoxTestContractExpiraty.Size = new System.Drawing.Size(328, 16);
            this.checkBoxTestContractExpiraty.TabIndex = 6;
            this.checkBoxTestContractExpiraty.Text = "Изпитателния срок на служителя изтича в интервала";
            this.checkBoxTestContractExpiraty.CheckedChanged += new System.EventHandler(this.checkBoxTestContractExpiraty_CheckedChanged);
            // 
            // dateTimePickerTestContractExpiry2
            // 
            this.dateTimePickerTestContractExpiry2.Enabled = false;
            this.dateTimePickerTestContractExpiry2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerTestContractExpiry2.Location = new System.Drawing.Point(234, 183);
            this.dateTimePickerTestContractExpiry2.Name = "dateTimePickerTestContractExpiry2";
            this.dateTimePickerTestContractExpiry2.Size = new System.Drawing.Size(180, 20);
            this.dateTimePickerTestContractExpiry2.TabIndex = 8;
            // 
            // dateTimePickerTestContractExpiry1
            // 
            this.dateTimePickerTestContractExpiry1.Enabled = false;
            this.dateTimePickerTestContractExpiry1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerTestContractExpiry1.Location = new System.Drawing.Point(26, 183);
            this.dateTimePickerTestContractExpiry1.Name = "dateTimePickerTestContractExpiry1";
            this.dateTimePickerTestContractExpiry1.Size = new System.Drawing.Size(180, 20);
            this.dateTimePickerTestContractExpiry1.TabIndex = 7;
            // 
            // checkBoxAssignedAt
            // 
            this.checkBoxAssignedAt.Location = new System.Drawing.Point(8, 19);
            this.checkBoxAssignedAt.Name = "checkBoxAssignedAt";
            this.checkBoxAssignedAt.Size = new System.Drawing.Size(232, 16);
            this.checkBoxAssignedAt.TabIndex = 0;
            this.checkBoxAssignedAt.Text = "Служителя е назначен в интервала";
            this.checkBoxAssignedAt.CheckedChanged += new System.EventHandler(this.checkBoxAssignedAt_CheckedChanged);
            // 
            // checkBoxContractExpiry
            // 
            this.checkBoxContractExpiry.Location = new System.Drawing.Point(6, 83);
            this.checkBoxContractExpiry.Name = "checkBoxContractExpiry";
            this.checkBoxContractExpiry.Size = new System.Drawing.Size(328, 16);
            this.checkBoxContractExpiry.TabIndex = 3;
            this.checkBoxContractExpiry.Text = "Срока на договора на служителя изтича в интервала";
            this.checkBoxContractExpiry.CheckedChanged += new System.EventHandler(this.checkBoxContractExpiry_CheckedChanged);
            // 
            // dateTimePickerContractExpiry2
            // 
            this.dateTimePickerContractExpiry2.Enabled = false;
            this.dateTimePickerContractExpiry2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerContractExpiry2.Location = new System.Drawing.Point(234, 121);
            this.dateTimePickerContractExpiry2.Name = "dateTimePickerContractExpiry2";
            this.dateTimePickerContractExpiry2.Size = new System.Drawing.Size(180, 20);
            this.dateTimePickerContractExpiry2.TabIndex = 5;
            // 
            // dateTimePickerContractExpiry1
            // 
            this.dateTimePickerContractExpiry1.Enabled = false;
            this.dateTimePickerContractExpiry1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerContractExpiry1.Location = new System.Drawing.Point(26, 121);
            this.dateTimePickerContractExpiry1.Name = "dateTimePickerContractExpiry1";
            this.dateTimePickerContractExpiry1.Size = new System.Drawing.Size(180, 20);
            this.dateTimePickerContractExpiry1.TabIndex = 4;
            // 
            // dateTimePickerAssignedAt2
            // 
            this.dateTimePickerAssignedAt2.Enabled = false;
            this.dateTimePickerAssignedAt2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerAssignedAt2.Location = new System.Drawing.Point(234, 57);
            this.dateTimePickerAssignedAt2.Name = "dateTimePickerAssignedAt2";
            this.dateTimePickerAssignedAt2.Size = new System.Drawing.Size(180, 20);
            this.dateTimePickerAssignedAt2.TabIndex = 2;
            // 
            // dateTimePickerAssignedAt1
            // 
            this.dateTimePickerAssignedAt1.Enabled = false;
            this.dateTimePickerAssignedAt1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerAssignedAt1.Location = new System.Drawing.Point(26, 57);
            this.dateTimePickerAssignedAt1.Name = "dateTimePickerAssignedAt1";
            this.dateTimePickerAssignedAt1.Size = new System.Drawing.Size(180, 20);
            this.dateTimePickerAssignedAt1.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(23, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "От:";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(231, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "До:";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(23, 102);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(24, 16);
            this.label12.TabIndex = 7;
            this.label12.Text = "От:";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(21, 164);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(24, 16);
            this.label13.TabIndex = 7;
            this.label13.Text = "От:";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(231, 102);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(24, 16);
            this.label14.TabIndex = 8;
            this.label14.Text = "До:";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(231, 164);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(24, 16);
            this.label15.TabIndex = 8;
            this.label15.Text = "До:";
            // 
            // gbAssignment
            // 
            this.gbAssignment.Controls.Add(this.checkBoxExperience);
            this.gbAssignment.Controls.Add(this.checkBoxSalaryAddon);
            this.gbAssignment.Controls.Add(this.buttonSelectPosition);
            this.gbAssignment.Controls.Add(this.checkedComboTutorAbsenceReason);
            this.gbAssignment.Controls.Add(this.checkedComboReasonAssignment);
            this.gbAssignment.Controls.Add(this.checkedContractType);
            this.gbAssignment.Controls.Add(this.checkedComboWorkTime);
            this.gbAssignment.Controls.Add(this.checkBoxActiveOnly);
            this.gbAssignment.Controls.Add(this.checkedComboContract);
            this.gbAssignment.Controls.Add(this.checkedComboAdministration);
            this.gbAssignment.Controls.Add(this.checkedComboProfessionn);
            this.gbAssignment.Controls.Add(this.checkedComboEKDA);
            this.gbAssignment.Controls.Add(this.checkedComboSector);
            this.gbAssignment.Controls.Add(this.checkedComboDirection);
            this.gbAssignment.Controls.Add(this.checkedComboDepartment);
            this.gbAssignment.Location = new System.Drawing.Point(3, 3);
            this.gbAssignment.Name = "gbAssignment";
            this.gbAssignment.Size = new System.Drawing.Size(973, 398);
            this.gbAssignment.TabIndex = 0;
            this.gbAssignment.TabStop = false;
            this.gbAssignment.Text = "Избор на условия по данни от трудов договор";
            // 
            // checkBoxSalaryAddon
            // 
            this.checkBoxSalaryAddon.AutoSize = true;
            this.checkBoxSalaryAddon.Location = new System.Drawing.Point(8, 343);
            this.checkBoxSalaryAddon.Name = "checkBoxSalaryAddon";
            this.checkBoxSalaryAddon.Size = new System.Drawing.Size(148, 17);
            this.checkBoxSalaryAddon.TabIndex = 11;
            this.checkBoxSalaryAddon.Text = "Надбавки / % Надбавки";
            this.checkBoxSalaryAddon.UseVisualStyleBackColor = true;
            // 
            // buttonSelectPosition
            // 
            this.buttonSelectPosition.Image = ((System.Drawing.Image)(resources.GetObject("buttonSelectPosition.Image")));
            this.buttonSelectPosition.Location = new System.Drawing.Point(455, 127);
            this.buttonSelectPosition.Name = "buttonSelectPosition";
            this.buttonSelectPosition.Size = new System.Drawing.Size(23, 23);
            this.buttonSelectPosition.TabIndex = 6;
            this.buttonSelectPosition.Click += new System.EventHandler(this.buttonSelectPosition_Click);
            // 
            // checkedComboTutorAbsenceReason
            // 
            this.checkedComboTutorAbsenceReason.Checked = false;
            this.checkedComboTutorAbsenceReason.Column = "personassignment.TutorAbsenceReason";
            this.checkedComboTutorAbsenceReason.Data = null;
            this.checkedComboTutorAbsenceReason.DropDownWidth = 160;
            this.checkedComboTutorAbsenceReason.Location = new System.Drawing.Point(8, 291);
            this.checkedComboTutorAbsenceReason.Name = "checkedComboTutorAbsenceReason";
            this.checkedComboTutorAbsenceReason.SelectedIndex = -1;
            this.checkedComboTutorAbsenceReason.Size = new System.Drawing.Size(450, 23);
            this.checkedComboTutorAbsenceReason.TabIndex = 2;
            this.checkedComboTutorAbsenceReason.TextCombo = "Отсъствие на титуляр:";
            // 
            // checkedComboReasonAssignment
            // 
            this.checkedComboReasonAssignment.Checked = false;
            this.checkedComboReasonAssignment.Column = "personassignment.assignreason";
            this.checkedComboReasonAssignment.Data = null;
            this.checkedComboReasonAssignment.DropDownWidth = 320;
            this.checkedComboReasonAssignment.Location = new System.Drawing.Point(8, 208);
            this.checkedComboReasonAssignment.Name = "checkedComboReasonAssignment";
            this.checkedComboReasonAssignment.SelectedIndex = -1;
            this.checkedComboReasonAssignment.Size = new System.Drawing.Size(450, 23);
            this.checkedComboReasonAssignment.TabIndex = 9;
            this.checkedComboReasonAssignment.TextCombo = "Основание назначение";
            // 
            // checkedContractType
            // 
            this.checkedContractType.Checked = false;
            this.checkedContractType.Column = "personassignment.law";
            this.checkedContractType.Data = null;
            this.checkedContractType.DropDownWidth = 160;
            this.checkedContractType.Location = new System.Drawing.Point(8, 262);
            this.checkedContractType.Name = "checkedContractType";
            this.checkedContractType.SelectedIndex = -1;
            this.checkedContractType.Size = new System.Drawing.Size(450, 23);
            this.checkedContractType.TabIndex = 10;
            this.checkedContractType.TextCombo = "Взаимоотношение";
            // 
            // checkedComboWorkTime
            // 
            this.checkedComboWorkTime.Checked = false;
            this.checkedComboWorkTime.Column = "personassignment.worktime";
            this.checkedComboWorkTime.Data = null;
            this.checkedComboWorkTime.DropDownWidth = 320;
            this.checkedComboWorkTime.Location = new System.Drawing.Point(8, 181);
            this.checkedComboWorkTime.Name = "checkedComboWorkTime";
            this.checkedComboWorkTime.SelectedIndex = -1;
            this.checkedComboWorkTime.Size = new System.Drawing.Size(450, 23);
            this.checkedComboWorkTime.TabIndex = 8;
            this.checkedComboWorkTime.TextCombo = "Работно време";
            // 
            // checkBoxActiveOnly
            // 
            this.checkBoxActiveOnly.AutoSize = true;
            this.checkBoxActiveOnly.Location = new System.Drawing.Point(8, 320);
            this.checkBoxActiveOnly.Name = "checkBoxActiveOnly";
            this.checkBoxActiveOnly.Size = new System.Drawing.Size(266, 17);
            this.checkBoxActiveOnly.TabIndex = 0;
            this.checkBoxActiveOnly.Text = "Изключване на допълнителните споразумения";
            this.checkBoxActiveOnly.UseVisualStyleBackColor = true;
            // 
            // checkedComboContract
            // 
            this.checkedComboContract.Checked = false;
            this.checkedComboContract.Column = "personassignment.contract";
            this.checkedComboContract.Data = null;
            this.checkedComboContract.DropDownWidth = 320;
            this.checkedComboContract.Location = new System.Drawing.Point(8, 154);
            this.checkedComboContract.Name = "checkedComboContract";
            this.checkedComboContract.SelectedIndex = -1;
            this.checkedComboContract.Size = new System.Drawing.Size(450, 23);
            this.checkedComboContract.TabIndex = 7;
            this.checkedComboContract.TextCombo = "Договор";
            // 
            // checkedComboAdministration
            // 
            this.checkedComboAdministration.Checked = false;
            this.checkedComboAdministration.Column = "personassignment.level1";
            this.checkedComboAdministration.Data = null;
            this.checkedComboAdministration.DropDownWidth = 320;
            this.checkedComboAdministration.Location = new System.Drawing.Point(8, 19);
            this.checkedComboAdministration.Name = "checkedComboAdministration";
            this.checkedComboAdministration.SelectedIndex = -1;
            this.checkedComboAdministration.Size = new System.Drawing.Size(450, 23);
            this.checkedComboAdministration.TabIndex = 1;
            this.checkedComboAdministration.TextCombo = "Администрация";
            // 
            // checkedComboProfessionn
            // 
            this.checkedComboProfessionn.Checked = false;
            this.checkedComboProfessionn.Column = "personassignment.position";
            this.checkedComboProfessionn.Data = null;
            this.checkedComboProfessionn.DropDownWidth = 320;
            this.checkedComboProfessionn.Location = new System.Drawing.Point(8, 127);
            this.checkedComboProfessionn.Name = "checkedComboProfessionn";
            this.checkedComboProfessionn.SelectedIndex = -1;
            this.checkedComboProfessionn.Size = new System.Drawing.Size(450, 23);
            this.checkedComboProfessionn.TabIndex = 5;
            this.checkedComboProfessionn.TextCombo = "Длъжност";
            // 
            // checkedComboEKDA
            // 
            this.checkedComboEKDA.Checked = false;
            this.checkedComboEKDA.Column = "personassignment.EKDACode";
            this.checkedComboEKDA.Data = null;
            this.checkedComboEKDA.DropDownWidth = 320;
            this.checkedComboEKDA.Location = new System.Drawing.Point(8, 235);
            this.checkedComboEKDA.Name = "checkedComboEKDA";
            this.checkedComboEKDA.SelectedIndex = -1;
            this.checkedComboEKDA.Size = new System.Drawing.Size(450, 23);
            this.checkedComboEKDA.TabIndex = 10;
            this.checkedComboEKDA.TextCombo = "Тип длъжност";
            // 
            // checkedComboSector
            // 
            this.checkedComboSector.Checked = false;
            this.checkedComboSector.Column = "personassignment.level4";
            this.checkedComboSector.Data = null;
            this.checkedComboSector.DropDownWidth = 320;
            this.checkedComboSector.Location = new System.Drawing.Point(8, 100);
            this.checkedComboSector.Name = "checkedComboSector";
            this.checkedComboSector.SelectedIndex = -1;
            this.checkedComboSector.Size = new System.Drawing.Size(450, 23);
            this.checkedComboSector.TabIndex = 4;
            this.checkedComboSector.TextCombo = "Сектор";
            // 
            // checkedComboDirection
            // 
            this.checkedComboDirection.Checked = false;
            this.checkedComboDirection.Column = "personassignment.level2";
            this.checkedComboDirection.Data = null;
            this.checkedComboDirection.DropDownWidth = 320;
            this.checkedComboDirection.Location = new System.Drawing.Point(8, 46);
            this.checkedComboDirection.Name = "checkedComboDirection";
            this.checkedComboDirection.SelectedIndex = -1;
            this.checkedComboDirection.Size = new System.Drawing.Size(450, 23);
            this.checkedComboDirection.TabIndex = 2;
            this.checkedComboDirection.TextCombo = "Дирекция";
            // 
            // checkedComboDepartment
            // 
            this.checkedComboDepartment.Checked = false;
            this.checkedComboDepartment.Column = "personassignment.level3";
            this.checkedComboDepartment.Data = null;
            this.checkedComboDepartment.DropDownWidth = 320;
            this.checkedComboDepartment.Location = new System.Drawing.Point(8, 73);
            this.checkedComboDepartment.Name = "checkedComboDepartment";
            this.checkedComboDepartment.SelectedIndex = -1;
            this.checkedComboDepartment.Size = new System.Drawing.Size(450, 23);
            this.checkedComboDepartment.TabIndex = 3;
            this.checkedComboDepartment.TextCombo = "Отдел";
            // 
            // tabPageAbsence
            // 
            this.tabPageAbsence.Controls.Add(this.gbAbsence);
            this.tabPageAbsence.Controls.Add(this.groupBox7);
            this.tabPageAbsence.Location = new System.Drawing.Point(4, 22);
            this.tabPageAbsence.Name = "tabPageAbsence";
            this.tabPageAbsence.Size = new System.Drawing.Size(980, 630);
            this.tabPageAbsence.TabIndex = 2;
            this.tabPageAbsence.Text = "Данни за отсъствия";
            this.tabPageAbsence.UseVisualStyleBackColor = true;
            // 
            // gbAbsence
            // 
            this.gbAbsence.Controls.Add(this.checkBoxAbsenceManagement);
            this.gbAbsence.Controls.Add(this.checkedComboTypeAbsence);
            this.gbAbsence.Location = new System.Drawing.Point(12, 10);
            this.gbAbsence.Name = "gbAbsence";
            this.gbAbsence.Size = new System.Drawing.Size(967, 225);
            this.gbAbsence.TabIndex = 0;
            this.gbAbsence.TabStop = false;
            this.gbAbsence.Text = "Избор на условия за отсъствие";
            // 
            // checkBoxAbsenceManagement
            // 
            this.checkBoxAbsenceManagement.AutoSize = true;
            this.checkBoxAbsenceManagement.Location = new System.Drawing.Point(16, 55);
            this.checkBoxAbsenceManagement.Name = "checkBoxAbsenceManagement";
            this.checkBoxAbsenceManagement.Size = new System.Drawing.Size(223, 17);
            this.checkBoxAbsenceManagement.TabIndex = 12;
            this.checkBoxAbsenceManagement.Text = "Отсъствия на управленски длъжности";
            this.checkBoxAbsenceManagement.UseVisualStyleBackColor = true;
            // 
            // checkedComboTypeAbsence
            // 
            this.checkedComboTypeAbsence.Checked = false;
            this.checkedComboTypeAbsence.Column = "absence.TypeAbsence";
            this.checkedComboTypeAbsence.Data = null;
            this.checkedComboTypeAbsence.DropDownWidth = 160;
            this.checkedComboTypeAbsence.Location = new System.Drawing.Point(16, 26);
            this.checkedComboTypeAbsence.Name = "checkedComboTypeAbsence";
            this.checkedComboTypeAbsence.SelectedIndex = -1;
            this.checkedComboTypeAbsence.Size = new System.Drawing.Size(450, 23);
            this.checkedComboTypeAbsence.TabIndex = 1;
            this.checkedComboTypeAbsence.TextCombo = "Вид на отсъствието";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label20);
            this.groupBox7.Controls.Add(this.checkBoxFrom);
            this.groupBox7.Controls.Add(this.dateTimePickerFrom2);
            this.groupBox7.Controls.Add(this.dateTimePickerFrom1);
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Location = new System.Drawing.Point(8, 240);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(968, 218);
            this.groupBox7.TabIndex = 10;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Хронологичност";
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(23, 42);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(32, 16);
            this.label20.TabIndex = 6;
            this.label20.Text = "От:";
            // 
            // checkBoxFrom
            // 
            this.checkBoxFrom.Location = new System.Drawing.Point(8, 16);
            this.checkBoxFrom.Name = "checkBoxFrom";
            this.checkBoxFrom.Size = new System.Drawing.Size(224, 24);
            this.checkBoxFrom.TabIndex = 0;
            this.checkBoxFrom.Text = "Служителя е отсъствал в интервала";
            this.checkBoxFrom.CheckedChanged += new System.EventHandler(this.checkBoxFrom_CheckedChanged);
            // 
            // dateTimePickerFrom2
            // 
            this.dateTimePickerFrom2.Enabled = false;
            this.dateTimePickerFrom2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerFrom2.Location = new System.Drawing.Point(248, 60);
            this.dateTimePickerFrom2.Name = "dateTimePickerFrom2";
            this.dateTimePickerFrom2.Size = new System.Drawing.Size(180, 20);
            this.dateTimePickerFrom2.TabIndex = 2;
            // 
            // dateTimePickerFrom1
            // 
            this.dateTimePickerFrom1.Enabled = false;
            this.dateTimePickerFrom1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerFrom1.Location = new System.Drawing.Point(26, 60);
            this.dateTimePickerFrom1.Name = "dateTimePickerFrom1";
            this.dateTimePickerFrom1.Size = new System.Drawing.Size(180, 20);
            this.dateTimePickerFrom1.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(245, 42);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 16);
            this.label10.TabIndex = 3;
            this.label10.Text = "До:";
            // 
            // tabPagePenalty
            // 
            this.tabPagePenalty.Controls.Add(this.groupBox2);
            this.tabPagePenalty.Controls.Add(this.gbPenalty);
            this.tabPagePenalty.Location = new System.Drawing.Point(4, 22);
            this.tabPagePenalty.Name = "tabPagePenalty";
            this.tabPagePenalty.Size = new System.Drawing.Size(980, 630);
            this.tabPagePenalty.TabIndex = 3;
            this.tabPagePenalty.Text = "Данни за наказание";
            this.tabPagePenalty.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxFormDate);
            this.groupBox2.Controls.Add(this.checkBoxPenaltyDate);
            this.groupBox2.Controls.Add(this.dateTimePickerFormDate1);
            this.groupBox2.Controls.Add(this.dateTimePickerFormDate2);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.dateTimePickerPenaltyDate2);
            this.groupBox2.Controls.Add(this.dateTimePickerPenaltyDate1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Location = new System.Drawing.Point(8, 240);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(955, 212);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Хронологичност";
            // 
            // checkBoxFormDate
            // 
            this.checkBoxFormDate.Enabled = false;
            this.checkBoxFormDate.Location = new System.Drawing.Point(6, 87);
            this.checkBoxFormDate.Name = "checkBoxFormDate";
            this.checkBoxFormDate.Size = new System.Drawing.Size(248, 24);
            this.checkBoxFormDate.TabIndex = 9;
            this.checkBoxFormDate.Text = "Наказанието е наложено в интервала";
            this.checkBoxFormDate.Visible = false;
            this.checkBoxFormDate.CheckedChanged += new System.EventHandler(this.checkBoxFormDate_CheckedChanged);
            // 
            // checkBoxPenaltyDate
            // 
            this.checkBoxPenaltyDate.Location = new System.Drawing.Point(6, 19);
            this.checkBoxPenaltyDate.Name = "checkBoxPenaltyDate";
            this.checkBoxPenaltyDate.Size = new System.Drawing.Size(336, 24);
            this.checkBoxPenaltyDate.TabIndex = 8;
            this.checkBoxPenaltyDate.Text = "Наказанието е влязло в сила в интервала";
            this.checkBoxPenaltyDate.CheckedChanged += new System.EventHandler(this.checkBoxPenaltyDate_CheckedChanged);
            // 
            // dateTimePickerFormDate1
            // 
            this.dateTimePickerFormDate1.Enabled = false;
            this.dateTimePickerFormDate1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerFormDate1.Location = new System.Drawing.Point(26, 129);
            this.dateTimePickerFormDate1.Name = "dateTimePickerFormDate1";
            this.dateTimePickerFormDate1.Size = new System.Drawing.Size(180, 20);
            this.dateTimePickerFormDate1.TabIndex = 6;
            this.dateTimePickerFormDate1.Visible = false;
            // 
            // dateTimePickerFormDate2
            // 
            this.dateTimePickerFormDate2.Enabled = false;
            this.dateTimePickerFormDate2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerFormDate2.Location = new System.Drawing.Point(246, 129);
            this.dateTimePickerFormDate2.Name = "dateTimePickerFormDate2";
            this.dateTimePickerFormDate2.Size = new System.Drawing.Size(180, 20);
            this.dateTimePickerFormDate2.TabIndex = 7;
            this.dateTimePickerFormDate2.Visible = false;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(243, 110);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(32, 16);
            this.label17.TabIndex = 3;
            this.label17.Text = "До:";
            this.label17.Visible = false;
            // 
            // dateTimePickerPenaltyDate2
            // 
            this.dateTimePickerPenaltyDate2.Enabled = false;
            this.dateTimePickerPenaltyDate2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerPenaltyDate2.Location = new System.Drawing.Point(246, 63);
            this.dateTimePickerPenaltyDate2.Name = "dateTimePickerPenaltyDate2";
            this.dateTimePickerPenaltyDate2.Size = new System.Drawing.Size(180, 20);
            this.dateTimePickerPenaltyDate2.TabIndex = 5;
            // 
            // dateTimePickerPenaltyDate1
            // 
            this.dateTimePickerPenaltyDate1.Enabled = false;
            this.dateTimePickerPenaltyDate1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerPenaltyDate1.Location = new System.Drawing.Point(26, 63);
            this.dateTimePickerPenaltyDate1.Name = "dateTimePickerPenaltyDate1";
            this.dateTimePickerPenaltyDate1.Size = new System.Drawing.Size(180, 20);
            this.dateTimePickerPenaltyDate1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(243, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "До:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(23, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "От:";
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(23, 111);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(32, 16);
            this.label16.TabIndex = 2;
            this.label16.Text = "От:";
            this.label16.Visible = false;
            // 
            // gbPenalty
            // 
            this.gbPenalty.Controls.Add(this.checkedComboTypeReason);
            this.gbPenalty.Controls.Add(this.checkedComboReason);
            this.gbPenalty.Location = new System.Drawing.Point(7, 3);
            this.gbPenalty.Name = "gbPenalty";
            this.gbPenalty.Size = new System.Drawing.Size(955, 232);
            this.gbPenalty.TabIndex = 2;
            this.gbPenalty.TabStop = false;
            this.gbPenalty.Text = "Избор на условия по данни от наказания";
            // 
            // checkedComboTypeReason
            // 
            this.checkedComboTypeReason.Checked = false;
            this.checkedComboTypeReason.Column = "penalty.typePenalty";
            this.checkedComboTypeReason.Data = null;
            this.checkedComboTypeReason.DropDownWidth = 160;
            this.checkedComboTypeReason.Location = new System.Drawing.Point(6, 16);
            this.checkedComboTypeReason.Name = "checkedComboTypeReason";
            this.checkedComboTypeReason.SelectedIndex = -1;
            this.checkedComboTypeReason.Size = new System.Drawing.Size(450, 24);
            this.checkedComboTypeReason.TabIndex = 8;
            this.checkedComboTypeReason.TextCombo = "Вид наказание";
            // 
            // checkedComboReason
            // 
            this.checkedComboReason.Checked = false;
            this.checkedComboReason.Column = "penalty.reason";
            this.checkedComboReason.Data = null;
            this.checkedComboReason.DropDownWidth = 160;
            this.checkedComboReason.Location = new System.Drawing.Point(6, 38);
            this.checkedComboReason.Name = "checkedComboReason";
            this.checkedComboReason.SelectedIndex = -1;
            this.checkedComboReason.Size = new System.Drawing.Size(450, 24);
            this.checkedComboReason.TabIndex = 7;
            this.checkedComboReason.TextCombo = "Основание";
            // 
            // tabPageFired
            // 
            this.tabPageFired.Controls.Add(this.labelFiredMessage);
            this.tabPageFired.Controls.Add(this.gbFired);
            this.tabPageFired.Location = new System.Drawing.Point(4, 22);
            this.tabPageFired.Name = "tabPageFired";
            this.tabPageFired.Size = new System.Drawing.Size(980, 630);
            this.tabPageFired.TabIndex = 4;
            this.tabPageFired.Text = "Прекратени договори";
            this.tabPageFired.UseVisualStyleBackColor = true;
            // 
            // labelFiredMessage
            // 
            this.labelFiredMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelFiredMessage.Location = new System.Drawing.Point(19, 174);
            this.labelFiredMessage.Name = "labelFiredMessage";
            this.labelFiredMessage.Size = new System.Drawing.Size(943, 56);
            this.labelFiredMessage.TabIndex = 66;
            this.labelFiredMessage.Text = "Справката за прекратени договори е достъпна през \"Картотека на прекратените догов" +
    "ори\"";
            this.labelFiredMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbFired
            // 
            this.gbFired.Controls.Add(this.label19);
            this.gbFired.Controls.Add(this.dateTimePickerFiredТоDate);
            this.gbFired.Controls.Add(this.checkBoxFiredFrom);
            this.gbFired.Controls.Add(this.checkedComboxFiredReason);
            this.gbFired.Controls.Add(this.dateTimePickerFiredFromDate);
            this.gbFired.Location = new System.Drawing.Point(8, 8);
            this.gbFired.Name = "gbFired";
            this.gbFired.Size = new System.Drawing.Size(968, 163);
            this.gbFired.TabIndex = 65;
            this.gbFired.TabStop = false;
            this.gbFired.Text = "Избор на условия по данни за прекратени договори";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(257, 67);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(25, 13);
            this.label19.TabIndex = 67;
            this.label19.Text = "До:";
            // 
            // dateTimePickerFiredТоDate
            // 
            this.dateTimePickerFiredТоDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerFiredТоDate.Location = new System.Drawing.Point(260, 89);
            this.dateTimePickerFiredТоDate.Name = "dateTimePickerFiredТоDate";
            this.dateTimePickerFiredТоDate.Size = new System.Drawing.Size(180, 20);
            this.dateTimePickerFiredТоDate.TabIndex = 66;
            // 
            // checkBoxFiredFrom
            // 
            this.checkBoxFiredFrom.Location = new System.Drawing.Point(16, 56);
            this.checkBoxFiredFrom.Name = "checkBoxFiredFrom";
            this.checkBoxFiredFrom.Size = new System.Drawing.Size(140, 24);
            this.checkBoxFiredFrom.TabIndex = 65;
            this.checkBoxFiredFrom.Text = "Прекратени  от:";
            this.checkBoxFiredFrom.CheckedChanged += new System.EventHandler(this.checkBoxFiredFrom_CheckedChanged);
            // 
            // checkedComboxFiredReason
            // 
            this.checkedComboxFiredReason.Checked = false;
            this.checkedComboxFiredReason.Column = "HR_fired.reason";
            this.checkedComboxFiredReason.Data = null;
            this.checkedComboxFiredReason.DropDownWidth = 320;
            this.checkedComboxFiredReason.Location = new System.Drawing.Point(16, 24);
            this.checkedComboxFiredReason.Name = "checkedComboxFiredReason";
            this.checkedComboxFiredReason.SelectedIndex = -1;
            this.checkedComboxFiredReason.Size = new System.Drawing.Size(450, 24);
            this.checkedComboxFiredReason.TabIndex = 2;
            this.checkedComboxFiredReason.TextCombo = "Oснование:";
            // 
            // dateTimePickerFiredFromDate
            // 
            this.dateTimePickerFiredFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerFiredFromDate.Location = new System.Drawing.Point(43, 89);
            this.dateTimePickerFiredFromDate.Name = "dateTimePickerFiredFromDate";
            this.dateTimePickerFiredFromDate.Size = new System.Drawing.Size(180, 20);
            this.dateTimePickerFiredFromDate.TabIndex = 63;
            // 
            // tabPageAtestacii
            // 
            this.tabPageAtestacii.Controls.Add(this.groupBoxAtestacii);
            this.tabPageAtestacii.Location = new System.Drawing.Point(4, 22);
            this.tabPageAtestacii.Name = "tabPageAtestacii";
            this.tabPageAtestacii.Size = new System.Drawing.Size(980, 630);
            this.tabPageAtestacii.TabIndex = 5;
            this.tabPageAtestacii.Text = "Атестации";
            this.tabPageAtestacii.UseVisualStyleBackColor = true;
            // 
            // groupBoxAtestacii
            // 
            this.groupBoxAtestacii.Controls.Add(this.label18);
            this.groupBoxAtestacii.Controls.Add(this.checkBoxAtestationPersonalRaise);
            this.groupBoxAtestacii.Controls.Add(this.checkBoxAtestationCountRaised);
            this.groupBoxAtestacii.Controls.Add(this.checkBoxAtestationRating);
            this.groupBoxAtestacii.Controls.Add(this.numericUpDownAtestationGrade);
            this.groupBoxAtestacii.Controls.Add(this.checkBoxAtestationEtaps);
            this.groupBoxAtestacii.Controls.Add(this.numericUpDownAtestationYears);
            this.groupBoxAtestacii.Location = new System.Drawing.Point(8, 8);
            this.groupBoxAtestacii.Name = "groupBoxAtestacii";
            this.groupBoxAtestacii.Size = new System.Drawing.Size(968, 619);
            this.groupBoxAtestacii.TabIndex = 0;
            this.groupBoxAtestacii.TabStop = false;
            this.groupBoxAtestacii.Text = "Избор на условия при атестации";
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(24, 32);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(208, 23);
            this.label18.TabIndex = 14;
            this.label18.Text = "Избор на годината за атестации";
            // 
            // checkBoxAtestationPersonalRaise
            // 
            this.checkBoxAtestationPersonalRaise.Location = new System.Drawing.Point(16, 136);
            this.checkBoxAtestationPersonalRaise.Name = "checkBoxAtestationPersonalRaise";
            this.checkBoxAtestationPersonalRaise.Size = new System.Drawing.Size(272, 24);
            this.checkBoxAtestationPersonalRaise.TabIndex = 13;
            this.checkBoxAtestationPersonalRaise.Text = "Служители подлежащи на повишение";
            // 
            // checkBoxAtestationCountRaised
            // 
            this.checkBoxAtestationCountRaised.Location = new System.Drawing.Point(16, 96);
            this.checkBoxAtestationCountRaised.Name = "checkBoxAtestationCountRaised";
            this.checkBoxAtestationCountRaised.Size = new System.Drawing.Size(376, 32);
            this.checkBoxAtestationCountRaised.TabIndex = 10;
            this.checkBoxAtestationCountRaised.Text = "Служители повишили оценката с спрямо предходната година";
            // 
            // checkBoxAtestationRating
            // 
            this.checkBoxAtestationRating.Location = new System.Drawing.Point(16, 64);
            this.checkBoxAtestationRating.Name = "checkBoxAtestationRating";
            this.checkBoxAtestationRating.Size = new System.Drawing.Size(232, 24);
            this.checkBoxAtestationRating.TabIndex = 8;
            this.checkBoxAtestationRating.Text = "Служители с оценка: ";
            this.checkBoxAtestationRating.CheckedChanged += new System.EventHandler(this.checkBoxAtestationRating_CheckedChanged);
            // 
            // numericUpDownAtestationGrade
            // 
            this.numericUpDownAtestationGrade.Enabled = false;
            this.numericUpDownAtestationGrade.Location = new System.Drawing.Point(272, 64);
            this.numericUpDownAtestationGrade.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownAtestationGrade.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAtestationGrade.Name = "numericUpDownAtestationGrade";
            this.numericUpDownAtestationGrade.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownAtestationGrade.TabIndex = 3;
            this.numericUpDownAtestationGrade.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // checkBoxAtestationEtaps
            // 
            this.checkBoxAtestationEtaps.Location = new System.Drawing.Point(16, 168);
            this.checkBoxAtestationEtaps.Name = "checkBoxAtestationEtaps";
            this.checkBoxAtestationEtaps.Size = new System.Drawing.Size(272, 24);
            this.checkBoxAtestationEtaps.TabIndex = 2;
            this.checkBoxAtestationEtaps.Text = "Проведени ли са трите етапа на оценяване";
            // 
            // numericUpDownAtestationYears
            // 
            this.numericUpDownAtestationYears.Location = new System.Drawing.Point(272, 32);
            this.numericUpDownAtestationYears.Maximum = new decimal(new int[] {
            2015,
            0,
            0,
            0});
            this.numericUpDownAtestationYears.Minimum = new decimal(new int[] {
            2002,
            0,
            0,
            0});
            this.numericUpDownAtestationYears.Name = "numericUpDownAtestationYears";
            this.numericUpDownAtestationYears.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownAtestationYears.TabIndex = 0;
            this.numericUpDownAtestationYears.Value = new decimal(new int[] {
            2003,
            0,
            0,
            0});
            // 
            // tabPageRangs
            // 
            this.tabPageRangs.Controls.Add(this.gbMilitaryRangs);
            this.tabPageRangs.Controls.Add(this.groupBoxRangHistoty);
            this.tabPageRangs.Location = new System.Drawing.Point(4, 22);
            this.tabPageRangs.Name = "tabPageRangs";
            this.tabPageRangs.Size = new System.Drawing.Size(980, 630);
            this.tabPageRangs.TabIndex = 6;
            this.tabPageRangs.Text = "Военни звания";
            this.tabPageRangs.UseVisualStyleBackColor = true;
            // 
            // gbMilitaryRangs
            // 
            this.gbMilitaryRangs.Controls.Add(this.textBoxRangNumberOrder);
            this.gbMilitaryRangs.Controls.Add(this.checkBoxRangNumberOrder);
            this.gbMilitaryRangs.Controls.Add(this.checkedComboMilitaryRang);
            this.gbMilitaryRangs.Location = new System.Drawing.Point(8, 12);
            this.gbMilitaryRangs.Name = "gbMilitaryRangs";
            this.gbMilitaryRangs.Size = new System.Drawing.Size(960, 232);
            this.gbMilitaryRangs.TabIndex = 13;
            this.gbMilitaryRangs.TabStop = false;
            this.gbMilitaryRangs.Text = "Военни звания";
            // 
            // textBoxRangNumberOrder
            // 
            this.textBoxRangNumberOrder.Enabled = false;
            this.textBoxRangNumberOrder.Location = new System.Drawing.Point(190, 46);
            this.textBoxRangNumberOrder.Name = "textBoxRangNumberOrder";
            this.textBoxRangNumberOrder.Size = new System.Drawing.Size(100, 20);
            this.textBoxRangNumberOrder.TabIndex = 24;
            // 
            // checkBoxRangNumberOrder
            // 
            this.checkBoxRangNumberOrder.Location = new System.Drawing.Point(7, 48);
            this.checkBoxRangNumberOrder.Name = "checkBoxRangNumberOrder";
            this.checkBoxRangNumberOrder.Size = new System.Drawing.Size(179, 16);
            this.checkBoxRangNumberOrder.TabIndex = 23;
            this.checkBoxRangNumberOrder.Text = "Номер на заповед за звание :";
            this.checkBoxRangNumberOrder.CheckedChanged += new System.EventHandler(this.checkBoxRangNumberOrder_CheckedChanged);
            // 
            // checkedComboMilitaryRang
            // 
            this.checkedComboMilitaryRang.Checked = false;
            this.checkedComboMilitaryRang.Column = "militaryrangs.MilitaryRang";
            this.checkedComboMilitaryRang.Data = null;
            this.checkedComboMilitaryRang.DropDownWidth = 160;
            this.checkedComboMilitaryRang.Location = new System.Drawing.Point(8, 19);
            this.checkedComboMilitaryRang.Name = "checkedComboMilitaryRang";
            this.checkedComboMilitaryRang.SelectedIndex = -1;
            this.checkedComboMilitaryRang.Size = new System.Drawing.Size(450, 23);
            this.checkedComboMilitaryRang.TabIndex = 7;
            this.checkedComboMilitaryRang.TextCombo = "Военно звание";
            // 
            // groupBoxRangHistoty
            // 
            this.groupBoxRangHistoty.Controls.Add(this.checkBoxMilitaryRangFrom);
            this.groupBoxRangHistoty.Controls.Add(this.checkBoxMilitaryRangOrderFrom);
            this.groupBoxRangHistoty.Controls.Add(this.dateTimePickerMilitaryRangOrderTo);
            this.groupBoxRangHistoty.Controls.Add(this.dateTimePickerMilitaryRangOrderFrom);
            this.groupBoxRangHistoty.Controls.Add(this.dateTimePickerMilitaryRangTo);
            this.groupBoxRangHistoty.Controls.Add(this.dateTimePickerMilitaryRangFrom);
            this.groupBoxRangHistoty.Controls.Add(this.label11);
            this.groupBoxRangHistoty.Controls.Add(this.label21);
            this.groupBoxRangHistoty.Controls.Add(this.label22);
            this.groupBoxRangHistoty.Controls.Add(this.label24);
            this.groupBoxRangHistoty.Location = new System.Drawing.Point(8, 250);
            this.groupBoxRangHistoty.Name = "groupBoxRangHistoty";
            this.groupBoxRangHistoty.Size = new System.Drawing.Size(960, 210);
            this.groupBoxRangHistoty.TabIndex = 12;
            this.groupBoxRangHistoty.TabStop = false;
            this.groupBoxRangHistoty.Text = "Хронологичност";
            // 
            // checkBoxMilitaryRangFrom
            // 
            this.checkBoxMilitaryRangFrom.Location = new System.Drawing.Point(8, 19);
            this.checkBoxMilitaryRangFrom.Name = "checkBoxMilitaryRangFrom";
            this.checkBoxMilitaryRangFrom.Size = new System.Drawing.Size(232, 16);
            this.checkBoxMilitaryRangFrom.TabIndex = 0;
            this.checkBoxMilitaryRangFrom.Text = "Военно звание влязло в сила в интервала";
            this.checkBoxMilitaryRangFrom.CheckedChanged += new System.EventHandler(this.checkBoxMilitaryRangFrom_CheckedChanged);
            // 
            // checkBoxMilitaryRangOrderFrom
            // 
            this.checkBoxMilitaryRangOrderFrom.Location = new System.Drawing.Point(8, 83);
            this.checkBoxMilitaryRangOrderFrom.Name = "checkBoxMilitaryRangOrderFrom";
            this.checkBoxMilitaryRangOrderFrom.Size = new System.Drawing.Size(328, 16);
            this.checkBoxMilitaryRangOrderFrom.TabIndex = 3;
            this.checkBoxMilitaryRangOrderFrom.Text = "Заповед за звание подписана в интервала";
            this.checkBoxMilitaryRangOrderFrom.CheckedChanged += new System.EventHandler(this.checkBoxMilitaryRangOrderFrom_CheckedChanged);
            // 
            // dateTimePickerMilitaryRangOrderTo
            // 
            this.dateTimePickerMilitaryRangOrderTo.Enabled = false;
            this.dateTimePickerMilitaryRangOrderTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerMilitaryRangOrderTo.Location = new System.Drawing.Point(234, 121);
            this.dateTimePickerMilitaryRangOrderTo.Name = "dateTimePickerMilitaryRangOrderTo";
            this.dateTimePickerMilitaryRangOrderTo.Size = new System.Drawing.Size(180, 20);
            this.dateTimePickerMilitaryRangOrderTo.TabIndex = 5;
            // 
            // dateTimePickerMilitaryRangOrderFrom
            // 
            this.dateTimePickerMilitaryRangOrderFrom.Enabled = false;
            this.dateTimePickerMilitaryRangOrderFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerMilitaryRangOrderFrom.Location = new System.Drawing.Point(26, 121);
            this.dateTimePickerMilitaryRangOrderFrom.Name = "dateTimePickerMilitaryRangOrderFrom";
            this.dateTimePickerMilitaryRangOrderFrom.Size = new System.Drawing.Size(180, 20);
            this.dateTimePickerMilitaryRangOrderFrom.TabIndex = 4;
            // 
            // dateTimePickerMilitaryRangTo
            // 
            this.dateTimePickerMilitaryRangTo.Enabled = false;
            this.dateTimePickerMilitaryRangTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerMilitaryRangTo.Location = new System.Drawing.Point(234, 57);
            this.dateTimePickerMilitaryRangTo.Name = "dateTimePickerMilitaryRangTo";
            this.dateTimePickerMilitaryRangTo.Size = new System.Drawing.Size(180, 20);
            this.dateTimePickerMilitaryRangTo.TabIndex = 2;
            // 
            // dateTimePickerMilitaryRangFrom
            // 
            this.dateTimePickerMilitaryRangFrom.Enabled = false;
            this.dateTimePickerMilitaryRangFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerMilitaryRangFrom.Location = new System.Drawing.Point(26, 57);
            this.dateTimePickerMilitaryRangFrom.Name = "dateTimePickerMilitaryRangFrom";
            this.dateTimePickerMilitaryRangFrom.Size = new System.Drawing.Size(180, 20);
            this.dateTimePickerMilitaryRangFrom.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(23, 38);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(24, 16);
            this.label11.TabIndex = 7;
            this.label11.Text = "От:";
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(231, 38);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(24, 16);
            this.label21.TabIndex = 8;
            this.label21.Text = "До:";
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(23, 102);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(24, 16);
            this.label22.TabIndex = 7;
            this.label22.Text = "От:";
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(231, 102);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(24, 16);
            this.label24.TabIndex = 8;
            this.label24.Text = "До:";
            // 
            // checkBoxExperience
            // 
            this.checkBoxExperience.AutoSize = true;
            this.checkBoxExperience.Location = new System.Drawing.Point(8, 366);
            this.checkBoxExperience.Name = "checkBoxExperience";
            this.checkBoxExperience.Size = new System.Drawing.Size(89, 17);
            this.checkBoxExperience.TabIndex = 12;
            this.checkBoxExperience.Text = "% Пр. време";
            this.checkBoxExperience.UseVisualStyleBackColor = true;
            // 
            // formStatisticTotal
            // 
            this.AcceptButton = this.buttonFind;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.buttonExit;
            this.ClientSize = new System.Drawing.Size(984, 702);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.checkBoxExportToExcel);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonFind);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formStatisticTotal";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Общи справки";
            this.tabControl1.ResumeLayout(false);
            this.TabPersonalInfo.ResumeLayout(false);
            this.gbPersonal.ResumeLayout(false);
            this.gbPersonal.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.TabPageAssignment.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.gbAssignment.ResumeLayout(false);
            this.gbAssignment.PerformLayout();
            this.tabPageAbsence.ResumeLayout(false);
            this.gbAbsence.ResumeLayout(false);
            this.gbAbsence.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.tabPagePenalty.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.gbPenalty.ResumeLayout(false);
            this.tabPageFired.ResumeLayout(false);
            this.gbFired.ResumeLayout(false);
            this.gbFired.PerformLayout();
            this.tabPageAtestacii.ResumeLayout(false);
            this.groupBoxAtestacii.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtestationGrade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtestationYears)).EndInit();
            this.tabPageRangs.ResumeLayout(false);
            this.gbMilitaryRangs.ResumeLayout(false);
            this.gbMilitaryRangs.PerformLayout();
            this.groupBoxRangHistoty.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private string GetPersonsFrom(string posId, ArrayList arr)
        {
            foreach (object o in arr)
            {
                posId += string.Format("  " + TableNames.Person + ".NodeId = {0} OR ", o.ToString());
            }
            return posId;
        }

        private void buttonFind_Click(object sender, System.EventArgs e)
        {
            try
            {
                // Check functions is for acquering data from database based on specific criteria - to Fill dtXXXX data tables
                this.join_clause = "";
                this.where_clause = "";
                CheckAssignment();
                CheckPenalty();
                CheckPersonal();
                CheckHoliday();
                CheckFired();
                CheckMilitaryRangs();
                CheckAtestation();

                ArrayList arrID = new ArrayList();
                ArrayList arrIDContainer = new ArrayList();
                DataTable dt1 = new DataTable();
                DataLayer.DataAction dAction = new DataLayer.DataAction(this.main.connString);


                // Adding all column criteria based on each specific statistics
                ArrayList arrColumns = new ArrayList();
                arrColumns.Add(TableNames.Person + ".id");
                if (arrColumnPersonal != null)
                {
                    arrColumns.InsertRange(arrColumns.Count, arrColumnPersonal);
                }
                if (arrColumnPenalty != null)
                {
                    arrColumns.InsertRange(arrColumns.Count, arrColumnPenalty);
                }
                if (arrColumnAbsence != null)
                {
                    arrColumns.InsertRange(arrColumns.Count, arrColumnAbsence);
                }
                if (arrColumnAssignment != null)
                {
                    arrColumns.InsertRange(arrColumns.Count, arrColumnAssignment);
                }
                if (arrColumnFired != null)
                {
                    arrColumns.InsertRange(arrColumns.Count, arrColumnFired);
                }
                if (arrColumnAtestation != null)
                {
                    arrColumns.InsertRange(arrColumns.Count, arrColumnAtestation);
                }

                if (arrColumnMilitaryRangs != null && this.MilitaryRangsChecked)
                {
                    arrColumns.InsertRange(arrColumns.Count, arrColumnMilitaryRangs);
                }

                if (!this.AssignmentChecked && !this.PersonalChecked && !this.PenaltyChecked && !this.AbsenceChecked && !this.FiredChecked && !this.AtestationChecked && !this.MilitaryRangsChecked)
                { // if no chekbox or text is selected - when there is no criteria
                    dt1 = dAction.OneJoin(arrColumns, join_clause, where_clause, IsFiredd);
                }
                else
                {
                    string language = "";
                    if (checkedComboLanguage.combobox.Enabled)
                    {
                        language = checkedComboLanguage.combobox.SelectedItem.ToString();
                    }
                    dt1 = dAction.OneJoin(arrColumns, join_clause, where_clause, IsFiredd);
                    if (dt1 == null)
                        return;
                }
                if (this.IsRunFromKartoteka)
                {
                    if (!this.checkBoxExportToExcel.Checked)
                    {
                        this.main.formKartoteka.GridDataSource = dt1;
                        this.main.formKartoteka.FilterColumns = arrColumns;
                        this.Hide();
                    }
                    else
                    {
                        // Tuka trqbwa da se wika eksporta to excel

                        if (arrColumns.Count != 0)
                        {
                            Ex.ExtractCustom(this.main, dt1, arrColumns);
                        }
                        else
                        {
                            MessageBox.Show("Не сте избрали критерии за търсене");
                        }
                    }
                }
                else
                {
                    if (!this.checkBoxExportToExcel.Checked)
                    {
                        KartotekaLichenSystaw kartoteka = new KartotekaLichenSystaw(this.main, dt1, "Списък на всички служители според съответните критерии в справката", false);
                        kartoteka.FilterColumns = arrColumns;
                        kartoteka.ShowDialog();
                    }
                    else
                    {
                        // Tuka trqbwa da se wika eksporta to excel
                        //Ex = new ExcelExpo();					

                        if (arrColumns.Count != 0)
                        {
                            Ex.ExtractCustom(this.main, dt1, arrColumns);
                            //Ex = null;
                            GC.Collect();
                        }
                        else
                        {
                            MessageBox.Show("Не сте избрали критерии за търсене");
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void buttonExit_Click(object sender, System.EventArgs e)
        {
            this.Hide();
        }

        private void checkBoxAssignedAt_CheckedChanged(object sender, System.EventArgs e)
        {
            this.dateTimePickerAssignedAt1.Enabled = this.checkBoxAssignedAt.Checked;
            this.dateTimePickerAssignedAt2.Enabled = this.checkBoxAssignedAt.Checked;
        }

        private void checkBoxContractExpiry_CheckedChanged(object sender, System.EventArgs e)
        {
            this.dateTimePickerContractExpiry1.Enabled = this.checkBoxContractExpiry.Checked;
            this.dateTimePickerContractExpiry2.Enabled = this.checkBoxContractExpiry.Checked;
        }

        private void checkBoxTestContractExpiraty_CheckedChanged(object sender, System.EventArgs e)
        {
            this.dateTimePickerTestContractExpiry1.Enabled = this.checkBoxTestContractExpiraty.Checked;
            this.dateTimePickerTestContractExpiry2.Enabled = this.checkBoxTestContractExpiraty.Checked;
        }

        private void checkBoxPayment_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.checkBoxPayment.Checked)
            {
                this.numBoxPaymentFrom.Enabled = true;
                this.numBoxPaymentTo.Enabled = true;
            }
            else
            {
                this.numBoxPaymentFrom.Enabled = false;
                this.numBoxPaymentTo.Enabled = false;
            }
        }

        private void checkBoxExp_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.checkBoxExp.Checked)
            {
                this.numBoxExpTo.Enabled = true;
                this.numBoxExpFrom.Enabled = true;
                //this.checkBoxExpYear.Enabled =true;
            }
            else
            {
                this.numBoxExpTo.Enabled = false;
                this.numBoxExpFrom.Enabled = false;
                //this.checkBoxExpYear.Enabled =false;
            }
        }

        private void combobox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.checkedComboDirection.combobox.Items.Clear();
                this.checkedComboDirection.combobox.Text = "";
                this.checkedComboDirection.combobox.Items.Add("");
                this.checkedComboDepartment.combobox.Items.Clear();
                this.checkedComboDepartment.combobox.Text = "";
                this.checkedComboDepartment.combobox.Items.Add("");
                this.checkedComboSector.combobox.Items.Clear();
                this.checkedComboSector.combobox.Text = "";
                this.checkedComboSector.combobox.Items.Add("");
                //this.checkedComboProfession.combobox.Items.Clear();
                //this.checkedComboProfession.combobox.Text = "";
                //this.checkedComboProfession.combobox.Items.Add("");

                this.arrDirectionNum.Clear();
                this.arrSector.Clear();
                this.arrDepartment.Clear();
                if (this.checkedComboAdministration.combobox.SelectedIndex > 0)
                {
                    string cond = "par = " + this.vueAdministration[this.checkedComboAdministration.combobox.SelectedIndex - 1]["id"].ToString();
                    this.administration = int.Parse(this.vueAdministration[this.checkedComboAdministration.combobox.SelectedIndex - 1]["id"].ToString());

                    vueDirection = new DataView(dtTree, cond, "level", dvrs);

                    for (int i = 0; i < this.vueDirection.Count; i++)
                    {
                        this.arrDirectionNum.Add(vueDirection[i]["id"]);
                        cond = "par = " + vueDirection[i]["id"].ToString();
                        vueDepartment = new DataView(dtTree, cond, "level", dvrs);
                        for (int j = 0; j < this.vueDepartment.Count; j++)
                        {
                            this.arrDepartment.Add(vueDepartment[j]["id"]);
                            cond = "par = " + vueDepartment[j]["id"].ToString();
                            vueSector = new DataView(dtTree, cond, "level", dvrs);
                            for (int z = 0; z < this.vueSector.Count; z++)
                            {
                                this.arrSector.Add(vueSector[z]["id"]);
                            }
                        }
                        this.checkedComboDirection.combobox.Items.Add(vueDirection[i]["level"]);
                    }

                    vuePosition = new DataView(dtPosition, cond, "id", dvrs);
                    //				for(int i = 0; i < this.vuePosition.Count; i++)
                    //				{
                    //					this.checkedComboProfession.combobox.Items.Add(vuePosition[i]["nameOfPosition"]);
                    //				}
                    this.nodeID = (int)this.vueAdministration[this.checkedComboAdministration.combobox.SelectedIndex - 1]["id"];
                }
                else
                {
                    this.nodeID = 0;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void combobox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.checkedComboDepartment.combobox.Items.Clear();
                this.checkedComboDepartment.combobox.Text = "";
                this.checkedComboDepartment.combobox.Items.Add("");
                this.checkedComboSector.combobox.Items.Clear();
                this.checkedComboSector.combobox.Text = "";
                this.checkedComboSector.combobox.Items.Add("");
                //this.checkedComboProfession.combobox.Items.Clear();
                //this.checkedComboProfession.combobox.Text = "";
                //this.checkedComboProfession.combobox.Items.Add("");

                this.arrDirectionNum.Clear();
                this.arrDepartment.Clear();
                this.arrSector.Clear();

                if (this.checkedComboDirection.combobox.SelectedIndex > 0)
                {
                    string cond = "par = " + this.vueDirection[this.checkedComboDirection.combobox.SelectedIndex - 1]["id"].ToString();
                    this.arrDirectionNum.Add(this.vueDirection[this.checkedComboDirection.combobox.SelectedIndex - 1]["id"]);
                    vueDepartment = new DataView(dtTree, cond, "level", dvrs);

                    for (int i = 0; i < this.vueDepartment.Count; i++)
                    {
                        this.arrDepartment.Add(vueDepartment[i]["id"]);
                        cond = "par = " + vueDepartment[i]["id"].ToString();
                        vueSector = new DataView(dtTree, cond, "level", dvrs);
                        for (int z = 0; z < this.vueSector.Count; z++)
                        {
                            this.arrSector.Add(vueSector[z]["id"]);
                        }
                        this.checkedComboDepartment.combobox.Items.Add(vueDepartment[i]["level"]);
                    }

                    vuePosition = new DataView(dtPosition, cond, "id", dvrs);
                    //				for(int i = 0; i < this.vuePosition.Count; i++)
                    //				{
                    //					this.checkedComboProfession.combobox.Items.Add(vuePosition[i]["nameOfPosition"]);
                    //				}
                    this.nodeID = (int)this.vueDirection[this.checkedComboDirection.combobox.SelectedIndex - 1]["id"];
                }
                else
                {
                    this.nodeID = 0;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void combobox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.checkedComboSector.combobox.Items.Clear();
                this.checkedComboSector.combobox.Text = "";
                this.checkedComboSector.combobox.Items.Add("");
                //this.checkedComboProfession.combobox.Items.Clear();
                //this.checkedComboProfession.combobox.Text = "";
                //this.checkedComboProfession.combobox.Items.Add("");
                this.arrDepartment.Clear();
                this.arrSector.Clear();

                if (this.checkedComboDepartment.combobox.SelectedIndex > 0)
                {
                    string cond = "par = " + this.vueDepartment[this.checkedComboDepartment.combobox.SelectedIndex - 1]["id"].ToString();
                    this.arrDepartment.Add(this.vueDepartment[this.checkedComboDepartment.combobox.SelectedIndex - 1]["id"]);
                    vueSector = new DataView(dtTree, cond, "level", dvrs);

                    for (int i = 0; i < this.vueSector.Count; i++)
                    {
                        this.arrSector.Add(vueSector[i]["id"]);
                        this.checkedComboSector.combobox.Items.Add(vueSector[i]["level"]);
                    }

                    vuePosition = new DataView(dtPosition, cond, "id", dvrs);
                    //				for(int i = 0; i < this.vuePosition.Count; i++)
                    //				{
                    //					this.checkedComboProfession.combobox.Items.Add(vuePosition[i]["nameOfPosition"]);
                    //				}
                    this.nodeID = (int)this.vueDepartment[this.checkedComboDepartment.combobox.SelectedIndex - 1]["id"];
                }
                else if (this.checkedComboDirection.combobox.SelectedIndex > 0)
                {
                    this.nodeID = (int)this.vueDirection[this.checkedComboDirection.combobox.SelectedIndex - 1]["id"];
                }
                else
                {
                    this.nodeID = 0;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void combobox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //this.checkedComboProfession.combobox.Items.Clear();
                //this.checkedComboProfession.combobox.Text = "";
                //this.checkedComboProfession.combobox.Items.Add("");
                this.arrSector.Clear();

                if (this.checkedComboSector.combobox.SelectedIndex > 0)
                {
                    string cond = string.Format("par = {0}", this.vueSector[this.checkedComboSector.combobox.SelectedIndex - 1]["id"].ToString());
                    this.arrSector.Add(this.vueSector[this.checkedComboSector.combobox.SelectedIndex - 1]["id"]);
                    vuePosition = new DataView(dtPosition, cond, "id", dvrs);
                    //				for(int i = 0; i < vuePosition.Count; i++)
                    //				{
                    //					this.checkedComboProfession.combobox.Items.Add(vuePosition[i]["nameOfPosition"]);
                    //				}
                    this.nodeID = (int)this.vueSector[this.checkedComboSector.combobox.SelectedIndex - 1]["id"];
                }
                else if (this.checkedComboDepartment.combobox.SelectedIndex > 0)
                {
                    this.nodeID = (int)this.vueDepartment[this.checkedComboDepartment.combobox.SelectedIndex - 1]["id"];
                }
                else if (this.checkedComboDirection.combobox.SelectedIndex > 0)
                {
                    this.nodeID = (int)this.vueDirection[this.checkedComboDirection.combobox.SelectedIndex - 1]["id"];
                }
                else
                {
                    this.nodeID = 0;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }

        private void comboboxProfession_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //			if(this.checkedComboProfession.combobox.SelectedIndex > 0)
                //			{
                //				this.positionID = int.Parse( vuePosition[this.checkedComboProfession.combobox.SelectedIndex -1]["id"].ToString());
                //			}
                //			else
                //				this.positionID = 0;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }

        }

        private void checkBoxFrom_CheckedChanged(object sender, System.EventArgs e)
        {
            this.dateTimePickerFrom1.Enabled = checkBoxFrom.Checked;
            this.dateTimePickerFrom2.Enabled = checkBoxFrom.Checked;
        }

        private void checkBoxAge_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.checkBoxAge.Checked)
            {
                this.numBoxOlder.Enabled = true;
                this.numBoxYounger.Enabled = true;
            }
            else
            {
                this.numBoxOlder.Enabled = false;
                this.numBoxYounger.Enabled = false;

            }
        }

        private void checkBoxPenaltyDate_CheckedChanged(object sender, System.EventArgs e)
        {
            this.dateTimePickerPenaltyDate1.Enabled = this.checkBoxPenaltyDate.Checked;
            this.dateTimePickerPenaltyDate2.Enabled = this.checkBoxPenaltyDate.Checked;
        }

        private void checkBoxFormDate_CheckedChanged(object sender, System.EventArgs e)
        {
            this.dateTimePickerFormDate1.Enabled = this.checkBoxFormDate.Checked;
            this.dateTimePickerFormDate2.Enabled = this.checkBoxFormDate.Checked;
        }

        private void populatePositionView()
        {
            this.vuePosition.Table.TableName = "Positions";
            FormChoose form = new FormChoose(vuePosition, "длъжност");
            form.ShowDialog();
            if (form.dataGridView1.CurrentRow != null)
                return;
            try
            {
                if (form.DialogResult == DialogResult.OK)
                {
                    NKPCode = form.dataGridView1.CurrentRow.Cells["NKPCode"].Value.ToString();
                    EKDACode = form.dataGridView1.CurrentRow.Cells["EKDACode"].Value.ToString();

                    //this.checkedComboProfessionn.SelectedIndex = this.checkedComboProfessionn.fi form.dataGridView1.CurrentRowIndex;
                }
            }
            catch
            {
                NKPCode = "''";
                EKDACode = "''";
            }
        }

        private void buttonSelectPosition_Click(object sender, System.EventArgs e)
        {
            if (this.checkedComboProfessionn.Checked)
            {
                if (this.checkedComboAdministration.Checked && false)
                {
                    if (this.checkedComboAdministration.SelectedIndex > 0)
                    {
                        DataTable dtPosition = this.da.SelectWhere(TableNames.FirmPersonal3, "*", " ORDER BY id");
                        if (dtPosition == null)
                        {
                            MessageBox.Show("Грешка при зареждане на данни за структурата на организацията", ErrorMessages.NoConnection);
                            this.Close();
                        }
                        if (this.checkedComboDirection.Checked && this.checkedComboDirection.SelectedIndex > 0)
                        {
                            if (checkedComboDepartment.Checked && this.checkedComboDepartment.SelectedIndex > 0)
                            {
                                if (checkedComboSector.Checked && this.checkedComboSector.SelectedIndex > 0)
                                {
                                }
                                else
                                {
                                }
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                            string cond = "par = " + this.vueAdministration[this.checkedComboAdministration.SelectedIndex - 1]["id"].ToString();
                            vuePosition = new DataView(dtPosition, cond, "id", DataViewRowState.CurrentRows);
                            populatePositionView();
                        }
                    }
                }
                else
                {
                    DataTable dtPosition = this.da.SelectWhere(TableNames.GlobalPositions, "*", " ORDER BY id");
                    if (dtPosition == null)
                    {
                        MessageBox.Show("Грешка при зареждане на данни за длъжностите в организацията", ErrorMessages.NoConnection);
                        this.Close();
                    }
                    int index = checkedComboProfessionn.combobox.SelectedIndex;
                    if (index > -1)
                    {
                        //string cond = "nameOfPosition = '" + checkedComboProfessionn.combobox.Text + "'";
                        string cond = "1 = 1";
                        vuePosition = new DataView(dtPosition, cond, "id", dvrs);
                    }
                    else
                    {
                        vuePosition = new DataView(dtPosition, "", "id", dvrs);
                    }

                    if (vuePosition != null)
                    {
                        vuePosition.Table.TableName = "GlobalPositions";
                        FormChoose form = new FormChoose(vuePosition, "длъжност");
                        form.ShowDialog();
                        if (form.dataGridView1.CurrentRow == null)
                            return;
                        try
                        {
                            if (form.DialogResult == DialogResult.OK)
                            {
                                NKPCode = form.dataGridView1.CurrentRow.Cells["NKPCode"].Value.ToString();
                                EKDACode = form.dataGridView1.CurrentRow.Cells["EKDACode"].ToString();
                                int ix = this.checkedComboProfessionn.combobox.FindStringExact(form.dataGridView1.CurrentRow.Cells["positionname"].Value.ToString());
                                if (ix > 0)
                                {
                                    this.checkedComboProfessionn.SelectedIndex = ix;
                                }
                            }
                        }
                        catch
                        {
                            NKPCode = "''";
                            EKDACode = "''";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не сте избрали звено от организацията");
                        NKPCode = "''";
                        EKDACode = "''";
                    }
                }
            }
        }

        private void combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            NKPCode = "''";
            EKDACode = "''";
        }

        private void checkBoxFiredFrom_CheckedChanged(object sender, System.EventArgs e)
        {
            dateTimePickerFiredFromDate.Enabled = this.checkBoxFiredFrom.Checked;
            dateTimePickerFiredТоDate.Enabled = this.checkBoxFiredFrom.Checked;
        }

        private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
        {

            if (this.tabControl1.SelectedTab == this.tabPageFired && !this.IsFiredd)
            {
                this.labelFiredMessage.Visible = true;
            }
            else
            {
                this.labelFiredMessage.Visible = false;
            }
        }

        private void checkBoxAtestationRating_CheckedChanged(object sender, System.EventArgs e)
        {
            numericUpDownAtestationGrade.Enabled = checkBoxAtestationRating.Checked;
        }

        private void checkBoxMilitaryRangFrom_CheckedChanged(object sender, EventArgs e)
        {
            this.dateTimePickerMilitaryRangFrom.Enabled = this.checkBoxMilitaryRangFrom.Checked;
            this.dateTimePickerMilitaryRangTo.Enabled = this.checkBoxMilitaryRangFrom.Checked;
        }

        private void checkBoxMilitaryRangOrderFrom_CheckedChanged(object sender, EventArgs e)
        {
            this.dateTimePickerMilitaryRangOrderFrom.Enabled = this.checkBoxMilitaryRangOrderFrom.Checked;
            this.dateTimePickerMilitaryRangOrderTo.Enabled = this.checkBoxMilitaryRangOrderFrom.Checked;
        }

        private void checkBoxBirthday_CheckedChanged(object sender, EventArgs e)
        {
            this.numBoxBirthDay.Enabled = this.checkBoxBirthday.Checked;
        }

        private void checkBoxBirthMonth_CheckedChanged(object sender, EventArgs e)
        {
            this.numBoxBirthMonth.Enabled = this.checkBoxBirthMonth.Checked;
        }

        private void checkBoxBirthYear_CheckedChanged(object sender, EventArgs e)
        {
            this.numBoxBirthYear.Enabled = this.checkBoxBirthYear.Checked;
        }

        private void checkBoxBirthPlace_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxBirthPlace.Enabled = this.checkBoxBirthPlace.Checked;
        }

        private void checkBoxLivingPlace_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxLivingPlace.Enabled = this.checkBoxLivingPlace.Checked;
        }

        private void checkBoxRangNumberOrder_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxRangNumberOrder.Enabled = this.checkBoxRangNumberOrder.Checked;
        }

        private void checkBoxName_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxName.Enabled = this.checkBoxName.Checked;
        }

        private void checkBoxSurName_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxSurName.Enabled = this.checkBoxSurName.Checked;
        }

        private void checkBoxFamily_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxFamily.Enabled = this.checkBoxFamily.Checked;
        }

        private void checkBoxIDCardExpiry_CheckedChanged(object sender, EventArgs e)
        {
            this.dateTimePickerIDCardExpiresTo.Enabled = this.checkBoxIDCardExpiry.Checked;
        }
    }
}
