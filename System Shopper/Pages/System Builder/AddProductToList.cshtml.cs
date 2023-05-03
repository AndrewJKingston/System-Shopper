using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System_Shopper.Models;

namespace System_Shopper.Pages.System_Builder
{
    public class AddProductToListModel : PageModel
    {
        [BindProperty]
        public ShoppingSession ShoppingSession { get; set; } = new ShoppingSession();

        [BindProperty]
        public SystemList SystemList { get; set; } = new SystemList();

        public IActionResult OnGet(int id)
        {
            PopulateShoppingSession();
            PopulateSystemList();
            InsertProductList(id);
            AddProductToProductList(id);

            return RedirectToPage("/System Builder/Index");
        }

        public void InsertProductList(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "INSERT INTO ProductList (SystemListID, ProductID) VALUES (@systemListId, @productId)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@systemListId", SystemList.SystemListId);
                cmd.Parameters.AddWithValue("@productId", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
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

        public void AddProductToProductList(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                // Check if ProductList contains id
                string sql = "SELECT * FROM ProductList WHERE EXISTS(SELECT * FROM ProductList WHERE SystemListID = @systemListId AND ProductID = @productId)";
                //string sql = "SELECT * FROM SystemList WHERE EXISTS(SELECT * FROM SystemList WHERE ShoppingSessionID = @shoppingSessionId)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@systemListId", SystemList.SystemListId);
                cmd.Parameters.AddWithValue("@productId", id);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                int quantity = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader.IsDBNull(reader.GetOrdinal("Quantity")))
                        {
                            quantity = 0;
                        }
                        else
                        {
                            quantity = int.Parse(reader["Quantity"].ToString());
                        }
                    }
                }
                reader.Close();

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
        }
    }
}
