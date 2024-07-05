using System;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using YMGS.Data.Entity;
using System.Security;
using YMGS.Data.Common;
using System.Threading;
using System.Globalization;

namespace YMGS.Trade.Web.Common
{
    public class ServiceBase : WebService
    {
        protected virtual bool IsCheckSecurity
        {
            get
            {
                return false;
            }
        }

        public ServiceBase()
        {
            if (IsCheckSecurity)
            {
                if (!IsLogin)
                {
                    throw new SecurityException("当前用户没有登录，不能访问该接口");
                }
            }
        }

        /// <summary>
        /// 当前系统登录用户信息
        /// </summary>
        public DetailUserInfo CurrentUser
        {
            get
            {
                return PageHelper.GetCurrentUser();
            }
        }

        private UserAccess userAccess;
        /// <summary>
        /// 用户权限访问对象
        /// </summary>
        protected UserAccess UserAccess
        {
            get
            {
                if (userAccess == null)
                    userAccess = new UserAccess(CurrentUser);

                return userAccess;
            }
        }

        public bool IsAllow(int functionId)
        {
            return UserAccess.IsAllow(functionId);
        }

        public bool IsLogin
        {
            get
            {
                if (!User.Identity.IsAuthenticated)
                    return false;

                if (Session[CommonConstant.CurrentLoginUserSessionKey] == null)
                    return false;
                else
                    return true;
            }
        }

        protected void InitializeCulture(LanguageEnum Language)
        {
            string languageStr = CommonConstant.Language_Chinese_string;
            if (Language == LanguageEnum.English)
            {
                languageStr = CommonConstant.Language_English_string;
            }

            if (!string.IsNullOrEmpty(languageStr))
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageStr);
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(languageStr);
            }
        }
    }
}