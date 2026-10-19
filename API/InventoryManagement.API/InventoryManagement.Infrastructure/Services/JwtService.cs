using InventoryManagement.Application.DTOs.CommandDTOs.AuthCommandDTOs;
using InventoryManagement.Application.Exceptions;
using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Interfaces.IServices;
using InventoryManagement.Core.Models;
using InventoryManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Services
{
    public class JwtService(IConfiguration configuration, UserManager<User> userManager) : IJwtService
    {
        private readonly string _secretKey = configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException("SecretKey is not configured!");
        public async Task<TokenResultModel> GenerateToken(Employee employee, CancellationToken cancellationToken)
        {
            //finding user in AspNetUsers table
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.EmployeeId == employee.Id, cancellationToken);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            var userRoles = await userManager.GetRolesAsync(user);
            if (userRoles.Count == 0)
                throw new InvalidOperationException("User has no assigned role");
            var userRole = userRoles.First();
            //configuring token options
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity([
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Role, userRole),
                            new Claim(ClaimTypes.Email, user.Email!)
                    ]),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(configuration["Jwt:TokenExpiryInMinutes"])),
                SigningCredentials = credentials,
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"]
            };
            var tokenHandler = new JsonWebTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new TokenResultModel() { Token = token, UserRole = userRole, ExpireAt = tokenDescriptor.Expires.Value };
        }
    }
}
