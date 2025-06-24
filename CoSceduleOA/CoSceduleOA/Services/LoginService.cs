using BCrypt.Net;
using CoScheduleOA.Infrastructure;
using CoScheduleOA.Models;
using CoScheduleOA.Repositories;

namespace CoScheduleOA.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly AwsSecretManager _secretManager;

        public LoginService(ILoginRepository loginRepository,
                            AwsSecretManager secretManager)
        {
            _loginRepository = loginRepository;
            _secretManager = secretManager;
        }

        public async Task<UserProfile?> GetUserByUserId(string userId)
        {
            var result = await _loginRepository.GetUserProfileByUserId(userId);
            return result;
        }

        public async Task<UserProfile> CreateUser(CreateAccountRequestModel createAccountRequest)
        {
            createAccountRequest.Password = BCrypt.Net.BCrypt.HashPassword(createAccountRequest.Password + _secretManager.GetPepper(), workFactor: 12);
            return await _loginRepository.CreateUser(createAccountRequest);
        }

        public async Task<bool> VerifyPassword(string userId, string password)
        {
            var user = await _loginRepository.GetUserModelByUserId(userId);
            if (user is null) return false;
            
            return BCrypt.Net.BCrypt.Verify(password + _secretManager.GetPepper(), user.PasswordHash);
        }
    }
}
