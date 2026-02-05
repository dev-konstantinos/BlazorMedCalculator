namespace BlazorMedCalculator.Web.Models
{
    public sealed class CalculatorCategory
    {
        public string Code { get; init; } = default!;
        public string Title { get; init; } = default!;
        public string Description { get; init; } = default!;
        public string Url => $"/categories/{Code}";
    }

}
