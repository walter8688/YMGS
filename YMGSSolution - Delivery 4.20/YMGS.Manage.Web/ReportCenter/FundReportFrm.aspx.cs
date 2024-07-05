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
using System.Data;

namespace YMGS.Manage.Web.ReportCenter
{
    [TopMenuId(FunctionIdList.ReportCenter.ReportCenterModule)]
    [LeftMenuId(FunctionIdList.ReportCenter.FundReport)]
    public partial class FundReportFrm : BasePage
    {
        private const string _SheetName = "资金报表";
        private const string _FileName = "资金报表 ";
        public string[] ExcelColumnNames
        {
            get
            {
                return new string[] { "类型", "时间", "金额", "会员名", "状态" };
            }
        }

        protected override int Func_PageId
        {
            get
            {
                return FunctionIdList.ReportCenter.FundReport;
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

        private void LoadPageData()
        {
            calEndDate.Value = DateTime.Now;
            calStartDate.Value = DateTime.Now.AddDays(-3);
            var fundTypeList = CommonFunction.QueryAllFundReportTypeInfo();
            PageHelper.BindListControlData(drpFundType, fundTypeList, "FundRepotyTypeText", "FundReportTypeValue", true);
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
            var fundReportDS = FundReportManager.QueryFundReport(sDate, eDate, fundReportType);

            decimal fundTotal = 0;
            foreach (var row in fundReportDS.TBFundReport)
            {
                if(!row.IsTRAN_AMOUNTNull())
                    fundTotal += row.TRAN_AMOUNT;
            }
            lblFundTotal.Text = fundTotal.ToString();

            ViewState["fundReportDS"] = fundReportDS.TBFundReport;
            pageNavigator.databinds(fundReportDS.TBFundReport, gdvFund);
        }

        protected void gdvFund_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var bindRow = (DSFundReport.TBFundReportRow)((DataRowView)e.Row.DataItem).Row;
                var lblFundType = e.Row.FindControl("lblFundType") as Label;
                var lblFundDate = e.Row.FindControl("lblFundDate") as Label;
                var lblFundAmt = e.Row.FindControl("lblFundAmt") as Label;
                var lblFundUserName = e.Row.FindControl("lblFundUserName") as Label;
                var lblStatus = e.Row.FindControl("lblStatus") as Label;

                lblFundType.Text = bindRow.TRAN_TYPE;
                lblFundDate.Text = UtilityHelper.DateToStr(bindRow.TRAN_DATE);
                lblFundAmt.Text = bindRow.TRAN_AMOUNT.ToString();
                lblFundUserName.Text = bindRow.LOGIN_NAME;
                lblStatus.Text = bindRow.TRAN_STATUS;
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
            var fundReportDS = FundReportManager.QueryFundReport(sDate, eDate, fundReportType);
            ExcelHelper.ExportDataToExcel(fundReportDS.TBFundReport, _SheetName, ExcelColumnNames, _FileName);
        }

        protected void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            LoadGridData();
        }
    }
}