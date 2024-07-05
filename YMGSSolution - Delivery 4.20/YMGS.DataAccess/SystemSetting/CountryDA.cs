using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.DataBase;
using YMGS.Framework;
using System.Data;

namespace YMGS.DataAccess.SystemSetting
{
    public class CountryDA : DaBase
    {
        public static DSCountry QueryAllCountry()
        {
            var resultDt = SQLHelper.ExecuteStoredProcForDataSet<DSCountry>("pr_get_all_country", null);
            return resultDt;
        }
    }
}
