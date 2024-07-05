using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using YMGS.Framework;
using NPOI.HSSF.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;


namespace YMGS.Trade.Web.Common
{
    public class ExcelHelper
    {
        /// <summary>
        /// Excel导出
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sheetName"></param>
        public static void ExportDataToExcel(DataTable dt, string sheetName, string[] columnNames,string fileName)
        {
            if (dt == null)
                return;
            if (dt.Rows.Count == 0)
                return;
            if (columnNames.Length != dt.Columns.Count)
                return;

            try
            {
                IWorkbook workbook = new HSSFWorkbook();
                ISheet sheet = workbook.CreateSheet(sheetName);
                IRow row = null;
                ICell cell = null;

                var exportFileName = fileName + UtilityHelper.DateToStr(DateTime.Now) + Guid.NewGuid().ToString().Substring(0, 4) + ".xls";
                var colmunCount = dt.Columns.Count;
                var rowCount = dt.Rows.Count + 1;

                for (int i = 0; i < rowCount; i++)
                {
                    row = sheet.CreateRow(i);
                    for (int j = 0; j < colmunCount; j++)
                    {
                        cell = row.CreateCell(j);
                        //构造表头
                        if (i == 0)
                            cell.SetCellValue(columnNames[j]);
                        cell.SetCellValue(dt.Rows[i][j].ToString());
                    }
                }
                MemoryStream ms = new MemoryStream();
                workbook.Write(ms);
                HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + exportFileName + ""));
                HttpContext.Current.Response.BinaryWrite(ms.ToArray());
                workbook = null;
                ms.Close();
                ms.Dispose();
            }
            catch
            {
            }
        }
    }
}