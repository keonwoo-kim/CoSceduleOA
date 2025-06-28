using CoScheduleOA.Domain.Context;
using CoScheduleOA.Domain.Entities;
using CoScheduleOA.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoScheduleOA.Repositories
{
    public sealed class ItemRepository : IItemRepository
    {
        private readonly CoScheduleOAContext _dbContext;

        public ItemRepository(CoScheduleOAContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsAsync(string source, string externalId)
        {
            return await _dbContext.Items
                .AnyAsync(i => i.Source == source && i.ExternalId == externalId);
        }

        public async Task<Item?> FindAsync(string source, string externalId)
        {
            return await _dbContext.Items
                .AsNoTracking()
                .SingleOrDefaultAsync(i => i.Source == source && i.ExternalId == externalId);
        }

        public async Task AddAsync(Item item)
        {
            _dbContext.Items.Add(item);
            await _dbContext.SaveChangesAsync();
        }
    }
}
