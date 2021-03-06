﻿namespace UniversityStudentSystem.Web.Models.Courses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using CoursesTask;
    using Marks;
    using Resources;
    using Semesters;
    using Solutions;
    using Specialties;
    using UniversityStudentSystem.Data.Models;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;
    using Users;

    public class CourseViewModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DisplayFormat(NullDisplayText = "Description was not provided")]
        public string Description { get; set; }

        public ICollection<UserViewModel> Trainers { get; set; }

        public ICollection<TaskViewModel> Tasks { get; set; }

        public ICollection<ResourceViewModel> Resources { get; set; }

        public ICollection<SolutionViewModel> Solutions { get; set; }

        public ICollection<MarkViewModel> Marks { get; set; }

        public SemesterViewModel Semester { get; set; }

        public bool HasActiveTests { get; set; }

        public string Specialty { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Course, CourseViewModel>()
                .ForMember(
                    c => c.Specialty,
                    o => o.MapFrom(corse => corse.Semester.Specialty.Name))
                .ForMember(
                    c => c.HasActiveTests, 
                    o => o.MapFrom(course => course.Tests.Any(test => 
                        test.StartDate < DateTime.Now && DateTime.Now < test.EndDate && test.IsEnabled)));
        }
    }
}