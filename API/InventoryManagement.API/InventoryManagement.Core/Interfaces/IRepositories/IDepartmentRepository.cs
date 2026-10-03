using InventoryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Core.Interfaces.IRepositories
{
    public interface IDepartmentRepository
    {
        Task<Department> CreateAsync(Department department);
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(Guid id);
        Task<Department?> UpdateAsync(Guid id, Department updatedDepartment);
        Task<bool> DeleteAsync(Guid id);
    }
}
