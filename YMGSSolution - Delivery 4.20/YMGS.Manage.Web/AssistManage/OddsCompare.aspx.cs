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
using YMGS.Business.GameMarket;
using YMGS.Data.DataBase;

namespace YMGS.Manage.Web.AssistManage
{
    [LeftMenuId(FunctionIdList.AssistantManagement.OddsManagePage)]
    [TopMenuId(FunctionIdList.AssistantManagement.AssistantManageModule)]
    public partial class OddsCompare : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(OddsCompare), "AssistManage").AbsoluteUri;
        }
        protected override int Func_PageId
        {
            get
            {
                return FunctionIdList.AssistantManagement.OddsManagePage;
            }
        }
        protected IEnumerable<DSODDSCOMPARE.TB_ODDS_COMPARERow> GetData()
        {
            return OddsCompareManager.QueryOddsCompare2().TB_ODDS_COMPARE.Where(s => s.MATCHNAME.Contains(txtmatchName.Text.Trim()));
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
                LoadMatchlist();
                btnNew.Visible = MySession.Accessable(FunctionIdList.AssistantManagement.AddOdds);
                 PageNavigator1_PageIndexChanged(null,null);
            }
        }

        public void LoadMatchlist()
        {
            ddlmatch.DataValueField = "MARCHID";
            ddlmatch.DataTextField = "MARCHNAME";
             var ds = MatchManager.QueryMatchAndMarketForBetting();
             var matchlist = ds.Match_List.Where(s => ((s.IsEVENTTYPE_NAMENull()?"":s.EVENTTYPE_NAME) == "体育类"));

            var data = from s in matchlist
                       select new { MARCHID = s.MATCH_ID, MARCHNAME = s.MATCH_NAME };
            var oddsc = from s in OddsCompareManager.QueryOddsCompare2().TB_ODDS_COMPARE
                        select new { MARCHID = s.MATCHID, MARCHNAME = s.MATCHNAME };

            ddlmatch.DataSource = data.Union(oddsc).Distinct();
            ddlmatch.DataBind();
            ddlmatch.Items.Insert(0, new ListItem("请选择比赛", ""));
        }

        //public void BindGrid()
        //{
        //    //rptmatch.DataSource = GetData();
        //    //rptmatch.DataBind();
        //}
        public void ClearData()
        {
            ddlmatch.SelectedIndex = 0;
            txtcncorp.Text = string.Empty;
            txtencorp.Text = string.Empty;
            txtprofit.Text = string.Empty;
            hfdpid.Value = string.Empty;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
             PageNavigator1_PageIndexChanged(null,null);
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ClearData();
            if (sender is Button)
            {
                setenable(true);
                this.btnSave.CommandArgument = ButtonCommandType.Add.ToString();
                mdlPopup.Show();
            }
            if (sender is LinkButton)
            {
                setenable(false);
                LinkButton btn = sender as LinkButton;
                var commandString = btn.CommandArgument;
                string[] ids = commandString.Split(new string[] { "," }, StringSplitOptions.None);
                hfdpid.Value = commandString;
                GetoddscompareData(ids[0], ids[1]);
                this.btnSave.CommandArgument = ButtonCommandType.Edit.ToString();
                mdlPopup.Show();
            }
        }

        public void setenable(bool flag)
        {
            this.ddlmatch.Enabled = flag;
            txtcncorp.Enabled = flag;
            txtencorp.Enabled = flag;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int flag = btnSave.CommandArgument == ButtonCommandType.Edit.ToString() ? 3 : 1;
                OddsCompareManager.EditDSADWords(SetTopRaceData(), flag);
                PageNavigator1_PageIndexChanged(null,null);
                setenable(true);
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this.Page, "编辑失败");
                return;
            }
        }
        protected virtual void PageNavigator1_PageIndexChanged(object sender, EventArgs e)
        {
            var data = GetData();
            
           Session["oddscompare"] = data;
           var datalist = from s in data
                          select new { s.MATCHID, s.MATCHNAME };
            DataTable dt = new DataTable();
            dt.Columns.Add("MATCHID"); dt.Columns.Add("MATCHNAME");
            foreach (var i in datalist.Distinct())
            { 
                DataRow dr=dt.NewRow();
                dr["MATCHID"] = i.MATCHID;
                dr["MATCHNAME"] = i.MATCHNAME;
                dt.Rows.Add(dr);
            }
            dt.AcceptChanges();
            //datalist.Distinct().ToList().;
            if (PageNavigator1 != null)
            {
                PageNavigator1.databinds(dt, gdvMain);
            }
            else
            {
                gdvMain.DataSource = dt;
                gdvMain.DataBind();
            }
        }

        protected void gdvMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {   
                LinkButton hlDelete = e.Row.FindControl("hlDelete") as LinkButton;
                GridView gdvsubMain = e.Row.FindControl("gdvsubMain") as GridView;
               IEnumerable<DSODDSCOMPARE.TB_ODDS_COMPARERow> list= Session["oddscompare"] as IEnumerable<DSODDSCOMPARE.TB_ODDS_COMPARERow>;
               var data= list.Where(s=>s.MATCHID.ToString()==hlDelete.CommandArgument);
               gdvsubMain.DataSource = data;
               gdvsubMain.DataBind();
                hlDelete.Visible = MySession.Accessable(FunctionIdList.AssistantManagement.DeleteOdds);
            }
        }

        protected void gdvsubMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton hlsubEdit = e.Row.FindControl("hlsubEdit") as LinkButton;
                LinkButton hlsubDelete = e.Row.FindControl("hlsubDelete") as LinkButton;
                hlsubEdit.Visible = MySession.Accessable(FunctionIdList.AssistantManagement.EditOdds);
                    hlsubDelete.Visible = MySession.Accessable(FunctionIdList.AssistantManagement.DeleteOdds);
            }
        }


        public void GetoddscompareData(string marchid,string corpname)
        {
            var data = OddsCompareManager.QueryOddsCompare2().TB_ODDS_COMPARE.Where(s => s.MATCHID.ToString()==marchid && s.CN_CORP==corpname);
            if (data == null)
                return;
            if (data.Count() <1)
            {
                return;
            }
              DSODDSCOMPARE.TB_ODDS_COMPARERow row=  data.FirstOrDefault();
              txtcncorp.Text = row.CN_CORP;
              txtencorp.Text = row.EN_CORP;
              txtprofit.Text = row.PROFIT.ToString();
            ListItem li = ddlmatch.Items.FindByValue(row.MATCHID.ToString());
            if (li == null)
                ddlmatch.SelectedValue = "";
            else
                ddlmatch.SelectedValue = row.MATCHID.ToString();
        }

        public DSODDSCOMPARE.TB_ODDS_COMPARERow SetTopRaceData()
        {
            DSODDSCOMPARE ds = new DSODDSCOMPARE();
            DSODDSCOMPARE.TB_ODDS_COMPARERow data = ds.TB_ODDS_COMPARE.NewRow() as DSODDSCOMPARE.TB_ODDS_COMPARERow;
            data.MATCHID = int.Parse(this.ddlmatch.SelectedValue);
            data.MATCHNAME = ddlmatch.SelectedItem.Text;
            data.CN_CORP = txtcncorp.Text;
            data.EN_CORP = txtencorp.Text;
            data.PROFIT = decimal.Parse(txtprofit.Text);
            return data;
        }

        protected void rptmatch_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptMatchMarket = e.Item.FindControl("rptMatchMarket") as Repeater;
                HiddenField hfdmatchid=e.Item.FindControl("hfdmatchid") as HiddenField;
                rptMatchMarket.DataSource = OddsCompareManager.QueryOddsCompare2().TB_ODDS_COMPARE.Where(s => s.MATCHID.ToString() == hfdmatchid.Value);
                rptMatchMarket.DataBind();
            }
        }

        protected void martchDetail_OnClick(object sender, EventArgs e)
        {
          
        }
        protected void martchDel_OnClick(object sender, EventArgs e)
        {
            try
            {
                string id = ((LinkButton)sender).CommandArgument;
                var data = OddsCompareManager.QueryOddsCompare2().TB_ODDS_COMPARE.Where(s => s.MATCHID.ToString() == id);
                if (data == null)
                    return;
                if (data.Count() < 1)
                    return;
                var row = data.FirstOrDefault();
                int flag = 2;
                OddsCompareManager.EditDSADWords(row, flag);
                 PageNavigator1_PageIndexChanged(null,null);
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this.Page, "删除失败");
                return;
            }
        }
        protected void DelItem_OnClick(object sender, EventArgs e)
        {
            try
            {
                string[] ids = ((LinkButton)sender).CommandArgument.Split(new string[] { "," }, StringSplitOptions.None);
                var data = OddsCompareManager.QueryOddsCompare2().TB_ODDS_COMPARE.Where(s => s.MATCHID.ToString() == ids[0] && s.CN_CORP == ids[1]);
                if (data == null)
                    return;
                if (data.Count() < 1)
                    return;
                var row = data.FirstOrDefault();
                int flag = 4;
                OddsCompareManager.EditDSADWords(row, flag);
                 PageNavigator1_PageIndexChanged(null,null);
            }
            catch (Exception ex)
            {
                PageHelper.ShowMessage(this.Page, "删除失败");
                return;
            }
        }


    }
}