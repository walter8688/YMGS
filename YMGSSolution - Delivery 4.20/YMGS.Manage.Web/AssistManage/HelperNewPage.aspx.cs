using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Data;
using YMGS.Data.DataBase;
using YMGS.Business.AssistManage;
using YMGS.Manage.Web.Common;
using System.Data;
using YMGS.Data.Common;

namespace YMGS.Manage.Web.AssistManage
{
    [LeftMenuId(FunctionIdList.AssistantManagement.HelperManagePage)]
    [TopMenuId(FunctionIdList.AssistantManagement.AssistantManageModule)]
    public partial class HelperNewPage : BasePage
    {

        private const int RootID = -1;
        DSHelper helperDataDS = null;
        private int LevelNO = 0;

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetButtonStatus(new bool[] { false, false, false });
                SetTextBoxStatus(new bool[] { false, false, false, false, false, false, false, false, false, false, false, false });
                BindTreeView();
                BindDDL();
            }
        }

        /// <summary>
        /// 绑定TreeView
        /// </summary>
        private void BindTreeView()
        {
            helperDataDS = HelperManager.QueryHelper();
            this.tvHelperData.Nodes.Clear();
            IntiTreeNode(this.tvHelperData.Nodes, RootID);
            this.tvHelperData.ExpandDepth = 1;
            this.tvHelperData.ExpandAll();
        }

        private void BindDDL()
        {
            var rulesLst = CommonFunction.QueryAllRulesTypes();
            drpRules.DataValueField = "RulesID";
            drpRules.DataTextField = "RulesName";
            drpRules.DataSource = rulesLst;
            drpRules.DataBind();
        }

        /// <summary>
        /// 初始化TreeView
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="parentID"></param>
        private void IntiTreeNode(TreeNodeCollection nodes, int pItemID)
        {
            DataView paramZoneTemp = HelperManager.QueryHelperDataByParentItemID(helperDataDS, pItemID);
            if (paramZoneTemp == null)
                return;
            TreeNode node = null;
            foreach (DataRowView rv in paramZoneTemp)
            {
                node = new TreeNode();
                node.Value = rv["ITEMID"].ToString();
                node.Text = rv["CNITEMNAME"].ToString().Replace("<", "&lt").Replace(">", "&gt");
                nodes.Add(node);
                IntiTreeNode(nodes[nodes.Count - 1].ChildNodes, Convert.ToInt32(node.Value));
            }
        }

        /// <summary>
        /// 页面操作权限
        /// </summary>
        private void InitPageActionAccess()
        {
            this.btnAddCatalog.Enabled = MySession.Accessable(FunctionIdList.AssistantManagement.AddHelper);
            this.btnSaveCatalog.Enabled = MySession.Accessable(FunctionIdList.AssistantManagement.EditHelper);
            this.btnDelCatalog.Enabled = MySession.Accessable(FunctionIdList.AssistantManagement.DeleteHelper);
        }


        /// <summary>
        /// TreeView Node选中事件，更新数据维护区域数据&状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvHelperData_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeNode node = (sender as TreeView).SelectedNode;
            txtItemID.Text = node.Value;
            txtItemNameCN.Text = node.Text.Replace("&lt", "<").Replace("&gt", ">");
            try
            {
                this.txtPItemNameCN.Text = node.Parent.Text.Replace("&lt", "<").Replace("&gt", ">");
                this.txtPItemID.Text = node.Parent.Value;
                int CItemID = Convert.ToInt32(node.Value);
                int PItemID = Convert.ToInt32(node.Parent.Value);
                DSHelper.TB_HELPERRow ChildRow = GetDataRowByID(CItemID);
                DSHelper.TB_HELPERRow ParentRow = GetDataRowByID(PItemID);
                SetTextBoxValue(new string[] { ParentRow.CNITEMNAME, ParentRow.ENITEMNAME, ChildRow.CNITEMNAME, ChildRow.ENITEMNAME, ChildRow.IsWEBLINKNull() ? "" : ChildRow.WEBLINK, ChildRow.IsENWEBLINKNull() ? "" : ChildRow.ENWEBLINK, "", "", "", "", ChildRow.IsOrderNONull() ? "" : ChildRow.OrderNO, ChildRow.IsRulesIDNull() ? "" : ChildRow.RulesID });
            }
            catch
            {
                this.txtPItemNameCN.Text = node.Text;
                this.txtPItemID.Text = RootID.ToString();
                int itemID = Convert.ToInt32(node.Value);
                DSHelper.TB_HELPERRow row = GetDataRowByID(itemID);
                SetTextBoxValue(new string[] { row.CNITEMNAME, row.ENITEMNAME, row.CNITEMNAME, row.ENITEMNAME, row.IsWEBLINKNull() ? "" : row.WEBLINK, row.IsENWEBLINKNull() ? "" : row.ENWEBLINK, "", "", "", "", row.IsOrderNONull() ? "" : row.OrderNO, row.IsRulesIDNull() ? "" : row.RulesID });
            }
            //页面操作权限
            InitPageActionAccess();
            //树的深度
            int tvDepth = tvHelperData.SelectedNode.Depth;
            LevelNO = tvDepth;
            if (tvDepth == 0)
            {//根节点，只能增加子节点
                SetTextBoxStatus(new bool[] { false, false, false, false, false, false, true, true, true, true, false, false });
                SetButtonStatus(new bool[] { true, false, false });
            }
            else if (tvDepth == Convert.ToInt32(SystemConfigManager.HelperCenterDeep))
            {
                SetTextBoxStatus(new bool[] { false, false, true, true, true, true, false, false, false, false, true, true });
                SetButtonStatus(new bool[] { false, true, true });
            }
            else
            {
                SetTextBoxStatus(new bool[] { false, false, true, true, true, true, true, true, true, true, true, true });
                SetButtonStatus(new bool[] { true, true, true });
            }
        }

        protected void btnAddCatalog_Click(object sender, EventArgs e)
        {
            try
            {
                DSHelper.TB_HELPERRow row = CurrentAddChildRow();
                int flag = 1;
                HelperManager.EditHelper(row, flag);
                BindTreeView();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
            }
        }

        protected void btnDelCatalog_Click(object sender, EventArgs e)
        {
            try
            {
                DSHelper.TB_HELPERRow row = CurrentSaveRow();
                int flag = 2;
                HelperManager.EditHelper(row, flag);
                BindTreeView();
                SetTextBoxValue(new string[] { "", "", "", "", "", "", "", "", "", "", "", "" });
                SetButtonStatus(new bool[] { false, false, false });
                SetTextBoxStatus(new bool[] { false, false, false, false, false, false, false, false, false, false, false, false });
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
            }
        }

        protected void btnSaveCatalog_Click(object sender, EventArgs e)
        {
            try
            {
                DSHelper.TB_HELPERRow row = CurrentSaveRow();
                //if (row.RulesID != "")
                //{
                //    helperDataDS = HelperManager.QueryHelper();
                //    foreach (DataRow dr in helperDataDS.Tables[0].Rows)
                //    {
                //        string rulesDR = dr["RulesID"] == DBNull.Value ? "" : dr["RulesID"].ToString();
                //        int itemID = Convert.ToInt32(dr["ITEMID"]);
                //        if (row.RulesID == rulesDR && row.ITEMID != itemID)
                //        {
                //            PageHelper.ShowMessage(this, "改规则已经存在，请重新选择！");
                //            return;
                //        }
                //    }
                //}
                int flag = 3;
                HelperManager.EditHelper(row, flag);
                BindTreeView();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
            }
        }

        /// <summary>
        /// 根据ItemID返回datarow
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        private DSHelper.TB_HELPERRow GetDataRowByID(int ItemID)
        {
            helperDataDS = HelperManager.QueryHelper();
            DSHelper.TB_HELPERRow row = helperDataDS.TB_HELPER.FindByITEMID(ItemID);
            return row;
        }

        /// <summary>
        /// 获取当前参数
        /// </summary>
        /// <returns></returns>
        private DSHelper.TB_HELPERRow CurrentSaveRow()
        {
            DSHelper.TB_HELPERRow row = new DSHelper().TB_HELPER.NewTB_HELPERRow();
            row.ITEMID = Convert.ToInt32(txtItemID.Text);
            row.PITEMID = Convert.ToInt32(txtPItemID.Text);
            row.CNITEMNAME = txtItemNameCN.Text;
            row.ENITEMNAME = txtItemNameEN.Text;
            row.WEBLINK = txtItemLinkNameCN.Text;
            row.ENWEBLINK = txtItemLinkNameEN.Text;
            row.OrderNO = txtOrderNO.Text;
            row.RulesID = drpRules.SelectedValue;
            //无需进行处理
            row.LevelNO = 0;
            return row;
        }

        /// <summary>
        /// 获取当前参数
        /// </summary>
        /// <returns></returns>
        private DSHelper.TB_HELPERRow CurrentAddChildRow()
        {
            DSHelper.TB_HELPERRow row = new DSHelper().TB_HELPER.NewTB_HELPERRow();
            row.ITEMID = Convert.ToInt32(txtItemID.Text);
            row.PITEMID = Convert.ToInt32(txtItemID.Text);
            row.CNITEMNAME = txtChildItemNameCN.Text;
            row.ENITEMNAME = txtChildItemNameEN.Text;
            row.WEBLINK = txtChildItemLinkNameCN.Text;
            row.ENWEBLINK = txtChildItemLinkNameEN.Text;
            row.OrderNO = "";
            row.RulesID = "";
            row.LevelNO = tvHelperData.SelectedNode.Depth + 1;
            return row;
        }

        /// <summary>
        /// 设置TextBox值
        /// </summary>
        /// <param name="valueArr"></param>
        private void SetTextBoxValue(string[] valueArr)
        {
            txtPItemNameCN.Text = valueArr[0];
            txtPItemNameEN.Text = valueArr[1];
            txtItemNameCN.Text = valueArr[2];
            txtItemNameEN.Text = valueArr[3];
            txtItemLinkNameCN.Text = valueArr[4];
            txtItemLinkNameEN.Text = valueArr[5];
            txtChildItemNameCN.Text = valueArr[6];
            txtChildItemNameEN.Text = valueArr[7];
            txtChildItemLinkNameCN.Text = valueArr[8];
            txtChildItemLinkNameEN.Text = valueArr[9];
            txtOrderNO.Text = valueArr[10];
            //重新绑定DDL
            drpRules.SelectedValue = valueArr[11];
            BindDDL();
            //drpRules.Enabled = valueArr[11];
        }

        /// <summary>
        /// 设定TextBox控件状态
        /// </summary>
        /// <param name="status"></param>
        /// <param name="obj"></param>
        private void SetTextBoxStatus(bool[] statusArr)
        {
            txtPItemNameCN.Enabled = statusArr[0];
            txtPItemNameEN.Enabled = statusArr[1];
            txtItemNameCN.Enabled = statusArr[2];
            txtItemNameEN.Enabled = statusArr[3];
            txtItemLinkNameCN.Enabled = statusArr[4];
            txtItemLinkNameEN.Enabled = statusArr[5];
            txtChildItemNameCN.Enabled = statusArr[6];
            txtChildItemNameEN.Enabled = statusArr[7];
            txtChildItemLinkNameCN.Enabled = statusArr[8];
            txtChildItemLinkNameEN.Enabled = statusArr[9];
            txtOrderNO.Enabled = statusArr[10];
            drpRules.Enabled = statusArr[11];
        }

        /// <summary>
        /// 设定Button控件状态
        /// </summary>
        /// <param name="status"></param>
        private void SetButtonStatus(bool[] statusArr)
        {
            this.btnAddCatalog.Enabled = statusArr[0];
            this.btnDelCatalog.Enabled = statusArr[1];
            this.btnSaveCatalog.Enabled = statusArr[2];
        }
    }
}