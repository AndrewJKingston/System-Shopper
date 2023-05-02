using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System_Shopper.Models;

namespace System_Shopper.Pages
{
    public class ProductPageModel : PageModel
    {
        [BindProperty]
        public Product ExistingProduct { get; set; } = new Product();

        private readonly ILogger<ProductPageModel> _logger;

        public ProductPageModel(ILogger<ProductPageModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(int id)
        {
            ExistingProduct.ProductId = id;
            Dictionary<int, int> discounts = PopulateDiscounts();
            PopulateProduct(discounts);
        }

        public Dictionary<int, int> PopulateDiscounts()
        {
            Dictionary<int, int> discounts = new Dictionary<int, int>();

            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT * FROM Discount";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int discountId;
                        int discountPercent;
                        if (int.TryParse(reader["DiscountId"].ToString(), out discountId) &&
                            int.TryParse(reader["DiscountPercent"].ToString(), out discountPercent))
                        {
                            discounts[discountId] = discountPercent;
                        }
                    }
                }
            }

            return discounts;
        }



        public void PopulateProduct(Dictionary<int, int> discounts)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT * FROM Product WHERE ProductId=@productId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@productId", ExistingProduct.ProductId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ExistingProduct.ProductName = reader["ProductName"].ToString();
                        ExistingProduct.ProductDescription = reader["ProductDescription"].ToString();
                        ExistingProduct.ManufacturerId = int.Parse(reader["ManufacturerId"].ToString());
                        ExistingProduct.Price = decimal.Parse(reader["Price"].ToString());
                        ExistingProduct.DiscountId = int.Parse(reader["DiscountId"].ToString());
                        ExistingProduct.ProductImage = reader["ProductImage"].ToString();

                        int productTypeId;
                        if (int.TryParse(reader["ProductTypeId"].ToString(), out productTypeId))
                        {
                            ExistingProduct.ProductType = productTypeId;
                        }

                        if (discounts.ContainsKey(ExistingProduct.DiscountId))
                        {
                            int DiscountPercent = discounts[ExistingProduct.DiscountId];
                            ExistingProduct.Price = ExistingProduct.Price * (100 - DiscountPercent) / 100;
                        }
                    }
                }
            }
        }
    }
}
