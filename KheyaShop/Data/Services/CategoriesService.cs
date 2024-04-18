using KheyaShop.Data.ViewModels;
using KheyaShop.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KheyaShop.Data.Services
{
    public class CategoriesService : ICategoriesService

    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _context;
        
        public CategoriesService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task AddAsync(CategoriesVM Category)
        {
            string uniqueFileName = UploadedFile(Category);

            var newCategories = new Category()
            {
                ParentCategory = Category.ParentCategory,
                CategoryMain = Category.CategoryMain,   
                CategoryImage = uniqueFileName,
            };
            
            await _context.Categories.AddAsync(newCategories);
            await _context.SaveChangesAsync();
        }
        private string UploadedFile(CategoriesVM model)
        {
            string uniqueFileName = null;

            if (model.CategoryImageFile != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.CategoryImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CategoryImageFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
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

        public async Task<IEnumerable<Category>> GetCategoryByParentAsync(int id)
        {
            var ParentCategories = await _context.Categories.Where(n=> id.ToString() == n.ParentCategory).ToListAsync();   
            return ParentCategories;  
        }

        public async Task<DropdownVM> GetNewDropdownValues()
        {
            var response = new DropdownVM()
            {
                Categories = await _context.Categories.OrderBy(n => n.ParentCategory).ToListAsync()
            };
            
            return response;
        }

        public async Task<Category> UpdateAsync(int id, CategoriesVM newCategory)
        {
            string uniqueFileName = UploadedFiles(newCategory);
            var categories = await _context.Categories.FirstOrDefaultAsync(n => n.Id == id);
            if (categories != null)
            {
                categories.ParentCategory = newCategory.ParentCategory;
                categories.CategoryMain = newCategory.CategoryMain; 
                categories.CategoryImage = newCategory.CategoryImage;   
            }

            categories.Id = id;
            if(uniqueFileName!= null)
            {
                categories.CategoryImage = uniqueFileName;
            }

            _context.Categories.Update(categories);
            await _context.SaveChangesAsync();
            return categories;
        }

        private string UploadedFiles(CategoriesVM model)
        {
            string uniqueFileName = null;

            if (model.CategoryImageFile != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.CategoryImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CategoryImageFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
