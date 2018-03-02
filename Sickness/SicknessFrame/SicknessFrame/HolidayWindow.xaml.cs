using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;

using System.Data;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Data.Objects;
using ExcelExport;

namespace SicknessFrame
{	
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
	public partial class MainWindow : Window
	{
		private string connectionString;
		List<HR_absence> lstUpdatedItems;
		List<HR_absence> lstNewItems;
		DataClasses1DataContext context;
		List<HR_absence> lstAbsence;
		List<HR_person> lstPerson;
		List<HR_person> lstSysco;
		List<CustomAbsence> lstCustomAbsence;
		List<string> lstDuration;
		
        public MainWindow(string connstring)
        {
			//string address = @"192.168.0.39";
			//string database = "syscodb";

			////string address = @"81.161.245.39\sqlexpress2005";
			////string database = "syscodb1";
			
			//string user = "root";
			//string password = "tessla";
			
            InitializeComponent();
			//this.connectionString = String.Format("Data Source={0};Initial Catalog= {1};uid={2};Password={3};", address, database, user, password);
			this.connectionString = connstring;

			this.lstUpdatedItems = new List<HR_absence>();
			this.lstNewItems = new List<HR_absence>();
			this.lstCustomAbsence = new List<CustomAbsence>();
			this.lstDuration = new List<string>();
			
        }

		private HR_absence FillAbsenceRow(CustomAbsence ab)
		{
			HR_absence nab = new HR_absence();
			nab.AdditionalDocs = ab.AdditionalDocs;
			nab.Attachment7 = ab.Attachment7;
			nab.CalendarDays = ab.CalendarDays;
			nab.countDays = ab.countDays;
			nab.Declaration39 = ab.Declaration39;
			nab.fromDate = ab.fromDate;
			nab.issuedatdate = ab.issuedatdate;
			nab.MKB = ab.MKB;
			nab.modifiedByUser = ab.modifiedByUser;
			nab.NAPDocs = ab.NAPDocs;
			nab.numberOrder = ab.numberOrder;
			nab.orderFromDate = ab.orderFromDate;
			nab.parent = ab.parent;
			nab.reason = ab.reason;
			nab.reasons = ab.reasons;
			nab.sicknessduration = ab.sicknessduration;
			nab.SicknessNumber = ab.SicknessNumber;
			nab.toDate = ab.toDate;
			nab.typeAbsence = ab.typeAbsence;
			nab.Year = ab.Year;
			return nab;
		}

		private void UpdateAbsenceRow(CustomAbsence ab, HR_absence nab)
		{			
			nab.AdditionalDocs = ab.AdditionalDocs;
			nab.Attachment7 = ab.Attachment7;
			nab.CalendarDays = ab.CalendarDays;
			nab.countDays = ab.countDays;
			nab.Declaration39 = ab.Declaration39;
			nab.fromDate = ab.fromDate;
			nab.issuedatdate = ab.issuedatdate;
			nab.MKB = ab.MKB;
			nab.modifiedByUser = ab.modifiedByUser;
			nab.NAPDocs = ab.NAPDocs;
			nab.numberOrder = ab.numberOrder;
			nab.orderFromDate = ab.orderFromDate;
			nab.parent = ab.parent;
			nab.reason = ab.reason;
			nab.reasons = ab.reasons;
			nab.sicknessduration = ab.sicknessduration;
			nab.SicknessNumber = ab.SicknessNumber;
			nab.toDate = ab.toDate;
			nab.typeAbsence = ab.typeAbsence;
			nab.Year = ab.Year;			
		}

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
			try
			{
				this.lstAbsence = (from absence in this.context.HR_absences
								   where ((absence.Year == "2011") && (absence.typeAbsence == "Болнични"))
								   select absence).ToList();
				foreach (CustomAbsence ab in this.dgAbsence.ItemsSource)
				{
					if (ab.isNew)
					{
						this.context.HR_absences.InsertOnSubmit(this.FillAbsenceRow(ab));
					}
					else if (ab.isUpdated)
					{
						HR_absence up = this.lstAbsence.Find(
							delegate(HR_absence abs)
							{
								return abs.id == ab.id;
							});
						if (up != null)
						{
							this.UpdateAbsenceRow(ab, up);
						}
					}
				}
				this.context.SubmitChanges();
				foreach (CustomAbsence ab in this.dgAbsence.ItemsSource)
				{
					ab.isNew = false;
					ab.isUpdated = false;
				}
				this.dgAbsence.Items.Refresh();
			}
			catch (InvalidOperationException)
			{
				MessageBox.Show("Не може да бъде направен запис, докато има грешки при редактиране на данните");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
			this.InitialiseGrid();			
        }

		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				CustomAbsence row = new CustomAbsence();

