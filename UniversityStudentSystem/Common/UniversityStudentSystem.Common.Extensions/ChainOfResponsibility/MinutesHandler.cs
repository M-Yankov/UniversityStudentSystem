namespace UniversityStudentSystem.Common.Extensions.ChainOfResponsibility
{
    using System;

    internal class MinutesHandler : Handler
    {
        public override string HandleDateSpan(TimeSpan span, string timeString)
        {
            if (span.Minutes > 0)
            {
                return string.Format("{0} {1} ago",
                span.Minutes, span.Minutes == 1 ? "minute" : "minutes");
            }
            else
            {
                return this.Successor.HandleDateSpan(span, timeString);
            }
        }
    }
}
