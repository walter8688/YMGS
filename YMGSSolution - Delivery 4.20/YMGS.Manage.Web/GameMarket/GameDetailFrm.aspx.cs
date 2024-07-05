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
using YMGS.Business.EventManage;
using YMGS.Data.Presentation;
using YMGS.Framework;

namespace YMGS.Manage.Web.GameMarket
{
    [TopMenuId(FunctionIdList.GameMarketManagement.GameMarketManageModule)]
    [LeftMenuId(FunctionIdList.GameMarketManagement.GameManagePage)]
    public partial class GameDetailFrm : BasePage
    {
        private const string _UserOperateTypeKey = "UserOpType";
        private const string _EditDataKey = "EditDataKey";

        protected override int Func_PageId
        {
            get
            {
                return FunctionIdList.GameMarketManagement.GameManagePage;
            }
        }

        public static string Url(int matchId, UserOperateTypeEnum userOperateType)
        {
            return UrlHelper.BuildUrl(typeof(GameDetailFrm), "GameMarket", _EditDataKey, matchId, _UserOperateTypeKey, (int)userOperateType).AbsoluteUri;
        }

        private int CurrentUserId
        {
            get
            {
                return MySession.CurrentUser.ACCOUNT[0].USER_ID;
            }
        }

        private UserOperateTypeEnum CurUserOperateType
        {
            get
            {
                string strTemp = Request.QueryString[_UserOperateTypeKey];
                if (string.IsNullOrEmpty(strTemp))
                    return UserOperateTypeEnum.AddData;

                return (UserOperateTypeEnum)Convert.ToInt32(strTemp);
            }
        }

        private int? CurEditDataId
        {
            get
            {
                string strTemp = Request.QueryString[_EditDataKey];
                if (string.IsNullOrEmpty(strTemp))
                    return null;

                return Convert.ToInt32(strTemp);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurUserOperateType == UserOperateTypeEnum.AddData)
                    btnSave.Visible = MySession.Accessable(FunctionIdList.GameMarketManagement.AddGame);
                else
                    btnSave.Visible = MySession.Accessable(FunctionIdList.GameMarketManagement.EditGame);
                LoadEventItem();
                LoadEventZone(null);
                LoadEvent(null);
                LoadMarketTemplate();
                DisplayDetailInfo(CurEditDataId);
                LoadPageStyle();
            }
        }

        private void LoadPageStyle()
        {
            if (CurUserOperateType != UserOperateTypeEnum.AddData &&
               CurUserOperateType != UserOperateTypeEnum.EditData &&
               CurUserOperateType != UserOperateTypeEnum.SaveAs)
                return;
            if (CurUserOperateType == UserOperateTypeEnum.AddData || CurUserOperateType == UserOperateTypeEnum.SaveAs)
            {
                trSocreHalf.Visible = false;
                trSocreSecHalf.Visible = false;
                trSocreOverTime.Visible = false;
                trSocrePoint.Visible = false;
                trMatchStatus.Visible = false;
            }
            else
            {
                trSocreHalf.Visible = true;
                trSocreSecHalf.Visible = true;
                trSocreOverTime.Visible = true;
                trSocrePoint.Visible = true;
                trMatchStatus.Visible = true;
            }
        }

        #region 载入赛事项目、区域、赛事和模板、参赛队伍

        /// <summary>
        /// 加载赛事项目
        /// </summary>
        private void LoadEventItem()
        {
            var eventItemList = EventZoneManager.QueryEventItem();
            var blankRow = eventItemList.TB_EVENT_ITEM.NewTB_EVENT_ITEMRow();
            blankRow.EventItem_ID = -1;
            blankRow.EventItem_Name = string.Empty;
            eventItemList.TB_EVENT_ITEM.Rows.InsertAt(blankRow, 0);
            ddlEventItem.DataValueField = "EVENTITEM_ID";
            ddlEventItem.DataTextField = "EVENTITEM_NAME";
            ddlEventItem.DataSource = eventItemList.TB_EVENT_ITEM;
            ddlEventItem.DataBind();
            ddlEventItem.SelectedIndex = 0;
        }

