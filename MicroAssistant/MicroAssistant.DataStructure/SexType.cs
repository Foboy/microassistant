using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroAssistant.DataStructure
{
    public enum SexType
    {
        /// <summary>
        /// 不考虑性别因数，查询时
        /// </summary>
        Ignore=0,
        /// <summary>
        /// 小公子
        /// </summary>
        Man=1,
        /// <summary>
        /// 小公主
        /// </summary>
        Woman=2
    }
}
