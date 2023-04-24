using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System_Shopper.Models;

namespace System_Shopper.Pages.System_Builder
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<Product> Products { get; set; } = new List<Product>();

        [BindProperty]
        String filterText { get; set; }
        [BindProperty]
        public List<ProductList> ProductList { get; set; } = new List<ProductList>();

        [BindProperty]
        public List<Product> SystemList { get; set; } = new List<Product>();

        public void OnGet(int id)
        {
            PopulateProducts();
            PopulateProductList(id);
        }

        public void PopulateProducts()
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
                        Products.Add(product);
                    }
                }
            }
        }

        public void PopulateProductList(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT * FROM ProductList WHERE SystemListID = @systemListId";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@systemListId", id);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductList productList = new ProductList();
                        productList.SystemListID = int.Parse(reader["SystemListID"].ToString());
                        productList.ProductID = int.Parse(reader["ProductID"].ToString());
                        ProductList.Add(productList);
                    }
                }
            }
        }

        public void OnPost(int id)
        {

        }
    }
}
