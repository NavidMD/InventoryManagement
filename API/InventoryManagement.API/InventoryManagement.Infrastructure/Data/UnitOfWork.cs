using InventoryManagement.Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Data
{
    public sealed class UnitOfWork(AppDbContext context) : IUnitOfWork, IAsyncDisposable
    {
        private IDbContextTransaction? _transaction;
        public async Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("تراکنش هنوز به پایان نرسیده است!");
            }
            _transaction = await context.Database.BeginTransactionAsync(isolationLevel);
        }

        public async Task CommitAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("تراکنش فعالی وجود ندارد!");
            }
            try
            {
                await _transaction.CommitAsync();
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction == null) 
                return;

            try
            {
                await _transaction.RollbackAsync();
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeTransactionAsync();
        }

        private async Task DisposeTransactionAsync()
        {
            var transaction = _transaction;
            _transaction = null;

            if (transaction is not null)
                await transaction.DisposeAsync();
        }

    }
}
