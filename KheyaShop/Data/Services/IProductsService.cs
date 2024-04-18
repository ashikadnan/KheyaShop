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

        Task <IEnumerable<Product>> GetBrowseProductByIdAsync(int id);

        Task<IEnumerable<Product>> GetTopSoldItems();

        Task AddProductAsync(ProductsVM product);

        Task AddSoldProductAsync(Product product, int num);
        Task UpdateAsync(ProductsVM product);

        Task DeleteAsync(int id);

        Task<ProductDropdownVM> GetDropdownValues();

        Task<Product_Review> GetById(int id);
        Task AddReviewAsync(ReviewVM review, int id, string user);
        
    }
}
