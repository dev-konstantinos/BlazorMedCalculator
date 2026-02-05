using BlazorMedCalculator.Web.Components;
using BlazorMedCalculator.Web.Components.Account;
using BlazorMedCalculator.Web.Data;
using BlazorMedCalculator.Web.Endpoints;
using BlazorMedCalculator.Web.Infrastructure.Content;
using BlazorMedCalculator.Web.Infrastructure.Email;
using BlazorMedCalculator.Web.Infrastructure.Email.Development;
using BlazorMedCalculator.Web.Infrastructure.Identity;
using BlazorMedCalculator.Web.Infrastructure.Pdf;
using BlazorMedCalculator.Web.Interfaces;
using BlazorMedCalculator.Web.Services.Search;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorMedCalculator.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

            // used for [Authorize] in Razor components
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("CanAccessPdf", policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
            });

            builder.Services.AddScoped<ICurrentUser, CurrentUser>(); // register CurrentUser to access user info in business logic

            builder.Services.AddScoped<IContentService, FileContentService>(); // register service to read markdown

            builder.Services.AddScoped<IContentEditorService, FileContentEditorService>(); // register service to edit markdown

            builder.Services.AddScoped<IUserContactInfo, UserContactInfo>(); // register additional service to check account info

            builder.Services.AddScoped<IPdfExportService, QuestPdfExportService>(); // register PDF export service

            builder.Services.AddScoped<PdfEmailService>(); // register service to send pdf reports

            builder.Services.AddScoped<CalculatorSearchService>(); // register calculator search service

            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = IdentityConstants.ApplicationScheme;
                    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                })
                .AddIdentityCookies();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentityCore<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            builder.Services.Configure<SmtpOptions>(
                builder.Configuration.GetSection("Smtp")); // bind SmtpOptions from configuration

            var emailEnabled =
                builder.Configuration.GetValue<bool>("Email:Enabled"); // read email enabled setting from configuration

            if (emailEnabled)
            {
                builder.Services.AddScoped<IEmailService, SmtpEmailService>(); // register SmtpEmailService for sending real emails
                builder.Services.AddScoped<IEmailSender<ApplicationUser>, IdentityEmailSender>(); // real IdentityEmailSender
            }
            else
            {
                builder.Services.AddScoped<IEmailService, FakeEmailService>(); // register FakeEmailService for development/testing
                builder.Services.AddScoped<IEmailSender<ApplicationUser>, DevIdentityEmailSender>(); // fake IdentityEmailSender
            }

            var app = builder.Build();

            await IdentitySeeder.SeedAsync(app.Services); // seed roles and admin user

            // this Confirmator can be used to set all user accounts as "confirmed" at the start of the application
            // await DevelopmentEmailConfirmation.AutoConfirmEmailsAsync(app.Services, app.Environment);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            // Add additional endpoints required by the Identity /Account Razor components.
            app.MapAdditionalIdentityEndpoints();

            app.MapPdfEndpoints();

            app.Run();
        }
    }
}
