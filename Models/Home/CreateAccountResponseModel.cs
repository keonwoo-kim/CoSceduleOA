namespace CoScheduleOA.Models.Home
{
    public class CreateAccountResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Msg { get; set; } = string.Empty;
        public AccountDto UserProfile { get; set; } = new AccountDto();
    }
}
