namespace BlazorMedCalculator.Web.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(string to, string subject, string htmlBody);

        Task SendWithAttachmentAsync(
            string to,
            string subject,
            string htmlBody,
            byte[] attachment,
            string fileName,
            string contentType);
    }
}
