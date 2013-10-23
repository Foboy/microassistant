
﻿using MicroAssistant.Cache;
using MicroAssistant.Common;
using MicroAssistant.DataAccess;
using MicroAssistant.DataStructure;
using MicroAssistant.Meta;
using MicroAssistantMvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicroAssistantMvc.Areas.MarketingManagement.Controllers
{
    public class MarketingController : MicControllerBase
    {
        //
        // GET: /MarketingManagement/Marketing/
        /// <summary>
        /// 销售机会即企业客户以及个人客户信息，此处都是调用客户信息
        /// </summary>
        /// <returns></returns>



        /// <summary>
        /// 添加销售机会（机会类型，客户类型，联系人，机会描述，联系方式{}，是否同步，token）返回（true/false）
        /// 客户类型分为：1:企业客户2:个人客户；默认为企业客户
        /// </summary>
        public JsonResult AddMarketingChance(int chanceType, int customerType,
            string username, string chanceDetail, string tel,
            string phone, string email, string qq, bool Isasyn)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();

            try
            {
                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    MarketingChance chance = new MarketingChance();
                    chance.AddTime = DateTime.Now;
                    chance.ChanceType = chanceType;
                    chance.ContactName = username;
                    chance.CustomerType = customerType;
                    chance.Email = email;
                    chance.EntId = CurrentUser.EntId;
                    chance.Phone = phone;
                    chance.Qq = qq;
                    chance.Remark = chanceDetail;
                    chance.Tel = tel;
                    chance.UserId = CurrentUser.UserId;
                    if (Isasyn)
                    {
                        switch (customerType)
                        {
                            case 1:
                                //添加企业客户
                                break;
                            case 2:
                                //添加个人客户
                                break;
                        }
                    }
                    int i = MarketingChanceAccessor.Instance.Insert(chance);
                    if (i > 0)
                        result.Error = AppError.ERROR_SUCCESS;
                    else
                        result.Error = AppError.ERROR_FAILED;
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
        /// //获取销售机会列表（同获取客户信息列表（包括个人跟企业））（token）
        /// 返回 列表（机会描述，联系人，联系方式，创建时间，企业/个人客户）
        /// </summary>
        /// <param name="entid"></param>
        /// <returns></returns>
        public JsonResult SearchMarketingList(int pageIndex, int pageSize)
        {
            var Res = new JsonResult();
            AdvancedResult<PageEntity<MarketingChance>> result = new AdvancedResult<PageEntity<MarketingChance>>();

            try
            {
                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    PageEntity<MarketingChance> returnValue = new PageEntity<MarketingChance>();
                   returnValue = MarketingChanceAccessor.Instance.Search(CurrentUser.UserId, pageIndex, pageSize);
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = returnValue;
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

        //获取单个销售机会详情（销售机会ID，token） 返回 （机会类型，客户类型，机会描述，联系人，联系方式，录入时间）
        public JsonResult GetMarketingInfo(int cid)
        {
            var Res = new JsonResult();
            AdvancedResult<MarketingChance> result = new AdvancedResult<MarketingChance>();

            try
            {
                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    MarketingChance chance = new MarketingChance();
                   chance = MarketingChanceAccessor.Instance.Get(cid);
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = chance;
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


        //编辑销售机会详情（机会ID，机会类型，客户类型，机会描述，联系人，联系方式，token） 返回（true/false）
        public JsonResult EditMarketingInfo(int cid,int chanceType, int customerType,
            string username, string chanceDetail, string tel,
            string phone, string email, string qq)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();

            try
            {
                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    MarketingChance chance = new MarketingChance();
                    chance = MarketingChanceAccessor.Instance.Get(cid);
                    chance.ChanceType = chanceType;
                    chance.ContactName = username;
                    chance.CustomerType = customerType;
                    chance.Email = email;
                    chance.Phone = phone;
                    chance.Qq = qq;
                    chance.Remark = chanceDetail;
                    chance.Tel = tel;
                    MarketingChanceAccessor.Instance.Update(chance);
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

        //针对单个销售机会拜访客户（销售机会ID，拜访方式，拜访描述，报价，地点，token）
        //返回（True/false）注：如果是第一次则需修改机会表字段为已拜访
        public JsonResult ToVisit(int cid, int visitType, string remark, Double amount, String address)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();

            try
            {
                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    MarketingVisit mv=new MarketingVisit();
                    mv.Address=address;
                    mv.Amount=amount;
                    mv.ChanceId=cid;
                    mv.Remark=remark;
                    mv.VisitTime=DateTime.Now;
                    mv.VisitType=visitType;
                   int i = MarketingVisitAccessor.Instance.Insert(mv);
                    if (i > 0)
                        result.Error = AppError.ERROR_SUCCESS;
                    else
                        result.Error = AppError.ERROR_FAILED;
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

        //获取单个销售机会的拜访记录（销售机会ID，toke）返回 拜访列表（拜访方式，拜访描述，报价,地点），赢率
        public JsonResult GetVisitInfo(int eid)
        {
            var Res = new JsonResult();
            AdvancedResult<MarketingVisit> result = new AdvancedResult<MarketingVisit>();

            try
            {
                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Error = AppError.ERROR_SUCCESS;
                    //result.Data = con;
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
        //获取拜访记录列表（token）返回 拜访记录列表（机会描述，拜访次数，盈率，最近一次拜访时间）
        public JsonResult SearchVisitInfoList(int eid)
        {
            var Res = new JsonResult();
            AdvancedResult<ContractInfo> result = new AdvancedResult<ContractInfo>();

            try
            {
                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    result.Error = AppError.ERROR_SUCCESS;
                    //result.Data = con;
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
        //编辑拜访记录（拜访记录ID，拜访方式，拜访描述，报价，地点）返回（true/false）
        public JsonResult EditVisitInfo(int vid)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();

            try
            {
                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {

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
        //修改拜访赢率 （销售机会ID，赢率，toke）返回（true/false）
        public JsonResult EditCustomerRate(float num)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();

            try
            {
                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {

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
////添加售后跟进记录
//        public JsonResult AddAfterSalesService()
//        {
//            return null;
//        }
////编辑售后跟进记录
//        public JsonResult EditAfterSalesService()
//        {
//            return null;
//        }
////获取售后跟进列表
//        public JsonResult GetAfterSalesService(int aid)
//        {
//            return null;
//        }

    }
}
