
﻿using MicroAssistant.Cache;
using MicroAssistant.Common;
using MicroAssistant.DataAccess;
using MicroAssistant.DataStructure;
using MicroAssistant.Meta;
using MicroAssistantMvc.Areas.MarketingManagement.Models;
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
                    if (!CheckUserFunction("1202"))
                    {
                        result.Error = AppError.ERROR_PERMISSION_FORBID;
                        Res.Data = result;
                        Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                        return Res;
                    }

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
                                CustomerEnt ce = new CustomerEnt();
                                ce.EntName = username;
                                ce.ContactUsername = username;
                                ce.ContactMobile = phone;
                                ce.ContactPhone = phone;
                                ce.ContactEmail = email;
                                ce.ContactQq = qq;
                                ce.Detail = chanceDetail;
                                ce.OwnerId = CurrentUser.UserId;
                                ce.EntId = CurrentUser.EntId;
                                CustomerEntAccessor.Instance.Insert(ce);
                                break;
                            case 2:
                                //添加个人客户
                                CustomerPrivate cp = new CustomerPrivate();
                                cp.Name = username;
                                cp.Mobile = phone;
                                cp.Phone = phone;
                                cp.Email = email;
                                cp.Qq = qq;
                                cp.Detail = chanceDetail;

                                cp.OwnerId = CurrentUser.UserId;
                                cp.EntId = CurrentUser.EntId; CustomerPrivateAccessor.Instance.Insert(cp);
                                break;
                        }
                    }
                    int i = MarketingChanceAccessor.Instance.Insert(chance);
                    if (i > 0)
                    {
                        result.Id = i;
                        result.Error = AppError.ERROR_SUCCESS;
                    }
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
                    if (!CheckUserFunction("1201"))
                    {
                        result.Error = AppError.ERROR_PERMISSION_FORBID;
                        Res.Data = result;
                        Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                        return Res;
                    }
                    PageEntity<MarketingChance> returnValue = new PageEntity<MarketingChance>();
                    returnValue = MarketingChanceAccessor.Instance.Search(1, CurrentUser.UserId, pageIndex, pageSize);//1:未拜访 2：已拜访
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
                    if (!CheckUserFunction("1205"))
                    {
                        result.Error = AppError.ERROR_PERMISSION_FORBID;
                        Res.Data = result;
                        Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                        return Res;
                    }
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
                    if (!CheckUserFunction("1202"))
                    {
                        result.Error = AppError.ERROR_PERMISSION_FORBID;
                        Res.Data = result;
                        Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                        return Res;
                    }

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
                    if (!CheckUserFunction("1204"))
                    {
                        result.Error = AppError.ERROR_PERMISSION_FORBID;
                        Res.Data = result;
                        Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                        return Res;
                    }
                    MarketingVisit mv=new MarketingVisit();
                    mv.Address=address;
                    mv.Amount=amount;
                    mv.ChanceId=cid;
                    mv.Remark=remark;
                    mv.VisitTime=DateTime.Now;
                    mv.VisitType=visitType;
                    mv.EntId = CurrentUser.EntId;
                   int i = MarketingVisitAccessor.Instance.Insert(mv);

                   if (i > 0)
                   {
                       MarketingChanceAccessor.Instance.UpdateisVisit(cid, 2);
                       result.Error = AppError.ERROR_SUCCESS;
                   }
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
        public JsonResult GetVisitInfo(int cid, int pageIndex, int pageSize)
        {
            var Res = new JsonResult();
            AdvancedResult<SingleVisitListModel> result = new AdvancedResult<SingleVisitListModel>();

            try
            {
                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    if (!CheckUserFunction("1205"))
                    {
                        result.Error = AppError.ERROR_PERMISSION_FORBID;
                        Res.Data = result;
                        Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                        return Res;
                    }
                    PageEntity<MarketingVisit> returnValue = new PageEntity<MarketingVisit>();
                    result.Data = new SingleVisitListModel();
                  result.Data.Vlist =  MarketingVisitAccessor.Instance.Search(cid, pageIndex, pageSize);
                  result.Data.Rate = MarketingChanceAccessor.Instance.Get(cid).Rate;
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
        //获取拜访记录列表 返回 拜访记录列表（机会描述，拜访次数，盈率，最近一次拜访时间）
        public JsonResult SearchVisitInfoList(int pageIndex, int pageSize)
        {
            var Res = new JsonResult();
            AdvancedResult<PageEntity<VisitModel>> result = new AdvancedResult<PageEntity<VisitModel>>();
            List<VisitModel> vlist = new List<VisitModel>();
            try
            {
                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {
  
                    if (!CheckUserFunction("1203"))
                    {
                        result.Error = AppError.ERROR_PERMISSION_FORBID;
                        Res.Data = result;
                        Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                        return Res;
                    }
                   PageEntity<MarketingChance> clist = MarketingChanceAccessor.Instance.Search(2,CurrentUser.UserId, pageIndex, pageSize);
                   result.Data.RecordsCount = clist.RecordsCount;
                   result.Data.PageIndex = pageIndex;
                   result.Data.PageSize = pageSize;
                   List<MarketingVisit> mvlist = new List<MarketingVisit>();
                   for (int i = 0; i < clist.Items.Count; i++)
                   {
                       VisitModel vm = new VisitModel();
                       vm.Rate = clist.Items[i].Rate;
                       vm.Remark = clist.Items[i].Remark;
                       vm.IdmarketingChance = clist.Items[i].IdmarketingChance;
                 
                      PageEntity<MarketingVisit> pmvlist =  MarketingVisitAccessor.Instance.Search(clist.Items[i].IdmarketingChance, 0, 100);
                      if (pmvlist.RecordsCount > 0)
                      {
                          vm.LastVisitTime = pmvlist.Items[0].VisitTime;
                          vm.VisitNum = pmvlist.RecordsCount;
                      }
                      else
                      {
                          vm.VisitNum = 0;
                      }
                      vlist.Add(vm);
                      
                   }
                    
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data.Items = vlist;
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
        public JsonResult EditVisitInfo(int vid, int visitType, string remark, Double amount, String address)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();

            try
            {
                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    if (!CheckUserFunction("1204"))
                    {
                        result.Error = AppError.ERROR_PERMISSION_FORBID;
                        Res.Data = result;
                        Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                        return Res;
                    }
                    MarketingVisit mv = MarketingVisitAccessor.Instance.Get(vid);
                    mv.Address = address;
                    mv.Amount = amount;
                    mv.IdmarketingVisit = vid;
                    mv.Remark = remark;
                    mv.VisitType = visitType;
                   MarketingVisitAccessor.Instance.Update(mv);
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
        public JsonResult EditCustomerRate(int cid,int num)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();

            try
            {
                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    if (!CheckUserFunction("1204"))
                    {
                        result.Error = AppError.ERROR_PERMISSION_FORBID;
                        Res.Data = result;
                        Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                        return Res;
                    }
                    MarketingChanceAccessor.Instance.UpdateRate(cid, num);
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
        /// 获取机会数量，拜访数量，合同数量
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMarketingCount()
        {
            var Res = new JsonResult();
            AdvancedResult<List<int>> result = new AdvancedResult<List<int>>();
            List<int> nums = new List<int>();
            try
            {
                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {

                    if (!CheckUserFunction("12"))
                    {
                        result.Error = AppError.ERROR_PERMISSION_FORBID;
                        Res.Data = result;
                        Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                        return Res;
                    }
                    nums.Add(MarketingChanceAccessor.Instance.GetMarketingChanceCount(CurrentUser.UserId, 1));//获取销售机会数量
                    nums.Add(MarketingChanceAccessor.Instance.GetMarketingChanceCount(CurrentUser.UserId,2));//获取拜访记录数以销售机会为基准
                    nums.Add(ContractInfoAccessor.Instance.GetContractInfoCount(CurrentUser.EntId));
                   
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = nums;
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
