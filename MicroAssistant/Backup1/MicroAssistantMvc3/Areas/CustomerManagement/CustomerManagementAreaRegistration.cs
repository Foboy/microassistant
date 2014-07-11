using System.Web.Mvc;

namespace MicroAssistantMvc.Areas.CustomerManagement
{
    public class CustomerManagementAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CustomerManagement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CustomerManagement_default",
                "CustomerManagement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
