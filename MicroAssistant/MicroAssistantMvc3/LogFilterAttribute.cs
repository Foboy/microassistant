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
        public string Message { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            string actionName = filterContext.ActionDescriptor.ActionName;


            var parameters = filterContext.ActionDescriptor.GetParameters();
            string strPar = JsonHelper.Serialize(parameters);
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


            string res = JsonHelper.Serialize(filterContext.Result);
            //filterContext.HttpContext.Response.Write("执行之前ActionName" + des + "<br />");
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            var parameters = filterContext.ActionDescriptor.GetParameters();
            foreach (var parameter in parameters)
            {
                if (parameter.ParameterType == typeof(string))
                {
                    //获取字符串参数原值
                   // var orginalValue = filterContext.[parameter.ParameterName] as string;
                    //使用过滤算法处理字符串
                    //var filteredValue = SensitiveWordsFilter.Instance.Filter(orginalValue);
                    ////将处理后值赋给参数
                    //filterContext.ActionParameters[parameter.ParameterName] = filteredValue;
                }
            }
            string res = JsonHelper.Serialize(filterContext.Result);
            //filterContext.HttpContext.Response.Write("Action执行之后" + Message + "<br />");
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);



            var parameters = filterContext.HttpContext.Request.Params;
            foreach (var parameter in parameters)
            {
                //if (filterContext.HttpContext.Request.Params[parameter.ToString()] == typeof(string))
                //{
                    //获取字符串参数原值
                var orginalValue = filterContext.HttpContext.Request.Params[parameter.ToString()];
                if (orginalValue.ToString() == "rrr")
                {
 
                }
                    //使用过滤算法处理字符串
                    //var filteredValue = SensitiveWordsFilter.Instance.Filter(orginalValue);
                    ////将处理后值赋给参数
                    //filterContext.ActionParameters[parameter.ParameterName] = filteredValue;
                //}
            }


            string res = JsonHelper.Serialize(filterContext.Result);


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

            //filterContext.HttpContext.Response.Write("返回Result之前" + Message + "<br />");
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {

            base.OnResultExecuted(filterContext);

            string res = JsonHelper.Serialize(filterContext.Result);

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
            //filterContext.HttpContext.Response.Write("返回Result之后" + Message + "<br />");
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