 /*  作者：       tianzh
 *  创建时间：   2012/7/21 22:43:48
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TZHSWEET.UI;
using System.ComponentModel;
using TZHSWEET.ViewModel;
using TZHSWEET.IBLL;
using TZHSWEET.BLL;
using TZHSWEET.Common;
using TZHSWEET.Entity;
namespace TZHSWEET.WebUI.Areas.Admin.Controllers
{
  
    [Description("日志管理")]
    public class OperateLogController :BaseController
    {
        //
        // GET: /Admin/OperateLog/
        [Description("[Index主页]系统操作日志管理")]
        [ViewPage]
        [DefaultPage]
        public ActionResult Index()
        {
            //记录操作日志
            UserOperateLog.WriteOperateLog("浏览[操作日志管理]");
            return View();
        }
        [Description("[Index页面Grid请求]获取系统操作日志信息(首页必须)")]
        [LoginAllowView]
        public ActionResult GetLogsGrid()
        {
          
            LigerUIGridRequest request = new LigerUIGridRequest(HttpContext);
            IOperateLogService logService = new OperateLogService();
            return this.JsonFormat(logService.GetAllLogsToGrid(request));
        }
        [Description("[Detail页面Load一条日志请求]获取一条日志信息(Detail必须)")]
        [LigerUIExceptionResult]
        [LoginAllowView]
        public ActionResult Get()
        {
            //获取请求信息
            RequestBase Request = new RequestBase(HttpContext);
            IOperateLogService service = new OperateLogService();
            //执行状态
            tbOperateLog log = service.GetEntity(p => p.ID ==Convert.ToInt64(Request.ID));
            var data = ViewModelOperateLog.ToViewModel(log);
            UserOperateLog.WriteOperateLog("[日志管理]" + SysOperate.Operate.ToMessage(data.IsNullOrEmpty()));
            return this.JsonFormat(data,SysOperate.Operate);
        }
        [Description("[Detail主页]查看日志详情(Detail必须)")]
        [ViewPage]
        [LigerUIExceptionResult]
        public ActionResult Detail()
        {
            ViewDetailPage page = new ViewDetailPage(HttpContext);
            ViewBag.IsView = page.IsView;
            ViewBag.CurrentID = page.CurrentID;

            UserOperateLog.WriteOperateLog("[查看日志]" + SysOperate.Operate.ToMessage(true));
            return View();
        }
        [Description("[Index页面删除请求]清空三个月以前的日志")]
        [LigerUIExceptionResult]
        public ActionResult DeleteThreeMonthLog()
        {
            IOperateLogService logService = new OperateLogService();
            //执行状态
            bool status = logService.DeleteThreeMonthLogs();
            UserOperateLog.WriteOperateLog("[清空日志]" + SysOperate.Delete.ToMessage(status));
            return this.JsonFormat(status,SysOperate.Delete);
        }


    }
}
