using System.ComponentModel.DataAnnotations;

namespace System_Shopper.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [StringLength(300)]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "This field is required.")]
        [StringLength(1000)]
        public string ProductDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "This field is required.")]
        public int ManufacturerId { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public decimal Price { get; set; }

        public int DiscountId { get; set; }
        
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(300)]
        public string ProductImage { get; set; } = string.Empty;

        public int ProductType { get; set; }
    }
}
