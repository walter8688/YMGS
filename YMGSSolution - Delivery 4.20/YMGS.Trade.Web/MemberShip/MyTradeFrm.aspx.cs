using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Common;
using YMGS.Data.Common;
using YMGS.Data.DataBase;
using YMGS.Business.MemberShip;
using System.Data;
using YMGS.Business.GameMarket;
using YMGS.Data.Entity;
using System.Drawing;

namespace YMGS.Trade.Web.MemberShip
{
     [TopMenuId(FunctionIdList.MemberCenter.MemberCenterModule)]
    public partial class MyTradeFrm : MemberShipBasePage
    {
        public override string PageTitle
        {
            get
            {
                return LangManager.GetString("HisTradeReport");
            }
        }

        public override bool IsAccessible(YMGS.Trade.Web.Common.UserAccess userAccess)
        {
            return base.IsAllow(FunctionIdList.MemberCenter.MyBetPage);
        }

        public static string Url()
        {

            return UrlHelper.BuildUrl(typeof(MyTradeFrm), "MemberShip").AbsoluteUri;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMyMatch();
            }
        }

        private bool Lan
        {
            get {
                return Language == LanguageEnum.Chinese ? true : false;
            }
        }

        private void LoadMyMatch()
        {
            var myMatchList = UserFundManager.QueryMyMatch(CurrentUser.UserId);
            ddlMyMatch.Items.Clear();
            ddlMyMatch.DataSource = myMatchList;
            ddlMyMatch.DataTextField = Lan ? "MATCH_NAME" : "MATCH_NAME_EN";
            ddlMyMatch.DataValueField = Lan ? "MATCH_NAME" : "MATCH_NAME_EN";
            ddlMyMatch.DataBind();
            ddlMyMatch.Items.Insert(0, new ListItem(LangManager.GetString("All"), ""));
        }

