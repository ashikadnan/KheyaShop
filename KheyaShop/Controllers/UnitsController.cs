using KheyaShop.Data.Services;
using KheyaShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KheyaShop.Controllers
{
    public class UnitsController : Controller
    {
        private readonly IUnitService _service;

        public UnitsController(IUnitService service)
        {
            _service = service;
        }
        public async Task <IActionResult> Index()
        {

            var result = await _service.GetAllAsync();
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Create([Bind("Unit")] ProductUnit productUnit)
        {
            if (!ModelState.IsValid)
            {
                return View(productUnit);
            }
            await _service.AddAsync(productUnit);
            return RedirectToAction(nameof(Index));
        }

        public async Task <IActionResult> Edit(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if(result == null)
            {
                return View("NotFound");
            }
            
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Unit")] ProductUnit productUnit)
        {
            if (!ModelState.IsValid)
            {
                return View(productUnit);
            }
            await _service.UpdateAsync(id, productUnit);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var data = await _service.GetByIdAsync(id);
            if(data == null)
            {
                return View("NotFound");
            }

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
