namespace BlazorMedCalculator.Application.Common.Interfaces
{
    // Interface to access information about the current user from the business logic layer
    public interface ICurrentUser
    {
        string? UserId { get; }
        bool IsAuthenticated { get; }
        bool IsInRole(string role);
    }
}
