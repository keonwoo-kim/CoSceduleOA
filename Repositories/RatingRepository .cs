using CoScheduleOA.Domain.Context;
using CoScheduleOA.Domain.Entities;
using CoScheduleOA.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoScheduleOA.Repositories
{
    public sealed class RatingRepository : IRatingRepository
    {
        private readonly CoScheduleOAContext _dbContext;

        public RatingRepository(CoScheduleOAContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Rating> AddAsync(Rating entity)
        {
            _dbContext.Ratings.Add(entity);
            await _dbContext.SaveChangesAsync();

            return await _dbContext.Ratings
                .Include(c => c.User)
                .FirstAsync(c => c.Id == entity.Id);
        }

        public async Task<Rating?> FindByIdAsync(int id)
        {
            return await _dbContext.Ratings
                .AsNoTracking()
                .Include(c => c.User)
                .SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Rating>> GetByItemAsync(int itemId)
        {
            return await _dbContext.Ratings
                .AsNoTracking()
                .Include(c => c.User)
                .Where(r => r.ItemId == itemId)
                .ToListAsync();
        }

        public async Task UpdateAsync(Rating entity)
        {
            _dbContext.Ratings.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var r = await _dbContext.Ratings.FindAsync(id);
            if (r is null)
            {
                return;
            }
            _dbContext.Ratings.Remove(r);
            await _dbContext.SaveChangesAsync();
        }
    }
}