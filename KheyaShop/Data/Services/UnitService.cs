using KheyaShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KheyaShop.Data.Services
{
    public class UnitService : IUnitService
    {
        private readonly AppDbContext _context;

        public UnitService(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task AddAsync(ProductUnit productUnit)
        {
             await _context.ProductUnit.AddAsync(productUnit);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.ProductUnit.FirstOrDefaultAsync(n => n.Id == id);
            _context.ProductUnit.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductUnit>> GetAllAsync()
        {
            var data = await _context.ProductUnit.ToListAsync();
            return data;
        }

        public async Task<ProductUnit> GetByIdAsync(int id)
        {
            var result = await _context.ProductUnit.FirstOrDefaultAsync(n => n.Id == id);
            return result;
        }

        public async Task <ProductUnit> UpdateAsync(int id, ProductUnit productUnit)
        {

            productUnit.Id = id;
            _context.ProductUnit.Update(productUnit);
            await _context.SaveChangesAsync();
            return productUnit;
        }
    }
}
