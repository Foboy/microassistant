/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/6/14 19:00:42
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
        /// 
        /// </summary>
        public Double Price
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 PNum
        { get; set; }

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        public Int32 Pid
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
            this.Pid = DBConvert.ToInt32(reader["pid"]);
            return this;
        }
    }
}