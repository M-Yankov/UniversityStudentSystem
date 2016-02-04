namespace UniversityStudentSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class Document
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int CandidateId { get; set; }

        public virtual Candidate Candidate { get; set; }

        [Required]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string Path { get; set; }

        [DataRange(ModelConstants.MinDate, ModelConstants.MaxDate)]
        public DateTime DateUploaded { get; set; }
    }
}