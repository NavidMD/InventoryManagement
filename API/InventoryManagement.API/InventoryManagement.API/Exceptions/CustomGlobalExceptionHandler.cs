using InventoryManagement.Application._ٍExceptions;
using InventoryManagement.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using ApplicationException = InventoryManagement.Application.Exceptions.ApplicationException;

namespace InventoryManagement.API.Exceptions
{
    public class CustomGlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            int statusCode;
            string errorCode;
            string message = exception.Message;
            switch (exception)
            {
                case DuplicateUserException:
                    statusCode = StatusCodes.Status409Conflict;
                    break;
                case EmployeeNotFoundException or UserNotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    break;
                case InvalidOtpException or OtpExpiredException:
                    statusCode = StatusCodes.Status400BadRequest;
                    break;
                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    message = "خطای غیر منتظره ای رخ داده است!";
                    break;
            }
            if (exception is ApplicationException applicationException)
            {
                errorCode = applicationException.ErrorCode;
            }
            else
            {
                errorCode = "INTERNAL_SERVER_ERROR";
            }

            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(
                new
                {
                    errorCode,
                    message
                },
                cancellationToken
            );
            return true;
        }
    }
}
