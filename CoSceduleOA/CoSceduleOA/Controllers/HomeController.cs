using CoScheduleOA.Models;
using CoScheduleOA.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoScheduleOA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILoginService _loginService;

        public HomeController(ILogger<HomeController> logger, ILoginService loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel loginRequest)
        {
            var user = await _loginService.GetUserByUserId(loginRequest.UserId);

            if (user is null || await _loginService.VerifyPassword(user.UserId, loginRequest.Password) is false)
                return Unauthorized(new LoginResponseModel { IsSuccess = false, Msg = "Invalid Credentials." });

            return Ok(new LoginResponseModel { IsSuccess = true, Msg = "Login Success", UserProfile = user });
        }

        [HttpPost("CreateAccount")]
        public async Task<ActionResult<CreateAccountResponseModel>> CreateAccount(CreateAccountRequestModel createAccountRequest)
        {
            if(createAccountRequest.Password != createAccountRequest.RePassword)
            {
                return Ok(new CreateAccountResponseModel { IsSuccess = false, Msg = "Passwords Did Not Match" });
            }

            var user = await _loginService.GetUserByUserId(createAccountRequest.UserId);
            if (user is not null)
            {
                return Ok(new CreateAccountResponseModel { IsSuccess = false, Msg = "User With Same ID Already Exists." });
            }

            user = await _loginService.CreateUser(createAccountRequest);
            return Ok(new CreateAccountResponseModel() { IsSuccess = true, Msg = $"Welcome {user.UserName}." });
        }
    }
}
