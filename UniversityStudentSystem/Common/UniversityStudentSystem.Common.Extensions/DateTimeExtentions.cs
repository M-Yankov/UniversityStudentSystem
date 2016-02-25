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
        }
    }
}
