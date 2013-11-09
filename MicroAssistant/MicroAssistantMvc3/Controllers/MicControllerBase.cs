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
    public class MicControllerBase :Controller,IController
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
            List<SysFunction> fs = CurrentUser.userFuns;

           foreach(SysFunction sf in fs)
           {
               if(sf.FunctionCode==functionCode)
               {
                   result=true;
               }
           }

            return result;
        }
        #endregion

        #region common

        /// <summary>
        /// 获取当前年月季度的第一天
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected DateTime GetStartTime(TimeType type)
        {
            DateTime now = DateTime.Now;
            switch (type)
            {
                case TimeType.month:
                    now = new DateTime(now.Year, now.Month, 1); //本月第一天

                    break;
                case TimeType.quarter:
                    now = now.AddMonths(0 - (now.Month - 1) % 3).AddDays(1 - now.Day);  //本季度初

                    break;
                case TimeType.year:
                    now = new DateTime(now.Year, 1, 1); //本月第一天
                    break;
                default:
                    break;
            }
            return now;
        }
        /// <summary>
        /// 获取当前年份的第二年第一天，获取当前月份的第二个月的第一天,获取当前季度下个季度的最后一天
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected DateTime GetEndTime(TimeType type)
        {
            DateTime now = DateTime.Now;
            switch (type)
            {
                case TimeType.month:
                    DateTime d1 = new DateTime(now.Year, now.Month, 1); //本月第一天
                    now = d1.AddMonths(1);
                    break;
                case TimeType.quarter:
                    DateTime startQuarter = now.AddMonths(0 - (now.Month - 1) % 3).AddDays(1 - now.Day);
                    now = startQuarter.AddMonths(4);  //本季度末
                    break;
                case TimeType.year:
                    DateTime d3 = new DateTime(now.Year, 1, 1); //本月第一天
                    now = d3.AddYears(1);
                    break;
                default:
                    break;
            }
            return now;
        }

#endregion common

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            ViewBag.LoginUser = CurrentUser;
            ViewBag.CurrentToken = token;
            base.OnAuthorization(filterContext);
        }


    }
}