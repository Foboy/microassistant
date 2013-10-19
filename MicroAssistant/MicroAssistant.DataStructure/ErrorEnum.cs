using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MicroAssistant.DataStructure
{
    public enum AppError
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        [Description("成功")]
        ERROR_SUCCESS = 0,

        /// <summary>
        /// 操作失败
        /// </summary>
        [Description("失败")]
        ERROR_FAILED = 1,

        /// <summary>
        /// 平台账号没有和任何社交账号绑定
        /// </summary>
        [Description("提示:平台账号没有和任何社交账号绑定")]
        ERROR_NOT_BINDWITH_SOCIALUSER =2,


        /// <summary>
        /// 此社交账号没有和平台账号绑定
        /// </summary>
        [Description("提示:此社交账号没有和平台账号绑定")]
        ERROR_NOT_BINDWITH_PLATUSER = 3,

        /// <summary>
        /// 登陆失败:用户名或者密码错误
        /// </summary>
        [Description("错误:用户名不存在或者密码不正确")]
        ERROR_LOGIN_FAILED= 4,


        /// <summary>
        /// 用户不存在
        /// </summary>
        [Description("错误:不存在该用户")]
        ERROR_PERSON_NOT_FOUND= 5,


        /// <summary>
        /// 系统中不存在该邮件地址
        /// </summary>
        [Description("错误:系统中不存在该邮件地址")]
        ERROR_MAIL_NOT_EXIST = 6,



        /// <summary>
        /// 邮件已被占用
        /// </summary>
        [Description("错误:邮件已被占用,请使用其他邮箱地址")]
        ERROR_DUP_MAIL= 7,


        /// <summary>
        /// 密码找回成功
        /// </summary>
        [Description("成功:密码找回成功,请查收邮件")]
        ERROR_PWD_GET_SUCCESS = 8, 

        [Description("错误:无效参数")]
        ERROR_INVALID_PARAMETER =9,
        /// <summary>
        /// 账号已存在
        /// </summary>
        [Description("错误:存在该用户")]
        ERROR_PERSON_FOUND = 10,
        /// <summary>
        /// 该用户未登录
        /// </summary>
        [Description("错误:该用户未登录")]
        ERROR_PERSON_NOT_LOGIN = 11,
        /// <summary>
        /// 成长记录名不能为空
        /// </summary>
        [Description("错误:账号已禁用")]
        ERROR_USER_FORBID = 12
        
    }
}
