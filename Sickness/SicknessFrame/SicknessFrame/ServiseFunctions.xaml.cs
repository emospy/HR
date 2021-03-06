﻿
using HRDataLayer;
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
using Microsoft.Office.Interop.Excel;
using Microsoft.Win32;
using Window = System.Windows.Window;
using System.IO;
using OfficeOpenXml;

namespace SicknessFrame
{
	/// <summary>
	/// Interaction logic for ServiseFunctions.xaml
	/// </summary>
	public partial class ServiseFunctions : Window
	{
		HRDataLayer.Entities data;
		string connstr;

		public ServiseFunctions(string connstring)
		{
			InitializeComponent();
			this.data = new HRDataLayer.Entities(connstring);
			this.connstr = connstring;
		}

		private void btnFixGlobalPositions_Click(object sender, RoutedEventArgs e)
		{
			var lstPositionsFP3 = this.data.HR_FirmPersonal3.Select(p => p).ToList();
			var lstGlobalPosition = this.data.HR_GlobalPositions.Select(p => p).ToList();
			var lstMissingPositions = new List<HR_FirmPersonal3>();
			var lstMultiplePositions = new List<HR_FirmPersonal3>();
			int fixes = 0;
			int nkpFixes = 0;

			foreach (var fp in lstPositionsFP3)
			{
				if (fp.globalpositionid == null | fp.globalpositionid == 0)
				{
					//find position match or list it as a hanging position
					var positions = lstGlobalPosition.Where(p => p.PositionName == fp.nameOfPosition).ToList();
					if (positions.Count == 0)
					{
						lstMissingPositions.Add(fp);
					}
					else if (positions.Count == 1)
					{
						var pos = positions.First();
						fp.nKPlevel = pos.NKPLevel;
						fp.NKPCode = pos.NKPCode;
						fp.NKPClass = pos.NKPClass;
						fp.positioneng = pos.engposition;
						fixes++;
						data.SaveChanges();
					}
					else
					{
						var lstFilteredPositions = positions.Where(p => p.NKPCode == fp.NKPCode).ToList();
						if (lstFilteredPositions.Count == 0)
						{
							lstMissingPositions.Add(fp);
						}
						else if (lstFilteredPositions.Count == 1)
						{
							var pos = lstFilteredPositions.First();
							fp.nKPlevel = pos.NKPLevel;
							fp.NKPCode = pos.NKPCode;
							fp.NKPClass = pos.NKPClass;
							fp.positioneng = pos.engposition;
							data.SaveChanges();
							fixes++;
						}
						else
						{
							lstMultiplePositions.Add(fp);
						}
					}
				}
				else
				{
					//check if name and NKP match - if not - fix them
					var pos = lstGlobalPosition.FirstOrDefault(p => p.id == fp.globalpositionid);
					if (fp.nameOfPosition != pos.PositionName || fp.NKPCode != pos.NKPCode || fp.nKPlevel != pos.NKPLevel || fp.NKPClass != pos.NKPClass || fp.positioneng != pos.engposition)
					{
						fp.nameOfPosition = pos.PositionName;
						fp.nKPlevel = pos.NKPLevel;
						fp.NKPCode = pos.NKPCode;
						fp.NKPClass = pos.NKPClass;
						fp.positioneng = pos.engposition;
						data.SaveChanges();
						nkpFixes++;
					}
				}
			}
			MessageBox.Show("НКП корекции " + nkpFixes);
			MessageBox.Show("Корекции по длъжности" + fixes);
			MessageBox.Show("Развързани длъжности");
			MessageBox.Show("Неопределени длъжности");
		}

		private void btnFixDVS_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Title = "Изберете файл за импорт на данни";
			ofd.Filter = "Excel Files (*.xlsx)|*.xlsx| Excel Files (*.xls)|*.xls| All Files (*.*)|*.*";
			ofd.ShowDialog();

			Worksheet xlsheet;
			Workbook xlwkbook;

			xlwkbook = (Workbook)System.Runtime.InteropServices.Marshal.BindToMoniker(ofd.FileName);
			xlsheet = (Worksheet)xlwkbook.ActiveSheet;

			//Range oRng;
			data = new Entities(this.connstr);
			//string level;

