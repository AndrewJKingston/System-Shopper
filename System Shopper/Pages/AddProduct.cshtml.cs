using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System_Shopper.Models;

namespace System_Shopper.Pages
{
    public class HomeController : Controller
    {
        public ActionResult IndexTuple()
        {
            var tupleModel = new Tuple<AddProductModel, Manufacturers.IndexModel>(new AddProductModel(), new Manufacturers.IndexModel());
            return View(tupleModel);
        }
    }

    public class AddProductModel : PageModel
    {
        [BindProperty]
        public Product NewProduct { get; set; } = new Product();

        public void OnGet()
        {
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

                    //string getManufacturerId = "SELECT ManufacturerId FROM Manufacturer WHERE ManufacturerName='@manufacturerName'";

                    // Step 2
                    string sql = "INSERT INTO Product(ProductName, ProductDescription, Price, ProductImage, ManufacturerId) " +
                        "VALUES(@productName, @productDescription, @price, @productImage, @manufacturerId)";

                    //SqlCommand cmdOne = new SqlCommand(sql, conn);
                    //cmdOne.Parameters.AddWithValue("@manufacturerName", NewProduct.ManufacturerId);

                    // Step 3
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@productName", NewProduct.ProductName);
                    cmd.Parameters.AddWithValue("@productDescription", NewProduct.ProductDescription);
                    cmd.Parameters.AddWithValue("@price", NewProduct.Price);
                    cmd.Parameters.AddWithValue("@productImage", NewProduct.ProductImage);
                    cmd.Parameters.AddWithValue("@manufacturerId", NewProduct.ManufacturerId);

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
