using CoScheduleOA.Domain.Context;
using CoScheduleOA.Domain.Entities;
using CoScheduleOA.Interfaces.Repositories;
using CoScheduleOA.Models.Home;
using Microsoft.EntityFrameworkCore;

namespace CoScheduleOA.Repositories
{
    public sealed class AccountRepository : IAccountRepository
    {
        private readonly CoScheduleOAContext _dbContext;

        public AccountRepository(CoScheduleOAContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AccountDto?> GetAccountByLoginId(string userId)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.UserId == userId);
            return user == null ? null : new AccountDto { Id = user.Id, UserId = user.UserId, UserName = user.UserName, CreatedUtc = user.CreatedUtc, UpdatedUtc = user.UpdatedUtc };
        }

        public async Task<AccountDto?> GetAccountById(int id)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == id);
            return user == null ? null : new AccountDto { Id = user.Id, UserId = user.UserId, UserName = user.UserName, CreatedUtc = user.CreatedUtc, UpdatedUtc = user.UpdatedUtc };
        }

        public async Task<User> AddAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> Exists(string userId)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .AnyAsync(u => u.UserId == userId);
        }



        public async Task<User?> GetUserModelByUserId(string userId)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.UserId == userId);
            return user;
        }
    }
}
