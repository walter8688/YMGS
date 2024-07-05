using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Entity;

namespace YMGS.Data.Common
{
    /// <summary>
    /// 初始化进度条代理
    /// </summary>
    public delegate void InitProgressDelegate();

    /// <summary>
    /// 更新进度条的代理
    /// </summary>
    /// <param name="strProgressText"></param>
    /// <param name="value"></param>
    public delegate void UpdateProgressDelegate(string strProgressText, int value);


    public class CommonFunction
    {
        /// <summary>
        /// 查询市场模板类型
        /// </summary>
        /// <returns></returns>
        public static IList<MarketTemplateType> QueryAllMarketTemplateType()
        {
            IList<MarketTemplateType> tempList = new List<MarketTemplateType>();
            tempList.Add(new MarketTemplateType()
            {
                MarketTmplateTypeId = (int)MarketTemplateTypeEnum.HalfTime,
                MarketTmplateTypeName = "半场"
            });

            tempList.Add(new MarketTemplateType()
            {
                MarketTmplateTypeId = (int)MarketTemplateTypeEnum.FullTime,
                MarketTmplateTypeName = "全场"
            });

            tempList.Add(new MarketTemplateType()
            {
                MarketTmplateTypeId = (int)MarketTemplateTypeEnum.HalfAndFullTime,
                MarketTmplateTypeName = "半场/全场"
            });

            return tempList;
        }

        /// <summary>
        /// 查询赛事成员状态类型
        /// </summary>
        /// <returns></returns>
        public static IList<EventTeamStatus> QueryAllEventTeamStatus()
        {
            IList<EventTeamStatus> eventTeamStstusList = new List<EventTeamStatus>()
            {
                new EventTeamStatus(){ EventTeamStatusId = (int)EventTeamStatusEnum.Activity, EventTeamStatusName = "启用"},
                new EventTeamStatus(){ EventTeamStatusId = (int)EventTeamStatusEnum.InActivity, EventTeamStatusName = "禁用"}
            };
            return eventTeamStstusList;
        }

        /// <summary>
        /// 获取所有赛事状态
        /// </summary>
        /// <returns></returns>
        public static IList<EventStatus> QueryAllEventStatus()
        {
            IList<EventStatus> eventStatusList = new List<EventStatus>()
            {
                new EventStatus(){ EventStatusID= (int)EventStatusEnum.Activated, EventStatusName="激活"},
                new EventStatus(){ EventStatusID= (int)EventStatusEnum.Pause, EventStatusName="暂停"},
                new EventStatus(){ EventStatusID= (int)EventStatusEnum.Abort, EventStatusName="终止"},
                new EventStatus(){ EventStatusID= (int)EventStatusEnum.UnActivated, EventStatusName="未激活"}
            };
            return eventStatusList;
        }

        /// <summary>
        /// 查询比赛状态
        /// </summary>
        /// <returns></returns>
        public static IList<MatchStatusInfo> QueryAllMatchStatus()
        {
            IList<MatchStatusInfo> tempList = new List<MatchStatusInfo>();
            tempList.Add(new MatchStatusInfo()
            {
                MatchStatus = (int)MatchStatusEnum.NotActivated,
                MatchStatusName = "未激活"
            });

            tempList.Add(new MatchStatusInfo()
            {
                MatchStatus = (int)MatchStatusEnum.Activated,
                MatchStatusName = "已激活"
            });

            tempList.Add(new MatchStatusInfo()
            {
                MatchStatus = (int)MatchStatusEnum.Abort,
                MatchStatusName = "终止"
            });

            tempList.Add(new MatchStatusInfo()
            {
                MatchStatus = (int)MatchStatusEnum.InMatching,
                MatchStatusName = "比赛已开始"
            });

            tempList.Add(new MatchStatusInfo()
            {
                MatchStatus = (int)MatchStatusEnum.HalfTimeFinished,
                MatchStatusName = "半场休息"
            });

            tempList.Add(new MatchStatusInfo()
            {
                MatchStatus = (int)MatchStatusEnum.SecHalfStarted,
                MatchStatusName = "下半场已开始"
            });

            tempList.Add(new MatchStatusInfo()
            {
                MatchStatus = (int)MatchStatusEnum.FullTimeFinished,
                MatchStatusName = "全场已结束"
            });

            tempList.Add(new MatchStatusInfo()
            {
                MatchStatus = (int)MatchStatusEnum.FinishedCalculation,
                MatchStatusName = "已结算"
            });

            return tempList;
        }

