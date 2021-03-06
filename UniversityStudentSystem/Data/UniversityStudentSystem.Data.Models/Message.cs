﻿namespace UniversityStudentSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Common;
    using CommonModels;
    using CustomAttributes;

    public class Message : BaseModel<int>
    {
        [Required]
        [DataRange(ModelConstants.MinDate, ModelConstants.MaxDate)]
        public DateTime DateSent { get; set; }

        [Required]
        public bool IsViewed { get; set; }

        [Required]
        [MaxLength(ModelConstants.DescriptionMaxLength)]
        public string Content { get; set; }

        [Required]
        public string SenderId { get; set; }

        public virtual User Sender { get; set; }

        public string ReceiverId { get; set; }

        public virtual User Receiver { get; set; }
    }
}