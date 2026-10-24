using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Core.Interfaces.IServices
{
    public interface IRefreshTokenService
    {
        string GenerateRefreshToken();
        string HashRefreshToken(string rawToken);
        Task<RawRefreshTokenResultModel> HandleRefreshToken(Employee employee, CancellationToken cancellationToken);
        bool CheckRefreshTokenExpiration(RefreshTokenModel refreshToken);
    }
}
