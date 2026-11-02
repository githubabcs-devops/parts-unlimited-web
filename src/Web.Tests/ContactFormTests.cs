using System.ComponentModel.DataAnnotations;
using GhAdoE2eDemo.Web;
using Xunit;

namespace GhAdoE2eDemo.Web.Tests;

public class ContactFormTests
{
    private static IList<ValidationResult> Validate(ContactForm form)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(form);
        Validator.TryValidateObject(form, context, results, validateAllProperties: true);
        return results;
    }

    private static ContactForm ValidForm() => new()
    {
        Name = "Ada Lovelace",
        Email = "ada@example.com",
        Message = "I need help with my recent order, please.",
    };

    [Fact]
    public void Valid_form_has_no_validation_errors()
    {
        Assert.Empty(Validate(ValidForm()));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("A")]
    public void Invalid_name_is_rejected(string? name)
    {
        var form = ValidForm();
        form.Name = name;

        Assert.Contains(Validate(form), r => r.MemberNames.Contains(nameof(ContactForm.Name)));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("not-an-email")]
    [InlineData("missing-at-sign.com")]
    public void Invalid_email_is_rejected(string? email)
    {
        var form = ValidForm();
        form.Email = email;

        Assert.Contains(Validate(form), r => r.MemberNames.Contains(nameof(ContactForm.Email)));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("too short")]
    public void Invalid_message_is_rejected(string? message)
    {
        var form = ValidForm();
        form.Message = message;

        Assert.Contains(Validate(form), r => r.MemberNames.Contains(nameof(ContactForm.Message)));
    }
}
