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
	public partial class AssignmentsWindow : Window
	{
		private string connectionString;
		private List<HR_Absence> lstUpdatedItems;
		private List<HR_Absence> lstNewItems;
		private Entities context;
		private List<HR_Absence> lstAbsence;
		private List<HR_Person> lstPerson;
		private List<HR_Person> lstSysco;
		private List<CustomAbsenceModel> lstCustomAbsence;
		//List<string> lstDuration;

		public AssignmentsWindow(string connstring)
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
			this.lstCustomAbsence = new List<CustomAbsenceModel>();


		}

		private HR_Absence FillAbsenceRow(CustomAbsenceModel ab)
		{
			HR_Absence nab = new HR_Absence();
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

		private void UpdateAbsenceRow(CustomAbsenceModel ab, HR_Absence nab)
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
				//var date = DateTime.Now.Year.ToString();
				//this.lstAbsence = (from absence in this.context.absence
				//				   where ((absence.Year == date) && (absence.typeAbsence == "Болнични"))
				//				   select absence).ToList();
				//foreach (CustomAbsence ab in this.dgAbsence.ItemsSource)
				//{
				//	if (ab.isNew)
				//	{
				//		this.context.absence.AddObject(this.FillAbsenceRow(ab));
				//	}
				//	else if (ab.isUpdated)
				//	{
				//		absence up = this.lstAbsence.Find(
				//			delegate(absence abs)
				//				{
				//					return abs.id == ab.id;
				//				});
				//		if (up != null)
				//		{
				//			this.UpdateAbsenceRow(ab, up);
				//		}
				//	}
				//}
				//this.context.SaveChanges();
				//foreach (CustomAbsence ab in this.dgAbsence.ItemsSource)
				//{
				//	ab.isNew = false;
				//	ab.isUpdated = false;
				//}
				//this.dgAbsence.Items.Refresh();
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

				this.lstCustomAbsence = (from absence in this.context.HR_Absence
				                         join per in this.context.HR_Person on absence.parent equals per.id
				                         where ((absence.typeAbsence == "Болнични"))
				                         select new CustomAbsenceModel
					                                {
						                                AdditionalDocs = absence.AdditionalDocs,
						                                Attachment7 = absence.Attachment7,
						                                CalendarDays = (int) absence.CalendarDays,
						                                countDays = (int) absence.countDays,
						                                Declaration39 = absence.Declaration39,
						                                fromDate = (DateTime) absence.fromDate,
						                                id = absence.id,
						                                //id_sysco = per.id_sysco,
						                                isNew = false,
						                                issuedatdate = (DateTime) absence.issuedatdate,
						                                isUpdated = false,
						                                MKB = absence.MKB,
						                                name = per.name,
						                                NAPDocs = absence.NAPDocs,
						                                numberOrder = absence.numberOrder,
						                                orderFromDate = (DateTime) absence.orderFromDate,
						                                parent = absence.parent,
						                                reason = absence.reason,
						                                reasons = absence.reasons,
						                                sicknessduration = absence.sicknessduration,
						                                SicknessNumber = absence.SicknessNumber,
						                                toDate = (DateTime) absence.toDate,
						                                typeAbsence = absence.typeAbsence,
						                                Year = absence.Year,
					                                }).ToList();

				int no;

				this.lstCustomAbsence.ForEach(p => p.numberOrder = ((int.TryParse(p.numberOrder, out no)) ? string.Format("{0:0000}", no) : "0000"));

				this.lstCustomAbsence = this.lstCustomAbsence.OrderBy(p => p.numberOrder).ToList();

				this.lstPerson = (from person in this.context.HR_Person
				                  orderby person.name
				                  select person).ToList();

				this.lstSysco = (from person in this.context.HR_Person
				                 select person).ToList();

				this.dgAbsence.ItemsSource = this.lstCustomAbsence;

				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
		}

		private void dpYear_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			InitialiseGrid();
		}
	}
}
