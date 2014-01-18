/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/12/28 11:50:00
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
    public class SysEntExtra
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 IdsysEntExtra
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 EntId
        { get; set; }

        /// <summary>
        /// 企业法人
        /// </summary>
        public String ArtificialPerson
        { get; set; }

        /// <summary>
        /// 注册资金
        /// </summary>
        public String RegisteredCapital
        { get; set; }

        /// <summary>
        /// 成立日期
        /// </summary>
        public DateTime DateOfEstablishment
        { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public String Address
        { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public String Province
        { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public String City
        { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactPhone
        { get; set; }

        /// <summary>
        /// 官方网站
        /// </summary>
        public String Web
        { get; set; }

        /// <summary>
        /// 微博
        /// </summary>
        public String Weibo
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Weixin
        { get; set; }

        /// <summary>
        /// 主营业务
        /// </summary>
        public String MainBusiness
        { get; set; }


        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public SysEntExtra BuildSampleEntity(IDataReader reader)
        {
            this.IdsysEntExtra = DBConvert.ToInt32(reader["idsys_ent_extra"]);
            this.EntId = DBConvert.ToInt32(reader["ent_id"]);
            this.ArtificialPerson = DBConvert.ToString(reader["artificial_person"]);
            this.RegisteredCapital = DBConvert.ToString(reader["registered_capital"]);
            this.DateOfEstablishment = DBConvert.ToDateTime(reader["date_of_establishment"]);
            this.Address = DBConvert.ToString(reader["address"]);
            this.Province = DBConvert.ToString(reader["province"]);
            this.City = DBConvert.ToString(reader["city"]);
            this.ContactPhone = DBConvert.ToString(reader["contact_phone"]);
            this.Web = DBConvert.ToString(reader["web"]);
            this.Weibo = DBConvert.ToString(reader["weibo"]);
            this.Weixin = DBConvert.ToString(reader["weixin"]);
            this.MainBusiness = DBConvert.ToString(reader["main_business"]);
            return this;
        }
    }
}