using System.Net.Mail;

namespace Refundation_App_Services.Services.Impl
{
    public class EmailSender : IEmailSender
    {
        public void sendMail(string sendFrom, string sendTo, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.agrokor.hr", 25);

            mail.From = new MailAddress(sendFrom);
            mail.To.Add(sendTo);
            mail.Subject = subject;
            mail.IsBodyHtml = isBodyHtml;
            mail.Body = body;

            try
            {
                SmtpServer.Send(mail);
            }
            catch (SmtpFailedRecipientException exp)
            {
                MailMessage mailGreska = new MailMessage();
                SmtpClient SmtpServerGreska = new SmtpClient("smtp.agrokor.hr", 25);
                mailGreska.From = new MailAddress(sendFrom);
                mailGreska.Subject = "Greska";
                mailGreska.Body = "Neuspesno slanje na adresu: " + exp.FailedRecipient;
                SmtpServerGreska.Send(mailGreska);
            }
        }
    }
}
