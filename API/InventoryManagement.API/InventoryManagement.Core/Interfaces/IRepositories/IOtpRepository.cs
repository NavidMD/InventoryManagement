using InventoryManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Core.Interfaces.IRepositories
{
    public interface IOtpRepository
    {
        Task<OtpModel> CreateAsync(OtpModel otp);
        Task<OtpModel?> GetActiveOtpByEmployeeIdAsync(Guid employeeId);
        Task<OtpModel?> GetActiveOtpByIdAsync(Guid otpId);
        Task<OtpModel?> UpdateAsync(Guid otpId, OtpModel updatedOtp);
        Task<OtpModel?> GetActiveOtpByHashAsync(string hash);
        Task<OtpModel?> DeactivateOtp(OtpModel otp);
    }
}
