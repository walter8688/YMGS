using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using YMGS.Framework;
using YMGS.Data.Common;
using YMGS.Manage.Web.Common;
using YMGS.Data.Presentation;
using YMGS.Business.SystemSetting;
using YMGS.Data.DataBase;
using YMGS.Business.AssistManage;

namespace YMGS.Manage.Web.SystemSetting
{
    [TopMenuId(FunctionIdList.SystemManagement.SystemManageModule)]
    [LeftMenuId(FunctionIdList.SystemManagement.VCardManagePage)]
    public partial class VCardManagePage : BasePage
    {
        private const string _sheetName = "V网卡列表";
        private const string _fileName = "V网卡列表";

        public string[] ColumnNames { get { return new string[] { "V网卡卡号", "V网卡激活码", "V网卡面值",
            "V网卡状态", "生成日期", "激活人", "激活日期"};
        }
        }

        protected override int Func_PageId
        {
            get
            {
                return FunctionIdList.SystemManagement.VCardManagePage;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            pageNavigator.PageIndexChanged += new EventHandler(pageNavigator_PageIndexChanged);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPageData();
                LoadGridData();
            }
        }

        private void LoadPageData()
        {
            //var VCardFaceValueList = CommonFunction.QueryAllVCardFaceValueInfo();
            var param = new DSParamParam.TB_PARAM_PARAMDataTable().NewTB_PARAM_PARAMRow();
            param.PARAM_TYPE =  6;
            param.PARAM_NAME = string.Empty;
            var VCardFaceValueList = ParamParamManager.QueryParam(param);
            PageHelper.BindListControlData(DrpVCardFaceValue, VCardFaceValueList, "PARAM_NAME", "PARAM_NAME", true);
            PageHelper.BindListControlData(PopDrpVCardFaceValue, VCardFaceValueList, "PARAM_NAME", "PARAM_NAME", false);
            PageHelper.BindListControlData(DrpVCardStatus, CommonFunction.QueryAllVCardStatusInfo(), "VCardStatusText", "VCardStatusID", true);
        }

        private void LoadGridData()
        {
            var VCardStatus = Convert.ToInt32(DrpVCardStatus.SelectedValue);
            var VCardFaceValue = Convert.ToInt32(DrpVCardFaceValue.SelectedValue);
            DateTime sDate, eDate;
            if (calStartDate.Value.HasValue)
                sDate = calStartDate.Value.Value;
            else
                sDate = DateTime.MinValue;
            if (calEndDate.Value.HasValue)
                eDate = calEndDate.Value.Value.AddDays(1);
            else
                eDate = DateTime.MaxValue;
            var VCardDS = VCardManager.QueryAllVCardInfo(VCardFaceValue, VCardStatus, sDate, eDate);
            pageNavigator.databinds(VCardDS.TB_VCARD_DETAIL, gdvVCard);
        }

        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void pageNavigator_PageIndexChanged(object sender, EventArgs e)
        {
            LoadGridData();
        }

        protected void BtnGenerateVCard_Click(object sender, EventArgs e)
        {
            if(PopDrpVCardFaceValue.Items.Count > 0)
                PopDrpVCardFaceValue.SelectedIndex = 0;
            txtVCardNums.Text = string.Empty;
            mdlPopup.Show();
        }

