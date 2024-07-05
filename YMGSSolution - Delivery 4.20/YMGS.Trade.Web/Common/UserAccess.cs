using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YMGS.Data.Entity;

namespace YMGS.Trade.Web.Common
{
    public class UserAccess
    {
        private DetailUserInfo userInfo;
        private UserAccess()
        {
        }

        public UserAccess(DetailUserInfo userInfo)
        {
            this.userInfo = userInfo;
        }

        /// <summary>
        /// 是否允许访问该功能点
        /// </summary>
        /// <param name="functionId"></param>
        /// <returns></returns>
        public bool IsAllow(int functionId)
        {
            if (userInfo == null)
                return false;
            if (userInfo.UserFunctionList == null)
                return false;

            var permRows = userInfo.UserFunctionList.Select("FUNC_ID='" + functionId + "'");
            if (permRows.Length > 0)
                return true;
            else
                return false;
        }
    }
}