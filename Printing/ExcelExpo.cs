using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Reflection;
using System.Runtime.InteropServices;
using Office;
using System.Data.OleDb;
using DataLayer;
using System.Linq;
using System.Collections.Generic;
using HRDataLayer;
//using Excel = Microsoft.Office.Interop.Excel;

namespace HR
{
	/// <summary>
	/// Summary description for ExcelExpo.
	/// </summary>
	public class ExcelExpo
	{
		const int HeaderRowNumber = 2;

		private struct ViewsArray
		{
			public DataView vue;
			public int index;
			public string vuename;
		};
		private formWait form;
		DataSet dsTemplateStructure;
		DataTable dtAssignments, dtPos, dtTree, dtPersons, dtHoliday, dtAttestations, dtRang, dtPenalty, dtEkdaPayLevels;
		private static object vk_missing = System.Reflection.Missing.Value;

		private static object vk_visible = true;
		private static object vk_false = false;
		private static object vk_true = true;

		#region OPEN WORKBOOK VARIABLES
		private object vk_update_links = 0;
		private object vk_read_only = vk_true;
		private object vk_format = 1;
		private object vk_password = vk_missing;
		private object vk_write_res_password = vk_missing;
		private object vk_ignore_read_only_recommend = vk_true;
		private object vk_origin = vk_missing;
		private object vk_delimiter = vk_missing;
		private object vk_editable = vk_false;
		private object vk_notify = vk_false;
		private object vk_converter = vk_missing;
		private object vk_add_to_mru = vk_false;
		private object vk_local = vk_false;
		private object vk_corrupt_load = vk_false;
		#endregion

		#region CLOSE WORKBOOK VARIABLES
		private object vk_save_changes = vk_false;
		private object vk_route_workbook = vk_false;
		#endregion

		private Excel.Application m_objExcel = null;
		private Excel.Workbooks m_objBooks = null;
		private Excel._Workbook m_objBook = null;
		private Excel.Sheets m_objSheets = null;
		private Excel._Worksheet m_objSheet = null;
		private Excel.Range m_objRange = null;

		Dictionary<int, int> tDict;

		private object opt = System.Reflection.Missing.Value;
		/// <summary>
		/// Summary description for ExcelExpo.
		/// </summary>
		public ExcelExpo()
		{
			//System.Globalization.CultureInfo cultureEn = new System.Globalization.CultureInfo("bg-BG");
			//System.Threading.Thread.CurrentThread.CurrentCulture = cultureEn;
		}

		/// <summary>
		/// Summary description for ExcelExpo.
		/// </summary>

