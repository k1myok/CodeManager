/******************************************************************************
 *  作者：       tianzh
 *  创建时间：   2012/6/6 22:22:20
 *
 *
 ******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Web.Script.Serialization;
namespace TZHSWEET.Common
{
    /// <summary>
    /// 过期版本,以后不使用
    /// </summary>
    public class FormatToJson
    {
        public FormatToJson()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region ListToJson
        /// <summary>  
        /// List转成json   
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="jsonName"></param>  
        /// <param name="list"></param>  
        /// <returns></returns>  
        public static string ListToJson<T>(IList<T> list, string jsonName)
        {
            StringBuilder Json = new StringBuilder();
            if (string.IsNullOrEmpty(jsonName))
                jsonName = list[0].GetType().Name;
            Json.Append("{\"" + jsonName + "\":[");
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    T obj = Activator.CreateInstance<T>();
                    PropertyInfo[] pi = obj.GetType().GetProperties();
                    Json.Append("{");
                    for (int j = 0; j < pi.Length; j++)
                    {
                        //注意这里要做非空判断,否则空值会发生异常
                        if (TZHSWEET.Common.Tools.IsNullOrEmpty(pi[j].GetValue(list[i], null)))
                        {

                            Json.Append("\"" + pi[j].Name.ToString() + "\":" + "\"\"");
                        }
                        else
                        {
                            Type type = pi[j].GetValue(list[i], null).GetType();
                            Json.Append("\"" + pi[j].Name.ToString() + "\":" + StringFormat(pi[j].GetValue(list[i], null).ToString(), type));
                        }
                        if (j < pi.Length - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < list.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }
        /// <summary>  
        /// List转成json (不生成jsonName版本)  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="list"></param>  
        /// <returns></returns>  
        public static string ListToJsonNoName<T>(IList<T> list)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("[");
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    T obj = Activator.CreateInstance<T>();
                    PropertyInfo[] pi = obj.GetType().GetProperties();
                    Json.Append("{");
                    for (int j = 0; j < pi.Length; j++)
                    {
                        //注意这里要做非空判断,否则空值会发生异常
                        if (TZHSWEET.Common.Tools.IsNullOrEmpty(pi[j].GetValue(list[i], null)))
                        {

                            Json.Append("\"" + pi[j].Name.ToString() + "\":" + "\"\"");
                        }
                        else
                        {
                            Type type = pi[j].GetValue(list[i], null).GetType();
                            Json.Append("\"" + pi[j].Name.ToString() + "\":" + StringFormat(pi[j].GetValue(list[i], null).ToString(), type));
                        }
                        if (j < pi.Length - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < list.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]");
            return Json.ToString();

        }

        /// <summary>  
        /// List转成json   
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="list"></param>  
        /// <returns></returns>  
        public static string ListToJson<T>(IList<T> list)
        {
            object obj = list[0];
            return ListToJson<T>(list, obj.GetType().Name);
        } 
        #endregion
        /// <summary>
        /// 序列化object类型
        /// </summary>
        /// <param name="obj">object类型</param>
        /// <returns></returns>
        public static string ScriptSerializationToJson(object obj)
        {
            return new JavaScriptSerializer().Serialize(obj);

        }
        /// <summary>
        /// 解析json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static object DeserializeToJson(string input)
        {
            return new JavaScriptSerializer().DeserializeObject(input);
        }
        /// <summary>   
        /// 对象转换为Json字符串   
        /// </summary>   
        /// <param name="jsonObject">对象</param>   
        /// <returns>Json字符串</returns>   
        public static string ToJson(object jsonObject)
        {
            string jsonString = "{";
            PropertyInfo[] propertyInfo = jsonObject.GetType().GetProperties();
            for (int i = 0; i < propertyInfo.Length; i++)
            {
                object objectValue = propertyInfo[i].GetGetMethod().Invoke(jsonObject, null);
                string value = string.Empty;
                if (objectValue is DateTime || objectValue is Guid || objectValue is TimeSpan)
                {
                    value = "'" + objectValue.ToString() + "'";
                }
                else if (objectValue is string)
                {
                    value = "'" + ToJson(objectValue.ToString()) + "'";
                }
                else if (objectValue is IEnumerable)
                {
                    value = ToJson((IEnumerable)objectValue);
                }
                else
                {
                    value = ToJson(objectValue.ToString());
                }
                jsonString += "\"" + ToJson(propertyInfo[i].Name) + "\":" + value + ",";
            }
            jsonString.Remove(jsonString.Length - 1, jsonString.Length);
            return jsonString + "}";
        }

        /// <summary>   
        /// 对象集合转换Json   
        /// </summary>   
        /// <param name="array">集合对象</param>   
        /// <returns>Json字符串</returns>   
        public static string ToJson(IEnumerable array)
        {
            string jsonString = "[";
            foreach (object item in array)
            {
                jsonString += ToJson(item) + ",";
            }
            jsonString.Remove(jsonString.Length - 1, jsonString.Length);
            return jsonString + "]";
        }

        /// <summary>   
        /// 普通集合转换Json   
        /// </summary>   
        /// <param name="array">集合对象</param>   
        /// <returns>Json字符串</returns>   
        public static string ToArrayString(IEnumerable array)
        {
            string jsonString = "[";
            foreach (object item in array)
            {
                jsonString = ToJson(item.ToString()) + ",";
            }
            jsonString.Remove(jsonString.Length - 1, jsonString.Length);
            return jsonString + "]";
        }



        /// <summary>   
        /// Datatable转换为Json   
        /// </summary>   
        /// <param name="table">Datatable对象</param>   
        /// <returns>Json字符串</returns>   
        public static string ToJson(DataTable dt)
        {
            StringBuilder jsonString = new StringBuilder();
            jsonString.Append("[");
            DataRowCollection drc = dt.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strKey = dt.Columns[j].ColumnName;
                    string strValue = drc[i][j].ToString();
                    Type type = dt.Columns[j].DataType;
                    jsonString.Append("\"" + strKey + "\":");
                    strValue = StringFormat(strValue, type);
                    if (j < dt.Columns.Count - 1)
                    {
                        jsonString.Append(strValue + ",");
                    }
                    else
                    {
                        jsonString.Append(strValue);
                    }
                }
                jsonString.Append("},");
            }
            jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]");
            return jsonString.ToString();
        }
        #region 为MINIUI生成的json数据转换
        /// <summary>
        /// 为miniui生成json格式(记住,传入数据,包括总行数和数据项的json
        /// </summary>
        /// <param name="MiniUi">数据项的json,比如[{"id":1,"name":"tianzh"},{"id":2,"name":"3"}]</param>
        /// <param name="total">记录总行数</param>
        /// <returns></returns>
        public static string MiniUiToJsonFormat(string MiniUi, int total)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"total\":" + total + ",\"data\":" + MiniUi + "}");
            return sb.ToString();

        }
        /// <summary>
        /// 专门为muniUi解析树形的json 
        /// 大致格式:[{id:"root",text:"总根目录",expanded:false,children:[{id:"xxx",text:"xx"},{id:"xxx",text:"xx"}]}]
        /// </summary>
        /// <param name="strMiniUi">传入参数为[{id:"xxx",text:"xx"},{id:"xxx",text:"xx"}]</param>
        /// <param name="rootName">根标题</param>
        /// <returns></returns>
        public static string MiniUiToJsonForTree(string strMiniUi, string rootName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[{\"id\":\"-1\",\"text\":\"" + rootName + "\",\"children\":" + strMiniUi + "}]");
            return sb.ToString();
        }
        /// <summary>
        /// 专门生成为MiniUi生成json数据(List->json)
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="list">实现了Ilist接口的list</param>
        /// <param name="total">记录总数</param>
        /// <param name="paramMaxMinAvg">这里放排序的参数例如,string para="\"maxAge\":37,\"avgAge\":27,\"minAge\":24"</param>
        /// <returns></returns>
        public static string MiniUiListToJson<T>(IList<T> list, int total, string paramMaxMinAvg)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("{\"total\":" + total + ",");
            if (!string.IsNullOrEmpty(paramMaxMinAvg))
                Json.Append(paramMaxMinAvg);
            Json.Append("\"data\":[");
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    T obj = Activator.CreateInstance<T>();
                    PropertyInfo[] pi = obj.GetType().GetProperties();
                    Json.Append("{");
                    for (int j = 0; j < pi.Length; j++)
                    {
                        //注意这里要做非空判断,否则空值会发生异常
                        if (TZHSWEET.Common.Tools.IsNullOrEmpty(pi[j].GetValue(list[i], null)))
                        {

                            Json.Append("\"" + pi[j].Name.ToString() + "\":" + "\"\"");
                        }
                        else
                        {
                            Type type = pi[j].GetValue(list[i], null).GetType();
                            Json.Append("\"" + pi[j].Name.ToString() + "\":" + StringFormat(pi[j].GetValue(list[i], null).ToString(), type));
                        }

                        if (j < pi.Length - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < list.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();

        } 
        #endregion
        #region 为easyUI生成的json数据
        /// <summary>
        /// 专门生成为EasyUI生成json数据(List->json)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static string EasyUIListToJson<T>(IList<T> list, int total)
        {
            StringBuilder Json = new StringBuilder();
            Json.AppendLine("{\"total\":" + total + ",");
            Json.Append("\"rows\":[");
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    T obj = Activator.CreateInstance<T>();
                    PropertyInfo[] pi = obj.GetType().GetProperties();
                    Json.Append("{");
                    for (int j = 0; j < pi.Length; j++)
                    {
                        Type type = pi[j].GetValue(list[i], null).GetType();
                        Json.Append("\"" + pi[j].Name.ToString() + "\":" + StringFormat(pi[j].GetValue(list[i], null).ToString(), type));

                        if (j < pi.Length - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < list.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();

        }
        /// <summary>
        /// 专门生成为DataGrid的json数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="jsonName"></param>
        /// <returns></returns>
        public static string EasyUIDataGridToJson(DataTable dt, string jsonName, int total)
        {
            StringBuilder Json = new StringBuilder();
            if (string.IsNullOrEmpty(jsonName))
                jsonName = dt.TableName;
            Json.Append("{\"total\":" + total + ",\"" + jsonName + "\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Type type = dt.Rows[i][j].GetType();
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + StringFormat(dt.Rows[i][j].ToString(), type));

                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }

                    }
                    //追加编辑
                    Json.Append(",\"opt\":\"编辑\"");



                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }
        #endregion
        /// <summary>  
        /// DataTable转成Json   
        /// </summary>  
        /// <param name="jsonName"></param>  
        /// <param name="dt"></param>  
        /// <returns></returns>  
        public static string ToJson(DataTable dt, string jsonName)
        {
            StringBuilder Json = new StringBuilder();
            if (string.IsNullOrEmpty(jsonName))
                jsonName = dt.TableName;
            Json.Append("{\"" + jsonName + "\":[");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Json.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        Type type = dt.Rows[i][j].GetType();
                        Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + StringFormat(dt.Rows[i][j].ToString(), type));
                        if (j < dt.Columns.Count - 1)
                        {
                            Json.Append(",");
                        }
                    }
                    Json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        Json.Append(",");
                    }
                }
            }
            Json.Append("]}");
            return Json.ToString();
        }

        /// <summary>   
        /// DataReader转换为Json   
        /// </summary>   
        /// <param name="dataReader">DataReader对象</param>   
        /// <returns>Json字符串</returns>   
        public static string ToJson(DbDataReader dataReader)
        {
            StringBuilder jsonString = new StringBuilder();
            jsonString.Append("[");
            while (dataReader.Read())
            {
                jsonString.Append("{");
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    Type type = dataReader.GetFieldType(i);
                    string strKey = dataReader.GetName(i);
                    string strValue = dataReader[i].ToString();
                    jsonString.Append("\"" + strKey + "\":");
                    strValue = StringFormat(strValue, type);
                    if (i < dataReader.FieldCount - 1)
                    {
                        jsonString.Append(strValue + ",");
                    }
                    else
                    {
                        jsonString.Append(strValue);
                    }
                }
                jsonString.Append("},");
            }
            dataReader.Close();
            jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]");
            return jsonString.ToString();
        }

        /// <summary>   
        /// DataSet转换为Json   
        /// </summary>   
        /// <param name="dataSet">DataSet对象</param>   
        /// <returns>Json字符串</returns>   
        public static string ToJson(DataSet dataSet)
        {
            string jsonString = "{";
            foreach (DataTable table in dataSet.Tables)
            {
                jsonString += "\"" + table.TableName + "\":" + ToJson(table) + ",";
            }
            jsonString = jsonString.TrimEnd(',');
            return jsonString + "}";
        }

        /// <summary>  
        /// 过滤特殊字符  
        /// </summary>  
        /// <param name="s"></param>  
        /// <returns></returns>  
        private static string String2Json(String s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s.ToCharArray()[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\""); break;
                    case '\\':
                        sb.Append("\\\\"); break;
                    case '/':
                        sb.Append("\\/"); break;
                    case '\b':
                        sb.Append("\\b"); break;
                    case '\f':
                        sb.Append("\\f"); break;
                    case '\n':
                        sb.Append("\\n"); break;
                    case '\r':
                        sb.Append("\\r"); break;
                    case '\t':
                        sb.Append("\\t"); break;
                    default:
                        sb.Append(c); break;
                }
            }
            return sb.ToString();
        }

        /// <summary>  
        /// 格式化字符型、日期型、布尔型  
        /// </summary>  
        /// <param name="str"></param>  
        /// <param name="type"></param>  
        /// <returns></returns>  
        private static string StringFormat(string str, Type type)
        {
            if (type == typeof(string))
            {
                str = String2Json(str);
                str = "\"" + str + "\"";
            }
            else if (type == typeof(DateTime))
            {
                DateTime dt = DateTime.Parse(str);
                str = "\"" + dt.GetDateTimeFormats('s')[0].ToString() + "\"";
                // str = + str + ;
            }
            else if (type == typeof(bool))
            {
                str = str.ToLower();
            }
            return str;
        }
    }

}
