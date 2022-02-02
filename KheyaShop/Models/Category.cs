using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KheyaShop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string ParentCategory { get; set; }
        public string CategoryMain { get; set; }

        //Relationships

    }
}
