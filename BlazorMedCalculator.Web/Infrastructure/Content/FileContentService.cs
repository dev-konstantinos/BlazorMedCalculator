using BlazorMedCalculator.Application.Interfaces;
using System.Security;
using System.Text.RegularExpressions;

namespace BlazorMedCalculator.Web.Infrastructure.Content;

// class to read markdown content files from the file system
public sealed class FileContentService : IContentService
{
    private static readonly Regex SafeCode =
        new("^[a-z0-9\\-]+$", RegexOptions.IgnoreCase);

    private readonly string _contentRoot;

    public FileContentService(IHostEnvironment env)
    {
        _contentRoot = Path.Combine(env.ContentRootPath, "Content");
    }

    // reads markdown article for a given calculator code
    public async Task<string> GetCalculatorArticleAsync(string calculatorCode)
    {
        ValidateCode(calculatorCode);

        var path = Path.Combine(
            _contentRoot,
            "Calculators",
            $"{calculatorCode}.md"
        );

        return await ReadFileAsync(path);
    }

    // reads markdown page for a given page code
    public async Task<string> GetPageAsync(string pageCode)
    {
        ValidateCode(pageCode);

        var path = Path.Combine(
            _contentRoot,
            "Pages",
            $"{pageCode}.md"
        );

        return await ReadFileAsync(path);
    }

    // validates that the code contains only safe characters
    private static void ValidateCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code) || !SafeCode.IsMatch(code))
            throw new SecurityException("Invalid content code");
    }

    // reads content, returns empty string if not exists
    private static async Task<string> ReadFileAsync(string path)
    {
        if (!File.Exists(path))
            return string.Empty;

        return await File.ReadAllTextAsync(path);
    }
}
