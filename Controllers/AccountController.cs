using CoScheduleOA.Domain;
using CoScheduleOA.Interfaces.Services;
using CoScheduleOA.Models.Home;
using CoScheduleOA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoScheduleOA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService, ITokenService tokenService)
        {
            _logger = logger;
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseModel>> Login([FromBody] LoginRequestModel loginRequest)
        {
            if(!await _accountService.VerifyPasswordAsync(loginRequest.UserId, loginRequest.Password))
            {
                return Unauthorized(new LoginResponseModel { IsSuccess = false, Msg = "Invalid Credentials." });
            }
            var user = await _accountService.GetAccountByLoginIdAsync(loginRequest.UserId);
            var token = _tokenService.GenerateToken(user!);
            return Ok(new LoginResponseModel
            {
                IsSuccess = true,
                Msg = "Login Success",
                UserProfile = user!,
                JwtToken = token
            });
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<CreateAccountResponseModel>> CreateAccount([FromBody] CreateAccountRequestModel createAccountRequest)
        {
            if(createAccountRequest.Password != createAccountRequest.ConfirmPassword)
            {
                return Ok(new CreateAccountResponseModel { IsSuccess = false, Msg = "Passwords Did Not Match" });
            }

            var user = await _accountService.CreateAccountAsync(createAccountRequest);

            if (user is null)
            {
                return Ok(new CreateAccountResponseModel { IsSuccess = false, Msg = "User With Same ID Already Exists." });
            }

            return Ok(new CreateAccountResponseModel() { IsSuccess = true, Msg = $"Welcome {user.UserName}." });
        }

        //[Authorize]
        //[HttpGet]
        //public async Task<ActionResult<CreateAccountResponseModel>> CreateAccount(CreateAccountRequestModel createAccountRequest)
        //{
        //    return Ok(token);
        //}
    }
}
