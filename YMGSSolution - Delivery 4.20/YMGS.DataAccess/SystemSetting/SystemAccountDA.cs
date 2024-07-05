using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.DataBase;
using YMGS.Framework;
using System.Data;

namespace YMGS.DataAccess.SystemSetting
{
    public class SystemAccountDA : DaBase
    {
        public static int AddRoleFunc(string status, string roleid, string roleName, string roledesc,int createrid,int laster, string funcidstring)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                 new ParameterData(){ParameterName="@STATUS",ParameterType=DbType.String,ParameterValue=status},
                new ParameterData(){ParameterName="@ROLE_ID",ParameterType=DbType.Int32,ParameterValue=int.Parse(roleid)},
                 new ParameterData(){ParameterName="@ROLENAME",ParameterType=DbType.String,ParameterValue=roleName},
                new ParameterData(){ParameterName="@ROLEDESC",ParameterType=DbType.String,ParameterValue=roledesc},
                  new ParameterData(){ParameterName="@CREATER",ParameterType=DbType.Int32,ParameterValue=createrid},
                new ParameterData(){ParameterName="@LASTEUPDATEUSER",ParameterType=DbType.Int32,ParameterValue=laster},
                 new ParameterData(){ParameterName="@FUNCLIST",ParameterType=DbType.String,ParameterValue=funcidstring}
            };
            var returnValue = SQLHelper.ExecuteStoredProcForScalar("pr_edit_role_func", parameters);
            if (returnValue == null)
                returnValue = -1;
            return Convert.ToInt32(returnValue);
            
        }

        public static void DelRoleFunc(string status, string roleid, string roleName, string roledesc, int createrid, int laster, string funcidstring)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                 new ParameterData(){ParameterName="@STATUS",ParameterType=DbType.String,ParameterValue=status},
                new ParameterData(){ParameterName="@ROLE_ID",ParameterType=DbType.Int32,ParameterValue=int.Parse(roleid)},
                 new ParameterData(){ParameterName="@ROLENAME",ParameterType=DbType.String,ParameterValue=roleName},
                new ParameterData(){ParameterName="@ROLEDESC",ParameterType=DbType.String,ParameterValue=roledesc},
                  new ParameterData(){ParameterName="@CREATER",ParameterType=DbType.Int32,ParameterValue=createrid},
                new ParameterData(){ParameterName="@LASTEUPDATEUSER",ParameterType=DbType.Int32,ParameterValue=laster},
                 new ParameterData(){ParameterName="@FUNCLIST",ParameterType=DbType.String,ParameterValue=funcidstring}
            };
             SQLHelper.ExecuteNonQueryStoredProcedure("pr_edit_role_func", parameters);
        }

        public static DSSystemRole QueryRole(DSSystemRole.TB_SYSTEM_ROLERow srrow)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@ROLE_ID",ParameterType=DbType.Int32,ParameterValue=srrow.ROLE_ID},
                 new ParameterData(){ParameterName="@ROLE_NAME",ParameterType=DbType.String,ParameterValue=srrow.IsROLE_NAMENull()?"":srrow.ROLE_NAME}
            };
            var resultDt = SQLHelper.ExecuteStoredProcForDataSet<DSSystemRole>("pr_get_system_role", parameters);
            return resultDt; 
        }
        public static DSSystemFunc QueryFunc(DSSystemFunc.TB_SYSTEM_FUNCRow sfrow)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@PARENT_FUNC_ID",ParameterType=DbType.Int32,ParameterValue=sfrow.IsPARENT_FUNC_IDNull()?0:sfrow.PARENT_FUNC_ID},
                 new ParameterData(){ParameterName="@FUNC_ID",ParameterType=DbType.Int32,ParameterValue=sfrow.FUNC_ID},
                 new ParameterData(){ParameterName="@FUNC_NAME",ParameterType=DbType.String,ParameterValue=sfrow.IsFUNC_NAMENull()?"":sfrow.FUNC_NAME}
            };
            var resultDt = SQLHelper.ExecuteStoredProcForDataSet<DSSystemFunc>("pr_get_system_func", parameters);
            return resultDt; 
        }
        public static DSSystemAccount QueryData(string LoginName,int userid,string email)
        { 
             IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@LOGIN_NAME",ParameterType=DbType.String,ParameterValue=LoginName},
                 new ParameterData(){ParameterName="@USER_ID",ParameterType=DbType.Int32,ParameterValue=userid},
                 new ParameterData(){ParameterName="@EMAIL",ParameterType=DbType.String,ParameterValue=email}
            };
             var resultDt = SQLHelper.ExecuteStoredProcForDataSet<DSSystemAccount>("pr_get_system_account", parameters);
             return resultDt; 
        }

        public static DSAccount QueryAccount(string userName,DateTime startDate,DateTime endDate)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                 new ParameterData(){ParameterName="@USERNAME",ParameterType=DbType.String,ParameterValue=userName}
            };
            if (startDate != DateTime.MinValue)
                parameters.Add(new ParameterData() { ParameterName = "@Start_Date", ParameterType = DbType.DateTime, ParameterValue = startDate });
            else
                parameters.Add(new ParameterData() { ParameterName = "@Start_Date", ParameterType = DbType.DateTime, ParameterValue = DBNull.Value });
            if (endDate != DateTime.MaxValue)
                parameters.Add(new ParameterData() { ParameterName = "@End_Date", ParameterType = DbType.DateTime, ParameterValue = endDate });
            else
                parameters.Add(new ParameterData() { ParameterName = "@End_Date", ParameterType = DbType.DateTime, ParameterValue = DBNull.Value });
            var resultDt = SQLHelper.ExecuteStoredProcForDataSet<DSAccount>("pr_system_account", parameters);
            return resultDt; 
        }
        public static DSLeftMenuItem QueryLeftMenuItem()
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
            };
            var resultDt = SQLHelper.ExecuteForDataSetWithCmd<DSLeftMenuItem>("pr_get_left_menuitem", parameters);
            myCmd = SQLHelper.SqlCmd;
            return resultDt;
        }
        public static System.Data.SqlClient.SqlCommand myCmd
        {
            get;
            set;
        }

        /// <summary>
        /// 保存帐户
        /// </summary>
        /// <param name="betDT"></param>
        public static int SaveSystemAccount(DSSystemAccount.TB_SYSTEM_ACCOUNTRow saRow)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@USER_NAME",ParameterType=DbType.String,ParameterValue=saRow.USER_NAME},
                new ParameterData(){ParameterName="@BORN_YEAR",ParameterType=DbType.String,ParameterValue=saRow.IsBORN_YEARNull()?"":saRow.BORN_YEAR},
                new ParameterData(){ParameterName="@BORN_MONTH",ParameterType=DbType.String,ParameterValue=saRow.IsBORN_MONTHNull()?"":saRow.BORN_MONTH},
                new ParameterData(){ParameterName="@BORN_DAY",ParameterType=DbType.String,ParameterValue=saRow.IsBORN_DAYNull()?"":saRow.BORN_DAY},
                new ParameterData(){ParameterName="@EMAIL_ADDRESS",ParameterType=DbType.String,ParameterValue=saRow.IsEMAIL_ADDRESSNull()?"":saRow.EMAIL_ADDRESS},
                new ParameterData(){ParameterName="@COUNTRY",ParameterType=DbType.Int32,ParameterValue=saRow.IsCOUNTRYNull()?0:saRow.COUNTRY},
                new ParameterData(){ParameterName="@CITY",ParameterType=DbType.String,ParameterValue=saRow.IsCITYNull()?"":saRow.CITY},
                new ParameterData(){ParameterName="@ADDRESS",ParameterType=DbType.String,ParameterValue=saRow.IsADDRESSNull()?"":saRow.ADDRESS},  
                new ParameterData(){ParameterName="@ZIP_CODE",ParameterType=DbType.String,ParameterValue=saRow.IsZIP_CODENull()?"":saRow.ZIP_CODE},
                new ParameterData(){ParameterName="@PHONE_TYPE",ParameterType=DbType.Int32,ParameterValue=saRow.IsPHONE_TYPENull()?0:saRow.PHONE_TYPE},
                new ParameterData(){ParameterName="@PHONE_ZONE",ParameterType=DbType.String,ParameterValue=saRow.IsPHONE_ZONENull()?"":saRow.PHONE_ZONE},
                new ParameterData(){ParameterName="@PHONE_NUMBER",ParameterType=DbType.String,ParameterValue=saRow.IsPHONE_NUMBERNull()?"":saRow.PHONE_NUMBER},
                new ParameterData(){ParameterName="@LOGIN_NAME",ParameterType=DbType.String,ParameterValue=saRow.LOGIN_NAME},
                new ParameterData(){ParameterName="@PASSWORD",ParameterType=DbType.String,ParameterValue=saRow.PASSWORD},
                new ParameterData(){ParameterName="@SQUESTION1",ParameterType=DbType.Int32,ParameterValue=saRow.IsSQUESTION1Null()?0:saRow.SQUESTION1},
                new ParameterData(){ParameterName="@SANSWER1",ParameterType=DbType.String,ParameterValue=saRow.IsSANSWER1Null()?"":saRow.SANSWER1},
                new ParameterData(){ParameterName="@ROLE_ID",ParameterType=DbType.Int32,ParameterValue=saRow.IsROLE_IDNull()?0:saRow.ROLE_ID},
                new ParameterData(){ParameterName="@ACCOUNT_STATUS",ParameterType=DbType.Int32,ParameterValue=saRow.IsACCOUNT_STATUSNull()?0:saRow.ACCOUNT_STATUS},  
                new ParameterData(){ParameterName="@AGENT_ID",ParameterType=DbType.Int32,ParameterValue=saRow.IsAGENT_IDNull()?0:saRow.AGENT_ID},
                new ParameterData(){ParameterName="@CURRENCY_ID",ParameterType=DbType.Int32,ParameterValue=saRow.IsCURRENCY_IDNull()?0:saRow.CURRENCY_ID},
                new ParameterData(){ParameterName="@TIMEZONE_ID",ParameterType=DbType.Int32,ParameterValue=saRow.IsTIMEZONE_IDNull()?0:saRow.TIMEZONE_ID}
            };
            var returnValue = SQLHelper.ExecuteStoredProcForScalar("pr_add_system_account", parameters);
            if (returnValue == null)
                returnValue = -1;
            return Convert.ToInt32(returnValue);
        }
        /// <summary>
        /// 更新帐户
        /// </summary>
        /// <param name="saRow"></param>
        /// <returns></returns>
        public static int UpdateSystemAccount(DSSystemAccount.TB_SYSTEM_ACCOUNTRow saRow)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@USER_ID",ParameterType=DbType.Int32,ParameterValue=saRow.USER_ID},
                new ParameterData(){ParameterName="@USER_NAME",ParameterType=DbType.String,ParameterValue=saRow.USER_NAME},
                new ParameterData(){ParameterName="@BORN_YEAR",ParameterType=DbType.String,ParameterValue=saRow.IsBORN_YEARNull()?"":saRow.BORN_YEAR},
                new ParameterData(){ParameterName="@BORN_MONTH",ParameterType=DbType.String,ParameterValue=saRow.IsBORN_MONTHNull()?"":saRow.BORN_MONTH},
                new ParameterData(){ParameterName="@BORN_DAY",ParameterType=DbType.String,ParameterValue=saRow.IsBORN_DAYNull()?"":saRow.BORN_DAY},
                new ParameterData(){ParameterName="@EMAIL_ADDRESS",ParameterType=DbType.String,ParameterValue=saRow.IsEMAIL_ADDRESSNull()?"":saRow.EMAIL_ADDRESS},
                new ParameterData(){ParameterName="@COUNTRY",ParameterType=DbType.Int32,ParameterValue=saRow.IsCOUNTRYNull()?0:saRow.COUNTRY},
                new ParameterData(){ParameterName="@CITY",ParameterType=DbType.String,ParameterValue=saRow.IsCITYNull()?"":saRow.CITY},
                new ParameterData(){ParameterName="@ADDRESS",ParameterType=DbType.String,ParameterValue=saRow.IsADDRESSNull()?"":saRow.ADDRESS},  
                new ParameterData(){ParameterName="@ZIP_CODE",ParameterType=DbType.String,ParameterValue=saRow.IsZIP_CODENull()?"":saRow.ZIP_CODE},
                new ParameterData(){ParameterName="@PHONE_TYPE",ParameterType=DbType.Int32,ParameterValue=saRow.IsPHONE_TYPENull()?0:saRow.PHONE_TYPE},
                new ParameterData(){ParameterName="@PHONE_ZONE",ParameterType=DbType.String,ParameterValue=saRow.IsPHONE_ZONENull()?"":saRow.PHONE_ZONE},
                new ParameterData(){ParameterName="@PHONE_NUMBER",ParameterType=DbType.String,ParameterValue=saRow.IsPHONE_NUMBERNull()?"":saRow.PHONE_NUMBER},
                new ParameterData(){ParameterName="@LOGIN_NAME",ParameterType=DbType.String,ParameterValue=saRow.LOGIN_NAME},
                new ParameterData(){ParameterName="@PASSWORD",ParameterType=DbType.String,ParameterValue=saRow.PASSWORD},
                new ParameterData(){ParameterName="@SQUESTION1",ParameterType=DbType.Int32,ParameterValue=saRow.IsSQUESTION1Null()?0:saRow.SQUESTION1},
                new ParameterData(){ParameterName="@SANSWER1",ParameterType=DbType.String,ParameterValue=saRow.IsSANSWER1Null()?"":saRow.SANSWER1},
                new ParameterData(){ParameterName="@ROLE_ID",ParameterType=DbType.Int32,ParameterValue=saRow.IsROLE_IDNull()?0:saRow.ROLE_ID},
                new ParameterData(){ParameterName="@ACCOUNT_STATUS",ParameterType=DbType.Int32,ParameterValue=saRow.IsACCOUNT_STATUSNull()?0:saRow.ACCOUNT_STATUS},  
                new ParameterData(){ParameterName="@AGENT_ID",ParameterType=DbType.Int32,ParameterValue=saRow.IsAGENT_IDNull()?0:saRow.AGENT_ID},
                new ParameterData(){ParameterName="@CURRENCY_ID",ParameterType=DbType.Int32,ParameterValue=saRow.IsCURRENCY_IDNull()?0:saRow.CURRENCY_ID},
                new ParameterData(){ParameterName="@TIMEZONE_ID",ParameterType=DbType.Int32,ParameterValue=saRow.IsTIMEZONE_IDNull()?0:saRow.TIMEZONE_ID}
            };
            var returnValue = SQLHelper.ExecuteStoredProcForScalar("pr_up_system_account", parameters);
            if (returnValue == null)
                returnValue = -1;
            return Convert.ToInt32(returnValue);
        }

        /// <summary>
        /// 发展会员注册
        /// </summary>
        /// <param name="saRow"></param>
        public static void RegisterGrowMemberAccount(DSSystemAccount.TB_SYSTEM_ACCOUNTRow saRow)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@USER_NAME",ParameterType=DbType.String,ParameterValue=saRow.USER_NAME},
                new ParameterData(){ParameterName="@BORN_YEAR",ParameterType=DbType.String,ParameterValue=saRow.IsBORN_YEARNull()?"":saRow.BORN_YEAR},
                new ParameterData(){ParameterName="@BORN_MONTH",ParameterType=DbType.String,ParameterValue=saRow.IsBORN_MONTHNull()?"":saRow.BORN_MONTH},
                new ParameterData(){ParameterName="@BORN_DAY",ParameterType=DbType.String,ParameterValue=saRow.IsBORN_DAYNull()?"":saRow.BORN_DAY},
                new ParameterData(){ParameterName="@COUNTRY",ParameterType=DbType.Int32,ParameterValue=saRow.IsCOUNTRYNull()?0:saRow.COUNTRY},
                new ParameterData(){ParameterName="@CITY",ParameterType=DbType.String,ParameterValue=saRow.IsCITYNull()?"":saRow.CITY},
                new ParameterData(){ParameterName="@ADDRESS",ParameterType=DbType.String,ParameterValue=saRow.IsADDRESSNull()?"":saRow.ADDRESS},  
                new ParameterData(){ParameterName="@ZIP_CODE",ParameterType=DbType.String,ParameterValue=saRow.IsZIP_CODENull()?"":saRow.ZIP_CODE},
                new ParameterData(){ParameterName="@PHONE_TYPE",ParameterType=DbType.Int32,ParameterValue=saRow.IsPHONE_TYPENull()?0:saRow.PHONE_TYPE},
                new ParameterData(){ParameterName="@PHONE_ZONE",ParameterType=DbType.String,ParameterValue=saRow.IsPHONE_ZONENull()?"":saRow.PHONE_ZONE},
                new ParameterData(){ParameterName="@PHONE_NUMBER",ParameterType=DbType.String,ParameterValue=saRow.IsPHONE_NUMBERNull()?"":saRow.PHONE_NUMBER},
                new ParameterData(){ParameterName="@LOGIN_NAME",ParameterType=DbType.String,ParameterValue=saRow.LOGIN_NAME},
                new ParameterData(){ParameterName="@PASSWORD",ParameterType=DbType.String,ParameterValue=saRow.PASSWORD},
                new ParameterData(){ParameterName="@SQUESTION1",ParameterType=DbType.Int32,ParameterValue=saRow.IsSQUESTION1Null()?0:saRow.SQUESTION1},
                new ParameterData(){ParameterName="@SANSWER1",ParameterType=DbType.String,ParameterValue=saRow.IsSANSWER1Null()?"":saRow.SANSWER1},
                new ParameterData(){ParameterName="@ACCOUNT_STATUS",ParameterType=DbType.Int32,ParameterValue=saRow.IsACCOUNT_STATUSNull()?0:saRow.ACCOUNT_STATUS}
            };
            SQLHelper.ExecuteStoredProcForScalar("pr_register_growmember_account", parameters);
        }

        public static int CheckResetEmail(int userId,string emailAddress)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@User_Id",ParameterType=DbType.Int32,ParameterValue=userId},
                new ParameterData(){ParameterName="@Email_address",ParameterType=DbType.String,ParameterValue=emailAddress}
            };
            var returnObj = SQLHelper.ExecuteStoredProcForScalar("pr_check_reset_email", parameters);
            return Convert.ToInt32(returnObj);
        }
        public static void SetUserFund(int setUser, int modifiedUser, decimal userFund)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@UserID",ParameterType=DbType.Int32,ParameterValue=setUser},
                new ParameterData(){ParameterName="@ModifiedUser",ParameterType=DbType.Int32,ParameterValue=modifiedUser},
                new ParameterData(){ParameterName="@CurFund",ParameterType=DbType.Decimal,ParameterValue=userFund}
            };
            SQLHelper.ExecuteStoredProcForScalar("pr_set_user_fund", parameters);
        }
    }
}
