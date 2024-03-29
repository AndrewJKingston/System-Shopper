using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using System_Shopper.Models;

namespace System_Shopper.Pages.Account
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

        public async Task<IActionResult> OnPost()
        {
            // TODO: Add authentication logic
            // If authentication is successful, redirect to another page
            // return RedirectToPage("/SomePage");

            if (ModelState.IsValid)
            {
                if (LoginInfo.Email == "admin@systemshopper.com" && LoginInfo.Password == "1234")
                {
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.Email, "Admin"),
                    new Claim(ClaimTypes.Name, "Andrew")
                    };

                    var identity = new ClaimsIdentity(claims, "LoginCookie");
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync("LoginCookie", principal);

                    using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
                    {
                        string sql = "INSERT INTO ShoppingSession (UserId) Values(1)";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
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
