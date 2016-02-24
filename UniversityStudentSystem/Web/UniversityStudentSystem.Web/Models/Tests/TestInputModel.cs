namespace UniversityStudentSystem.Web.Models.Tests
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TestInputModel
    {
        [Required]
        public IList<int> Questions { get; set; }
       
        [Required]
        public int TestId { get; set; }
    }
}