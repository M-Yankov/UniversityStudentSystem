namespace UniversityStudentSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Common;
    using CommonModels;
    using CustomAttributes;

    public class Candidate : BaseModel<int>
    {
        private ICollection<Document> documents;

        public Candidate()
        {
            this.documents = new HashSet<Document>();
        }
        
        [DataRange(ModelConstants.MinDate, ModelConstants.MaxDate)]
        public DateTime DateSent { get; set; }

        public bool IsApproved { get; set; }

        public bool IsRejected { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int SpecialtyId { get; set; }

        public virtual Specialty Specialty { get; set; }

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