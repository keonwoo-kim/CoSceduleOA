using CoScheduleOA.Domain.Context;
using CoScheduleOA.Domain.Entities;
using CoScheduleOA.Interfaces.Repositories;
using CoScheduleOA.Models.Home;
using Microsoft.EntityFrameworkCore;

namespace CoScheduleOA.Repositories
{
    public sealed class AccountRepository : IAccountRepository
    {
        private readonly IDbContextFactory<CoScheduleOAContext> _dbContextFactory;

        public AccountRepository(IDbContextFactory<CoScheduleOAContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<AccountDto?> GetAccountByLoginIdAsync(string userId)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            var user = await context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.UserId == userId);

            return user == null ? null : new AccountDto
            {
                Id = user.Id,
                UserId = user.UserId,
                UserName = user.UserName,
                CreatedUtc = user.CreatedUtc,
                UpdatedUtc = user.UpdatedUtc
            };
        }

        public async Task<AccountDto?> GetAccountById(int id)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            var user = await context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == id);

            return user == null ? null : new AccountDto
            {
                Id = user.Id,
                UserId = user.UserId,
                UserName = user.UserName,
                CreatedUtc = user.CreatedUtc,
                UpdatedUtc = user.UpdatedUtc
            };
        }

        public async Task<User> AddAsync(User user)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> Exists(string userId)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            return await context.Users
                .AsNoTracking()
                .AnyAsync(u => u.UserId == userId);
        }

        public async Task<User?> GetUserModelByUserIdAsync(string userId)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            return await context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.UserId == userId);
        }
    }
}
