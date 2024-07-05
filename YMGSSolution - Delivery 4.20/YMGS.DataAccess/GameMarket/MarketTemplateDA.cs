using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.DataBase;
using YMGS.Framework;
using System.Data;
namespace YMGS.DataAccess.GameMarket
{
    public class MarketTemplateDA:DaBase
    {
        /// <summary>
        /// 按照主键查询市场模板
        /// </summary>
        /// <returns></returns>
        public static DSMarketTemplate QueryMarketTemplateById(int? marketTypeId)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Market_Tmp_id",ParameterType=DbType.Int32,ParameterValue=marketTypeId}
            };

            var resultDS = SQLHelper.ExecuteStoredProcForDataSet<DSMarketTemplate>("pr_get_market_template_by_id", parameters);
            return resultDS;
        }

        /// <summary>
        /// 按照条件查询市场模板
        /// </summary>
        /// <returns></returns>
        public static DSMarketTemplate QueryMarketTemplateByParam(int? betTypeId, string marketTypeName, int marketTMPType)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Bet_Type_Id",ParameterType=DbType.Int32,ParameterValue=betTypeId},
                new ParameterData(){ParameterName="@Market_Tmp_Name",ParameterType=DbType.String,ParameterValue=marketTypeName},
                new ParameterData(){ParameterName="@Market_TMP_Type",ParameterType=DbType.Int32,ParameterValue=marketTMPType}
            };

            var resultDS = SQLHelper.ExecuteStoredProcForDataSet<DSMarketTemplate>("pr_get_market_template_by_param", parameters);
            return resultDS;
        }


        /// <summary>
        /// 新增市场模板
        /// </summary>
        /// <param name="betDT"></param>
        public static int AddMarketTemplate(DSMarketTemplate.TB_MARKET_TEMPLATERow marketRow)
        {
            int? iHomeScore = null;
            int? iAwayScore = null;
            decimal? dblGoals = null;
            decimal? dblScoreA = null;
            decimal? dblScoreB = null;
            
            if(!marketRow.IsHOMESCORENull())
                iHomeScore = marketRow.HOMESCORE;
            if (!marketRow.IsAWAYSCORENull())
                iAwayScore = marketRow.AWAYSCORE;
            if (!marketRow.IsGOALSNull())
                dblGoals = marketRow.GOALS;
            if (!marketRow.IsSCOREANull())
                dblScoreA = marketRow.SCOREA;
            if (!marketRow.IsSCOREBNull())
                dblScoreB = marketRow.SCOREB;

            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Market_Tmp_Name",ParameterType=DbType.String,ParameterValue=marketRow.MARKET_TMP_NAME},
                new ParameterData(){ParameterName="@Market_Tmp_Name_En",ParameterType=DbType.String,ParameterValue=marketRow.MARKET_TMP_NAME_EN},
                new ParameterData(){ParameterName="@Bet_Type_Id",ParameterType=DbType.String,ParameterValue=marketRow.BET_TYPE_ID},
                new ParameterData(){ParameterName="@Market_Tmp_Type",ParameterType=DbType.Int16,ParameterValue=marketRow.Market_Tmp_Type},
                new ParameterData(){ParameterName="@Home_Score",ParameterType=DbType.Int32,ParameterValue=iHomeScore},
                new ParameterData(){ParameterName="@Away_Score",ParameterType=DbType.Int32,ParameterValue=iAwayScore},
                new ParameterData(){ParameterName="@Goals",ParameterType=DbType.Decimal,ParameterValue=dblGoals},
                new ParameterData(){ParameterName="@ScoreA",ParameterType=DbType.Decimal,ParameterValue=dblScoreA},
                new ParameterData(){ParameterName="@ScoreB",ParameterType=DbType.Decimal,ParameterValue=dblScoreB},
                new ParameterData(){ParameterName="@Create_User",ParameterType=DbType.Int32,ParameterValue=marketRow.CREATE_USER},
                new ParameterData(){ParameterName="@Last_Update_User",ParameterType=DbType.Int32,ParameterValue=marketRow.LAST_UPDATE_USER}
            };
            var returnValue = SQLHelper.ExecuteStoredProcForScalar("pr_add_market_template", parameters);
            return Convert.ToInt32(returnValue);
        }

        /// <summary>
        /// 更新市场模板
        /// </summary>
        /// <param name="betDT"></param>
        public static int UpdateMarketTemplate(DSMarketTemplate.TB_MARKET_TEMPLATERow marketRow)
        {
            int? iHomeScore = null;
            int? iAwayScore = null;
            decimal? dblGoals = null;
            decimal? dblScoreA = null;
            decimal? dblScoreB = null;

            if (!marketRow.IsHOMESCORENull())
                iHomeScore = marketRow.HOMESCORE;
            if (!marketRow.IsAWAYSCORENull())
                iAwayScore = marketRow.AWAYSCORE;
            if (!marketRow.IsGOALSNull())
                dblGoals = marketRow.GOALS;
            if (!marketRow.IsSCOREANull())
                dblScoreA = marketRow.SCOREA;
            if (!marketRow.IsSCOREBNull())
                dblScoreB = marketRow.SCOREB;

            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Market_Tmp_Id",ParameterType=DbType.Int32,ParameterValue=marketRow.MARKET_TMP_ID},
                new ParameterData(){ParameterName="@Market_Tmp_Name",ParameterType=DbType.String,ParameterValue=marketRow.MARKET_TMP_NAME},
                new ParameterData(){ParameterName="@Market_Tmp_Name_En",ParameterType=DbType.String,ParameterValue=marketRow.MARKET_TMP_NAME_EN},
                new ParameterData(){ParameterName="@Bet_Type_Id",ParameterType=DbType.String,ParameterValue=marketRow.BET_TYPE_ID},
                new ParameterData(){ParameterName="@Market_Tmp_Type",ParameterType=DbType.Int16,ParameterValue=marketRow.Market_Tmp_Type},
                new ParameterData(){ParameterName="@Home_Score",ParameterType=DbType.Int32,ParameterValue=iHomeScore},
                new ParameterData(){ParameterName="@Away_Score",ParameterType=DbType.Int32,ParameterValue=iAwayScore},
                new ParameterData(){ParameterName="@Goals",ParameterType=DbType.Decimal,ParameterValue=dblGoals},
                new ParameterData(){ParameterName="@ScoreA",ParameterType=DbType.Decimal,ParameterValue=dblScoreA},
                new ParameterData(){ParameterName="@ScoreB",ParameterType=DbType.Decimal,ParameterValue=dblScoreB},
                new ParameterData(){ParameterName="@Last_Update_User",ParameterType=DbType.Int32,ParameterValue=marketRow.LAST_UPDATE_USER}
            };
            var returnValue = SQLHelper.ExecuteStoredProcForScalar("pr_up_market_template", parameters);
            return Convert.ToInt32(returnValue);
        }


        /// <summary>
        /// 删除市场模板
        /// </summary>
        /// <param name="marketTmpId"></param>
        public static void DeleteMarketTemplate(int marketTmpId)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Market_Tmp_Id",ParameterType=DbType.Int32,ParameterValue=marketTmpId}
            };
            SQLHelper.ExecuteNonQueryStoredProcedure("pr_del_market_template_by_id", parameters);            
        }
    }
}
