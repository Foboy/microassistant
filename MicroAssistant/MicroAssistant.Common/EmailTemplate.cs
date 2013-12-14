using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroAssistant.Common
{
    public class EmailTemplate
    {
        private static string ForgotPwdTemplate = "<div id=\"mailContentContainer\" class=\"qmbox\" style=\"height: auto; min-height: 100px; word-wrap: break-word; font-size: 14px; padding: 0px; font-family: 'lucida Grande', Verdana;\">" +
                       " <table style=\"border-collapse:collapse;border:1px solid #CCC;width:590px;font-size:12px;line-height:1.6;background:#ffffff;\">" +
                            "<tbody>" + "<tr>" +
                                "<td>" + "<img src=\"http://p.xzhushou.com/www/img/logo1.png\">" + "</td>" +
                            "</tr>" +
                            "<tr>" +
                                "<td>" +
                                    "<div style=\"padding:50px 30px 0;color:#333333;\">" +
                                       " <div style=\"margin:0;font-size:14px;padding-top:0;padding-bottom:20px;font-weight:bold;\">亲爱的" + "<a href=\"mailto:" + "{0}" + "\" target=\"_blank\">" + "{0}" + "</a>：</div>" +
                                        "<p style=\"margin:0;text-indent:24px;font-size:12px;padding-top:0;padding-bottom:20px;\">欢迎使用企业助手找回密码功能。</p>" +
                                        "<p style=\"margin:0;text-indent:24px;font-size:12px;padding-top:0;padding-bottom:20px;\">您此次找回密码的链接是：<a href=\"{1}\">点击此链接修改</a>，请在<b>30分钟内</b>找回密码</p>" +
                                        "<p style=\"margin:0;text-indent:24px;font-size:12px;padding-top:0;padding-bottom:20px;\">如果您并未发过此请求，则可能是因为其他用户在尝试重设密码时误输入了您的电子邮件地址而使您收到这封邮件，那么您可以放心的忽略此邮件，无需进一步采取任何操作。</p>" +
                                       " <p style=\"margin:0;text-indent:24px;font-size:12px;padding-top:0;padding-bottom:20px;\">此致</p>" +
                                        "<p style=\"margin:0;text-indent:24px;font-size:12px;padding-top:0;\">企业助手全体员工敬上</p>" +
                                        "<p style=\"margin:0;text-indent:24px;font-size:12px;padding-top:0;\"><span style=\"border-bottom:1px dashed #ccc;\" t=\"5\" times=\"\">" + System.DateTime.Now.ToString("d") + "</span></p>" +
                                    "</div>" +
                                    "<div style=\"margin:0;padding:35px 30px 60px;color:#999999;font-size:12px;\">（请注意，该电子邮件地址不接受回复邮件)<br>" +
                         "</div>" +
                                "</td>" +
                            "</tr>" +
                       " </tbody></table>" +
                      "</div>";

        /// <summary>
        /// 获取邮件模板
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public static string GetEmailTemplate(EmailType Type)
        {
            string template = string.Empty;
            switch (Type)
            {
                case EmailType.ForgotPwd:
                    template = ForgotPwdTemplate;
                    break;
                default:
                    break;
            }
            return template;
        }
    }
}
