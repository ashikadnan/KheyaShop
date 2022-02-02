using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KheyaShop.Models
{
    public class ProductUnit
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Product Unit")]
        [Required(ErrorMessage ="Product Unit is required")]
        public string Unit { get; set; }
    }
}
