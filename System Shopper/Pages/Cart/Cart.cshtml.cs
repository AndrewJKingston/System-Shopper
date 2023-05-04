using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System_Shopper.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace System_Shopper.Pages.Cart
{
    public class IndexModel : PageModel
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        public void OnGet()
        {
            PopulateCartProducts();
        }

        public void PopulateCartProducts()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                // Assuming there is a ShoppingSessionID associated with the UserID (1 in this case)
                string sql = @"
                    SELECT 
                        P.ProductID, P.ProductName, P.ProductDescription, P.ManufacturerID, 
                        P.Price, P.DiscountID, P.ProductImage, C.Quantity, D.DiscountPercent
                    FROM Cart C
                    JOIN Product P ON C.ProductID = P.ProductID
                    JOIN ShoppingSession SS ON C.ShoppingSessionID = SS.ShoppingSessionID
                    JOIN Discount D ON P.DiscountID = D.DiscountID
                    WHERE SS.UserID = 1;
                ";

                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CartItem cartItem = new CartItem();
                        cartItem.ProductId = int.Parse(reader["ProductID"].ToString());
                        cartItem.ProductName = reader["ProductName"].ToString();
                        cartItem.ProductDescription = reader["ProductDescription"].ToString();
                        cartItem.ManufacturerId = int.Parse(reader["ManufacturerID"].ToString());
                        cartItem.Price = decimal.Parse(reader["Price"].ToString());
                        cartItem.DiscountId = int.Parse(reader["DiscountID"].ToString());
                        cartItem.ProductImage = reader["ProductImage"].ToString();
                        cartItem.Quantity = int.Parse(reader["Quantity"].ToString());
                        cartItem.DiscountPercent = decimal.Parse(reader["DiscountPercent"].ToString());
                        CartItems.Add(cartItem);
                    }
                }
            }
        }

        public IActionResult OnPostDeleteFromCart(int id)
        {
            // Delete the product from the cart table in the database
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                // Assuming there is a ShoppingSessionID associated with the UserID (1 in this case)
                string sql = @"
                    DELETE FROM Cart 
                    WHERE ProductID = @productId 
                    AND ShoppingSessionID IN (
                        SELECT TOP 1 ShoppingSessionID 
                        FROM ShoppingSession 
                        WHERE UserID = 1 
                        ORDER BY ShoppingSessionID DESC
                    );
                ";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@productId", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToPage();
        }
    }
}
