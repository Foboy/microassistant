/**
 * @author yangchao
 * @email:aaronyangchao@gmail.com
 * @date: 2013/8/31 14:51:39
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
    public class ProProductionType
    {
        /// <summary>
        /// 产品类型ID
        /// </summary>
        public Int32 PTypeId
        { get; set; }

        /// <summary>
        ///产品类型名称 
        /// </summary>
        public String PTypeName
        { get; set; }

        /// <summary>
        /// 企业ID
        /// </summary>
        public Int32 EntId
        { get; set; }

        /// <summary>
        /// 产品分类父类ID
        /// </summary>
        public Int32 FatherId
        { get; set; }

        /// <summary>
        /// 产品分类图片
        /// </summary>
        public Int32 PicId
        { get; set; }

        public ResPic Pic
        { get; set; }


        /// <summary>
        /// 从读取器向完整实例对象赋值
        /// </summary>/// <param name="reader">数据读取器</param>
        /// <returns>返回本对象实例</returns>
        public ProProductionType BuildSampleEntity(IDataReader reader)
        {
            this.PTypeId = DBConvert.ToInt32(reader["p_type_id"]);
            this.PTypeName = DBConvert.ToString(reader["p_type_name"]);
            this.EntId = DBConvert.ToInt32(reader["ent_id"]);
            this.FatherId = DBConvert.ToInt32(reader["father_id"]);
            this.PicId = DBConvert.ToInt32(reader["pic_id"]);
            return this;
        }
    }
}