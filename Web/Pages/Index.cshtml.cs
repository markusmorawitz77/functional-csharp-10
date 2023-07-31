using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace Web.Pages;

public class IndexModel : PageModel
{
    public IActionResult OnGet() => RedirectToPage("Specifications");
}
