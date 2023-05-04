namespace System_Shopper.Models
{
    public class CartItem : Product
    {
        public int Quantity { get; set; }
        public decimal DiscountPercent { get; set; }
    }
}
