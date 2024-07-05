using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Framework;
using YMGS.Data.DataBase;
using YMGS.DataAccess.GameMarket;

namespace YMGS.Business.GameMarket
{
    public class BetTypeManager : BrBase
    {
        /// <summary>
        /// 获得所有的交易类型
        /// </summary>
        /// <returns></returns>
        public static DSBetType QueryAllBetType()
        {
            return BetTypeDA.QueryAllBetType();
        }
        public static DSDeal QueryDeal(int BETTYPE, int BETID, int MATCH_ID, int MATCH_TYPE)
        {
            return BetTypeDA.QueryDeal(BETTYPE, BETID, MATCH_ID, MATCH_TYPE);
        }
        /// <summary>
        /// 保存交易类型
        /// </summary>
        /// <param name="betDT"></param>
        public static void SaveBetType(DSBetType.TB_BET_TYPERow betRow)
        {
            BetTypeDA.SaveBetType(betRow);
        }
    }
}
