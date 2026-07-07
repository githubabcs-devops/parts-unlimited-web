using GhAdoE2eDemo.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GhAdoE2eDemo.Web.Tests;

/// <summary>
/// Integration tests that verify the home page renders the expected markup,
/// confirming that view and content changes ship together.
/// </summary>
public class HomePageRenderingTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public HomePageRenderingTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task HomePage_Returns_200()
    {
        var response = await _client.GetAsync("/");
        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task HomePage_Renders_HeroBanner()
    {
        var html = await _client.GetStringAsync("/");
        Assert.Contains(HomeContent.HeroBanner, html);
    }

    [Fact]
    public async Task HomePage_Renders_AllHighlights()
    {
        var html = await _client.GetStringAsync("/");
        foreach (var item in HomeContent.Highlights)
        {
            Assert.Contains(item, html);
        }
    }

    [Fact]
    public async Task HomePage_Links_External_Stylesheet()
    {
        var html = await _client.GetStringAsync("/");
        Assert.Contains("site.css", html);
    }

    [Fact]
    public async Task HomePage_Contains_WhatsNew_Section()
    {
        var html = await _client.GetStringAsync("/");
        Assert.Contains("whatsnew-heading", html);
    }
}
