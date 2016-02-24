namespace UniversityStudentSystem.Common.Extensions.ChainOfResponsibility
{
    using System;

    internal class DaysHandler : Handler
    {
        public override string HandleDateSpan(TimeSpan span, string timeString)
        {
            if (span.Days > 0)
            {
                return string.Format("{0} {1} ago",
               span.Days, span.Days == 1 ? "day" : "days");
            }
            else
            {
                return this.Successor.HandleDateSpan(span, timeString);
            }
        }
    }
}
