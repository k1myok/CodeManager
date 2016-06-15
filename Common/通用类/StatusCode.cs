// 源文件头信息：
// <copyright file="StatusCode.cs">
// Copyright(c)2014-2034 Kencery.All rights reserved.
// 个人博客：http://www.cnblogs.com/hanyinglong
// 创建人：韩迎龙(kencery)
// 创建时间：2015-6-11
// </copyright>
namespace KenceryCommonMethod
{
    /// <summary>
    /// 成功与否的枚举状态
    /// <auther>
    ///     <name>Kencery</name>
    ///     <date>2015-6-11</date>
    /// </auther>
    /// </summary>
    public enum Successed
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        Ok = 200,

        /// <summary>
        /// 查询结果为空
        /// </summary>
        Empty = 201,

        /// <summary>
        /// 操作失败
        /// </summary>
        Error = 201
    }

    /// <summary>
    /// 系统错误信息
    /// <auther>
    ///     <name>Kencery</name>
    ///     <date>2015-6-11</date>
    /// </auther>
    /// </summary>
    public enum System
    {
        /// <summary>
        /// 内部服务器错误
        /// </summary>
        InternalServerError = 1000,

        /// <summary>
        /// 请求参数错误
        /// </summary>
        InvalidRequest = 1002
    }

    /// <summary>
    /// 系统校验信息
    /// <auther>
    ///     <name>Kencery</name>
    ///     <date>2015-6-11</date>
    /// </auther>
    /// </summary>
    public enum Validate
    {
        /// <summary>
        /// 手机号不能为空
        /// </summary>
        EmptyTelephoneCode = 40000,

        /// <summary>
        /// Email不能为空
        /// </summary>
        EmptyEmailCode = 40001,

        /// <summary>
        /// 验证码不能为空
        /// </summary>
        EmptyAuthCode = 40002,

        /// <summary>
        /// 密码不能为空
        /// </summary>
        EmptyPasswordCode = 40003,

        /// <summary>
        /// 旧密码不能为空
        /// </summary>
        EmptyOldPasswordCode = 40004,

        /// <summary>
        /// 新密码不能为空
        /// </summary>
        EmptyNewPasswordCOde = 40005,

        /// <summary>
        /// 两次输入的密码不一致
        /// </summary>
        InconFormityPasswordCode = 40006,

        /// <summary>
        /// 密码设置错误
        /// </summary>
        SetPasswordError = 40007,

        /// <summary>
        /// 请输入正确的手机号
        /// </summary>
        InvalidTelephone = 50000,

        /// <summary>
        /// 请输入正确的邮箱
        /// </summary>
        InvalidEmail = 50001,

        /// <summary>
        /// 请输入正确的手机号或者Email地址
        /// </summary>
        InvalidEmailOrTelePhone = 50002,

        /// <summary>
        /// 无效的用户信息
        /// </summary>
        InvalidUserId = 50003,

        /// <summary>
        /// 未找到要删除的信息
        /// </summary>
        NotFoundDeleteInfo = 30000,

        /// <summary>
        /// 未找到要修改的信息
        /// </summary>
        NotFoundUpdateInfo = 30001,

        /// <summary>
        /// 手机号码已经存在
        /// </summary>
        TelephoneRegistered = 60000,

        /// <summary>
        /// Email已经存在
        /// </summary>
        EmailRegistered = 60001,

        /// <summary>
        /// 用户已经存在
        /// </summary>
        UserRegistered = 60002,

        /// <summary>
        /// 用户尚未登录
        /// </summary>
        UserNotLogin = 7000,

        /// <summary>
        /// 用户尚未注册
        /// </summary>
        UserUnregistered = 7001,

        /// <summary>
        /// 无权限登录
        /// </summary>
        NoPermissionToLogin = 7002,

        /// <summary>
        /// 验证码错误，请重新输入
        /// </summary>
        VerificationCodeErrorPleaseEnterAgain = 8000,

        /// <summary>
        /// 验证码发送失败
        /// </summary>
        SendAuthCodeFail = 8001,

        /// <summary>
        /// 验证码发送过于频繁，请稍后重试
        /// </summary>
        FrequentSendAuthCode = 8002
    }

    /// <summary>
    /// 第三方插件错误提示信息
    /// <auther>
    ///     <name>Kencery</name>
    ///     <date>2015-6-11</date>
    /// </auther>
    /// </summary>
    public enum PlugIn
    {
        /// <summary>
        /// 百度服务返回结果无效
        /// </summary>
        BaiDuServiceError = 10001,

        /// <summary>
        /// 阿里巴巴服务返回结果无效
        /// </summary>
        AliPayServiceError = 10002,

        /// <summary>
        /// 微信服务返回结果无效
        /// </summary>
        WechatServiceError = 10003
    }
}