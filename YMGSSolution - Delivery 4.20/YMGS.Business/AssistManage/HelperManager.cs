using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Framework;
using YMGS.Data.DataBase;
using YMGS.DataAccess.AssistManage;
using System.Data;
using YMGS.Data.Entity;
using YMGS.Business.Cache;
using YMGS.Data.Common;

namespace YMGS.Business.AssistManage
{
    public class HelperManager : BrBase
    {
        public LanguageEnum Language { get; set; }

        private readonly string scriptString = "javascript:void(0)";

        public HelperManager()
        {
        }

        public HelperManager(LanguageEnum language)
        {
            this.Language = language;
        }

        public static DSHelper QueryHelper()
        {
            DSHelper oddsDS = HelperDA.QueryHelper();
            return oddsDS;
        }

        public static void EditHelper(DSHelper.TB_HELPERRow row, int flag)
        {
            HelperDA.EditHelper(row, flag);
        }

        #region 根据PITEMID获取改列表下的目录
        public static DataView QueryHelperDataByParentItemID(DSHelper parHelperData, int PITEMID)
        {
            DataTable parHelperDataTemp = parHelperData.Copy().Tables[0];
            if (parHelperDataTemp == null)
                return null;
            if (parHelperDataTemp.Rows.Count == 0)
                return null;

            DataView dv = parHelperDataTemp.DefaultView;
            dv.RowFilter = string.Format("PITEMID = {0}", PITEMID);
            return dv;
        }
        #endregion

        #region 前台帮助文档
        /// <summary>
        /// 给前台帮助文档返回数据
        /// </summary>
        /// <returns></returns>
        public IList<HelperObject> GetHelperDataLstByCache()
        {
            DSHelper ds = (new CachedHelp()).QueryCachedData<DSHelper>();
            var dsHelper = ds.TB_HELPER;
            IList<HelperObject> helperData = new List<HelperObject>();
            foreach (var item in dsHelper)
            {
                HelperObject bo = new HelperObject();
                bo.ITEMID = item.ITEMID;
                bo.PITEMID = item.PITEMID;
                bo.ItemName = Language == LanguageEnum.Chinese ? item.CNITEMNAME : item.ENITEMNAME;
                bo.LinkAddress = Language == LanguageEnum.Chinese ? (item.IsWEBLINKNull() || string.IsNullOrEmpty(item.WEBLINK)) ? scriptString : item.WEBLINK : (item.IsENWEBLINKNull() || string.IsNullOrEmpty(item.ENWEBLINK)) ? scriptString : item.ENWEBLINK;
                bo.OrderNO = item.IsOrderNONull() ? "" : item.OrderNO;
                bo.RulesID = item.IsRulesIDNull() ? "" : item.RulesID;
                bo.IsHasChild = dsHelper.Where(x => x.PITEMID == item.ITEMID).Count() > 0 ? true : false;
                bo.LevelNO = item.IsLevelNONull() ? 0 : item.LevelNO;

                helperData.Add(bo);
            }
            return helperData;
        }

        #endregion
    }
}
