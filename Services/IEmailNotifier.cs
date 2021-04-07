using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JointProjectLMSAPI.Services
{
    public interface IEmailNotifier
    {
        Task SendEmailAsync(string to, string from, string subject, string body);
    }
}
