using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroAssistant.DataStructure
{
    public enum StateType
    {
        /// <summary>
        /// 不考虑类别
        /// </summary>
        Ignore=0,
        /// <summary>
        /// 启用
        /// </summary>
        Active = 1,
        /// <summary>
        /// 禁用
        /// </summary>
        UnActive = 2
    }
}
