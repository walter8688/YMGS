using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Data.Common;
using YMGS.Framework;
using YMGS.Data.Presentation;
using YMGS.Business.ReportCenter;
using YMGS.Manage.Web.Common;
using YMGS.Data.DataBase;
using YMGS.Business.EventManage;

namespace YMGS.Manage.Web.ReportCenter
{
    [TopMenuId(FunctionIdList.ReportCenter.ReportCenterModule)]
    [LeftMenuId(FunctionIdList.ReportCenter.DealReport)]
    public partial class DealReportFrm : BasePage
    {
        private const string _sheetName = "撮合报表";
        private const string _fileName = "撮合报表";

        public string[] ColumnNames { get { return new string[] { "赛事名称", "比赛名称", "玩法", "半全场", "市场名称", "赔率", "成交金额", "交易时间", "投注人", "受注人" }; } }

        protected override int Func_PageId
        {
            get
            {
                return FunctionIdList.ReportCenter.DealReport;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            pageNavigator.PageIndexChanged += new EventHandler(pageNavigator_PageIndexChanged);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPageData();
                LoadGridData();
            }
        }

        private void LoadEventItem()
        {
            var eventItemList = EventZoneManager.QueryEventItem();
            var blankRow = eventItemList.TB_EVENT_ITEM.NewTB_EVENT_ITEMRow();
            blankRow.EventItem_ID = -1;
            blankRow.EventItem_Name = string.Empty;
            blankRow.EventItem_Name_En = string.Empty;
            eventItemList.TB_EVENT_ITEM.Rows.InsertAt(blankRow, 0);
            ddlEventItem.DataValueField = "EVENTITEM_ID";
            ddlEventItem.DataTextField = "EVENTITEM_NAME";
            ddlEventItem.DataSource = eventItemList.TB_EVENT_ITEM;
            ddlEventItem.DataBind();
            ddlEventItem.SelectedIndex = 0;
        }

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

        protected void ddlEventItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEventItem.SelectedIndex == -1)
                LoadEventZone(null);
            else
                LoadEventZone(Convert.ToInt32(ddlEventItem.SelectedValue));
        }

        private void LoadPageData()
        {
            calEndDate.Value = DateTime.Now;
            calStartDate.Value = DateTime.Now.AddDays(-3);
            var betTypeList = CommonFunction.QueryAllBetTypeInfo();
            PageHelper.BindListControlData(drpBetType, betTypeList, "BetTypeText", "BetTypeValue", true);
            LoadEventItem();
        }

        private void LoadGridData()
        {
            DateTime sDate, eDate;
            if (calStartDate.Value.HasValue)
                sDate = calStartDate.Value.Value;
            else
                sDate = DateTime.MinValue;
            if (calEndDate.Value.HasValue)
                eDate = calEndDate.Value.Value.AddDays(1);
            else
                eDate = DateTime.MaxValue;
            var betType = drpBetType.SelectedItem.Text;
            var eventItemName = ddlEventItem.SelectedIndex > 0 ? ddlEventItem.SelectedItem.Text : string.Empty;
            var eventZoneName = ddlEventZone.SelectedIndex > 0 ? ddlEventZone.SelectedItem.Text : string.Empty;
            var eventName = ddlEvent.SelectedIndex > 0 ? ddlEvent.SelectedItem.Text : string.Empty;
            var dealReportDS = DealReportManager.QueryDealReport(sDate, eDate, betType, eventItemName, eventZoneName, eventName);
            pageNavigator.databinds(dealReportDS.TB_Deal_Report, gdvDeal);
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            DateTime sDate, eDate;
            if (calStartDate.Value.HasValue)
                sDate = calStartDate.Value.Value;
            else
                sDate = DateTime.MinValue;
            if (calEndDate.Value.HasValue)
                eDate = calEndDate.Value.Value.AddDays(1);
            else
                eDate = DateTime.MaxValue;
            var betType = drpBetType.SelectedItem.Text;
            var eventItemName = ddlEventItem.SelectedIndex > 0 ? ddlEventItem.SelectedItem.Text : string.Empty;
            var eventZoneName = ddlEventZone.SelectedIndex > 0 ? ddlEventZone.SelectedItem.Text : string.Empty;
            var eventName = ddlEvent.SelectedIndex > 0 ? ddlEvent.SelectedItem.Text : string.Empty;
            var dealReportDS = DealReportManager.QueryDealReport(sDate, eDate, betType, eventItemName, eventZoneName, eventName);
            ExcelHelper.ExportDataToExcel(dealReportDS.TB_Deal_Report, _sheetName, ColumnNames, _fileName);
        }

        protected void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            LoadGridData();
        }
    }
}