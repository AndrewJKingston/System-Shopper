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

            return RedirectToPage("/Cart/Cart");
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

        public void AddProductToCart(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "INSERT INTO Cart (ShoppingSessionID, ProductID, Quantity) VALUES (@shoppingSessionId, @productId, @quantity)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@shoppingSessionId", ShoppingSession.ShoppingSessionId);
                cmd.Parameters.AddWithValue("@productId", id);
                cmd.Parameters.AddWithValue("@quantity", 1);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
