using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System_Shopper.Models;

namespace System_Shopper.Pages.System_Builder
{
    public class RemoveProductFromListModel : PageModel
    {
        [BindProperty]
        public ShoppingSession ShoppingSession { get; set; } = new ShoppingSession();

        [BindProperty]
        public SystemList SystemList { get; set; } = new SystemList();

        public IActionResult OnGet(int id)
        {
            PopulateShoppingSession();
            PopulateSystemList();
            RemoveProductFromList(id);

            return RedirectToPage("/System Builder/Index");
        }

        public void PopulateShoppingSession()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT TOP 1 * FROM ShoppingSession WHERE UserID = 1 ORDER BY ShoppingSessionID DESC";

                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ShoppingSession.ShoppingSessionId = int.Parse(reader["ShoppingSessionID"].ToString());
                    }
                }
            }
        }

        public void PopulateSystemList()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT * FROM SystemList WHERE EXISTS(SELECT * FROM SystemList WHERE ShoppingSessionID = @shoppingSessionId)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@shoppingSessionId", ShoppingSession.ShoppingSessionId);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SystemList.SystemListId = int.Parse(reader["SystemListID"].ToString());
                        SystemList.ShoppingSessionId = int.Parse(reader["ShoppingSessionID"].ToString());
                    }
                }
                reader.Close();
                if (SystemList.SystemListId == 0)
                {
                    sql = "INSERT INTO SystemList (ShoppingSessionID) VALUES (@shoppingSessionId)";
                    SqlCommand cmd2 = new SqlCommand(sql, conn);
                    cmd2.Parameters.AddWithValue("@shoppingSessionId", ShoppingSession.ShoppingSessionId);
                    cmd2.ExecuteNonQuery();

                    sql = "SELECT * FROM SystemList WHERE ShoppingSessionID = @shoppingSessionId";
                    SqlCommand cmd3 = new SqlCommand(sql, conn);
                    cmd3.Parameters.AddWithValue("@shoppingSessionId", ShoppingSession.ShoppingSessionId);

                    SqlDataReader reader2 = cmd3.ExecuteReader();
                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {
                            SystemList.SystemListId = int.Parse(reader2["SystemListID"].ToString());
                        }
                    }
                }
                /*                

                string sql2 = "SELECT * FROM ProductList WHERE SystemListID=@systemListId";

                SqlCommand cmd2 = new SqlCommand(sql2, conn);
                conn.Open();

                cmd2.Parameters.AddWithValue("@systemListId", SystemList.SystemListId);
                */
            }
        }

        public void RemoveProductFromList(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                // Check if quantity is > 1
                string sql = "SELECT * FROM ProductList WHERE SystemListID = @systemListId AND ProductID = @productId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("systemListId", SystemList.SystemListId);
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
                    sql = "UPDATE ProductList SET Quantity = @quantity WHERE SystemListID = @systemListId AND ProductID = @productId";
                    SqlCommand cmd2 = new SqlCommand(sql, conn);
                    cmd2.Parameters.AddWithValue("@systemListId", SystemList.SystemListId);
                    cmd2.Parameters.AddWithValue("@quantity", quantity - 1);
                    cmd2.Parameters.AddWithValue("@productId", id);

                    cmd2.ExecuteNonQuery();
                }
                // Else, delete ProductList
                else
                {
                    sql = "DELETE FROM ProductList WHERE SystemListID = @systemListId AND ProductID = @productId";

                    SqlCommand cmd3 = new SqlCommand(sql, conn);
                    cmd3.Parameters.AddWithValue("@systemListId", SystemList.SystemListId);
                    cmd3.Parameters.AddWithValue("@productId", id);

                    cmd3.ExecuteNonQuery();
                }

            }

        }
    }
}
