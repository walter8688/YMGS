using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Data;
using YMGS.Data.DataBase;
using YMGS.Framework;
using YMGS.Data.Presentation;

namespace YMGS.DataAccess.AssistManage
{
    public class ParamZoneDA : DaBase
    {
        #region 获取区域参数
        public static DSParamZone QueryParamZone()
        {
            var persistBroker = PersistBroker.GetPersistBroker();
            try
            {
                var resultDS = persistBroker.ExecuteForDataSet<DSParamZone>("pr_get_param_zone", null, CommandType.StoredProcedure);
                return resultDS;
            }
            finally
            {
                persistBroker.Close();
            }
        }
        #endregion

        #region 删除区域参数
        public static int DelParamZone(DSParamZone.TB_PARAM_ZONERow row)
        {
            var persistBroker = PersistBroker.GetPersistBroker();
            try
            {
                IList<ParameterData> paramters = new List<ParameterData>()
                {
                    new ParameterData(){ParameterName="Zone_ID",ParameterType=DbType.Int32,ParameterValue=row.ZONE_ID}
                };
                var retuenValue = persistBroker.ExecuteForScalar("pr_del_param_zone", paramters, CommandType.StoredProcedure);
                return Convert.ToInt32(retuenValue);
            }
            finally
            {
                persistBroker.Close();
            }
        }
        #endregion

        #region 更新区域数据
        public static int UpdateParamZone(DSParamZone.TB_PARAM_ZONERow row)
        {
            var persisBroker = PersistBroker.GetPersistBroker();
            try
            {
                IList<ParameterData> parameters = new List<ParameterData>()
                {
                    new ParameterData(){ParameterName="Zone_ID",ParameterType = DbType.Int32,ParameterValue = row.ZONE_ID},
                    new ParameterData(){ParameterName="Parent_Zone_ID",ParameterType = DbType.Int32,ParameterValue = row.PARENT_ZONE_ID},
                    new ParameterData(){ParameterName="Zone_Name",ParameterType = DbType.String,ParameterValue = row.ZONE_NAME}
                };
                var returnValue = persisBroker.ExecuteForScalar("pr_up_param_zone", parameters, CommandType.StoredProcedure);
                return Convert.ToInt32(returnValue);
            }
            finally
            {
                persisBroker.Close();
            }
        }
        #endregion

        #region 新增区域数据
        public static int AddParamZone(DSParamZone.TB_PARAM_ZONERow row)
        {
            var persisBroker = PersistBroker.GetPersistBroker();
            try
            {
                IList<ParameterData> parameters = new List<ParameterData>()
                {
                    new ParameterData(){ParameterName="Parent_Zone_ID",ParameterType = DbType.Int32,ParameterValue = row.PARENT_ZONE_ID},
                    new ParameterData(){ParameterName="Zone_Name",ParameterType = DbType.String,ParameterValue = row.ZONE_NAME}
                };
                var returnValue = persisBroker.ExecuteForScalar("pr_add_param_zone", parameters, CommandType.StoredProcedure);
                return Convert.ToInt32(returnValue);
            }
            finally
            {
                persisBroker.Close();
            }
        }
        #endregion
    }

