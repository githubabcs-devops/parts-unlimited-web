using GhAdoE2eDemo.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseRouting();

// User Story: "Add a health-check endpoint (/health)" — used by monitoring and the pipeline.
app.MapGet("/health", () => Results.Json(new
{
    status = "ok",
    service = "gh-ado-e2e-demo",
    utc = DateTime.UtcNow
}));

// User Story: "Add a health-check endpoint (/health6)" — used by additional uptime monitors.
app.MapGet("/health6", () => Results.Json(new
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
}));

// User Story (AB#1659): "/version2" returns the running build version so operators can
// confirm what is deployed.
app.MapGet("/version2", () => Results.Json(BuildInfo.Current));

app.MapRazorPages();

app.Run();

// Exposed so integration tests can use WebApplicationFactory<Program> if desired.
public partial class Program { }
