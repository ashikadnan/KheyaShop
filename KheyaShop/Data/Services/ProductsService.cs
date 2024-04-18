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
        private readonly ICategoriesService _categoriesService;

        public ProductsService(AppDbContext context, IWebHostEnvironment webHostEnvironment, ICategoriesService categoriesService)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _categoriesService = categoriesService;

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

            if (model.ProductImageFile != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProductImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProductImageFile.CopyTo(fileStream);
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
            .Include(c => c.Categories).Include(u => u.ProUnit).Include(x=>x.ProUnit).Include(x=>x.ProductReviewObj).ThenInclude(x=>x.Review).ThenInclude(x=>x.User).FirstOrDefaultAsync(n => n.Id == id);


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
                dbProduct.ProductImage = product.ProductImage;               
            }
       
            if (uniqueFileName != null)
            {
                dbProduct.ProductImage = uniqueFileName;
            }



            _context.Products.Update(dbProduct);
            await _context.SaveChangesAsync();
            //_context.Products.Update(dbProduct);

        }

        private string UploadedFiles(ProductsVM model)
        {
            string uniqueFileName = null;

            if (model.ProductImageFile != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProductImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProductImageFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public async Task<IEnumerable<Product>> GetBrowseProductByIdAsync(int id)
        {
            var category = await _categoriesService.GetByIdAsync(id);
            List<Product> result = new List<Product>();
            List<Product> final = new List<Product>();


            if (category.ParentCategory == "0")
            {
                var getCategories = await _categoriesService.GetCategoryByParentAsync(id);

                foreach (var cat in getCategories)
                {
                    result = await _context.Products.Include(c => c.Categories).Include(u => u.ProUnit).Where(n => n.CategoryId == cat.Id).ToListAsync();
                    final.AddRange(result);
                }              
                return final;

            }

            var data = await _context.Products.Include(c => c.Categories).Include(u => u.ProUnit).Where(n => n.CategoryId == id).ToListAsync();

            return data;

        }

        public async Task AddSoldProductAsync(Product product, int num)
        {
            var dbProduct = await _context.Products.FirstOrDefaultAsync(n => n.Id == product.Id);
            if(dbProduct != null)
            {
                dbProduct.soldNum += num;    
            }
            _context.Products.Update(dbProduct);
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Product>> GetTopSoldItems()
        {
            List<Product> topSold = new List<Product>();    
            var topSoldProducts = await _context.Products.Include(c => c.Categories).Include(u => u.ProUnit).OrderByDescending(n=>n.soldNum).ToListAsync();
            topSold = topSoldProducts.GetRange(0, 4);   
            return topSold;
        }

        public async Task<Product_Review> GetById(int id)
        {
            var result = await _context.Product_Reviews.FirstOrDefaultAsync(x=>x.ProductId == id);
            return result;
        }

        public async Task AddReviewAsync(ReviewVM review, int id, string user)
        {
            var reviewNew = new Review()
            {
                ReviewText = review.ReviewText,
                UserId = user,
                ProductId = id,


            };
            await _context.Reviews.AddAsync(reviewNew);
            await _context.SaveChangesAsync();

            var reviewProducts = new Product_Review()
            {
                ProductId = id,
                ReviewId = reviewNew.Id,
            };

            await _context.Product_Reviews.AddAsync(reviewProducts);
            await _context.SaveChangesAsync();

        }
    }

}
