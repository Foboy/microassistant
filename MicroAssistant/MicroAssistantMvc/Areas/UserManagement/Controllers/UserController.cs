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

namespace MicroAssistantMvc.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /MicUser/

        public ActionResult Index()
        {
            return View();
        }

        #region IUserService 成员
        public AdvancedResult<string> Register(string account, string pwd)
        {
            AdvancedResult<string> result = new AdvancedResult<string>();
            try
            {
                AdvancedResult<bool> dr = CheckAccout(account);
                if (dr.Data)
                {
                    result.Error = AppError.ERROR_PERSON_FOUND;
                    return result;
                }

                SysUser user = new SysUser();
                user.UserName = account;
                user.Pwd = pwd;
                user.Email = string.Empty;
                int i = SysUserAccessor.Instance.Insert(user);

                if (i>0)
                {
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = SecurityHelper.GetToken(i.ToString());
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return result;
        }
        public AdvancedResult<bool> CheckAccout(string account)
        {
            AdvancedResult<bool> result = new AdvancedResult<bool>();
            SysUser user = null;
            //user = SysUserAccessor.Instance.Get(0, account.Trim(), string.Empty, StateType.Ignore);

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
        public AdvancedResult<bool> CheckLogin(string token)
        {
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
            return result;
        }
        public AdvancedResult<string> Login(string account, string pwd)
        {
            AdvancedResult<string> result = new AdvancedResult<string>();
            try
            {
                SysUser user = null;
                //user = SysUserAccessor.Instance.Get(0, account.Trim(), pwd.Trim(), StateType.Active);
                if (user != null)
                {
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = SecurityHelper.GetToken(user.UserId.ToString());
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
            return result;
        }
        public RespResult Logout(string token)
        {
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
            return result;
        }

        public JsonResult GetUserInfoByID(int userid)
        {
            var res = new JsonResult();
            AdvancedResult<SysUser> result = new AdvancedResult<SysUser>();
            try
            {
                if (userid > 0)
                {
                    SysUser user = SysUserAccessor.Instance.Get(userid);
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = user;
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
        public AdvancedResult<SysUser> GetUserInfo(string token)
        {
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
                        //SysUser user = SysUserAccessor.Instance.Get(userid, string.Empty, string.Empty, StateType.Ignore);
                        result.Error = AppError.ERROR_SUCCESS;
                        //result.Data = user;
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
            return result;
        }


        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="user">修改用户名，性别，密码，地址，qq，手机</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public RespResult EditeUserInfo(SysUser user, string token)
        {
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
            return result;
        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        public AdvancedResult<List<SysUser>> GetUserAllInfo()
        {
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
            return result;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool Insert(SysUser user)
        {
            AdvancedResult<string> result = new AdvancedResult<string>();
            try
            {
                AdvancedResult<bool> dr = CheckAccout(user.UserName);
                if (dr.Data)
                {
                    result.Error = AppError.ERROR_PERSON_FOUND;
                    return false;
                }

                SysUser newuser = new SysUser();
                newuser.UserName = user.UserName;
                newuser.Pwd = user.Pwd;
                newuser.Email = user.Email;
                int i = SysUserAccessor.Instance.Insert(newuser);

                if (i>0)
                {
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = SecurityHelper.GetToken(i.ToString());
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            return true;
        }

        /// <summary>
        /// 删除数据
        /// <param name="es">数据实体对象数组</param>
        /// <returns></returns>
        /// </summary>
        public bool Delete(int UserId)
        {
            return false;

        }

        /// <summary>
        /// 修改指定的数据
        /// <param name="e">修改后的数据实体对象</param>
        /// <para>数据对应的主键必须在实例中设置</para>
        /// </summary>
        public void Update(SysUser e)
        {

        }

        /// <summary>
        /// 根据条件分页获取指定数据
        /// <param name="pageIndex">当前页</param>
        /// <para>索引从0开始</para>
        /// <param name="pageSize">每页记录条数</param>
        /// <para>记录数必须大于0</para>
        /// </summary>
        public PageEntity<SysUser> Search(Int32 UserId, String UserName, String Pwd, String Mobile, String Email, DateTime CreateTime, DateTime EndTime, Int32 FatherId, int pageIndex, int pageSize)
        {

            return null;
        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        public List<SysUser> Search()
        {
            return null;
        }

        /// <summary>
        /// 获取指定记录
        /// <param name="id">Id值</param>
        /// </summary>
        public SysUser Get(int UserId)
        {

            return null;
        }

    }
}
