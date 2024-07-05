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
    public class BzMail
    {
        private class ThreadStarter
        {
            private readonly string fromValue;
            private readonly NotificationMail mailToSendValue;
            private readonly IMailSender daValue;

            public ThreadStarter(string from, NotificationMail mailTosend, IMailSender da)
            {
                if (from == null)
                {
                    throw new ArgumentNullException("from");
                }
                if (mailTosend == null)
                {
                    throw new ArgumentNullException("mailTosend");
                }
                if (da == null)
                {
                    throw new ArgumentNullException("da");
                }

                fromValue = from;
                mailToSendValue = mailTosend;
                daValue = da;
            }

            public void Start()
            {
                SendMail(fromValue, mailToSendValue, daValue);
            }

            public void StartMulti()
            {
                SendMailMulti(fromValue, mailToSendValue, daValue);
            }
        }
        private static int _lettersSent = 0;
        private static int _lettersNotSent = 0;

        public static int LettersSent
        {
            get { return _lettersSent; }
        }

        public static int LettersNotSent
        {
            get { return _lettersNotSent; }
        }

        public static void SendMailInThread(string from, NotificationMail mailTosend, IMailSender da)
        {
            var threadStarter = new ThreadStarter(from, mailTosend, da);
            var thread = new Thread(threadStarter.Start);
            thread.Start();
        }

        public static void SendMail(string from, NotificationMail notification, IMailSender da)
        {
            try
            {
                var mail = new MailMessage
                {
                    From = new MailAddress(from),
                    To = { notification.To },
                    Subject = notification.Subject,
                    Body = notification.Body,
                    IsBodyHtml = notification.IsBodyHtml
                };
                if (!string.IsNullOrEmpty(notification.Cc))
                {
                    mail.CC.Add(notification.Cc);
                }

                foreach (var attachment in notification.Attachments)
                {
                    mail.Attachments.Add(new Attachment(
                        new MemoryStream(attachment.Content),
                        attachment.Name
                    ));
                }

                da.Send(mail);
                _lettersSent += 1;
            }
            catch (FormatException)
            {
                _lettersNotSent += 1;
            }
            catch (ArgumentException)
            {
                _lettersNotSent += 1;
            }
            catch (SmtpException)
            {
                _lettersNotSent += 1;
            }
        }

        public static void SendMultiMailInThread(string from, NotificationMail mailTosend, IMailSender da)
        {
            var threadStarter = new ThreadStarter(from, mailTosend, da);
            var thread = new Thread(threadStarter.StartMulti);
            thread.Start();
        }

        public static void SendMailMulti(string from, NotificationMail notification, IMailSender da)
        {
            try
            {
                var mail = new MailMessage();
                mail.From = new MailAddress(from);
                mail.Subject = notification.Subject;
                mail.Body = notification.Body;
                mail.IsBodyHtml = notification.IsBodyHtml;

                System.Text.RegularExpressions.Regex emalRegex = new System.Text.RegularExpressions.Regex("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");
                foreach (string str in notification.To.Split(';'))
                {
                    if (emalRegex.IsMatch(str))
                        mail.Bcc.Add(str);
                }

                if (!string.IsNullOrEmpty(notification.Cc))
                {
                    mail.CC.Add(notification.Cc);
                }

                foreach (var attachment in notification.Attachments)
                {
                    mail.Attachments.Add(new Attachment(
                        new MemoryStream(attachment.Content),
                        attachment.Name
                    ));
                }

                da.Send(mail);
                _lettersSent += 1;
            }
            catch (FormatException)
            {
                _lettersNotSent += 1;
            }
            catch (ArgumentException)
            {
                _lettersNotSent += 1;
            }
            catch (SmtpException)
            {
                _lettersNotSent += 1;
            }
        }
          
        public static string FormatEmailAddress(string adresseeName, string emailAddress)
        {
            if (emailAddress.Trim().Length > 0)
            {
                string encodedName = MailSender.EncodeHeader(adresseeName);
                return string.Format("{0}<{1}>", encodedName, emailAddress);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string FormatCC(params string[] emailAddress)
        {
            string cc = string.Empty;

            foreach (string email in emailAddress)
            {
                if (email != string.Empty)
                {
                    cc += email;
                    cc += ",";
                }
            }

            if (!string.IsNullOrEmpty(cc))
            {
                cc = cc.Remove(cc.Length - 1);
            }

            return cc;
        }
    }
}
