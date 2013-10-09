
﻿using MicroAssistant.Common;
using MicroAssistant.DataAccess;
using MicroAssistant.DataStructure;
using MicroAssistant.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicroAssistantMvc.Areas.MarketingManagement.Controllers
{
    public class MarketingController : Controller
    {
        //
        // GET: /MarketingManagement/Marketing/
        /// <summary>
        /// 销售机会即企业客户以及个人客户信息，此处都是调用客户信息
        /// </summary>
        /// <returns></returns>

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 添加销售机会（添加企业客户）
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public JsonResult AddMarketingChanceE(CustomerEnt customer, int entid)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();
            try
            {
                int i =CustomerEntAccessor.Instance.Insert(customer);
                if (i>0)
                    result.Error = AppError.ERROR_SUCCESS;
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
        /// 添加销售机会（添加个人客户）
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="IsEnt">true 为企业客户 false为个人客户</param>
        /// <returns></returns>
        public JsonResult AddMarketingChance(Customer customer,bool IsEnt,int entid )
        {
            return null;
        }

        public JsonResult AddMarketingChanceP(CustomerPrivate customer, int entid)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();
            try
            {

                //bool check =(bool) CustomerEntAccessor.Instance.Insert(ce);
                //if (check)
                //    result.Error = AppError.ERROR_SUCCESS;

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
        /// 获取销售机会列表
        /// </summary>
        /// <param name="entid"></param>
        /// <returns></returns>
        public JsonResult SearchMarketingList(int entid)
        {
            return null;
        }

        public JsonResult GetMarketingInfo(int cid)
        {
            return null;
        }


       //编辑销售机会详情
        public JsonResult EditMarketingInfo(Customer customer, bool IsEnt)
        {
            return null;
        }

       //编辑销售机会详情 企业
        public JsonResult EditMarketingInfoE(CustomerEnt customer)
        {
            return null;
        }
        //编辑销售机会详情 个人
        public JsonResult EditMarketingInfoP(CustomerPrivate customer)

        {
            return null;
        }
//针对单个销售机会拜访客户(改变销售机会状态为拜访)
        public JsonResult ToVisit(int eid)
        {
            return null;
        }
//获取单个销售机会的拜访记录
        public JsonResult SearchVisitInfoByEID(int eid)
        {
            return null;
        }
//添加拜访记录
        public JsonResult AddVisitInfo(int eid)
        {
            return null;
        }
//编辑拜访记录
        public JsonResult EditVisitInfo(int vid)
        {
            return null;
        }
//修改拜访赢率
        public JsonResult EditCustomerRate(float num)
        {
            return null;
        }
//添加售后跟进记录
        public JsonResult AddAfterSalesService()
        {
            return null;
        }
//编辑售后跟进记录
        public JsonResult EditAfterSalesService()
        {
            return null;
        }
//获取售后跟进列表
        public JsonResult GetAfterSalesService(int aid)
        {
            return null;
        }

    }
}
