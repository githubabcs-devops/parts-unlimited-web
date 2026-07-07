namespace GhAdoE2eDemo.Web;

/// <summary>
/// Home-page content. Kept in one place so the demo's simple user stories (banner text,
/// highlights) map to a single, easily-reviewed and unit-tested change.
/// </summary>
public static class HomeContent
{
    // User Story: "Update the home page hero banner text".
    public const string HeroBanner = "Welcome to Parts Unlimited - now powered by GitHub Copilot";

    // User Story: "Add a 'What's New' highlights section to the home page".
    public static readonly IReadOnlyList<HomeHighlight> Highlights = new[]
    {
        new HomeHighlight(
            "Fresh home page highlights",
            "See the latest storefront updates in a short, scannable section."),
        new HomeHighlight(
            "GitHub-powered delivery",
            "Track work from pull requests through automated builds, tests, and deployments."),
        new HomeHighlight(
            "AI-assisted reviews",
            "Catch issues early with automated review feedback on every proposed change."),
        new HomeHighlight(
            "Reliable releases",
            "Promote changes through Dev, QA, and Prod with the same delivery workflow."),
    };
}

public sealed record HomeHighlight(string Title, string Description);
