using CoScheduleOA.Domain;
using CoScheduleOA.Models;

namespace CoScheduleOA.Repositories
{
    public interface ILoginRepository
    {
        public Task<UserProfile?> GetUserProfileByUserId(string userId);
        public Task<UserProfile> CreateUser(CreateAccountRequestModel createAccountRequest);
        public Task<User?> GetUserModelByUserId(string userId);
    }
}
