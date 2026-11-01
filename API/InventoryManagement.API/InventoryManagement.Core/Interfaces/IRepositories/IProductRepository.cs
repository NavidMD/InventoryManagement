using InventoryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Core.Interfaces.IRepositories
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product product);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(Guid id);
        Task<Product?> UpdateAsync(Guid id, Product updatedProduct);
        Task<bool> DeleteAsync(Guid id);
        Task<int> DecreaseInventoryAsync(Guid productId, int decreaseCount, CancellationToken cancellationToken);
    }
}
