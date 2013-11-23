using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroAssistantMvc.Areas.FinancialManagement.Models
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
          /// 合同承办人
          /// </summary>
        public string OpUser
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

          /// <summary>
          /// 是否全部收完
          /// </summary>
        public bool IsAllRec
        { get; set; }
    }
}