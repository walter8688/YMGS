using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace YMGS.Framework
{
    [SerializableAttribute]
    public class ConfigInfo
    {
        private string strDBConnectionString;
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string DBConnectionString
        {
            get { return strDBConnectionString; }
            set { strDBConnectionString = value; }
        }

        private DataBaseType databaseType;
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DataBaseType DatabaseType
        {
            get { return databaseType; }
            set { databaseType = value; }
        }

        private IsolationLevel transIsolationLevel;
        /// <summary>
        /// 事务隔离级别
        /// </summary>
        public IsolationLevel TransIsolationLevel
        {
            get { return transIsolationLevel; }
            set { transIsolationLevel = value; }
        }

        public bool IsEncrptDbString
        {
            get;
            set;
        }
    }
}
