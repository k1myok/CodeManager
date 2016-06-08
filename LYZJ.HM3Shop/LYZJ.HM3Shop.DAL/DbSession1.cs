 
using System.Data.Objects;

namespace LYZJ.HM3Shop.DAL
{
    public partial class DbSession:IDAL.IDbSession  
    {   
	



	public IDAL.IActionGroupRepository ActionGroupRepository { get { return new ActionGroupRepository(); } }

	



	public IDAL.IActionInfoRepository ActionInfoRepository { get { return new ActionInfoRepository(); } }

	



	public IDAL.IR_UserInfo_ActionInfoRepository R_UserInfo_ActionInfoRepository { get { return new R_UserInfo_ActionInfoRepository(); } }

	



	public IDAL.IR_UserInfo_RoleRepository R_UserInfo_RoleRepository { get { return new R_UserInfo_RoleRepository(); } }

	



	public IDAL.IRoleRepository RoleRepository { get { return new RoleRepository(); } }

	



	public IDAL.IUserInfoRepository UserInfoRepository { get { return new UserInfoRepository(); } }

	}
}