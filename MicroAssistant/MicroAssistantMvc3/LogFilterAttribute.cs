using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Reflection;
using System.Resources;
using MicroAssistant.Common;
using System.Text;
using System.IO;
using System.Web.Security;
using MicroAssistant.Cache;

namespace MicroAssistantMvc
{
    public class LogFilterAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            string actionName = filterContext.ActionDescriptor.ActionName;
            var parameters = filterContext.ActionDescriptor.GetParameters();
            foreach (var parameter in parameters)
            {
                if (parameter.ParameterType == typeof(string))
                {
                    //获取字符串参数原值
                    var orginalValue = filterContext.ActionParameters[parameter.ParameterName] as string;
                    //使用过滤算法处理字符串
                    //var filteredValue = SensitiveWordsFilter.Instance.Filter(orginalValue);
                    ////将处理后值赋给参数
                    //filterContext.ActionParameters[parameter.ParameterName] = filteredValue;
                }
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);


        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {

            base.OnResultExecuted(filterContext);

            string res = JsonHelper.Serialize(filterContext.Result);

       
        }



        private void AddActionlog(string actionname, string prams, string result, ControllerContext filterContext)
        {
            //filterContext.RequestContext.HttpContext.Request
            if (FormsAuthentication.CookiesSupported)
            {
                if (filterContext.RequestContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(
                          filterContext.RequestContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                        string currentToken = ticket.UserData;
                        int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(currentToken));
                    }
                    catch (Exception e)
                    {
                        // 票据解密失败

                    }
                }
            }
        }


        //获取描述信息
        protected string GetDescription(string strKey)
        {
            if (!String.IsNullOrEmpty(strKey))
            {
                object res = HttpContext.GetGlobalResourceObject("MethodDescription", strKey);
                if (res != null)
                    return res.ToString();
                else
                    return null;
            }
            else
            {
                return null;
            }
        }
    }
}