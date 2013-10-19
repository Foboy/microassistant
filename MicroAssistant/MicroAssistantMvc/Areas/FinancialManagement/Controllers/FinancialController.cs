using MicroAssistant.Cache;
using MicroAssistant.Common;
using MicroAssistant.DataStructure;
using MicroAssistant.Meta;
using MicroAssistantMvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicroAssistantMvc.Areas.FinancialManagement.Controllers
{
    public class FinancialController : MicControllerBase
    {
        //
        // GET: /FinancialManagement/Financial/

    

//       
//
//
//
        /// <summary>
        ///  根据企业ID获取应收款列表（token）返回 应收款列表（客户名称，合同编号，本期应收金额，本期应收时间）
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public JsonResult SearchReceivables(string token)
        {
            var Res = new JsonResult();
            AdvancedResult<ContractHowtopay> result = new AdvancedResult<ContractHowtopay>();
            if (CacheManagerFactory.GetMemoryManager().Contains(token))
            {
                // int ownerid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                try
                {
                    ContractHowtopay con = new ContractHowtopay();
                   // con = ContractInfoAccessor.Instance.Get(ContractNo);
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = con;

                }
                catch (Exception e)
                {
                    result.Error = AppError.ERROR_FAILED;
                    result.ExMessage = e.ToString();
                }

                result.Error = AppError.ERROR_SUCCESS;
            }
            else
            {
                result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
            }


            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
       }
        /// <summary>
        /// 根据企业ID获取应付款列表 （token）返回 应付款列表（采购批次，单价，数量，提交时间，付款状态）
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public JsonResult SearchPayablesByEID(string token)
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
        public JsonResult GetHowToPayByEID(string contractNo,string token)
        {
            return null;
        }
        /// <summary>
        /// 根据企业ID确认应收款（合同编号，收款序号，token）返回（true/false）
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public JsonResult ConfirmReceived(string contractNo,int rNum,string token)
        {
            return null;
        }



    }
}
