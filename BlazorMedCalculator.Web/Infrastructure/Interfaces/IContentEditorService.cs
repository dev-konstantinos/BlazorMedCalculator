namespace BlazorMedCalculator.Web.Infrastructure.Interfaces
{
    public interface IContentEditorService
    {
        Task<string> LoadCalculatorAsync(string code);
        Task SaveCalculatorAsync(string code, string markdown);
    }
}
