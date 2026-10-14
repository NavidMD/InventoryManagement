using InventoryManagement.Core.Interfaces.IRepositories;
using InventoryManagement.Core.Models;
using InventoryManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Repositories
{
    public class OtpRepository : IOtpRepository
    {
        private readonly AuthDbContext authDbContext;

        public OtpRepository(AuthDbContext authDbContext)
        {
            this.authDbContext = authDbContext;
        }

        public async Task<OtpModel> CreateAsync(OtpModel otp)
        {
            authDbContext.OtpChallenges.Add(otp);
            await authDbContext.SaveChangesAsync();
            return otp;
        }

        public async Task<OtpModel?> GetActiveOtpByEmployeeIdAsync(Guid employeeId)
        {
            return await authDbContext.OtpChallenges
                .Where(o => o.IsActive == true)
                .FirstOrDefaultAsync(otp => otp.EmployeeId == employeeId);
        }

        public async Task<OtpModel?> GetActiveOtpByIdAsync(Guid otpId)
        {
            return await authDbContext.OtpChallenges
                .Where(o => o.IsActive == true)
                .FirstOrDefaultAsync(otp => otp.Id == otpId);
        }

        public async Task<OtpModel?> UpdateAsync(Guid otpId, OtpModel updatedOtp)
        {
            var otpFoundForUpdate = await authDbContext.OtpChallenges.FirstOrDefaultAsync(o => o.Id  == otpId);
            if (otpFoundForUpdate != null)
            {
                otpFoundForUpdate.IsActive = updatedOtp.IsActive;
                await authDbContext.SaveChangesAsync();
                return otpFoundForUpdate;
            }
            return null;
        }
    }
}
