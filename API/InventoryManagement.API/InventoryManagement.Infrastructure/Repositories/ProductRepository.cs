using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Interfaces.IRepositories;
using InventoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext context;

        public ProductRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return product;
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await context.Products
                .Include(p => p.Inventory)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await context.Products
                .Include(p => p.Inventory)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product?> UpdateAsync(Guid id, Product updatedProduct)
        {
            var productFoundForUpdate = await context.Products
                .Include(p => p.Inventory)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (productFoundForUpdate != null)
            {
                productFoundForUpdate.Title = updatedProduct.Title;
                productFoundForUpdate.SerialNumber = updatedProduct.SerialNumber;
                productFoundForUpdate.CategoryId = updatedProduct.CategoryId;
                productFoundForUpdate.Inventory!.StorageBalance = updatedProduct.Inventory!.StorageBalance;
                await context.SaveChangesAsync();
                return productFoundForUpdate;
            }
            return null;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var rowsDeleted = await context.Products
                .Where(p => p.Id == id)
                .ExecuteDeleteAsync();
            return rowsDeleted > 0;
        }
    }
}
