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
    public class ActionGroupController : BaseController
    {
        IActionGroupService _actiongroupService = new ActionGroupService();
        IRoleService _roleServices = new RoleService();

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 读取用户所有的权限组的信息显示在前台
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllActionGroupInfo()
        {
            int pageIndex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pageSize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);
            string GroupName = Request["SearchActionName"];
            string GroupType = Request["SearchActionType"];

            GetModelQuery actionGroupInfo = new GetModelQuery();
            actionGroupInfo.pageIndex = pageIndex;
            actionGroupInfo.pageSize = pageSize;
            actionGroupInfo.total = 0;
            actionGroupInfo.GroupName = GroupName;
            actionGroupInfo.GroupType = GroupType;

            // var data = _actiongroupService.LoadPagerEntities(pageSize, pageIndex, out total, c => true, true, c => c.ID);
            var data = from x in _actiongroupService.LoadEntityActionGroup(actionGroupInfo)
                       select new { x.ID, x.DelFlag, x.GroupName, x.GroupType };
            var postJson = new { total = actionGroupInfo.total, rows = data };

            return Json(postJson, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 实现添加菜单组的详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult AddActionGroupInfo(ActionGroup actionGroup)
        {
            actionGroup.DelFlag = 0;
            _actiongroupService.AddEntities(actionGroup);
            return Content("OK");
        }

        /// <summary>
        /// 实现对前台信息的绑定
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult BindActionGroupInfo(int ID)
        {
            var data = _actiongroupService.LoadEntities(c => c.ID == ID).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 实现对菜单组的修改工作
        /// </summary>
        /// <param name="actionGroup"></param>
        /// <returns></returns>
        public ActionResult UpdateActionGroup(ActionGroup actionGroup)
        {
            //首先查询出所有的actionInfo的单个信息根据ID
            var editorActionInfo = _actiongroupService.LoadEntities(c => c.ID == actionGroup.ID).FirstOrDefault();
            //获取要删除的数据
            editorActionInfo.GroupName = actionGroup.GroupName;
            editorActionInfo.GroupType = actionGroup.GroupType;

            _actiongroupService.UpdateEntities(editorActionInfo);

            return Content("OK");
        }

        /// <summary>
        /// 解析删除权限组的信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult DeleteActionGroupInfo(string ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                return Content("请选择您要删除的数据");
            }
            var deleteID = ID.Split(',');
            //定义数组存放需要删除的ID
            List<int> list = new List<int>();
            foreach (var Dsid in deleteID)
            {
                list.Add(Convert.ToInt32(Dsid));
            }
            //然后执行删除的方法删除数据
            _actiongroupService.DeleteSetActionGroupInfo(list);
            return Content("OK");
        }

        /// <summary>
        /// 获取角色信息显示在前台页面中
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult SetRole(int ID)
        {
            //首先根据ID,查询出菜单组的所有的信息
            var actionGroup = _actiongroupService.LoadEntities(c => c.ID == ID).FirstOrDefault();
            ViewData.Model = actionGroup;

            //然后查询出所有的角色信息显示在前台
            short RoleID = (short)DelFlagEnum.Normal;
            var allRoleInfo = _roleServices.LoadEntities(c => c.DelFlag == RoleID).ToList();
            ViewBag.RoleInfo = allRoleInfo;

            //判断此角色是否被选择
            ViewBag.exists = (from n in actionGroup.Role  //菜单项和角色表的中间项
                              select n.ID).ToList();


            return View();
        }

        /// <summary>
        /// 对菜单项的角色的添加信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetRole()
        {
            //根据前台隐藏字段传递过来菜单项的ID信息
            int GroupID = Request["HidenID"] == null ? 0 : Convert.ToInt32(Request["HidenID"]);
            //查询出菜单项中的GroupID的数据
            var GroupInfo = _actiongroupService.LoadEntities(c => c.ID == GroupID).FirstOrDefault();
            if (GroupInfo != null)
            {
                //判断如果菜单项不为空的话，读取前台的所有的信息
                var allKeys = from n in Request.Form.AllKeys
                              where n.StartsWith("al_")
                              select n;
                //定义一个集合存放传递过来的key
                List<int> list = new List<int>();
                if (GroupID > 0)
                {
                    foreach (var key in allKeys)
                    {
                        list.Add(Convert.ToInt32(key.Replace("al_", "")));
                    }
                }
                _actiongroupService.setRole(GroupID, list);
            }
            return Content("OK");
        }

    }
}
