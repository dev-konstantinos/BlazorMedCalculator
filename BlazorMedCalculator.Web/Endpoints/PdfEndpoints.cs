using BlazorMedCalculator.Web.Interfaces;
using BlazorMedCalculator.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorMedCalculator.Web.Endpoints;

public static class PdfEndpoints
{
    public static void MapPdfEndpoints(this WebApplication app)
    {
        app.MapPost("/api/pdf/{code}", (
            string code,
            [FromServices] IPdfExportService pdfService,
            [FromForm] CalculationResult result) =>
        {
            var pdf = pdfService.Generate(result);

            return Results.File(
                pdf,
                "application/pdf",
                $"{result.CalculatorCode}-result.pdf");
        })
        .RequireAuthorization("CanAccessPdf")
        .DisableAntiforgery();
    }
}
