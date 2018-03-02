using System;
using System.Collections.Generic;
using System.Data;
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
using DataLayer;
using HRDataLayer;

namespace SicknessFrame
{
	/// <summary>
	/// Interaction logic for CheckHolidays.xaml
	/// </summary>
	public partial class CheckHolidays : Window
	{
		private string connectionString;
		Entities context;
		
		public CheckHolidays(string connstring)
		{
			InitializeComponent();
			this.connectionString = connstring;
		}

		private void CheckNormalHolidays()
		{
			List<string> lstnames = new List<string>();
			this.context = new Entities(this.connectionString);

			List<CheckHolidayModel> lstModels = new List<CheckHolidayModel>();
			var Year = context.HR_Year.FirstOrDefault().Year;

			var personsRaw = (from p in this.context.HR_Person
							  join a in this.context.HR_PersonAssignment on p.id equals a.parent
							  join y in this.context.HR_Year_Holiday on p.id equals y.parent
							  where p.fired == 0
								&& y.year == Year
							  select new
										  {
											  p,
											  a,
											  y
										  }).ToList();

			var personGroups = personsRaw.GroupBy(p => p.p.id);

			List<CheckHolidayPerson> lstPersons = new List<CheckHolidayPerson>();

			foreach (var group in personGroups)
			{
				if (group.Any(g => g.a.isActive == 1) == true)
				{
					var per = new CheckHolidayPerson();
					var fa = group.First(g => g.a.IsAdditionalAssignment == 0);
					var aa = group.First(g => g.a.isActive == 1);
					per.HiredAt = fa.a.assignedAt;
					per.id_person = fa.p.id;
					per.Leftover = fa.y.leftover;
					per.Name = fa.p.name;
					per.Total = fa.y.total;
					per.Contract = aa.a.AdditionalHoliday + int.Parse(aa.a.NumHoliday);
					per.Position = aa.a.position;
					lstPersons.Add(per);
				}
			}

			foreach (var person in lstPersons)
			{
				var holidays = this.context.HR_Absence.Where(a => a.Year == Year.ToString() && a.parent == person.id_person && a.typeAbsence == "Полагаем годишен отпуск");

				var cancellations = this.context.HR_Absence.Where(a => a.Year == Year.ToString() && a.parent == person.id_person && a.typeAbsence == "Прекратяване на отпуск");

				int? used = holidays.Sum(a => (int?)a.countDays) - cancellations.Sum(a => (int?)a.countDays);

				if (used == null)
				{
					used = 0;
				}

				var pTotal = person.Contract;

				int cl = (int)person.Total - (int)used;

				if (person.HiredAt.Value.Year == Year)
				{
					DateTime dthir = person.HiredAt.Value;
					float a_day = 0, a_month = 0, day_rest = 0, month_rest = 0, left = 0;
					day_rest = 30 - dthir.Day;
					month_rest = 12 - dthir.Month;

					if (pTotal > 0)
					{
						a_day = (float)pTotal / 360;
						a_month = (float)pTotal / 12;

						// Закръгляне
						//if (this.ms)
						//	if (day_rest < 0.5)
						//	{
						//		day_rest = 0;
						//	}
						//Пропорцианалоно отпуск = (Остатък месеци) * (отпуск за месец) + (остатък дни) * (отпуск за ден)
						double leftt = month_rest * a_month + day_rest * a_day;
						leftt = Math.Round(leftt);
						left = (int)leftt;

						cl = (int)left - (int)used;

						if (person.Leftover != cl || person.Total != pTotal)
						{
							CheckHolidayModel model = new CheckHolidayModel();
							model.ActualTotal = (int)person.Total;
							model.CalculatedLeftover = cl;
							model.CalculatedTotal = (int)person.Total;
							model.id_person = person.id_person;
							model.Leftover = (int)person.Leftover;
							model.Name = person.Name;
							model.Total = (int)pTotal;
							model.Used = (int)used;
							model.Position = person.Position;
							lstModels.Add(model);
						}
					}
					else
					{
						CheckHolidayModel model = new CheckHolidayModel();
						model.ActualTotal = (int)person.Total;
						model.CalculatedLeftover = cl;
						model.id_person = person.id_person;
						model.CalculatedTotal = (int)person.Total;
						model.Leftover = (int)person.Leftover;
						model.Name = person.Name;
						model.Total = (int)pTotal;
						model.Used = (int)used;
						model.Position = person.Position;
						lstModels.Add(model);
					}
				}
				else
				{
					if (person.Total != pTotal || pTotal == 0 || person.Leftover != cl || person.Total < 20)
					{
						CheckHolidayModel model = new CheckHolidayModel();
						model.ActualTotal = (int)person.Total;
						model.CalculatedLeftover = cl;
						model.id_person = person.id_person;
						model.Leftover = (int)person.Leftover;
						
							model.CalculatedTotal = (int)person.Total;
						
						model.Name = person.Name;
						model.Total = (int)pTotal;
						model.Used = (int)used;
						model.Position = person.Position;

						lstModels.Add(model);
					}
				}
			}
			this.dgREsults.ItemsSource = lstModels;
			this.txtResults.Text = lstModels.Count.ToString();
		}