        /// <summary>
        /// 查询比赛辅助状态
        /// </summary>
        /// <returns></returns>
        public static IList<MatchStatusInfo> QueryAllMatchAdditionalStatus()
        {
            IList<MatchStatusInfo> tempList = new List<MatchStatusInfo>();
            tempList.Add(new MatchStatusInfo()
            {
                MatchStatus = (int)MatchAdditionalStatusEnum.Normal,
                MatchStatusName = "正常"
            });

            tempList.Add(new MatchStatusInfo()
            {
                MatchStatus = (int)MatchAdditionalStatusEnum.Suspended,
                MatchStatusName = "暂停"
            });

            tempList.Add(new MatchStatusInfo()
            {
                MatchStatus = (int)MatchAdditionalStatusEnum.FreezingMatch,
                MatchStatusName = "封盘"
            });

            return tempList;
        }

        /// <summary>
        /// 获取冠军赛事类别
        /// </summary>
        /// <returns></returns>
        public static IList<ChampEventType> QueryAllChampEventType()
        {
            IList<ChampEventType> champEventTypeList = new List<ChampEventType>();
            champEventTypeList.Add(new ChampEventType() { ChampEventTypeID = (int)ChampEventTypeEnum.Sports, ChampEventTypeName = "体育" });
            champEventTypeList.Add(new ChampEventType() { ChampEventTypeID = (int)ChampEventTypeEnum.Entertainment, ChampEventTypeName = "娱乐" });
            return champEventTypeList;
        }

        /// <summary>
        /// 获取代理类别
        /// </summary>
        /// <returns></returns>
        public static IList<AgentType> QueryAllAgentType()
        {
            var agentTypeList = new List<AgentType>();
            agentTypeList.Add(new AgentType() { AgentTypeId = (int)AgentTypeEnum.RootAgent, AgentTypeName = "总代理" });
            agentTypeList.Add(new AgentType() { AgentTypeId = (int)AgentTypeEnum.NoramlAgent, AgentTypeName = "代理" });
            return agentTypeList;
        }

        /// <summary>
        /// 获取用户提现状态
        /// </summary>
        /// <returns></returns>
        public static IList<UserWithDrawStatusInfo> QuertAllUserWithDrawStatus()
        {
            var userWDStatusList = new List<UserWithDrawStatusInfo>()
            {
                new UserWithDrawStatusInfo(){ WDStatusName = "申请中", WDStatusID = (int)UserWithDrawStatus.Supplying },
                new UserWithDrawStatusInfo(){ WDStatusName = "已确认", WDStatusID = (int)UserWithDrawStatus.Confirmed },
                new UserWithDrawStatusInfo(){ WDStatusName = "已拒绝", WDStatusID = (int)UserWithDrawStatus.Rejected },
                new UserWithDrawStatusInfo(){ WDStatusName = "已转账", WDStatusID = (int)UserWithDrawStatus.Transfered },
                new UserWithDrawStatusInfo(){ WDStatusName = "已取消", WDStatusID = (int)UserWithDrawStatus.Cancled }
            };
            return userWDStatusList;
        }

        /// <summary>
        /// 获取V网卡面值
        /// </summary>
        /// <returns></returns>
        public static IList<VCardFaceValueInfo> QueryAllVCardFaceValueInfo()
        {
            var VCardFaceValueList = new List<VCardFaceValueInfo>()
            {
                new VCardFaceValueInfo(){ VCardFaceValue=10, VCardFaceValueText = "10"},
                new VCardFaceValueInfo(){ VCardFaceValue=20, VCardFaceValueText = "20"},
                new VCardFaceValueInfo(){ VCardFaceValue=50, VCardFaceValueText = "50"},
                new VCardFaceValueInfo(){ VCardFaceValue=100, VCardFaceValueText = "100"},
                new VCardFaceValueInfo(){ VCardFaceValue=500, VCardFaceValueText = "500"},
                new VCardFaceValueInfo(){ VCardFaceValue=1000, VCardFaceValueText = "1000"}
            };
            return VCardFaceValueList;
        }

