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
using YMGS.Data.DataBase;

namespace YMGS.Manage.Web.AssistManage
{
    [LeftMenuId(FunctionIdList.AssistantManagement.HelperManagePage)]
    [TopMenuId(FunctionIdList.AssistantManagement.AssistantManageModule)]
    public partial class Helper : QueryBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(Helper), "AssistManage").AbsoluteUri;
        }
        protected override int Func_PageId
        {
            get
            {
                return FunctionIdList.AssistantManagement.HelperManagePage;
            }
        }
        protected override DataTable GetData()
        {
            DSHelper ds = HelperManager.QueryHelper();
                ViewState["helper"] = ds;
                return ds.TB_HELPER as DataTable;
        }
        protected DSHelper data
        {
            get
            {
                if (ViewState["helper"] == null)
                {
                    DSHelper ds = HelperManager.QueryHelper();
                    ViewState["helper"] = ds;
                    return ds;
                }
                return ViewState["helper"] as DSHelper;
            }
        }
        public void Loadddlpitem()
        {
            this.ddlpitem.DataSource = GetData();
            ddlpitem.DataBind();
            ddlpitem.Items.Insert(0, new ListItem("请选择...", "0"));
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
                Loadddlpitem();
                btnNew.Visible = MySession.Accessable(FunctionIdList.AssistantManagement.AddHelper);
                BindGrid();
            }
        }
        protected void gdvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string pitemid = e.Row.Cells[0].Text;
                if (pitemid == "0")
                    e.Row.Cells[0].Text = "";
                else
                e.Row.Cells[0].Text = data.TB_HELPER.Where(s => s.ITEMID.ToString() == pitemid).First().CNITEMNAME;

                LinkButton hlEdit = e.Row.FindControl("hlEdit") as LinkButton;
                LinkButton hlDelete = e.Row.FindControl("hlDelete") as LinkButton;
                hlEdit.Visible = MySession.Accessable(FunctionIdList.AssistantManagement.EditHelper);
                hlDelete.Visible = MySession.Accessable(FunctionIdList.AssistantManagement.EditHelper);
            }
        }

        public void ClearData()
        {
            hfdpid.Value = "0";
            this.ddlpitem.SelectedIndex = -1;
            this.txtcnitemname.Text = string.Empty;
            this.txtenitemname.Text = string.Empty;
            this.txtweblink.Text = string.Empty;
            this.txtenweblink.Text = string.Empty;
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            ClearData();
            if (sender is Button)
            {
                this.btnSave.CommandArgument = ButtonCommandType.Add.ToString();
                mdlPopup.Show();
            }
            if (sender is LinkButton)
            {
                LinkButton btn = sender as LinkButton;
                var commandString = btn.CommandArgument;
              DSHelper.TB_HELPERRow row=  data.TB_HELPER.Where(s => s.ITEMID.ToString() == commandString).First();

              hfdpid.Value = commandString;
              this.ddlpitem.SelectedValue = row.PITEMID.ToString();
              this.txtcnitemname.Text = row.CNITEMNAME;
              this.txtenitemname.Text = row.ENITEMNAME;
              this.txtweblink.Text = row.WEBLINK;
              this.txtenweblink.Text = row.ENWEBLINK;

                    this.btnSave.CommandArgument = ButtonCommandType.Edit.ToString();
                    mdlPopup.Show();
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {

                LinkButton btn = sender as LinkButton;
                var commandString = btn.CommandArgument;
                hfdpid.Value = commandString;
                DSHelper.TB_HELPERRow row = data.TB_HELPER.Where(s => s.ITEMID.ToString() == commandString).First();

                int flag = 2;
                HelperManager.EditHelper(row, flag);
                BindGrid();
                Loadddlpitem();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this.Page, ex.Message);
                return;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DSHelper ds = new DSHelper();
                DSHelper.TB_HELPERRow row = ds.TB_HELPER.NewRow() as DSHelper.TB_HELPERRow;
                row.ITEMID = int.Parse(hfdpid.Value);
                row.PITEMID = int.Parse(ddlpitem.SelectedValue);
                row.CNITEMNAME = txtcnitemname.Text;
                row.ENITEMNAME = txtenitemname.Text;
                row.WEBLINK = txtweblink.Text;
                row.ENWEBLINK = txtenweblink.Text;
                int flag = btnSave.CommandArgument == ButtonCommandType.Edit.ToString() ? 3 : 1;
                HelperManager.EditHelper(row, flag);
                BindGrid();
                Loadddlpitem();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this.Page,ex.Message);
                return;
            }
        }

    }
}