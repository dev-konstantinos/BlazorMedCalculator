using BlazorMedCalculator.Application.Models;

namespace BlazorMedCalculator.Application.Interfaces
{
    public interface IPdfExportService
    {
        byte[] Generate(CalculationResult result);
    }
}
