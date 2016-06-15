// 源文件头信息：
// <copyright file="EntityConverter.cs">
// Copyright(c)2014-2034 Kencery.All rights reserved.
// 个人博客：http://www.cnblogs.com/hanyinglong
// 创建人：韩迎龙(kencery)
// 创建时间：2016-4-18
// </copyright>

using System.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace KenceryCommonMethod
{
    /// <summary>
    /// 实体转换类
    /// <auther>
    ///     <name>哈尔滨-奶瓶提供的共用类、韩迎龙做测试</name>
    ///     <date>2016-4-18</date>
    /// </auther>
    /// </summary>
    /// <typeparam name="T">泛型T</typeparam>
    public class EntityConverter<T> where T : new()
    {
        /// <summary>
        /// 实体类转DataTable
        /// </summary>
        /// <returns>返回 DataTable</returns>
        public static DataTable CreateDataTableByEntity()
        {
            Type entityType = typeof(T);
            PropertyDescriptorCollection descriptors = TypeDescriptor.GetProperties(entityType);
            DataTable result = new DataTable();
            Type type = null;
            foreach (PropertyDescriptor descriptor in descriptors)
            {
                type = (descriptor.PropertyType.IsGenericType == true && descriptor.PropertyType.Name.Equals(typeof(Nullable<>).Name)) ? Nullable.GetUnderlyingType(descriptor.PropertyType) : descriptor.PropertyType;
                result.Columns.Add(descriptor.Name, type);
            }
            return result;
        }

        /// <summary>
        /// List转DataTable 对象
        /// </summary>
        /// <param name="list">List集合对象</param>
        /// <returns>返回DataTable 对象</returns>
        public static DataTable CreateDataTableByAnyListEntity(List<T> list)
        {
            DataTable result = null;
            if (list != null && list.Count > 0)
            {
                result = CreateDataTableByEntity();
                Type entityType = typeof(T);
                PropertyDescriptorCollection descriptors = TypeDescriptor.GetProperties(entityType);
                foreach (T entity in list)
                {
                    DataRow row = result.NewRow();
                    foreach (PropertyDescriptor descriptor in descriptors)
                    {
                        try
                        {
                            GetValue(descriptor, entity, ref row);
                        }
                        catch (Exception e)
                        {
                            continue;
                        }
                    }
                    result.Rows.Add(row);
                }
            }
            return result;
        }

        /// <summary>
        /// DataRow 转实体类
        /// </summary>
        /// <param name="row">数据行</param>
        /// <returns>返回实体类</returns>
        public static T CreateEntityByDataRow(DataRow row)
        {
            T entity = new T();
            PropertyDescriptorCollection descriptors = TypeDescriptor.GetProperties(entity);
            DataColumnCollection columns = row.Table.Columns;
            foreach (PropertyDescriptor descriptor in descriptors)
            {
                string columnName = descriptor.Name;
                if (columns.Contains(columnName))
                {
                    object value = row[columnName];
                    if (descriptor.IsReadOnly || !descriptor.IsBrowsable || value is DBNull || value == DBNull.Value)
                        continue;
                    try
                    {
                        SetValue(descriptor, value, ref entity);
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }

            }
            return entity;
        }

        /// <summary>
        /// DataTable 转 List
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <returns>返回 List</returns>
        public static List<T> CreateAnyListEntityByDataTable(DataTable table)
        {
            List<T> list = null;
            if (table != null && table.Rows.Count > 0)
            {
                list = new List<T>();
                T entity = default(T);
                foreach (DataRow row in table.Rows)
                {
                    entity = CreateEntityByDataRow(row);
                    list.Add(entity);
                }
            }
            return list;
        }

        /// <summary>
        /// 取值
        /// </summary>
        /// <param name="descriptor">属性解释器</param>
        /// <param name="entity">实体类</param>
        /// <param name="row">数据行</param>
        private static void GetValue(PropertyDescriptor descriptor, T entity, ref DataRow row)
        {

            TypeCode code = TypeCode.Object;
            if (Enum.TryParse<TypeCode>(descriptor.PropertyType.Name, out code))
            {
                switch (code)
                {
                    case TypeCode.Boolean:
                        row[descriptor.Name] = Convert.ToBoolean(descriptor.GetValue(entity), CultureInfo.CurrentCulture);
                        break;
                    case TypeCode.Byte:
                        row[descriptor.Name] = Convert.ToByte(descriptor.GetValue(entity), CultureInfo.CurrentCulture);
                        break;
                    case TypeCode.Char:
                        row[descriptor.Name] = Convert.ToChar(descriptor.GetValue(entity), CultureInfo.CurrentCulture);
                        break;
                    case TypeCode.DBNull:
                        row[descriptor.Name] = System.DBNull.Value;
                        break;
                    case TypeCode.DateTime:
                        row[descriptor.Name] = Convert.ToDateTime(descriptor.GetValue(entity), CultureInfo.CurrentCulture);
                        break;
                    case TypeCode.Decimal:
                        row[descriptor.Name] = Convert.ToDecimal(descriptor.GetValue(entity), CultureInfo.CurrentCulture);
                        break;
                    case TypeCode.Double:
                        row[descriptor.Name] = Convert.ToDouble(descriptor.GetValue(entity), CultureInfo.CurrentCulture);
                        break;
                    case TypeCode.Empty:
                        row[descriptor.Name] = string.Empty;
                        break;
                    case TypeCode.Int16:
                        row[descriptor.Name] = Convert.ToInt16(descriptor.GetValue(entity), CultureInfo.CurrentCulture);
                        break;
                    case TypeCode.Int32:
                        row[descriptor.Name] = Convert.ToInt32(descriptor.GetValue(entity), CultureInfo.CurrentCulture);
                        break;
                    case TypeCode.Int64:
                        row[descriptor.Name] = Convert.ToInt64(descriptor.GetValue(entity), CultureInfo.CurrentCulture);
                        break;
                    case TypeCode.Object:
                        row[descriptor.Name] = descriptor.GetValue(entity);
                        break;
                    case TypeCode.SByte:
                        row[descriptor.Name] = Convert.ToSByte(descriptor.GetValue(entity), CultureInfo.CurrentCulture);
                        break;
                    case TypeCode.Single:
                        row[descriptor.Name] = Convert.ToSingle(descriptor.GetValue(entity), CultureInfo.CurrentCulture);
                        break;
                    case TypeCode.String:
                        row[descriptor.Name] = Convert.ToString(descriptor.GetValue(entity), CultureInfo.CurrentCulture);
                        break;
                    case TypeCode.UInt16:
                        row[descriptor.Name] = Convert.ToUInt16(descriptor.GetValue(entity), CultureInfo.CurrentCulture);
                        break;
                    case TypeCode.UInt32:
                        row[descriptor.Name] = Convert.ToUInt32(descriptor.GetValue(entity), CultureInfo.CurrentCulture);
                        break;
                    case TypeCode.UInt64:
                        row[descriptor.Name] = Convert.ToUInt64(descriptor.GetValue(entity), CultureInfo.CurrentCulture);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                row[descriptor.Name] = (descriptor.GetValue(entity).GetType().IsGenericType && descriptor.GetValue(entity).GetType().Name.Equals(typeof(Nullable<>))) ?
                    Convert.ChangeType(descriptor.GetValue(entity), Nullable.GetUnderlyingType(descriptor.GetValue(entity).GetType())) :
                    Convert.ChangeType(descriptor.GetValue(entity), descriptor.GetValue(entity).GetType());
            }
        }

        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="descriptor">属性解释器</param>
        /// <param name="value">值</param>
        /// <param name="entity">实体类</param>
        private static void SetValue(PropertyDescriptor descriptor, object value, ref T entity)
        {
            TypeCode code = TypeCode.Object;
            if (Enum.TryParse<TypeCode>(descriptor.PropertyType.Name, out code))
            {
                switch (code)
                {
                    case TypeCode.Boolean:
                        descriptor.SetValue(entity, Convert.ToBoolean(value, CultureInfo.CurrentCulture));
                        break;
                    case TypeCode.Byte:
                        descriptor.SetValue(entity, Convert.ToByte(value, CultureInfo.CurrentCulture));
                        break;
                    case TypeCode.Char:
                        descriptor.SetValue(entity, Convert.ToChar(value, CultureInfo.CurrentCulture));
                        break;
                    case TypeCode.DBNull:
                        descriptor.SetValue(entity, System.DBNull.Value);
                        break;
                    case TypeCode.DateTime:
                        descriptor.SetValue(entity, Convert.ToDateTime(value, CultureInfo.CurrentCulture));
                        break;
                    case TypeCode.Decimal:
                        descriptor.SetValue(entity, Convert.ToDecimal(value, CultureInfo.CurrentCulture));
                        break;
                    case TypeCode.Double:
                        descriptor.SetValue(entity, Convert.ToDouble(value, CultureInfo.CurrentCulture));
                        break;
                    case TypeCode.Empty:
                        descriptor.SetValue(entity, string.Empty);
                        break;
                    case TypeCode.Int16:
                        descriptor.SetValue(entity, Convert.ToInt16(value, CultureInfo.CurrentCulture));
                        break;
                    case TypeCode.Int32:
                        descriptor.SetValue(entity, Convert.ToInt32(value, CultureInfo.CurrentCulture));
                        break;
                    case TypeCode.Int64:
                        descriptor.SetValue(entity, Convert.ToInt64(value, CultureInfo.CurrentCulture));
                        break;
                    case TypeCode.Object:
                        descriptor.SetValue(entity, value);
                        break;
                    case TypeCode.SByte:
                        descriptor.SetValue(entity, Convert.ToSByte(value, CultureInfo.CurrentCulture));
                        break;
                    case TypeCode.Single:
                        descriptor.SetValue(entity, Convert.ToSingle(value, CultureInfo.CurrentCulture));
                        break;
                    case TypeCode.String:
                        descriptor.SetValue(entity, Convert.ToString(value, CultureInfo.CurrentCulture));
                        break;
                    case TypeCode.UInt16:
                        descriptor.SetValue(entity, Convert.ToUInt16(value, CultureInfo.CurrentCulture));
                        break;
                    case TypeCode.UInt32:
                        descriptor.SetValue(entity, Convert.ToUInt32(value, CultureInfo.CurrentCulture));
                        break;
                    case TypeCode.UInt64:
                        descriptor.SetValue(entity, Convert.ToUInt64(value, CultureInfo.CurrentCulture));
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Type type = (descriptor.PropertyType.IsGenericType == true && descriptor.PropertyType.Name.Equals(typeof(Nullable<>).Name)) ? Nullable.GetUnderlyingType(descriptor.PropertyType) : descriptor.PropertyType;
                descriptor.SetValue(entity, Convert.ChangeType(value, type, CultureInfo.CurrentCulture));
            }
        }
    }
}