using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Manage.Web.Common;
using YMGS.Data.Common;
using YMGS.Business.EventManage;
using YMGS.Data.DataBase;
using YMGS.Data.Entity;
using YMGS.Business.GameMarket;
using YMGS.Manage.Web.Controls;
using System.Data;
using YMGS.Framework;
using System.Collections;
using YMGS.Business.GameSettle;

namespace YMGS.Manage.Web.GameSettle
{
    [TopMenuId(FunctionIdList.GameSettle.GameSettleManageModule)]
    [LeftMenuId(FunctionIdList.GameSettle.ChampionEventSettlePage)]
    public partial class ChampionEventSettleFrm : BasePage
    {
        private const string _CalcCommandName = "CalcCommand";
        private const string _ReCalcCommandName = "ReCalcCommand";

        private const string _ChampSportsEvent = "体育";
        private const string _ChampEntertainmentEvent = "娱乐";
        private const string _ChampEventStatusUnActivated = "未激活";
        private const string _ChampEventStatusActivated = "激活";
        private const string _ChampEventStatusPause = "暂停";
        private const string _ChampEventStatusAbort = "终止";
        private const string _ChampEventStatusCalculated = "已结算";
        private const string _ChampEventStatusFinished = "已结束";
        private ASPProgressBar _AspProgressBar = null;

