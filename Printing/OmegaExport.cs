using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataLayer;

namespace HR.Printing
{
	public partial class OmegaExport : Form
	{
		private DataAction dataAdapter;
		private bool IsAdditional;

		public OmegaExport(mainForm form, bool isAdditional)
		{
			InitializeComponent();
			dataAdapter = new DataAction(form.connString);
			this.IsAdditional = isAdditional;

			if (isAdditional == true)
			{
				this.Text = "Експорт на данни за допълнителни споразумения към Омега";
			}
		}

		private DataTable GetEmployees()
		{
			DataTable dtAssignments;
			if (IsAdditional == false)
			{
				string where = string.Format("LEFT JOIN {1} on {0}.id = {1}.parent LEFT JOIN {4} on {1}.positionid = {4}.id LEFT JOIN {5} on {5}.id = {4}.par WHERE {2} AND {3} AND {1}.isadditionalassignment = 0", TableNames.Person, TableNames.PersonAssignment, DataAction.DateComparison(this.dateTimePickerStart.Value.Date, ComparisonOperators.eGreater, TableNames.PersonAssignment, "assignedat"), DataAction.DateComparison(this.dateTimePickerEnd.Value.Date, ComparisonOperators.eLess, TableNames.PersonAssignment, "assignedat"), TableNames.FirmPersonal3, TableNames.NewTree2);
				dtAssignments = dataAdapter.SelectWhere(TableNames.Person, "*", @where);
			}
			else
			{
				string where = string.Format("LEFT JOIN {1} on {0}.id = {1}.parent LEFT JOIN {4} on {1}.positionid = {4}.id LEFT JOIN {5} on {5}.id = {4}.par WHERE {2} AND {3} AND {1}.isadditionalassignment = 1 AND isactive = 1", TableNames.Person, TableNames.PersonAssignment, DataAction.DateComparison(this.dateTimePickerStart.Value.Date, ComparisonOperators.eGreater, TableNames.PersonAssignment, "assignedat"), DataAction.DateComparison(this.dateTimePickerEnd.Value.Date, ComparisonOperators.eLess, TableNames.PersonAssignment, "assignedat"), TableNames.FirmPersonal3, TableNames.NewTree2);
				dtAssignments = dataAdapter.SelectWhere(TableNames.Person, "*", @where);
			}
			return dtAssignments;
		}

		private void buttonExport_Click(object sender, EventArgs e)
		{
			DataTable dtAssignments = new DataTable();
			DataTable dtFired = new DataTable();

			dtAssignments = GetEmployees();

			//where = string.Format("LEFT JOIN {1} on {0}.id = {1}.parent left join {4} on {0}.id = {4}.parent WHERE {2} AND {3} and {0}.fired = 1", TableNames.Person, TableNames.PersonAssignment, DataAction.DateComparison(this.dateTimePickerStart.Value.Date, ComparisonOperators.eGreater, TableNames.Fired, "FromDate"), DataAction.DateComparison(this.dateTimePickerEnd.Value.Date, ComparisonOperators.eLess, TableNames.Fired, "FromDate"), TableNames.Fired);
			//dtFired = dataAdapter.SelectWhere(TableNames.Person, "*", where);

			SaveFileDialog ofd = new SaveFileDialog();
			ofd.Title = "Моля изберете файл в който да се запишат данните от експорта";
			ofd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
			
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				System.IO.StreamWriter file = new System.IO.StreamWriter(ofd.FileName, false, Encoding.Default);
				foreach (DataRow row in dtAssignments.Rows)
				{
					//validations before file write
					if (string.IsNullOrEmpty(row["Position"].ToString()) == true)
					{
						MessageBox.Show(string.Format("Няма длъжност за служител {0}, {1}. Данните за него няма да бъдат експортирани", row["id_sysco"], row["Name"]));
						continue;
					}
					//validations end

					file.Write(row["code"].ToString() + row["id_sysco"].ToString());
					file.Write(";");
					file.Write(row["Name"].ToString());
					file.Write(";");
					file.Write(row["EGN"].ToString());
					file.Write(";");
                    switch (row["Sex"].ToString().ToLower())
                    {
                        case "мъж":
                            file.Write("м");
                            break;
                        case "жена":
                            file.Write("ж");
                            break;
                        default:
                            file.Write("м");
                            break;
                    }
					
					file.Write(";");

                    if (row["Position"].ToString().Length > 30)
                    {
                        file.Write(row["Position"].ToString().Remove(30));
                    }
                    else
                    {
                        file.Write(row["Position"].ToString());
                    }

					file.Write(";");
					DateTime assign;
					DateTime.TryParse(row["assignedat"].ToString(), out assign);
					file.Write(string.Format("{0:00}", assign.Day) + "." + string.Format("{0:00}", assign.Month) + "." + string.Format("{0:00}", assign.Year));
					file.Write(";");
					int y, m, d;
					int.TryParse(row["years"].ToString(), out y);
					int.TryParse(row["months"].ToString(), out m);
					int.TryParse(row["days"].ToString(), out d);

					file.Write(string.Format("{0:00}", y) + string.Format("{0:00}", m) + string.Format("{0:00}", d));
					file.Write(";");
					file.Write(string.Format("{0:00}", y) + string.Format("{0:00}", m) + string.Format("{0:00}", d));
					file.Write(";");

					if (row["worktime"].ToString().ToLower() == "пълно, 8 часа")
					{
						file.Write(8.ToString());
					}
					else if (row["worktime"].ToString().ToLower() == "непълно, 4 часа")
					{
						file.Write("4");
					}
					else
					{
						file.Write("8");
					}
					file.Write(";");
					file.Write("1");
					file.Write(";");
					file.Write(row["town"].ToString());
					file.Write(";");
					file.Write(row["kwartal"].ToString());
					file.Write(";");
					file.Write(row["region"].ToString());
					file.Write(";");
					file.Write(row["pcard"].ToString());
					file.Write(";");

					DateTime publish;
					DateTime.TryParse(row["pcardpublish"].ToString(), out publish);
					file.Write(string.Format("{0:00}", publish.Day) + "." + string.Format("{0:00}", publish.Month) + "." + string.Format("{0:00}", publish.Year));
					file.Write(";");
					file.Write(row["publishedby"].ToString());
					file.Write(";");
					file.Write(row["phone"].ToString());
					file.Write(";");
                    if (row["basesalary"].ToString() == "")
                    {
                        file.Write(0.ToString());
                    }
                    else
                    {
                        file.Write(row["basesalary"].ToString()); //phone
                    }
					file.Write(";");
					file.Write(";");
					file.Write(";");
                    file.Write("\r\n");
				}
				file.Close();
				MessageBox.Show("Експортирането приключи успешно");
			}

//1. Служебен номер
//2. Име, презиме, фамилия (може и в отделни полета)
//3. ЕГН
//4. Пол (м/ж)
//5. Длъжност
//6. Дата на назначаване
//7. Трудов стаж по специалността (формат: ГГММДД)
//8 . Трудов стаж общ (формат: ГГММДД)
//9. Работен ден в часове (обикновено 8)
//10. Вид осигурен (ако има такава информация при вас, например 01-Трета категория труд, 04-Допълнителен трудов договор, 10-Договор за управление и т.н. според номенклатурата на НАП)
//11. Адрес – град
//12. Адрес – пощенски код
//13. Адрес – ул./ж.к. ...
//14. Адрес – област
//15. Лична карта №
//16. Л.к. дата на издаване (формат: ДД.ММ.ГГГГ)
//17. Л.к. издадена от 
//18. Телефон
//19. Основна заплата
//20. Банкова сметка
		}
	}
}
