using KheyaShop.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KheyaShop.Data.ViewComponents
{
    public class SliderImage:ViewComponent
    {
        private readonly ISliderService _service;

        public SliderImage(ISliderService service)
        {
            _service = service;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _service.GetAllAsync();

            return View(result);
        }
    }

    
}
