using LYZJ.HM3Shop.BLL;
using LYZJ.HM3Shop.IBLL;
using LYZJ.HM3Shop.Model;
using LYZJ.HM3Shop.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LYZJ.HM3Shop.UI.Portal.Controllers
{
    public class ActionInfoController : BaseController
    {
        //首先实例化一个ActionInfo对象
        IActionInfoService _actioninfoService = new ActionInfoService();
        IRoleService _roleService = new RoleService();
        IActionGroupService _actionGroupService = new ActionGroupService();

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取权限的所有的信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetActionUserInfoShow()
        {
            //首先获取前台传递过来的参数
            int pageIndex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pageSize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);

            //获取前台传递过来的数据实现进行模糊查询
            string SearchActionName = Request["SearchActionName"];
            string SearchRequestHttpType = Request["SearchRequestHttpType"];

            //定义对象，得到所有的参数值
            GetModelQuery actionInfo = new GetModelQuery();
            actionInfo.ActionName = SearchActionName;
            actionInfo.pageIndex = pageIndex;
            actionInfo.pageSize = pageSize;
            actionInfo.RequestHttpType = SearchRequestHttpType;
            actionInfo.total = 0;

            //调用方法，实现绑定所有的数据
            var data = from c in _actioninfoService.LoadDataActionInfo(actionInfo)
                       select new { c.ID, c.ActionName, c.ActionType, c.RequestUrl, c.RequestHttpType, c.SubTime };
            //var data = _actioninfoService.LoadPagerEntities(pageSize, pageIndex, out total, c => true, true, e => e.ID);
            //获取前台需要的数据
            var jsonResult = new { total = actionInfo.total, rows = data };
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加用户权限信息
        /// </summary>
        /// <param name="actioninfo"></param>
        /// <returns></returns>
        public ActionResult AddActionInfo(ActionInfo actioninfo)
        {
            actioninfo.SubTime = DateTime.Now;
            _actioninfoService.AddEntities(actioninfo);
            return Content("OK");
        }

        /// <summary>
        /// 绑定用户权限问题
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult BindActionInfo(int ID)
        {
            var jsonData = _actioninfoService.LoadEntities(c => c.ID == ID).FirstOrDefault();
            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 对用户权限信息进行修改
        /// </summary>
        /// <param name="actionInfo"></param>
        /// <returns></returns>
        public ActionResult UpdateActionInfo(ActionInfo actionInfo)
        {
            //首先查询出所有的用户权限信息
            ActionInfo EditActionInfo = _actioninfoService.LoadEntities(c => c.ID == actionInfo.ID).FirstOrDefault();
            if (EditActionInfo == null)
            {
                return Content("修改错误，请您检查");
            }
            //给要修改的实体对象赋值
            EditActionInfo.ActionName = actionInfo.ActionName;
            EditActionInfo.RequestHttpType = actionInfo.RequestHttpType;
            EditActionInfo.RequestUrl = actionInfo.RequestUrl;
            EditActionInfo.ActionType = actionInfo.ActionType;
            //进行修改信息
            _actioninfoService.UpdateEntities(EditActionInfo);
            return Content("OK");
        }

        /// <summary>
        /// 多选删除用户信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult DeleteActionInfo(string ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                return Content("请选择您要删除的数据");
            }
            var deleteID = ID.Split(',');

            //定义List集合存放这些需要删除的数据
            List<int> list = new List<int>();
            foreach (var dID in deleteID)
            {
                list.Add(Convert.ToInt32(dID));
            }

            //实现删除多条数据的方法
            if (_actioninfoService.DeleteActionInfo(list)>0)
            {
                return Content("OK");
            }
            return Content("删除失败，请您检查");

            return View();
        }

        /// <summary>
        /// 实现设置权限角色信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult SetRole(int ID)
        {
            //首先根据传递过来的ID找到权限信息
            var currrentSetRoleAction = _actioninfoService.LoadEntities(u => u.ID == ID).FirstOrDefault();
            ViewData.Model = currrentSetRoleAction;

            //传递过去前台需要用到的数据,遍历角色，显示出来
            short RoleNomal = (short)DelFlagEnum.Normal;
            var allRoles = _roleService.LoadEntities(c => c.DelFlag == RoleNomal).ToList();
            ViewBag.AllRoles = allRoles;

            //传递给前台权限对应的角色信息，方便用户给角色设置权限
            ViewBag.ExistRoleIds = (from n in currrentSetRoleAction.Role  //权限和角色表的中间表的数据
                                    select n.ID).ToList();

            return View();
        }

        /// <summary>
        /// 处理权限的的请求，添加角色信息，
        /// </summary>
        /// <returns></returns>
        [HttpPost ]
        public ActionResult SetRole()
        {
            //首先读取到前台传递过来的权限ID
            int ActionID = Request["hidenActionID"] == null ? 0 : Convert.ToInt32(Request["hidenActionID"]);
            //查询出当前权限的所有的信息,根据Action
            var currentActionInfo = _actioninfoService.LoadEntities(c => c.ID == ActionID).FirstOrDefault();
            if (currentActionInfo != null)
            {
                //判断如果权限信息不为空的话执行，拿到前台表单传递的信息，进行操作，ckb_1,ckb_2,ckb_3
                var allKeys = from key in Request.Form.AllKeys
                              where key.Contains("ckb_")
                              select key;
                //定义一个List集合存放传递过来的Key
                List<int> list = new List<int>();
                if (ActionID > 0)
                {
                    //循环便利所有的前台的信息展示给用户显示
                    foreach (var key in allKeys)
                    {
                        list.Add(Convert.ToInt32(key.Replace("ckb_", "")));
                    }
                }
                _actioninfoService.SetRole(ActionID, list);
            }
            return Content("OK");
        }

        /// <summary>
        /// 获取设置用户分组的信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult SetAction(int ID)
        {
            //根据ID查询出权限信息的信息
            var currentUser = _actioninfoService.LoadEntities(u => u.ID == ID).FirstOrDefault();
            ViewData.Model = currentUser;

            //根据传递过去的菜单组获取到所有的菜单组信息
            short ActionID = (short)DelFlagEnum.Normal;
            var allAction = _actionGroupService.LoadEntities(c => c.DelFlag == ActionID).ToList();
            ViewBag.AllAction = allAction; 
            //然后传递给前台判断权限组数据是否被选中
            ViewBag.Exists = (from n in currentUser.ActionGroup  //获取到权限表
                           select n.ID).ToList();
            return View();
        }

        /// <summary>
        /// 对权限里面的权限分组设置菜单组的操作
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetAction()
        {
            //首先根据前台传递过来的隐藏域得到actionID
            int actionID = Request["hidenActionID"] == null ? 0 : Convert.ToInt32(Request["hidenActionID"]);
            //根据actionID查询出来当前权限对应的ID
            var currentActionInfo = _actioninfoService.LoadEntities(c => c.ID == actionID).FirstOrDefault();
            if (currentActionInfo != null)
            {
                //拿到前台表单传递的表单选中值，形势为act_1，act_2,act_3
                var allKeys = from key in Request.Form.AllKeys
                              where key.Contains("act_")
                              select key;
                //定义一个集合用来存放传递过来的Key
                List<int> list = new List<int>();
                //循环遍历得到所有的前台数据显示在这里
                if (actionID > 0)
                {
                    foreach (var key in allKeys)
                    {
                        list.Add(Convert.ToInt32(key.Replace("act_", "")));
                    }
                }
                _actioninfoService.SetAction(actionID, list);
            }
            return Content("OK");
        }
    }
}
