using InventoryManagement.Application._ٍExceptions;
using InventoryManagement.Application.DTOs.CommandDTOs.AuthCommandDTOs;
using InventoryManagement.Application.Exceptions;
using InventoryManagement.Core.Interfaces.IRepositories;
using InventoryManagement.Core.Interfaces.IServices;
using InventoryManagement.Core.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Commands.AuthCommands
{
    public record VerifyOtpCommand(VertificationRequestDTO SentOtp) : IRequest<VertificationResponseDTO>;
    public class VerifyOtpCommandHandler(
        IEmployeeRepository employeeRepository,
        IOtpRepository otpRepository,
        IOtpService otpService,
        IJwtService jwtService,
        IRefreshTokenService refreshTokenService) : IRequestHandler<VerifyOtpCommand, VertificationResponseDTO>
    {
        public async Task<VertificationResponseDTO> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
        {
            //finding otp in db - checking expiration
            var enteredOtpHash = otpService.HashGeneratedOtp(request.SentOtp.OtpCode);
            var otpFoundByHash = await otpRepository.GetActiveOtpByHashAsync(enteredOtpHash);
            if (otpFoundByHash == null)
            {
                throw new InvalidOtpException();
            }
            var otpExpired = await otpService.CheckOtpExpiration(otpFoundByHash.Id);
            if (otpExpired)
            {
                //updating otp : deactivating expired otp
                await otpRepository.DeactivateOtp(new OtpModel()
                {
                    Id = otpFoundByHash.Id,
                    IsActive = false
                });

                throw new OtpExpiredException();
            }
            //user signed in successfully
            //updating otp : deactivating used otp - finding user to create access token
            await otpRepository.DeactivateOtp(new OtpModel()
            {
                Id = otpFoundByHash.Id,
                IsActive = false,
                IsUsed = true
            });

            var employee = await employeeRepository.GetByIdAsync(otpFoundByHash.EmployeeId);
            if (employee == null)
            {
                throw new EmployeeNotFoundException();
            }
            //generating access token + refresh token
            var refreshToken = await refreshTokenService.HandleRefreshToken(employee, cancellationToken);
            var accessToken = await jwtService.GenerateToken(employee, cancellationToken);
            return new VertificationResponseDTO()
            {
                AccessToken = new AccessTokenResultModel()
                {
                    EmployeeId = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Role = accessToken.UserRole,
                    AccessToken = accessToken.Token,
                    ExpiresAt = accessToken.ExpireAt,
                },
                RefreshToken = new RawRefreshTokenResultModel()
                {
                    RawRefreshToken = refreshToken.RawRefreshToken,
                    ExpiresAt = refreshToken.ExpiresAt
                }
            };
        }
    }
}
