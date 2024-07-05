using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YMGS.Business.EventManage;
using System.Data;

namespace YMGS.Manage.Web.GameMarket
{
    /// <summary>
    /// Summary description for GetTeamDetail
    /// </summary>
    public class GetTeamDetail : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.QueryString["hometeam"] != null && context.Request["guestteam"] != null)
            {
                var homeTeamId = Convert.ToInt32(context.Request.QueryString["hometeam"].ToString());
                var guestTeamId = Convert.ToInt32(context.Request["guestteam"].ToString());
                var dt = EventTeamManager.GetEventTeamNameEN(homeTeamId, guestTeamId);
                string resData = string.Empty;
                foreach (DataRow row in dt.Rows)
                {
                    resData += row[0].ToString() + "@";
                }
                resData = resData.Substring(0, resData.Length - 1);
                context.Response.Write(resData);
            }
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