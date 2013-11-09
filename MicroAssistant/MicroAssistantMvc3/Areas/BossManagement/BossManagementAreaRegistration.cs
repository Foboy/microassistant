using System.Web.Mvc;

namespace MicroAssistantMvc.Areas.BossManagement
{
    public class BossManagementAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "BossManagement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "BossManagement_default",
                "BossManagement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
