namespace GhAdoE2eDemo.Web;

using System.Text.Json.Serialization;

/// <summary>Serialized as JSON by the <c>/health</c> endpoint.</summary>
public record HealthStatus(string Status, string Service, DateTime Utc);

/// <summary>
/// Source-generated JSON metadata for the app's minimal-API responses. Using a
/// <see cref="JsonSerializerContext"/> replaces reflection-based serialization with compile-time
/// generated code, which lowers per-request cost and startup time and is trim/AOT friendly.
/// The camelCase policy preserves the existing JSON contract (e.g. <c>status</c>, <c>version</c>).
/// </summary>
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(HealthStatus))]
[JsonSerializable(typeof(VersionInfo))]
public partial class AppJsonSerializerContext : JsonSerializerContext
{
}
