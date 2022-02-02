using KheyaShop.Data.ViewModels;
using KheyaShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KheyaShop.Data.Services
{
    public class CategoriesService : ICategoriesService

    {
        private readonly AppDbContext _context;

        public CategoriesService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Category Category)
        {
            await _context.Categories.AddAsync(Category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(n => n.Id == id);
             _context.Categories.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var result = await _context.Categories.ToListAsync();
            return result;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(n => n.Id == id);
            return result;
        }

        public async Task<DropdownVM> GetNewDropdownValues()
        {
            var response = new DropdownVM()
            {
                Categories = await _context.Categories.OrderBy(n => n.ParentCategory).ToListAsync()
            };
            
            return response;
        }

        public async Task<Category> UpdateAsync(int id, Category newCategory)
        {
            newCategory.Id = id;
            _context.Categories.Update(newCategory);
            await _context.SaveChangesAsync();
            return newCategory;
        }
    }
}
