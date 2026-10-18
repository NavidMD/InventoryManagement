using InventoryManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Core.Interfaces.IServices
{
    public interface IOtpService
    {
        string GenerateOtp();
        Task<bool> CheckOtpExpiration(Guid otpId);
        string HashGeneratedOtp(string rawOtp);
    }
}
