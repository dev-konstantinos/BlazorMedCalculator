using BlazorMedCalculator.Web.Interfaces;
using System.Security;
using System.Text.RegularExpressions;

namespace BlazorMedCalculator.Web.Infrastructure.Content
{
    public sealed class FileContentEditorService : IContentEditorService
    {
        private static readonly Regex SafeCode =
            new("^[a-z0-9\\-]+$", RegexOptions.IgnoreCase);

        private readonly string _contentRoot;

        public FileContentEditorService(IHostEnvironment env)
        {
            _contentRoot = Path.Combine(env.ContentRootPath, "Content");
        }

        public async Task<string> LoadCalculatorAsync(string code)
        {
            ValidateCode(code);

            var path = Path.Combine(
                _contentRoot,
                "Calculators",
                $"{code}.md");

            if (!File.Exists(path))
                return string.Empty;

            return await File.ReadAllTextAsync(path);
        }

        public async Task SaveCalculatorAsync(string code, string markdown)
        {
            ValidateCode(code);

            var dir = Path.Combine(_contentRoot, "Calculators");
            Directory.CreateDirectory(dir);

            var path = Path.Combine(dir, $"{code}.md");
            await File.WriteAllTextAsync(path, markdown);
        }

        private static void ValidateCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code) || !SafeCode.IsMatch(code))
                throw new SecurityException("Invalid content code");
        }
    }
}
