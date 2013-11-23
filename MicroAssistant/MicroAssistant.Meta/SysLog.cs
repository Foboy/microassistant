/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/11/23 15:29:33
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
    public class SysLog
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 IdsysLog
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 UserId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime AddTime
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Action
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Parameter
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Result
        { get; set; }


        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public SysLog BuildSampleEntity(IDataReader reader)
        {
            this.IdsysLog = DBConvert.ToInt32(reader["idsys_log"]);
            this.UserId = DBConvert.ToInt32(reader["user_id"]);
            this.AddTime = DBConvert.ToDateTime(reader["add_time"]);
            this.Action = DBConvert.ToString(reader["action"]);
            this.Parameter = DBConvert.ToString(reader["parameter"]);
            this.Result = DBConvert.ToString(reader["result"]);
            return this;
        }
    }
}