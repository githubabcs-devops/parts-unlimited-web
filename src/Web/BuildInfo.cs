namespace GhAdoE2eDemo.Web;

using System.Reflection;

/// <summary>
/// Build/version information for the running app. Exposed via the <c>/version2</c> endpoint so
/// operators can confirm what is deployed.
/// </summary>
public static class BuildInfo
{
    public static string Version { get; } =
        typeof(BuildInfo).Assembly.GetName().Version?.ToString() ?? "unknown";

    public static VersionInfo Current => new("parts-unlimited-web", Version);
}

/// <summary>Serialized as JSON by the <c>/version2</c> endpoint.</summary>
public record VersionInfo(string Service, string Version);
