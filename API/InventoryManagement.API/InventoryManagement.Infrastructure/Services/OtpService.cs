using InventoryManagement.Core.Interfaces.IRepositories;
using InventoryManagement.Core.Interfaces.IServices;
using InventoryManagement.Core.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Services
{
    public class OtpService(IConfiguration configuration, IOtpRepository otpRepository) : IOtpService
    {
        private readonly string _secretKey = configuration["OtpSettings:SecretKey"] ?? throw new InvalidOperationException("SecretKey is not configured!");

        public string GenerateOtp()
        {
            return RandomNumberGenerator.GetInt32(0, 1_000_000).ToString("D6");
        }

        public string HashGeneratedOtp(string otp)
        {
            byte[] secretKeyByte = Convert.FromBase64String(_secretKey);
            byte[] otpByte = Encoding.UTF8.GetBytes(otp);
            byte[] hash = HMACSHA256.HashData(secretKeyByte, otpByte);

            return Convert.ToHexString(hash);
        }

        public async Task<bool> CheckOtpExpiration(Guid otpId)
        {
            var otp = await otpRepository.GetActiveOtpByIdAsync(otpId);
            if (otp is null)
            {
                throw new KeyNotFoundException("OTP not found.");
            }
            return otp.ExpireTime <= DateTime.UtcNow;
        }
    }
}
