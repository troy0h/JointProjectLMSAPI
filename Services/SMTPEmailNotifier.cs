using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Configuration;

namespace JointProjectLMSAPI.Services
{
    public class SMTPEmailNotifier : IEmailNotifier
    {
        private string Server;
        private int Port;
        private NetworkCredential Credentials;

        public SMTPEmailNotifier()
        {
            Server = "smtp.gmail.com";
            Port = 587;
            Credentials = new NetworkCredential(ConfigurationManager.AppSettings["Email"],
                                                ConfigurationManager.AppSettings["Password"]);
        }

        public async Task SendEmailAsync(string Sender, string Recipient, string Subject, string Body)
        {
            using (SmtpClient client = new SmtpClient(Server))
            {
                client.Port = Port;
                client.Credentials = Credentials;
                client.EnableSsl = true;

                MailMessage message = new MailMessage(Sender, Recipient, Subject, Body);
                await client.SendMailAsync(message);
            }            
        }
    }
}
