using GhAdoE2eDemo.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Xunit;

namespace GhAdoE2eDemo.Web.Tests;

public class ContactModelTests
{
    [Fact]
    public void OnPost_with_valid_form_sets_submitted_flag()
    {
        var model = new ContactModel
        {
            Form = new ContactForm
            {
                Name = "Ada Lovelace",
                Email = "ada@example.com",
                Message = "I need help with my recent order, please.",
            },
        };

        var result = model.OnPost();

        Assert.IsType<PageResult>(result);
        Assert.True(model.Submitted);
    }

    [Fact]
    public void OnPost_with_invalid_form_does_not_submit()
    {
        var model = new ContactModel
        {
            Form = new ContactForm { Name = "", Email = "bad", Message = "" },
        };
        model.ModelState.AddModelError("Form.Name", "Please enter your name.");

        var result = model.OnPost();

        Assert.IsType<PageResult>(result);
        Assert.False(model.Submitted);
    }
}
