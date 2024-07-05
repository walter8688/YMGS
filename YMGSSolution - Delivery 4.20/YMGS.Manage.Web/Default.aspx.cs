using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Data.Entity;
using YMGS.Framework;
using YMGS.Business.SystemSetting;
using YMGS.Data.DataBase;
using YMGS.Manage.Web.Common;
using YMGS.Manage.Web.SystemSetting;
using System.Web.Security;

namespace YMGS.Manage.Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }
        public static string Url()
        {
            return UrlHelper.BuildUrl(typeof(Default), string.Empty).AbsoluteUri;
        }
        protected void btnLogin_Click(object sender, ImageClickEventArgs e)
        {
            string userId = txtUserId.Text.Trim();
            string EncryptPsw = EncryptManager.GetEncryString(txtPsw.Text.Trim());
            DSSystemAccount.TB_SYSTEM_ACCOUNTDataTable dt=  SystemSettingManager.QueryData(userId,0,"").TB_SYSTEM_ACCOUNT;
      
            if(dt.Count==1)
            {
                if (EncryptPsw == dt[0].PASSWORD)
                {
                    if (dt[0].ACCOUNT_STATUS == 0)
                    {
                        PageHelper.ShowMessage(this.Page, "请激活帐户!");
                        return;
                    }
                    if (dt[0].ACCOUNT_STATUS ==2 )
                    {
                        PageHelper.ShowMessage(this.Page, "帐户已锁定!");
                        return;
                    }
                    UserDetail user = new UserDetail();
                    user.ACCOUNT = dt;
                    user.ROLE_FUNC=  RoleFuncMapManager.QueryData(dt[0].ROLE_ID).TB_ROLE_FUNC_MAP;
                    MySession.CurrentUser = user;

                    FormsAuthentication.SetAuthCookie(user.ACCOUNT.ToString(), false);
                    RedirectUrl();
                }
                else
                    PageHelper.ShowMessage(this.Page, "用户名或密码不正确!");  
            }
            PageHelper.ShowMessage(this.Page, "没有该用户!");  
        }

        private void RedirectUrl()
        {
            SiteMapProvider smp = SiteMapDataSource1.Provider;
            SiteMapNodeCollection nodes = smp.RootNode.ChildNodes;
            SiteMapNodeCollection list=  GetRoleMenu(smp.RootNode.ChildNodes);
            if (list.Count > 0)
            {
                SiteMapNode node = list[0];
                if (node.HasChildNodes)
                {
                    SiteMapNodeCollection mapNodeList = GetRoleMenu(node.ChildNodes);
                    if (mapNodeList.Count > 0)
                        Response.Redirect(mapNodeList[0].Url);
                }
            }             
        }

        public SiteMapNodeCollection GetRoleMenu(SiteMapNodeCollection nodeList)
        {
            SiteMapNodeCollection smnc = new SiteMapNodeCollection();

            foreach (SiteMapNode node in nodeList)
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
    }
}