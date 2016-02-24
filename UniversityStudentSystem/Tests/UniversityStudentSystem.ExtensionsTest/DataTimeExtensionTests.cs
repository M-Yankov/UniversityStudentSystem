namespace UniversityStudentSystem.ExtensionsTest
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using UniversityStudentSystem.Common.Extensions;

    [TestClass]
    public class DataTimeExtensionTests
    {
        [TestMethod]
        public void ExpectToReturnCorrectResultYearsAgo()
        {
            var date = DateTime.Now.AddYears(-2);
            string result = date.DateTimeAgo();
            string expected = "2 years ago";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ExpectToReturnCorrectResultMonthsAgo()
        {
            //// Imagine this situation: Now is middle of February 2016. 5 months ago is middle of September 2015;
            //// Feb 2016, Jan 2016, Dec 2015, Nov 2015, Oct 2015, Sept 2015 => it's exactly 6 months.
            const int SubstractMonths = 5;
            var date = DateTime.Now.AddMonths(-SubstractMonths);
            string result = date.DateTimeAgo();
            string expected = $"{ SubstractMonths + 1 } months ago";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ExpectToReturnCorrectResultDaysAgo()
        {
            const int SubstractDays = 7;
            var date = DateTime.Now.AddDays(-SubstractDays);
            string result = date.DateTimeAgo();
            string expected = $"{ SubstractDays } days ago";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ExpectToReturnCorrectResultWhenItsYesterday()
        {
            const int SubstractDays = 1;
            var yesterday = DateTime.Now.AddDays(-SubstractDays);
            string result = yesterday.DateTimeAgo();
            string expected = $"{ SubstractDays } day ago";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ExpectToRerurnCorrectResultHoursAgo()
        {
            const int HoursSubstrat = 8;
            var date = DateTime.Now.AddHours(-HoursSubstrat);
            string result = date.DateTimeAgo();
            string expected = $"{ HoursSubstrat } hours ago";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ExpectToReturnCorrectResultWhenTimeIsOneHourAgo()
        {
            const int Hour = 1;
            var beforeOneHour = DateTime.Now.AddHours(-Hour);
            string result = beforeOneHour.DateTimeAgo();
            string expected = $"{ Hour } hour ago";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ExpectToReturnCorrectResultMinutesAgo()
        {
            const int MinutesToSubstract = 20;
            var date = DateTime.Now.AddMinutes(-MinutesToSubstract);
            string result = date.DateTimeAgo();
            string expected = $"{ MinutesToSubstract } minutes ago";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ExpectToReturnCorrectResultWhenItsOneMinuteAgo()
        {
            const int Minute = 1;
            var date = DateTime.Now.AddMinutes(-Minute);
            string result = date.DateTimeAgo();
            string expected = $"{ Minute } minute ago";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ExpectToReturnCorrectResultSecondsAgo()
        {
            const int Seconds = 39;
            var date = DateTime.Now.AddSeconds(-Seconds);
            string result = date.DateTimeAgo();
            string expected = $"{ Seconds } seconds ago";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ExpectToReturnCorrectResultJustNow()
        {
            const int Seconds = 3;
            var date = DateTime.Now.AddSeconds(-Seconds);
            string result = date.DateTimeAgo();
            const string Expected = "just now";
            Assert.AreEqual(Expected, result);
        }

        [TestMethod]
        public void ExpectToReturnCorrectResultDateTimeNow()
        {
            var date = DateTime.Now;
            string result = date.DateTimeAgo();
            const string Expected = "just now";
            Assert.AreEqual(Expected, result);
        }

        [TestMethod]
        public void ExpectToReturnJustNowWhenDateIsInTheFuture()
        {
            var date = DateTime.Now.AddYears(2);
            date.AddSeconds(30);
            string result = date.DateTimeAgo();
            const string Expected = "just now";
            Assert.AreEqual(Expected, result);
        }
    }
}
