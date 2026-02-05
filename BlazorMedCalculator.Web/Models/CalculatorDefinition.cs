namespace BlazorMedCalculator.Web.Models
{
    public sealed class CalculatorDefinition
    {
        public string Code { get; init; } = default!;
        public string Title { get; init; } = default!;
        public string Description { get; init; } = default!;
        public string Url => $"/calculators/{Code}";
    }

}
