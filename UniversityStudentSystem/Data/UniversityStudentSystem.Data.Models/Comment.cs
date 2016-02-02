﻿namespace UniversityStudentSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataRange("01.01.2016", "01.01.2030")]
        public DateTime DatePosted { get; set; }

        [Required]
        [MinLength(3)]
        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int ForumPostId { get; set; }

        public virtual ForumPost ForumPost { get; set; }
    }
}
