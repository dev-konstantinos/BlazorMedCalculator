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
