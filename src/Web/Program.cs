using GhAdoE2eDemo.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

// Register source-generated JSON metadata so the minimal-API endpoints below serialize without
// runtime reflection (faster, lower startup cost, trim/AOT friendly).
builder.Services.ConfigureHttpJsonOptions(options =>
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default));

var app = builder.Build();

app.UseRouting();

// User Story: "Add a health-check endpoint (/health)" — used by monitoring and the pipeline.
app.MapGet("/health", static () => Results.Json(
    new HealthStatus("ok", "gh-ado-e2e-demo", DateTime.UtcNow),
    AppJsonSerializerContext.Default.HealthStatus));

// GET /version — returns the assembly informational version as JSON.
app.MapGet("/version", static () => Results.Json(
    new VersionInfo("parts-unlimited-web", BuildInfo.InformationalVersion),
    AppJsonSerializerContext.Default.VersionInfo));

// User Story (AB#1659): "/version2" returns the running build version so operators can
// confirm what is deployed.
app.MapGet("/version2", static () => Results.Json(
    BuildInfo.Current,
    AppJsonSerializerContext.Default.VersionInfo));

app.MapRazorPages();

app.Run();

// Exposed so integration tests can use WebApplicationFactory<Program> if desired.
public partial class Program { }
