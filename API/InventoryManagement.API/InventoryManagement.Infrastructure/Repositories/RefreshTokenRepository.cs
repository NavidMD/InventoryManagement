using InventoryManagement.Core.Interfaces.IRepositories;
using InventoryManagement.Core.Models;
using InventoryManagement.Infrastructure.Data;
using InventoryManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AuthDbContext authDbContext;

        public RefreshTokenRepository(AuthDbContext authDbContext)
        {
            this.authDbContext = authDbContext;
        }
        public async Task<RefreshTokenModel> CreateAsync(RefreshTokenModel token)
        {
            authDbContext.RefreshTokens.Add(token);
            await authDbContext.SaveChangesAsync();
            return token;
        }

        public async Task<RefreshTokenModel?> GetActiveRefreshTokenByUserId(Guid userId)
        {
            return await authDbContext.RefreshTokens
                .Where(t => t.ExpiresAt >= DateTime.UtcNow)
                .Where(t => t.RevokedAt == null)
                .FirstOrDefaultAsync(t => t.UserId == userId);
        }

        public async Task<RefreshTokenModel?> UpdateAsync(Guid tokenId, DateTime revokeTime)
        {
            var tokenFoundForUpdate = await authDbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Id == tokenId);
            if (tokenFoundForUpdate != null)
            {
                tokenFoundForUpdate.RevokedAt = revokeTime;
                await authDbContext.SaveChangesAsync();
                return tokenFoundForUpdate;
            }
            return null;
        }
    }
}
