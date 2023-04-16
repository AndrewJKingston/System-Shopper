using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace System_Shopper.Pages.Shared
{
    public class Index1Model : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult OnPost(string username, string password)
        {
            // TODO: Add authentication logic
            // If authentication is successful, redirect to another page
            // return RedirectToPage("/SomePage");

            // If authentication fails, show an error message
            ModelState.AddModelError("", "Invalid login attempt.");

            return Page();
        }
    }
}
