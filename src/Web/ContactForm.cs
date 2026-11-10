using System.ComponentModel.DataAnnotations;

namespace GhAdoE2eDemo.Web;

/// <summary>
/// Input model for the Contact page form. Validation lives on the model via data annotations
/// so it is enforced both server-side (ModelState) and easily unit-tested in isolation.
/// </summary>
public class ContactForm
{
    [Required(ErrorMessage = "Please enter your name.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Please enter your email address.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Please enter a message.")]
    [StringLength(1000, MinimumLength = 10, ErrorMessage = "Message must be between 10 and 1000 characters.")]
    public string? Message { get; set; }
}
