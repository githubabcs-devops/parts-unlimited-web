using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GhAdoE2eDemo.Web.Tests;

public class CookieConsentTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public CookieConsentTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Home_page_shows_cookie_banner_on_first_visit()
    {
        using var client = _factory.CreateClient();

        var response = await client.GetAsync("/");
        var html = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("cookie-consent-banner", html);
        Assert.Contains("cookie-consent-privacy-link", html);
        Assert.Contains("href=\"/privacy\"", html);
    }

    [Fact]
    public async Task Cookie_choice_is_remembered_after_posting_consent()
    {
        using var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        using var form = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["choice"] = "accepted",
            ["returnUrl"] = "/"
        });

        var postResponse = await client.PostAsync("/cookie-consent", form);

        Assert.Equal(HttpStatusCode.Redirect, postResponse.StatusCode);
        Assert.Equal("/", postResponse.Headers.Location?.ToString());
        Assert.Contains(HomeContent.CookieConsentCookieName, string.Join(';', postResponse.Headers.GetValues("Set-Cookie")));

        using var followUpRequest = new HttpRequestMessage(HttpMethod.Get, "/");
        followUpRequest.Headers.Add("Cookie", $"{HomeContent.CookieConsentCookieName}=accepted");

        var followUpResponse = await client.SendAsync(followUpRequest);
        var html = await followUpResponse.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, followUpResponse.StatusCode);
        Assert.DoesNotContain("cookie-consent-banner", html);
    }

    [Fact]
    public async Task Privacy_page_is_available()
    {
        using var client = _factory.CreateClient();

        var response = await client.GetAsync("/privacy");
        var html = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("Privacy Policy", html);
    }
}
