using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Diagnostics;

namespace YMGS.Framework
{
    public class Category
    {
        public const string General = "General";
    }

    public class Priority
    {
        public const int Lowest = 0;
        public const int Low = 1;
        public const int Normal = 2;
        public const int High = 3;
        public const int Highest = 4;
    }

    public class LogHelper
    {
        public static void LogInformation(string strMessage)
        {
            LogInformation(strMessage, TraceEventType.Information);
        }

        public static void LogWarning(string strMessage)
        {
            LogInformation(strMessage, TraceEventType.Warning);
        }

        public static void LogError(string strMessage)
        {
            LogInformation(strMessage, TraceEventType.Error);
        }

        public static void LogCritical(string strMessage)
        {
            LogInformation(strMessage, TraceEventType.Critical);
        }

        private static void LogInformation(string strMessage, TraceEventType type)
        {
            try
            {
                LogEntry logEntity = new LogEntry();
                logEntity.Categories.Add(Category.General);
                logEntity.Message = @strMessage;
                logEntity.Title = "YMGS";
                logEntity.Priority = Priority.Normal;
                logEntity.Severity = type;
                Logger.Write(logEntity);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
    }
}
