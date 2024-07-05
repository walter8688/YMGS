using System;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.IO;
using System.Text;

namespace YMGS.Framework
{
    public class ExceptionHelper
    {
        public static void Publish(Exception exc)
        {
            Publish(exc, System.Diagnostics.TraceEventType.Information);
        }

        public static void Publish(Exception e, System.Diagnostics.TraceEventType type)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);
            ExceptionPublishFormatter formatter = new ExceptionPublishFormatter(writer, e);
            formatter.Format();   
      
            LogEntry log = new LogEntry();
            log.Message = sb.ToString();
            log.Priority = Priority.Highest;
            log.Severity = type;
            log.Title = "YMGS Error";
            log.Categories.Add(Category.General);
            Logger.Write(log);
        }
    }
}
