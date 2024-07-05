using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YMGS.Framework
{
    [Serializable]
    public class ExceptionSessionInfo
    {
        public enum ExceptionLevel
        {
            Warning, Error, Fatal
        }

        public const string SessionKey = "ExceptionHandler.EXCEPTION";

        public ExceptionSessionInfo(Exception ex, ExceptionLevel level, string message)
        {
            this.Exception = ex;
            this.Level = level;
            this.Message = message;
        }
        public readonly Exception Exception;
        public readonly ExceptionLevel Level;
        public readonly string Message;
    }
}
