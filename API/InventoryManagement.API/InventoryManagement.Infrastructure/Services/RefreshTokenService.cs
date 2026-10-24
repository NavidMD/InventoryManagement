using InventoryManagement.Application.Exceptions;
using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Interfaces.IRepositories;
using InventoryManagement.Core.Interfaces.IServices;
using InventoryManagement.Core.Models;
using InventoryManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Services
{
    public class RefreshTokenService(UserManager<User> userManager, IRefreshTokenRepository refreshTokenRepository) : IRefreshTokenService
    {
        public string GenerateRefreshToken()
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(randomBytes);
        }

        public bool CheckRefreshTokenExpiration(RefreshTokenModel refreshToken)
        {
            throw new NotImplementedException();
        }

        public async Task<RawRefreshTokenResultModel> HandleRefreshToken(Employee employee, CancellationToken cancellationToken)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.EmployeeId == employee.Id, cancellationToken);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            var userRefreshToken = await refreshTokenRepository.GetActiveRefreshTokenByUserId(user.Id);
            if (userRefreshToken != null)
            {
                //revoke previous active token 
                var revokeTime = DateTime.UtcNow;
                var revokeResult = await refreshTokenRepository.UpdateAsync(userRefreshToken.Id, revokeTime);
                if (revokeResult == null)
                {
                    throw new TokenOperationFailedException();
                }
            }

            //create new token
            var newRawToken = GenerateRefreshToken();
            var newHash = HashRefreshToken(newRawToken);
            var refreshToken = new RefreshTokenModel()
            {
                UserId = user.Id,
                RefreshTokenHash = newHash,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(2),
                RevokedAt = null,
            };
            var createResult = await refreshTokenRepository.CreateAsync(refreshToken);
            return new RawRefreshTokenResultModel() { RawRefreshToken = newRawToken, ExpiresAt = createResult.ExpiresAt };
        }

        public string HashRefreshToken(string rawToken)
        {
            var rawTokenBytes = Encoding.UTF8.GetBytes(rawToken);
            var hash = SHA256.HashData(rawTokenBytes);
            return Convert.ToBase64String(hash);
        }
    }
}
