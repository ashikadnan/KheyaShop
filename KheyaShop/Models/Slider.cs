using System.ComponentModel.DataAnnotations;

namespace KheyaShop.Models
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }

        public string SliderImage { get; set; }
    }
}
