using MicroAssistant.Cache;
using MicroAssistant.Common;
using MicroAssistant.DataAccess;
using MicroAssistant.DataStructure;
using MicroAssistantMvc.Areas.BossManagement.Models;
using MicroAssistantMvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicroAssistantMvc.Areas.BossManagement.Controllers
{
    public class BossController : MicControllerBase
    {
        //
        // GET: /BossManagement/Boss/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeType">1:月 2：季度 3：年</param>
        /// <returns></returns>
        public JsonResult SearchSalesReport(int timeType)
        {
            var Res = new JsonResult();
            AdvancedResult<List<int>> result = new AdvancedResult<List<int>>();
            try
            {
                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    //if (!CheckUserFunction("1801"))
                    //{
                    //    result.Error = AppError.ERROR_PERMISSION_FORBID;
                    //    Res.Data = result;
                    //    Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    //    return Res;
                    //}

                    DateTime stime = DateTime.Now;
                    DateTime etime = DateTime.Now;
                    switch (timeType)
                    {
                        case 1:
                            stime = GetStartTime(TimeType.month);
                            etime = GetEndTime(TimeType.month);
                            break;
                        case 2:
                               stime = GetStartTime(TimeType.month);
                            etime = GetEndTime(TimeType.month);
                            break;
                        case 3:
                               stime = GetStartTime(TimeType.month);
                            etime = GetEndTime(TimeType.month);
                            break;
                        default:
                            break;
                    }

                    List<int> list = new List<int>();
                    int entid = CurrentUser.EntId;
                    list.Add(BossAccessor.Instance.GetMarketingChanceCount(entid, 1, stime, etime));
                    list.Add(BossAccessor.Instance.GetFirstAndMoreVisitCount(entid, 1, stime, etime));
                    list.Add(BossAccessor.Instance.GetFirstAndMoreVisitCount(entid, 2, stime, etime));
                    list.Add(BossAccessor.Instance.GetFirstAndMoreVisitCount(entid, 3, stime, etime));
                    list.Add(BossAccessor.Instance.GetContractInfoCount(entid, stime, etime));
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = list;

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
        /// 
        /// </summary>
        /// <param name="timeType">1:月 2：季度 3：年</param>
        /// <returns></returns>
        public JsonResult SearchSalesOppReport(int timeType)
        {
            var Res = new JsonResult();
            AdvancedResult<List<SalesOppReport>> result = new AdvancedResult<List<SalesOppReport>>();
            try
            {
                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    //if (!CheckUserFunction("1801"))
                    //{
                    //    result.Error = AppError.ERROR_PERMISSION_FORBID;
                    //    Res.Data = result;
                    //    Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    //    return Res;
                    //}

                    DateTime stime = DateTime.Now;
                    DateTime etime = DateTime.Now;
                    switch (timeType)
                    {
                        case 1:
                            stime = GetStartTime(TimeType.month);
                            etime = GetEndTime(TimeType.month);
                            break;
                        case 2:
                            stime = GetStartTime(TimeType.month);
                            etime = GetEndTime(TimeType.month);
                            break;
                        case 3:
                            stime = GetStartTime(TimeType.month);
                            etime = GetEndTime(TimeType.month);
                            break;
                        default:
                            break;
                    }

                    List<SalesOppReport> list = new List<SalesOppReport>();

                    for (int i = 4; i < 7; i++)
                    {
                        SalesOppReport obj = new SalesOppReport();
                        Random Random1 = new Random();
                        obj.month = i.ToString();
                        obj.newc = Random1.Next(0, 1001);
                        obj.old = Random1.Next(0, 1001); 
                        list.Add(obj);
                    }

                        //list = CustomerEntAccessor.Instance.SearchCustomerEntByName(name);
                        result.Error = AppError.ERROR_SUCCESS;
                    result.Data = list;

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
        /// 
        /// </summary>
        /// <param name="timeType">1:月 2：季度 3：年</param>
        /// <returns></returns>
        public JsonResult SalesFinanceReport(int timeType)
        {
            var Res = new JsonResult();
            AdvancedResult<List<SalesFinanceReport>> result = new AdvancedResult<List<SalesFinanceReport>>();
            try
            {
                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    //if (!CheckUserFunction("1801"))
                    //{
                    //    result.Error = AppError.ERROR_PERMISSION_FORBID;
                    //    Res.Data = result;
                    //    Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    //    return Res;
                    //}

                    DateTime stime = DateTime.Now;
                    DateTime etime = DateTime.Now;
                    switch (timeType)
                    {
                        case 1:
                            stime = GetStartTime(TimeType.month);
                            etime = GetEndTime(TimeType.month);
                            break;
                        case 2:
                            stime = GetStartTime(TimeType.month);
                            etime = GetEndTime(TimeType.month);
                            break;
                        case 3:
                            stime = GetStartTime(TimeType.month);
                            etime = GetEndTime(TimeType.month);
                            break;
                        default:
                            break;
                    }

                    List<SalesFinanceReport> list = new List<SalesFinanceReport>();

                    for (int i = 4; i < 7; i++)
                    {
                        SalesFinanceReport obj = new SalesFinanceReport();
                        Random Random1 = new Random();
                        obj.month = i.ToString();
                        obj.paydone = Random1.Next(0, 1001);
                        obj.notpay = Random1.Next(0, 1001);
                        obj.received = Random1.Next(0, 1001);
                        obj.notreceive = Random1.Next(0, 1001); 
                        list.Add(obj);
                    }

                    //list = CustomerEntAccessor.Instance.SearchCustomerEntByName(name);
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = list;

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
