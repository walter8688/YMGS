using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Common;
using YMGS.Data.Common;
using YMGS.Business.GameMarket;

namespace YMGS.Manage.Web.GameMarket
{
    [TopMenuId(FunctionIdList.MemberCenter.MemberCenterModule)]
    public partial class GameMarketManageFrm : BasePage
    {
        public override string PageTitle
        {
            get
            {
                return LangManager.GetString("MemberHomePage");
            }
        }

        public override bool IsAccessible(YMGS.Trade.Web.Common.UserAccess userAccess)
        {
            return true;
        }   

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            var dsTemp = BetTypeManager.QueryAllBetType();
            gridData.DataSource = dsTemp.TB_BET_TYPE;
            gridData.DataBind();
        }

        void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //BindGrid(1);
        }

        protected void gridData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType != DataControlRowType.DataRow)
            //    return;

            //var bindRow = ((DataRowView)e.Row.DataItem).Row;
            //if (bindRow == null)
            //    return;

            //string applyFormCode = bindRow["APPLY_FORM_CODE"].ToString();
            //ImageButton btnDetail = (ImageButton)e.Row.FindControl("btnDetail");
            //btnDetail.CommandArgument = applyFormCode;
            //btnDetail.CommandName = "Detail";

            //ImageButton btnDel = (ImageButton)e.Row.FindControl("btnDel");
            //btnDel.CommandName = "Del";
            //btnDel.CommandArgument = applyFormCode;
            //btnDel.OnClientClick = "if(window.confirm('确定要删除吗?')) return true;else return false;";
        }

        protected void gridData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "Detail")
            //{
            //    //导航到详细信息页面。
            //    Response.Redirect(PackageApplyFormDetailFrm.Url(e.CommandArgument.ToString(), UserOperateTypeEnum.Edit, PackageApplyFormFrm.Url()), true);
            //    return;
            //}

            //if (e.CommandName == "Del")
            //{
            //    ApplyFormController tempController = new ApplyFormController(e.CommandArgument.ToString());
            //    if (tempController.FormStatus == ApplyFormStatusEnum.NotSubmitted)
            //    {
            //        tempController.DeleteApplyForm();
            //        BindGrid(1);
            //    }
            //    else
            //    {
            //        PageHelper.ShowMessage(this, "不能删除该表单，只有未提交的表单才能被删除!");
            //    }
            //}
        }


        protected void btnNew_Click(object sender, EventArgs e)
        {
            mdlPopup.Show();
            //Response.Redirect(PackageApplyFormDetailFrm.Url(string.Empty, UserOperateTypeEnum.Add, PackageApplyFormFrm.Url()), true);
        }
    }
}