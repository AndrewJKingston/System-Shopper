using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace System_Shopper.Pages.Shared
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential LoginInfo { get; set; }

        private readonly ILogger<LoginModel> _logger;

        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> asyncOnPost(string username, string password)
        {
            // TODO: Add authentication logic
            // If authentication is successful, redirect to another page
            // return RedirectToPage("/SomePage");
            
            if (ModelState.IsValid)
            {
                if (LoginInfo.Email == "admin@systemshopper.com" && LoginInfo.Password == "123412341234")
                {
                    var claims = new List<Claim> {
                        new Claim("Username", "Admin"),
                        new Claim(ClaimTypes.Name, "Andrew")
                    };

                    var identity = new ClaimsIdentity(claims, "LoginCookie");
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(principal);

                    return RedirectToPage("/Index");
                }
                return Page();
            }
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
