using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MicroAssistant.Common;
using MicroAssistant.DataAccess;
using MicroAssistant.DataStructure;
using MicroAssistant.Meta;
using MicroAssistant.Cache;
using MicroAssistantMvc.Controllers;

namespace MicroAssistantMvc.Areas.CustomerManagement.Controllers
{
    public class CustomerController : MicControllerBase
    {
        //
        // GET: /CustomerManagement/Customer/

        /// <summary>
        /// 通过销售人员ID获取销售的企业客户
        /// </summary>
        /// <param name="ownerid"></param>
        /// <returns></returns>
        public JsonResult SearchCustomerEntByOwnerId(int pageIndex, int pageSize)
        {
            var Res = new JsonResult();
            AdvancedResult<PageEntity<CustomerEnt>> result = new AdvancedResult<PageEntity<CustomerEnt>>();
            if (CacheManagerFactory.GetMemoryManager().Contains(token))
            {
                int ownerid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                try
                {
                    PageEntity<CustomerEnt> list = new PageEntity<CustomerEnt>();
                    list = CustomerEntAccessor.Instance.SearchCustomerEntByOwnerId(ownerid,pageIndex,pageSize);
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = list;

                }
                catch (Exception e)
                {
                    result.Error = AppError.ERROR_FAILED;
                    result.ExMessage = e.ToString();
                }

                result.Error = AppError.ERROR_SUCCESS;
            }
            else
            {
                result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
            }


            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }
        //根据用户ID查询个人客户（token）返回 个人客户列表（姓名，年龄，所属行业，所在地，联系方式）
        public JsonResult SearchCustomerPrivByOwnerId(int pageIndex, int pageSize)
        {
            var Res = new JsonResult();
            AdvancedResult<PageEntity<CustomerPrivate>> result = new AdvancedResult<PageEntity<CustomerPrivate>>();
            if (CacheManagerFactory.GetMemoryManager().Contains(token))
            {
                int ownerid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                try
                {
                    PageEntity<CustomerPrivate> list = new PageEntity<CustomerPrivate>();
                    list = CustomerPrivateAccessor.Instance.SearchCustomerPrivByOwnerId(ownerid,pageIndex,pageSize);
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = list;

                }
                catch (Exception e)
                {
                    result.Error = AppError.ERROR_FAILED;
                    result.ExMessage = e.ToString();
                }
            }
            else
            {
                result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
            }
            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }
        //添加修改企业客户（企业名称，所属行业，联系人{姓名，手机，座机，email,qq}，企业地址，企业资料,客户ID,token）
        public JsonResult AddEntCustomer(int entid, string entName, string industy, string contactUsername, string contactMobile, string phone, string email, string qq, string address, string Detail)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();
            int _entid = 0;
            if (CacheManagerFactory.GetMemoryManager().Contains(token))
            {
                int ownerid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                try
                {
                    CustomerEnt ce = new CustomerEnt();
                    //通过CustomerEntModel序列化为实体
                    ce.OwnerId = Convert.ToInt32(ownerid);
                    ce.Address = address;
                    ce.EntName = entName;
                    ce.Industy = industy;
                    ce.ContactUsername = contactUsername;
                    ce.ContactMobile = contactMobile;
                    ce.ContactPhone = phone;
                    ce.ContactEmail = email;
                    ce.ContactQq = qq;
                    ce.Detail = Detail;
                    if (entid == 0)
                    {
                        _entid = CustomerEntAccessor.Instance.Insert(ce);
                    }
                    else
                    {
                        _entid = entid;

                        ce.EntId = _entid;
                        CustomerEntAccessor.Instance.Update(ce);
                    }
                    result.Error = AppError.ERROR_SUCCESS;


                }
                catch (Exception e)
                {
                    result.Error = AppError.ERROR_FAILED;
                    result.ExMessage = e.ToString();
                }
            }
            else
            {
                result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
            }



            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }
        //添加修改个人客户（姓名，性别，出生日期，联系方式{},所属行业，所在地，个人信息，token）返回（true/false）
        public JsonResult AddPrivateCustomer(int pivid, string name, int sex, DateTime birthday, string contactMobile, string phone, string email, string qq, string address, string detail)
        {
            var Res = new JsonResult();
            RespResult result = new RespResult();

            int _pivid = 0;
            if (CacheManagerFactory.GetMemoryManager().Contains(token))
            {
                int ownerid = Convert.ToInt32(CacheManagerFactory.GetMemoryManager().Get(token));
                try
                {
                    CustomerPrivate cp = new CustomerPrivate();
                    cp.Name = name;
                    cp.Sex = sex;
                    cp.Birthday = birthday;
                    cp.Mobile = contactMobile;
                    cp.Phone = phone;
                    cp.Email = email;
                    cp.Qq = qq;
                    cp.Detail = detail;
                    cp.Address = address;

                    cp.OwnerId = Convert.ToInt32(ownerid);
                    if (pivid == 0)
                    {
                        _pivid = CustomerPrivateAccessor.Instance.Insert(cp);
                    }
                    else
                    {
                        _pivid = pivid;

                        cp.CustomerPrivateId = _pivid;
                        CustomerPrivateAccessor.Instance.Update(cp);
                    }
                    result.Error = AppError.ERROR_SUCCESS;


                }
                catch (Exception e)
                {
                    result.Error = AppError.ERROR_FAILED;
                    result.ExMessage = e.ToString();
                }
            }
            else
            {
                result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
            }
            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }

        // 根据联系人名称查询企业客户（联系人名称，token）返回（联系方式，企业资料）
        public JsonResult SearchCustomerEntByName(string name)
        {
            var Res = new JsonResult();
            AdvancedResult<List<CustomerEnt>> result = new AdvancedResult<List<CustomerEnt>>();
            if (CacheManagerFactory.GetMemoryManager().Contains(token))
            {
                try
                {
                    List<CustomerEnt> list = new List<CustomerEnt>();
                    list = CustomerEntAccessor.Instance.SearchCustomerEntByName(name);
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = list;

                }
                catch (Exception e)
                {
                    result.Error = AppError.ERROR_FAILED;
                    result.ExMessage = e.ToString();
                }
            }
            else
            {
                result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
            }
            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }
        //根据联系人名称查询个人客户（联系人名称，token）返回（联系方式，个人信息）
        public JsonResult SearchCustomerPrivateByName(string name)
        {
            var Res = new JsonResult();
            AdvancedResult<List<CustomerPrivate>> result = new AdvancedResult<List<CustomerPrivate>>();
            if (CacheManagerFactory.GetMemoryManager().Contains(token))
            {
                try
                {
                    List<CustomerPrivate> list = new List<CustomerPrivate>();
                    list = CustomerPrivateAccessor.Instance.SearchCustomerPrivByName(name);
                    result.Error = AppError.ERROR_SUCCESS;
                    result.Data = list;

                }
                catch (Exception e)
                {
                    result.Error = AppError.ERROR_FAILED;
                    result.ExMessage = e.ToString();
                }
            }
            else
            {
                result.Error = AppError.ERROR_PERSON_NOT_LOGIN;
            }
            Res.Data = result;
            Res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return Res;
        }
    }
}
