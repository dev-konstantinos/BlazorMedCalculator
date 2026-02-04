using BlazorMedCalculator.Web.Data;
using BlazorMedCalculator.Web.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BlazorMedCalculator.Web.Infrastructure.Email;

public sealed class IdentityEmailSender : IEmailSender<ApplicationUser>
{
    private readonly IEmailService _email;

    public IdentityEmailSender(IEmailService email)
    {
        _email = email;
    }

    public async Task SendConfirmationLinkAsync(
        ApplicationUser user,
        string email,
        string confirmationLink)
    {
        var body = $"""
            <p>Hello!</p>
            <p>Please confirm your email by clicking the link below:</p>
            <p><a href="{confirmationLink}">Confirm email</a></p>
        """;

        await _email.SendAsync(
            email,
            "Confirm your email",
            body);
    }

    public async Task SendPasswordResetLinkAsync(
        ApplicationUser user,
        string email,
        string resetLink)
    {
        var body = $"""
            <p>Password reset requested.</p>
            <p><a href="{resetLink}">Reset password</a></p>
        """;

        await _email.SendAsync(
            email,
            "Reset password",
            body);
    }

    public async Task SendPasswordResetCodeAsync(
        ApplicationUser user,
        string email,
        string resetCode)
    {
        await _email.SendAsync(
            email,
            "Password reset code",
            $"Your reset code: <b>{resetCode}</b>");
    }
}
