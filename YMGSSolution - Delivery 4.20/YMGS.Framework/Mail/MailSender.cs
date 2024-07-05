using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.Net;

namespace YMGS.Framework
{
    public class MailSender : IMailSender
    {
        public MailSender() { }
        public static string EncodeHeader(string header) {
            UTF8Encoding enc = new UTF8Encoding();
            string encodedFullName = Convert.ToBase64String(enc.GetBytes(header));
            return string.Format("=?UTF-8?B?{0}?=", encodedFullName);
        }

        public virtual void Send(MailMessage message) {
            string server = ConfigurationManager.AppSettings[CommConstant.SmtpServerKey];
            string login = ConfigurationManager.AppSettings[CommConstant.SmtpLoginKey];
            string password = ConfigurationManager.AppSettings[CommConstant.SmtpPasswordKey];
            SmtpClient client = new SmtpClient(server);
            NetworkCredential credential = new NetworkCredential(login, password);

            bool authenticationOverride = (null != login) && (login.Length > 0);
            if (authenticationOverride) {
                client.UseDefaultCredentials = false;
                client.Credentials = credential;
            }
            client.Send(message);
        }

        public void Dispose() {}
    }
}
