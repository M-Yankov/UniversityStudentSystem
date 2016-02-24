namespace UniversityStudentSystem.Common.Extensions.ChainOfResponsibility
{
    using System;

    internal class MinutesHandler : Handler
    {
        public override string HandleDateSpan(TimeSpan span, string timeString)
        {
            if (span.Minutes > 0)
            {
                timeString = span.Minutes == 1 ? "minute" : "minutes";
                return $"{ span.Minutes } { timeString } ago";
            }
            else
            {
                return this.Successor.HandleDateSpan(span, timeString);
            }
        }
    }
}
