using CoScheduleOA.Models.Home;

namespace CoScheduleOA.Interfaces.Services
{
    public interface IAccountService
    {
        /// <summary>
        /// Retrieves an account DTO by user ID. Returns null if the user does not exist.
        /// </summary>
        Task<AccountDto?> GetAccountByLoginIdAsync(string userId);

        /// <summary>
        /// Creates a new user account using the provided request model and returns the created account DTO.
        /// </summary>
        Task<AccountDto?> CreateAccountAsync(CreateAccountRequestModel createAccountRequest);

        /// <summary>
        /// Verifies whether the provided password matches the stored credentials for the given user ID.
        /// </summary>
        Task<bool> VerifyPasswordAsync(string userId, string password);
    }
}