        protected void gdvVCard_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var bindRow = (DSVCardDetail.TB_VCARD_DETAILRow)((DataRowView)e.Row.DataItem).Row;
                var lblVCardNo = e.Row.FindControl("lblVCardNo") as Label;
                var lblVCardActivateNo = e.Row.FindControl("lblVCardActivateNo") as Label;
                var lblVCardStatus = e.Row.FindControl("lblVCardStatus") as Label;
                var lblVCardGenerateDate = e.Row.FindControl("lblVCardGenerateDate") as Label;
                var lblActivateDate = e.Row.FindControl("lblActivateDate") as Label;
                lblVCardNo.Text = EncryptManager.DESDeCrypt(bindRow.VCARD_NO);
                lblVCardActivateNo.Text = EncryptManager.DESDeCrypt(bindRow.VCARD_ACTIVATE_NO);
                lblVCardStatus.Text = (VCardStatusEnum)bindRow.VCARD_STATUS == VCardStatusEnum.UnActivated ? "未激活" : ((VCardStatusEnum)bindRow.VCARD_STATUS == VCardStatusEnum.Activated ? "已激活" : "已失效");
                lblVCardGenerateDate.Text = bindRow.CREATE_DATE.ToString();
                lblActivateDate.Text = bindRow.ACTIVATE_DATE == null ? string.Empty : bindRow.ACTIVATE_DATE.ToString();
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var VCardFaceValue = Convert.ToInt32(PopDrpVCardFaceValue.SelectedValue);
                var VCardGenerateNums = Convert.ToInt32(txtVCardNums.Text);
                var successNums = VCardManager.GenerateVCards(VCardFaceValue, MySession.CurrentUser.ACCOUNT[0].USER_ID, VCardGenerateNums);
                LoadGridData();
                PageHelper.ShowMessage(Page, string.Format("成功生成{0}张V网卡",successNums));
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(Page,ex.Message);
            }
        }

        private DataTable GenerateExportData(DSVCardDetail dsVacard)
        {
            DataTable tempDT = new DataTable();
            tempDT.Columns.Add("VCardNum");
            tempDT.Columns.Add("VCardActivateNum");
            tempDT.Columns.Add("VCardFaceValue");
            tempDT.Columns.Add("VCardStatus");
            tempDT.Columns.Add("VCardGenerateDate");
            tempDT.Columns.Add("VCardActivateUser");
            tempDT.Columns.Add("VCardActivateDate");

            Array.ForEach(dsVacard.TB_VCARD_DETAIL.ToArray(),r=>
                {
                    DataRow tempRow = tempDT.NewRow();
                    tempRow["VCardNum"] =  EncryptManager.DESDeCrypt(r.VCARD_NO);
                    tempRow["VCardActivateNum"] = EncryptManager.DESDeCrypt(r.VCARD_NO);
                    tempRow["VCardFaceValue"] = r.VCARD_FACE_VALUE;
                    tempRow["VCardStatus"] = (VCardStatusEnum)r.VCARD_STATUS == VCardStatusEnum.UnActivated ? "未激活" : ((VCardStatusEnum)r.VCARD_STATUS == VCardStatusEnum.Activated ? "已激活" : "已失效");
                    tempRow["VCardGenerateDate"] = UtilityHelper.DateToStr(r.CREATE_DATE);
                    tempRow["VCardActivateUser"] = r.LOGIN_NAME;
                    tempRow["VCardActivateDate"] = r.ACTIVATE_DATE == null ? string.Empty : UtilityHelper.DateToStr(r.ACTIVATE_DATE);
                    tempDT.Rows.Add(tempRow);
                });

            return tempDT;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var VCardStatus = Convert.ToInt32(DrpVCardStatus.SelectedValue);
                var VCardFaceValue = Convert.ToInt32(DrpVCardFaceValue.SelectedValue);
                DateTime sDate, eDate;
                if (calStartDate.Value.HasValue)
                    sDate = calStartDate.Value.Value;
                else
                    sDate = DateTime.MinValue;
                if (calEndDate.Value.HasValue)
                    eDate = calEndDate.Value.Value.AddDays(1);
                else
                    eDate = DateTime.MaxValue;
                var queryDT = GenerateExportData(VCardManager.QueryAllVCardInfo(VCardFaceValue, VCardStatus, sDate, eDate));

                ExcelHelper.ExportDataToExcel(queryDT, _sheetName, ColumnNames, _fileName);
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(Page, ex.Message);
            }
        }
    }
}