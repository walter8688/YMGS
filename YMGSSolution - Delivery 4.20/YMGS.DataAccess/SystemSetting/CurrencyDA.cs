using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Framework;
using YMGS.Data.DataBase;

namespace YMGS.DataAccess.SystemSetting
{
    public class CurrencyDA : DaBase
    {
        public static DSCurrency QueryAllCurrency()
        {
            return SQLHelper.ExecuteStoredProcForDataSet<DSCurrency>("pr_get_currency", null);
        }
    }
}