		public void ExportView(DataGridView dg, DataView vue, string header)
		{
			try
			{
				int i, j, k;

				try
				{
					m_objExcel = new Excel.Application();
				}
				catch
				{
					MessageBox.Show("На компютъра няма инсталиран Microsoft Excel.");
					return;
				}
				m_objBooks = (Excel.Workbooks)m_objExcel.Workbooks;
				m_objBook = (Excel._Workbook)(m_objBooks.Add(opt));
				m_objSheets = (Excel.Sheets)m_objBook.Worksheets;
				m_objSheet = (Excel._Worksheet)(m_objSheets.Item[1]);

				m_objSheet.Cells[1, 1] = header;

				for (i = 0, j = 0; i < dg.Columns.Count; i++)
				{
					if (dg.Columns[i].Visible)
					{
						m_objSheet.Cells[HeaderRowNumber, j + 1] = dg.Columns[i].HeaderText;
						j++;
					}
				}

				for (i = 0; i < dg.Rows.Count; i++)
				{
					for (j = 0, k = 0; j < dg.Columns.Count; j++)
					{
						if (dg.Columns[j].Visible)
						{
							if (vue[i][j] is DateTime)
							{
								DateTime s = (DateTime)vue[i][j];

								m_objSheet.Cells[i + HeaderRowNumber, k + 1] = s.ToShortDateString();
							}
							else
							{
								m_objSheet.Cells[i + HeaderRowNumber, k + 1] = vue[i][j].ToString();
							}

							k++;
						}
					}
				}

				m_objRange = m_objSheet.Range[m_objSheet.Cells[1, 1], m_objSheet.Cells[vue.Count + HeaderRowNumber, dg.Columns.Count]];
				m_objRange.EntireColumn.AutoFit();

				m_objExcel.Visible = true;
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
			finally
			{
				ReleaseExcelApplication();
			}
		}

		/// <summary>
		/// Summary description for ExcelExpo.
		/// </summary>
		void ReleaseExcelApplication()
		{
			if (m_objBook != null)
				System.Runtime.InteropServices.Marshal.ReleaseComObject(m_objBook);
			if (m_objBooks != null)
				System.Runtime.InteropServices.Marshal.ReleaseComObject(m_objBooks);
			if (m_objSheet != null)
				System.Runtime.InteropServices.Marshal.ReleaseComObject(m_objSheet);
			if (m_objSheets != null)
				System.Runtime.InteropServices.Marshal.ReleaseComObject(m_objSheets);
			if (m_objRange != null)
				System.Runtime.InteropServices.Marshal.ReleaseComObject(m_objRange);
			if (m_objExcel != null)
				System.Runtime.InteropServices.Marshal.ReleaseComObject(m_objExcel);

			//			if( opt != null )
			//			opt = null;//System.Runtime.InteropServices.Marshal.ReleaseComObject( opt );
			//
			//			if( vk_save_changes != null )
			//			vk_save_changes  = null;
			//
			//			if( vk_route_workbook != null )
			//				vk_route_workbook = null;
			//			if( vk_read_only != null )
			//				 vk_read_only = null;
			//			if( vk_format != null )
			//				vk_format = null;
			//			if( vk_password != null )
			//				vk_password = null;
			//			if( vk_write_res_password != null )
			//				 vk_write_res_password =null;
			//			if( vk_ignore_read_only_recommend != null )
			//				 vk_ignore_read_only_recommend =null;
			//			if( vk_origin != null )
			//				vk_origin = null;
			//			if( vk_update_links != null )
			//				vk_update_links = null;
			//			if( vk_delimiter != null )
			//				vk_delimiter = null;
			//			if( vk_editable != null )
			//				 vk_editable = null;
			//			if( vk_notify != null )
			//				vk_notify = null;
			//			if( vk_converter != null )
			//				vk_converter = null;
			//			if( vk_add_to_mru != null )
			//				vk_add_to_mru = null;
			//			if( vk_local != null )
			//				vk_local = null;
			//			if( vk_corrupt_load != null )
			//				vk_corrupt_load = null;


			m_objRange = null;
			m_objBooks = null;
			m_objBook = null;
			m_objExcel = null;
			m_objSheets = null;
			m_objSheet = null;
			GC.Collect();
		}

		private void threadStart()
		{
			form.ShowDialog();
		}

		/// <summary>
		/// Exporting attestations data here
		/// </summary>
		/// <param name="main"></param>
		public void ExportPSR(mainForm main)
		{
			int CurrentRow = 5;
			DataTable dtFirmPersonal = new DataTable();
			DataView vuePositions = new DataView();
			DataView vueTree = new DataView();

			this.dtTree = main.nomenclaatureData.dtTreeTable;

			DataAction da = new DataAction(main.connString);

			this.dtPos = da.SelectWhere(TableNames.FirmPersonal3, "*", "");
			this.dtEkdaPayLevels = da.SelectWhere(TableNames.EkdaPayLevels, "*", "");
			DataTable dtOptions = da.SelectWhere(TableNames.Options, "*", "");

			if (this.dtPos == null || dtOptions == null)
			{
				MessageBox.Show("Грешка при зареждане на структурата на организацията", ErrorMessages.NoConnection);
				return;
			}

			float classcoef = 0;
			try
			{
				classcoef = float.Parse(dtOptions.Rows[0]["classcoef"].ToString());
			}
			catch (Exception ex)
			{
				classcoef = 1;
				MessageBox.Show(ex.Message, "Невалидна стойност за увеличение на процент прослужено време.");
			}

			this.dtAssignments = da.SelectPSR();
			if (this.dtAssignments == null)
			{
				MessageBox.Show("Грешка при зареждане на структурата на организацията", ErrorMessages.NoConnection);
				return;
			}


			dtAssignments.Columns.Add("shumenraisedsalary");
			dtAssignments.Columns.Add("raisedsalary");
			dtAssignments.Columns.Add("classAddon");
			dtAssignments.Columns.Add("shumenclasssalary");
			dtAssignments.Columns.Add("shumentotalsalary");
			dtAssignments.Columns.Add("totalsalary");
			dtAssignments.Columns.Add("cyears");
			dtAssignments.Columns.Add("cpercent");

			CalculatePSRCalculatedColumns(dtAssignments, classcoef);

			try
			{
				this.dsTemplateStructure = new DataSet();
				this.dsTemplateStructure.ReadXml(Application.StartupPath + @"\XMLLabels\PSR.xml");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				dsTemplateStructure = null;
				return;
			}

			try
			{
				m_objExcel = new Excel.Application();
			}
			catch
			{
				MessageBox.Show("На компютъра няма инсталиран Microsoft Excel.");
				return;
			}
			try
			{
				// Open a workbook in Excel
				m_objBook = m_objExcel.Workbooks.Open(Application.StartupPath +
					"\\TemplatePSR.xls", vk_update_links, vk_read_only, vk_format, vk_password,
					vk_write_res_password, vk_ignore_read_only_recommend, vk_origin,
					vk_delimiter, vk_editable, vk_notify, vk_converter, vk_add_to_mru
					);
			}
			catch (Exception e)
			{
				MessageBox.Show("Липсва шаблонен файл", e.Message);
				this.ReleaseExcelApplication();
				return;
			}
			System.Threading.ThreadStart dele = new System.Threading.ThreadStart(threadStart);
			System.Threading.Thread th = new System.Threading.Thread(dele);
			this.form = new formWait("PSR");
			th.Start();


			m_objSheets = (Excel.Sheets)m_objBook.Worksheets;
			m_objSheet = (Excel._Worksheet)(m_objSheets.Item[1]);

			float staff = 0, salaries = 0;
			ExportPSRLevel(0, ref CurrentRow, ref staff, ref salaries);

			m_objExcel.Visible = true;

			ReleaseExcelApplication();
			form.SetReferencePoint();
			form.StoreIncrements();
			th.Abort();
		}

		private void CalculatePSRCalculatedColumns(DataTable dt, float classcoef)
		{
			foreach (DataRow Row in dt.Rows)
			{
				CalculateShimenRaisedSalary(Row);

				CalculateRaisedSalary(Row);

				//"(basesalary + salaryaddon) as shumenraisedsalary, " +
				//"(basesalary*salaryaddon/100) as raisedsalary, " +
				//  "((basesalary + basesalary*salaryaddon/100)*(FLOOR((DATEDIFF(DAY, CURRENT_TIMESTAMP,AssignedAt) + DATEDIFF(MONTH, CURRENT_TIMESTAMP,AssignedAt) * 30 + DATEDIFF(YEAR, CURRENT_TIMESTAMP,AssignedAt) * 365  + (Years * 365 + Months * 30 + Days))/365))/100*{0}) as classaddon, " +
				//  "case when FLOOR(DATEDIFF(DAY, CURRENT_TIMESTAMP,AssignedAt) + DATEDIFF(MONTH , CURRENT_TIMESTAMP,AssignedAt)* 30 + DATEDIFF(YEAR, CURRENT_TIMESTAMP,AssignedAt) * 365 + (Years * 365 + Months * 30 + Days))/365 > 2 then ((basesalary + salaryaddon)*(FLOOR((DATEDIFF(DAY, CURRENT_TIMESTAMP,AssignedAt) + DATEDIFF(MONTH , CURRENT_TIMESTAMP,AssignedAt)* 30 + DATEDIFF(YEAR, CURRENT_TIMESTAMP,AssignedAt) * 365 + (Years * 365 + Months * 30 + Days))/365))/100 * 1)when 1=1 then  0 END as shumenclasssalary,  " +
				//  "(basesalary + salaryaddon + monthlyaddon + yearlyaddon + (basesalary + salaryaddon) * (FLOOR((DATEDIFF(DAY, CURRENT_TIMESTAMP, AssignedAt) + DATEDIFF(MONTH, CURRENT_TIMESTAMP, AssignedAt) * 30 + DATEDIFF(YEAR, CURRENT_TIMESTAMP, AssignedAt) * 365 + (Years * 365 + Months * 30 + Days)) / 365)) / 100 * 1) as shumentotalsalary, " +
				//  "(basesalary + monthlyaddon + basesalary*salaryaddon/100 + (basesalary + basesalary*salaryaddon/100)*(FLOOR((DATEDIFF(DAY, CURRENT_TIMESTAMP,AssignedAt) + DATEDIFF(MONTH , CURRENT_TIMESTAMP,AssignedAt)* 30 + DATEDIFF(YEAR, CURRENT_TIMESTAMP,AssignedAt) * 365 + (Years * 365 + Months * 30 + Days))/365))/100 * {3}) as totalsalary, " +
				//  "FLOOR((DATEDIFF(DAY, CURRENT_TIMESTAMP,AssignedAt) + DATEDIFF(MONTH , CURRENT_TIMESTAMP,AssignedAt)* 30 + DATEDIFF(YEAR, CURRENT_TIMESTAMP,AssignedAt) * 365 + (Years * 365 + Months * 30 + Days))/365) AS cyears, " +
				//  "case when FLOOR((DATEDIFF(DAY, CURRENT_TIMESTAMP,AssignedAt) + DATEDIFF(MONTH , CURRENT_TIMESTAMP,AssignedAt)* 30 + DATEDIFF(YEAR, CURRENT_TIMESTAMP,AssignedAt) * 365 + (Years * 365 + Months * 30 + Days))/365) > 0 then FLOOR((DATEDIFF(DAY, CURRENT_TIMESTAMP,AssignedAt) + DATEDIFF(MONTH , CURRENT_TIMESTAMP,AssignedAt)* 30 + DATEDIFF(YEAR, CURRENT_TIMESTAMP,AssignedAt) * 365 + (Years * 365 + Months * 30 + Days))/365)*1 when 1=1 then 0 END AS cpercent " + 

				CalculateClassAddon(classcoef, Row);

				CalculateShumenClassSalary(classcoef, Row);

				CalculateShumenTotalSalary(classcoef, Row);

				CalculateTotalSalary(classcoef, Row);

				CalculateCyears(Row);

				CalculateCpercent(classcoef, Row);
			}
		}

		private static void CalculateCpercent(float classcoef, DataRow Row)
		{
			//  "case when FLOOR((DATEDIFF(DAY, CURRENT_TIMESTAMP,AssignedAt) + DATEDIFF(MONTH , CURRENT_TIMESTAMP,AssignedAt)* 30 + DATEDIFF(YEAR, CURRENT_TIMESTAMP,AssignedAt) * 365 + (Years * 365 + Months * 30 + Days))/365) > 0 
			//then FLOOR((DATEDIFF(DAY, CURRENT_TIMESTAMP,AssignedAt) + DATEDIFF(MONTH , CURRENT_TIMESTAMP,AssignedAt)* 30 + DATEDIFF(YEAR, CURRENT_TIMESTAMP,AssignedAt) * 365 + (Years * 365 + Months * 30 + Days))/365)*1 when 1=1 then 0 END AS cpercent " + 


			DateTime AssignedAt;
			if (DateTime.TryParse(Row["AssignedAt"].ToString(), out AssignedAt) == false)
			{
				return;
			}

			int days = 0, months = 0, years = 0;
			int.TryParse(Row["days"].ToString(), out days);
			int.TryParse(Row["months"].ToString(), out months);
			int.TryParse(Row["years"].ToString(), out years);

			var Span = DateTime.Now.Subtract(AssignedAt);

			int tyears = (days + months * 30 + years * 365 + Span.Days) / 365;

			Row["cpercent"] = tyears * classcoef;
		}

		private static void CalculateCyears(DataRow Row)
		{
			//  "FLOOR((DATEDIFF(DAY, CURRENT_TIMESTAMP,AssignedAt) + DATEDIFF(MONTH , CURRENT_TIMESTAMP,AssignedAt)* 30 + DATEDIFF(YEAR, CURRENT_TIMESTAMP,AssignedAt) * 365 + (Years * 365 + Months * 30 + Days))/365) AS cyears, " +


			DateTime AssignedAt;
			if (DateTime.TryParse(Row["AssignedAt"].ToString(), out AssignedAt) == false)
			{
				return;
			}

			int days = 0, months = 0, years = 0;
			int.TryParse(Row["days"].ToString(), out days);
			int.TryParse(Row["months"].ToString(), out months);
			int.TryParse(Row["years"].ToString(), out years);

			var Span = DateTime.Now.Subtract(AssignedAt);

			int tyears = (days + months * 30 + years * 365 + Span.Days) / 365;

			Row["cyears"] = tyears;
		}

		private static void CalculateTotalSalary(float classcoef, DataRow Row)
		{
			//  "(basesalary + monthlyaddon + basesalary*salaryaddon/100 + (basesalary + basesalary*salaryaddon/100)*(FLOOR((DATEDIFF(DAY, CURRENT_TIMESTAMP,AssignedAt) + DATEDIFF(MONTH , CURRENT_TIMESTAMP,AssignedAt)* 30 + DATEDIFF(YEAR, CURRENT_TIMESTAMP,AssignedAt) * 365 + (Years * 365 + Months * 30 + Days))/365))/100 * {3}) as totalsalary, " +
			float baseSalary = 0, salaryAddon = 0, monthlyAddon;
			float.TryParse(Row["basesalary"].ToString(), out baseSalary);
			float.TryParse(Row["salaryaddon"].ToString(), out salaryAddon);
			float.TryParse(Row["monthlyaddon"].ToString(), out monthlyAddon);



			DateTime AssignedAt;
			if (DateTime.TryParse(Row["AssignedAt"].ToString(), out AssignedAt) == false)
			{
				return;
			}

			int days = 0, months = 0, years = 0;
			int.TryParse(Row["days"].ToString(), out days);
			int.TryParse(Row["months"].ToString(), out months);
			int.TryParse(Row["years"].ToString(), out years);

			var Span = DateTime.Now.Subtract(AssignedAt);

			int tyears = (days + months * 30 + years * 365 + Span.Days) / 365;

			float classaddon = baseSalary + monthlyAddon + (baseSalary * salaryAddon) / 100 + (baseSalary + baseSalary * salaryAddon / 100) * (((float)tyears / 100) * classcoef);

			Row["totalsalary"] = classaddon;
		}

		private static void CalculateShumenTotalSalary(float classcoef, DataRow Row)
		{
			//  "(basesalary + salaryaddon + monthlyaddon + yearlyaddon + (basesalary + salaryaddon) * (FLOOR((DATEDIFF(DAY, CURRENT_TIMESTAMP, AssignedAt) + DATEDIFF(MONTH, CURRENT_TIMESTAMP, AssignedAt) * 30 + DATEDIFF(YEAR, CURRENT_TIMESTAMP, AssignedAt) * 365 + (Years * 365 + Months * 30 + Days)) / 365)) / 100 * 1) as shumentotalsalary, " +
			float baseSalary = 0, salaryAddon = 0, monthlyAddon = 0, yearlyAddon = 0;
			float.TryParse(Row["basesalary"].ToString(), out baseSalary);
			float.TryParse(Row["salaryaddon"].ToString(), out salaryAddon);
			float.TryParse(Row["monthlyaddon"].ToString(), out monthlyAddon);
			float.TryParse(Row["yearlyaddon"].ToString(), out yearlyAddon);



			DateTime AssignedAt;
			if (DateTime.TryParse(Row["AssignedAt"].ToString(), out AssignedAt) == false)
			{
				return;
			}

			int days = 0, months = 0, years = 0;
			int.TryParse(Row["days"].ToString(), out days);
			int.TryParse(Row["months"].ToString(), out months);
			int.TryParse(Row["years"].ToString(), out years);

			var Span = DateTime.Now.Subtract(AssignedAt);

			int tyears = (days + months * 30 + years * 365 + Span.Days) / 365;

			float classaddon = baseSalary + salaryAddon + monthlyAddon + yearlyAddon + (baseSalary + salaryAddon) * (((float)tyears / 100) * classcoef);

			Row["shumentotalsalary"] = classaddon;
		}

		private static void CalculateShumenClassSalary(float classcoef, DataRow Row)
		{
			//  "case when FLOOR(DATEDIFF(DAY, CURRENT_TIMESTAMP,AssignedAt) + DATEDIFF(MONTH , CURRENT_TIMESTAMP,AssignedAt)* 30 + DATEDIFF(YEAR, CURRENT_TIMESTAMP,AssignedAt) * 365 + (Years * 365 + Months * 30 + Days))/365 > 2 
			//then ((basesalary + salaryaddon)*(FLOOR((DATEDIFF(DAY, CURRENT_TIMESTAMP,AssignedAt) + DATEDIFF(MONTH , CURRENT_TIMESTAMP,AssignedAt)* 30 + DATEDIFF(YEAR, CURRENT_TIMESTAMP,AssignedAt) * 365 + (Years * 365 + Months * 30 + Days))/365))/100 * 1)
			//when 1=1 then  0 END as shumenclasssalary,  " +
			float baseSalary, salaryAddon;
			float.TryParse(Row["basesalary"].ToString(), out baseSalary);
			float.TryParse(Row["salaryaddon"].ToString(), out salaryAddon);

			float classaddon = ((baseSalary + salaryAddon));


			DateTime AssignedAt;
			if (DateTime.TryParse(Row["AssignedAt"].ToString(), out AssignedAt) == false)
			{
				return;
			}

			int days = 0, months = 0, years = 0;
			int.TryParse(Row["days"].ToString(), out days);
			int.TryParse(Row["months"].ToString(), out months);
			int.TryParse(Row["years"].ToString(), out years);

			var Span = DateTime.Now.Subtract(AssignedAt);

			int tyears = (days + months * 30 + years * 365 + Span.Days) / 365;

			classaddon = classaddon * (((float)tyears / 100) * classcoef);

			Row["shumenclasssalary"] = classaddon;
			return;
		}

		private static void CalculateClassAddon(float classcoef, DataRow Row)
		{
			//((basesalary + basesalary*salaryaddon/100)*(FLOOR((DATEDIFF(DAY, CURRENT_TIMESTAMP,AssignedAt) + DATEDIFF(MONTH, CURRENT_TIMESTAMP,AssignedAt) * 30 + DATEDIFF(YEAR, CURRENT_TIMESTAMP,AssignedAt) * 365  + (Years * 365 + Months * 30 + Days))/365))/100*{0}) as classaddon, " +
			float baseSalary, salaryAddon;
			float.TryParse(Row["basesalary"].ToString(), out baseSalary);
			float.TryParse(Row["salaryaddon"].ToString(), out salaryAddon);

			float classaddon = ((baseSalary + baseSalary * salaryAddon / 100));


			DateTime AssignedAt;
			if (DateTime.TryParse(Row["AssignedAt"].ToString(), out AssignedAt) == false)
			{
				return;
			}

			int days = 0, months = 0, years = 0;
			int.TryParse(Row["days"].ToString(), out days);
			int.TryParse(Row["months"].ToString(), out months);
			int.TryParse(Row["years"].ToString(), out years);

			var Span = DateTime.Now.Subtract(AssignedAt);

			int tyears = (days + months * 30 + years * 365 + Span.Days) / 365;

			classaddon = classaddon * (((float)tyears / 100) * classcoef);
			Row["classaddon"] = classaddon;
			return;
		}

		private static void CalculateRaisedSalary(DataRow Row)
		{
			float baseSalary, salaryAddon;
			float.TryParse(Row["basesalary"].ToString(), out baseSalary);
			float.TryParse(Row["salaryaddon"].ToString(), out salaryAddon);

			Row["raisedsalary"] = baseSalary * salaryAddon / 100;
		}

		private static void CalculateShimenRaisedSalary(DataRow Row)
		{
			float baseSalary = 0, salaryAddon = 0;
			float.TryParse(Row["basesalary"].ToString(), out baseSalary);
			float.TryParse(Row["salaryaddon"].ToString(), out salaryAddon);

			Row["shumenraisedsalary"] = baseSalary + salaryAddon;
		}

		private void ExportPSRLevel(int parrot, ref int CurrentRow, ref float staff, ref float sals)
		{
			int par;
			float siblings = 0, salaries = 0, salsa = 0;
			bool slave = false;
			string master = "";
			DataView vuePositions;
			DataViewRowState dvrs = DataViewRowState.CurrentRows;
			DataView vueTree = new DataView(this.dtTree, "par = " + parrot.ToString(), "id", dvrs);
			DataView vueAssignments;//, vuePersons;
			for (int i = 0; i < vueTree.Count; i++)
			{
				int NodeId;
				try
				{
					par = int.Parse(vueTree[i]["par"].ToString());
				}
				catch (System.Exception e)
				{
					MessageBox.Show(e.Message, "Грешни данни ExportPSR");
					par = 0;
				}
				if (par == parrot)
				{
					if (vueTree[i]["code"].ToString() != "")
					{
						staff += siblings;
						sals += salaries;
						master = vueTree[i]["code"].ToString();
						siblings = 0;
						salaries = 0;
					}
					try
					{
						NodeId = int.Parse(vueTree[i]["id"].ToString());
					}
					catch (System.Exception e)
					{
						MessageBox.Show(e.Message, "Грешни данни ExportPSR NodeId");
						NodeId = 0;
					}
					string NodeText = vueTree[i]["level"].ToString();

					string cond = "par = " + NodeId.ToString();
					vuePositions = new DataView(dtPos, cond, "id", dvrs);

					//Име на administraciq


					//m_objSheet.Cells[CurrentRow, 3] = NodeText;
					//m_objSheet.Cells[CurrentRow, 2] = vueTree[i]["code"].ToString();
					//m_objRange = m_objSheet.get_Range(m_objSheet.Cells[CurrentRow, 2], m_objSheet.Cells[CurrentRow, 3]);
					//m_objRange.Font.Bold = true;

					m_objSheet.Cells[CurrentRow, 2] = NodeText;
					m_objRange = m_objSheet.Range[m_objSheet.Cells[CurrentRow, 2], m_objSheet.Cells[CurrentRow, 2]];
					m_objRange.Font.Bold = true;

					CurrentRow++;

					ViewsArray va1 = new ViewsArray();
					va1.vue = vuePositions;
					va1.vuename = "vuePositions";

					for (int pk = 0; pk < vuePositions.Count; pk++, CurrentRow++)
					{
						m_objSheet.Cells[CurrentRow, 2] = vuePositions[pk]["nameofposition"];
						//CurrentRow++;
						va1.index = pk;
						vueAssignments = new DataView(this.dtAssignments, "isactive = 1 and posid = " + vuePositions[pk]["id"], "id", dvrs);
						ViewsArray va2 = new ViewsArray();
						va2.vue = vueAssignments;
						va2.vuename = "vuePerson";
						for (int jm = 0; jm < vueAssignments.Count; jm++)
						{
							//vuePersons = new DataView(dtPersons, "id = " + vueAssignments[jm]["parent"], "id", dvrs);
							//if(vuePersons.Count > 0)
							//{

							va2.index = jm;
							ArrayList VuesArray = new ArrayList();
							VuesArray.Add(va1);
							VuesArray.Add(va2);
							CurrentRow++;
							this.PrintXMLRow(CurrentRow, VuesArray);
							siblings++;
							try
							{
								salsa = float.Parse(vueAssignments[jm]["totalsalary"].ToString());
							}
							catch (FormatException)
							{
								salsa = 0;
							}
							catch (Exception ex)
							{
								MessageBox.Show("Грешка при изчисляване на заплати" + ex.Message);
							}
							salaries += salsa;
						}
						//						if(dtAssignments.Rows.Count == 0)
						//						{
						//							CurrentRow--;
						//						}
						try  //Тук се цели там където има свободно работно място да остане празен ред
						{
							int freerow = int.Parse(vuePositions[pk]["free"].ToString());
							if (freerow > 0)
							{
								#region BIM 10% Freerows
								int columnnumber = 0;
								int pryc = 0;

								//Code for adding 10 percent for freee rows here
								for (pryc = 0; pryc < this.dsTemplateStructure.Tables["vuePositions"].Rows.Count; pryc++)
								{
									try
									{
										string cn = this.dsTemplateStructure.Tables["vuePositions"].Rows[pryc]["column_text"].ToString();
										if (cn == "bim10percent")
										{
											columnnumber = int.Parse(this.dsTemplateStructure.Tables["vuePositions"].Rows[pryc]["column_number"].ToString());
											break;
										}
									}
									catch
									{
										MessageBox.Show("Грешен номер на колона за " + this.dsTemplateStructure.Tables["vuePositions"].Rows[pryc]["column_text"]);
										continue;
									}

								}
								if (pryc < this.dsTemplateStructure.Tables["vuePositions"].Rows.Count && columnnumber != 0)
								{
									try
									{
										double Bim10Percent = 0;
										Bim10Percent = double.Parse(vuePositions[pk]["MinSalary"].ToString());
										Bim10Percent = Bim10Percent + Bim10Percent * 0.1;
										for (int hjk = 0; hjk < freerow; hjk++)
										{
											m_objSheet.Cells[CurrentRow + hjk + 1, columnnumber] = Bim10Percent.ToString();
										}
									}
									catch
									{
										//To do Add error log here
									}
								}
								#endregion
								CurrentRow += freerow;
							}
						}
						catch (FormatException)
						{
							//MessageBox.Show("Грешка в бройката на свободни работни места");
						}
					}
					ExportPSRLevel(NodeId, ref CurrentRow, ref siblings, ref salaries);
					#region Recapitulation
					if (vueTree[i]["code"].ToString() != "" || slave == true)
					{
						if (i + 1 < vueTree.Count)
						{
							if (vueTree[i + 1]["code"].ToString() == "")
							{
								slave = true;
								continue;
							}
						}
						if (slave == true)
						{
							m_objSheet.Cells[CurrentRow, 3] = "Всичко т. " + master;
						}
						else
						{
							m_objSheet.Cells[CurrentRow, 3] = "Всичко т. " + vueTree[i]["code"].ToString();
						}

						m_objSheet.Cells[CurrentRow, 4] = siblings.ToString(); //Щатните бройки
						m_objSheet.Cells[CurrentRow, 5] = salaries.ToString(); //Общо заплати в отдела
						m_objRange = m_objSheet.Range[m_objSheet.Cells[CurrentRow, 3], m_objSheet.Cells[CurrentRow, 5]];
						m_objRange.Font.Bold = true;
						slave = false;
						CurrentRow++;
					}
					#endregion
				}
			}
			staff += siblings;
			sals += salaries;
		}

		/// <summary>
		/// Exporting attestations data here
		/// </summary>
		/// <param name="main"></param>
		public void ExportOSR(mainForm main)
		{
			try
			{
				int CurrentRow = 11;
				float staff = 0;

				this.dtTree = main.nomenclaatureData.dtTreeTable;

				DataAction da = new DataAction(main.connString);

				this.dtPos = da.SelectWhere(TableNames.FirmPersonal3, "*", " ORDER BY id");

				if (this.dtPos == null)
				{
					MessageBox.Show("Грешка при зареждане на структурата на организацията", ErrorMessages.NoConnection);
					return;
				}

				try
				{
					m_objExcel = new Excel.Application();
				}
				catch
				{
					MessageBox.Show("На компютъра няма инсталиран Microsoft Excel.");
					return;
				}

				try
				{
					this.dsTemplateStructure = new DataSet();
					this.dsTemplateStructure.ReadXml(Application.StartupPath + @"\XMLLabels\OSR.xml");
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					dsTemplateStructure = null;
				}

				System.Threading.ThreadStart dele = new System.Threading.ThreadStart(threadStart);
				System.Threading.Thread th = new System.Threading.Thread(dele);
				this.form = new formWait("OSR");
				th.Start();
				try
				{
					// Open a workbook in Excel
					m_objBook = m_objExcel.Workbooks.Open(Application.StartupPath +
						"\\TemplateOSR.xls", vk_update_links, vk_read_only, vk_format, vk_password,
						vk_write_res_password, vk_ignore_read_only_recommend, vk_origin,
						vk_delimiter, vk_editable, vk_notify, vk_converter, vk_add_to_mru
						);
				}
				catch (Exception e)
				{
					MessageBox.Show("Липсва шаблонен файл", e.Message);
					return;
				}

				m_objSheets = (Excel.Sheets)m_objBook.Worksheets;
				m_objSheet = (Excel._Worksheet)(m_objSheets.Item[1]);

				ExportOSRLevel(0, ref CurrentRow, ref staff);

				#region recapitulation

				m_objSheet.Cells[CurrentRow, 2] = "Всичко в администрацията: ";
				m_objSheet.Cells[CurrentRow, 3] = staff.ToString();
				m_objRange = m_objSheet.Range[m_objSheet.Cells[CurrentRow, 2], m_objSheet.Cells[CurrentRow, 5]];
				m_objRange.Font.Bold = true;
				#endregion

				m_objExcel.Visible = true;

				ReleaseExcelApplication();
				form.SetReferencePoint();
				form.StoreIncrements();
				th.Abort();
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		public void ExportOSRNSO(mainForm main)
		{
			try
			{
				int CurrentRow = 11;
				float staff = 0;

				this.dtTree = main.nomenclaatureData.dtTreeTable;

				DataAction da = new DataAction(main.connString);

				this.dtPos = da.SelectWhere(TableNames.FirmPersonal3, "*", " ORDER BY id");

				if (this.dtPos == null)
				{
					MessageBox.Show("Грешка при зареждане на структурата на организацията", ErrorMessages.NoConnection);
					return;
				}

				try
				{
					m_objExcel = new Excel.Application();
				}
				catch
				{
					MessageBox.Show("На компютъра няма инсталиран Microsoft Excel.");
					return;
				}

				try
				{
					this.dsTemplateStructure = new DataSet();
					this.dsTemplateStructure.ReadXml(Application.StartupPath + @"\XMLLabels\OSR.xml");
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					dsTemplateStructure = null;
				}

				System.Threading.ThreadStart dele = new System.Threading.ThreadStart(threadStart);
				System.Threading.Thread th = new System.Threading.Thread(dele);
				this.form = new formWait("OSR");
				th.Start();
				try
				{
					// Open a workbook in Excel
					m_objBook = m_objExcel.Workbooks.Open(Application.StartupPath +
						"\\TemplateOSR.xls", vk_update_links, vk_read_only, vk_format, vk_password,
						vk_write_res_password, vk_ignore_read_only_recommend, vk_origin,
						vk_delimiter, vk_editable, vk_notify, vk_converter, vk_add_to_mru
						);
				}
				catch (Exception e)
				{
					MessageBox.Show("Липсва шаблонен файл", e.Message);
					return;
				}

				m_objSheets = (Excel.Sheets)m_objBook.Worksheets;
				m_objSheet = (Excel._Worksheet)(m_objSheets.Item[1]);

				ExportOSRLevelNSO(0, ref CurrentRow, ref staff);

				#region recapitulation

				m_objSheet.Cells[CurrentRow, 2] = "Всичко в администрацията: ";
				m_objSheet.Cells[CurrentRow, 3] = staff.ToString();
				m_objRange = m_objSheet.Range[m_objSheet.Cells[CurrentRow, 2], m_objSheet.Cells[CurrentRow, 5]];
				m_objRange.Font.Bold = true;
				#endregion

				m_objExcel.Visible = true;

				ReleaseExcelApplication();
				form.SetReferencePoint();
				form.StoreIncrements();
				th.Abort();
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void ExportOSRLevelNSO(int parrot, ref int CurrentRow, ref float staff)
		{
			int par;
			float siblings = 0;
			bool slave = false;
			string master = "";
			DataViewRowState dvrs = DataViewRowState.CurrentRows;
			DataView vueTree = new DataView(this.dtTree, "par = " + parrot.ToString(), "id", dvrs);
			DataView vuePositions;

			for (int i = 0; i < vueTree.Count; i++)
			{
				try
				{
					par = int.Parse(vueTree[i]["par"].ToString());
				}
				catch (System.Exception e)
				{
					MessageBox.Show(e.Message, "Грешни данни ExportOSR");
					par = 0;
				}
				if (par == parrot)
				{
					int NodeId;

					if (vueTree[i]["code"].ToString() != "")
					{
						staff += siblings;
						master = vueTree[i]["level"].ToString();
						siblings = 0;
					}

					try
					{
						NodeId = int.Parse(vueTree[i]["id"].ToString());
					}
					catch (System.Exception e)
					{
						MessageBox.Show(e.Message, "Грешни данни ExportOSR");
						NodeId = 0;
					}

					string NodeText = vueTree[i]["level"].ToString();

					string cond = "par = " + NodeId.ToString();
					vuePositions = new DataView(dtPos, cond, "id", dvrs);
					ViewsArray va = new ViewsArray();
					va.vue = vuePositions;
					va.vuename = "vuePositions";

					if (vueTree[i]["code"].ToString() != "")
					{
						m_objSheet.Cells[CurrentRow, 2] = NodeText;
						//m_objSheet.Cells[CurrentRow, 1] = vueTree[i]["code"].ToString();
						m_objRange = m_objSheet.Range[m_objSheet.Cells[CurrentRow, 2], m_objSheet.Cells[CurrentRow, 2]];
						m_objRange.Font.Bold = true;
						CurrentRow++;
					}

					for (int pk = 0; pk < vuePositions.Count; pk++, CurrentRow++)
					{
						int number;
						try
						{
							bool conv;
							conv = int.TryParse(vuePositions[pk]["staffCount"].ToString(), out number);
							if (conv == false)
							{
								number = 0;
							}
						}
						catch (System.Exception e)
						{
							MessageBox.Show(e.Message, "Грешни данни ExportOSR");
							number = 0;
						}
						siblings += number;

						//ArrayList vues = new ArrayList();
						//va.index = pk;
						//vues.Add(va);
						m_objSheet.Cells[CurrentRow, 1] = pk + 1;
						this.PrintXMLRowNSO(CurrentRow, vuePositions[pk]);
					}
					ExportOSRLevelNSO(NodeId, ref CurrentRow, ref siblings);
					#region recapitulation
					if (vueTree[i]["code"].ToString() != "" || slave == true)
					{
						if (i + 1 < vueTree.Count)
						{
							if (vueTree[i + 1]["code"].ToString() == "")
							{
								slave = true;
								continue;
							}
						}
						if (slave == true)
						{
							m_objSheet.Cells[CurrentRow, 2] = "Всичко " + master;
						}
						else
						{
							m_objSheet.Cells[CurrentRow, 2] = "Всичко " + vueTree[i]["level"].ToString();
						}

						int off = 0, star = 0, civ = 0;
						foreach (DataRowView r in vuePositions)
						{
							var mil = r["positioneng"].ToString();
							int m = 0;
							int.TryParse(mil, out m);
							if (m == 0)
							{
								int c = 0;
								int.TryParse(r["StaffCount"].ToString(), out c);
								civ += c;
							}
							else if (m >= 1 && m <= 5)
							{
								int c = 0;
								int.TryParse(r["StaffCount"].ToString(), out c);
								star += c;
							}
							else
							{
								int c = 0;
								int.TryParse(r["StaffCount"].ToString(), out c);
								off += c;
							}
						}

						m_objSheet.Cells[CurrentRow, 4] = off.ToString();
						m_objSheet.Cells[CurrentRow, 5] = star.ToString();
						m_objSheet.Cells[CurrentRow, 6] = civ.ToString();
						m_objSheet.Cells[CurrentRow, 7] = siblings.ToString(); //Щатните бройки

						m_objRange = m_objSheet.Range[m_objSheet.Cells[CurrentRow, 2], m_objSheet.Cells[CurrentRow, 7]];
						m_objRange.Font.Bold = true;
						slave = false;
						CurrentRow++;
					}
					#endregion
				}
			}
			staff += siblings;
		}

		private void PrintXMLRowNSO(int CurrentRow, DataRowView RowPositions)
		{
			m_objSheet.Cells[CurrentRow, 2] = RowPositions["nameOfPosition"].ToString();

			var mil = RowPositions["positioneng"].ToString();
			int m = 0;
			int.TryParse(mil, out m);
			if (m == 0)
			{
				m_objSheet.Cells[CurrentRow, 6] = RowPositions["StaffCount"].ToString();
			}
			else if (m >= 1 && m <= 5)
			{
				m_objSheet.Cells[CurrentRow, 5] = RowPositions["StaffCount"].ToString();
			}
			else
			{
				m_objSheet.Cells[CurrentRow, 4] = RowPositions["StaffCount"].ToString();
			}

			m_objSheet.Cells[CurrentRow, 7] = RowPositions["StaffCount"].ToString();

			switch (m)
			{
				case 1:
					m_objSheet.Cells[CurrentRow, 3] = "Младши сержант";
					break;
				case 2:
					m_objSheet.Cells[CurrentRow, 3] = "Сержант";
					break;
				case 3:
					m_objSheet.Cells[CurrentRow, 3] = "Старши сержант";
					break;
				case 4:
					m_objSheet.Cells[CurrentRow, 3] = "Старшина";
					break;
				case 5:
					m_objSheet.Cells[CurrentRow, 3] = "Офицерски кандидат";
					break;
				case 6:
					m_objSheet.Cells[CurrentRow, 3] = "Младши лейтенант";
					break;
				case 7:
					m_objSheet.Cells[CurrentRow, 3] = "Лейтенант";
					break;
				case 8:
					m_objSheet.Cells[CurrentRow, 3] = "Старши лейтенант";
					break;
				case 9:
					m_objSheet.Cells[CurrentRow, 3] = "Капитан";
					break;
				case 10:
					m_objSheet.Cells[CurrentRow, 3] = "Майор";
					break;
				case 11:
					m_objSheet.Cells[CurrentRow, 3] = "Подполковник";
					break;
				case 12:
					m_objSheet.Cells[CurrentRow, 3] = "Полковник";
					break;
				case 13:
					m_objSheet.Cells[CurrentRow, 3] = "Бригаден генерал";
					break;
				case 14:
					m_objSheet.Cells[CurrentRow, 3] = "Генерал-майор";
					break;
			}
		}

		private void ExportOSRLevel(int parrot, ref int CurrentRow, ref float staff)
		{
			int par;
			float siblings = 0;
			bool slave = false;
			string master = "";
			DataViewRowState dvrs = DataViewRowState.CurrentRows;
			DataView vueTree = new DataView(this.dtTree, "par = " + parrot.ToString(), "id", dvrs);
			DataView vuePositions;

			for (int i = 0; i < vueTree.Count; i++)
			{
				try
				{
					par = int.Parse(vueTree[i]["par"].ToString());
				}
				catch (System.Exception e)
				{
					MessageBox.Show(e.Message, "Грешни данни ExportOSR");
					par = 0;
				}
				if (par == parrot)
				{
					int NodeId;

					if (vueTree[i]["code"].ToString() != "")
					{
						staff += siblings;
						master = vueTree[i]["code"].ToString();
						siblings = 0;
					}

					try
					{
						NodeId = int.Parse(vueTree[i]["id"].ToString());
					}
					catch (System.Exception e)
					{
						MessageBox.Show(e.Message, "Грешни данни ExportOSR");
						NodeId = 0;
					}

					string NodeText = vueTree[i]["level"].ToString();

					string cond = "par = " + NodeId.ToString();
					vuePositions = new DataView(dtPos, cond, "id", dvrs);
					ViewsArray va = new ViewsArray();
					va.vue = vuePositions;
					va.vuename = "vuePositions";

					m_objSheet.Cells[CurrentRow, 2] = NodeText;
					m_objSheet.Cells[CurrentRow, 1] = vueTree[i]["code"].ToString();
					m_objRange = m_objSheet.Range[m_objSheet.Cells[CurrentRow, 2], m_objSheet.Cells[CurrentRow, 2]];
					m_objRange.Font.Bold = true;
					CurrentRow++;

					for (int pk = 0; pk < vuePositions.Count; pk++, CurrentRow++)
					{
						int number;
						try
						{
							bool conv;
							conv = int.TryParse(vuePositions[pk]["staffCount"].ToString(), out number);
							if (conv == false)
							{
								number = 0;
							}
						}
						catch (System.Exception e)
						{
							MessageBox.Show(e.Message, "Грешни данни ExportOSR");
							number = 0;
						}
						siblings += number;

						ArrayList vues = new ArrayList();
						va.index = pk;
						vues.Add(va);

						this.PrintXMLRow(CurrentRow, vues);
					}
					ExportOSRLevel(NodeId, ref CurrentRow, ref siblings);
					#region recapitulation
					if (vueTree[i]["code"].ToString() != "" || slave == true)
					{
						if (i + 1 < vueTree.Count)
						{
							if (vueTree[i + 1]["code"].ToString() == "")
							{
								slave = true;
								continue;
							}
						}
						if (slave == true)
						{
							m_objSheet.Cells[CurrentRow, 2] = "Всичко т. " + master;
						}
						else
						{
							m_objSheet.Cells[CurrentRow, 2] = "Всичко т. " + vueTree[i]["code"].ToString();
						}

						m_objSheet.Cells[CurrentRow, 3] = siblings.ToString(); //Щатните бройки
																			   //m_objSheet.Cells[ CurrentRow , 5] = sumadmin.ToString(); // Брой месеци
						m_objRange = m_objSheet.Range[m_objSheet.Cells[CurrentRow, 2], m_objSheet.Cells[CurrentRow, 5]];
						m_objRange.Font.Bold = true;
						slave = false;
						CurrentRow++;
					}
					#endregion
				}
			}
			staff += siblings;
		}

		/// <summary>
		/// Exporting attestations data here
		/// </summary>
		/// <param name="main"></param>
		public void ExportZZBUT(mainForm main)
		{
			int CurrentRow = 5;
			DataTable dtFirmPersonal = new DataTable();

			this.dtTree = main.nomenclaatureData.dtTreeTable;

			DataAction da = new DataAction(main.connString);
			DataTable dtAssignments = new DataTable();
			DataTable dtPersons = new DataTable();

			this.dtPersons = da.SelectWhere(TableNames.Person, "*", " ORDER BY id");
			this.dtAssignments = da.SelectWhere(TableNames.PersonAssignment, "*", " ORDER BY id");
			this.dtPos = da.SelectWhere(TableNames.FirmPersonal3, "*", " ORDER BY id");

			if (this.dtPersons == null || this.dtAssignments == null || this.dtPos == null)
			{
				MessageBox.Show("Грешка при зареждане на данните за лицата", ErrorMessages.NoConnection);
				return;
			}

			try
			{
				m_objExcel = new Excel.Application();
			}
			catch
			{
				MessageBox.Show("На компютъра няма инсталиран Microsoft Excel.");
				return;
			}
			try
			{
				// Open a workbook in Excel
				m_objBook = m_objExcel.Workbooks.Open(Application.StartupPath +
					"\\TemplateZZBUT.xls", vk_update_links, vk_read_only, vk_format, vk_password,
					vk_write_res_password, vk_ignore_read_only_recommend, vk_origin,
					vk_delimiter, vk_editable, vk_notify, vk_converter, vk_add_to_mru
					);
			}
			catch (Exception e)
			{
				MessageBox.Show("Липсва шаблонен файл", e.Message);
				this.ReleaseExcelApplication();
				return;
			}
			System.Threading.ThreadStart dele = new System.Threading.ThreadStart(threadStart);
			System.Threading.Thread th = new System.Threading.Thread(dele);
			this.form = new formWait("ZZBUT");
			th.Start();


			m_objSheets = (Excel.Sheets)m_objBook.Worksheets;
			m_objSheet = (Excel._Worksheet)(m_objSheets.Item[1]);

			ExportZZBUTLevel(0, ref CurrentRow);


			m_objExcel.Visible = true;

			ReleaseExcelApplication();
			form.SetReferencePoint();
			form.StoreIncrements();
			th.Abort();
		}

		private void ExportZZBUTLevel(int parrot, ref int CurrentRow)
		{
			int par;
			DataView vuePositions;
			DataViewRowState dvrs = DataViewRowState.CurrentRows;
			DataView vueTree = new DataView(this.dtTree, "par = " + parrot.ToString(), "id", dvrs);
			DataView vueAssignments, vuePersons;

			for (int i = 0; i < vueTree.Count; i++)
			{
				try
				{
					par = int.Parse(vueTree[i]["par"].ToString());
				}
				catch (System.Exception e)
				{
					MessageBox.Show(e.Message, "Грешни данни ExportZZBUT par1");
					par = 0;
				}
				if (par == parrot)
				{
					int NodeId;
					try
					{
						NodeId = int.Parse(vueTree[i]["id"].ToString());
					}
					catch (System.Exception e)
					{
						MessageBox.Show(e.Message, "Грешни данни ExportPSR NodeId");
						NodeId = 0;
					}
					string NodeText = vueTree[i]["level"].ToString();

					string cond = "par = " + NodeId.ToString();
					vuePositions = new DataView(dtPos, cond, "id", dvrs);

					for (int pk = 0; pk < vuePositions.Count; pk++)
					{
						vueAssignments = new DataView(this.dtAssignments, "isactive = 1 and positionid = " + vuePositions[pk]["id"], "id", dvrs);
						for (int jm = 0; jm < vueAssignments.Count; jm++)
						{
							vuePersons = new DataView(this.dtPersons, "id = " + vueAssignments[jm]["parent"].ToString(), "id", dvrs);

							if (vuePersons.Count > 0)
							{
								CurrentRow++;
								this.PrintZZBUTRow(CurrentRow, pk, vuePositions, vuePersons, 0, vueAssignments, jm);
							}
						}
					}
					this.ExportZZBUTLevel(NodeId, ref CurrentRow);
				}
			}
		}

		private void PrintZZBUTRow(int CurrentRow, int pk, DataView vuePositions, DataView dtPerson, int person_index, DataView dtAssignment, int ass_index)
		{
			//Име на лицето - 1
			m_objSheet.Cells[CurrentRow, 1] = dtPerson[person_index]["name"];
			//ЕГН - 2
			m_objSheet.Cells[CurrentRow, 2] = dtPerson[person_index]["egn"];
			//Град - 3
			m_objSheet.Cells[CurrentRow, 3] = dtPerson[person_index]["town"];
			//Адрес - 4
			m_objSheet.Cells[CurrentRow, 4] = dtPerson[person_index]["kwartal"];
			//Длъжност - 5
			m_objSheet.Cells[CurrentRow, 5] = vuePositions[pk]["nameofposition"];
			//Администрация 6
			m_objSheet.Cells[CurrentRow, 6] = dtAssignment[ass_index]["level1"];
			//Дирекция 7
			m_objSheet.Cells[CurrentRow, 7] = dtAssignment[ass_index]["level2"];
			//Отдел 8
			m_objSheet.Cells[CurrentRow, 8] = dtAssignment[ass_index]["level3"];
			//Сектор 9
			m_objSheet.Cells[CurrentRow, 9] = dtAssignment[ass_index]["level4"];

			Experience Exp;
			int days, years, months;
			try
			{
				years = int.Parse(dtAssignment[ass_index]["years"].ToString());
			}
			catch (FormatException)
			{
				years = 0;
			}
			try
			{
				months = int.Parse(dtAssignment[ass_index]["months"].ToString());
			}
			catch (FormatException)
			{
				months = 0;
			}
			try
			{
				days = int.Parse(dtAssignment[ass_index]["days"].ToString());
			}
			catch (FormatException)
			{
				days = 0;
			}

			DateTime StaffDate;

			StaffDate = (DateTime)dtAssignment[ass_index]["assignedat"];

			Exp = new Experience(years, months, days);
			Exp.CalculateToNow(StaffDate);
			//Трудов стаж ГГ 9
			m_objSheet.Cells[CurrentRow, 9] = Exp.Years.ToString(); //dtAssignment.Rows[ass_index]["years"];
																	//ММ 10
			m_objSheet.Cells[CurrentRow, 10] = Exp.Months.ToString(); // dtAssignment.Rows[ass_index]["months"];
																	  //ДД 11
			m_objSheet.Cells[CurrentRow, 11] = Exp.Days.ToString();//dtAssignment.Rows[ass_index]["days"];
																   //кодНКП 12
			m_objSheet.Cells[CurrentRow, 12] = vuePositions[pk]["NKPCode"];
			//ЕКДА Код - 13
			m_objSheet.Cells[CurrentRow, 13] = vuePositions[pk]["EKDACode"];
		}

		/// <summary>
		/// Summary description for ExcelExpo.
		/// </summary>
		public void ExtractHoliday(mainForm main)
		{
			int CurrentRow = 3;
			DataTable dtFirmPersonal = new DataTable();
			DataView vuePositions = new DataView();

			this.dtTree = main.nomenclaatureData.dtTreeTable;

			DataAction da = new DataAction(main.connString);

			this.dtPos = da.SelectWhere(TableNames.FirmPersonal3, "*", " ORDER BY id");
			this.dtAssignments = da.SelectWhere(TableNames.PersonAssignment, "*", " ORDER BY id");
			this.dtHoliday = da.SelectWhere(TableNames.YearHoliday, "*", " ORDER BY id");
			this.dtPersons = da.SelectWhere(TableNames.Person, "*", " ORDER BY id");
			this.dtHoliday.Columns["parent"].ColumnName = "par";

			if (this.dtPos == null || this.dtAssignments == null || this.dtHoliday == null || this.dtPersons == null || this.dtHoliday == null)
			{
				MessageBox.Show("Грешка при зареждане на данните за лицата", ErrorMessages.NoConnection);
				return;
			}

			System.Threading.ThreadStart dele = new System.Threading.ThreadStart(threadStart);
			System.Threading.Thread th = new System.Threading.Thread(dele);
			this.form = new formWait("Holiday");
			th.Start();
			try
			{
				m_objExcel = new Excel.Application();
			}
			catch
			{
				MessageBox.Show("На компютъра няма инсталиран Microsoft Excel.");
				return;
			}
			try
			{
				// Open a workbook in Excel
				m_objBook = m_objExcel.Workbooks.Open(Application.StartupPath +
					"\\TemplateHoliday.xls", vk_update_links, vk_read_only, vk_format, vk_password,
					vk_write_res_password, vk_ignore_read_only_recommend, vk_origin,
					vk_delimiter, vk_editable, vk_notify, vk_converter, vk_add_to_mru
					);
			}
			catch (Exception e)
			{
				MessageBox.Show("Липсва шаблонен файл", e.Message);
				return;
			}


			m_objSheets = (Excel.Sheets)m_objBook.Worksheets;
			m_objSheet = (Excel._Worksheet)(m_objSheets.Item[1]);

			int CountPersons = 1;
			this.ExportHolidayLevel(0, ref CurrentRow, ref CountPersons);



			m_objRange = m_objSheet.Range[m_objSheet.Cells[1, 1], m_objSheet.Cells[CurrentRow, 15]];
			m_objRange.EntireColumn.AutoFit();
			m_objExcel.Visible = true;

			ReleaseExcelApplication();
			form.SetReferencePoint();
			form.StoreIncrements();
			th.Abort();

		}

		private void ExportHolidayLevel(int parrot, ref int CurrentRow, ref int CountPersons)
		{
			int par;
			DataView vuePositions;
			DataViewRowState dvrs = DataViewRowState.CurrentRows;
			DataView vueTree = new DataView(this.dtTree, "par = " + parrot.ToString(), "id", dvrs);
			DataView vueAssignments, vuePersons;

			for (int i = 0; i < vueTree.Count; i++)
			{
				try
				{
					par = int.Parse(vueTree[i]["par"].ToString());
				}
				catch (System.Exception e)
				{
					MessageBox.Show(e.Message, "Грешни данни ExctractHoliday par1");
					par = 0;
				}
				if (par == parrot)
				{
					int NodeId;
					try
					{
						NodeId = int.Parse(vueTree[i]["id"].ToString());
					}
					catch (System.Exception e)
					{
						MessageBox.Show(e.Message, "Грешни данни ExtractHoliday NodeId");
						NodeId = 0;
					}
					string NodeText = vueTree[i]["level"].ToString();

					string cond = "par = " + NodeId.ToString();
					vuePositions = new DataView(dtPos, cond, "id", dvrs);

					//Име на administraciq
					m_objSheet.Cells[CurrentRow, 2] = NodeText;
					m_objSheet.Cells[CurrentRow, 1] = vueTree[i]["code"].ToString();

					m_objRange = m_objSheet.Range[m_objSheet.Cells[CurrentRow, 1], m_objSheet.Cells[CurrentRow, 2]];
					m_objRange.Font.Bold = true;
					CurrentRow++;
					for (int pk = 0; pk < vuePositions.Count; pk++)
					{
						vueAssignments = new DataView(dtAssignments, "isActive = 1 and positionID = " + vuePositions[pk]["id"], "id", dvrs);
						for (int jm = 0; jm < vueAssignments.Count; jm++, CurrentRow++)
						{
							vuePersons = new DataView(dtPersons, "id = " + vueAssignments[jm]["parent"], "id", dvrs);
							if (vuePersons.Count > 0)
							{
								this.PrintHolidayRow(CurrentRow, pk, dtHoliday, vuePersons, 0, CountPersons, vueAssignments[jm]["law"].ToString());
								CountPersons++;
							}
						}
					}
					this.ExportHolidayLevel(NodeId, ref CurrentRow, ref CountPersons);
				}
			}
		}

		private void PrintHolidayRow(int CurrentRow, int pk, DataTable dtHoliday, DataView dtPerson, int person_index, int CountPersons, string law)
		{
			DataViewRowState dvrs = DataViewRowState.CurrentRows;
			string Condition = "par = " + dtPerson[person_index]["id"];
			DataView vueHoliday = new DataView(dtHoliday, Condition, "year", dvrs);
			int days, total;
			//Номер по ред
			m_objSheet.Cells[CurrentRow, 1] = CountPersons.ToString() + ".";
			//Име на лицето - 1
			m_objSheet.Cells[CurrentRow, 2] = dtPerson[person_index]["name"];

			m_objSheet.Cells[CurrentRow, 3] = law;
			//2001
			for (int i = 0; i < vueHoliday.Count; i++)
			{
				try
				{
					days = (int)vueHoliday[i]["leftover"];
				}
				catch
				{
					days = 0;
				}
				try
				{
					total = (int)vueHoliday[i]["total"];
				}
				catch (Exception)
				{
					total = 0;
				}
				switch (vueHoliday[i]["year"].ToString())
				{
					case "2008":
						{
							m_objSheet.Cells[CurrentRow, 4] = total.ToString();
							m_objSheet.Cells[CurrentRow, 5] = days.ToString();
							break;
						}
					case "2009":
						{
							m_objSheet.Cells[CurrentRow, 6] = total.ToString();
							m_objSheet.Cells[CurrentRow, 7] = days.ToString();
							break;
						}
					case "2010":
						{
							m_objSheet.Cells[CurrentRow, 8] = total.ToString();
							m_objSheet.Cells[CurrentRow, 9] = days.ToString();
							break;
						}
					case "2011":
						{
							m_objSheet.Cells[CurrentRow, 10] = total.ToString();
							m_objSheet.Cells[CurrentRow, 11] = days.ToString();
							break;
						}
					case "2012":
						{
							m_objSheet.Cells[CurrentRow, 12] = total.ToString();
							m_objSheet.Cells[CurrentRow, 13] = days.ToString();
							break;
						}
					case "2013":
						{
							m_objSheet.Cells[CurrentRow, 14] = total.ToString();
							m_objSheet.Cells[CurrentRow, 15] = days.ToString();
							break;
						}
					case "2014":
						{
							m_objSheet.Cells[CurrentRow, 16] = total.ToString();
							m_objSheet.Cells[CurrentRow, 17] = days.ToString();
							break;
						}
					case "2015":
						{
							m_objSheet.Cells[CurrentRow, 18] = total.ToString();
							m_objSheet.Cells[CurrentRow, 19] = days.ToString();
							break;
						}
					case "2016":
						{
							m_objSheet.Cells[CurrentRow, 20] = total.ToString();
							m_objSheet.Cells[CurrentRow, 21] = days.ToString();
							break;
						}
					case "2017":
						{
							m_objSheet.Cells[CurrentRow, 22] = total.ToString();
							m_objSheet.Cells[CurrentRow, 23] = days.ToString();
							break;
						}
					case "2018":
						{
							m_objSheet.Cells[CurrentRow, 24] = total.ToString();
							m_objSheet.Cells[CurrentRow, 25] = days.ToString();
							break;
						}
					case "2019":
						{
							m_objSheet.Cells[CurrentRow, 26] = total.ToString();
							m_objSheet.Cells[CurrentRow, 27] = days.ToString();
							break;
						}
					case "2020":
						{
							m_objSheet.Cells[CurrentRow, 28] = total.ToString();
							m_objSheet.Cells[CurrentRow, 29] = days.ToString();
							break;
						}
				}
			}
		}

		public void ExtractFreeEntity(mainForm main)
		{
			int CurrentRow = 3;
			string cs;

			mainForm.GetConnString(out cs);

			var data = new HRDataLayer.Entities(cs);

			var lstDepartments = data.HR_Newtree2.Where(b => b.par == 0).ToList();

			try
			{
				m_objExcel = new Excel.Application();
			}
			catch
			{
				MessageBox.Show("На компютъра няма инсталиран Microsoft Excel.");
				return;
			}

			System.Threading.ThreadStart dele = new System.Threading.ThreadStart(threadStart);
			System.Threading.Thread th = new System.Threading.Thread(dele);
			this.form = new formWait("Free");
			th.Start();
			try
			{
				// Open a workbook in Excel
				m_objBook = m_objExcel.Workbooks.Open(Application.StartupPath +
					"\\TemplateFree.xls", vk_update_links, vk_read_only, vk_format, vk_password,
					vk_write_res_password, vk_ignore_read_only_recommend, vk_origin,
					vk_delimiter, vk_editable, vk_notify, vk_converter, vk_add_to_mru
					);
			}
			catch (Exception e)
			{
				MessageBox.Show("Липсва шаблонен файл", e.Message);
				return;
			}
			m_objSheets = (Excel.Sheets)m_objBook.Worksheets;
			m_objSheet = (Excel._Worksheet)(m_objSheets.Item[1]);

			this.ExtractFreeLevelEntity(null, ref CurrentRow, data);

			CurrentRow++;

			//m_objSheet.Cells[CurrentRow, 1] = "Длъжностно ниво";
			//m_objSheet.Cells[CurrentRow, 2] = "НАИМЕНОВАНИЯ НА ДЛЪЖНОСТИТЕ";
			//m_objSheet.Cells[CurrentRow, 3] = "Брой по щат";
			//m_objSheet.Cells[CurrentRow, 4] = "Брой заети";
			//m_objSheet.Cells[CurrentRow, 5] = "Брой вакантни";
			//CurrentRow++;
			//m_objSheet.Cells[CurrentRow, 1] = 1;
			//m_objSheet.Cells[CurrentRow, 2] = 2;
			//m_objSheet.Cells[CurrentRow, 3] = 3;
			//m_objSheet.Cells[CurrentRow, 4] = 4;
			//m_objSheet.Cells[CurrentRow, 5] = 5;
			//CurrentRow++;

			//Recapitulation for everybody in the field of state experts - removed for now. Else for every EKDA code present in organisation create a summary recapitulation			

			m_objRange = m_objSheet.Range[m_objSheet.Cells[1, 1], m_objSheet.Cells[CurrentRow, 10]];
			m_objRange.EntireColumn.AutoFit();
			m_objExcel.Visible = true;

			ReleaseExcelApplication();
			form.SetReferencePoint();
			form.StoreIncrements();
			th.Abort();
		}		

		private void ExtractFreeLevelEntity(HR_Newtree2 pdep, ref int CurrentRow, Entities data)
		{
			int par;
			if (pdep == null)
				par = 0;
			else
			{
				par = pdep.id;
			}

			var lstDeps = data.HR_Newtree2.Where(a => a.par == par).ToList();
			foreach (var dep in lstDeps)
			{
				var lstPositions = data.HR_FirmPersonal3.Where(a => a.par == dep.id).ToList();

				m_objSheet.Cells[CurrentRow, 2] = dep.level;
				m_objSheet.Cells[CurrentRow, 1] = dep.code;
				m_objRange = m_objSheet.Range[m_objSheet.Cells[CurrentRow, 2], m_objSheet.Cells[CurrentRow, 2]];
				m_objRange.Font.Bold = true;
				CurrentRow++;

				foreach (var pos in lstPositions)
				{
					// 2 - Длъжностно ниво по ЕКДА
					m_objSheet.Cells[CurrentRow, 2] = pos.nameOfPosition;
					// 3 - Брой щатни бройки
					m_objSheet.Cells[CurrentRow, 3] = pos.StaffCount;

					var lstAssignments = data.HR_PersonAssignment.Where(a => a.positionID == pos.id && a.isActive == 1 && a.HR_Person.fired == 0 && (a.tutorname != "" && a.tutorname != null)).ToList();

					double busy = 0;
					double staff = 0;
					double.TryParse(pos.StaffCount, out staff);
					var lstWorktime = data.HR_Worktime.ToList();
					foreach (var ass in lstAssignments)
					{
						busy += (double)lstWorktime.FirstOrDefault(a => a.level == ass.worktime)?.staff;
					}
					// 4 - Брой заети щатни бройки
					m_objSheet.Cells[CurrentRow, 4] = busy;
					// 5 - Брой свободни
					m_objSheet.Cells[CurrentRow, 5] = staff - busy;
					CurrentRow++;
				}

				var lstChilds = data.HR_Newtree2.Where(a => a.par == dep.id).ToList();
				foreach (var chi in lstChilds)
				{
					this.ExtractFreeLevelEntity(chi, ref CurrentRow, data);
				}
			}
		}

		
		/// <summary>
		/// Summary description for ExcelExpo.
		/// </summary>

		public void ExportAttestations(mainForm main)
		{
			int CurrentRow = 5;
			this.dtTree = main.nomenclaatureData.dtTreeTable;
			DataTable dtPos = new DataTable();
			DataAction da = new DataAction(main.connString);
			this.dtAssignments = da.SelectWhere(TableNames.PersonAssignment, "*", " ORDER BY id");
			this.dtPos = da.SelectWhere(TableNames.FirmPersonal3, "*", " ORDER BY id");
			this.dtPersons = da.SelectWhere(TableNames.Person, "*", " ORDER BY id");
			this.dtAttestations = da.SelectWhere(TableNames.Attestations, "*", " ORDER BY id");

			if (this.dtAssignments == null || this.dtPos == null || this.dtPersons == null || this.dtAttestations == null)
			{
				MessageBox.Show("Грешка при зареждане на данните за атестации", ErrorMessages.NoConnection);
				return;
			}

			try
			{
				m_objExcel = new Excel.Application();
			}
			catch
			{
				MessageBox.Show("На компютъра няма инсталиран Microsoft Excel.");
				return;
			}
			try
			{
				// Open a workbook in Excel
				m_objBook = m_objExcel.Workbooks.Open(Application.StartupPath +
					"\\TemplateAttestations.xls", vk_update_links, vk_read_only, vk_format, vk_password,
					vk_write_res_password, vk_ignore_read_only_recommend, vk_origin,
					vk_delimiter, vk_editable, vk_notify, vk_converter, vk_add_to_mru
					);
			}
			catch (Exception e)
			{
				MessageBox.Show("Липсва шаблонен файл", e.Message);
				this.ReleaseExcelApplication();
				return;
			}
			System.Threading.ThreadStart dele = new System.Threading.ThreadStart(threadStart);
			System.Threading.Thread th = new System.Threading.Thread(dele);
			this.form = new formWait("Attestations");
			th.Start();

			m_objSheets = (Excel.Sheets)m_objBook.Worksheets;
			m_objSheet = (Excel._Worksheet)(m_objSheets.Item[1]);

			this.ExportAttestationsLevel(0, ref CurrentRow);
			m_objExcel.Visible = true;

			ReleaseExcelApplication();
			form.SetReferencePoint();
			form.StoreIncrements();
			th.Abort();
		}

		private void ExportAttestationsLevel(int parrot, ref int CurrentRow)
		{
			int par;
			DataView vuePositions, vueAssignments, vuePersons;
			DataViewRowState dvrs = DataViewRowState.CurrentRows;
			DataView vueTree = new DataView(this.dtTree, "par = " + parrot.ToString(), "code", dvrs);
			for (int i = 0; i < vueTree.Count; i++)
			{
				try
				{
					par = int.Parse(vueTree[i]["par"].ToString());
				}
				catch (System.Exception e)
				{
					MessageBox.Show(e.Message, "Грешни данни ExportAttestations par1");
					par = 0;
				}
				if (par == parrot)
				{
					int NodeId;
					try
					{
						NodeId = int.Parse(vueTree[i]["id"].ToString());
					}
					catch (System.Exception e)
					{
						MessageBox.Show(e.Message, "Грешни данни ExportAttestations NodeId");
						NodeId = 0;
					}
					string NodeText = vueTree[i]["level"].ToString();

					string cond = "par = " + NodeId.ToString();
					vuePositions = new DataView(dtPos, cond, "id", dvrs);

					//Име на administraciq
					m_objSheet.Cells[CurrentRow, 2] = NodeText;
					m_objSheet.Cells[CurrentRow, 1] = vueTree[i]["code"].ToString();

					m_objRange = m_objSheet.Range[m_objSheet.Cells[CurrentRow, 2], m_objSheet.Cells[CurrentRow, 3]];
					m_objRange.Font.Bold = true;
					CurrentRow++;
					for (int pk = 0; pk < vuePositions.Count; pk++, CurrentRow++)
					{
						m_objSheet.Cells[CurrentRow, 2] = vuePositions[pk]["nameofposition"];
						//CurrentRow++;
						vueAssignments = new DataView(this.dtAssignments, "isActive = 1 and positionID = " + vuePositions[pk]["id"], "id", dvrs);
						for (int jm = 0; jm < vueAssignments.Count; jm++)
						{
							vuePersons = new DataView(this.dtPersons, "id = " + vueAssignments[jm]["parent"], "id", dvrs);
							if (vuePersons.Count > 0)
							{
								CurrentRow++;
								this.PrintAttestationRow(CurrentRow, pk, vuePositions, vuePersons, 0);
							}
						}
						try  //Тук се цели там където има свободно работно място да остане празен ред
						{
							int freerow = int.Parse(vuePositions[pk]["free"].ToString());
							CurrentRow += freerow;
						}
						catch (FormatException)
						{
						}
					}
					this.ExportAttestationsLevel(NodeId, ref CurrentRow);
				}
			}
		}

		private void PrintAttestationRow(int CurrentRow, int pk, DataView vuePositions, DataView dtPerson, int person_index)
		{
			//Име на лицето - 3
			m_objSheet.Cells[CurrentRow, 3] = dtPerson[person_index]["name"];
			DataViewRowState dvrs = DataViewRowState.CurrentRows;
			DataView vueAttestations = new DataView(this.dtAttestations, "par = " + dtPerson[person_index]["id"].ToString(), "id", dvrs);
			for (int i = 0; i < vueAttestations.Count; i++)
			{
				int inx;
				try
				{
					inx = int.Parse(vueAttestations[i]["year"].ToString());
					inx -= 2003;
					if (inx < 0)
						continue;
				}
				catch (FormatException)
				{
					continue;
				}
				//Всички + 4
				int numcolumns = 7;
				//Работен план + 0
				m_objSheet.Cells[CurrentRow, inx * numcolumns + 4 + 0] = vueAttestations[i]["hasworkplan"].ToString();
				//Междинна среща + 1
				m_objSheet.Cells[CurrentRow, inx * numcolumns + 4 + 1] = vueAttestations[i]["hasmiddlemeeting"].ToString();
				//Заключителна среща + 2
				m_objSheet.Cells[CurrentRow, inx * numcolumns + 4 + 2] = vueAttestations[i]["hasfinalmeeting"].ToString();
				//Обучение + 3
				m_objSheet.Cells[CurrentRow, inx * numcolumns + 4 + 3] = vueAttestations[i]["hastraining"].ToString();
				//Възражение + 4 
				m_objSheet.Cells[CurrentRow, inx * numcolumns + 4 + 4] = vueAttestations[i]["hasobjection"].ToString();
				//Крайна ожценка + 5
				m_objSheet.Cells[CurrentRow, inx * numcolumns + 4 + 5] = vueAttestations[i]["totalmark"].ToString();
				//Повишение в ранг + 6
				m_objSheet.Cells[CurrentRow, inx * numcolumns + 4 + 6] = vueAttestations[i]["hasrangupdate"].ToString();
			}
		}

		/// <summary>
		/// Summary description for ExcelExpo.
		/// </summary>
		public void ExtractCustom(mainForm main, DataTable dtFiltered, ArrayList Columns)
		{
			int CurrentRow = 2;

			if (dtFiltered.Rows.Count <= 0)
			{
				return;
			}
			if (System.IO.File.Exists(Application.StartupPath + "\\TemplateCustom.xls") == false)
			{
				MessageBox.Show("Липсва шаблонен файл");
				return;
			}

			this.dtTree = main.nomenclaatureData.dtTreeTable;
			this.dtPersons = dtFiltered;

			DataAction da = new DataAction(main.connString);

			try
			{
				this.dtPos = da.SelectWhere(TableNames.FirmPersonal3, "*", " ORDER BY id");
				this.dtAssignments = da.SelectWhere(TableNames.PersonAssignment, "*", " ORDER BY id");
				if (this.dtPos == null || this.dtAssignments == null)
				{
					MessageBox.Show("Грешка при зареждане на данни за назначения");
					return;
				}

				try
				{
					m_objExcel = new Excel.Application();
				}
				catch
				{
					MessageBox.Show("На компютъра няма инсталиран Microsoft Excel.");
					return;
				}

				// Open a workbook in Excel
				m_objBook = m_objExcel.Workbooks.Open(Application.StartupPath +
					"\\TemplateCustom.xls", vk_update_links, vk_read_only, vk_format, vk_password,
					vk_write_res_password, vk_ignore_read_only_recommend, vk_origin,
					vk_delimiter, vk_editable, vk_notify, vk_converter, vk_add_to_mru
					);

				System.Threading.ThreadStart dele = new System.Threading.ThreadStart(threadStart);
				System.Threading.Thread th = new System.Threading.Thread(dele);
				this.form = new formWait("Custom");
				th.Start();
				try
				{
					m_objSheets = (Excel.Sheets)m_objBook.Worksheets;
					m_objSheet = (Excel._Worksheet)(m_objSheets.Item[1]);



					int i = 0;
					while (i < Columns.Count)
					{
						string[] spl2 = Columns[i].ToString().Split(new char[] { '.' });
						string str;
						if (spl2.Length > 1)
						{
							str = spl2[1].ToLower();
						}
						else if (spl2.Length > 0)
						{
							str = spl2[0].ToLower();
						}
						else
						{
							str = "";
						}
						if (str == "id" || str == "position" || str == "level1" || str == "level2" || str == "level3" || str == "level4")
						{
							Columns.RemoveAt(i);
							continue;
						}
						i++;
					}
					this.PrintCustomColumnHeaders(Columns);
					m_objExcel.Visible = true;
					this.ExtractCustomLevel(0, ref CurrentRow, Columns);

					m_objRange = m_objSheet.Range[m_objSheet.Cells[1, 1], m_objSheet.Cells[CurrentRow, Columns.Count + 4]];
					m_objRange.EntireColumn.AutoFit();
					form.SetReferencePoint();
					form.StoreIncrements();
					th.Abort();


					ReleaseExcelApplication();
				}
				catch (Exception xc)
				{
					MessageBox.Show(xc.Message, "Грешка при ескпортиране към Excel");
					m_objBook.Close(vk_false, vk_missing, vk_missing);
					m_objExcel.Quit();
					ReleaseExcelApplication();
					th.Abort();
				}
			}
			catch (Exception e)
			{

				MessageBox.Show("Липсва шаблонен файл", e.Message);
				ReleaseExcelApplication();
				return;
			}
		}

		private bool ExtractCustomLevel(int parrot, ref int CurrentRow, ArrayList Columns)
		{
			int par;
			bool FlagCurrent = false, FlagBelow = false, FlagTotal = false;
			DataView vuePositions, vueAssignments, vueTree, vuePerson;
			DataViewRowState dvrs = DataViewRowState.CurrentRows;
			vueTree = new DataView(this.dtTree, "par = " + parrot.ToString(), "code", dvrs);
			for (int i = 0; i < vueTree.Count; i++)
			{
				try
				{
					par = int.Parse(vueTree[i]["par"].ToString());
				}
				catch (System.Exception e)
				{
					MessageBox.Show(e.Message, "Грешни данни Custom par1");
					par = 0;
				}
				if (par == parrot)
				{
					//					FlagCurrent = false;
					//					FlagBelow = false;
					int NodeId;
					try
					{
						NodeId = int.Parse(vueTree[i]["id"].ToString());
					}
					catch (System.Exception e)
					{
						MessageBox.Show(e.Message, "Грешни данни Custom NodeId");
						NodeId = 0;
					}

					string cond = "par = " + NodeId.ToString();
					vuePositions = new DataView(dtPos, cond, "id", dvrs);

					for (int pk = 0; pk < vuePositions.Count; pk++)
					{
						string condAssignment = "isactive = 1 and positionID = " + vuePositions[pk]["id"];
						vueAssignments = new DataView(this.dtAssignments, condAssignment, "id", dvrs);

						for (int jm = 0; jm < vueAssignments.Count; jm++)
						{
							string condPerson = "id = " + vueAssignments[jm]["parent"];
							vuePerson = new DataView(this.dtPersons, condPerson, "id", dvrs);
							if (vuePerson.Count > 0)
							{
								if (FlagCurrent == false)
								{
									FlagCurrent = true;
									m_objSheet.Cells[CurrentRow, 1] = vueTree[i]["code"];
									m_objSheet.Cells[CurrentRow, 2] = vueTree[i]["level"];
									m_objRange = m_objSheet.Range[m_objSheet.Cells[CurrentRow, 1], m_objSheet.Cells[CurrentRow, 2]];
									m_objRange.Font.Bold = true;
									CurrentRow++;
								}
								this.PrintCustomRow(CurrentRow, pk, vuePositions, vuePerson, 0, vueAssignments, jm, Columns);
								CurrentRow++;
							}
						}
					}
					if (FlagCurrent == false)
					{
						m_objSheet.Cells[CurrentRow, 1] = vueTree[i]["code"];
						m_objSheet.Cells[CurrentRow, 2] = vueTree[i]["level"];
						m_objRange = m_objSheet.Range[m_objSheet.Cells[CurrentRow, 1], m_objSheet.Cells[CurrentRow, 2]];
						m_objRange.Font.Bold = true;
						CurrentRow++;
					}
					FlagBelow = this.ExtractCustomLevel(NodeId, ref CurrentRow, Columns);
					if (FlagCurrent == false && FlagBelow == false)
					{
						CurrentRow--;
						m_objSheet.Cells[CurrentRow, 1] = "";
						m_objSheet.Cells[CurrentRow, 2] = "";
						m_objRange = m_objSheet.Range[m_objSheet.Cells[CurrentRow, 1], m_objSheet.Cells[CurrentRow, 2]];
						m_objRange.Font.Bold = false;
					}
					FlagTotal = FlagCurrent || FlagBelow || FlagTotal;
				}
			}
			return FlagTotal;
		}

		private void PrintCustomRow(int CurrentRow, int pk, DataView vuePositions, DataView vuePerson, int person_index, DataView vueAssignment, int ass_index, ArrayList Columns)
		{
			//Име на лицето - 4
			m_objSheet.Cells[CurrentRow, 2] = vuePerson[person_index]["name"];
			//string [] sArr = vuePerson[person_index]["name"].ToString().Split(new char[] {' '});
			//string middle = "";

			//m_objSheet.Cells[CurrentRow, 2] = sArr[0];
			//for (int i = 1; i < sArr.Length - 1; i++)
			//{
			//    middle += " " + sArr[i];
			//}
			//m_objSheet.Cells[CurrentRow, 3] = middle;
			//m_objSheet.Cells[CurrentRow, 4] = sArr[sArr.Length - 1];
			//длъжност - 5
			m_objSheet.Cells[CurrentRow, 3] = vuePositions[pk]["nameofposition"];
			//Всичко друго
			string str;
			for (int j = 0; j < Columns.Count; j++)
			{
				string[] spl = Columns[j].ToString().Split(new char[] { '.' });
				if (spl.Length > 1)
				{
					str = spl[1];
				}
				else
				{
					str = spl[0];
				}
				if (str.ToLower() == "id")
					continue; //Skips printing "id" column
				if (vuePerson[person_index][str] is DateTime)
				{
					DateTime t = (DateTime)vuePerson[person_index][str];
					string s = string.Format("{0:00}.{1:00}.{2}", t.Day, t.Month, t.Year);
					m_objSheet.Cells[CurrentRow, 4 + j] = s;
				}
				else
				{
					m_objSheet.Cells[CurrentRow, 4 + j] = vuePerson[person_index][str].ToString();
				}
			}
		}

		private void PrintCustomColumnHeaders(ArrayList Columns)
		{
			int offset = 4;
			m_objSheet.Cells[1, 1] = "Код";
			m_objSheet.Cells[1, 2] = "Структурни звена";
			m_objSheet.Cells[1, 3] = "Длъжност";
			for (int i = 0; i < Columns.Count; i++)
			{
				string[] spl = Columns[i].ToString().Split(new char[] { '.' });
				string str;
				if (spl.Length > 1)
				{
					str = spl[1];
				}
				else
				{
					str = spl[0];
				}
				switch (str.ToLower())
				{
					case "contract":
						{
							m_objSheet.Cells[1, i + offset] = "Договор";
							break;
						}
					case "worktime":
						{
							m_objSheet.Cells[1, i + offset] = "Работно време";
							break;
						}
					case "assignedat":
						{
							m_objSheet.Cells[1, i + offset] = "Назначен на";
							break;
						}
					case "typepenalty":
						{
							m_objSheet.Cells[1, i + offset] = "Вид наказание";
							break;
						}
					case "penaltydatefrom":
						{
							m_objSheet.Cells[1, i + offset] = "Валидно от";
							break;
						}
					case "dateto":
						{
							m_objSheet.Cells[1, i + offset] = "Валидно до";
							break;
						}
					case "reason":
						{
							m_objSheet.Cells[1, i + offset] = "Причина";
							break;
						}
					case "numberorder":
						{
							m_objSheet.Cells[1, i + offset] = "Номер на заповед";
							break;
						}
					case "year":
						{
							m_objSheet.Cells[1, i + offset] = "Година";
							break;
						}
					case "leftover":
						{
							m_objSheet.Cells[1, i + offset] = "Остатък";
							break;
						}
					case "total":
						{
							m_objSheet.Cells[1, i + offset] = "Полагаем";
							break;
						}
					case "fromdate":
						{
							m_objSheet.Cells[1, i + offset] = "От дата";
							break;
						}
					case "todate":
						{
							m_objSheet.Cells[1, i + offset] = "До дата";
							break;
						}
					case "countdays":
						{
							m_objSheet.Cells[1, i + offset] = "Брой дни";
							break;
						}
					case "typeabsence":
						{
							m_objSheet.Cells[1, i + offset] = "Вид отсъствие";
							break;
						}
					case "militaryrang":
						{
							m_objSheet.Cells[1, i + offset] = "Военен ранг";
							break;
						}
					case "country":
						{
							m_objSheet.Cells[1, i + offset] = "Държава";
							break;
						}
					case "town":
						{
							m_objSheet.Cells[1, i + offset] = "Град";
							break;
						}
					case "borntown":
						{
							m_objSheet.Cells[1, i + offset] = "Месторождение";
							break;
						}
					case "region":
						{
							m_objSheet.Cells[1, i + offset] = "Област";
							break;
						}
					case "familystatus":
						{
							m_objSheet.Cells[1, i + offset] = "Семеен статус";
							break;
						}
					case "education":
						{
							m_objSheet.Cells[1, i + offset] = "Образование";
							break;
						}
					case "profession":
						{
							m_objSheet.Cells[1, i + offset] = "Професия";
							break;
						}
					case "sciencetitle":
						{
							m_objSheet.Cells[1, i + offset] = "Научно звание";
							break;
						}
					case "languages":
						{
							m_objSheet.Cells[1, i + offset] = "Чужди езици";
							break;
						}
					case "language":
						{
							m_objSheet.Cells[1, i + offset] = "Чужди език";
							break;
						}
					case "name":
						{
							m_objSheet.Cells[1, i + offset] = "Име";
							break;
						}
					case "egn":
						{
							m_objSheet.Cells[1, i + offset] = "ЕГН";
							break;
						}
					case "sex":
						{
							m_objSheet.Cells[1, i + offset] = "Пол";
							break;
						}
					case "staff":
						{
							m_objSheet.Cells[1, i + offset] = "Щат";
							break;
						}
					case "penaltydate":
						{
							m_objSheet.Cells[1, i + offset] = "Дата на наказанието";
							break;
						}
					case "assignreason":
						{
							m_objSheet.Cells[1, i + offset] = "Основание за назначаване";
							break;
						}
					case "pcard":
						{
							m_objSheet.Cells[1, i + offset] = "Л.К. №";
							break;
						}
					case "pcardpublish":
						{
							m_objSheet.Cells[1, i + offset] = "Дата на издаване";
							break;
						}
					case "publishedby":
						{
							m_objSheet.Cells[1, i + offset] = "Издадена от";
							break;
						}
					case "law":
						{
							m_objSheet.Cells[1, i + offset] = "Правоотношение";
							break;
						}
					case "contractexpiry":
						{
							m_objSheet.Cells[1, i + offset] = "Срок на договора до";
							break;
						}
					case "testcontractdate":
						{
							m_objSheet.Cells[1, i + offset] = "Изпитателен срок до";
							break;
						}
					case "basesalary":
						{
							m_objSheet.Cells[1, i + offset] = "Основна заплата";
							break;
						}
					case "years":
						{
							m_objSheet.Cells[1, i + offset] = "Трудов стаж (години)";
							break;
						}
				}
			}
		}

		private void PrintXMLRow(int CurrentRow, ArrayList Vues)
		{
			if (this.dsTemplateStructure != null)
			{
				int tableCount = this.dsTemplateStructure.Tables.Count;
				if (tableCount > 0)
				{
					for (int kl = 0; kl < Vues.Count; kl++)
					{
						ViewsArray va = (ViewsArray)Vues[kl];
						ViewsArray vaPos = (ViewsArray)Vues[0];

						ViewsArray vaPers;
						if (Vues.Count > 1)
						{
							vaPers = (ViewsArray)Vues[1];
						}
						else
						{
							vaPers = (ViewsArray)Vues[0];
						}
						for (int jm = 0; jm < this.dsTemplateStructure.Tables[va.vuename].Rows.Count; jm++)
						{
							DataRow templateRow = this.dsTemplateStructure.Tables[va.vuename].Rows[jm];
							int columnnumber;
							try
							{
								columnnumber = int.Parse(this.dsTemplateStructure.Tables[va.vuename].Rows[jm]["column_number"].ToString());
							}
							catch
							{
								MessageBox.Show("Грешен номер на колона за " + this.dsTemplateStructure.Tables[va.vuename].Rows[jm]["column_text"]);
								continue;
							}
							try
							{
								if (this.dsTemplateStructure.Tables[va.vuename].Rows[jm]["column_text"].ToString().ToLower() == "bim10percent")
								{
									continue;
								}
								if (this.dsTemplateStructure.Tables[va.vuename].Rows[jm]["column_text"].ToString().ToLower() == "smin")
								{
									//add code to get the minimal salary for this position
									var x = va.vue[va.index]["ekdaPayLevel"];
									var y = va.vue[va.index]["id"];
									var deg = vaPers.vue[vaPers.index]["ekdapaydegree"];

									DataView vueEkda = new DataView(this.dtEkdaPayLevels, "LevelName = '" + x + "'", "id_ekdapaylevels", DataViewRowState.CurrentRows);
									DataView vueP = new DataView(this.dtPos, "id = " + y, "id", DataViewRowState.CurrentRows);
									if (vueEkda.Count == 1 && vueP.Count == 1)
									{
										switch (deg.ToString())
										{
											case "1":
												m_objSheet.Cells[CurrentRow, columnnumber] = vueEkda[0]["S1Min"].ToString();
												break;
											case "2":
												m_objSheet.Cells[CurrentRow, columnnumber] = vueEkda[0]["S2Min"].ToString();
												break;
											case "3":
												m_objSheet.Cells[CurrentRow, columnnumber] = vueEkda[0]["S3Min"].ToString();
												break;
											case "4":
												m_objSheet.Cells[CurrentRow, columnnumber] = vueEkda[0]["S4Min"].ToString();
												break;
											case "5":
												m_objSheet.Cells[CurrentRow, columnnumber] = vueEkda[0]["S5Min"].ToString();
												break;
											case "6":
												m_objSheet.Cells[CurrentRow, columnnumber] = vueEkda[0]["S6Min"].ToString();
												break;
										}
									}
									continue;
								}

								if (this.dsTemplateStructure.Tables[va.vuename].Rows[jm]["column_text"].ToString().ToLower() == "smax")
								{
									//add code to get the minimal salary for this position
									var x = va.vue[va.index]["ekdaPayLevel"];
									var y = va.vue[va.index]["id"];
									var deg = vaPers.vue[vaPers.index]["ekdapaydegree"];

									DataView vueEkda = new DataView(this.dtEkdaPayLevels, "LevelName = '" + x + "'", "id_ekdapaylevels", DataViewRowState.CurrentRows);
									DataView vueP = new DataView(this.dtPos, "id = " + y, "id", DataViewRowState.CurrentRows);
									if (vueEkda.Count == 1 && vueP.Count == 1)
									{
										switch (deg.ToString())
										{
											case "1":
												m_objSheet.Cells[CurrentRow, columnnumber] = vueEkda[0]["S1Max"].ToString();
												break;
											case "2":
												m_objSheet.Cells[CurrentRow, columnnumber] = vueEkda[0]["S2Max"].ToString();
												break;
											case "3":
												m_objSheet.Cells[CurrentRow, columnnumber] = vueEkda[0]["S3Max"].ToString();
												break;
											case "4":
												m_objSheet.Cells[CurrentRow, columnnumber] = vueEkda[0]["S4Max"].ToString();
												break;
											case "5":
												m_objSheet.Cells[CurrentRow, columnnumber] = vueEkda[0]["S5Max"].ToString();
												break;
											case "6":
												m_objSheet.Cells[CurrentRow, columnnumber] = vueEkda[0]["S6Max"].ToString();
												break;
										}
									}
									continue;
								}
								if (this.dsTemplateStructure.Tables[va.vuename].Rows[jm]["column_text"].ToString().ToLower() == "SMax")
								{
									//add code to get the maximal salary for this position
									continue;
								}
								m_objSheet.Cells[CurrentRow, columnnumber] = va.vue[va.index][this.dsTemplateStructure.Tables[va.vuename].Rows[jm]["column_text"].ToString()].ToString();
							}
							catch (Exception ex)
							{
								var x = ex.Message;
								// To do Add log error code here
							}
						}
					}
				}
			}
		}
		/// <summary>
		/// Summary description for ExcelExpo.
		/// </summary>
		public void ExtractRangUpdate(mainForm main, DateTime refDate)
		{
			int CurrentRow = 2;
			DataTable dtFirmPersonal = new DataTable();
			DataView vuePositions = new DataView();

			this.dtTree = main.nomenclaatureData.dtTreeTable;

			DataAction da = new DataAction(main.connString);

			this.dtPos = da.SelectWhere(TableNames.FirmPersonal3, "*", " ORDER BY id");
			this.dtAssignments = da.SelectWhere(TableNames.PersonAssignment, "*", " ORDER BY id");
			this.dtRang = da.SelectWhere(TableNames.MilitaryRang, "*", " WHERE israngupdate = 1 ORDER BY id");
			this.dtRang.Columns["parent"].ColumnName = "par";
			this.dtPersons = da.SelectWhere(TableNames.Person, "*", " ORDER BY id");
			this.dtPenalty = da.SelectWhere(TableNames.Penalty, "*", " ORDER BY id");
			this.dtPenalty.Columns["parent"].ColumnName = "par";

			if (this.dtPos == null || this.dtAssignments == null || this.dtPersons == null || this.dtRang == null)
			{
				MessageBox.Show("Грешка при зареждане на данните за лицата", ErrorMessages.NoConnection);
				return;
			}

			System.Threading.ThreadStart dele = new System.Threading.ThreadStart(threadStart);
			System.Threading.Thread th = new System.Threading.Thread(dele);
			this.form = new formWait("MilitaryRang");
			th.Start();
			try
			{
				m_objExcel = new Excel.Application();
			}
			catch
			{
				MessageBox.Show("На компютъра няма инсталиран Microsoft Excel.");
				return;
			}
			try
			{
				// Open a workbook in Excel
				m_objBook = m_objExcel.Workbooks.Open(Application.StartupPath +
					"\\TemplateMilitaryRangs.xls", vk_update_links, vk_read_only, vk_format, vk_password,
					vk_write_res_password, vk_ignore_read_only_recommend, vk_origin,
					vk_delimiter, vk_editable, vk_notify, vk_converter, vk_add_to_mru
					);
			}
			catch (Exception e)
			{
				MessageBox.Show("Липсва шаблонен файл", e.Message);
				return;
			}

			m_objSheets = (Excel.Sheets)m_objBook.Worksheets;
			m_objSheet = (Excel._Worksheet)(m_objSheets.Item[1]);

			int CountPersons = 1;
			this.ExportRangLevel(0, ref CurrentRow, ref CountPersons, refDate);

			m_objRange = m_objSheet.Range[m_objSheet.Cells[1, 1], m_objSheet.Cells[CurrentRow, 15]];
			m_objRange.EntireColumn.AutoFit();
			m_objExcel.Visible = true;

			ReleaseExcelApplication();
			form.SetReferencePoint();
			form.StoreIncrements();
			th.Abort();

		}

		private void ExportRangLevel(int parrot, ref int CurrentRow, ref int CountPersons, DateTime refDate)
		{
			int par;
			DataView vuePositions;
			DataViewRowState dvrs = DataViewRowState.CurrentRows;
			DataView vueTree = new DataView(this.dtTree, "par = " + parrot.ToString(), "id", dvrs);
			DataView vueAssignments, vuePersons, vueRangs;

			tDict = new Dictionary<int, int>();

			//			level descriptor  englevel
			//Генерал - майор   militarydegree  14
			//Бригаден генерал    militarydegree  13
			//Полковник I ст.militarydegree  12
			//Полковник II ст.militarydegree  12
			//Полковник III ст.militarydegree  12
			//Полковник IV ст.militarydegree  12
			//Полковник V ст.militarydegree  12
			//Подполковник I ст.militarydegree  11
			//Подполковник II ст.militarydegree  11
			//Подполковник III ст.militarydegree  11
			//Подполковник IV ст.militarydegree  11
			//Подполковник V ст.militarydegree  11
			//Подполковник VI ст.militarydegree  11
			//Майор I ст.militarydegree  10
			//Майор II ст.militarydegree  10
			//Майор III ст.militarydegree  10
			//Майор IV ст.militarydegree  10
			//Майор V ст.militarydegree  10
			//Майор VI ст.militarydegree  10
			//Капитан I ст.militarydegree  9
			//Капитан II ст.militarydegree  9
			//Капитан III ст.militarydegree  9
			//Капитан IV ст.militarydegree  9
			//Капитан V ст.militarydegree  9
			//Капитан VI ст.militarydegree  9
			//Старши лейтенант I ст.	militarydegree  8
			//Старши лейтенант II ст.	militarydegree  8
			//Старши лейтенант III ст.	militarydegree  8
			//Старши лейтенант IV ст.	militarydegree  8
			//Старши лейтенант V ст.	militarydegree  8
			//Старши лейтенант VI ст.	militarydegree  8
			//Лейтенант I ст.militarydegree  7
			//Лейтенант II ст.militarydegree  7
			//Лейтенант III ст.militarydegree  7
			//Лейтенант IV ст.militarydegree  7
			//Лейтенант V ст.militarydegree  7
			//Лейтенант VI ст.militarydegree  7
			//Младши лейтенант I ст.	militarydegree  6
			//Главен старшина 1 - ви клас militarydegree  5
			//Старшина I ст.militarydegree  4
			//Старшина II ст.militarydegree  4
			//Старшина III ст.militarydegree  4
			//Старши сержант I ст.	militarydegree  3
			//Сержант I ст.militarydegree  2
			//Младши сержант I ст.	militarydegree  1



			tDict.Add(1, 2);
			tDict.Add(2, 3);
			tDict.Add(3, 5);
			tDict.Add(4, 5);
			tDict.Add(5, 7);

			tDict.Add(7, 2);
			tDict.Add(8, 3);
			tDict.Add(9, 3);
			tDict.Add(10, 4);
			tDict.Add(11, 4);

			for (int i = 0; i < vueTree.Count; i++)
			{
				try
				{
					par = int.Parse(vueTree[i]["par"].ToString());
				}
				catch (System.Exception e)
				{
					MessageBox.Show(e.Message, "Грешни данни ExctractRang par1");
					par = 0;
				}
				if (par == parrot)
				{
					int NodeId;
					try
					{
						NodeId = int.Parse(vueTree[i]["id"].ToString());
					}
					catch (System.Exception e)
					{
						MessageBox.Show(e.Message, "Грешни данни ExtractRang NodeId");
						NodeId = 0;
					}
					string NodeText = vueTree[i]["level"].ToString();

					string cond = "par = " + NodeId.ToString();
					vuePositions = new DataView(dtPos, cond, "id", dvrs);

					//Име на administraciq
					m_objSheet.Cells[CurrentRow, 2] = NodeText;
					m_objSheet.Cells[CurrentRow, 1] = vueTree[i]["code"].ToString();

					m_objRange = m_objSheet.Range[m_objSheet.Cells[CurrentRow, 1], m_objSheet.Cells[CurrentRow, 2]];
					m_objRange.Font.Bold = true;
					CurrentRow++;
					for (int pk = 0; pk < vuePositions.Count; pk++)
					{
						vueAssignments = new DataView(this.dtAssignments, "isActive = 1 and positionID = " + vuePositions[pk]["id"], "id", dvrs);
						for (int jm = 0; jm < vueAssignments.Count; jm++)
						{
							vueRangs = new DataView(this.dtRang, "israngupdate = 1 and par = " + vueAssignments[jm]["parent"], "id", dvrs);
							vuePersons = new DataView(dtPersons, "id = " + vueAssignments[jm]["parent"], "id", dvrs);
							if (vueRangs.Count > 0 && vuePersons.Count == 1)
							{
								DateTime compdate;
								if (DateTime.TryParse(vueRangs[vueRangs.Count - 1]["rangordervalidfrom"].ToString(), out compdate))
								{
									int key;
									if (int.TryParse(vuePositions[pk]["positioneng"].ToString(), out key))
									{
										int ra;
										if (int.TryParse(vueRangs[vueRangs.Count - 1]["rangweight"].ToString(), out ra))
										{
											if (tDict.ContainsKey(ra))
											{
												int v1, v2;
												v1 = refDate.Subtract(compdate).Days; //DateTime.Now.Subtract(compdate).Days;
												v2 = tDict[ra] * 365;
												if (v1 >= v2)
												{
													this.PrintRangRow(CurrentRow, vuePersons, 0, CountPersons, vuePositions, pk, ra);
													CountPersons++;
													CurrentRow++;
												}
											}
										}
									}
								}
							}
						}
					}
					this.ExportRangLevel(NodeId, ref CurrentRow, ref CountPersons, refDate);
				}
			}
		}

		private void PrintRangRow(int CurrentRow, DataView vuePerson, int person_index, int CountPersons, DataView vuePositions, int pos_idx, int rangweight)
		{
			DataView vueRangs = new DataView(this.dtRang, "israngupdate = 1 and par = " + vuePerson[0]["id"], "id", DataViewRowState.CurrentRows);
			string Condition = "par = " + vuePerson[person_index]["id"] + " and " + String.Format(System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat, "Orderdate > #{0}#", new DateTime(DateTime.Now.Year - 1, DateTime.Now.Month, DateTime.Now.Day));
			DataView vuePenalty = new DataView(this.dtPenalty, Condition, "id", DataViewRowState.CurrentRows);
			//Номер по ред
			m_objSheet.Cells[CurrentRow, 1] = CountPersons.ToString() + ".";
			//Име на лицето - 1
			m_objSheet.Cells[CurrentRow, 2] = vuePerson[person_index]["name"];
			//Persons current military rang
			m_objSheet.Cells[CurrentRow, 3] = vuePerson[person_index]["militaryrang"];

			m_objSheet.Cells[CurrentRow, 4] = vueRangs[vueRangs.Count - 1]["rangordernumber"];

			m_objSheet.Cells[CurrentRow, 5] = vueRangs[vueRangs.Count - 1]["rangordervalidfrom"];

			m_objSheet.Cells[CurrentRow, 6] = vueRangs[vueRangs.Count - 1]["rangorderdate"];

			bool penalty = false, position = false;
			int key;
			if (int.TryParse(vuePositions[pos_idx]["positioneng"].ToString(), out key))
			{

				if (rangweight >= key)//if position weight is lower than current rang
				{
					position = true;
				}
			}
			//dataView.RowFilter = String.Format(CultureInfo.InvariantCulture.DateTimeFormat, "Date = #{0}#", new DateTime(2008, 12, 31, 16, 44, 58));
			if (vuePenalty.Count > 0)
			{
				penalty = true;
			}

			if (penalty && position)
			{
				m_objRange = m_objSheet.Range[m_objSheet.Cells[CurrentRow, 1], m_objSheet.Cells[CurrentRow, 6]];
				m_objRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
			}
			else if (penalty)
			{
				m_objRange = m_objSheet.Range[m_objSheet.Cells[CurrentRow, 1], m_objSheet.Cells[CurrentRow, 6]];
				m_objRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange);
			}
			else if (position)
			{
				m_objRange = m_objSheet.Range[m_objSheet.Cells[CurrentRow, 1], m_objSheet.Cells[CurrentRow, 6]];
				m_objRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Magenta);
			}
			else
			{
				m_objRange = m_objSheet.Range[m_objSheet.Cells[CurrentRow, 1], m_objSheet.Cells[CurrentRow, 6]];
				m_objRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
			}
		}

		/// <summary>
		/// Summary description for ExcelExpo.
		/// </summary>
		public void ExtractImportantHoliday(mainForm main)
		{
			int CurrentRow = 2;
			DataTable dtFirmPersonal = new DataTable();
			DataView vuePositions = new DataView();

			this.dtTree = main.nomenclaatureData.dtTreeTable;

			DataAction da = new DataAction(main.connString);

			this.dtPos = da.SelectWhere(TableNames.FirmPersonal3, "*", " ORDER BY id");
			this.dtAssignments = da.SelectWhere(TableNames.PersonAssignment, "*", " ORDER BY id");
			this.dtHoliday = da.SelectWhere(TableNames.YearHoliday, "*", " ORDER BY id");
			this.dtPersons = da.SelectWhere(TableNames.Person, "*", " ORDER BY id");
			this.dtHoliday.Columns["parent"].ColumnName = "par";

			if (this.dtPos == null || this.dtAssignments == null || this.dtHoliday == null || this.dtPersons == null || this.dtHoliday == null)
			{
				MessageBox.Show("Грешка при зареждане на данните за лицата", ErrorMessages.NoConnection);
				return;
			}

			System.Threading.ThreadStart dele = new System.Threading.ThreadStart(threadStart);
			System.Threading.Thread th = new System.Threading.Thread(dele);
			this.form = new formWait("Holiday");
			th.Start();
			try
			{
				m_objExcel = new Excel.Application();
			}
			catch
			{
				MessageBox.Show("На компютъра няма инсталиран Microsoft Excel.");
				return;
			}
			try
			{
				// Open a workbook in Excel
				m_objBook = m_objExcel.Workbooks.Open(Application.StartupPath +
					"\\TemplateImportantHoliday.xls", vk_update_links, vk_read_only, vk_format, vk_password,
					vk_write_res_password, vk_ignore_read_only_recommend, vk_origin,
					vk_delimiter, vk_editable, vk_notify, vk_converter, vk_add_to_mru
					);
			}
			catch (Exception e)
			{
				MessageBox.Show("Липсва шаблонен файл", e.Message);
				return;
			}


			m_objSheets = (Excel.Sheets)m_objBook.Worksheets;
			m_objSheet = (Excel._Worksheet)(m_objSheets.Item[1]);

			int CountPersons = 1;
			this.ExportImportantHolidayLevel(0, ref CurrentRow, ref CountPersons);

			m_objRange = m_objSheet.Range[m_objSheet.Cells[1, 1], m_objSheet.Cells[CurrentRow, 15]];
			m_objRange.EntireColumn.AutoFit();
			m_objExcel.Visible = true;

			ReleaseExcelApplication();
			form.SetReferencePoint();
			form.StoreIncrements();
			th.Abort();

		}

		private void ExportImportantHolidayLevel(int parrot, ref int CurrentRow, ref int CountPersons)
		{
			int par;
			DataView vuePositions;
			DataViewRowState dvrs = DataViewRowState.CurrentRows;
			DataView vueTree = new DataView(this.dtTree, "par = " + parrot.ToString(), "id", dvrs);
			DataView vueAssignments, vuePersons;

			for (int i = 0; i < vueTree.Count; i++)
			{
				try
				{
					par = int.Parse(vueTree[i]["par"].ToString());
				}
				catch (System.Exception e)
				{
					MessageBox.Show(e.Message, "Грешни данни ExctractHoliday par1");
					par = 0;
				}
				if (par == parrot)
				{
					int NodeId;
					try
					{
						NodeId = int.Parse(vueTree[i]["id"].ToString());
					}
					catch (System.Exception e)
					{
						MessageBox.Show(e.Message, "Грешни данни ExtractHoliday NodeId");
						NodeId = 0;
					}

					string cond = "par = " + NodeId.ToString();
					vuePositions = new DataView(dtPos, cond, "id", dvrs);

					for (int pk = 0; pk < vuePositions.Count; pk++)
					{
						vueAssignments = new DataView(dtAssignments, "isActive = 1 and positionID = " + vuePositions[pk]["id"], "id", dvrs);
						for (int jm = 0; jm < vueAssignments.Count; jm++)
						{
							vuePersons = new DataView(dtPersons, "id = " + vueAssignments[jm]["parent"], "id", dvrs);
							if (vuePersons.Count > 0)
							{
								if (vuePositions[pk]["otherrequirements"].ToString() == "*")
								{
									this.PrintImportantHolidayRow(CurrentRow, pk, dtHoliday, vuePersons, 0, CountPersons);
									CountPersons++;
									CurrentRow++;
								}
							}
						}
					}
					this.ExportImportantHolidayLevel(NodeId, ref CurrentRow, ref CountPersons);
				}
			}
		}

		private void PrintImportantHolidayRow(int CurrentRow, int pk, DataTable dtHoliday, DataView dtPerson, int person_index, int CountPersons)
		{
			DataViewRowState dvrs = DataViewRowState.CurrentRows;
			string Condition = "par = " + dtPerson[person_index]["id"];
			DataView vueHoliday = new DataView(dtHoliday, Condition, "year", dvrs);
			int days;
			int nodeid = (int)dtPerson[person_index]["nodeid"];

			int depth = this.GetDepth(nodeid);

			this.PrintImportantHolidayStruct(depth, nodeid, CurrentRow);
			//Номер по ред
			m_objSheet.Cells[CurrentRow, 1] = CountPersons.ToString() + ".";
			//Име на лицето - 1
			m_objSheet.Cells[CurrentRow, 6] = dtPerson[person_index]["name"];
			//2001
			for (int i = 0; i < vueHoliday.Count; i++)
			{
				switch (vueHoliday[i]["year"].ToString())
				{
					case "2001":
						{
							try
							{
								days = (int)vueHoliday[i]["leftover"];
							}
							catch
							{
								days = 0;
							}
							m_objSheet.Cells[CurrentRow, 7] = days.ToString();
							break;
						}
					case "2002":
						{
							try
							{
								days = (int)vueHoliday[i]["leftover"];
							}
							catch
							{
								days = 0;
							}
							m_objSheet.Cells[CurrentRow, 8] = days.ToString();
							break;
						}
					case "2003":
						{
							try
							{
								days = (int)vueHoliday[i]["leftover"];
							}
							catch
							{
								days = 0;
							}
							m_objSheet.Cells[CurrentRow, 9] = days.ToString();
							break;
						}
					case "2004":
						{
							try
							{
								days = (int)vueHoliday[i]["leftover"];
							}
							catch
							{
								days = 0;
							}
							m_objSheet.Cells[CurrentRow, 10] = days.ToString();
							break;
						}
					case "2005":
						{
							try
							{
								days = (int)vueHoliday[i]["leftover"];
							}
							catch
							{
								days = 0;
							}
							m_objSheet.Cells[CurrentRow, 11] = days.ToString();
							break;
						}
					case "2006":
						{
							try
							{
								days = (int)vueHoliday[i]["leftover"];
							}
							catch
							{
								days = 0;
							}
							m_objSheet.Cells[CurrentRow, 12] = days.ToString();
							break;
						}
					case "2007":
						{
							try
							{
								days = (int)vueHoliday[i]["leftover"];
							}
							catch
							{
								days = 0;
							}
							m_objSheet.Cells[CurrentRow, 13] = days.ToString();
							break;
						}
					case "2008":
						{
							try
							{
								days = (int)vueHoliday[i]["leftover"];
							}
							catch
							{
								days = 0;
							}
							m_objSheet.Cells[CurrentRow, 14] = days.ToString();
							break;
						}
					case "2009":
						{
							try
							{
								days = (int)vueHoliday[i]["leftover"];
							}
							catch
							{
								days = 0;
							}
							m_objSheet.Cells[CurrentRow, 15] = days.ToString();
							break;
						}
					case "2010":
						{
							try
							{
								days = (int)vueHoliday[i]["leftover"];
							}
							catch
							{
								days = 0;
							}
							m_objSheet.Cells[CurrentRow, 16] = days.ToString();
							break;
						}
					case "2011":
						{
							try
							{
								days = (int)vueHoliday[i]["leftover"];
							}
							catch
							{
								days = 0;
							}
							m_objSheet.Cells[CurrentRow, 17] = days.ToString();
							break;
						}
					case "2012":
						{
							try
							{
								days = (int)vueHoliday[i]["leftover"];
							}
							catch
							{
								days = 0;
							}
							m_objSheet.Cells[CurrentRow, 18] = days.ToString();
							break;
						}
					case "2013":
						try
						{
							days = (int)vueHoliday[i]["leftover"];
						}
						catch
						{
							days = 0;
						}
						m_objSheet.Cells[CurrentRow, 19] = days.ToString();
						break;

				}
			}
		}

		void PrintImportantHolidayStruct(int depth, int nodeid, int CurrentRow)
		{
			for (int i = depth; i > 0; i--)
			{
				DataView Vuet = new DataView(this.dtTree, "id = " + nodeid, "id", DataViewRowState.CurrentRows);
				m_objSheet.Cells[CurrentRow, i + 1] = Vuet[0]["level"];
				nodeid = (int)Vuet[0]["par"];
			}
		}

		int GetDepth(int nodeid)
		{
			int i = 0;

			int cid = nodeid;

			while (cid != 0)
			{
				DataView vueTreee = new DataView(this.dtTree, "id = " + cid, "id", DataViewRowState.CurrentRows);
				cid = (int)vueTreee[0]["par"];
				i++;
			}

			return i;
		}

		public void ExportSyscoAbsences(mainForm main, DateTime RefDate)
		{
			try
			{
				int CurrentRow = 5;

				this.dtTree = main.nomenclaatureData.dtTreeTable;

				DataAction da = new DataAction(main.connString);
				this.dtPos = da.SelectWhere(TableNames.FirmPersonal3, "*", " ORDER BY id");
				this.dtAssignments = da.SelectWhere(TableNames.Person, " HR_Person.Name,HR_personassignment.id, HR_personassignment.parent, HR_personassignment.positionid as posid, isactive, id_sysco", " left join HR_personassignment on HR_Person.id = HR_personassignment.parent where HR_personassignment.isactive = 1"); //select HR_Person.Name, HR_personassignment.parent, HR_personassignment.positionid as posid from HR_Person left join HR_personassignment on HR_Person.id = HR_personassignment.parent where HR_personassignment.isactive = 1
				string holidayWhere = " left join HR_personassignment on HR_Person.id = HR_personassignment.parent left join HR_absence on HR_absence.parent = HR_Person.id where HR_personassignment.isactive = 1";
				string additional = " AND ( (";
				additional += DataAction.DateComparison(RefDate, ComparisonOperators.eGreater, TableNames.Absence, "FromDate");
				additional += " AND ";
				additional += DataAction.DateComparison(RefDate.AddDays(7), ComparisonOperators.eLess, TableNames.Absence, "FromDate");
				additional += " ) OR ( ";
				additional += DataAction.DateComparison(RefDate, ComparisonOperators.eGreater, TableNames.Absence, "ToDate");
				additional += " AND ";
				additional += DataAction.DateComparison(RefDate.AddDays(7), ComparisonOperators.eLess, TableNames.Absence, "ToDate");
				additional += " ) OR ( ";
				additional += DataAction.DateComparison(RefDate, ComparisonOperators.eLess, TableNames.Absence, "FromDate");
				additional += " AND ";
				additional += DataAction.DateComparison(RefDate.AddDays(7), ComparisonOperators.eGreater, TableNames.Absence, "ToDate");
				additional += " ) ) ";

				holidayWhere += additional;
				this.dtHoliday = da.SelectWhere(TableNames.Person, " HR_Person.Name, id_sysco, HR_personassignment.id, HR_personassignment.parent, HR_personassignment.positionid as posid, isactive, HR_Absence.TypeAbsence", holidayWhere);

				string FPWhere = " WHERE ";
				FPWhere += DataAction.DateComparison(RefDate, ComparisonOperators.eGreater, "UN_PresenceForms", "Date");
				FPWhere += " AND ";
				FPWhere += DataAction.DateComparison(RefDate.AddDays(7), ComparisonOperators.eLess, "UN_PresenceForms", "Date");

				this.dtPersons = da.SelectWhere("UN_PresenceForms", "*", FPWhere);

				if (this.dtPos == null)
				{
					MessageBox.Show("Грешка при зареждане на структурата на организацията", ErrorMessages.NoConnection);
					return;
				}

				try
				{
					m_objExcel = new Excel.Application();
				}
				catch
				{
					MessageBox.Show("На компютъра няма инсталиран Microsoft Excel.");
					return;
				}

				try
				{
					this.dsTemplateStructure = new DataSet();
					this.dsTemplateStructure.ReadXml(Application.StartupPath + @"\XMLLabels\OSR.xml");
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					dsTemplateStructure = null;
				}

				System.Threading.ThreadStart dele = new System.Threading.ThreadStart(threadStart);
				System.Threading.Thread th = new System.Threading.Thread(dele);
				this.form = new formWait("OSR");
				th.Start();
				try
				{
					// Open a workbook in Excel
					m_objBook = m_objExcel.Workbooks.Open(Application.StartupPath +
						"\\SyscosetAbsences.xlsx", vk_update_links, vk_read_only, vk_format, vk_password,
						vk_write_res_password, vk_ignore_read_only_recommend, vk_origin,
						vk_delimiter, vk_editable, vk_notify, vk_converter, vk_add_to_mru
						);
				}
				catch (Exception e)
				{
					MessageBox.Show("Липсва шаблонен файл", e.Message);
					return;
				}

				m_objSheets = (Excel.Sheets)m_objBook.Worksheets;
				m_objSheet = (Excel._Worksheet)(m_objSheets.Item[1]);

				ExportSyscoAbsencesLevel(0, ref CurrentRow, RefDate);

				m_objExcel.Visible = true;

				ReleaseExcelApplication();
				form.SetReferencePoint();
				form.StoreIncrements();
				th.Abort();
			}
			catch (Exception ex)
			{
				ErrorLog.WriteException(ex, ex.Message);
				MessageBox.Show(ex.Message);
			}
		}

		private void ExportSyscoAbsencesLevel(int parrot, ref int CurrentRow, DateTime RefDate)
		{
			int par;
			DataViewRowState dvrs = DataViewRowState.CurrentRows;
			DataView vueTree = new DataView(this.dtTree, "par = " + parrot.ToString(), "id", dvrs);
			DataView vuePositions, vueAssignments, vueHolidays, vuePF;

			for (int i = 0; i < vueTree.Count; i++)
			{
				try
				{
					par = int.Parse(vueTree[i]["par"].ToString());
				}
				catch (System.Exception e)
				{
					MessageBox.Show(e.Message, "Грешни данни ExportOSR");
					par = 0;
				}

				if (par == parrot)
				{
					int NodeId;

					try
					{
						NodeId = int.Parse(vueTree[i]["id"].ToString());
					}
					catch (System.Exception e)
					{
						MessageBox.Show(e.Message, "Грешни данни ExportOSR");
						NodeId = 0;
					}

					string NodeText = vueTree[i]["level"].ToString();

					//Check all data for all positions in the node
					string cond = "par = " + NodeId.ToString();
					vuePositions = new DataView(this.dtPos, cond, "id", dvrs);
					int totalInDepartment = 0;
					int maternity = 0;
					int sickness = 0;
					int holiday = 0;
					int compensation = 0;
					int businesstrip = 0;

					for (int pk = 0; pk < vuePositions.Count; pk++)
					{
						vueAssignments = new DataView(this.dtAssignments, "isactive = 1 and posid = " + vuePositions[pk]["id"], "id", dvrs);
						totalInDepartment += vueAssignments.Count;

						vueHolidays = new DataView(dtHoliday, "TypeAbsence = 'Отглеждане на дете' and posid = " + vuePositions[pk]["id"], "id", dvrs);
						maternity += vueHolidays.Count;

						vueHolidays = new DataView(dtHoliday, "TypeAbsence = 'Болнични' and posid = " + vuePositions[pk]["id"], "id", dvrs);
						sickness += vueHolidays.Count;

						vueHolidays = new DataView(dtHoliday, "(TypeAbsence = 'Полагаем годишен отпуск' or TypeAbsence = 'Неплатен отпуск' or TypeAbsence = 'Полагаем отпуск ТЕЛК' or TypeAbsence = 'Полагаем отпуск обучение' or TypeAbsence = 'Полагаем отпуск друг') and posid = " + vuePositions[pk]["id"], "id", dvrs);
						holiday += vueHolidays.Count;

						for (int cas = 0; cas < vueAssignments.Count; cas++)
						{
							vuePF = new DataView(this.dtPersons, "id_presenceType = 7 and id_user = " + vueAssignments[cas]["id_sysco"].ToString(), "id_presenceform", dvrs);
							if (vuePF.Count > 0)
							{
								compensation++;
							}
							vuePF = new DataView(this.dtPersons, "id_presenceType = 14 and id_user = " + vueAssignments[cas]["id_sysco"].ToString(), "id_presenceform", dvrs);
							if (vuePF.Count > 0)
							{
								businesstrip++;
							}
						}
					}

					m_objSheet.Cells[CurrentRow, 2] = totalInDepartment;
					m_objSheet.Cells[CurrentRow, 3] = maternity;
					m_objSheet.Cells[CurrentRow, 5] = sickness;
					m_objSheet.Cells[CurrentRow, 6] = holiday;
					m_objSheet.Cells[CurrentRow, 7] = compensation;
					m_objSheet.Cells[CurrentRow, 8] = businesstrip;

					m_objSheet.Cells[CurrentRow, 1] = NodeText;
					m_objRange = m_objSheet.Range[m_objSheet.Cells[CurrentRow, 2], m_objSheet.Cells[CurrentRow, 2]];
					m_objRange.Font.Bold = true;
					CurrentRow++;

					ExportSyscoAbsencesLevel(NodeId, ref CurrentRow, RefDate);
				}
			}
		}
	}
}