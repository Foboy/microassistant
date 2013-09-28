using MicroAssistant.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicroAssistantMvc.Areas.ContractManagement.Controllers
{
    public class ContractInfoController : Controller
    {
        //
        // GET: /ContractManagement/ContractInfo/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 根据合同ID获取合同信息
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public JsonResult GetContractInfoByEID(int eid)
        {
            return null;
        }
        /// <summary>
        /// 添加合同
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public JsonResult AddContractInfo(ContractInfo contract)
        {
            return null;
        }
        /// <summary>
        /// 上传合同附件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public JsonResult UploadContractAttachedFile(object file)
        {
            return null;
        }


    }
}
