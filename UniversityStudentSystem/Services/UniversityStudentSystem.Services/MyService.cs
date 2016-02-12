using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityStudentSystem.Data;
using UniversityStudentSystem.Data.Models;
using UniversityStudentSystem.Data.Repositories;
using UniversityStudentSystem.Services.Contracts;

namespace UniversityStudentSystem.Services
{
    public class MyService : IMyService
    {
        private IRepository<User, string> users;
        private IRepository<Course> courses;

        public MyService(
            IRepository<User, string> usersRepo,
            IRepository<Course> c
            )
        {
            this.users = usersRepo;
            this.courses = c;
        }

        public User Random()
        {
            return new User() { FirstName = "Test" };
        }

        public Course Co()
        {
            return new Course() { Name = "testCourse" };
        }
    }
}
