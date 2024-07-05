using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YMGS.Data;
using YMGS.Data.DataBase;
using YMGS.Framework;
using YMGS.DataAccess.AssistManage;
using YMGS.Data.Presentation;

namespace YMGS.Business.AssistManage
{
    public class ParamZoneManager : BrBase
    {
        #region 获取区域参数
        public static DSParamZone QueryParamZone()
        {
            return ParamZoneDA.QueryParamZone();
        }
        #endregion

        #region 根据ZoneID获取区域参数
        public static DataView QueryParamZoneByParentZoneID(DSParamZone paranZone, int ParentZoneID)
        {
            DataTable paramZoneTemp = paranZone.Copy().Tables[0];
            if (paramZoneTemp == null)
                return null;
            if (paramZoneTemp.Rows.Count == 0)
                return null;

            DataView dv = paramZoneTemp.DefaultView;
            dv.RowFilter = string.Format("Parent_Zone_ID = {0}", ParentZoneID);
            return dv;
        }
        #endregion

        #region 删除区域参数
        public static int DelParamZone(DSParamZone.TB_PARAM_ZONERow row)
        {
            return ParamZoneDA.DelParamZone(row);
        }
        #endregion

        #region 更新区域数据
        public static int UpdateParamZone(DSParamZone.TB_PARAM_ZONERow row)
        {
            return ParamZoneDA.UpdateParamZone(row);
        }
        #endregion

        #region 新增区域数据
        public static int AddParamZone(DSParamZone.TB_PARAM_ZONERow row)
        {
            return ParamZoneDA.AddParamZone(row);
        }
        #endregion
    }

    public class NoticeManager : BrBase
    { 
            public static void EditNotice(DSNOTICE.TB_AD_NOTICERow row, int flag)
            {
            NoticeDA.EditNotice(row,flag);
            }

            public static DSNOTICE QueryNotice(DSNOTICE.TB_AD_NOTICERow row, int flag)
            {
                return NoticeDA.QueryNotice(row, flag);
            }
    }

    public class TopRaceManager : BrBase
    {
        public static void EditNotice(DSTopRace.TB_AD_TOPRACERow row, int flag)
        {
            TopRaceDA.EditTopRace(row, flag);
        }

        public static DSTopRace QueryTopRace()
        {
            return TopRaceDA.QueryTopRace();
        }
    }
}