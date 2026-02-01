using BlazorMedCalculator.Web.Data;
using Microsoft.AspNetCore.Identity;

namespace BlazorMedCalculator.Web.Infrastructure.Email.Development;

public sealed class DevIdentityEmailSender
    : IEmailSender<ApplicationUser>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHostEnvironment _env;

    public DevIdentityEmailSender(
        UserManager<ApplicationUser> userManager,
        IHostEnvironment env)
    {
        _userManager = userManager;
        _env = env;
    }

    public async Task SendConfirmationLinkAsync(
        ApplicationUser user,
        string email,
        string confirmationLink)
    {
        if (_env.IsDevelopment())
        {
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);
        }
    }

    public Task SendPasswordResetLinkAsync(
        ApplicationUser user,
        string email,
        string resetLink)
        => Task.CompletedTask;

    public Task SendPasswordResetCodeAsync(
        ApplicationUser user,
        string email,
        string resetCode)
        => Task.CompletedTask;
}
