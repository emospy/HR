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
using System.Windows.Shapes;
using DataLayer;
using HRDataLayer;


namespace HolidayPlan
{
	/// <summary>
	/// Interaction logic for YearWorkdays.xaml
	/// </summary>
	public partial class YearWorkdays : Window
	{
		private Entities entity;
		private string connString;
		List<HR_YearWorkdays> lstWDTable = new List<HR_YearWorkdays>();
		DateTime CurrentDate;

		public YearWorkdays(string connectionString)
		{
			connString = connectionString;
			InitializeComponent();
		}

        void InitDataGrid()
        {
        	var lstCalRow = new List<CalendarRow>();
			if (this.dpCurrentDate.SelectedDate != null)
			{
				CalendarRow row = new CalendarRow(this.dpCurrentDate.SelectedDate.Value, this.connString);
				lstCalRow.Add(row);
				int dmon = DateTime.DaysInMonth(this.dpCurrentDate.SelectedDate.Value.Year, this.dpCurrentDate.SelectedDate.Value.Month);
				switch (dmon)
				{
					case 28:
						dgcmb29.Visibility = Visibility.Hidden;
						dgcmb30.Visibility = Visibility.Hidden;
						dgcmb31.Visibility = Visibility.Hidden;
						break;
					case 29:
						dgcmb29.Visibility = Visibility.Visible;
						dgcmb30.Visibility = Visibility.Hidden;
						dgcmb31.Visibility = Visibility.Hidden;
						break;
					case 30:
						dgcmb29.Visibility = Visibility.Visible;
						dgcmb30.Visibility = Visibility.Visible;
						dgcmb31.Visibility = Visibility.Hidden;
						break;
					case 31:
						dgcmb29.Visibility = Visibility.Visible;
						dgcmb30.Visibility = Visibility.Visible;
						dgcmb31.Visibility = Visibility.Visible;
						break;
				}
				this.dgWorkDays.ItemsSource = lstCalRow;
				this.ColorGridHeadres();
			}
			else
			{
				return;
			}
        }

        private void ColorGridHeadres()
        {
            int x = this.dgcmb1.DisplayIndex;
            for(int i = 0; i < DateTime.DaysInMonth(this.CurrentDate.Year, this.CurrentDate.Month); i++)
            {
                DateTime tmpDate = new DateTime(this.CurrentDate.Year, this.CurrentDate.Month, i + 1);
                if(tmpDate.DayOfWeek == DayOfWeek.Sunday || tmpDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    this.dgWorkDays.Columns[i + x].HeaderStyle = (Style) this.FindResource("ColumnHeaderStyleWeekend");
                }
                else
                {
                    this.dgWorkDays.Columns[i + x].HeaderStyle = (Style)this.FindResource("ColumnHeaderStyleWeek");
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

			this.entity = new Entities(this.connString);
			var lstWorkdays = (from wd in entity.HR_YearWorkdays
							   where wd.Date.Year == this.CurrentDate.Year && wd.Date.Month == this.CurrentDate.Month
							   select wd).ToList();

        	CalendarRow cal = ((List<CalendarRow>) this.dgWorkDays.ItemsSource).First();

			
			for (int i = 1; i < DateTime.DaysInMonth(this.CurrentDate.Year, this.CurrentDate.Month) + 1; i++)
			{
				DateTime CD = new DateTime(this.CurrentDate.Year, this.CurrentDate.Month, i);
				var day = lstWorkdays.Find(wd => wd.Date == CD);
					
				if ((day == null) && ( ((CD.DayOfWeek == DayOfWeek.Saturday || CD.DayOfWeek == DayOfWeek.Sunday) && cal[i]) || ((CD.DayOfWeek != DayOfWeek.Saturday && CD.DayOfWeek != DayOfWeek.Sunday) && !cal[i])))
				{ //if it is an exception
					day = new HR_YearWorkdays();
					day.Date = new DateTime(this.dpCurrentDate.SelectedDate.Value.Year, this.dpCurrentDate.SelectedDate.Value.Month, i);
					day.IsHoliday = cal[i];

					this.entity.HR_YearWorkdays.AddObject(day);
				}
				else if ((day != null) && (((CD.DayOfWeek == DayOfWeek.Saturday || CD.DayOfWeek == DayOfWeek.Sunday) && !cal[i]) || ((CD.DayOfWeek != DayOfWeek.Saturday && CD.DayOfWeek != DayOfWeek.Sunday) && cal[i])))
				{
					this.entity.HR_YearWorkdays.DeleteObject(day);
				}
			}
			this.entity.SaveChanges();
        }

        private void dtpCurrentDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
			if ((this.dpCurrentDate.SelectedDate.Value.Month != this.CurrentDate.Month) || (this.dpCurrentDate.SelectedDate.Value.Year != this.CurrentDate.Year))
			{
				this.CurrentDate = this.dpCurrentDate.SelectedDate.Value;
				this.InitDataGrid();
			}
        }

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			this.CurrentDate = DateTime.Now;
			this.dpCurrentDate.SelectedDate = DateTime.Now;

            InitDataGrid();
		}

