using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YMGS.Trade.Web.Football.Model
{
    public class FTMarketsObj
    {
        public FootballMatchInfo MatchInfo { get; set; }
        public IList<FootballMarketInfo> MarketInfo { get; set; }
    }

    public class Top1DataSource
    {
        public FTMarketsObj matchSource { get; set; }
        public MarketBetTypeOrderInfo betOrder { get; set; }
    }

    public class FootballMatchInfo
    {
        /// <summary>
        /// 赛事ID
        /// </summary>
        public int EventId { get; set; }
        /// <summary>
        /// 比赛类别
        /// </summary>
        public int MatchType { get; set; }
        /// <summary>
        /// 比赛ID
        /// </summary>
        public int MatchId { get; set; }
        /// <summary>
        /// 比赛名称
        /// </summary>
        public string MatchName { get; set; }
        /// <summary>
        /// 比赛名称-中文
        /// </summary>
        public string MatchName_CN { get; set; }
        /// <summary>
        /// 比赛名称-英文
        /// </summary>
        public string MatchName_EN { get; set; }
        /// <summary>
        /// 比赛状态
        /// </summary>
        public int MatchStatus { get; set; }
        /// <summary>
        /// 是否走地
        /// </summary>
        public bool isZouDi { get; set; }
        /// <summary>
        /// 比赛辅助状态
        /// </summary>
        public int MADDStatus { get; set; }
        /// <summary>
        /// 比赛开始时间
        /// </summary>
        public string MStartDate { get; set; }
        /// <summary>
        /// 比赛结束时间
        /// </summary>
        public string MatchEndDate { get; set; }
        /// <summary>
        /// 亚洲盘：半场默认盘口
        /// </summary>
        public string HandicapHD { get; set; }
        /// <summary>
        /// 亚洲盘：全场默认盘口
        /// </summary>
        public string HandicapFD { get; set; }
        /// <summary>
        /// 查看整个市场的链接
        /// </summary>
        public string ViewFullADD { get; set; }
    }

    public class FootballMarketInfo
    {
        /// <summary>
        /// 市场ID
        /// </summary>
        public int MarketId { get; set; }
        /// <summary>
        /// 市场名称
        /// </summary>
        public string MKName { get; set; }
        /// <summary>
        /// 市场名称-中文
        /// </summary>
        public string MKName_CN { get; set; }
        /// <summary>
        /// 市场名称-英文
        /// </summary>
        public string MKName_EN { get; set; }
        /// <summary>
        /// 市场模板ID
        /// </summary>
        public int MarketTmpId { get; set; }
        /// <summary>
        /// 市场模板名称
        /// </summary>
        public string MKTmpName { get; set; }
        /// <summary>
        /// 市场模板名称-中文
        /// </summary>
        public string MKTmpName_CN { get; set; }
        /// <summary>
        /// 市场模板名称-英文
        /// </summary>
        public string MKTmpName_EN { get; set; }
        /// <summary>
        /// 全场 1/半场 0/半全场 2
        /// </summary>
        public int MarketTmpType { get; set; }
        /// <summary>
        /// 获胜标志
        /// </summary>
        public int MarketFlag { get; set; }
        /// <summary>
        /// 比分A
        /// </summary>
        public double ScoreA { get; set; }
        /// <summary>
        /// 比分B
        /// </summary>
        public double ScoreB { get; set; }
        /// <summary>
        /// 玩法ID
        /// </summary>
        public int BetTypeId { get; set; }
        /// <summary>
        /// 玩法名称
        /// </summary>
        public string BetTypeName { get; set; }
        /// <summary>
        /// 单个市场的成交量
        /// </summary>
        public decimal DealAmount { get; set; }

        private string _BackAT1 = "";
        /// <summary>
        /// 买的投注数目1
        /// </summary>
        public string BackAT1 { get { return _BackAT1; } set { _BackAT1 = value; } }

        private string _LayAT1 = "";
        /// <summary>
        /// 卖的投注数目1
        /// </summary>
        public string LayAT1 { get { return _LayAT1; } set { _LayAT1 = value; } }

        private string _BackOT1 = "";
        /// <summary>
        /// 买的赔率1
        /// </summary>
        public string BackOT1 { get { return _BackOT1; } set { _BackOT1 = value; } }

        private string _LayOT1 = "";
        /// <summary>
        /// 卖的赔率1
        /// </summary>
        public string LayOT1 { get { return _LayOT1; } set { _LayOT1 = value; } }

        private string _BackAT2 = "";
        /// <summary>
        /// 买的投注数目2
        /// </summary>
        public string BackAT2 { get { return _BackAT2; } set { _BackAT2 = value; } }

        private string _LayAT2 = "";
        /// <summary>
        /// 卖的投注数目2
        /// </summary>
        public string LayAT2 { get { return _LayAT2; } set { _LayAT2 = value; } }

        private string _BackOT2 = "";
        /// <summary>
        /// 买的赔率2
        /// </summary>
        public string BackOT2 { get { return _BackOT2; } set { _BackOT2 = value; } }

        private string _LayOT2 = "";
        /// <summary>
        /// 卖的赔率2
        /// </summary>
        public string LayOT2 { get { return _LayOT2; } set { _LayOT2 = value; } }

        private string _BackAT3 = "";
        /// <summary>
        /// 买的投注数目3
        /// </summary>
        public string BackAT3 { get { return _BackAT3; } set { _BackAT3 = value; } }

        private string _LayAT3 = "";
        /// <summary>
        /// 卖的投注数目3
        /// </summary>
        public string LayAT3 { get { return _LayAT3; } set { _LayAT3 = value; } }

        private string _BackOT3 = "";
        /// <summary>
        /// 买的赔率3
        /// </summary>
        public string BackOT3 { get { return _BackOT3; } set { _BackOT3 = value; } }

        private string _LayOT3 = "";
        /// <summary>
        /// 卖的赔率3
        /// </summary>
        public string LayOT3 { get { return _LayOT3; } set { _LayOT3 = value; } }

        /// <summary>
        /// 比赛状态--关闭
        /// </summary>
        public bool IsMClosed { get; set; }
        /// <summary>
        /// 比赛状态--封盘
        /// </summary>
        public bool IsMSuspend { get; set; }
        /// <summary>
        /// 比赛状态--冻结
        /// </summary>
        public bool IsMFreezed { get; set; }
        /// <summary>
        /// 显示的遮罩层文字
        /// </summary>
        public string DivCharacter { get; set; }
        /// <summary>
        /// 遮罩层的样式
        /// </summary>
        public string MStatusClass { get; set; }
        /// <summary>
        /// 挂单量
        /// </summary>
        public decimal MatchAmountsSum { get; set; }
        /// <summary>
        /// 规则的链接地址
        /// </summary>
        public string RulesLinkAdd { get; set; }
    }

}
