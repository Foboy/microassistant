using System.Web.Mvc;

namespace MicroAssistantMvc.Areas.FileManagement
{
    public class FileManagementAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "FileManagement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "FileManagement_default",
                "FileManagement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
