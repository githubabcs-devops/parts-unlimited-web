using GhAdoE2eDemo.Web;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

// Performance: compress HTML/JSON responses (Brotli preferred, Gzip fallback) to cut
// bandwidth and speed up page loads. Applies to the default set of text MIME types.
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});
builder.Services.Configure<BrotliCompressionProviderOptions>(o => o.Level = CompressionLevel.Fastest);
builder.Services.Configure<GzipCompressionProviderOptions>(o => o.Level = CompressionLevel.Fastest);

var app = builder.Build();

app.UseResponseCompression();

app.UseRouting();

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
}));

// User Story (AB#1659): "/version2" returns the running build version so operators can
// confirm what is deployed.
app.MapGet("/version2", () => Results.Json(BuildInfo.Current));

app.MapRazorPages();

app.Run();

// Exposed so integration tests can use WebApplicationFactory<Program> if desired.
public partial class Program { }
