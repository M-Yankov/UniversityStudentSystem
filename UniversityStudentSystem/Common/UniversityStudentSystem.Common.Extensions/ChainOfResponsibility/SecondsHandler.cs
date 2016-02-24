namespace UniversityStudentSystem.Common.Extensions.ChainOfResponsibility
{
    using System;

    internal class SecondsHandler : Handler
    {
        public override string HandleDateSpan(TimeSpan span, string timeString)
        {
            if (span.Seconds > 5)
            {
                return string.Format("{0} seconds ago", span.Seconds);
            }
            else
            {
                return this.Successor.HandleDateSpan(span, timeString);
            }
        }
    }
}
