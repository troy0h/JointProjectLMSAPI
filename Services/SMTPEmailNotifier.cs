using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Polly;
using Serilog;
using System.Configuration;

namespace JointProjectLMSAPI.Services
{
    public class SMTPEmailNotifier : IEmailNotifier
    {
        public async Task SendEmailAsync(string Sender, string Recipient, string Subject, string Body)
        {
            SmtpClient smtpClient = new("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(ConfigurationManager.AppSettings["Email"],
                                                    ConfigurationManager.AppSettings["Password"]),
                EnableSsl = true,
            };

            MailMessage message = new MailMessage(Sender, Recipient, Subject, Body);

            await smtpClient.SendMailAsync(message);
        }
    }
}
