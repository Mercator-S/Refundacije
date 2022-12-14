namespace Refundation_App_Services.Services
{
    public interface IEmailSender
    {
        void sendMail(string sendFrom, string sendTo, string subject, string body, bool isBodyHtml);
    }
}
