/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2012/7/7 21:39:12
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MicroAssistant.Common;
using MicroAssistant.DataStructure;

namespace MicroAssistant.Meta
{
    [Serializable]
    public class ResPic
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 PicId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String PicDescription
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 ObjId
        { get; set; }

        /// <summary>
        /// 0：产品 1：用户头像 2：投票；3：微印书
        /// </summary>
        public PicType ObjType
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String PicUrl
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 PicHeight
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 PicWidth
        { get; set; }

        /// <summary>
        /// 2:禁止 1：可用
        /// </summary>
        public StateType State
        { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime
        {
            get;
            set;
        }


        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public ResPic BuildSampleEntity(IDataReader reader)
        {
            this.PicId = DBConvert.ToInt32(reader["pic_id"]);
            this.PicDescription = DBConvert.ToString(reader["pic_description"]);
            this.ObjId = DBConvert.ToInt32(reader["obj_id"]);
            this.ObjType = (PicType)DBConvert.ToInt32(reader["obj_type"]);
            this.PicUrl = DBConvert.ToString(reader["pic_url"]);
            this.PicHeight = DBConvert.ToInt32(reader["pic_height"]);
            this.PicWidth = DBConvert.ToInt32(reader["pic_width"]);
            this.State = (StateType)DBConvert.ToInt32(reader["state"]);
            this.CreateTime = DBConvert.ToDateTime(reader["create_time"]);
            return this;
        }
    }
}