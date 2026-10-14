using InventoryManagement.Application.DTOs.CommandDTOs.AuthCommandDTOs;
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
    public record LoginUserCommand(LoginRequestDTO loginData) : IRequest<LoginResponseDTO>;
    public class LoginUserCommandHandler(IEmployeeRepository employeeRepository, IOtpService otpService, IOtpRepository otpRepository, IEmailService emailService) : IRequestHandler<LoginUserCommand, LoginResponseDTO>
    {
        public async Task<LoginResponseDTO> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            //finding employee by personnel code and checking input email with employee email
            var employeeFoundByPersonnelCode = await employeeRepository.GetByPersonnelCode(request.loginData.PersonnelCode, cancellationToken);

            if (employeeFoundByPersonnelCode == null ||
                !String.Equals(employeeFoundByPersonnelCode.EmailAddress.Trim(),
                    request.loginData.EmailAddress.Trim(),
                    StringComparison.OrdinalIgnoreCase)
                )
            {
                return new LoginResponseDTO()
                {
                    OtpRequired = false,
                    Message = "اطلاعات وارد شده معتبر نمی باشد!"
                };
            }
            //checking database to see if there is an Active otp for this user
            var activeOtp = await otpRepository.GetActiveOtpByEmployeeIdAsync(employeeFoundByPersonnelCode.Id);
            if (activeOtp != null)
            {
                var otpExpired = await otpService.CheckOtpExpiration(activeOtp.Id);
                if (otpExpired)
                {
                    //updating otp : deactivating expired otp
                    var deactivatedOtp = new OtpModel()
                    {
                        Id = activeOtp.Id,
                        IsActive = false,
                    };
                    await otpRepository.UpdateAsync(activeOtp.Id, deactivatedOtp);
                }
                else
                {
                    return new LoginResponseDTO()
                    {
                        OtpRequired = true,
                        Message = "کد ورود یکبار مصرف قبلا ارسال شده است!"
                    };
                }
            }

            //generating new active otp and adding to database
            var newRawOtp = otpService.GenerateOtp();
            var newOtpChallenge = new OtpModel
            {
                OtpHash = otpService.HashGeneratedOtp(newRawOtp),
                EmployeeId = employeeFoundByPersonnelCode.Id,
                IsActive = true
            };
            await otpRepository.CreateAsync(newOtpChallenge);

            //creating email message for sending 
            var message = new EmailModel()
            {
                To = employeeFoundByPersonnelCode.EmailAddress,
                Subject = "کد تایید ورود",
                Body = $"""
                    <!DOCTYPE html>
                    <html lang="fa" dir="rtl">
                        <body style="margin:0; padding:0; background-color:#f3f6fb;
                                     font-family:Tahoma, Arial, sans-serif;">
                            <div style="max-width:600px; margin:30px auto;
                                        background-color:#ffffff; border-radius:16px;
                                        overflow:hidden; border:1px solid #e5eaf2;">
                                <div style="background-color:#155dfc; padding:24px;
                                            text-align:center; color:#ffffff;">
                                    <h2 style="margin:0; font-size:23px;">
                                        Inventory Management
                                    </h2>
                                    <p style="margin:8px 0 0; font-size:13px;">
                                        سامانه مدیریت انبار
                                    </p>
                                </div>
                                <div style="padding:32px 24px; text-align:center;
                                            color:#1e293b;">
                                    <h2 style="font-size:22px; margin:0 0 20px;">
                                        کد تأیید ورود به سیستم
                                    </h2>
                                    <p style="font-size:15px; line-height:2;
                                              text-align:right;">
                                        سلام،
                                        <br />
                                        برای تکمیل فرایند ورود به سامانه،
                                        از کد تأیید زیر استفاده کنید.
                                    </p>
                                    <div style="margin:28px 0; padding:22px 12px;
                                                background-color:#eff6ff;
                                                border:1px dashed #155dfc;
                                                border-radius:12px;">
                                        <p style="margin:0 0 12px; color:#64748b;
                                                  font-size:14px;">
                                            کد یک‌بارمصرف شما
                                        </p>
                                        <div dir="ltr"
                                             style="font-size:36px; font-weight:bold;
                                                    letter-spacing:8px; color:#155dfc;">
                                            {newRawOtp}
                                        </div>
                                    </div>
                                    <p style="font-size:14px; line-height:2;">
                                        این کد تا
                                        <strong style="color:#155dfc;">۵ دقیقه</strong>
                                        معتبر است.
                                    </p>
                                    <div style="margin-top:24px; padding:16px;
                                                background-color:#fff7ed;
                                                border-radius:8px; text-align:right;
                                                font-size:13px; line-height:2;
                                                color:#9a3412;">
                                        اگر شما درخواست ورود نداده‌اید،
                                        این ایمیل را نادیده بگیرید و کد را
                                        در اختیار دیگران قرار ندهید.
                                    </div>
                                </div>
                                <div style="padding:20px; text-align:center;
                                            background-color:#f8fafc;
                                            color:#94a3b8; font-size:12px; line-height:2;">
                                    این ایمیل به‌صورت خودکار ارسال شده است.
                                    <br />
                                    © Inventory Management
                                </div>
                            </div>
                        </body>
                    </html>
                 """
            };
            //sending email to user
            await emailService.SendEmailAsync(message, cancellationToken);

            return new LoginResponseDTO()
            {
                OtpRequired = true,
                Message = "کد ورود یکبار مصرف برای ایمیل شما ارسال شد"
            };
        }
    }
}
