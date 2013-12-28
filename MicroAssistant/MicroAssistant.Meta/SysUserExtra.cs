/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/12/28 11:58:32
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
    public class SysUserExtra
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 IdsysUserExtra
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Diploma
        { get; set; }
        
        /// <summary>
        ///  
        /// </summary>
        public String School
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Major
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime GraduationTime
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Detail
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 SysUserId
        { get; set; }


        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public SysUserExtra BuildSampleEntity(IDataReader reader)
        {
            this.IdsysUserExtra = DBConvert.ToInt32(reader["idsys_user_extra"]);
            this.Diploma = DBConvert.ToString(reader["diploma"]);
            this.School = DBConvert.ToString(reader["school"]);
            this.Major = DBConvert.ToString(reader["major"]);
            this.GraduationTime = DBConvert.ToDateTime(reader["graduation_time"]);
            this.Detail = DBConvert.ToString(reader["detail"]);
            this.SysUserId = DBConvert.ToInt32(reader["sys_user_id"]);
            return this;
        }
    }
}