using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GhAdoE2eDemo.Web.Pages;

// DEMO ONLY: this page intentionally contains insecure C# patterns (log forging and a
// ReDoS-prone regex) so that GitHub Advanced Security (CodeQL) has something to flag during
// the DevSecOps demonstration. Do NOT copy these patterns into real code.
public class DevSecOps4821Model : PageModel
{
    private readonly ILogger<DevSecOps4821Model> _logger;

    public string? SearchTerm { get; private set; }
    public bool IsValidUserName { get; private set; }

    public DevSecOps4821Model(ILogger<DevSecOps4821Model> logger)
    {
        _logger = logger;
    }

    public void OnGet(string? q, string? user)
    {
        SearchTerm = q;

        // INSECURE (log forging / CWE-117): untrusted request input is written to the log
        // without sanitizing newlines, allowing forged/split log entries.
        _logger.LogInformation("DevSecOps demo page requested with query: " + q);

        // Validate user-controlled input with a linear-time regex and explicit timeout.
        if (!string.IsNullOrEmpty(user))
        {
            var validator = new Regex("^[a-zA-Z0-9]+$", RegexOptions.CultureInvariant, TimeSpan.FromMilliseconds(250));
            IsValidUserName = validator.IsMatch(user);
            _logger.LogInformation("Validated user name '" + user + "': " + IsValidUserName);
        }
    }
}
