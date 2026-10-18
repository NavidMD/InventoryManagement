using InventoryManagement.Application._ٍExceptions;
using InventoryManagement.Application.DTOs.CommandDTOs.AuthCommandDTOs;
using InventoryManagement.Application.Exceptions;
using InventoryManagement.Core.Interfaces.IRepositories;
using InventoryManagement.Core.Interfaces.IServices;
using InventoryManagement.Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Commands.AuthCommands
{
    public record VerifyOtpCommand(VertificationRequestDTO SentOtp) : IRequest<VertificationResponseDTO>;
    public class VerifyOtpCommandHandler(IEmployeeRepository employeeRepository, IOtpRepository otpRepository, IOtpService otpService) : IRequestHandler<VerifyOtpCommand, VertificationResponseDTO>
    {
        public async Task<VertificationResponseDTO> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
        {
            //finding entered otp in db
            var enteredOtpHash = otpService.HashGeneratedOtp(request.SentOtp.OtpCode);
            var otpFoundByHash = await otpRepository.GetActiveOtpByHashAsync(enteredOtpHash);
            if (otpFoundByHash == null)
            {
                throw new InvalidOtpException();
            }
            //checking found otp validation
            var otpExpired = await otpService.CheckOtpExpiration(otpFoundByHash.Id);
            if (otpExpired)
            {
                //updating otp : deactivating expired otp
                await otpRepository.UpdateAsync(otpFoundByHash.Id, new OtpModel()
                {
                    Id = otpFoundByHash.Id,
                    IsActive = false,
                });

                throw new OtpExpiredException();
            }
            //user signed in successfully
            //updating otp : deactivating used otp
            await otpRepository.UpdateAsync(otpFoundByHash.Id, new OtpModel()
            {
                Id = otpFoundByHash.Id,
                IsActive = false,
                IsUsed = true,
            });
            //finding user to create access token
            var employee = await employeeRepository.GetByIdAsync(otpFoundByHash.EmployeeId);
            if (employee == null)
            {
                throw new EmployeeNotFoundException();
            }

        }
    }
}
