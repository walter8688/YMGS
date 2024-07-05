using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Data.Entity;
using System.Data;
using YMGS.Manage.Web.Common;
using System.Web.Security;

namespace YMGS.Manage.Web.MasterPage
{
    public partial class BetBase : System.Web.UI.MasterPage
    {  
        public string TopMenuID{get;set;}
        public string LeftMenuID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblCurTimeZone.Text = TimeZoneInfo.Local.DisplayName;
                lblUser.Text = MySession.CurrentUser.ACCOUNT[0].USER_NAME;

                SiteMapProvider smp = SiteMapDataSource1.Provider;
                SiteMapNodeCollection nodes=smp.RootNode.ChildNodes;
                SiteMapNodeCollection leftnodeList=null;
           
                foreach (SiteMapNode node in nodes)
                {
                    if (node.ResourceKey == TopMenuID)
                    {
                        leftnodeList = node.ChildNodes;
                        foreach (SiteMapNode leftnode in leftnodeList)
                        {
                            if (leftnode.ResourceKey == LeftMenuID)
                            {
                                Page.Title = leftnode.Title;
                                lbllocation.Text = string.Format(CommonConstant.MemberShipPageNaviTitleFormat, node.Title, leftnode.Title);
                                break;
                            }
                        }
                        break;
                    }
                }
                if (leftnodeList == null)
                    Response.Redirect(CommonConstant.NavigateDefaultPage);

                TopMenu1.GetTopMenu(GetMenu(GetRoleMenu(smp.RootNode.ChildNodes), TopMenuID));
                LeftMenu1.GetLeftMenu(GetMenu(GetRoleMenu(leftnodeList), LeftMenuID));
               
            }
        }

        private SiteMapNodeCollection GetLeftnodeList(SiteMapNode node, SiteMapNodeCollection leftnodeList)
        {
            SiteMapNodeCollection smnc = null;
            foreach (SiteMapNode cnode in leftnodeList)
            {
                if (cnode.ChildNodes.Contains(node))
                {
                    smnc = cnode.ChildNodes;
                    break;
                }
            }
            return smnc;
        }
        public SiteMapNodeCollection GetRoleMenu(SiteMapNodeCollection nodeList)
        {
            SiteMapNodeCollection smnc = new SiteMapNodeCollection();
           
            foreach(SiteMapNode node in nodeList)
            {
                try
                {
                    if (MySession.Accessable(Int32.Parse(node.ResourceKey)))
                        smnc.Add(node);
                }
                catch
                {
                    continue;
                }
            }
            return smnc;
        }
        public List<RoleMenu> GetMenu(SiteMapNodeCollection nodeList, string selectedKey)
        { 
           List< RoleMenu> rmList=new List<RoleMenu>();
            foreach (SiteMapNode node in nodeList)
            {
                RoleMenu rm = new RoleMenu();
                rm.NodeId = node.ResourceKey;
                if (selectedKey == node.ResourceKey)
                    rm.NodeSelected = true;
                else
                    rm.NodeSelected = false;
                rm.NodeTitle = node.Title;
                if(node.HasChildNodes)
                {
                    SiteMapNodeCollection mapNodeList = GetRoleMenu(node.ChildNodes);
                    if (mapNodeList.Count > 0)
                       rm.NodeUrl= mapNodeList[0].Url;
                    else
                    continue;
                }
                else
                rm.NodeUrl = node.Url;
                rmList.Add(rm);
            }
            return rmList;
        }
        protected void lbtExit_Click(object sender, EventArgs e)
        {
            MySession.ClearSessions();
            FormsAuthentication.SignOut();
        }


        protected void ScriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            ScriptManager1.AsyncPostBackErrorMessage = e.Exception.Message;
        }

        public ScriptManager ScriptManager
        {
            get
            {
                return ScriptManager1;
            }
        }
    }
}