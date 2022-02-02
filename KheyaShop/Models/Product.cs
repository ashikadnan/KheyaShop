using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KheyaShop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Enter product name!")]
        [Display(Name ="Product Name")]
        public string ProductName { get; set; }
        
        [Required(ErrorMessage = "Enter product price!")]
        [Display(Name = "Product Price")]
        public double ProductPrice { get; set; }
       
        [Required(ErrorMessage = "Enter product unit value!")]
        [Display(Name = "Product Unit Value")]
        public int ProductUnitValue { get; set; }
        
        [Required(ErrorMessage = "Enter product quantity!")]
        [Display(Name = "Quantity")]
        public int ProductQuantity { get; set; }
       
        [Required(ErrorMessage = "Enter product SKU!")]
        [Display(Name = "Product SKU")]
        public string ProductSku { get; set; }

        //public string ProductCategory { get; set; }
        [Required(ErrorMessage = "Enter product short description!")]
        [Display(Name = "Short Description")]
        public string ShortDesciption { get; set; }
       
        [Required(ErrorMessage = "Enter product long description!")]
        [Display(Name = "Long Description")]
        public string LongDesciption { get; set; }
        
        [Required(ErrorMessage = "Upload product image!")]
        [Display(Name = "Product Image")]
        public string ProductImage { get; set; }

        //Relationships

        //Cayegory
       
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Categories { get; set; }

       //Unit

        public int UnitId { get; set; }
        [ForeignKey("UnitId")]
        public ProductUnit ProUnit { get; set; }

    }
}
