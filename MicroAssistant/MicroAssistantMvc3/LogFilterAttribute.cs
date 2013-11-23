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
using System.Threading.Tasks;
using MicroAssistant.Meta;
using MicroAssistant.DataAccess;

namespace MicroAssistantMvc
{
    public class LogFilterAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            string actionName = filterContext.ActionDescriptor.ActionName;
            string parms = string.Empty;
            var parameters = filterContext.ActionDescriptor.GetParameters();
            foreach (var parameter in parameters)
            {
                //if (parameter.ParameterType == typeof(string))
                //{
                    //获取字符串参数原值
                    string orginalValue = Convert.ToString(filterContext.ActionParameters[parameter.ParameterName]);
                    parms += parameter.ParameterName + ":" + orginalValue + ";";
                    //使用过滤算法处理字符串
                    //var filteredValue = SensitiveWordsFilter.Instance.Filter(orginalValue);
                    ////将处理后值赋给参数
                    //filterContext.ActionParameters[parameter.ParameterName] = filteredValue;
                //}
            }

            Task tt = new Task(() => {
                AddActionlog(actionName, parms, string.Empty, filterContext);
            });
            tt.Start();
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

            Task tt = new Task(() =>
            {
                AddActionlog(filterContext.RequestContext.HttpContext.Request.Path, string.Empty, res, filterContext);
            });
            tt.Start();
        }



        private void AddActionlog(string actionname, string prams, string result, ControllerContext filterContext)
        {
            int userid = 0;
            if (FormsAuthentication.CookiesSupported)
            {
                if (filterContext.RequestContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(
                          filterContext.RequestContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                        string currentToken = ticket.UserData;
                        userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(currentToken));
                    }
                    catch (Exception e)
                    {
                        // 票据解密失败

                    }
                }
            }

            SysLog log = new SysLog();
            log.Action = actionname;
            log.AddTime = DateTime.Now;
            log.Parameter = prams;
            log.UserId = userid;
            log.Result = result;
            SysLogAccessor.Instance.Insert(log);
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