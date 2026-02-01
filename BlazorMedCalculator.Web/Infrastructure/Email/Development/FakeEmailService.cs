using BlazorMedCalculator.Application.Interfaces;

namespace BlazorMedCalculator.Web.Infrastructure.Email.Development;

public sealed class FakeEmailService : IEmailService
{
    private readonly ILogger<FakeEmailService> _logger;

    public FakeEmailService(ILogger<FakeEmailService> logger)
    {
        _logger = logger;
    }

    public Task SendAsync(string to, string subject, string htmlBody)
    {
        _logger.LogInformation("""
            [FAKE EMAIL]
            To: {To}
            Subject: {Subject}
            Body:
            {Body}
            """,
            to, subject, htmlBody);

        return Task.CompletedTask;
    }

    public Task SendWithAttachmentAsync(
    string to,
    string subject,
    string htmlBody,
    byte[] attachment,
    string fileName,
    string contentType)
    {
        _logger.LogInformation("""
        [FAKE EMAIL WITH ATTACHMENT]
        To: {To}
        Subject: {Subject}
        Attachment: {FileName} ({Size} bytes)
        Body:
        {Body}
        """,
            to,
            subject,
            fileName,
            attachment.Length,
            htmlBody);

        return Task.CompletedTask;
    }

}
