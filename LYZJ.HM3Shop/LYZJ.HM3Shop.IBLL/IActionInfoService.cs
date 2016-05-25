using LYZJ.HM3Shop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYZJ.HM3Shop.IBLL
{
    public partial interface IActionInfoService:IBaseService<ActionInfo>
    {
        //实现删除所有选择数据的接口
        int DeleteActionInfo(List<int> ActionInfoID);

        //实现模糊查询的接口
        IQueryable<ActionInfo> LoadDataActionInfo(GetModelQuery actionInfo);

        //实现将权限的角色绑定到权限上面显示
        bool SetRole(int actionID, List<int> List);

        //实现将权限的菜单项绑定到权限上面
        bool SetAction(int actionID, List<int> list);
    }
}
