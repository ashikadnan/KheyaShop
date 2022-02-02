using KheyaShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KheyaShop.Data.Services
{
    public class OrdersService : IOrdersService

    {
        private readonly AppDbContext _context;

        public OrdersService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetOrderByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders = await _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Product).Include(n=>n.User).ToListAsync();
            if (userRole != "Admin")
            {
                orders = await _context.Orders.Where(n => n.UserId == userId).ToListAsync();
            }
            return orders;
        }

        public async Task StoreOrdeAsync(List<ShoppingCartItems> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress,


            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach(var item in items)
            {
                var orderItem = new OrderItem()
                {
                    amount = item.amount,
                    ProductId = item.Product.Id,
                    OrderId = order.Id,
                    Price = item.Product.ProductPrice
                };
                await _context.OrderItems.AddAsync(orderItem);

            }
            await _context.SaveChangesAsync();
            
        }
    }
}
