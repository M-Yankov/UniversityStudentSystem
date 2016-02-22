namespace UniversityStudentSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common;
    using CommonModels;

    public class Diploma : BaseModel<int>
    {
        private ICollection<User> users;

        public Diploma()
        {
            this.users = new HashSet<User>();
        }
        
        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.NameMaxLength)]
        public string Name { get; set; }

        public int SpecialtyId { get; set; }

        public virtual Specialty Specialty { get; set; }

        [Required]
        [MinLength(ModelConstants.NameMinLength)]
        [MaxLength(ModelConstants.DescriptionMaxLength)]
        public string PathToImage { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}