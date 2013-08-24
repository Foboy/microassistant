using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicroAssistantMvc.Areas.CustomerManagement.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /CustomerManagement/CustomerEnt/

        public JsonResult SearchCustomerEntByOwnerId(int OwnerId)
        {
            return null;
        }
        public JsonResult SearchCustomerPrivByOwnerId(int OwnerId)
        {
            return null;
        }
        public JsonResult AddEntCustomer(string CustomerEntModel, string ownerId)
        {
            return null;
        }

        public JsonResult AddPrivateCustomer(string CustomerPrivModel, string ownerId)
        {
            return null;
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
