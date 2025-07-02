using CoScheduleOA.Domain.Context;
using CoScheduleOA.Domain.Entities;
using CoScheduleOA.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoScheduleOA.Repositories
{
    public sealed class ItemRepository : IItemRepository
    {
        private readonly IDbContextFactory<CoScheduleOAContext> _dbContextFactory;

        public ItemRepository(IDbContextFactory<CoScheduleOAContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<bool> ExistsAsync(string source, string externalId)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            return await context.Items
                .AnyAsync(i => i.Source == source && i.ExternalId == externalId);
        }

        public async Task<Item?> FindAsync(string source, string externalId)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            return await context.Items
                .AsNoTracking()
                .SingleOrDefaultAsync(i => i.Source == source && i.ExternalId == externalId);
        }

        public async Task AddAsync(Item item)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            context.Items.Add(item);
            await context.SaveChangesAsync();
        }
    }
}
