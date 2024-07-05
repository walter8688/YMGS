using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Framework;
using YMGS.Data.DataBase;
using YMGS.DataAccess.GameMarket;

namespace YMGS.Business.GameMarket
{
    public class MarketTemplateManager : BrBase
    {
        /// <summary>
        /// 按照主键查询市场模板
        /// </summary>
        /// <returns></returns>
        public static DSMarketTemplate QueryMarketTemplateById(int? marketTypeId)
        {
            return MarketTemplateDA.QueryMarketTemplateById(marketTypeId);
        }

        /// <summary>
        /// 按照条件查询市场模板
        /// </summary>
        /// <returns></returns>
        public static DSMarketTemplate QueryMarketTemplateByParam(int? betTypeId, string marketTypeName, int marketTMPType)
        {
            return MarketTemplateDA.QueryMarketTemplateByParam(betTypeId, marketTypeName, marketTMPType);
        }


        /// <summary>
        /// 新增交易类型
        /// </summary>
        /// <param name="betDT"></param>
        public static int AddMarketTemplate(DSMarketTemplate.TB_MARKET_TEMPLATERow marketRow)
        {
            return MarketTemplateDA.AddMarketTemplate(marketRow);
        }

        /// <summary>
        /// 更新交易类型
        /// </summary>
        /// <param name="betDT"></param>
        public static int UpdateMarketTemplate(DSMarketTemplate.TB_MARKET_TEMPLATERow marketRow)
        {
            return MarketTemplateDA.UpdateMarketTemplate(marketRow);
        }

        /// <summary>
        /// 删除市场模板
        /// </summary>
        /// <param name="marketTmpId"></param>
        /// <returns>
        /// 成功：返回空
        /// 失败：返回失败原因
        /// </returns>
        public static void DeleteMarketTemplate(int marketTmpId)
        {
            MarketTemplateDA.DeleteMarketTemplate(marketTmpId);
        }
    }
}
