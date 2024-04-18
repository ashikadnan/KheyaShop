using KheyaShop.Data.ViewModels;
using KheyaShop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KheyaShop.Data.Services
{
    public interface ISliderService
    {
        Task AddSliderSync(SliderVM slider);
        Task<IEnumerable<Slider>> GetAllAsync();
    }
}
