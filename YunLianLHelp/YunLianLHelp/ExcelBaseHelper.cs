﻿// 源文件头信息：
// <copyright file="ExcelBaseHelper.cs">
// Copyright(c)2014-2034 Kencery.All rights reserved.
// 个人博客：http://www.cnblogs.com/hanyinglong
// 创建人：韩迎龙(kencery)
// 创建时间：2014/12/24
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace YunLianLHelp
{
    /// <summary>
    /// 提供给Excel调用的基础帮助类，实现一些Excel的基础功能
    /// <auther>
    ///     <name>Kencery</name>
    ///     <date>2014/12/24</date>
    /// </auther>
    /// 修改记录：时间  内容  姓名
    /// 
    /// </summary>
    public class ExcelBaseHelper
    {
        /// <summary>
        /// 工作簿的定义
        /// </summary>
        private readonly IWorkbook _iWorkbook;

        /// <summary>
        /// 文件流初始化对象
        /// </summary>
        /// <param name="stream">传入流对象</param>
        public ExcelBaseHelper(Stream stream)
        {
            _iWorkbook = CreateWorkBook(stream);
        }

        /// <summary>
        /// 传入文件名称读取Excel文件中的信息
        /// </summary>
        /// <param name="fileName">传递的文件名称(包含路径)</param>
        public ExcelBaseHelper(string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                _iWorkbook = CreateWorkBook(fileStream);
            }
        }

        /// <summary>
        /// 使用try-Catch创建一个工作薄对象
        /// </summary>
        /// <param name="stream">传递过来创建的工作薄对象(流文件)</param>
        /// <returns></returns>
        private IWorkbook CreateWorkBook(Stream stream)
        {
            try
            {
                return new XSSFWorkbook(stream);
            }
            catch
            {
                return new HSSFWorkbook(stream);
            }
        }

        /// <summary>
        /// 读取Excel中默认第一张sheet的集合
        /// </summary>
        /// <typeparam name="T">需要读取的Excel实体集合和Excel中的字段对应</typeparam>
        /// <param name="fields">excel中各个列，以此要转换成为对象的字段</param>
        /// <returns>返回List集合对象</returns>
        public IList<T> ExcelToList<T>(List<string> fields) where T : class, new()
        {
            return ExportToList<T>(_iWorkbook.GetSheetAt(0), fields);
        }

        /// <summary>
        /// 将Excel中第一个Sheet中的数据信息转换成List集合
        /// </summary>
        /// <typeparam name="T">需要读取的Excel实体集合和Excel中的字段对应</typeparam>
        /// <param name="iSheet">表示Excel中第一个Sheet</param>
        /// <param name="fields">excel中各个列，以此要转换成为对象的字段</param>
        /// <returns></returns>
        private IList<T> ExportToList<T>(ISheet iSheet, List<string> fields) where T : class, new()
        {
            IList<T> list = new List<T>();
            //循环遍历每一行的数据
            for (int i = iSheet.FirstRowNum + 1, len = iSheet.LastRowNum + 1; i < len; i++)
            {
                T t = new T();
                IRow row = iSheet.GetRow(i);
                //循环遍历列数据
                for (int j = 0, len2 = fields.Count; j < len2; j++)
                {
                    ICell cell = row.GetCell(j);
                    object cellValue;
                    switch (cell.CellType)
                    {
                        case CellType.String: //文本
                            cellValue = cell.StringCellValue;
                            break;
                        case CellType.Numeric: //数值
                            cellValue = Convert.ToString(cell.NumericCellValue);
                            break;
                        case CellType.Boolean: //bool类型
                            cellValue = cell.BooleanCellValue;
                            break;
                        case CellType.Blank: //空值
                            cellValue = "";
                            break;
                        default:
                            cellValue = "";
                            break;
                    }
                    typeof (T).GetProperty(fields[j]).SetValue(t, cellValue, null);
                }
                list.Add(t);
            }
            return list;
        }
    }
}