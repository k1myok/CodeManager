using LYZJ.HM3Shop.IBLL;
using LYZJ.HM3Shop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYZJ.HM3Shop.BLL
{
    public partial class ActionInfoService : BaseService<ActionInfo>, IActionInfoService
    {
        /// <summary>
        /// 实现删除多条数据
        /// </summary>
        /// <param name="ActionInfoID"></param>
        /// <returns></returns>
        public int DeleteActionInfo(List<int> ActionInfoID)
        {
            foreach (var ID in ActionInfoID)
            {
                _dbSession.ActionInfoRepository.DeleteEntities(new ActionInfo { ID = ID });
            }
            return _dbSession.SaveChanges();
        }

        /// <summary>
        /// 实现对数据的模糊查询
        /// </summary>
        /// <param name="actionInfo"></param>
        /// <returns></returns>
        public IQueryable<ActionInfo> LoadDataActionInfo(GetModelQuery actionInfo)
        {
            //首先拿到所有的数据
            var temp = _dbSession.ActionInfoRepository.LoadEntities(u => true);

            //然后进行权限名称过滤
            if (!string.IsNullOrEmpty(actionInfo.ActionName))
            {
                temp = temp.Where<ActionInfo>(c => c.ActionName.Contains(actionInfo.ActionName));
            }

            //然后进行请求方式的过滤
            if (!string.IsNullOrEmpty(actionInfo.RequestHttpType))
            {
                temp = temp.Where<ActionInfo>(c => c.RequestHttpType.Contains(actionInfo.RequestHttpType));
            }
            
            //返回总数
            actionInfo.total = temp.Count();

            //最后实现分页
            return temp.Skip<ActionInfo>(actionInfo.pageSize * (actionInfo.pageIndex - 1)).Take<ActionInfo>(actionInfo.pageSize).AsQueryable();
        }

        /// <summary>
        /// 设置权限的角色信息
        /// </summary>
        /// <param name="actionID"></param>
        /// <param name="List"></param>
        /// <returns></returns>
        public bool SetRole(int actionID, List<int> List)
        {
            //首先读到用户的选中的权限ID的信息
            var currrentActionInfo = _dbSession.ActionInfoRepository.LoadEntities(c => c.ID == actionID).FirstOrDefault();
            //判断是否为空
            if (currrentActionInfo == null)
            {
                return false;
            }
            //将角色项目全部给移除两个表之间的关联
            currrentActionInfo.Role.Clear();
            //在此循环便利给权限添加角色信息
            foreach (var roleID in List)
            {
                //首先查询出角色的所有的信息
                var currentRole = _dbSession.RoleRepository.LoadEntities(c => c.ID == roleID).FirstOrDefault();
                currrentActionInfo.Role.Add(currentRole);
            }
            //保存设置的角色信息
            return _dbSession.SaveChanges() > 0;
        }

        /// <summary>
        /// 设置权限的菜单项信息
        /// </summary>
        /// <param name="actionID"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool SetAction(int actionID, List<int> list)
        {
            //首先根据所有的actionID读取出所有的Action信息
            var currentActionInfo = _dbSession.ActionInfoRepository.LoadEntities(c => c.ID == actionID).FirstOrDefault();
            //判断得到的对象是否为空
            if (currentActionInfo == null)
            {
                return false;
            }
            //将菜单项全部移除出这两个表的观念
            currentActionInfo.ActionGroup.Clear();
            //然后循环遍历给权限添加菜单项
            foreach (var aID in list)
            {
                //首先查询出菜单项的所有信息
                var currentAction = _dbSession.ActionGroupRepository.LoadEntities(c => c.ID == aID).FirstOrDefault();
                currentActionInfo.ActionGroup.Add(currentAction);
            }
            //保存设置的菜单项信息
            return _dbSession.SaveChanges() > 0;
        }
    }
}
