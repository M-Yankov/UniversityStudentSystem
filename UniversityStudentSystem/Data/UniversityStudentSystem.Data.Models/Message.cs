namespace UniversityStudentSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataRange(ModelConstants.MinDate, ModelConstants.MaxDate)]
        public DateTime DateSent { get; set; }

        [Required]
        public bool IsViewed { get; set; }

        [Required]
        public string SenderId { get; set; }

        public virtual User Sender { get; set; }

        public string ReceiverId { get; set; }

        public virtual User Receiver { get; set; }
    }
}