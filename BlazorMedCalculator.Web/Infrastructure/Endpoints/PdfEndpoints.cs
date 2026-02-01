using BlazorMedCalculator.Application.Interfaces;
using BlazorMedCalculator.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorMedCalculator.Web.Infrastructure.Endpoints;

public static class PdfEndpoints
{
    public static void MapPdfEndpoints(this WebApplication app)
    {
        app.MapPost("/api/pdf/{code}", (
            string code,
            [FromServices] IPdfExportService pdfService,
            [FromForm] CalculationResult result) =>
        {
            // safety check: URL must match payload
            if (!string.Equals(code, result.CalculatorCode, StringComparison.OrdinalIgnoreCase))
            {
                return Results.BadRequest("Calculator code mismatch.");
            }

            var pdf = pdfService.Generate(result);

            var fileName = $"{result.CalculatorCode}-result.pdf";

            return Results.File(
                pdf,
                "application/pdf",
                fileName);
        })
        .DisableAntiforgery();
    }
}
