using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using HRDataLayer;
using DataLayer;

namespace HolidayPlan
{
    /// <summary>
    /// Interaction logic for HolidayPlan.xaml Some fix
    /// </summary>
    public partial class HolidayPlanNewWindow : Window
    {
        private readonly string connectionString;
        //private long currentYear;

        List<HolidayPlanRow> lstHolidayPlanRows;
		Entities data;

		public HolidayPlanNewWindow(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) 
            {
                MessageBox.Show("Invalid Connection String!");
                this.Close();
            }
            else
            {
                this.connectionString = connectionString;
            }

            InitializeComponent();

			this.data = new Entities(connectionString);
			
        } 

        //Wpf components and datagrid init functions             
        
        private bool GetPeopleInformation()
        {
            try
            {
				using (Entities db = new Entities(this.connectionString))
				{
					//var persons = (from person in db.HR_Person
					//			   join pa in db.HR_PersonAssignment on person.id equals pa.parent into pas
					//			   from paa in pas.DefaultIfEmpty()
					//			   join fi in db.HR_Fired on person.id equals fi.parent into fis
					//			   from fii in fis.DefaultIfEmpty()
					//			   where fii.FromDate.Value.Year == currentYear
					//					 || paa.isActive == 1
					//			   orderby person.name
					//			   select person).ToList();

					//var personQuery = (from personAssignment in this.lstPersonAssignment
					//				   where (personAssignment.isActive == 1)
					//				   from personInfo in this.lstPeople
					//				   where (personInfo.id == personAssignment.parent)
					//				   select new
					//							  {
					//								  Name = personInfo.name,
					//								  EGN = personInfo.egn,
					//								  ID = personInfo.id,
					//							  }).ToList();

					//var mixedQuery = (from personInfo in personQuery
					//				  orderby personInfo.Name
					//				  select new
					//							 {
					//								 Name = personInfo.Name,
					//								 EGN = personInfo.EGN,
					//								 ID = personInfo.ID,
					//							 }).ToList();

					//this.dgcbNames.ItemsSource = mixedQuery;
					//this.dgcbEGN.ItemsSource = mixedQuery;
				}
				return true;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\n" + exc.Source);
                return false;
            }

        }

		private int CalculatePlannedDays(HR_PlannedHolidays plannedHoliday)
        {
            try
            {
                int daysSum = 0;

                if (plannedHoliday.JanDays != null)
                {
                    daysSum += (int)plannedHoliday.JanDays;
                }
                if (plannedHoliday.FebDays != null)
                {
                    daysSum += (int)plannedHoliday.FebDays;
                }
                if (plannedHoliday.MarDays != null)
                {
                    daysSum += (int)plannedHoliday.MarDays;
                }
                if (plannedHoliday.AprDays != null)
                {
                    daysSum += (int)plannedHoliday.AprDays;
                }
                if (plannedHoliday.MayDays != null)
                {
                    daysSum += (int)plannedHoliday.MayDays;
                }
                if (plannedHoliday.JunDays != null)
                {
                    daysSum += (int)plannedHoliday.JunDays;
                }
                if (plannedHoliday.JulDays != null)
                {
                    daysSum += (int)plannedHoliday.JulDays;
                }
                if (plannedHoliday.AugDays != null)
                {
                    daysSum += (int)plannedHoliday.AugDays;
                }
                if (plannedHoliday.SepDays != null)
                {
                    daysSum += (int)plannedHoliday.SepDays;
                }
                if (plannedHoliday.OctDays != null)
                {
                    daysSum += (int)plannedHoliday.OctDays;
                }
                if (plannedHoliday.NovDays != null)
                {
                    daysSum += (int)plannedHoliday.NovDays;
                }
                if (plannedHoliday.DecDays != null)
                {
                    daysSum += (int)plannedHoliday.DecDays;
                }

                return daysSum;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\n" + exc.Source);
                return 0;
            }  
        }
      
