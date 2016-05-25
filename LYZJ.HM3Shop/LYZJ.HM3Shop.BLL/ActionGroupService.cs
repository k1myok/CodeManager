using LYZJ.HM3Shop.IBLL;
using LYZJ.HM3Shop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYZJ.HM3Shop.BLL
{
    public partial class ActionGroupService : BaseService<ActionGroup>, IActionGroupService
    {
        public IQueryable<ActionGroup> LoadEntityActionGroup(GetModelQuery actionGroup)
        {
            //首先读取到所有的数据
            var temp = _dbSession.ActionGroupRepository.LoadEntities(c => true);

            //根据菜单组名称进行过滤
            if (!string.IsNullOrEmpty(actionGroup.GroupName))
            {
                temp = temp.Where<ActionGroup>(c => c.GroupName.Contains(actionGroup.GroupName));
            }
            //根据菜单组类型进行过滤
            if (actionGroup.GroupType != "-1" && !string.IsNullOrEmpty(actionGroup.GroupType))
            {
                temp = temp.Where<ActionGroup>(c => c.GroupType.Equals(Convert.ToInt16(actionGroup.GroupType)));
            }
            //得到菜单组的总数
            actionGroup.total = temp.Count();

            //进行分页查询信息
            return temp.Skip<ActionGroup>(actionGroup.pageSize * (actionGroup.pageIndex - 1)).Take(actionGroup.pageSize);
        }

        /// <summary>
        /// 实现对权限组的信息的删除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int DeleteSetActionGroupInfo(List<int> list)
        {
            foreach (var ID in list)
            {
                _dbSession.ActionGroupRepository.DeleteEntities(new ActionGroup { ID = ID });
            }
            return _dbSession.SaveChanges();
        }

        /// <summary>
        /// 设置菜单项的角色信息
        /// </summary>
        /// <param name="actionID"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool setRole(int actionID, List<int> list)
        {
            //首先根据actionId查询出菜单组的对象
            var ActionGroupInfoShow = _dbSession.ActionGroupRepository.LoadEntities(c => c.ID == actionID).FirstOrDefault();
            //判断得到的对象是否为空
            if (ActionGroupInfoShow == null)
            {
                return false;
            }
            //删除以前的旧数据
            ActionGroupInfoShow.Role.Clear();

            //然后将List集合循环遍历添加到项目中即可
            foreach (var roleID in list)
            {
                //首先查询出角色ID的信息
                var RoleInfo = _dbSession.RoleRepository.LoadEntities(c => c.ID == roleID).FirstOrDefault();
                //实现添加
                ActionGroupInfoShow.Role.Add(RoleInfo);
            }
            return _dbSession.SaveChanges() > 0;
        }
    }
}
