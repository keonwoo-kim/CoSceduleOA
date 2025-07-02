using CoScheduleOA.Domain.Context;
using CoScheduleOA.Domain.Entities;
using CoScheduleOA.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoScheduleOA.Repositories
{
    public sealed class CommentRepository : ICommentRepository
    {
        private readonly IDbContextFactory<CoScheduleOAContext> _dbContextFactory;

        public CommentRepository(IDbContextFactory<CoScheduleOAContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Comment> AddAsync(Comment entity)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            context.Comments.Add(entity);
            await context.SaveChangesAsync();

            return await context.Comments
                .Include(c => c.User)
                .FirstAsync(c => c.Id == entity.Id);
        }

        public async Task<Comment?> FindByIdAsync(int id)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            return await context.Comments
                .AsNoTracking()
                .Include(c => c.User)
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Comment>> GetByItemAsync(int itemId)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            return await context.Comments
                .AsNoTracking()
                .Where(c => c.ItemId == itemId)
                .Include(c => c.User)
                .ToListAsync();
        }

        public async Task UpdateAsync(Comment entity)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            context.Comments.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            var comment = await context.Comments.FindAsync(id);
            if (comment is null)
            {
                return;
            }
            context.Comments.Remove(comment);
            await context.SaveChangesAsync();
        }
    }
}
