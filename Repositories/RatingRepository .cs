using CoScheduleOA.Domain.Context;
using CoScheduleOA.Domain.Entities;
using CoScheduleOA.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoScheduleOA.Repositories
{
    public sealed class RatingRepository : IRatingRepository
    {
        private readonly IDbContextFactory<CoScheduleOAContext> _dbContextFactory;

        public RatingRepository(IDbContextFactory<CoScheduleOAContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Rating> AddAsync(Rating entity)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            context.Ratings.Add(entity);
            await context.SaveChangesAsync();

            return await context.Ratings
                .Include(c => c.User)
                .FirstAsync(c => c.Id == entity.Id);
        }

        public async Task<Rating?> FindByIdAsync(int id)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            return await context.Ratings
                .AsNoTracking()
                .Include(c => c.User)
                .SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<bool> Exists(int userId, int itemId)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            return await context.Ratings
                .AsNoTracking()
                .AnyAsync(r => r.UserId == userId && r.ItemId == itemId);
        }

        public async Task<IEnumerable<Rating>> GetByItemAsync(int itemId)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            return await context.Ratings
                .AsNoTracking()
                .Include(c => c.User)
                .Where(r => r.ItemId == itemId)
                .ToListAsync();
        }

        public async Task UpdateAsync(Rating entity)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            context.Ratings.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            var r = await context.Ratings.FindAsync(id);
            if (r is null)
            {
                return;
            }
            context.Ratings.Remove(r);
            await context.SaveChangesAsync();
        }
    }
}