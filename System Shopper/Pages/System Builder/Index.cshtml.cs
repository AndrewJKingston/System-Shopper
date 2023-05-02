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
        public ShoppingSession ShoppingSession { get; set; } = new ShoppingSession();

        [BindProperty]
        public SystemList SystemList { get; set; } = new SystemList();

        //        [BindProperty]
        //        String filterText { get; set; }

        [BindProperty]
        public List<ProductList> ProductList { get; set; } = new List<ProductList>();

        [BindProperty]
        public List<Product> ProductsInList { get; set; } = new List<Product>();

        public void OnGet()
        {
            PopulateShoppingSession();
            PopulateSystemList();
            PopulateProducts();
            PopulateProductList(SystemList.SystemListId);
            PopulateProductsInList();
        }

        public void PopulateShoppingSession()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT TOP 1 * FROM ShoppingSession WHERE UserID = 1 ORDER BY ShoppingSessionID DESC";

                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ShoppingSession.ShoppingSessionId = int.Parse(reader["ShoppingSessionID"].ToString());
                    }
                }
            }
        }

        public void PopulateSystemList()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT * FROM SystemList WHERE EXISTS(SELECT * FROM SystemList WHERE ShoppingSessionID = @shoppingSessionId)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@shoppingSessionId", ShoppingSession.ShoppingSessionId);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        SystemList.SystemListId = int.Parse(reader["SystemListID"].ToString());
                        SystemList.ShoppingSessionId = int.Parse(reader["ShoppingSessionID"].ToString());
                    }
                }
                reader.Close();
                if (SystemList.SystemListId == 0)
                {
                    sql = "INSERT INTO SystemList (ShoppingSessionID) VALUES (@shoppingSessionId)";
                    SqlCommand cmd2 = new SqlCommand(sql, conn);
                    cmd2.Parameters.AddWithValue("@shoppingSessionId", ShoppingSession.ShoppingSessionId);
                    cmd2.ExecuteNonQuery();

                    sql = "SELECT * FROM SystemList WHERE ShoppingSessionID = @shoppingSessionId";
                    SqlCommand cmd3 = new SqlCommand(sql, conn);
                    cmd3.Parameters.AddWithValue("@shoppingSessionId", ShoppingSession.ShoppingSessionId);

                    SqlDataReader reader2 = cmd3.ExecuteReader();
                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {
                            SystemList.SystemListId = int.Parse(reader2["SystemListID"].ToString());
                        }
                    }
                }
                /*                
                
                string sql2 = "SELECT * FROM ProductList WHERE SystemListID=@systemListId";

                SqlCommand cmd2 = new SqlCommand(sql2, conn);
                conn.Open();

                cmd2.Parameters.AddWithValue("@systemListId", SystemList.SystemListId);
                */
            }
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
                string sql = "SELECT * FROM ProductList WHERE EXISTS(SELECT * FROM ProductList WHERE SystemListID = @systemListId)";

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
                        if (reader.IsDBNull(reader.GetOrdinal("Quantity")))
                        {
                            productList.Quantity = 0;
                        }
                        else
                        {
                            productList.Quantity = int.Parse(reader["Quantity"].ToString());
                        }
                        ProductList.Add(productList);
                    }
                }
            }
        }

        public void PopulateProductsInList()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "SELECT * FROM Product WHERE ProductID = @productId";
                SqlCommand cmd;
                SqlDataReader reader;
                Product product;

                for (int i = 0; i < ProductList.Count; i++)
                {
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@productId", ProductList[i].ProductID.ToString());
                    conn.Open();

                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            product = new Product();
                            product.ProductId = int.Parse(reader["ProductId"].ToString());
                            product.ProductName = reader["ProductName"].ToString();
                            product.ProductDescription = reader["ProductDescription"].ToString();
                            product.ManufacturerId = int.Parse(reader["ManufacturerId"].ToString());
                            product.Price = decimal.Parse(reader["Price"].ToString());
                            product.DiscountId = int.Parse(reader["DiscountId"].ToString());
                            product.ProductImage = reader["ProductImage"].ToString();
                            ProductsInList.Add(product);
                        }
                    }
                    conn.Close();
                    reader.Close();
                }
            }
        }

        public IActionResult OnPost(int id)
        {
            /*
            using (SqlConnection conn = new SqlConnection( DBHelper.GetConnectionString()))
            {
                string sql = "INSERT INTO ProductList (SystemListID, ProductID) VALUES (@systemListId, @productId)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@systemListId", SystemList.SystemListId);
                cmd.Parameters.AddWithValue("@productId", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            */
            return Page();
            
        }
    }
}
