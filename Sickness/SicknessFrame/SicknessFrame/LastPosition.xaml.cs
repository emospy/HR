using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using HRDataLayer;
using OfficeOpenXml;

namespace SicknessFrame
{
	/// <summary>
	/// Interaction logic for LastPosition.xaml
	/// </summary>
	public partial class LastPosition : Window
	{
		private string connS;
		public LastPosition(string cs)
		{
			InitializeComponent();
			this.dpFromDate.SelectedDate = this.dpToDate.SelectedDate = DateTime.Now;
			connS = cs;
			using (HRDataLayer.Entities data = new Entities(this.connS))
			{
				this.cmbAdministration.ItemsSource = data.HR_Newtree2.Where(a => a.par == 0 || a.par == a.id).ToList();
				this.cmbDirection.ItemsSource = null;
				this.cmbDepartment.ItemsSource = null;
				this.cmbSector.ItemsSource = null;
			}
		}

		private void cmbAdministration_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			using (HRDataLayer.Entities data = new Entities(this.connS))
			{
				var item = this.cmbAdministration.SelectedItem as HR_Newtree2;
				cmbDirection.ItemsSource = data.HR_Newtree2.Where(a => a.par == item.id).ToList();
				this.cmbDepartment.ItemsSource = null;
				this.cmbSector.ItemsSource = null;
			}
		}

