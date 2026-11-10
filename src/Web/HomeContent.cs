namespace GhAdoE2eDemo.Web;

/// <summary>
/// Home-page content. Kept in one place so the demo's simple user stories (banner text,
/// highlights) map to a single, easily-reviewed and unit-tested change.
/// </summary>
public static class HomeContent
{
    public const string Title = "Welcome to Parts Unlimited v3.1";

    public const string HeroMessage = "Now powered by GitHub Copilot (GHCP)";

    // User Story: "Add a 'What's New' highlights section to the home page".
    public static readonly IReadOnlyList<string> Highlights = new[]
    {
        "Free shipping on orders over 100",
        "See the latest updates in the What is New section",
        "Migrated from Azure DevOps to GitHub with full history and pull requests",
        "AI-assisted code review on every pull request",
        "Automated CI/CD to Azure App Service across Dev, QA and Prod",
    };
}
