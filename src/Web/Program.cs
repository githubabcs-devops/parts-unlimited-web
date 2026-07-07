var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

// User Story: "Add a health-check endpoint (/health)" — used by monitoring and the pipeline.
app.MapGet("/health", () => Results.Json(new
{
    status = "ok",
    service = "gh-ado-e2e-demo",
    utc = DateTime.UtcNow
}));

app.MapRazorPages();

app.Run();

// Exposed so integration tests can use WebApplicationFactory<Program> if desired.
public partial class Program { }
