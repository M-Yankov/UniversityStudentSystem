namespace UniversityStudentSystem.Common.Extensions.ChainOfResponsibility
{
    using System;

    internal abstract class Handler
    {
        protected Handler Successor { get; set; }

        public void SetSuccessor(Handler successor)
        {
            this.Successor = successor;
        }

        public abstract string HandleDateSpan(TimeSpan span, string timeString);
    }
}
