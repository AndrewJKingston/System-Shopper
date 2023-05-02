using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System_Shopper.Models;

namespace System_Shopper.Pages
{
    [Authorize]
    public class AddProductModel : PageModel
    {
        [BindProperty]
        public Product NewProduct { get; set; } = new Product();

        [BindProperty]
        public List<SelectListItem> Manufacturers { get; set; } = new List<SelectListItem>();

        [BindProperty]
        public List<SelectListItem> ProductTypes { get; set; } = new List<SelectListItem>();

        [BindProperty]
        public List<SelectListItem> Discounts { get; set; } = new List<SelectListItem>();

        public void OnGet()
        {
            PopulateManufacturers();
            PopulateDiscounts();
            PopulateProductTypes();
        }

        private void PopulateManufacturers()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT * FROM Manufacturer ORDER BY ManufacturerName";
                SqlCommand cmd = new SqlCommand(sql, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SelectListItem manufacturer = new SelectListItem();
                        manufacturer.Value = reader["ManufacturerId"].ToString();
                        manufacturer.Text = reader["ManufacturerName"].ToString();
                        Manufacturers.Add(manufacturer);
                    }
                }
            }
        }

        private void PopulateProductTypes()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT * FROM ProductType ORDER BY ProductType";
                SqlCommand cmd = new SqlCommand(sql, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SelectListItem productType = new SelectListItem();
                        productType.Value = reader["ProductTypeId"].ToString();
                        productType.Text = reader["ProductType"].ToString();
                        ProductTypes.Add(productType);
                    }
                }
            }
        }

        private void PopulateDiscounts()
        {
            using (SqlConnection conn = new SqlConnection( DBHelper.GetConnectionString()))
            {
                string sql = "SELECT * FROM Discount ORDER BY DiscountPercent";
                SqlCommand cmd = new SqlCommand(sql, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SelectListItem discount = new SelectListItem();
                        discount.Value = reader["DiscountId"].ToString();
                        discount.Text = reader["DiscountName"].ToString() + ": " + reader["DiscountPercent"].ToString();
                        Discounts.Add(discount);
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
                    string sql = "INSERT INTO Product(ProductName, ProductDescription, Price, ProductImage, ManufacturerId, DiscountId, ProductTypeId) " +
                        "VALUES(@productName, @productDescription, @price, @productImage, @manufacturerId, @discountId, @productType)";

                    // Step 3
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@productName", NewProduct.ProductName);
                    cmd.Parameters.AddWithValue("@productDescription", NewProduct.ProductDescription);
                    cmd.Parameters.AddWithValue("@price", NewProduct.Price);
                    cmd.Parameters.AddWithValue("@productImage", NewProduct.ProductImage);
                    cmd.Parameters.AddWithValue("@manufacturerId", NewProduct.ManufacturerId);
                    cmd.Parameters.AddWithValue("@discountId", NewProduct.DiscountId);
                    cmd.Parameters.AddWithValue("@productType", NewProduct.ProductType);

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
