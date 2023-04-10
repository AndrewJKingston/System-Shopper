using System.ComponentModel.DataAnnotations;

namespace System_Shopper.Models
{
    public class Discount
    {
        [Key]
        public int DiscountId { get; set; }

        [StringLength(100)]
        public string DiscountName { get; set; } = string.Empty;

        public decimal DiscountPercent { get; set; }


    }
}
