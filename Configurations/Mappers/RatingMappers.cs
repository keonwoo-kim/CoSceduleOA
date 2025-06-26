using AutoMapper;
using CoScheduleOA.Domain.Entities;
using CoScheduleOA.Models.Ratings;

namespace CoScheduleOA.Configurations.Mappers
{
    public sealed class RatingMappers : Profile
    {
        public RatingMappers()
        {
            CreateMap<RatingCreateModel, Rating>()
                .ForMember(dest => dest.CreatedUtc, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedUtc, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<RatingUpdateModel, Rating>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.UpdatedUtc, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<Rating, RatingDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
        }
    }
}
