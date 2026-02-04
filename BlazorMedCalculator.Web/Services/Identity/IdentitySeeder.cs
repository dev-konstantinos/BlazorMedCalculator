using BlazorMedCalculator.Web.Data;
using Microsoft.AspNetCore.Identity;

namespace BlazorMedCalculator.Web.Infrastructure.Identity;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var roleManager = scope.ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();

        var userManager = scope.ServiceProvider
            .GetRequiredService<UserManager<ApplicationUser>>();

        // 1. Roles list
        foreach (var role in new[] { Roles.Admin, Roles.User })
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                var result = await roleManager.CreateAsync(
                    new IdentityRole(role));

                if (!result.Succeeded)
                    throw new InvalidOperationException(
                        $"Failed to create role {role}");
            }
        }

        // 2. Admin user
        const string adminEmail = "admin@localhost.com"; // only for demo/testing in development
        const string adminPassword = "Admin123!"; // only for demo/testing in development

        var admin = await userManager.FindByEmailAsync(adminEmail);
        if (admin == null)
        {
            admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var createResult =
                await userManager.CreateAsync(admin, adminPassword);

            if (!createResult.Succeeded)
                throw new InvalidOperationException(
                    string.Join("; ",
                        createResult.Errors.Select(e => e.Description)));

            var roleResult =
                await userManager.AddToRoleAsync(admin, Roles.Admin);

            if (!roleResult.Succeeded)
                throw new InvalidOperationException(
                    $"Failed to assign Admin role");
        }
    }
}
