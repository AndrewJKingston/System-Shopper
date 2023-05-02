using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System_Shopper.Models;

namespace System_Shopper.Pages.System_Builder
{
    public class RemoveProductFromListModel : PageModel
    {
        public IActionResult OnGet(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                // Check if quantity is > 1
                string sql = "SELECT * FROM ProductList WHERE SystemListID = 1 AND ProductID = @productId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@productId", id);

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
                reader.Close();

                // If quantity > 1, update quantity - 1
                if (quantity > 1)
                {
                    sql = "UPDATE ProductList SET Quantity = @quantity WHERE ProductID = @productId";
                    SqlCommand cmd2 = new SqlCommand(sql, conn);
                    cmd2.Parameters.AddWithValue("@quantity", quantity - 1);
                    cmd2.Parameters.AddWithValue("@productId", id);

                    cmd2.ExecuteNonQuery();
                }
                // Else, delete ProductList
                else
                {
                    sql = "DELETE FROM ProductList WHERE ProductID = @productId";

                    SqlCommand cmd3 = new SqlCommand(sql, conn);
                    cmd3.Parameters.AddWithValue("@productId", id);

                    cmd3.ExecuteNonQuery();
                }

            }
            return RedirectToPage("/System Builder/Index");
        }
    }
}
