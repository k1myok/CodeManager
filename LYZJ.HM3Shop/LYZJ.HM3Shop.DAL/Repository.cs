 

using LYZJ.HM3Shop.IDAL;
using LYZJ.HM3Shop.Model;

namespace LYZJ.HM3Shop.DAL
{
   
	

	public partial class ActionGroupRepository :BaseRepository<ActionGroup>,IActionGroupRepository
    {
         
    }

	

	public partial class ActionInfoRepository :BaseRepository<ActionInfo>,IActionInfoRepository
    {
         
    }

	

	public partial class R_UserInfo_ActionInfoRepository :BaseRepository<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoRepository
    {
         
    }

	

	public partial class R_UserInfo_RoleRepository :BaseRepository<R_UserInfo_Role>,IR_UserInfo_RoleRepository
    {
         
    }

	

	public partial class RoleRepository :BaseRepository<Role>,IRoleRepository
    {
         
    }

	

	public partial class UserInfoRepository :BaseRepository<UserInfo>,IUserInfoRepository
    {
         
    }

	
}