using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroAssistant.Common;
using MicroAssistant.DataAccess;
using MicroAssistant.DataStructure;
using MicroAssistant.Meta;

namespace MicroAssistantMvc.Areas.CustomerManagement.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /CustomerManagement/CustomerEnt/

        /// <summary>
        /// 通过销售人员ID获取销售的企业客户
        /// </summary>
        /// <param name="ownerid"></param>
        /// <returns></returns>
        public JsonResult SearchCustomerEntByOwnerId(int ownerid)
        {
            var Res = new JsonResult();
            AdvancedResult<List<CustomerEnt>> result = new AdvancedResult<List<CustomerEnt>>();
            try
            {
                List<CustomerEnt> list = new List<CustomerEnt>();
                list = CustomerEntAccessor.Instance.SearchCustomerEntByOwnerId(ownerid);
                result.Error = AppError.ERROR_SUCCESS;
                result.Data = list;

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
        public JsonResult SearchCustomerPrivByOwnerId(int OwnerId)
        {
            var Res = new JsonResult();
            AdvancedResult<List<CustomerPrivate>> result = new AdvancedResult<List<CustomerPrivate>>();
            try
            {
                List<CustomerPrivate> list = new List<CustomerPrivate>();
                list = CustomerPrivateAccessor.Instance.SearchCustomerPrivByOwnerId(OwnerId);
                result.Error = AppError.ERROR_SUCCESS;
                result.Data = list;

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
        public JsonResult AddEntCustomer(string CustomerEntModel, string ownerId)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();
            try
            {
                CustomerEnt ce = new CustomerEnt();
                //通过CustomerEntModel序列化为实体
                ce.OwnerId = Convert.ToInt32(ownerId);
                bool check = CustomerEntAccessor.Instance.Insert(ce);
                if (check)
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

        public JsonResult AddPrivateCustomer(string CustomerPrivModel, string ownerId)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();
            try
            {
                CustomerPrivate cp = new CustomerPrivate();
                //通过CustomerPrivModel序列化为实体
                cp.OwnerId = Convert.ToInt32(ownerId);
                bool check = CustomerPrivateAccessor.Instance.Insert(cp);
                if (check)
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

        public JsonResult EditEntCustomer(string CustomerEntModel, string customerId)
        {
            return null;
        }

        public JsonResult EditPrivCustomer(string CustomerPrivModel, string customerId)
        {
            return null;
        }

    }
}
