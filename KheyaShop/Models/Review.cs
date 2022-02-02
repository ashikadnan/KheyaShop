using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KheyaShop.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        public string ReviewText { get; set; }
        [ForeignKey("ProductId")]
        public string ProductId { get; set; }
        public Product Prouducts { get; set; }
    }
}
