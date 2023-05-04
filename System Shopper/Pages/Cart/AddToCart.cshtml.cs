using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System_Shopper.Models;

namespace System_Shopper.Pages.Cart
{
    public class AddToCartModel : PageModel
    {
        [BindProperty]
        public ShoppingSession ShoppingSession { get; set; } = new ShoppingSession();

        public IActionResult OnGet(int id)
        {
            PopulateShoppingSession();
            AddProductToCart(id);

            return RedirectToPage("/System Builder/Index");
        }

        public void PopulateShoppingSession()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT TOP 1 * FROM ShoppingSession WHERE UserID = 1 " +
                    "ORDER BY ShoppingSessionID DESC";

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

        public void AddProductToCart(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                // Check if Cart contains id
                string sql = "SELECT * FROM Cart WHERE EXISTS " +
                    "(SELECT * FROM Cart WHERE ShoppingSessionID = @shoppingSessionId " +
                    "AND ProductID = @productId)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@shoppingSessionId", ShoppingSession.ShoppingSessionId);
                cmd.Parameters.AddWithValue("productId", id);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                int quantity = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("Quantity")))
                        {
                            quantity = int.Parse(reader["Quantity"].ToString());
                        }
                    }
                }
                reader.Close();

                // If true, increment quantity
                if (quantity > 0)
                {
                    sql = "UPDATE Cart SET Quantity = @quantity WHERE ShoppingSessionID = @shoppingSessionId " +
                        "AND ProductID = @productId";

                    SqlCommand cmd2 = new SqlCommand(sql, conn);
                    cmd2.Parameters.AddWithValue("@quantity", quantity + 1);
                    cmd2.Parameters.AddWithValue("@shoppingSessionId", ShoppingSession.ShoppingSessionId);
                    cmd2.Parameters.AddWithValue("@productId", id);

                    cmd2.ExecuteNonQuery();
                }
                // Else, insert new Cart
                else
                {
                    sql = "INSERT INTO Cart (ShoppingSessionID, ProductID, Quantity) " +
                        "VALUES (@shoppingSessionId, @productId, @quantity)";

                    SqlCommand cmd3 = new SqlCommand(sql, conn);
                    cmd3.Parameters.AddWithValue("@shoppingSessionId", ShoppingSession.ShoppingSessionId);
                    cmd3.Parameters.AddWithValue("@productId", id);
                    cmd3.Parameters.AddWithValue("@quantity", 1);

                    cmd3.ExecuteNonQuery();
                }
            }
        }
    }
}
