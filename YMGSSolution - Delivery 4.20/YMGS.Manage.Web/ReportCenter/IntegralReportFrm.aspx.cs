using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Data.Common;
using YMGS.Framework;
using YMGS.Data.Presentation;
using YMGS.Manage.Web.Common;
using YMGS.Business.ReportCenter;

namespace YMGS.Manage.Web.ReportCenter
{
    [TopMenuId(FunctionIdList.ReportCenter.ReportCenterModule)]
    [LeftMenuId(FunctionIdList.ReportCenter.IntegralReport)]
    public partial class IntegralReportFrm : BasePage
    {
        private const string _brokerageSheetName = "佣金报表";
        private const string _commissionSheetName = "返点报表";
        private const string _brokerageFileName = "佣金报表";
        private const string _commissionFileName = "返点报表";


        public string[] bokerageColumnNames
        {
            get { return new string[] { "类型", "结算时间", "赛事名称", "比赛名称", "玩法", "市场名称", "成交赔率", "成交金额", "交易时间", "赢家", "输家", "佣金率", "佣金额" }; }
        }

        public string[] commissionColumnNames
        {
            get { return new string[] { "类型", "结算时间", "赛事名称", "比赛名称", "玩法", "市场名称", "成交赔率", "成交金额", "交易时间", "赢家", "输家", "代理返点率", "总代理返点率", "代理返点金额", "总代理返点金额" }; }
        }

        protected override int Func_PageId
        {
            get
            {
                return FunctionIdList.ReportCenter.IntegralReport;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            pageNavigator.PageIndexChanged += new EventHandler(pageNavigator_PageIndexChanged);
            pageNavigatorCommission.PageIndexChanged += new EventHandler(pageNavigator_PageIndexChanged);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPageData();
                LoadGridData();
            }
        }

        private void LoadPageData()
        {
            calEndDate.Value = DateTime.Now;
            calStartDate.Value = DateTime.Now.AddDays(-3);
            var integralTypeList = CommonFunction.QueryAllIntegralReportTypeInfo();
            PageHelper.BindListControlData(drpFundType, integralTypeList, "IntegralReportTypeText", "IntegralReportTypeValue", false);
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
            
            var fundReportType = Convert.ToInt32(drpFundType.SelectedValue);
            decimal totalFlag = 0;
            if (fundReportType == 0)
            {
                gdvCommission.Visible = false;
                pageNavigatorCommission.Visible = false;
                gdvBrokerage.Visible = true;
                pageNavigator.Visible = true;
                var brokerageDS = IntegralReportManager.QueryBrokerageReport(sDate, eDate);
                ViewState["brokerageDS"] = brokerageDS.TB_Brokerage_Report;
                pageNavigator.databinds(brokerageDS.TB_Brokerage_Report, gdvBrokerage);
                divBrk.Visible = true;
                divAgent.Visible = false;
                foreach (var row in brokerageDS.TB_Brokerage_Report)
                {
                    if (!row.IsBROKERAGENull())
                        totalFlag += row.BROKERAGE;
                }
                lblBrkFundTotal.Text = totalFlag.ToString();
            }
            else if (fundReportType == 1)
            {
                gdvCommission.Visible = true;
                pageNavigatorCommission.Visible = true;
                gdvBrokerage.Visible = false;
                pageNavigator.Visible = false;
                var commissionDS = IntegralReportManager.QueryCommissionReport(sDate, eDate);
                ViewState["commissionDS"] = commissionDS.TB_Commission_Report;
                pageNavigatorCommission.databinds(commissionDS.TB_Commission_Report, gdvCommission);
                divBrk.Visible = false;
                divAgent.Visible = true;
                decimal temp = 0;
                foreach (var row in commissionDS.TB_Commission_Report)
                {
                    if (!row.IsAGENT_COMMISSION_TRADE_FUNDNull())
                        totalFlag += row.AGENT_COMMISSION_TRADE_FUND;
                    if (!row.IsMAIN_AGENT_COMMISSION_TRADE_FUNDNull())
                        temp += row.MAIN_AGENT_COMMISSION_TRADE_FUND;
                }
                lblAgentFundTotal.Text = totalFlag.ToString();
                lblGAFundTotal.Text = temp.ToString();
            }
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

            var fundReportType = Convert.ToInt32(drpFundType.SelectedValue);
            if (fundReportType == 0)
            {
                var brokerageDS = IntegralReportManager.QueryBrokerageReport(sDate, eDate);
                ExcelHelper.ExportDataToExcel(brokerageDS.TB_Brokerage_Report, _brokerageSheetName, bokerageColumnNames, _brokerageFileName);
            }
            else if (fundReportType == 1)
            {
                var commissionDS = IntegralReportManager.QueryCommissionReport(sDate, eDate);
                ExcelHelper.ExportDataToExcel(commissionDS.TB_Commission_Report, _commissionSheetName, commissionColumnNames, _commissionFileName);
            }
        }
        
        protected void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            LoadGridData();
        }
    }
}