using CoScheduleOA.Models.Home;

namespace CoScheduleOA.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateToken(AccountDto user);
    }
}
