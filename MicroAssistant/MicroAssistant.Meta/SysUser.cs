/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/6/15 10:35:36
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
    public class SysUser
    {
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
        /// 
        /// </summary>
        public String UserAccount
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Pwd
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Mobile
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Email
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime EndTime
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 FatherId
        { get; set; }


        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public SysUser BuildSampleEntity(IDataReader reader)
        {
            this.UserId = DBConvert.ToInt32(reader["user_id"]);
            this.UserName = DBConvert.ToString(reader["user_name"]);
            this.UserAccount = DBConvert.ToString(reader["user_account"]);
            this.Pwd = DBConvert.ToString(reader["pwd"]);
            this.Mobile = DBConvert.ToString(reader["mobile"]);
            this.Email = DBConvert.ToString(reader["email"]);
            this.CreateTime = DBConvert.ToDateTime(reader["create_time"]);
            this.EndTime = DBConvert.ToDateTime(reader["end_time"]);
            this.FatherId = DBConvert.ToInt32(reader["father_id"]);
            return this;
        }
    }
}