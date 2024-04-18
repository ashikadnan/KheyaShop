using KheyaShop.Data;
using KheyaShop.Data.Services;
using KheyaShop.Data.ViewModels;
using KheyaShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KheyaShop.Controllers
{
    public class CategoriesController : Controller
    {
        
        private readonly ICategoriesService _service;

        public CategoriesController(ICategoriesService service)
        {
            _service = service;
        }

        public async Task <IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            var categoryDropdownsData = await _service.GetNewDropdownValues();

            ViewBag.Categories = new SelectList(categoryDropdownsData.Categories, "Id", "CategoryMain");
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Create([Bind("ParentCategory,CategoryMain", "CategoryImage")] CategoriesVM category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            await _service.AddAsync(category);
            return RedirectToAction(nameof(Index));
        }

        public async Task <IActionResult> Edit(int id)
        {
            var categoryResult = await _service.GetByIdAsync(id);

            if (categoryResult == null)
            {
                return View("NotFound");
            }

            var response = new CategoriesVM()
            {
                CategoryMain = categoryResult.CategoryMain, 
                ParentCategory = categoryResult.ParentCategory, 
                CategoryImage = categoryResult.CategoryImage,   
            };
            
            var categoryDropdownsData = await _service.GetNewDropdownValues();
            ViewBag.Categories = new SelectList(categoryDropdownsData.Categories, "Id", "CategoryMain");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoriesVM category)
        {
           
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            

            await _service.UpdateAsync(id, category);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null)
            {
                return View("NotFound");
            }

            return View(actorDetails);

        }

        [HttpPost, ActionName("Delete")]
        public async Task <IActionResult> DeleteConfirmed(int id)
        {
            var categoryDetails = await _service.GetByIdAsync(id);
            if (categoryDetails == null)
            {
                return View("NotFound");
            }

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
                
        }

        



    }
}
