using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Framework;
using YMGS.Data.DataBase;

namespace YMGS.DataAccess.SystemConfig
{
    public class TestDA :DaBase
    {
        #region 无参数传递的查询

        public static DSTest QueryTest1()
        {
            var persistBroker = PersistBroker.GetPersistBroker();
            try
            {
                var resultDS = persistBroker.ExecuteForDataSet<DSTest>("pr_test_enum", null, CommandType.StoredProcedure);
                return resultDS;
            }
            finally
            {
                persistBroker.Close();
            }
        }

        public static DSTest QueryTest2()
        {
            var resultDS = SQLHelper.ExecuteStoredProcForDataSet<DSTest>("pr_test_enum", null);
            return resultDS;
        }

        #endregion

        #region 插入数据表数据

        public static Int32 AddTests1(DSTest.YMGS_TESTRow testRow)
        {
            var broker = PersistBroker.GetPersistBroker();
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="Names",ParameterType=DbType.String,ParameterValue=testRow.NAMES},
                new ParameterData(){ParameterName="Description",ParameterType=DbType.String,ParameterValue=testRow.DESCRIPTION}
            };

            try
            {
                var returnValue = broker.ExecuteForScalar("pr_test_ins", parameters, CommandType.StoredProcedure);
                return Convert.ToInt32(returnValue);
            }
            finally
            {
                broker.Close();
            }
        }

        public static Int32 AddTests2(DSTest.YMGS_TESTRow testRow)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Names",ParameterType=DbType.String,ParameterValue=testRow.NAMES},
                new ParameterData(){ParameterName="@Description",ParameterType=DbType.String,ParameterValue=testRow.DESCRIPTION}
            };

            var returnValue = SQLHelper.ExecuteStoredProcForScalar("pr_test_ins", parameters);
            return Convert.ToInt32(returnValue);
        }

        #endregion  

        #region 测试数据库事物

        public static Int32 AddTestsFailed(DSTest.YMGS_TESTRow testRow)
        {
            var broker = PersistBroker.GetPersistBroker();
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="Names",ParameterType=DbType.String,ParameterValue=testRow.NAMES},
                new ParameterData(){ParameterName="Description",ParameterType=DbType.String,ParameterValue=testRow.DESCRIPTION}
            };

            try
            {
                broker.BeginTrans();
                var returnValue = broker.ExecuteForScalar("pr_test_ins", parameters, CommandType.StoredProcedure);


                int i = 3;
                int j = 0;
                int s = i / j;

                broker.CommitTrans();
                return Convert.ToInt32(returnValue);
            }
            catch (Exception ex)
            {
                broker.RollbackTrans();
                throw ex;
            }
            finally
            {
                broker.Close();
            }
        }


        #endregion  
    }
}
