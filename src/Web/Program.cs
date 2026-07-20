using GhAdoE2eDemo.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddOutputCache(options =>
{
    options.AddPolicy("HomePageCache", policy => policy.Expire(TimeSpan.FromMinutes(5)));
    options.AddPolicy("VersionCache", policy => policy.Expire(TimeSpan.FromMinutes(10)));
});

var app = builder.Build();

app.UseRouting();
app.UseOutputCache();

// User Story: "Add a health-check endpoint (/health)" — used by monitoring and the pipeline.
app.MapGet("/health", () => Results.Json(new
{
    status = "ok",
    service = "gh-ado-e2e-demo",
    utc = DateTime.UtcNow
}));

// GET /version — returns the assembly informational version as JSON.
app.MapGet("/version", () => Results.Json(new
{
    service = "parts-unlimited-web",
    version = BuildInfo.InformationalVersion
}))
    .CacheOutput("VersionCache");

// User Story (AB#1659): "/version2" returns the running build version so operators can
// confirm what is deployed.
app.MapGet("/version2", () => Results.Json(BuildInfo.Current))
    .CacheOutput("VersionCache");

app.MapRazorPages()
    .CacheOutput("HomePageCache");

app.Run();

// Exposed so integration tests can use WebApplicationFactory<Program> if desired.
public partial class Program { }
