namespace UniversityStudentSystem.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Helpers;

    internal class UserData
    {
        private IList<string> firstNames;
        private IList<string> lastNames;
        private IList<string> emais;
        private IList<string> usernames;

        public UserData()
        {
            this.InitializeCollection(ref this.firstNames, new RandomFirstNames());
            this.InitializeCollection(ref this.lastNames, new RandomLastNames());
            this.InitializeCollection(ref this.emais, new RandomEmails());
            this.InitializeCollection(ref this.usernames, new RandomUsernames());
        }

        public IList<string> FirstNames
        {
            get
            {
                return this.firstNames;
            }
        }

        public IList<string> LastNames
        {
            get
            {
                return this.lastNames;
            }
        }

        public IList<string> Emais
        {
            get
            {
                return this.emais;
            }
        }

        public IList<string> Usernames
        {
            get
            {
                return this.usernames;
            }
        }

        private void InitializeCollection(ref IList<string> colletion, IRandomData data)
        {
            colletion = data.GetData()
                .Split(
                    new string[] { Environment.NewLine },
                    StringSplitOptions.RemoveEmptyEntries)
                .ToList();
        }
    }
}
