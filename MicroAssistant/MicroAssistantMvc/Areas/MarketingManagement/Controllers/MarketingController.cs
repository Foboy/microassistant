
﻿using MicroAssistant.Common;
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
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public JsonResult AddMarketingChance(Customer customer,bool IsEnt,int entid ,string token)
        {
            return null;
        }

      
        /// <summary>
        /// //获取销售机会列表（同获取客户信息列表（包括个人跟企业））（token）
        /// 返回 列表（机会描述，联系人，联系方式，创建时间，企业/个人客户）
        /// </summary>
        /// <param name="entid"></param>
        /// <returns></returns>
        public JsonResult SearchMarketingList(string token)
        {
            return null;
        }

        //获取单个销售机会详情（销售机会ID，token） 返回 （机会类型，客户类型，机会描述，联系人，联系方式，录入时间）
        public JsonResult GetMarketingInfo(int cid)
        {
            return null;
        }


        //编辑销售机会详情（机会ID，机会类型，客户类型，机会描述，联系人，联系方式，token） 返回（true/false）
        public JsonResult EditMarketingInfo(Customer customer, bool IsEnt)
        {
            return null;
        }

        //针对单个销售机会拜访客户（销售机会ID，拜访方式，拜访描述，报价，地点，token）
        //返回（True/false）注：如果是第一次则需修改机会表字段为已拜访
        public JsonResult ToVisit(CustomerEnt customer)
        {
            return null;
        }

        //获取单个销售机会的拜访记录（销售机会ID，toke）返回 拜访列表（拜访方式，拜访描述，报价,地点），赢率
        public JsonResult GetVisitInfo(int eid)
        {
            return null;
        }
        //获取拜访记录列表（token）返回 拜访记录列表（机会描述，拜访次数，盈率，最近一次拜访时间）
        public JsonResult SearchVisitInfoList(int eid)
        {
            return null;
        }
        //编辑拜访记录（拜访记录ID，拜访方式，拜访描述，报价，地点）返回（true/false）
        public JsonResult EditVisitInfo(int vid)
        {
            return null;
        }
        //修改拜访赢率 （销售机会ID，赢率，toke）返回（true/false）
        public JsonResult EditCustomerRate(float num)
        {
            return null;
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
