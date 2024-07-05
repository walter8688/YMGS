using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace YMGS.Framework
{
    internal abstract class AbstractPersistBroker : IPersistBroker, IDisposable
    {
        //数据库事务
        IDbTransaction dbTrans;
        
        //数据库连接是否在事务中
        bool bIsInTrans = false;

        #region 公共接口

        /// <summary>
        /// 获得数据库连接
        /// </summary>
        /// <returns></returns>
        public abstract IDbConnection GetDbConnection();

        /// <summary>
        /// 获得数据库事务


        /// </summary>
        /// <returns></returns>
        public IDbTransaction GetDbTransaction
        {
            get
            {
                return dbTrans;
            }
        }

        /// <summary>
        /// 获得数据连接是否在事务中
        /// </summary>
        public bool DbConnectionInTransaction
        {
            get { return bIsInTrans; }
        }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public void Open()
        {
            try
            {
                IDbConnection tempCN = GetDbConnection();
                if (tempCN.State != ConnectionState.Open)
                    tempCN.Open();

            }
            catch (Exception ex)
            {
                throw new DbException("It's failed to open db connection", ex);
            }
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Close()
        {
            try
            {
                IDbConnection tempCN = GetDbConnection();
                if (tempCN.State != ConnectionState.Closed)
                    tempCN.Close();
            }
            catch (Exception ex)
            {
                throw new DbException("It's failed to close db connection", ex);
            }
        }

        /// <summary>
        /// 关闭数据库连接，并强制进行垃圾回收
        /// </summary>
        /// <param name="isBeginGC"></param>
        public void Close(bool isBeginGC)
        {
            Close();
            if (isBeginGC)
            {
                GC.SuppressFinalize(this);
                GC.Collect();
            }
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTrans()
        {
            try
            {
                IDbConnection tempCN = GetDbConnection();
                if (tempCN.State != ConnectionState.Open)
                    tempCN.Open();

                if (bIsInTrans)
                    throw new Exception("当前数据连接已经在事务中，不能开始新的事务");

                IsolationLevel tempIDBIsolationLevel = GetTransIsolationLevel();
                if (tempIDBIsolationLevel == IsolationLevel.ReadUncommitted)
                    dbTrans = tempCN.BeginTransaction();
                else
                    dbTrans = tempCN.BeginTransaction(tempIDBIsolationLevel);

                bIsInTrans = true;
            }
            catch (Exception ex)
            {
                throw new DbException("It's failed to begin transaction", ex);
            }
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTrans()
        {
            try
            {
                if (!bIsInTrans || dbTrans == null)
                    throw new Exception("当前数据连接不在事务中，不能提交事务");
                dbTrans.Commit();
                bIsInTrans = false;
            }
            catch (Exception ex)
            {
                throw new DbException("It's failed to commit db transaction", ex);
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTrans()
        {
            try
            {
                if (!bIsInTrans || dbTrans == null)
                    throw new Exception("当前数据连接不在事务中，不能回滚事务");
                dbTrans.Rollback();
                bIsInTrans = false;
            }
            catch (Exception ex)
            {
                throw new DbException("It's failed to rollback db transaction", ex);
            }
        }

        private int iCommandTimeout = 0;
        /// <summary>
        /// 设置Command超时值
        /// </summary>
        /// <param name="iTimeOut"></param>
        public void SetTimeout(int iTimeOut)
        {
            iCommandTimeout = iTimeOut;
        }

        /// <summary>
        /// 获得Timeout值
        /// </summary>
        /// <returns></returns>
        protected int GetTimeOut()
        {
            return iCommandTimeout;
        }

        /// <summary>
        /// 数据库隔离级别
        /// </summary>
        IsolationLevel tempIsolationLevel = IsolationLevel.ReadUncommitted;

        /// <summary>
        /// 设置当前数据库连接的事务隔离级别
        /// </summary>
        /// <param name="isolationLevel"></param>
        public void SetTransIsolationLevel(System.Data.IsolationLevel isolationLevel)
        {
            tempIsolationLevel = isolationLevel;
        }
        
        /// <summary>
        /// 获得数据库类型
        /// </summary>
        /// <returns></returns>
        public abstract DataBaseType GetDBType();

        protected IsolationLevel GetTransIsolationLevel()
        {
            return tempIsolationLevel;
        }

        /// <summary>
        /// 关闭数据库连接，并开始强制性垃圾收集


        /// </summary>
        public void Dispose()
        {
            // TODO:  添加 PersistBroker.Dispose 实现

            Close(true);
        }

        #endregion

        #region 执行无返回数据的Sql语句

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        public void ExecuteNonQuery(string strSql)
        {
            ExecuteNonQuery(strSql, CommandType.Text);
        }

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        public void ExecuteNonQuery(string strSql,CommandType commandType)
        {
            ExecuteNonQuery(strSql, null, CommandType.Text);
        }

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">参数列表，列表内的对象必须为ParameterData对象的列表</param>
        public void ExecuteNonQuery(string strSql, IList<ParameterData> parameters)
        {
            ExecuteNonQuery(strSql, parameters, CommandType.Text);
        }

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">参数列表，列表内的对象必须为ParameterData对象的列表</param>
        /// <param name="commandType">命令类型</param>
        public abstract void ExecuteNonQuery(string strSql, IList<ParameterData> parameters, CommandType commandType);

        #endregion

        #region 执行返回DataTable的Sql语句

        /// <summary>
        /// 执行Sql语句返回DataTable
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        public DataTable ExecuteForDataTable(string strSql)
        {
            return ExecuteForDataSet(strSql, null).Tables[0];
        }

        /// <summary>
        /// 执行Sql语句返回DataTable
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        public DataTable ExecuteForDataTable(string strSql,CommandType commandType)
        {
            return ExecuteForDataSet(strSql, null,commandType).Tables[0];
        }

        /// <summary>
        /// 执行Sql语句返回DataTable
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="paramName">参数名</param>
        /// <param name="paramValue">参数值</param>
        public DataTable ExecuteForDataTable(string strSql, IList<ParameterData> parameters)
        {
            return ExecuteForDataSet(strSql,parameters,CommandType.Text).Tables[0];
        }

        /// <summary>
        /// 执行Sql语句返回DataTable
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="paramName">参数</param>
        public DataTable ExecuteForDataTable(string strSql, IList<ParameterData> parameters,CommandType commandType)
        {
            return ExecuteForDataSet(strSql, parameters, commandType).Tables[0];
        }

        #endregion

        #region 执行返回DataSet的Sql语句

        /// <summary>
        /// 执行Sql语句返回DataSet
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        public DataSet ExecuteForDataSet(string strSql)
        {
            return ExecuteForDataSet(strSql, null, CommandType.Text);
        }

        /// <summary>
        /// 执行Sql语句返回DataSet
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        public DataSet ExecuteForDataSet(string strSql,CommandType commandType)
        {
            return ExecuteForDataSet(strSql, null, commandType);
        }


        /// <summary>
        /// 执行Sql语句返回DataSet
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">参数列表，列表内的对象必须为ParameterData对象的列表</param>
        public DataSet ExecuteForDataSet(string strSql, IList<ParameterData> parameters)
        {
            return ExecuteForDataSet(strSql, parameters, CommandType.Text);
        }

        /// <summary>
        /// 执行Sql语句返回DataSet
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">参数列表，列表内的对象必须为ParameterData对象的列表</param>
        /// <param name="commandType">命令类型</param>
        public DataSet ExecuteForDataSet(string strSql, IList<ParameterData> parameters, CommandType commandType)
        {
            return ExecuteForDataSet<DataSet>(strSql, parameters, commandType);
        }

        /// <summary>
        /// 执行Sql语句返回DataSet
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="commandType">命令类型</param>
        public abstract T ExecuteForDataSet<T>(string strSql, IList<ParameterData> parameters, CommandType commandType) where T : DataSet,new();

        #endregion

        #region 执行返回单一值的SQL

        /// <summary>
        /// 执行返回单一值的SQL或存储过程
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public abstract object ExecuteForScalar(string strSql, IList<ParameterData> parameters, CommandType commandType);

        #endregion
    }
}
