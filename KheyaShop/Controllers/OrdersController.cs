using KheyaShop.Data.cart;
using KheyaShop.Data.Services;
using KheyaShop.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KheyaShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IProductsService _productService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;

        public OrdersController(IProductsService productService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _productService = productService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }

        public async Task <IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var orders = await _ordersService.GetOrderByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }
        public IActionResult ShoppingCartSummary()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVm()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()


            };
            
            return View(response);
        }

        public async Task <IActionResult> AddToShoppingCart(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product != null)
            {
                _shoppingCart.AddItemTocart(product);
            }

            return RedirectToAction(nameof(ShoppingCartSummary));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product != null)
            {
                _shoppingCart.RemoveItemFromCart(product);
            }

            return RedirectToAction(nameof(ShoppingCartSummary));
        }

        public async Task <IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();

            var productsforTop = await _productService.GetAllAsync();

            foreach(var item in items)
            {
                foreach(var product in productsforTop)
                {
                    if(item.Product.Id == product.Id)
                    {
                        await _productService.AddSoldProductAsync(product, item.amount);
                    }
                }  
            }  
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);
            await _ordersService.StoreOrdeAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }


    }
}
