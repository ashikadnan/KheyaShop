using KheyaShop.Data.ViewModels;
using KheyaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KheyaShop.Data.Services
{
    public interface ICategoriesService
    {
        Task <IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task AddAsync(Category Category);
        Task<Category> UpdateAsync(int id, Category newCategory);
        Task DeleteAsync(int id);
        Task<DropdownVM> GetNewDropdownValues();


    }
}
