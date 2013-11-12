using MicroAssistant.Common;
using MicroAssistant.DataStructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MicroAssistant.Meta
{
     [Serializable]
   public class BossFinancial
    {
        /// <summary>
        /// 收付款类型 1： 未收款 2：已收款 3：未付款 4：已付款
        /// </summary>
         public int PType
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
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public BossFinancial BuildSampleEntity(IDataReader reader)
        {
            this.PType = DBConvert.ToInt32(reader["instalments_no"]);
            this.Amount = DBConvert.ToDouble(reader["amount"]);
            this.PayTime = DBConvert.ToDateTime(reader["pay_time"]);
            return this;
        }
    }
}
