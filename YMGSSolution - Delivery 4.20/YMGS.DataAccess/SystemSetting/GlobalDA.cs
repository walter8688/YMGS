using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.DataBase;
using YMGS.Framework;
using System.Data;

namespace YMGS.DataAccess.SystemSetting
{
    public class GlobalDA : DaBase
    {
        /// <summary>
        /// 获得系统当前时间
        /// </summary>
        /// <returns></returns>
        public static DateTime QueryCurrentDateTime()
        {
            return Convert.ToDateTime(SQLHelper.ExecuteStoredProcForScalar("pr_get_current_datetime", null));
        }
    }
}
