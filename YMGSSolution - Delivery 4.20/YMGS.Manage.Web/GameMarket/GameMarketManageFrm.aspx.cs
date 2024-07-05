using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using YMGS.Manage.Web.Common;
using YMGS.Data.Common;
using YMGS.Data.DataBase;
using YMGS.Business.GameMarket;
using YMGS.Data.Entity;

namespace YMGS.Manage.Web.GameMarket
{
    [TopMenuId(FunctionIdList.GameMarketManagement.GameMarketManageModule)]
    [LeftMenuId(FunctionIdList.GameMarketManagement.MarketTemplateManagePage)]
    public partial class GameMarketManageFrm : BasePage
    {
        private const string _UserOperateTypeKey = "UserOperateType";
        private const string _EditDataKey = "EditDataKey";

        protected override int Func_PageId
        {
            get
            {
                return FunctionIdList.GameMarketManagement.MarketTemplateManagePage;
            }
        }

        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(GameMarketManageFrm), "GameMarket").AbsoluteUri;
        }

        private UserOperateTypeEnum CurUserOperateType
        {
            get
            {
                if (ViewState[_UserOperateTypeKey] == null)
                    return UserOperateTypeEnum.QueryData;
                else
                {
                    var curState = Convert.ToInt32(ViewState[_UserOperateTypeKey]);
                    return (UserOperateTypeEnum)curState;
                }
            }
            set
            {
                ViewState[_UserOperateTypeKey] = (int)value;
            }
        }

        private int? CurEditDataId
        {
            get
            {
                if (ViewState[_EditDataKey] == null)
                    return null;
                else
                    return Convert.ToInt32(ViewState[_EditDataKey]);
            }
            set
            {
                ViewState[_EditDataKey] = value;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            pageNavigator.PageIndexChanged += new EventHandler(pageNavigator_PageIndexChanged);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnNew.Visible = MySession.Accessable(FunctionIdList.GameMarketManagement.AddMarketTemplate);
                LoadBetType(ddlBetType, true);
                LoadBetType(ddlEditBetType, false);
                LoadMarketTmpType();
                LoadData();
            }
        }

        #region 加载页面初始化数据
        private void LoadData()
        {
            CurUserOperateType = UserOperateTypeEnum.QueryData;
            int iBetTypeId = -1;
            if (ddlBetType.SelectedIndex >= 0)
                iBetTypeId = Convert.ToInt32(ddlBetType.SelectedValue);
            var marketTMPType = ddlMarketTmpTypeQuery.SelectedIndex > -1 ? Convert.ToInt32(ddlMarketTmpTypeQuery.SelectedValue) : -1;
            var queryDS = MarketTemplateManager.QueryMarketTemplateByParam(iBetTypeId, txtTmpName.Text, marketTMPType);
            pageNavigator.databinds(queryDS.TB_MARKET_TEMPLATE, gridData);
        }

        /// <summary>
        /// 加载交易类型
        /// </summary>
        /// <param name="ddlTemp"></param>
        private void LoadBetType(DropDownList ddlTemp,bool isAddBlankRow)
        {
            var dsBetType = BetTypeManager.QueryAllBetType();
            if (isAddBlankRow)
            {
                DSBetType.TB_BET_TYPERow blankRow = dsBetType.TB_BET_TYPE.NewTB_BET_TYPERow();
                blankRow.BET_TYPE_ID = -1;
                blankRow.BET_TYPE_NAME = string.Empty;
                dsBetType.TB_BET_TYPE.Rows.InsertAt(blankRow, 0);
            }
            ddlTemp.DataValueField = "BET_TYPE_ID";
            ddlTemp.DataTextField = "BET_TYPE_NAME";
            ddlTemp.DataSource = dsBetType.TB_BET_TYPE;
            ddlTemp.DataBind();
        }

        /// <summary>
        /// 加载市场模板类型
        /// </summary>
        private void LoadMarketTmpType()
        {
            ddlMarketTmpType.Items.Clear();
            ddlMarketTmpType.DataValueField = "MarketTmplateTypeId";
            ddlMarketTmpType.DataTextField = "MarketTmplateTypeName";
            ddlMarketTmpType.DataSource = CommonFunction.QueryAllMarketTemplateType();
            ddlMarketTmpType.DataBind();
            if(ddlMarketTmpType.Items.Count>0)
                ddlMarketTmpType.SelectedIndex = 0;

            ddlMarketTmpTypeQuery.Items.Clear();
            ddlMarketTmpTypeQuery.DataValueField = "MarketTmplateTypeId";
            ddlMarketTmpTypeQuery.DataTextField = "MarketTmplateTypeName";
            ddlMarketTmpTypeQuery.DataSource = CommonFunction.QueryAllMarketTemplateType();
            ddlMarketTmpTypeQuery.DataBind();
            ddlMarketTmpTypeQuery.Items.Insert(0, new ListItem("", "-1"));
            if (ddlMarketTmpTypeQuery.Items.Count > 0)
                ddlMarketTmpTypeQuery.SelectedIndex = 0;
        }

        #endregion

        void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void gridData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            var bindRow = (DSMarketTemplate.TB_MARKET_TEMPLATERow)((DataRowView)e.Row.DataItem).Row;
            if (bindRow == null)
                return;

            MarketTemplateTypeEnum marketTmpType = (MarketTemplateTypeEnum)bindRow.Market_Tmp_Type;

            Label lblBetType = (Label)e.Row.FindControl("lblBetTypeName");
            Label lblMarketTmpType = (Label)e.Row.FindControl("lblMarketTmpType");
            Label lblHomeScore = (Label)e.Row.FindControl("lblHomeScore");
            Label lblAwayScore = (Label)e.Row.FindControl("lblAwayScore");
            Label lblGoals = (Label)e.Row.FindControl("lblGoals");
            Label lblScoreA = (Label)e.Row.FindControl("lblScoreA");
            Label lblScoreB = (Label)e.Row.FindControl("lblScoreB");
            LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");

            var betTypeItem = ddlBetType.Items.FindByValue(bindRow.BET_TYPE_ID.ToString());
            if (betTypeItem != null)
                lblBetType.Text = betTypeItem.Text;
            var marketTmpItem = ddlMarketTmpType.Items.FindByValue(bindRow.Market_Tmp_Type.ToString());
            if (marketTmpItem != null)
                lblMarketTmpType.Text = marketTmpItem.Text;

            if (bindRow.BET_TYPE_ID == (int)BetTypeEnum.CorrectScore)
            {
                lblHomeScore.Text = bindRow.HOMESCORE.ToString();
                lblAwayScore.Text = bindRow.AWAYSCORE.ToString();
            }

            if (bindRow.BET_TYPE_ID == (int)BetTypeEnum.OverUnderGoal)
            {
                lblGoals.Text = bindRow.GOALS.ToString();
            }

            if (bindRow.BET_TYPE_ID == (int)BetTypeEnum.AsianHandicap)
            {
                if(!bindRow.IsSCOREANull())
                    lblScoreA.Text = bindRow.SCOREA.ToString();
                if (!bindRow.IsSCOREBNull())
                    lblScoreB.Text = bindRow.SCOREB.ToString();
            }

            btnEdit.CommandArgument = bindRow.MARKET_TMP_ID.ToString();
            btnEdit.CommandName = "Modify";
            btnEdit.Visible = MySession.Accessable(FunctionIdList.GameMarketManagement.EditMarketTemplate);
            btnDelete.CommandArgument = bindRow.MARKET_TMP_ID.ToString();
            btnDelete.CommandName = "Del";
            btnDelete.OnClientClick = "if(window.confirm('确定要删除吗?')) return true;else return false;";
            btnDelete.Visible = MySession.Accessable(FunctionIdList.GameMarketManagement.DeleteMarketTemplate);
        }

        protected void gridData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modify")
            {
                DisplayDetailInfo(Convert.ToInt32(e.CommandArgument));
                CurUserOperateType = UserOperateTypeEnum.EditData;
                return;
            }

            if (e.CommandName == "Del")
            {
                Delete(Convert.ToInt32(e.CommandArgument));
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            CurUserOperateType = UserOperateTypeEnum.AddData;
            DisplayDetailInfo(null);
        }

        #region 显示详细信息
        private void DisplayDetailInfo(int? marketTmpId)
        {
            //新增
            if (!marketTmpId.HasValue)
            {
                txtMarketTmpName.Text = string.Empty;
                txtMarketTmpNameEn.Text = string.Empty;
                ddlEditBetType.SelectedIndex = 0;
                ddlMarketTmpType.SelectedIndex = 0;
                txtHomeScore.Text = string.Empty;
                txtAwayScore.Text = string.Empty;
                txtScoreA.Text = string.Empty;
                txtScoreB.Text = string.Empty;
                txtGoals.Text = string.Empty;
                divCorrectScore.Style[HtmlTextWriterStyle.Display] = "none";
                divGoals.Style[HtmlTextWriterStyle.Display] = "none";
                divScore.Style[HtmlTextWriterStyle.Display] = "none";
            }
            else
            {
                DSMarketTemplate dsMarketTemplate = MarketTemplateManager.QueryMarketTemplateById(marketTmpId);
                if (dsMarketTemplate.TB_MARKET_TEMPLATE.Rows.Count == 0)
                {
                    PageHelper.ShowMessage(this, "没有查询到该笔记录!");
                    return;
                }
                CurEditDataId = marketTmpId;
                var templateRow = dsMarketTemplate.TB_MARKET_TEMPLATE[0];

                txtMarketTmpName.Text = templateRow.MARKET_TMP_NAME;
                txtMarketTmpNameEn.Text = templateRow.IsMARKET_TMP_NAME_ENNull() ? string.Empty : templateRow.MARKET_TMP_NAME_EN;
                ddlEditBetType.SelectedValue = templateRow.BET_TYPE_ID.ToString();
                ddlMarketTmpType.SelectedValue = templateRow.Market_Tmp_Type.ToString();

                txtHomeScore.Text = string.Empty;
                txtAwayScore.Text = string.Empty;
                txtScoreA.Text = string.Empty;
                txtScoreB.Text = string.Empty;
                txtGoals.Text = string.Empty;
                BetTypeEnum betType = (BetTypeEnum)templateRow.BET_TYPE_ID;
                switch (betType)
                {
                    case BetTypeEnum.MatchOdds:
                        divCorrectScore.Style[HtmlTextWriterStyle.Display] = "none";
                        divGoals.Style[HtmlTextWriterStyle.Display] = "none";
                        divScore.Style[HtmlTextWriterStyle.Display] = "none";
                        break;
                    case BetTypeEnum.CorrectScore:
                        txtHomeScore.Text = templateRow.HOMESCORE.ToString();
                        txtAwayScore.Text = templateRow.AWAYSCORE.ToString();
                        divCorrectScore.Style[HtmlTextWriterStyle.Display] = "";
                        divGoals.Style[HtmlTextWriterStyle.Display] = "none";
                        divScore.Style[HtmlTextWriterStyle.Display] = "none";
                        break;
                    case BetTypeEnum.OverUnderGoal:
                        txtGoals.Text = templateRow.GOALS.ToString();
                        divCorrectScore.Style[HtmlTextWriterStyle.Display] = "none";
                        divGoals.Style[HtmlTextWriterStyle.Display] = "";
                        divScore.Style[HtmlTextWriterStyle.Display] = "none";
                        break;
                    case BetTypeEnum.AsianHandicap:
                        if(!templateRow.IsSCOREANull())
                            txtScoreA.Text = templateRow.SCOREA.ToString();
                        txtScoreB.Text = templateRow.SCOREB.ToString();
                        divCorrectScore.Style[HtmlTextWriterStyle.Display] = "none";
                        divGoals.Style[HtmlTextWriterStyle.Display] = "none";
                        divScore.Style[HtmlTextWriterStyle.Display] = "";
                        break;
                }
            }

            mdlPopup.Show();
        }
        #endregion

        #region 保存

        private DSMarketTemplate.TB_MARKET_TEMPLATERow GetNeededSaveRow()
        {
            DSMarketTemplate dsTemp = new DSMarketTemplate();
            DSMarketTemplate.TB_MARKET_TEMPLATERow savedRow = dsTemp.TB_MARKET_TEMPLATE.NewTB_MARKET_TEMPLATERow();
            if (CurUserOperateType == UserOperateTypeEnum.EditData)
            {
                savedRow.MARKET_TMP_ID = CurEditDataId.Value;
            }

            savedRow.MARKET_TMP_NAME = txtMarketTmpName.Text.Trim();
            savedRow.MARKET_TMP_NAME_EN = txtMarketTmpNameEn.Text.Trim();
            savedRow.BET_TYPE_ID = Convert.ToInt32(ddlEditBetType.SelectedValue);
            savedRow.Market_Tmp_Type = Convert.ToInt32(ddlMarketTmpType.SelectedValue);
            savedRow.CREATE_USER = MySession.CurrentUser.ACCOUNT[0].USER_ID;
            savedRow.LAST_UPDATE_USER = savedRow.CREATE_USER;
            BetTypeEnum betType = (BetTypeEnum)savedRow.BET_TYPE_ID;
            switch (betType)
            {
                case BetTypeEnum.MatchOdds:
                    break;
                case BetTypeEnum.CorrectScore:
                    savedRow.HOMESCORE = Convert.ToInt32(txtHomeScore.Text);
                    savedRow.AWAYSCORE = Convert.ToInt32(txtAwayScore.Text);
                    divCorrectScore.Visible = true;
                    break;
                case BetTypeEnum.OverUnderGoal:
                    savedRow.GOALS = Convert.ToDecimal(txtGoals.Text);
                    divGoals.Visible = true;
                    break;
                case BetTypeEnum.AsianHandicap:
                    if (!string.IsNullOrEmpty(txtScoreA.Text))
                        savedRow.SCOREA = Convert.ToDecimal(txtScoreA.Text);
                    savedRow.SCOREB = Convert.ToDecimal(txtScoreB.Text);
                    divScore.Visible = true;
                    break;
            }


            dsTemp.TB_MARKET_TEMPLATE.AddTB_MARKET_TEMPLATERow(savedRow);
            return savedRow;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CurUserOperateType != UserOperateTypeEnum.AddData &&
                CurUserOperateType != UserOperateTypeEnum.EditData)
                return;

            if (CurEditDataId == null && CurUserOperateType == UserOperateTypeEnum.EditData)
                return;

            try
            {
                DSMarketTemplate.TB_MARKET_TEMPLATERow savedRow = GetNeededSaveRow();
                if (CurUserOperateType == UserOperateTypeEnum.AddData)
                    MarketTemplateManager.AddMarketTemplate(savedRow);
                else
                    MarketTemplateManager.UpdateMarketTemplate(savedRow);
                LoadData();
                mdlPopup.Hide();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
            }
        }
        #endregion

        #region 删除
        private void Delete(int marketTmpId)
        {
            try
            {
                MarketTemplateManager.DeleteMarketTemplate(marketTmpId);
                LoadData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
            }
        }
        #endregion
    }
}