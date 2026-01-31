using BlazorMedCalculator.Application.Interfaces;
using BlazorMedCalculator.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorMedCalculator.Web.Endpoints;

public static class PdfEndpoints
{
    public static void MapPdfEndpoints(this WebApplication app)
    {
        app.MapPost("/api/pdf/bmi", (
            [FromServices] IPdfExportService pdfService,
            [FromForm] CalculationResult result) =>
        {
            var pdf = pdfService.Generate(result);

            return Results.File(
                pdf,
                "application/pdf",
                "bmi-result.pdf");
        })
        .DisableAntiforgery(); // allowing PDF generation via API calls without antiforgery token
    }
}
