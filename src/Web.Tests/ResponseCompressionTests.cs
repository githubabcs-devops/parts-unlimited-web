using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GhAdoE2eDemo.Web.Tests;

public class ResponseCompressionTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory = factory;

    [Fact]
    public async Task Version_endpoint_is_gzip_compressed_when_requested()
    {
        var client = _factory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Get, "/version");
        request.Headers.AcceptEncoding.ParseAdd("gzip");

        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        Assert.Contains("gzip", response.Content.Headers.ContentEncoding);
    }

    [Fact]
    public async Task Home_page_is_brotli_compressed_when_requested()
    {
        var client = _factory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Get, "/");
        request.Headers.AcceptEncoding.ParseAdd("br");

        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        Assert.Contains("br", response.Content.Headers.ContentEncoding);
    }
}
