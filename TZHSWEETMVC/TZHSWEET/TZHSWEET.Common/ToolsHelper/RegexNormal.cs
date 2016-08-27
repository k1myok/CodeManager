using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TZHSWEET.Common
{
    /// <summary>
    /// 关于正则验证信息类
    /// </summary>
    public class NormalRegexType
    { 
     /// <summary>
        /// 正则验证帐号和密码的模式字符串(字母开头，允许5-16字节，允许字母数字下划线)
        /// </summary>
        public static string IsNumberOrPasswordPattern = @"^[a-zA-Z][a-zA-Z0-9_]{4,15}$ ";
        /// <summary>
        /// 正则验证Email的模式字符串
        /// </summary>
        public static string IsEmailPattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)* ";
    }
 

    
}
