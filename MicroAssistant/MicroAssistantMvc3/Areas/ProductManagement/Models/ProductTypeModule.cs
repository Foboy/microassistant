using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroAssistantMvc.Areas.ProductManagement.Models
{
      [Serializable]
    public class ProductTypeModule
    {
        /// <summary>
        /// 
        /// </summary>
        public String PicUrl
        { get; set; }

        /// <summary>
        /// 产品图片ID
        /// </summary>
        public Int32 PicId
        { get; set; }

        /// <summary>
        ///产品类型名称 
        /// </summary>
        public String PTypeName
        { get; set; }

        /// <summary>
        /// 产品类型ID
        /// </summary>
        public Int32 PTypeId
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

    }
}