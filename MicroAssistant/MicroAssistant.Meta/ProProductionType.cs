/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/6/14 19:00:10
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
    public class ProProductionType
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 PTypeId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String PTypeName
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
        public ProProductionType BuildSampleEntity(IDataReader reader)
        {
            this.PTypeId = DBConvert.ToInt32(reader["p_type_id"]);
            this.PTypeName = DBConvert.ToString(reader["p_type_name"]);
            this.UserId = DBConvert.ToInt32(reader["user_id"]);
            return this;
        }
    }
}