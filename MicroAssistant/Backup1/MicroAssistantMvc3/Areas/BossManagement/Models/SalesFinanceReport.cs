using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroAssistantMvc.Areas.BossManagement.Models
{
    public class SalesFinanceReport
    {
        public string month
        { get; set; }
        public double paydone
        { get; set; }
        public double notpay
        { get; set; }
        public double received
        { get; set; }
        public double notreceive
        { get; set; }
    }
}