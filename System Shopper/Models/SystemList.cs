using System.ComponentModel.DataAnnotations;

namespace System_Shopper.Models
{
    public class SystemList
    {
        [Key]
        public int SystemListId { get; set; }

        public int ShoppingSessionId { get; set; }
    }
}
