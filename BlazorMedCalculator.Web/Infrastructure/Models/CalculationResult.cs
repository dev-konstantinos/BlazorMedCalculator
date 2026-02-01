namespace BlazorMedCalculator.Application.Models
{
    public sealed class CalculationResult
    {
        public string CalculatorCode { get; init; } = default!;
        public IReadOnlyDictionary<string, string> Inputs { get; init; }
            = new Dictionary<string, string>();

        public IReadOnlyDictionary<string, string> Outputs { get; init; }
            = new Dictionary<string, string>();
    }
}
