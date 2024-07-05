using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YMGS.Framework
{
    public class NotificationMail
    {
        public NotificationMail()
        {
            this.IsBodyHtml = true;
            this.Attachments = new List<MailAttachment>();
        }

        public string To { get; set; }
        public bool IsBodyHtml { get; set; }
        public string Cc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public IList<MailAttachment> Attachments { get; private set; }
    }
}
