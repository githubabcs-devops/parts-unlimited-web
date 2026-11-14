using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace GhAdoE2eDemo.Web.Tests;

public class AboutPageTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    // AC: /about returns HTTP 200 with the expected title, h1, and both sections.
    [Fact]
    public async Task Get_About_Returns200()
    {
        var response = await _client.GetAsync("/about");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_About_HasExpectedTitle()
    {
        var response = await _client.GetAsync("/about");
        var body = await response.Content.ReadAsStringAsync();

        Assert.Contains("About this demo", body);
    }

    [Fact]
    public async Task Get_About_HasH1AboutThisDemo()
    {
        var response = await _client.GetAsync("/about");
        var body = await response.Content.ReadAsStringAsync();

        Assert.Matches(@"<h1[^>]*>\s*About this demo\s*</h1>", body);
    }

    [Fact]
    public async Task Get_About_HasH2WhatThisAppDemonstrates()
    {
        var response = await _client.GetAsync("/about");
        var body = await response.Content.ReadAsStringAsync();

        Assert.Matches(@"<h2[^>]*>\s*What this app demonstrates\s*</h2>", body);
    }

    [Fact]
    public async Task Get_About_HasH2DemoLimitations()
    {
        var response = await _client.GetAsync("/about");
        var body = await response.Content.ReadAsStringAsync();

        Assert.Matches(@"<h2[^>]*>\s*Demo limitations\s*</h2>", body);
    }

    // AC: The page explicitly says real orders and payments are not supported.
    [Fact]
    public async Task Get_About_ExplicitlyStatesNoRealOrdersOrPayments()
    {
        var response = await _client.GetAsync("/about");
        var body = await response.Content.ReadAsStringAsync();

        Assert.Contains("real orders or payments", body);
    }

    // AC: The shared footer provides a descriptive link to /about.
    [Fact]
    public async Task Footer_ContainsDescriptiveLinkToAbout()
    {
        var response = await _client.GetAsync("/");
        var body = await response.Content.ReadAsStringAsync();

        Assert.Contains("href=\"/about\"", body);
        Assert.Contains("About this demo", body);
    }

    // AC: Existing footer links and the home page are preserved.
    [Fact]
    public async Task Footer_PreservesExistingLinks()
    {
        var response = await _client.GetAsync("/");
        var body = await response.Content.ReadAsStringAsync();

        Assert.Contains("href=\"/privacy\"", body);
        Assert.Contains("href=\"/contact\"", body);
    }

    [Fact]
    public async Task Get_Home_Returns200()
    {
        var response = await _client.GetAsync("/");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
