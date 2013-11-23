using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroAssistant.DataStructure
{
    public enum UserType
    {
        /// <summary>
        /// 不考虑类别
        /// </summary>
        Ignore = 0,
        /// <summary>
        /// 老板
        /// </summary>
        Boss = 1,
        /// <summary>
        /// 管理员
        /// </summary>
        Admin = 2,
        /// <summary>
        /// 普通用户
        /// </summary>
        User =3
    }
}