		private void btnCheck_Click(object sender, RoutedEventArgs e)
		{
			if (connectionString.Contains("hrshumen"))
			{
				this.CheckShumenHolidays();
			}
			else
			{
				this.CheckNormalHolidays();
			}
		}

		private void CheckShumenHolidays()
		{
			List<string> lstnames = new List<string>();
			this.context = new Entities(this.connectionString);
			List<CheckHolidayModel> lstModels = new List<CheckHolidayModel>();
			var Year = context.HR_Year.FirstOrDefault().Year;

			var personsRaw = (from p in this.context.HR_Person
							  join a in this.context.HR_PersonAssignment on p.id equals a.parent

							  where p.fired == 0
								&& a.isActive == 1
							  select new
							  {
								  p,
								  a,
							  }).ToList();

			int fixCounter = 0;

			foreach (var person in personsRaw)
			{
				var pid = person.p.id;
				var firstAssignment = this.context.HR_PersonAssignment.FirstOrDefault(a => a.parent == person.p.id && a.IsAdditionalAssignment == 0);
				if (firstAssignment == null)
				{
					return;
				}

				var lastAssignment = person.a;
				if (lastAssignment == null)
				{
					return;
				}

				var PYH = this.context.HR_Year_Holiday.FirstOrDefault(p => p.parent == pid && p.year == Year);

				if (PYH == null)
				{
					continue;
				}

				var refDate = new DateTime(2015, 12, 31);
				var years = refDate.Year - firstAssignment.assignedAt.Value.Year;

				var syear = Year.ToString();
				var holidays = this.context.HR_Absence.Where(a => a.Year == syear && a.parent == person.p.id && a.typeAbsence == "Полагаем годишен отпуск");

				var cancellations = this.context.HR_Absence.Where(a => a.Year == syear && a.parent == person.p.id && a.typeAbsence == "Прекратяване на отпуск");

				int? used = holidays.Sum(a => (int?)a.countDays) - cancellations.Sum(a => (int?)a.countDays);

				if (used == null)
				{
					used = 0;
				}
				var model = new CheckHolidayModel();
				int Nh = 0, ah = 0;
				if (lastAssignment.AdditionalHoliday != null)
				{
					ah = (int)lastAssignment.AdditionalHoliday;
				}
				int.TryParse(lastAssignment.NumHoliday, out Nh);
				model.ActualTotal = (int)PYH.total;
				model.AssignedAt = firstAssignment.assignedAt.Value;
				model.Name = person.p.name;
				model.Position = lastAssignment.position;
				model.Total = Nh + ah;
				model.Used = (int)used;

				if (lastAssignment.position.ToLower().Contains("асистент")
						|| lastAssignment.position.ToLower().Contains("доцент")
						|| lastAssignment.position.ToLower().Contains("професор")
						|| lastAssignment.position.ToLower().Contains("преподавател"))
				{
					if (person.p.languages.ToLower() != "синдикален член")
					{
						lastAssignment.NumHoliday = "48";
						lastAssignment.AdditionalHoliday = 0;
					}
					else
					{
						lastAssignment.NumHoliday = "48";
						lastAssignment.AdditionalHoliday = years / 4;
					}
				}
				else
				{
					if (person.p.languages.ToLower() != "синдикален член")
					{
						lastAssignment.NumHoliday = "20";
						lastAssignment.AdditionalHoliday = 0;
					}
					else
					{
						lastAssignment.NumHoliday = "20";
						lastAssignment.AdditionalHoliday = years / 2;
					}
				}

				int nh;
				int.TryParse(lastAssignment.NumHoliday, out nh);
				int total = nh + (int)lastAssignment.AdditionalHoliday;

				if (firstAssignment.assignedAt.Value.Year == 2015)
				{
					DateTime dthir = firstAssignment.assignedAt.Value;
					float a_day = 0, a_month = 0, day_rest = 0, month_rest = 0;
					int left = 0;
					day_rest = 30 - dthir.Day;
					month_rest = 12 - dthir.Month;

					a_day = (float)total / 360;
					a_month = (float)total / 12;

					//Пропорцианалоно отпуск = (Остатък месеци) * (отпуск за месец) + (остатък дни) * (отпуск за ден)
					double leftt = month_rest * a_month + day_rest * a_day;
					leftt = Math.Round(leftt);
					left = (int)leftt;

					var cl = (int)left - (int)used;

					if (PYH.total != left || PYH.leftover != cl)
					{
						model.Leftover = (int)PYH.leftover;
						model.CalculatedLeftover = cl;
						model.CalculatedTotal = total;
						model.IsMember = person.p.languages;
						fixCounter++;
						PYH.total = left;
						PYH.leftover = cl;
						lstModels.Add(model);
					}
				}
				else
				{
					var cl = (int)total - (int)used;
					if (PYH.total != total || PYH.leftover != cl)
					{
						fixCounter++;
						model.Leftover = (int)PYH.leftover;
						model.CalculatedLeftover = cl;
						model.CalculatedTotal = total;
						model.IsMember = person.p.languages;
						PYH.total = total;
						PYH.leftover = cl;
						lstModels.Add(model);
					}
				}
				//context.SaveChanges();
			}

			this.dgREsults.ItemsSource = lstModels;
			this.txtResults.Text = lstModels.Count.ToString();
		}

