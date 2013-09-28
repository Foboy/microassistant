using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Reflection;
using System.Resources;
using MicroAssistant.Common;

namespace MicroAssistantMvc.Filters
{
    public class LogFilterAttribute : ActionFilterAttribute
    {
        public string Message { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            string actionName = filterContext.ActionDescriptor.ActionName;
           string des =GetDescription(actionName);

<<<<<<< HEAD
          // filterContext.HttpContext.Response.Write("执行之前ActionName" + des + "<br />");
=======
           filterContext.HttpContext.Response.Write("执行之前ActionName" + des + "<br />");
>>>>>>> ff89a5a760e31eaaf33ef9d1b03ea6b4d8720970
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
<<<<<<< HEAD
          //  filterContext.HttpContext.Response.Write("Action执行之后" + Message + "<br />");
=======
            filterContext.HttpContext.Response.Write("Action执行之后" + Message + "<br />");
>>>>>>> ff89a5a760e31eaaf33ef9d1b03ea6b4d8720970
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
<<<<<<< HEAD
           // filterContext.HttpContext.Response.Write("返回Result之前" + Message + "<br />");
=======
            filterContext.HttpContext.Response.Write("返回Result之前" + Message + "<br />");
>>>>>>> ff89a5a760e31eaaf33ef9d1b03ea6b4d8720970
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            
            base.OnResultExecuted(filterContext);

            string requestpath = filterContext.HttpContext.Request.Path;
            string res = JsonHelper.Serialize(filterContext.Result);

<<<<<<< HEAD
           // filterContext.HttpContext.Response.Write("返回Result之后" + Message + "<br />");
=======
            filterContext.HttpContext.Response.Write("返回Result之后" + Message + "<br />");
>>>>>>> ff89a5a760e31eaaf33ef9d1b03ea6b4d8720970
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