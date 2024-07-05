using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YMGS.Trade.Web.Common
{
    public class CommonConstant
    {
        #region 系统中使用Session时SessionKey的定义

        //保存当前用户信息的SessionKey
        public static readonly string CurrentLoginUserSessionKey = "CurrentLoginUserSessionKey";

        public static readonly string DefaultWebSiteRootTitle = "YMGS";

        public static readonly int HomePageId = 8000;

        public static readonly string DefaultLanguageKey = "DefaultLanguageKey";

        //页面标题格式
        public static readonly string PageTitleFormat = "{0} - {1}";

        //会员页面导航标题格式
        public static readonly string MemberShipPageNaviTitleFormat = "&gt;{0}&gt;{1}";

        //简单模板页面导航标题格式
        public static readonly string SimpleMasterPageNaviTitleFormat = LangManager.GetString("Yourlocation") + "&gt;{0}";

        public static readonly string Language_Chinese_string = "zh-CN";
        public static readonly string Language_English_string = "en-US";

        ////使用登记SessionKey
        //public static readonly string CurUseRegistrationControllerSessionKey = "CurUseRegistrationController";

        ////请领SessionKey
        //public static readonly string CurPackageApplyFormControllerSessionKey = "CurPackageApplyFormController";

        //#endregion

        ////权限功能点中Web站点相关功能点的前缀
        //public static readonly string WebPermissionPrefix = "Web-";


        ////Mailto格式
        //public static readonly string MailToFormat = "mailto:{0}";



        #endregion

        public static string DropDownListNullKey = "";
        public static string DropDownListNullValue = "-1";

        #region 银联支付常量的定义
        //网关公钥
        public const string ChinaPayPublicKeyPath = @"//App_Data//PgPubk.key";
        //网关私钥
        //public const string ChinaPayPrivateKeyPath = @"//App_Data//MerPrK_808080233501551_20120813132441.key";
        public const string ChinaPayPrivateKeyPath = @"//App_Data//MerPrK_808080040192810_20130401162642.key";
        
        //订单交易币种
        public const string ChinaPayCuryId = "156";
        //交易类型|支付
        public const string ChinaPayTransType_Pay = "0001";
        //交易版本
        public const string ChinaPayVersion20070129 = "20070129";
        //ChinaPay交易回传状态
        public const string ChinaPayResponseStatusSuccess = "1001";

        /// <summary>
        /// 合法签名长度
        /// </summary>
        public const int ChinaPaySignLength = 256;
        #endregion
    }
}