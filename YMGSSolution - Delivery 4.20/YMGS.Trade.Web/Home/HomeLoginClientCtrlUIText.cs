using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YMGS.Trade.Web.Common;
using YMGS.Trade.Web.Public;
using YMGS.Trade.Web.MemberShip;

namespace YMGS.Trade.Web.Home
{
    public class HomeLoginClientCtrlUIText
    {
        public string DefaultUserName
        {
            get
            {
                return LangManager.GetString("DefaultUserName");
            }
        }

        public string DeafalutPassword
        {
            get
            {
                return LangManager.GetString("DeafalutPassword");
            }
        }

        public string Login
        {
            get
            {
                return LangManager.GetString("login");
            }
        }

        public string Register
        {
            get
            {
                return LangManager.GetString("Register");
            }
        }

        public string ForgetPWD
        {
            get
            {
                return LangManager.GetString("forgetpsw");
            }
        }

        public string CurAccount
        {
            get
            {
                return LangManager.GetString("CurAccount");
            }
        }

        public string Myaccount
        {
            get
            {
                return LangManager.GetString("myaccount");
            }
        }

        public string OnlineCharge
        {
            get
            {
                return LangManager.GetString("OnlineCharge");
            }
        }

        public string HisTradeReport
        {
            get
            {
                return LangManager.GetString("HisTradeReport");
            }
        }

        public string SecureLogout
        {
            get
            {
                return LangManager.GetString("SecureLogout");
            }
        }

        public string UserRegisterFrmURL
        {
            get
            {
                return UserRegisterFrm.Url();
            }
        }

        public string MemberShipHomeFrmURL
        {
            get
            {
                return MemberShipHomeFrm.Url();
            }
        }

        public string MyTradeFrmURL
        {
            get
            {
                return MyTradeFrm.Url();
            }
        }

        public string OnlineChargeFrmURL
        {
            get
            {
                return OnlineChargeFrm.Url();
            }
        }

        public string ForgotPasswordURL
        {
            get
            {
                return ForgotPassword.Url();
            }
        }
    }
}