using KheyaShop.Data.Services;
using KheyaShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KheyaShop.Data.ViewComponents
{
    public class SingleProduct:ViewComponent
    {
        private readonly IProductsService _products;
        public SingleProduct(IProductsService products )
        {
            _products = products;   
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            
            var result = await _products.GetProductByIdAsync(id);
            return View(result);    
        }
    }
}
