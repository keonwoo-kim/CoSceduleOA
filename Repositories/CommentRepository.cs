using CoScheduleOA.Domain.Context;
using CoScheduleOA.Domain.Entities;
using CoScheduleOA.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoScheduleOA.Repositories
{
    public sealed class CommentRepository : ICommentRepository
    {
        private readonly CoScheduleOAContext _dbContext;

        public CommentRepository(CoScheduleOAContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Comment> AddAsync(Comment entity)
        {
            _dbContext.Comments.Add(entity);
            await _dbContext.SaveChangesAsync();

            return await _dbContext.Comments
                .Include(c => c.User)
                .FirstAsync(c => c.Id == entity.Id);
        }

        public async Task<Comment?> FindByIdAsync(int id)
        {
            return await _dbContext.Comments
                .AsNoTracking()
                .Include(c => c.User)
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Comment>> GetByItemAsync(int itemId)
        {
            return await _dbContext.Comments
                .AsNoTracking()
                .Where(c => c.ItemId == itemId)
                .Include(c => c.User)
                .ToListAsync();
        }

        public async Task UpdateAsync(Comment entity)
        {
            _dbContext.Comments.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var comment = await _dbContext.Comments.FindAsync(id);
            if (comment is null)
            {
                return;
            }
            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync();
        }
    }
}