        protected override int Func_PageId
        {
            get
            {
                return FunctionIdList.GameSettle.ChampionEventSettlePage;
            }
        }

        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(ChampionEventSettleFrm), "GameSettle").AbsoluteUri;
        }

        private int CurrentUserId
        {
            get
            {
                return MySession.CurrentUser.ACCOUNT[0].USER_ID;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.pageNavigator.PageIndexChanged += new EventHandler(pageNavigator_PageIndexChanged);
            if (!IsPostBack)
            {
                LoadPageContralData();
                LoadGridData();
            }
        }

        #region 加载页面数据

        private void LoadPageContralData()
        {
            var champEventTypeList = CommonFunction.QueryAllChampEventType();
            PageHelper.BindListControlData(this.drpChampEventType, champEventTypeList, "ChampEventTypeName", "ChampEventTypeID", true);

            var curDate = PageHelper.QueryCurSysDateTime();
            calStartDate.Value = curDate.AddDays(-30);
        }

        private void LoadGridData()
        {
            int? champEventType = Convert.ToInt32(this.drpChampEventType.SelectedValue);
            string champEventName = this.txtChampEventName.Text.Trim();
            string champEventDesc = this.txtChampEventDesc.Text.Trim();
            DateTime? startDate, endDate;
            if (this.calStartDate.Value.HasValue)
                startDate = this.calStartDate.Value.Value;
            else
                startDate = null;
            if (this.calEndDate.Value.HasValue)
                endDate = this.calEndDate.Value.Value;
            else
                endDate = null;
            var champEventList = ChampEventManager.QueryChampEvent(champEventType, champEventName,string.Empty, champEventDesc, startDate, endDate);
            //champEventList.TB_Champ_Event.DefaultView.RowFilter = "CHAMP_EVENT_STATUS IN(4,5)";
            var dt = champEventList.TB_Champ_Event.Clone();
            foreach (var row in champEventList.TB_Champ_Event)
            {
                if (row.Champ_Event_Status == (int)ChampEventStatusEnum.Calculated ||
                    row.Champ_Event_Status == (int)ChampEventStatusEnum.Finished)
                {
                    dt.ImportRow(row);
                }
            }
            this.pageNavigator.databinds(dt, this.gdvChampEvent);
        }

        protected void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            LoadGridData();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadGridData();
        }

        #endregion

        #region Grid数据绑定
        protected void gdvChampEvent_RowDataBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var bindRow = (DSChampEvent.TB_Champ_EventRow)((DataRowView)e.Row.DataItem).Row;//获取当前数据行
                var typeEnum = (ChampEventTypeEnum)bindRow.Champ_Event_Type;
                Label lblChampEventTypeName = (e.Row.FindControl("lblChampEventTypeName") as Label);
                lblChampEventTypeName.Text = typeEnum == ChampEventTypeEnum.Sports ? _ChampSportsEvent : _ChampEntertainmentEvent;
                var statusEnum = (ChampEventStatusEnum)bindRow.Champ_Event_Status;
                Label lblChampEventStatusName = (e.Row.FindControl("lblChampEventStatusName") as Label);

                LinkButton btnCalcChampEvent = (LinkButton)e.Row.FindControl("btnCalcChampEvent");
                LinkButton btnReCalcChampEvent = (LinkButton)e.Row.FindControl("btnReCalcChampEvent");

                btnCalcChampEvent.CommandName = _CalcCommandName;
                btnCalcChampEvent.CommandArgument = bindRow.Champ_Event_ID.ToString();
                btnCalcChampEvent.OnClientClick = "if(window.confirm('确定要进行冠军赛事的比赛结算吗?')) return true;else return false;";
                btnCalcChampEvent.Visible = false;

                btnReCalcChampEvent.CommandName = _ReCalcCommandName;
                btnReCalcChampEvent.CommandArgument = bindRow.Champ_Event_ID.ToString();
                btnReCalcChampEvent.OnClientClick = "if(window.confirm('确定要重新结算吗?')) return true;else return false;";
                btnReCalcChampEvent.Visible = false;

                switch (statusEnum)
                {
                    case ChampEventStatusEnum.UnActivated:
                        lblChampEventStatusName.Text = _ChampEventStatusUnActivated;
                        break;
                    case ChampEventStatusEnum.Activated:
                        lblChampEventStatusName.Text = _ChampEventStatusActivated;
                        break;
                    case ChampEventStatusEnum.Pause:
                        lblChampEventStatusName.Text = _ChampEventStatusPause;
                        break;
                    case ChampEventStatusEnum.Abort:
                        lblChampEventStatusName.Text = _ChampEventStatusAbort;
                        break;
                    case ChampEventStatusEnum.Calculated:
                        lblChampEventStatusName.Text = _ChampEventStatusCalculated;
                        btnReCalcChampEvent.Visible = true;
                        break;
                    case ChampEventStatusEnum.Finished:
                        lblChampEventStatusName.Text = _ChampEventStatusFinished;
                        btnCalcChampEvent.Visible = true;
                        break;
                }
                btnCalcChampEvent.Visible = btnCalcChampEvent.Visible && MySession.Accessable(FunctionIdList.GameSettle.SetteleChampionEvent);
                btnReCalcChampEvent.Visible = btnReCalcChampEvent.Visible && MySession.Accessable(FunctionIdList.GameSettle.ReSettleChampionEvent);
            }
        }
        #endregion

        #region 冠军赛事结算

        protected void gridData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //结算
            if (e.CommandName == _CalcCommandName)
            {
                CalcChampEvent(Convert.ToInt32(e.CommandArgument));
                return;
            }

            //重新结算
            if (e.CommandName == _ReCalcCommandName)
            {
                ReCalcChampEvent(Convert.ToInt32(e.CommandArgument));
                return;
            }
        }

        private void InitProgressBar()
        {
            _AspProgressBar = new ASPProgressBar(this, "GameSettle");
            _AspProgressBar.Init();
            _AspProgressBar.Show();
        }

        private void UpdateProgress(string strProgressText, int value)
        {
            if (_AspProgressBar != null)
            {
                _AspProgressBar.UpdateProgress(value, strProgressText);
            }
        }

        private void CalcChampEvent(int editKeyId)
        {
            try
            {
                string strMessage = ChampionGameSettleManager.CalcChampEventGame(editKeyId, CurrentUserId,InitProgressBar, UpdateProgress,true);
                if (!string.IsNullOrEmpty(strMessage))
                {
                    PageHelper.ShowMessage(this, strMessage);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex.Message);
            }
            finally
            {
                if (_AspProgressBar != null)
                {
                    _AspProgressBar.Hide(ChampionEventSettleFrm.Url());
                }
            }
        }
        #endregion

        #region 冠军赛事重新结算

        private void ReCalcChampEvent(int editKeyId)
        {
            try
            {
                string strMessage = RollbackSettlementManager.RollbackSettlement(editKeyId,
                    2, 1, InitProgressBar, UpdateProgress);
                    
                if (!string.IsNullOrEmpty(strMessage))
                {
                    PageHelper.ShowMessage(this, strMessage);
                }

                strMessage = ChampionGameSettleManager.CalcChampEventGame(editKeyId, CurrentUserId, InitProgressBar, UpdateProgress, false);
                if (!string.IsNullOrEmpty(strMessage))
                {
                    PageHelper.ShowMessage(this, strMessage);
                }

            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex.Message);
            }
            finally
            {
                if (_AspProgressBar != null)
                {
                    _AspProgressBar.Hide(ChampionEventSettleFrm.Url());
                }
            }
        }

        #endregion
    }
}