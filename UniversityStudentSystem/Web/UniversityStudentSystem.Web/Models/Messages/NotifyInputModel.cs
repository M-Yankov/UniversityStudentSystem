
namespace UniversityStudentSystem.Web.Models.Messages
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Infrastructure.Mapping;
    using UniversityStudentSystem.Data.Models;

    public class NotifyInputModel: IMapTo<Message>
    {
        [Required]
        public DateTime DateSent { get; set; }

        [Required]
        public bool IsViewed { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [MaxLength(ModelConstants.DescriptionMaxLength)]
        public string Content { get; set; }

        public string SenderId { get; set; }

        public int SpecialtyId { get; set; }
    }
}