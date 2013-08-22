using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MicroAssistant.Common;
using System.EnterpriseServices;
using MicroAssistant.Meta;

namespace MicroAssistant.WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IUserController”。
    [ServiceContract]
    public interface IUserController
    {
        [OperationContract]
        [Description("用户名检测")]
        AdvancedResult<bool> CheckAccout(string userName);

        [OperationContract]
        [Description("是否登陆")]
        AdvancedResult<bool> CheckLogin(string token);
        [OperationContract]
        [Description("获取当前用户信息")]
        AdvancedResult<SysUser> GetUserInfo(string token);
        [OperationContract]
        [Description("通过用户ID获取用户信息")]
        AdvancedResult<SysUser> GetUserInfoByID(int userid);
        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="user">修改用户名，性别，密码，地址，qq，手机</param>
        /// <param name="token"></param>
        /// <returns></returns>
        [OperationContract]
        [Description("编辑用户信息")]
        RespResult EditeUserInfo(SysUser user, string token);
        [OperationContract]
        [Description("登陆")]
        AdvancedResult<string> Login(string account, string pwd);
        [OperationContract]
        [Description("登出")]
        RespResult Logout(string token);
        [OperationContract]
        [Description("注册")]
        AdvancedResult<string> Register(string account, string pwd);
        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [Description("获取所有用户信息")]
        AdvancedResult<List<SysUser>> GetUserAllInfo();

        [OperationContract]
        [Description("修改密码")]
        RespResult UpdatePwd(string oldpwd, string newpwd, string token);

        RespResult GetOldPwd(string token);

        [OperationContract]
        [Description("检查用户账号是否过期")]
        RespResult IsUserExpire(string username);

        [OperationContract]
        [Description("用户续约")]
        RespResult RenewUser(string username, DateTime endTime);
    }
}
