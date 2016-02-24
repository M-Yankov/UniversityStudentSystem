namespace UniversityStudentSystem.Common.Extensions.ChainOfResponsibility
{
    using System;

    internal class JustNowHandler : Handler
    {
        public override string HandleDateSpan(TimeSpan span, string timeString)
        {
            if (span.Seconds <= 5)
            {
                return "just now";
            }

            //// If program comes here something bad is happened!
            return string.Empty;
        }
    }
}
