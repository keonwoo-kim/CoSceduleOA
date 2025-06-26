using System.Collections.Generic;
using System.Threading.Tasks;
using CoScheduleOA.Domain.Entities;

namespace CoScheduleOA.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        /// <summary>
        /// Adds a new comment and returns the added comment.
        /// </summary>
        Task<Comment> AddAsync(Comment entity);

        /// <summary>
        /// Returns the comment matching the given ID, or null if not found.
        /// </summary>
        Task<Comment?> FindByIdAsync(int id);

        /// <summary>
        /// Retrieves all comments associated with a specific item.
        /// </summary>
        Task<IEnumerable<Comment>> GetByItemAsync(int itemId);

        /// <summary>
        /// Updates the content of an existing comment.
        /// </summary>
        Task UpdateAsync(Comment entity);

        /// <summary>
        /// Deletes the comment with the specified ID.
        /// </summary>
        Task DeleteAsync(int id);
    }
}