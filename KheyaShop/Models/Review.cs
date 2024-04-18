using System.Collections.Generic;

namespace KheyaShop.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string ReviewText { get; set; }

        public string UserId { get; set; }
        
        public string UserName { get; set; }
        public ApplicationUser User { get; set; }   

        public int ProductId { get; set; }

        public List<Product_Review> ProductReviewObject { get; set; }
    }
}
