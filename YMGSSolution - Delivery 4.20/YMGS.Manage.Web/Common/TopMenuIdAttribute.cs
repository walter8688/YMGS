using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.IO;

namespace YMGS.Manage.Web.Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TopMenuIdAttribute : Attribute
    {
        private int topMenuId;
        public TopMenuIdAttribute(int topMenuId)
        {
            this.topMenuId = topMenuId;
        }

        public int TopMenuId
        {
            get
            {
                return this.topMenuId;
            }
        }

        public static int Get(MemberInfo member)
        {
            object[] attributes = member.GetCustomAttributes(typeof(TopMenuIdAttribute), false);
            if (attributes.Length == 0)
            {
                throw new ApplicationException("Page should override TopMenuId or have TopMenuId attribute");
            }
            else
                return ((TopMenuIdAttribute)attributes[0]).TopMenuId;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class LeftMenuIdAttribute : Attribute
    {
        private int leftMenuId;
        public LeftMenuIdAttribute(int leftMenuId)
        {
            this.leftMenuId = leftMenuId;
        }

        public int LeftMenuId
        {
            get
            {
                return this.leftMenuId;
            }
        }

        public static int Get(MemberInfo member)
        {
            object[] attributes = member.GetCustomAttributes(typeof(LeftMenuIdAttribute), false);
            if (attributes.Length == 0)
            {
                throw new ApplicationException("Page should override TopMenuId or have TopMenuId attribute");
            }
            else
                return ((LeftMenuIdAttribute)attributes[0]).LeftMenuId;
        }
    }
}
