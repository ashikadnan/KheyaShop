using KheyaShop.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KheyaShop.Data.ViewComponents
{
    public class BestSelling:ViewComponent
    {
        private readonly IProductsService _productsService;

        public BestSelling(IProductsService productsService)
        {
            _productsService = productsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _productsService.GetTopSoldItems();

            return View(categories);
        }
    }
}
