using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Threading;
using System.Net;
using System.IO;

namespace YMGS.Framework
{
    public class MailHelper
    {
        public static void SendMailInThread(string from, NotificationMail mail)
        {
            using(IMailSender daMail = new MailSender())
            {
                BzMail.SendMailInThread(from, mail, daMail);
            }
        }

        public static void SendMail(string from, NotificationMail mail)
        {
            using (IMailSender daMail = new MailSender())
            {
                BzMail.SendMail(from, mail, daMail);
            }
        }

        public static void SendMultiMailInThread(string from, NotificationMail mail)
        {
            using (IMailSender daMail = new MailSender())
            {
                BzMail.SendMultiMailInThread(from, mail, daMail);
            }
        }

        public static void SendMailMulti(string from, NotificationMail mail)
        {
            using (IMailSender daMail = new MailSender())
            {
                BzMail.SendMultiMailInThread(from, mail, daMail);
            }
        }
    }
}
