using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroAssistant.DataStructure
{
    [Flags]
    public enum SocialUserTypeEnum
    {   
        /// <summary>
        /// 不考虑类别
        /// </summary>
        Ignore=0,
        /// <summary>
        /// 新浪账户 
        /// value=1
        /// </summary>
        Sina = 1, 
        
        /// <summary>
        /// 腾讯微博账号 
        /// value=2
        /// </summary>
        QQ=2, 

        /// <summary>
        /// NetEase
        /// value=4
        /// </summary>
        NetEase=4,

        /// <summary>
        /// 搜狐账号
        /// value =8
        /// </summary>
        Sohu = 8
    }
}
