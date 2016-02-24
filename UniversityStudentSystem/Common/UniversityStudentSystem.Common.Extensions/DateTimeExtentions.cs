namespace UniversityStudentSystem.Common.Extensions
{
    using System;
    using ChainOfResponsibility;

    /// <summary>
    /// http://www.codeproject.com/Articles/770323/How-to-Convert-a-Date-Time-to-X-minutes-ago-in-Csh
    /// </summary>
    public static class DateTimeExtentions
    {
        public static string DateTimeAgo(this DateTime date)
        {
            TimeSpan span = DateTime.Now - date;
            string timeAsSting = string.Empty;

            var yearsHandler = new YearsHandler();
            var monthsHandler = new MonthsHandler();
            var daysHandler = new DaysHandler();
            var hoursHandler = new HoursHandler();
            var minutesHandler = new MinutesHandler();
            var secondsHandler = new SecondsHandler();
            var justNowHandler = new JustNowHandler();

            yearsHandler.SetSuccessor(monthsHandler);
            monthsHandler.SetSuccessor(daysHandler);
            daysHandler.SetSuccessor(hoursHandler);
            hoursHandler.SetSuccessor(minutesHandler);
            minutesHandler.SetSuccessor(secondsHandler);
            secondsHandler.SetSuccessor(justNowHandler);

            return yearsHandler.HandleDateSpan(span, timeAsSting);

            /*// Done
            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                {
                    years += 1;
                }

                timeAsSting = years == 1 ? "year" : "years";
                return $"{ years } { timeAsSting } ago";
            }

            // Done
            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                if (span.Days % 31 != 0)
                {
                    months += 1;
                }

                return string.Format("{0} {1} ago",
                months, months == 1 ? "month" : "months");
            }
            // Done
            if (span.Days > 0)
            {
                return string.Format("{0} {1} ago",
               span.Days, span.Days == 1 ? "day" : "days");
            }

            // Done
            if (span.Hours > 0)
            {
                return string.Format("{0} {1} ago",
                span.Hours, span.Hours == 1 ? "hour" : "hours");
            }

            // Done
            if (span.Minutes > 0)
            {
                return string.Format("{0} {1} ago",
                span.Minutes, span.Minutes == 1 ? "minute" : "minutes");
            }

            // Done
            if (span.Seconds > 5)
            {
                return string.Format("{0} seconds ago", span.Seconds);
            }

            if (span.Seconds <= 5)
            {
                return "just now";
            }

            return string.Empty;*/
        }
    }
}
