using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YMGS.Framework;

namespace YMGS.Manage.Web.GameControl
{
    /// <summary>
    /// Summary description for GetServerDateHandler
    /// </summary>
    public class GetServerDateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Write(UtilityHelper.DateTimeDefaultForamtString(DateTime.Now));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}