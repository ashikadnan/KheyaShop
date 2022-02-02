using KheyaShop.Data.ViewModels;
using KheyaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KheyaShop.Data.Services
{
    public interface IProductsService
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(ProductsVM product);
        Task UpdateAsync(ProductsVM product);

        Task DeleteAsync(int id);

        Task<ProductDropdownVM> GetDropdownValues();
        
    }
}
