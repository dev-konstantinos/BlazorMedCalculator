namespace BlazorMedCalculator.Application.Interfaces
{
    // interface to access information about the current user in business logic
    public interface ICurrentUser
    {
        string? UserId { get; }
        bool IsAuthenticated { get; }
        bool IsInRole(string role);
    }
}
