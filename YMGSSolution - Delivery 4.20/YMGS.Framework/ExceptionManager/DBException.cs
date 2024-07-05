using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace YMGS.Framework
{
    [Serializable]
    public class DbException : System.Exception
    {
        public DbException() { }
        public DbException(string message) : base(message) { }
        public DbException(string message, System.Exception inner) : base(message, inner) { }
        protected DbException(
          SerializationInfo info,
          StreamingContext context)
            : base(info, context) { }
    }
}
