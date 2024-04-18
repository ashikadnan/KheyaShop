using KheyaShop.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KheyaShop.Data.ViewComponents
{
    public class BrowseCategories:ViewComponent
    {
        private readonly ICategoriesService _categoriesService;

        public BrowseCategories(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoriesService.GetAllAsync();
            
            return View(categories);  
        }


    }
}
