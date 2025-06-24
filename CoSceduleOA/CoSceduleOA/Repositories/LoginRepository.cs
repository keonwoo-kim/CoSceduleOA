using CoScheduleOA.Domain;
using CoScheduleOA.Models;
using Microsoft.EntityFrameworkCore;

namespace CoScheduleOA.Repositories
{
    public sealed class LoginRepository : ILoginRepository
    {
        private readonly CoSceduleOAContext _dbContext;

        public LoginRepository(CoSceduleOAContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserProfile?> GetUserProfileByUserId(string userId)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.UserId == userId);
            return user == null ? null : new UserProfile { Id = user.Id, UserId = user.UserId, UserName = user.UserName, CreatedUtc = user.CreatedUtc, UpdatedUtc = user.UpdatedUtc };
        }

        public async Task<UserProfile> CreateUser(CreateAccountRequestModel createAccountRequest)
        {
            var user = new User
            {
                UserId = createAccountRequest.UserId,
                UserName = createAccountRequest.UserName,
                PasswordHash = createAccountRequest.Password,
                CreatedUtc = DateTime.UtcNow,
                UpdatedUtc = DateTime.UtcNow
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return new UserProfile
            {
                Id = user.Id,
                UserId = user.UserId,
                UserName = user.UserName,
                CreatedUtc = user.CreatedUtc,
                UpdatedUtc = user.UpdatedUtc
            };
        }

        public async Task<User?> GetUserModelByUserId(string userId)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .SingleAsync(u => u.UserId == userId);
            return user;
        }
    }
}
