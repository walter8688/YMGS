using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Data.Common;
using System.Data;
using YMGS.Manage.Web.Common;
using YMGS.Business.SystemSetting;
using YMGS.Data.DataBase;

namespace YMGS.Manage.Web.SystemSetting
{
    [LeftMenuId(FunctionIdList.SystemManagement.RoleManagePage)]
    [TopMenuId(FunctionIdList.SystemManagement.SystemManageModule)]
    public partial class RoleManageFrm : QueryBasePage
    {
        protected override int Func_PageId
        {
            get
            {
                return FunctionIdList.SystemManagement.RoleManagePage;
            }
        }
        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(RoleManageFrm), "SystemSetting").AbsoluteUri;
        }
        protected override DataTable GetData()
        {

            DSSystemRole ds = new DSSystemRole();
            DSSystemRole.TB_SYSTEM_ROLERow srrow = ds.TB_SYSTEM_ROLE.NewRow() as DSSystemRole.TB_SYSTEM_ROLERow;
            srrow.ROLE_ID = 0;
            srrow.ROLE_NAME = txtRoleName.Text.Trim();
            return SystemSettingManager.QueryRole(srrow).TB_SYSTEM_ROLE;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!IsPostBack)
            {
                tvFunc.CollapseAll();
                DSSystemFunc ds = new DSSystemFunc();
                DSSystemFunc.TB_SYSTEM_FUNCRow sfrow = ds.TB_SYSTEM_FUNC.NewRow() as DSSystemFunc.TB_SYSTEM_FUNCRow;
                sfrow.FUNC_ID = 0;
                sfrow.PARENT_FUNC_ID = 0;
                BindTreeView(SystemSettingManager.QueryFunc(sfrow));
                    btnNew.Visible = MySession.Accessable(FunctionIdList.SystemManagement.AddRole);
                    BindGrid();
            }
        }

        protected void BindTreeView(DSSystemFunc dssf)
        {
            foreach (DSSystemFunc.TB_SYSTEM_FUNCRow sfrow in dssf.TB_SYSTEM_FUNC.Where(s => s.IsPARENT_FUNC_IDNull()))
            {
                TreeNode tn = new TreeNode();
                tn.CollapseAll();

                tn.SelectAction = TreeNodeSelectAction.None;
                tn.Text = sfrow.FUNC_NAME;
                tn.Value = sfrow.FUNC_ID.ToString();
                GetChilds(tn, dssf);
                tvFunc.Nodes.Add(tn);
            }
        }
        private void GetChilds(TreeNode tn, DSSystemFunc dssf)
        {
            foreach (DSSystemFunc.TB_SYSTEM_FUNCRow item in dssf.TB_SYSTEM_FUNC.Where(s => s.IsPARENT_FUNC_IDNull() ? false : s.PARENT_FUNC_ID.ToString() == tn.Value))
            {
                TreeNode child = new TreeNode();
                child.SelectAction = TreeNodeSelectAction.None;
                child.CollapseAll();
                child.Value = item.FUNC_ID.ToString();
                child.Text = item.FUNC_NAME;
                tn.ChildNodes.Add(child);
                GetChilds(child, dssf);
            }
        }


        protected void btnNew_Click(object sender, EventArgs e)
        {
            cleartree(tvFunc.Nodes);
            if (sender is Button)
            {
                txtRoleName.Text = "";
                ROLE_NAME.Text = roledesc.Text = "";
                ROLE_NAME.Enabled = roledesc.Enabled = true;
                this.btnSave.CommandArgument = ButtonCommandType.Add.ToString();
                mdlPopup.Show();
            }
            else if (sender is LinkButton)
            {
                LinkButton btndel = sender as LinkButton;
                if (btndel.CommandName == "del")
                {
                    try
                    {
                        string status = "del";
                        string treevalues = "";
                        int userid = MySession.CurrentUser.ACCOUNT[0].USER_ID;
                        this.txtroleID.Text = btndel.CommandArgument;
                        SystemSettingManager.DelRoleFunc(status, this.txtroleID.Text,"", "", userid, userid, treevalues);
                        BindGrid();
                    }
                    catch (Exception ex)
                    {
                        PageHelper.ShowMessage(this.Page,ex.Message);
                        return;
                    }
                }
                else
                {
                    var commandString = (sender as LinkButton).CommandArgument;
                    this.txtroleID.Text = commandString;
                    if (commandString == "2" || commandString == "3" || commandString == "4")
                        ROLE_NAME.Enabled = roledesc.Enabled = false;
                    else
                        ROLE_NAME.Enabled = roledesc.Enabled = true;
                    this.ROLE_NAME.Text = GetName(commandString);
                    roledesc.Text = GetDesc(commandString);
                    this.btnSave.CommandArgument = ButtonCommandType.Edit.ToString();
                    DSRoleFuncMap.TB_ROLE_FUNC_MAPDataTable dt = RoleFuncMapManager.QueryData(int.Parse(commandString)).TB_ROLE_FUNC_MAP;
                    setTreeView(dt);
                    mdlPopup.Show();
                }
            }


        }

        private string GetName(string id)
        {
            foreach (GridViewRow item in gdvMain.Rows)
            {
                if (item.Cells[0].Text == id)
                    return item.Cells[1].Text.Replace("&nbsp;", "");
            }
            return "";
        }
        private string GetDesc(string id)
        {
            foreach (GridViewRow item in gdvMain.Rows)
            {
                if (item.Cells[0].Text == id)
                    return item.Cells[2].Text.Replace("&nbsp;", "");
            }
            return "";
        }

        private void cleartree(TreeNodeCollection nodes)
        {
            foreach (TreeNode item in nodes)
            {
                item.Checked = false;
                if (item.ChildNodes.Count > 0)
                    cleartree(item.ChildNodes);
            }
        }

        /// <summary>
        /// Grid绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gdvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool editAccess = MySession.Accessable(FunctionIdList.SystemManagement.EditRole);
                bool delAccess = MySession.Accessable(FunctionIdList.SystemManagement.DeleteRole);
                var btnEdit = e.Row.FindControl("btnEdit") as LinkButton;
                var btnDelete = e.Row.FindControl("btnDelete") as LinkButton;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //权限处理
                    btnEdit.Visible = editAccess;
                    btnDelete.Visible = delAccess;
                    if (delAccess)
                        btnDelete.Attributes.Add("onclick", "javascript:return confirm('是否确认删除？');");
                }
                int result = 0;
                int.TryParse(e.Row.Cells[1].Text.Replace("级总代理",""),out result);
                var isGeneralAgent = (result > 0 && result < 6) ? true : false;
                var roleId = Convert.ToInt32(e.Row.Cells[0].Text);
                if (roleId == 2 || roleId == 3 || roleId == 4 || isGeneralAgent)
                    btnDelete.Visible = false;
                if (roleId == 1)
                    btnDelete.Visible = btnEdit.Visible = false;
                
            }
        }
        private void setTreeView(DSRoleFuncMap.TB_ROLE_FUNC_MAPDataTable dt)
        {
            foreach (int item in dt.Select(s => s.FUNC_ID))
            {
                settreeValue(item.ToString(), tvFunc.Nodes);
            }
        }

        private void settreeValue(string tvalue, TreeNodeCollection nodes)
        {
            foreach (TreeNode item in nodes)
            {
                if (item.Value == tvalue)
                {
                    item.Checked = true;
                }
                if (item.ChildNodes.Count > 0)
                    settreeValue(tvalue, item.ChildNodes);
            }
        }
        private string GetTreeValue()
        {
            string valuelist = "";
            Getvalue(tvFunc.Nodes, ref valuelist);
            return valuelist;
        }

        private void Getvalue(TreeNodeCollection nodes, ref string valuelist)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                    valuelist += node.Value + ",";
                if (node.ChildNodes.Count > 0)
                    Getvalue(node.ChildNodes, ref valuelist);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int userid = MySession.CurrentUser.ACCOUNT[0].USER_ID;
                int result = 0;
                string treevalues = GetTreeValue();
                string status = "";
                string opStatus = (sender as Button).CommandArgument;
                if (opStatus == ButtonCommandType.Add.ToString())
                {
                    status = "add";
                    result = SystemSettingManager.AddRoleFunc(status, "0", ROLE_NAME.Text, roledesc.Text, userid, userid, treevalues);
                }

                if (opStatus == ButtonCommandType.Edit.ToString())
                {
                    status = "edit";
                    result = SystemSettingManager.AddRoleFunc(status, this.txtroleID.Text, ROLE_NAME.Text, roledesc.Text, userid, userid, treevalues);
                }
                BindGrid();
                clearControl();
            }
            catch(Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
                mdlPopup.Show();
            }
        }
        private void clearControl()
        { 
            cleartree(tvFunc.Nodes);
            this.txtroleID.Text=ROLE_NAME.Text=roledesc.Text="";
        }     

    }
}