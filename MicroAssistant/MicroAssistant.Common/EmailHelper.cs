using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace MicroAssistant.Common
{
    public class EmailHelper
    {
        public static void SendEamil(string EmailBody,string Email)
        {
            MailMessage myMail = new MailMessage();
            myMail.From = new MailAddress(Config.Default["SystemEamilAccount"].ToString());
            myMail.To.Add(new MailAddress(Email));
            myMail.Subject = Config.Default["SystemEmailSubject"].ToString();
            myMail.SubjectEncoding = Encoding.UTF8;
            myMail.Body = "this is a test email from QQ!";
            myMail.BodyEncoding = Encoding.UTF8;
            myMail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = Config.Default["SystemQQSMPT"].ToString();
            smtp.Credentials = new NetworkCredential(Config.Default["SystemEamilAccount"].ToString(), Config.Default["SystemEamilPwd"].ToString());
            smtp.Send(myMail);
        }
    }
}
