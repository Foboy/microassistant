using System.Web.Mvc;

namespace MicroAssistantMvc3.Areas.MarketingManagement
{
    public class MarketingManagementAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MarketingManagement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MarketingManagement_default",
                "MarketingManagement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
