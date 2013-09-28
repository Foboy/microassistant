<<<<<<< HEAD
<<<<<<< HEAD
﻿using MicroAssistantMvc.Filters;
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
=======
﻿using MicroAssistantMvc.Filters;
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
>>>>>>> ff89a5a760e31eaaf33ef9d1b03ea6b4d8720970
=======
<<<<<<< HEAD
﻿using MicroAssistantMvc.Filters;
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
=======
﻿using MicroAssistantMvc.Filters;
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
>>>>>>> ff89a5a760e31eaaf33ef9d1b03ea6b4d8720970
>>>>>>> 91ab2d29f461ef571da1d58fc0399813156857ea
}