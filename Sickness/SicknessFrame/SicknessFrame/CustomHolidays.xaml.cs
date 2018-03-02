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
using HRDataLayer;



namespace SicknessFrame
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class CustomHolidays : Window
	{
		private string connectionString;
		List<HR_Absence> lstUpdatedItems;
		List<HR_Absence> lstNewItems;
		Entities context;
		List<HR_Absence> lstAbsence;
		List<HR_Person> lstPerson;
		//List<person> lstSysco;
		List<CustomHolidaysModel> lstCustomAbsence;
		//List<string> lstDuration;

		public CustomHolidays(string connstring)
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

			this.lstUpdatedItems = new List<HR_Absence>();
			this.lstNewItems = new List<HR_Absence>();
			this.lstCustomAbsence = new List<CustomHolidaysModel>();
		}

		private HR_Absence FillAbsenceRow(CustomHolidaysModel ab)
		{
			HR_Absence nab = new HR_Absence();
			//nab.AdditionalDocs = ab.AdditionalDocs;
			//nab.Attachment7 = ab.Attachment7;
			nab.CalendarDays = ab.CalendarDays;
			nab.countDays = ab.countDays;
			//nab.Declaration39 = ab.Declaration39;
			nab.fromDate = ab.fromDate;
			nab.issuedatdate = ab.issuedatdate;
			//nab.MKB = ab.MKB;
			nab.modifiedByUser = ab.modifiedByUser;
			//nab.NAPDocs = ab.NAPDocs;
			nab.numberOrder = ab.numberOrder;
			nab.orderFromDate = ab.orderFromDate;
			nab.parent = ab.parent;
			nab.reason = ab.reason;
			nab.reasons = ab.reasons;
			//nab.sicknessduration = ab.sicknessduration;
			//nab.sicknessnumber = ab.sicknessnumber;
			nab.toDate = ab.toDate;
			nab.typeAbsence = ab.typeAbsence;
			nab.Year = ab.Year;
			return nab;
		}

		private void UpdateAbsenceRow(CustomHolidaysModel ab, HR_Absence nab)
		{
			//nab.AdditionalDocs = ab.AdditionalDocs;
			//nab.Attachment7 = ab.Attachment7;
			nab.CalendarDays = ab.CalendarDays;
			nab.countDays = ab.countDays;
			//nab.Declaration39 = ab.Declaration39;
			nab.fromDate = ab.fromDate;
			nab.issuedatdate = ab.issuedatdate;
			//nab.MKB = ab.MKB;
			nab.modifiedByUser = ab.modifiedByUser;
			//nab.NAPDocs = ab.NAPDocs;
			nab.numberOrder = ab.numberOrder;
			nab.orderFromDate = ab.orderFromDate;
			nab.parent = ab.parent;
			nab.reason = ab.reason;
			nab.reasons = ab.reasons;
			//nab.sicknessduration = ab.sicknessduration;
			//nab.sicknessnumber = ab.sicknessnumber;
			nab.toDate = ab.toDate;
			nab.typeAbsence = ab.typeAbsence;
			nab.Year = ab.Year;
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var date = DateTime.Now.Year.ToString();
				this.lstAbsence = (from absence in this.context.HR_Absence
								   where ((absence.Year == date) && (absence.typeAbsence == "Болнични"))
								   select absence).ToList();

				foreach (CustomHolidaysModel ab in this.dgAbsence.Items)
				{
					if (ab.isNew)
					{
						this.context.HR_Absence.AddObject(this.FillAbsenceRow(ab));
					}
					else if (ab.isUpdated)
					{
						HR_Absence up = this.lstAbsence.Find(abs => abs.id == ab.id);
						if (up != null)
						{
							this.UpdateAbsenceRow(ab, up);
						}
					}
				}
				this.context.SaveChanges();
				foreach (CustomAbsenceModel ab in this.dgAbsence.Items)
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
				CustomHolidaysModel row = new CustomHolidaysModel();

				row.typeAbsence = "Полагаем годишен отпуск";
				row.fromDate = DateTime.Now;
				row.toDate = DateTime.Now;
				row.orderFromDate = DateTime.Now;
				row.issuedatdate = DateTime.Now;
				row.isNew = true;
				//row.sicknessduration = "първ.";
				this.lstCustomAbsence.Add(row);
				this.dgAbsence.ItemsSource = null;
				this.dgAbsence.Items.Clear();
				this.dgAbsence.ItemsSource = this.lstCustomAbsence;
				this.dgAbsence.Items.Refresh();
				
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
			
			//this.lstDuration.Clear();
			//this.lstDuration.Add("първ.");
			//this.lstDuration.Add("прод.");
			//if(InitialiseGrid() == false)
			//    this.Close();
		}

		private bool InitialiseGrid()
		{
			try
			{
				this.context = new Entities(this.connectionString);
				this.lstAbsence = new List<HR_Absence>();
				//HR_absence ab;
				string year = "2013";

				this.lstCustomAbsence = (from absence in this.context.HR_Absence
										 join per in this.context.HR_Person on absence.parent equals per.id
										 where ((absence.typeAbsence == "Полагаем годишен отпуск") && (absence.Year == year))
										 select new CustomHolidaysModel
										 {
											 //AdditionalDocs = absence.AdditionalDocs,
											 //Attachment7 = absence.Attachment7,
											 CalendarDays = (int)absence.CalendarDays,
											 countDays = (int)absence.countDays,
											 //Declaration39 = absence.Declaration39,
											 fromDate = (DateTime)absence.fromDate,
											 id = absence.id,
											 user_id = per.other2,
											 isNew = false,
											 issuedatdate = (DateTime)absence.issuedatdate,
											 isUpdated = false,
											// MKB = absence.MKB,
											 name = per.name,
											 //NAPDocs = absence.NAPDocs,
											 numberOrder = absence.numberOrder,
											 orderFromDate = (DateTime)absence.orderFromDate,
											 parent = absence.parent,
											 reason = absence.reason,
											 reasons = absence.reasons,
											 //sicknessduration = absence.sicknessduration,
											 //sicknessnumber = absence.sicknessnumber,
											 toDate = (DateTime)absence.toDate,
											 typeAbsence = absence.typeAbsence,
											 Year = absence.Year,
										 }).ToList();

				int no;

				this.lstCustomAbsence.ForEach(p => p.numberOrder = ((int.TryParse(p.numberOrder, out no)) ? string.Format("{0:0000}", no) : "0000"));

				this.lstCustomAbsence = this.lstCustomAbsence.OrderBy(p => p.numberOrder).ToList();

				this.lstPerson = (from person in this.context.HR_Person
								  orderby person.name
								  select person).ToList();

				dgcmbName.ItemsSource = lstPerson;

				this.dgAbsence.ItemsSource = this.lstCustomAbsence;

				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		private static void test()
		{
			string x = string.Format("{0:0000}", 5);
		}

		private void dgAbsence_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
		{
			if ((this.lstNewItems.Contains(this.dgAbsence.SelectedItem) == false) && (this.lstUpdatedItems.Contains(this.dgAbsence.SelectedItem) == false))
			{
				CustomHolidaysModel ab;
				ab = (CustomHolidaysModel)(this.dgAbsence.SelectedItem);
				ab.isUpdated = true;
			}
			else
			{
				CustomHolidaysModel ab = (CustomHolidaysModel) e.Row.Item;

				try
				{
					if (ab != null && ab.fromDate != null && ab.toDate != null)
					{
						DateTime DateStart = ab.fromDate.Date;
						DateTime DateEnd = ab.toDate.Date;

						int workdays = HolidayPlan.CalendarRow.GetCountWorkDays(DateStart, DateEnd, this.connectionString);
						TimeSpan span = DateEnd.Subtract(DateStart);
						int caldays = span.Days + 1;

						ab.CalendarDays = caldays;
						ab.countDays = workdays;
						//TimeSpan span = ab.toDate.Value.Subtract(ab.fromDate.Value);
						//ab.CalendarDays = span.Days + 1;
						//if (ab.CalendarDays == 0)
						//{
						//    ab.CalendarDays = 1;
						//}
					}
				}
				catch (Exception ex)
				{
					//ErrorLog.WriteException(ex, ex.Message);
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void dgAbsence_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			CustomHolidaysModel ab = (CustomHolidaysModel)this.dgAbsence.SelectedItem;
			if (ab != null)
			{
				if (ab.isNew)
				{
					dgcmbName.IsReadOnly = false;
				}
				else
				{
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
			CustomHolidaysModel ab = (CustomHolidaysModel)e.Row.Item;
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
						string year = "2013";
						CustomHolidaysModel ab = (CustomHolidaysModel)this.dgAbsence.SelectedItem;
						this.lstAbsence = (from absence in this.context.HR_Absence
										   where ((absence.Year == year) && (absence.typeAbsence == "Болнични"))
										   select absence).ToList();
						HR_Absence del = this.lstAbsence.Find(
							delegate(HR_Absence abs)
							{
								return abs.id == ab.id;
							});
						if (del != null)
						{
							this.context.HR_Absence.DeleteObject(del);
							this.lstAbsence.Remove((HR_Absence)this.dgAbsence.SelectedItem);
							this.lstCustomAbsence.Remove(ab);
							this.dgAbsence.Items.Refresh();
						}
					}
				}
				else if (lstNewItems.Contains((HR_Absence)this.dgAbsence.SelectedItem))
				{
					lstNewItems.Remove((HR_Absence)this.dgAbsence.SelectedItem);
					lstAbsence.Remove((HR_Absence)this.dgAbsence.SelectedItem);
					this.dgAbsence.Items.Refresh();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnPrint_Click(object sender, RoutedEventArgs e)
		{
			ExcelExport.ExcelExport ex = new ExcelExport.ExcelExport();
			List<DataTable> lstData = new List<DataTable>();

			var lstAbsences = from ab in this.context.HR_Absence
							  join p in this.context.HR_Person on ab.parent equals p.id
							  where ab.typeAbsence == "Болнични"
							  orderby ab.numberOrder
							  select new
							  {
								  numberOrder = ab.numberOrder,
								  orderFromDate = ab.orderFromDate,
								  SyscoID = p.id_sysco,
								  Name = p.name,
								  FromDate = ab.fromDate,
								  ToDate = ab.toDate,
								  IssuedAt = ab.issuedatdate,
								  WorkDays = ab.countDays,
								  CalDays = ab.CalendarDays,
								  Reasons = ab.reasons,
								  Notes = ab.reason
							  };

			DataTable dt = ToDataTableList.LINQToDataTable(lstAbsences);
			DataTable dtd = new DataTable();

			foreach (DataColumn col in dt.Columns)
			{
				DataColumn c;
				if (col.DataType.Name == "DateTime")
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
				for (int i = 0; i < dt.Columns.Count; i++)
				{
					if (dt.Columns[i].DataType.Name == "DateTime")
					{
						DateTime test;
						if (DateTime.TryParse(r[i].ToString(), out test))
						{
							row[i] = string.Format("{0:00}.{1:00}.{2}", test.Day, test.Month, test.Year);
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
			dtPrint.Columns.Add("Номер на дело", "".GetType());
			dtPrint.Columns.Add("Име", "".GetType());
			dtPrint.Columns.Add("От дата", "".GetType());
			dtPrint.Columns.Add("До дата", "".GetType());
			dtPrint.Columns.Add("Издаден на", "".GetType());
			dtPrint.Columns.Add("Раб. дни", "".GetType());
			dtPrint.Columns.Add("Кал. дни", "".GetType());
			dtPrint.Columns.Add("Забележки", "".GetType());
			foreach (DataRow row in dtd.Rows)
			{
				DataRow pr = dtPrint.NewRow();
				pr["Номер на заповед"] = row["numberOrder"];
				pr["Дата"] = row["orderFromDate"];
				pr["Номер на дело"] = row["user_id"];
				pr["Име"] = row["Name"];
				pr["От дата"] = row["FromDate"];
				pr["До дата"] = row["ToDate"];
				pr["Издаден на"] = row["IssuedAt"];
				pr["Раб. дни"] = row["WorkDays"];
				pr["Кал. дни"] = row["CalDays"];
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
