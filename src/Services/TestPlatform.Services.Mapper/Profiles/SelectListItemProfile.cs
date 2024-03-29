﻿namespace TestPlatform.Services.Mapper.Profiles
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using TestPlatform.Database.Entities.Authorization;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Database.Entities.Subjects;

    internal class SelectListItemProfile : Profile
    {
        public SelectListItemProfile()
        {
            this.CreateMap<QuestionCopy, SelectListItem>()
                    .ForMember(csli => csli.Text, mo => mo.MapFrom(qtvm => qtvm.OriginalQuestion.Title))
                    .ForMember(csli => csli.Value, mo => mo.MapFrom(qtvm => qtvm.Id));

            this.CreateMap<QuestionType, SelectListItem>()
                    .ForMember(csli => csli.Text, mo => mo.MapFrom(qtvm => qtvm.Name))
                    .ForMember(csli => csli.Value, mo => mo.MapFrom(qtvm => qtvm.Id));

            this.CreateMap<SubjectTag, SelectListItem>()
                    .ForMember(csli => csli.Text, mo => mo.MapFrom(qtvm => qtvm.Name))
                    .ForMember(csli => csli.Value, mo => mo.MapFrom(qtvm => qtvm.Id));

            this.CreateMap<User, SelectListItem>()
                    .ForMember(csli => csli.Text, mo => mo.MapFrom(u => u.Email))
                    .ForMember(csli => csli.Value, mo => mo.MapFrom(u => u.Id));
        }
    }
}
