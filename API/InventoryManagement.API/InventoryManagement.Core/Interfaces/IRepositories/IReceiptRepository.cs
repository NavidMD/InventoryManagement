using InventoryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Core.Interfaces.IRepositories
{
    public interface IReceiptRepository
    {
        Task<Receipt> CreateAsync(Receipt receipt, CancellationToken cancellationToken);
        Task<IEnumerable<Receipt>> GetAllAsync();
        Task<Receipt?> GetByIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
