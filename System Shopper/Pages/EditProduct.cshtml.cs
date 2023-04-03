using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System_Shopper.Models;

namespace System_Shopper.Pages
{
    public class EditProductModel : PageModel
    {
        [BindProperty]
        public Product ExistingProduct { get; set; } = new Product();

        [BindProperty]
        public List<Manufacturer> GetManufacturerList { get; set; } = new List<Manufacturer>();

        public void OnGet(int id)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
                {
                    string sql = "SELECT * FROM Product WHERE ProductId = @productId";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@productId", id);
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        ExistingProduct.ProductName = reader["ProductName"].ToString();
                        ExistingProduct.ProductDescription = reader["ProductDescription"].ToString();
                        ExistingProduct.ManufacturerId = (int)reader["ManufacturerId"];
                        ExistingProduct.Price = (decimal)reader["Price"];
                        ExistingProduct.DiscountId = (int)reader["DiscountId"];
                        ExistingProduct.ProductImage = reader["ProductImage"].ToString();
                    }
                    reader.Close();
                    string sqlManufacturer = "SELECT * FROM Manufacturer ORDER BY ManufacturerName";

                    SqlCommand cmdManufacturer = new SqlCommand(sqlManufacturer, conn);

                    SqlDataReader readerTwo = cmdManufacturer.ExecuteReader();
                    if (readerTwo.HasRows)
                    {
                        while (readerTwo.Read())
                        {
                            Manufacturer manufacturer = new Manufacturer();
                            manufacturer.ManufacturerName = readerTwo["ManufacturerName"].ToString();
                            manufacturer.ManufacturerBio = readerTwo["ManufacturerBio"].ToString();
                            manufacturer.ManufacturerId = int.Parse(readerTwo["ManufacturerId"].ToString());
                            manufacturer.ManufacturerLogo = readerTwo["ManufacturerLogo"].ToString();
                            GetManufacturerList.Add(manufacturer);
                        }
                    }
                    readerTwo.Close();
                }
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
                {
                    string sql = "UPDATE Product " +
                        "SET ProductName = @productName, ProductDescription = @productDescription, ManufacturerId = @manufacturerId, Price = @price, DiscoundId = @discountId, ProductImage = @productImage" +
                        "WHERE ProductId = @productId;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@productName", ExistingProduct.ProductName);
                    cmd.Parameters.AddWithValue("@productDescription", ExistingProduct.ProductDescription);
                    cmd.Parameters.AddWithValue("@manufacturerId", ExistingProduct.ManufacturerId);
                    cmd.Parameters.AddWithValue("@price", ExistingProduct.Price);
                    cmd.Parameters.AddWithValue("@discountId", ExistingProduct.DiscountId);
                    cmd.Parameters.AddWithValue("@productImage", ExistingProduct.ProductImage);
                    conn.Open();

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
