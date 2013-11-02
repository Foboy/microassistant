using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroAssistant.Common;
using MicroAssistant.DataAccess;
using MicroAssistant.DataStructure;
using MicroAssistant.Meta;
using MicroAssistantMvc.Controllers;

namespace MicroAssistantMvc.Areas.SystemManagement.Controllers
{
    public class PermissionController : MicControllerBase
    {
        

        //
        // GET: /SystemManagement/Permission/
        #region IPermissionController 成员
        /// <summary>
        /// 获取所有权限列表
        /// </summary>
        /// <returns></returns>
        public JsonResult SearchAllFunctionList()
        {
            var Res = new JsonResult();
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
            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }
        /// <summary>
        /// 获取一个角色的页面权限列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public JsonResult  SearchFunctionListByRole(int roleId)
        {
            var Res = new JsonResult();
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
            Res.Data = result;
            Res.JsonRequestBehavior=JsonRequestBehavior.AllowGet;
            return Res;
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
        public JsonResult UpdateUserRole(int userId, List<int> roleIds)
        {
            var Res = new JsonResult();
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
            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }
        /// <summary>
        /// 添加用户到某个角色(不删除原有角色)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public JsonResult AddRolesToUser(int userId, List<int> roleIds)
        {
            var Res = new JsonResult();
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
                result.Id = userId;
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
               
            }
            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }
        /// <summary>
        /// 为角色添加用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public JsonResult AddUsersToRole(int roleId, List<int> userIds)
        {
            var Res = new JsonResult();
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
            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }
        /// <summary>
        /// 移除用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public JsonResult RemoveUserRole(int userId, List<int> roleIds)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();
            try
            {
                foreach (int roleId in roleIds)
                {
                    SysRoleUserAccessor.Instance.Delete(userId, roleId);
                }
                result.Error = AppError.ERROR_SUCCESS;
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }
        /// <summary>
        /// 获取某个用户的角色信息列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public JsonResult SearchRolesByUserID(int userId)
        {
            var Res = new JsonResult();
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
            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }
        /// <summary>
        /// 加载某个用户所有权限列表（包括所有级）
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public JsonResult SearchUserAllFunction(int userId)
        {
            var Res = new JsonResult();
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
            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }
        /// <summary>
        /// 判断用户是否有此页面权限(暂未实现)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="functionCode"></param>
        /// <returns></returns>
        public JsonResult CheckUserFunction(int userId, string functionCode)
        {
            var Res = new JsonResult();
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
            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }

        //--------------------------------

//        通过token获取企业所有权限 返回 权限列表（权限ID，全部（人数））
//通过token添加部门 返回 true/false
//通过权限ID获取用户信息 返回 用户列表（账号，姓名，sex，头像，手机号，入职时间，权限ID）
//根据用户ID修改用户权限




        #endregion





    }
}
