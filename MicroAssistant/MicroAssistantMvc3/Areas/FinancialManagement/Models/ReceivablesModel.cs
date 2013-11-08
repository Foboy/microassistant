using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroAssistantMvc3.Areas.FinancialManagement.Models
{
      [Serializable]
    public class ReceivablesModel
    {
        /// <summary>
        /// 合同编号
        /// </summary>
        public String ContractNo
        { get; set; }

        /// <summary>
        /// 合同名称
        /// </summary>
        public String CName
        { get; set; }


        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName
        { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public Double Amount
        { get; set; }

        /// <summary>
        /// 客户应付款时间
        /// </summary>
        public DateTime PayTime
        { get; set; }

        /// <summary>
        /// 收款时间
        /// </summary>
        public DateTime ReceivedTime
        { get; set; }
    }
}