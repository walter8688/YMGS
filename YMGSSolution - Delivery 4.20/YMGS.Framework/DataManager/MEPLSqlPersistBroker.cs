using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace YMGS.Framework
{
    internal sealed class MEPLSqlPersistBroker : AbstractPersistBroker
    {
        private Database database = null;
        private IDbConnection dbConnection = null;
        private object lockObject = new object();

        public MEPLSqlPersistBroker()
        {
            database = DatabaseFactory.CreateDatabase(CommConstant.MEMLDataBaseKey);
            if (database == null)
                throw new ApplicationException("不能建立数据库连接，请检查数据库配置是否正确!");
        }
        
        /// <summary>
        /// 获得数据库连接
        /// </summary>
        /// <returns></returns>
        public override IDbConnection GetDbConnection()
        {
            if (dbConnection == null)
            {
                lock (lockObject)
                {
                    if (dbConnection == null)
                        dbConnection = database.CreateConnection();
                }
            }
            return dbConnection;
        }

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="paramName">参数</param>
        /// <param name="commandType">命令类型</param>
        public override void ExecuteNonQuery(string strSql, IList<ParameterData> parameters, CommandType commandType)
        {
            Open();
            DbCommand cmd = PreparedSqlCommand(strSql, parameters, commandType);
            database.ExecuteNonQuery(cmd);
        }


        /// <summary>
        /// 执行Sql语句返回DataSet
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="paramName">参数名</param>
        /// <param name="paramValue">参数值</param>
        /// <param name="commandType">命令类型</param>
        public override T ExecuteForDataSet<T>(string strSql, IList<ParameterData> parameters, CommandType commandType)
        {
            Open();
            try
            {
                DbCommand tempCmd = PreparedSqlCommand(strSql, parameters, commandType);
                T ds = new T();
                var tableNames = new string[ds.Tables.Count];
                for (var i = 0; i < ds.Tables.Count; i++)
                {
                    tableNames[i] = ds.Tables[i].TableName;
                }
                var enforceConstraints = ds.EnforceConstraints;

                ds.EnforceConstraints = false;
                database.LoadDataSet(tempCmd, ds, tableNames);
                ds.EnforceConstraints = enforceConstraints;
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 执行返回单一值的SQL或存储过程
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public override object ExecuteForScalar(string strSql, IList<ParameterData> parameters, CommandType commandType)
        {
            Open();
            try
            {
                DbCommand cmd = PreparedSqlCommand(strSql, parameters, commandType);
                return database.ExecuteScalar(cmd);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DbCommand PreparedSqlCommand(string strSql, IList<ParameterData> parameters, CommandType commandType)
        {
            try
            {
                DbCommand tempCmd;
                if (commandType == CommandType.StoredProcedure)
                    tempCmd = database.GetStoredProcCommand(strSql);
                else
                    tempCmd = database.GetSqlStringCommand(strSql);

                if (DbConnectionInTransaction)
                    tempCmd.Transaction = (SqlTransaction)GetDbTransaction;
                tempCmd.Parameters.Clear();
                if (parameters != null && parameters.Count>0)
                {
                    for (int i = 0; i < parameters.Count; i++)
                    {
                        ParameterData param = parameters[i];
                        database.AddInParameter(tempCmd, param.ParameterName,
                                            param.ParameterType,
                                            param.ParameterValue == null ? DBNull.Value : param.ParameterValue);                            
                    }
                }

                int iTimeOut = GetTimeOut();
                if (iTimeOut != 0)
                    tempCmd.CommandTimeout = iTimeOut;
                return tempCmd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 返回数据类型
        /// </summary>
        /// <returns></returns>
        public override DataBaseType GetDBType()
        {
            return DataBaseType.SQLSERVER;
        }
    }
}
