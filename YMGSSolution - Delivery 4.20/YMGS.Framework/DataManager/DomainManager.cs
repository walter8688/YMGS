using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Threading;
using System.Configuration;

namespace YMGS.Framework
{
    public class DomainManager
    {
        public DomainManager()
        {
        }

        public static bool InitFrameworkDomain()
        {
            try
            {
                string strDbString = System.Configuration.ConfigurationManager.AppSettings[CommConstant.DbConnStringKey];
                string strDbType = System.Configuration.ConfigurationManager.AppSettings[CommConstant.DbTypeKey];
                string strTransLevel = System.Configuration.ConfigurationManager.AppSettings[CommConstant.DbTranLevelKey];
                string strIsEncrypt = System.Configuration.ConfigurationManager.AppSettings[CommConstant.IsEncrptDbStringKey];
                if (String.IsNullOrEmpty(strDbString) || String.IsNullOrEmpty(strDbType) ||
                    string.IsNullOrEmpty(strTransLevel) || string.IsNullOrEmpty(strIsEncrypt))
                {
                    throw new ApplicationException("请正确配置数据库连接信息!");
                }

                bool bIsEncrypt = bool.Parse(strIsEncrypt);
                if(bIsEncrypt)
                    strDbString = EncryptManager.DESDeCrypt(@strDbString);


                //配置数据库连接
                ConfigInfo configObject = new ConfigInfo();
                configObject.DBConnectionString = @strDbString;
                configObject.DatabaseType = (DataBaseType)Enum.Parse(typeof(DataBaseType), @strDbType, true);
                configObject.TransIsolationLevel = (IsolationLevel)Enum.Parse(typeof(IsolationLevel), strTransLevel, true);

                Thread.GetDomain().SetData(CommConstant.DomainConfigKey, configObject);
                return true;
            }
            catch (Exception ex)
            {
                throw new DbException("Init lframework domain error", ex);
            }
        }

        public static ConfigInfo GetLFrameworkDbConfigInfo()
        {
            try
            {
                object obj = (object)Thread.GetDomain().GetData(CommConstant.DomainConfigKey);
                if (obj == null)
                {
                    InitFrameworkDomain();
                }
                obj = (object)Thread.GetDomain().GetData(CommConstant.DomainConfigKey);
                if (obj == null)
                    throw new DbException("初始化数据连接时发生错误!");
                return (ConfigInfo)obj;
            }
            catch (Exception ex)
            {
                throw new DbException("读取配置文件错误!", ex);
            }
        }
    }
}
