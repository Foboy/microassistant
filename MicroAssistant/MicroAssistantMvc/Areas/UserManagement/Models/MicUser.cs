using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroAssistantMvc.Areas.UserManagement.Models
{
    public class MicUser
    {
        /// <summary>
        /// 
        /// </summary>
        public Int32 UserId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String UserName
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String UserAccount
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Mobile
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Email
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime EndTime
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 EntId
        { get; set; }
    }
}