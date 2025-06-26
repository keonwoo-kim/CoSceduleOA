using CoScheduleOA.Infrastructure.Models;
using CoScheduleOA.Interfaces.Providers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CoScheduleOA.Configurations
{
    public static class JwtConfigurationExtensions
    {
        public static IServiceCollection ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(
            configuration.GetSection("JwtSettings"));

            services
              .AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
              .Configure<IOptions<JwtSettings>, IAwsSecretsProvider>(
                (options, jwtOpt, aws) =>
                {
                    var jwt = jwtOpt.Value;
                    var secretKey = aws.GetJwtSecret();

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwt.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwt.Audience,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                                                   Encoding.UTF8.GetBytes(secretKey))
                    };
                });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();
            return services;
        }
    }
}
