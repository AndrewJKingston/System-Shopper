using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace System_Shopper.Pages.Shared
{
    public class Index1Model : PageModel
    {
        [BindProperty]
        public Credential LoginInfo { get; set; }

        private readonly ILogger<Index1Model> _logger;

        public Index1Model(ILogger<Index1Model> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost(string username, string password)
        {
            // TODO: Add authentication logic
            // If authentication is successful, redirect to another page
            // return RedirectToPage("/SomePage");
            
            if (ModelState.IsValid)
            {
                if (LoginInfo.Email == "example.email.com" && LoginInfo.Password == "12345")
                {

                }
            }
            // Verify user credential

            // Create security context


            // If authentication fails, show an error message
            ModelState.AddModelError("", "Invalid login attempt.");

            return Page();
        }
    }

    public class Credential
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
