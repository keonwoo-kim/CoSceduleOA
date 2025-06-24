namespace CoScheduleOA.Models
{
    public class CreateAccountResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Msg { get; set; } = string.Empty;
        public UserProfile UserProfile { get; set; } = new UserProfile();
    }
}
