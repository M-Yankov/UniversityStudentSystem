namespace UniversityStudentSystem.Data.Migrations.Helpers
{
    using System;

    /// <summary>
    /// Information gathered from https://randomuser.me/
    /// </summary>
    public class RandomUserDataProvider
    {
        private UserData userData;
        private static Random random = new Random();

        public RandomUserDataProvider()
        {
            this.userData = new UserData();
        }

        internal string ProvideRandomFirstName()
        {
            int index = random.Next(0, this.userData.FirstNames.Count);
            string firstName = this.userData.FirstNames[index];
            string firstLetterUppercase = firstName[0].ToString().ToUpper();
            return firstLetterUppercase + firstName.Substring(1);
        }

        internal string ProvideRandomLastName()
        {
            int index = random.Next(0, this.userData.FirstNames.Count);
            string lastName = this.userData.LastNames[index];
            string firstLetterUppercase = lastName[0].ToString().ToUpper();
            return firstLetterUppercase + lastName.Substring(1);
        }

        internal string ProvideRandomEmail()
        {
            int index = random.Next(0, this.userData.FirstNames.Count);
            return this.userData.Emais[index];
        }

        internal string ProvideRandomUsername()
        {
            int index = random.Next(0, this.userData.FirstNames.Count);
            return this.userData.Usernames[index];
        }

        internal int ProvideRandomNumber(int min, int max)
        {
            return random.Next(min, max + 1);
        }
    }
}
