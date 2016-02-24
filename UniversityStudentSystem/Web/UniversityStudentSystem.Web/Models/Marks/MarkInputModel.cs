namespace UniversityStudentSystem.Web.Models.Marks
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using AutoMapper;
    using Common;
    using Data.Models;
    using UniversityStudentSystem.Web.Infrastructure.Mapping;

    public class MarkInputModel : IMapFrom<Mark>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [UIHint("MarkValue")]
        [Range(ModelConstants.MarkMinValue, ModelConstants.MarkMaxValue)]
        public int Value { get; set; }

        [UIHint("MultiLine")]
        [DataType(DataType.MultilineText)]
        [MaxLength(ModelConstants.ContentMaxLength)]
        public string Reason { get; set; }

        [UIHint("UsernameAutoComplete")]
        public string Username { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string UserId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int CourseId { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Mark, MarkInputModel>()
                .ForMember(m => m.Username, o => o.MapFrom(mark => mark.User.UserName));
        }
    }
}