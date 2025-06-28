using CoScheduleOA.Domain.Entities;
using CoScheduleOA.Models.Items;
using System.Threading.Tasks;

namespace CoScheduleOA.Interfaces.Services
{
    public interface IItemService
    {
        /// <summary>
        /// Saves a new item if it does not already exist in the database.
        /// </summary>
        Task<int> SaveIfNotExistsAndReturnIdAsync(ItemCreateModel model);

        /// <summary>
        /// Retrieves an item by its source and external ID.
        /// </summary>
        Task<Item?> FindAsync(string source, string externalId);
    }
}