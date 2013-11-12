using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroAssistant.DataStructure
{
    /// <summary>
    /// 收付款类型 1： 未收款 2：已收款 3：未付款 4：已付款
    /// </summary>
    public enum PayOrRecType
    {
        /// <summary>
        /// 不考虑类别
        /// </summary>
        Ignore = 0,
        /// <summary>
        /// 未付款
        /// </summary>
        notpay = 1,
        /// <summary>
        /// 已付款
        /// </summary>
        paydone = 2,

        /// <summary>
        /// 未收款
        /// </summary>
        notreceive = 4,
        /// <summary>
        /// 已收款
        /// </summary>
        received = 8
    }
}
