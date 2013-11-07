using MicroAssistant.Cache;
using MicroAssistant.Common;
using MicroAssistant.DataAccess;
using MicroAssistant.DataStructure;
using MicroAssistant.Meta;
using MicroAssistantMvc.Areas.FinancialManagement.Models;
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

        /// <summary>
        ///  根据企业ID获取应收款列表（token）返回 应收款列表（客户名称，合同编号，本期应收金额，本期应收时间）
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public JsonResult SearchReceivables(int pageIndex,int pageSize)
        {
            var Res = new JsonResult();
            AdvancedResult<List<ReceivablesModel>> result = new AdvancedResult<List<ReceivablesModel>>();
            List<ReceivablesModel> rmlist = new List<ReceivablesModel>();

                try
                {
                    if (CacheManagerFactory.GetMemoryManager().Contains(token))
                    {
                        int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                        SysUser user = SysUserAccessor.Instance.Get(userid);

                        PageEntity<ContractInfo> clist = new PageEntity<ContractInfo>();
                        clist = ContractInfoAccessor.Instance.Search(pageIndex, pageSize, user.EntId);
                        for (int i = 0; i < clist.Items.Count; i++)
                        {
                            ReceivablesModel rm = new ReceivablesModel();
                            rm.ContractNo = clist.Items[i].ContractNo;
                            rm.CustomerName = clist.Items[i].CustomerName;

                            List<ContractHowtopay> hplist = new List<ContractHowtopay>();
                            hplist = ContractHowtopayAccessor.Instance.Search(rm.ContractNo, 1);
                            if (hplist.Count > 0)
                            {
                                rm.PayTime = hplist[0].PayTime;
                                rm.ReceivedTime = hplist[0].ReceivedTime;
                                rmlist.Add(rm);
                            }
                            else
                            {
                                hplist = ContractHowtopayAccessor.Instance.Search(rm.ContractNo, 2);
                                rm.PayTime = hplist[hplist.Count - 1].PayTime;
                                rm.ReceivedTime = hplist[hplist.Count - 1].ReceivedTime;
                                rmlist.Add(rm);
                            }
                        }
                        result.Error = AppError.ERROR_SUCCESS;
                        result.Data = rmlist;
                    }
                    else
                    {
                        result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                    }

                }
                catch (Exception e)
                {
                    result.Error = AppError.ERROR_FAILED;
                    result.ExMessage = e.ToString();
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
        public JsonResult SearchPayablesByEID(int pageIndex, int pageSize)
        {
            var Res = new JsonResult();
            AdvancedResult<PageEntity<ProProductonDetail>> result = new AdvancedResult<PageEntity<ProProductonDetail>>();
            PageEntity<ProProductonDetail> rmlist = new PageEntity<ProProductonDetail>();
            
                try
                {
                    if (CacheManagerFactory.GetMemoryManager().Contains(token))
                    {
                        int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                        SysUser user = SysUserAccessor.Instance.Get(userid);
                        //获取应付款列表
                        rmlist = ProProductonDetailAccessor.Instance.Search(0, 0, user.EntId, pageIndex, pageSize);

                        result.Error = AppError.ERROR_SUCCESS;
                        result.Data = rmlist;
                    }
                    else
                    {
                        result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                    }

                }
                catch (Exception e)
                {
                    result.Error = AppError.ERROR_FAILED;
                    result.ExMessage = e.ToString();
                }
            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }
        /// <summary>
        /// 根据合同编号获取应收款详情（合同编号，token）
        /// 返回 （合同编号，合同名称，客户名称，合同有效期1，合同有效期2，合同承办人，
        /// 合同签订时间，合同金额，付款方式列表（序号，收款时间，实际收款时间，收款状态））
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public JsonResult GetHowToPayByEID(string contractNo)
        {
            var Res = new JsonResult();
            AdvancedResult<ContractInfo> result = new AdvancedResult<ContractInfo>();
           
                try
                {
                    if (CacheManagerFactory.GetMemoryManager().Contains(token))
                    {
                        // int ownerid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                        ContractInfo con = new ContractInfo();
                        con = ContractInfoAccessor.Instance.Get(contractNo);
                        con.HowtopayList = ContractHowtopayAccessor.Instance.Search(contractNo, 0);
                        result.Error = AppError.ERROR_SUCCESS;
                        result.Data = con;
                    }
                    else
                    {
                        result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                    }
                }
                catch (Exception e)
                {
                    result.Error = AppError.ERROR_FAILED;
                    result.ExMessage = e.ToString();
                }
            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }
        /// <summary>
        /// 根据合同编号以及收款序号确认应收款（合同编号，收款序号，token）返回（true/false）
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public JsonResult ConfirmReceived(string contractNo,int rNum)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();
         
                try
                {
                    if (CacheManagerFactory.GetMemoryManager().Contains(token))
                    {
                        ContractHowtopayAccessor.Instance.UpdateIsReceived(contractNo, rNum, 2);
                        result.Error = AppError.ERROR_SUCCESS;
                    }
                    else
                    {
                        result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                    }
                }
                catch (Exception e)
                {
                    result.Error = AppError.ERROR_FAILED;
                    result.ExMessage = e.ToString();
                }

            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }

        /// <summary>
        /// 根据采购批次确认应付款（采购批次，token）返回（true/false）
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public JsonResult ConfirmPay(string PCode)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();

            try
            {
                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    ProProductonDetailAccessor.Instance.UpdateIsPay(PCode, 2);
                    result.Error = AppError.ERROR_SUCCESS;
                }
                else
                {
                    result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
                }
            }
            catch (Exception e)
            {
                result.Error = AppError.ERROR_FAILED;
                result.ExMessage = e.ToString();
            }
            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }

    }
}
