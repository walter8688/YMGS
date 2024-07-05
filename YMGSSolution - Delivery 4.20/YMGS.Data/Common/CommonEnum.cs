using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YMGS.Data.Common
{
    /// <summary>
    /// 系统多语言枚举
    /// </summary>
    public enum LanguageEnum
    {
        UnKnown,
        Chinese,
        English
    }

    /// <summary>
    /// 数据操作枚举
    /// </summary>
    public enum DataHandlerEnum
    {
        Query,
        Add,
        Update,
        Delete
    }

    /// <summary>
    /// 公告状态
    /// </summary>
    public enum BillboardStatus
    {
        /// <summary>
        /// 暂停
        /// </summary>
        Pause = 0,

        /// <summary>
        /// 激活状态
        /// </summary>
        InProgress = 1,
    }

    /// <summary>
    /// 交易类型
    /// </summary>
    public enum BetTypeEnum
    {
        UnKnown=0,

        /// <summary>
        /// 标准盘
        /// </summary>
        MatchOdds=1,

        /// <summary>
        /// 波胆
        /// </summary>
        CorrectScore=2,

        /// <summary>
        /// 大小球
        /// </summary>
        OverUnderGoal=3,

        /// <summary>
        /// 让分盘
        /// </summary>
        AsianHandicap=4
    }

    /// <summary>
    /// 系统参数是否使用中
    /// </summary>
    public enum ParamInUse
    {
        /// <summary>
        /// 不在使用中
        /// </summary>
        NotInUse,
        /// <summary>
        /// 使用中
        /// </summary>
        InUse
    }

    /// <summary>
    /// 排序
    /// </summary>
    public enum OrderAction
    {
        /// <summary>
        /// 上移
        /// </summary>
        OrderUp,
        /// <summary>
        /// 下移
        /// </summary>
        OrderDown
    }

    /// <summary>
    /// 市场模板类型
    /// </summary>
    public enum MarketTemplateTypeEnum
    {
        /// <summary>
        /// 半场
        /// </summary>
        HalfTime=0,
        
        /// <summary>
        /// 全场
        /// </summary>
        FullTime=1,

        /// <summary>
        /// 半场/全场
        /// </summary>
        HalfAndFullTime=2
    }

    /// <summary>
    /// Button类型
    /// </summary>
    public enum ButtonCommandType
    {
        /// <summary>
        /// 查询
        /// </summary>
        Query,
        /// <summary>
        /// 新增
        /// </summary>
        Add,
        /// <summary>
        /// 编辑
        /// </summary>
        Edit,
        /// <summary>
        /// 删除
        /// </summary>
        Delete,
        /// <summary>
        /// 另存为
        /// </summary>
        SaveAs
    }


    /// <summary>
    /// 用户操作类型
    /// </summary>
    public enum UserOperateTypeEnum
    {
        /// <summary>
        /// 查询数据
        /// </summary>
        AddData = 0,

        /// <summary>
        /// 编辑数据
        /// </summary>
        EditData = 1,

        /// <summary>
        /// 修改数据
        /// </summary>
        QueryData = 2,

        /// <summary>
        /// 另存数据
        /// </summary>
        SaveAs =3
    }


    /// <summary>
    /// 参赛成员状态
    /// </summary>
    public enum EventTeamStatusEnum
    {
        /// <summary>
        /// 启用
        /// </summary>
        Activity = 0,
        /// <summary>
        /// 禁用
        /// </summary>
        InActivity = 1
    }

    /// <summary>
    /// 参数类型
    /// </summary>
    public enum ParamTypeEnum
    {
        /// <summary>
        /// 参数
        /// </summary>
        ParamCountry = 1,

        /// <summary>
        /// 参赛成员类型(国家/职业)
        /// </summary>
        ParamEventTeamType = 2,

        /// <summary>
        /// 参赛成员类型(男子/女子)
        /// </summary>
        ParamEventTeamTypeSex = 3
    }

    /// <summary>
    /// 比赛市场标志
    /// </summary>
    public enum MatchMarketFlagEnum
    {
        Unknow = 0,

        /// <summary>
        /// 主队胜(标准盘)
        /// </summary>
        HomeTeamWin = 1,

        /// <summary>
        /// 客队胜(标准盘)
        /// </summary>
        VisitingTeamWin = 2,

        /// <summary>
        /// 平局(标准盘)
        /// </summary>
        TheDraw = 3,

        /// <summary>
        /// 主队胜/主队胜(半/全场标准盘)
        /// </summary>
        HomeTeamWin_HomeTeamWin=4,

        /// <summary>
        /// 主队胜/客队胜(半/全场标准盘)
        /// </summary>
        HomeTeamWin_VisitingTeamWin=5,

        /// <summary>
        /// 主队胜/平局(半/全场标准盘)
        /// </summary>
        HomeTeamWin_TheDraw=6,

        /// <summary>
        /// 客队胜/主队胜(半/全场标准盘)
        /// </summary>
        VisitingTeamWin_HomeTeamWin=7,

        /// <summary>
        /// 客队胜/客队胜(半/全场标准盘)
        /// </summary>
        VisitingTeamWin_VisitingTeamWin=8,
        
        /// <summary>
        /// 客队胜/平局(半/全场标准盘)
        /// </summary>
        VisitingTeamWin_TheDraw=9,

        /// <summary>
        /// 平局/主队胜(半/全场标准盘)
        /// </summary>
        TheDraw_HomeTeamWin=10,

        /// <summary>
        /// 平局/客队胜(半/全场标准盘)
        /// </summary>
        TheDraw_VisitingTeamWin=11,

        /// <summary>
        /// 平局/平局(半/全场标准盘)
        /// </summary>
        TheDraw_TheDraw=12,

        /// <summary>
        /// 精确比分（波胆）
        /// </summary>
        PreciseScore=13,

        /// <summary>
        /// 超过（大小球、让球盘)
        /// </summary>
        Over=14,

        /// <summary>
        /// 不超过(大小球、让球盘)
        /// </summary>
        Under=15
    }

    /// <summary>
    /// 比赛状态
    /// </summary>
    public enum MatchStatusEnum
    {
        /// <summary>
        /// 未激活
        /// </summary>
        NotActivated = 0,

        /// <summary>
        /// 已激活
        /// </summary>
        Activated = 1,

        /// <summary>
        /// 比赛已开始
        /// </summary>
        InMatching=2,

        /// <summary>
        /// 半场已结束
        /// </summary>
        HalfTimeFinished=3,

        /// <summary>
        /// 全场已结束
        /// </summary>
        FullTimeFinished = 4,

        /// <summary>
        /// 已结算
        /// </summary>
        FinishedCalculation = 5,

        /// <summary>
        /// 终止
        /// </summary>
        Abort =6,

        /// <summary>
        /// 下半场开始
        /// </summary>
        SecHalfStarted = 7
    }

    /// <summary>
    /// 比赛辅助状态
    /// </summary>
    public enum MatchAdditionalStatusEnum
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal = 1,


        /// <summary>
        /// 暂停
        /// </summary>
        Suspended = 2,

        /// <summary>
        /// 封盘中
        /// </summary>
        FreezingMatch = 3
    }

    /// <summary>
    /// 赛事状态
    /// </summary>
    public enum EventStatusEnum
    {
        /// <summary>
        /// 激活
        /// </summary>
        Activated = 0,
        /// <summary>
        /// 暂停
        /// </summary>
        Pause = 1,
        /// <summary>
        /// 终止
        /// </summary>
        Abort = 2,
        /// <summary>
        /// 未激活
        /// </summary>
        UnActivated = 3
    }

    /// <summary>
    /// 用户资金账户状态
    /// </summary>
    public enum UserFundStatusEnum
    {
        /// <summary>
        /// 激活
        /// </summary>
        Activated = 0,
        /// <summary>
        /// 冻结
        /// </summary>
        Freezeed = 1
    }

    /// <summary>
    /// 冠军赛事类型
    /// </summary>
    public enum ChampEventTypeEnum
    {
        /// <summary>
        /// 体育
        /// </summary>
        Sports = 1,
        /// <summary>
        /// 娱乐
        /// </summary>
        Entertainment = 2
    }

    /// <summary>
    /// 冠军赛事状态
    /// </summary>
    public enum ChampEventStatusEnum
    {
        /// <summary>
        /// 未激活
        /// </summary>
        UnActivated = 0,

        /// <summary>
        /// 激活
        /// </summary>
        Activated = 1,

        /// <summary>
        /// 暂停
        /// </summary>
        Pause = 2,

        /// <summary>
        /// 终止
        /// </summary>
        Abort = 3,

        /// <summary>
        /// 已计算
        /// </summary>
        Calculated = 4,

        /// <summary>
        /// 已结束
        /// </summary>
        Finished = 5
    }

    /// <summary>
    /// 佣金积分率状态
    /// </summary>
    public enum BrokerageIntegralStatusEnum
    {
        /// <summary>
        /// 不可用
        /// </summary>
        Disabled = 0,
        /// <summary>
        /// 可用
        /// </summary>
        Enabled = 1
    }

    /// <summary>
    /// 代理类型
    /// </summary>
    public enum AgentTypeEnum
    {
        /// <summary>
        /// 总代理
        /// </summary>
        RootAgent = 2,
        /// <summary>
        /// 代理
        /// </summary>
        NoramlAgent = 3
    }

    /// <summary>
    /// 系统用户状态
    /// </summary>
    public enum SysAccountStatusEnum
    {
        /// <summary>
        /// 未激活
        /// </summary>
        UnActivated = 0,
        /// <summary>
        /// 激活
        /// </summary>
        Activated = 1
    }

    public enum SysAccountTypeEnum
    {
        /// <summary>
        /// 系统管理员
        /// </summary>
        Admin = 1,
        /// <summary>
        /// 总代理
        /// </summary>
        GeneralAgent = 2,
        /// <summary>
        /// 代理
        /// </summary>
        Agent = 3,
        /// <summary>
        /// 会员
        /// </summary>
        Member = 4,
    }

    public enum SqlCacheDataTypeEnum
    {
        /// <summary>
        /// 赛事项目
        /// </summary>
        EventItem = 0,

        /// <summary>
        /// 赛事区域
        /// </summary>
        EventZone = 1,

        /// <summary>
        /// 赛事
        /// </summary>
        Event = 2,

        /// <summary>
        /// 比赛和比赛市场
        /// </summary>
        MatchAndMarket =3,

        /// <summary>
        /// 冠军赛事和市场
        /// </summary>
        ChampionAndMarket = 4,

        /// <summary>
        /// 文字广告
        /// </summary>
        ADWords=5,

        /// <summary>
        /// 图片
        /// </summary>
        ADPic=6,

        /// <summary>
        /// 公告
        /// </summary>
        ADNotice = 7,

        /// <summary>
        /// 置顶比赛
        /// </summary>
        ADTopRace = 8,

        /// <summary>
        /// 賠率對比
        /// </summary>
        OddsCompare = 9,

        /// <summary>
        /// 帮助列表
        /// </summary>
        HelpList = 10,

        /// <summary>
        /// 投注信息
        /// </summary>
        ExchangeBack = 11,

        /// <summary>
        /// 受注信息
        /// </summary>
        ExchangeLay = 12,

        /// <summary>
        /// 你的走地盘
        /// </summary>
        YourInPlay = 13
    }

    /// <summary>
    /// 撮合交易状态
    /// </summary>
    public enum ExchangeDealStatusEnum
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal=1,

        /// <summary>
        /// 已结算
        /// </summary>
        Calculated=2,
        
        /// <summary>
        /// 已取消
        /// </summary>
        Cancelled=3
    }

    /// <summary>
    /// 撮合交易类型
    /// </summary>
    public enum ExchangeDealTypeEnum
    {
        /// <summary>
        /// 体育比赛
        /// </summary>
        SportMatch=1,

        /// <summary>
        /// 冠军交易
        /// </summary>
        ChampionMatch=2
    }

    /// <summary>
    /// 用户资金交易类型
    /// </summary>
    public enum UserFundTradeType
    {
        /// <summary>
        /// 充值
        /// </summary>
        ChargeCash = 0,
        
        /// <summary>
        /// 提现
        /// </summary>
        WithDrawCash = 1,

        /// <summary>
        /// 投注
        /// </summary>
        Buy = 2,

        /// <summary>
        /// 受注
        /// </summary>
        Sell = 3,

        /// <summary>
        /// 取消冻结
        /// </summary>
        CancelFreezeCash = 4,

        /// <summary>
        /// 结算
        /// </summary>
        Calculating = 5,

        /// <summary>
        /// 佣金
        /// </summary>
        Commission = 6,

        /// <summary>
        /// 代理返点
        /// </summary>
        AgentReimbursement = 7,

        /// <summary>
        /// 总代理返点
        /// </summary>
        MainAgentReimbursement = 8,

        /// <summary>
        /// 线下转账
        /// </summary>
        OfflineTransfer = 9,

        /// <summary>
        /// 实时结算对冲
        /// </summary>
        RealTimeHedge = 10,

        /// <summary>
        /// 结算扣除释放的对冲资金
        /// </summary>
        ReleaseRealTimeHedge = 11,

    }

    /// <summary>
    /// 用户下注状态
    /// </summary>
    public enum UserBettingStatus
    {
        /// <summary>
        /// 可撮合
        /// </summary>
        MayExhcang = 1,

        /// <summary>
        /// 撮合完
        /// </summary>
        ExchangeFinished= 2,

        /// <summary>
        /// 已结算撮合记录
        /// </summary>
        DealRecordCalculated = 3,

        /// <summary>
        /// 已结算
        /// </summary>
        FullCalculated = 4,

        /// <summary>
        /// 已封盘
        /// </summary>
        Suspended = 5,

        /// <summary>
        /// 已取消
        /// </summary>
        Cancelled = 6
    }

    /// <summary>
    /// 用户充值状态
    /// </summary>
    public enum UserPayStatus
    {
        /// <summary>
        /// 等待付款
        /// </summary>
        WaitingPay =0 ,
        /// <summary>
        /// 付款成功
        /// </summary>
        PaySuccessed = 1,
        /// <summary>
        /// 充值成功
        /// </summary>
        ChargeSuccessed = 2,
        /// <summary>
        /// 充值失败
        /// </summary>
        ChargeFailed = 3
    }

    /// <summary>
    /// 用户提现状态
    /// </summary>
    public enum UserWithDrawStatus
    {
        /// <summary>
        /// 申请中
        /// </summary>
        Supplying = 0,
        /// <summary>
        /// 已确认
        /// </summary>
        Confirmed = 1,
        /// <summary>
        /// 已拒绝
        /// </summary>
        Rejected = 2,
        /// <summary>
        /// 已转账
        /// </summary>
        Transfered = 3,
        /// <summary>
        /// 已取消
        /// </summary>
        Cancled = 4
    }

    /// <summary>
    /// V网卡状态
    /// </summary>
    public enum VCardStatusEnum
    {
        /// <summary>
        /// 未激活
        /// </summary>
        UnActivated = 0,
        /// <summary>
        /// 已激活
        /// </summary>
        Activated = 1,
        /// <summary>
        /// 已失效
        /// </summary>
        UnUsed = 2
    }

    public enum PayTypeEnum
    {
        /// <summary>
        /// 银联支付
        /// </summary>
        ChinaPay = 0,
        /// <summary>
        /// 支付宝支付
        /// </summary>
        AliPay = 1
    }

    public enum UserPayTypeEnum
    {
        /// <summary>
        /// V网卡
        /// </summary>
        VCard = 0,
        /// <summary>
        /// 银联
        /// </summary>
        ChinaPay = 1
    }

    public enum MatchSettleStatus
    {
        /// <summary>
        /// 未结算
        /// </summary>
        UnSettle = 0,
        /// <summary>
        /// 半场结算
        /// </summary>
        HalfSettled = 1,
        /// <summary>
        /// 全场结算
        /// </summary>
        FullSettled = 2
    }

    public enum MatchType
    {
        Football = 0,
        Entertainment = 1
    }

    public enum ApplyAgentStatus
    {
        /// <summary>
        /// 申请中
        /// </summary>
        Appyling = 0,
        /// <summary>
        /// 审批中
        /// </summary>
        ApprovalProcess = 1,
        /// <summary>
        /// 已批准
        /// </summary>
        Confirmed = 2,
        /// <summary>
        /// 已拒绝
        /// </summary>
        Rejected = 3,
        /// <summary>
        /// 已取消
        /// </summary>
        Canceled = 4
    }

    public enum PageIdEnum
    {
        /// <summary>
        /// 体育
        /// </summary>
        Sports = 0,
        /// <summary>
        /// 走地盘
        /// </summary>
        InPlay = 1,
        /// <summary>
        /// 足球
        /// </summary>
        Football = 2,
        /// <summary>
        /// 娱乐
        /// </summary>
        Entertaiment = 3
    }

    public enum LeftNavigatorTypeEnum
    {
        /// <summary>
        /// 体育大类
        /// </summary>
        Sports = 0,
        /// <summary>
        /// 体育项目
        /// </summary>
        EventItem = 1,
        /// <summary>
        /// 赛事区域
        /// </summary>
        EventZone = 2,
        /// <summary>
        /// 赛事
        /// </summary>
        Event = 3,
        /// <summary>
        /// 比赛开始日期
        /// </summary>
        MatchStartDate = 4,
        /// <summary>
        /// 冠军比赛
        /// </summary>
        ChampionMatch = 5,
        /// <summary>
        /// 比赛
        /// </summary>
        Match = 6,
        /// <summary>
        /// 玩法
        /// </summary>
        BetType = 7
    }
}

