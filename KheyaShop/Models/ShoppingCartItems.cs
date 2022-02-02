using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KheyaShop.Models
{
    public class ShoppingCartItems
    {
        [Key]
        public int Id { get; set; }

        public Product Product { get; set; }

        public int amount { get; set; }

        public string ShoppingCartId { get; set; }


    }
}
