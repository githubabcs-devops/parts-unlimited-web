using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GhAdoE2eDemo.Web.Tests;

public class HealthEndpointTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Theory]
    [InlineData("/health")]
    [InlineData("/health5")]
    public async Task HealthEndpoints_Return200(string path)
    {
        var response = await _client.GetAsync(path);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory]
    [InlineData("/health")]
    [InlineData("/health5")]
    public async Task HealthEndpoints_ReturnExpectedPayload(string path)
    {
        var response = await _client.GetAsync(path);
        var body = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(body);
        Assert.Equal("ok", doc.RootElement.GetProperty("status").GetString());
        Assert.Equal("gh-ado-e2e-demo", doc.RootElement.GetProperty("service").GetString());

        var utcValue = doc.RootElement.GetProperty("utc").GetString();
        Assert.False(string.IsNullOrWhiteSpace(utcValue));
        Assert.True(DateTimeOffset.TryParse(utcValue, out _));
    }
}
