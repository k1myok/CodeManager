 

using LYZJ.HM3Shop.Model;

namespace LYZJ.HM3Shop.IDAL
{
   
	
    public partial interface IActionGroupRepository :IBaseRepository<ActionGroup>
    {         
    }
	
    public partial interface IActionInfoRepository :IBaseRepository<ActionInfo>
    {         
    }
	
    public partial interface IR_UserInfo_ActionInfoRepository :IBaseRepository<R_UserInfo_ActionInfo>
    {         
    }
	
    public partial interface IR_UserInfo_RoleRepository :IBaseRepository<R_UserInfo_Role>
    {         
    }
	
    public partial interface IRoleRepository :IBaseRepository<Role>
    {         
    }
	
    public partial interface IUserInfoRepository :IBaseRepository<UserInfo>
    {         
    }
	
}