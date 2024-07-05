using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace YMGS.Framework
{
    public interface IMailSender : IDisposable
    {
        void Send(MailMessage message);
    }
}
