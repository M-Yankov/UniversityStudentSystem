namespace UniversityStudentSystem.Data.Models.CustomAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DataRangeAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "'{0}' must be a date between {1:d} and {2:d}.";
        private DateTime minDateRange;
        private DateTime maxDateRange;

        /// <summary>
        /// Initialize a DataRange attribute.
        /// </summary>
        /// <param name="minDate">In format: "dd.MM.yyyy"</param>
        /// <param name="maxDate">In format: "dd.MM.yyyy"</param>
        public DataRangeAttribute(string minDate, string maxDate) : base(DefaultErrorMessage)
        {
            this.minDateRange = DateTime.Parse(minDate);
            this.maxDateRange = DateTime.Parse(maxDate);
        }

        public override bool IsValid(object value)
        {
            DateTime dateToValidate = Convert.ToDateTime(value);
            return this.minDateRange <= dateToValidate && dateToValidate <= this.maxDateRange;
        }
    }
}