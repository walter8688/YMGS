using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.DataAccess.GameControlService;
using YMGS.Data.DataBase;

namespace YMGS.Business.GameControlService
{
    public class GameControlServiceManager
    {
        public static void AutoFreezeStartMatch(int matchId)
        {
            GameControlServiceDA.AutoFreezeStartMatch(matchId);
        }

        public static DSMatch GetAutoFreezeStartMatchs()
        {
            return GameControlServiceDA.GetAutoFreezeStartMatchs();
        }

    }
}
