/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/6/14 18:59:36
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
    public class ProProduction
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 PId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String PName
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String PInfo
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Unit
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 PTypeId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Double LowestPrice
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Double MarketPrice
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
        public ProProduction BuildSampleEntity(IDataReader reader)
        {
            this.PId = DBConvert.ToInt32(reader["p_id"]);
            this.PName = DBConvert.ToString(reader["p_name"]);
            this.PInfo = DBConvert.ToString(reader["p_info"]);
            this.Unit = DBConvert.ToString(reader["unit"]);
            this.PTypeId = DBConvert.ToInt32(reader["p_type_id"]);
            this.LowestPrice = DBConvert.ToDouble(reader["lowest_price"]);
            this.MarketPrice = DBConvert.ToDouble(reader["market_price"]);
            this.UserId = DBConvert.ToInt32(reader["user_id"]);
            return this;
        }
    }
}