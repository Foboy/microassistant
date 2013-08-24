/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/8/24 14:58:20
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
    public class CustomerEnt
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 CustomerEntId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String EntName
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Industy
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ContactUsername
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ContactMobile
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ContactPhone
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ContactEmail
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ContactQq
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Address
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Detail
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 OwnerId
        { get; set; }


        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public CustomerEnt BuildSampleEntity(IDataReader reader)
        {
            this.CustomerEntId = DBConvert.ToInt32(reader["customer_ent_id"]);
            this.EntName = DBConvert.ToString(reader["ent_name"]);
            this.Industy = DBConvert.ToString(reader["industy"]);
            this.ContactUsername = DBConvert.ToString(reader["contact_username"]);
            this.ContactMobile = DBConvert.ToString(reader["contact_mobile"]);
            this.ContactPhone = DBConvert.ToString(reader["contact_phone"]);
            this.ContactEmail = DBConvert.ToString(reader["contact_email"]);
            this.ContactQq = DBConvert.ToString(reader["contact_qq"]);
            this.Address = DBConvert.ToString(reader["address"]);
            this.Detail = DBConvert.ToString(reader["detail"]);
            this.OwnerId = DBConvert.ToInt32(reader["owner_id"]);
            return this;
        }
    }
}