			string Message = "";
			Range excelRange = xlsheet.UsedRange;
			//get an object array of all of the cells in the worksheet (their values)
			object[,] valueArray = (object[,])excelRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);
			for (int i = 2; i <= excelRange.Rows.Count; i++)
			{
				string egn = valueArray[i, 1]?.ToString();
				var per = data.HR_Person.Where(a => a.fired == 0 && a.egn == egn).ToList();
				if (per == null || per.Count == 0)
				{
					Message += "Служител " + egn + " не е намерен\n";
					continue;
				}
				else if (per.Count > 1)
				{
					Message += "Служител " + egn + " има повече от едно досие\n";
					continue;
				}
				var person = per.FirstOrDefault();
				string cn, cs, cz;
				cn = valueArray[i, 15].ToString();
				cs = valueArray[i, 16].ToString();
				cz = valueArray[i, 17].ToString();
				var lstSame =
					data.HR_Cards.Where(a => a.CardNumber == cn && a.CardSeries == cs && a.CardSign == cz).ToList();

				if (lstSame.Count > 0)
				{
					Message += "Карта на " + egn + " с такива атрибути вече е издавана\n";
				}
				else
				{
					var card = new HR_Cards();
					DateTime cd = new DateTime(1970, 1, 1);
					var dateStr = valueArray[i, 14].ToString();
					DateTime.TryParse(dateStr, out cd);
					if (person.engname == null || person.engname == string.Empty)
					{
						person.engname = valueArray[i, 9]?.ToString() + " " + valueArray[i, 10]?.ToString() + " " +
										 valueArray[i, 11]?.ToString() + valueArray[i, 12]?.ToString();

					}
					card.CardIssueDate = cd;
					card.parent = person.id;
					card.CardNumber = cn;
					card.CardSeries = cs;
					card.CardSign = cz;
					card.MilitaryDegree = valueArray[i, 2].ToString();
					card.MilitaryDegreeEng = valueArray[i, 3].ToString();
					card.isactive = true;

					var lstCards = data.HR_Cards.Where(a => a.parent == person.id).ToList();
					foreach (var ca in lstCards)
					{
						ca.isactive = false;
					}

					var picPath = ofd.FileName.Substring(0, ofd.FileName.Length - ofd.SafeFileName.Length) +
							  valueArray[i, 13].ToString();
					byte[] picture;
					try
					{
						picture = File.ReadAllBytes(picPath);
						var photo = data.HR_Pictures.FirstOrDefault(a => a.parent == person.id);
						if (photo != null)
						{
							//replace the photo
							photo.picture = picture;
						}
						else
						{
							//create new photo
							photo = new HR_Pictures();
							photo.parent = person.id;
							photo.picture = picture;
							data.HR_Pictures.AddObject(photo);
						}
					}
					catch (Exception)
					{
						Message += "Снимка " + valueArray[i, 13].ToString() + " не е намерена";
					}

					data.HR_Cards.AddObject(card);
					data.SaveChanges();
				}

			}
			if (Message.Length > 0)
			{
				MessageBox.Show(Message);
			}
		}

		private void btnFixAbsence_Click(object sender, RoutedEventArgs e)
		{
			var lstYearHoliday = this.data.HR_Year_Holiday.Select(a => a);
		}

		private void btnFixAssignmentDate_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Title = "Изберете файл за импорт на данни";
			ofd.Filter = "Excel Files (*.xls)|*.xls| Excel Files (*.xlsx)|*.xlsx| All Files (*.*)|*.*";
			ofd.ShowDialog();

			Worksheet xlsheet;
			Workbook xlwkbook;

			xlwkbook = (Workbook)System.Runtime.InteropServices.Marshal.BindToMoniker(ofd.FileName);
			xlsheet = (Worksheet)xlwkbook.ActiveSheet;

			//Range oRng;

			//string level;

			Range excelRange = xlsheet.UsedRange;
			//get an object array of all of the cells in the worksheet (their values)
			object[,] valueArray = (object[,])excelRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);

			List<string> lstMissingPersons = new List<string>();

			for (int i = 1; i < excelRange.Rows.Count; i++)
			{
				int numb;
				if (valueArray[i, 1] != null)
				{
					if (int.TryParse(valueArray[i, 1].ToString(), out numb))
					{
						if (int.TryParse(valueArray[i, 3].ToString(), out numb))
						{
							string name = valueArray[i, 2].ToString();
							int startYear = numb;

							var lstPersons = this.data.HR_Person.Where(p => p.name == name && p.fired == 0).ToList();
							if (lstPersons.Count == 0)
							{
								lstMissingPersons.Add(name);
							}
							foreach (var p in lstPersons)
							{
								var firstAssignment = this.data.HR_PersonAssignment.FirstOrDefault(a => a.parent == p.id && a.IsAdditionalAssignment == 0);
								if (firstAssignment != null)
								{
									if (firstAssignment.assignedAt.HasValue)
									{
										if (firstAssignment.assignedAt.Value.Year != startYear)
										{
											firstAssignment.assignedAt = new DateTime(startYear, firstAssignment.assignedAt.Value.Month, firstAssignment.assignedAt.Value.Day);
										}

									}
								}
								p.languages = "синдикален член";
								data.SaveChanges();
							}
						}
					}
				}
			}
		}

		private void btnFixFirstAssignment_Click(object sender, RoutedEventArgs e)
		{
			var fullJoin = (from person in data.HR_Person
							join assignment in data.HR_PersonAssignment on person.id equals assignment.parent into asses
							from ass in asses.DefaultIfEmpty()
							where ass.parent == person.id
							&& ass.IsAdditionalAssignment == 0
							&& person.fired == 0
							select new
							{
								Person = person,
								Assignment = ass
							}).ToList();
			int counter = 0;
			foreach (var per in fullJoin)
			{
				if (per.Person.hiredAt != per.Assignment.assignedAt)
				{
					per.Person.hiredAt = per.Assignment.assignedAt;
					counter++;
				}
			}
			this.data.SaveChanges();
			MessageBox.Show(counter.ToString());
		}

		private void btnFixSicknessMessages_Click(object sender, RoutedEventArgs e)
		{
			var lstMessages = this.data.HR_Messages.Where(a => a.HR_MessageInstances.HR_MessageTypes.id_messageType == 7).OrderBy(a => a.id_message).ToList();

			var groups = lstMessages.GroupBy(a => a.id_person);

			foreach (var group in groups)
			{
				var og = group.OrderBy(g => g.id_message);
				int i = 0;
				foreach (var pm in og)
				{
					if (i != 0)
					{
						this.data.DeleteObject(pm);
					}
					i++;
				}
			}

			data.SaveChanges();
		}

		private void btnFixShumenHolidays_Click(object sender, RoutedEventArgs e)
		{
			List<string> lstnames = new List<string>();

			List<CheckHolidayModel> lstModels = new List<CheckHolidayModel>();
			var Year = DateTime.Now.Year.ToString();

			var personsRaw = (from p in this.data.HR_Person
							  join a in this.data.HR_PersonAssignment on p.id equals a.parent

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
				var firstAssignment = this.data.HR_PersonAssignment.FirstOrDefault(a => a.parent == person.p.id && a.IsAdditionalAssignment == 0);
				if (firstAssignment == null)
				{
					return;
				}

				var lastAssignment = person.a;
				if (lastAssignment == null)
				{
					return;
				}

				var PYH = this.data.HR_Year_Holiday.Where(p => p.parent == pid).FirstOrDefault();

				if (PYH == null)
				{
					continue;
				}

				var refDate = new DateTime(2014, 12, 31);
				var years = refDate.Year - firstAssignment.assignedAt.Value.Year;

				var holidays = this.data.HR_Absence.Where(a => a.Year == Year && a.parent == person.p.id && a.typeAbsence == "Полагаем годишен отпуск");

				int? used = holidays.Sum(a => (int?)a.countDays);

				if (used == null)
				{
					used = 0;
				}

				if (lastAssignment.position.ToLower().Contains("асистент")
						|| lastAssignment.position.ToLower().Contains("доцент")
						|| lastAssignment.position.ToLower().Contains("професор")
						|| lastAssignment.position.ToLower().Contains("преподавател"))
				{
					if (person.p.languages.ToLower() != "синдикален член")
					{
						int Nh = 0, ah = 0;
						if (lastAssignment.AdditionalHoliday != null)
						{
							ah = (int)lastAssignment.AdditionalHoliday;
						}
						int.TryParse(lastAssignment.NumHoliday, out Nh);

						lastAssignment.NumHoliday = "48";
						lastAssignment.AdditionalHoliday = 0;
					}
					else
					{
						int Nh = 0, ah = 0;
						if (lastAssignment.AdditionalHoliday != null)
						{
							ah = (int)lastAssignment.AdditionalHoliday;
						}
						int.TryParse(lastAssignment.NumHoliday, out Nh);

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

				if (firstAssignment.assignedAt.Value.Year == 2014)
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
						fixCounter++;
						PYH.total = left;
						PYH.leftover = cl;
					}
				}
				else
				{
					var cl = (int)total - (int)used;
					if (PYH.total != total || PYH.leftover != cl)
					{
						fixCounter++;
						PYH.total = total;
						PYH.leftover = cl;
					}
				}
				data.SaveChanges();
			}
		}

		private void btnFixShumenExperience_Click(object sender, RoutedEventArgs e)
		{
			var Now = new DateTime(2015, 1, 1);
			var conold = connstr.Replace("database=HRShumen".ToLower(), "database=HRShumenBefore".ToLower());
			var dataOld = new Entities(conold);



			var lstEmployeeAssignmentsNew = this.data.HR_PersonAssignment.Where(a => a.IsAdditionalAssignment == 0 && a.HR_Person.fired == 0).ToList();
			var lstEmployeeAssignmentsOld = dataOld.HR_PersonAssignment.Where(a => a.IsAdditionalAssignment == 0 && a.HR_Person.fired == 0).ToList();
			int fixCount = 0;
			foreach (var ass in lstEmployeeAssignmentsNew)
			{

				var oldAss = lstEmployeeAssignmentsOld.FirstOrDefault(a => a.parent == ass.parent);
				if (oldAss != null)
				{
					//int nwy= 0, nwm = 0, nwd = 0;
					//int noy = 0, nom = 0, nod = 0;
					if (oldAss.assignedAt != ass.assignedAt)
					{
						ass.days = ass.days - (oldAss.assignedAt.Value.Day - ass.assignedAt.Value.Day);
						if (ass.days < 0)
						{
							ass.days += 30;
							ass.months--;
						}
						ass.months = ass.months - (oldAss.assignedAt.Value.Month - ass.assignedAt.Value.Month);
						if (ass.months < 0)
						{
							ass.months += 12;
							ass.years--;
						}
						ass.years = ass.years - (oldAss.assignedAt.Value.Year - ass.assignedAt.Value.Year);


						int AssY, AssM, AssD, CYear, CDay, CMonth, TY, TM, TD;

						AssY = ass.assignedAt.Value.Year;
						AssM = ass.assignedAt.Value.Month;
						AssD = ass.assignedAt.Value.Day;
						CYear = Now.Year - AssY;
						if ((CMonth = Now.Month - AssM) < 0)
						{
							CYear--;
							CMonth += 12;
						}
						if ((CDay = Now.Day - AssD) <= 0)
						{
							CDay += 30;
							CMonth--;
							if (CMonth < 0)
							{
								CMonth += 12;
								CYear--;
							}
						}
						TY = TM = TD = 0;
						try
						{

							TY = CYear + (int)ass.years;
							TM = CMonth + (int)ass.months;
							TD = CDay + (int)ass.days;
						}
						catch
						{
						}
						if (TD >= 30)
						{
							TM++;
							TD -= 30;
						}
						if (TM >= 12)
						{
							TM -= 12;
							TY++;
						}



						//var tillNow = Now.Subtract(ass.assignedAt.Value);

						//nwd = (int)((tillNow.Days % 365) % 30) + (int)ass.days;
						//if (nwd > 30)
						//{
						//	nwd -= 30;
						//	nwm++;
						//}

						//nwm += (int)((tillNow.Days % 365) / 30) + (int)ass.months;
						//if (nwm > 11)
						//{
						//	nwm -= 11;
						//	nwy++;
						//}
						//nwy += (int)(tillNow.Days / 365) + (int)ass.years;

						//var tillOld = Now.Subtract(oldAss.assignedAt.Value);

						//nod = (int)((tillOld.Days % 365) % 30) + (int)oldAss.days;
						//if (nod > 30)
						//{
						//	nod -= 30;
						//	nom++;
						//}

						//nom += (int)((tillOld.Days % 365) / 30) + (int)oldAss.months;
						//if (nom > 11)
						//{
						//	nom -= 11;
						//	noy++;
						//}
						//noy += (int)(tillOld.Days / 365) + (int)oldAss.years;

						fixCount++;
					}
				}
			}
			data.SaveChanges();
		}

		private void btnFixNSOAddons_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Title = "Изберете файл за импорт на данни";
			ofd.Filter = "Excel Files (*.xls)|*.xls| Excel Files (*.xlsx)|*.xlsx| All Files (*.*)|*.*";
			ofd.ShowDialog();

			Worksheet xlsheet;
			Workbook xlwkbook;

			xlwkbook = (Workbook)System.Runtime.InteropServices.Marshal.BindToMoniker(ofd.FileName);
			xlsheet = (Worksheet)xlwkbook.ActiveSheet;



			Range excelRange = xlsheet.UsedRange;
			//get an object array of all of the cells in the worksheet (their values)
			object[,] valueArray = (object[,])excelRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);

			List<string> lstMissingPersons = new List<string>();

			for (int i = 7; i < excelRange.Rows.Count; i++)
			{
				if (valueArray[i, 1] != null)
				{
					var name = valueArray[i, 1].ToString();
					HR_Person person;
					try
					{
						person = data.HR_Person.Where(p => p.name == name && p.fired == 0).Single();
					}
					catch
					{
						MessageBox.Show(name);
						continue;
					}

					try
					{
						var ass = person.HR_PersonAssignment.Where(p => p.parent == person.id && p.isActive == 1).Single();
						double perc;
						double.TryParse(valueArray[i, 3].ToString(), out perc);
						ass.MonthlyAddon = ((perc * 100)).ToString();
						data.SaveChanges();
					}
					catch
					{
						MessageBox.Show(name);
						continue;
					}
				}
			}
		}

		private void btnNSOAddons_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Title = "Изберете файл за импорт на данни";
			ofd.Filter = "Excel Files (*.xls)|*.xls| Excel Files (*.xlsx)|*.xlsx| All Files (*.*)|*.*";
			ofd.ShowDialog();

			Worksheet xlsheet;
			Workbook xlwkbook;

			xlwkbook = (Workbook)System.Runtime.InteropServices.Marshal.BindToMoniker(ofd.FileName);
			xlsheet = (Worksheet)xlwkbook.ActiveSheet;

			Range excelRange = xlsheet.UsedRange;
			//get an object array of all of the cells in the worksheet (their values)
			object[,] valueArray = (object[,])excelRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);

			List<string> lstMissingPersons = new List<string>();

			for (int i = 7; i < excelRange.Rows.Count; i++)
			{
				if (valueArray[i, 1] != null)
				{
					var name = valueArray[i, 1].ToString();
					HR_Person person;
					try
					{
						person = data.HR_Person.Where(p => p.name == name && p.fired == 0).Single();
					}
					catch
					{
						MessageBox.Show(name);
						continue;
					}

					try
					{
						var ass = person.HR_PersonAssignment.Where(p => p.parent == person.id && p.isActive == 1).Single();
						double perc;
						double.TryParse(valueArray[i, 3].ToString(), out perc);
						ass.salaryAddon = (perc * 100).ToString();
						data.SaveChanges();
					}
					catch
					{
						MessageBox.Show(name);
						continue;
					}
				}
			}
		}

		private void BtnImportNSOKDA_OnClick(object sender, RoutedEventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Title = "Изберете файл за импорт на данни";
			ofd.Filter = "Excel Files (*.xls)|*.xls| Excel Files (*.xlsx)|*.xlsx| All Files (*.*)|*.*";
			ofd.ShowDialog();

			Worksheet xlsheet;
			Workbook xlwkbook;

			xlwkbook = (Workbook)System.Runtime.InteropServices.Marshal.BindToMoniker(ofd.FileName);
			xlsheet = (Worksheet)xlwkbook.ActiveSheet;



			Range excelRange = xlsheet.UsedRange;
			//get an object array of all of the cells in the worksheet (their values)
			object[,] valueArray = (object[,])excelRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);

			List<string> lstMissingPersons = new List<string>();

			for (int i = 2; i <= excelRange.Rows.Count; i++)
			{
				//int numb;
				if (valueArray[i, 1] != null)
				{
					var name = valueArray[i, 1].ToString();
					HR_Person person;
					try
					{
						person = data.HR_Person.Where(p => p.name == name && p.fired == 0).Single();
					}
					catch
					{
						MessageBox.Show(name);
						continue;
					}

					try
					{
						var ass = person.HR_PersonAssignment.Where(p => p.parent == person.id && p.isActive == 1).Single();
						double sal = 0;

						if (valueArray[i, 7] != null)
						{
							double.TryParse(valueArray[i, 7].ToString(), out sal);
							ass.baseSalary = sal;
						}
						if (valueArray[i, 6] != null)
						{
							int pd = 0;
							int.TryParse(valueArray[i, 6].ToString(), out pd);
							if (pd != 0)
							{
								ass.ekdaPayDegree = pd;
							}
						}

						var sp = this.data.HR_FirmPersonal3.FirstOrDefault(a => a.id == ass.positionID);
						if (sp == null)
						{
							continue;
						}
						var gp = this.data.HR_GlobalPositions.FirstOrDefault(a => a.id == sp.globalpositionid);
						if (valueArray[i, 5] != null)
						{
							sp.ekdaPayLEvel = valueArray[i, 5].ToString();

							if (gp == null)
							{
								continue;
							}
						}
						if (valueArray[i, 8] != null)
						{
							gp.Rang = valueArray[i, 8].ToString();
						}

						data.SaveChanges();
					}
					catch
					{
						MessageBox.Show(name);
						continue;
					}
				}
			}
		}

		private void btnImportStructure_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Title = "Изберете файл за импорт на данни";
			ofd.Filter = "Excel Files (*.xlsx)|*.xlsx| Excel Files (*.xls)|*.xls| All Files (*.*)|*.*";
			if (ofd.ShowDialog() == false)
			{
				return;
			}

			FileInfo file = new FileInfo(ofd.FileName);
			using (ExcelPackage package = new ExcelPackage(file))
			{
				this.data = new Entities(this.connstr);
				var lstGlobalPositions = data.HR_GlobalPositions.ToList();

				var wb = package.Workbook.Worksheets["Structure"];

				int end = wb.Dimension.End.Row;

				//usbale starts from row 1

				for (int i = 1; i <= end; i++)
				{
					if (wb.Cells[i, 2].Value == null)
					{
						continue;
					}

					char c = wb.Cells[i, 1].Value.ToString()[0];
					if (char.IsLetter(c))
					{
						this.ImportDepartment(null, ref i, wb, end, lstGlobalPositions);
					}
				}
			}
		}

		private void ImportDepartment(HR_Newtree2 parentDepartment, ref int cr, ExcelWorksheet wb, int end, List<HR_GlobalPositions> lstGlobalPositions)
		{
			var department = new HR_Newtree2();

			department.level = wb.Cells[cr, 2].Value.ToString();
			department.code = wb.Cells[cr, 1].Value.ToString();

			if (parentDepartment == null)
			{
				department.par = 0;
			}
			else
			{
				department.par = parentDepartment.id;
			}

			data.HR_Newtree2.AddObject(department);
			data.SaveChanges();
			cr++;

			for (; cr <= end; cr++)
			{
				if (wb.Cells[cr, 2].Value == null)
				{
					//cr++;
					return;
				}

				if (wb.Cells[cr, 1].Value != null)
				{
					this.ImportDepartment(department, ref cr, wb, end, lstGlobalPositions);
				}
				else
				{
					ImportPosition(cr, wb, lstGlobalPositions, department);
				}
			}
		}

		private void ImportPosition(int cr, ExcelWorksheet wb, List<HR_GlobalPositions> lstGlobalPositions, HR_Newtree2 dep)
		{
			var position = new HR_FirmPersonal3();
			position.nameOfPosition = wb.Cells[cr, 2].Value.ToString();
			var pn = position.nameOfPosition.Trim();
			var gp = lstGlobalPositions.FirstOrDefault(a => a.PositionName.Trim() == pn);
			if (gp == null)
			{
				MessageBox.Show("No global position fopund at row " + cr);
			}
			position.globalpositionid = gp.id;
			position.NKPCode = wb.Cells[cr, 3].Value?.ToString();
			position.Law = "трудово";

			position.StaffCount = wb.Cells[cr, 4].Value?.ToString();
			position.MinSalary = wb.Cells[cr, 5].Value?.ToString();

			position.education = (wb.Cells[cr, 6].Value?.ToString() + " " + wb.Cells[cr, 7].Value?.ToString()).Trim();
			position.par = dep.id;

			data.HR_FirmPersonal3.AddObject(position);
		}

		private void btnImportPositions_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Title = "Изберете файл за импорт на данни";
			ofd.Filter = "Excel Files (*.xls)|*.xls| Excel Files (*.xlsx)|*.xlsx| All Files (*.*)|*.*";
			if (ofd.ShowDialog() == false)
			{
				return;
			}

			FileInfo file = new FileInfo(ofd.FileName);
			using (ExcelPackage package = new ExcelPackage(file))
			{
				var data = new Entities(this.connstr);

				var wb = package.Workbook.Worksheets["Import"];

				int end = wb.Dimension.End.Row;

				for (int i = 1; i < end; i++)
				{
					var pos = new HR_GlobalPositions();
					pos.PositionName = wb.Cells[i, 1].Value.ToString();
					pos.EKDACode = wb.Cells[i, 2].Value?.ToString();
					pos.Minsalary = wb.Cells[i, 3].Value?.ToString();
					pos.Education = (wb.Cells[i, 4].Value?.ToString() + " " + wb.Cells[i, 5].Value?.ToString()).Trim();
					data.HR_GlobalPositions.AddObject(pos);
				}
				try
				{
					data.SaveChanges();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void btnImportPersons_Click(object sender, RoutedEventArgs e)
		{
			//fix positions
			//this.data = new Entities(this.connstr);
			//var lstPersons = data.HR_Person.ToList();
			//foreach (var per in lstPersons)
			//{
			//	var ass = data.HR_PersonAssignment.First(a => a.parent == per.id);
			//	var pos = data.HR_FirmPersonal3.First(a => a.id == ass.positionID);
			//	per.nodeID = pos.par;
			//}
			//data.SaveChanges();

			//fix names
			//this.data = new Entities(this.connstr);
			//var lstPersons = data.HR_Person.ToList();
			//foreach(var per in lstPersons)
			//{
			//	var nameArr = per.name.Split(new char[] { ' ' });
			//	per.name = "";
			//	foreach(var np in nameArr)
			//	{
			//		per.name += " " + np;
			//	}
			//	per.name = per.name.Trim();
			//}
			//data.SaveChanges();

			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Title = "Изберете файл за импорт на данни";
			ofd.Filter = "Excel Files (*.xlsx)|*.xlsx| Excel Files (*.xls)|*.xls| All Files (*.*)|*.*";
			if (ofd.ShowDialog() == false)
			{
				return;
			}

			FileInfo file = new FileInfo(ofd.FileName);
			using (ExcelPackage package = new ExcelPackage(file))
			{
				this.data = new Entities(this.connstr);

				var wb = package.Workbook.Worksheets[1];
				int end = wb.Dimension.End.Row;

				for (int i = 3; i <= end; i++)
				{
					if (wb.Cells[i, 1].Value == null)
					{
						continue;
					}
					var name = (wb.Cells[i, 2].Value?.ToString().Trim()  + " " + wb.Cells[i, 3].Value?.ToString().Trim() + " " +  wb.Cells[i, 4].Value?.ToString().Trim()).Trim() ;
					var egn = wb.Cells[i, 5].Value?.ToString().Trim();
					var per = data.HR_Person.FirstOrDefault(a => a.egn == egn);
					

					int y = 0, m = 0, d = 0;
					int.TryParse(wb.Cells[i, 7].Value?.ToString().Trim(), out y);
					int.TryParse(wb.Cells[i, 8].Value?.ToString().Trim(), out m);
					int.TryParse(wb.Cells[i, 9].Value?.ToString().Trim(), out d);

					per.TotalExpD = d;
					per.TotalExpM = m;
					per.TotalExpY = y;


					data.SaveChanges();
				}
			}

			//OpenFileDialog ofd = new OpenFileDialog();
			//ofd.Title = "Изберете файл за импорт на данни";
			//ofd.Filter = "Excel Files (*.xlsx)|*.xlsx| Excel Files (*.xls)|*.xls| All Files (*.*)|*.*";
			//if (ofd.ShowDialog() == false)
			//{
			//	return;
			//}

			//FileInfo file = new FileInfo(ofd.FileName);
			//using (ExcelPackage package = new ExcelPackage(file))
			//{
			//	this.data = new Entities(this.connstr);


			//	var wb = package.Workbook.Worksheets["Structure"];

			//	int end = wb.Dimension.End.Row;

			//	//usbale starts from row 1

			//	for (int i = 205; i <= end; i++)
			//	{
			//		if (wb.Cells[i, 2].Value == null)
			//		{
			//			continue;
			//		}					
			//		this.ImportDepartmentPersons(null, ref i, wb, end);
			//	}
			//}
		}

		private void ImportDepartmentPersons(object p, ref int cr, ExcelWorksheet wb, int end)
		{
			string level = wb.Cells[cr, 2].Value.ToString();
			var department = data.HR_Newtree2.Where(a => a.level == level).FirstOrDefault();

			if (department == null)
			{
				return;
			}

			cr++;

			for (; cr <= end; cr++)
			{
				if (wb.Cells[cr, 2].Value == null)
				{
					//cr++;
					return;
				}

				if (wb.Cells[cr, 10].Value != null)
				{
					this.ImportPerson(department, ref cr, wb);
				}
			}
		}

		private void ImportPerson(HR_Newtree2 department, ref int cr, ExcelWorksheet wb)
		{
			HR_Person per = new HR_Person();
			per.name = wb.Cells[cr, 10].Value.ToString();
			per.sex = wb.Cells[cr, 20].Value.ToString();

			HR_PersonAssignment ass = new HR_PersonAssignment();
			ass.HR_Person = per;

			var positionname = wb.Cells[cr, 2].Value.ToString();

			var position = data.HR_FirmPersonal3.Where(a => a.par == department.id && a.nameOfPosition == positionname).FirstOrDefault();

			ass.law = "трудово";
			ass.position = positionname;
			ass.positionID = position.id;
			ass.contractNumber = "0";
			ass.pcontractreasoncode = "1";
			var wt = wb.Cells[cr, 19].Value.ToString();
			switch (wt)
			{
				case "0.5":
					ass.worktime = "Непълно 4 часа";
					break;
				case "0.75":
					ass.worktime = "Непълно 6 часа";
					break;
				case "1":
					ass.worktime = "Пълно 8 часа";
					break;
			}

			string l3, l2, l1;
			l3 = department.level;
			var d2 = data.HR_Newtree2.FirstOrDefault(a => a.id == department.par);
			if (d2 == null)
			{
				l1 = l3;
				l3 = null;
				ass.level1 = l1;
			}
			else
			{
				var d1 = data.HR_Newtree2.FirstOrDefault(a => a.id == d2.par);
				if (d1 == null)
				{
					l1 = d2.level;
					l2 = l3;
					l3 = null;
					ass.level1 = l1;
					ass.level2 = l2;
				}
				else
				{
					l1 = d1.level;
					l2 = d2.level;
					ass.level1 = l1;
					ass.level2 = l2;
					ass.level3 = l3;
				}
			}

			data.HR_Person.AddObject(per);
			data.HR_PersonAssignment.AddObject(ass);

			data.SaveChanges();
		}

		private void btnImportHolidays_Click(object sender, RoutedEventArgs e)
		{
			//this.data = new Entities();

			var lstasses = data.HR_PersonAssignment.ToList();

			foreach (var per in lstasses)
			{
				if(per.assignedAt == null)
				{
					per.assignedAt = new DateTime(1970, 1, 1);
				}
				if (per.contractExpiry == null)
				{
					per.contractExpiry = new DateTime(1970, 1, 1);
				}
				if (per.TestContractDate == null)
				{
					per.TestContractDate = new DateTime(1970, 1, 1);
				}
				if (per.ParentContractDate == null)
				{
					per.ParentContractDate = new DateTime(1970, 1, 1);
				}
			}
			data.SaveChanges();
		}

        private void BtnRestoreEmployee_Click(object sender, RoutedEventArgs e)
        {
            if(this.txtEGN.Text.Length == 10)
            {
                var asses = data.HR_PersonAssignment.Where(a => a.HR_Person.egn == this.txtEGN.Text).ToList();
                if(asses.Count(a => a.isActive == 1) == 0)
                {
                    var las = asses.Last();
                    las.isActive = 1;
                    data.SaveChanges();
                }
            }
        }
    }
}
