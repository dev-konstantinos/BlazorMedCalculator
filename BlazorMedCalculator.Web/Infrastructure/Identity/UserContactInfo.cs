using BlazorMedCalculator.Application.Interfaces;
using BlazorMedCalculator.Web.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

public sealed class UserContactInfo : IUserContactInfo
{
    private readonly AuthenticationStateProvider _auth;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserContactInfo(
        AuthenticationStateProvider auth,
        UserManager<ApplicationUser> userManager)
    {
        _auth = auth;
        _userManager = userManager;
    }

    private async Task<ApplicationUser?> GetUserAsync()
    {
        var state = await _auth.GetAuthenticationStateAsync();
        var principal = state.User;

        if (principal.Identity?.IsAuthenticated != true)
            return null;

        return await _userManager.GetUserAsync(principal);
    }

    public async Task<string?> GetEmailAsync()
        => (await GetUserAsync())?.Email;

    public async Task<bool> IsEmailConfirmedAsync()
        => (await GetUserAsync())?.EmailConfirmed ?? false;
}
