using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System_Shopper.Models;

namespace System_Shopper.Pages.Manufacturers
{
    public class IndexModel : PageModel
    {
        public void OnGet()
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
            SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString());

            // Step 2
            string sql = "SELECT * FROM Manufacturer ORDER BY ManufacturerName";

            // Step 3
            SqlCommand cmd = new SqlCommand(sql, conn);

            // Step 4
            conn.Open();

            // Step 5
            cmd.ExecuteNonQuery();

            // Step 6
            conn.Close();

        }
    }
}
