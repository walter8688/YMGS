using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YMGS.Trade.Web.Common;
using YMGS.Business.SystemSetting;

namespace YMGS.Trade.Web.Public
{
    public partial class YourInPlay : BasePage
    {
        public const string NotLoginCode = "NOLOGIN";
        public const string AddFavSuccessCode = "OK";
        public override bool IsAccessible(UserAccess userAccess)
        {
            return true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int matchId = -1;
            int faved = -1;
            int userId = -1;
            if (CurrentUser == null)
                ResponseData(NotLoginCode);
            else
                userId = CurrentUser.UserId;

            if (Request.QueryString["matchId"] != null && Request.QueryString["matchId"].ToString() != "")
            {
                matchId = Convert.ToInt32(Request.QueryString["matchId"].ToString());
            }
            if (Request.QueryString["faved"] != null && Request.QueryString["faved"].ToString() != "")
            {
                faved = Convert.ToInt32(Request.QueryString["faved"].ToString());
            }
            ResponseData(AddYourInPlay(userId, matchId, faved));
        }

        private string AddYourInPlay(int userId,int matchId, int faved)
        {
            faved = 1 - faved;
            YourInPlayManager.ManageYourInPlay(userId, matchId, faved);
            return AddFavSuccessCode;
        }

        private void ResponseData(string data)
        {
            Response.Write(data);
            Response.End();
        }
    }
}