    	private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
    	{
    		this.InitDataGrid();
    	}
	}

	public class CalendarRow
	{
		private string ConnString;
		private DateTime date;

		public bool Day1 { get; set; }
		public bool Day2 { get; set; }
		public bool Day3 { get; set; }
		public bool Day4 { get; set; }
		public bool Day5 { get; set; }
		public bool Day6 { get; set; }
		public bool Day7 { get; set; }
		public bool Day8 { get; set; }
		public bool Day9 { get; set; }
		public bool Day10 { get; set; }
		public bool Day11 { get; set; }
		public bool Day12 { get; set; }
		public bool Day13 { get; set; }
		public bool Day14 { get; set; }
		public bool Day15 { get; set; }
		public bool Day16 { get; set; }
		public bool Day17 { get; set; }
		public bool Day18 { get; set; }
		public bool Day19 { get; set; }
		public bool Day20 { get; set; }
		public bool Day21 { get; set; }
		public bool Day22 { get; set; }
		public bool Day23 { get; set; }
		public bool Day24 { get; set; }
		public bool Day25 { get; set; }
		public bool Day26 { get; set; }
		public bool Day27 { get; set; }
		public bool Day28 { get; set; }
		public bool Day29 { get; set; }
		public bool Day30 { get; set; }
		public bool Day31 { get; set; }

		public bool this[int index]
		{
			set
			{
				switch (index)
				{
					case 1:
						Day1 = value;
						break;
					case 2:
						Day2 = value;
						break;
					case 3:
						Day3 = value;
						break;
					case 4:
						Day4 = value;
						break;
					case 5:
						Day5 = value;
						break;
					case 6:
						Day6 = value;
						break;
					case 7:
						Day7 = value;
						break;
					case 8:
						Day8 = value;
						break;
					case 9:
						Day9 = value;
						break;
					case 10:
						Day10 = value;
						break;
					case 11:
						Day11 = value;
						break;
					case 12:
						Day12 = value;
						break;
					case 13:
						Day13 = value;
						break;
					case 14:
						Day14 = value;
						break;
					case 15:
						Day15 = value;
						break;
					case 16:
						Day16 = value;
						break;
					case 17:
						Day17 = value;
						break;
					case 18:
						Day18 = value;
						break;
					case 19:
						Day19 = value;
						break;
					case 20:
						Day20 = value;
						break;
					case 21:
						Day21 = value;
						break;
					case 22:
						Day22 = value;
						break;
					case 23:
						Day23 = value;
						break;
					case 24:
						Day24 = value;
						break;
					case 25:
						Day25 = value;
						break;
					case 26:
						Day26 = value;
						break;
					case 27:
						Day27 = value;
						break;
					case 28:
						Day28 = value;
						break;
					case 29:
						Day29 = value;
						break;
					case 30:
						Day30 = value;
						break;
					case 31:
						Day31 = value;
						break;
				}
			}
			get
			{
				switch (index)
				{
					case 1:
						return Day1;
					case 2:
						return Day2;
					case 3:
						return Day3;
					case 4:
						return Day4;
					case 5:
						return Day5;
					case 6:
						return Day6;
					case 7:
						return Day7;
					case 8:
						return Day8;
					case 9:
						return Day9;
					case 10:
						return Day10;
					case 11:
						return Day11;
					case 12:
						return Day12;
					case 13:
						return Day13;
					case 14:
						return Day14;
					case 15:
						return Day15;
					case 16:
						return Day16;
					case 17:
						return Day17;
					case 18:
						return Day18;
					case 19:
						return Day19;
					case 20:
						return Day20;
					case 21:
						return Day21;
					case 22:
						return Day22;
					case 23:
						return Day23;
					case 24:
						return Day24;
					case 25:
						return Day25;
					case 26:
						return Day26;
					case 27:
						return Day27;
					case 28:
						return Day28;
					case 29:
						return Day29;
					case 30:
						return Day30;
					case 31:
						return Day31;
				}
				return false;
			}
		}

