namespace UniversityStudentSystem.Common
{
    public class ModelConstants
    {
        public const string MinDate = "01.01.2016";
        public const string MaxDate = "01.01.2030";
        public const int NameMaxLength = 50;
        public const int NameMinLength = 3;
        public const int ContentMaxLength = 500;
        public const int DescriptionMaxLength = 15000;

        public const string FacebookProfileRegularExpression = 
            "((http|https):\\/\\/|)(www\\.|)facebook\\.com\\/[a-zA-Z0-9.]{1,}";
        public const string SkypeNameRegularexpression = "^$|[a-zA-Z][a-zA-Z0-9_\\-\\,\\.]{5,31}";
        public const string LinkedInProfileRegularExpression = 
            "(?i)^$|^((http|https)?(\\://)?(www\\.)?linkedin.com/)(.)+$";

        public const string ErrorMessageProfile = "Provide a {0} profile";

        public const int MarkMinValue = 2;
        public const int MarkMaxValue = 6;

        public const int MinAge = 18;
        public const int MaxAge = 100;

        public const int FacultyStartNumber = 1000;
        public const int FacultyEndNumber = 999999;

        public const double SemesterFeeMinValue = 0;
        public const double SemesterFeeMaxValue = double.MaxValue;
    }
}
