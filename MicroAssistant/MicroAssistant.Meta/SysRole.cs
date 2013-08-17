/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/5/21 11:35:26
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
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public SysRole BuildSampleEntity(IDataReader reader)
        {
            this.RoleId = DBConvert.ToInt32(reader["role_id"]);
            this.RoleName = DBConvert.ToString(reader["role_name"]);
            return this;
        }
    }
}