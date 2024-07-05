﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Framework;
using YMGS.Data.DataBase;
using YMGS.DataAccess.SystemSetting;

namespace YMGS.Business.SystemSetting
{
    public class TimeZoneManager
    {
        public static DSTimeZone QueryAllTimeZone()
        {
            return TimeZoneDA.QueryAllTimeZone();
        }
    }
}