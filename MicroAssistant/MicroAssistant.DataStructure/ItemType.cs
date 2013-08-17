using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroAssistant.DataStructure
{
    /// <summary>
    /// 宝贝类别
    /// </summary>
    public enum ItemType
    {
        /// <summary>
        /// 不考虑类别
        /// </summary>
        Ignore = 0,
        /// <summary>
        /// 衣服
        /// </summary>
        Clothes=1,
        /// <summary>
        /// 玩具
        /// </summary>
        Toys=2,
        /// <summary>
        /// 其他
        /// </summary>
        Others=3
    }
}
