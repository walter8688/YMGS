using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Security;
using System.Threading;
using System.Globalization;
using YMGS.Data.Common;
using YMGS.Data.Entity;

namespace YMGS.Trade.Web.Common
{
    public interface IPageObject
    {
        bool IsAccessible(UserAccess userAccess);
    }

    public class BasePage : Page, IPageObject
    {
        public string PageName
        {
            get
            {
                int pos = Request.RawUrl.LastIndexOf("/");
                return pos != -1 ? Request.RawUrl.Remove(0, pos + 1) : String.Empty;
            }
        }

        /// <summary>
        /// 所属模块Id
        /// </summary>
        protected virtual int TopMenuId
        {
            get { return TopMenuIdAttribute.Get(GetType().BaseType); }
        }

        /// <summary>
        /// 页面标题
        /// </summary>
        public virtual string PageTitle
        {
            get
            {
                return string.Empty;
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
        public UserAccess UserAccess
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
                if (Session[CommonConstant.CurrentLoginUserSessionKey] == null)
                    return false;
                else
                    return true;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            //验证是否有权限访问当前页面
            if (!IsAccessible(UserAccess))
                Response.Redirect(Default.Url());

            base.OnLoad(e);
        }

        public LanguageEnum Language
        {
            get
            {
                LanguageEnum language = LanguageEnum.UnKnown;
                if (Session[CommonConstant.DefaultLanguageKey] != null)
                    language = (LanguageEnum)Session[CommonConstant.DefaultLanguageKey];
                else
                {
                    string languageStr = Thread.CurrentThread.CurrentUICulture.Name;
                    if (languageStr.CompareTo(CommonConstant.Language_English_string) != 0)
                    {
                        language = LanguageEnum.Chinese;
                    }
                    else
                    {
                        language = LanguageEnum.English;
                    }
                    Session[CommonConstant.DefaultLanguageKey] = language;
                }

                return (LanguageEnum)Session[CommonConstant.DefaultLanguageKey];
            }
            set
            {
                Session[CommonConstant.DefaultLanguageKey] = value;
            }
        }

        #region IPageObject 成员

        public virtual bool IsAccessible(UserAccess userAccess)
        {
            throw new NotImplementedException();
        }

        #endregion

        protected override void InitializeCulture()
        {
            base.InitializeCulture();
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