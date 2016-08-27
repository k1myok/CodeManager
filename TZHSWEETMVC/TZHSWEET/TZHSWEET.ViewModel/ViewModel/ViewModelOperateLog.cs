/*  作者：       tianzh
*  创建时间：   2012/7/21 23:40:40
*
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Routing;
using TZHSWEET.Common;
using TZHSWEET.Entity;

namespace TZHSWEET.ViewModel
{
    /// <summary>
    /// 给UI展示的ViewModel
    /// </summary>
    public class ViewModelOperateLog
    {
        #region - 属性 -

        /// <summary>
        /// ID 
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 当前进程名称(Controller控制器名称)
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// 进程描述(Controller控制器描述)
        /// </summary>
        public string ProcessDesc { get; set; }

        /// <summary>
        /// 方法(Action动作)
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// 方法描述
        /// </summary>
        public string MethodDesc { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int? UserID { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 访问的IP地址
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateDate { get; set; }

        #endregion

        #region - 构造函数 -

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ViewModelOperateLog()
        {

        }

        /// <summary>
        /// 系统日志构造函数
        /// </summary>
        /// <param name="context">当前上下文</param>
        /// <param name="userName">用户名称</param>
        /// <param name="userID">用户ID</param>
        /// <param name="description">描述</param>
        public ViewModelOperateLog(string description)
        {
            RouteData routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));
            ProcessName = routeData.Values["controller"].ToString();
            MethodName = routeData.Values["action"].ToString();
            IPAddress = HttpContext.Current.Request.UserHostAddress;
            CreateDate = DateTime.Now;
            this.UserID = Convert.ToInt32(SessionHelper.Get("UserID"));
            this.UserName = SessionHelper.Get("Title");
            Description = description;
        }

        #endregion

        #region - 方法 -
        #region ToEntity
        /// <summary>
        /// 转化为tbOperateLog的 model信息
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public static tbOperateLog ToEntity(ViewModelOperateLog log)
        {
            tbOperateLog operateLog = new tbOperateLog();
            operateLog.IPAddress = log.IPAddress;
            operateLog.ProcessName = log.ProcessName;
            operateLog.MethodName = log.MethodName;
            operateLog.UserID = log.UserID;
            operateLog.UserName = log.UserName;
            operateLog.CreateDate = log.CreateDate;
            operateLog.Description = log.Description;
            return operateLog;
        }
        #endregion
        #region ToViewModel

        /// <summary>
        /// 转化为ViewModelOperateLog
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public static ViewModelOperateLog ToViewModel(tbOperateLog log)
        {
            ViewModelOperateLog item = new ViewModelOperateLog();
            item.ID = log.ID;
            item.ProcessName = log.ProcessName;
            item.ProcessDesc = log.ProcessDesc;
            item.MethodName = log.MethodName;
            item.MethodDesc = log.MethodDesc;
            item.UserID = log.UserID;
            item.UserName = log.UserName;
            item.IPAddress = log.IPAddress;
            item.Description = log.Description;
            item.CreateDate = log.CreateDate;
            return item;
        } 
        #endregion

        #endregion


    }
}
