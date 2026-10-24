using InventoryManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Core.Interfaces.IRepositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshTokenModel> CreateAsync(RefreshTokenModel token);
        Task<RefreshTokenModel?> UpdateAsync(Guid tokenId, DateTime revokeTime);
        Task<RefreshTokenModel?> GetActiveRefreshTokenByUserId(Guid userId);

    }
}
