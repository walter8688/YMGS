using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using YMGS.Trade.Web.Common;
using YMGS.Data.Common;
using YMGS.Framework;
using YMGS.Data.DataBase;
using YMGS.Business.SystemSetting;
using YMGS.Trade.Web.Public;
using YMGS.Data.Entity;
using System.Web.Security;
using YMGS.Business.MemberShip;

namespace YMGS.Trade.Web.Services
{
    /// <summary>
    /// Summary description for HomeLoginService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class HomeLoginService : ServiceBase
    {
        [WebMethod(EnableSession=true)]
        public ServiceOperationResult<IEnumerable<string>> UserLogin(int language, string loginName, string password)
        {
            try
            {
                base.InitializeCulture((LanguageEnum)language);
                if (CheckUserLogin(loginName, password) != null)
                {
                    return CheckUserLogin(loginName, password);
                }
                else
                {
                    DSSystemAccount dssa = SystemSettingManager.QueryData(loginName, 0, "");
                    DSSystemAccount.TB_SYSTEM_ACCOUNTRow obj = dssa.TB_SYSTEM_ACCOUNT[0];
                    DSRoleFuncMap.TB_ROLE_FUNC_MAPDataTable dsUserFuncMap = RoleFuncMapManager.QueryData(obj.ROLE_ID).TB_ROLE_FUNC_MAP;

                    DetailUserInfo userInfo = new DetailUserInfo();
                    userInfo.UserId = obj.USER_ID;
                    userInfo.RoleId = obj.ROLE_ID;
                    userInfo.UserName = obj.USER_NAME;
                    userInfo.UserFunctionList = dsUserFuncMap;
                    PageHelper.SetUserInfoToSession(userInfo);
                    FormsAuthentication.SetAuthCookie(obj.USER_NAME, false);

                    var userFund = UserFundManager.QueryUserFund(obj.USER_ID).TB_USER_FUND[0];
                    var succeedStr = string.Format("{0}@{1}@{2}", "Success", userFund.CUR_FUND, loginName);
                    IList<string> returnList = new List<string>();
                    returnList.Add(succeedStr);
                    return ServiceOperationResult<IEnumerable<string>>.Success(returnList);
                }
            }
            catch (Exception ex)
            {
                string errMessage = string.Format("DateTime:{0};UserLogin Failed:{1}", UtilityHelper.DateToStr(DateTime.Now), ex.Message);
                LogHelper.LogError(errMessage);
                return ServiceOperationResult<IEnumerable<string>>.Fail(errMessage);
            }
        }

        [WebMethod]
        public ServiceOperationResult<IEnumerable<UserFundInfo>> GetUserFund(int userId)
        {
            try
            {
                var userFund = UserFundManager.QueryUserFund(userId).TB_USER_FUND[0];
                UserFundInfo userFundInfo = new UserFundInfo();
                userFundInfo.UserId = userFund.USER_ID;
                userFundInfo.UserFund = userFund.CUR_FUND;
                IList<UserFundInfo> returnList = new List<UserFundInfo>();
                returnList.Add(userFundInfo);
                return ServiceOperationResult<IEnumerable<UserFundInfo>>.Success(returnList);
            }
            catch (Exception ex)
            {
                string errMessage = string.Format("DateTime:{0};GetUserInfo Failed:{1}", UtilityHelper.DateToStr(DateTime.Now), ex.Message);
                LogHelper.LogError(errMessage);
                return ServiceOperationResult<IEnumerable<UserFundInfo>>.Fail(errMessage);
            }
        }

        [WebMethod(EnableSession = true)]
        public ServiceOperationResult<IEnumerable<string>> UserLogout()
        {
            try
            {
                PageHelper.SetUserInfoToSession(null);
                FormsAuthentication.SignOut();
                IList<string> returnList = new List<string>();
                returnList.Add(Default.Url());
                return ServiceOperationResult<IEnumerable<string>>.Success(returnList);
            }
            catch (Exception ex)
            {
                string errMessage = string.Format("DateTime:{0};UserLogout Failed:{1}", UtilityHelper.DateToStr(DateTime.Now), ex.Message);
                LogHelper.LogError(errMessage);
                return ServiceOperationResult<IEnumerable<string>>.Fail(errMessage);
            }
        }

        public ServiceOperationResult<IEnumerable<string>> CheckUserLogin(string loginName, string password)
        {
            DSSystemAccount dssa = SystemSettingManager.QueryData(loginName, 0, "");
            if (dssa.TB_SYSTEM_ACCOUNT.Count > 1)
            {
                return ServiceOperationResult<IEnumerable<string>>.Fail(LangManager.GetString("usernameexist"));
            }
            if (dssa.TB_SYSTEM_ACCOUNT.Count < 1)
            {
                return ServiceOperationResult<IEnumerable<string>>.Fail(LangManager.GetString("notexistuser"));
            }
            DSSystemAccount.TB_SYSTEM_ACCOUNTRow obj = dssa.TB_SYSTEM_ACCOUNT[0];
            if (string.IsNullOrEmpty(obj.PASSWORD))
            {
                IList<string> returnList = new List<string>();
                returnList.Add(UserRegisterFrm.Url(EncryptManager.DESEnCrypt(obj.USER_ID.ToString())));
                return ServiceOperationResult<IEnumerable<string>>.Success(returnList);
            }
            if (obj.ACCOUNT_STATUS == 2)
            {
                return ServiceOperationResult<IEnumerable<string>>.Fail(LangManager.GetString("AccountLockError"));
            }
            if (obj.ACCOUNT_STATUS == 0)
            {
                IList<string> returnList = new List<string>();
                returnList.Add(ResendEmail.Url(obj.USER_ID));
                return ServiceOperationResult<IEnumerable<string>>.Success(returnList);
            }
            if (obj.PASSWORD != EncryptManager.GetEncryString(password))
            {
                return ServiceOperationResult<IEnumerable<string>>.Fail(LangManager.GetString("passworderror"));
            }
            return null;
        }
    }
}
