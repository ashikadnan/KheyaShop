using KheyaShop.Models;
using System.Collections.Generic;

namespace KheyaShop.Data.ViewModels
{
    public class HomeVM
    {
        public Product Products { get; set; }

        public Category Categories { get; set; }
        public LoginVM LoginVM { get; set; }

        public RegisterVM RegisterVM { get; set; }    
    }
}
