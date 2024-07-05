using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.DataAccess.SystemSetting;
using YMGS.Framework;

namespace YMGS.Business.SystemSetting
{
    public class GlobalBrManager:BrBase
    {
        /// <summary>
        /// 获得系统当前时间
        /// </summary>
        /// <returns></returns>
        public static DateTime QueryCurrentDateTime()
        {
            return GlobalDA.QueryCurrentDateTime();
        }
    }
}