		private void cmbDirection_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			using (HRDataLayer.Entities data = new Entities(this.connS))
			{
				var item = this.cmbDirection.SelectedItem as HR_Newtree2;
			    if (item != null)
			    {
			        this.cmbDepartment.ItemsSource = data.HR_Newtree2.Where(a => a.par == item.id).ToList();
			        this.cmbSector.ItemsSource = null;
			    }
			    else
			    {
			        this.cmbDepartment.ItemsSource = null;
                    this.cmbSector.ItemsSource = null;
                }
			}
		}

		private void cmbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			using (HRDataLayer.Entities data = new Entities(this.connS))
			{
				var item = this.cmbDepartment.SelectedItem as HR_Newtree2;
			    if (item != null)
			    {
			        this.cmbSector.ItemsSource = data.HR_Newtree2.Where(a => a.par == item.id).ToList();
			    }
			    else
			    {
			        this.cmbSector.ItemsSource = null;
			    }
			}
		}

		private void BtnGenerate_OnClick(object sender, RoutedEventArgs e)
		{
			using (Entities data = new Entities(this.connS))
			{
				bool lvl1 = false;
				bool lvl2 = false;
				bool lvl3 = false;
				bool lvl4 = false;
				
				//get selected department
				if (this.cmbSector.SelectedItem != null)
				{
					lvl4 = true;
				}
				else if (this.cmbDepartment.SelectedItem != null)
				{
					lvl3 = true;
				}
				else if (this.cmbDirection.SelectedItem != null)
				{
					lvl2 = true;
				}
				else if (this.cmbAdministration.SelectedItem != null)
				{
					lvl1 = true;
				}

				var result = from person in data.HR_Person
							   join assignment in data.HR_PersonAssignment on person.id equals assignment.parent into asses
							   from ass in asses.DefaultIfEmpty()
							   join fp3 in data.HR_FirmPersonal3 on ass.positionID equals fp3.id into fp3ss
							   from fp3s in fp3ss.DefaultIfEmpty()
							   select new StatisticsModel
							   {
								   Person = person,
								   Assignment = ass,
								   FP3 = fp3s,
								   Language = null,
								   Absence = null,
								   Penalty = null,
								   Fired = null,
								   MilitaryRangs = null,
							   };

				
					result = from prev in result
							   join military in data.HR_MilitaryRangs on prev.Person.id equals military.parent into militaryss
							   from mils in militaryss.DefaultIfEmpty()
							   select new StatisticsModel
							   {
								   Person = prev.Person,
								   Assignment = prev.Assignment,
								   FP3 = prev.FP3,
								   Language = prev.Language,
								   Absence = prev.Absence,
								   Penalty = prev.Penalty,
								   Fired = prev.Fired,
								   MilitaryRangs = mils
							   };

				result = result.Where(a => (a.MilitaryRangs.isactive == "1" || a.MilitaryRangs == null)&& a.Person.fired == 0 && a.Assignment.isActive == 1 );

				if (lvl1 || lvl2 || lvl3 || lvl4)
				{
					if (lvl4)
					{
						result = result.Where(a => a.Assignment.level4 == this.cmbSector.Text
													&& a.Assignment.level3 == this.cmbDepartment.Text
													&& a.Assignment.level2 == this.cmbDirection.Text
													&& a.Assignment.level1 == this.cmbAdministration.Text);
					}
					else if(lvl3)
					{
						result = result.Where(a => a.Assignment.level3 == this.cmbDepartment.Text
													&& a.Assignment.level2 == this.cmbDirection.Text
													&& a.Assignment.level1 == this.cmbAdministration.Text);
					}
					else if (lvl2)
					{
						result = result.Where(a => a.Assignment.level2== this.cmbDirection.Text
													&& a.Assignment.level1 == this.cmbAdministration.Text);
					}
					else if (lvl1)
					{
						result = result.Where(a => a.Assignment.level1 == this.cmbAdministration.Text);
					}
				}

				var lr = result.ToList();

				var resGroups = lr.GroupBy(a => a.Person.id);

				var finalResult = new List<StatisticsModel>();

				foreach (var pg in resGroups)
				{
					var lpg = pg.OrderByDescending(a => a.Assignment.assignedAt).ToList();
					var la = lpg.Last();

				    la.lstAssignments = data.HR_PersonAssignment.Where(a => a.parent == la.Person.id).ToList();
				    la.lstMilitaryRangs = data.HR_MilitaryRangs.Where(a => a.parent == la.Person.id).OrderBy(a => a.rangorderdate).ToList();
				    //if (la.lstAssignments.Count == 0)
				    //{
				    //    continue;
				    //}
				    var lar = la.lstAssignments.Last();
                    
					//if (lvl4)
					//{
					//	if (la.Assignment.level4 != this.cmbSector.Text)
					//	{
					//		continue;
					//	}
					//}
					//else if (lvl3)
					//{
					//	if (la.Assignment.level3 != this.cmbDepartment.Text)
					//	{
					//		continue;
					//	}
					//}
					//else if (lvl2)_
					//{
					//	if (la.Assignment.level2 != this.cmbDirection.Text)
					//	{
					//		continue;
					//	}
					//}
					//else if (lvl1)
					//{
					//	if (la.Assignment.level1 != this.cmbAdministration.Text)
					//	{
					//		continue;
					//	}
					//}
					for (int i = la.lstAssignments.Count - 2; i >= 0; i--)
					{
						if (la.lstAssignments[i].position != la.lstAssignments[i + 1].position)
						{
							break;
						}
						else
						{
							la.Assignment.assignedAt = la.lstAssignments[i].assignedAt;
						}
					}

                    for (int i = la.lstMilitaryRangs.Count - 2; i >= 0; i--)
                    {
                        if (la.lstMilitaryRangs[i].militaryrang != la.lstMilitaryRangs[i + 1].militaryrang)
                        {
                            break;
                        }
                        else
                        {
                            la.MilitaryRangs.rangorderdate = la.lstMilitaryRangs[i].rangorderdate;
                        }
                    }
				    if ((la.Assignment.assignedAt >= this.dpFromDate.SelectedDate.Value &&
				         la.Assignment.assignedAt <= this.dpToDate.SelectedDate.Value) ||
				        this.dpFromDate.SelectedDate.Value == this.dpToDate.SelectedDate.Value) 
				    {
				        finalResult.Add(la);
				    }
				}

				var fileName = "result.xlsx";
				FileInfo newFile = new FileInfo(fileName);
				if (newFile.Exists)
				{
					newFile.Delete(); // ensures we create a new workbook
					newFile = new FileInfo(fileName);
				}

				int currentRow = 2;

				using (ExcelPackage package = new ExcelPackage(newFile))
				{
					ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

					//result egn, ime, adm, otd, dep, sec, pos, assDate, rang
					worksheet.Cells[1, 1].Value = "ЕГН";
					worksheet.Cells[1, 2].Value = "Име";
					worksheet.Cells[1, 3].Value = "ОТДЕЛ";
					worksheet.Cells[1, 4].Value = "СЕКТОР";
					worksheet.Cells[1, 5].Value = "Група";
					worksheet.Cells[1, 6].Value = "Звено";
					worksheet.Cells[1, 7].Value = "Длъжност";
					worksheet.Cells[1, 8].Value = "Дата на заемане";
					worksheet.Cells[1, 9].Value = "Звание";
                    worksheet.Cells[1, 10].Value = "Дата на звание";

                    for (int i = 2; i < finalResult.Count + 2; i++)
				    {
				        var res = finalResult[i - 2];
				        worksheet.Cells[i, 1].Value = res.Person.egn;
                        worksheet.Cells[i, 2].Value = res.Person.name;
                        worksheet.Cells[i, 3].Value = res.Assignment.level1;
                        worksheet.Cells[i, 4].Value = res.Assignment.level2;
                        worksheet.Cells[i, 5].Value = res.Assignment.level3;
                        worksheet.Cells[i, 6].Value = res.Assignment.level4;
                        worksheet.Cells[i, 7].Value = res.Assignment.position;
                        worksheet.Cells[i, 8].Value = string.Format("{0:dd.MM.yyyy}",res.Assignment.assignedAt);
                        worksheet.Cells[i, 9].Value = (res.MilitaryRangs != null)?res.MilitaryRangs.militarydegree:"";
                        worksheet.Cells[i, 10].Value = (res.MilitaryRangs != null) ? string.Format("{0:dd.MM.yyyy}",res.MilitaryRangs.rangorderdate) : "";
                    }

                    package.Save();
				}

			    System.Diagnostics.Process.Start(fileName);
			}
		}
	}

	public class PG
	{
		public HR_Person per;
		public HR_PersonAssignment ass;
		public HR_MilitaryRangs mil;
	}

	public class StatisticsModel
	{
		public HR_Person Person;
		public HR_PersonAssignment Assignment;
		public HR_FirmPersonal3 FP3;
		public HR_LanguageLevel Language;
		public HR_Absence Absence;
		public HR_Penalty Penalty;
		public HR_MilitaryRangs MilitaryRangs;
		public HR_Fired Fired;
	    public List<HR_PersonAssignment> lstAssignments;
        public List<HR_MilitaryRangs> lstMilitaryRangs;
    }
}
