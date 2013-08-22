using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.EnterpriseServices;
using MicroAssistant.Meta;
using MicroAssistant.Common;

namespace MicroAssistant.WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IPermissionController”。
    [ServiceContract]
    public interface IPermissionController
    {

        [OperationContract]
        [Description("加载所有页面权限列表")]
        AdvancedResult<List<SysFunction>> SearchAllFunctionList();



        [OperationContract]
        [Description("获取一个角色的页面权限列表")]
        AdvancedResult<List<SysRoleFunction>> SearchFunctionListByRole(int roleId);



        [OperationContract]
        [Description("修改一个角色的页面权限")]
        RespResult UpdateRoleFunction(int roleId, List<int> functionids);


        [OperationContract]
        [Description("修改一个用户的角色（清除原有角色）")]
        RespResult UpdateUserRole(int userId, List<int> roleIds);



        [OperationContract]
        [Description("添加用户到某个角色(不删除原有角色)")]
        RespResult AddRolesToUser(int userId, List<int> roleIds);



        [OperationContract]
        [Description("为角色添加用户")]
        RespResult AddUsersToRole(int roleId, List<int> userIds);



        [OperationContract]
        [Description("移除用户角色")]
        RespResult RemoveUserRole(int userId, List<int> roleIds);


        [OperationContract]
        [Description("获取某个用户的角色信息列表")]
        AdvancedResult<List<SysRoleUser>> SearchRolesByUserID(int userId);



        [OperationContract]
        [Description("加载某个用户所有权限列表（包括所有级）")]
        AdvancedResult<List<SysFunction>> SearchUserAllFunction(int userId);


        [OperationContract]
        [Description("判断用户是否有此页面权限")]
        RespResult CheckUserFunction(int userId, string functionCode);
    }
}
