using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Manage.Web.Common;
using YMGS.Data.Common;
using System.Data;
using YMGS.Business.AssistManage;
using YMGS.Data.Presentation;

namespace YMGS.Manage.Web.AssistManage
{
    [LeftMenuId(FunctionIdList.AssistantManagement.NoticeManagePage)]
    [TopMenuId(FunctionIdList.AssistantManagement.AssistantManageModule)]
    public partial class HotLine : QueryBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(HotLine), "AssistManage").AbsoluteUri;
        }
        protected override int Func_PageId
        {
            get
            {
                return FunctionIdList.AssistantManagement.NoticeManagePage;
            }
        }
        protected override DataTable GetData()
        {
            DSNOTICE ds = new DSNOTICE();
            DSNOTICE.TB_AD_NOTICERow row = ds.TB_AD_NOTICE.NewRow() as DSNOTICE.TB_AD_NOTICERow;
            row.PID = 0;
            row.TITLE = txttitles.Text;
            row.ISV = ckbIsValiabe.Checked ? 1 : 0;
            return NoticeManager.QueryNotice(row, 0).TB_AD_NOTICE;
        }

        public int CurUserStatus
        {
            get
            {
                if (ViewState["Status"] == null)
                    return 0;
                return (int)ViewState["Status"];
            }
            set
            {
                ViewState["Status"] = value;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!IsPostBack)
            {
                btnNew.Visible = MySession.Accessable(FunctionIdList.AssistantManagement.AddNotice);
                BindGrid();
            }
        }
        protected void gdvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = e.Row.Cells[2].Text;
                if (status == "0")
                    status = "无效";
                if (status == "1")
                    status = "有效";

                e.Row.Cells[2].Text = status;
                LinkButton hlEdit = e.Row.FindControl("hlEdit") as LinkButton;
                LinkButton hlDelete = e.Row.FindControl("hlDelete") as LinkButton;
                hlEdit.Visible = MySession.Accessable(FunctionIdList.AssistantManagement.EditNotice);
                hlDelete.Visible = MySession.Accessable(FunctionIdList.AssistantManagement.DeleteNotice);
            }
        }

        public void ClearData()
        {
            hfdpid.Value = "0";
            txttitle.Text = string.Empty;
            txtentitle.Text = string.Empty;
            txtcontent.Text = string.Empty;
            txtencontent.Text = string.Empty;
            ckbisv.Checked = false;
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            ClearData();
            ckbisv.Visible = MySession.Accessable(FunctionIdList.AssistantManagement.SuspendNotice);
            if (sender is Button)
            {

                this.btnSave.CommandArgument = ButtonCommandType.Add.ToString();
                mdlPopup.Show();
            }
            if (sender is LinkButton)
            {
                LinkButton btn = sender as LinkButton;
                var commandString = btn.CommandArgument;
                hfdpid.Value = commandString;
                DataTable dt = GetData();
                DataRow[] DRS = dt.Select("PID=" + commandString);
                DataRow dr;
                if (DRS.Count() > 0)
                {
                    dr = DRS[0];
                    hfdpid.Value = dr["PID"].ToString();
                    txttitle.Text = dr["TITLE"].ToString();
                    txtentitle.Text = dr["ENTITLE"].ToString();
                    txtcontent.Text = dr["CONTENT"].ToString();
                    txtencontent.Text = dr["ENCONTENT"].ToString();
                    ckbisv.Checked = dr["ISV"].ToString() == "1" ? true : false;

                    this.btnSave.CommandArgument = ButtonCommandType.Edit.ToString();
                    mdlPopup.Show();
                }
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = sender as LinkButton;
                var commandString = btn.CommandArgument;
                hfdpid.Value = commandString;
                DataTable dt = GetData();
                DataRow[] DRS = dt.Select("PID=" + commandString);
                DSNOTICE.TB_AD_NOTICERow row = null;
                if (DRS.Count() > 0)
                {
                    row = DRS[0] as DSNOTICE.TB_AD_NOTICERow;
                }
                int flag = 2;
                NoticeManager.EditNotice(row, flag);
                BindGrid();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this.Page, "删除失败");
                return;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DSNOTICE ds = new DSNOTICE();
                DSNOTICE.TB_AD_NOTICERow row = ds.TB_AD_NOTICE.NewRow() as DSNOTICE.TB_AD_NOTICERow;
                row.PID = int.Parse(hfdpid.Value);
                row.TITLE = txttitle.Text;
                row.ENTITLE = txtentitle.Text;
                row.CONTENT = txtcontent.Text;
                row.ENCONTENT = txtencontent.Text;
                row.ISV = this.ckbisv.Checked ? 1 : 0;
                int flag = btnSave.CommandArgument == ButtonCommandType.Edit.ToString() ? 3 : 1;
                NoticeManager.EditNotice(row, flag);
                BindGrid();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this.Page, "删除失败");
                return;
            }
        }
    }
}