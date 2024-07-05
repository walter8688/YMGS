using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.DataBase;
using YMGS.Framework;
using System.Data;
using YMGS.Data.Presentation;
using YMGS.Data.Entity;
using YMGS.Data.Common;

namespace YMGS.DataAccess.GameMarket
{
    public class MatchDA : DaBase
    {
        #region 查询比赛

        /// <summary>
        /// 查询比赛信息
        /// </summary>
        /// <param name="matchName">比赛名称</param>
        /// <param name="matchEventItem">赛事项目</param>
        /// <param name="matchEventZone">赛事区域</param>
        /// <param name="eventName">赛事名称</param>
        /// <param name="beginMatchDate">开始比赛时间</param>
        /// <param name="endMatchDate">结束比赛时间</param>
        /// <returns></returns>
        public static DsMatchList QueryMatchByParam(string matchName, int? matchEventItem, int? matchEventZone,
                            string eventName, DateTime? beginMatchDate, DateTime? endMatchDate)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Match_Name",ParameterType=DbType.String,ParameterValue=matchName},
                new ParameterData(){ParameterName="@Match_Event_Zone",ParameterType=DbType.Int32,ParameterValue=matchEventZone.Value},
                new ParameterData(){ParameterName="@Match_Event_Item",ParameterType=DbType.Int32,ParameterValue=matchEventItem.Value},
                new ParameterData(){ParameterName="@Event_Name",ParameterType=DbType.String,ParameterValue=eventName},
                new ParameterData(){ParameterName="@Start_Date",ParameterType=DbType.DateTime,ParameterValue=beginMatchDate},
                new ParameterData(){ParameterName="@End_Date",ParameterType=DbType.DateTime,ParameterValue=endMatchDate}
            };

