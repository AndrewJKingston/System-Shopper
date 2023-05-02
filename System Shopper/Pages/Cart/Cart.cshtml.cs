using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System_Shopper.Models;
using System.Collections.Generic;

namespace System_Shopper.Pages.Cart
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<Product> CartProducts { get; set; } = new List<Product>();

        public void OnGet()
        {
            PopulateCartProducts();
        }

        public void PopulateCartProducts()
        {
            // Fetch cart items and their related information from the database
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT * FROM CartItems JOIN Products ON CartItems.ProductId = Products.ProductId"; // Update the SQL query based on your database schema

                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Product product = new Product();
                        product.ProductId = int.Parse(reader["ProductId"].ToString());
                        product.ProductName = reader["ProductName"].ToString();
                        product.ProductDescription = reader["ProductDescription"].ToString();
                        product.ManufacturerId = int.Parse(reader["ManufacturerId"].ToString());
                        product.Price = decimal.Parse(reader["Price"].ToString());
                        product.DiscountId = int.Parse(reader["DiscountId"].ToString());
                        product.ProductImage = reader["ProductImage"].ToString();
                        CartProducts.Add(product);
                    }
                }
            }
        }

        public IActionResult OnPostAddToCart(int id)
        {
            // Insert the product into the cart table in the database
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "INSERT INTO CartItems (ProductId) VALUES (@productId)"; // Update the SQL query based on your database schema

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@productId", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToPage();
        }
    }
}
