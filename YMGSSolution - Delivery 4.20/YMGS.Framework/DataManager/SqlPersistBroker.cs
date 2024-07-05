using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace YMGS.Framework
{
    internal sealed class SqlPersistBroker : AbstractPersistBroker
    {
        private SqlConnection sqlCN;
        private string strDbConnectionString;
        private object lockObject = new object();

        public SqlPersistBroker(string strDbConnStr)
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
                        sqlCN = new SqlConnection(strDbConnectionString);
                }
            }

            return sqlCN;
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
            SqlCommand cmd = PreparedSqlCommand(strSql, parameters, commandType);
            cmd.ExecuteNonQuery();
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
                SqlCommand cmd = PreparedSqlCommand(strSql, parameters, commandType);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                T dsTemp = new T();
                if (dsTemp.Tables.Count == 0)
                {
                    adapter.Fill(dsTemp);
                }
                else
                {
                    string[] tableNames = new string[dsTemp.Tables.Count];
                    for (int i = 0; i < tableNames.Length; i++)
                    {
                        tableNames[i] = dsTemp.Tables[i].TableName;
                    }

                    string systemCreatedTableNameRoot = "Table";
                    for (int i = 0; i < tableNames.Length; i++)
                    {
                        string systemCreatedTableName = (i == 0)
                                                            ? systemCreatedTableNameRoot
                                                            : systemCreatedTableNameRoot + i;

                        adapter.TableMappings.Add(systemCreatedTableName, tableNames[i]);
                    }

                    var isEnforceConstraints = dsTemp.EnforceConstraints;
                    dsTemp.EnforceConstraints = false;
                    adapter.Fill(dsTemp);
                    dsTemp.EnforceConstraints = isEnforceConstraints;
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
                SqlCommand cmd = PreparedSqlCommand(strSql, parameters, commandType);
                return cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public SqlCommand PreparedSqlCommand(string strSql, IList<ParameterData> parameters, CommandType commandType)
        {
            try
            {
                SqlCommand tempCmd = sqlCN.CreateCommand();
                if (DbConnectionInTransaction)
                    tempCmd.Transaction = (SqlTransaction)GetDbTransaction;

                if (parameters != null && parameters.Count>0)
                {
                    for (int i = 0; i < parameters.Count; i++)
                    {
                        ParameterData param = parameters[i];
                        SqlParameter sqlParam = new SqlParameter();
                        sqlParam.ParameterName =param.ParameterName;
                        sqlParam.DbType = param.ParameterType;
                        sqlParam.Value = param.ParameterValue == null ? DBNull.Value : param.ParameterValue;
                        tempCmd.Parameters.Add(sqlParam);
                    }
                }

                tempCmd.CommandType = commandType;
                tempCmd.CommandText = strSql;
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
