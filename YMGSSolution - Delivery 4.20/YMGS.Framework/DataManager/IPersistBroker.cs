using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace YMGS.Framework
{
    /// <summary>
    /// 数据库访问接口类
    /// </summary>
    public interface IPersistBroker
    {
        #region 公共接口

        /// <summary>
        /// 获得数据库连接

        /// </summary>
        /// <returns></returns>
        IDbConnection GetDbConnection();

        /// <summary>
        /// 打开数据库连接

        /// </summary>
        void Open();

        /// <summary>
        /// 关闭数据库连接

        /// </summary>
        void Close();

        /// <summary>
        /// 关闭数据库连接，并强制进行垃圾回收

        /// </summary>
        /// <param name="isBeginGC"></param>
        void Close(bool isBeginGC);

        /// <summary>
        /// 开始事务

        /// </summary>
        void BeginTrans();

        /// <summary>
        /// 提交事务
        /// </summary>
        void CommitTrans();

        /// <summary>
        /// 回滚事务
        /// </summary>
        void RollbackTrans();

        /// <summary>
        /// 设置Command超时值

        /// </summary>
        /// <param name="iTimeOut"></param>
        void SetTimeout(int iTimeOut);

        /// <summary>
        /// 设置当前数据库连接的事务隔离级别
        /// </summary>
        /// <param name="isolationLevel"></param>
        void SetTransIsolationLevel(System.Data.IsolationLevel isolationLevel);
        
        /// <summary>
        /// 获取数据库类型，返回大写字母
        /// </summary>
        /// <returns></returns>
        DataBaseType GetDBType();

        #endregion

        #region 执行无返回值得Sql语句

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        void ExecuteNonQuery(string strSql);

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="commandType"></param>
        void ExecuteNonQuery(string strSql, CommandType commandType);

 
        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">参数列表，列表内的对象必须为ParameterData对象的列表</param>
        void ExecuteNonQuery(string strSql, IList<ParameterData> parameters);

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">参数列表，列表内的对象必须为ParameterData对象的列表</param>
        /// <param name="commandType">命令类型</param>
        void ExecuteNonQuery(string strSql, IList<ParameterData> parameters,CommandType commandType);

        #endregion

        #region 执行返回DataTable的Sql语句

        /// <summary>
        /// 执行Sql语句返回DataTable
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        DataTable ExecuteForDataTable(string strSql);

        /// <summary>
        /// 执行Sql语句返回DataTable
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        DataTable ExecuteForDataTable(string strSql,CommandType commandType);

        /// <summary>
        /// 执行Sql语句返回DataTable
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">参数列表，列表内的对象必须为ParameterData对象的列表</param>
        DataTable ExecuteForDataTable(string strSql, IList<ParameterData> parameters);

        /// <summary>
        /// 执行Sql语句返回DataTable
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">参数列表，列表内的对象必须为ParameterData对象的列表</param>
        /// <param name="commandType">命令类型</param>
        DataTable ExecuteForDataTable(string strSql, IList<ParameterData> parameters, CommandType commandType);

        #endregion

        #region 执行返回DataSet的Sql语句

        /// <summary>
        /// 执行Sql语句返回DataSet
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        DataSet ExecuteForDataSet(string strSql);

        /// <summary>
        /// 执行Sql语句返回DataSet
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        DataSet ExecuteForDataSet(string strSql,CommandType commandType);

        /// <summary>
        /// 执行Sql语句返回DataSet
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">参数列表，列表内的对象必须为ParameterData对象的列表</param>
        DataSet ExecuteForDataSet(string strSql, IList<ParameterData> parameters);

        /// <summary>
        /// 执行Sql语句返回DataSet
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="parameters">参数列表，列表内的对象必须为ParameterData对象的列表</param>
        /// <param name="commandType">命令类型</param>
        DataSet ExecuteForDataSet(string strSql, IList<ParameterData> parameters, CommandType commandType);

        /// <summary>
        /// 执行Sql语句返回DataSet
        /// </summary>
        /// <param name="strSql">Sql语句</param>
        /// <param name="paramName">参数名</param>
        /// <param name="paramValue">参数值</param>
        /// <param name="commandType">命令类型</param>
        T ExecuteForDataSet<T>(string strSql, IList<ParameterData> parameters, CommandType commandType) where T : DataSet, new();

        #endregion

        #region 执行返回单一值的SQL

        /// <summary>
        /// 执行返回单一值的SQL或存储过程
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        object ExecuteForScalar(string strSql, IList<ParameterData> parameters, CommandType commandType);

        #endregion
    }
}
