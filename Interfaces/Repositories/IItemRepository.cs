using System.Threading.Tasks;
using CoScheduleOA.Domain.Entities;

namespace CoScheduleOA.Interfaces.Repositories
{
    public interface IItemRepository
    {
        /// <summary>
        /// Checks whether an item exists with the specified source and external ID.
        /// </summary>
        Task<bool> ExistsAsync(string source, string externalId);

        /// <summary>
        /// Retrieves an item by source and external ID.
        /// </summary>
        Task<Item?> FindAsync(string source, string externalId);

        /// <summary>
        /// Adds a new item to the database.
        /// </summary>
        Task AddAsync(Item item);
    }
}
