namespace KheyaShop.Models
{
    public class Product_Review
    {
        public int ReviewId { get; set; }
        public Review Review { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
