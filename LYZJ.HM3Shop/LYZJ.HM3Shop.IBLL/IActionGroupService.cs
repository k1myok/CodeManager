using LYZJ.HM3Shop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYZJ.HM3Shop.IBLL
{
    public partial interface IActionGroupService : IBaseService<ActionGroup>
    {
        /// <summary>
        /// 实现模糊查询的接口
        /// </summary>
        /// <param name="actionGroup"></param>
        /// <returns></returns>
        IQueryable<ActionGroup> LoadEntityActionGroup(GetModelQuery actionGroup);

        /// <summary>
        /// 实现对权限组的删除功能
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        int DeleteSetActionGroupInfo(List<int> list);

        /// <summary>
        /// 添加菜单项的角色信息
        /// </summary>
        /// <param name="actionID"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        bool setRole(int actionID, List<int> list);

    }
}
