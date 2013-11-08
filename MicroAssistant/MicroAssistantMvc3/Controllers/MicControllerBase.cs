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

namespace MicroAssistantMvc3.Controllers
{
    public class MicControllerBase : Controller
    {
        #region token
        private string currentToken;
        public string token
        {
            get
            {
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
                }
                else
                {
                    FormsAuthentication.SignOut();
                }
                return currentUser;
            }
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