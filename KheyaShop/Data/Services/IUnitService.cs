using KheyaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KheyaShop.Data.Services
{
    public interface IUnitService
    {
        Task<IEnumerable<ProductUnit>> GetAllAsync();
        Task<ProductUnit> GetByIdAsync(int id);
        Task AddAsync(ProductUnit productUnit);

        Task<ProductUnit> UpdateAsync(int id, ProductUnit productUnit);
        Task DeleteAsync(int id);
    }
}
