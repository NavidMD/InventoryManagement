using InventoryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Core.Interfaces.IRepositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> CreateAsync(Employee employee);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(Guid id);
        Task<Employee?> UpdateAsync(Guid id, Employee updatedEmployee);
        Task<bool> DeleteAsync(Guid id);
    }
}
