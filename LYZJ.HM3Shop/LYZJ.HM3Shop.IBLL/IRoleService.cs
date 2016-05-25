using LYZJ.HM3Shop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYZJ.HM3Shop.IBLL
{
    public partial interface IRoleService:IBaseService<Role>
    {
        /// <summary>
        /// 多选删除
        /// </summary>
        /// <param name="deleteIDList"></param>
        /// <returns></returns>
        int DeleteUserRoleInfo(List<int> deleteIDList);

        /// <summary>
        /// 模糊条件的查询信息
        /// </summary>
        /// <param name="roleInfo"></param>
        /// <returns></returns>
        IQueryable<Role> LoadRoleInfo(GetModelQuery roleInfo);
    }
}
