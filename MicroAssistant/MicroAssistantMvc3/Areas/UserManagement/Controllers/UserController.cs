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
using System.Web.Security;
using System.Xml;
using System.IO;

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
                SysUser lastuser = new SysUser();
                string EntCode = GetEntCode(GenCode(6));
                SysUser user = new SysUser();
                user.UserName = entName;
                user.UserAccount = account;
                user.Pwd = SecurityHelper.MD5(pwd);
                user.Email = account;
                user.CreateTime = DateTime.Now;
                user.EndTime = DateTime.Now.AddDays(90);
                user.IsEnable = 1;
                user.Type = 2;
                user.EntCode = EntCode;
                int i = SysUserAccessor.Instance.Insert(user);


                if (i>0)
                {
                    SysUserAccessor.Instance.UpdateUserEntId(i, i);//更新所属企业ID
                    //初始化权限
                    initEntRole(i);
                    string token = SecurityHelper.GetToken(i.ToString());
                    CacheManagerFactory.GetMemoryManager().Set(token, i.ToString());
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = token;
                    WriteAuthCookie(user.UserName, token);

                    AddUserTimeMachine(i, 2, 0);//添加时光轴
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
        public JsonResult UserRegister(string username,string account, string pwd, string entCode)
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
                
                SysUser entUser = new SysUser();
                SysUser user = new SysUser();
                if (!string.IsNullOrEmpty(entCode))
                {
                    entUser = SysUserAccessor.Instance.GetEntUserByEntCode(entCode.ToUpper().Trim());
                    if (entUser != null)
                    {
                        user.EntId = entUser.EntId;
                        user.EntCode = entCode.ToUpper();
                    }
                    else
                    {
                        result.Error = AppError.ERROR_FAILED;
                        Res.Data = result;
                        Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                        return Res;
                    }
                }
                user.UserAccount = account;
                user.UserName = username;
                user.Pwd = SecurityHelper.MD5(pwd);
                user.Email = account;
                user.CreateTime = DateTime.Now;
                user.EndTime = DateTime.Now.AddDays(90);
                user.IsEnable = 1;
                user.Type = 1;
                int i = SysUserAccessor.Instance.Insert(user);

                if (i > 0)
                {
                    string token = SecurityHelper.GetToken(i.ToString());
                    CacheManagerFactory.GetMemoryManager().Set(token, i.ToString());
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = token;
                    WriteAuthCookie(user.UserName, token);
                    AddUserTimeMachine(i, 1, 0);
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
        public JsonResult CheckLogin()
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
                user = SysUserAccessor.Instance.GetSysUserByAcountAndPwd(account.Trim(), SecurityHelper.MD5(pwd.Trim()));
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
                        WriteAuthCookie(user.UserName, token);
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
        public JsonResult Logout()
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
        /// 获取用户信息||企业信息
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
                        user.Pwd = string.Empty;
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
        public JsonResult GetUserInfo()
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

                    result.Error = AppError.ERROR_SUCCESS;
                    CurrentUser.Pwd = string.Empty;
                    result.Data = CurrentUser;
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
        public JsonResult EditeUserInfo(string username,int sex,int age)
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
                        SysUser olduser = SysUserAccessor.Instance.Get(userid);
                        olduser.UserName = username;
                        olduser.Sex = sex;
                        olduser.Birthday = DateTime.Now.AddYears(-age);
                        //olduser.Pwd = user.Pwd;
                        //olduser.Province = user.Province;
                        //olduser.City = user.City;
                        //olduser.County = user.County;
                        //olduser.Street = user.Street;
                        //olduser.Mobile = user.Mobile;
                        //olduser.Qq = user.Qq;


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
                        
                        //olduser.Pwd = user.Pwd;
                        //olduser.Province = user.Province;
                        //olduser.City = user.City;
                        //olduser.County = user.County;
                        //olduser.Street = user.Street;
                        //olduser.Mobile = user.Mobile;
                        //olduser.Qq = user.Qq;

                        SysUserAccessor.Instance.Update(olduser);
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
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="oldpwd"></param>
        /// <param name="newpwd"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public JsonResult UpdatePwd(string oldpwd, string newpwd)
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
                        SysUser olduser = SysUserAccessor.Instance.Get(CurrentUser.UserId);

                        if (SecurityHelper.MD5(oldpwd) != olduser.Pwd)
                        {
                            result.Error = AppError.ERROR_FAILED;
                        }
                        else
                        {
                            olduser.Pwd = SecurityHelper.MD5(newpwd);

                            SysUserAccessor.Instance.Update(olduser);
                            result.Error = AppError.ERROR_SUCCESS;
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
        /// 修改用户邮箱
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public JsonResult UpdateEmail(string email, string pwd)
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
                    SysUser olduser = SysUserAccessor.Instance.Get(CurrentUser.UserId);

                    if (SecurityHelper.MD5(pwd) != olduser.Pwd)
                    {
                        result.Error = AppError.ERROR_FAILED;
                    }
                    else if (!CheckUserAccout(email).Data)
                    {
                        result.Error = AppError.ERROR_FAILED;
                    }else
                    {
                        olduser.Email = email;
                        SysUserAccessor.Instance.Update(olduser);
                        result.Error = AppError.ERROR_SUCCESS;
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

        //用户时间轴
        public JsonResult SearchUserTimeMachine()
        {
            var Res = new JsonResult();
            AdvancedResult<List<SysUserTimemachine>> result = new AdvancedResult<List<SysUserTimemachine>>();
            try
            {
                if (!CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
                else
                {

                    result.Data = SysUserTimemachineAccessor.Instance.Search(CurrentUser.UserId);
                    result.Error = AppError.ERROR_SUCCESS;
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
     

        //关联企业
        public JsonResult EditeUserEntCode(string username,string entCode)
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
                        SysUser entUser = new SysUser();
                        if (!string.IsNullOrEmpty(entCode.Trim()))
                        {
                            entUser = SysUserAccessor.Instance.GetEntUserByEntCode(entCode.Trim());
                            if (entUser == null)
                            {
                                result.Error = AppError.ERROR_PERSON_NOT_FOUND;
                                Res.Data = result;
                                Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                                return Res;
                            }
                        }

                        SysUser olduser = SysUserAccessor.Instance.Get(userid);
                        olduser.EntId = entUser.EntId;
                        olduser.EntCode = entUser.EntCode;
                        olduser.UserName = username;

                        SysUserAccessor.Instance.Update(olduser);
                        result.Error = AppError.ERROR_SUCCESS;
                        AddUserTimeMachine(userid, 3, 0);
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
        //关联用户头像
        public JsonResult EditeUserHeadImg(int picid)
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
                        SysUser olduser = SysUserAccessor.Instance.Get(userid);
                        olduser.PicId = picid;


                        SysUserAccessor.Instance.Update(olduser);
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
        /// 普通用户绑定企业CODE
        /// </summary>
        /// <param name="username"></param>
        /// <param name="entCode"></param>
        /// <returns></returns>
        public JsonResult EditeCurrentEntCode(string entCode)
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
                        SysUser entUser = new SysUser();
                        if (!string.IsNullOrEmpty(entCode.Trim()))
                        {
                            entUser = SysUserAccessor.Instance.GetEntUserByEntCode(entCode.Trim());
                            if (entUser != null)
                            {
                                result.Error = AppError.ERROR_ENTCODE_EXIST;
                                Res.Data = result;
                                Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                                return Res;
                            }
                        }

                        SysUser olduser = SysUserAccessor.Instance.Get(userid);
                        //olduser.EntCode = entUser.EntCode;
                        olduser.EntCode = entCode;

                        SysUserAccessor.Instance.Update(olduser);
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
        /// 管理员修改所管理企业CODE
        /// </summary>
        /// <param name="username"></param>
        /// <param name="entCode"></param>
        /// <returns></returns>
        public JsonResult AdminEditEntCode(string entCode,int entId)
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
                    if (!CheckUserFunction(25))
                    {
                        result.Error = AppError.ERROR_PERMISSION_FORBID;
                        Res.Data = result;
                        Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                        return Res;
                    }

                    int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                    if (userid > 0)
                    {
                        SysUser entUser = new SysUser();
                        if (!string.IsNullOrEmpty(entCode.Trim()))
                        {
                            entUser = SysUserAccessor.Instance.GetEntUserByEntCode(entCode.Trim());
                            if (entUser != null)
                            {
                                result.Error = AppError.ERROR_ENTCODE_EXIST;
                                Res.Data = result;
                                Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                                return Res;
                            }
                        }

                        SysUser olduser = SysUserAccessor.Instance.Get(entId);
                        //olduser.EntCode = entUser.EntCode;
                        olduser.EntCode = entCode;

                        SysUserAccessor.Instance.Update(olduser);
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

        public JsonResult AdminEditEntName(string entName, int entId)
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
                    if (!CheckUserFunction(25))
                    {
                        result.Error = AppError.ERROR_PERMISSION_FORBID;
                        Res.Data = result;
                        Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                        return Res;
                    }
                    int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                    if (userid > 0)
                    {
                      
                        SysUser olduser = SysUserAccessor.Instance.Get(entId);
                        //olduser.EntCode = entUser.EntCode;
                        olduser.UserName = entName;

                        SysUserAccessor.Instance.Update(olduser);
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

        #region 找回密码

        private string GetUserTokenByEmail(string email)
        {
            string token = string.Empty;
            try
            {
                SysUser user = SysUserAccessor.Instance.GetSysUserByAcount(email);
                 token = SecurityHelper.GetToken(user.UserId.ToString());
                CacheManagerFactory.GetMemoryManager().Set(token, user.UserId.ToString(), new TimeSpan(0, 30, 0));

            }
            catch (Exception e)
            {
   
            }

            return token;
        }

        public JsonResult UpdateNewPwd(string userToken, string newPwd)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();
            try
            {
                if (!CacheManagerFactory.GetMemoryManager().Contains(userToken))
                {
                    result.Error = AppError.ERROR_FAILED;
                }
                else
                {
                    int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(userToken));
                    if (userid > 0)
                    {
                        SysUser olduser = SysUserAccessor.Instance.Get(userid);
                        olduser.Pwd = SecurityHelper.MD5(newPwd);

                        SysUserAccessor.Instance.Update(olduser);
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

        #endregion

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
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="EmailBody">邮件内容</param>
        /// <returns></returns>
        public JsonResult SendEamil(string EmailBody, string Email)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();
            result.Error = AppError.ERROR_SUCCESS;
            try
            {
                EmailHelper.SendEamil(EmailBody,Email);
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
        #region 私有方法

        private void WriteAuthCookie(string username, string token)
        {
            base.currentToken = token;

            DateTime issueDate = DateTime.Now;
            DateTime expiration = issueDate.AddDays(1);
            string userData = token;
            bool isPersistent = true;

            FormsAuthenticationTicket ticket;
            ticket = new FormsAuthenticationTicket(
                1, username, issueDate, expiration, isPersistent, userData);

            string value = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = FormsAuthentication.GetAuthCookie(username, isPersistent);
            cookie.Value = value;
            cookie.Expires = expiration;
            Response.Cookies.Set(cookie);

        }

        protected static XmlDocument roleXmlModel = null;
        //初始化企业权限(后期改为异步)
        private void initEntRole(int entId)
        {
            try
            {
            if (roleXmlModel == null)
            {
                string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "Config/Role.xml";
                if (System.IO.File.Exists(filePath) == false) throw new FileNotFoundException("默认权限模板配置文件没有找到", filePath);
                string roleConfig = System.IO.File.ReadAllText(filePath);
                roleXmlModel = new XmlDocument();
                roleXmlModel.LoadXml(roleConfig);
            }
            int newadminId = 0;

                    foreach (XmlNode rolenode in roleXmlModel.SelectNodes("//role"))
                    {
                        string rolenodename = rolenode.Attributes["name"].Value;
                           //添加企业角色
                            SysRole erole=new SysRole();
                            erole.EntId = entId;
                            erole.FatherId = 0;
                            erole.RoleName = rolenodename;
                            int eroleid = SysRoleAccessor.Instance.Insert(erole);
                            if (rolenodename == "管理员")
                            {
                                newadminId = eroleid;
                            }
                        foreach (XmlNode fathernode in rolenode.ChildNodes)
                        {
                            string fnname = fathernode.Attributes["name"].Value;
                            string fnid = fathernode.Attributes["id"].Value;
                            //添加企业角色权限
                            SysRoleFunction frf = new SysRoleFunction();
                            frf.EntId = entId;
                            frf.RoleId = eroleid;
                            frf.FunctionId = Convert.ToInt32(fnid);
                            SysRoleFunctionAccessor.Instance.Insert(frf);
                            foreach (XmlNode sonnode in fathernode.ChildNodes)
                            {
                                string sname = sonnode.Attributes["name"].Value;
                                string sid = sonnode.Attributes["id"].Value;
                                SysRoleFunction srf = new SysRoleFunction();
                                srf.EntId = entId;
                                srf.RoleId = eroleid;
                                srf.FunctionId = Convert.ToInt32(sid);
                                SysRoleFunctionAccessor.Instance.Insert(srf);
                            }
                        }
                    }
                SysRoleUser ru=new SysRoleUser();
                ru.EntId=entId;
                ru.UserId=entId;
                ru.RoleId=newadminId;
                SysRoleUserAccessor.Instance.Insert(ru);

            }
            catch
            {
                throw new InvalidDataException("权限模板配置文件内容不正确");
            }
        }
        private string GetEntCode(string entCode)
        {
            if (SysUserAccessor.Instance.GetEntUserByEntCode(entCode) != null)
            {
                entCode = GetEntCode(GenCode(6));
            }
            return entCode;
        }

        #endregion

        #endregion



    }
}
