using System.Web.Mvc;

namespace MicroAssistantMvc.Areas.ClientManage
{
    public class ClientManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "ClientManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ClientManage_default",
                "ClientManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
