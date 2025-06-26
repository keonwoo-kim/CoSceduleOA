using CoScheduleOA.Domain.Entities;

namespace CoScheduleOA.Interfaces.Repositories
{
    public interface IRatingRepository
    {
        /// <summary>
        /// Adds a new rating entity to the data store and returns the added entity.
        /// </summary>
        Task<Rating> AddAsync(Rating entity);

        /// <summary>
        /// Retrieves a rating by its ID. Returns null if the rating does not exist.
        /// </summary>
        Task<Rating?> FindByIdAsync(int id);

        /// <summary>
        /// Retrieves all ratings associated with the specified item ID.
        /// </summary>
        Task<IEnumerable<Rating>> GetByItemAsync(int itemId);

        /// <summary>
        /// Updates the specified rating entity in the data store.
        /// </summary>
        Task UpdateAsync(Rating entity);

        /// <summary>
        /// Deletes the rating with the given ID from the data store.
        /// </summary>
        Task DeleteAsync(int id);
    }
}