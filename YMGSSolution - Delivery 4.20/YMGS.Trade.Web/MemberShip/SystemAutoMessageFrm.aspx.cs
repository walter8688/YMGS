using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using YMGS.Framework;
using YMGS.Data.Common;
using YMGS.Data.DataBase;
using YMGS.Trade.Web.Common;
using YMGS.Business.MemberShip;
using YMGS.Business.GameSettle;

namespace YMGS.Trade.Web.MemberShip
{
    [TopMenuId(FunctionIdList.MemberCenter.MemberCenterModule)]
    public partial class SystemAutoMessageFrm : MemberShipBasePage
    {
        private const string _CommandDelete = "CommandDelete";
        private const string _CommandView = "CommandView";

        public override string PageTitle
        {
            get
            {
                return LangManager.GetString("SystemMessage");
            }
        }

        public override bool IsAccessible(UserAccess userAccess)
        {
            return base.IsAllow(FunctionIdList.MemberCenter.FundAccountManagePage);
        }

        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(SystemAutoMessageFrm), "MemberShip").AbsoluteUri;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGridData();
            }
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
            pageNavigator.databinds(SysAutoMegManager.QuerySystemAutoMessage(sDate, eDate, CurrentUser.UserId).TB_SYSTEM_AUTOMESSAGE, gdvSystemAutoMessage);
        }

        protected void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            var msgID = Convert.ToInt32((sender as LinkButton).CommandArgument);

            //var userFund = UserFundManager.QueryUserFund(userId).TB_USER_FUND[0];
            //txtCurUserFund.Text = userFund.CUR_FUND.ToString();
            //txtCurUserName.Text = userName;
            //txtuserID.Text = userId.ToString();
            //mdlSysAutoMsgPopup.Show();
        }

        protected void gdvSystemAutoMessage_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var bindRow = (DSSystemAutoMessage.TB_SYSTEM_AUTOMESSAGERow)((DataRowView)e.Row.DataItem).Row;

                var lblMsgContent = e.Row.FindControl("lblMsgContent") as Label;
                var lblMsgDate = e.Row.FindControl("lblMsgDate") as Label;
                var btnView = e.Row.FindControl("btnView") as LinkButton;
                var btnDelete = e.Row.FindControl("btnDelete") as LinkButton;

                lblMsgContent.Text = Language == LanguageEnum.Chinese ? bindRow.MESSAGE_CONTENT : bindRow.MESSAGE_CONTENT_EN;
                lblMsgDate.Text = UtilityHelper.DateTimeDefaultForamtString(bindRow.MESSAGE_SEND_DATE);

                btnView.CommandName = _CommandView;
                btnView.CommandArgument = bindRow.MESSAGEID.ToString();

                string warningStr = LangManager.GetString("DeleteConfrim");
                btnDelete.Attributes.Add("onclick", "javascript:return confirm('" + warningStr + "');");
                btnDelete.CommandName = _CommandDelete;
                btnDelete.CommandArgument = bindRow.MESSAGEID.ToString();
            }
        }

        protected void gdvSystemAutoMessage_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == _CommandDelete)
            {
                var msgID = Convert.ToInt32(e.CommandArgument);
                DeleteSystemAutoMessage(msgID);
                return;
            }
            if (e.CommandName == _CommandView)
            {
                var msgID = Convert.ToInt32(e.CommandArgument);
                ViewSystemAutoMessageDetails(msgID);
                return;
            }
        }

        /// <summary>
        /// 查看系统信息的详细内容
        /// </summary>
        /// <param name="msgID"></param>
        private void ViewSystemAutoMessageDetails(int msgID)
        {
            try
            {
                var details = SysAutoMegManager.QuerySingleSystemAutoMessageByID(msgID).TB_SYSTEM_AUTOMESSAGE[0];
                lblMsgContentDetails.Text = Language == LanguageEnum.Chinese ? details.MESSAGE_CONTENT : details.MESSAGE_CONTENT_EN;
                mdlSysAutoMsgPopup.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "alert('" + ex.Message + "');", true);
            }
        }

        /// <summary>
        /// 删除系统消息
        /// </summary>
        /// <param name="userWDId"></param>
        private void DeleteSystemAutoMessage(int msgID)
        {
            try
            {
                SysAutoMegManager.DeleteSystemAutoMessageByMegID(msgID);
                LoadGridData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), Guid.NewGuid().ToString(), "alert('" + ex.Message + "');", true);
            }
        }

    }
}