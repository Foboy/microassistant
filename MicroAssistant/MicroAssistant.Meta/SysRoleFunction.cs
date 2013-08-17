/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/5/21 11:34:51
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
    public class SysRoleFunction
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 SysRoleFunctionId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 RoleId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 FunctionId
        { get; set; }


        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public SysRoleFunction BuildSampleEntity(IDataReader reader)
        {
            this.SysRoleFunctionId = DBConvert.ToInt32(reader["sys_role_function_id"]);
            this.RoleId = DBConvert.ToInt32(reader["role_id"]);
            this.FunctionId = DBConvert.ToInt32(reader["function_id"]);
            return this;
        }
    }
}