using BlazorMedCalculator.Web.Models;

namespace BlazorMedCalculator.Web.Interfaces
{
    public interface IPdfExportService
    {
        byte[] Generate(CalculationResult result);
    }
}
