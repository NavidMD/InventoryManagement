using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Core.Interfaces.IServices
{
    public interface IJwtService
    {
        Task<TokenResultModel> GenerateToken(Employee employee, CancellationToken cancellationToken);

    }
}
