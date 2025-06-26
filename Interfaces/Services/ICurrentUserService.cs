namespace CoScheduleOA.Interfaces.Services
{
    public interface ICurrentUserService
    {
        int? Id { get; }
        string? UserId { get; }
        string? UserName { get; }
        bool IsAuthenticated { get; }
    }
}
