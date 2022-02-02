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
    public class ProductsService : IProductsService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }
       
        public async Task AddProductAsync(ProductsVM product)
        {
            string uniqueFileName = UploadedFile(product);
            var newProduct = new Product()
            {
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ProductUnitValue = product.ProductUnitValue,
                UnitId = product.UnitId,
                CategoryId = product.CategoryId,
                ProductSku = product.ProductSku,
                ProductQuantity = product.ProductQuantity,
                LongDesciption = product.LongDesciption,
                ShortDesciption = product.ShortDesciption,
                ProductImage = uniqueFileName
            };

            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
        }
        private string UploadedFile(ProductsVM model)
        {
            string uniqueFileName = null;

            if (model.ProductImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProductImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProductImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Products.FirstOrDefaultAsync(n => n.Id == id);
            _context.Products.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var data = await _context.Products
            .Include(c => c.Categories).Include(u => u.ProUnit).ToListAsync(); ;
            return data;
        }

        public async Task<ProductDropdownVM> GetDropdownValues()
        {
            var response = new ProductDropdownVM()
            {
                Categories = await _context.Categories.OrderBy(n => n.CategoryMain).ToListAsync(),
                ProductUnits = await _context.ProductUnit.OrderBy(n => n.Unit).ToListAsync()
        };
            return response;
        }


        public async Task<Product> GetProductByIdAsync(int id)
        {
            var data = await _context.Products
            .Include(c => c.Categories).Include(u => u.ProUnit).FirstOrDefaultAsync(n => n.Id == id);


            return data;
        }

       

        public async Task UpdateAsync(ProductsVM product)
        {

            string uniqueFileName = UploadedFiles(product);
            var dbProduct = await _context.Products.FirstOrDefaultAsync(n => n.Id == product.Id);
           
            if (dbProduct != null)
            {
                dbProduct.Id = product.Id;
                dbProduct.ProductName = product.ProductName;
                dbProduct.ProductPrice = product.ProductPrice;
                dbProduct.ProductUnitValue = product.ProductUnitValue;
                dbProduct.UnitId = product.UnitId;
                dbProduct.CategoryId = product.CategoryId;
                dbProduct.ProductSku = product.ProductSku;
                dbProduct.LongDesciption = product.LongDesciption;
                dbProduct.ShortDesciption = product.ShortDesciption;
                dbProduct.ProductImage = uniqueFileName;          
            }
            _context.Products.Update(dbProduct);
            await _context.SaveChangesAsync();
            //_context.Products.Update(dbProduct);

        }

        private string UploadedFiles(ProductsVM model)
        {
            string uniqueFileName = null;

            if (model.ProductImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProductImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProductImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }

}
