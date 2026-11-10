using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GhAdoE2eDemo.Web.Tests;

public class VersionEndpointCachingTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Get_Version_SecondRequest_UsesOutputCache()
    {
        HttpResponseMessage firstResponse = await _client.GetAsync("/version");
        HttpResponseMessage secondResponse = await _client.GetAsync("/version");

        Assert.Equal(HttpStatusCode.OK, firstResponse.StatusCode);
        Assert.Equal(HttpStatusCode.OK, secondResponse.StatusCode);
        Assert.True(secondResponse.Headers.Contains("Age"));
    }
}
