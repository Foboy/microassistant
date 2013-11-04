/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/11/4 0:06:49
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
    public class SysUserTimemachine
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 IdsysUserTimemachine
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 UserId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String UserName
        { get; set; }

        /// <summary>
        /// 职位名
        /// </summary>
        public String RoleName
        { get; set; }

        /// <summary>
        /// 所在公司名
        /// </summary>
        public String EntName
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 EntId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartTime
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime EndTime
        { get; set; }


        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public SysUserTimemachine BuildSampleEntity(IDataReader reader)
        {
            this.IdsysUserTimemachine = DBConvert.ToInt32(reader["idsys_user_timemachine"]);
            this.UserId = DBConvert.ToInt32(reader["user_id"]);
            this.UserName = DBConvert.ToString(reader["user_name"]);
            this.RoleName = DBConvert.ToString(reader["role_name"]);
            this.EntName = DBConvert.ToString(reader["ent_name"]);
            this.EntId = DBConvert.ToInt32(reader["ent_id"]);
            this.StartTime = DBConvert.ToDateTime(reader["start_time"]);
            this.EndTime = DBConvert.ToDateTime(reader["end_time"]);
            return this;
        }
    }
}