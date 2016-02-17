namespace UniversityStudentSystem.Web.Models.Documents
{
    using System;
    using UniversityStudentSystem.Data.Models;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;

    public class DocumentViewModel : IMapFrom<Document>
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public DateTime DateUploaded { get; set; }
    }
}