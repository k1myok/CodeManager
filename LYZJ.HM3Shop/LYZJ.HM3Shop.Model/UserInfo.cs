//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace LYZJ.HM3Shop.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserInfo
    {
        public UserInfo()
        {
            this.DelFlag = 0;
            this.R_UserInfo_Role = new HashSet<R_UserInfo_Role>();
            this.R_UserInfo_ActionInfo = new HashSet<R_UserInfo_ActionInfo>();
            this.ActionGroup = new HashSet<ActionGroup>();
            this.实体1 = new HashSet<实体1>();
        }
    
        public int ID { get; set; }
        public string UName { get; set; }
        public string Pwd { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public System.DateTime SubTime { get; set; }
        public System.DateTime LastModifiedOn { get; set; }
        public short DelFlag { get; set; }
    
        public virtual ICollection<R_UserInfo_Role> R_UserInfo_Role { get; set; }
        public virtual ICollection<R_UserInfo_ActionInfo> R_UserInfo_ActionInfo { get; set; }
        public virtual ICollection<ActionGroup> ActionGroup { get; set; }
        public virtual ICollection<实体1> 实体1 { get; set; }
    }
}
