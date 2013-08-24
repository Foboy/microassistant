using System.Web.Mvc;

namespace MicroAssistantMvc.Areas.FinanceManage
{
    public class FinanceManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "FinanceManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "FinanceManage_default",
                "FinanceManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
