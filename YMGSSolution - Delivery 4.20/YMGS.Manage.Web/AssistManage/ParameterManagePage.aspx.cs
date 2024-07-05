using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Data.Presentation;
using YMGS.Data.DataBase;
using YMGS.Business.AssistManage;
using YMGS.Data.Common;
using YMGS.Manage.Web.Common;
using YMGS.Business.SystemSetting;


namespace YMGS.Manage.Web.AssistManage
{
    [LeftMenuId(FunctionIdList.AssistantManagement.ParameterManagePage)]
    [TopMenuId(FunctionIdList.AssistantManagement.AssistantManageModule)]
    public partial class ParameterManagePage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitParamTypeData();
                BindGridData();
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitPageActionAccess();
            pageNavigator.PageIndexChanged += new EventHandler(pageNavigator_PageIndexChanged);            
        }

        #region 页面操作权限
        protected override int Func_PageId
        {
            get
            {
                return FunctionIdList.AssistantManagement.ParameterManagePage;
            }
        }

        private void InitPageActionAccess()
        {
            this.btnAddParam.Visible = MySession.Accessable(FunctionIdList.AssistantManagement.ParameterManage);
        }
        #endregion

        #region 页面数据
        /// <summary>
        /// 加载参数类型
        /// </summary>
        private void InitParamTypeData()
        {
            this.drpParamType.Items.Clear();
            this.drpParamType.DataSource = this.popupDrpParamType.DataSource =QueryParamType();
            this.drpParamType.DataTextField = this.popupDrpParamType.DataTextField = CommonConstant.ParameterManagePage_ParamType_Name;
            this.drpParamType.DataValueField = this.popupDrpParamType.DataValueField = CommonConstant.ParameterManagePage_ParamType_ID;
            this.drpParamType.DataBind();
            this.popupDrpParamType.DataBind();
            ListItem item = new ListItem("",CommonConstant.PageErrorCode.ToString());
            this.drpParamType.Items.Insert(0, item);
        }

        /// <summary>
        /// 绑定Grid数据
        /// </summary>
        private void BindGridData()
        {
            DSParameter paramDS = ParamParamManager.QueryParam(GetQueryParamRow());
            pageNavigator.databinds(paramDS.Tables[0], this.gdvMain);
        }
        /// <summary>
        /// 获取系统参数类型DataSet
        /// </summary>
        /// <returns></returns>
        private DSParamType QueryParamType()
        {
            return ParamParamManager.QueryParamType(DataHandlerEnum.Query);
        }

        /// <summary>
        /// 获取当前参数
        /// </summary>
        /// <returns></returns>
        private DSParamParam.TB_PARAM_PARAMRow GetNoQueryParamRow()
        {
            DSParamParam.TB_PARAM_PARAMRow row = new DSParamParam.TB_PARAM_PARAMDataTable().NewTB_PARAM_PARAMRow();
            row.PARAM_TYPE = Convert.ToInt32(this.popupDrpParamType.SelectedValue);
            row.PARAM_NAME = this.popupTxtParamName.Text.Trim();
            row.IS_USE = this.popupCkcInUse.Checked ? (int)ParamInUse.InUse : (int)ParamInUse.NotInUse;
            row.CREATE_USER = MySession.CurrentUser.ACCOUNT[0].USER_ID;
            row.LAST_UPDATE_USER = MySession.CurrentUser.ACCOUNT[0].USER_ID;
            return row;
        }

        private DSParamParam.TB_PARAM_PARAMRow GetQueryParamRow()
        {
            DSParamParam.TB_PARAM_PARAMRow row = new DSParamParam.TB_PARAM_PARAMDataTable().NewTB_PARAM_PARAMRow();
            row.PARAM_TYPE = Convert.ToInt32(this.drpParamType.SelectedValue);
            row.PARAM_NAME = this.txtParamName.Text.Trim();
            return row;
        }
        #endregion

        #region 页面事件
        /// <summary>
        /// 新增参数弹出Popup窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddParam_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                this.popupTxtParamName.Text = "";
                this.popupDrpParamType.Enabled = true;
                this.popupDrpParamType.SelectedIndex = 0;
                this.popupCkcInUse.Checked = true;
                this.btnSave.CommandArgument = ButtonCommandType.Add.ToString();
            }
            else if (sender is LinkButton)
            {
                var commandString = (sender as LinkButton).CommandArgument.Split(',');
                this.popupDrpParamType.SelectedValue = commandString[2];
                this.popupDrpParamType.Enabled = false;
                this.popupTxtParamName.Text = commandString[0];
                this.popupCkcInUse.Checked = commandString[1] == CommonConstant.ParameterManagePage_InUse ? true : false;
                this.txtHiddenParamID.Text = commandString[3];
                this.btnSave.CommandArgument = ButtonCommandType.Edit.ToString();
            }
            mdlPopup.Show();
        }

        /// <summary>
        /// 保存参数数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DSParamParam.TB_PARAM_PARAMRow row = GetNoQueryParamRow();
            if ((sender as Button).CommandArgument == ButtonCommandType.Add.ToString())
            {
                ParamParamManager.AddParam(row);
            }
            else if ((sender as Button).CommandArgument == ButtonCommandType.Edit.ToString())
            {
                row.PARAM_ID = Convert.ToInt32(this.txtHiddenParamID.Text);
                ParamParamManager.UpdateParam(row);
            }
            //初始化Pop页面状态
            this.popupDrpParamType.SelectedIndex = 0;
            this.popupDrpParamType.Enabled = true;
            this.popupTxtParamName.Text = "";
            this.popupCkcInUse.Checked = true;
            BindGridData();
        }

        /// <summary>
        /// 查询参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQueryParam_Click(object sender, EventArgs e)
        {
            BindGridData();
        }

        /// <summary>
        /// Grid绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gdvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            bool editAccess = MySession.Accessable(FunctionIdList.AssistantManagement.ParameterManage);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //绑定数据
                (e.Row.FindControl("hlUp") as LinkButton).CommandArgument = (sender as GridView).DataKeys[e.Row.RowIndex].Value.ToString();
                (e.Row.FindControl("hlDown") as LinkButton).CommandArgument = (sender as GridView).DataKeys[e.Row.RowIndex].Value.ToString();
                (e.Row.FindControl("hlDelete") as LinkButton).CommandArgument = (sender as GridView).DataKeys[e.Row.RowIndex].Value.ToString();
                (e.Row.FindControl("hlEdit") as LinkButton).CommandArgument = string.Format("{0},{1},{2},{3}", e.Row.Cells[1].Text, e.Row.Cells[2].Text, (e.Row.FindControl("lblParamType") as Label).Text, (sender as GridView).DataKeys[e.Row.RowIndex].Value.ToString());
                //权限处理
                (e.Row.FindControl("hlDelete") as LinkButton).Visible = editAccess;
                (e.Row.FindControl("hlDown") as LinkButton).Visible = editAccess;
                (e.Row.FindControl("hlUp") as LinkButton).Visible = editAccess;
            }
        }

        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnParamOrderUp_Click(object sender, EventArgs e)
        {
            int paramID = Convert.ToInt32((sender as LinkButton).CommandArgument);
            ParamParamManager.OrderParam(paramID, OrderAction.OrderUp);
            BindGridData();
        }
        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnParamOrderDown_Click(object sender, EventArgs e)
        {
            int paramID = Convert.ToInt32((sender as LinkButton).CommandArgument);
            ParamParamManager.OrderParam(paramID, OrderAction.OrderDown);
            BindGridData();
        }
        /// <summary>
        /// 删除系统参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnParamDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int paramID = Convert.ToInt32((sender as LinkButton).CommandArgument);
                DSParamParam.TB_PARAM_PARAMRow row = new DSParamParam.TB_PARAM_PARAMDataTable().NewTB_PARAM_PARAMRow();
                row.PARAM_ID = paramID;
                ParamParamManager.DelParam(row);
                BindGridData();
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this.Page, ex.Message);
            }
        }

        protected void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            BindGridData();
        }
        #endregion
    }
}