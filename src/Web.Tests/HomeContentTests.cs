using GhAdoE2eDemo.Web;
using Xunit;

namespace GhAdoE2eDemo.Web.Tests;

public class HomeContentTests
{
    // Demonstrates a test shipping alongside the home-page change (per the demo user stories).
    [Fact]
    public void HeroBanner_is_not_empty()
    {
        Assert.False(string.IsNullOrWhiteSpace(HomeContent.HeroBanner));
    }

    [Fact]
    public void Highlights_are_listed()
    {
        Assert.NotEmpty(HomeContent.Highlights);
    }

    [Fact]
    public void Highlights_have_titles_and_descriptions()
    {
        Assert.All(HomeContent.Highlights, highlight =>
        {
            Assert.False(string.IsNullOrWhiteSpace(highlight.Title));
            Assert.False(string.IsNullOrWhiteSpace(highlight.Description));
        });
    }
}
