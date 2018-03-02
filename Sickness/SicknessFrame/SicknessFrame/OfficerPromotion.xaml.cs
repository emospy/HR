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
	public partial class OfficerPromotion : Window
	{
		private string connS;
		public OfficerPromotion(string cs)
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
							   select new OfficerPromotionModel
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
							   select new OfficerPromotionModel
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

				result = result.Where(a => (a.MilitaryRangs != null)&& a.Person.fired == 0 && a.Assignment.isActive == 1);

				

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

				var finalResult = new List<OfficerPromotionModel>();

				foreach (var pg in resGroups)
				{
					var lpg = pg.OrderByDescending(a => a.Assignment.assignedAt).ToList();
					var la = lpg.Last();

				    //la.lstAssignments = data.HR_PersonAssignment.Where(a => a.parent == la.Person.id).OrderBy(a => a.assignedAt).ToList();
				    la.lstMilitaryRangs = data.HR_MilitaryRangs.Where(a => a.parent == la.Person.id).OrderBy(a => a.rangorderdate).ToList();

				    //var lar = la.lstAssignments.Last();
				    bool IsOfficer = false;
				    bool HasBeenRegular = false;

				    if (la.lstMilitaryRangs.Last().rangweight > 5)
				    {
				        IsOfficer = true;
				    }
					for (int i = la.lstMilitaryRangs.Count - 1; i >= 0  && IsOfficer == true; i--)
					{
						if (la.lstMilitaryRangs[i].rangweight < 6)
						{

						    if (la.lstMilitaryRangs[i + 1].rangorderdate >= this.dpFromDate.SelectedDate.Value 
                                && la.lstMilitaryRangs[i + 1].rangorderdate < this.dpToDate.SelectedDate.Value)
						    {
                                HasBeenRegular = true;
						        la.PromotionDate = la.lstMilitaryRangs[i + 1].rangorderdate;
						    }
							break;
						}
					}
				    if (HasBeenRegular)
				    {
				        finalResult.Add(la);
				    }
				}

				var fileName = "result.xlsx";
				FileInfo newFile = new FileInfo(fileName);
				if (newFile.Exists)
				{
				    try
				    {
				        newFile.Delete(); // ensures we create a new workbook
				        newFile = new FileInfo(fileName);
				    }
				    catch (Exception ex)
				    {
				        MessageBox.Show("Моля затворете справката преди да генерирате нова.");
				        return;
				    }
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
					worksheet.Cells[1, 7].Value = "Актуална длъжност";
					worksheet.Cells[1, 8].Value = "Актуално звание";
                    worksheet.Cells[1, 9].Value = "Дата на повишаване";



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
                        worksheet.Cells[i, 8].Value = (res.MilitaryRangs != null)?res.MilitaryRangs.militarydegree:"";
                        worksheet.Cells[i, 9].Value = res.PromotionDate.Value.ToShortDateString();
                    }
                    package.Save();
				}

			    System.Diagnostics.Process.Start(fileName);
			}
		}
	}

    public class OfficerPromotionModel : StatisticsModel
    {
        public DateTime? PromotionDate;
    }
}
