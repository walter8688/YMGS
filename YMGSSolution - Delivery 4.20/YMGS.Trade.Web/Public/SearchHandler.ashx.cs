using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YMGS.Data.Presentation;
using YMGS.Business.EventManage;
using YMGS.Data.Entity;
using YMGS.Trade.Web.Common;
using YMGS.Business.Search;

namespace YMGS.Trade.Web.Public
{
    /// <summary>
    /// Summary description for SearchHandler
    /// </summary>
    public class SearchHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var searchKey = context.Request.QueryString["term"].ToString();
            var lan = context.Request.QueryString["lan"].ToString();
            var jsonObj = JsonHelper.ListToJson<SearchObject>(SearchManager.GetAutoCompleteSearchedList(searchKey,lan));
            context.Response.ContentType = "application/json";
            context.Response.Write(jsonObj);
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