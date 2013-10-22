using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroAssistant.Cache;
using MicroAssistant.Common;
using MicroAssistant.DataAccess;
using MicroAssistant.DataStructure;
using MicroAssistant.Meta;
using MicroAssistantMvc.Controllers;
using MicroAssistantMvc.Areas.ProductManagement.Models;

namespace MicroAssistantMvc.Areas.ProductManagement.Controllers
{
    public class ProductionController : MicControllerBase
    {
        //
        // GET: /ProductManagement/Production/

        #region IProductionController 成员
        /// <summary>
        /// 根据分类获取产品列表(Ok)
        /// </summary>
        /// <param name="typeid">分类ID	</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns>产品列表（产品名称，售价，最低售价，库存。。。)</returns>
        public JsonResult SearchProductionSByType(int typeid,string token,int pageIndex, int pageSize)
        {
            var Res = new JsonResult();
            AdvancedResult<PageEntity<ProProduction>> result = new AdvancedResult<PageEntity<ProProduction>>();
            try
            {
                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    PageEntity<ProProduction> list = new PageEntity<ProProduction>();
                    int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                    list = ProProductionAccessor.Instance.Search(string.Empty, typeid, userid, pageIndex, pageSize);
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
        /// 根据企业ID获取分类列表
        /// </summary>
        /// <param name="entid"></param>
        /// <returns></returns>

        public JsonResult SearchProductTypeListByEntID(int entid,string token,int pageIndex, int pageSize)
        {
            var Res = new JsonResult();
            AdvancedResult<List<ProductTypeModule>> result = new AdvancedResult<List<ProductTypeModule>>();
            List<ProductTypeModule> ptlist = new List<ProductTypeModule>();
            try
            {
                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    PageEntity<ProProductionType> list = new PageEntity<ProProductionType>();
                    list = ProProductionTypeAccessor.Instance.Search(0, string.Empty, entid, 0, pageIndex, pageSize);
                    for (int i = 0; i < list.Items.Count; i++)
                    {
                        ProductTypeModule pt = new ProductTypeModule();
                        pt.EntId = list.Items[i].EntId;
                        pt.PTypeId = list.Items[i].PTypeId;
                        pt.PTypeName = list.Items[i].PTypeName;
                        pt.FatherId = list.Items[i].FatherId;
                        pt.PicUrl = ResPicAccessor.Instance.Get(list.Items[0].PicId).PicUrl;
                        ptlist.Add(pt);
                    }
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = ptlist;
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
        /// 根据产品ID获取产品信息
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public JsonResult GetProductInfoByPID(int pid)
        {
            var Res = new JsonResult();
            AdvancedResult<ProProduction> result = new AdvancedResult<ProProduction>();
            try
            {
                result.Data = ProProductionAccessor.Instance.Get(pid);
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
        /// 添加入库单
        /// </summary>
        /// <param name="ppd"></param>
        /// <returns></returns>
        public JsonResult AddProductonDetail(ProProductonDetail ppd)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();
            try
            {
                result.Id=ProProductonDetailAccessor.Instance.Insert(ppd);
                result.Error =result.Id >0 ? AppError.ERROR_SUCCESS : AppError.ERROR_FAILED;
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
        /// 获取产品入库单列表
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public JsonResult SearchProductonDetailList(int pid, int pageIndex, int pageSize, string token)
        {
            var Res = new JsonResult();
            AdvancedResult<PageEntity<ProProductonDetail>> result = new AdvancedResult<PageEntity<ProProductonDetail>>();
            try
            {
                if (CacheManagerFactory.GetMemoryManager().Contains(token))
                {
                    int userid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));

                    SysUser user = SysUserAccessor.Instance.Get(userid);

                    PageEntity<ProProductonDetail> list = new PageEntity<ProProductonDetail>();
                    list = ProProductonDetailAccessor.Instance.Search(user.UserId, pid, user.EntId, pageIndex, pageSize);
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = list;
                }
                else
                    result.Error = AppError.ERROR_FAILED;
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
        ///添加产品
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public JsonResult AddProduction(ProProduction pro)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();
            try
            {
                result.Id = ProProductionAccessor.Instance.Insert(pro);
                result.Error = result.Id > 0 ? AppError.ERROR_SUCCESS : AppError.ERROR_FAILED;
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
        ///添加产品分类
        /// </summary>
        /// <param name="ptype"></param>
        /// <returns></returns>
        public JsonResult AddProductionType(ProProductionType ptype)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();
            try
            {
                result.Id = ProProductionTypeAccessor.Instance.Insert(ptype);
                result.Error = result.Id > 0 ? AppError.ERROR_SUCCESS : AppError.ERROR_FAILED;
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
        /// 删除产品
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public JsonResult DeleteProduction(int pid)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();
            try
            {
                result.Error = ProProductionAccessor.Instance.Delete(pid) ? AppError.ERROR_SUCCESS : AppError.ERROR_FAILED;
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

        #endregion

    }
}
