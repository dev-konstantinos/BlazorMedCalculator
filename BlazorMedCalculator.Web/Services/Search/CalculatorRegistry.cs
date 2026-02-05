using BlazorMedCalculator.Web.Models;

namespace BlazorMedCalculator.Web.Services.Search
{
    public static class CalculatorRegistry
    {
        public static readonly IReadOnlyList<CalculatorDefinition> All =
            new List<CalculatorDefinition>
            {
            new()
            {
                Code = "bmi",
                Title = "Body Mass Index",
                Description = "Calculate BMI based on height and weight",
                CategoryCode = "anthropometry"
            },
            new()
            {
                Code = "egfr",
                Title = "eGFR",
                Description = "Estimate kidney function (CKD-EPI)",
                CategoryCode = "nephrology"
            }
            };
    }

}
