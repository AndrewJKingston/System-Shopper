using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System_Shopper.Models;

namespace System_Shopper.Pages.Manufacturers
{
    [Authorize]
    public class DeleteManufacturerModel : PageModel
    {
        public IActionResult OnGet(int id)
        {
            using(SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "DELETE FROM Manufacturers WHERE ManufacturerId = @manufacturerId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@manufacturerId", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToPage("Index");
        }
    }
}
