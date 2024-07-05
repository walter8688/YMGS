using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.DataAccess.SystemSetting;
using YMGS.Data.DataBase;

namespace YMGS.Business.SystemSetting
{
    public class YourInPlayManager
    {
        public static void ManageYourInPlay(int userId, int matchId, int faved)
        {
            YourInPlayDA.ManageYourInPlay(userId, matchId, faved);
        }

        public static DSYourInPlay QueryYourInPlay(int userId)
        {
            return YourInPlayDA.QueryYourInPlay(userId);
        }

        public static DSYourInPlay QueryAllYourInPlay()
        {
            return YourInPlayDA.QueryAllYourInPlay();
        }
    }
}
