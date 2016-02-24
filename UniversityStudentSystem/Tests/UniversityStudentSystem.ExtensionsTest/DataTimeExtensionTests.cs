using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            const int substractMonths = 5;
            var date = DateTime.Now.AddMonths(-substractMonths);
            string result = date.DateTimeAgo();
            string expected = $"{ substractMonths + 1 } months ago";
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void ExpectToReturnCorrectResultDaysAgo()
        {
            const int substractDays = 7;
            var date = DateTime.Now.AddDays(-substractDays);
            string result = date.DateTimeAgo();
            string expected = $"{ substractDays } days ago";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ExpectToReturnCorrectResultWhenItsYesterday()
        {
            const int substractDays = 1;
            var yesterday = DateTime.Now.AddDays(-substractDays);
            string result = yesterday.DateTimeAgo();
            string expected = $"{ substractDays } day ago";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ExpectToRerurnCorrectResultHoursAgo()
        {
            const int hoursSubstrat = 8;
            var date = DateTime.Now.AddHours(-hoursSubstrat);
            string result = date.DateTimeAgo();
            string expected = $"{ hoursSubstrat } hours ago";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ExpectToReturnCorrectResultWhenTimeIsOneHourAgo()
        {
            const int hour = 1;
            var beforeOneHour = DateTime.Now.AddHours(-hour);
            string result = beforeOneHour.DateTimeAgo();
            string expected = $"{ hour } hour ago";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ExpectToReturnCorrectResultMinutesAgo()
        {
            const int minutesToSubstract = 20;
            var date = DateTime.Now.AddMinutes(-minutesToSubstract);
            string result = date.DateTimeAgo();
            string expected = $"{ minutesToSubstract } minutes ago";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ExpectToReturnCorrectResultWhenItsOneMinuteAgo()
        {
            const int minute = 1;
            var date = DateTime.Now.AddMinutes(-minute);
            string result = date.DateTimeAgo();
            string expected = $"{ minute } minute ago";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ExpectToReturnCorrectResultSecondsAgo()
        {
            const int seconds = 39;
            var date = DateTime.Now.AddSeconds(-seconds);
            string result = date.DateTimeAgo();
            string expected = $"{ seconds } seconds ago";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ExpectToReturnCorrectResultJustNow()
        {
            const int seconds = 3;
            var date = DateTime.Now.AddSeconds(-seconds);
            string result = date.DateTimeAgo();
            const string expected = "just now";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ExpectToReturnCorrectResultDateTimeNow()
        {
            var date = DateTime.Now;
            string result = date.DateTimeAgo();
            const string expected = "just now";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ExpectToReturnJustNowWhenDateIsInTheFuture()
        {
            var date = DateTime.Now.AddYears(2);
            date.AddSeconds(30);
            string result = date.DateTimeAgo();
            const string expected = "just now";
            Assert.AreEqual(expected, result);
        }
    }
}
