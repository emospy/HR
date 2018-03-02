using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using HRDataLayer;

namespace HolidayPlan
{
	public static class MaxMonthDays
	{
		public static bool SimpleMode;
		public static int MaxJanuary;
		public static int MaxFebryary;
		public static int MaxMarch;
		public static int MaxApril;
		public static int MaxMay;
		public static int MaxJune;
		public static int MaxJuly;
		public static int MaxAugust;
		public static int MaxSeptember;
		public static int MaxOctober;
		public static int MaxNovember;
		public static int MaxDecember;
	}

	public class PeopleAndAssignments
	{
		public HR_Person person;
		public List<HR_PersonAssignment> lstAssignments;
	}

	public class HolidayPlanRow : INotifyPropertyChanged
	{

		public HolidayPlanRow()
		{
			//this.january = this.february = this.march = this.april = this.may = this.june = this.july = this.august = this.september = this.october = this.november = this.december = "";
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void ParseFailed(string message);
		public event ParseFailed ParseMessageError;

		public int id_plannedHoliday { get; set; }
		public int id_person { get; set; }
		public string Name { get; set; }
		public string EGN { get; set; }
		public int Total { get; set; }
		public bool IsChanged { get; set; }
		public int PrevYearLeftover { get; set; }
		public int Year { get; set; }
		public string connString { get; set; }

		private int janDays;
		private string january;
		private int febDays;
		private string february;
		private int marDays;
		private string march;
		private int aprDays;
		private string april;
		private int mayDays;
		private string may;
		private int junDays;
		private string june;
		private int julDays;
		private string july;
		private int augDays;
		private string august;
		private int sepDays;
		private string september;
		private int octDays;
		private string october;
		private int novDays;
		private string november;
		private int decDays;
		private string december;

		public int TotalLeftover
		{
			get
			{
				//return this.TotalLeftover;
				return this.Total + this.PrevYearLeftover - this.JanuaryDays;
			}
			set
			{
				//this.TotalLeftover = this.Total + this.PrevYearLeftover - this.JanuaryDays;
			}
		}

		private int ParseMonthData(string value, int maxDays, int month)
		{
			int totalDaysAbsence = 0;
			value = value.Trim();
			value = value.Trim(new char[] { ',' });
			var arrayOfStrings = value.Split(new char[] { ',' });

			foreach (var str in arrayOfStrings)
			{
				var arrayOfDates = str.Split(new char[] { '-' });

				if (arrayOfDates.Count() == 2)
				{
					int start, end;
					if (int.TryParse(arrayOfDates[0], out start) == false)
					{
						return -1;
					}
					if (int.TryParse(arrayOfDates[1], out end) == false)
					{
						return -1;
					}
					if (start > end)
					{
						return -1;
					}
					if (end > maxDays)
					{
						return -1;
					}
					totalDaysAbsence += CalendarRow.GetCountWorkDays(new DateTime(this.Year, month, start), new DateTime(this.Year, month, end), this.connString);
				}
				else if (arrayOfDates.Count() == 1)
				{
					int start;
					if (arrayOfDates[0] == string.Empty)
					{
						return 0;
					}
					if (int.TryParse(arrayOfDates[0], out start) == false || start > maxDays)
					{
						return -1;
					}
					totalDaysAbsence += CalendarRow.GetCountWorkDays(new DateTime(this.Year, month, start), new DateTime(this.Year, month, start), this.connString);
				}
				else
				{
					return -1;
				}
			}
			return totalDaysAbsence;
		}

		public string January
		{
			get
			{
				return this.january;
			}
			set
			{
				if (MaxMonthDays.SimpleMode == false)
				{
					if (this.january != value && value != "")
					{
						var parseResult = this.ParseMonthData(value, DateTime.DaysInMonth(this.Year, 1), 1);
						if (parseResult != -1 && parseResult <= MaxMonthDays.MaxJanuary)
						{
							this.january = value;
							this.IsChanged = true;
							this.janDays = parseResult;
							this.NotifyPropertyChanged("JanuaryDays");
						}
						else if (value == "")
						{
							this.january = value;
							this.IsChanged = true;
						}
						else
						{
							ParseMessageError("Невалидно въведени дни за отпуск" + value);
						}
					}
				}
			}
		}

		public int JanuaryDays
		{
			get
			{
				return this.janDays;
			}
			set
			{
				if (MaxMonthDays.SimpleMode == true)
				{
					var oldValue = this.janDays;
					if (value > MaxMonthDays.MaxJanuary)
					{
						this.janDays = MaxMonthDays.MaxJanuary;
					}
					else if (value < 0)
					{
						this.janDays = 0;
					}
					else
					{
						this.janDays = value;
					}

					if (this.janDays != oldValue)
					{
						this.IsChanged = true;
					}
				}
				NotifyPropertyChanged("TotalLeftover");
			}
		}

		public string February
		{
			get
			{
				return this.february;
			}
			set
			{
				if (MaxMonthDays.SimpleMode == false)
				{
					if (this.february != value && value != null)
					{
						var parseResult = this.ParseMonthData(value, DateTime.DaysInMonth(this.Year, 2), 2);
						if (parseResult != -1 && parseResult <= MaxMonthDays.MaxFebryary)
						{
							this.february = value;
							this.IsChanged = true;
							this.febDays = parseResult;
							this.NotifyPropertyChanged("FebruaryDays");
						}
						else if (this.february != value)
						{
							this.february = value;
							this.IsChanged = true;
						}
						else
						{
							ParseMessageError("Невалидно въведени дни за отпуск" + value);
						}
					}
				}
			}
		}

		public int FebruaryDays
		{
			get
			{
				return this.febDays;
			}
			set
			{
				if (MaxMonthDays.SimpleMode == true)
				{
					var oldValue = this.febDays;
					if (value > MaxMonthDays.MaxFebryary)
					{
						this.febDays = MaxMonthDays.MaxFebryary;
					}
					else if (value < 0)
					{
						this.febDays = 0;
					}
					else
					{
						this.febDays = value;
					}

					if (this.febDays != oldValue)
					{
						this.IsChanged = true;
					}
				}
				NotifyPropertyChanged("TotalLeftover");
			}
		}

		public string March
		{
			get
			{
				return this.march;
			}
			set
			{
				if (this.march != value)
				{
					var parseResult = this.ParseMonthData(value, DateTime.DaysInMonth(this.Year, 3), 3);
					if (parseResult != -1 && parseResult <= MaxMonthDays.MaxMarch)
					{
						this.march = value;
						this.IsChanged = true;
						this.marDays = parseResult;
						this.NotifyPropertyChanged("MarchDays");
					}
					else
					{
						ParseMessageError("Невалидно въведени дни за отпуск" + value);
					}
				}
			}
		}

		public int MarchDays
		{
			get
			{
				return this.marDays;
			}
			set
			{
				if (MaxMonthDays.SimpleMode == true)
				{
					var oldValue = this.marDays;
					if (value > MaxMonthDays.MaxMarch)
					{
						this.febDays = MaxMonthDays.MaxMarch;
					}
					else if (value < 0)
					{
						this.marDays = 0;
					}
					else
					{
						this.marDays = value;
					}

					if (this.marDays != oldValue)
					{
						this.IsChanged = true;
					}
				}
				NotifyPropertyChanged("TotalLeftover");
			}
		}

		public string April
		{
			get
			{
				return this.april;
			}
			set
			{
				if (this.april != value)
				{
					var parseResult = this.ParseMonthData(value, DateTime.DaysInMonth(this.Year, 4), 4);
					if (parseResult != -1 && parseResult <= MaxMonthDays.MaxApril)
					{
						this.april = value;
						this.IsChanged = true;
						this.aprDays = parseResult;
						this.NotifyPropertyChanged("AprilDays");
					}
					else
					{
						ParseMessageError("Невалидно въведени дни за отпуск" + value);
					}
				}
				//parse and notify changes in days

			}
		}

		public int AprilDays
		{
			get
			{
				return this.aprDays;
			}
			set
			{
				if (MaxMonthDays.SimpleMode == true)
				{
					var oldValue = this.aprDays;
					if (value > MaxMonthDays.MaxApril)
					{
						this.aprDays = MaxMonthDays.MaxApril;
					}
					else if (value < 0)
					{
						this.aprDays = 0;
					}
					else
					{
						this.aprDays = value;
					}

					if (this.aprDays != oldValue)
					{
						this.IsChanged = true;
					}
				}
				NotifyPropertyChanged("TotalLeftover");
			}
		}

		public string May
		{
			get
			{
				return this.may;
			}
			set
			{
				if (this.may != value)
				{
					var parseResult = this.ParseMonthData(value, DateTime.DaysInMonth(this.Year, 5), 5);
					if (parseResult != -1 && parseResult <= MaxMonthDays.MaxMay)
					{
						this.may = value;
						this.IsChanged = true;
						this.mayDays = parseResult;
						this.NotifyPropertyChanged("MayDays");
					}
					else
					{
						ParseMessageError("Невалидно въведени дни за отпуск" + value);
					}
				}
				//parse and notify changes in days

			}
		}

		public int MayDays
		{
			get
			{
				return this.mayDays;
			}
			set
			{
				if (MaxMonthDays.SimpleMode == true)
				{
					var oldValue = this.mayDays;
					if (value > MaxMonthDays.MaxMay)
					{
						this.mayDays = MaxMonthDays.MaxMay;
					}
					else if (value < 0)
					{
						this.mayDays = 0;
					}
					else
					{
						this.mayDays = value;
					}

					if (this.mayDays != oldValue)
					{
						this.IsChanged = true;
					}
				}
				NotifyPropertyChanged("TotalLeftover");
			}
		}

		public string June
		{
			get
			{
				return this.june;
			}
			set
			{
				if (this.june != value)
				{
					var parseResult = this.ParseMonthData(value, DateTime.DaysInMonth(this.Year, 6), 6);
					if (parseResult != -1 && parseResult <= MaxMonthDays.MaxJune)
					{
						this.june = value;
						this.IsChanged = true;
						this.junDays = parseResult;
						this.NotifyPropertyChanged("JuneDays");
					}
					else
					{
						ParseMessageError("Невалидно въведени дни за отпуск" + value);
					}
				}
				//parse and notify changes in days

			}
		}

		public int JuneDays
		{
			get
			{
				return this.junDays;
			}
			set
			{
				if (MaxMonthDays.SimpleMode == true)
				{
					var oldValue = this.junDays;
					if (value > MaxMonthDays.MaxJune)
					{
						this.junDays = MaxMonthDays.MaxJune;
					}
					else if (value < 0)
					{
						this.junDays = 0;
					}
					else
					{
						this.junDays = value;
					}

					if (this.junDays != oldValue)
					{
						this.IsChanged = true;
					}
				}

				NotifyPropertyChanged("TotalLeftover");
			}
		}

		public string July
		{
			get
			{
				return this.july;
			}
			set
			{
				if (this.july != value)
				{
					var parseResult = this.ParseMonthData(value, DateTime.DaysInMonth(this.Year, 7), 7);
					if (parseResult != -1 && parseResult <= MaxMonthDays.MaxJuly)
					{
						this.july = value;
						this.IsChanged = true;
						this.julDays = parseResult;
						this.NotifyPropertyChanged("JulyDays");
					}
					else
					{
						ParseMessageError("Невалидно въведени дни за отпуск" + value);
					}
				}
				//parse and notify changes in days

			}
		}

		public int JulyDays
		{
			get
			{
				return this.julDays;
			}
			set
			{
				if (MaxMonthDays.SimpleMode == true)
				{
					var oldValue = this.julDays;
					if (value > MaxMonthDays.MaxJuly)
					{
						this.julDays = MaxMonthDays.MaxJuly;
					}
					else if (value < 0)
					{
						this.julDays = 0;
					}
					else
					{
						this.julDays = value;
					}

					if (this.julDays != oldValue)
					{
						this.IsChanged = true;
					}
				}

				NotifyPropertyChanged("TotalLeftover");
			}
		}

		public string August
		{
			get
			{
				return this.august;
			}
			set
			{
				if (this.august != value)
				{
					var parseResult = this.ParseMonthData(value, DateTime.DaysInMonth(this.Year, 8), 8);
					if (parseResult != -1 && parseResult <= MaxMonthDays.MaxAugust)
					{
						this.august = value;
						this.IsChanged = true;
						this.augDays = parseResult;
						this.NotifyPropertyChanged("AugustDays");
					}
					else
					{
						ParseMessageError("Невалидно въведени дни за отпуск" + value);
					}
				}
				//parse and notify changes in days

			}
		}

		public int AugustDays
		{
			get
			{
				return this.augDays;
			}
			set
			{
				if (MaxMonthDays.SimpleMode == true)
				{
					var oldValue = this.augDays;
					if (value > MaxMonthDays.MaxAugust)
					{
						this.augDays = MaxMonthDays.MaxAugust;
					}
					else if (value < 0)
					{
						this.augDays = 0;
					}
					else
					{
						this.augDays = value;
					}

					if (this.augDays != oldValue)
					{
						this.IsChanged = true;
					}
				}
				NotifyPropertyChanged("TotalLeftover");
			}
		}

		public string September
		{
			get
			{
				return this.september;
			}
			set
			{
				if (this.september != value)
				{
					var parseResult = this.ParseMonthData(value, DateTime.DaysInMonth(this.Year, 9), 9);
					if (parseResult != -1 && parseResult <= MaxMonthDays.MaxSeptember)
					{
						this.september = value;
						this.IsChanged = true;
						this.sepDays = parseResult;
						this.NotifyPropertyChanged("SeptemberDays");
					}
					else
					{
						ParseMessageError("Невалидно въведени дни за отпуск" + value);
					}
				}
				//parse and notify changes in days

			}
		}

		public int SeptemberDays
		{
			get
			{
				return this.sepDays;
			}
			set
			{
				if (MaxMonthDays.SimpleMode == true)
				{
					var oldValue = this.sepDays;
					if (value > MaxMonthDays.MaxSeptember)
					{
						this.sepDays = MaxMonthDays.MaxSeptember;
					}
					else if (value < 0)
					{
						this.sepDays = 0;
					}
					else
					{
						this.sepDays = value;
					}

					if (this.sepDays != oldValue)
					{
						this.IsChanged = true;
					}
				}
				NotifyPropertyChanged("TotalLeftover");
			}
		}

		public string October
		{
			get
			{
				return this.october;
			}
			set
			{
				if (this.october != value)
				{
					var parseResult = this.ParseMonthData(value, DateTime.DaysInMonth(this.Year, 10), 10);
					if (parseResult != -1 && parseResult <= MaxMonthDays.MaxOctober)
					{
						this.october = value;
						this.IsChanged = true;
						this.octDays = parseResult;
						this.NotifyPropertyChanged("OctoberDays");
					}
					else
					{
						ParseMessageError("Невалидно въведени дни за отпуск" + value);
					}
				}
				//parse and notify changes in days

			}
		}

		public int OctoberDays
		{
			get
			{
				return this.octDays;
			}
			set
			{
				if (MaxMonthDays.SimpleMode == true)
				{
					var oldValue = this.octDays;
					if (value > MaxMonthDays.MaxOctober)
					{
						this.octDays = MaxMonthDays.MaxOctober;
					}
					else if (value < 0)
					{
						this.octDays = 0;
					}
					else
					{
						this.octDays = value;
					}

					if (this.octDays != oldValue)
					{
						this.IsChanged = true;
					}
				}

				NotifyPropertyChanged("TotalLeftover");
			}
		}

		public string November
		{
			get
			{
				return this.november;
			}
			set
			{
				if (this.november != value)
				{
					var parseResult = this.ParseMonthData(value, DateTime.DaysInMonth(this.Year, 11), 11);
					if (parseResult != -1 && parseResult <= MaxMonthDays.MaxNovember)
					{
						this.november = value;
						this.IsChanged = true;
						this.novDays = parseResult;
						this.NotifyPropertyChanged("NovemberDays");
					}
					else
					{
						ParseMessageError("Невалидно въведени дни за отпуск" + value);
					}
				}
				//parse and notify changes in days

			}
		}

		public int NovemberDays
		{
			get
			{
				return this.novDays;
			}
			set
			{
				if (MaxMonthDays.SimpleMode == true)
				{
					var oldValue = this.novDays;
					if (value > MaxMonthDays.MaxNovember)
					{
						this.novDays = MaxMonthDays.MaxNovember;
					}
					else if (value < 0)
					{
						this.novDays = 0;
					}
					else
					{
						this.novDays = value;
					}

					if (this.novDays != oldValue)
					{
						this.IsChanged = true;
					}
				}
				NotifyPropertyChanged("TotalLeftover");
			}
		}

		public string December
		{
			get
			{
				return this.december;
			}
			set
			{
				if (this.december != value)
				{
					var parseResult = this.ParseMonthData(value, DateTime.DaysInMonth(this.Year, 12), 12);
					if (parseResult != -1 && parseResult <= MaxMonthDays.MaxDecember)
					{
						this.december = value;
						this.IsChanged = true;
						this.decDays = parseResult;
						this.NotifyPropertyChanged("DecemberDays");
					}
					else
					{
						ParseMessageError("Невалидно въведени дни за отпуск" + value);
					}
				}
				//parse and notify changes in days

			}
		}

		public int DecemberDays
		{
			get
			{
				return this.decDays;
			}
			set
			{
				if (MaxMonthDays.SimpleMode == true)
				{
					var oldValue = this.decDays;
					if (value > MaxMonthDays.MaxDecember)
					{
						this.decDays = MaxMonthDays.MaxDecember;
					}
					else if (value < 0)
					{
						this.decDays = 0;
					}
					else
					{
						this.decDays = value;
					}

					if (this.decDays != oldValue)
					{
						this.IsChanged = true;
					}
				}
				NotifyPropertyChanged("TotalLeftover");
			}
		}

		public bool ParseMonthDates(string monthDates)
		{
			return true;
		}

		//<telerik:GridViewDataColumn Header="Февруари" DataMemberBinding="{Binding February, Mode=TwoWay}"/>
		//<telerik:GridViewDataColumn Header="Дни" DataMemberBinding="{Binding FebruaryDays, Mode=TwoWay}"/>
		//<telerik:GridViewDataColumn Header="Март" DataMemberBinding="{Binding March, Mode=TwoWay}"/>
		//<telerik:GridViewDataColumn Header="Дни" DataMemberBinding="{Binding MarchDays, Mode=TwoWay}"/>
		//<telerik:GridViewDataColumn Header="Април" DataMemberBinding="{Binding April, Mode=TwoWay}" />
		//<telerik:GridViewDataColumn Header="Дни" DataMemberBinding="{Binding AprilDays, Mode=TwoWay}"/>
		//<telerik:GridViewDataColumn Header="Май" />
		//<telerik:GridViewDataColumn Header="Дни" />
		//<telerik:GridViewDataColumn Header="Юни" />
		//<telerik:GridViewDataColumn Header="Дни" />
		//<telerik:GridViewDataColumn Header="Юли" />
		//<telerik:GridViewDataColumn Header="Дни" />
		//<telerik:GridViewDataColumn Header="Август" />
		//<telerik:GridViewDataColumn Header="Дни" />
		//<telerik:GridViewDataColumn Header="Септември" />
		//<telerik:GridViewDataColumn Header="Дни" />
		//<telerik:GridViewDataColumn Header="Октомври" />
		//<telerik:GridViewDataColumn Header="Дни" />
		//<telerik:GridViewDataColumn Header="Ноември" />
		//<telerik:GridViewDataColumn Header="Дни" />
		//<telerik:GridViewDataColumn Header="Декември" />
		//<telerik:GridViewDataColumn Header="Дни" />

		private void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
	}
}
