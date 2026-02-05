using BlazorMedCalculator.Web.Models;

namespace BlazorMedCalculator.Web.Services.Search
{
    public static class CalculatorCategoryRegistry
    {
        public static readonly IReadOnlyList<CalculatorCategory> All =
            new List<CalculatorCategory>
            {
            new()
            {
                Code = "anthropometry",
                Title = "Anthropometry",
                Description = "Body measurements and indices"
            },
            new()
            {
                Code = "nephrology",
                Title = "Nephrology",
                Description = "Kidney function and renal markers"
            }
            };
    }

}
