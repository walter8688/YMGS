using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YMGS.Data.Entity
{
    public class FootballCalendar
    {
        public string WeekCalendarName { get; set; }
        public string WeekCalendarNameEn { get; set; }
        public DateTime CalendarDate { get; set; }
        public string CalendarHref { get; set; }
        public string CalendarContentId { get; set; }
        public int CalendarDateId { get; set; }
    }
}
