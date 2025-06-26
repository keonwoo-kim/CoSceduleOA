using AutoMapper;
using CoScheduleOA.Domain.Entities;
using CoScheduleOA.Models.Comments;

namespace CoScheduleOA.Configurations.Mappers
{
    public sealed class CommentMappers : Profile
    {
        public CommentMappers()
        {
            CreateMap<CommentCreateModel, Comment>()
                .ForMember(dest => dest.CreatedUtc, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<CommentUpdateModel, Comment>()
                .ForMember(dest => dest.UpdatedUtc, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
        }
    }
}
