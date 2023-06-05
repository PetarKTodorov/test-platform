namespace TestPlatform.DTOs.BindingModels.Common
{
    using System;
    using TestPlatform.Database.Entities;
    using TestPlatform.Services.Mapper.Interfaces;

    public class BaseBM : IMapFrom<BaseEntity>, IMapTo<BaseEntity>
    {
        public Guid Id { get; set; }

        public Guid CreatedBy { get; set; }
    }
}
