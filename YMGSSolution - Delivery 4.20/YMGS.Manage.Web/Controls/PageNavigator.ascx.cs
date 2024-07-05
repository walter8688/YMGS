using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace YMGS.Manage.Web.Controls
{
    /// <summary>
    /// 分页导航控件
    /// </summary>
    public partial class PageNavigator : System.Web.UI.UserControl
    {

        public event EventHandler PageIndexChanged;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(TotalCount>0)
                this.Visible = true;
                else
                this.Visible = false;
                txtShowPage.Text = PageSize.ToString();
            }
        }


        /// <summary>
        /// 绑定分页数据
        /// </summary>
        /// <param name="dt">数据table</param>
        /// <param name="DataList1">待绑定的DataList控件名称</param>
        public void databinds(DataTable dt, GridView ctrl)
        {
            try
            {
                int dtCount = dt == null ? 0 : dt.Rows.Count;

                if (dtCount > 0)
                {
                    SetPagerVisiable(true);
                    PagedDataSource objPage = new PagedDataSource();

                    //设置数据源
                    objPage.DataSource = dt.DefaultView;
                    //允许分页
                    objPage.AllowPaging = true;
                    //设置每页显示的项数
                    int psize = 20;
                    
                        if (int.TryParse(txtShowPage.Text, out psize))
                        {
                            objPage.PageSize = psize;
                        }
                    if(psize==0)
                        objPage.PageSize = psize = PageSize;
                    //定义变量用来保存当前页索引
                    int CurPage = 1;

                    string UrlPage = ViewState["CurPage"] == null ? "" : Convert.ToString(ViewState["CurPage"]);

                    if (!string.IsNullOrEmpty(UrlPage))
                    {
                        int result = 1;
                        if (int.TryParse(UrlPage.Trim(), out result))
                        {
                            CurPage = result;
                        }
                    }
                    if (CurPage > objPage.PageCount)
                    {
                        ViewState["CurPage"] = CurPage = 1;
                    }

                    //ViewState["CurPage"] = CurPage;
                    //设置当前页的索引
                    int countnumber = dtCount;
                    double db = countnumber / objPage.PageSize;
                    int n = int.Parse(db.ToString());
                    if (countnumber != objPage.PageSize * n)
                    {
                        n += 1;
                    }
                    if (n == 0)
                    {
                        n = 1;
                    }

                    objPage.CurrentPageIndex = CurPage - 1;
                    PagerSetting(psize, dtCount);

                    ViewState["PageCount"] = objPage.PageCount;
                    ctrl.DataSource = objPage;
                    ctrl.DataBind();

                }
                else //没有记录
                {
                    #region
                    ctrl.DataSource = null;
                    ctrl.DataBind();
                    SetPagerVisiable(false);

                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetPagerVisiable(bool flag)
        {
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Visible = flag;
            }
        }


        /// <summary>
        /// 获取当前页码
        /// </summary>
        public int CurrentPageIndex
        {
            get
            {
                string[] s = this.label1.Text.Trim().Split(new Char[] { '/' });

                int currentIndex = Convert.ToInt32(s[0]);
                if (currentIndex == 0)
                    return 0;
                else
                    return currentIndex - 1;

            }
        }
        /// <summary>
        /// 获取或者设置页大小
        /// </summary>
        private int _PageSize;
        public int PageSize
        {
            get {
                if(_PageSize==0)
                return int.Parse(System.Configuration.ConfigurationManager.AppSettings["pageSize"].ToString());
                return _PageSize;
            }
            set { _PageSize = value; }
        }
        /// <summary>
        /// 总页数
        /// </summary>
        private int PageCount
        {
            get
            {
                string[] s = this.label1.Text.Trim().Split(new Char[] { '/' });
                int currentIndex = Convert.ToInt32(s[1]);

                return currentIndex;

            }
        }
        /// <summary>
        /// 返回记录的总行数
        /// </summary>
        public int TotalCount
        {
            get
            {
                var s = this.totalCount.Text.Replace("共", string.Empty).Replace("条记录", string.Empty).Trim();
                if (string.IsNullOrEmpty(s))
                    return 0;
                return Convert.ToInt32(s);
            }
        }

        public void PagerSetting(DataTable dt)
        {
            int dtCount = dt == null ? 0 : dt.Rows.Count;
            PagerSetting(PageSize, dtCount);
        }

        /// <summary>
        ///控件设置
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        public void PagerSetting(int pageSize, int totalCount)
        {
            try
            {
                int pageCount;

                if ((totalCount % pageSize) == 0)
                {
                    pageCount = totalCount / pageSize;
                }
                else
                {
                    pageCount = totalCount / pageSize + 1;
                }
                if (pageCount > 0)
                {
                    string curpage = ViewState["CurPage"] == null ? "1" : ViewState["CurPage"].ToString();

                    if (int.Parse(curpage) > pageCount)
                        curpage = "1";
                    this.label1.Text = string.Format("{0}/{1}", curpage, pageCount);

                    if (pageCount == 1)
                    {
                        SetButtonEnabled(false, false, false, false);
                    }
                    else
                    {
                        if (curpage == "1")
                        {
                            SetButtonEnabled(false, false, true, true);
                        }
                        else if (curpage == pageCount.ToString())
                        {
                            SetButtonEnabled(true, true, false, false);
                        }
                        else
                        {
                            SetButtonEnabled(true, true, true, true);
                        }

                    }
                }
                else
                {
                    this.label1.Text = string.Format("{0}/{1}", 0, pageCount);
                    SetButtonEnabled(false, false, false, false);
                }
                this.totalCount.Text = string.Format("共{0}条记录", totalCount);

                this.Visible = true;
            }
            catch 
            {

            }
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FirstButton_Click(object sender, EventArgs e)
        {
            ViewState["CurPage"] = 1;
            OnPageClickedEvent();

        }
        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PreviousButton_Click(object sender, EventArgs e)
        {
            var currentPageIndex = CurrentPageIndex;
            if (currentPageIndex > 0)
            {
                currentPageIndex = currentPageIndex - 1;
                ViewState["CurPage"] = currentPageIndex + 1;
                OnPageClickedEvent();
            }


        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NextButton_Click(object sender, EventArgs e)
        {
            var currentPageIndex = CurrentPageIndex;
            var pageCount = PageCount;

            if (currentPageIndex < pageCount - 1)
            {
                currentPageIndex = currentPageIndex + 1;
                ViewState["CurPage"] = currentPageIndex + 1;
                OnPageClickedEvent();
            }
        }
        /// <summary>
        /// 最后一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LastButton_Click(object sender, EventArgs e)
        {
            ViewState["CurPage"] = PageCount;
            OnPageClickedEvent();
        }

        /// <summary>
        /// 发出分页事件
        /// </summary>
        private void OnPageClickedEvent()
        {
            if (PageIndexChanged != null)
            {
                PageIndexChanged(this, EventArgs.Empty);

            }
        }
        /// <summary>
        /// 设置按纽的状态
        /// </summary>
        /// <param name="first"></param>
        /// <param name="previous"></param>
        /// <param name="next"></param>
        /// <param name="last"></param>
        private void SetButtonEnabled(bool first, bool previous, bool next, bool last)
        {
            this.FirstButton.Enabled = first;
            this.PreviousButton.Enabled = previous;
            this.NextButton.Enabled = next;
            this.LastButton.Enabled = last;
        }




    }
}