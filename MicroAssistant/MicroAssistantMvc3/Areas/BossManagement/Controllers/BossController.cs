using MicroAssistant.Cache;
using MicroAssistant.Common;
using MicroAssistant.DataAccess;
using MicroAssistant.DataStructure;
using MicroAssistant.Meta;
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
                               stime = GetStartTime(TimeType.quarter);
                            etime = GetEndTime(TimeType.quarter);
                            break;
                        case 3:
                               stime = GetStartTime(TimeType.year);
                            etime = GetEndTime(TimeType.year);
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
                            stime = GetStartTime(TimeType.quarter);
                            etime = GetEndTime(TimeType.quarter);
                            break;
                        case 3:
                            stime = GetStartTime(TimeType.year);
                            etime = GetEndTime(TimeType.year);
                            break;
                        default:
                            break;
                    }

                    List<SalesOppReport> list = new List<SalesOppReport>();
                    List<MarketingChance> clist = new List<MarketingChance>();

                    clist = BossAccessor.Instance.LoadChanceByEntId(CurrentUser.EntId,stime,etime);
                    
                    switch (timeType)
                    {
                        case 1://本月

                                SalesOppReport obj = new SalesOppReport();
                                obj.month = stime.Month.ToString()+"月";
                                obj.newc = clist.Select(o => o.ChanceType == 1).Count();
                                obj.old = clist.Select(o => o.ChanceType == 0).Count();
                                list.Add(obj);
                            break;
                        case 2:
                            for (int i = 0; i < 3; i++)
                            {
                                SalesOppReport objq = new SalesOppReport();
                                objq.month = (stime.Month+i).ToString() + "月";
                                objq.newc = clist.Select(o => o.ChanceType == 1 && o.AddTime >= stime.AddMonths(i) && o.AddTime < stime.AddMonths(i+1)).Count();
                                objq.old = clist.Select(o => o.ChanceType == 0 && o.AddTime >= stime.AddMonths(i) && o.AddTime < stime.AddMonths(i+1)).Count();
                                list.Add(objq);
                            }
                            break;
                        case 3:
                            for (int i = 0; i < 12; i++)
                            {
                                SalesOppReport objy = new SalesOppReport();
                                objy.month = (stime.Month + i).ToString() + "月";
                                objy.newc = clist.Select(o => o.ChanceType == 1 && o.AddTime >= stime.AddMonths(i) && o.AddTime < stime.AddMonths(i+1)).Count();
                                objy.old = clist.Select(o => o.ChanceType == 0 && o.AddTime >= stime.AddMonths(i) && o.AddTime < stime.AddMonths(i+1)).Count();
                                list.Add(objy);
                            }
                            break;
                        default:
                            break;
                    }
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
                            stime = GetStartTime(TimeType.quarter);
                            etime = GetEndTime(TimeType.quarter);
                            break;
                        case 3:
                            stime = GetStartTime(TimeType.year);
                            etime = GetEndTime(TimeType.year);
                            break;
                        default:
                            break;
                    }

                    List<SalesFinanceReport> list = new List<SalesFinanceReport>();


                    List<BossFinancial> flist = new List<BossFinancial>();

                    flist = BossAccessor.Instance.LoadBossFinancialList(CurrentUser.EntId, stime, etime);

                    switch (timeType)
                    {
                        case 1://本月
                                SalesFinanceReport obj = new SalesFinanceReport();
                               
                                obj.month = stime.Month.ToString() + "月";
                                obj.paydone = flist.FindAll(o=>o.PType==2).Sum(o => o.Amount);
                                obj.notpay = flist.FindAll(o => o.PType == 1).Sum(o => o.Amount);
                                obj.received = flist.FindAll(o => o.PType == 3).Sum(o => o.Amount);
                                obj.notreceive = flist.FindAll(o => o.PType == 4).Sum(o => o.Amount);
                                list.Add(obj);
                            break;
                        case 2:
                            for (int i = 0; i < 3; i++)
                            {
                                SalesFinanceReport objq = new SalesFinanceReport();

                                objq.month = stime.Month.ToString() + "月";
                                objq.paydone = flist.FindAll(o => o.PType == 2 && o.PayTime >= stime.AddMonths(i) && o.PayTime < stime.AddMonths(i + 1)).Sum(o => o.Amount);
                                objq.notpay = flist.FindAll(o => o.PType == 1 && o.PayTime >= stime.AddMonths(i) && o.PayTime < stime.AddMonths(i + 1)).Sum(o => o.Amount);
                                objq.received = flist.FindAll(o => o.PType == 3 && o.PayTime >= stime.AddMonths(i) && o.PayTime < stime.AddMonths(i + 1)).Sum(o => o.Amount);
                                objq.notreceive = flist.FindAll(o => o.PType == 4 && o.PayTime >= stime.AddMonths(i) && o.PayTime < stime.AddMonths(i + 1)).Sum(o => o.Amount);
                                list.Add(objq);

                            }
                            break;
                        case 3:
                            for (int i = 0; i < 12; i++)
                            {
                                SalesFinanceReport objy = new SalesFinanceReport();

                                objy.month = stime.Month.ToString() + "月";
                                objy.paydone = flist.FindAll(o => o.PType == 2 && o.PayTime >= stime.AddMonths(i) && o.PayTime < stime.AddMonths(i + 1)).Sum(o => o.Amount);
                                objy.notpay = flist.FindAll(o => o.PType == 1 && o.PayTime >= stime.AddMonths(i) && o.PayTime < stime.AddMonths(i + 1)).Sum(o => o.Amount);
                                objy.received = flist.FindAll(o => o.PType == 3 && o.PayTime >= stime.AddMonths(i) && o.PayTime < stime.AddMonths(i + 1)).Sum(o => o.Amount);
                                objy.notreceive = flist.FindAll(o => o.PType == 4 && o.PayTime >= stime.AddMonths(i) && o.PayTime < stime.AddMonths(i + 1)).Sum(o => o.Amount);
                                list.Add(objy);
                            }
                            break;
                        default:
                            break;
                    }


                    //for (int i = 4; i < 7; i++)
                    //{
                    //    SalesFinanceReport obj = new SalesFinanceReport();
                    //    Random Random1 = new Random();
                    //    obj.month = i.ToString();
                    //    obj.paydone = Random1.Next(0, 1001);
                    //    obj.notpay = Random1.Next(0, 1001);
                    //    obj.received = Random1.Next(0, 1001);
                    //    obj.notreceive = Random1.Next(0, 1001); 
                    //    list.Add(obj);
                    //}

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
