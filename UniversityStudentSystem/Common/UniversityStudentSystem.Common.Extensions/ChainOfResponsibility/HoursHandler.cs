namespace UniversityStudentSystem.Common.Extensions.ChainOfResponsibility
{
    using System;

    internal class HoursHandler : Handler
    {
        public override string HandleDateSpan(TimeSpan span, string timeString)
        {
            if (span.Hours > 0)
            {
                return string.Format("{0} {1} ago",
                    span.Hours, span.Hours == 1 ? "hour" : "hours");
            }
            else
            {
                return this.Successor.HandleDateSpan(span, timeString);
            }
        }
    }
}
