/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/10/19 14:53:13
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
        public string EntCode
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 UserId
        { get; set; }

        /// <summary>
        /// 1:男 2：女
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
        public Int32 EntAdminId
        { get; set; }

        /// <summary>
        /// 2:不可用 1：可用
        /// </summary>
        public Int32 IsEnable
        { get; set; }

        /// <summary>
        /// 1：普通用户 2：企业用户
        /// </summary>
        public Int32 Type
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 EntId
        { get; set; }

        public Int32 RoleId
        { get; set; }

     

        public int PicId
        { get; set; }

        //登陆后用户权限
        public List<SysFunction> userFuns
        { get; set; }


        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public SysUser BuildSampleEntity(IDataReader reader)
        {
            this.EntCode = DBConvert.ToString(reader["ent_code"]);
            this.UserId = DBConvert.ToInt32(reader["user_id"]);
            this.UserName = DBConvert.ToString(reader["user_name"]);
            this.UserAccount = DBConvert.ToString(reader["user_account"]);
            this.Pwd = DBConvert.ToString(reader["pwd"]);
            this.Mobile = DBConvert.ToString(reader["mobile"]);
            this.Email = DBConvert.ToString(reader["email"]);
            this.CreateTime = DBConvert.ToDateTime(reader["create_time"]);
            this.EndTime = DBConvert.ToDateTime(reader["end_time"]);
            this.EntAdminId = DBConvert.ToInt32(reader["ent_admin_id"]);
            this.IsEnable = DBConvert.ToInt32(reader["is_enable"]);
            this.Type = DBConvert.ToInt32(reader["type"]);
            this.EntId = DBConvert.ToInt32(reader["ent_id"]);
            if (reader["birthday"] != null)
                this.Birthday = DBConvert.ToDateTime(reader["birthday"]);
            this.Sex = DBConvert.ToInt32(reader["sex"]);
            this.PicId = DBConvert.ToInt32(reader["pic_id"]);
            return this;
        }
        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public SysUser BuildUserRoleEntity(IDataReader reader)
        {
            this.EntCode = DBConvert.ToString(reader["ent_code"]);
            this.UserId = DBConvert.ToInt32(reader["user_id"]);
            this.UserName = DBConvert.ToString(reader["user_name"]);
            this.UserAccount = DBConvert.ToString(reader["user_account"]);
            this.Pwd = DBConvert.ToString(reader["pwd"]);
            this.Mobile = DBConvert.ToString(reader["mobile"]);
            this.Email = DBConvert.ToString(reader["email"]);
            this.CreateTime = DBConvert.ToDateTime(reader["create_time"]);
            this.EndTime = DBConvert.ToDateTime(reader["end_time"]);
            this.EntAdminId = DBConvert.ToInt32(reader["ent_admin_id"]);
            this.IsEnable = DBConvert.ToInt32(reader["is_enable"]);
            this.Type = DBConvert.ToInt32(reader["type"]);
            this.EntId = DBConvert.ToInt32(reader["ent_id"]);
            this.RoleId = DBConvert.ToInt32(reader["role_id"]);
            if (reader["birthday"] != null)
                this.Birthday = DBConvert.ToDateTime(reader["birthday"]);
            this.Sex = DBConvert.ToInt32(reader["sex"]);
            this.PicId = DBConvert.ToInt32(reader["pic_id"]);
            return this;
        }
    }
}