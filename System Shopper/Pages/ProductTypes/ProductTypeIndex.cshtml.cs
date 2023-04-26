using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System_Shopper.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace System_Shopper.Pages.ProductTypes
{
    [Authorize]
    public class ProductTypeIndex : PageModel
    {
        [BindProperty]
        public ProductType NewProductType { get; set; } = new ProductType();

        [BindProperty]
        public List<ProductType> GetProductTypeList { get; set; } = new List<ProductType>();

        public void OnGet()
        {
            // Step 1
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                // Step 2
                string sql = "SELECT * FROM ProductType ORDER BY ProductType";

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
                        ProductType productType = new ProductType();
                        productType.ProductTypeName = reader["ProductType"].ToString();

                        GetProductTypeList.Add(productType);
                    }
                }
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Step 1
                using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
                {
                    // Step 2
                    string sql = "INSERT INTO ProductType(ProductType) VALUES(@ProductType)";

                    // Step 3
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ProductType", NewProductType.ProductTypeName);

                    // Step 4
                    conn.Open();

                    // Step 5
                    cmd.ExecuteNonQuery();
                }
                return RedirectToPage("/Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
