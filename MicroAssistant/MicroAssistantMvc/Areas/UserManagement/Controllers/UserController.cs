using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroAssistant.Cache;
using MicroAssistant.Common;
using MicroAssistant.DataAccess;
using MicroAssistant.DataStructure;
using MicroAssistant.Meta;
using MicroAssistantMvc.Controllers;

namespace MicroAssistantMvc.Areas.UserManagement.Controllers
{
    public class UserController : MicControllerBase
    {
        //
        // GET: /UserManagement/User

        #region IUserService 成员
       /// <summary>
        /// 企业注册
       /// </summary>
       /// <param name="entName">企业名称</param>
       /// <param name="account"></param>
       /// <param name="pwd"></param>
       /// <returns></returns>
        public JsonResult EntRegister(string entName, string account, string pwd)
        {
            var Res = new JsonResult();
            AdvancedResult<string> result = new AdvancedResult<string>();
            try
            {
                AdvancedResult<bool> dr = CheckUserAccout(account);
                if (dr.Data)
                {
                    result.Error = AppError.ERROR_PERSON_FOUND;
                    Res.Data = result;
                    Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    return Res;
                    
                }

                SysUser user = new SysUser();
                user.UserAccount = account;
                user.Pwd = pwd;
                user.Email = string.Empty;
                int i = SysUserAccessor.Instance.Insert(user);

                if (i>0)
                {
                    string token = SecurityHelper.GetToken(i.ToString());
                    CacheManagerFactory.GetMemoryManager().Set(token, i.ToString());
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = token;
                }
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
        /// 员工注册
        /// </summary>
        /// <param name="account">员工账号是邮箱格式</param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public JsonResult UserRegister(string account, string pwd, int entId)
        {
            var Res = new JsonResult();
            AdvancedResult<string> result = new AdvancedResult<string>();
            try
            {
                AdvancedResult<bool> dr = CheckUserAccout(account);
                if (dr.Data)
                {
                    result.Error = AppError.ERROR_PERSON_FOUND;
                    Res.Data = result;
                    Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    return Res;
                }

                SysUser user = new SysUser();
                user.UserAccount = account;
                user.Pwd = pwd;
                user.Email = account;
                user.EntId = entId;
                int i = SysUserAccessor.Instance.Insert(user);

                if (i > 0)
                {
                    string token = SecurityHelper.GetToken(i.ToString());
                    CacheManagerFactory.GetMemoryManager().Set(token, i.ToString());
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = token;
                }
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
        private AdvancedResult<bool> CheckUserAccout(string account)
        {
            AdvancedResult<bool> result = new AdvancedResult<bool>();
            SysUser user = null;
            user = SysUserAccessor.Instance.GetSysUserByAcount(account.Trim());

            try
            {
                if (user != null)
                {
                    result.Error = AppError.ERROR_PERSON_FOUND;
                    result.Data = true;
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        public JsonResult CheckAccout(string account)
        {
            var Res = new JsonResult();

            Res.Data = CheckUserAccout(account);
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }
        public JsonResult CheckLogin(string token)
        {
            var Res = new JsonResult();
            AdvancedResult<bool> result = new AdvancedResult<bool>();
            try
            {
                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Data = true;
                    result.Error = AppError.ERROR_SUCCESS;
                }
                else
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
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
        public JsonResult Login(string account, string pwd)
        {
            var Res = new JsonResult();
            AdvancedResult<string> result = new AdvancedResult<string>();
            try
            {
                SysUser user = null;
                user = SysUserAccessor.Instance.GetSysUserByAcountAndPwd(account.Trim(), pwd.Trim());
                if (user != null)
                {
                    if (user.IsEnable == 2)
                    {
                        result.Error = AppError.ERROR_USER_FORBID;
                    }
                    else
                    {
                        string token = SecurityHelper.GetToken(user.UserId.ToString());
                        CacheManagerFactory.GetMemoryManager().Set(token, user.UserId);
                        result.Error = AppError.ERROR_SUCCESS;
                        result.Data = token;
                    }
                }
                else
                {
                    result.Error = AppError.ERROR_LOGIN_FAILED;
                }
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
        public JsonResult Logout(string token)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();
            try
            {
                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    CacheManagerFactory.GetMemoryManager().Remove(token);
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
        /// 获取用户信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public JsonResult GetUserInfoByID(int userid)
        {
            var res = new JsonResult();
            AdvancedResult<SysUser> result = new AdvancedResult<SysUser>();
            try
            {
                if (userid > 0)
                {
                    SysUser user = SysUserAccessor.Instance.Get(userid);
                    if (user.IsEnable == 2)
                    {
                        result.Error = AppError.ERROR_USER_FORBID;
                    }
                    else
                    {
                        result.Error = AppError.ERROR_SUCCESS;
                        result.Data = user;
                    }
                }
                else
                {
                    result.Error = AppError.ERROR_FAILED;
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            res.Data = result;
            res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return res;
        }
        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public JsonResult GetUserInfo(string token)
        {
            var Res = new JsonResult();
            AdvancedResult<SysUser> result = new AdvancedResult<SysUser>();
            try
            {
                if (!CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
                else
                {
                    int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                    if (userid > 0)
                    {
                        SysUser user = SysUserAccessor.Instance.Get(userid);
                        if (user.IsEnable == 2)
                        {
                            result.Error = AppError.ERROR_USER_FORBID;
                        }
                        else
                        {
                            result.Error = AppError.ERROR_SUCCESS;
                            result.Data = user;
                        }
                    }
                    else
                    {
                        result.Error = AppError.ERROR_FAILED;
                    }
                }
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
        /// 编辑用户信息
        /// </summary>
        /// <param name="user">修改用户名，性别，密码，地址，qq，手机</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public JsonResult EditeUserInfo(SysUser user, string token)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();
            try
            {
                if (!CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
                else
                {
                    int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                    if (userid > 0)
                    {
                        //SysUser olduser = SysUserAccessor.Instance.Get(userid, string.Empty, string.Empty, StateType.Ignore);
                        //if (olduser.UserAccount != user.UserAccount)
                        //{
                        //    AdvancedResult<bool> dr = CheckAccout(user.UserAccount);
                        //    if (dr.Data)
                        //    {
                        //        result.Error = AppError.ERROR_PERSON_FOUND;
                        //        return result;
                        //    }
                        //}
                        //olduser.UserAccount = user.UserAccount;
                        //olduser.Sex = user.Sex;
                        //olduser.Pwd = user.Pwd;
                        //olduser.Province = user.Province;
                        //olduser.City = user.City;
                        //olduser.County = user.County;
                        //olduser.Street = user.Street;
                        //olduser.Mobile = user.Mobile;
                        //olduser.Qq = user.Qq;

                        //SysUserAccessor.Instance.Update(olduser);
                        result.Error = AppError.ERROR_SUCCESS;

                    }
                    else
                    {
                        result.Error = AppError.ERROR_FAILED;
                    }
                }
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
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetUserAllInfo()
        {
            var Res = new JsonResult();
            AdvancedResult<List<SysUser>> result = new AdvancedResult<List<SysUser>>();
            try
            {
                List<SysUser> users = SysUserAccessor.Instance.Search();
                result.Error = AppError.ERROR_SUCCESS;
                result.Data = users;
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


        public RespResult UpdatePwd(string oldpwd, string newpwd, string token)
        {
            throw new NotImplementedException();
        }

        public RespResult GetOldPwd(string token)
        {
            throw new NotImplementedException();
        }

        public RespResult IsUserExpire(string username)
        {
            throw new NotImplementedException();
        }

        public RespResult RenewUser(string username, DateTime endTime)
        {
            throw new NotImplementedException();
        }

        #endregion

       

    }
}
