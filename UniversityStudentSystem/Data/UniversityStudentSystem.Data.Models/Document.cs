namespace UniversityStudentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class Document
    {
        private ICollection<Candidate> candidatures;

        public Document()
        {
            this.candidatures = new HashSet<Candidate>();
        }

        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string Path { get; set; }

        [DataRange(ModelConstants.MinDate, ModelConstants.MaxDate)]
        public DateTime DateUploaded { get; set; }

        public ICollection<Candidate> Candidatures
        {
            get
            {
                return this.candidatures;
            }
            set
            {
                this.candidatures = value;
            }
        }
    }
}