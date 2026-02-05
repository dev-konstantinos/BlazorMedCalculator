using BlazorMedCalculator.Web.Models;

namespace BlazorMedCalculator.Web.Services.Search
{
    public sealed class CalculatorSearchService
    {
        public IEnumerable<CalculatorDefinition> Search(
            string? query,
            IEnumerable<CalculatorDefinition> source)
        {
            if (string.IsNullOrWhiteSpace(query))
                return source;

            query = query.Trim().ToLowerInvariant();

            return source.Where(c =>
                c.Title.ToLowerInvariant().Contains(query) ||
                c.Description.ToLowerInvariant().Contains(query) ||
                c.Code.ToLowerInvariant().Contains(query));
        }
    }
}
