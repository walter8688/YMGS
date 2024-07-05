using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Framework;
using YMGS.Data.Common;
using YMGS.Data.DataBase;
using YMGS.Business.GameSettle;
using YMGS.Manage.Web.Common;
using System.Data;


namespace YMGS.Manage.Web.GameSettle
{
    [TopMenuId(FunctionIdList.GameSettle.GameSettleManageModule)]
    [LeftMenuId(FunctionIdList.GameSettle.CommissionManagePage)]
    public partial class CommissionManagePage : BasePage
    {
        private const string BtnCommandNew = "btnCommandNew";
        private const string BtnCommandEdit = "btnCommandEdit";
        private static bool canSetBrokerage = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPageAccess();
                LoadGridData();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.pageNavigator.PageIndexChanged +=new EventHandler(pageNavigator_PageIndexChanged);
        }

        protected override int Func_PageId
        {
            get
            {
                return FunctionIdList.GameSettle.CommissionManagePage;
            }
        }

        private void InitPageAccess()
        {
            this.btnNew.Visible = MySession.Accessable(FunctionIdList.GameSettle.CommissionRateManage);
            canSetBrokerage = MySession.Accessable(FunctionIdList.GameSettle.CommissionRateManage);
        }

        private void InitPageStyle()
        {
            this.txtBrokerage.Text = string.Empty;
            this.txtIntegralAbove.Text = string.Empty;
            this.txtIntegralBelow.Text = string.Empty;
        }

        private DSBrokerageIntegral.TB_BROKERAGE_INTEGRAL_MAPRow GetCurrentRow()
        {
            var newRow = new DSBrokerageIntegral.TB_BROKERAGE_INTEGRAL_MAPDataTable().NewTB_BROKERAGE_INTEGRAL_MAPRow();
            newRow.Brokerage_Rate = (Convert.ToDecimal(this.txtBrokerage.Text)) / 100;
            newRow.Min_Integral = Convert.ToInt32(this.txtIntegralAbove.Text);
            newRow.Max_Integral = Convert.ToInt32(this.txtIntegralBelow.Text);
            newRow.Create_User = MySession.CurrentUser.ACCOUNT[0].USER_ID;
            return newRow;
        }

        private void LoadGridData()
        {
            var brokerageIntegralDS = CommissionManager.QueryBrokerageIntegral();
            this.pageNavigator.databinds(brokerageIntegralDS.Tables[0], this.gdvBrokerage);
        }

        protected void btnEidt_Click(object sender, EventArgs e)
        {
            mdlPopup.Show();
            this.btnSave.CommandName = BtnCommandEdit;
            var argArr = (sender as LinkButton).CommandArgument.Split(',');
            this.hidTxtBrokerageID.Text = argArr[0];
            this.txtBrokerage.Text = argArr[1];
            this.txtIntegralAbove.Text = argArr[2];
            this.txtIntegralBelow.Text = argArr[3];
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var brokerageId = Convert.ToInt32((sender as LinkButton).CommandArgument);
            CommissionManager.DelBrokerageIntegral(brokerageId);
            LoadGridData();
        }

        //protected void btnQuery_Click(object sender, EventArgs e)
        //{
        //    LoadGridData();
        //}

        protected void btnNew_Click(object sender, EventArgs e)
        {
            mdlPopup.Show();
            this.btnSave.CommandName = BtnCommandNew;
            InitPageStyle();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var saveObj = (sender as Button);
                if (saveObj.CommandName == BtnCommandNew)
                {
                    CommissionManager.AddBrokerageIntegral(GetCurrentRow());
                }
                else if (saveObj.CommandName == BtnCommandEdit)
                {
                    var editRow = GetCurrentRow();
                    editRow.Brokerage_Rate_ID = Convert.ToInt32(hidTxtBrokerageID.Text);
                    CommissionManager.UpdateBrokerageIntegral(editRow);
                }
                LoadGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this.Page, ex.Message);
            }
        }

        protected void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void gdvBrokerage_RowDataBind(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var bindRow = (DSBrokerageIntegral.TB_BROKERAGE_INTEGRAL_MAPRow)((DataRowView)e.Row.DataItem).Row;
                var lblBrokerageRate = (e.Row.FindControl("lblBrokerageRate") as Label);
                var lblIntegral = (e.Row.FindControl("lblIntegral") as Label);
                lblBrokerageRate.Text = string.Format("{0}", bindRow.Brokerage_Rate * 100);
                lblBrokerageRate.Text = lblBrokerageRate.Text.Substring(0, lblBrokerageRate.Text.Length - 2) + "%";
                lblIntegral.Text = string.Format("{0}-{1}", bindRow.Min_Integral, bindRow.Max_Integral);

                (e.Row.FindControl("hlEdit") as LinkButton).Visible = canSetBrokerage;
                (e.Row.FindControl("hlDelete") as LinkButton).Visible = canSetBrokerage;
                (e.Row.FindControl("hlEdit") as LinkButton).CommandArgument = string.Format("{0},{1},{2},{3}", bindRow.Brokerage_Rate_ID, bindRow.Brokerage_Rate, bindRow.Min_Integral, bindRow.Max_Integral);
                (e.Row.FindControl("hlDelete") as LinkButton).CommandArgument = bindRow.Brokerage_Rate_ID.ToString();
            }
        }
    }
}