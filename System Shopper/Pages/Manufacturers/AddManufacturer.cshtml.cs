using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System_Shopper.Models;

namespace System_Shopper.Pages
{
    public class AddManufacturerModel : PageModel
    {
        [BindProperty]
        public Manufacturer NewManufacturer { get; set; } = new Manufacturer();
        
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                /*
                 * 1. Create a SQL connection object
                 * 2. Construct a SQL statement
                 * 3. Create a SQL command object
                 * 4. Open the SQL connection
                 * 5. Execute the SQL command
                 * 6. Close the SQL connection
                 */

                // Step 1
                using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
                {
                    // Step 2
                    string sql = "INSERT INTO Manufacturer(ManufacturerName, ManufacturerBio, ManufacturerLogo) " +
                        "VALUES(@manufacturerName, @manufacturerBio, @manufacturerLogo)";

                    // Step 3
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@manufacturerName", NewManufacturer.ManufacturerName);
                    cmd.Parameters.AddWithValue("@manufacturerBio", NewManufacturer.ManufacturerBio);
                    cmd.Parameters.AddWithValue("@manufacturerLogo", NewManufacturer.ManufacturerLogo);

                    // Step 4
                    conn.Open();

                    // Step 5
                    cmd.ExecuteNonQuery();
                }
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
