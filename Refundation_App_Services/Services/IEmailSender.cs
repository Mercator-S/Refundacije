using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refundation_App_Services.Services
{
    public interface IEmailSender
    {
        void sendMail(string sendFrom, string sendTo, string subject, string body, bool isBodyHtml);
    }
}
