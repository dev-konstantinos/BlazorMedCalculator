namespace BlazorMedCalculator.Web.Interfaces
{
    // interface to work with markdown content for calculators and pages
    public interface IContentService
    {
        Task<string> GetCalculatorArticleAsync(string calculatorCode);
        Task<string> GetPageAsync(string pageCode);
    }
}
