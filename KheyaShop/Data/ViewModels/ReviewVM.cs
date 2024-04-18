using KheyaShop.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KheyaShop.Data.ViewModels
{
    public class ReviewVM
    {
        [Key]
        public int Id { get; set; }
        
        public string UserId { get; set; }
        [Display(Name ="Give a review")]
        [Required(ErrorMessage ="Please fill out the review form!")]
        public string ReviewText { get; set; }
        

    }
}
