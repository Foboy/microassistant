using MicroAssistantMvc.Filters;
using System.Web;
using System.Web.Mvc;

namespace MicroAssistantMvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //注册全局过滤器
            filters.Add(new LogFilterAttribute() { Message = "全局" });
        }
    }
}