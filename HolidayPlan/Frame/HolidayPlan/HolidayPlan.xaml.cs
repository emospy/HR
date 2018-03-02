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
    public partial class HolidayPlanWindow : Window
    {
        private readonly string connectionString;
        private long currentYear;

        List<DataGridRow> lstEditedRows;
        List<HR_Person> lstPeople;
        List<HR_Year_Holiday> lstCurrentYearHoliday;
		List<HR_Year_Holiday> lstPastYearHoliday;
		List<HR_Year_Holiday> lstTotalYearHoliday;
        List<HR_PlannedHolidays> holidayQuery;

        List<HR_PersonAssignment> lstPersonAssignment;
		Entities dbBindingEntity;
        List<CurrentYearLeftover> lstCurrentYearLeftoverStatic;
        List<CurrentYearLeftover> lstCurrentYearLeftover;
        List<PastYearsLeftover> lstPastYearsLefotver;
        List<TotalLeftover> lstTotalLeftover;

        public HolidayPlanWindow(string connectionString)
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
        } 

        //Wpf components and datagrid init functions             
        private bool InitDataGrid()
        {
            try
            {
                this.lstEditedRows = new List<DataGridRow>();

                bool initResult = this.InitLists();
                
                if (initResult)
                {
                    initResult = this.GetPeopleInformation();
                }
                if (initResult)
                {
                    initResult = this.CalculateLeftOver();
                }
                if (initResult)
                {
                    initResult = this.AddDataBinding();
                }
                if (initResult)
                {
                    initResult = this.AddDataGridEventHandlers();
                }

                if (initResult)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\n" + exc.Source);
                return false;
            }              
        }
        private bool AddDataBinding()
        {
            try
            {
				this.dbBindingEntity = new Entities(this.connectionString);

                this.AddRowsToPlannedHoliday();

                this.holidayQuery = (from plannedHoliday in dbBindingEntity.HR_PlannedHolidays where plannedHoliday.Year == this.currentYear select plannedHoliday).ToList(); ;
            	this.holidayQuery = this.holidayQuery.OrderBy(p => p.HR_Person.name).ToList();

                this.DataGrid.ItemsSource = holidayQuery;
 
                foreach (HR_PlannedHolidays holiday in holidayQuery)
                {
                    this.UpdateLeftOver(holiday);
                }

                return true;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\n" + exc.Source);
                return false;
            }

        }
        private bool AddDataGridEventHandlers()
        {
            try
            {
                this.DataGrid.KeyDown += DataGrid_KeyDown;
                this.DataGrid.CellEditEnding += Grid_CellEditEnding;
                this.DataGrid.PreviewKeyUp += DataGrid_PreviewKeyUp;
                this.DataGrid.RowEditEnding += DataGrid_RowEditEnding;

                return true;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\n" + exc.Source);
                return false;
            }
        }
        private void InitLeftoverColumns()
        {
            this.dgcbCurentYearLeftoverStatic.ItemsSource = this.lstCurrentYearLeftoverStatic;
            this.dgcbCurrentYeaLeftover.ItemsSource = this.lstCurrentYearLeftover;
            this.dgcbPastYearsLeftover.ItemsSource = this.lstPastYearsLefotver;
            this.dgcbTotaLeftover.ItemsSource = this.lstTotalLeftover; 
        }
        private void ResetRowsBackGroundColor()
        {
            foreach (DataGridRow row in this.lstEditedRows)
            {
                row.Background = Brushes.White;
            }

            this.lstEditedRows.Clear();
        }
        private bool InitLists()
        {
            try 
            {
				using (Entities db = new Entities(this.connectionString))
                {   
                    this.currentYear = (from y in db.HR_Year select y).First().Year;
                    
                    this.lstPersonAssignment = (from personAssignment in db.HR_PersonAssignment select personAssignment).ToList();
                    this.lstPeople = (from personInfo in db.HR_Person select personInfo).ToList();
                    this.lstCurrentYearHoliday = (from yearHoliday in db.HR_Year_Holiday where (yearHoliday.year == currentYear) select yearHoliday).ToList();
					this.lstPastYearHoliday = (from yearHoliday in db.HR_Year_Holiday where (yearHoliday.year < currentYear) select yearHoliday).ToList();
					this.lstTotalYearHoliday = (from yearHoliday in db.HR_Year_Holiday select yearHoliday).ToList();
                }

                return true;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\n" + exc.Source);
                return false;
            }  
        }
        private bool GetPeopleInformation()
        {
            try
            {
				using (Entities db = new Entities(this.connectionString))
				{
					var persons = (from person in db.HR_Person
					               join pa in db.HR_PersonAssignment on person.id equals pa.parent into pas
					               from paa in pas.DefaultIfEmpty()
					               join fi in db.HR_Fired on person.id equals fi.parent into fis
					               from fii in fis.DefaultIfEmpty()
					               where fii.FromDate.Value.Year == currentYear
					                     || paa.isActive == 1
					               orderby person.name
					               select person).ToList();

					var personQuery = (from personAssignment in this.lstPersonAssignment
					                   where (personAssignment.isActive == 1)
					                   from personInfo in this.lstPeople
					                   where (personInfo.id == personAssignment.parent)
					                   select new
						                          {
							                          Name = personInfo.name,
							                          EGN = personInfo.egn,
							                          ID = personInfo.id,
						                          }).ToList();

					var mixedQuery = (from personInfo in personQuery
					                  orderby personInfo.Name
					                  select new
						                         {
							                         Name = personInfo.Name,
							                         EGN = personInfo.EGN,
							                         ID = personInfo.ID,
						                         }).ToList();

					this.dgcbNames.ItemsSource = mixedQuery;
					this.dgcbEGN.ItemsSource = mixedQuery;
				}
				return true;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\n" + exc.Source);
                return false;
            }

        }
        private bool CalculateLeftOver()
        {
            try
            {
                this.lstCurrentYearLeftoverStatic = (from currentYear in this.lstCurrentYearHoliday
                                                     select new CurrentYearLeftover((int)currentYear.parent, (int)currentYear.leftover)).ToList();

                this.lstCurrentYearLeftover = (from currentYear in this.lstCurrentYearHoliday
                                               select new CurrentYearLeftover((int)currentYear.parent, (int)currentYear.leftover)).ToList();

                this.lstPastYearsLefotver = (from pastYears in this.lstPastYearHoliday
                                             group pastYears by pastYears.parent into groups
                                             select new PastYearsLeftover((int)groups.Key, (int)groups.Sum(pastYear => pastYear.leftover))).ToList();

                this.lstTotalLeftover = (from pastYears in this.lstTotalYearHoliday
                                         group pastYears by pastYears.parent into groups
                                         select new TotalLeftover((int)groups.Key, (int)groups.Sum(pastYear => pastYear.leftover))).ToList();


                this.InitLeftoverColumns();
                return true;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\n" + exc.Source);
                return false;
            }
        }
        private void AddRowsToPlannedHoliday()
        {
            try
            {
                bool newRowsAdded = false;
                bool found = false;

                List<HR_PersonAssignment> personQuery = (from personInfo in this.lstPeople from personAssignment in this.lstPersonAssignment where (personAssignment.isActive == 1 && personAssignment.parent == personInfo.id) select personAssignment).ToList();
				List<HR_PlannedHolidays> holidayQuery = (from plannedHoliday in dbBindingEntity.HR_PlannedHolidays where plannedHoliday.Year == currentYear select plannedHoliday).ToList();

                foreach (HR_PersonAssignment personInfo in personQuery)
                {
					foreach (HR_PlannedHolidays plannedHoliday in holidayQuery)
                    {
                        if (plannedHoliday.par == personInfo.parent)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (found == false)
                    {
						HR_PlannedHolidays newRow = new HR_PlannedHolidays
                                                    {
                                                        par = personInfo.parent, 
                                                        Year = (int)currentYear
                                                    };

						this.dbBindingEntity.HR_PlannedHolidays.AddObject(newRow);
                        newRowsAdded = true;
                    }

                    found = false;
                }

                if (newRowsAdded)
                {
                    this.dbBindingEntity.SaveChanges();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\n" + exc.Source);
            }
        }

        //Logic
        private void UpdateLeftOver(HR_PlannedHolidays plannedHoliday)
        {
            try
            {
                if (plannedHoliday != null)
                {
                    int personID = (int)plannedHoliday.par;
                    int daysSum = this.CalculatePlannedDays(plannedHoliday);

                    CurrentYearLeftover currentYear = this.FindCurrentYearLeftoverObject(personID);
                    PastYearsLeftover pastYears = this.FindPastYearsLeftoverObject(personID);
                    TotalLeftover totalLeftover = this.FindTotalLeftoverObject(personID);

                    //Calculate totalleftver
                    if (totalLeftover != null)
                    {
                        totalLeftover.Leftover = totalLeftover.LeftoverOriginal - daysSum;
                    }

                    if (currentYear != null)// <- Defensive Code, not to be confused with the logic below !!!
                    {
                        if (currentYear.LeftoverOriginal > daysSum) //If the person has enough days for this year - subtract 
                        {
                            currentYear.Leftover = currentYear.LeftoverOriginal - daysSum;
                            if (pastYears != null)
                            {
                                pastYears.Leftover = pastYears.LeftoverOriginal;
                            }
                        }
                        else if (currentYear.LeftoverOriginal <= daysSum)
                        {
                            if (pastYears != null) //If the person doesnt have enough days for this year - set the current year leftover to 0, and subtract the rest from past years
                            {
                                currentYear.Leftover = 0;
                                pastYears.Leftover = pastYears.LeftoverOriginal - (daysSum - currentYear.LeftoverOriginal);
                            }
                            else if( pastYears == null )//If there are not past years - a negative number will show the shortage of days
                            {
                                currentYear.Leftover = currentYear.LeftoverOriginal - daysSum;
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\n" + exc.Source);
            }
                           
        }
        private CurrentYearLeftover FindCurrentYearLeftoverObject(int personID)
        {
            CurrentYearLeftover found = null;

            if (this.lstCurrentYearLeftover != null)
            {
                foreach (CurrentYearLeftover currentYear in this.lstCurrentYearLeftover)
                {
                    if (personID == currentYear.PersonID)
                    {
                        found = currentYear;
                        break;
                    }
                }
            }

            return found;
        }
        private PastYearsLeftover FindPastYearsLeftoverObject(int personID)
        {
            PastYearsLeftover found = null;
            if (this.lstPastYearsLefotver != null)
            {
                foreach (PastYearsLeftover pastYears in this.lstPastYearsLefotver)
                {
                    if (personID == pastYears.PersonID)
                    {
                        found = pastYears;
                        break;
                    }
                }
            }
            return found;
        }
        private TotalLeftover FindTotalLeftoverObject(int personID)
        {
            TotalLeftover found = null;
            if (this.lstTotalLeftover != null)
            {
                foreach (TotalLeftover totalLeftover in this.lstTotalLeftover)
                {
                    if (personID == totalLeftover.PersonID)
                    {
                        found = totalLeftover;
                        break;
                    }
                }
            }
            return found;
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
            bool result = this.InitDataGrid();

            if (result != true)
            {
                this.Close();
            }
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                this.dbBindingEntity.Dispose();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\n" + exc.Source);
                this.Close();
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

			HR_PlannedHolidays plannedHoliday = this.DataGrid.SelectedItem as HR_PlannedHolidays;
            if (plannedHoliday != null)
            {
                this.UpdateLeftOver(plannedHoliday);
            }
        }
        private void btnUpdateData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.dbBindingEntity.SaveChanges();
                this.ResetRowsBackGroundColor();
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
                this.dbBindingEntity.Dispose();
                bool bindingResult = this.AddDataBinding();

                if (bindingResult == false)
                {
                    this.Close();
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\n" + exc.Source);
                this.Close();
            }
        }
        private void DataGrid_PreviewKeyUp(object sender, KeyEventArgs e)
        {
			HR_PlannedHolidays plannedHoliday = this.DataGrid.SelectedItem as HR_PlannedHolidays;
            if (plannedHoliday != null)
            {
                this.UpdateLeftOver(plannedHoliday);
            }
        }
        private void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            e.Row.Background = Brushes.LightPink;
            if (this.lstEditedRows.Contains(e.Row) == false)
            {
                this.lstEditedRows.Add(e.Row);
            }
        }
       
        //Input Validation
        private void DataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if( (this.DataGrid.CurrentColumn.DisplayIndex % 2 ) == 0)
            {
                bool validationResult =  this.DigitAndSpecialSymbolsValidation(e);

                if (validationResult == false)
                {
                    e.Handled = true;
                }
            }else
            if ((this.DataGrid.CurrentColumn.DisplayIndex % 2) != 0)
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
            if ( Keyboard.IsKeyDown( Key.LeftShift ) || Keyboard.IsKeyDown( Key.RightShift ) )
            {
                return true;
            }
            else
            {
                return false;
            }
        }     

    }
}
