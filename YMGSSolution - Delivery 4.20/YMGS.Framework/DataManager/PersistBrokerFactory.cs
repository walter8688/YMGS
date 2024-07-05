using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace YMGS.Framework
{
    internal class PersistBrokerFactory
    {
        private PersistBrokerFactory()
        {
        }

        private static PersistBrokerFactory objPBFactory = null;
        private static object lockObject = new object();
        public static PersistBrokerFactory Instance()
        {
            if (objPBFactory == null)
            {
                lock (lockObject)
                {
                    if (objPBFactory == null)
                        objPBFactory = new PersistBrokerFactory();
                }
            }
            return objPBFactory;
        }

        public IPersistBroker NewPersistBroker()
        {
            try
            {
                ConfigInfo objPalauConfig = DomainManager.GetLFrameworkDbConfigInfo();

                if (objPalauConfig.DBConnectionString == null || objPalauConfig.DBConnectionString == "")
                    throw new DbException("数据库连接字符串为空,请配置!");

                switch (objPalauConfig.DatabaseType)
                {
                    case DataBaseType.SQLSERVER:
                        return new SqlPersistBroker(objPalauConfig.DBConnectionString);
                    case DataBaseType.DB2:
                        return null;
                    case DataBaseType.ORACLE:
                        return new OraclePersistBroker(objPalauConfig.DBConnectionString);
                    case DataBaseType.MYSQL:
                        return null;
                    case DataBaseType.MEPLSQL:
                        return new MEPLSqlPersistBroker();
                    default:
                        return new SqlPersistBroker(objPalauConfig.DBConnectionString);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
