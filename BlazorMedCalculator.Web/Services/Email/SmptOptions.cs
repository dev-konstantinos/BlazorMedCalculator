namespace BlazorMedCalculator.Web.Infrastructure.Email
{
    public sealed class SmtpOptions
    {
        public string Host { get; init; } = default!;
        public int Port { get; init; }
        public string Username { get; init; } = default!;
        public string Password { get; init; } = default!;
        public string FromEmail { get; init; } = default!;
        public string FromName { get; init; } = default!;
    }
}
