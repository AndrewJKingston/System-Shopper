using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System_Shopper.Models;

namespace System_Shopper.Pages
{
    [Authorize]
    public class DeleteProductModel : PageModel
    {
        [BindProperty]
        public Product ExistingProduct { get; set; } = new Product();

        public IActionResult OnGet(int id)
        {
            ExistingProduct.ProductId = id;
            RemoveProduct();
            return RedirectToPage("Index");
        }

        private void RemoveProduct()
        {
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                string sql = "DELETE FROM Product WHERE ProductId = @productId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@productId", ExistingProduct.ProductId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
