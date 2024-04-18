using KheyaShop.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KheyaShop.Data.ViewComponents
{
    public class CreateReview : ViewComponent
    {
        private readonly IProductsService _product;
        public CreateReview(IProductsService product)
        {
            _product = product;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.id = id;
            return View();
        }
    }
}
