using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YMGS.Trade.Web.Common
{
    public sealed class ServiceOperationResult<TValue>
    {
        private readonly bool m_isSucceeded;
        private readonly string m_message;
        private readonly TValue m_value;

        public static ServiceOperationResult<TValue> Success(TValue value)
        {
            return new ServiceOperationResult<TValue>(value);
        }

        public static ServiceOperationResult<TValue> Fail(string message)
        {
            return new ServiceOperationResult<TValue>(message);
        }

        public static ServiceOperationResult<TValue> Fail(string message, TValue value)
        {
            return new ServiceOperationResult<TValue>(false, message, value);
        }

        private ServiceOperationResult(bool isSucceeded, string message, TValue value)
        {
            m_isSucceeded = isSucceeded;
            m_message = message ?? string.Empty;
            m_value = value;
        }

        private ServiceOperationResult(TValue value)
            : this(true, string.Empty, value)
        {
        }

        private ServiceOperationResult(string message)
            : this(false, message, default(TValue))
        {
        }

        private ServiceOperationResult()
            : this(string.Empty)
        {
        }

        public bool IsSucceeded
        {
            get { return m_isSucceeded; }
        }

        public string Message
        {
            get { return m_message ?? string.Empty; }
        }

        public TValue Value
        {
            get { return m_value; }
        }
    }
}