        /// <summary>
        /// 获取V网卡状态
        /// </summary>
        /// <returns></returns>
        public static IList<VCardStatusInfo> QueryAllVCardStatusInfo()
        {
            var VCardStatusList = new List<VCardStatusInfo>()
            {
                new VCardStatusInfo(){ VCardStatusID=(int)VCardStatusEnum.UnActivated, VCardStatusText="未激活"},
                new VCardStatusInfo(){ VCardStatusID=(int)VCardStatusEnum.Activated, VCardStatusText="已激活"}
            };
            return VCardStatusList;
        }

        public static IList<FundReportType> QueryAllFundReportTypeInfo()
        {
            var fundReportTypeList = new List<FundReportType>()
            {
                new FundReportType(){ FundReportTypeValue="0", FundRepotyTypeText="V网卡充值"},
                new FundReportType(){ FundReportTypeValue="1", FundRepotyTypeText="在线充值"},
                new FundReportType(){ FundReportTypeValue="2", FundRepotyTypeText="提现"},
                new FundReportType(){ FundReportTypeValue="3", FundRepotyTypeText="线下转账"}
            };
            return fundReportTypeList;
        }

        public static IList<IntegralReportType> QueryAllIntegralReportTypeInfo()
        {
            var integralReportTypeList = new List<IntegralReportType>()
            {
                new IntegralReportType(){ IntegralReportTypeText="佣金", IntegralReportTypeValue="0"},
                new IntegralReportType(){ IntegralReportTypeText="返点", IntegralReportTypeValue="1"}
            };
            return integralReportTypeList;
        }

        public static IList<ExchangeType> QueryAllExchangeTypeInfo()
        {
            var exchangeTypeList = new List<ExchangeType>()
            {
                new ExchangeType(){ ExchangeTypeText="  ", ExchangeTypeValue = "-1"},
                new ExchangeType(){ ExchangeTypeText="投注", ExchangeTypeValue = "0"},
                new ExchangeType(){ ExchangeTypeText="受注", ExchangeTypeValue = "1"}
            };
            return exchangeTypeList;
        }

        public static IList<BetType> QueryAllBetTypeInfo()
        {
            var betTypeList = new List<BetType>()
            {
                new BetType(){ BetTypeText="标准盘", BetTypeValue="0"},
                new BetType(){ BetTypeText="波胆", BetTypeValue="1"},
                new BetType(){ BetTypeText="大小球", BetTypeValue="2"},
                new BetType(){ BetTypeText="让分盘", BetTypeValue="3"},
                new BetType(){ BetTypeText="冠军", BetTypeValue="4"},
                new BetType(){ BetTypeText="娱乐", BetTypeValue="5"}
            };
            return betTypeList;
        }

        /// <summary>
        /// 玩法的种类
        /// </summary>
        /// <returns></returns>
        public static IList<RulesObject> QueryAllRulesTypes()
        {
            var rulesLst = new List<RulesObject>() 
            {
                new RulesObject(){RulesID = "",RulesName="--请选择--"},
                new RulesObject(){RulesID = "1-0",RulesName="标准盘-半场"},
                new RulesObject(){RulesID = "1-1",RulesName="标准盘-全场"},
                new RulesObject(){RulesID = "1-2",RulesName="标准盘-半/全场"},
                new RulesObject(){RulesID = "2-0",RulesName="波胆-半场"},
                new RulesObject(){RulesID = "2-1",RulesName="波胆-全场"},
                new RulesObject(){RulesID = "3-0",RulesName="大小球-半场"},
                new RulesObject(){RulesID = "3-1",RulesName="大小球-全场"},
                new RulesObject(){RulesID = "4-0",RulesName="让球盘-半场"},
                new RulesObject(){RulesID = "4-1",RulesName="让球盘-全场"},
                new RulesObject(){RulesID = "-99",RulesName="娱乐冠军比赛"}
            };

            return rulesLst;
        }
    }
}
