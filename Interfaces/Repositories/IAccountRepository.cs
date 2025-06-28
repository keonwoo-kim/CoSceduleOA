using CoScheduleOA.Domain.Entities;
using CoScheduleOA.Models.Home;

namespace CoScheduleOA.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Retrieves an account DTO by user ID. Returns null if not found.
        /// </summary>
        Task<AccountDto?> GetAccountByLoginIdAsync(string userId);

        /// <summary>
        /// Retrieves an account DTO by ID. Returns null if not found.
        /// </summary>
        Task<AccountDto?> GetAccountById(int id);

        /// <summary>
        /// Adds a new User entity to the data store and returns the created entity.
        /// </summary>
        Task<User> AddAsync(User user);

        /// <summary>
        /// Checks if the user entity exists by user ID.
        /// </summary>
        Task<bool> Exists(string userId);

        /// <summary>
        /// Retrieves the user entity by user ID. Returns null if the user does not exist.
        /// </summary>
        Task<User?> GetUserModelByUserIdAsync(string userId);
    }
}
