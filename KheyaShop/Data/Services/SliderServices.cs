using KheyaShop.Data.ViewModels;
using KheyaShop.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace KheyaShop.Data.Services
{
    public class SliderServices : ISliderService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SliderServices(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task AddSliderSync(SliderVM slider)
        {
            string uniqueFileName = UploadedFile(slider);
            var sliders = new Slider()
            {
                SliderImage = uniqueFileName
            };

            await _context.AddAsync(sliders);
            await _context.SaveChangesAsync();      
        }

        public async Task<IEnumerable<Slider>> GetAllAsync()
        {
            var result = await _context.Sliders.ToListAsync();
            return result;
        }

        private string UploadedFile(SliderVM model)
        {
            string uniqueFileName = null;

            if (model.SliderImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.SliderImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.SliderImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }


    }
}
