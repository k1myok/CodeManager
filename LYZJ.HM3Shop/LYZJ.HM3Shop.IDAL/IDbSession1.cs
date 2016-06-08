 

using System.Data.Objects;
namespace LYZJ.HM3Shop.IDAL
{
    public partial interface IDbSession
    {
   
	  

		IDAL.IActionGroupRepository ActionGroupRepository { get; }
	  

		IDAL.IActionInfoRepository ActionInfoRepository { get; }
	  

		IDAL.IR_UserInfo_ActionInfoRepository R_UserInfo_ActionInfoRepository { get; }
	  

		IDAL.IR_UserInfo_RoleRepository R_UserInfo_RoleRepository { get; }
	  

		IDAL.IRoleRepository RoleRepository { get; }
	  

		IDAL.IUserInfoRepository UserInfoRepository { get; }
	}
}