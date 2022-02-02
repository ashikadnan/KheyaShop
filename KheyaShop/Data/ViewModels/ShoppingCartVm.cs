using KheyaShop.Data.cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KheyaShop.Data.ViewModels
{
    public class ShoppingCartVm
    {
        public ShoppingCart ShoppingCart { get; set; }

        public double ShoppingCartTotal { get; set; }
    }
}
