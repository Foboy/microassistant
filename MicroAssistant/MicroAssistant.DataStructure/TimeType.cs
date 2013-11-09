using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroAssistant.DataStructure
{
    public enum TimeType
    {
        /// <summary>
        /// 不考虑类别
        /// </summary>
        Ignore = 0,
        /// <summary>
        /// 本月
        /// </summary>
        month = 1,
        /// <summary>
        /// 季度
        /// </summary>
        quarter = 2,
        /// <summary>
        /// 年
        /// </summary>
        year = 4
    }
}
