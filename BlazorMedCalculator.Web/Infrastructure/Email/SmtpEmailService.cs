using System.Net;
using System.Net.Mail;
using BlazorMedCalculator.Application.Interfaces;
using Microsoft.Extensions.Options;

namespace BlazorMedCalculator.Web.Infrastructure.Email;

public sealed class SmtpEmailService : IEmailService
{
    private readonly SmtpOptions _options;

    public SmtpEmailService(IOptions<SmtpOptions> options)
    {
        _options = options.Value;
    }

    public async Task SendAsync(string to, string subject, string htmlBody)
    {
        using var client = new SmtpClient(_options.Host, _options.Port)
        {
            Credentials = new NetworkCredential(
                _options.Username,
                _options.Password),
            EnableSsl = true
        };

        using var message = new MailMessage
        {
            From = new MailAddress(
                _options.FromEmail,
                _options.FromName),
            Subject = subject,
            Body = htmlBody,
            IsBodyHtml = true
        };

        message.To.Add(to);

        await client.SendMailAsync(message);
    }

    public async Task SendWithAttachmentAsync(
    string to,
    string subject,
    string htmlBody,
    byte[] attachment,
    string fileName,
    string contentType)
    {
        using var client = new SmtpClient(_options.Host, _options.Port)
        {
            Credentials = new NetworkCredential(
                _options.Username,
                _options.Password),
            EnableSsl = true
        };

        using var message = new MailMessage
        {
            From = new MailAddress(
                _options.FromEmail,
                _options.FromName),
            Subject = subject,
            Body = htmlBody,
            IsBodyHtml = true
        };

        message.To.Add(to);

        using var stream = new MemoryStream(attachment);
        var mailAttachment = new Attachment(stream, fileName, contentType);
        message.Attachments.Add(mailAttachment);

        await client.SendMailAsync(message);
    }

}
