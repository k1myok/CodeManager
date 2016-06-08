 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LYZJ.HM3Shop.Model;

namespace LYZJ.HM3Shop.IBLL
{
   
	
	public partial interface IActionGroupService:IBaseService<ActionGroup>
    {   
    }
	
	public partial interface IActionInfoService:IBaseService<ActionInfo>
    {   
    }
	
	public partial interface IR_UserInfo_ActionInfoService:IBaseService<R_UserInfo_ActionInfo>
    {   
    }
	
	public partial interface IR_UserInfo_RoleService:IBaseService<R_UserInfo_Role>
    {   
    }
	
	public partial interface IRoleService:IBaseService<Role>
    {   
    }
	
	public partial interface IUserInfoService:IBaseService<UserInfo>
    {   
    }
	
}