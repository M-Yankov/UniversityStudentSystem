namespace UniversityStudentSystem.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal class UserData
    {
        private const string LastNamesFile = "LNames.txt";
        private const string FirstNamesFile = "FNames.txt";
        private const string EmailsFile = "Emails.txt";
        private const string UsernamesFile = "Usernames.txt";
        private const string CurrentDirectory = "Data/UniversityStudentSystem.Data/";
        private string path;

        private IList<string> firstNames;
        private IList<string> lastNames;
        private IList<string> emais;
        private IList<string> usernames;

        public UserData()
        {
            AppDomain domain = AppDomain.CurrentDomain;
            DirectoryInfo baseDirevtory = Directory.GetParent(domain.BaseDirectory);
            this.path = Path.Combine(baseDirevtory.Parent.Parent.FullName, CurrentDirectory);

            // Should find some dynamic solution
            string pathToUsernames = Path.Combine(this.path, UsernamesFile);
            string pathToEmails = Path.Combine(this.path, EmailsFile);
            string pathToLastNames = Path.Combine(this.path, LastNamesFile);
            string pathToFirstNames = Path.Combine(this.path, FirstNamesFile);

            this.InitializeCollection(ref this.firstNames, pathToFirstNames);
            this.InitializeCollection(ref this.lastNames, pathToLastNames);
            this.InitializeCollection(ref this.emais, pathToEmails);
            this.InitializeCollection(ref this.usernames, pathToUsernames);
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

        private void InitializeCollection(ref IList<string> colletion, string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                colletion = reader.ReadToEnd()
                                    .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                                    .ToList();
            }
        }
    }
}
