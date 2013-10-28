using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroAssistantMvc.Areas.MarketingManagement.Models
{
    public class VisitModel
    {
        public Int32 IdmarketingChance { get; set; }

        /// <summary>
        /// 拜访次数
        /// </summary>
        public Int32 VisitNum
        { get; set; }

        public DateTime LastVisitTime
        { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String Remark
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 Rate
        { get; set; }
    }
}