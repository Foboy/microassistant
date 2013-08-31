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
        ///产品ID
        /// </summary>
        public Int32 PId
        { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public String PName
        { get; set; }

        /// <summary>
        /// 产品描述
        /// </summary>
        public String PInfo
        { get; set; }

        /// <summary>
        /// 产品单位
        /// </summary>
        public String Unit
        { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public Int32 PTypeId
        { get; set; }

        ///<summary>
        ///最低售价
        ///</summary>
        public Double LowestPrice
        { get; set; }

        /// <summary>
        /// 市场售价
        /// </summary>
        public Double MarketPrice
        { get; set; }

        /// <summary>
        /// 销售人员ID
        /// </summary>
        public Int32 UserId
        { get; set; }
        /// <summary>
        /// 企业ID
        /// </summary>
        public Int32 EntId
        { get; set; }
        /// <summary>
        /// 库存数量
        /// </summary>
        public Int32 StockCount
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
            this.EntId = DBConvert.ToInt32(reader["ent_id"]);
            this.StockCount = DBConvert.ToInt32(reader["stock_count"]);
            return this;
        }
    }
}