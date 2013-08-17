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
        /// 宝贝图片
        /// </summary>
        BBPicture = 1,
        /// <summary>
        /// 用户头像
        /// </summary>
        UserHeadImg=2,

        /// <summary>
        /// 投票图片
        /// </summary>
        VotePicture = 3,

        /// <summary>
        /// book图片
        /// </summary>
        Book=4
    }
}
