// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Email.Core
{
    /// <summary>
    /// 邮件帮助类
    /// </summary>
    public class EmailHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="host"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="Subject"></param>
        /// <param name="content"></param>
        /// <param name="fromEmail"></param>
        /// <param name="toEmail"></param>
        /// <param name="port"></param>
        /// <param name="isHtml"></param>
        /// <param name="enableSsl"></param>
        /// <param name="fromName"></param>
        public static async Task SendEmail(string host,
            string userName,
            string password,
            string Subject, 
            string content,
            string fromEmail,
            string toEmail,
            int port=25,
            bool isHtml=false,
            bool enableSsl=true,
            string fromName=null)
        {
            var MailMessage_Mai = new MailMessage();

            SmtpClient mail = new SmtpClient();
            //发送方式
            mail.DeliveryMethod = SmtpDeliveryMethod.Network;
            //初始化Ｓmtp服务器信息,
            mail.Host = host;
            //端口
            mail.Port = port;
            mail.EnableSsl = enableSsl;
            //验证发件邮箱地址和密码
            mail.Credentials = new System.Net.NetworkCredential(userName, password);

            if (!string.IsNullOrEmpty(Subject))
            {
                //邮件主题
                MailMessage_Mai.Subject = Subject;
                MailMessage_Mai.SubjectEncoding = Encoding.UTF8;
            }
            MailMessage_Mai.Headers.Add("X-Priority", "3");

            MailMessage_Mai.Headers.Add("X-MSMail-Priority", "Normal");
            //本文以outlook名义发送邮件，不会被当作垃圾邮件
            MailMessage_Mai.Headers.Add("X-Mailer", "Microsoft Outlook Express 6.00.2900.2869");
            MailMessage_Mai.Headers.Add("X-MimeOLE", "Produced By Microsoft MimeOLE V6.00.2900.2869");
            MailMessage_Mai.Headers.Add("ReturnReceipt", "1");
            //邮件正文
            MailMessage_Mai.Body = content;
            MailMessage_Mai.BodyEncoding = Encoding.UTF8;
            //HTML编译
            MailMessage_Mai.IsBodyHtml = isHtml;
            //级别
            MailMessage_Mai.Priority = MailPriority.High;
            MailAddress fromMailAddress = new MailAddress(fromEmail, fromName);
            //发件人邮箱
            MailMessage_Mai.From = fromMailAddress;


            //收件人地址
            if (!string.IsNullOrEmpty(toEmail))
            {
                MailAddress   mailAddressTo = new MailAddress(toEmail);
                MailMessage_Mai.To.Add(mailAddressTo);

            }
           await mail.SendMailAsync(MailMessage_Mai);
        }
    }
}
