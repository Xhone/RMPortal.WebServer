using System.Data;

namespace RMPortal.WebServer.Util
{
    public class ExcelUtil
    {


        /// <summary>
        /// 将数据集中的数据导出到EXCEL文件
        /// </summary>
        /// <param name="dataSet">输入数据集</param>
        /// <param name="isShowExcle">是否显示该EXCEL文件</param>
        /// <returns></returns>
        public bool DataSetToExcel(DataTable dataTable, bool isShowExcle, string name)
        {
            //DataTable dataTable = dataSet.Tables[0];
            int rowNumber = dataTable.Rows.Count;//不包括字段名
            int columnNumber = dataTable.Columns.Count;
            int colIndex = 0;

            if (rowNumber == 0)
            {
                return false;
            }

            //建立Excel对象
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            //excel.Application.Workbooks.Add(true);
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
            excel.Visible = isShowExcle;
            //Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.Worksheets[1];
            Microsoft.Office.Interop.Excel.Range range;

            //生成字段名称
            foreach (DataColumn col in dataTable.Columns)
            {
                colIndex++;
                excel.Cells[1, colIndex] = col.ColumnName;
            }

            object[,] objData = new object[rowNumber, columnNumber];

            for (int r = 0; r < rowNumber; r++)
            {
                for (int c = 0; c < columnNumber; c++)
                {
                    objData[r, c] = dataTable.Rows[r][c];
                }
                //Application.DoEvents();
            }

            // 写入Excel
            range = worksheet.get_Range(excel.Cells[2, 1], excel.Cells[rowNumber + 1, columnNumber]);
            //range.NumberFormat = "@";//设置单元格为文本格式
            range.Value2 = objData;
            //worksheet.get_Range(excel.Cells[2, 1], excel.Cells[rowNumber + 1, 1]).NumberFormat = "yyyy-m-d h:mm";
            string changePathName = "PackListExcel" + DateTime.Now.ToString("yyyyMMddhhmm");

            string lastPathName = @"D:\Excel\" + changePathName + @"\";
            if (!Directory.Exists(lastPathName))
            {
                Directory.CreateDirectory(lastPathName);
            }

            string lastName = lastPathName + name;
            workbook.SaveAs(lastName);
            SetDirAttrNormal(lastName);
            SetDirAttrNormal(lastName);
            KillExcel(excel, workbook);
            //workbook.Save();
            return true;
        }
        /// <summary>
        /// 退出excel
        /// </summary>
        private void KillExcel(Microsoft.Office.Interop.Excel.Application app, Microsoft.Office.Interop.Excel.Workbook book)
        {
            if (book != null)
                book = null;  // WorkBook 的实例欢畅
            if (app != null)
                //app.Quit(); // Microsoft.Office.Interop.Excel  的实例对象 
                GC.Collect();  // 回收资源
            System.Diagnostics.Process[] excelProcess = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            foreach (var item in excelProcess)
            {
                item.Kill();
            }
        }
        /// <summary>
        /// 去除文件夹只读属性
        /// </summary>
        /// <param name="strPath">文件夹物理路径</param>
        public static bool SetDirAttrNormal(string strPath)
        {
            bool success = true;
            try
            {
                string[] strFiles = Directory.GetFiles(strPath);
                string[] strDirs = Directory.GetDirectories(strPath);

                int fileCount = strFiles.Length;
                for (int i = 0; i < fileCount; i++)
                {
                    File.SetAttributes(strFiles[i], FileAttributes.Normal);
                }

                int dirCount = strDirs.Length;
                if (dirCount > 0)
                {
                    for (int i = 0; i < dirCount; i++)
                    {
                        SetDirAttrNormal(strDirs[i]);
                    }
                }
            }
            catch { success = false; }

            return success;
        }
    }
}
