using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.DataBase;
using YMGS.DataAccess.SystemConfig;
using System.Transactions;
using YMGS.Framework;

namespace YMGS.Business.SystemConfig
{
    public class TestManager : BrBase
    {
        public static DSTest QueryTest1()
        {
            return TestDA.QueryTest1();
        }

        public static DSTest QueryTest2()
        {
            return TestDA.QueryTest2();
        }


        public static Int32 AddTests1(DSTest.YMGS_TESTRow testRow)
        {
            return TestDA.AddTests1(testRow);
        }

        public static Int32 AddTests2(DSTest.YMGS_TESTRow testRow)
        {
            return TestDA.AddTests2(testRow);
        }

        public static Int32 AddTestsFailed(DSTest.YMGS_TESTRow testRow)
        {
            return TestDA.AddTestsFailed(testRow);
        }


        public static void TestBusinessRule(bool isCanSuccess)
        {
            try
            {
                using (TransactionScope trans = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(0, 1, 0)))
                {
                    DSTest dsTemp = new DSTest();
                    DSTest.YMGS_TESTRow row1 = dsTemp.YMGS_TEST.NewYMGS_TESTRow();
                    row1.NAMES = "BusinessRule1";
                    row1.DESCRIPTION = "BusinessRule1";
                    AddTests1(row1);

                    if (!isCanSuccess)
                    {
                        int i = 3;
                        int j = 0;
                        int s = i / j;
                    }

                    DSTest.YMGS_TESTRow row2 = dsTemp.YMGS_TEST.NewYMGS_TESTRow();
                    row2.NAMES = "BusinessRule2";
                    row2.DESCRIPTION = "BusinessRule2";
                    AddTests1(row2);

                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
