using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YMGS.Trade.Web.Common
{
    /// <summary>
    /// 菜单项
    /// </summary>
    [Serializable]
    public class MenuLink
    {
        public string Text { get; set; }
        public string Url { get; set; }
        public string UrlTarget { get; set; }
        public bool Selected { get; set; }
        public bool IsAccessible { get; set; }
        public int MenuId { get; set; }
        public string ResourceKey { get; set; }
        public string DisplayText
        {
            get
            {
                var temp = LangManager.GetString(ResourceKey);
                if (string.IsNullOrEmpty(temp))
                    return Text;
                else
                    return temp;     
            }
        }
    }

    /// <summary>
    /// 模块
    /// </summary>
    [Serializable]
    public class ModuleMenuLink : MenuLink
    {
        public IList<MenuLink> SubMenus { get; set; }
    }

    public class SystemMenuHelper
    {
        public static IList<ModuleMenuLink> GetSystemMenus()
        {
            IList<ModuleMenuLink> moduleMenuList = new List<ModuleMenuLink>();
            var webSiteMenus = SiteMapHelper.GetSubMenus(CommonConstant.DefaultWebSiteRootTitle);
            Array.ForEach(webSiteMenus.ToArray(), x =>
            {
                ModuleMenuLink moduleMenu = new ModuleMenuLink()
                {
                    Text = x.Title,
                    Url = x.Url,
                    UrlTarget = string.Empty,
                    Selected = false,
                    IsAccessible = false,
                    MenuId = Convert.ToInt32(x.Description.Split('|')[0]),
                    ResourceKey = x.Description.Split('|')[1]
                };

                IList<MenuLink> subMenuList = new List<MenuLink>();

                if (x.HasChildNodes)
                {
                    Array.ForEach(x.ChildNodes.Cast<SiteMapNode>().ToArray(), y =>
                    {
                        subMenuList.Add(new MenuLink()
                        {
                            Text = y.Title,
                            Url = y.Url,
                            Selected = false,
                            IsAccessible = false,
                            MenuId = Convert.ToInt32(y.Description.Split('|')[0]),
                            ResourceKey = y.Description.Split('|')[1]
                        });
                    });
                }

                moduleMenu.SubMenus = subMenuList;
                moduleMenuList.Add(moduleMenu);
            });

            return moduleMenuList;
        }
    }
}