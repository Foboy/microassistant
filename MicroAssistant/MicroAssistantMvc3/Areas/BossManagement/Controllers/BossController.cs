using MicroAssistant.Cache;
using MicroAssistant.Common;
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

        public JsonResult SearchSalesReport()
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


                    List<int> list = new List<int>();

                    list.Add(1152);
                    list.Add(234);
                    list.Add(532);
                    list.Add(321);
                    list.Add(23);

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

        public JsonResult SearchSalesOppReport()
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
        public JsonResult SalesFinanceReport()
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
