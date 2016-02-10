namespace UniversityStudentSystem.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Common;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    public sealed class Configuration : DbMigrationsConfiguration<UniversityDbContext>
    {
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
            this.SeedTrainers(context, userManager);
            this.SeedConfirmedStudents(context, userManager);
            this.SeedSemesters(context);
            this.SeedCourses(context);
        }

        private void SeedRoles(UniversityDbContext context, RoleManager<IdentityRole> manager)
        {
            int rolesCount = context
                .Roles
                .Count(r => r.Name == RoleConstants.Admin || r.Name == RoleConstants.Trainer);

            if (rolesCount >= SeedConstants.RolesCount)
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

            const string AdminUsername = "admin";
            IdentityRole adminRole = context
                 .Roles
                 .FirstOrDefault(r => r.Name == RoleConstants.Admin && r.Users.Count >= SeedConstants.AdminsCount);

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

            for (int i = 0; i < SeedConstants.AdminsCount; i++)
            {
                User admin = new User
                {
                    FirstName = this.ProvideRandomFirstName(),
                    LastName = this.ProvideRandomLastName(),
                    Email = this.ProvideRandomEmail(),
                    UserName = AdminUsername + ((i == 0) ? string.Empty : i.ToString()),
                    Age = this.ProvideRandomNumber(ModelConstants.MinAge, ModelConstants.MaxAge),
                    FacultyNumber = ModelConstants.FacultyStartNumber + (i + 1),
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
            int count = context.Speciallties.Count();

            if (count >= SeedConstants.SpecialtiesCount)
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
            int count = context.Users.Count(u => u.Status == Status.Pending && !u.UserName.Contains("admin"));

            if (count >= SeedConstants.CandidatesCount)
            {
                return;
            }

            // Keep unique usernames
            HashSet<string> dbUsernames = new HashSet<string>();
            List<Specialty> specialties = context.Speciallties.ToList();

            Document[] initialDocuments = new Document[]
            {
                new Document() { DateUploaded = DateTime.Now, Path = "~/App_Data/Common/CVBG.pdf" },
                new Document() { DateUploaded = DateTime.Now, Path = "~/App_Data/Common/CVFR.pdf" },
                new Document() { DateUploaded = DateTime.Now, Path = "~/App_Data/Common/CVEN.pdf" },
            };

            List<User> candidateStudents = new List<User>();
            int currentFacultyNumber = (int)context.Users.Select(c => c.FacultyNumber).Max();
            for (int i = 0; i < SeedConstants.CandidatesCount; i++)
            {
                // Ensure Usernames will not duplicate! db throws an exception.
                string currentUsername = this.ProvideRandomUsername();
                while (dbUsernames.Contains(currentUsername))
                {
                    currentUsername = this.ProvideRandomUsername();
                }

                User candidateStudent = new User
                {
                    FirstName = this.ProvideRandomFirstName(),
                    LastName = this.ProvideRandomLastName(),
                    Email = this.ProvideRandomEmail(),
                    UserName = currentUsername,
                    Age = this.ProvideRandomNumber(ModelConstants.MinAge, ModelConstants.MaxAge),
                    FacultyNumber = ++currentFacultyNumber,
                    Genre = this.ProvideRandomNumber(ModelConstants.MinAge, ModelConstants.MaxAge) % 2 == 0
                            ? Genre.Female
                            : Genre.Male,
                    DateRegistered = DateTime.Now,
                    Status = Status.Pending
                };

                IdentityResult result = manager.Create(candidateStudent, candidateStudent.UserName);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(
                        "Candidates cannot be added! " + string.Join(" ", result.Errors));
                }

                dbUsernames.Add(currentUsername);
                candidateStudents.Add(candidateStudent);
            }

            context.SaveChanges();

            for (int i = 0; i < SeedConstants.CandidatesCount; i++)
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

        private void SeedTrainers(UniversityDbContext context, UserManager<User> manager)
        {
            IdentityRole roleWithTrainers = context.Roles.FirstOrDefault(r => r.Name == RoleConstants.Trainer && r.Users.Count >= SeedConstants.TrainersCount);

            if (roleWithTrainers != null)
            {
                return;
            }

            // Keep unique usernames --too many queries :(
            HashSet<string> dbUsernames = new HashSet<string>(context.Users.Select(u => u.UserName));
            int currentFacultyNumber = (int)context.Users.Select(c => c.FacultyNumber).Max();

            for (int i = 0; i < SeedConstants.TrainersCount; i++)
            {
                // Ensure Usernames will not duplicate! db throws an exception.
                string currentUsername = this.ProvideRandomUsername();
                while (dbUsernames.Contains(currentUsername))
                {
                    currentUsername = this.ProvideRandomUsername();
                }

                User trainer = new User
                {
                    FirstName = this.ProvideRandomFirstName(),
                    LastName = this.ProvideRandomLastName(),
                    Email = this.ProvideRandomEmail(),
                    UserName = currentUsername,
                    Age = this.ProvideRandomNumber(ModelConstants.MinAge, ModelConstants.MaxAge),
                    FacultyNumber = ++currentFacultyNumber,
                    Genre = this.ProvideRandomNumber(ModelConstants.MinAge, ModelConstants.MaxAge) % 2 == 0
                            ? Genre.Female
                            : Genre.Male,
                    DateRegistered = DateTime.Now,
                    Status = Status.Staff
                };

                IdentityResult result = manager.Create(trainer, trainer.UserName);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException("Trainer cannot be added! " + string.Join(" ", result.Errors));
                }

                result = manager.AddToRole(trainer.Id, RoleConstants.Trainer);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(
                        "Trainer cannot be added to it' role! " + string.Join(" ", result.Errors));
                }

                dbUsernames.Add(currentUsername);
            }

            context.SaveChanges();
        }

        private void SeedConfirmedStudents(UniversityDbContext context, UserManager<User> manager)
        {
            IList<Specialty> specialties = context.Speciallties.ToList();

            if (specialties.Any(s => s.Students.Count > 0))
            {
                return;
            }

            HashSet<string> dbUsernames = new HashSet<string>(context.Users.Select(u => u.UserName));
            int currentFacultyNumber = (int)context.Users.Select(c => c.FacultyNumber).Max();

            for (int z = 0; z < specialties.Count; z++)
            {
                for (int i = 0; i < SeedConstants.StudentsPerSpecialty; i++)
                {
                    string currentUsername = this.ProvideRandomUsername();
                    while (dbUsernames.Contains(currentUsername))
                    {
                        currentUsername = this.ProvideRandomUsername();
                    }

                    User student = new User
                    {
                        FirstName = this.ProvideRandomFirstName(),
                        LastName = this.ProvideRandomLastName(),
                        Email = this.ProvideRandomEmail(),
                        UserName = currentUsername,
                        Age = this.ProvideRandomNumber(ModelConstants.MinAge, ModelConstants.MaxAge),
                        FacultyNumber = ++currentFacultyNumber,
                        Genre = this.ProvideRandomNumber(ModelConstants.MinAge, ModelConstants.MaxAge) % 2 == 0
                                ? Genre.Female
                                : Genre.Male,
                        DateRegistered = DateTime.Now,
                        Status = Status.Confirmed
                    };

                    IdentityResult result = manager.Create(student, student.UserName);
                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException(
                            "Student cannot be added! " + string.Join(" ", result.Errors));
                    }

                    specialties[z].Students.Add(student);
                    dbUsernames.Add(student.UserName);
                }

                context.SaveChanges();
            }
        }

        private void SeedSemesters(UniversityDbContext context)
        {
            IList<Specialty> specialties = context.Speciallties.ToList();

            if (specialties.Any(s => s.Semesters.Count > 0))
            {
                return;
            }

            // For each specialty there are different semesters. Only for seed they will have same property values.
            for (int i = 0; i < specialties.Count; i++)
            {
                specialties[i].Semesters.Add(new Semester()
                {
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(2),
                    Name = SeedConstants.FirstNameModule,
                    Fee = SeedConstants.FirstModuleFee,
                });

                specialties[i].Semesters.Add(new Semester()
                {
                    StartDate = DateTime.Now.AddMonths(4),
                    EndDate = DateTime.Now.AddMonths(6),
                    Name = SeedConstants.SecondModule,
                    Fee = SeedConstants.SecondModuleFee,
                });

                specialties[i].Semesters.Add(new Semester()
                {
                    StartDate = DateTime.Now.AddMonths(8),
                    EndDate = DateTime.Now.AddMonths(10),
                    Name = SeedConstants.ThirdModule,
                    Fee = SeedConstants.ThirdModuleFee,
                });
            }

            context.SaveChanges();
        }

        private void SeedCourses(UniversityDbContext context)
        {
            if (context.Courses.Any())
            {
                return;
            }

            // Hell ↓
            var trainerRole = context.Roles.FirstOrDefault(r => r.Name == RoleConstants.Trainer);
            IList<User> trainers = context.Users.Where(u => u.Roles.Any(r => r.RoleId == trainerRole.Id)).ToList();

            this.SeedCoursesForSoftware(context, SpecialtiesConstants.SoftwareEngineering, trainers);
            this.SeedCoursesForHardware(context, SpecialtiesConstants.HardwareEngineering, trainers);
            this.SeedCoursesForInfomationTechnologies(context, SpecialtiesConstants.InformationTechnologies, trainers);
            this.SeedCoursesForBusiness(context, SpecialtiesConstants.BusinessManagement, trainers);
            this.SeedCoursesForEconomics(context, SpecialtiesConstants.Economics, trainers);
        }

        private void SeedCoursesForEconomics(UniversityDbContext context, string specialtyName, IList<User> trainers)
        {
            Semester firstSemSoft = this.GetSemester(
                context,
                SeedConstants.FirstNameModule,
                specialtyName);

            firstSemSoft.Courses.Add(this.GetCourse(CoursersConstants.Economics, trainers));
            firstSemSoft.Courses.Add(this.GetCourse(CoursersConstants.Macroeconomics, trainers));
            firstSemSoft.Courses.Add(this.GetCourse(CoursersConstants.Marketing, trainers));

            Semester secondSemSoft = this.GetSemester(
                context,
                SeedConstants.SecondModule,
                specialtyName);

            secondSemSoft.Courses.Add(this.GetCourse(CoursersConstants.EconomicsOfIndustry, trainers));
            secondSemSoft.Courses.Add(this.GetCourse(CoursersConstants.EconomicsOfTourism, trainers));
            secondSemSoft.Courses.Add(this.GetCourse(CoursersConstants.EconomicsOfTrade, trainers));

            Semester thirdSemSoft = this.GetSemester(
                context,
                SeedConstants.ThirdModule,
                specialtyName);

            thirdSemSoft.Courses.Add(this.GetCourse(CoursersConstants.RealEstateEconomics, trainers));
            thirdSemSoft.Courses.Add(this.GetCourse(CoursersConstants.StatisticsAndEconometrics, trainers));

            context.SaveChanges();
        }

        private void SeedCoursesForBusiness(UniversityDbContext context, string specialtyName, IList<User> trainers)
        {
            Semester firstSemSoft = this.GetSemester(
                context,
                SeedConstants.FirstNameModule,
                specialtyName);

            firstSemSoft.Courses.Add(this.GetCourse(CoursersConstants.BusinessAdministration, trainers));
            firstSemSoft.Courses.Add(this.GetCourse(CoursersConstants.InternationalRelations, trainers));
            firstSemSoft.Courses.Add(this.GetCourse(CoursersConstants.BusinessEconomics, trainers));

            Semester secondSemSoft = this.GetSemester(
                context,
                SeedConstants.SecondModule,
                specialtyName);

            secondSemSoft.Courses.Add(this.GetCourse(CoursersConstants.BusinessInformatics, trainers));
            secondSemSoft.Courses.Add(this.GetCourse(CoursersConstants.PublicAdministration, trainers));
            secondSemSoft.Courses.Add(this.GetCourse(CoursersConstants.Sociology, trainers));

            Semester thirdSemSoft = this.GetSemester(
                context,
                SeedConstants.ThirdModule,
                specialtyName);

            thirdSemSoft.Courses.Add(this.GetCourse(CoursersConstants.RegionalDevelopment, trainers));
            thirdSemSoft.Courses.Add(this.GetCourse(CoursersConstants.BusinessLogistics, trainers));

            context.SaveChanges();
        }

        private void SeedCoursesForInfomationTechnologies(UniversityDbContext context, string specialtyName, IList<User> trainers)
        {
            Semester firstSemSoft = this.GetSemester(
                context,
                SeedConstants.FirstNameModule,
                specialtyName);

            firstSemSoft.Courses.Add(this.GetCourse(CoursersConstants.InformationalSystems, trainers));
            firstSemSoft.Courses.Add(this.GetCourse(CoursersConstants.LinearMaths, trainers));
            firstSemSoft.Courses.Add(this.GetCourse(CoursersConstants.AdvancedMathematics, trainers));

            Semester secondSemSoft = this.GetSemester(
                context,
                SeedConstants.SecondModule,
                specialtyName);

            secondSemSoft.Courses.Add(this.GetCourse(CoursersConstants.ArtificialIntelligence, trainers));
            secondSemSoft.Courses.Add(this.GetCourse(CoursersConstants.DataBaseTechnologies, trainers));
            secondSemSoft.Courses.Add(this.GetCourse(CoursersConstants.ProcessAndModelingSystems, trainers));

            Semester thirdSemSoft = this.GetSemester(
                context,
                SeedConstants.ThirdModule,
                specialtyName);

            thirdSemSoft.Courses.Add(this.GetCourse(CoursersConstants.RouterConfiguration, trainers));
            thirdSemSoft.Courses.Add(this.GetCourse(CoursersConstants.OperatingSystems, trainers));
            thirdSemSoft.Courses.Add(this.GetCourse(CoursersConstants.LogicProgramming, trainers));

            context.SaveChanges();
        }

        private void SeedCoursesForHardware(UniversityDbContext context, string specialtyName, IList<User> trainers)
        {
            Semester firstSemSoft = this.GetSemester(
                context,
                SeedConstants.FirstNameModule,
                specialtyName);

            firstSemSoft.Courses.Add(this.GetCourse(CoursersConstants.MicroprocessorsAndApplications, trainers));
            firstSemSoft.Courses.Add(this.GetCourse(CoursersConstants.ElectricalEngineering, trainers));

            Semester secondSemSoft = this.GetSemester(
                context,
                SeedConstants.SecondModule,
                specialtyName);

            secondSemSoft.Courses.Add(this.GetCourse(CoursersConstants.NetworkAdministration, trainers));
            secondSemSoft.Courses.Add(this.GetCourse(CoursersConstants.DigitalCircuitFaultDetection, trainers));

            Semester thirdSemSoft = this.GetSemester(
                context,
                SeedConstants.ThirdModule,
                specialtyName);

            thirdSemSoft.Courses.Add(this.GetCourse(CoursersConstants.MicrocomputerDesign, trainers));
            thirdSemSoft.Courses.Add(this.GetCourse(CoursersConstants.InfrastructureEngineering, trainers));

            context.SaveChanges();
        }

        private void SeedCoursesForSoftware(UniversityDbContext context, string specialtyName, IList<User> trainers)
        {
            Semester firstSemSoft = this.GetSemester(
                context,
                SeedConstants.FirstNameModule,
                specialtyName);

            firstSemSoft.Courses.Add(this.GetCourse(CoursersConstants.CPlusPlus, trainers));
            firstSemSoft.Courses.Add(this.GetCourse(CoursersConstants.CSharp, trainers));
            firstSemSoft.Courses.Add(this.GetCourse(CoursersConstants.OOP, trainers));

            Semester secondSemSoft = this.GetSemester(
                context,
                SeedConstants.SecondModule,
                specialtyName);

            secondSemSoft.Courses.Add(this.GetCourse(CoursersConstants.JSBasic, trainers));
            secondSemSoft.Courses.Add(this.GetCourse(CoursersConstants.JSDom, trainers));
            secondSemSoft.Courses.Add(this.GetCourse(CoursersConstants.JSAdvanced, trainers));

            Semester thirdSemSoft = this.GetSemester(
                context,
                SeedConstants.ThirdModule,
                specialtyName);

            thirdSemSoft.Courses.Add(this.GetCourse(CoursersConstants.ASPWebApi, trainers));
            thirdSemSoft.Courses.Add(this.GetCourse(CoursersConstants.ASPWebForms, trainers));
            thirdSemSoft.Courses.Add(this.GetCourse(CoursersConstants.ASPMVC, trainers));

            context.SaveChanges();
        }

        private Course GetCourse(string name , IList<User> trainers)
        {
            return new Course()
            {
                Name = name,
                Trainers = new HashSet<User>() { trainers[this.ProvideRandomNumber(0, trainers.Count - 1)] }
            };
        }

        private Semester GetSemester(UniversityDbContext context, string fromModule, string specialtyName)
        {
            return context.Semesters
                .FirstOrDefault(c =>
                    c.Name == fromModule &&
                    c.Specialty.Name == specialtyName);
        }

        // TODO: Separate Random Provider
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
