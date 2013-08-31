/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/8/31 14:45:26
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
    public class ContractInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 ContractInfoId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ContractNo
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CName
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 CustomerId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartTime
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime EndTime
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 OwnerId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ContractTime
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Double Amount
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 Howtopay
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 HowtopayId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 EntId
        { get; set; }


        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public ContractInfo BuildSampleEntity(IDataReader reader)
        {
            this.ContractInfoId = DBConvert.ToInt32(reader["contract_info_id"]);
            this.ContractNo = DBConvert.ToString(reader["contract_no"]);
            this.CName = DBConvert.ToString(reader["c_name"]);
            this.CustomerId = DBConvert.ToInt32(reader["customer_id"]);
            this.StartTime = DBConvert.ToDateTime(reader["start_time"]);
            this.EndTime = DBConvert.ToDateTime(reader["end_time"]);
            this.OwnerId = DBConvert.ToInt32(reader["owner_id"]);
            this.ContractTime = DBConvert.ToDateTime(reader["contract_time"]);
            this.Amount = DBConvert.ToDouble(reader["amount"]);
            this.Howtopay = DBConvert.ToInt32(reader["howtopay"]);
            this.HowtopayId = DBConvert.ToInt32(reader["howtopay_id"]);
            this.EntId = DBConvert.ToInt32(reader["ent_id"]);
            return this;
        }
    }
}