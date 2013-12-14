using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace MicroAssistant.Common
{
    public class EmailHelper
    {
        public static void SendEamil(string EmailBody, string Email)
        {
            MailMessage myMail = new MailMessage();
            myMail.From = new MailAddress(ConfigurationManager.AppSettings["SystemEamilAccount"]);
            myMail.To.Add(new MailAddress(Email));
            myMail.Subject = ConfigurationManager.AppSettings["SystemEmailSubject"];
            myMail.SubjectEncoding = Encoding.UTF8;
            myMail.Body = EmailBody;
            myMail.BodyEncoding = Encoding.UTF8;
            myMail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["SystemQQSMPT"].ToString();
            smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SystemEamilAccount"].ToString(), ConfigurationManager.AppSettings["SystemEamilPwd"].ToString());
            smtp.Send(myMail);
        }
    }
}
