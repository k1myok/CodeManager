using LYZJ.HM3Shop.BLL;
using LYZJ.HM3Shop.IBLL;
using LYZJ.HM3Shop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LYZJ.HM3Shop.UI.Portal.Controllers
{
    public class RoleController : BaseController
    {
        IRoleService _roleService = new RoleService();

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 实现对用户角色的绑定
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllUserRoleInfo()
        {
            //首先获取从前台传递过来的参数
            int pageIndex = Request["page"] == null ? 1 : Convert.ToInt32(Request["page"]);
            int pageSize = Request["rows"] == null ? 10 : Convert.ToInt32(Request["rows"]);

            //获取从前台传递过来的需要多条件模糊查询的数据
            string RoleName = Request["RoleName"];
            string RoleType = Request["RoleType"];

            //定义对象，得到所有的参数
            GetModelQuery roleInfo = new GetModelQuery();
            roleInfo.pageIndex = pageIndex;
            roleInfo.pageSize = pageSize;
            roleInfo.total = 0;
            roleInfo.RoleName = RoleName;
            roleInfo.RoleType = RoleType;

            //获取所有的总数输入
            var data = from n in _roleService.LoadRoleInfo(roleInfo)
                       select new { n.ID, n.DelFlag, n.RoleName, n.RoleType, n.SubTime };
            //var data = _roleService.LoadPagerEntities(pageSize, pageIndex, out total, c => true, true, d => d.ID);

            var jsonResult = new { total = roleInfo.total, rows = data };

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 实现对用户角色的添加信息
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public ActionResult AddUserRoleInfo(Role role)
        {
            //实现对用户的添加信息
            role.DelFlag = (short)LYZJ.HM3Shop.Model.Enum.DelFlagEnum.Normal;
            role.SubTime = DateTime.Now;
            _roleService.AddEntities(role);
            return Content("OK");
        }

        /// <summary>
        /// 实现对用户角色的绑定信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult BindUserRoleInfo(int ID)
        {
            var BindUserRoleInfoJson = _roleService.LoadEntities(c => c.ID == ID).FirstOrDefault();

            return Json(BindUserRoleInfoJson, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改用户角色信息
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        public ActionResult UpdateUserRoleInfo(Role roleInfo)
        {
            //查询出Role实体对象
            var EditRole = _roleService.LoadEntities(c => c.ID == roleInfo.ID).FirstOrDefault();

            //查询出实体对象然后修改
            EditRole.RoleName = roleInfo.RoleName;
            EditRole.RoleType = roleInfo.RoleType;
            _roleService.UpdateEntities(EditRole);
            return Content("OK");
        }

        /// <summary>
        /// 删除用户角色信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult DeleteUserRoleInfo(string ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                return Content("请选择您要删除的数据");
            }

            //截取传递过来的字符串显示成数组形式
            var deleteID = ID.Split(',');

            //定义数组存放删除的ID
            List<int> deleteIDList = new List<int>();

            foreach (var dID in deleteID)
            {
                deleteIDList.Add(Convert.ToInt32(dID));
            }

            if (_roleService.DeleteUserRoleInfo(deleteIDList)>0)
            {
                return Content("OK");
            }
            return Content("删除失败，请您检查");
        }

    }
}
