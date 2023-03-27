namespace System_Shopper.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string ProductDescription { get; set; } = string.Empty;

        public int ManufacturerId { get; set; }

        public double Price { get; set; }

        public int DiscountId { get; set; }

        public string ProductImage { get; set; } = string.Empty;
    }
}
