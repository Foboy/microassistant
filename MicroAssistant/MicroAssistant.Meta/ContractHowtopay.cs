/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/8/31 14:46:33
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MicroAssistant.Common;

namespace MicroAssistant.Meta
{
    [Serializable]
    public class ContractHowtopay
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 HowtopayId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 InstalmentsNo
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Double Amount
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime PayTime
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ReceivedTime
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 Isreceived
        { get; set; }


        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public ContractHowtopay BuildSampleEntity(IDataReader reader)
        {
            this.HowtopayId = DBConvert.ToInt32(reader["howtopay_id"]);
            this.InstalmentsNo = DBConvert.ToInt32(reader["instalments_no"]);
            this.Amount = DBConvert.ToDouble(reader["amount"]);
            this.PayTime = DBConvert.ToDateTime(reader["pay_time"]);
            this.ReceivedTime = DBConvert.ToDateTime(reader["received_time"]);
            this.Isreceived = DBConvert.ToInt32(reader["IsReceived"]);
            return this;
        }
    }
}