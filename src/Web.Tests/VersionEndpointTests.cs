using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GhAdoE2eDemo.Web.Tests;

public class VersionEndpointTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Get_Version5_Returns200()
    {
        var response = await _client.GetAsync("/version5");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_Version5_ReturnsServiceAndVersion()
    {
        var response = await _client.GetAsync("/version5");
        var body = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(body);
        Assert.Equal("parts-unlimited-web", doc.RootElement.GetProperty("service").GetString());
        Assert.False(string.IsNullOrWhiteSpace(
            doc.RootElement.GetProperty("version").GetString()));
    }
}
