using CoScheduleOA.Domain;
using CoScheduleOA.Infrastructure.Models;
using CoScheduleOA.Interfaces.Providers;
using CoScheduleOA.Interfaces.Services;
using CoScheduleOA.Models.Home;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoScheduleOA.Services
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly IAwsSecretsProvider _secrets;

        public TokenService(
            IOptions<JwtSettings> jwtSettings,
            IAwsSecretsProvider secrets)
        {
            _jwtSettings = jwtSettings;
            _secrets = secrets;
        }

        public string GenerateToken(AccountDto account)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new("userid", account.UserId),
                new("username", account.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secrets.GetJwtSecret()));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Value.Issuer,
                audience: _jwtSettings.Value.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
