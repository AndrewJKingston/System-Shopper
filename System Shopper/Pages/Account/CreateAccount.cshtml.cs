using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System_Shopper.Models;

namespace System_Shopper.Pages.Account
{
    public class CreateAccountModel : PageModel
    {
        public User NewUser { get; set; } = new User();

        public void OnGet()
        {
        }

        public void OnPost() 
        {

        }
    }
}
