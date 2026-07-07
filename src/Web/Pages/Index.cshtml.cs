using GhAdoE2eDemo.Web;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GhAdoE2eDemo.Web.Pages;

public class IndexModel : PageModel
{
    public bool ShowCookieConsentBanner => !Request.Cookies.ContainsKey(HomeContent.CookieConsentCookieName);

    public string ReturnUrl => $"{Request.Path}{Request.QueryString}";

    public void OnGet()
    {
    }
}
