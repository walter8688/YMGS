using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YMGS.Framework
{
    /// <summary>
    /// 邮件附件
    /// </summary>
    public class MailAttachment
    {
        public string Name { get; set; }
        public byte[] Content { get; set; }
    }
}
