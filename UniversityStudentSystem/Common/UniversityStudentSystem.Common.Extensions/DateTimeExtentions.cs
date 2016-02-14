namespace UniversityStudentSystem.Common.Extensions
{
    using System;

    /// <summary>
    /// http://www.codeproject.com/Articles/770323/How-to-Convert-a-Date-Time-to-X-minutes-ago-in-Csh
    /// </summary>
    public static class DateTimeExtentions
    {
        // TODO: Chain of responsibility ?
        public static string DateTimeAgo(this DateTime date)
        {
            TimeSpan span = DateTime.Now - date;
            string timeAsSting = string.Empty;
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

            if (span.Days > 0)
            {
                return string.Format("{0} {1} ago",
               span.Days, span.Days == 1 ? "day" : "days");
            }

            if (span.Hours > 0)
            {
                return string.Format("{0} {1} ago",
                span.Hours, span.Hours == 1 ? "hour" : "hours");
            }

            if (span.Minutes > 0)
            {
                return string.Format("{0} {1} ago",
                span.Minutes, span.Minutes == 1 ? "minute" : "minutes");
            }

            if (span.Seconds > 5)
            {
                return string.Format("{0} seconds ago", span.Seconds);
            }

            if (span.Seconds <= 5)
            {
                return "just now";
            }

            return string.Empty;
        }
    }
}
