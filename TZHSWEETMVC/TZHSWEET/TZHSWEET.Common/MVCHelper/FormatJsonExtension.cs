/*  作者：       tianzh
*  创建时间：   2012/7/19 15:42:15
*
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Web.Script.Serialization;
using System.IO;
using System.Globalization;
using Newtonsoft.Json;
namespace TZHSWEET.Common
{

    /// <summary>
    /// 格式化json扩展
    /// </summary>
    public static class FormatJsonExtension
    {
        /// <summary>
        /// 普通序列化(不进行UI友好的json化)
        /// </summary>
        /// <param name="c">控制器</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static FormatJsonResult JsonFormat(this Controller c, object data)
        { 
          FormatJsonResult result = new FormatJsonResult();
          result.NotLigerUIFriendlySerialize = true;
            result.Data = data;
            return result;
        
        }
        /// <summary>
        /// UI友好的json格式序列化
        /// </summary>
        /// <param name="c"></param>
        /// <param name="data"></param>
        /// <param name="IsError"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static FormatJsonResult JsonFormat(this Controller c, object data, bool IsError, string message)
        {
            FormatJsonResult result = new FormatJsonResult();

            result.Data = data;
            result.Message = message;
            result.IsError = IsError;
            return result;
        }
        /// <summary>
        /// 根据操作和提供的数据判断执行状态
        /// </summary>
        /// <param name="c">控制器</param>
        /// <param name="data">数据</param>
        /// <param name="op">操作类型(增删改查,等等)</param>
        /// <returns></returns>
        public static FormatJsonResult JsonFormat(this Controller c, object data,SysOperate op)
        {
           
            if (!data.IsNullOrEmpty())
            {
               return JsonFormatSuccess(c, data, op.ToMessage(true));
            }
            return JsonFormatError(c,op.ToMessage(false));
        }
        /// <summary>
        /// 根据操作和提供的数据判断执行状态
        /// </summary>
        /// <param name="c">控制器</param>
        /// <param name="data">数据</param>
        /// <param name="status">数据</param>
        /// <param name="op">操作类型(增删改查,等等)</param>
        /// <returns></returns>
        public static FormatJsonResult JsonFormat(this Controller c, object data,bool status, SysOperate op)
        {

            if (status)
            {
                return JsonFormatSuccess(c, data, op.ToMessage(true));
            }
            return JsonFormatError(c, op.ToMessage(false));
        }
        /// <summary>
        /// 成功的json返回
        /// </summary>
        /// <param name="c"></param>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static FormatJsonResult JsonFormatSuccess(this Controller c, object data,string message)
        {
            return JsonFormat(c, data, false, message);
        }
        /// <summary>
        /// 失败的json返回
        /// </summary>
        /// <param name="c"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static FormatJsonResult JsonFormatError(this Controller c, string message)
        {
            return JsonFormat(c, null, true, message);
        }

    }
    /// <summary>
    /// JsonResult格式化扩展
    /// </summary>
    public class FormatJsonResult : ActionResult
    {
        private bool iserror = false;
        /// <summary>
        /// 是否产生错误
        /// </summary>
        public bool IsError
        {
            get { return iserror; }
            set { this.iserror = value; }
        }

        /// <summary>
        /// 错误信息，或者成功信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 成功可能时返回的数据
        /// </summary>
        public Object Data { get; set; }
        /// <summary>
        /// 正常序列化方式(为True则不进行UI友好的序列化)
        /// </summary>
        public bool NotLigerUIFriendlySerialize { get; set; }
        public override void ExecuteResult(ControllerContext context)
        {
         
                if (context == null)
                {
                    throw new ArgumentNullException("context");
                }
                HttpResponseBase response = context.HttpContext.Response;
                response.ContentType = "application/json";

                StringWriter sw = new StringWriter();
                JsonSerializer serializer = JsonSerializer.Create(
                    new JsonSerializerSettings
                    {
                        // Converters = new JsonConverter[] { new Newtonsoft.Json.Converters.IsoDateTimeConverter() },
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore
                       
                    }
                    );
               
              
                using (JsonWriter jsonWriter = new JsonTextWriter(sw))
                {
                    jsonWriter.Formatting = Formatting.Indented;

                    if (!NotLigerUIFriendlySerialize)
                        serializer.Serialize(jsonWriter, this);
                    else
                        serializer.Serialize(jsonWriter, Data);
                }
                response.Write(sw.ToString());

        }
    }
}


