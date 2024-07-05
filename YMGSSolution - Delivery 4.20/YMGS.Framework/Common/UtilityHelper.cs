using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace YMGS.Framework
{
    /// <summary>
    /// 适用工具类
    /// </summary>
    public class UtilityHelper
    {
        #region 日期相关
        public static DateTime StrToDate(string str)
        {
            return DateTime.ParseExact(str, CommConstant.DateFormatString, DateTimeFormatInfo.InvariantInfo);
        }

        public static string DateToStr(DateTime date)
        {
            return DateToStrOrDefault(date, string.Empty);
        }

        public static string DateToStr(object date)
        {
            return DateToStrOrDefault(date, string.Empty);
        }

        public static string DateToStrOrDefault(object date, string defaultValue)
        {
            if (null == date || DBNull.Value.Equals(date))
            {
                return defaultValue;
            }
            return DateToStrOrDefault((DateTime)date, defaultValue);
        }

        public static string DateToStrOrDefault(DateTime date, string defaultValue)
        {
            if (date == DateTime.MinValue || date == DateTime.MaxValue)
            {
                return defaultValue;
            }
            return date.ToString(CommConstant.DateFormatString, DateTimeFormatInfo.InvariantInfo);
        }

        public static DateTime ConvertToDateTime(string strDate, string strTime)
        {
            string strTemp = strDate + " " + strTime;
            return DateTime.ParseExact(strTemp, CommConstant.DateTimeFormatString, DateTimeFormatInfo.InvariantInfo);
        }

        public static string DateToDateAndTimeStr(DateTime date)
        {
            return date.ToString(CommConstant.DateTimeFormatString, DateTimeFormatInfo.InvariantInfo);
        }

        public static string DateTimeDefaultForamtString(DateTime date)
        {
            return date.ToString(CommConstant.DateTimeDefaultForamtString, DateTimeFormatInfo.InvariantInfo);
        }

        public static string TimeToStr(DateTime date)
        {
            return date.ToString(CommConstant.TimeFormatString, DateTimeFormatInfo.InvariantInfo);
        }
        #endregion
    }
}
