using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System_Shopper.Models;

namespace System_Shopper.Pages.System_Builder
{
    public class AddProductToListModel : PageModel
    {
        public IActionResult OnGet(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "INSERT INTO ProductList (SystemListID, ProductID) VALUES (@systemListId, @productId)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@systemListId", 1);
                cmd.Parameters.AddWithValue("@productId", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToPage("/System Builder/Index");
        }
    }
}
