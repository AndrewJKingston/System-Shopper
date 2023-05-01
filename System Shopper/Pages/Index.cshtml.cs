using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System_Shopper.Models;

namespace System_Shopper.Pages
{
    public class IndexController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }

    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<Product> ProductList { get; set; } = new List<Product>();

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
//            PopulateDiscounts
            PopulateProducts();
        }

//        PopulateDiscounts()
//        {

//        }

        public void PopulateProducts()
        {
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
                        product.ManufacturerId = int.Parse(reader["ManufacturerId"].ToString());
                        product.Price = decimal.Parse(reader["Price"].ToString());
                        product.DiscountId = int.Parse(reader["DiscountId"].ToString());
                        product.ProductImage = reader["ProductImage"].ToString();
                        product.ProductType = int.Parse(reader["ProductTypeId"].ToString());

                        /*
                        if (discountId != 3) 
                        {
                            product.Price = product.Price * (100 - discountPercent) / 100);
                        }
                        */

                        ProductList.Add(product);
                    }
                }
            }
        }
    }
}
