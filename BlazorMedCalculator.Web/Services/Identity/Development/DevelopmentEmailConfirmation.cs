using BlazorMedCalculator.Web.Data;
using Microsoft.AspNetCore.Identity;

namespace BlazorMedCalculator.Web.Infrastructure.Identity.Development;

public static class DevelopmentEmailConfirmation
{
    public static async Task AutoConfirmEmailsAsync(
        IServiceProvider services,
        IHostEnvironment env)
    {
        if (!env.IsDevelopment())
            return;

        using var scope = services.CreateScope();

        var userManager =
            scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var users = userManager.Users
            .Where(u => !u.EmailConfirmed)
            .ToList();

        foreach (var user in users)
        {
            user.EmailConfirmed = true;
            await userManager.UpdateAsync(user);
        }
    }
}