        /// <summary>
        /// 加载赛事区域
        /// </summary>
        /// <param name="eventItemId"></param>
        private void LoadEventZone(int? eventItemId)
        {
            ddlEventZone.Items.Clear();
            ddlEventZone.SelectedIndex = -1;
            if (eventItemId.HasValue)
            {
                DSEventZone dsEventZone = new DSEventZone();
                DSEventZone.TB_EVENT_ZONERow queryRow = dsEventZone.TB_EVENT_ZONE.NewTB_EVENT_ZONERow();
                queryRow.EVENTITEM_ID = eventItemId.Value;
                queryRow.EVENTZONE_NAME = string.Empty;
                queryRow.EVENTZONE_NAME_EN = string.Empty;
                queryRow.EVENTZONE_DESC = string.Empty;
                queryRow.PARAM_ZONE_ID = -1;
                var eventZoneList = EventZoneManager.QueryEventZone(queryRow);
                var blankRow = eventZoneList.TB_EVENT_ZONE.NewTB_EVENT_ZONERow();
                blankRow.EVENTZONE_ID = -1;
                blankRow.EVENTZONE_NAME = string.Empty;
                blankRow.EVENTZONE_NAME_EN = string.Empty;
                eventZoneList.TB_EVENT_ZONE.Rows.InsertAt(blankRow, 0);
                ddlEventZone.DataValueField = "EVENTZONE_ID";
                ddlEventZone.DataTextField = "EVENTZONE_NAME";
                ddlEventZone.DataSource = eventZoneList.TB_EVENT_ZONE;
                ddlEventZone.DataBind();
            }
            else
            {
                ddlEventZone.Items.Add(new ListItem(string.Empty, "-1"));
            }

            ddlEventZone.SelectedIndex = 0;
        }

        private void LoadEvent(int? eventZoneId)
        {
            if (!eventZoneId.HasValue)
                eventZoneId = -1;
            var eventData = EventManager.QueryEvent(eventZoneId, string.Empty, string.Empty, string.Empty, null, null, 0, -1, string.Empty);
            var blankRow = eventData._DSEventTeamList.NewDSEventTeamListRow();
            blankRow.EVENT_ID = -1;
            blankRow.EVENT_NAME = string.Empty;
            blankRow.EVENT_NAME_EN = string.Empty;
            blankRow.EventItem_ID = -1;
            eventData._DSEventTeamList.Rows.InsertAt(blankRow, 0);
            ddlEvent.DataTextField = "EVENT_NAME";
            ddlEvent.DataValueField = "EVENT_ID";
            ddlEvent.DataSource = eventData;
            ddlEvent.DataBind();
            ddlEvent.SelectedIndex = 0;
        }

        private void LoadMarketTemplate()
        {
            var templateDS = MarketTemplateManager.QueryMarketTemplateByParam(-1, string.Empty, -1);
            LoadSingleMarketTemplate(ckbMatchOdds, templateDS, BetTypeEnum.MatchOdds, MarketTemplateTypeEnum.HalfTime);
            LoadSingleMarketTemplate(ckbCorrectScore, templateDS, BetTypeEnum.CorrectScore, MarketTemplateTypeEnum.HalfTime);
            LoadSingleMarketTemplate(ckbOverUnder, templateDS, BetTypeEnum.OverUnderGoal, MarketTemplateTypeEnum.HalfTime);
            LoadSingleMarketTemplate(ckbAsianHandicap, templateDS, BetTypeEnum.AsianHandicap, MarketTemplateTypeEnum.HalfTime);
            LoadSingleMarketTemplate(ckbCorrectScoreSecHalf, templateDS, BetTypeEnum.CorrectScore, MarketTemplateTypeEnum.FullTime);
            LoadSingleMarketTemplate(ckbOverUnderSecHalf, templateDS, BetTypeEnum.OverUnderGoal, MarketTemplateTypeEnum.FullTime);
            LoadSingleMarketTemplate(ckbAsianHandicapSecHalf, templateDS, BetTypeEnum.AsianHandicap, MarketTemplateTypeEnum.FullTime);
            //设置默认盘口
            LoadAsianHandicapMarketDefault(drpHalfDefault, templateDS, BetTypeEnum.AsianHandicap, MarketTemplateTypeEnum.HalfTime);
            LoadAsianHandicapMarketDefault(drpFullDefault, templateDS, BetTypeEnum.AsianHandicap, MarketTemplateTypeEnum.FullTime);
        }

