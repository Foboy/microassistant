using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroAssistant.DataStructure
{
    public enum PicType
    {
        /// <summary>
        /// 不考虑
        /// </summary>
        Ignore=0,
        /// <summary>
        /// 产品分类图片
        /// </summary>
        ProTypePicture = 1,
        /// <summary>
        /// 用户头像
        /// </summary>
        UserHeadImg=2,
        /// <summary>
        /// 合同附件
        /// </summary>
        ContractFile = 3
    }
}
