using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System_Shopper.Models;

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
        Dictionary<int, int> discounts = PopulateDiscounts();
        PopulateProducts(discounts);
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



    public void PopulateProducts(Dictionary<int, int> discounts)
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

                    int productTypeId;
                    if (int.TryParse(reader["ProductTypeId"].ToString(), out productTypeId))
                    {
                        product.ProductType = productTypeId;
                    }

                    if (discounts.ContainsKey(product.DiscountId))
                    {
                        int DiscountPercent = discounts[product.DiscountId];
                        product.Price = product.Price * (100 - DiscountPercent) / 100;
                    }

                    ProductList.Add(product);
                }
            }
        }
    }

}
