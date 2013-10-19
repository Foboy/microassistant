/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/10/19 10:30:40
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
    public class MarketingVisit
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 IdmarketingVisit
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 VisitType
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Double Amount
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Address
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Remark
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime VisitTime
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 ChanceId
        { get; set; }


        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public MarketingVisit BuildSampleEntity(IDataReader reader)
        {
            this.IdmarketingVisit = DBConvert.ToInt32(reader["idmarketing_visit"]);
            this.VisitType = DBConvert.ToInt32(reader["visit_type"]);
            this.Amount = DBConvert.ToDouble(reader["amount"]);
            this.Address = DBConvert.ToString(reader["address"]);
            this.Remark = DBConvert.ToString(reader["remark"]);
            this.VisitTime = DBConvert.ToDateTime(reader["visit_time"]);
            this.ChanceId = DBConvert.ToInt32(reader["chance_id"]);
            return this;
        }
    }
}