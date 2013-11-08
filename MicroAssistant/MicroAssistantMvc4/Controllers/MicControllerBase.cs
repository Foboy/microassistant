using MicroAssistant.Cache;
using MicroAssistant.DataAccess;
using MicroAssistant.DataStructure;
using MicroAssistant.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MicroAssistantMvc.Controllers
{
    public class MicControllerBase : Controller
    {
        #region token
        private string currentToken;
        public string token {
            get {
                if (currentToken != null)
                    return currentToken;
                if (FormsAuthentication.CookiesSupported)
                {
                    if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                    {
                        try
                        {
                            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(
                              Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                            currentToken = ticket.UserData;
                        }
                        catch (Exception e)
                        {
                            // 票据解密失败
                            
                        }
                    }
                }
                else
                {
                      //客户端不支持Cookie
                }
                return currentToken;
            }
        }
        #endregion

        #region currentUser
        private SysUser currentUser;
        public SysUser CurrentUser
        {
            get
            {
                if (token == null)
                {
                    return null;
                }
                if (currentUser != null)
                    return currentUser;

                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                    currentUser = SysUserAccessor.Instance.Get(userid);
                    currentUser.userFuns = SysFunctionAccessor.Instance.SearchSysUserRolePermisson(userid);
                }
                else
                {
                    FormsAuthentication.SignOut();
                }
                return currentUser;
            }
        }
        /// <summary>
        /// 判断用户是否有此页面权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="functionCode"></param>
        /// <returns></returns>
        protected bool CheckUserFunction(string functionCode)
        {
            bool result = false;

           foreach(SysFunction sf in CurrentUser.userFuns)
           {
               if(sf.FunctionCode==functionCode)
               {
                   result=true;
               }
           }

            return result;
        }
        #endregion

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            ViewBag.LoginUser = CurrentUser;
            ViewBag.CurrentToken = token;
            base.OnAuthorization(filterContext);
        }
    
    }
}