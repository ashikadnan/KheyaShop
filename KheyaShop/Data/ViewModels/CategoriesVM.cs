using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KheyaShop.Data.ViewModels
{
    public class CategoriesVM
    {
        public int Id { get; set; }

        [Display(Name = "Choose Parent Category")]
        public string ParentCategory { get; set; }
        [Required(ErrorMessage = "Category name is required")]
        [Display(Name ="Category Name")]
        public string CategoryMain { get; set; }
    }
}
