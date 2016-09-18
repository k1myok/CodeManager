using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Ninesky.Models
{
    public class User
    {
        [Key]
        public Guid UserID { get; set; }
        [Required(ErrorMessage = "必填")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "{1}到{0}个字符")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "必填")]
        [Display(Name = "用户组ID")]
        public int GroupID { get; set; }
        [Required(ErrorMessage = "必填")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "{1}到{0}个字符")]
        [Display(Name = "显示名")]
        public string DisplayName { get; set; }
        [Required(ErrorMessage = "必填")]
        [Display(Name="密码")]
         [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "必填")]
        [Display(Name = "邮箱")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public int Status { get; set; }
        public DateTime RegistrationTime {get;set;}
        public DateTime LoginTime { get; set; }
        public virtual UserGroup Group { get; set; }
    }
    public class UserGroup
    { 
    [Key]
        public Guid GroupID { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "必填")]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "{1}到{0}个字")]
    [Display(Name = "名称")]
    public string Name { get; set; }

    /// <summary>
    /// 用户组类型<br />
    /// 0普通类型（普通注册用户），1特权类型（像VIP之类的类型），3管理类型（管理权限的类型）
    /// </summary>
    [Required(ErrorMessage = "必填")]
    [Display(Name = "用户组类型")]
    public int GroupType { get; set; }

    /// <summary>
    /// 说明
    /// </summary>
    [Required(ErrorMessage = "必填")]
    [StringLength(50, ErrorMessage = "少于{0}个字")]
    [Display(Name = "说明")]
    public string Description { get; set; }
    }
    /// <summary>
    /// 用户配置
    /// <remarks>
    /// 创建:2014.02.06
    /// </remarks>
    /// </summary>
    public class UserConfig
    {
        [Key]
        public int ConfigID { get; set; }

        /// <summary>
        /// 启用注册
        /// </summary>
        [Display(Name = "启用注册")]
        [Required(ErrorMessage = "必填")]
        public bool Enabled { get; set; }

        /// <summary>
        /// 禁止使用的用户名<br />
        /// 用户名之间用“|”隔开
        /// </summary>
        [Display(Name = "禁止使用的用户名")]
        public string ProhibitUserName { get; set; }

        /// <summary>
        /// 启用管理员验证
        /// </summary>
        [Display(Name = "启用管理员验证")]
        [Required(ErrorMessage = "必填")]
        public bool EnableAdminVerify { get; set; }

        /// <summary>
        /// 启用邮件验证
        /// </summary>
        [Display(Name = "启用邮件验证")]
        [Required(ErrorMessage = "必填")]
        public bool EnableEmailVerify { get; set; }

        /// <summary>
        /// 默认用户组Id
        /// </summary>
        [Display(Name = "默认用户组Id")]
        [Required(ErrorMessage = "必填")]
        public int DefaultGroupId { get; set; }
    }
}
