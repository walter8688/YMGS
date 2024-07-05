using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Common;

namespace YMGS.Data.Entity
{
    /// <summary>
    /// 比赛状态
    /// </summary>
    [Serializable]
    public class MatchStatusInfo
    {
        public int MatchStatus
        {
            get;
            set;
        }

        public string MatchStatusName
        {
            get;
            set;
        }
    }

  [Serializable]
    public class MatchMarcketInfo
    {
      public int MATCHTYPE { get; set; }
        public int MATCH_ID { get; set; }
        public string MATCH_NAME { get; set; }
        public int MARKET_ID { get; set; }
        public string MARKET_NAME { get; set; }
        public int MARKET_TMP_ID { get; set; }
        public string MARKET_TMP_NAME { get; set; }
        public string odds { get; set; }
        public string AMOUNTS { get; set; }
        public override bool Equals(object obj)
        {
            MatchMarcketInfo o=obj as MatchMarcketInfo;
            if (this.MARKET_ID == o.MARKET_ID && this.MATCHTYPE==o.MATCHTYPE)
                return true;
            if (this == obj)
                return true;
            
            return false;
        }

        public override int GetHashCode()
        {
            return (this.MARKET_ID + this.MATCHTYPE).GetHashCode();
        }
    }
}