        //Start up / Closing events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
			this.dpYear.SelectedDate = DateTime.Now;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            try
            {
				if (((List<HolidayPlanRow>)this.dgPlanView.ItemsSource).Any(h => h.IsChanged == true))
				{
					if (MessageBox.Show("Направили сте изменения в данните. Наистина ли искате да ги загубите?", "Въпрос", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
					{
						
					}
					else
					{
						e.Cancel = true;
					}
				}
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\n" + exc.Source);
                
            }
        }

        //Event handlers
        private void Grid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {            
            var dataEntered = ((TextBox)e.EditingElement).Text;
            if (dataEntered.Length == 0)
            {
                ((TextBox)e.EditingElement).Text = "0";
            }

			//HR_PlannedHolidays plannedHoliday = this.DataGrid.SelectedItem as HR_PlannedHolidays;
			//if (plannedHoliday != null)
			//{
			//	this.UpdateLeftOver(plannedHoliday);
			//}
        }
        private void btnUpdateData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
				var ChangedHolidayPlans = ((List<HolidayPlanRow>)this.dgPlanView.ItemsSource).Where(h => h.IsChanged == true);
				foreach (var plan in ChangedHolidayPlans)
				{
					var hp = new HR_PlannedHolidays();
					hp.id_plannedHoliday = plan.id_plannedHoliday;

					this.data.HR_PlannedHolidays.Attach(hp);

					hp.Year = plan.Year;
					hp.Apr = plan.April;
					hp.AprDays = plan.AprilDays;
					hp.Aug = plan.August;
					hp.AugDays = plan.AugustDays;
					hp.Dec = plan.December;
					hp.DecDays = plan.DecemberDays;
					hp.Feb = plan.February;
					hp.FebDays = plan.FebruaryDays;
					hp.Jan = plan.January;
					hp.JanDays = plan.JanuaryDays;
					hp.Jul = plan.July;
					hp.JulDays = plan.JulyDays;
					hp.Jun = plan.June;
					hp.JunDays = plan.JuneDays;
					hp.Leftover = plan.TotalLeftover;
					hp.Mar = plan.March;
					hp.MarDays = plan.MarchDays;
					hp.May = plan.May;
					hp.MayDays = plan.MayDays;
					hp.Nov = plan.November;
					hp.NovDays = plan.NovemberDays;
					hp.Oct = plan.October;
					hp.OctDays = plan.OctoberDays;
					hp.par = plan.id_person;
					hp.Sep = plan.September;
					hp.SepDays = plan.SeptemberDays;
				}
	            this.data.SaveChanges();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\n" + exc.Source);
                this.Close();
            }
        }
        private void btnCancelChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
				this.data = new Entities(this.connectionString);
				this.dpYear_SelectedDateChanged(sender, null);

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\n" + exc.Source);
                this.Close();
            }
        }
        
        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            e.Row.Background = Brushes.LightPink;
			//if (this.lstEditedRows.Contains(e.Row) == false)
			//{
			//	this.lstEditedRows.Add(e.Row);
			//}
        }
       
        //Input Validation
        private void DataGrid_KeyDown(object sender, KeyEventArgs e)
        {
			if ((this.dgPlanView.CurrentColumn.DisplayIndex % 2) == 1)
			{
				bool validationResult = this.DigitAndSpecialSymbolsValidation(e);

				if (validationResult == false)
				{
					e.Handled = true;
				}
			}
			else
				if ((this.dgPlanView.CurrentColumn.DisplayIndex % 2) == 0)
				{
					bool validationResult = this.DigitValidation(e);

					if (validationResult == false)
					{
						e.Handled = true;
					}
				}
        }

		private bool DigitValidation(KeyEventArgs e)
		{
			bool validationResult;

			if (this.IsShiftPressed() == false)
			{
				bool isDigit = (e.Key >= Key.D0) && (e.Key <= Key.D9);
				bool isNumPadKey = (e.Key >= Key.NumPad0) && (e.Key <= Key.NumPad9) && Keyboard.IsKeyToggled(Key.NumLock);

				if (isDigit || isNumPadKey)
				{
					validationResult = true;
				}
				else
				{
					validationResult = false;
				}

				return validationResult;
			}
			else
			{
				return false;
			}
		}

		private bool DigitAndSpecialSymbolsValidation(KeyEventArgs e)
		{
			bool validationResult;

			bool isDigit = this.DigitValidation(e);
			bool isSpecialKey = (e.Key == Key.OemComma) || (e.Key == Key.OemMinus);

			if (isDigit || isSpecialKey)
			{
				validationResult = true;
			}
			else
			{
				validationResult = false;
			}

			return validationResult;
		}

		private bool IsShiftPressed()
		{
			if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private void dpYear_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.dpYear.SelectedDate == null)
			{
				return;
			}
			var lstMonths = CalendarRow.CalculateMaxMonthWorkdays(connectionString, DateTime.Now.Year);

			MaxMonthDays.MaxJanuary = lstMonths[0];
			MaxMonthDays.MaxFebryary = lstMonths[1];
			MaxMonthDays.MaxMarch = lstMonths[2];
			MaxMonthDays.MaxApril = lstMonths[3];
			MaxMonthDays.MaxMay = lstMonths[4];
			MaxMonthDays.MaxJune = lstMonths[5];
			MaxMonthDays.MaxJuly = lstMonths[6];
			MaxMonthDays.MaxAugust = lstMonths[7];
			MaxMonthDays.MaxSeptember = lstMonths[8];
			MaxMonthDays.MaxOctober = lstMonths[9];
			MaxMonthDays.MaxNovember = lstMonths[10];
			MaxMonthDays.MaxDecember = lstMonths[11];

			//reload list of persons and data.
			//load all people that have been assigned in the previous year plus all employees that have been assigned in the curent year.

			var lstPeople = (from person in this.data.HR_Person
							  join pa in this.data.HR_PersonAssignment on person.id equals pa.parent into pas
											   from paa in pas.DefaultIfEmpty()
							  join fi in this.data.HR_Fired on person.id equals fi.parent into fis
											   from fii in fis.DefaultIfEmpty()
									where fii.FromDate.Value.Year >= this.dpYear.SelectedDate.Value.Year - 1
									|| paa.isActive == 1
									orderby person.name
									select new{person, pas}).ToList();

			var lstPeopleGrouped = new List<PeopleAndAssignments>();
			var lstPeopleGroups = lstPeople.GroupBy(p => p.person.egn);
			foreach (var group in lstPeopleGroups)
			{
				var p = new PeopleAndAssignments();
				var lastGroup = group.Last();

				p.person = lastGroup.person;
				p.lstAssignments = lastGroup.pas.ToList();
				lstPeopleGrouped.Add(p);
			}

			this.lstHolidayPlanRows = new List<HolidayPlanRow>();

			var lstHolidayPlans = this.data.HR_PlannedHolidays.Where(h => h.Year == this.dpYear.SelectedDate.Value.Year || h.Year == this.dpYear.SelectedDate.Value.Year - 1).ToList();

			foreach (var person in lstPeopleGrouped)
			{
				HolidayPlanRow hol = new HolidayPlanRow();
				hol.id_person = person.person.id;
				hol.EGN = person.person.egn;
				hol.Name = person.person.name;
				hol.ParseMessageError += this.ShowMessage;
				hol.Year = this.dpYear.SelectedDate.Value.Year;
				hol.connString = this.connectionString;

				HR_PersonAssignment currentAssignment;
				currentAssignment = person.lstAssignments.Last();

				int nh = 0;
				int.TryParse(currentAssignment.NumHoliday, out nh);
				int ah;
				ah = (currentAssignment.AdditionalHoliday != null) ? (int)currentAssignment.AdditionalHoliday : 0;
				hol.Total = ah + nh;

				var planFromDB = lstHolidayPlans.Find(h => h.par == person.person.id && h.Year == this.dpYear.SelectedDate.Value.Year);
				if (planFromDB == null)
				{
					HR_PlannedHolidays h = new HR_PlannedHolidays();
					h.par = person.person.id;
					h.Year = this.dpYear.SelectedDate.Value.Year;
					this.data.HR_PlannedHolidays.AddObject(h);
					lstHolidayPlans.Add(h);
					this.data.SaveChanges();

					hol.id_plannedHoliday = h.id_plannedHoliday;
					hol.JanuaryDays = hol.JanuaryDays = 0; // to list all other months
				}
				else
				{
					hol.id_plannedHoliday = planFromDB.id_plannedHoliday;
					hol.JanuaryDays = (planFromDB.JanDays == null)? 0 : (int)planFromDB.JanDays;
					hol.January = planFromDB.Jan;
					hol.FebruaryDays = (planFromDB.FebDays == null) ? 0 : (int)planFromDB.FebDays;
					hol.February = planFromDB.Feb;
					hol.MarchDays = (planFromDB.MarDays == null) ? 0 : (int)planFromDB.MarDays;
					hol.March = planFromDB.Mar;
					hol.AprilDays = (planFromDB.AprDays == null) ? 0 : (int)planFromDB.AprDays;
					hol.April = planFromDB.Apr;
					hol.MayDays = (planFromDB.MayDays == null) ? 0 : (int)planFromDB.MayDays;
					hol.May = planFromDB.May;
					hol.JuneDays = (planFromDB.JunDays == null) ? 0 : (int)planFromDB.JunDays;
					hol.June = planFromDB.Jun;
					hol.JulyDays = (planFromDB.JulDays == null) ? 0 : (int)planFromDB.JulDays;
					hol.July = planFromDB.Jul;
					hol.AugustDays = (planFromDB.AugDays == null) ? 0 : (int)planFromDB.AugDays;
					hol.August = planFromDB.Aug;
					hol.SeptemberDays = (planFromDB.SepDays == null) ? 0 : (int)planFromDB.SepDays;
					hol.September = planFromDB.Sep;
					hol.OctoberDays = (planFromDB.OctDays == null) ? 0 : (int)planFromDB.OctDays;
					hol.October = planFromDB.Oct;
					hol.NovemberDays = (planFromDB.NovDays == null) ? 0 : (int)planFromDB.NovDays;
					hol.November = planFromDB.Nov;
					hol.DecemberDays = (planFromDB.DecDays == null) ? 0 : (int)planFromDB.DecDays;
					hol.December = planFromDB.Dec;
				}

				var prevHolPlan = lstHolidayPlans.Find(h => h.Year == this.dpYear.SelectedDate.Value.Year - 1);
				if (prevHolPlan != null)
				{
					hol.PrevYearLeftover = (prevHolPlan.Leftover != null) ? (int)prevHolPlan.Leftover : 0;
				}

				var leftover = hol.Total + hol.PrevYearLeftover - hol.JanuaryDays - hol.FebruaryDays - hol.MarchDays - hol.AprilDays - hol.MayDays - hol.JuneDays - hol.JulyDays - hol.AugustDays - hol.SeptemberDays - hol.OctoberDays - hol.NovemberDays - hol.DecemberDays; //.. continue till the end of time
				
				hol.TotalLeftover = leftover;

				this.lstHolidayPlanRows.Add(hol);
			}

			this.dgPlanView.ItemsSource = this.lstHolidayPlanRows;
			//Do not allow editing of previous years (maybe)
		}

		private void ShowMessage(string message)
		{
			MessageBox.Show(message);
		}
    }
}
