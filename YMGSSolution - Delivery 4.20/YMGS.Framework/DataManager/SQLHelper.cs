using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace YMGS.Framework
{
    public class SQLHelper
    {
        public SQLHelper()
        {}

        #region 执行无返回值的SQL语句

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        public static void ExecuteNonQuery(string strSql,IList<ParameterData> parameters, CommandType commandType)
        {
            IPersistBroker broker = PersistBroker.GetPersistBroker();
            try
            {
                broker.ExecuteNonQuery(strSql,parameters, commandType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (broker != null)
                    broker.Close();
            }
        }

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        public static void ExecuteNonQueryStoredProcedure(string strSql, IList<ParameterData> parameters)
        {
            IPersistBroker broker = PersistBroker.GetPersistBroker();
            try
            {
                broker.ExecuteNonQuery(strSql, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (broker != null)
                    broker.Close();
            }
        }

        #endregion

        #region 执行返回DataSet的Sql语句

        /// <summary>
        /// 执行Sql语句返回DataSet
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">参数列表，列表内的对象必须为ParameterData对象的列表</param>
        /// <param name="commandType">命令类型</param>
        public static T ExecuteStoredProcForDataSet<T>(string strSql, IList<ParameterData> parameters) where T : DataSet,new()
        {
            IPersistBroker broker = PersistBroker.GetPersistBroker();
            try
            {
                return broker.ExecuteForDataSet<T>(strSql, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (broker != null)
                    broker.Close();
            }
        }

        /// <summary>
        /// 执行Sql语句返回DataSet
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">参数列表，列表内的对象必须为ParameterData对象的列表</param>
        /// <param name="commandType">命令类型</param>
        public static T ExecuteForDataSet<T>(string strSql, IList<ParameterData> parameters, CommandType commandType) where T : DataSet, new()
        {
            IPersistBroker broker = PersistBroker.GetPersistBroker();
            try
            {
                return broker.ExecuteForDataSet<T>(strSql, parameters, commandType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (broker != null)
                    broker.Close();
            }
        }

        #endregion

        #region 执行返回单一数值的Sql

        /// <summary>
        /// 执行Sql语句返回DataSet
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">参数列表，列表内的对象必须为ParameterData对象的列表</param>
        /// <param name="commandType">命令类型</param>
        public static object ExecuteForScalar(string strSql, IList<ParameterData> parameters,CommandType commandType)
        {
            IPersistBroker broker = PersistBroker.GetPersistBroker();
            try
            {
                return broker.ExecuteForScalar(strSql, parameters, commandType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (broker != null)
                    broker.Close();
            }
        }

        /// <summary>
        /// 执行Sql语句返回DataSet
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">参数列表，列表内的对象必须为ParameterData对象的列表</param>
        /// <param name="commandType">命令类型</param>
        public static object ExecuteStoredProcForScalar(string strSql, IList<ParameterData> parameters)
        {
            IPersistBroker broker = PersistBroker.GetPersistBroker();
            try
            {
                return broker.ExecuteForScalar(strSql, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (broker != null)
                    broker.Close();
            }
        }

        public static T ExecuteForDataSetWithCmd<T>(string strSql, IList<ParameterData> parameters) where T : DataSet, new()
        {
            IPersistBroker broker = PersistBroker.GetPersistBroker();
            try
            {
                broker.Open();
                SqlCmd = (broker as SqlPersistBroker).PreparedSqlCommand(strSql, parameters, CommandType.StoredProcedure);
                return broker.ExecuteForDataSet<T>(strSql, parameters, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (broker != null)
                    broker.Close();
            }
        }

        public static SqlCommand SqlCmd
        {
            get;
            set;
        }
        #endregion
    }
}
