using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using YMGS.Data.DataBase;
using YMGS.Business.AssistManage;
using YMGS.Data.Common;
using YMGS.Trade.Web.Common;
using System.Threading;
using YMGS.Business.Cache;
using System.Data;
using YMGS.Data.Entity;

namespace YMGS.Trade.Web.Public
{
    public partial class HelperCenter : BasePage
    {
        public string LeftNavigatorStr = string.Empty;

        public override bool IsAccessible(YMGS.Trade.Web.Common.UserAccess userAccess)
        {
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLeftNavigtor();
            }
        }
        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(HelperCenter), "Public").AbsoluteUri;
        }

        private void BindLeftNavigtor()
        {
            LeftNavigatorStr = "";
            LoadLeftNavigtor();
        }

        private IList<HelperObject> GetHelperData()
        {
            HelperManager bl = new HelperManager(Language);
            return bl.GetHelperDataLstByCache();
        }

        private void LoadLeftNavigtor()
        {
            IList<HelperObject> helpData = GetHelperData();
            IList<HelperObject> subLst = helpData.Where(x => x.PITEMID == 0).ToList<HelperObject>();
            if (subLst.Count == 0)
                return;
            foreach (HelperObject bo in subLst)
            {
                if (bo.IsHasChild)
                {
                    //LeftNavigatorStr += "<li><a href='#'  class='hassubmenu'>" + bo.ItemName + "</a>";
                    LeftNavigatorStr += "<li><a href='" + bo.LinkAddress + "'  class='hassubmenu' target='WebPath'>" + bo.ItemName + "</a>";
                    LoadChildNavigtor(bo.ITEMID);
                }
                else
                {
                    //LeftNavigatorStr += "<li><a href='#'>" + bo.ItemName + "</a></li>";
                    LeftNavigatorStr += "<li><a href='" + bo.LinkAddress + "' target='WebPath'>" + bo.ItemName + "</a></li>";
                }
            }
        }

        private void LoadChildNavigtor(int pItemID)
        {
            IList<HelperObject> helpData = GetHelperData();
            IList<HelperObject> subLst = helpData.Where(x => x.PITEMID == pItemID).ToList<HelperObject>();
            if (subLst.Count == 0)
                return;
            LeftNavigatorStr += "<ul>";
            foreach (HelperObject bo in subLst)
            {
                if (bo.IsHasChild)
                {
                    //LeftNavigatorStr += "<li><a href='#'  class='hassubmenu'>" + bo.ItemName + "</a>";
                    LeftNavigatorStr += "<li><a href='" + bo.LinkAddress + "'  class='hassubmenu' target='WebPath'>" + bo.ItemName + "</a>";
                    LoadChildNavigtor(bo.ITEMID);
                }
                else
                {
                    //LeftNavigatorStr += "<li><a href='#'>" + bo.ItemName + "</a></li>";
                    LeftNavigatorStr += "<li><a href='" + bo.LinkAddress + "' target='WebPath'>" + bo.ItemName + "</a></li>";
                }
            }
            LeftNavigatorStr += "</ul></li>";
        }

        #region 缩进
        //private void LoadChildNavigtor(int pItemID)
        //{
        //    IList<HelperObject> helpData = GetHelperData();
        //    IList<HelperObject> subLst = helpData.Where(x => x.PITEMID == pItemID).ToList<HelperObject>();
        //    if (subLst.Count == 0)
        //        return;
        //    LeftNavigatorStr += "<ul>";
        //    foreach (HelperObject bo in subLst)
        //    {
        //        if (bo.IsHasChild)
        //        {
        //            LeftNavigatorStr += "<li class='hassubmenu2'";
        //            if (bo.LevelNO > 2)
        //            {
        //                LeftNavigatorStr += "style='margin-left:0px;'";
        //            }
        //            LeftNavigatorStr += "><a href='" + bo.LinkAddress + "' target='WebPath'>" + bo.ItemName + "</a>";
        //            LoadChildNavigtor(bo.ITEMID);
        //        }
        //        else
        //        {
        //            LeftNavigatorStr += "<li class='nosubmenu2'";
        //            if (bo.LevelNO > 2)
        //            {
        //                LeftNavigatorStr += "style='margin-left:0px;'";
        //            }
        //            LeftNavigatorStr += "><a href='" + bo.LinkAddress + "' target='WebPath'>" + bo.ItemName + "</a></li>";
        //        }
        //    }
        //    LeftNavigatorStr += "</ul></li>";
        //}
        #endregion

        private string GetHtml(string url)
        {
            WebRequest request = WebRequest.Create(url);//发出对统一资源标识符URI的请求

            WebResponse response = request.GetResponse();//提供统一资源标识符URI的响应

            Stream resStream = response.GetResponseStream();//从Internet资源返回数据流

            StreamReader sr = new StreamReader(resStream, System.Text.Encoding.GetEncoding("utf-8"));//以特定的编码读取字节流

            string ContentHtml = sr.ReadToEnd();//从流的当前位置到未尾读取字节流

            resStream.Close();
            sr.Close();
            return ContentHtml;
        }

    }
}