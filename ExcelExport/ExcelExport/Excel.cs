using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using XlHAlign = Microsoft.Office.Interop.Excel.XlHAlign;


namespace ExcelExport
{

    public class ExcelExport
    {
        
        internal Application m_objExcel = null;
        internal Workbooks m_objBooks = null;
        internal _Workbook m_objBook = null;
        internal Sheets m_objSheets = null;
        internal _Worksheet m_objSheet = null;
        internal Range m_objRange = null;

        internal object opt = System.Reflection.Missing.Value;

        public ExcelExport()
        {
            System.Globalization.CultureInfo cultureEn = new System.Globalization.CultureInfo("en-GB");
            System.Threading.Thread.CurrentThread.CurrentCulture = cultureEn;			
        }

        public bool Export(List<System.Data.DataTable> listTables)
        {
            int i, j, k;

            try
            {
                m_objExcel = new Application();

            }
            catch(Exception )
            {
                //TaktWpfControls.ErrorLog.WriteException(exc, "На компютъра няма инсталиран Microsoft Excel.");
                //MessageBox.Show("На компютъра няма инсталиран Microsoft Excel.");
                return false;
            }

            m_objBooks = (Workbooks)m_objExcel.Workbooks;

            m_objBook = (_Workbook)(m_objBooks.Add(opt));
            m_objSheets = (Sheets)m_objBook.Worksheets;

            
            int z = 1;
            while (listTables.Count > m_objSheets.Count)
            {
                m_objSheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            }
            foreach (System.Data.DataTable table in listTables)
            {
                m_objSheet = (_Worksheet)(m_objSheets.get_Item(z));
                m_objSheet.Name = table.TableName + z.ToString();
                z++;
                for (i = 0, j = 0; i < table.Columns.Count; i++, j++)
                {
                    m_objSheet.Cells[1, j + 1] = table.Columns[i].ColumnName;

                }
                if (table.Rows.Count == 0) continue;
                for (i = 0; i < table.Rows.Count; i++)
                {
                    for (j = 0, k = 1; j < table.Columns.Count; j++)
                    {
                        m_objSheet.Cells[i + 2, k] = table.Rows[i][j].ToString();
                        k++;
                    }
                }

                m_objRange = m_objSheet.get_Range(m_objSheet.Cells[1, 1], m_objSheet.Cells[table.Rows.Count, table.Columns.Count]);
                m_objRange.EntireColumn.AutoFit();
                //for (i = 0; i < table.Columns.Count; i ++)
                //{
                //    DataColumn col = table.Columns[i];
                //    if (col.DataType.ToString() == "DateTime")
                //    {
                //        m_objRange = m_objSheet.get_Range(m_objSheet.Cells[1, i], m_objSheet.Cells[table.Rows.Count, i]);
                //        m_objRange.ClearFormats();
                //    }
                //}
            }

            m_objExcel.Visible = true;

            ReleaseExcelApplication();
            return true;
        }

