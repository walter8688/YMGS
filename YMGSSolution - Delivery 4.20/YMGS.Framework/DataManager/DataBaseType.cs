using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YMGS.Framework
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    [SerializableAttribute]
    public enum DataBaseType
    {
        SQLSERVER,
        ORACLE,
        DB2,
        MYSQL,
        MEPLSQL,
    }
}
