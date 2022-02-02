using KheyaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KheyaShop.Data.ViewModels
{
    public class DropdownVM
    {
        public DropdownVM()
        {
            Categories = new List<Category>();
        }
        
        public List<Category> Categories { get; set; }

    }
}
