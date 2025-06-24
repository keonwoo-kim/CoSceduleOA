using CoScheduleOA.Models;

namespace CoScheduleOA.Services
{
    public interface ILoginService
    {
        public Task<UserProfile?> GetUserByUserId(string userId);
        public Task<UserProfile> CreateUser(CreateAccountRequestModel userId);
        public Task<bool> VerifyPassword(string userId, string password);
    }
}
