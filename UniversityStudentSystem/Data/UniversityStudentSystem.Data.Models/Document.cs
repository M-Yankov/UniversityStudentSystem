namespace UniversityStudentSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Document
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int CandidateId { get; set; }

        public virtual Candidate Candidate { get; set; }

        [Required]
        [MaxLength(500)]
        public string Path { get; set; }

        [DataRange("01.01.2016", "01.01.2030")]
        public DateTime DateUploaded { get; set; }
    }
}