        public void LoadData()
        {
            DSMyTrade ds = new DSMyTrade();
           DSMyTrade.TB_MY_TRADERow obj= ds.TB_MY_TRADE.NewTB_MY_TRADERow();
           obj.BETTYPE = int.Parse(ddlbettype.SelectedValue);
           obj.STATUS = int.Parse(ddlStatus.SelectedValue);
           obj.TRADE_USER = CurrentUser.UserId;
           DateTime start = startDate.Value == null ? new DateTime(1753,1,1) : startDate.Value.Value;
           DateTime end = endDate.Value == null ? new DateTime(9999, 12, 31) : endDate.Value.Value.AddDays(1);
           var matchName = ddlMyMatch.SelectedItem.Value;
           var data = from s in UserFundManager.QueryTrade(start, end, obj).TB_MY_TRADE
                      select new
                      {
                          s.BET_AMOUNTS,
                          s.BETID,
                          s.BETTYPE,
                          s.MARKET_ID,
                          MARKET_NAME = Lan ? s.MARKET_NAME : s.MARKET_NAME_EN,
                          s.MARKET_NAME_EN,
                          s.MATCH_AMOUNTS,
                          s.MATCH_ID,
                          MATCH_NAME = Lan ? s.MATCH_NAME : s.MATCH_NAME_EN,
                          s.MATCH_NAME_EN,
                          s.MATCH_TYPE,
                          s.ODDS,
                          s.STATUS,
                          s.TRADE_USER,
                          s.TRADE_TIME,
                          EXCHANGE_WIN_FLAG = s.IsEXCHANGE_WIN_FLAGNull() ? -1 : s.EXCHANGE_WIN_FLAG,
                          HOME_TEAM_SCORE = s.IsHOME_TEAM_SCORENull() ? -1 : s.HOME_TEAM_SCORE,
                          GUEST_TEAM_SCORE = s.IsGUEST_TEAM_SCORENull() ? 1 : s.GUEST_TEAM_SCORE,
                          Market_Tmp_Type = s.IsMarket_Tmp_TypeNull() ? -1 : s.Market_Tmp_Type,
                          BET_TYPE_NAME = Lan ? s.BET_TYPE_NAME : s.BET_TYPE_NAME_EN,
                          TRADE_FUND = s.IsTRADE_FUNDNull() ? 0 : s.TRADE_FUND
                      };
            if(!string.IsNullOrEmpty(matchName))
                data = from s in data where s.MATCH_NAME == matchName || s.MATCH_NAME_EN == matchName select s;
            DSMyTrade dsMt = new DSMyTrade();
            var tempBetID = 0;
            var tempBetType = 0;
            foreach (var s in data)
            {
                DSMyTrade.TB_MY_TRADERow row = dsMt.TB_MY_TRADE.NewRow() as DSMyTrade.TB_MY_TRADERow;
                if (tempBetID == s.BETID && tempBetType == s.BETTYPE)
                    continue;
                tempBetID = s.BETID;
                tempBetType = s.BETTYPE;
                row.BET_AMOUNTS = s.BET_AMOUNTS;
                row.BETID = s.BETID;
                row.BETTYPE = s.BETTYPE;
                row.MARKET_ID = s.MARKET_ID;
                row.MARKET_NAME = s.MARKET_NAME;
                row.MARKET_NAME_EN = s.MARKET_NAME_EN;
                row.MATCH_AMOUNTS = s.MATCH_AMOUNTS;
                row.MATCH_ID = s.MATCH_ID;
                row.MATCH_NAME = s.MATCH_NAME;
                row.MATCH_NAME_EN = s.MATCH_NAME_EN;
                row.MATCH_TYPE = s.MATCH_TYPE;
                row.ODDS = s.ODDS;
                row.STATUS = s.STATUS;
                row.TRADE_USER = s.TRADE_USER;
                row.TRADE_TIME = s.TRADE_TIME;
                row.EXCHANGE_WIN_FLAG = s.EXCHANGE_WIN_FLAG;
                row.HOME_TEAM_SCORE = s.HOME_TEAM_SCORE;
                row.GUEST_TEAM_SCORE = s.GUEST_TEAM_SCORE;
                row.Market_Tmp_Type = s.Market_Tmp_Type;
                row.BET_TYPE_NAME = s.BET_TYPE_NAME;
                row.TRADE_FUND = s.TRADE_FUND;
                dsMt.TB_MY_TRADE.AddTB_MY_TRADERow(row);
            }

           PageNavigator1.databinds(dsMt.TB_MY_TRADE, gdvMain);

          //gdvMain.DataSource= UserFundManager.QueryTrade(startDate.Value.Value, endDate.Value.Value, obj);
          // //gdvMain.DataSource = UserFundManager.QueryTrade(DateTime.Now.AddDays(-100), DateTime.Now, obj);
          //gdvMain.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void PageNavigator1_PageIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void gridData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DSMyTrade.TB_MY_TRADERow obj=((DataRowView)e.Row.DataItem).Row as DSMyTrade.TB_MY_TRADERow;
                var lblCurrentScore = e.Row.FindControl("lblCurrentScore") as Label;
                if (obj.HOME_TEAM_SCORE != -1 && obj.GUEST_TEAM_SCORE != -1 && obj.MATCH_TYPE == 1)
                    lblCurrentScore.Text = string.Format("{0}:{1}", obj.HOME_TEAM_SCORE, obj.GUEST_TEAM_SCORE);

                //if (e.Row.Cells[11].Text.Trim() == "-1")
                //    e.Row.Cells[11].Text = string.Empty;

                e.Row.Cells[0].BackColor = obj.BETTYPE == 1 ? Color.FromArgb(114, 187, 239) : Color.Pink;
                var cellResult = e.Row.Cells[11];
                decimal betResult = 0;
                decimal.TryParse(cellResult.Text.Trim(), out betResult);
                if (betResult > 0)
                {
                    cellResult.ForeColor = Color.Green;
                }
                else if (betResult < 0)
                {
                    cellResult.ForeColor = Color.Red;
                }
                else if (obj.STATUS == 3)
                {
                    cellResult.Text = "0";
                }
                else
                {
                    cellResult.Text = "";
                }

                //if ((obj.STATUS == 3 || obj.STATUS == 4) && e.Row.Cells[11].Text.Trim() != "")
                //{
                //    var cellResult = e.Row.Cells[11];
                //    var settleResult = cellResult.Text.Trim();
                //    var betType = e.Row.Cells[0].Text;

                //    if (betType == "1" && settleResult == "0")
                //    {
                //        //cellResult.Text = LangManager.GetString("win");
                //        cellResult.Text = obj.TRADE_FUND.ToString();
                //        cellResult.ForeColor = Color.Green;
                //    }
                //    else if (betType == "1" && settleResult == "1")
                //    {
                //        //cellResult.Text = LangManager.GetString("lose");
                //        cellResult.Text = obj.TRADE_FUND.ToString();
                //        cellResult.ForeColor = Color.Red;
                //    }

                //    if (betType == "2" && settleResult == "0")
                //    {
                //        //cellResult.Text = LangManager.GetString("lose");
                //        cellResult.Text = obj.TRADE_FUND.ToString();
                //        cellResult.ForeColor = Color.Red;
                //    }
                //    else if (betType == "2" && settleResult == "1")
                //    {
                //        //cellResult.Text = LangManager.GetString("win");
                //        cellResult.Text = obj.TRADE_FUND.ToString();
                //        cellResult.ForeColor = Color.Green;
                //    }
                //    if (settleResult == "2")
                //    {
                //        cellResult.Text = LangManager.GetString("Draw");
                //    }
                //}
                e.Row.Cells[0].Text = obj.BETTYPE == 1 ? LangManager.GetString("buybet") : obj.BETTYPE == 2 ? LangManager.GetString("Salebet") : LangManager.GetString("unknown");
                e.Row.Cells[10].Text = obj.STATUS == 1 ? LangManager.GetString("matching") : obj.STATUS == 2 ? LangManager.GetString("matched") : obj.STATUS == 3 ? LangManager.GetString("settlement") : obj.STATUS == 4 ? LangManager.GetString("settlement") : obj.STATUS == 5 ? LangManager.GetString("Rotaryheader") : LangManager.GetString("Canceled");
                e.Row.Cells[4].Text = obj.Market_Tmp_Type == 0 ? LangManager.GetString("Half") : obj.Market_Tmp_Type == 1 ? LangManager.GetString("Full") : obj.Market_Tmp_Type == 2 ? LangManager.GetString("HalfFull") : "";
                e.Row.Cells[9].Text = CurrentUser.UserName;

                LinkButton btnCancel = e.Row.FindControl("btnCancel") as LinkButton;
                btnCancel.Attributes.Add("onclick", "javascript:return confirm('" + LangManager.GetString("confirmmatchamount") + "');");

                if (obj.STATUS != 1)
                {
                    btnCancel.Visible = false;
                }
                else
                {
                    bool flag = base.IsAllow(FunctionIdList.MemberCenter.CancelBet);
                    btnCancel.Visible = flag;
                }
            }
        }
        protected void btnDetail_Click(object sender, EventArgs e)
        {
            string datakeys = ((LinkButton)sender).CommandArgument;
            string[] arrays = datakeys.Split(new string[] { "," }, StringSplitOptions.None);
            string BETTYPE = arrays[0];
            string BETID = arrays[1];
            string MATCH_ID = arrays[2];
            string MATCH_TYPE = arrays[3];
           lblmatch.Text = arrays[4];
            lblmarket.Text = arrays[5];
            lblbettatal.Text = arrays[6];
           var data= BetTypeManager.QueryDeal(int.Parse(BETTYPE), int.Parse(BETID), int.Parse(MATCH_ID), int.Parse(MATCH_TYPE));
           gdvsubmain.DataSource = data;
           gdvsubmain.DataBind();
           mdlPopup.Show();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            int userid = ((DetailUserInfo)Session[CommonConstant.CurrentLoginUserSessionKey]).UserId;
            string datakeys = ((LinkButton)sender).CommandArgument;
            string[] arrays = datakeys.Split(new string[] { "," }, StringSplitOptions.None);
            string BETTYPE = arrays[0];
            string BETID = arrays[1];
            string MATCH_ID = arrays[2];
            string MATCH_TYPE = arrays[3];
            if (BETTYPE == "1")
            {
                DSExchange_Back dsback = new DSExchange_Back();
                DSExchange_Back.TB_EXCHANGE_BACKRow backrow = dsback.TB_EXCHANGE_BACK.NewTB_EXCHANGE_BACKRow();
                backrow.EXCHANGE_BACK_ID = int.Parse(BETID);
                backrow.MARKET_ID = 0;
                backrow.MATCH_ID = 0;
                backrow.ODDS = 0;
                backrow.BET_AMOUNTS = backrow.MATCH_AMOUNTS = 0;
                backrow.TRADE_USER = userid;
                backrow.STATUS = 6;
                backrow.MATCH_TYPE = MATCH_TYPE;
                MatchManager.ExchangeBack("cancel", backrow);
            }
            if (BETTYPE == "2")
            {
                DSExchangeLay dslay = new DSExchangeLay();
                DSExchangeLay.TB_EXCHANGE_LAYRow layrow = dslay.TB_EXCHANGE_LAY.NewTB_EXCHANGE_LAYRow();
                layrow.EXCHANGE_LAY_ID = int.Parse(BETID);
                layrow.MARKET_ID = 0;
                layrow.MATCH_ID = 0;
                layrow.ODDS = 0;
                layrow.BET_AMOUNTS = layrow.MATCH_AMOUNTS = 0;
                layrow.TRADE_USER = userid;
                layrow.STATUS = 1;
                layrow.MATCH_TYPE = MATCH_TYPE;
                MatchManager.ExchangeLay("cancel", layrow);
            }
            LoadData();
        }
    }
}