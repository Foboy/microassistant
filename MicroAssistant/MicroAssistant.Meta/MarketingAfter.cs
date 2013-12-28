/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/12/28 12:28:00
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
    public class MarketingAfter
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 IdmarketingAfter
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 ChanceId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 ContractId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Detail
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 EntId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime AddTime
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String OpUserId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String OpUserName
        { get; set; }


        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public MarketingAfter BuildSampleEntity(IDataReader reader)
        {
            this.IdmarketingAfter = DBConvert.ToInt32(reader["idmarketing_after"]);
            this.ChanceId = DBConvert.ToInt32(reader["chance_id"]);
            this.ContractId = DBConvert.ToInt32(reader["contract_id"]);
            this.Detail = DBConvert.ToString(reader["detail"]);
            this.EntId = DBConvert.ToInt32(reader["ent_id"]);
            this.AddTime = DBConvert.ToDateTime(reader["add_time"]);
            this.OpUserId = DBConvert.ToString(reader["op_user_id"]);
            this.OpUserName = DBConvert.ToString(reader["op_user_name"]);
            return this;
        }
    }
}