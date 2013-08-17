/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/5/21 12:16:10
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
    public class SysFunction
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 IdsysFunction
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String FunctionName
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 FatherId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Mark
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String FunctionUrl
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String FunctionCode
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 Level
        { get; set; }


        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public SysFunction BuildSampleEntity(IDataReader reader)
        {
            this.IdsysFunction = DBConvert.ToInt32(reader["idsys_function"]);
            this.FunctionName = DBConvert.ToString(reader["function_name"]);
            this.FatherId = DBConvert.ToInt32(reader["father_id"]);
            this.Mark = DBConvert.ToString(reader["mark"]);
            this.FunctionUrl = DBConvert.ToString(reader["function_url"]);
            this.FunctionCode = DBConvert.ToString(reader["function_code"]);
            this.Level = DBConvert.ToInt32(reader["level"]);
            return this;
        }
    }
}