				row.Year = this.dpYear.SelectedDate.Value.Year.ToString();
				row.typeAbsence = "Болнични";
				row.fromDate = DateTime.Now;
				row.toDate = DateTime.Now;
				row.orderFromDate = DateTime.Now;
				row.issuedatdate = DateTime.Now;
				row.isNew = true;				
				row.sicknessduration = this.lstDuration[0];
				this.lstCustomAbsence.Add(row);
				this.dgAbsence.ItemsSource = null;
				this.dgAbsence.Items.Clear();
				this.dgAbsence.ItemsSource = this.lstCustomAbsence;
				this.dgAbsence.Items.Refresh();			
				this.dgAbsence.SelectedIndex = this.dgAbsence.Items.Count - 1;
				this.dgAbsence.ScrollIntoView(row);

				//dgcmbIdSysco.IsReadOnly = false;
				dgcmbName.IsReadOnly = false;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
            this.dpYear.SelectedDate = DateTime.Now;
			if(InitialiseGrid() == false)
				this.Close();
		}     
		
		private bool InitialiseGrid()
		{
			try
			{				
				this.context = new DataClasses1DataContext(this.connectionString);
				this.lstAbsence = new List<HR_absence>();
				//HR_absence ab;
				
				this.lstCustomAbsence = (from absence in this.context.HR_absences
										 join per in this.context.HR_persons on absence.parent equals per.id
										 where ((absence.typeAbsence == "Болнични") && (absence.Year == this.dpYear.SelectedDate.Value.Year.ToString()))
										 orderby absence.numberOrder
										 select new CustomAbsence
										 {
											 AdditionalDocs = absence.AdditionalDocs,
											 Attachment7 = absence.Attachment7,
											 CalendarDays = (int)absence.CalendarDays,
											 countDays = (int)absence.countDays,
											 Declaration39 = absence.Declaration39,
											 fromDate = (DateTime)absence.fromDate,
											 id = absence.id,
											 id_sysco = per.id_sysco,
											 isNew = false,
											 issuedatdate = (DateTime)absence.issuedatdate,
											 isUpdated = false,
											 MKB = absence.MKB,
											 name = per.name,
											 NAPDocs = absence.NAPDocs,
											 numberOrder = absence.numberOrder,
											 orderFromDate = (DateTime)absence.orderFromDate,
											 parent = absence.parent,
											 reason = absence.reason,
											 reasons = absence.reasons,
											 sicknessduration = absence.sicknessduration,
											 SicknessNumber = absence.SicknessNumber,
											 toDate = (DateTime)absence.toDate,
											 typeAbsence = absence.typeAbsence,
											 Year = absence.Year
										 }).ToList();
				
				this.lstPerson = (from person in this.context.HR_persons
								  orderby person.name
								  select person).ToList();

				this.lstSysco = (from person in this.context.HR_persons
								  select person).ToList();

				this.lstDuration.Add("първ.");
				this.lstDuration.Add("прод.");

				dgcmbIdSysco.ItemsSource = lstPerson;
				dgcmbName.ItemsSource = lstPerson;
				dgtbDuration.ItemsSource = lstDuration;

				this.dgAbsence.ItemsSource = this.lstCustomAbsence;

				//foreach (var cab in lstCustomAbsence)
				//{
				//    SicknessControl scItem = new SicknessControl(cab, lstPerson);
				//    listBoxSickness.Items.Add(scItem);
				//}
				
				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		private void dgAbsence_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
		{
			if ((this.lstNewItems.Contains(this.dgAbsence.SelectedItem) == false) && (this.lstUpdatedItems.Contains(this.dgAbsence.SelectedItem) == false))
			{
				CustomAbsence ab;
				ab = (CustomAbsence)(this.dgAbsence.SelectedItem);
				ab.isUpdated = true;				
			}
			if ((e.Column == dgtbFromDate) || (e.Column == dgtbToDate))
			{
				CustomAbsence ab = (CustomAbsence)e.Row.Item;

				if (ab != null && ab.fromDate != null && ab.toDate != null)
				{
					TimeSpan span = ab.toDate.Value.Subtract(ab.fromDate.Value);
					ab.CalendarDays = span.Days;
					if (ab.CalendarDays == 0)
					{
						ab.CalendarDays = 1;
					}
				}
			}
            if(e.Column == dgtbSicknessNumber)
            {
                List<CustomAbsence> cc = (List<CustomAbsence>)dgAbsence.ItemsSource;
                CustomAbsence ab = (CustomAbsence)e.Row.Item;
                var found = cc.FindAll(absn => absn.SicknessNumber == ab.SicknessNumber);
                if (found.Count > 1)
                {
                    MessageBox.Show("Този номер на болничен е вече въведен в болнични номер " + found[0].numberOrder);
                }
    		}
		}

		private void dgAbsence_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			CustomAbsence ab = (CustomAbsence) this.dgAbsence.SelectedItem;
			if (ab != null)
			{
				if (ab.isNew)
				{
					//dgcmbIdSysco.IsReadOnly = false;
					dgcmbName.IsReadOnly = false;
				}
				else
				{
					//dgcmbIdSysco.IsReadOnly = true;
					dgcmbName.IsReadOnly = true;
				}
			}
		}

		private void dgAbsence_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
		{
			e.Row.Background = Brushes.LightPink;
		}

		private void dgAbsence_LoadingRow(object sender, DataGridRowEventArgs e)
		{
			CustomAbsence ab = (CustomAbsence)e.Row.Item;
			if (ab.isNew || ab.isUpdated)
			{
				e.Row.Background = Brushes.LightPink;
			}
		}

		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			try
			{				
				if ((this.dgAbsence.SelectedItem != null))
				{
					if (MessageBox.Show("Наистина ли желаете да изтриете избраното отсъствие?", "Въпрос?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
					{
						CustomAbsence ab = (CustomAbsence)this.dgAbsence.SelectedItem;
						this.lstAbsence = (from absence in this.context.HR_absences
										   where ((absence.Year == "2011") && (absence.typeAbsence == "Болнични"))
										   select absence).ToList();
						HR_absence del = this.lstAbsence.Find(
							delegate(HR_absence abs)
							{
								return abs.id == ab.id;
							});
						if (del != null)
						{
							this.context.HR_absences.DeleteOnSubmit(del);
							this.lstAbsence.Remove((HR_absence)this.dgAbsence.SelectedItem);
							this.lstCustomAbsence.Remove(ab);
							this.dgAbsence.Items.Refresh();
						}
					}
				}
				else if (lstNewItems.Contains((HR_absence)this.dgAbsence.SelectedItem))
				{
					lstNewItems.Remove((HR_absence)this.dgAbsence.SelectedItem);
					lstAbsence.Remove((HR_absence)this.dgAbsence.SelectedItem);
					this.dgAbsence.Items.Refresh();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void dgAbsence_Sorting(object sender, DataGridSortingEventArgs e)
		{

		}

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {

            ExcelExport.ExcelExport ex = new ExcelExport.ExcelExport();
            List <DataTable> lstData = new List<DataTable>();

            var lstAbsences = from ab in this.context.HR_absences
                              join p in this.context.HR_persons on ab.parent equals p.id
                              where ab.typeAbsence == "Болнични"
                              orderby ab.numberOrder
                              select new {
                                  numberOrder = ab.numberOrder,
                                  orderFromDate = ab.orderFromDate,
                                  SicknessNumber = ab.SicknessNumber,
                                  SyscoID = p.id_sysco,
                                  Name = p.name,
                                  FromDate = ab.fromDate,
                                  ToDate = ab.toDate,
                                  Att7 = ab.Attachment7,
                                  Dec39 = ab.Declaration39,
                                  Supr = ab.AdditionalDocs,
                                  IssuedAt = ab.issuedatdate,
                                  Cont = ab.sicknessduration,
                                  WorkDays = ab.countDays,
                                  CalDays = ab.CalendarDays,
                                  MKB = ab.MKB,
                                  Reasons = ab.reasons,
                                  NAP = ab.NAPDocs,
                                  Notes = ab.reason
                              };

            DataTable dt = ExcelExport.ToDataTableList.LINQToDataTable(lstAbsences);
            DataTable dtd = new DataTable();

            foreach (DataColumn col in dt.Columns)
            {
                DataColumn c;
                if(col.DataType.Name == "DateTime")
                {
                    c = new DataColumn(col.ColumnName, "".GetType());
                }
                else
                {
                    c = new DataColumn(col.ColumnName, col.DataType);    
                }
                
                dtd.Columns.Add(c);
            }

            foreach (DataRow r in dt.Rows)
            {
                DataRow row = dtd.NewRow();
                for (int i = 0; i < dt.Columns.Count; i++ )
                {
                    if(dt.Columns[i].DataType.Name == "DateTime")
                    {
                        DateTime test;
                        if(DateTime.TryParse(r[i].ToString(), out test))
                        {
                            row[i] = string.Format("{0:00}.{1:00}.{2}", test.Day, test.Month, test.Year );
                        }
                    }
                    else
                    {
                        row[i] = r[i];
                    }
                }
                dtd.Rows.Add(row);
            }
            
            DataTable dtPrint = new DataTable();

            dtPrint.Columns.Add("Номер на заповед", "".GetType());
            dtPrint.Columns.Add("Дата", "".GetType());
            dtPrint.Columns.Add("№ б.лист", "".GetType());
            dtPrint.Columns.Add("SyscoID", "".GetType());
            dtPrint.Columns.Add("Име", "".GetType());
            dtPrint.Columns.Add("От дата", "".GetType());
            dtPrint.Columns.Add("До дата", "".GetType());
            dtPrint.Columns.Add("Прил. 7", "".GetType());
            dtPrint.Columns.Add("Дек. 39", "".GetType());
            dtPrint.Columns.Add("Съпр.док.", "".GetType());
            dtPrint.Columns.Add("Издаден на", "".GetType());
            dtPrint.Columns.Add("Прод.", "".GetType());
            dtPrint.Columns.Add("Раб. дни", "".GetType());
            dtPrint.Columns.Add("Кал. дни", "".GetType());
            dtPrint.Columns.Add("МКБ", "".GetType());
            dtPrint.Columns.Add("Причини", "".GetType());
            dtPrint.Columns.Add("НАП", "".GetType());
            dtPrint.Columns.Add("Забележки", "".GetType());
            foreach (DataRow row in dtd.Rows)
            {
                DataRow pr = dtPrint.NewRow();
                pr["Номер на заповед"] = row["numberOrder"];
                pr["Дата"] = row["orderFromDate"];
                pr["№ б.лист"] = row["SicknessNumber"];
                pr["SyscoID"] = row["SyscoID"];
                pr["Име"] = row["Name"];
                pr["От дата"] = row["FromDate"];
                pr["До дата"] = row["ToDate"];
                pr["Прил. 7"] = row["Att7"];
                pr["Дек. 39"] = row["Dec39"];
                pr["Съпр.док."] = row["Supr"];
                pr["Издаден на"] = row["IssuedAt"];
                pr["Прод."] = row["Cont"];
                pr["Раб. дни"] = row["WorkDays"];
                pr["Кал. дни"] = row["CalDays"];
                pr["МКБ"] = row["MKB"];
                pr["Причини"] = row["Reasons"];
                pr["НАП"] = row["NAP"];
                pr["Забележки"] = row["Notes"];
                dtPrint.Rows.Add(pr);
            }
            lstData.Add(dtPrint);
            ex.Export(lstData);
        }

        private void dpYear_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            InitialiseGrid();
        }
	}
}
