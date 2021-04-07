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
        private string _smptServer;
        private int _smtpPort;
        private string _userName;
        private string _password;

        public SMTPEmailNotifier()
        {
            _smptServer = "smtp.gmail.com";
            _smtpPort = 587;
            _userName = ConfigurationManager.AppSettings["Email"];
            _password = ConfigurationManager.AppSettings["Password"];
        }

        public async Task SendEmailAsync(string Recipient, string Sender, string Subject, string Body)
        {
            using (SmtpClient client = new SmtpClient(_smptServer, _smtpPort))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_userName, _password);

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(Sender);
                mailMessage.To.Add(Recipient);
                mailMessage.Body = Body;
                mailMessage.Subject = Subject;
                await client.SendMailAsync(mailMessage);

                await Policy
                    .Handle<Exception>()
                    .WaitAndRetry(3, r => TimeSpan.FromSeconds(2), (ex, ts) => { Log.Error("Error sending mail. Retrying in 2 sec."); })
                    .Execute(() => client.SendMailAsync(mailMessage))
                    .ContinueWith(_ => Log.Information($"Notification mail sent to {Recipient}."));
            }
        }
    }
}
