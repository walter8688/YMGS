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
    [LeftMenuId(FunctionIdList.AssistantManagement.AssistantManagePage)]
    [TopMenuId(FunctionIdList.AssistantManagement.AssistantManageModule)]
    public partial class ParamZoneManage : BasePage
    {
        private const int RootID = 0;
        private static int rootZoneID;
        DSParamZone paramZoneDS = null;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetButtonStatus(new bool[] { false, false, false });
                SetTextBoxStatus(new bool[] { false, false, false });
                BindTreeView();
            }
        }

        /// <summary>
        /// 绑定TreeView
        /// </summary>
        private void BindTreeView()
        {
            paramZoneDS = ParamZoneManager.QueryParamZone();
            this.tvParamZone.Nodes.Clear();
            IntiTreeNode(this.tvParamZone.Nodes, RootID);
            this.tvParamZone.ExpandDepth = 1;
            this.tvParamZone.ExpandAll();
        }
        /// <summary>
        /// 初始化TreeView
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="parentID"></param>
        private void IntiTreeNode(TreeNodeCollection nodes,int parentID)
        {
            DataView paramZoneTemp = ParamZoneManager.QueryParamZoneByParentZoneID(paramZoneDS, parentID);
            if (paramZoneTemp == null)
                return;
            TreeNode node = null;
            foreach (DataRowView rv in paramZoneTemp)
            {
                if (parentID == RootID)
                {
                    rootZoneID = Convert.ToInt32(rv["ZONE_ID"].ToString());
                }
                node = new TreeNode();
                node.Value = rv["ZONE_ID"].ToString();
                node.Text = rv["ZONE_NAME"].ToString().Replace("<","&lt").Replace(">","&gt");
                nodes.Add(node);
                IntiTreeNode(nodes[nodes.Count - 1].ChildNodes, Convert.ToInt32(node.Value));
            }
        }

        /// <summary>
        /// 辅助检查paramZoneDS
        /// </summary>
        /// <param name="paramZoneDS"></param>
        /// <returns></returns>
        private bool CheckDSNull(DSParamZone paramZoneDS)
        {
            if (paramZoneDS == null)
                return false;
            if (paramZoneDS.Tables[0].Rows.Count == 0)
                return false;
            return true;
        }

        /// <summary>
        /// 页面权限ID
        /// </summary>
        protected override int Func_PageId
        {
            get
            {
                return FunctionIdList.AssistantManagement.AssistantManagePage;
            }
        }

        /// <summary>
        /// 页面操作权限
        /// </summary>
        private void InitPageActionAccess()
        {
            this.btnAddZone.Enabled = MySession.Accessable(FunctionIdList.AssistantManagement.AddZone);
            this.btnSaveZone.Enabled = MySession.Accessable(FunctionIdList.AssistantManagement.EditZone);
            this.btnDelZone.Enabled = MySession.Accessable(FunctionIdList.AssistantManagement.DeleteZone);
        }

        /// <summary>
        /// TreeView Node选中事件，更新数据维护区域数据&状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tvParamZone_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeNode node = (sender as TreeView).SelectedNode;
            this.txtZnoeID.Text = node.Value;
            this.txtZoneName.Text = node.Text.Replace("&lt", "<").Replace("&gt", ">");
            try
            {
                this.txtParentZoneName.Text = node.Parent.Text.Replace("&lt","<").Replace("&gt",">");
                this.txtParentZoneID.Text = node.Parent.Value;
            }
            catch
            {
                this.txtParentZoneName.Text = node.Text;
                this.txtParentZoneID.Text = RootID.ToString();
            }
            InitPageActionAccess();
            if (this.txtParentZoneID.Text == RootID.ToString())
            {
                this.btnDelZone.Enabled = this.btnSaveZone.Enabled = false;
                SetTextBoxStatus(new bool[] { false, false, true });

            }
            else
            {
                SetTextBoxStatus(new bool[] { false, true, true });
            }
        }

        /// <summary>
        /// 获取当前参数区域
        /// </summary>
        /// <returns></returns>
        private DSParamZone.TB_PARAM_ZONERow CurrentRow()
        {
            DSParamZone.TB_PARAM_ZONERow row = new DSParamZone().TB_PARAM_ZONE.NewTB_PARAM_ZONERow();
            row.ZONE_NAME = this.txtZoneName.Text.Trim();
            row.PARENT_ZONE_ID = Convert.ToInt32(this.txtParentZoneID.Text);
            row.ZONE_ID = Convert.ToInt32(this.txtZnoeID.Text);
            return row;
        }

        /// <summary>
        /// 新增ParamZone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddZone_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtChildZoneName.Text.Trim()))
            {
                PageHelper.ShowMessage(Page, CommonConstant.AssistantManagePage_AddParamZone_Msg);
            }
            else
            {
                DSParamZone.TB_PARAM_ZONERow row = CurrentRow();
                row.PARENT_ZONE_ID = Convert.ToInt32(this.txtZnoeID.Text);
                row.ZONE_NAME = this.txtChildZoneName.Text.Trim();
                ParamZoneManager.AddParamZone(row);
                this.txtChildZoneName.Text = "";
                BindTreeView();
            }
        }

        /// <summary>
        /// 删除ParamZone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelZone_Click(object sender, EventArgs e)
        {
            try
            {
                DSParamZone.TB_PARAM_ZONERow row = CurrentRow();
                ParamZoneManager.DelParamZone(row);
                BindTreeView();
                SetTextBoxValue(new string[] { "", "", "", "", "" });
                SetTextBoxStatus(new bool[] { false, false, false });
                SetButtonStatus(new bool[] { false, false, false });
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this, ex.Message);
            }
        }

        /// <summary>
        /// 编辑ParamZone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveZone_Click(object sender, EventArgs e)
        {
            DSParamZone.TB_PARAM_ZONERow row = CurrentRow();
            ParamZoneManager.UpdateParamZone(row);
            BindTreeView();
        }

        /// <summary>
        /// 设置TextBox值
        /// </summary>
        /// <param name="valueArr"></param>
        private void SetTextBoxValue(string[] valueArr)
        {
            this.txtParentZoneName.Text = valueArr[0];
            this.txtZoneName.Text = valueArr[1];
            this.txtChildZoneName.Text = valueArr[2];
            this.txtParentZoneID.Text = valueArr[3];
            this.txtZnoeID.Text = valueArr[4];
        }

        /// <summary>
        /// 设定TextBox控件状态
        /// </summary>
        /// <param name="status"></param>
        /// <param name="obj"></param>
        private void SetTextBoxStatus(bool[] statusArr)
        {
            this.txtParentZoneName.Enabled = statusArr[0];
            this.txtZoneName.Enabled = statusArr[1];
            this.txtChildZoneName.Enabled = statusArr[2];
        }
        /// <summary>
        /// 设定Button控件状态
        /// </summary>
        /// <param name="status"></param>
        private void SetButtonStatus(bool[] statusArr)
        {
            this.btnAddZone.Enabled = statusArr[0];
            this.btnDelZone.Enabled = statusArr[1];
            this.btnSaveZone.Enabled = statusArr[2];
        }
    }
}