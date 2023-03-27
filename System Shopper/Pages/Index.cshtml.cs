using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System_Shopper.Models;

namespace System_Shopper.Pages
{
    public class IndexModel : PageModel
    {
        public List<Product> ProductList { get; set; } = new List<Product>();

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            /*
             * 1. Create a SQL connection object
             * 2. Construct a SQL statement
             * 3. Create a SQL command object
             * 4. Open the SQL connection
             * 5. Execute the SQL command
             * 6. Close the SQL connection
             */

            // Step 1
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                // Step 2
                string sql = "SELECT * FROM Product ORDER BY ProductName";

                // Step 3
                SqlCommand cmd = new SqlCommand(sql, conn);

                // Step 4
                conn.Open();

                // Step 5
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Product product = new Product();
                        product.ProductId = int.Parse(reader["ProductId"].ToString());
                        product.ProductName = reader["ProductName"].ToString();
                        product.ProductDescription = reader["ProductDescription"].ToString();
                        product.ProductImage = reader["ProductImage"].ToString();
                        product.Price = double.Parse(reader["Price"].ToString());
                        product.DiscountId = int.Parse(reader["DiscountId"].ToString());
                        ProductList.Add(product);

                    }
                }
            }

        }
    }
}