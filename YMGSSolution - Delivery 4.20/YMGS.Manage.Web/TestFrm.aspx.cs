using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Business.SystemConfig;
using YMGS.Data.DataBase;
using YMGS.Manage.Web.Common;
using YMGS.Framework;

namespace YMGS.Manage.Web
{
    public partial class TestFrm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnQuery_Click(this, null);
        }

        private DSTest.YMGS_TESTRow GetCurrentRow()
        {
            if (string.IsNullOrEmpty(txtNames.Text) || string.IsNullOrEmpty(txtDescription.Text))
                return null;

            DSTest.YMGS_TESTDataTable tempDT = new DSTest.YMGS_TESTDataTable();
            DSTest.YMGS_TESTRow curRow = tempDT.NewYMGS_TESTRow();
            curRow.NAMES = txtNames.Text;
            curRow.DESCRIPTION = txtDescription.Text;
            return curRow;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var curRow = GetCurrentRow();
            if(curRow == null)
                return;
            int id = TestManager.AddTests1(curRow);
            PageHelper.ShowMessage(this,"当前新增数据主键ID为" + id.ToString());
            btnQuery_Click(this, null);
        }

        protected void btnAdd0_Click(object sender, EventArgs e)
        {
            var curRow = GetCurrentRow();
            if (curRow == null)
                return;
            int id = TestManager.AddTests2(curRow);
            PageHelper.ShowMessage(this, "当前新增数据主键ID为" + id.ToString());
            btnQuery_Click(this, null);
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            var resultDS = TestManager.QueryTest1();
            GridView1.DataSource = resultDS.YMGS_TEST;
            GridView1.DataBind();

            //访问其中的数据

            for (int i = 0; i < resultDS.YMGS_TEST.Rows.Count; i++)
            {
                var tempNames = resultDS.YMGS_TEST[i].NAMES;
            }
        }

        protected void btnQuery0_Click(object sender, EventArgs e)
        {
            var resultDS = TestManager.QueryTest2();
            GridView1.DataSource = resultDS.YMGS_TEST;
            GridView1.DataBind();
        }

        protected void btnAdd1_Click(object sender, EventArgs e)
        {
            var curRow = GetCurrentRow();
            if (curRow == null)
                return;
            int id = TestManager.AddTestsFailed(curRow);   
         }

        protected void btnAdd2_Click(object sender, EventArgs e)
        {

            TestManager.TestBusinessRule(true);   
            btnQuery_Click(null, null);
        }

        protected void btnAdd3_Click(object sender, EventArgs e)
        {   
            TestManager.TestBusinessRule(false);      
            btnQuery_Click(null, null);
        }

        protected void btnLog_Click(object sender, EventArgs e)
        {
            LogHelper.LogInformation("this is a test log!");
        }
    }
}