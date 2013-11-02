/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/11/2 12:56:22
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
    public class SysRole
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 RoleId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String RoleName
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 EntId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 FatherId
        { get; set; }

        public int Count
        { get; set; }


        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public SysRole BuildSampleEntity(IDataReader reader)
        {
            this.RoleId = DBConvert.ToInt32(reader["role_id"]);
            this.RoleName = DBConvert.ToString(reader["role_name"]);
            this.EntId = DBConvert.ToInt32(reader["ent_id"]);
            this.FatherId = DBConvert.ToInt32(reader["father_id"]);
            return this;
        }

        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public SysRole BuildCountEntity(IDataReader reader)
        {
            this.RoleId = DBConvert.ToInt32(reader["role_id"]);
            this.RoleName = DBConvert.ToString(reader["role_name"]);
            this.FatherId = DBConvert.ToInt32(reader["father_id"]);
            this.Count = DBConvert.ToInt32(reader["count"]);
            return this;
        }
    }
}