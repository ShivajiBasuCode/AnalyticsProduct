using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace RealityCS.SharedMethods
{
    public class RelativeDateRange
    {
        //https://stackoverflow.com/a/711154/4336330
        /// <summary>
        /// 
        /// </summary>
        public struct DateRange
        {
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }
        /// <summary>
        /// This Year Start End
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateRange ThisYear(DateTime date)
        {
            DateRange range = new DateRange();

            range.Start = new DateTime(date.Year, 1, 1);
            range.End = range.Start.AddYears(1).AddSeconds(-1);

            return range;
        }
        /// <summary>
        /// Last Year Start End
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateRange LastYear(DateTime date)
        {
            DateRange range = new DateRange();

            range.Start = new DateTime(date.Year - 1, 1, 1);
            range.End = range.Start.AddYears(1).AddSeconds(-1);

            return range;
        }
        /// <summary>
        /// ThisMonth Start End
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateRange ThisMonth(DateTime date)
        {
            DateRange range = new DateRange();

            range.Start = new DateTime(date.Year, date.Month, 1);
            range.End = range.Start.AddMonths(1).AddSeconds(-1);

            return range;
        }
        /// <summary>
        /// LastMonth Start End
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateRange LastMonth(DateTime date)
        {
            DateRange range = new DateRange();

            range.Start = (new DateTime(date.Year, date.Month, 1)).AddMonths(-1);
            range.End = range.Start.AddMonths(1).AddSeconds(-1);

            return range;
        }
        /// <summary>
        /// Querter Start End
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        //https://stackoverflow.com/a/1492129/4336330
        public static DateRange Querter(DateTime date)
        {
            DateRange range = new DateRange();

            int quarterNumber = (date.Month - 1) / 3 + 1;
            range.Start = new DateTime(date.Year, (quarterNumber - 1) * 3 + 1, 1);
            range.End = range.Start.AddMonths(3).AddDays(-1);

            return range;
        }

        /// <summary>
        /// QQ YYYY
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        //https://stackoverflow.com/a/8698317/4336330
        public static string FormatedQuerter(DateTime date)
        {
            return string.Format("Q{0} {1}", (date.Month + 2) / 3,date.Year.ToString());
        }
        /// <summary>
        /// Week First Date Monday
        /// </summary>
        /// <returns></returns>
        public static DateRange ThisWeek()
        {
            DateRange range = new DateRange();
            DateTime Today = DateTime.Today;

            range.Start = Today.Date.AddDays(-(int)(Today.DayOfWeek- DayOfWeek.Monday));
            range.End = range.Start.AddDays(7).AddSeconds(-1);

            return range;
        }
        /// <summary>
        /// Week First Date Monday
        /// </summary>
        /// <returns></returns>
        public static DateRange ThisWeek(DateTime date)
        {
            DateRange range = new DateRange();

            range.Start = date.Date.AddDays(-(int)(date.DayOfWeek - DayOfWeek.Monday));
            range.End = range.Start.AddDays(7).AddSeconds(-1);

            return range;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateRange LastWeek(DateTime date)
        {
            DateRange range = ThisWeek(date);

            range.Start = range.Start.AddDays(-7);
            range.End = range.End.AddDays(-7);

            return range;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        // This presumes that weeks start with Sunday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        //https://stackoverflow.com/a/11155102/4336330
        public static int GetWeekOfYear(DateTime date)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(date);
            if (day >= DayOfWeek.Sunday && day <= DayOfWeek.Wednesday)
            {
                date = date.AddDays(4);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="weekno"></param>
        /// <returns></returns>
        public static DateRange GetWeekDateRangeOfByWeekNumber(Int32 weekno)
        {           
            DateTime firstdateofweek = FirstDateOfWeek(DateTime.Now.Year, weekno);
            var range = ThisWeek(firstdateofweek);
            return range;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="weekno"></param>
        /// <returns></returns>
        public static DateRange GetWeekDateRangeOfByWeekNumber(Int32 year,Int32 weekno)
        {
            DateTime firstdateofweek = FirstDateOfWeek(year, weekno);
            var range = ThisWeek(firstdateofweek);
            return range;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateRange GetWeekDateRangeOfByWeekNumber(DateTime date)
        {
            Int32 weekno = GetWeekOfYear(date);
            DateTime firstdateofweek = FirstDateOfWeek(date.Year, weekno);
            var range = ThisWeek(firstdateofweek);
            return range;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<DateRange> GetWeekDateRangeListUpto(DateTime date)
        {
            List<DateRange> dateRanges = new List<DateRange>();
            Int32 weekno = GetWeekOfYear(date);
            for(int i = 1; i <= weekno; i++)
            {
                var range = GetWeekDateRangeOfByWeekNumber(i);
                dateRanges.Add(new DateRange { Start = range.Start, End = range.End });
            }

            return dateRanges;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="weekno"></param>
        /// <returns></returns>
        public static List<DateRange> GetWeekDateRangeListUpto(Int32 weekno)
        {
            List<DateRange> dateRanges = new List<DateRange>();            
            for (int i = 1; i <= weekno; i++)
            {
                var range = GetWeekDateRangeOfByWeekNumber(i);
                dateRanges.Add(new DateRange { Start = range.Start, End = range.End });
            }

            return dateRanges;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="weekno"></param>
        /// <returns></returns>
        public static List<DateRange> GetWeekDateRangeListUpto(Int32 year, Int32 weekno)
        {
            List<DateRange> dateRanges = new List<DateRange>();
            
            for (int i = 1; i <= weekno; i++)
            {
                var range = GetWeekDateRangeOfByWeekNumber(year, weekno);
                dateRanges.Add(new DateRange { Start = range.Start, End = range.End });
            }

            return dateRanges;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="weekOfYear"></param>
        /// <returns></returns>
        //https://stackoverflow.com/a/9064954/4336330
        private static  DateTime FirstDateOfWeek(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            // Use first Thursday in January to get first week of the year as
            // it will never be in Week 52/53
            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            // As we're adding days to a date in Week 1,
            // we need to subtract 1 in order to get the right date for week #1
            if (firstWeek == 1)
            {
                weekNum -= 1;
            }

            // Using the first Thursday as starting week ensures that we are starting in the right year
            // then we add number of weeks multiplied with days
            var result = firstThursday.AddDays(weekNum * 7);

            // Subtract 3 days from Thursday to get Monday, which is the first weekday in ISO8601
            return result.AddDays(-3);
        }
    }
}