		public void SetDayFromSP(HR_YearWorkdays day)
		{
			this[day.Date.Day] = (bool)day.IsHoliday;
		}

		public CalendarRow(DateTime dateS, string connstring)
		{
			this.date = dateS;
			this.ConnString = connstring;
			int dim = DateTime.DaysInMonth(date.Year, date.Month);
			for (int i = 1; i <= dim; i ++ )
			{
				DateTime CD = new DateTime(date.Year, date.Month, i );
				if(CD.DayOfWeek == DayOfWeek.Sunday || CD.DayOfWeek == DayOfWeek.Saturday)
				{
					this[i] = false;
				}
				else
				{
					this[i] = true;
				}
			}

			if (dim < 31)
			{
				for (int i = dim + 1; i <= 31; i++)
				{
					this[i] = false;
				}
			}

			InitRowFromDataBase();
		}

		private void InitRowFromDataBase()
		{
            try
            {
				using (Entities entity = new Entities(this.ConnString))
                {
                    var lstWorkdays = (from wd in entity.HR_YearWorkdays
                                       where wd.Date.Year == this.date.Year && wd.Date.Month == this.date.Month
                                       select wd).ToList();
                    foreach (var hrYearWorkdayse in lstWorkdays)
                    {
                        this[hrYearWorkdayse.Date.Day] = (bool)hrYearWorkdayse.IsHoliday;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.InnerException.Message);
            }
		}

		public static List<int> CalculateMaxMonthWorkdays(string connstring, int year)
		{
			List<int> lstMonthDays = new List<int>();

			lstMonthDays.Add(GetCountWorkDays(new DateTime(year, 1, 1), new DateTime(year, 1, DateTime.DaysInMonth(year, 1)), connstring));
			lstMonthDays.Add(GetCountWorkDays(new DateTime(year, 2, 1), new DateTime(year, 2, DateTime.DaysInMonth(year, 2)), connstring));
			lstMonthDays.Add(GetCountWorkDays(new DateTime(year, 3, 1), new DateTime(year, 3, DateTime.DaysInMonth(year, 3)), connstring));
			lstMonthDays.Add(GetCountWorkDays(new DateTime(year, 4, 1), new DateTime(year, 4, DateTime.DaysInMonth(year, 4)), connstring));
			lstMonthDays.Add(GetCountWorkDays(new DateTime(year, 5, 1), new DateTime(year, 5, DateTime.DaysInMonth(year, 5)), connstring));
			lstMonthDays.Add(GetCountWorkDays(new DateTime(year, 6, 1), new DateTime(year, 6, DateTime.DaysInMonth(year, 6)), connstring));
			lstMonthDays.Add(GetCountWorkDays(new DateTime(year, 7, 1), new DateTime(year, 7, DateTime.DaysInMonth(year, 7)), connstring));
			lstMonthDays.Add(GetCountWorkDays(new DateTime(year, 8, 1), new DateTime(year, 8, DateTime.DaysInMonth(year, 8)), connstring));
			lstMonthDays.Add(GetCountWorkDays(new DateTime(year, 9, 1), new DateTime(year, 9, DateTime.DaysInMonth(year, 9)), connstring));
			lstMonthDays.Add(GetCountWorkDays(new DateTime(year, 10, 1), new DateTime(year, 10, DateTime.DaysInMonth(year, 10)), connstring));
			lstMonthDays.Add(GetCountWorkDays(new DateTime(year, 11, 1), new DateTime(year, 11, DateTime.DaysInMonth(year, 11)), connstring));
			lstMonthDays.Add(GetCountWorkDays(new DateTime(year, 12, 1), new DateTime(year, 12, DateTime.DaysInMonth(year, 12)), connstring));

			return lstMonthDays;
		}

		public static int GetCountWorkDays(DateTime DateStart, DateTime DateEnd, string connstring)
		{
			try
			{
				int days = 0;

				if (DateStart > DateEnd)
					return 0;

				CalendarRow Cal = new CalendarRow(DateStart, connstring);

				if (DateStart.Month < DateEnd.Month || DateStart.Year < DateEnd.Year)
				{
				    int j = DateTime.DaysInMonth(DateStart.Year, DateStart.Month);
					for (int i = DateStart.Day; i <= j; i++)
					{
						if (Cal[i])
						{
							days++;
						}
					}
					if (DateStart.Month == 12)
					{
						DateStart = new DateTime(DateStart.Year + 1, 1, 1);
					}
					else
					{
						DateStart = new DateTime(DateStart.Year, DateStart.Month + 1, 1);
					}
					Cal = new CalendarRow(DateStart, connstring);
				}

				if (DateStart.Month < DateEnd.Month || DateStart.Year < DateEnd.Year)
				{
					do
					{
						if (DateStart.Month < DateEnd.Month || DateStart.Year < DateEnd.Year)
						{
							for (int i = DateStart.Day; i <= DateTime.DaysInMonth(DateStart.Year, DateStart.Month); i++)
							{
								if (Cal[i])
								{
									days++;
								}
							}
						}
						if (DateStart.Month == 12)
						{
							DateStart = new DateTime(DateStart.Year + 1, 1, 1);
						}
						else
						{
							DateStart = new DateTime(DateStart.Year, DateStart.Month + 1, 1);
						}
						Cal = new CalendarRow(DateStart, connstring);
					} while (DateStart.Year < DateEnd.Year || DateStart.Month < DateEnd.Month);
				}

				for (int i = DateStart.Day; i <= DateEnd.Day; i++)
				{
					if (Cal[i])
					{
						days++;
					}
				}

				return days;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
				return 0;
			}
		}

        public static void CalculateCancellation(DateTime DateStart, string connstring, int parent)
        {
            try
            {
				Entities MyData = new Entities(connstring);

                var AbsencesToCancel = (from ab in MyData.HR_Absence
                                        where ab.fromDate <= DateStart 
                                            && ab.toDate >= DateStart
                                            && ab.parent == parent
                                        select ab).ToList(); //all these absences should be cancelled



                foreach (var abs in AbsencesToCancel)
                {
                    int Year = 0;
                    int.TryParse(abs.Year, out Year);
                    var yhRow = (from yh in MyData.HR_Year_Holiday
                                 where yh.year == Year
                                 && yh.parent == parent
                                 select yh).First();

                    int DaysToReturn = GetCountWorkDays(DateStart, (DateTime)abs.toDate, connstring);
                    
                    if (yhRow != null)
                    {
                        switch (abs.typeAbsence)
                        {
                            case "Полагаем годишен отпуск":
                                yhRow.leftover += DaysToReturn;
                                break;
                            case "Неплатен отпуск":
                                yhRow.Unpayed += DaysToReturn;
                                break;
                            case "Полагаем отпуск ТЕЛК":
                                yhRow.telk += DaysToReturn;
                                break;
                            case "Полагаем отпуск обучение":
                                yhRow.Education += DaysToReturn;
                                break;
                            case "Полагаем отпуск друг":
                                yhRow.Additional += DaysToReturn;
                                break;
                        }
                    }
                    MyData.SaveChanges();
                }
                
            }
            catch (Exception ex)
            {
                ErrorLog.WriteException(ex, ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        public static void FixAssignmentDates(string connstring)
        {
            try
            {
				Entities MyData = new Entities(connstring);

                var CorrectDates = (from per in MyData.HR_Person
                                    join ass in MyData.HR_PersonAssignment on per.id equals ass.parent
                                    where ass.IsAdditionalAssignment == 0
                                    select new { ID = per.id,
                                                 HIR = ass.assignedAt}).ToList(); //all these absences should be cancelled

                foreach (var rec in CorrectDates)
                {
                    var person = (from p in MyData.HR_Person
                                 where p.id == rec.ID
                                 select p).Single();
                    person.hiredAt = rec.HIR;
                    MyData.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteException(ex, ex.Message);
                MessageBox.Show(ex.Message);
            }
        }
	}
}
