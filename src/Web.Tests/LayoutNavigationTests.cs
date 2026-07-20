using System.IO;
using Xunit;

namespace GhAdoE2eDemo.Web.Tests;

public class LayoutNavigationTests
{
    private static readonly string LayoutPath = Path.GetFullPath(
        "../../../../Web/Pages/Shared/_Layout.cshtml", AppContext.BaseDirectory);

    private static string ReadLayout() => File.ReadAllText(LayoutPath);

    [Fact]
    public void Layout_includes_hamburger_button_with_accessible_navigation_attributes()
    {
        string layout = ReadLayout();

        Assert.Contains("class=\"nav-toggle\"", layout);
        Assert.Contains("type=\"button\"", layout);
        Assert.Contains("aria-controls=\"primary-navigation\"", layout);
        Assert.Contains("aria-expanded=\"false\"", layout);
        Assert.Contains("aria-label=\"Toggle navigation\"", layout);
        Assert.Contains("<nav class=\"site-nav\" aria-label=\"Primary\">", layout);
    }

    [Fact]
    public void Layout_uses_mobile_breakpoint_styles_to_collapse_navigation()
    {
        string layout = ReadLayout();

        Assert.Contains("@@media (max-width: 640px)", layout);
        Assert.Contains(".site-nav-links {", layout);
        Assert.Contains("max-height: 0;", layout);
        Assert.Contains("transition: max-height 0.2s ease, opacity 0.2s ease;", layout);
        Assert.Contains(".site-nav-links.is-open {", layout);
        Assert.Contains("max-height: 80vh;", layout);
        Assert.Contains("overflow-y: auto;", layout);
    }

    [Fact]
    public void Layout_script_supports_keyboard_escape_to_close_the_menu()
    {
        string layout = ReadLayout();

        Assert.Contains("document.addEventListener('keydown'", layout);
        Assert.Contains("if (event.key === 'Escape'", layout);
        Assert.Contains("navMenu.classList.contains('is-open')", layout);
        Assert.Contains("navToggle.focus();", layout);
    }

    [Theory]
    [InlineData("Privacy.cshtml")]
    [InlineData("Contact.cshtml")]
    public void Primary_navigation_links_have_backing_razor_pages(string pageFile)
    {
        if (Path.IsPathRooted(pageFile))
            throw new ArgumentException($"pageFile must be a relative filename, not a rooted path: {pageFile}", nameof(pageFile));

        string pagesDir = Path.GetFullPath("../../../../Web/Pages", AppContext.BaseDirectory);
        string pagePath = Path.Combine(pagesDir, pageFile);

        Assert.True(File.Exists(pagePath), $"Expected Razor page '{pageFile}' to exist so its navigation link does not 404.");
        Assert.Contains("@page", File.ReadAllText(pagePath));
    }
}
