 /*  作者：       tianzh
 *  创建时间：   2012/7/23 23:30:59
 *
 */
/*  作者：       tianzh
*  创建时间：   2012/7/23 22:48:43
*
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TZHSWEET.Common
{
    /// <summary>
    /// 系统的常见操作
    /// </summary>
    public  enum SysOperate
    { 
        Add=0,
        Update=1,
        Load=2,
        Delete=3,
        Operate=4,
        UnkownError=5
    }
    
    /// <summary>
    /// 系统管理模块友好的提示信息
    /// </summary>
    public  enum SystemMessage
    {
        LoadSuccess = 0,
        LoadError = 1,
        OperateSuccess = 2,
        OperateError = 3,
        AddSuccess = 4,
        AddError = 5,
        UpdateSuccess = 6,
        UpdateError = 7,
        DeleteSuccess = 8,
        DeleteError = 9,
        UnkownError = 10
    }
    /// <summary>
    /// 用户模块界面友好的消息提示
    /// </summary>
    public  enum UserMessage
    {
        UserNotLogin = 0,
        UserLoginSuccess = 1,
        UserLoginError = 2,
        ChangePasswordSuccess = 3,
        ChangePasswordError = 4,
        UnkonwnError = 5
    }
    #region 友好的提示信息
    /// <summary>
    /// 友好的提示信息
    /// </summary>
    public static class FriendlyMessage
    {
        #region
        /// <summary>
        /// 获取消息(根据操作类型和状态)
        /// </summary>
        /// <param name="sysOp">操作类型</param>
        /// <param name="status">执行状态</param>
        /// <returns></returns>
        public static string ToMessage(this SysOperate sysOp, bool status)
        {
            string message = "";
            //根据操作类型和执行状态返回消息
            switch (sysOp)
            {
                case SysOperate.Add:
                    message = status ? SystemMessage.AddSuccess.ToMessage() : SystemMessage.AddError.ToMessage();
                    break;
                case SysOperate.Load:
                    message = status ? SystemMessage.LoadSuccess.ToMessage() : SystemMessage.LoadError.ToMessage();
                    break;
                case SysOperate.Update:
                    message = status ? SystemMessage.UpdateSuccess.ToMessage() : SystemMessage.UpdateError.ToMessage();
                    break;

                case SysOperate.Delete:
                    message = status ? SystemMessage.DeleteSuccess.ToMessage() : SystemMessage.DeleteError.ToMessage();
                    break;
                case SysOperate.Operate:
                    message = status ? SystemMessage.OperateSuccess.ToMessage() : SystemMessage.OperateError.ToMessage();
                    break;
                case SysOperate.UnkownError:
                    message = SystemMessage.UnkownError.ToMessage();
                    break;
            }
            return message;
        }
        /// <summary>
        /// 获取系统管理模块友好提示信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string ToMessage(this SystemMessage code)
        {
            string message = "";
            switch (code)
            {
                case SystemMessage.AddSuccess:
                    message = "添加成功!";
                    break;
                case SystemMessage.AddError:
                    message = "添加失败!";
                    break;
                case SystemMessage.DeleteSuccess:
                    message = "删除成功!";
                    break;
                case SystemMessage.DeleteError:
                    message = "删除失败!";
                    break;
                case SystemMessage.LoadSuccess:
                    message = "加载成功!";
                    break;
                case SystemMessage.LoadError:
                    message = "加载失败!";
                    break;
                case SystemMessage.OperateSuccess:
                    message = "操作成功!";
                    break;
                case SystemMessage.OperateError:
                    message = "操作失败!";
                    break;
                case SystemMessage.UpdateSuccess:
                    message = "更新成功!";
                    break;
                case SystemMessage.UpdateError:
                    message = "更新失败!";
                    break;
                case SystemMessage.UnkownError:
                    message = "未知错误!";
                    break;
                default:
                    message = "错误";
                    break;
            }
            return message;
        }
        #endregion


        #region 获取用户模块界面友好的消息提示
        /// <summary>
        /// 获取用户模块界面友好的消息提示
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string ToMessage(this UserMessage code)
        {
            string message = "";
            switch (code)
            {
                case UserMessage.UserNotLogin:
                    message = "用户未登录!";
                    break;
                case UserMessage.UserLoginSuccess:
                    message = "登录成功!";
                    break;
                case UserMessage.UserLoginError:
                    message = "用户登录失败!";
                    break;
                case UserMessage.ChangePasswordSuccess:
                    message = "修改密码成功!";
                    break;
                case UserMessage.ChangePasswordError:
                    message = "修改密码失败!";
                    break;
                case UserMessage.UnkonwnError:
                    message = "未知错误";
                    break;
                default:
                    message = "未知错误";
                    break;
            }
            return message;
        }
        #endregion
    } 
    #endregion
    
}
