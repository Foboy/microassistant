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
        public int paydone
        { get; set; }
        public int notpay
        { get; set; }
        public int received
        { get; set; }
        public int notreceive
        { get; set; }
    }
}