using AutoMapper;
using CoScheduleOA.Domain.Entities;
using CoScheduleOA.Interfaces.Providers;
using CoScheduleOA.Interfaces.Repositories;
using CoScheduleOA.Interfaces.Services;
using CoScheduleOA.Models.Home;

namespace CoScheduleOA.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly IAwsSecretsProvider _awsSecretsProvider;

        public AccountService(IMapper mapper,
                            IAccountRepository accountRepository,
                            IAwsSecretsProvider awsSecretsProvider)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
            _awsSecretsProvider = awsSecretsProvider;
        }

        public async Task<AccountDto?> GetAccountByLoginId(string userId)
        {
            var result = await _accountRepository.GetAccountByLoginId(userId);
            return result;
        }

        public async Task<AccountDto?> CreateAccount(CreateAccountRequestModel request)
        {
            if (await _accountRepository.Exists(request.UserId))
            {
                return null;
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password + _awsSecretsProvider.GetPepper(), 12);

            var user = new User
            {
                UserId = request.UserId,
                UserName = request.UserName,
                PasswordHash = hashedPassword,
                CreatedUtc = DateTime.UtcNow,
                UpdatedUtc = DateTime.UtcNow
            };

            var newUser = await _accountRepository.AddAsync(user);
            return _mapper.Map<AccountDto>(newUser);
        }

        public async Task<bool> VerifyPassword(string userId, string password)
        {
            var user = await _accountRepository.GetUserModelByUserId(userId);
            if (user is null)
            {
                return false;
            }
            return BCrypt.Net.BCrypt.Verify(password + _awsSecretsProvider.GetPepper(), user.PasswordHash);
        }
    }
}
