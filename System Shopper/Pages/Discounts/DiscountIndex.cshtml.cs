using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System_Shopper.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace System_Shopper.Pages.Discounts
{
    [Authorize]
    public class DiscountIndex : PageModel
    {
        [BindProperty]
        public Discount NewDiscount { get; set; } = new Discount();

        [BindProperty]
        public List<Discount> GetDiscountList { get; set; } = new List<Discount>();

        public void OnGet()
        {
            // Step 1
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                // Step 2
                string sql = "SELECT DiscountID, DiscountName, DiscountPercent FROM Discount";

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
                        Discount discount = new Discount();
                        discount.DiscountName = reader["DiscountName"].ToString();
                        discount.DiscountPercent = decimal.Parse(reader["DiscountPercent"].ToString());
                        discount.DiscountId = int.Parse(reader["DiscountID"].ToString());
                        GetDiscountList.Add(discount);
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
                    string sql = "INSERT INTO Discount (DiscountName, DiscountPercent) VALUES (@DiscountName, @DiscountPercent)";

                    // Step 3
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@DiscountName", NewDiscount.DiscountName);
                    cmd.Parameters.AddWithValue("@DiscountPercent", NewDiscount.DiscountPercent);

                    // Step 4
                    conn.Open();

                    // Step 5
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Step 6
                    if (rowsAffected > 0)
                    {
                        return RedirectToPage();
                    }
                }
            }
            return Page();
        }
    }
}