    public class TopRaceDA : DaBase
    {
        #region 置顶比赛
        public static DSTopRace QueryTopRace()
        {
            var persistBroker = PersistBroker.GetPersistBroker();
            try
            {
                IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@op_flag",ParameterType=DbType.Int32,ParameterValue=0},
                new ParameterData(){ParameterName="@matchid",ParameterType=DbType.Int32,ParameterValue=0},
                new ParameterData(){ParameterName="@cnpic",ParameterType=DbType.Binary,ParameterValue=new byte[]{}},
                new ParameterData(){ParameterName="@enpic",ParameterType=DbType.Binary,ParameterValue=new byte[]{}},
                new ParameterData(){ParameterName="@cntitle",ParameterType=DbType.String,ParameterValue=""},
                new ParameterData(){ParameterName="@entitle",ParameterType=DbType.String,ParameterValue=""},
                new ParameterData(){ParameterName="@cncontent",ParameterType=DbType.String,ParameterValue=""},
                 new ParameterData(){ParameterName="@encontent",ParameterType=DbType.String,ParameterValue=""}
            };
                var resultDS = persistBroker.ExecuteForDataSet<DSTopRace>("pr_edit_toprace", parameters, CommandType.StoredProcedure);
                return resultDS;
            }
            finally
            {
                persistBroker.Close();
            }
        }
        public static void EditTopRace(DSTopRace.TB_AD_TOPRACERow row, int flag)
        {
            var persistBroker = PersistBroker.GetPersistBroker();
            try
            {
                IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@op_flag",ParameterType=DbType.Int32,ParameterValue=flag},
                new ParameterData(){ParameterName="@matchid",ParameterType=DbType.Int32,ParameterValue=row.MARCHID},
                new ParameterData(){ParameterName="@cnpic",ParameterType=DbType.Binary,ParameterValue=row.CNPIC},
                new ParameterData(){ParameterName="@enpic",ParameterType=DbType.Binary,ParameterValue=row.ENPIC},
                new ParameterData(){ParameterName="@cntitle",ParameterType=DbType.String,ParameterValue=row.CNTITLE},
                new ParameterData(){ParameterName="@entitle",ParameterType=DbType.String,ParameterValue=row.ENTITLE},
                new ParameterData(){ParameterName="@cncontent",ParameterType=DbType.String,ParameterValue=row.CNCONTENT},
                new ParameterData(){ParameterName="@encontent",ParameterType=DbType.String,ParameterValue=row.ENCONTENT}
            };
                persistBroker.ExecuteForScalar("pr_edit_toprace", parameters, CommandType.StoredProcedure);
            }
            finally
            {
                persistBroker.Close();
            }
        }
        #endregion
    }

    public class NoticeDA : DaBase
    {
        #region 获取区域参数
        public static DSNOTICE QueryNotice(DSNOTICE.TB_AD_NOTICERow row, int flag)
        {
            var persistBroker = PersistBroker.GetPersistBroker();
            try
            {
                IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@op_flag",ParameterType=DbType.Int32,ParameterValue=flag},
                new ParameterData(){ParameterName="@id",ParameterType=DbType.Int32,ParameterValue=row.PID},
                new ParameterData(){ParameterName="@title",ParameterType=DbType.String,ParameterValue=row.IsTITLENull()?"":row.TITLE},
                new ParameterData(){ParameterName="@entitle",ParameterType=DbType.String,ParameterValue=""},
                new ParameterData(){ParameterName="@content",ParameterType=DbType.String,ParameterValue=row.IsCONTENTNull()?"":row.CONTENT},
                new ParameterData(){ParameterName="@encontent",ParameterType=DbType.String,ParameterValue=""},
                new ParameterData(){ParameterName="@isv",ParameterType=DbType.Int32,ParameterValue=row.ISV}
            };
                var resultDS = persistBroker.ExecuteForDataSet<DSNOTICE>("pr_get_notice", parameters, CommandType.StoredProcedure);
                return resultDS;
            }
            finally
            {
                persistBroker.Close();
            }
        }
        public static void EditNotice(DSNOTICE.TB_AD_NOTICERow row, int flag)
        {
            var persistBroker = PersistBroker.GetPersistBroker();
            try
            {
                IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@op_flag",ParameterType=DbType.Int32,ParameterValue=flag},
                new ParameterData(){ParameterName="@id",ParameterType=DbType.Int32,ParameterValue=row.PID},
                new ParameterData(){ParameterName="@title",ParameterType=DbType.String,ParameterValue=row.TITLE},
                new ParameterData(){ParameterName="@entitle",ParameterType=DbType.String,ParameterValue=row.ENTITLE},
                new ParameterData(){ParameterName="@content",ParameterType=DbType.String,ParameterValue=row.CONTENT},
                new ParameterData(){ParameterName="@encontent",ParameterType=DbType.String,ParameterValue=row.ENCONTENT},
                new ParameterData(){ParameterName="@isv",ParameterType=DbType.Int32,ParameterValue=row.ISV}
            };
                persistBroker.ExecuteForScalar("pr_get_notice", parameters, CommandType.StoredProcedure);
            }
            finally
            {
                persistBroker.Close();
            }
        }

       

        #endregion
    }
}
