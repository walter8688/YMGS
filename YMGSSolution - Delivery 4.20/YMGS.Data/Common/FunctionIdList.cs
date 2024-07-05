using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YMGS.Data.Common
{
    /// <summary>
    /// 功能点列表
    /// </summary>
    public class FunctionIdList
    {
        ///按照模块组织功能点

        /// <summary>
        /// 系统管理
        /// </summary>
		public static class SystemManagement
		{
            public const int SystemManageModule = 1000; //系统管理（模块）

            public const int UserAccountPage = 1001; //会员帐号管理（页面）
            public const int LockUserAccount = 1002; //锁定或解锁帐号（权限）
            public const int SetUserRole = 1003; //修改角色（权限）
            public const int SetUserFund = 1004; //修改角色（权限）

            public const int RoleManagePage = 1051; //角色和权限管理（页面）
            public const int AddRole = 1052; //新增角色（权限）
            public const int EditRole = 1053; //修改角色（权限）
            public const int DeleteRole = 1054; //删除角色（权限）

            public const int AgentManagePage = 1101; //代理管理（页面）
            public const int ManageAgent = 1102; //代理管理（权限）

            public const int VCardManagePage = 1151; //V网卡管理（页面）
            public const int CreateVCard = 1152; //生成V网卡（权限）

            public const int FundAccountManagePage = 1201; //资金账户管理（页面）
            public const int BankCardSetting = 1202; //设置银行卡（权限）

            public const int WithdrawalManagePage = 1251; //会员提现管理（页面）
            public const int ApproveWithdrawal = 1252; //审批提现（权限）
            public const int CompleteWithdrawal = 1253; //转款完成（权限）
            public const int RejectWithdrawl = 1254;//拒绝提现（权限）

            public const int AgentApplyManagePage = 1301;//代理申请管理（页面）
            public const int AgentApplyApproval = 1302;//代理申请审批（权限）
            public const int AgentApplyConfirm = 1303;//代理批准审批（权限）
            public const int AgentApplyReject = 1304;//代理拒绝审批（权限）
        }

        /// <summary>
        /// 赛事管理
        /// </summary>
        public static class EventManagement
        {
            public const int EventManageModule = 2000; //赛事管理（模块）

            public const int EventZoneManagePage = 2001; //赛事区域管理（页面）
            public const int AddEventZone = 2002; //新增赛事区域（权限）
            public const int EditEventZone = 2003; //修改赛事区域（权限）
            public const int DeleteEventZone = 2004; //删除赛事区域（权限）

            public const int EventManagePage = 2051; //赛事管理（页面）
            public const int AddEvent = 2052; //新增赛事（权限）
            public const int EditEvent = 2053; //修改赛事（权限）
            public const int DeleteEvent = 2054; //删除赛事（权限）
            public const int SaveAsEvent = 2055; //赛事另存（权限）
            public const int SuspendEvent = 2056; //启动或暂停赛事（权限）
            public const int StopEvent = 2057; //终止赛事（权限）

            public const int TeamManagePage = 2101; //参赛成员管理（页面）
            public const int AddTeam = 2102; //新增参赛成员（权限）
            public const int EditTeam = 2103; //修改参赛成员（权限）
            public const int DeleteTeam = 2104; //删除参赛成员（权限）
            public const int DisableTeam = 2105; //启动或禁用参赛成员（权限）
        }

        /// <summary>
        /// 比赛市场管理
        /// </summary>
        public static class GameMarketManagement
        {
            public const int GameMarketManageModule = 3000; //比赛市场管理（模块）

            //public const int BetTypeManagePage = 3001; //交易类型管理（页面）
            //public const int BetTypeManage = 3002; //交易类型设置（权限）

            public const int MarketTemplateManagePage = 3051; //市场模板管理（页面）
            public const int AddMarketTemplate = 3052; //新增市场模板（权限）
            public const int EditMarketTemplate = 3053; //修改市场模板（权限）
            public const int DeleteMarketTemplate = 3054; //删除市场模板（权限）

            public const int GameManagePage = 3101; //比赛管理（页面）
            public const int AddGame = 3102; //新增比赛（权限）
            public const int EditGame = 3103; //修改比赛（权限）
            public const int DeleteGame = 3104; //删除比赛（权限）
            public const int SuspendGame = 3105; //启动或暂停比赛（权限）
            public const int StopGame = 3106; //终止比赛（权限）
            public const int SaveAsGame = 3107; //比赛另存（权限）
            public const int RecommendGame = 3108; //推荐比赛（权限）

            public const int ChampionGameManagePage = 3151; //冠军交易管理（页面）
            public const int AddChampionGame = 3152; //新增冠军赛事（权限）
            public const int EditChampionGame = 3153; //修改冠军赛事（权限）
            public const int DeleteChampionGame = 3154; //删除冠军赛事（权限）
            public const int SaveAsChampionGame = 3155; //冠军赛事另存（权限）
            public const int SuspendChampionGame = 3156; //启动或暂停冠军赛事（权限）
            public const int StopChampionGame = 3157; //终止冠军赛事（权限）
            public const int FinishChampionGame = 3158; //结束冠军赛事（权限）
            public const int RecordChampionGameResult = 3159; //录入冠军赛事结果（权限）
        }

        /// <summary>
        /// 赛中控制
        /// </summary>
        public static class GameControl
        {
            public const int GameControlModule = 4000; //赛中控制（模块）

            public const int GameControlPage = 4001; //比赛控制（页面）
            public const int EntertainGame = 4002; //比赛封盘（权限）
            public const int ScoreGame = 4003; //录入比分（权限）
            public const int EndGame = 4004; //结束比赛（权限）
            public const int ActivatedGame = 4005; //激活比赛（权限）
            public const int HalfEndGame = 4006; //半场结束（权限）
            public const int ClearGameMarket = 4007; //清理市场（权限）
            public const int EditGameStartTime = 4008; //修改比赛开始时间（权限）
            public const int StartGame = 4009;//开始比赛(权限)
            public const int SecHalfStart = 4010;//开始下半场比赛
        }

        /// <summary>
        /// 赛后结算
        /// </summary>
        public static class GameSettle
        {
            public const int GameSettleManageModule = 5000; //赛后结算（模块）

            public const int GameSettlePage = 5001; //比赛结算（页面）
            public const int SettleGame = 5002; //结算比赛（权限）
            public const int ReSettleGame = 5003;//重新结算比赛

            public const int ChampionEventSettlePage = 5010;//冠军赛事结算（页面）
            public const int SetteleChampionEvent = 5011;//结算冠军赛事(权限)
            public const int ReSettleChampionEvent = 5012;//重新结算冠军赛事

            public const int CommissionManagePage = 5051; //佣金管理（页面）
            public const int CommissionRateManage = 5052; //设置佣金率（权限）
        }

        /// <summary>
        /// 辅助管理
        /// </summary>
        public static class AssistantManagement
        {
            public const int AssistantManageModule = 6000; //辅助管理（模块）

            public const int AssistantManagePage = 6001; //区域管理（页面）
            public const int AddZone = 6002; //新增区域（权限）
            public const int EditZone = 6003; //修改区域（权限）
            public const int DeleteZone = 6004; //删除区域（权限）

            public const int OddsManagePage = 6051; //赔率对比管理（页面）
            public const int AddOdds = 6052; //新增赔率对比（权限）
            public const int EditOdds = 6053; //修改赔率对比（权限）
            public const int DeleteOdds = 6054; //删除赔率对比（权限）

            public const int AdsManagePage = 6101; //广告位信息管理（页面）
            public const int AdsManage = 6102; //设置广告位（权限）

            public const int NoticeManagePage = 6151; //网站公告管理（页面）
            public const int AddNotice = 6152; //新增公告（权限）
            public const int EditNotice = 6153; //修改公告（权限）
            public const int DeleteNotice = 6154; //删除公告（权限）
            public const int SuspendNotice = 6155; //启动或暂停公告（权限）

            public const int ParameterManagePage = 6201; //参数管理（页面）
            public const int ParameterManage = 6202; //参数设置（权限）

            public const int HelperManagePage = 6251; //文章目录管理（页面）
            public const int AddHelper = 6252; //新增目录（权限）
            public const int EditHelper = 6253; //修改目录（权限）
            public const int DeleteHelper = 6254; //删除目录（权限）
        }

        /// <summary>
        /// 报表中心
        /// </summary>
        public static class ReportCenter
        {
            public const int ReportCenterModule = 7000; //报表中心（模块）

            public const int BetReport = 7001; //下注报表（页面）

            public const int DealReport = 7051; //交易报表（页面）

            public const int FundReport = 7101; //资金报表（页面）

            public const int IntegralReport = 7151; //佣金报表（页面）
        }

        /// <summary>
        /// 交易平台
        /// </summary>
        public static class BetSite
        {
            public const int BetSiteModule = 8000; //交易平台（模块）

            public const int RegisterPage = 8001; //注册登录（页面）
            public const int Register = 8002; //注册会员（权限）
            public const int ResetPassword = 8003; //忘记密码（权限）

            public const int SearchMarket = 8051; //市场检索（页面）

            public const int BetSitePage = 8101; //下注交易（页面）
            public const int Bet = 8102; //下注（权限）
        }

        /// <summary>
        /// 会员中心
        /// </summary>
        public static class MemberCenter
        {
            public const int MemberCenterModule = 9000; //会员中心（模块）

            public const int UserInfoManagePage = 9001; //会员信息管理（页面）
            public const int UserInfoManage = 9002; //修改用户信息（权限）

            public const int FundAccountManagePage = 9051; //资金账户管理（页面）
            public const int BankCardManage = 9052; //设置银行卡信息（权限）
            public const int OnlinePay = 9053; //在线充值（权限）
            public const int ApplyWithdrawal = 9054; //提现申请（权限）

            public const int AgentManagePage = 9101; //代理会员管理（页面）
            public const int GrowAgent = 9102; //发展会员（权限）
            public const int SetAgent = 9103; //指定或撤销代理（权限）
            public const int AgentManage = 9104; //代理设置（权限）

            public const int MyBetPage = 9151; //我的交易管理（页面）
            public const int ModifyBet = 9152; //修改下注（权限）
            public const int CancelBet = 9153; //撤销下注（权限）

            public const int MyIntegralPage = 9201; //我的积分查询（页面）

            public const int MyProxyPage = 9301;//代理管理（页面）
        }
    }
}
