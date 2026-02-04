namespace BlazorMedCalculator.Web.Interfaces
{
    // interface for accessing user contact data (email, confirmation)
    public interface IUserContactInfo
    {
        Task<string?> GetEmailAsync();
        Task<bool> IsEmailConfirmedAsync();
    }
}
