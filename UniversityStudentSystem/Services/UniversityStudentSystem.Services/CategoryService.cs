namespace UniversityStudentSystem.Services
{
    using System;
    using System.Linq;
    using Data.Repositories;
    using UniversityStudentSystem.Data.Models;
    using UniversityStudentSystem.Services.Contracts;

    public class CategoryService : ICategoryService
    {
        private IRepository<Category> categoriesRepository;

        public CategoryService(IRepository<Category> categories)
        {
            this.categoriesRepository = categories;
        }

        public IQueryable<Category> GetAll()
        {
            return this.categoriesRepository.All();
        }
    }
}
