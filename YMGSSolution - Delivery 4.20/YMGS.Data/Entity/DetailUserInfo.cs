using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YMGS.Data.DataBase;

namespace YMGS.Data.Entity
{
    /// <summary>
    /// 用户实体类
    /// </summary>
    [Serializable]
    public class DetailUserInfo
    {
        public int UserId
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public int RoleId
        {
            get;
            set;
        }

        public DSRoleFuncMap.TB_ROLE_FUNC_MAPDataTable UserFunctionList
        {
            get;
            set;
        }
    }
}