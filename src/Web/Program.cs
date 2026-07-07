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

app.MapPost("/cookie-consent", async Task<IResult> (HttpContext context) =>
{
    var form = await context.Request.ReadFormAsync();
    var choice = form["choice"].ToString();

    if (choice is not ("accepted" or "declined"))
    {
        return Results.BadRequest();
    }

    var returnUrl = form["returnUrl"].ToString();
    if (string.IsNullOrWhiteSpace(returnUrl) || !returnUrl.StartsWith('/') || returnUrl.StartsWith("//"))
    {
        returnUrl = "/";
    }

    context.Response.Cookies.Append(HomeContent.CookieConsentCookieName, choice, new CookieOptions
    {
        Expires = DateTimeOffset.UtcNow.AddYears(1),
        HttpOnly = true,
        IsEssential = true,
        SameSite = SameSiteMode.Lax,
        Secure = context.Request.IsHttps
    });

    return Results.LocalRedirect(returnUrl);
}).DisableAntiforgery();

app.MapRazorPages();

app.Run();

// Exposed so integration tests can use WebApplicationFactory<Program> if desired.
public partial class Program { }
