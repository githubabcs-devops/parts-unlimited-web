using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GhAdoE2eDemo.Web.Pages;

public class ContactModel : PageModel
{
    [BindProperty]
    public ContactForm Form { get; set; } = new();

    public bool Submitted { get; private set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Submitted = true;
        Form = new ContactForm();
        ModelState.Clear();
        return Page();
    }
}
