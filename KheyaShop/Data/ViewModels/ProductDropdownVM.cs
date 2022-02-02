using KheyaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KheyaShop.Data.ViewModels
{
    public class ProductDropdownVM
    {

        public ProductDropdownVM()
        {
            Categories = new List<Category>();
            ProductUnits = new List<ProductUnit>();
        }
        public List <Category> Categories { get; set; }
        public List <ProductUnit> ProductUnits { get; set; }
    }
}
