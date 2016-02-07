namespace UniversityStudentSystem.Data.Migrations
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using Common;
    using System;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    public sealed class Configuration : DbMigrationsConfiguration<UniversityDbContext>
    {
        private const string PathToLastNames = "LNames.txt";
        private const string PathToFirstNames = "FNames.txt";
        private const string PathToEmails = "LNames.txt";
        private const string PathToUsernames = "Usernames.txt";
        private const string CurrentDirectory = "Data/UniversityStudentSystem.Data/";

        private static Random random = new Random();

        private UserData userData;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(UniversityDbContext context)
        {
            this.userData = new UserData();

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);

            this.SeedRoles(context, roleManager);
            this.SeedAdmins(context, userManager);
            this.SeedSpecialties(context);
            this.SeedCandidates(context, userManager);
        }

        private void SeedRoles(UniversityDbContext context, RoleManager<IdentityRole> manager)
        {
            const int RolesCount = 2;

            int rolesCount = context
                .Roles
                .Count(r => r.Name == RoleConstants.Admin || r.Name == RoleConstants.Trainer);

            if (rolesCount == RolesCount)
            {
                return;
            }

            IdentityRole[] roles = new IdentityRole[]
            {
                new IdentityRole(RoleConstants.Admin),
                new IdentityRole(RoleConstants.Trainer)
            };

            for (int i = 0; i < roles.Length; i++)
            {
                IdentityResult result = manager.Create(roles[i]);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException("Roles cannot be saved! " + string.Join(" ", result.Errors));
                }
            }

            context.SaveChanges();
        }

        private void SeedAdmins(UniversityDbContext context, UserManager<User> manager)
        {
            const int AdminsCount = 3;
            const string AdminUsername = "admin";
            IdentityRole adminRole = context
                 .Roles
                 .FirstOrDefault(r => r.Name == RoleConstants.Admin && r.Users.Count == AdminsCount);

            if (adminRole != null)
            {
                return;
            }

            manager.PasswordValidator = new PasswordValidator()
            {
                RequireDigit = false,
                RequiredLength = 5,
                RequireLowercase = false,
                RequireNonLetterOrDigit = false,
                RequireUppercase = false
            };

            for (int i = 0; i < AdminsCount; i++)
            {
                User admin = new User
                {
                    FirstName = this.ProvideRandomFirstName(),
                    LastName = this.ProvideRandomLastName(),
                    Email = this.ProvideRandomEmail(),
                    UserName = AdminUsername + ((i == 0) ? string.Empty : i.ToString()),
                    Age = this.ProvideRandomNumber(ModelConstants.MinAge, ModelConstants.MaxAge),
                    FacultyNumber = ModelConstants.FacultyStartNumber,
                    Genre = Genre.NotSpecified,
                    DateRegistered = DateTime.Now,
                    Status = Status.Staff
                };

                IdentityResult result = manager.Create(admin, admin.UserName);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException("Admins cannot be added! " + string.Join(" ", result.Errors));
                }

                result = manager.AddToRole(admin.Id, RoleConstants.Admin);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(
                        "Admins cannot be added to role! " + string.Join(" ", result.Errors));
                }
            }

            context.SaveChanges();
        }

        private void SeedSpecialties(UniversityDbContext context)
        {
            const int SpecialtiesCount = 5;
            int count = context.Speciallties.Count();

            if (count == SpecialtiesCount)
            {
                return;
            }

            Specialty[] specialties = new Specialty[]
            {
                new Specialty()
                {
                    Name = SpecialtiesConstants.InformationTechnologies,
                    Description = SpecialtiesConstants.InformationTechnologiesDescription
                },
                new Specialty()
                {
                    Name = SpecialtiesConstants.HardwareEngineering,
                    Description = SpecialtiesConstants.HardwareEngineeringDescription
                },
                new Specialty()
                {
                    Name = SpecialtiesConstants.SoftwareEngineering,
                    Description = SpecialtiesConstants.SoftwareEngineeringDescriptoion
                },
                new Specialty()
                {
                    Name = SpecialtiesConstants.Economics,
                    Description = SpecialtiesConstants.EconomicsDescription
                },
                new Specialty()
                {
                    Name = SpecialtiesConstants.BusinessManagement,
                    Description = SpecialtiesConstants.BusinessManagement
                },
            };

            for (int i = 0; i < specialties.Length; i++)
            {
                context.Speciallties.Add(specialties[i]);
            }

            context.SaveChanges();
        }

        private void SeedCandidates(UniversityDbContext context, UserManager<User> manager)
        {
            const int CandidatesCount = 50;
            int count = context.Users.Count(u => u.Status == Status.Pending && !u.UserName.Contains("admin"));

            if (count == CandidatesCount)
            {
                return;
            }

            List<Specialty> specialties = context.Speciallties.ToList();

            Document[] initialDocuments = new Document[]
            {
                new Document() { DateUploaded = DateTime.Now, Path = "~/App_Data/Common/CVBG.pdf" },
                new Document() { DateUploaded = DateTime.Now, Path = "~/App_Data/Common/CVFR.pdf" },
                new Document() { DateUploaded = DateTime.Now, Path = "~/App_Data/Common/CVEN.pdf" },
            };

            List<User> candidateStudents = new List<User>();

            for (int i = 0; i < CandidatesCount; i++)
            {
                User candidateStudent = new User
                {
                    FirstName = this.ProvideRandomFirstName(),
                    LastName = this.ProvideRandomLastName(),
                    Email = this.ProvideRandomEmail(),
                    UserName = this.ProvideRandomUsername(),
                    Age = this.ProvideRandomNumber(ModelConstants.MinAge, ModelConstants.MaxAge),
                    FacultyNumber = ModelConstants.FacultyStartNumber,
                    Genre = this.ProvideRandomNumber(ModelConstants.MinAge, ModelConstants.MaxAge) % 2 == 0
                            ? Genre.Female 
                            : Genre.Male,
                    DateRegistered = DateTime.Now,
                    Status = Status.Pending
                };

                IdentityResult result = manager.Create(candidateStudent, candidateStudent.UserName);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException("Admins cannot be added! " + string.Join(" ", result.Errors));
                }

                candidateStudents.Add(candidateStudent);
            }

            context.SaveChanges();

            for (int i = 0; i < CandidatesCount; i++)
            {
                Candidate candidature = new Candidate
                {
                    UserId = candidateStudents[i].Id,
                    DateSent = DateTime.Now,
                    Documents = new List<Document> { initialDocuments[i % initialDocuments.Length] },
                    SpecialtyId = specialties[i % specialties.Count].Id,
                };

                context.Candidates.Add(candidature);
            }

            context.SaveChanges();
        }

        private string ProvideRandomFirstName()
        {
            int index = random.Next(0, this.userData.FirstNames.Count);
            string firstName = this.userData.FirstNames[index];
            string firstLetterUppercase = firstName[0].ToString().ToUpper();
            return firstLetterUppercase + firstName.Substring(1);
        }

        private string ProvideRandomLastName()
        {
            int index = random.Next(0, this.userData.FirstNames.Count);
            string lastName = this.userData.LastNames[index];
            string firstLetterUppercase = lastName[0].ToString().ToUpper();
            return firstLetterUppercase + lastName.Substring(1);
        }

        private string ProvideRandomEmail()
        {
            int index = random.Next(0, this.userData.FirstNames.Count);
            return this.userData.Emais[index];
        }

        private string ProvideRandomUsername()
        {
            int index = random.Next(0, this.userData.FirstNames.Count);
            return this.userData.Usernames[index];
        }

        private int ProvideRandomNumber(int min, int max)
        {
            return random.Next(min, max + 1);
        }
    }
}
