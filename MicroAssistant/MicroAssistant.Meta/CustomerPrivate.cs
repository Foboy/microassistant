/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/8/28 15:33:44
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
    public class CustomerPrivate : Customer
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 CustomerPrivateId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Name
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 Sex
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Birthday
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Industy
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
        public String Qq
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Phone
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
        public Int32 EntId
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
        public CustomerPrivate BuildSampleEntity(IDataReader reader)
        {
            this.CustomerPrivateId = DBConvert.ToInt32(reader["customer_private_id"]);
            this.Name = DBConvert.ToString(reader["name"]);
            this.Sex = DBConvert.ToInt32(reader["sex"]);
            this.Birthday = DBConvert.ToDateTime(reader["birthday"]);
            this.Industy = DBConvert.ToString(reader["industy"]);
            this.Mobile = DBConvert.ToString(reader["mobile"]);
            this.Email = DBConvert.ToString(reader["email"]);
            this.Qq = DBConvert.ToString(reader["qq"]);
            this.Phone = DBConvert.ToString(reader["phone"]);
            this.Address = DBConvert.ToString(reader["address"]);
            this.Detail = DBConvert.ToString(reader["detail"]);
            this.EntId = DBConvert.ToInt32(reader["ent_id"]);
            this.OwnerId = DBConvert.ToInt32(reader["owner_id"]);
            return this;
        }
    }
}