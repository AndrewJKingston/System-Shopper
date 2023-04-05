using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System_Shopper.Models;

namespace System_Shopper.Pages.Manufacturers
{
    public class EditManufacturerModel : PageModel
    {
        [BindProperty]
        public Manufacturer ExistingManufacturer { get; set; } = new Manufacturer();

        [BindProperty]
        public Manufacturer NewManufacturer { get; set; } = new Manufacturer();

        public void OnGet(int id)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
                {
                    string sql = "SELECT * FROM Manufacturer WHERE ManufacturerID = @manufacturerID";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@manufacturerID", id);
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        ExistingManufacturer.ManufacturerName = reader["ManufacturerName"].ToString();
                        ExistingManufacturer.ManufacturerBio = reader["ManufacturerBio"].ToString();
                        ExistingManufacturer.ManufacturerLogo = reader["ManufacturerLogo"].ToString();
                    }
                }
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
                {
                    string sql = "UPDATE Manufacturer " +
                        "SET ManufacturerName = @manufacturerName, ManufacturerBio = @manufacturerBio, ManufacturerLogo = @manufacturerLogo " +
                        "WHERE ManufacturerID = @manufacturerID;";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@manufacturerName", NewManufacturer.ManufacturerName);
                    cmd.Parameters.AddWithValue("@manufacturerBio", NewManufacturer.ManufacturerBio);
                    cmd.Parameters.AddWithValue("@manufacturerLogo", NewManufacturer.ManufacturerLogo);
                    cmd.Parameters.AddWithValue("@manufacturerID", ExistingManufacturer.ManufacturerId);
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
