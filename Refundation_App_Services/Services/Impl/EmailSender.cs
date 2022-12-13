using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Refundation_App_Services.Services.Impl
{
    public class EmailSender : IEmailSender
    {
        public void sendMail(string sendFrom, string sendTo, string subject, string body, bool isBodyHtml=true)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.agrokor.hr", 25);
            try
                {
                mail.From = new MailAddress(sendFrom);
                mail.To.Add(sendTo);
                mail.Subject = subject;
                mail.IsBodyHtml = isBodyHtml;
                mail.Body = body;
                SmtpServer.Send(mail);
                }
            catch (SmtpFailedRecipientException exp)
                {
                mail.From = new MailAddress(sendFrom);
                mail.Subject = "Greska";
                mail.Body = "Neuspesno slanje na adresu: " + exp.FailedRecipient;
                SmtpServer.Send(mail);
            }
        }
    }
}
