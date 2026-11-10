using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GhAdoE2eDemo.Web.Tests;

public class ShippingOptionsEndpointTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Get_ShippingOptions_ReturnsFixedDemoContract()
    {
        using var response = await _client.GetAsync("/api/shipping/options");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);

        using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var root = doc.RootElement;
        Assert.Equal(3, root.EnumerateObject().Count());
        Assert.Equal(JsonValueKind.True, root.GetProperty("isDemo").ValueKind);
        Assert.Equal("USD", root.GetProperty("currency").GetString());

        var options = root.GetProperty("options");
        Assert.Equal(JsonValueKind.Array, options.ValueKind);
        Assert.Equal(2, options.GetArrayLength());
        AssertOption(options[0], "standard", "Standard", 4.99m, 3, 5);
        AssertOption(options[1], "express", "Express", 9.99m, 1, 2);
    }

    [Fact]
    public async Task Get_ShippingOptions_ReturnsIdenticalResponsesOnRepeatedRequests()
    {
        using var first = await _client.GetAsync("/api/shipping/options");
        using var second = await _client.GetAsync("/api/shipping/options");

        Assert.Equal(HttpStatusCode.OK, first.StatusCode);
        Assert.Equal(HttpStatusCode.OK, second.StatusCode);
        Assert.Equal(await first.Content.ReadAsStringAsync(), await second.Content.ReadAsStringAsync());
    }

    private static void AssertOption(
        JsonElement option, string id, string name, decimal cost, int minBusinessDays, int maxBusinessDays)
    {
        Assert.Equal(5, option.EnumerateObject().Count());
        Assert.Equal(id, option.GetProperty("id").GetString());
        Assert.Equal(name, option.GetProperty("name").GetString());
        Assert.Equal(JsonValueKind.Number, option.GetProperty("cost").ValueKind);
        Assert.Equal(cost, option.GetProperty("cost").GetDecimal());
        Assert.Equal(JsonValueKind.Number, option.GetProperty("minBusinessDays").ValueKind);
        Assert.Equal(minBusinessDays, option.GetProperty("minBusinessDays").GetInt32());
        Assert.Equal(JsonValueKind.Number, option.GetProperty("maxBusinessDays").ValueKind);
        Assert.Equal(maxBusinessDays, option.GetProperty("maxBusinessDays").GetInt32());
    }
}
