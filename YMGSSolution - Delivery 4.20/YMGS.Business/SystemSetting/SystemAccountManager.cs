using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.DataBase;
using YMGS.DataAccess.SystemSetting;
using YMGS.Framework;

namespace YMGS.Business.SystemSetting
{
    public class SystemSettingManager : BrBase
    {
        public static int AddRoleFunc(string status, string roleid, string roleName, string roledesc,int createrid,int laster, string funcidstring)
        {
            return SystemAccountDA.AddRoleFunc(status,roleid,roleName,roledesc,createrid,laster, funcidstring);
        }

        public static void DelRoleFunc(string status, string roleid, string roleName, string roledesc, int createrid, int laster, string funcidstring)
        {
             SystemAccountDA.DelRoleFunc(status, roleid, roleName, roledesc, createrid, laster, funcidstring);
        }
        public static DSSystemAccount QueryData(string loginame, int userid,string email)
      {
          return SystemAccountDA.QueryData(loginame,userid,email);
      }
        public static int SaveSystemAccount(DSSystemAccount.TB_SYSTEM_ACCOUNTRow saRow)
        {
            return SystemAccountDA.SaveSystemAccount(saRow);
        }
        public static int UpdateSystemAccount(DSSystemAccount.TB_SYSTEM_ACCOUNTRow saRow)
        {
            return SystemAccountDA.UpdateSystemAccount(saRow);
        }

        public static DSSystemFunc QueryFunc(DSSystemFunc.TB_SYSTEM_FUNCRow sfrow)
        {
            return SystemAccountDA.QueryFunc(sfrow);
        }
        public static DSSystemRole QueryRole(DSSystemRole.TB_SYSTEM_ROLERow srrow)
        {
            return SystemAccountDA.QueryRole(srrow);
        }

        public static DSAccount QueryAccount(string userName, DateTime startDate, DateTime endDate)
        {
            return SystemAccountDA.QueryAccount(userName, startDate, endDate);
        }

        public static DSLeftMenuItem QueryLeftMenuItem()
        {
            return SystemAccountDA.QueryLeftMenuItem();
        }
        public static System.Data.SqlClient.SqlCommand myCmd
        {
            get { return SystemAccountDA.myCmd; }
        }

        public static void RegisterGrowMemberAccount(DSSystemAccount.TB_SYSTEM_ACCOUNTRow saRow)
        {
            SystemAccountDA.RegisterGrowMemberAccount(saRow);
        }

        public static int CheckResetEmail(int userId, string emailAddress)
        {
            return SystemAccountDA.CheckResetEmail(userId, emailAddress);
        }

        public static void SetUserFund(int setUser, int modifiedUser, decimal userFund)
        {
            SystemAccountDA.SetUserFund(setUser, modifiedUser, userFund);
        }
    }
}
