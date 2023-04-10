using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System_Shopper.Models;

namespace System_Shopper.Pages.System_Builder
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<Product> ProductList { get; set; } = new List<Product>();

        public void OnGet()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT * FROM Product ORDER BY ProductName";

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
                        ProductList.Add(product);
                    }
                }
            }
        }
    }
}
