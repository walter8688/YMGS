using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Framework;
using YMGS.Data.DataBase;

namespace YMGS.DataAccess.SystemSetting
{
    public class TimeZoneDA : DaBase
    {
        public static DSTimeZone QueryAllTimeZone()
        {
            return SQLHelper.ExecuteStoredProcForDataSet<DSTimeZone>("pr_get_timezone", null);
        }
    }
}
