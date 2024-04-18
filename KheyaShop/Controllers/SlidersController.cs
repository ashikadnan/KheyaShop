using KheyaShop.Data.Services;
using KheyaShop.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KheyaShop.Controllers
{
    public class SlidersController : Controller
    {
        private readonly ISliderService _service;

        public SlidersController(ISliderService service)
        {
            _service = service;
        }
        
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SliderVM slider)
        {
            await _service.AddSliderSync(slider);
            return RedirectToAction(nameof(Index));
        }

        public async Task <IActionResult> Index()
        {
            var sliders = await _service.GetAllAsync();
            
            return View(sliders);
        }
    }
}
