namespace TestPlatform.Services.Mapper.Profiles
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using TestPlatform.Database.Entities.Questions;
    using TestPlatform.Database.Entities.Subjects;

    internal class SelectListItemProfile : Profile
    {
        public SelectListItemProfile()
        {
            this.CreateMap<QuestionType, SelectListItem>()
                    .ForMember(csli => csli.Text, mo => mo.MapFrom(qtvm => qtvm.Name))
                    .ForMember(csli => csli.Value, mo => mo.MapFrom(qtvm => qtvm.Id));

            this.CreateMap<SubjectTag, SelectListItem>()
                    .ForMember(csli => csli.Text, mo => mo.MapFrom(qtvm => qtvm.Name))
                    .ForMember(csli => csli.Value, mo => mo.MapFrom(qtvm => qtvm.Id));
        }
    }
}
