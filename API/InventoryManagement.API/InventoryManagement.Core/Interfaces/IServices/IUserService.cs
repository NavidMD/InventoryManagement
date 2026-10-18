using InventoryManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Core.Interfaces.IServices
{
    public interface IUserService
    {
        Task<Guid> CreateUserAsync(Employee employee);
    }
}
