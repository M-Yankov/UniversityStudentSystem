namespace UniversityStudentSystem.Common.Extensions.ChainOfResponsibility
{
    using System;

    internal class HoursHandler : Handler
    {
        public override string HandleDateSpan(TimeSpan span, string timeString)
        {
            if (span.Hours > 0)
            {
                timeString = span.Hours == 1 ? "hour" : "hours";
                return $"{ span.Hours } { timeString } ago";
            }
            else
            {
                return this.Successor.HandleDateSpan(span, timeString);
            }
        }
    }
}
