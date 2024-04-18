using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace KheyaShop.Data.ViewModels
{
    public class SliderVM
    {
        public int Id { get; set; }

        [Display(Name = "Slider Image")]
        public IFormFile SliderImage { get; set; }
    }
}
