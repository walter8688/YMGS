using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Entity;

namespace YMGS.Business.Game
{
    public interface IMatchObject
    {
        IList<MatchObject> GetMatchList();
    }
}
