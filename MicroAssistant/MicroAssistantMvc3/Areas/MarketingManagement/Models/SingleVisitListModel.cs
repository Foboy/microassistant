using MicroAssistant.Common;
using MicroAssistant.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroAssistantMvc3.Areas.MarketingManagement.Models
{
    public class SingleVisitListModel
    {
        public PageEntity<MarketingVisit> Vlist
        { get; set; }
        public int Rate
        { get; set; }
    }
}