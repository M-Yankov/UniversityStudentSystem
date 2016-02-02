namespace UniversityStudentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class Candidate
    {
        private ICollection<Document> documents;

        public Candidate()
        {
            this.documents = new HashSet<Document>();
        }

        [Key]
        public int Id { get; set; }

        [DataRange("01.01.2016", "01.01.2030")]
        public DateTime DateSent { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int SpecialtyId { get; set; }

        public virtual Specialty Specialty { get; set; }

        public int CandidateId { get; set; }

        public virtual ICollection<Document> Documents
        {
            get
            {
                return this.documents;
            }
            set
            {
                this.documents = value;
            }
        }
    }
}