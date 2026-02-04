using BlazorMedCalculator.Web.Interfaces;
using BlazorMedCalculator.Web.Models;

public sealed class PdfEmailService
{
    private readonly ICurrentUser _currentUser;
    private readonly IUserContactInfo _contact;
    private readonly IPdfExportService _pdfService;
    private readonly IEmailService _emailService;

    public PdfEmailService(
        ICurrentUser currentUser,
        IUserContactInfo contact,
        IPdfExportService pdfService,
        IEmailService emailService)
    {
        _currentUser = currentUser;
        _contact = contact;
        _pdfService = pdfService;
        _emailService = emailService;
    }

    public async Task SendResultAsync(CalculationResult result)
    {
        if (!_currentUser.IsAuthenticated)
            throw new InvalidOperationException("User is not authenticated.");

        var email = await _contact.GetEmailAsync();
        var confirmed = await _contact.IsEmailConfirmedAsync();

        if (!confirmed || email is null)
            throw new InvalidOperationException("Email is not confirmed.");

        var pdf = _pdfService.Generate(result);

        await _emailService.SendWithAttachmentAsync(
            email,
            "Your calculation result",
            "<p>Your PDF result is attached.</p>",
            pdf,
            $"{result.CalculatorCode}-result.pdf",
            "application/pdf");
    }
}
