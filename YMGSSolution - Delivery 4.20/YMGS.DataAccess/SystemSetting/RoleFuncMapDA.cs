using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.DataBase;
using YMGS.Framework;
using System.Data;

namespace YMGS.DataAccess.SystemSetting
{
    public class RoleFuncMapDA : DaBase
    {
        public static DSRoleFuncMap QueryData(int RoleId)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="ROLE_ID",ParameterType=DbType.Int32,ParameterValue=RoleId}
            };
            var resultDt = SQLHelper.ExecuteStoredProcForDataSet<DSRoleFuncMap>("pr_get_role_func_map", parameters);
            return resultDt;
        }
    }
}
