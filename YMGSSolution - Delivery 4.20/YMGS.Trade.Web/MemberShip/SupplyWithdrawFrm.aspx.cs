using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Common;
using YMGS.Data.Common;
using System.Data;
using YMGS.Data.DataBase;
using YMGS.Business.MemberShip;
using YMGS.Framework;

namespace YMGS.Trade.Web.MemberShip
{
    [TopMenuId(FunctionIdList.MemberCenter.MemberCenterModule)]
    public partial class SupplyWithdrawFrm : MemberShipBasePage
    {
        private const string _CommandCancle = "CommandCancle";
        public override string PageTitle
        {
            get
            {
                return LangManager.GetString("FinancialAccountPage");
            }
        }

        public override bool IsAccessible(UserAccess userAccess)
        {
            return base.IsAllow(FunctionIdList.MemberCenter.FundAccountManagePage);
        }

        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(SupplyWithdrawFrm), "MemberShip").AbsoluteUri;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            pageNavigator.PageIndexChanged += new EventHandler(pageNavigator_PageIndexChanged);
            btnOnlineCharge.Visible = base.IsAllow(FunctionIdList.MemberCenter.OnlinePay);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadGridData();
        }

        private void LoadGridData()
        {
            DateTime sDate, eDate;
            if (calStartDate.Value.HasValue)
                sDate = calStartDate.Value.Value;
            else
                sDate = DateTime.MinValue;
            if (calEndDate.Value.HasValue)
                eDate = calEndDate.Value.Value;
            else
                eDate = DateTime.MaxValue;
            pageNavigator.databinds(UserWithDrawManager.QueryUserWithDraw(sDate, eDate, CurrentUser.UserId).TB_USER_WITHDRAW, gdvUserWithDraw);
        }

        private DSUserWithDraw.TB_USER_WITHDRAWRow GetCurUserWD()
        {
            DSUserWithDraw.TB_USER_WITHDRAWRow userWD = new DSUserWithDraw.TB_USER_WITHDRAWDataTable().NewTB_USER_WITHDRAWRow();
            userWD.USER_ID = CurrentUser.UserId;
            userWD.TRANS_ID = string.Empty;
            userWD.WD_AMOUNT = decimal.Parse(txtWDAmt.Text);
            userWD.REMARK = string.Empty;
            return userWD;
        }

        protected void BtnSetBankInfo_Click(object sender, EventArgs e)
        {
            Response.Redirect(FinancialAccountFrm.Url());
        }

        protected void BtnOnlineCharge_Click(object sender, EventArgs e)
        {
            Response.Redirect(OnlineChargeFrm.Url());
        }

        protected void btnFundDetail_Click(object sender, EventArgs e)
        {
            Response.Redirect(UserFundDetailFrm.Url());
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void BtnWithDraw_Cilck(object sender, EventArgs e)
        {
            txtWDAmt.Text = "100";
            mdlPopup.Show();
        }

        protected void BtnSaveWithDraw_Click(object sender, EventArgs e)
        {
            try
            {
                var userWD = GetCurUserWD();
                userWD.WD_STATUS = (int)UserWithDrawStatus.Supplying;
                UserWithDrawManager.AddUserWithDraw(userWD);
                LoadGridData();
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "alert('" + ex.Message + "');", true);
            }
        }

        protected void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void gdvWithDraw_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var bindRow = (DSUserWithDraw.TB_USER_WITHDRAWRow)((DataRowView)e.Row.DataItem).Row;
                var lblWDStatus = e.Row.FindControl("lblWDStatus") as Label;
                var lblWDDate = e.Row.FindControl("lblWDDate") as Label;
                var btnCancleWD = e.Row.FindControl("btnCancleWD") as LinkButton;

                switch ((UserWithDrawStatus)bindRow.WD_STATUS)
                {
                    case UserWithDrawStatus.Supplying:
                        lblWDStatus.Text = LangManager.GetString("Supplying");
                        break;
                    case UserWithDrawStatus.Confirmed:
                        lblWDStatus.Text = LangManager.GetString("Confirmed");
                        break;
                    case UserWithDrawStatus.Rejected:
                        lblWDStatus.Text = LangManager.GetString("Rejected");
                        break;
                    case UserWithDrawStatus.Transfered:
                        lblWDStatus.Text = LangManager.GetString("Transfered");
                        break;
                    case UserWithDrawStatus.Cancled:
                        lblWDStatus.Text = LangManager.GetString("Cancled");
                        break;
                }

                lblWDDate.Text = UtilityHelper.DateToStr(bindRow.WD_DATE);

                btnCancleWD.Visible = bindRow.WD_STATUS == (int)UserWithDrawStatus.Supplying ? true : false;
                btnCancleWD.CommandName = _CommandCancle;
                btnCancleWD.CommandArgument = bindRow.USER_WD_ID.ToString();
            }
        }

        protected void gdvWithDraw_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == _CommandCancle)
            {
                var userWDId = Convert.ToInt32(e.CommandArgument);
                CancleUserWithDraw(userWDId);
                return;
            }
        }

        /// <summary>
        /// 取消用户提现
        /// </summary>
        /// <param name="userWDId"></param>
        private void CancleUserWithDraw(int userWDId)
        {
            try
            {
                UserWithDrawManager.CancleUserWithDraw(userWDId);
                LoadGridData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "alert('" + ex.Message + "');", true);
            }
        }
    }
}