        public bool ExportPF(List<System.Data.DataTable> listTables, string header, DateTime month)
        {
            int i, j, k;

            try
            {
                m_objExcel = new Application();

            }
            catch (Exception )
            {
                //TaktWpfControls.ErrorLog.WriteException(exc, "На компютъра няма инсталиран Microsoft Excel.");
                //MessageBox.Show("На компютъра няма инсталиран Microsoft Excel.");
                return false;
            }

            m_objBooks = (Workbooks)m_objExcel.Workbooks;

            m_objBook = (_Workbook)(m_objBooks.Add(opt));
            m_objSheets = (Sheets)m_objBook.Worksheets;


            int z = 1;
            while (listTables.Count > m_objSheets.Count)
            {
                m_objSheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            }
            foreach (System.Data.DataTable table in listTables)
            {
                m_objSheet = (_Worksheet)(m_objSheets.get_Item(z));
                m_objSheet.Name = table.TableName + z.ToString();
                z++;
                m_objRange = m_objSheet.get_Range(m_objSheet.Cells[1, 1], m_objSheet.Cells[2, 33]);
                m_objRange.Merge(true);
                m_objRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                m_objSheet.Cells[1, 1] = header;
                m_objSheet.Cells[2, 1] = month.Month.ToString() + "." + month.Year.ToString();
                
                for (i = 0, j = 0; i < table.Columns.Count; i++, j++)
                {
                    string customName = "";
                    switch(table.Columns[i].ColumnName.ToLower())
                    {
                        case "idsys":
                            customName = "Номер";
                            break;
                        case "nam":
                            customName = "Име";
                            break;
                        case "da1":
                            customName = "1";
                            break;
                        case "da2":
                            customName = "2";
                            break;
                        case "da3":
                            customName = "3";
                            break;
                        case "da4":
                            customName = "4";
                            break;
                        case "da5":
                            customName = "5";
                            break;
                        case "da6":
                            customName = "6";
                            break;
                        case "da7":
                            customName = "7";
                            break;
                        case "da8":
                            customName = "8";
                            break;
                        case "da9":
                            customName = "9";
                            break;
                        case "da10":
                            customName = "10";
                            break;
                        case "da11":
                            customName = "11";
                            break;
                        case "da12":
                            customName = "12";
                            break;
                        case "da13":
                            customName = "13";
                            break;
                        case "da14":
                            customName = "14";
                            break;
                        case "da15":
                            customName = "15";
                            break;
                        case "da16":
                            customName = "16";
                            break;
                        case "da17":
                            customName = "17";
                            break;
                        case "da18":
                            customName = "18";
                            break;
                        case "da19":
                            customName = "19";
                            break;
                        case "da20":
                            customName = "20";
                            break;
                        case "da21":
                            customName = "21";
                            break;
                        case "da22":
                            customName = "22";
                            break;
                        case "da23":
                            customName = "23";
                            break;
                        case "da24":
                            customName = "24";
                            break;
                        case "da25":
                            customName = "25";
                            break;
                        case "da26":
                            customName = "26";
                            break;
                        case "da27":
                            customName = "27";
                            break;
                        case "da28":
                            customName = "28";
                            break;
                        case "da29":
                            customName = "29";
                            break;
                        case "da30":
                            customName = "30";
                            break;
                        case "da31":
                            customName = "31";
                            break;
                        case "shif":
                            customName = "Смени";
                            break;
                        case "abs":
                            customName = "Отсъствия";
                            break;
                        case "over":
                            customName = "Овъртайм";
                            break;
                        case "tot":
                            customName = "Общо";
                            break;
                        case "nor":
                            customName = "Норма";
                            break;
                        case "diff":
                            customName = "Разлика";
                            break;
                        case "comp":
                            customName = "Компенсация";
                            break;
                    }
                    m_objSheet.Cells[3, j + 1] = customName;
                }
                if (table.Rows.Count == 0) continue;
                for (i = 0; i < table.Rows.Count; i++)
                {
                    for (j = 0, k = 1; j < table.Columns.Count; j++)
                    {
                        m_objSheet.Cells[i + 4, k] = table.Rows[i][j].ToString();
                        k++;
                    }
                }

                m_objRange = m_objSheet.get_Range(m_objSheet.Cells[3, 1], m_objSheet.Cells[table.Rows.Count + 3, table.Columns.Count]);
                m_objSheet.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                m_objRange.Font.Size = 9;
                m_objRange.EntireColumn.AutoFit();
                m_objSheet.Cells[table.Rows.Count + 4, 1] = "Подпис:";
                m_objSheet.Cells[table.Rows.Count + 4, 20] = "Подпис:";
                m_objSheet.Cells[table.Rows.Count + 6, 1] = "Управител:";
                m_objSheet.Cells[table.Rows.Count + 6, 20] = "Супервайзор/Мениджър -";
                m_objSheet.Cells[table.Rows.Count + 7, 20] = "Дата:";   

            }

            m_objExcel.Visible = true;

            ReleaseExcelApplication();
            return true;
        }

            //int z = 1;
        
        public bool Export(List<System.Data.DataTable> listTables, string filePath, string[] sheet )
        {

            
            int i, j, k;

            try
            {
                m_objExcel = new Application();
                if (filePath != null)
                {
                    m_objBook = (Workbook)m_objExcel.Workbooks.Open(filePath, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", true, false, 0, true, 1, 0);
                }
            }
            catch (Exception )
            {
                //TaktWpfControls.ErrorLog.WriteException(exc, "На компютъра няма инсталиран Microsoft Excel.");
                //MessageBox.Show("На компютъра няма инсталиран Microsoft Excel.");
                return false;
            }

          

            //m_objBook = (_Workbook)(m_objBooks.Add(opt));
            m_objSheets = (Sheets)m_objBook.Worksheets;

            //int z = 1;
            //while (listTables.Count > m_objSheets.Count)
            //{
            //    m_objSheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            //}
            int currentSheet = 0;
            foreach (System.Data.DataTable table in listTables)
            {
                m_objSheet = (_Worksheet)(m_objSheets.get_Item(sheet[currentSheet]));
                
                for (i = 0, j = 0; i < table.Columns.Count; i++, j++)
                {
                    m_objSheet.Cells[1, j + 1] = table.Columns[i].ColumnName;

                }
                if (table.Rows.Count == 0) continue;
                for (i = 0; i < table.Rows.Count; i++)
                {
                    for (j = 0, k = 1; j < table.Columns.Count; j++)
                    {

                        m_objSheet.Cells[i + 2, k] = table.Rows[i][j].ToString();
                        k++;
                    }
                }

                m_objRange = m_objSheet.get_Range(m_objSheet.Cells[1, 1], m_objSheet.Cells[table.Rows.Count + 1, table.Columns.Count]);
                m_objRange.EntireColumn.AutoFit();
                
                currentSheet++;
            }

            m_objExcel.Visible = true;

            ReleaseExcelApplication();
            return true;
        }

        public void ReleaseExcelApplication()
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

            m_objRange = null;
            m_objBooks = null;
            m_objBook = null;
            m_objExcel = null;
            m_objSheets = null;
            m_objSheet = null;
            GC.Collect();
        }
    }
}
