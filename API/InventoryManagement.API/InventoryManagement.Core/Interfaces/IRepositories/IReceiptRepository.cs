using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Models;
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
        Task<IEnumerable<Receipt>> GetByEmployeeIdAsync(Guid employeeId);
        Task<IEnumerable<Receipt>> GetByDepartmentIdAsync(Guid departmentId);
        Task<IEnumerable<Receipt>> GetByProductIdAsync(Guid productId);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
