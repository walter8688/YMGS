using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;

namespace YMGS.Framework
{
    internal sealed class OraclePersistBroker : AbstractPersistBroker
    {
        private OracleConnection sqlCN;
        private string strDbConnectionString;
        private static object lockObject = new object();


        public OraclePersistBroker(string strDbConnStr)
        {
            strDbConnectionString = strDbConnStr;
        }
        
        /// <summary>
        /// 获得数据库连接

        /// </summary>
        /// <returns></returns>
        public override IDbConnection GetDbConnection()
        {
            if (sqlCN == null)
            {
                lock (lockObject)
                {
                    if (sqlCN == null)
                        sqlCN = new OracleConnection(strDbConnectionString);
                }
            }

            return sqlCN;
        }

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="commandType">命令类型</param>
        public override void ExecuteNonQuery(string strSql, IList<ParameterData> parameters, CommandType commandType)
        {
            Open();
            try
            {
                OracleCommand cmd = PreparedSqlCommand(strSql, parameters, commandType);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 执行Sql语句返回DataSet
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="commandType">命令类型</param>
        public override T ExecuteForDataSet<T>(string strSql, IList<ParameterData> parameters, CommandType commandType)
        {
            Open();
            try
            {
                OracleCommand cmd = PreparedSqlCommand(strSql, parameters, commandType);
                OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                T dsTemp = new T();
                if (dsTemp.Tables.Count == 0)
                {
                    adapter.Fill(dsTemp);
                }
                else
                {
                    adapter.Fill(dsTemp, dsTemp.Tables[0].TableName);
                }
                return dsTemp;
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
                OracleCommand cmd = PreparedSqlCommand(strSql, parameters, commandType);
                return cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        private OracleCommand PreparedSqlCommand(string strSql, IList<ParameterData> parameters, CommandType commandType)
        {
            try
            {
                strSql = strSql.Replace('@', ':');

                OracleCommand tempCmd = sqlCN.CreateCommand();
                if (DbConnectionInTransaction)
                    tempCmd.Transaction = (OracleTransaction)GetDbTransaction;

                if (parameters != null && parameters.Count > 0)
                {
                    for (int i = 0; i < parameters.Count; i++)
                    {
                        ParameterData param = parameters[i];
                        OracleParameter sqlParam = new OracleParameter();
                        sqlParam.ParameterName = param.ParameterName.Replace("@",":");
                        sqlParam.DbType = param.ParameterType;
                        sqlParam.Value = param.ParameterValue;
                        tempCmd.Parameters.Add(sqlParam);
                    }
                }

                tempCmd.CommandType = commandType;
                tempCmd.CommandText = strSql;
                return tempCmd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override DataBaseType GetDBType()
        {
            return DataBaseType.ORACLE;
        }
    }
}
