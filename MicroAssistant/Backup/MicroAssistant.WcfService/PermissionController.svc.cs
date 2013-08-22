using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MicroAssistant.Meta;
using MicroAssistant.Common;
using MicroAssistant.DataStructure;
using MicroAssistant.DataAccess;

namespace MicroAssistant.WcfService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“PermissionController”。
    public class PermissionController : IPermissionController
    {
        #region IPermissionController 成员
        /// <summary>
        /// 获取所有权限列表
        /// </summary>
        /// <returns></returns>
        public AdvancedResult<List<SysFunction>> SearchAllFunctionList()
        {
            AdvancedResult<List<SysFunction>> result = new AdvancedResult<List<SysFunction>>();
            try
            {
                List<SysFunction> functions = SysFunctionAccessor.Instance.Search();
                result.Error = AppError.ERROR_SUCCESS;
                result.Data = functions;
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        /// <summary>
        /// 获取一个角色的页面权限列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public AdvancedResult<List<SysRoleFunction>> SearchFunctionListByRole(int roleId)
        {
            AdvancedResult<List<SysRoleFunction>> result = new AdvancedResult<List<SysRoleFunction>>();
            try
            {
                List<SysRoleFunction> list = new List<SysRoleFunction>();
                list = SysRoleFunctionAccessor.Instance.Search(0, roleId, 0, 0, int.MaxValue).Items;
                result.Error = AppError.ERROR_SUCCESS;
                result.Data = list;
                
            }
            catch (Exception e)
            {

                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        /// <summary>
        /// 修改一个角色的页面权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="functionids"></param>
        /// <returns></returns>
        public RespResult UpdateRoleFunction(int roleId, List<int> functionids)
        {
            throw new NotImplementedException(); ;
        }
        /// <summary>
        /// 修改一个用户的角色（清除原有角色）
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public RespResult UpdateUserRole(int userId, List<int> roleIds)
        {
            RespResult result = new RespResult();
            try
            {
                if (!SysRoleAccessor.Instance.Delete(userId))
                {
                    result.Error = AppError.ERROR_FAILED;
                }
                else
                {
                    foreach (int roleId in roleIds)
                    {
                        SysRoleUser item = new SysRoleUser();
                        item.UserId = userId;
                        item.RoleId = roleId;
                        SysRoleUserAccessor.Instance.Insert(item);
                    }
                }
                result.Error = AppError.ERROR_SUCCESS;
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        /// <summary>
        /// 添加用户到某个角色(不删除原有角色)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public RespResult AddRolesToUser(int userId, List<int> roleIds)
        {
            RespResult result = new RespResult();
            try
            {
                foreach (int roleId in roleIds)
                {
                    SysRoleUser item = new SysRoleUser();
                    item.UserId = userId;
                    item.RoleId = roleId;
                    SysRoleUserAccessor.Instance.Insert(item);
                }
                result.Error = AppError.ERROR_SUCCESS;
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        /// <summary>
        /// 为角色添加用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public RespResult AddUsersToRole(int roleId, List<int> userIds)
        {
            RespResult result = new RespResult();
            try
            {

                foreach (int userId in userIds)
                {
                    SysRoleUser item = new SysRoleUser();
                    item.UserId = userId;
                    item.RoleId = roleId;
                    SysRoleUserAccessor.Instance.Insert(item);
                }
                result.Error = AppError.ERROR_SUCCESS;
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        /// <summary>
        /// 移除用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public RespResult RemoveUserRole(int userId, List<int> roleIds)
        {
            RespResult result = new RespResult();
            try
            {
                foreach (int roleId in roleIds)
                {
                    SysRoleUserAccessor.Instance.Delete(userId,roleId);
                }
                result.Error = AppError.ERROR_SUCCESS;
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        /// <summary>
        /// 获取某个用户的角色信息列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public AdvancedResult<List<SysRoleUser>> SearchRolesByUserID(int userId)
        {
            AdvancedResult<List<SysRoleUser>> result = new AdvancedResult<List<SysRoleUser>>();
            try
            {
                List<SysRoleUser> list = new List<SysRoleUser>();
                list = SysRoleUserAccessor.Instance.Search(0, 0, userId, 0, int.MaxValue).Items;
                result.Error = AppError.ERROR_SUCCESS;
                result.Data = list;
            }
            catch (Exception e)
            {

                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        /// <summary>
        /// 加载某个用户所有权限列表（包括所有级）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public AdvancedResult<List<SysFunction>> SearchUserAllFunction(int userId)
        {
            AdvancedResult<List<SysFunction>> result = new AdvancedResult<List<SysFunction>>();
            try
            {
                List<SysFunction> list = new List<SysFunction>();
                list = SysFunctionAccessor.Instance.SearchSysUserRolePermisson(userId);
                result.Error = AppError.ERROR_SUCCESS;
                result.Data = list;
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        /// <summary>
        /// 判断用户是否有此页面权限(暂未实现)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="functionCode"></param>
        /// <returns></returns>
        public RespResult CheckUserFunction(int userId, string functionCode)
        {
            RespResult result = new RespResult();
            try
            {
               
                result.Error = AppError.ERROR_SUCCESS;
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }

        #endregion
    }
}
