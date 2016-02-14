﻿namespace UniversityStudentSystem.Common
{
    public class WebConstants
    {
        public const string DefaultImageNews = "/Images/university-default-news.png";
        public const string DefaultImageProfile = "/Images/student-default.png";
        public const string AcceptImageProfileTypes = ".img, .png, .gif, .jpg, .jpeg";
        public const int MaxContentLengthImage = 1000 * 1000; //1000 = 1 KB => 1000 KB = 1MB

        public const int NewsShortContentLength = 150;
        public const int TopNewsCount = 6;
        public const int TopForumPostsCount = 10;
        public const int HomePageCacheDuration = 10 * 60; // 600 seconds i.e. 10 min
    }
}
