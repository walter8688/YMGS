using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YMGS.Data.Entity;

namespace YMGS.Business.Game.GameHelper
{
    public class FootballCalendarManager
    {
        public static List<FootballCalendar> GetFootballCalendar()
        {
            List<FootballCalendar> fbCalList = new List<FootballCalendar>();
            FootballCalendar fbCal = null;
            for (int i = 0; i < 7; i++)
            {
                fbCal = new FootballCalendar();
                if (i == 0)
                    fbCal.WeekCalendarName = "Today";
                else if(i==1)
                    fbCal.WeekCalendarName = "Tomorrow";
                else
                    fbCal.WeekCalendarName = DateTime.Now.AddDays(i).DayOfWeek.ToString();
                fbCal.CalendarDate = DateTime.Now.AddDays(i);
                fbCal.CalendarHref = string.Format("#ft-{0}", i + 1);
                fbCal.CalendarContentId = string.Format("ft-{0}", i + 1);
                fbCal.CalendarDateId = i + 1;
                fbCalList.Add(fbCal);
            }
            return fbCalList;
        }
    }
}
