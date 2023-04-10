using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System_Shopper.Models;

namespace System_Shopper.Pages
{

    public class AddProductModel : PageModel
    {
        [BindProperty]
        public Product NewProduct { get; set; } = new Product();

        [BindProperty]
        public List<Manufacturer> GetManufacturerList { get; set; } = new List<Manufacturer>();

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
                string sql = "SELECT * FROM Manufacturer ORDER BY ManufacturerName";

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
                        Manufacturer manufacturer = new Manufacturer();
                        manufacturer.ManufacturerName = reader["ManufacturerName"].ToString();
                        manufacturer.ManufacturerBio = reader["ManufacturerBio"].ToString();
                        manufacturer.ManufacturerId = int.Parse(reader["ManufacturerId"].ToString());
                        manufacturer.ManufacturerLogo = reader["ManufacturerLogo"].ToString();
                        GetManufacturerList.Add(manufacturer);
                    }
                }
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
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
                    string sql = "INSERT INTO Product(ProductName, ProductDescription, Price, ProductImage, ManufacturerId, DiscountId) " +
                        "VALUES(@productName, @productDescription, @price, @productImage, @manufacturerId, @discountId)";

                    // Step 3
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@productName", NewProduct.ProductName);
                    cmd.Parameters.AddWithValue("@productDescription", NewProduct.ProductDescription);
                    cmd.Parameters.AddWithValue("@price", NewProduct.Price);
                    cmd.Parameters.AddWithValue("@productImage", NewProduct.ProductImage);
                    cmd.Parameters.AddWithValue("@manufacturerId", NewProduct.ManufacturerId);
                    cmd.Parameters.AddWithValue("@discountId", 1);

                    // Step 4
                    conn.Open();

                    // Step 5
                    cmd.ExecuteNonQuery();
                }
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
    
    }
