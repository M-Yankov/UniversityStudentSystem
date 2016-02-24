namespace UniversityStudentSystem.Common.Extensions.ChainOfResponsibility
{
    using System;

    internal class MonthsHandler : Handler
    {
        public override string HandleDateSpan(TimeSpan span, string timeString)
        {
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
            else
            {
                return this.Successor.HandleDateSpan(span, timeString);
            }
        }
    }
}
