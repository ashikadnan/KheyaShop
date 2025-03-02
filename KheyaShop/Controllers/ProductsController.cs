﻿using KheyaShop.Data.Services;
using KheyaShop.Data.ViewModels;
using KheyaShop.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace KheyaShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;



        public ProductsController(IProductsService service, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;



        }

        public async Task<IActionResult> Index()
        {
            var allProducts = await _service.GetAllAsync();

            return View(allProducts);
        }

        public IActionResult SingleProduct(int id)
        {
            ViewBag.id = id;
            return View("SingleProduct");
        }
             
        public async Task<IActionResult> Create()
        {
            var dropdownData = await _service.GetDropdownValues();
            ViewBag.Categories = new SelectList(dropdownData.Categories, "Id", "CategoryMain");
            ViewBag.ProductUnits = new SelectList(dropdownData.ProductUnits, "Id", "Unit");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductsVM product)
        {
            if (!ModelState.IsValid)
            {
                var dropdownData = await _service.GetDropdownValues();
                ViewBag.Categories = new SelectList(dropdownData.Categories, "Id", "CategoryMain");
                ViewBag.ProductUnits = new SelectList(dropdownData.ProductUnits, "Id", "Unit");
                return View(product);
            }

            await _service.AddProductAsync(product);
            return RedirectToAction(nameof(Index));
        }

       


        public async Task<IActionResult> Edit(int id)
        {

            
            var data = await _service.GetProductByIdAsync(id);
            if (data == null)
            {
                return View("NotFound");
            }
            
            var response = new ProductsVM()
            {
                Id = data.Id,
                ProductName = data.ProductName,
                ProductPrice = data.ProductPrice,
                ProductUnitValue = data.ProductUnitValue,
                UnitId = data.UnitId,
                CategoryId = data.CategoryId,
                ProductSku = data.ProductSku,
                LongDesciption = data.LongDesciption,
                ShortDesciption = data.ShortDesciption,
                ProductImage = data.ProductImage
                
            };

            var dropdownData = await _service.GetDropdownValues();
            ViewBag.Categories = new SelectList(dropdownData.Categories, "Id", "CategoryMain");
            ViewBag.ProductUnits = new SelectList(dropdownData.ProductUnits, "Id", "Unit");
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductsVM product)
        {
            if (!ModelState.IsValid)
            {
                var dropdownData = await _service.GetDropdownValues();
                ViewBag.Categories = new SelectList(dropdownData.Categories, "Id", "CategoryMain");
                ViewBag.ProductUnits = new SelectList(dropdownData.ProductUnits, "Id", "Unit");
                return View(product);
            }

            await _service.UpdateAsync(product);
            return RedirectToAction(nameof(Index));
        }


        public async Task <IActionResult> Details(int id)
        {
            var result = await _service.GetProductByIdAsync(id);
            if (result == null)
            {
                return View("NotFound");
            }

            return View(result);
        }
        public async Task <IActionResult> Delete(int id)
        {
            var result = await _service.GetProductByIdAsync(id);
            if (result == null)
            {
                return View("NotFound");
            }
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
            
        }

        public async Task <IActionResult> AllProducts()
        {
            var allProducts = await _service.GetAllAsync();

            return View(allProducts);
        }

        public async Task<IActionResult> GetBrowseProduct(int id)
        {
            var result = await _service.GetBrowseProductByIdAsync(id);

            return View(result);
        }

        /*public async Task<IActionResult> GetTopSoldProduct()
        {
            var tProduct = await _service.GetTopSoldItems();

            return View(tProduct);
        }*/

        [HttpPost]
        public async Task<IActionResult> AddReview(ReviewVM review, int id)
        {
            
            var exUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
           
                await _service.AddReviewAsync(review, id, exUser);
            

            return RedirectToAction("SingleProduct", new { id = id });
            
            
        }




    }
}
