using System;

namespace UniversityStudentSystem.Common.Extensions.ChainOfResponsibility
{
    internal class YearsHandler : Handler
    {
        public override string HandleDateSpan(TimeSpan span, string timeAsSting)
        {
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
            else
            {
                return this.Successor.HandleDateSpan(span, timeAsSting);
            }
        }
    }
}
