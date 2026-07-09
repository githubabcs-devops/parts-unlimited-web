using GhAdoE2eDemo.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GhAdoE2eDemo.Web.Tests;

public class HomeContentTests
{
    [Fact]
    public void Title_matches_expected_home_page_title()
    {
        Assert.Equal("Welcome to Parts Unlimited v3.1", HomeContent.Title);
    }

    // Demonstrates a test shipping alongside the home-page change (per the demo user stories).
    [Fact]
    public void HeroMessage_is_not_empty()
    {
        Assert.False(string.IsNullOrWhiteSpace(HomeContent.HeroMessage));
    }

    [Fact]
    public void Highlights_are_listed()
    {
        Assert.NotEmpty(HomeContent.Highlights);
    }
}

public class BuildInfoTests
{
    // AB#1659: /version2 returns the app version as JSON.
    [Fact]
    public void Version_is_not_empty()
    {
        Assert.False(string.IsNullOrWhiteSpace(BuildInfo.Version));
    }

    [Fact]
    public void Current_exposes_service_and_version()
    {
        VersionInfo info = BuildInfo.Current;

        Assert.Equal("parts-unlimited-web", info.Service);
        Assert.Equal(BuildInfo.Version, info.Version);
    }
}

public class Health3EndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public Health3EndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    // AB#1661: GET /health3 must return 200 with a JSON body containing status = "ok".
    [Fact]
    public async Task Health3_returns_200_with_ok_status()
    {
        HttpResponseMessage response = await _client.GetAsync("/health3");

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

        string body = await response.Content.ReadAsStringAsync();
        Assert.Contains("\"status\"", body);
        Assert.Contains("\"ok\"", body);
    }
}
