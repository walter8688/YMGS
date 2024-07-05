using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Framework;
using YMGS.Data.DataBase;
using YMGS.DataAccess.AssistManage;
using YMGS.Business.GameMarket;
using YMGS.Data.Presentation;

namespace YMGS.Business.AssistManage
{
    public class OddsCompareManager : BrBase
    {
        public static DSODDSCOMPARE QueryOddsCompare1(string matchname)
        {
            DSODDSCOMPARE oddsDS = OddsCompareDA.QueryOddsCompare();
            var matchDS = MatchManager.QueryMatchAndMarketForBetting();
            DSMatchAndMarket.Match_ListDataTable matchTB = matchDS.Match_List;
           var data= from m in matchTB.Where(s=>s.MATCH_NAME.StartsWith(matchname))
                     join o in oddsDS.TB_ODDS_COMPARE on m.MATCH_ID equals o.MATCHID into oc
                     from x in oc.DefaultIfEmpty()
                     select new{
                     matchname=m.MATCH_NAME,
                     matchid=m.MATCH_ID,
                     x.CN_CORP,x.EN_CORP,x.PROFIT
                     };
            return oddsDS;
        }
        public static DSODDSCOMPARE QueryOddsCompare2()
        {
            DSODDSCOMPARE oddsDS = OddsCompareDA.QueryOddsCompare();
            return oddsDS;
        }

        public static void EditDSADWords(DSODDSCOMPARE.TB_ODDS_COMPARERow row,int flag)
        {
            OddsCompareDA.EditOddsCompare(row, flag);
        }
    }
}