        private void LoadSingleMarketTemplate(CheckBoxList ckbTemp, DSMarketTemplate dsTemplate, BetTypeEnum betType, MarketTemplateTypeEnum marketTmpType)
        {
            ckbTemp.DataValueField = "MARKET_TMP_ID";
            ckbTemp.DataTextField = "MARKET_TMP_NAME";
            var curData = dsTemplate.TB_MARKET_TEMPLATE.Where(r => r.BET_TYPE_ID == (int)betType);
            if (betType != BetTypeEnum.MatchOdds)
                curData = from d in curData where d.Market_Tmp_Type == (int)marketTmpType select d;
            ckbTemp.DataSource = curData;
            ckbTemp.DataBind();
            if (betType == BetTypeEnum.MatchOdds)
            {
                foreach (ListItem item in ckbTemp.Items)
                {
                    var fullCurData = curData.Where(r => r.Market_Tmp_Type == (int)MarketTemplateTypeEnum.FullTime);
                    DataView viewData = fullCurData.AsDataView();
                    if (item.Value == viewData[0]["MARKET_TMP_ID"].ToString())
                    {
                        item.Selected = true;
                        item.Enabled = false;
                    }
                }
            }
        }
        /// <summary>
        /// 设置让球盘的默认盘口
        /// </summary>
        /// <param name="drpDefault"></param>
        /// <param name="dsTemplate"></param>
        /// <param name="betType"></param>
        /// <param name="marketTmpType"></param>
        private void LoadAsianHandicapMarketDefault(DropDownList drpDefault, DSMarketTemplate dsTemplate, BetTypeEnum betType, MarketTemplateTypeEnum marketTmpType)
        {
            drpDefault.DataValueField = "MARKET_TMP_ID";
            drpDefault.DataTextField = "MARKET_TMP_NAME";
            var curData = dsTemplate.TB_MARKET_TEMPLATE.Where(r => r.BET_TYPE_ID == (int)betType);
            if (betType != BetTypeEnum.MatchOdds)
                curData = from d in curData where d.Market_Tmp_Type == (int)marketTmpType select d;
            drpDefault.DataSource = curData;
            drpDefault.DataBind();
            if (marketTmpType == MarketTemplateTypeEnum.HalfTime)
            {
                foreach (ListItem item in drpDefault.Items)
                {
                    if (item.Text.ToString() == "半场0")
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
            if (marketTmpType == MarketTemplateTypeEnum.FullTime)
            {
                foreach (ListItem item in drpDefault.Items)
                {
                    if (item.Text.ToString() == "全场0")
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
        }

        private void LoadTeam(DropDownList ddlEventTeam, int? eventId, int? selectedId)
        {
            if (eventId.HasValue)
            {
                var dsEventTeam = EventTeamManager.QueryEventTeamByEventID(eventId.Value);
                var blankRow = dsEventTeam.TB_EVENT_TEAM.NewTB_EVENT_TEAMRow();
                blankRow.EVENT_TEAM_ID = -1;
                blankRow.EVENT_TEAM_NAME = string.Empty;
                dsEventTeam.TB_EVENT_TEAM.Rows.InsertAt(blankRow, 0);
                ddlEventTeam.DataValueField = "EVENT_TEAM_ID";
                ddlEventTeam.DataTextField = "EVENT_TEAM_NAME";
                ddlEventTeam.DataSource = dsEventTeam.TB_EVENT_TEAM;
                ddlEventTeam.DataBind();
            }
            else
            {
                ddlEventTeam.Items.Clear();
                ddlEventTeam.Items.Add(new ListItem(string.Empty, "-1"));
            }

            if (selectedId.HasValue)
            {
                var objTemp = ddlEventTeam.Items.FindByValue(selectedId.Value.ToString());
                if (objTemp != null)
                    ddlEventTeam.SelectedValue = selectedId.Value.ToString();
                else
                    ddlEventTeam.SelectedIndex = 0;
            }
            else
            {
                ddlEventTeam.SelectedIndex = 0;
            }
        }

        #endregion

        #region 获得比赛的状态

        private IList<MatchStatusInfo> _MatchStatusList = null;
        private IList<MatchStatusInfo> QueryMatchStatus()
        {
            if (_MatchStatusList == null)
            {
                _MatchStatusList = CommonFunction.QueryAllMatchStatus();
            }
            return _MatchStatusList;
        }

        private IList<MatchStatusInfo> _MatchAddiStatusList = null;
        private IList<MatchStatusInfo> QueryMatchAddiStatus()
        {
            if (_MatchAddiStatusList == null)
            {
                _MatchAddiStatusList = CommonFunction.QueryAllMatchAdditionalStatus();
            }
            return _MatchAddiStatusList;
        }

        private string GetMatchStatus(MatchStatusEnum matchStatus, MatchAdditionalStatusEnum matchAddiStatus)
        {
            string strStatus = string.Empty;
            IList<MatchStatusInfo> matchStatusList = QueryMatchStatus();
            var status = matchStatusList.Where(r => r.MatchStatus == (int)matchStatus).FirstOrDefault();
            var matchAddiStatusList = QueryMatchAddiStatus();
            var addiStatus = matchAddiStatusList.Where(r => r.MatchStatus == (int)matchAddiStatus).FirstOrDefault();

            if (status != null)
                strStatus = status.MatchStatusName;

            switch (matchStatus)
            {
                case MatchStatusEnum.NotActivated:
                    if (matchAddiStatus != MatchAdditionalStatusEnum.Normal)
                    {
                        strStatus = addiStatus.MatchStatusName;
                    }
                    break;
                case MatchStatusEnum.Activated:
                    if (matchAddiStatus != MatchAdditionalStatusEnum.Normal)
                    {
                        strStatus = addiStatus.MatchStatusName;
                    }
                    break;
                case MatchStatusEnum.InMatching:
                    if (matchAddiStatus != MatchAdditionalStatusEnum.Normal)
                    {
                        strStatus = strStatus + "[" + addiStatus.MatchStatusName + "]";
                    }
                    break;
                case MatchStatusEnum.HalfTimeFinished:
                    if (matchAddiStatus != MatchAdditionalStatusEnum.Normal)
                    {
                        strStatus = strStatus + "[" + addiStatus.MatchStatusName + "]";
                    }
                    break;
                default:
                    break;
            }

            return strStatus;
        }

        #endregion

        #region 显示详细信息

        private void SetUIReadOnly(bool bReadOnly)
        {
            txtMatchName.ReadOnly = bReadOnly;
            txtMatchNameEn.ReadOnly = bReadOnly;
            txtBeginDate.ReadOnly = bReadOnly;
            txtBeginTime.ReadOnly = bReadOnly;
            txtEndDate.ReadOnly = bReadOnly;
            txtEndTime.ReadOnly = bReadOnly;
            txtFreezeDate.ReadOnly = bReadOnly;
            txtFreezeTime.ReadOnly = bReadOnly;
            rbRecommend.Enabled = !bReadOnly;
            rbNotRecommend.Enabled = !bReadOnly;
            txtMemo.ReadOnly = bReadOnly;
            txtStatus.ReadOnly = bReadOnly;
            ckbMatchOdds.Enabled = !bReadOnly;
            ckbCorrectScore.Enabled = !bReadOnly;
            ckbOverUnder.Enabled = !bReadOnly;
            ckbAsianHandicap.Enabled = !bReadOnly;
            ckbCorrectScoreSecHalf.Enabled = !bReadOnly;
            ckbOverUnderSecHalf.Enabled = !bReadOnly;
            ckbAsianHandicapSecHalf.Enabled = !bReadOnly;
            ddlEvent.Enabled = !bReadOnly;
            ddlEventItem.Enabled = !bReadOnly;
            ddlEventZone.Enabled = !bReadOnly;
            ddlHomeTeam.Enabled = !bReadOnly;
            ddlVisitingTeam.Enabled = !bReadOnly;
            drpHalfDefault.Enabled = !bReadOnly;
            drpFullDefault.Enabled = !bReadOnly;
        }

        private void DisplayDetailInfo(int? matchId)
        {
            if (CurUserOperateType == UserOperateTypeEnum.AddData || !matchId.HasValue)
            {
                txtStatus.Text = GetMatchStatus(MatchStatusEnum.NotActivated, MatchAdditionalStatusEnum.Normal);
                LoadTeam(ddlHomeTeam, null, null);
                LoadTeam(ddlVisitingTeam, null, null);
                SetUIReadOnly(false);
            }
            else
            {
                var dsMatchInfo = MatchManager.QueryMatchIncMarketById(matchId.Value);
                if (dsMatchInfo.TB_MATCH.Rows.Count == 0)
                {
                    PageHelper.ShowMessage(this, "不存在当前ID的比赛信息!");
                    return;
                }
                var curMatch = dsMatchInfo.TB_MATCH[0];

                var eventObject = ddlEvent.Items.FindByValue(curMatch.EVENT_ID.ToString());
                if (eventObject == null)
                    ddlEvent.SelectedIndex = 0;
                else
                    ddlEvent.SelectedValue = curMatch.EVENT_ID.ToString();

                LoadTeam(ddlHomeTeam, Convert.ToInt32(ddlEvent.SelectedValue), curMatch.EVENT_HOME_TEAM_ID);
                LoadTeam(ddlVisitingTeam, Convert.ToInt32(ddlEvent.SelectedValue), curMatch.EVENT_HOME_GUEST_ID);

                //如果当前是另存，则需要把得分全部清零
                if (CurUserOperateType == UserOperateTypeEnum.SaveAs)
                {
                    curMatch.STATUS = (int)MatchStatusEnum.NotActivated;
                    curMatch.ADDITIONALSTATUS = (int)MatchAdditionalStatusEnum.Normal;
                }


                txtMatchName.Text = curMatch.MATCH_NAME;
                txtMatchNameEn.Text = curMatch.IsMATCH_NAME_ENNull() ? string.Empty : curMatch.MATCH_NAME_EN;
                txtBeginDate.Value = curMatch.STARTDATE;
                txtBeginTime.Text = UtilityHelper.TimeToStr(curMatch.STARTDATE);
                if (curMatch.IsENDDATENull())
                    txtEndDate.Value = null;
                else
                    txtEndDate.Value = curMatch.ENDDATE;
                txtEndTime.Text = curMatch.IsENDDATENull() ? string.Empty : UtilityHelper.TimeToStr(curMatch.ENDDATE);
                txtFreezeDate.Value = curMatch.AUTO_FREEZE_DATE;
                txtFreezeTime.Text = UtilityHelper.TimeToStr(curMatch.AUTO_FREEZE_DATE);
                rbRecommend.Checked = curMatch.RECOMMENDMATCH;
                rbNotRecommend.Checked = !curMatch.RECOMMENDMATCH;
                rbZouDi.Checked = curMatch.IsIS_ZOUDINull() ? false : curMatch.IS_ZOUDI;
                rbNotZouDi.Checked = curMatch.IsIS_ZOUDINull() ? true : !curMatch.IS_ZOUDI;
                txtHomeFirHalfScore.Text = curMatch.IsHOME_FIR_HALF_SCORENull() || curMatch.HOME_FIR_HALF_SCORE == -1 ? string.Empty : curMatch.HOME_FIR_HALF_SCORE.ToString();
                txtGuestFirHalfScore.Text = curMatch.IsGUEST_FIR_HALF_SCORENull() || curMatch.GUEST_FIR_HALF_SCORE == -1 ? string.Empty : curMatch.GUEST_FIR_HALF_SCORE.ToString();
                txtHomeSecHalfScore.Text = curMatch.IsHOME_SEC_HALF_SCORENull() || curMatch.HOME_SEC_HALF_SCORE == -1 ? string.Empty : curMatch.HOME_SEC_HALF_SCORE.ToString();
                txtGuestSecHalfScore.Text = curMatch.IsGUEST_SEC_HALF_SCORENull() || curMatch.GUEST_SEC_HALF_SCORE == -1 ? string.Empty : curMatch.GUEST_SEC_HALF_SCORE.ToString();
                txtHomeOvertimeScore.Text = curMatch.IsHOME_OVERTIME_SCORENull() || curMatch.HOME_OVERTIME_SCORE == -1 ? string.Empty : curMatch.HOME_OVERTIME_SCORE.ToString();
                txtGuestOvertimeScore.Text = curMatch.IsGUEST_OVERTIME_SCORENull() || curMatch.GUEST_OVERTIME_SCORE == -1 ? string.Empty : curMatch.GUEST_OVERTIME_SCORE.ToString();
                txtHomePointScore.Text = curMatch.IsHOME_POINT_SCORENull() || curMatch.HOME_POINT_SCORE == -1 ? string.Empty : curMatch.HOME_POINT_SCORE.ToString();
                txtGuestPointScore.Text = curMatch.IsGUEST_POINT_SCORENull() || curMatch.GUEST_POINT_SCORE == -1 ? string.Empty : curMatch.GUEST_POINT_SCORE.ToString();
                txtMemo.Text = curMatch.IsMATCH_DESCNull() ? string.Empty : curMatch.MATCH_DESC;
                txtStatus.Text = GetMatchStatus((MatchStatusEnum)curMatch.STATUS, (MatchAdditionalStatusEnum)curMatch.ADDITIONALSTATUS);

                SelectMarketTemplate(ckbMatchOdds, dsMatchInfo.TB_MATCH_MARKET);
                SelectMarketTemplate(ckbCorrectScore, dsMatchInfo.TB_MATCH_MARKET);
                SelectMarketTemplate(ckbOverUnder, dsMatchInfo.TB_MATCH_MARKET);
                SelectMarketTemplate(ckbAsianHandicap, dsMatchInfo.TB_MATCH_MARKET);
                SelectMarketTemplate(ckbCorrectScoreSecHalf, dsMatchInfo.TB_MATCH_MARKET);
                SelectMarketTemplate(ckbOverUnderSecHalf, dsMatchInfo.TB_MATCH_MARKET);
                SelectMarketTemplate(ckbAsianHandicapSecHalf, dsMatchInfo.TB_MATCH_MARKET);
                if (!curMatch.IsHandicapHalfDefaultNull())
                {
                    drpHalfDefault.SelectedValue = curMatch.HandicapHalfDefault;
                }

                if (!curMatch.IsHandicapFullDefaultNull())
                {
                    drpFullDefault.SelectedValue = curMatch.HandicapFullDefault;
                }
                //如果是另存比赛，比赛名称清空
                if (CurUserOperateType == UserOperateTypeEnum.SaveAs)
                {
                    txtMatchName.Text = string.Empty;
                    txtMatchNameEn.Text = string.Empty;
                }

                //只有未激活状态才能保存
                btnSave.Visible = ((MatchStatusEnum)curMatch.STATUS) == MatchStatusEnum.NotActivated ? true : false;
                SetUIReadOnly(!btnSave.Visible);
            }
        }

        private void SelectMarketTemplate(CheckBoxList ckbTemp, DSMatch.TB_MATCH_MARKETDataTable dtMarket)
        {
            foreach (ListItem item in ckbTemp.Items)
            {
                if (string.IsNullOrEmpty(item.Value))
                    continue;
                var tempMarkets = dtMarket.Where(r => r.MARKET_TMP_ID == Convert.ToInt32(item.Value)).ToArray();
                item.Selected = tempMarkets.Length > 0;
            }
        }
        #endregion

        #region 保存

        private DSMatch GetNeededSaveData()
        {
            DSMatch dsMatchInfo;
            if (CurUserOperateType == UserOperateTypeEnum.AddData)
            {
                dsMatchInfo = new DSMatch();
                DSMatch.TB_MATCHRow newRow = dsMatchInfo.TB_MATCH.NewTB_MATCHRow();
                newRow.MATCH_ID = -1;
                newRow.CREATE_USER = CurrentUserId;
                dsMatchInfo.TB_MATCH.AddTB_MATCHRow(newRow);
            }
            else
            {
                dsMatchInfo = MatchManager.QueryMatchIncMarketById(CurEditDataId.Value);
                if (dsMatchInfo.TB_MATCH.Rows.Count == 0)
                {
                    throw new ApplicationException("当前ID的比赛不存在!");
                }
            }

            var curMatchRow = dsMatchInfo.TB_MATCH[0];
            curMatchRow.MATCH_NAME = txtMatchName.Text.Trim();
            curMatchRow.MATCH_NAME_EN = txtMatchNameEn.Text.Trim();
            curMatchRow.EVENT_ID = Convert.ToInt32(ddlEvent.SelectedValue);
            curMatchRow.EVENT_HOME_TEAM_ID = Convert.ToInt32(ddlHomeTeam.SelectedValue);
            curMatchRow.EVENT_HOME_GUEST_ID = Convert.ToInt32(ddlVisitingTeam.SelectedValue);
            curMatchRow.STARTDATE = UtilityHelper.ConvertToDateTime(UtilityHelper.DateToStr(txtBeginDate.Value), txtBeginTime.Text);
            curMatchRow.ENDDATE = string.IsNullOrEmpty(txtEndTime.Text) ? DateTime.MaxValue : UtilityHelper.ConvertToDateTime(UtilityHelper.DateToStr(txtEndDate.Value), txtEndTime.Text);
            curMatchRow.AUTO_FREEZE_DATE = UtilityHelper.ConvertToDateTime(UtilityHelper.DateToStr(txtFreezeDate.Value), txtFreezeTime.Text);
            curMatchRow.RECOMMENDMATCH = rbRecommend.Checked;
            curMatchRow.IS_ZOUDI = rbZouDi.Checked;
            curMatchRow.MATCH_DESC = txtMemo.Text;
            if (CurUserOperateType == UserOperateTypeEnum.AddData || CurUserOperateType == UserOperateTypeEnum.SaveAs)
            {
                curMatchRow.STATUS = (int)MatchStatusEnum.NotActivated;
                curMatchRow.ADDITIONALSTATUS = (int)MatchAdditionalStatusEnum.Normal;
                if (CurUserOperateType == UserOperateTypeEnum.SaveAs)
                {
                    curMatchRow.CREATE_USER = CurrentUserId;
                }
            }
            curMatchRow.LAST_UPDATE_USER = CurrentUserId;
            curMatchRow.HandicapHalfDefault = drpHalfDefault.SelectedValue;
            curMatchRow.HandicapFullDefault = drpFullDefault.SelectedValue;
            dsMatchInfo.TB_MATCH_MARKET.Clear();
            return dsMatchInfo;
        }

        /// <summary>
        /// 获得比赛所选择的市场模板
        /// </summary>
        /// <returns></returns>
        private IList<int> GetNeedMarketTemplates()
        {
            IList<int> templateList = new List<int>();
            GetSelectedMarketTemplate(ckbMatchOdds, templateList);
            GetSelectedMarketTemplate(ckbCorrectScore, templateList);
            GetSelectedMarketTemplate(ckbOverUnder, templateList);
            GetSelectedMarketTemplate(ckbAsianHandicap, templateList);
            GetSelectedMarketTemplate(ckbCorrectScoreSecHalf, templateList);
            GetSelectedMarketTemplate(ckbOverUnderSecHalf, templateList);
            GetSelectedMarketTemplate(ckbAsianHandicapSecHalf, templateList);
            return templateList;
        }

        private void GetSelectedMarketTemplate(CheckBoxList ckbTemp, IList<int> templateList)
        {
            foreach (ListItem item in ckbTemp.Items)
            {
                if (item.Selected)
                    templateList.Add(Convert.ToInt32(item.Value));
            }
        }

        /// <summary>
        /// 验证输入是否正确
        /// </summary>
        /// <returns></returns>
        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txtMatchName.Text))
            {
                PageHelper.ShowMessage(this, "请输入比赛名称!");
                return false;
            }

            if (string.IsNullOrEmpty(txtMatchNameEn.Text))
            {
                PageHelper.ShowMessage(this, "请输入比赛英文名称!");
                return false;
            }

            if (ddlEvent.SelectedIndex <= 0)
            {
                PageHelper.ShowMessage(this, "请选择赛事!");
                return false;
            }

            if (ddlHomeTeam.SelectedIndex <= 0)
            {
                PageHelper.ShowMessage(this, "请选择主队!");
                return false;
            }

            if (ddlVisitingTeam.SelectedIndex <= 0)
            {
                PageHelper.ShowMessage(this, "请选择客队!");
                return false;
            }

            string beginDate = txtBeginDate.TextField.Text + txtBeginTime.Text;
            string beginDateFmt = txtBeginDate.TextField.Text + " " + txtBeginTime.Text;
            string endDate = txtEndDate.TextField.Text + txtEndTime.Text;
            string freezeDate = txtFreezeDate.TextField.Text + txtFreezeTime.Text;

            if (DateTime.Parse(beginDateFmt).CompareTo(DateTime.Now) < 0 && CurUserOperateType != UserOperateTypeEnum.EditData)
            {
                PageHelper.ShowMessage(this, "请检查开始日期，确认比赛开始日期不早于当前时间!");
                return false;
            }

            if (((beginDate.CompareTo(endDate) >= 0 || freezeDate.CompareTo(endDate) >= 0) && !string.IsNullOrEmpty(endDate.Trim()))
                || freezeDate.CompareTo(beginDate) >= 0)
            {
                PageHelper.ShowMessage(this, "请检查日期区间，确认开始时间小于结束时间!");
                return false;
            }
            //半场盘口
            IList<int> halfAsianHandicap = new List<int>();
            GetSelectedMarketTemplate(ckbAsianHandicap, halfAsianHandicap);
            int halfDefault = Convert.ToInt32(drpHalfDefault.SelectedValue);
            if (!halfAsianHandicap.Contains(halfDefault))
            {
                PageHelper.ShowMessage(this, "请确认该半场默认盘口已经被选中!");
                return false;
            }
            //全场盘口
            IList<int> fullAsianHandicap = new List<int>();
            GetSelectedMarketTemplate(ckbAsianHandicapSecHalf, fullAsianHandicap);
            int fullDefault = Convert.ToInt32(drpFullDefault.SelectedValue);
            if (!fullAsianHandicap.Contains(fullDefault))
            {
                PageHelper.ShowMessage(this, "请确认该全场默认盘口已经被选中!");
                return false;
            }
            return true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CurUserOperateType != UserOperateTypeEnum.AddData &&
                CurUserOperateType != UserOperateTypeEnum.EditData &&
                CurUserOperateType != UserOperateTypeEnum.SaveAs)
                return;

            if (!ValidateInput())
                return;

            if (CurEditDataId == null && CurUserOperateType == UserOperateTypeEnum.EditData)
                return;

            try
            {
                DSMatch dsMatchInfo = GetNeededSaveData();
                IList<int> marketTemplates = GetNeedMarketTemplates();
                if (CurUserOperateType == UserOperateTypeEnum.AddData || CurUserOperateType == UserOperateTypeEnum.SaveAs)
                {
                    int matchId = MatchManager.AddMatch(dsMatchInfo, marketTemplates);
                    //Response.Redirect(GameDetailFrm.Url(matchId,UserOperateTypeEnum.EditData));
                    Response.Redirect(MatchManageFrm.Url());
                    return;
                }
                else
                    MatchManager.UpdateMatch(dsMatchInfo, marketTemplates);
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
            }
        }
        #endregion

        #region Event
        protected void ddlEventItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEventItem.SelectedIndex > 0)
            {
                LoadEventZone(Convert.ToInt32(ddlEventItem.SelectedValue));
            }
            else
            {
                LoadEventZone(null);
            }
        }

        protected void ddlEventZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEventZone.SelectedIndex > 0)
            {
                LoadEvent(Convert.ToInt32(ddlEventZone.SelectedValue));
            }
            else
            {
                LoadEvent(null);
            }
        }

        protected void ddlEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEvent.SelectedIndex > 0)
            {
                LoadTeam(ddlHomeTeam, Convert.ToInt32(ddlEvent.SelectedValue), null);
                LoadTeam(ddlVisitingTeam, Convert.ToInt32(ddlEvent.SelectedValue), null);
            }
            else
            {
                LoadTeam(ddlHomeTeam, Convert.ToInt32(ddlEvent.SelectedValue), null);
                LoadTeam(ddlVisitingTeam, Convert.ToInt32(ddlEvent.SelectedValue), null);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(MatchManageFrm.Url());
        }

        #endregion
    }
}