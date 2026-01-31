using System.Security.Claims;
using BlazorMedCalculator.Application.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorMedCalculator.Web.Infrastructure.Identity;

// class to access information about the current user in a Blazor app using AuthenticationStateProvider
public sealed class CurrentUser : ICurrentUser
{
    private readonly AuthenticationStateProvider _authStateProvider;

    public CurrentUser(AuthenticationStateProvider authStateProvider)
    {
        _authStateProvider = authStateProvider;
    }

    public string? UserId
    {
        get
        {
            var user = GetUser();
            return user?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }

    public bool IsAuthenticated =>
        GetUser()?.Identity?.IsAuthenticated ?? false;

    public bool IsInRole(string role) =>
        GetUser()?.IsInRole(role) ?? false;

    // helper method to get the current ClaimsPrincipal synchronously
    private ClaimsPrincipal? GetUser()
    {
        var authState = _authStateProvider
            .GetAuthenticationStateAsync()
            .GetAwaiter()
            .GetResult();

        return authState.User;
    }
}
