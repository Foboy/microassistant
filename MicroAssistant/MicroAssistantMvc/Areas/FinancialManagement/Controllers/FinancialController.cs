using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicroAssistantMvc.Areas.FinancialManagement.Controllers
{
    public class FinancialController : Controller
    {
        //
        // GET: /FinancialManagement/Financial/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 根据企业ID获取应收款列表
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public JsonResult SearchReceivablesByEID(int eid)
        {
            return null;
       }
        /// <summary>
        /// 根据企业ID获取应付款列表
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public JsonResult SearchPayablesByEID(int eid)
        {
            return null;
        }
        /// <summary>
        /// 根据企业ID获取应收款详情(方法名字可能要改)
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public JsonResult GetHowToPayByEID(int eid)
        {
            return null;
        }
        /// <summary>
        /// 根据企业ID确认应收款
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public JsonResult ConfirmReceived(int eid)
        {
            return null;
        }



    }
}
