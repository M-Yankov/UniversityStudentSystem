﻿namespace UniversityStudentSystem.Common
{
    public class WebConstants
    {
        public const string DefaultImageNews = "/Images/university-default-news.png";
        public const string DefaultImageProfile = "/Images/student-default.png";
        public const string AcceptImageProfileTypes = ".img, .png, .gif, .jpg, .jpeg";
        public const string AcceptArchives = ".zip, .rar, .7z";
        public const int MaxContentLengthImage = 1000 * 1000; //// 1000 = 1 KB => 1000 KB = 1MB
        public const int MaxContentLengthSolution = 2 * 1000 * 1000; //// 2MB

        public const string PathToCertificate = "~/Images/certificate.jpg";

        public const int NewsShortContentLength = 150;
        public const int TopNewsCount = 6;
        public const int TopForumPostsCount = 10;
        public const int HomePageCacheDuration = 10 * 60; //// 600 seconds i.e. 10 min

        public const string CookieKeyForChat = "client";

        public const int PageSizeCourse = 6;
        public const int MinTextLength = 3;
        public const int DefaultCountOfItemsInNavigation = 5;

        public const string SearchPatternValidation = ".*\\S.{1,}\\S.*";
        public const string WebDateFormat = "dd/MM/yyyy HH:mm";
        public const string DateFormatPlaceholder = "{0:dd/MM/yyyy HH:mm}";
    }
}
