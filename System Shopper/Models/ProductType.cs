using System.ComponentModel.DataAnnotations;

namespace System_Shopper.Models
{
    public class ProductType
    {
        [Key]
        public int ProductTypeId { get; set; }

        [StringLength(100)]
        public string ProductTypeName { get; set; } = string.Empty;
    }
}