		private void btnExport_Click(object sender, RoutedEventArgs e)
		{
			if(this.dgREsults.ItemsSource != null)
			{
				ExcelExport.ExcelExport ex = new ExcelExport.ExcelExport();

				List<DataTable> lstData = new List<DataTable>();
				List<CheckHolidayModel> lstHolidays = (List<CheckHolidayModel>)this.dgREsults.ItemsSource;

				DataTable dt = ExcelExport.ToDataTableList.LINQToDataTable(lstHolidays);
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

				dtPrint.Columns.Add("Име", "".GetType());
				dtPrint.Columns.Add("Полагаем по договор", "".GetType());
				dtPrint.Columns.Add("Полагаем", "".GetType());
				dtPrint.Columns.Add("Ползван", "".GetType());
				dtPrint.Columns.Add("Остатък", "".GetType());
				dtPrint.Columns.Add("Изчислен остатък", "".GetType());
				dtPrint.Columns.Add("Изчислен полагаем", "".GetType());
				dtPrint.Columns.Add("Длъжност", "".GetType());
				dtPrint.Columns.Add("Назначен на", "".GetType());
				dtPrint.Columns.Add("Синдикален член", "".GetType());
				
				foreach (DataRow row in dtd.Rows)
				{
					DataRow pr = dtPrint.NewRow();
					pr["Име"] = row["Name"];
					pr["Полагаем по договор"] = row["Total"];
					pr["Полагаем"] = row["ActualTotal"];
					pr["Ползван"] = row["Used"];
					pr["Остатък"] = row["Leftover"];
					pr["Изчислен остатък"] = row["CalculatedLeftover"];
					pr["Изчислен полагаем"] = row["CalculatedTotal"];
					pr["Длъжност"] = row["Position"];
					pr["Синдикален член"] = row["IsMember"];
					pr["Назначен на"] = row["AssignedAt"];

					
					dtPrint.Rows.Add(pr);
				}
				lstData.Add(dtPrint);
				ex.Export(lstData);
			}
		}
	}
}
