using System;
using Microsoft.Office.Interop.Excel;

namespace ExcelExport
{

    public class ExcelEdit : ExcelExport
    {
        
        public ExcelEdit( )
        {
        	
        }
        public bool OpenExcel(string filepath)
        {
            
            try
            {
                
                m_objExcel = new Application();
                m_objBook = m_objExcel.Workbooks.Open(filepath,
                    0, false, 5, "", "", false, XlPlatform.xlWindows, "",
                    true, false, 0, true, false, false);
                
                //m_objBook = (_Workbook)(m_objBooks.Add(opt));
                m_objSheets = (Sheets)m_objBook.Worksheets;
            }
            catch( Exception )
            {
               // TaktWpfControls.ErrorLog.WriteException(exc, "OpenExcel  filepath " + filepath);
                return false;
            }
            
            return true;
        }
        public void PrintSheet( int sheet)
        {
            m_objSheet = (_Worksheet)(m_objSheets.get_Item(sheet));
            m_objSheet.PrintPreview(false);
        }
        public void SetVisibleExcel()
        {
            m_objExcel.Visible = true;
        }
        public object GetCell(int sheet, int column, int row)
        {
            object res = null;
            if (m_objExcel == null)
                return res;
            if (m_objSheets == null)
                return res;
            if (m_objSheets.Count < sheet)
                return res;
            if (sheet == 0)
                return res;
            try
            {
                m_objSheet = (_Worksheet)(m_objSheets.get_Item(sheet));
                res = ((Range)m_objSheet.Cells.get_Item(row, column)).Value2;
            }
            catch (Exception )
            {
                //TaktWpfControls.ErrorLog.WriteException(exc, "GetCell  sheet " + sheet + "  column " + column + "  row " + row);
                return res;
            }

            return res;
        }
        public void CloseExcel()
        {
            
            m_objExcel.Quit();

            if (m_objBook != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_objBook);
            if (m_objSheets != null)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_objSheets);
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
        public System.Data.DataTable Import(string sheet, int startCol, int startRow, int endCol, int endRow)
        {
            int i, j, k;
            System.Data.DataTable table = null;

            try
            {
                table = new System.Data.DataTable();
                m_objSheet = (_Worksheet)(m_objSheets.get_Item(sheet));
                for (i = 0; i < endCol - startCol; i++)
                {
                    System.Data.DataColumn col = new System.Data.DataColumn(i.ToString(), typeof(string));
                    table.Columns.Add(col);
                }

                for (j = startRow, i = 0; j < endRow; j++, i++)
                {
                    System.Data.DataRow row = table.NewRow();
                    for (k = startCol; k < endCol; k++)
                    {
                        row[k-startCol] = ((Range)m_objSheet.Cells.get_Item(j, k)).Value2.ToString();
                        //row[i] = ((Range)m_objSheet.Cells.get_Item(j, k)).Value2;
                        //row[k] = ((Range)m_objSheet.Cells[j, k]).Value2.ToString();
                    }
                    table.Rows.Add(row);
                }
            }

            catch (Exception )
            {
                //TaktWpfControls.ErrorLog.WriteException(ex, "Import  sheet " + sheet + "  startCol " + startCol + "  startRow " + startRow + "  endRow " + endRow);
            }
            //m_objExcel.Visible = true;

            
            //m_objBook = (_Workbook)(m_objBooks.Add(opt));
             

            return table;
        }

        public bool SetValues(int sheet, int rowStart, int row_length, int colStart, int col_length, object[,] values)
        {
            //bool res = false;
            if (m_objExcel == null)
                return false;
            if (m_objSheets == null)
                return false;
            if (m_objSheets.Count < sheet)
                return false;
            if (sheet == 0)
                return false;
            try
            {
                m_objSheet = (_Worksheet)(m_objSheets.get_Item(sheet));          
                Range c1 = (Range)m_objSheet.Cells[rowStart, colStart];
                Range c2 = (Range)m_objSheet.Cells[rowStart + row_length, colStart + col_length-1];
                Range range = m_objSheet.get_Range(c1, c2);

                range.Value2 = values;

            }
            catch( Exception  )
            {
                //TaktWpfControls.ErrorLog.WriteException(exc, "Import  sheet " + sheet + "  rowStart " + rowStart + "  row_length " + row_length +
                  //  "  colStart " + colStart + "  col_length " + col_length);
                return false;
            }
            
            return true;
        }
        public bool SetCell( int sheet, int column, int row, object val )
        {
            //bool res = false;
            if (m_objExcel == null)
                return false;
            if (m_objSheets == null)
                return false;
            if (m_objSheets.Count < sheet)
                return false;
            if (sheet == 0)
                return false;
            try
            {
                m_objSheet = (_Worksheet)(m_objSheets.get_Item(sheet));
                m_objSheet.Cells[row, column] = val;
            }
            catch(Exception )
            {
                //TaktWpfControls.ErrorLog.WriteException(exc, "Import  sheet " + sheet + "  column " + column + "  row " + row + "  val " + val.ToString());
                return false;
            }
            
            return true;

        }
    }
}