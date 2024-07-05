using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YMGS.Framework
{
    public class CommConstant
    {
        public const string MEMLDataBaseKey = "YGMSConn";
        public const string DbConnStringKey = "ConnectionString";
        public const string DbTypeKey = "DatabaseType";
        public const string DbTranLevelKey = "TransactionIsolationLevel";
        public const string IsEncrptDbStringKey = "IsEncrptDbString";
        public const string DomainConfigKey = "DomainConfigKey";

        public const string SmtpServerKey = "SMTP_SERVER";
        public const string SmtpLoginKey = "SMTP_LOGIN";
        public const string SmtpPasswordKey = "SMTP_PASSWORD";
        public const string SmtpFromUserKey = "MAIL_FROM";

        public const string DateFormatString = "yyyy-MM-dd";
        public const string DateTimeFormatString = "yyyy-MM-dd HH:mm";
        public const string DateTimeDefaultForamtString = "yyyy-MM-dd HH:mm:ss";
        public const string TimeFormatString = "HH:mm";

        public const string TheDrawString = "平局";
        public const string TheDrawEnString = "The Draw";
        public const string MarketOverFormatString = "大于 {0} 球";
        public const string MarketUnderFormatString = "小于 {0} 球";
        public const string MarketOverEnFormatString = "Over {0} Goals";
        public const string MarketUnderEnFormatString = "Under {0} Goals";

        public const string MarketAsianHandicapFormatOneString = "{0} {1}";
        public const string MarketAsianHandicapFormatTwoString = "{0} {1} & {2}";

    }
}
