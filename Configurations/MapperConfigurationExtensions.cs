using CoScheduleOA.Configurations.Mappers;

namespace CoScheduleOA.Configurations
{
    public static class MapperConfigurationExtensions
    {
        public static IServiceCollection ConfigureMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserMappers));
            services.AddAutoMapper(typeof(CommentMappers));
            services.AddAutoMapper(typeof(RatingMappers));
            return services;
        }
    }
}
