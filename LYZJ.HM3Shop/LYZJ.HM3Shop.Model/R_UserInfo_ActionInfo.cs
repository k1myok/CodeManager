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
    
    public partial class R_UserInfo_ActionInfo
    {
        public int ID { get; set; }
        public int UserInfoID { get; set; }
        public int ActionInfoID { get; set; }
        public bool HasPermation { get; set; }
    
        public virtual UserInfo UserInfo { get; set; }
        public virtual ActionInfo ActionInfo { get; set; }
    }
}
