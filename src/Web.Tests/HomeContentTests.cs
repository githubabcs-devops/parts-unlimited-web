using GhAdoE2eDemo.Web;
using Xunit;

namespace GhAdoE2eDemo.Web.Tests;

public class HomeContentTests
{
    [Fact]
    public void Title_matches_expected_home_page_title()
    {
        Assert.Equal("Welcome to Parts Unlimited v2", HomeContent.Title);
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
