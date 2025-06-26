using CoScheduleOA.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CoScheduleOA.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor accessor)
        {
            _httpContextAccessor = accessor;
        }

        public int? Id
        {
            get
            {
                var idString = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                return int.TryParse(idString, out var id) ? id : (int?)null;
            }
        }

        public string? UserId =>
            _httpContextAccessor.HttpContext?.User?.FindFirstValue("UserId");

        public string? UserName =>
            _httpContextAccessor.HttpContext?.User?.FindFirstValue("UserName");

        public bool IsAuthenticated =>
            _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }
}