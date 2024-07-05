using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.DataBase;
using YMGS.DataAccess.SystemSetting;
using YMGS.Framework;

namespace YMGS.Business.SystemSetting
{
    public class RoleFuncMapManager : BrBase
    {
       public static DSRoleFuncMap QueryData(int RoleId)
       {
           return RoleFuncMapDA.QueryData(RoleId);
       }
    }
}
