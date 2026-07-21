using System.Net;
using System.Text.Json;
using GhAdoE2eDemo.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GhAdoE2eDemo.Web.Tests;

public class VersionEndpointTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Get_Version3_Returns200()
    {
        var response = await _client.GetAsync("/version3");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_Version3_ReturnsBuildInfoPayload()
    {
        var response = await _client.GetAsync("/version3");
        var body = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(body);
        Assert.Equal("parts-unlimited-web", doc.RootElement.GetProperty("service").GetString());
        Assert.Equal(BuildInfo.Version, doc.RootElement.GetProperty("version").GetString());
    }
}
