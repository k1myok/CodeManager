using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Net;
using System.Collections;

using NPOI.HSSF;
using NPOI.Util;
using NPOI.XSSF.UserModel;
using NPOI.POIFS;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;
using NPOI.HSSF.UserModel;
using System.Configuration;
using System.Web;


namespace YunLianLHelp
{
   public class ExcelHelp
    {
       
        private string fileName = null; //文件名
        private IWorkbook workbook = null;
        private FileStream fs = null;
        private bool disposed;
        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="data">要导入的数据</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <returns>导入数据行数(包含列名那一行)</returns>
        public int DataTableToExcel(DataTable data, string sheetName, bool isColumnWritten)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;

            fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                workbook = new XSSFWorkbook();
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook();

            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                }
                else
                {
                    return -1;
                }

                if (isColumnWritten == true) //写入DataTable的列名
                {
                    IRow row = sheet.CreateRow(0);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                    }
                    count = 1;
                }
                else
                {
                    count = 0;
                }

                for (i = 0; i < data.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                    }
                    ++count;
                }
                workbook.Write(fs); //写入到excel
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns>返回的DataTable</returns>
        public DataTable ExcelToDataTable(string sheetName, bool isFirstRowColumn)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　
                        
                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    if (fs != null)
                        fs.Close();
                }

                fs = null;
                disposed = true;
            }
        }
        public object GetCellValue(ICell cell)
        {
            object value = null;
            try
            {
                if (cell.CellType != CellType.Blank)
                {
                    switch (cell.CellType)
                    {
                        case CellType.Numeric:
                            // Date comes here
                            if (DateUtil.IsCellDateFormatted(cell))
                            {
                                value = cell.DateCellValue;
                            }
                            else
                            {
                                // Numeric type
                                value = cell.NumericCellValue;
                            }
                            break;
                        case CellType.Boolean:
                            // Boolean type
                            value = cell.BooleanCellValue;
                            break;
                        case CellType.Formula:
                            value = cell.CellFormula;
                            break;
                        default:
                            // String type
                            value = cell.StringCellValue;
                            break;
                    }
                }
            }
            catch (Exception)
            {
                value = "";
            }

            return value;
        }
        public static void SetCellValue(ICell cell, object obj)
        {
            if (obj.GetType() == typeof(int))
            {
                cell.SetCellValue((int)obj);
            }
            else if (obj.GetType() == typeof(double))
            {
                cell.SetCellValue((double)obj);
            }
            else if (obj.GetType() == typeof(IRichTextString))
            {
                cell.SetCellValue((IRichTextString)obj);
            }
            else if (obj.GetType() == typeof(string))
            {
                cell.SetCellValue(obj.ToString());
            }
            else if (obj.GetType() == typeof(DateTime))
            {
                cell.SetCellValue((DateTime)obj);
            }
            else if (obj.GetType() == typeof(bool))
            {
                cell.SetCellValue((bool)obj);
            }
            else
            {
                cell.SetCellValue(obj.ToString());
            }
        }


        private static int ExcelMaxRow = Convert.ToInt32(ConfigurationManager.AppSettings["ExcelMaxRow"]);
        /// <summary>  
        /// 由DataSet导出Excel  
        /// </summary>  
        /// <param name="sourceTable">要导出数据的DataTable</param>     
        /// <param name="sheetName">工作表名称</param>  
        /// <returns>Excel工作表</returns>     
        private static Stream ExportDataSetToExcel(DataSet sourceDs)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();

            for (int i = 0; i < sourceDs.Tables.Count; i++)
            {
                HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet(sourceDs.Tables[i].TableName);
                HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
                // handling header.             
                foreach (DataColumn column in sourceDs.Tables[i].Columns)
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                // handling value.             
                int rowIndex = 1;
                foreach (DataRow row in sourceDs.Tables[i].Rows)
                {
                    HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                    foreach (DataColumn column in sourceDs.Tables[i].Columns)
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    }
                    rowIndex++;
                }
            }
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            workbook = null;
            return ms;
        }
        /// <summary>  
        /// 由DataSet导出Excel  
        /// </summary>    
        /// <param name="sourceTable">要导出数据的DataTable</param>  
        /// <param name="fileName">指定Excel工作表名称</param>  
        /// <returns>Excel工作表</returns>     
        public static void ExportDataSetToExcel(DataSet sourceDs, string fileName)
        {
            //检查是否有Table数量超过65325  
            for (int t = 0; t < sourceDs.Tables.Count; t++)
            {
                if (sourceDs.Tables[t].Rows.Count > ExcelMaxRow)
                {
                    DataSet ds = GetdtGroup(sourceDs.Tables[t].Copy());
                    sourceDs.Tables.RemoveAt(t);
                    //将得到的ds插入 sourceDs中  
                    for (int g = 0; g < ds.Tables.Count; g++)
                    {
                        DataTable dt = ds.Tables[g].Copy();
                        sourceDs.Tables.Add(dt);
                    }
                    t--;
                }
            }

            MemoryStream ms = ExportDataSetToExcel(sourceDs) as MemoryStream;
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            //HttpContext.Current.Response.End();  
            ms.Close();
            ms = null;
        }
        /// <summary>  
        /// 由DataTable导出Excel  
        /// </summary>  
        /// <param name="sourceTable">要导出数据的DataTable</param>  
        /// <returns>Excel工作表</returns>     
        private static Stream ExportDataTableToExcel(DataTable sourceTable)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet(sourceTable.TableName);
            HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
            // handling header.       
            foreach (DataColumn column in sourceTable.Columns)
                headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
            // handling value.       
            int rowIndex = 1;
            foreach (DataRow row in sourceTable.Rows)
            {
                HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                foreach (DataColumn column in sourceTable.Columns)
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }
                rowIndex++;
            }
            workbook.Write(ms);
            ms.Flush();
            ms.Position = 0;
            sheet = null;
            headerRow = null;
            workbook = null;
            return ms;
        }
        /// <summary>  
        /// 由DataTable导出Excel  
        /// </summary>  
        /// <param name="sourceTable">要导出数据的DataTable</param>  
        /// <param name="fileName">指定Excel工作表名称</param>  
        /// <returns>Excel工作表</returns>  
        public static void ExportDataTableToExcel(DataTable sourceTable, string fileName)
        {
            //如数据超过65325则分成多个Table导出  
            if (sourceTable.Rows.Count > ExcelMaxRow)
            {
                DataSet ds = GetdtGroup(sourceTable);
                //导出DataSet  
                ExportDataSetToExcel(ds, fileName);
            }
            else
            {
                MemoryStream ms = ExportDataTableToExcel(sourceTable) as MemoryStream;
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
                HttpContext.Current.Response.BinaryWrite(ms.ToArray());
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                //HttpContext.Current.Response.End();  
                ms.Close();
                ms = null;
            }
        }

        /// <summary>  
        /// 传入行数超过65325的Table，返回DataSet  
        /// </summary>  
        /// <param name="dt"></param>  
        /// <returns></returns>  
        public static DataSet GetdtGroup(DataTable dt)
        {
            string tablename = dt.TableName;

            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            double n = dt.Rows.Count / Convert.ToDouble(ExcelMaxRow);

            //创建表  
            for (int i = 1; i < n; i++)
            {
                DataTable dtAdd = dt.Clone();
                dtAdd.TableName = tablename + "_" + i.ToString();
                ds.Tables.Add(dtAdd);
            }

            //分解数据  
            for (int i = 1; i < ds.Tables.Count; i++)
            {
                //新表行数达到最大 或 基表数量不足  
                while (ds.Tables[i].Rows.Count != ExcelMaxRow && ds.Tables[0].Rows.Count != ExcelMaxRow)
                {
                    ds.Tables[i].Rows.Add(ds.Tables[0].Rows[ExcelMaxRow].ItemArray);
                    ds.Tables[0].Rows.RemoveAt(ExcelMaxRow);

                }
            }

            return ds;
        }

        /// <summary>  
        /// 由DataTable导出Excel  
        /// </summary>  
        /// <param name="sourceTable">要导出数据的DataTable</param>  
        /// <param name="fileName">指定Excel工作表名称</param>  
        /// <returns>Excel工作表</returns>  
        public static void ExportDataTableToExcelModel(DataTable sourceTable, string modelpath, string modelName, string fileName, string sheetName)
        {
            int rowIndex = 2;//从第二行开始，因为前两行是模板里面的内容  
            int colIndex = 0;
            FileStream file = new FileStream(modelpath + modelName + ".xls", FileMode.Open, FileAccess.Read);//读入excel模板  
            HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
            HSSFSheet sheet1 = (HSSFSheet)hssfworkbook.GetSheet("Sheet1");
            sheet1.GetRow(0).GetCell(0).SetCellValue("excelTitle");      //设置表头  
            foreach (DataRow row in sourceTable.Rows)
            {   //双循环写入sourceTable中的数据  
                rowIndex++;
                colIndex = 0;
                HSSFRow xlsrow = (HSSFRow)sheet1.CreateRow(rowIndex);
                foreach (DataColumn col in sourceTable.Columns)
                {
                    xlsrow.CreateCell(colIndex).SetCellValue(row[col.ColumnName].ToString());
                    colIndex++;
                }
            }
            sheet1.ForceFormulaRecalculation = true;
            FileStream fileS = new FileStream(modelpath + fileName + ".xls", FileMode.Create);//保存  
            hssfworkbook.Write(fileS);
            fileS.Close();
            file.Close();
        }  

    }
}
