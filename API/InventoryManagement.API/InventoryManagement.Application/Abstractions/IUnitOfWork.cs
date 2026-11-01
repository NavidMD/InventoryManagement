using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Abstractions
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync(IsolationLevel isolationLevel);
        Task CommitAsync();
        Task RollbackAsync();
    }
}
