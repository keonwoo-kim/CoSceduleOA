using CoScheduleOA.Models.Home;
using AutoMapper;
using CoScheduleOA.Domain.Entities;

namespace CoScheduleOA.Configurations.Mappers
{
    public sealed class UserMappers : Profile
    {
        public UserMappers()
        {
            CreateMap<CreateAccountRequestModel, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedUtc, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedUtc, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<User, AccountDto>();
        }
    }
}
