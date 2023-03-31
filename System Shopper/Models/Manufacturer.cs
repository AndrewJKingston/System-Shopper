using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace System_Shopper.Models
{
    public class Manufacturer
    {
        [Key]
        public int ManufacturerId { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [StringLength(100, MinimumLength = 1)]
        [DisplayName("Manufacturer Name:")]
        public string ManufacturerName { get; set;} = string.Empty;

        [Required(ErrorMessage = "This field is required.")]
        [StringLength(1000)]
        [DisplayName("Manufacturer Bio:")]
        public string ManufacturerBio { get; set; } = string.Empty;

        [DisplayName("Manufacturer Logo URL:")]
        public string ManufacturerLogo { get; set; } = string.Empty;
    }
}
