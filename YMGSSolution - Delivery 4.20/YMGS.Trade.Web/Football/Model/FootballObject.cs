using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YMGS.Trade.Web.Football.Model
{
    public class FootballObject
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventStartDate { get; set; }
        public IList<FootballMatch> FootballMatchs { get; set; }
    }
}