            return SQLHelper.ExecuteStoredProcForDataSet<DsMatchList>("pr_get_match_by_param", parameters);
        }

        /// <summary>
        /// 按照比赛ID查询比赛基本信息
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public static DSMatch QueryMatchById(int matchId)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Match_Id",ParameterType=DbType.Int32,ParameterValue=matchId}
            };
            return SQLHelper.ExecuteStoredProcForDataSet<DSMatch>("pr_get_match_by_id", parameters);
        }

        /// <summary>
        /// 按照比赛ID查询比赛基本信息
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public static DSMatchMarket QueryMatchMarketById(int matchId)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Match_Id",ParameterType=DbType.Int32,ParameterValue=matchId}
            };

            return SQLHelper.ExecuteStoredProcForDataSet<DSMatchMarket>("pr_get_match_market_by_matchid", parameters);
        }

        public static DSMatchMarket QueryMatchMarketByTmpId(int marketTmpId)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@marketTmpId",ParameterType=DbType.Int32,ParameterValue=marketTmpId}
            };
            return SQLHelper.ExecuteStoredProcForDataSet<DSMatchMarket>("pr_get_match_market_by_markettmpid", parameters);
        }

        public static DSMatchMarket QueryMatchMarketByEventId(int EventId)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Match_Id",ParameterType=DbType.Int32,ParameterValue=EventId}
            };

            return SQLHelper.ExecuteStoredProcForDataSet<DSMatchMarket>("pr_get_champion_match_market_by_eventid", parameters);
        }

        /// <summary>
        /// 查询当前可以参与下注的比赛和比赛市场信息[具体是否能下注，还需要参考玩法和配置信息]
        /// </summary>
        /// <returns></returns>
        public static DSMatchAndMarket QueryMatchAndMarketForBetting()
        {
            return SQLHelper.ExecuteStoredProcForDataSet<DSMatchAndMarket>("pr_get_match_market_for_betting", null);
        }

        #endregion

        #region 终止比赛

        /// <summary>
        /// 终止比赛
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="curUserId"></param>
        /// <returns></returns>
        public static void AbortMatch(int matchId,int curUserId)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Match_Id",ParameterType=DbType.Int32,ParameterValue=matchId},
                new ParameterData(){ParameterName="@Last_Update_User",ParameterType=DbType.Int32,ParameterValue=curUserId}
            };
            SQLHelper.ExecuteNonQueryStoredProcedure("pr_abort_match", parameters);
        }

        #endregion

        #region 激活比赛

        /// <summary>
        /// 激活比赛
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="curUserId"></param>
        /// <returns></returns>
        public static void ActivateMatch(int matchId, int curUserId)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Match_Id",ParameterType=DbType.Int32,ParameterValue=matchId},
                new ParameterData(){ParameterName="@Last_Update_User",ParameterType=DbType.Int32,ParameterValue=curUserId}
            };
            SQLHelper.ExecuteNonQueryStoredProcedure("pr_activate_match", parameters);
        }

        #endregion

        #region 开始下半场比赛
        /// <summary>
        /// 开始下半场比赛
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="curUserId"></param>
        public static void SecHalfStartMatch(int matchId, int curUserId)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Match_Id",ParameterType=DbType.Int32,ParameterValue=matchId},
                new ParameterData(){ParameterName="@Last_Update_User",ParameterType=DbType.Int32,ParameterValue=curUserId}
            };
            SQLHelper.ExecuteNonQueryStoredProcedure("pr_sechalfstart_match", parameters);
        }
        #endregion

        #region 删除比赛

        /// <summary>
        /// 删除比赛
        /// </summary>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public static void DeleteMatch(int matchId)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Match_Id",ParameterType=DbType.Int32,ParameterValue=matchId}
            };
            SQLHelper.ExecuteNonQueryStoredProcedure("pr_del_match", parameters);
        }

        #endregion

        #region 更新比赛时间
        /// <summary>
        /// 更新比赛时间
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="autoFreezeDate"></param>
        /// <param name="curUserId"></param>
        public static void ModifyMatchTime(int matchId, DateTime startDate,DateTime endDate,DateTime autoFreezeDate, int curUserId)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Match_Id",ParameterType=DbType.Int32,ParameterValue=matchId},
                new ParameterData(){ParameterName="@StartDate",ParameterType=DbType.DateTime,ParameterValue=startDate},
                new ParameterData(){ParameterName="@Auto_Freeze_Date",ParameterType=DbType.DateTime,ParameterValue=autoFreezeDate},
                new ParameterData(){ParameterName="@Last_Update_User",ParameterType=DbType.Int32,ParameterValue=curUserId}
            };
            if(endDate == DateTime.MinValue)
                parameters.Add(new ParameterData() { ParameterName = "@EndDate", ParameterType = DbType.DateTime, ParameterValue = DBNull.Value });
            else
                parameters.Add(new ParameterData() { ParameterName = "@EndDate", ParameterType = DbType.DateTime, ParameterValue = endDate });
            SQLHelper.ExecuteNonQueryStoredProcedure("pr_modify_match_datetime", parameters);
        }

        #endregion

        #region 暂停比赛

        /// <summary>
        /// 暂停比赛
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="curUserId"></param>
        /// <returns></returns>
        public static void SuspendMatch(int matchId, int curUserId)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Match_Id",ParameterType=DbType.Int32,ParameterValue=matchId},
                new ParameterData(){ParameterName="@Last_Update_User",ParameterType=DbType.Int32,ParameterValue=curUserId}
            };
            SQLHelper.ExecuteNonQueryStoredProcedure("pr_suspend_match", parameters);
        }

        #endregion

        #region 新增和更新比赛

        /// <summary>
        /// 新增比赛
        /// </summary>
        /// <param name="dsMatchInfo"></param>
        public static int AddMatch(DSMatch dsMatchInfo)
        {
            IPersistBroker broker = PersistBroker.GetPersistBroker();
            try
            {
                broker.BeginTrans();

                //新增比赛
                int matchId = Convert.ToInt32(broker.ExecuteForScalar("pr_add_match",GetMatchParamListForAdd(dsMatchInfo.TB_MATCH[0]),CommandType.StoredProcedure));

                //新增比赛市场
                for (int i = 0; i < dsMatchInfo.TB_MATCH_MARKET.Rows.Count;i++ )
                {
                    var curMarket = dsMatchInfo.TB_MATCH_MARKET[i];
                    decimal? dblScoreA = null;
                    decimal? dblScoreB = null;
                    if (!curMarket.IsSCOREANull())
                        dblScoreA = curMarket.SCOREA;
                    if (!curMarket.IsSCOREBNull())
                        dblScoreB = curMarket.SCOREB;

                    IList<ParameterData> marketParams = new List<ParameterData>(){
                        new ParameterData(){ParameterName="@Market_Name",ParameterType=DbType.String,ParameterValue=curMarket.MARKET_NAME},
                        new ParameterData(){ParameterName="@Market_Name_En",ParameterType=DbType.String,ParameterValue=curMarket.MARKET_NAME_EN},
                        new ParameterData(){ParameterName="@Match_Id",ParameterType=DbType.Int32,ParameterValue=matchId},
                        new ParameterData(){ParameterName="@Market_Tmp_Id",ParameterType=DbType.Int32,ParameterValue=curMarket.MARKET_TMP_ID},
                        new ParameterData(){ParameterName="@Market_Flag",ParameterType=DbType.Int32,ParameterValue=curMarket.MARKET_FLAG},
                        new ParameterData(){ParameterName="@ScoreA",ParameterType=DbType.Decimal,ParameterValue=dblScoreA},
                        new ParameterData(){ParameterName="@ScoreB",ParameterType=DbType.Decimal,ParameterValue=dblScoreB}
                    };
                    broker.ExecuteNonQuery("pr_add_match_market", marketParams, CommandType.StoredProcedure);
                }

                broker.CommitTrans();

                return matchId;
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

        /// <summary>
        /// 更新比赛
        /// </summary>
        /// <param name="dsMatchInfo"></param>
        public static void UpdateMatch(DSMatch dsMatchInfo)
        {
            IPersistBroker broker = PersistBroker.GetPersistBroker();
            try
            {
                broker.BeginTrans();

                int matchId = dsMatchInfo.TB_MATCH[0].MATCH_ID;

                //删除比赛下的市场
                IList<ParameterData> delMarketParams = new List<ParameterData>(){
                    new ParameterData(){ParameterName="@Match_Id",ParameterType=DbType.Int32,ParameterValue=matchId}
                };
                broker.ExecuteNonQuery("pr_del_match_market_by_match_id", delMarketParams, CommandType.StoredProcedure);


                //更新比赛
                broker.ExecuteNonQuery("pr_up_match", GetMatchParamListForEdit(dsMatchInfo.TB_MATCH[0]), CommandType.StoredProcedure);

                //重新新增比赛市场
                for (int i = 0; i < dsMatchInfo.TB_MATCH_MARKET.Rows.Count; i++)
                {
                    var curMarket = dsMatchInfo.TB_MATCH_MARKET[i];
                    decimal? dblScoreA = null;
                    decimal? dblScoreB = null;
                    if (!curMarket.IsSCOREANull())
                        dblScoreA = curMarket.SCOREA;
                    if (!curMarket.IsSCOREANull())
                        dblScoreB = curMarket.SCOREB;

                    IList<ParameterData> marketParams = new List<ParameterData>(){
                        new ParameterData(){ParameterName="@Market_Name",ParameterType=DbType.String,ParameterValue=curMarket.MARKET_NAME},
                        new ParameterData(){ParameterName="@Market_Name_En",ParameterType=DbType.String,ParameterValue=curMarket.MARKET_NAME_EN},
                        new ParameterData(){ParameterName="@Match_Id",ParameterType=DbType.Int32,ParameterValue=matchId},
                        new ParameterData(){ParameterName="@Market_Tmp_Id",ParameterType=DbType.Int32,ParameterValue=curMarket.MARKET_TMP_ID},
                        new ParameterData(){ParameterName="@Market_Flag",ParameterType=DbType.Int32,ParameterValue=curMarket.MARKET_FLAG},
                        new ParameterData(){ParameterName="@ScoreA",ParameterType=DbType.Decimal,ParameterValue=dblScoreA},
                        new ParameterData(){ParameterName="@ScoreB",ParameterType=DbType.Decimal,ParameterValue=dblScoreB}
                    };
                    broker.ExecuteNonQuery("pr_add_match_market", marketParams, CommandType.StoredProcedure);
                }

                broker.CommitTrans();
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

        /// <summary>
        /// 获得新增比赛参数列表
        /// </summary>
        /// <param name="matchRow"></param>
        /// <returns></returns>
        private static IList<ParameterData> GetMatchParamListForAdd(DSMatch.TB_MATCHRow matchRow)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Match_Name",ParameterType=DbType.String,ParameterValue=matchRow.MATCH_NAME},
                new ParameterData(){ParameterName="@Match_Name_En",ParameterType=DbType.String,ParameterValue=matchRow.MATCH_NAME_EN},
                new ParameterData(){ParameterName="@Match_Desc",ParameterType=DbType.String,ParameterValue=matchRow.MATCH_DESC},
                new ParameterData(){ParameterName="@Event_Id",ParameterType=DbType.Int32,ParameterValue=matchRow.EVENT_ID},
                new ParameterData(){ParameterName="@Event_Home_Team_Id",ParameterType=DbType.Int32,ParameterValue=matchRow.EVENT_HOME_TEAM_ID},
                new ParameterData(){ParameterName="@Event_Home_Guest_Id",ParameterType=DbType.Int32,ParameterValue=matchRow.EVENT_HOME_GUEST_ID},
                new ParameterData(){ParameterName="@StartDate",ParameterType=DbType.DateTime,ParameterValue=matchRow.STARTDATE},
                new ParameterData(){ParameterName="@Auto_Freeze_Date",ParameterType=DbType.DateTime,ParameterValue=matchRow.AUTO_FREEZE_DATE},                
                new ParameterData(){ParameterName="@Recommend_Match",ParameterType=DbType.Boolean,ParameterValue=matchRow.RECOMMENDMATCH},
                new ParameterData(){ParameterName="@Status",ParameterType=DbType.Int32,ParameterValue=matchRow.STATUS},
                new ParameterData(){ParameterName="@Additional_Status",ParameterType=DbType.Int32,ParameterValue=matchRow.ADDITIONALSTATUS},
                new ParameterData(){ParameterName="@Create_User",ParameterType=DbType.Int32,ParameterValue=matchRow.CREATE_USER},
                new ParameterData(){ParameterName="@Last_Update_User",ParameterType=DbType.Int32,ParameterValue=matchRow.LAST_UPDATE_USER},
                new ParameterData(){ParameterName="@Is_ZouDi",ParameterType=DbType.Boolean,ParameterValue=matchRow.IS_ZOUDI},
                new ParameterData(){ParameterName="@HandicapHalfDefault",ParameterType=DbType.String,ParameterValue=matchRow.HandicapHalfDefault},
                new ParameterData(){ParameterName="@HandicapFullDefault",ParameterType=DbType.String,ParameterValue=matchRow.HandicapFullDefault}
            };
            if(matchRow.ENDDATE == DateTime.MaxValue)
                parameters.Add(new ParameterData(){ParameterName="@EndDate",ParameterType=DbType.DateTime,ParameterValue=DBNull.Value});
            else
                parameters.Add(new ParameterData() { ParameterName = "@EndDate", ParameterType = DbType.DateTime, ParameterValue = matchRow.ENDDATE });

            return parameters;
        }

        /// <summary>
        /// 获得新增比赛参数列表
        /// </summary>
        /// <param name="matchRow"></param>
        /// <returns></returns>
        private static IList<ParameterData> GetMatchParamListForEdit(DSMatch.TB_MATCHRow matchRow)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Match_Id",ParameterType=DbType.Int32,ParameterValue=matchRow.MATCH_ID},
                new ParameterData(){ParameterName="@Match_Name",ParameterType=DbType.String,ParameterValue=matchRow.MATCH_NAME},
                new ParameterData(){ParameterName="@Match_Name_En",ParameterType=DbType.String,ParameterValue=matchRow.MATCH_NAME_EN},
                new ParameterData(){ParameterName="@Match_Desc",ParameterType=DbType.String,ParameterValue=matchRow.MATCH_DESC},
                new ParameterData(){ParameterName="@Event_Id",ParameterType=DbType.Int32,ParameterValue=matchRow.EVENT_ID},
                new ParameterData(){ParameterName="@Event_Home_Team_Id",ParameterType=DbType.Int32,ParameterValue=matchRow.EVENT_HOME_TEAM_ID},
                new ParameterData(){ParameterName="@Event_Home_Guest_Id",ParameterType=DbType.Int32,ParameterValue=matchRow.EVENT_HOME_GUEST_ID},
                new ParameterData(){ParameterName="@StartDate",ParameterType=DbType.DateTime,ParameterValue=matchRow.STARTDATE},
                new ParameterData(){ParameterName="@Auto_Freeze_Date",ParameterType=DbType.DateTime,ParameterValue=matchRow.AUTO_FREEZE_DATE},                
                new ParameterData(){ParameterName="@Recommend_Match",ParameterType=DbType.Boolean,ParameterValue=matchRow.RECOMMENDMATCH},
                new ParameterData(){ParameterName="@Status",ParameterType=DbType.Int32,ParameterValue=matchRow.STATUS},
                new ParameterData(){ParameterName="@Additional_Status",ParameterType=DbType.Int32,ParameterValue=matchRow.ADDITIONALSTATUS},
                new ParameterData(){ParameterName="@Last_Update_User",ParameterType=DbType.Int32,ParameterValue=matchRow.LAST_UPDATE_USER},
                new ParameterData(){ParameterName="@Is_ZouDi",ParameterType=DbType.Boolean,ParameterValue=matchRow.IS_ZOUDI},
                new ParameterData(){ParameterName="@HandicapHalfDefault",ParameterType=DbType.String,ParameterValue=matchRow.HandicapHalfDefault},
                new ParameterData(){ParameterName="@HandicapFullDefault",ParameterType=DbType.String,ParameterValue=matchRow.HandicapFullDefault}
            };
            if (matchRow.ENDDATE == DateTime.MaxValue)
                parameters.Add(new ParameterData() { ParameterName = "@EndDate", ParameterType = DbType.DateTime, ParameterValue = DBNull.Value });
            else
                parameters.Add(new ParameterData() { ParameterName = "@EndDate", ParameterType = DbType.DateTime, ParameterValue = matchRow.ENDDATE });
    
            return parameters;
        }

        #endregion

        #region 推荐比赛或取消比赛

        /// <summary>
        ///推荐比赛或取消比赛
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="curUserId"></param>
        /// <returns></returns>
        public static void RecommendOrCancelMatch(int matchId, int curUserId,bool bIsRecommend)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@Match_Id",ParameterType=DbType.Int32,ParameterValue=matchId},
                new ParameterData(){ParameterName="@Is_Recommend",ParameterType=DbType.Boolean,ParameterValue=bIsRecommend},
                new ParameterData(){ParameterName="@Last_Update_User",ParameterType=DbType.Int32,ParameterValue=curUserId}
            };
            SQLHelper.ExecuteNonQueryStoredProcedure("pr_recommend_match", parameters);
        }

        #endregion

        #region 正常比赛
        /// <summary>
        /// 正常比赛
        /// </summary>
        /// <param name="matchID"></param>
        /// <param name="lastUpdateUserID"></param>
        public static void NoramlMatch(int matchID, int lastUpdateUserID)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@MatchID", ParameterType = DbType.Int32, ParameterValue = matchID });
            parameters.Add(new ParameterData() { ParameterName = "@LastUpdateUser", ParameterType = DbType.Int32, ParameterValue = lastUpdateUserID });
            SQLHelper.ExecuteStoredProcForScalar("pr_normal_match_additonalstatus", parameters);
        }
        #endregion

        #region 投注
        /// <summary>
        /// 投注
        /// </summary>
        /// <param name="matchID"></param>
        /// <param name="lastUpdateUserID"></param>
        public static void ExchangeBack(string flag, DSExchange_Back.TB_EXCHANGE_BACKRow backrow)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@MATCHTYPE",ParameterType=DbType.Int32,ParameterValue=backrow.IsMATCH_TYPENull()?1:int.Parse(backrow.MATCH_TYPE)},
                new ParameterData(){ParameterName="@STATUS",ParameterType=DbType.String,ParameterValue=flag},
                new ParameterData(){ParameterName="@EXCHANGE_BACK_ID",ParameterType=DbType.Int32,ParameterValue=backrow.EXCHANGE_BACK_ID},
                new ParameterData(){ParameterName="@MATCH_ID",ParameterType=DbType.Int32,ParameterValue=backrow.IsMATCH_IDNull()?0:backrow.MATCH_ID},
                new ParameterData(){ParameterName="@MARKET_ID",ParameterType=DbType.Int32,ParameterValue=backrow.IsMARKET_IDNull()?0:backrow.MARKET_ID},
                new ParameterData(){ParameterName="@ODDS",ParameterType=DbType.Decimal,ParameterValue=backrow.IsODDSNull()?0:backrow.ODDS},
                new ParameterData(){ParameterName="@BET_AMOUNTS",ParameterType=DbType.Decimal,ParameterValue=backrow.IsBET_AMOUNTSNull()?0:backrow.BET_AMOUNTS},
                new ParameterData(){ParameterName="@MATCH_AMOUNTS",ParameterType=DbType.Decimal,ParameterValue=backrow.IsMATCH_AMOUNTSNull()?0:backrow.MATCH_AMOUNTS},
                new ParameterData(){ParameterName="@TRADE_USER",ParameterType=DbType.Int32,ParameterValue=backrow.IsTRADE_USERNull()?0:backrow.TRADE_USER}
            };
          SQLHelper.ExecuteNonQueryStoredProcedure("pr_edit_Exchange_Back", parameters);
        }
        #endregion

        #region 受注
        /// <summary>
        /// 受注
        /// </summary>
        /// <param name="matchID"></param>
        /// <param name="lastUpdateUserID"></param>
        public static void ExchangeLay(string flag, DSExchangeLay.TB_EXCHANGE_LAYRow layrow)
        {
            IList<ParameterData> parameters = new List<ParameterData>()
            {
                new ParameterData(){ParameterName="@MATCHTYPE",ParameterType=DbType.Int32,ParameterValue=int.Parse(layrow.MATCH_TYPE)},
                new ParameterData(){ParameterName="@STATUS",ParameterType=DbType.String,ParameterValue=flag},
                new ParameterData(){ParameterName="@EXCHANGE_LAY_ID",ParameterType=DbType.Int32,ParameterValue=layrow.EXCHANGE_LAY_ID},
                new ParameterData(){ParameterName="@MATCH_ID",ParameterType=DbType.Int32,ParameterValue=layrow.IsMATCH_IDNull()?0:layrow.MATCH_ID},
                new ParameterData(){ParameterName="@MARKET_ID",ParameterType=DbType.Int32,ParameterValue=layrow.IsMARKET_IDNull()?0:layrow.MARKET_ID},
                new ParameterData(){ParameterName="@ODDS",ParameterType=DbType.Decimal,ParameterValue=layrow.IsODDSNull()?0:layrow.ODDS},
                new ParameterData(){ParameterName="@BET_AMOUNTS",ParameterType=DbType.Decimal,ParameterValue=layrow.IsBET_AMOUNTSNull()?0:layrow.BET_AMOUNTS},
                new ParameterData(){ParameterName="@MATCH_AMOUNTS",ParameterType=DbType.Decimal,ParameterValue=layrow.IsMATCH_AMOUNTSNull()?0:layrow.MATCH_AMOUNTS},
                new ParameterData(){ParameterName="@TRADE_USER",ParameterType=DbType.Int32,ParameterValue=layrow.IsTRADE_USERNull()?0:layrow.TRADE_USER}
                //new ParameterData(){ParameterName="@MATCH_TYPE",ParameterType=DbType.Int32,ParameterValue=layrow.IsMATCH_TYPENull()?1:layrow.MATCH_TYPE}
            };
            SQLHelper.ExecuteNonQueryStoredProcedure("pr_edit_Exchange_Lay", parameters);
        }
        #endregion

        #region 录入比分
        /// <summary>
        /// 录入比分
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="score"></param>
        public static void RecordMatchScore(int matchId, FootBallMatchScore score)
        {
            var parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@Match_Id", ParameterType = DbType.Int32, ParameterValue = matchId });
            if (score.HomeFirHalfScore.HasValue)
                parameters.Add(new ParameterData() { ParameterName = "@Home_Fir_Half_Score", ParameterType = DbType.Int32, ParameterValue = score.HomeFirHalfScore });
            else
                parameters.Add(new ParameterData() { ParameterName = "@Home_Fir_Half_Score", ParameterType = DbType.Int32, ParameterValue = DBNull.Value });
            if (score.GuestFirHalfScore.HasValue)
                parameters.Add(new ParameterData() { ParameterName = "@Guest_Fir_Half_Score", ParameterType = DbType.Int32, ParameterValue = score.GuestFirHalfScore });
            else
                parameters.Add(new ParameterData() { ParameterName = "@Guest_Fir_Half_Score", ParameterType = DbType.Int32, ParameterValue = DBNull.Value });
            if (score.HomeSecHalfScore.HasValue)
                parameters.Add(new ParameterData() { ParameterName = "@Home_Sec_Half_Score", ParameterType = DbType.Int32, ParameterValue = score.HomeSecHalfScore });
            else
                parameters.Add(new ParameterData() { ParameterName = "@Home_Sec_Half_Score", ParameterType = DbType.Int32, ParameterValue = DBNull.Value });
            if (score.GuestSecHalfScore.HasValue)
                parameters.Add(new ParameterData() { ParameterName = "@Guest_Sec_Half_Score", ParameterType = DbType.Int32, ParameterValue = score.GuestSecHalfScore });
            else
                parameters.Add(new ParameterData() { ParameterName = "@Guest_Sec_Half_Score", ParameterType = DbType.Int32, ParameterValue = DBNull.Value });
            if (score.HomeOverTimeScore.HasValue)
                parameters.Add(new ParameterData() { ParameterName = "@Home_OverTime_Score", ParameterType = DbType.Int32, ParameterValue = score.HomeOverTimeScore });
            else
                parameters.Add(new ParameterData() { ParameterName = "@Home_OverTime_Score", ParameterType = DbType.Int32, ParameterValue = DBNull.Value });
            if (score.GuestOverTimeScore.HasValue)
                parameters.Add(new ParameterData() { ParameterName = "@Guest_OverTime_Score", ParameterType = DbType.Int32, ParameterValue = score.GuestOverTimeScore });
            else
                parameters.Add(new ParameterData() { ParameterName = "@Guest_OverTime_Score", ParameterType = DbType.Int32, ParameterValue = DBNull.Value });
            if (score.HomePointScore.HasValue)
                parameters.Add(new ParameterData() { ParameterName = "@Home_Point_Score", ParameterType = DbType.Int32, ParameterValue = score.HomePointScore });
            else
                parameters.Add(new ParameterData() { ParameterName = "@Home_Point_Score", ParameterType = DbType.Int32, ParameterValue = DBNull.Value });
            if (score.GuestPointScore.HasValue)
                parameters.Add(new ParameterData() { ParameterName = "@Guest_Point_Score", ParameterType = DbType.Int32, ParameterValue = score.GuestPointScore });
            else
                parameters.Add(new ParameterData() { ParameterName = "@Guest_Point_Score", ParameterType = DbType.Int32, ParameterValue = DBNull.Value });
            if (score.HomeFullScore.HasValue)
                parameters.Add(new ParameterData() { ParameterName = "@Home_Full_Score", ParameterType = DbType.Int32, ParameterValue = score.HomeFullScore });
            else
                parameters.Add(new ParameterData() { ParameterName = "@Home_Full_Score", ParameterType = DbType.Int32, ParameterValue = DBNull.Value });
            if (score.GuestFullScore.HasValue)
                parameters.Add(new ParameterData() { ParameterName = "@Guest_Full_Score", ParameterType = DbType.Int32, ParameterValue = score.GuestFullScore });
            else
                parameters.Add(new ParameterData() { ParameterName = "@Guest_Full_Score", ParameterType = DbType.Int32, ParameterValue = DBNull.Value });
            SQLHelper.ExecuteStoredProcForScalar("pr_record_match_score", parameters);
        }
        #endregion

        #region 开始比赛
        /// <summary>
        /// 开始比赛
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="lastUpdateUserId"></param>
        public static void StartMatch(int matchId, int lastUpdateUserId)
        {
            var parameters = new List<ParameterData>() 
            { 
                new ParameterData(){ ParameterName = "@Match_Id", ParameterType = DbType.Int32, ParameterValue = matchId},
                new ParameterData(){ ParameterName = "@Last_Update_User", ParameterType = DbType.Int32, ParameterValue = lastUpdateUserId}
            };
            SQLHelper.ExecuteStoredProcForScalar("pr_start_match", parameters);

        }
        #endregion

        #region 半场结束
        /// <summary>
        /// 比赛半场结束
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="lastUpdateUserId"></param>
        public static void HalfEndMatch(int matchId,int lastUpdateUserId)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@Match_Id", ParameterType = DbType.Int32, ParameterValue = matchId });
            parameters.Add(new ParameterData() { ParameterName = "@Last_Update_User_Id", ParameterType = DbType.Int32, ParameterValue = lastUpdateUserId });
            SQLHelper.ExecuteStoredProcForScalar("pr_half_end_match", parameters);
        }
        #endregion

        #region 全场结束
        /// <summary>
        /// 比赛全场结束
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="lastUpdateUserId"></param>
        public static void FullEndMatch(int matchId, int lastUpdateUserId)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@Match_Id", ParameterType = DbType.Int32, ParameterValue = matchId });
            parameters.Add(new ParameterData() { ParameterName = "@Last_Update_User_Id", ParameterType = DbType.Int32, ParameterValue = lastUpdateUserId });
            SQLHelper.ExecuteStoredProcForScalar("pr_full_end_match", parameters);
        }
        #endregion

        #region 封盘比赛
        /// <summary>
        /// 封盘比赛
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="lastUpdateUserId"></param>
        public static void FreezingMatch(int matchId, int lastUpdateUserId)
        {
            IList<ParameterData> parameters = new List<ParameterData>();
            parameters.Add(new ParameterData() { ParameterName = "@Match_Id", ParameterType = DbType.Int32, ParameterValue = matchId });
            parameters.Add(new ParameterData() { ParameterName = "@Last_Update_User_Id", ParameterType = DbType.Int32, ParameterValue = lastUpdateUserId });
            SQLHelper.ExecuteStoredProcForScalar("pr_freezing_match", parameters);
        }
        #endregion

        #region 获取投注Top3
        public static DSCachedExchangeBack GetCachedExchangeBack()
        {
            return SQLHelper.ExecuteStoredProcForDataSet<DSCachedExchangeBack>("pr_get_exchange_back_cache", null); 
        }
        #endregion

        #region 获取受注Top3
        public static DSCachedExchangeLay GetCachedExchangeLay()
        {
            return SQLHelper.ExecuteStoredProcForDataSet<DSCachedExchangeLay>("pr_get_exchange_lay_cache", null);
        }
        #endregion
    }
}
