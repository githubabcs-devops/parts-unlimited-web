using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GhAdoE2eDemo.Web.Tests;

public class AccessibilityTests
{
    [Fact]
    public async Task Home_page_has_skip_link_to_main_content()
    {
        await using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient();

        using var response = await client.GetAsync("/");
        var html = await response.Content.ReadAsStringAsync();

        Assert.True(response.IsSuccessStatusCode);

        var bodyIndex = html.IndexOf("<body>", StringComparison.Ordinal);
        var skipLinkIndex = html.IndexOf(
            "<a class=\"skip-link\" href=\"#main-content\">Skip to main content</a>",
            StringComparison.Ordinal);
        var mainIndex = html.IndexOf("<main id=\"main-content\" tabindex=\"-1\">", StringComparison.Ordinal);

        Assert.True(bodyIndex >= 0);
        Assert.True(skipLinkIndex > bodyIndex);
        Assert.True(mainIndex > skipLinkIndex);
        Assert.DoesNotContain("role=\"main\"", html);
    }
}
