/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/10/19 15:39:07
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
    public class ProProductonDetail
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 PDId
        { get; set; }

        /// <summary>
        /// 采购单价
        /// </summary>
        public Double Price
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 PNum
        { get; set; }

        /// <summary>
        /// 采购批次号
        /// </summary>
        public String PCode
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 UserId
        { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        public Int32 PId
        { get; set; }

        /// <summary>
        /// 企业ID
        /// </summary>
        public Int32 EntId
        { get; set; }

        /// <summary>
        /// 0:未付款 1:已付款
        /// </summary>
        public Int32 IsPay
        { get; set; }


        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public ProProductonDetail BuildSampleEntity(IDataReader reader)
        {
            this.PDId = DBConvert.ToInt32(reader["p_d_id"]);
            this.Price = DBConvert.ToDouble(reader["price"]);
            this.PNum = DBConvert.ToInt32(reader["p_num"]);
            this.PCode = DBConvert.ToString(reader["p_code"]);
            this.CreateTime = DBConvert.ToDateTime(reader["create_time"]);
            this.UserId = DBConvert.ToInt32(reader["user_id"]);
            this.PId = DBConvert.ToInt32(reader["p_id"]);
            this.EntId = DBConvert.ToInt32(reader["ent_id"]);
            this.IsPay = DBConvert.ToInt32(reader["is_pay"]);
            return this;
        }
    }
}