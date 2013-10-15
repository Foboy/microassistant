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

//       
//
//
//
        /// <summary>
        ///  根据企业ID获取应收款列表（token）返回 应收款列表（客户名称，合同编号，本期应收金额，本期应收时间）
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public JsonResult SearchReceivablesByEID(int eid)
        {
            return null;
       }
        /// <summary>
        /// 根据企业ID获取应付款列表 （token）返回 应付款列表（采购批次，单价，数量，提交时间，付款状态）
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public JsonResult SearchPayablesByEID(int eid)
        {
            return null;
        }
        /// <summary>
        /// 根据合同编号获取应收款详情（合同编号，token）
        /// 返回 （合同编号，合同名称，客户名称，合同有效期1，合同有效期2，合同承办人，
        /// 合同签订时间，合同金额，付款方式列表（序号，收款时间，实际收款时间，收款状态））
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public JsonResult GetHowToPayByEID(int eid)
        {
            return null;
        }
        /// <summary>
        /// 根据企业ID确认应收款（合同编号，收款序号，token）返回（true/false）
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public JsonResult ConfirmReceived(int eid)
        {
            return null;
        }



    }
}
