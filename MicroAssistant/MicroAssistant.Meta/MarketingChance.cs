/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/10/19 10:30:15
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
    public class MarketingChance
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 IdmarketingChance
        { get; set; }

        /// <summary>
        /// 机会类型分为：1:新客户机会2:老客户机会；默认新客户机会
        /// </summary>
        public Int32 ChanceType
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 CustomerType
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ContactName
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Remark
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime AddTime
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Qq
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Email
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Tel
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Phone
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 Rate
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 EntId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 UserId
        { get; set; }


        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public MarketingChance BuildSampleEntity(IDataReader reader)
        {
            this.IdmarketingChance = DBConvert.ToInt32(reader["idmarketing_chance"]);
            this.ChanceType = DBConvert.ToInt32(reader["chance_type"]);
            this.CustomerType = DBConvert.ToInt32(reader["customer_type"]);
            this.ContactName = DBConvert.ToString(reader["contact_name"]);
            this.Remark = DBConvert.ToString(reader["remark"]);
            this.AddTime = DBConvert.ToDateTime(reader["add_time"]);
            this.Qq = DBConvert.ToString(reader["qq"]);
            this.Email = DBConvert.ToString(reader["email"]);
            this.Tel = DBConvert.ToString(reader["tel"]);
            this.Phone = DBConvert.ToString(reader["phone"]);
            this.Rate = DBConvert.ToInt32(reader["rate"]);
            this.EntId = DBConvert.ToInt32(reader["ent_id"]);
            this.UserId = DBConvert.ToInt32(reader["user_id"]);
            return this;
        }
    }
}