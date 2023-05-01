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
                // Check if ProductList contains id
                string sql = "SELECT * FROM ProductList WHERE SystemListID = 1";

                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                int quantity = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        quantity = int.Parse(reader["Quantity"].ToString());

                    }
                }
                // If true, increment quantity
                if (quantity > 0)
                {
                    sql = "UPDATE ProductList SET Quantity = @quantity WHERE ProductID = @productId";

                    SqlCommand cmd2 = new SqlCommand(sql, conn);

                    cmd2.Parameters.AddWithValue("@quantity", quantity + 1);
                    cmd2.Parameters.AddWithValue("@productId", id);

                    cmd2.ExecuteNonQuery();
                }
                // Else, insert new ProductList
                else
                {
                    sql = "INSERT INTO ProductList (SystemListID, ProductID, Quantity) VALUES (@systemListId, @productId, @quantity)";

                    SqlCommand cmd3 = new SqlCommand(sql, conn);
                    cmd3.Parameters.AddWithValue("@systemListId", 1);
                    cmd3.Parameters.AddWithValue("@productId", id);
                    cmd3.Parameters.AddWithValue("@quantity", 1);

                    cmd3.ExecuteNonQuery();
                }
            }
            return RedirectToPage("/System Builder/Index");
        }
    }
}
