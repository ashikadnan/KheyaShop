using KheyaShop.Data.ViewModels;
using KheyaShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace KheyaShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
          return View();    
        }

       /* private List<LoginVM> GetLoginVM()
        {
            return new List<LoginVM>(); 
        }

        private List<RegisterVM> GetRegisterVM()
        {
            return new List<RegisterVM>();
        }

        private List<Product> GetProduct()
        {
            return new List<Product>();
        }

        private List<Category> GetCategory()
        {
            return new List<Category>();    
        }*/

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
