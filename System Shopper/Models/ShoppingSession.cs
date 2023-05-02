using System.ComponentModel.DataAnnotations;

namespace System_Shopper.Models
{
    public class ShoppingSession
    {
        [Key]
        public int ShoppingSessionId { get; set; }

        public int UserId { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; }

        public int SessionId { get; set; }
    }
}
